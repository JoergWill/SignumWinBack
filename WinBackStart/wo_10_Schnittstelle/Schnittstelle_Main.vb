Imports WeifenLuo.WinFormsUI.Docking

''' <summary>
''' MDI-Main-Fenster Schnittstelle
''' Abgeleitet von DockPanel_Main
'''     In DockPanelMain werden alle Funktionen für die Verwaltung der Layouts abgewickelt
'''     Default-Layout, Laden, Sichern, Löschen der Layouts
'''     Die Layouts werden, im Unterschied zu OrgaBack nicht vom jeweiligen Fenster sondern
'''     von der WinBack-Main-Form verwaltet.
''' </summary>
Public Class Schnittstelle_Main

    Public SchnittstelleLogger As New wb_Schnittstelle_Log              'Default-Fenster    (wird beim Öffnen immer angezeigt)
    Public SchnittstelleImport As wb_Schnittstelle_Import               'Detail-Fenster     (wird bei Bedarf erzeugt und angezeigt)
    Public SchnittstelleExport As wb_Schnittstelle_Export               'Detail-Fenster     (wird bei Bedarf erzeugt und angezeigt)
    Public SchnittstelleKonfig As wb_Schnittstelle_Konfig               'Detail-Fenster     (wird bei Bedarf erzeugt und angezeigt)
    Public SchnittstelleSetup As wb_Schnittstelle_Setup                 'Detail-Fenster     (wird bei Bedarf erzeugt und angezeigt)
    Public SchnittstelleSetupVorschau As wb_Schnittstelle_SetupVorschau 'Detail-Fenster     (wird bei Bedarf erzeugt und angezeigt)

    ''' <summary>
    ''' Execute-Command von Winback-Main-Form.
    ''' Routine wird von Winback-Main aufgerufen um verschiedene Funktionen in der MDI-Form auszuführen.
    ''' 
    '''     OPENDLISTE          -   Log-Fenster wird geöffnet und angezeigt.
    '''     OPENDETAILS         -   Konfigurations-Fenster wird geöffnet und angezeigt.
    '''     SCHNITTST_IMPORT    -   Import-Fenster wird geöffnet und angezeigt.
    '''     SCHNITTST_EXPORT    -   Export-Fenster wird geöffnet und angezeigt.
    '''     SCHNITTST_SETUP     -   Konfig-Fenster wird geöffnet und angezeigt.
    '''     SCHNITTST_VORSCHAU  -   Vorschau-Fenster wird geöffnet und angezeigt.
    '''     
    ''' </summary>
    ''' <param name="Cmd"></param>
    ''' <param name="Prm"></param>
    ''' <returns></returns>
    Public Overrides Function ExtendedCmd(Cmd As String, Prm As String) As Boolean
        Select Case Cmd
            Case "OPENLISTE"
                SchnittstelleLogger.Show(DockPanel, DockState.DockLeft)
                Return True
            Case "OPENDETAILS"
                If IsNothingOrDisposed(SchnittstelleKonfig) Then
                    SchnittstelleKonfig = New wb_Schnittstelle_Konfig
                End If
                SchnittstelleKonfig.Show(DockPanel)
                Return True
            Case "SCHNITTST_SETUP"
                If IsNothingOrDisposed(SchnittstelleSetup) Then
                    SchnittstelleSetup = New wb_Schnittstelle_Setup
                End If
                SchnittstelleSetup.Show(DockPanel)
                Return True
            Case "SCHNITTST_VORSCHAU"
                If IsNothingOrDisposed(SchnittstelleSetupVorschau) Then
                    SchnittstelleSetupVorschau = New wb_Schnittstelle_SetupVorschau
                End If
                SchnittstelleSetupVorschau.Show(DockPanel)
                Return True
            Case "SCHNITTST_IMPORT"
                If IsNothingOrDisposed(SchnittstelleImport) Then
                    SchnittstelleImport = New wb_Schnittstelle_Import
                End If
                SchnittstelleImport.Show(DockPanel)
                Return True
            Case "SCHNITTST_EXPORT"
                If IsNothingOrDisposed(SchnittstelleExport) Then
                    SchnittstelleExport = New wb_Schnittstelle_Export
                End If
                SchnittstelleExport.Show(DockPanel)
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
        SchnittstelleLogger.Show(DockPanel, DockState.DockLeft)
        SchnittstelleLogger.CloseButtonVisible = False
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
            Case "WinBack.wb_Schnittstelle_Log"
                SchnittstelleLogger.CloseButtonVisible = False
                _DockPanelList.Add(SchnittstelleLogger)
                Return SchnittstelleLogger

            Case "WinBack.wb_Schnittstelle_Import"
                SchnittstelleImport = New wb_Schnittstelle_Import
                _DockPanelList.Add(SchnittstelleImport)
                Return SchnittstelleImport

            Case "WinBack.wb_Schnittstelle_Export"
                SchnittstelleExport = New wb_Schnittstelle_Export
                _DockPanelList.Add(SchnittstelleExport)
                Return SchnittstelleExport

            Case "WinBack.wb_Schnittstelle_Setup"
                SchnittstelleSetup = New wb_Schnittstelle_Setup
                _DockPanelList.Add(SchnittstelleSetup)
                Return SchnittstelleSetup

            Case "WinBack.wb_Schnittstelle_SetupVorschau"
                SchnittstelleSetupVorschau = New wb_Schnittstelle_SetupVorschau
                _DockPanelList.Add(SchnittstelleSetupVorschau)
                Return SchnittstelleSetupVorschau

            Case "WinBack.wb_Schnittstelle_Konfig"
                SchnittstelleKonfig = New wb_Schnittstelle_Konfig
                _DockPanelList.Add(SchnittstelleKonfig)
                Return SchnittstelleKonfig

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
        wb_Functions.CloseAndDisposeSubForm(SchnittstelleLogger)
        wb_Functions.CloseAndDisposeSubForm(SchnittstelleImport)
        wb_Functions.CloseAndDisposeSubForm(SchnittstelleExport)
        wb_Functions.CloseAndDisposeSubForm(SchnittstelleKonfig)
        wb_Functions.CloseAndDisposeSubForm(SchnittstelleSetup)
    End Sub
End Class