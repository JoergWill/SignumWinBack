Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack

Public MustInherit Class DockPanel_Main
    Implements IMainMenu

    Protected _DkPnlConfigFileName As String = ""
    Protected _DockPanelList As New List(Of DockContent)

    ''' <summary>
    ''' Execute-Command von Winback-Main-Form.
    ''' Routine wird von Winback-Main aufgerufen um verschiedene Funktionen in der MDI-Form auszuführen.
    ''' 
    '''     SETDKPNLFILENAME    -   DockPanel-Konfiguration wird unter diesem Namen abgespeichert
    '''     RUNDKPNLFILENAME    -   Dockpanel-Konfiguration wird geladen
    '''     SAVEDKPNLFILENAME   -   DockPanel-Konfiguration wird gespeichert
    '''     
    '''     OPENDETAILS         -   Detail-Fenster wird geöffnet und angezeigt.
    '''     
    ''' </summary>
    ''' <param name="Cmd"></param>
    ''' <param name="Prm"></param>
    ''' <returns></returns>

    Public Function ExecuteCmd(Cmd As String, Prm As String) As Boolean Implements IMainMenu.ExecuteCmd
        Select Case Cmd
            Case "SETDKPNLFILENAME"
                DkPnlConfigFileName = Prm
                Return True
            Case "RUNDKPNLFILENAME"
                DkPnlConfigFileName = Prm
                LoadDockBarConfig()
                Return True
            Case "SAVEDKPNLFILENAME"
                DkPnlConfigFileName = Prm
                SaveDockBarConfig()
                Return True

            Case Else
                Return ExtendedCmd(Cmd, Prm)
        End Select
    End Function

    Public MustOverride Function ExtendedCmd(Cmd As String, Prm As String) As Boolean

    Public MustOverride Sub setDefaultLayout()

    Public Property DkPnlConfigFileName As String Implements IMainMenu.DkPnlConfigFileName
        Get
            Return _DkPnlConfigFileName
        End Get
        Set(value As String)
            _DkPnlConfigFileName = value
        End Set
    End Property

    Private Sub SaveDockBarConfig()
        Try
            DockPanel.SaveAsXml(DkPnlConfigFileName)
        Catch ex As Exception
            MsgBox("Fehler beim Sichern der Konfiguration: " & ex.Message.ToString)
            'TODO diese Konstruktion bei allen DockPanel.SaveAsXml einbauen
            'Fehler bei Win64 - Ohne TryCatch wird das Programm nicht geschlossen !!
        End Try
    End Sub

    Private Sub LoadDockBarConfig()
        'Farb-Schema einstellen
        DockPanel.Theme = wb_GlobalOrgaBack.Theme
        'Prüfen ob ein Dock-Panel-Konfigurations-File vorhanden ist
        If My.Computer.FileSystem.FileExists(DkPnlConfigFileName) Then

            'falls noch eine alte Konfiguration vorhanden ist
            For i = DockPanel.Contents.Count - 1 To 0 Step -1
                DockPanel.Contents(i).DockHandler.DockPanel = Nothing
            Next i

            'Liste aller Dock-Panels
            _DockPanelList.Clear()

            'Laden der Konfiguration
            DockPanel.LoadFromXml(DkPnlConfigFileName, AddressOf wbBuildDocContent)
            'alle Unterfenster aus der Liste anzeigen und Dock-Panel-State festlegen
            For Each x In _DockPanelList
                'Wenn ein Fenster beim Speichern Im State Float war, wird es anschliessend nicht mehr angezeigt
                If x.DockState = DockState.Float Then
                    x.DockState = DockState.Document
                End If
                x.Show(DockPanel, x.DockState)
                Debug.Print("DockState " & x.DockState.ToString)
            Next
        Else
            'Default Fenster-Konfiguration (wenn alles schief geht)
            setDefaultLayout()
        End If
    End Sub

    Public Overridable Function wbBuildDocContent(ByVal persistString As String) As DockContent
        Select Case persistString
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub Main_FormLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        'Fenster laden
        LoadDockBarConfig()
        'weitere Aktionen beim Öffnen des MDI-Main-Formulars
        FormOpen(sender, e)
    End Sub

    Public MustOverride Sub FormOpen(Sender As Object, e As EventArgs)

    Private Sub Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'LayoutFilename aus DockPanelConfigFilenamen ermitteln
        Dim LayoutFilename As String = wb_DockBarPanelMain.DkPnlConfigName(DkPnlConfigFileName, Me.Text)
        'Fenster-Einstellungen in winback.ini sichern
        wb_DockBarPanelShared.SaveFormBoundaries(Me.Top, Me.Left, Me.Width, Me.Height, LayoutFilename, Me.Text)
        'weitere Aktionen beim Schliessen des MDI-Main-Formulars
        FormClose(sender, e)
    End Sub

    Public MustOverride Sub FormClose(Sender As Object, e As FormClosedEventArgs)

End Class