Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack

Public MustInherit Class DockPanel_Main
    Implements IMainMenu

    Protected _DkPnlConfigFileName As String = ""
    Protected _DockPanelList As New List(Of DockContent)

    ''' <summary>
    ''' Execute-Command von Winback-Main-Form.
    ''' Routine wird von Winback-Main aufgerufen um verschiedene Funktionen der Layout-Verwaltung
    ''' in der MDI-Form auszuführen.
    ''' 
    '''     SETDKPNLFILENAME    -   DockPanel-Konfiguration wird unter diesem Namen abgespeichert
    '''     RUNDKPNLFILENAME    -   Dockpanel-Konfiguration wird geladen
    '''     SAVEDKPNLFILENAME   -   DockPanel-Konfiguration wird gespeichert
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


    ''' <summary>
    ''' Extendet-Execute-Command von Winback-Main-Form.
    ''' Routine wird von Winback-Main aufgerufen um verschiedene Funktionen in der MDI-Form auszuführen.
    '''     
    ''' </summary>
    ''' <param name="Cmd"></param>
    ''' <param name="Prm"></param>
    Public MustOverride Function ExtendedCmd(Cmd As String, Prm As String) As Boolean

    ''' <summary>
    ''' Prüft ob ein (Unter)Fenster im Dock-Panel schon vorhanden und/oder sichtbar ist. 
    ''' Wenn das Fenster vorhanden ist, wird geprüft ob dieses Fenster unsichtbar ist (DockState.Unknown)
    ''' Unsichtbare Fenster werden aus der Liste gelöscht (DockHandler.Dispose) und können dann neu angelegt werden.
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns>True - Fenster existiert und ist sichtbar</returns>
    Public Function DockIsVisible(Name As String) As Boolean
        For Each x As DockContent In DockPanel.Contents
            If x.Name = Name Then
                If x.DockState = DockState.Unknown Then
                    x.DockHandler.Dispose()
                    Return False
                Else
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Default-Layout anzeigen.
    ''' Falls keine Layout-Definitionen verhanden sind, wird das Haupt-Fenster (Liste) angezeigt.
    ''' </summary>
    Public MustOverride Sub setDefaultLayout()

    ''' <summary>
    ''' File-Name für die Konfig-Datei aus Layout-File-Name und Fom-Name.
    ''' Ohne Angaben wird der lokale Pfad zurückgegeben (..\Temp\xx, wobei xx die Arbeitsplatz-Nummer ist).
    ''' Optional der Globale-Pfad (..\Temp\00)
    ''' </summary>
    ''' <returns></returns>
    Public Property DkPnlConfigFileName As String Implements IMainMenu.DkPnlConfigFileName
        Get
            Return _DkPnlConfigFileName
        End Get
        Set(value As String)
            _DkPnlConfigFileName = value
        End Set
    End Property

    ''' <summary>
    ''' DockBar-Konfiguration sichern
    '''     Diese Einstellungen werden in wb_Main_Menu gelesen und verarbeitet
    ''' </summary>
    Private Sub SaveDockBarConfig()
        'Fehler bei Win64 - Ohne TryCatch wird das Programm nicht geschlossen !!
        Try
            DockPanel.SaveAsXml(DkPnlConfigFileName)
        Catch ex As Exception
            MsgBox("Fehler beim Sichern der Konfiguration: " & ex.Message.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Läd die Dock-Panel-Konfiguration aus der Konfiguration-Datei (*.xml). Die Konfiguration wird 
    ''' über SaveToXml gesichert.
    ''' </summary>
    Private Sub LoadDockBarConfig()
        'Farb-Schema einstellen
        DockPanel.Theme = wb_GlobalSettings.Theme
        'Prüfen ob ein Dock-Panel-Konfigurations-File vorhanden ist
        If My.Computer.FileSystem.FileExists(DkPnlConfigFileName) Then

            'falls noch eine alte Konfiguration vorhanden ist
            For i = DockPanel.Contents.Count - 1 To 0 Step -1
                DockPanel.Contents(i).DockHandler.DockPanel = Nothing
            Next i

            'Liste aller Dock-Panels
            _DockPanelList.Clear()

            'Laden der Konfiguration
            Try
                DockPanel.LoadFromXml(DkPnlConfigFileName, AddressOf wbBuildDocContent)
            Catch
            End Try
            'alle Unterfenster aus der Liste anzeigen und Dock-Panel-State festlegen

            If _DockPanelList.Count = 0 Then
                setDefaultLayout()
            Else
                For Each x In _DockPanelList
                    If x IsNot Nothing Then
                        'Wenn ein Fenster beim Speichern Im State Float war, wird es anschliessend nicht mehr angezeigt
                        If x.DockState = DockState.Float Then
                            x.DockState = DockState.Document
                        End If
                        Try
                            x.Show(DockPanel, x.DockState)
                        Catch
                        End Try
                    End If
                Next
            End If
        Else
            'Default Fenster-Konfiguration (wenn alles schief geht)
            setDefaultLayout()
        End If
    End Sub

    ''' <summary>
    ''' Muss in der abgeleiteten Klasse überschrieben werden. Gibt für den jeweiligen Form-Namenn
    ''' die entsprechenden Klasse zurück, die dann dargestellt wird
    ''' </summary>
    ''' <param name="persistString"></param>
    ''' <returns></returns>
    Public Overridable Function wbBuildDocContent(ByVal persistString As String) As DockContent
        Select Case persistString
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub Main_FormLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        'DockPanel-Hauptformular auf Maximalgröße aufziehen
        DockPanel.Dock = DockStyle.Fill
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

    ''' <summary>
    ''' Prüft ob die übergebene Form existiert(Nothing) und/oder gelöscht(disposed) ist.
    ''' Wenn die Form nicht mehr existiert wird True zurückgegeben, dann wird
    ''' sie im Hauptprogramm neu initialisiert. (New)
    ''' </summary>
    ''' <param name="f"></param>
    ''' <returns></returns>
    Public Function IsNothingOrDisposed(ByRef f As Form) As Boolean
        If f Is Nothing Then
            Return True
        Else
            If f.IsDisposed Then
                Return True
            End If
        End If
        Return False
    End Function

End Class