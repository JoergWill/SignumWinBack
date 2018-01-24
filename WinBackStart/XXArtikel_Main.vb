Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack

Public Class XXArtikel_Main
    Implements IMainMenu

    Public ArtikelListe As New wb_Artikel_Liste
    Public ArtikelDetails As wb_Artikel_Details

    Private _DkPnlConfigFileName As String = ""
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

            Case "OPENDETAILS"
                ArtikelDetails = New wb_Artikel_Details
                ArtikelDetails.Show(DockPanel, DockState.DockLeft)
                Return True

            Case Else
                Return False
        End Select
    End Function

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

    Private Sub setDefaultLayout()
        ArtikelListe.Show(DockPanel, DockState.DockLeft)
        ArtikelListe.CloseButtonVisible = False
        WinBack.LayoutFilename = "Default"
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "WinBack.wb_Artikel_Liste"
                ArtikelListe.CloseButtonVisible = False
                _DockPanelList.Add(ArtikelListe)
                Return ArtikelListe

            Case "WinBack.wb_Artikel_Details"
                ArtikelDetails = New wb_Artikel_Details
                _DockPanelList.Add(ArtikelDetails)
                Return ArtikelDetails

            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'LayoutFilename aus DockPanelConfigFilenamen ermitteln
        Dim LayoutFilename As String = wb_DockBarPanelMain.DkPnlConfigName(DkPnlConfigFileName, Me.Text)
        'Fenster-Einstellungen in winback.ini sichern
        wb_DockBarPanelShared.SaveFormBoundaries(Me.Top, Me.Left, Me.Width, Me.Height, LayoutFilename, Me.Text)

        'alle erzeugten Fenster wieder schliessen
        ArtikelListe.Close()
    End Sub

    Private Sub Main_FormLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        'Fenster laden
        LoadDockBarConfig()
    End Sub

End Class