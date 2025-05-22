Imports System.Windows.Forms
Imports System.IO
Imports WeifenLuo.WinFormsUI.Docking


Public Class wb_Admin_TwinCatADS
    Inherits DockContent

    Dim SPS As wb_TwinCatSteuerung
    Dim KKA_BehNr As UInt16
    Dim KKA_BefXX As UInt16
    Dim KKA_BefChargenNr As String
    Dim KKA_Bilanz As UInt16
    Dim KKA_User As UInt32

    Dim TwinCatString As String
    Dim KompNummer As String

    Enum TCReadResult
        TCError
        TCNoData
        TCBarCode
        TCBefuell
        TCBilanz
        TCUser
    End Enum

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        'TwinCat nicht verbunden
        lblConnected.Visible = False

    End Sub

    Private Sub wb_Admin_TwinCatADS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ADS-Kommunikation aktiviert
        If wb_GlobalSettings.TwinCatADS Then
            'CheckBox
            cbTwinCatADSaktiv.Checked = True
            'neue SPS-Steuerung (CX auf Port 801)
            SPS = New wb_TwinCatSteuerung(wb_GlobalSettings.TwinCatADS_IP, wb_GlobalSettings.TwinCatADS_Port)
        End If
    End Sub

    Public Sub MySQLRunner()
        If wb_GlobalSettings.TwinCatADS Then

            'neue SPS-Steuerung (CX auf Port 801)
            SPS = New wb_TwinCatSteuerung(wb_GlobalSettings.TwinCatADS_IP, wb_GlobalSettings.TwinCatADS_Port)

            'TwinCat verbinden - KKA-Behälter-Nummer (Silo) einlesen (UINT16 auf %MB100)
            If TwinCatConnect() Then
                Select Case TwinCatRead()
                    Case TCReadResult.TCBarCode
                        'Lese Rohstoff zum Silo aus der WinBack-Datenbank (Nummer(16) Bezeichnung(60) EAN-Nummer(16))
                        MySQLDBRead(KKA_BehNr, TCReadResult.TCBarCode)
                        'Daten an die SPS schreiben (STRING(92) auf %MB200
                        TwinCatWrite(200, TwinCatString)
                    Case TCReadResult.TCBefuell
                        'Lieferungen nur wenn Anzahl Gebinde größer Null
                        If KKA_BefXX > 0 Then
                            'schreibe Lieferung in die WinBack-Datenbank (Nummer(16) AnzahlGebinde)
                            MySQLDBWrite(KKA_BehNr, KKA_BefXX, KKA_BefChargenNr, TCReadResult.TCBefuell)
                        End If
                    Case TCReadResult.TCBilanz
                        'Bilanzmengen aus der WinBack-Datenbank
                        MySQLDBRead(KKA_Bilanz, TCReadResult.TCBilanz)
                        'Daten an die SPS schreiben (STRING(92) auf %MB300
                        TwinCatWrite(300, TwinCatString)
                    Case TCReadResult.TCUser
                        'User-Rechte aus der WinBack-Datenbank
                        UserLogin(KKA_User)
                        'Daten an die SPS schreiben (STRING(10) auf %MB300
                        TwinCatWrite(400, TwinCatString)
                End Select
            End If

        End If
    End Sub

    Private Function MySQLDBRead(BehNr As Integer, TCSqlFunc As TCReadResult) As String
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'Komponenten-Bezeichnung Silo-Füllstand und EAN-Code aus DB einlesen
        If winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRohstoffKKA, BehNr.ToString)) AndAlso winback.Read Then
            Select Case TCSqlFunc
                Case TCReadResult.TCBarCode
                    KompNummer = winback.sField("KO_Nr_AlNum")
                    TwinCatString = KompNummer.PadLeft(16) & winback.sField("KO_Bezeichnung").PadLeft(60) & winback.sField("KA_Kurzname").PadLeft(16)
                Case TCReadResult.TCBilanz
                    TwinCatString = winback.sField("KO_Nr_AlNum").PadLeft(16) & winback.sField("LG_Bezeichnung").PadLeft(60) & winback.sField("LG_Bilanzmenge").PadLeft(10) & winback.sField("LG_Silo_Nr").PadLeft(2)
                Case TCReadResult.TCUser
                    TwinCatString = winback.sField("KO_Nr_AlNum").PadLeft(16) & winback.sField("LG_Bezeichnung").PadLeft(60) & winback.sField("LG_Bilanzmenge").PadLeft(10) & winback.sField("LG_Silo_Nr").PadLeft(2)
            End Select
        Else
            TwinCatString = "123456789".PadLeft(16) & "TestRohstoff".PadLeft(60) & "098765432EAN".PadLeft(16)
        End If

        winback.Close()
        Return TwinCatString
    End Function

    Private Function UserLogin(User As Integer) As String
        'Result vorbelegen
        TwinCatString = ""

        'Login User
        If wb_AktUser.Login(User) Then

            'Benutzer-Rechte Material befüllen (Tag32)
            If wb_AktRechte.RechtOK("132", False) Then
                TwinCatString &= "X"
            Else
                TwinCatString &= "-"
            End If

            'Benutzer-Rechte Service (Tag05)
            If wb_AktRechte.RechtOK("105", False) Then
                TwinCatString &= "X"
            Else
                TwinCatString &= "-"
            End If

            'Benutzer-Name
            TwinCatString &= wb_AktUser.UserName
        End If

        Return TwinCatString
    End Function


    Private Function MySQLDBWrite(BehNr As Integer, Gebinde As Integer, ChargenNummer As String, TCSqlFunc As TCReadResult) As Boolean
        Dim Result As Boolean = False

        'Wnn die Rohstoff-Nummer als Silo-Rohstoff vorhanden ist
        If TCSqlFunc = TCReadResult.TCBefuell AndAlso wb_Rohstoffe_Shared.RohSilos_NachNummer.ContainsKey(KompNummer) Then
            Dim SiloRohstoff As New wb_LagerSilo
            SiloRohstoff.CopyFrom(wb_Rohstoffe_Shared.RohSilos_NachNummer(KompNummer))

            'Prüfen ob die Silo-Nummer übereinstimmt
            If SiloRohstoff.SiloNr = BehNr Then
                SiloRohstoff.BefGebinde = Gebinde
                SiloRohstoff.ChargenNummer = ChargenNummer

                'Datensatz aus Komponenten/Lagerorte lesen - Lieferung verbuchen - Bilanzmenge aktualisieren
                Dim Lieferungen As New wb_Lieferungen
                Lieferungen.LieferungVerbuchen(SiloRohstoff)
                Lieferungen = Nothing
                Result = True
            End If
            SiloRohstoff = Nothing
        End If

        Return Result
    End Function

    Private Sub BtnConnect_Click(sender As Object, e As EventArgs) Handles BtnConnect.Click
        TwinCatConnect()
    End Sub

    Private Sub BtnRead_Click(sender As Object, e As EventArgs) Handles BtnRead.Click
        TwinCatRead()
        lblBehNr.Text = KKA_BehNr.ToString
        lblGebinde.Text = KKA_BefXX.ToString
        lblBilanz.Text = KKA_Bilanz.ToString
    End Sub

    Private Sub BtnDBRead_Click(sender As Object, e As EventArgs) Handles BtnDBRead.Click
        MySQLDBRead(lblBehNr.Text, TCReadResult.TCBarCode)
        lblTwinCatString.Text = TwinCatString
    End Sub
    Private Sub BtnDBReadBilanz_Click(sender As Object, e As EventArgs) Handles BtnDBReadBilanz.Click
        MySQLDBRead(lblBehNr.Text, TCReadResult.TCBilanz)
        lblTwinCatString_Bilanz.Text = TwinCatString
    End Sub

    Private Sub BtnWriteEAN_Click(sender As Object, e As EventArgs) Handles BtnWriteEAN.Click
        If TwinCatConnect() Then
            TwinCatString = "123456789".PadLeft(16) & "TestRohstoff".PadLeft(60) & "098765432EAN".PadLeft(16)
            TwinCatWrite(200, TwinCatString)
        End If
    End Sub
    Private Sub BtnWriteLager_Click(sender As Object, e As EventArgs) Handles BtnWriteLager.Click
        If TwinCatConnect() Then

            If wb_Rohstoffe_Shared.RohSilos_NachNummer.ContainsKey("0002629810") Then
                Dim Lieferungen As New wb_Lieferungen
                Dim SiloRohstoff As New wb_LagerSilo

                SiloRohstoff.CopyFrom(wb_Rohstoffe_Shared.RohSilos_NachNummer("0002629810"))
                SiloRohstoff.BefMenge = 1000

                'Datensatz aus Komponenten/Lagerorte lesen - Lieferung verbuchen - Bilanzmenge aktualisieren
                Lieferungen.LieferungVerbuchen(SiloRohstoff)

            End If
        End If
    End Sub

    Private Function TwinCatConnect() As Boolean
        If SPS.TryConnect Then
            lblConnected.Visible = True
            Return True
        Else
            Debug.Print("Keine Verbindung zur SPS-Steuerung " & SPS.Ip & "/" & SPS.Port)
            Return False
        End If
    End Function

    Private Function TwinCatRead() As TCReadResult
        Static Dim BehNr As UInt16
        Static Dim BefXX As UInt16
        Static Dim Bilanz As UInt16
        Static Dim UserNr As UInt32

        'Daten aus der SPS(ADS) lesen- die einzelnen Werte stehen nacheinander im Datenstrom
        If SPS.ReadMem(100, 60) Then
            'Behälter-Nummer -> Abfrage EAN-Code
            KKA_BehNr = SPS.TwinCatADS.BinReader.ReadUInt16()
            'Anzahl der Gebinde -> Bilanzmenge korrigieren
            KKA_BefXX = SPS.TwinCatADS.BinReader.ReadUInt16()
            'Behälter-Nummer -> Abfrage Bilanzmenge
            KKA_Bilanz = SPS.TwinCatADS.BinReader.ReadUInt16()
            'User-Nummer -> Abfrage Benutzer-Rechte
            KKA_User = SPS.TwinCatADS.BinReader.ReadInt32()
            'Chargen-Nummer -> Befüllung (maximal 20 Zeichen) - Null-Character werden entfernt
            KKA_BefChargenNr = SPS.TwinCatADS.BinReader.ReadChars(20)
            KKA_BefChargenNr = KKA_BefChargenNr.TrimEnd(vbNullChar)

            'nur wenn sich die Behälter-Nummer ändert wird auch aus der DB gelesen und an die SPS geschrieben
            If KKA_BehNr <> BehNr Then
                BehNr = KKA_BehNr
                Return TCReadResult.TCBarCode
            ElseIf KKA_BefXX <> BefXX Then
                BefXX = KKA_BefXX
                Return TCReadResult.TCBefuell
            ElseIf KKA_Bilanz <> Bilanz Then
                Bilanz = KKA_Bilanz
                Return TCReadResult.TCBilanz
            ElseIf KKA_User <> UserNr Then
                UserNr = KKA_User
                Return TCReadResult.TCUser
            Else
                Return TCReadResult.TCNoData
            End If
        Else
            Debug.Print("Fehler beim Lesen von SPS " & SPS.Ip & "/" & SPS.Port)
            Return TCReadResult.TCError
        End If
    End Function

    Private Sub TwinCatWrite(TcAdr As Int32, TcString As String)
        SPS.WriteMem(TcAdr, TcString)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UserLogin(709760)
    End Sub
End Class