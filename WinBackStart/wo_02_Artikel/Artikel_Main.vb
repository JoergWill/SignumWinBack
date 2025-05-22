Imports WeifenLuo.WinFormsUI.Docking
Imports combit.Reporting.DataProviders

''' <summary>
''' MDI-Main-Fenster Artikelverwaltung
''' Abgeleitet von DockPanel_Main
'''     In DockPanelMain werden alle Funktionen für die Verwaltung der Layouts abgewickelt
'''     Default-Layout, Laden, Sichern, Löschen der Layouts
'''     Die Layouts werden, im Unetrschied zu OrgaBack nicht vom jeweiligen Fenster sondern
'''     von der WinBack-Main-Form verwaltet.
''' </summary>
Public Class Artikel_Main

    Public ArtikelListe As New wb_Artikel_Liste             'Default-Fenster     (wird beim Öffnen immer angezeigt)
    Public ArtikelDetails As wb_Artikel_Details             'Detail-Fenster      (wird bei Bedarf erzeugt und angezeigt)
    Public ArtikelHinweise As wb_Artikel_Hinweise           'Hinweise-Fenster    (wird bei Bedarf erzeugt und angezeigt)
    Public ArtikelTextHinweise As wb_Artikel_TextHinweise   'Hinweise(Text)-Fenster   (wird bei Bedarf erzeugt und angezeigt)
    Public ArtikelParameter As wb_Artikel_Parameter         'Parameter-Fenster   (wird bei Bedarf erzeugt und angezeigt)
    Public ArtikelProduktInfo As wb_Artikel_Nwt             'ProduktInfo-Fenster (wird bei Bedarf erzeugt und angezeigt)

    ''' <summary>
    ''' Execute-Command von Winback-Main-Form.
    ''' Routine wird von Winback-Main aufgerufen um verschiedene Funktionen in der MDI-Form auszuführen.
    ''' 
    '''     OPENDLISTE          -   Listen-Fenster wird geöffnet und angezeigt.
    '''     OPENDETAILS         -   Detail-Fenster wird geöffnet und angezeigt.
    '''     
    ''' </summary>
    ''' <param name="Cmd"></param>
    ''' <param name="Prm"></param>
    ''' <returns></returns>
    Public Overrides Function ExtendedCmd(Cmd As String, Prm As String) As Boolean
        Select Case Cmd
            Case "OPENLISTE"
                ArtikelListe.Show(DockPanel, DockState.DockLeft)
                Return True
            Case "OPENDETAILS"
                If IsNothingOrDisposed(ArtikelDetails) Then
                    ArtikelDetails = New wb_Artikel_Details
                End If
                ArtikelDetails.Show(DockPanel, DockState.Document)
                Return True
            Case "OPENHINWEISE"
                If IsNothingOrDisposed(ArtikelHinweise) Then
                    ArtikelHinweise = New wb_Artikel_Hinweise
                End If
                ArtikelHinweise.Show(DockPanel, DockState.Document)
                Return True
            Case "OPENTEXTHINWEISE"
                If IsNothingOrDisposed(ArtikelTextHinweise) Then
                    ArtikelTextHinweise = New wb_Artikel_TextHinweise
                End If
                ArtikelTextHinweise.Show(DockPanel, DockState.Document)
                Return True
            Case "OPENPARAMETER"
                If IsNothingOrDisposed(ArtikelParameter) Then
                    ArtikelParameter = New wb_Artikel_Parameter
                End If
                ArtikelParameter.Show(DockPanel, DockState.Document)
                Return True
            Case "OPENPRODUKTINFO"
                If IsNothingOrDisposed(ArtikelProduktInfo) Then
                    ArtikelProduktInfo = New wb_Artikel_Nwt
                End If
                ArtikelProduktInfo.Show(DockPanel, DockState.Document)
                Return True
            Case "NEW"
                ArtikelNeuAnlegen()
                Return True
            Case "DELETE"
                ArtikelLöschen()
                Return True
            Case "ARTIKEL_DRUCKEN"
                ArtikelDrucken()
                Return True
            Case "ARTIKEL_LISTE_DRUCKEN"
                ArtikelListeDrucken()
                Return True
            Case "PRODUKT_INFO_DRUCKEN"
                ProduktInfoDrucken()
                Return True
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>
    ''' Default-Layout anzeigen.
    ''' Falls keine Layout-Definitionen verhanden sind, wird das Haupt-Fenster (Liste) angezeigt.
    ''' </summary>
    Public Overrides Sub setDefaultLayout()
        DockPanel.Theme = wb_GlobalSettings.Theme
        ArtikelListe.Show(DockPanel, DockState.DockLeft)
        ArtikelListe.CloseButtonVisible = False
        OrgaBackOffice.LayoutFilename = "Default"
    End Sub

    ''' <summary>
    ''' Gibt für den jeweiligen Form-Namen die entsprechenden Klasse zurück, die dann im Dock dargestellt wird.
    ''' Füllt das Array DockPanelList in der Basis-Klasse
    ''' </summary>
    ''' <param name="persistString"></param>
    ''' <returns></returns>
    Public Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "WinBack.wb_Artikel_Liste"
                ArtikelListe.CloseButtonVisible = False
                _DockPanelList.Add(ArtikelListe)
                Return ArtikelListe

            Case "WinBack.wb_Artikel_Details"
                ArtikelDetails = New wb_Artikel_Details
                _DockPanelList.Add(ArtikelDetails)
                Return ArtikelDetails

            Case "WinBack.wb_Artikel_Hinweise"
                ArtikelHinweise = New wb_Artikel_Hinweise
                _DockPanelList.Add(ArtikelHinweise)
                Return ArtikelHinweise

            Case "WinBack.wb_Artikel_TextHinweise"
                ArtikelTextHinweise = New wb_Artikel_TextHinweise
                _DockPanelList.Add(ArtikelTextHinweise)
                Return ArtikelTextHinweise

            Case "WinBack.wb_Artikel_Parameter"
                ArtikelParameter = New wb_Artikel_Parameter
                _DockPanelList.Add(ArtikelParameter)
                Return ArtikelParameter

            Case "WinBack.wb_Artikel_Nwt"
                ArtikelProduktInfo = New wb_Artikel_Nwt
                _DockPanelList.Add(ArtikelProduktInfo)
                Return ArtikelProduktInfo

            Case Else
                Return Nothing
        End Select
    End Function

    ''' <summary>
    ''' MID-Form wird geöffnet. Vorher wurde schon in der Basis-Klasse die DockBar-Konfiguration geladen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Public Overrides Sub FormOpen(Sender As Object, e As EventArgs)

    End Sub

    ''' <summary>
    ''' MDI-Form wird geschlossen. Vorher wurde schon in der Basis-Klasse die DockBar-Konfiguration gesichert.
    ''' Schliesst alle erzeugten Fenster.
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Public Overrides Sub FormClose(Sender As Object, e As FormClosedEventArgs)
        'alle erzeugten Fenster wieder schliessen
        wb_Functions.CloseAndDisposeSubForm(ArtikelListe)
        wb_Functions.CloseAndDisposeSubForm(ArtikelDetails)
        wb_Functions.CloseAndDisposeSubForm(ArtikelHinweise)
        wb_Functions.CloseAndDisposeSubForm(ArtikelTextHinweise)
        wb_Functions.CloseAndDisposeSubForm(ArtikelParameter)
        wb_Functions.CloseAndDisposeSubForm(ArtikelProduktInfo)

        'alle Spuren in Artikel_Shared löschen
        wb_Artikel_Shared.Invalid()
    End Sub

    Public Sub ArtikelNeuAnlegen()
        Dim Komponente As New wb_Komponente
        Dim KompNrNeu As Integer = Komponente.MySQLdbNew(wb_Global.KomponTypen.KO_TYPE_ARTIKEL)
        ArtikelListe.RefreshData(KompNrNeu)
        Komponente = Nothing
    End Sub

    Public Sub ArtikelLöschen()
        'Sicherheitsabfrage Artikel löschen
        If MsgBox("Den Artikel " & wb_Artikel_Shared.Artikel.Nummer & " " & wb_Artikel_Shared.Artikel.Bezeichnung & " löschen?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim Komponente As New wb_Komponente
            Dim KompNrDel As Integer = wb_Artikel_Shared.Artikel.Nr
            If Komponente.MySQLdbCanBeDeleted(KompNrDel) Then
                Komponente.Nr = KompNrDel
                Komponente.MySQLdbDelete()
                ArtikelListe.RefreshData()
            Else
                MsgBox(Komponente.LastErrorText)
            End If
            Komponente = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Drucken Artikel-Stammblatt
    ''' Druckt über wb_RezeptDrucken, das Artikel-Stammblatt inklusive der Rezeptur aus.
    ''' </summary>
    Private Sub ArtikelDrucken()
        'Drucke Artikelstammdaten und Rezeptur inklusive aller verknüpften Rezepturen
        Dim RezeptDrucken As New wb_RezeptDrucken
        'einzelnes Rezept laden und drucken
        RezeptDrucken.Print(wb_Artikel_Shared.Artikel)
        'Speicher wieder freigeben
        RezeptDrucken = Nothing
    End Sub

    Private Function ArtikelListeDrucken() As Boolean
        'sicherheitshalber abfragen
        If Not IsNothing(ArtikelListe) Then

            'Druck-Daten
            Dim pDialog As New wb_PrinterDialog(False) 'Drucker-Dialog

            'Liste aller Rohstoffe aus den DataGridView
            pDialog.LL.DataSource = New AdoDataProvider(ArtikelListe.DataGridView.LLData)

            'List und Label-Verzeichnis für die Listen
            pDialog.ListSubDirectory = "Artikel"
            pDialog.ListFileName = "ArtikelListe.lst"
            pDialog.ShowDialog()
            pDialog = Nothing
        End If
        Return True
    End Function

    Private Sub ProduktInfoDrucken()
        'Druck-Daten
        Dim pDialog As New wb_PrinterDialog(False) With {
            .LL_KopfZeile_1 = "",
            .LL_KopfZeile_2 = "",
            .LL_Parameter_1 = "kg"
        } 'Drucker-Dialog

        'Artikel-Produktinformation - Daten zusammenfassen und zur Verfügung stellen
        Dim Produktinfo As New wb_KomponProduktInfo(wb_Artikel_Shared.Artikel)
        pDialog.LL.DataSource = New ObjectDataProvider(Produktinfo)

        'List und Label-Verzeichnis für die Listen
        pDialog.ListSubDirectory = "Rezepte"
        pDialog.ListFileName = "ArtikelProduktInfo.lst"
        pDialog.ShowDialog()
        pDialog = Nothing
        Produktinfo = Nothing
    End Sub

End Class