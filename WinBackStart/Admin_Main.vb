Imports WeifenLuo.WinFormsUI.Docking

''' <summary>
''' MDI-Main-Fenster Adminverwaltung
''' Abgeleitet von DockPanel_Main
'''     In DockPanelMain werden alle Funktionen für die Verwaltung der Layouts abgewickelt
'''     Default-Layout, Laden, Sichern, Löschen der Layouts
'''     Die Layouts werden, im Unetrschied zu OrgaBack nicht vom jeweiligen Fenster sondern
'''     von der WinBack-Main-Form verwaltet.
''' </summary>
Public Class Admin_Main

    Public AdminDatensicherung As New wb_Admin_Datensicherung     'Default-Fenster    (wird beim Öffnen immer angezeigt)
    Public AdminLog As wb_Admin_Log                               'Detail-Fenster     (wird bei Bedarf erzeugt und angezeigt)
    Public AdminEditIni As wb_Admin_EditIni                       'Detail-Fenster     (wird bei Bedarf erzeugt und angezeigt)
    Public AdminCheckDataBase As wb_Admin_CheckDatabase           'Detail-Fenster     (wird bei Bedarf erzeugt und angezeigt)
    Public AdminUpdateDataBase As wb_Admin_UpdateDatabase         'Detail-Fenster     (wird bei Bedarf erzeugt und angezeigt)
    Public AdminUpdateWinBack As wb_Admin_UpdateWinBack           'Detail-Fenster     (wird bei Bedarf erzeugt und angezeigt)

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
                AdminDatensicherung.Show(DockPanel, DockState.DockLeft)
                Return True
            Case "LOGGER"
                AdminLog = New wb_Admin_Log
                AdminLog.Show(DockPanel)
                Return True
            Case "EDITKONFIG"
                AdminEditIni = New wb_Admin_EditIni
                AdminEditIni.Show(DockPanel)
                Return True

            Case "CHECKDATABASE"
                AdminCheckDataBase = New wb_Admin_CheckDatabase
                AdminCheckDataBase.Show(DockPanel)
                Return True
            Case "CHECKDATABASEUPDATE"
                AdminUpdateDataBase = New wb_Admin_UpdateDatabase
                AdminUpdateDataBase.Show(DockPanel)
                Return True
            Case "CHECKWINBACKUPDATE"
                AdminUpdateWinBack = New wb_Admin_UpdateWinBack
                AdminUpdateWinBack.Show(DockPanel)
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
        AdminDatensicherung.Show(DockPanel, DockState.DockLeft)
        AdminDatensicherung.CloseButtonVisible = False
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
            Case "WinBack.wb_Admin_Datensicherung"
                AdminDatensicherung.CloseButtonVisible = False
                _DockPanelList.Add(AdminDatensicherung)
                Return AdminDatensicherung

            Case "WinBack.wb_Admin_Log"
                AdminLog = New wb_Admin_Log
                _DockPanelList.Add(AdminLog)
                Return AdminLog

            Case "WinBack.wb_Admin_EditIni"
                AdminEditIni = New wb_Admin_EditIni
                _DockPanelList.Add(AdminEditIni)
                Return AdminEditIni

            Case "WinBack.wb_Admin_UpdateDataBase"
                AdminCheckDataBase = New wb_Admin_CheckDatabase
                _DockPanelList.Add(AdminCheckDataBase)
                Return AdminCheckDataBase

            Case "WinBack.wb_Admin_UpdateDataBase"
                AdminUpdateDataBase = New wb_Admin_UpdateDatabase
                _DockPanelList.Add(AdminUpdateDataBase)
                Return AdminUpdateDataBase

            Case "WinBack.wb_Admin_UpdateWinBack"
                AdminUpdateWinBack = New wb_Admin_UpdateWinBack
                _DockPanelList.Add(AdminUpdateWinBack)
                Return AdminUpdateWinBack

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
        AdminDatensicherung.Close()
    End Sub
End Class