﻿Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

''' <summary>
''' Klasse (must Inherit) beeinhaltet alle notwendigen Routinen für die Anzeige der Unterfenster in
''' OrgaBack als Docking-Window. Status-Bar mit Schaltflächen für das Layout-Management.
''' 
''' Die einzelnen Layouts werden entsprechen der OrgaBack-Installation im ..\Temp\-Verzeichnis gespeichert.
''' Abgeleitete Klassen müssen folgende Prozeduren und Properties überschreiben
''' 
'''     -   FormText    (eindeutiger Schlüssel für das Fenster, ggf. Firmenname.AddIn)
'''     -   FormName    (eindeutiger Name für das Fenster, dient zum Speichern der Einstellungen)
'''     
''' </summary>
Public Class WinBack_DockMain
    Private _LayoutFilename As String = Nothing
    Private _SaveAtClose As Boolean = False
    Protected _DockPanelList As New List(Of DockContent)


#Region "MustOverride"
    ''' <summary>
    ''' Eindeutiger Name für die Basis-Form. 
    ''' Unter diesem Namen werden die Einstellungen in der winback.ini gespeichert.
    ''' 
    ''' Die DockPanel-Konfiguration wird gespeichert unter wbXXXXYYYY.xml, dabei ist XXXX der FormName und YYYY der Layout-Name.
    ''' </summary>
    ''' <returns></returns>
    Public Overridable ReadOnly Property FormName As String
        Get
            Throw New NotImplementedException
            Return "FormName"
        End Get
    End Property

    ''' <summary>
    ''' Stellt die Dock-Panel-Fensterkonfiguration wieder her. Wird von LoadDockBarConfig aufgerufen.
    ''' 
    ''' </summary>
    ''' <param name="persistString"> String - Name Fenster-Objekt</param>
    ''' <returns>Form - Fenster-Form-Objekt</returns>
    Protected Overridable Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Throw New NotImplementedException
        Return Nothing
    End Function

    ''' <summary>
    ''' Default Layout-Konfiguration (wenn alles schief geht). Wird von LoadDockBarConfig() aufgerufen
    ''' wenn kein gültiges Layout gefunden wurde
    ''' </summary>
    Public Overridable Sub SetDefaultLayout()
        Throw New NotImplementedException
    End Sub
#End Region

    ''' <summary>
    ''' Die Information kommt aus der winback.ini und wird in der Routine wb_Konfig.SetFormBoundaries ausgelesen (in wb_Main_Menu)
    ''' </summary>
    ''' <returns>String - Layout-Filename</returns>
    Private Property LayoutFilename As String
        Get
            If _LayoutFilename Is Nothing Then
                'Dock-Panel-Layout Filename aus winback.ini
                Dim IniFile As New Global.WinBack.wb_IniFile
                _LayoutFilename = IniFile.ReadString(FormName, "LayoutFileName", "Default")
                'Dispose
                IniFile = Nothing
            End If
            Return _LayoutFilename
        End Get
        Set(value As String)
            'neuen Wert setzen
            _LayoutFilename = value

            Try
                'Wenn dieses Layout im Arbeitsplatz-Ordner nicht vorhanden ist
                If Not My.Computer.FileSystem.FileExists(DkPnlConfigFileName) Then
                    'vom Default-Ordner kopieren
                    System.IO.File.Copy(DkPnlConfigFileName(wb_Global.OrgaBackDockPanelLayoutPath.ProgrammGlobal), DkPnlConfigFileName)
                End If
            Catch ex As Exception
                Trace.WriteLine("@E_Fehler beim Laden der Default-Dock-Panel-Konfig")
            End Try
        End Set
    End Property

    Public Sub FormClosed()
        'Fenster-Einstellungen in winback.ini sichern
        wb_DockBarPanelShared.SaveFormBoundaries(Me.Top, Me.Left, Me.Width, Me.Height, LayoutFilename, FormName)
        'Anzeige wird in OrgaBack beim Schliessen nicht gesichert !
        If _SaveAtClose Then
            SaveDockBarConfig()
        End If
    End Sub

    ''' <summary>
    ''' DockBar-Konfiguration sichern
    '''     Diese Einstellungen werden in wb_Main_Menu gelesen und verarbeitet
    ''' </summary>
    Private Sub SaveDockBarConfig(Optional DefaultPath As wb_Global.OrgaBackDockPanelLayoutPath = wb_Global.OrgaBackDockPanelLayoutPath.UserLokal)
        wbDockPanel.SaveAsXml(DkPnlConfigFileName(DefaultPath))
    End Sub

    ''' <summary>
    ''' Läd die Dock-Panel-Konfiguration aus der Konfiguration-Datei (*.xml). Die Konfiguration wird 
    ''' über SaveToXml gesichert.
    ''' </summary>
    <CodeAnalysis.SuppressMessage("Minor Bug", "S4158:Empty collections should not be accessed or iterated", Justification:="<Ausstehend>")>
    Private Sub LoadDockBarConfig()
        'Farb-Schema einstellen
        wbDockPanel.Theme = wb_GlobalSettings.Theme
        'Das Default-Layout kann nicht gelöscht werden
        BtnDelete.Enabled = Not (_LayoutFilename = "Default")

        'Prüfen ob ein Dock-Panel-Konfigurations-File vorhanden ist
        If My.Computer.FileSystem.FileExists(DkPnlConfigFileName) Then

            'falls noch eine alte Konfiguration vorhanden ist
            For i = wbDockPanel.Contents.Count - 1 To 0 Step -1
                wbDockPanel.Contents(i).DockHandler.DockPanel = Nothing
            Next i

            'Liste aller Dock-Panels
            _DockPanelList.Clear()

            'Laden der Konfiguration
            wbDockPanel.LoadFromXml(DkPnlConfigFileName, AddressOf wbBuildDocContent)
            'alle Unterfenster aus der Liste anzeigen und Dock-Panel-State festlegen
            For Each x In _DockPanelList
                'Wenn ein Fenster beim Speichern Im State Float war, wird es anschliessend nicht mehr angezeigt
                If x.DockState = DockState.Float Then
                    x.DockState = DockState.Document
                End If
                x.Show(wbDockPanel, x.DockState)
                Debug.Print("DockState " & x.DockState.ToString)
            Next
        Else
            'Default Fenster-Konfiguration (wenn alles schief geht)
            SetDefaultLayout()
            _LayoutFilename = "Default"
            cbLayouts.Text = _LayoutFilename
            _SaveAtClose = True
        End If
    End Sub

    ''' <summary>
    ''' Füllt die ListBox cbLayouts mit den Layout-Bezeichnungen. Die Bezeichnungen werden aus den FormNamen
    ''' gebildet.
    ''' </summary>
    Private Sub GetLayoutFileNames()
        'Liste alle Konfigurations-Dateien im Verzeichnis
        Dim ConfigFileNames As New List(Of String)
        'Anzeige ausschalten, wegen Geschwindigkeit
        cbLayouts.Visible = False
        cbLayouts.Items.Clear()

        'Globales Verzeichnis ..\Temp\00
        ConfigFileNames = GetDkPnlConfigNameList(wb_GlobalSettings.DockPanelPath(wb_Global.OrgaBackDockPanelLayoutPath.ProgrammGlobal), FormName)
        For Each x In ConfigFileNames
            'aktueller Layout-Filename
            If LayoutFilename = x Then
                cbLayouts.Text = x
            End If
            cbLayouts.Items.Add(x)
        Next

        'Arbeitsplatz Verzeichnis ..\Temp\xx
        ConfigFileNames = GetDkPnlConfigNameList(wb_GlobalSettings.DockPanelPath(wb_Global.OrgaBackDockPanelLayoutPath.UserLokal), FormName)
        For Each x In ConfigFileNames
            'nur noch neue Einträge hinzufügen
            If cbLayouts.FindStringExact(x) = ListBox.NoMatches Then
                'aktueller Layout-Filename
                If LayoutFilename = x Then
                    cbLayouts.Text = x
                End If
                cbLayouts.Items.Add(x)
            End If
        Next

        'Sortieren
        cbLayouts.Sorted = True
        'und wieder anzeigen
        cbLayouts.Visible = True
        'Speicher wieder freigeben
        ConfigFileNames = Nothing
    End Sub

    ''' <summary>
    ''' Die Auswahl in der Drop-Down-Liste hat sich geändert. Neues Layout laden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cbLayouts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLayouts.SelectedIndexChanged
        If LayoutFilename <> cbLayouts.Text Then
            LayoutFilename = cbLayouts.Text
            LoadDockBarConfig()
        End If
    End Sub

    ''' <summary>
    ''' Button "Reload". Layout neu aus Datei laden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnReload_Click(sender As Object, e As EventArgs) Handles BtnReload.Click
        LayoutFilename = cbLayouts.Text
        LoadDockBarConfig()
    End Sub

    ''' <summary>
    ''' Button "Save". Das Layout wird unter dem aktuellen Namen lokal gespeichert.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        'Layout wird lokal gespeichert
        SaveDockBarConfig()
        'Meldung ausgeben
        MessageBox.Show("Layout " & LayoutFilename & " gesichert",
                           "Layout sichern", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' Button "Save As". Öffnet das Fenster DockPanelConfigSaveAs. Auswahl des Layout-Namens
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnSaveAs_Click(sender As Object, e As EventArgs) Handles BtnSaveAs.Click
        Dim DkpPnlConfigSaveAs As New wb_DockBarPanelSaveAs(FormName)
        AddHandler DkpPnlConfigSaveAs.eSaveAs_Click, AddressOf ESaveAs_Click
        DkpPnlConfigSaveAs.ShowDialog(Me)
        DkpPnlConfigSaveAs = Nothing
    End Sub

    ''' <summary>
    ''' Speichert das Layout unter dem angegebene  Namen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="FileName"></param>
    ''' <param name="DefaultPath"></param>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S1172:Unused procedure parameters should be removed", Justification:="<Ausstehend>")>
    Private Sub ESaveAs_Click(sender As Object, FileName As String, DefaultPath As wb_Global.OrgaBackDockPanelLayoutPath)
        'aktuelles Layout unter dem neuen Namen abspeichern
        _LayoutFilename = FileName
        cbLayouts.Text = _LayoutFilename
        'Layout-Files in Status-Bar Listbox aktualisieren/einlesen
        GetLayoutFileNames()
        'Layout sichern
        SaveDockBarConfig(DefaultPath)
    End Sub

    ''' <summary>
    '''  Button "Delete". Löscht das ausgewählte Layout Lokal und Global.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        'Sicherheits-Abfrage
        If MessageBox.Show("Soll das Layout " & LayoutFilename & " wirklich gelöscht werden ",
                           "Layout löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            'aus der Auswahl-Liste entfernen
            cbLayouts.Items.Remove(cbLayouts.SelectedItem)
            'Layout-File wird lokal gelöscht
            System.IO.File.Delete(DkPnlConfigFileName(wb_Global.OrgaBackDockPanelLayoutPath.UserLokal))
            'Layout-File wird global gelöscht
            System.IO.File.Delete(DkPnlConfigFileName(wb_Global.OrgaBackDockPanelLayoutPath.ProgrammGlobal))

            'Default-Layout laden
            _LayoutFilename = "Default"
            cbLayouts.Text = _LayoutFilename
            LoadDockBarConfig()
        End If
    End Sub

    ''' <summary>
    ''' Erzeugt den File-Namen für die Konfig-Datei aus Layout-File-Name und Fom-Name.
    ''' Ohne Angaben wird der lokale Pfad zurückgegeben (..\Temp\xx, wobei xx die Arbeitsplatz-Nummer ist).
    ''' Optional der Globale-Pfad (..\Temp\00)
    ''' </summary>
    ''' <param name="DefaultPath"></param>
    ''' <returns></returns>
    Private ReadOnly Property DkPnlConfigFileName(Optional DefaultPath As wb_Global.OrgaBackDockPanelLayoutPath = wb_Global.OrgaBackDockPanelLayoutPath.UserLokal) As String
        Get
            Return wb_GlobalSettings.DockPanelPath(DefaultPath) & "wb" & FormName & LayoutFilename & ".xml"
        End Get
    End Property

    ''' <summary>
    ''' Extrahiert den Layout-Namen aus dem File-Namen der Config-Datei.
    ''' Wenn der Layout-Name nicht zum Form-Namen passt, wird ein Leerstring zurückgegeben.
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <param name="FormName"></param>
    ''' <returns></returns>
    Public Shared Function DkPnlConfigName(FileName As String, FormName As String) As String
        'Extension entfernen
        FileName = System.IO.Path.GetFileNameWithoutExtension(FileName)
        'wb... entfernen
        FileName = FileName.Replace("wb", "")

        'Prüfen ob der Filename zu diesem Fenster gehört
        If InStr(FileName, FormName) = 1 Then
            'Form-Name entfernen
            Return FileName.Replace(FormName, "")
        Else
            'File gehört nicht zur Form
            Return ""
        End If
    End Function

    ''' <summary>
    ''' Erzeugt eine Liste aller zum Form-Namen passenden Konfigurations-Namen
    ''' </summary>
    ''' <param name="DirName"></param>
    ''' <param name="FormName"></param>
    ''' <returns></returns>
    Public Shared Function GetDkPnlConfigNameList(DirName As String, FormName As String) As IList(Of String)
        'Ordner-Name ohne Backslash am Ende
        Dim oDir As New IO.DirectoryInfo(DirName.TrimEnd("\"))
        'Ergebnis-Array
        Dim FileNames As New List(Of String)

        ' alle Dateien des Ordners
        Dim oFiles As System.IO.FileInfo() = oDir.GetFiles("*.xml")
        Dim oFile As System.IO.FileInfo
        ' Layout-Name
        Dim LayoutName As String = ""

        ' Datei-Array durchlaufen und in ListBox übertragen
        For Each oFile In oFiles
            LayoutName = DkPnlConfigName(oFile.Name, FormName)
            If LayoutName <> "" Then
                FileNames.Add(LayoutName)
            End If
        Next

        Return FileNames
    End Function

    Private Sub WinBack_DockMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tsVersion.Text = "OrgaBack-Produktion " & wb_GlobalSettings.WinBackVersion
        tsIpAdresse.Text = "WinBack " & wb_GlobalSettings.WinBackDBVersion & " (" & wb_GlobalSettings.MySQLServerIP & ")"
        tsKundeName.Text = wb_GlobalSettings.MandantName
    End Sub
End Class
