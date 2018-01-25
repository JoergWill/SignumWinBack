Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack

Public Class XXRezepte_Main
    Implements IMainMenu

    Public RezeptListe As New wb_Rezept_Liste
    Public RezeptDetails As wb_Rezept_Details
    Public RezeptHinweise As wb_Rezept_Hinweise
    Public RezeptHistorie As wb_Rezept_Historie

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
                RezeptDetails = New wb_Rezept_Details
                RezeptDetails.Show(DockPanel, DockState.DockLeft)
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
            If _DockPanelList.Count = 0 Then
                setDefaultLayout()
            Else
                'alle Unterfenster aus der Liste anzeigen und Dock-Panel-State festlegen
                For Each x In _DockPanelList
                    'Wenn ein Fenster beim Speichern Im State Float war, wird es anschliessend nicht mehr angezeigt
                    If x.DockState = DockState.Float Then
                        x.DockState = DockState.Document
                    End If
                    x.Show(DockPanel, x.DockState)
                    Debug.Print("DockState " & x.DockState.ToString)
                Next
            End If
        Else
            'Default Fenster-Konfiguration (wenn alles schief geht)
            setDefaultLayout()
        End If
    End Sub

    Private Sub setDefaultLayout()
        RezeptListe.Show(DockPanel, DockState.DockLeft)
        RezeptListe.CloseButtonVisible = False
        WinBack.LayoutFilename = "Default"
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "WinBack.wb_RezeptListe"
                _DockPanelList.Add(RezeptListe)
                Return RezeptListe
            Case "WinBack.wb_RezeptDetails"
                _DockPanelList.Add(RezeptDetails)
                Return RezeptDetails
            Case "WinBack.wb_RezeptHinweise"
                _DockPanelList.Add(RezeptHinweise)
                Return RezeptHinweise
            Case "WinBack.wb_RezeptHistorie"
                _DockPanelList.Add(RezeptHistorie)
                Return RezeptHistorie
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub Rezepte_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'HashTable mit der Übersetzung der Varianten-Nummer zu Rezept-Varianten-Bezeichnung
        'HashTable mit der Übersetzung der Liniengruppen-Nummer zu Liniengruppen-Bezeichnung
        'wb_Rezept_Shared.LoadLinienGruppenTexte()

        'Fenster laden
        LoadDockBarConfig()
    End Sub

    Private Sub Rezepte_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'LayoutFilename aus DockPanelConfigFilenamen ermitteln
        Dim LayoutFilename As String = wb_DockBarPanelMain.DkPnlConfigName(DkPnlConfigFileName, Me.Text)
        'Fenster-Einstellungen in winback.ini sichern
        wb_DockBarPanelShared.SaveFormBoundaries(Me.Top, Me.Left, Me.Width, Me.Height, LayoutFilename, Me.Text)

        'alle erzeugten Fenster wieder schliessen
        RezeptListe.Close()
    End Sub
End Class