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
Public Class Rezepte_Main

    Public RezeptListe As New wb_Rezept_Liste
    Public RezeptDetails As wb_Rezept_Details
    Public RezeptHinweise As wb_Rezept_Hinweise
    Public RezeptVerwendung As wb_Rezept_Verwendung
    Public RezeptHistorie As wb_Rezept_Historie

    ''' <summary>
    ''' Execute-Command von Winback-Main-Form.
    ''' Routine wird von Winback-Main aufgerufen um verschiedene Funktionen in der MDI-Form auszuführen.
    ''' 
    '''     OPENDETAILS         -   Detail-Fenster wird geöffnet und angezeigt.
    '''     OPENHINWEISE        -   Rezepthinweise Fenster wird erzeugt, geöffnet und angezeigt
    '''     OPENVERWENDUNG      -   Rezept-Verwendung Fenster wird erzeugt, geöffnet und angezeigt
    '''     OPENHISTORIE        -   Rezept-Historie Fenster wird erzeugt, geöffnet und angezeigt
    '''     NEW                 -   Neues Rezept anlegen (Rezept_Shared)
    '''     RZPT_LISTE_DRUCKEN  -   Rezept-Liste drucken
    '''     RZPT_DRUCKEN        -   Rezept drucken
    '''     
    ''' </summary>
    ''' <param name="Cmd"></param>
    ''' <param name="Prm"></param>
    ''' <returns></returns>
    Public Overrides Function ExtendedCmd(Cmd As String, Prm As String) As Boolean
        Select Case Cmd
            Case "OPENLISTE"
                RezeptListe.Show(DockPanel, DockState.DockLeft)
                Return True
            Case "OPENDETAILS"
                If IsNothingOrDisposed(RezeptDetails) Then
                    RezeptDetails = New wb_Rezept_Details
                End If
                RezeptDetails.Show(DockPanel)
                Return True
            Case "OPENHINWEISE"
                If IsNothingOrDisposed(RezeptHinweise) Then
                    RezeptHinweise = New wb_Rezept_Hinweise
                End If
                RezeptHinweise.Show(DockPanel)
                Return True
            Case "OPENVERWENDUNG"
                If IsNothingOrDisposed(RezeptVerwendung) Then
                    RezeptVerwendung = New wb_Rezept_Verwendung
                End If
                RezeptVerwendung.Show(DockPanel)
                Return True
            Case "OPENHISTORIE"
                If IsNothingOrDisposed(RezeptHistorie) Then
                    RezeptHistorie = New wb_Rezept_Historie
                End If
                RezeptHistorie.Show(DockPanel)
                Return True

            Case "NEW"
                RezeptNeuAnlegen()
                Return True

            Case "RZPT_LISTE_DRUCKEN"
                Return RzptListeDrucken()

            Case "RZPT_DRUCKEN"
                Return RzptDrucken()

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
        RezeptListe.Show(DockPanel, DockState.DockLeft)
        RezeptListe.CloseButtonVisible = False
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
            Case "WinBack.wb_Rezept_Liste"
                RezeptListe.CloseButtonVisible = False
                _DockPanelList.Add(RezeptListe)
                Return RezeptListe

            Case "WinBack.wb_Rezept_Details"
                RezeptDetails = New wb_Rezept_Details
                _DockPanelList.Add(RezeptDetails)
                Return RezeptDetails

            Case "WinBack.wb_Rezept_Hinweise"
                RezeptHinweise = New wb_Rezept_Hinweise
                _DockPanelList.Add(RezeptHinweise)
                Return RezeptHinweise

            Case "WinBack.wb_Rezept_Verwendung"
                RezeptVerwendung = New wb_Rezept_Verwendung
                _DockPanelList.Add(RezeptVerwendung)
                Return RezeptVerwendung

            Case "WinBack.wb_Rezept_Historie"
                RezeptHistorie = New wb_Rezept_Historie
                _DockPanelList.Add(RezeptHistorie)
                Return RezeptHistorie

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
        'Event-Handler (Klick auf Rezept-Kopieren -> Rezept-Neu)
        AddHandler wb_Rezept_Shared.eRezept_Copy, AddressOf RezeptCopy
    End Sub

    ''' <summary>
    ''' MDI-Form wird geschlossen. Vorher wurde schon in der Basis-Klasse die DockBar-Konfiguration gesichert.
    ''' Schliesst alle erzeugten Fenster.
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Public Overrides Sub FormClose(Sender As Object, e As FormClosedEventArgs)
        'alle Events wieder freigeben
        RemoveHandler wb_Rezept_Shared.eRezept_Copy, AddressOf RezeptCopy
        'alle erzeugten Fenster wieder schliessen
        wb_Functions.CloseAndDisposeSubForm(RezeptListe)
        wb_Functions.CloseAndDisposeSubForm(RezeptDetails)
        wb_Functions.CloseAndDisposeSubForm(RezeptHinweise)
        wb_Functions.CloseAndDisposeSubForm(RezeptVerwendung)
        wb_Functions.CloseAndDisposeSubForm(RezeptHistorie)

        'alle "alten" Daten löschen
        wb_Rezept_Shared.Invalid()
    End Sub

    ''' <summary>
    ''' Neues Rezept anlegen. Wenn keine Rezeptnummer angegeben ist, wird nur eine leere Hülle erzeugt. Ist eine Rezeptnummer angegeben,
    ''' wird eine Kopie des aktuellen Rezeptes erzeugt und angezeigt.
    ''' </summary>
    ''' <param name="RzNr"></param>
    Private Sub RezeptNeuAnlegen(Optional RzNr As Integer = wb_Global.UNDEFINED, Optional RzVariante As Integer = 1)
        Dim Rezept As New wb_Rezept
        Dim RezeptNrNeu As Integer = Rezept.MySQLdbNew(wb_Global.LinienGruppeStandard)
        'Beim Aufruf aus Artikel/Rohstoffe (verknüpftes Rezept) gibt es keine Rezeptliste
        If RezeptListe IsNot Nothing Then
            RezeptListe.Anzeige = wb_Rezept_Shared.AnzeigeFilter.Alle
            RezeptListe.RefreshData(RezeptNrNeu)
        End If

        'Rezeptur kopieren
        If RzNr > wb_Global.UNDEFINED Then
            'Rezeptschritte kopieren
            Rezept.Copy(RzNr, RzVariante)
        End If

        'Speicher wieder freigeben
        Rezept = Nothing

        'Das neu erzeugte Rezept gleich öffnen
        Me.Cursor = Cursors.WaitCursor
        'Beim Erzeugen des Fensters werden die Daten aus der Datenbank gelesen
        Dim Rezeptur As New wb_Rezept_Rezeptur(RezeptNrNeu, wb_Global.RezeptVarianteStandard)
        'Rezept Bezeichnung anpassen wenn kopiert wurde
        If RzNr > 0 Then
            Rezeptur.RezeptBezeichnung = "Kopie von " & wb_Rezept_Shared.Rezept.RezeptBezeichnung
        End If

        Rezeptur.Show()
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Das Kopieren einer Rezeptur erfolgt über die Funktion Rezept-Neu.
    ''' Die übergebene Rezept-Nummer wird kopiert.
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="RzNr"></param>
    Private Sub RezeptCopy(Sender As Object, RzNr As Integer, Variante As Integer)
        RezeptNeuAnlegen(RzNr, Variante)
    End Sub

    Private Function RzptListeDrucken() As Boolean
        'sicherheitshalber abfragen
        If Not IsNothing(RezeptListe) Then

            'Druck-Daten
            Dim pDialog As New wb_PrinterDialog(False) 'Drucker-Dialog

            'Liste aller Rohstoffe aus den DataGridView
            pDialog.LL.DataSource = New AdoDataProvider(RezeptListe.DataGridView.LLData)

            'List und Label-Verzeichnis für die Listen
            pDialog.ListSubDirectory = "Rezepte"
            pDialog.ListFileName = "RezeptListe.lst"
            pDialog.ShowDialog()
            pDialog = Nothing
        End If
        Return True
    End Function

    Private Function RzptDrucken() As Boolean
        'sicherheitshalber abfragen
        If Not IsNothing(RezeptListe) Then

            Me.Cursor = Cursors.WaitCursor
            'Beim Erzeugen des Fensters werden die Daten aus der Datenbank gelesen - Ausdruck der Standard-Variante - Kopieren der Rezeptur ist nicht erlaubt
            Dim Rezeptur As New wb_Rezept_Rezeptur(wb_Rezept_Shared.Rezept.RezeptNr, wb_Global.RezeptVarianteStandard, wb_Global.UNDEFINED, False)
            Me.Cursor = Cursors.Default

            'Rezeptur drucken
            Rezeptur.BtnDrucken_Click(Nothing, Nothing)
            'Speicher wieder freigaben
            Rezeptur = Nothing

        End If
        Return True
    End Function

End Class
