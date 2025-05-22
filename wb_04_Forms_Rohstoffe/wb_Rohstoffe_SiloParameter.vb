Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting
Imports MySql.Data.MySqlClient

Public Class wb_Rohstoffe_SiloParameter

    Private _KompNr As Integer
    Private _KompNummer As String
    Private _KompBezeichnung As String
    Private _LagerOrt As String
    Private _RohSiloType As wb_Global.RohSiloTypen = wb_Global.RohSiloTypen.UNDEF
    Private _SiloNr As Integer
    Private _SiloListe As New List(Of wb_Silo)
    Private _Index As Integer = wb_Global.UNDEFINED

    Private _LinienSiloParameter(wb_Global.MaxWinBackLinien) As wb_Global.SiloParameter
    Private _LinienWaagenParameter(wb_Global.MaxWinBackLinien) As wb_Global.WaagenParameter

    Private WithEvents SiloParameter As wb_ArrayGridViewSiloParameter
    Private WithEvents WaagenParameter As wb_ArrayGridViewWaagenParameter

    Public Sub CopyFrom(Silo As wb_Silo)
        KompNr = Silo.KompNr
        KompNummer = Silo.KompNummer
        KompBezeichnung = Silo.KompBezeichnung
        LagerOrt = Silo.LagerOrt
        SiloNr = Silo.SiloNr
        RohSiloType = Silo.RohSiloType
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
        End Set
    End Property

    Public Property KompBezeichnung As String
        Get
            Return _KompBezeichnung
        End Get
        Set(value As String)
            _KompBezeichnung = value
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
        End Set
    End Property

    Public ReadOnly Property LinienSiloParameter As Array
        Get
            Return _LinienSiloParameter
        End Get
    End Property

    Public ReadOnly Property LinienWaagenParameter As Array
        Get
            Return _LinienWaagenParameter
        End Get
    End Property

    Public Property RohSiloType As wb_Global.RohSiloTypen
        Get
            Return _RohSiloType
        End Get
        Set(value As wb_Global.RohSiloTypen)
            _RohSiloType = value
        End Set
    End Property

    Public Sub New(Silo As wb_Silo)
        'Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        CopyFrom(Silo)
    End Sub

    Private Sub wb_Rohstoffe_SiloParameter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Tabelle SiloParameter
        DetailInfo_Silo()
        'Daten im Grid anzeigen
        SiloParameter = New wb_ArrayGridViewSiloParameter(LinienSiloParameter)
        SiloParameter.BackgroundColor = Me.BackColor
        SiloParameter.GridLocation(PnlSiloParameter)
        SiloParameter.PerformLayout()

        'Tabelle WaagenParameter
        DetailInfo_Waagen()
        'Daten im Grid anzeigen
        WaagenParameter = New wb_ArrayGridViewWaagenParameter(LinienWaagenParameter)
        WaagenParameter.BackgroundColor = Me.BackColor
        WaagenParameter.GridLocation(PnlWaagenParameter)
        WaagenParameter.PerformLayout()
        'Spaltenbreite an die Tabelle Silo-Parameter anpassen
        SiloParameterSizeChanged()

        'Liste aller Silos mit dieser Silo-Type (MW/MK/KKA)
        _SiloListe.Clear()
        Dim i As Integer = 0

        'alle Silos sortiert nach Silo-Nummer
        For Each x In wb_Rohstoffe_Shared.RohSilos_NachTyp
            If x.RohSiloType = RohSiloType Then
                i += 1
                _SiloListe.Add(x)
                If x.KompNr = KompNr Then
                    _Index = i
                End If
            End If
        Next

        'Schältflächen ein/ausblenden
        CheckIndex()
    End Sub

    ''' <summary>
    ''' Anzeige der Silo-Parameter des vorherigen Silos (Silo-Nummer minus Eins).
    ''' Wenn schon das Silo mit der niedrigsten Nummer angewählt ist, wird die Schaltfläche ausgeblendet
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnSiloVorher_Click(sender As Object, e As EventArgs) Handles BtnSiloVorher.Click
        'nächstes Silo
        _Index -= 1
        'Prüfen ob noch ein Silo in der Reihenfolge vorhanden ist, sonst wird die Schaltfläche ausgeblendet
        CheckIndex()

        'Silo-Parameter einlesen und aktualisieren
        CopyFrom(_SiloListe(_Index - 1))
        DetailInfo_Silo()
        SiloParameter.RefreshGrid(LinienSiloParameter)
        'Waagen-Parameter einlesen und aktualisieren
        DetailInfo_Waagen()
        WaagenParameter.RefreshGrid(LinienWaagenParameter)
        'Spaltenbreite an die Tabelle Silo-Parameter anpassen
        SiloParameterSizeChanged()
    End Sub

    ''' <summary>
    ''' Anzeige der Silo-Parameter des nächsten Silos (Silo-Nummer plus Eins)
    ''' Wenn schon das Silo mit der höchsten Nummer angewählt ist, wird die Schaltfläche ausgeblendet
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnSiloDanach_Click(sender As Object, e As EventArgs) Handles BtnSiloDanach.Click
        'nächstes Silo
        _Index += 1
        'Prüfen ob noch ein Silo in der Reihenfolge vorhanden ist, sonst wird die Schaltfläche ausgeblendet
        CheckIndex()

        'Silo-Daten aktualisiern (Rohstoff-Bezeichnung/Nummer...)
        CopyFrom(_SiloListe(_Index - 1))
        'Silo-Parameter einlesen und aktualisieren
        DetailInfo_Silo()
        SiloParameter.RefreshGrid(LinienSiloParameter)
        'Waagen-Parameter einlesen und aktualisieren
        DetailInfo_Waagen()
        WaagenParameter.RefreshGrid(LinienWaagenParameter)
        'Spaltenbreite an die Tabelle Silo-Parameter anpassen
        SiloParameterSizeChanged()
    End Sub

    ''' <summary>
    ''' Schaltflächen ein/ausblenden
    ''' </summary>
    Private Sub CheckIndex()
        BtnSiloVorher.Enabled = (_Index > 1)
        BtnSiloDanach.Enabled = (_Index < _SiloListe.Count)
    End Sub

    Private Sub DetailInfo_Silo()
        'Fenster-Text
        Me.Text = "Silo " & SiloNr.ToString & " Parameter "
        'Überschrift
        lblSiloParameter.Text = KompNummer & " " & KompBezeichnung & " - Silo " & SiloNr.ToString

        'Datenbank BCWegParams
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlgetSiloParams, KompNr))

        'Array Silo-Parameter initialisieren
        For i = 0 To wb_Global.MaxWinBackLinien
            _LinienSiloParameter(i).LinieNr = wb_Global.UNDEFINED
            _LinienSiloParameter(i).LinieAktiv = wb_Linien_Global.ProdLinien.ContainsKey(i)
        Next

        'Parameter für alle Linien zu diesem Rohstoff(Silo)
        While winback.Read

            'FieldCount-2 unterdrückt das Feld TimeStamp
            For i = 0 To winback.MySqlRead.FieldCount - 2
                Try
                    If winback.MySqlRead.GetDataTypeName(i) <> "TIMESTAMP" Then
                        MySQLdbRead_SiloParameter(winback.MySqlRead.GetName(i), winback.MySqlRead.GetValue(i))
                    End If
                Catch ex As Exception
                    Debug.Print("Exception MySQLdbReadStammdaten" & ex.Message)
                End Try
            Next
        End While

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Sub

    Private Sub DetailInfo_Waagen()
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlgetWaagenParams, KompNr))

        'Array Silo-Parameter initialisieren
        For i = 0 To wb_Global.MaxWinBackLinien
            _LinienWaagenParameter(i).WaageNr = wb_Global.UNDEFINED
            _LinienWaagenParameter(i).LinieNr = wb_Global.UNDEFINED
            _LinienWaagenParameter(i).LinieAktiv = wb_Linien_Global.ProdLinien.ContainsKey(i)
        Next

        'Parameter für alle Waagen zu diesem Rohstoff(Silo)
        While winback.Read

            'FieldCount-2 unterdrückt das Feld TimeStamp
            For i = 0 To winback.MySqlRead.FieldCount - 2
                Try
                    If winback.MySqlRead.GetDataTypeName(i) <> "TIMESTAMP" Then
                        MySQLdbRead_SiloParameter(winback.MySqlRead.GetName(i), winback.MySqlRead.GetValue(i))
                    End If
                Catch ex As Exception
                    Debug.Print("Exception MySQLdbReadStammdaten" & ex.Message)
                End Try
            Next
        End While

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Sub

    ''' <summary>
    ''' Schreibt den Wert aus Value in das entsprechende Feld im Array. 
    ''' </summary>
    ''' <param name="Name">String - Bezeichnung Datenbankfeld</param>
    ''' <param name="Value">Object - Wert Datenbankfeld(Inhalt)</param>
    ''' <returns></returns>
    Private Function MySQLdbRead_SiloParameter(Name As String, Value As Object) As Boolean
        Static LinieNr, WaageNr, ParamNr, ParamSatz As Integer

        'Try
        '    Debug.Print("ReadParameter/Value " & Name & "/" & Value)
        'Catch
        'End Try

        'Feldname aus der Datenbank
        Select Case Name

            'Linie-Nummer
            Case "W_Zuordnung"
                LinieNr = CInt(Value)

            'Waage-Nummer
            Case "WA_Nr"
                WaageNr = CInt(Value)

            'Parameter-Satz(Index aus Wege-Routen)
            Case "BCW_ParamSatz"
                ParamSatz = CInt(Value)

            'Parameter-Nummer
            Case "BCW_ParamNr"
                ParamNr = CInt(Value)

            'Parameter-Wert(Tabelle BCWegParams)
            Case "BCW_Wert"
                If LinieNr > 0 And LinieNr < wb_Global.MaxLinien Then
                    'Linie-Nummer (wenn gültig)
                    _LinienSiloParameter(LinieNr).LinieNr = LinieNr
                    'ParamSatz-Nummer (Index aus Wege-Routen)
                    _LinienSiloParameter(LinieNr).ParamSatz = ParamSatz

                    Select Case ParamNr
                        Case 2
                            'Nachlauf
                            _LinienSiloParameter(LinieNr).Nachlauf = wb_Functions.StrToDouble(Value)
                        Case 3
                            'Grob/Fein-Umschaltpunkt
                            _LinienSiloParameter(LinieNr).GrobFein = wb_Functions.StrToInt(Value)
                        Case 8
                            'Frequenz Grobstrom
                            _LinienSiloParameter(LinieNr).Frequenz_Grob = wb_Functions.StrToInt(Value)
                        Case 9
                            'Frequenz Feinstrom
                            _LinienSiloParameter(LinieNr).Frequenz_Fein = wb_Functions.StrToInt(Value)
                        Case 20
                            'Faktor kg/h
                            _LinienSiloParameter(LinieNr).Faktor_MengeZeit = wb_Functions.StrToDouble(Value)
                    End Select
                End If

            Case Else

                'Parameter Tabelle Waagen
                If WaageNr > 0 And WaageNr < wb_Global.MaxLinien Then

                    'Feldname aus der Datenbank Waagen
                    Select Case Name
                        Case "WA_Maximal_Dosierung"
                            _LinienWaagenParameter(WaageNr).WaagenGroesse = wb_Functions.StrToDouble(Value)
                        Case "WA_Foerderstrom_Ueberwachung"
                            _LinienWaagenParameter(WaageNr).FoerderStrom = wb_Functions.StrToDouble(Value)
                        Case "WA_Austragefunktion"
                            _LinienWaagenParameter(WaageNr).AustrageFkt = wb_Functions.StrToDouble(Value)
                        Case "WA_Anlaufzeit_Geblaese"
                            _LinienWaagenParameter(WaageNr).Anlauf_Transport = wb_Functions.StrToDouble(Value)
                        Case "WA_Nachlaufzeit_Geblaese"
                            _LinienWaagenParameter(WaageNr).Nachlauf_Transport = wb_Functions.StrToDouble(Value)
                        Case "WA_Zeit_Ruettler_Ein"
                            _LinienWaagenParameter(WaageNr).Ruettler_Ein = wb_Functions.StrToDouble(Value)
                        Case "WA_Pause_Ruettler_Aus"
                            _LinienWaagenParameter(WaageNr).Ruettler_Aus = wb_Functions.StrToDouble(Value)
                        Case "WA_Zeit_Austrageduesen_Ein"
                            _LinienWaagenParameter(WaageNr).Duesen_Ein = wb_Functions.StrToDouble(Value)
                        Case "WA_Pause_Austrageduesen_Aus"
                            _LinienWaagenParameter(WaageNr).Duesen_Aus = wb_Functions.StrToDouble(Value)
                        Case "WA_Zeit_Filterreinigung_Ein"
                            _LinienWaagenParameter(WaageNr).Filter_Ein = wb_Functions.StrToDouble(Value)
                        Case "WA_Pause_Filterreinigung_Aus"
                            _LinienWaagenParameter(WaageNr).Filter_Aus = wb_Functions.StrToDouble(Value)
                        Case "WA_Impuls_Klappe_Auf_Zu"
                            _LinienWaagenParameter(WaageNr).Impuls_Klappe = wb_Functions.StrToDouble(Value)
                        Case "WA_Anz_NK_Stellen"
                            _LinienWaagenParameter(WaageNr).Nachkomma = wb_Functions.StrToDouble(Value)
                        Case "WA_AK_Nr"
                            _LinienWaagenParameter(WaageNr).Analog_Kanal = wb_Functions.StrToDouble(Value)
                        Case "AK_BC9_Nr"
                            _LinienWaagenParameter(WaageNr).BC9000 = wb_Functions.StrToDouble(Value)
                        Case "AK_Param_Nr"
                            _LinienWaagenParameter(WaageNr).ParameterNr = wb_Functions.StrToDouble(Value)
                            'mit dem letzten Datenfeld wird auch die Linien/Waagen-Nummer übernommen
                            _LinienWaagenParameter(WaageNr).WaageNr = WaageNr
                            _LinienWaagenParameter(WaageNr).LinieNr = LinieNr
                    End Select
                End If
        End Select
        Return True
    End Function

    Private Sub SiloParameterSizeChanged() Handles SiloParameter.SizeChanged
        If WaagenParameter IsNot Nothing Then
            WaagenParameter.Columns(0).Width = SiloParameter.Columns(0).Width
        End If
    End Sub

    ''' <summary>
    ''' Zelle wurde geändert - Neuen Wert direkt in die Datenbank schreiben
    ''' Das Update-Statement wird dynamisch erzeugt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SiloParameter_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles SiloParameter.CellEndEdit
        'Spalte und Zeile
        Dim Row As Integer = e.RowIndex
        Dim Col As Integer = e.ColumnIndex
        'neuer Wert im Grid
        Dim Value As Double = wb_Functions.StrToDouble(SiloParameter.CurrentCell.Value)
        'Linie
        Dim Linie As Integer = SiloParameter.Rows(Row).HeaderCell.Value
        'Parameter-Satz in Datenbank-Tabelle winback.BCWegParams
        Dim ParamSatz As Integer = SiloParameter.Rows(0).Cells(Col).Tag
        'Parameter-Nummer in Datenbank-Tabelle winback.BCWegParams
        Dim ParamNr As Integer = SiloParameter.Rows(Row).Cells(0).Tag

        'Wert formatieren
        Select Case Row
            Case 0
                'Nachlauf - nur Werte zwischen 0..99
                Value = Math.Max(0, Math.Min(99.0, Value))
                'Formatiert auf 3 Nachkommastellen
                SiloParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 3)
                _LinienSiloParameter(Linie).Nachlauf = SiloParameter.CurrentCell.Value
            Case 1
                'Umschaltung Grob/Fein - nur Werte zwischen 0..99
                Value = Math.Max(0, Math.Min(99.0, Value))
                'Formatiert auf 3 Nachkommastellen
                SiloParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 3)
                _LinienSiloParameter(Linie).GrobFein = SiloParameter.CurrentCell.Value
            Case 2
                'Frequenz Grobstrom - nur Werte zwischen 0..999
                Value = Math.Max(0, Math.Min(999, Value))
                'Formatiert auf 0 Nachkommastellen
                SiloParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 0)
                _LinienSiloParameter(Linie).Frequenz_Grob = SiloParameter.CurrentCell.Value
            Case 3
                'Frequenz Feinstrom - nur Werte zwischen 0..999
                Value = Math.Max(0, Math.Min(999, Value))
                'Formatiert auf 0 Nachkommastellen
                SiloParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 0)
                _LinienSiloParameter(Linie).Frequenz_Fein = SiloParameter.CurrentCell.Value
            Case 4
                'Faktor Zeit/Menge - nur Werte zwischen 0..99
                Value = Math.Max(0, Math.Min(99.0, Value))
                'Formatiert auf 3 Nachkommastellen
                SiloParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 3)
                _LinienSiloParameter(Linie).Faktor_MengeZeit = SiloParameter.CurrentCell.Value
        End Select

        'Datenbank aktualisieren
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        If winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateSiloParams, SiloParameter.CurrentCell.Value, ParamNr.ToString, ParamSatz.ToString)) < 0 Then
            MsgBox("Fehler beim Aktualisieren der Silo-Parameter", MsgBoxStyle.Critical, "SiloParameter")
        End If
        winback.Close()
    End Sub

    ''' <summary>
    ''' Zelle wurde geändert - Neuen Wert direkt in die Datenbank schreiben
    ''' Das Update-Statement wird dynamisch erzeugt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub WaagenParameter_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles WaagenParameter.CellEndEdit
        'Spalte und Zeile
        Dim Row As Integer = e.RowIndex
        Dim Col As Integer = e.ColumnIndex
        'neuer Wert im Grid
        Dim Value As Double = wb_Functions.StrToDouble(WaagenParameter.CurrentCell.Value)
        'Waage
        Dim Waage As Integer = WaagenParameter.Columns(Col).HeaderCell.Value
        Dim ParameterName As String = WaagenParameter.Rows(Row).Cells(0).Tag

        Select Case Row
            Case 1
                'Waagengröße - nur Werte zwischen 0..9999
                Value = Math.Max(0, Math.Min(9999.0, Value))
                'Formatiert auf 1 Nachkommastellen
                WaagenParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 1)
                _LinienWaagenParameter(Waage).WaagenGroesse = WaagenParameter.CurrentCell.Value
            Case 2
                'Förderstromüberwachung - nur Werte zwischen 0..999
                Value = Math.Max(0, Math.Min(999, Value))
                'Formatiert auf 0 Nachkommastellen
                WaagenParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 0)
                _LinienWaagenParameter(Waage).FoerderStrom = WaagenParameter.CurrentCell.Value
            Case 3
                'Austragefunktion - nur Werte zwischen 0..9
                Value = Math.Max(0, Math.Min(9, Value))
                'Formatiert auf 0 Nachkommastellen
                WaagenParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 0)
                _LinienWaagenParameter(Waage).AustrageFkt = WaagenParameter.CurrentCell.Value
            Case 4
                'Anlauf Transportgebläse - nur Werte zwischen 0..999
                Value = Math.Max(0, Math.Min(999, Value))
                'Formatiert auf 0 Nachkommastellen
                WaagenParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 0)
                _LinienWaagenParameter(Waage).Anlauf_Transport = WaagenParameter.CurrentCell.Value
            Case 5
                'Nachlauf Transportgebläse - nur Werte zwischen 0..999
                Value = Math.Max(0, Math.Min(999, Value))
                'Formatiert auf 0 Nachkommastellen
                WaagenParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 0)
                _LinienWaagenParameter(Waage).Nachlauf_Transport = WaagenParameter.CurrentCell.Value
            Case 6
                'Rüttler tEin - nur Werte zwischen 0..99
                Value = Math.Max(0, Math.Min(99, Value))
                'Formatiert auf 0 Nachkommastellen
                WaagenParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 0)
                _LinienWaagenParameter(Waage).Ruettler_Ein = WaagenParameter.CurrentCell.Value
            Case 7
                'Rüttler tAus - nur Werte zwischen 0..99
                Value = Math.Max(0, Math.Min(99, Value))
                'Formatiert auf 0 Nachkommastellen
                WaagenParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 0)
                _LinienWaagenParameter(Waage).Ruettler_Aus = WaagenParameter.CurrentCell.Value
            Case 8
                'Düsen tEin - nur Werte zwischen 0..99
                Value = Math.Max(0, Math.Min(99, Value))
                'Formatiert auf 0 Nachkommastellen
                WaagenParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 0)
                _LinienWaagenParameter(Waage).Duesen_Ein = WaagenParameter.CurrentCell.Value
            Case 9
                'Düsen tAus - nur Werte zwischen 0..99
                Value = Math.Max(0, Math.Min(99, Value))
                'Formatiert auf 0 Nachkommastellen
                WaagenParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 0)
                _LinienWaagenParameter(Waage).Duesen_Aus = WaagenParameter.CurrentCell.Value
            Case 10
                'Filter tEin - nur Werte zwischen 0..99
                Value = Math.Max(0, Math.Min(99, Value))
                'Formatiert auf 0 Nachkommastellen
                WaagenParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 0)
                _LinienWaagenParameter(Waage).Filter_Ein = WaagenParameter.CurrentCell.Value
            Case 11
                'Filter tAus - nur Werte zwischen 0..99
                Value = Math.Max(0, Math.Min(99, Value))
                'Formatiert auf 0 Nachkommastellen
                WaagenParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 0)
                _LinienWaagenParameter(Waage).Filter_Aus = WaagenParameter.CurrentCell.Value
            Case 12
                'Impuls Klappe zu - nur Werte zwischen 0..99
                Value = Math.Max(0, Math.Min(99, Value))
                'Formatiert auf 0 Nachkommastellen
                WaagenParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 0)
                _LinienWaagenParameter(Waage).Impuls_Klappe = WaagenParameter.CurrentCell.Value
            Case 13
                'Nachkommastellen - nur Werte zwischen 0.3
                Value = Math.Max(0, Math.Min(3, Value))
                'Formatiert auf 0 Nachkommastellen
                WaagenParameter.CurrentCell.Value = wb_Functions.FormatStr(Value, 0)
                _LinienWaagenParameter(Waage).Nachkomma = WaagenParameter.CurrentCell.Value
        End Select

        'Datenbank aktualisieren
        If ParameterName <> "" Then
            Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
            If winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateWaagenParams, WaagenParameter.CurrentCell.Value, Waage.ToString, ParameterName.ToString)) < 0 Then
                MsgBox("Fehler beim Aktualisieren der Waagen-Parameter", MsgBoxStyle.Critical, "WaagenParameter")
            End If
            winback.Close()
        End If


    End Sub
End Class