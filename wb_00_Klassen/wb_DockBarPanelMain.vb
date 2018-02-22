Imports System.Windows.Forms
Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
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
Public Class wb_DockBarPanelMain
    Implements IExternalFormUserControl
    Private _LayoutFilename As String = Nothing
    Private _SaveAtClose As Boolean = False
    Protected _DockPanelList As New List(Of DockContent)
    Protected _ContextTabs As List(Of ITab)
    Protected _ServiceProvider As IOrgasoftServiceProvider
    Protected _MenuService As IMenuService
    Protected _ViewProvider As IViewProvider

    Public Event Close(sender As Object, e As EventArgs) Implements IBasicFormUserControl.Close

#Region "MustOverride"
    ''' <summary>
    ''' Fenster-Name (Caption). Wird von Init() aufgerufen
    ''' </summary>
    ''' <returns></returns>
    Public Overridable ReadOnly Property FormText As String
        Get
            Throw New NotImplementedException
            Return "FormText"
        End Get
    End Property

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
#Region "Signum"
    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    ''' <param name="ServiceProvider">ServiceProvider von OrgaSoft.NET</param>
    ''' <remarks></remarks>
    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ServiceProvider = ServiceProvider
        _MenuService = TryCast(ServiceProvider.GetService(GetType(Common.IMenuService)), Common.IMenuService)
        _ViewProvider = TryCast(ServiceProvider.GetService(GetType(IViewProvider)), IViewProvider)
    End Sub

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
    End Sub

    ''' <summary>
    ''' Routine wird aufgerufen, wenn das Fenster geladen wurde und angezeigt werden soll
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Die Caption des Fensters muss mit MyBase.Text gesetzt werde</remarks>
    Public Overridable Function Init() As Boolean Implements IBasicFormUserControl.Init
        'Fenster-Bezeichnung
        MyBase.Text = FormText
        'Layout-Files in Status-Bar Listbox aktualisieren/einlesen
        GetLayoutFileNames()
        'DockBar Konfiguration aus xml-Datei lesen
        LoadDockBarConfig()
        Return True
    End Function

    ''' <summary>
    ''' Diese Function wird aufgerufen, wenn das Fenster geschlossen werden soll.
    ''' </summary>
    ''' <param name="Reason"></param>
    ''' <returns>
    ''' False, wenn das Fenster geschlossen werden darf
    ''' True, wenn das Fenster geöffnet bleiben muss
    ''' </returns>
    ''' <remarks></remarks>
    Public Overridable Function FormClosing(Reason As Short) As Boolean Implements IBasicFormUserControl.FormClosing
        Return False
    End Function

    Public Function ExecuteCommand(CommandId As String, Parameter As Object) As Object Implements IBasicFormUserControl.ExecuteCommand
        'MessageBox.Show("ExecuteCommand!" & vbCrLf & CommandId, "AddIn", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Return Nothing
    End Function

    ''' <summary>
    ''' Eindeutiger Schlüssel für das Fenster, ggf. Firmenname.AddIn
    ''' z.B. Return "@WinBack DockPanelMain"
    ''' </summary>
    Public ReadOnly Property FormKey As String Implements IBasicFormUserControl.FormKey
        Get
            Return "@" & FormText
        End Get
    End Property
    ''' <summary>
    ''' Minimale Höhe des UserControls
    ''' </summary>
    Public ReadOnly Property MinHeight As Integer Implements IBasicFormUserControl.MinHeight
        Get
            Return Me.MinimumSize.Height
        End Get
    End Property

    ''' <summary>
    ''' Minimale Breite des UserControls
    ''' </summary>
    Public ReadOnly Property MinWidth As Integer Implements IBasicFormUserControl.MinWidth
        Get
            Return Me.MinimumSize.Width
        End Get
    End Property

    ''' <summary>
    ''' Gibt an, ob man die Größe dieses UserControls ändern darf
    ''' </summary>
    Public ReadOnly Property Sizable As Boolean Implements IBasicFormUserControl.Sizable
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Bezeichnung und Caption des UserControls
    ''' </summary>
    Public Shadows ReadOnly Property Text() As String Implements IBasicFormUserControl.Text
        Get
            Return MyBase.Text
        End Get
    End Property

    Public ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property
#End Region

    ''' <summary>
    ''' Die Information kommt aus der winback.ini und wird in der Routine wb_DockBarPanelShared.SetFormBoundaries ausgelesen (in wb_Main_Menu)
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
            End Try
        End Set
    End Property

    Public Sub FormClosed() Implements IBasicFormUserControl.FormClosed
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
        DockPanel.SaveAsXml(DkPnlConfigFileName(DefaultPath))
    End Sub

    ''' <summary>
    ''' Läd die Dock-Panel-Konfiguration aus der Konfiguration-Datei (*.xml). Die Konfiguration wird 
    ''' über SaveToXml gesichert.
    ''' </summary>
    Private Sub LoadDockBarConfig()
        'Farb-Schema einstellen
        DockPanel.Theme = wb_GlobalOrgaBack.Theme
        'Das Default-Layout kann nicht gelöscht werden
        BtnDelete.Enabled = Not (_LayoutFilename = "Default")

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
    End Sub

    ''' <summary>
    ''' Speichert das Layout unter dem angegebene  Namen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="FileName"></param>
    ''' <param name="DefaultPath"></param>
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
                           "Layout löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
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
        FileNames.Clear()

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
End Class
