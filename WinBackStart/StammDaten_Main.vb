Imports WeifenLuo.WinFormsUI.Docking

''' <summary>
''' MDI-Main-Fenster Planungverwaltung
''' Abgeleitet von DockPanel_Main
'''     In DockPanelMain werden alle Funktionen für die Verwaltung der Layouts abgewickelt
'''     Default-Layout, Laden, Sichern, Löschen der Layouts
'''     Die Layouts werden, im Unetrschied zu OrgaBack nicht vom jeweiligen Fenster sondern
'''     von der WinBack-Main-Form verwaltet.
''' </summary>
Public Class StammDaten_Main

    Public LinienGruppen As New wb_StammDaten_LinienGruppen
    Public WinBackKonfig As wb_StammDaten_Konfiguration

    ''' <summary>
    ''' Execute-Command von Winback-Main-Form.
    ''' Routine wird von Winback-Main aufgerufen um verschiedene Funktionen in der MDI-Form auszuführen.
    ''' 
    '''     OPENLINIENGRUPPEN   -   Tabelle Liniengruppen wird geöffnet und angezeigt.
    '''     OPENALLERGENE       -   Tabelle Inhaltsstoffe und Allergene wird geöffnet und angezeigt.
    '''     OPENGRUPPEN         -   Tabelle Rohstoff- und Artikelgruppen wird geöffnet und angezeigt.
    '''     OPENVARIANTEN       -   Tabelle Rezeptvarianten wird geöffnet und angezeigt.
    '''     OPENKONFIG          -   Tabelle Konfiguration wird geöffnet und angezeigt.
    '''     
    ''' </summary>
    ''' <param name="Cmd"></param>
    ''' <param name="Prm"></param>
    ''' <returns></returns>
    Public Overrides Function ExtendedCmd(Cmd As String, Prm As String) As Boolean
        Select Case Cmd
            Case "OPENLINIENGRUPPEN"
                LinienGruppen.Show(DockPanel, DockState.DockLeft)
                Return True
            Case "OPENALLERGENE"
                Return True
            Case "OPENGRUPPEN"
                Return True
            Case "OPENVARIANTEN"
                Return True
            Case "OPENKONFIG"
                If Not DockIsVisible("wb_StammDaten_Konfiguration") Then
                    WinBackKonfig = New wb_StammDaten_Konfiguration
                    WinBackKonfig.Show(DockPanel, DockState.DockLeft)
                End If
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
            Case "WinBack.wb_StammDaten_LinienGruppen"
                LinienGruppen.CloseButtonVisible = False
                _DockPanelList.Add(LinienGruppen)
                Return LinienGruppen

            Case "WinBack.wb_StammDaten_Konfiguration"
                _DockPanelList.Add(WinBackKonfig)
                Return WinBackKonfig

                'Case "WinBack.wb_Planung_Details"
                '    PlanungTeiler = New wb_Planung_Teiler
                '    _DockPanelList.Add(PlanungTeiler)
                '    Return PlanungTeiler

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
        If LinienGruppen IsNot Nothing Then
            LinienGruppen.Close()
        End If
    End Sub
End Class