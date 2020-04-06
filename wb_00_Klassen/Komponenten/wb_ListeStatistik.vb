Public Class wb_ListeStatistik
    Dim StatistikElementeGridView As wb_ArrayGridViewStatistik

    Private _ElementeArray As New ArrayList
    Private _ListeBerechnet As Boolean = False

    Public Event ListeAdd_Click(sender As Object, e As EventArgs)
    Public Event ListeBerechnet_Changed(sender As Object)

    Public Property ListeBerechnet As Boolean
        Get
            Return _ListeBerechnet
        End Get
        Set(value As Boolean)
            _ListeBerechnet = value
            RaiseEvent ListeBerechnet_Changed(Nothing)
        End Set
    End Property


    Private Sub wb_ListeStatistik_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        '   Spalten mit & dienen als Ausgleich der Breite
        '   Spalten mit # enthalten Datum/Uhrzeit-Angaben
        Dim sColNames As New List(Of String)
        sColNames.AddRange({"", "Nummer", "&Bezeichnung"})
        StatistikElementeGridView = New wb_ArrayGridViewStatistik(_ElementeArray, sColNames)
        'Tabelle darf editiert werden
        StatistikElementeGridView.ReadOnly = True
        'Tabelle nur ganze Zeile markieren
        StatistikElementeGridView.SelectionMode = Windows.Forms.DataGridViewSelectionMode.FullRowSelect

        'Tabelle (Leer) anzeigen
        StatistikElementeGridView.GridLocation(tpListe)
        StatistikElementeGridView.PerformLayout()

        'Default-Einstellungen Filtern bis (aktuelles Datum)
        dtFilterBis.Value = Now
        'Default-Einstellungen Filtern von (Anzeige aktuelles Jahr)
        dtFilterVon.Value = DateAdd(DateInterval.Year, -1, Now)

        'Liste muss neu berechnet werden
        ListeBerechnet = False
    End Sub

    ''' <summary>
    ''' Initialisierung der Drop-Down-Listen
    ''' Muss unbedingt ausserhalb des Load-Ereignisses erfolgen, sonst kann im Designer
    ''' kein Statistik-Fenster angezeigt werden
    ''' </summary>
    ''' <param name="StatistikType"></param>
    Public Sub InitGruppenListen(StatistikType As wb_Global.StatistikType)
        'Liste aller Linien
        cbLinien.Items.Clear()
        For Each Linie As wb_Global.wb_Linien In wb_Linien_Global.Linien
            cbLinien.Items.Add(Linie.Linie.ToString("  #") & " (" & Linie.Bezeichnung & ")")
        Next

        'Combo-Box(Rohstoff-Gruppe) mit Werten füllen
        cbRohstoffGrp1.Fill(wb_Rohstoffe_Shared.RohGruppe)
        cbRohstoffGrp2.Fill(wb_Rohstoffe_Shared.RohGruppe)
    End Sub

    ''' <summary>
    ''' Alle Listen initialisieren.
    ''' </summary>
    Public Sub InitAuswahlListen(StatistikType As wb_Global.StatistikType)

        'Combo-Box (Gruppe) ein/ausblenden
        gbRohGruppe.Visible = (StatistikType = wb_Global.StatistikType.StatistikRohstoffeDetails) Or (StatistikType = wb_Global.StatistikType.StatistikRohstoffeVerbrauch)

        'Filter Datum vom..bis aus winback.ini laden
        Dim IniFile As New wb_IniFile
        Dim IniRegn As String = GetIniSektion(StatistikType)

        'Daten aus Ini-File lesen
        dtFilterVon.Value = IniFile.ReadString(IniRegn, "FilterVon", DateAdd(DateInterval.Day, -1, Now))
        wb_Chargen_Shared.FilterVon = dtFilterVon.Value

        dtFilterBis.Value = IniFile.ReadString(IniRegn, "FilterBis", Now)
        wb_Chargen_Shared.FilterBis = dtFilterBis.Value

        'IniFile wieder freigeben
        IniFile = Nothing

    End Sub

    Public Sub SaveAuswahlListen(StatistikType As wb_Global.StatistikType)
        'Filter Datum vom..bis aus winback.ini laden
        Dim IniFile As New wb_IniFile
        Dim IniRegn As String = GetIniSektion(StatistikType)

        'Daten in ini-File schreiben
        IniFile.WriteString(IniRegn, "FilterVon", dtFilterVon.Value)
        IniFile.WriteString(IniRegn, "FilterBis", dtFilterBis.Value)

        'IniFile wieder freigeben
        IniFile = Nothing
    End Sub

    Private Function GetIniSektion(StatistikType As wb_Global.StatistikType) As String
        'abhängig von der Listen-Type
        Select Case StatistikType
            Case wb_Global.StatistikType.StatistikRezepte
                Return "StatistikRezepte"
            Case wb_Global.StatistikType.StatistikRohstoffeDetails
                Return "StatistikRohDetails"
            Case wb_Global.StatistikType.StatistikRohstoffeVerbrauch
                Return "StatistikRohVerbrauch"
            Case wb_Global.StatistikType.ChargenAuswertung
                Return "StatistikChargen"
            Case Else
                Return "UnDefined"
        End Select
    End Function

    ''' <summary>
    ''' Element e zur Liste hinzufügen. (Auswahl über Rohstoff/Rezeptliste)
    ''' </summary>
    ''' <param name="e"></param>
    Public Sub AddElement(e As wb_StatistikListenElement)
        _ElementeArray.Add(e)
        StatistikElementeGridView.FillGrid(_ElementeArray)
        'Statistik muss neu berechnet werden
        ListeBerechnet = False
    End Sub

    ''' <summary>
    ''' Alle Index-Nummern aus der Liste in ein eindimensionales Array schreiben
    ''' </summary>
    ''' <param name="x"></param>
    Public Sub GetElements(ByVal x As List(Of Integer))
        'Array löschen
        x.Clear()

        'Liste neu erzeugen
        If cbElementeAusListe.Checked Then
            'alle Elemente aus der Liste
            For Each e As wb_StatistikListenElement In _ElementeArray
                'Index in Array eintragen
                x.Add(e.Nr)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Liste aller Linien aus der CheckedListBox
    ''' </summary>
    ''' <param name="x"></param>
    Public Sub GetLinien(ByVal x As List(Of Integer))
        'Array löschen
        x.Clear()

        'Liste neu erzeugen
        If Not cbAlleLinien.Checked Then
            'alle Elemente aus der Liste
            For Each e In cbLinien.CheckedItems
                'Index in Array eintragen
                x.Add(wb_Functions.StrToInt(e.ToString))
            Next
        End If
    End Sub

    ''' <summary>
    ''' Auswahl-Fenster Rezepte/Rohstoffe aufrufen. Element zur Liste hinzufügen und anzeigen.
    ''' Der Aufruf des Auswahlfensters erfolgt eventgesteuert aus der jeweiligen Applikation (Rezept/Rohstoff)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnListeAdd_Click(sender As Object, e As EventArgs) Handles BtnListeAdd.Click
        RaiseEvent ListeAdd_Click(sender, e)
        'Check-Box "nur ausgewählte Elemente"
        cbElementeAusListe.Checked = True
        'Rohstoff-Gruppen leeren
        cbRohstoffGrp1.SelectedItem = Nothing
        cbRohstoffGrp2.SelectedItem = Nothing
    End Sub

    ''' <summary>
    ''' Markierte Zeile aus Liste entfernen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnListeRemove_Click(sender As Object, e As EventArgs) Handles BtnListeRemove.Click
        If StatistikElementeGridView.ColumnCount > 0 Then
            Dim idx As Integer = StatistikElementeGridView.SelectedRows(0).Index
            If idx > 0 Then
                _ElementeArray.RemoveAt(idx)
                StatistikElementeGridView.FillGrid(_ElementeArray)
            End If
        End If
        'Statistik muss neu berechnet werden
        ListeBerechnet = False
    End Sub

    ''' <summary>
    ''' Alle Elemente aus der Liste entfernen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnListeLeeren_Click(sender As Object, e As EventArgs) Handles BtnListeLeeren.Click
        DisableGridRohGruppe()
        _ElementeArray.Clear()
        StatistikElementeGridView.FillGrid(_ElementeArray)
        'Check-Box "nur ausgewählte Elemente"
        cbElementeAusListe.Checked = False

        'Statistik muss neu berechnet werden
        ListeBerechnet = False
    End Sub

    ''' <summary>
    ''' Alle Elemente der Liste aus einer Text-Datei laden. Vorher wird die Liste gelöscht.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnListeLaden_Click(sender As Object, e As EventArgs) Handles BtnListeLaden.Click
        Dim LoadListe As New System.Windows.Forms.OpenFileDialog
        LoadListe.Filter = "WinBack Statistik-Liste|.wsl"
        LoadListe.Title = "Liste laden"
        LoadListe.FileName = "*.wsl"
        LoadListe.InitialDirectory = wb_GlobalSettings.pTempPath

        If LoadListe.ShowDialog() Then
            If FileIO.FileSystem.FileExists(LoadListe.FileName) Then
                DisableGridRohGruppe()
                _ElementeArray.Clear()
                wb_Functions.ArrayRead(LoadListe.FileName, _ElementeArray)
                StatistikElementeGridView.FillGrid(_ElementeArray)
                'Check-Box "nur ausgewählte Elemente"
                cbElementeAusListe.Checked = True
            End If
        End If

        'Statistik muss neu berechnet werden
        ListeBerechnet = False
    End Sub

    ''' <summary>
    ''' Alle Elemente der Liste in eine Text-Datei speichern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnListeSpeichern_Click(sender As Object, e As EventArgs) Handles BtnListeSpeichern.Click
        Dim SaveListe As New System.Windows.Forms.SaveFileDialog
        SaveListe.Filter = "WinBack Statistik-Liste|.wsl"
        SaveListe.Title = "Liste speichern"
        SaveListe.FileName = "*.wsl"
        SaveListe.InitialDirectory = wb_GlobalSettings.pTempPath

        If SaveListe.ShowDialog() Then
            wb_Functions.ArraySave(SaveListe.FileName, _ElementeArray)
        End If
    End Sub

    Private Sub cbAlleLinien_Click(sender As Object, e As EventArgs) Handles cbAlleLinien.Click
        'Wert übernehmen
        wb_Chargen_Shared.AlleLinien = cbAlleLinien.Checked
        'alle Elemente der Liste aktivieren/deaktivieren
        cbLinien.Enabled = Not cbAlleLinien.Checked
        'Statistik muss neu berechnet werden
        ListeBerechnet = False
    End Sub

    Private Sub cbUhrzeitVon_Click(sender As Object, e As EventArgs) Handles cbUhrzeitVon.Click
        'Wert übernehmen
        wb_Chargen_Shared.UhrZeitVonChecked = cbUhrzeitVon.Checked
        dtUhrzeitVon.Enabled = cbUhrzeitVon.Checked
        'Statistik muss neu berechnet werden
        ListeBerechnet = False
    End Sub

    Private Sub cbUhrzeitBis_Click(sender As Object, e As EventArgs) Handles cbUhrzeitBis.Click
        'Wert übernehmen
        wb_Chargen_Shared.UhrZeitBisChecked = cbUhrzeitBis.Checked
        dtUhrzeitBis.Enabled = cbUhrzeitBis.Checked
        'Statistik muss neu berechnet werden
        ListeBerechnet = False
    End Sub

    Private Sub cbElementeAusListe_Click(sender As Object, e As EventArgs) Handles cbElementeAusListe.Click
        If cbElementeAusListe.Enabled Then
            DisableGridRohGruppe()
        End If
        'Statistik muss neu berechnet werden
        ListeBerechnet = False
    End Sub

    Private Sub dtFilterVon_ValueChanged(sender As Object, e As EventArgs) Handles dtFilterVon.ValueChanged
        'Filter Datum übernehmen
        wb_Chargen_Shared.FilterVon = dtFilterVon.Value
        'Statistik muss neu berechnet werden
        ListeBerechnet = False
    End Sub

    Private Sub dtFilterBis_ValueChanged(sender As Object, e As EventArgs) Handles dtFilterBis.ValueChanged
        'Filter Datum übernehmen
        wb_Chargen_Shared.FilterBis = dtFilterBis.Value
        'Statistik muss neu berechnet werden
        ListeBerechnet = False
    End Sub

    Private Sub cbWasserTempAusblenden_CheckedChanged(sender As Object, e As EventArgs) Handles cbWasserTempAusblenden.CheckedChanged
        'Wassertemperatur ausblenden
        wb_Chargen_Shared.WasserTempAusblenden = cbWasserTempAusblenden.Checked
    End Sub

    Private Sub cbIstwertNullAusblenden_CheckedChanged(sender As Object, e As EventArgs) Handles cbIstwertNullAusblenden.CheckedChanged
        'Istwert Null unterdrücken
        wb_Chargen_Shared.IstwertNullAusblenden = cbIstwertNullAusblenden.Checked
    End Sub

    ''' <summary>
    ''' Alle Einträge(Rohstoffe) aus der Gruppe zur Liste hinzufügen. Vorher wird die Liste geleert.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cbRohstoffGrp1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbRohstoffGrp1.SelectedIndexChanged
        'Auswahl der Rohstoff-Gruppen
        Dim RohStoffGruppe As Integer = cbRohstoffGrp1.GetKeyFromSelection
        If RohStoffGruppe > 0 Then
            FillGridRohGruppe(RohStoffGruppe)
            cbElementeAusListe.Checked = True
            BtnListeAdd.Enabled = False
            BtnListeRemove.Enabled = False
        End If
    End Sub

    Private Sub cbRohstoffGrp2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbRohstoffGrp2.SelectedIndexChanged
        'Auswahl der Rohstoff-Gruppen
        Dim RohStoffGruppe As Integer = cbRohstoffGrp2.GetKeyFromSelection
        If RohStoffGruppe > 0 Then
            FillGridRohGruppe(RohStoffGruppe)
            cbElementeAusListe.Checked = True
            BtnListeAdd.Enabled = False
            BtnListeRemove.Enabled = False
        End If
    End Sub

    Private Sub FillGridRohGruppe(Grp As Integer)
        'Liste aller Rohstoffe zu mit dieser Gruppe
        _ElementeArray.AddRange(wb_Rohstoffe_Shared.RohstoffeInGruppe(Grp))
        'Elemente in Liste anzeigen
        StatistikElementeGridView.FillGrid(_ElementeArray)
    End Sub

    Private Sub DisableGridRohGruppe()
        'wenn noch Gruppen angewählt sind, Liste löschen
        If (cbRohstoffGrp1.GetKeyFromSelection > 0) Or (cbRohstoffGrp2.GetKeyFromSelection > 0) Then
            _ElementeArray.Clear()
            StatistikElementeGridView.FillGrid(_ElementeArray)
        End If

        'Selektion aufheben
        cbRohstoffGrp1.SelectedItem = Nothing
        cbRohstoffGrp2.SelectedItem = Nothing
    End Sub
End Class
