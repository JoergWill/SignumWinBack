Imports WeifenLuo.WinFormsUI.Docking

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
    Public RezeptHistorie As wb_Rezept_Historie

    ''' <summary>
    ''' Execute-Command von Winback-Main-Form.
    ''' Routine wird von Winback-Main aufgerufen um verschiedene Funktionen in der MDI-Form auszuführen.
    ''' 
    '''     OPENDETAILS         -   Detail-Fenster wird geöffnet und angezeigt.
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
                RezeptDetails = New wb_Rezept_Details
                RezeptDetails.Show(DockPanel)
                Return True
            Case "OPENHINWEISE"
                RezeptHinweise = New wb_Rezept_Hinweise
                RezeptHinweise.Show(DockPanel)
                Return True
            Case "OPENHISTORIE"
                RezeptHistorie = New wb_Rezept_Historie
                RezeptHistorie.Show(DockPanel)
                Return True

            Case "NEW"
                RezeptNeuAnlegen()
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
        RezeptListe.Show(DockPanel, DockState.DockLeft)
        RezeptListe.CloseButtonVisible = False
        WinBack.LayoutFilename = "Default"
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

    End Sub

    ''' <summary>
    ''' MDI-Form wird geschlossen. Vorher wurde schon in der Basis-Klasse die DockBar-Konfiguration gesichert.
    ''' Schliesst alle erzeugten Fenster.
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Public Overrides Sub FormClose(Sender As Object, e As FormClosedEventArgs)
        'alle erzeugten Fenster wieder schliessen
        wb_Functions.CloseAndDisposeSubForm(RezeptDetails)
        wb_Functions.CloseAndDisposeSubForm(RezeptHinweise)
        wb_Functions.CloseAndDisposeSubForm(RezeptHistorie)
        wb_Functions.CloseAndDisposeSubForm(RezeptListe)
    End Sub

    Public Sub RezeptNeuAnlegen()
        Dim Rezept As New wb_Rezept
        Dim RezeptNrNeu As Integer = Rezept.MySQLdbNew(wb_Global.LinienGruppeStandard)
        RezeptListe.RefreshData(RezeptNrNeu)
        Rezept = Nothing

        'Das neu erzeugte Rezept gleich öffnen
        Me.Cursor = Cursors.WaitCursor
        'Beim Erzeugen des Fensters werden die Daten aus der Datenbank gelesen
        Dim Rezeptur As New wb_Rezept_Rezeptur(RezeptNrNeu, wb_Global.RezeptVarianteStandard)
        Rezeptur.Show()
        Me.Cursor = Cursors.Default
    End Sub

End Class
