Public Class wb_Rohstoffe_SiloKorrektur

    Private _KompNr As Integer
    Private _KompNummer As String
    Private _KompBezeichnung As String
    Private _LagerOrt As String
    Private _SiloNr As Integer
    Private _Istmenge As Double

    Public Sub CopyFrom(Silo As wb_Silo)
        KompNr = Silo.KompNr
        KompNummer = Silo.KompNummer
        KompBezeichnung = Silo.KompBezeichnung
        LagerOrt = Silo.LagerOrt
        SiloNr = Silo.SiloNr
        Istmenge = Silo.IstMenge
    End Sub

    Public Property KompNr As Integer
        Get
            Return _KompNr
        End Get
        Set(value As Integer)
            _KompNr = value
        End Set
    End Property

    Public Property KompNummer As String
        Get
            Return _KompNummer
        End Get
        Set(value As String)
            _KompNummer = value
            lblNummer.Text = "Nr. " & value
        End Set
    End Property

    Public Property KompBezeichnung As String
        Get
            Return _KompBezeichnung
        End Get
        Set(value As String)
            _KompBezeichnung = value
            lblName.Text = value
        End Set
    End Property

    Public Property LagerOrt As String
        Get
            Return _LagerOrt
        End Get
        Set(value As String)
            _LagerOrt = value
        End Set
    End Property

    Public Property SiloNr As Integer
        Get
            Return _SiloNr
        End Get
        Set(value As Integer)
            _SiloNr = value
            lblName.Text = "Silo " & value.ToString
        End Set
    End Property

    Public Property Istmenge As Integer
        Get
            Return _Istmenge
        End Get
        Set(value As Integer)
            _Istmenge = value
            tbIst.Text = _Istmenge & " kg"
            'Textfeld rot anzeigen wenn Füllstand negativ
            If _Istmenge >= 0 Then
                tbIst.ForeColor = System.Drawing.Color.Black
            Else
                tbIst.BackColor = tbIst.BackColor
                tbIst.ForeColor = System.Drawing.Color.Red
            End If
        End Set
    End Property

    Public Property MengeNeu As Integer
        Get
            Return wb_Functions.StrToInt(tbBestNeu.Text)
        End Get
        Set(value As Integer)
            tbBestNeu.Text = value.ToString & " kg"
        End Set
    End Property

    Public Sub New(Silo As wb_Silo)
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        CopyFrom(Silo)
    End Sub

    Private Sub wb_Rohstoffe_SiloParameter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Fenster-Text
        Me.Text = "Bestandskorrektur Silo " & SiloNr.ToString & " - " & KompNummer & " " & KompBezeichnung
    End Sub

    'Private Sub tbBestNeu_TextChanged(sender As Object, e As EventArgs) Handles tbBestNeu.TextChanged
    '    tbBestNeu.Text = tbBestNeu.Text.ToString & " kg"
    'End Sub

    Private Sub BtnBestandKorrektur_Click(sender As Object, e As EventArgs) Handles BtnBestandKorrektur.Click
        'Datenbank-Verbindung öffnen - MySQL
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'TODO alle Lieferungen zu dieser Rohstoff-Nummer erfassen !!


        'Lieferungen zu diesem Rohstoff
        Dim Lieferungen As New wb_Lieferungen
        'Rohstoff-Chargen im Silo aus der winback.Lagerkarte ermitteln
        Dim RohstoffChargen As String = Lieferungen.GetChargenListe(winback, LagerOrt, MengeNeu)

        'In OrgaBack verbuchen
        If ob_Rohstoffe_SiloInventur.Bestandskorrektur(KompNummer, MengeNeu, RohstoffChargen) Then
            'Bestandskorrektur in OrgaBack war erfolgreich - Bestand in WinBack (Lagerkarte) anpassen

            'Datenbank-Verbindung öffnen OrgaBack-msSQL (notwendig um die letzte LfdNr. aus der Artikel-Lagerkarte zu ermitteln)
            Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
            'Das Lagerkarten-Objekt nimmt alle Daten aus dbo.ArtikelLagerkarte auf
            Dim LagerKarte As New wb_LagerKarte

            'alle Buchungen ausgehend von der letzten Buchung einlesen
            Dim Sql = wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlArtikelLagerInit, KompNummer)

            If orgaback.sqlSelect(Sql) Then
                'wenn Einträge vorhanden sind
                If orgaback.Read Then
                    'der letzte Eintrag enthält den aktuellen Lagerbestand
                    LagerKarte.msSQLdbRead(orgaback.msRead)
                    'Liste aller Chargen-Nummern
                    LagerKarte.ChargenNummer = RohstoffChargen

                    Debug.Print("aktueller Datensatz aus OrgaBack Lfd = " & LagerKarte.Lfd)
                    Lieferungen.InitBestand(winback, LagerKarte)
                End If
            End If
            'Datenbank-Verbindung wieder schliessen
            Me.Cursor = Windows.Forms.Cursors.Default
            orgaback.Close()

        Else
            'Fehlermeldung ausgeben
            Me.Cursor = Windows.Forms.Cursors.Default
            MsgBox("Fehler bei der Bestandskorrektur" & vbCrLf & "Die Bestandsdaten konnten nicht in die Lagerkarte übernommen werden", MsgBoxStyle.Critical, "Bestandskorrektur")
        End If

        'Datenbank-Verbindung wieder schliessen
        winback.Close()

    End Sub

    Private Sub BtnNullSetzen_Click(sender As Object, e As EventArgs) Handles BtnNullSetzen.Click
        'Neuer Bestand 0kg
        MengeNeu = 0
        'Bestandskorrektur durchführen
        BtnBestandKorrektur_Click(sender, e)
    End Sub
End Class