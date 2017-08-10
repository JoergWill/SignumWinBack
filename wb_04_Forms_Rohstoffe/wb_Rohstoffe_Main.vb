Imports System.Windows.Forms
Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Main
    Implements IExternalFormUserControl
    Private Const FormName As String = "Rohstoffe"
    Private _LayoutFilename As String = Nothing
    Private _DockPanelList As New List(Of DockContent)
    Private _ContextTabs As List(Of GUI.ITab)
    Private _ServiceProvider As Common.IOrgasoftServiceProvider
    Private _MenuService As Common.IMenuService
    Private _ViewProvider As IViewProvider

    'Default-Fenster
    Public RohstoffListe As wb_Rohstoffe_Liste
    Public RohstoffDetails As wb_Rohstoffe_Details

    'alle anderen Fenster werden zur Laufzeit erzeugt
    Public RohstoffVerwendung As wb_Rohstoffe_Verwendung
    Public RohstoffParameter As wb_Rohstoffe_Parameter

    Public Event Close(sender As Object, e As EventArgs) Implements IBasicFormUserControl.Close


    Public Function ExecuteCommand(CommandId As String, Parameter As Object) As Object Implements IBasicFormUserControl.ExecuteCommand
        'MessageBox.Show("ExecuteCommand!" & vbCrLf & CommandId, "AddIn", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Return Nothing
    End Function

    ''' <summary>
    ''' Eindeutiger Schlüssel für das Fenster, ggf. Firmenname.AddIn
    ''' </summary>
    Public ReadOnly Property FormKey As String Implements IBasicFormUserControl.FormKey
        Get
            Return "@WinBack Rohstoff-Verwaltung"
        End Get
    End Property

    ''' <summary>
    ''' Routine wird aufgerufen, wenn das Fenster geladen wurde und angezeigt werden soll
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Die Caption des Fensters muss mit MyBase.Text gesetzt werde</remarks>
    Public Function Init() As Boolean Implements IBasicFormUserControl.Init
        MyBase.Text = "WinBack Rohstoff-Verwaltung"
        Return True
    End Function

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

    ''' <summary>
    ''' Der Filename der aktuellen Dock-Bar-Konfiguration wird im Tag-Objekt gespeichert.
    ''' Die Information kommt aus der winback.ini und wird in der Routine wb_Konfig.SetFormBoundaries ausgelesen (in wb_Main_Menu)
    ''' </summary>
    ''' <returns></returns>
    Private Property LayoutFilename As String
        Get
            If _LayoutFilename Is Nothing Then
                'Dock-Panel-Layout Filename aus winback.ini
                Dim IniFile As New WinBack.wb_IniFile
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

    Private ReadOnly Property DkPnlConfigFileName(Optional DefaultPath As wb_Global.OrgaBackDockPanelLayoutPath = wb_Global.OrgaBackDockPanelLayoutPath.UserLokal) As String
        Get
            Return wb_GlobalSettings.DockPanelPath(DefaultPath) & "wb" & FormName & LayoutFilename & ".xml"
        End Get
    End Property

    Public ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("RohstoffVerwaltung", "WinBack-Rohstoffe", "Verwaltung der WinBack-Rohstoffe")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpRohstoffe", "WinBack Rohstoffe")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnRohstoffDetails", "Details", "weitere Rohstoff-Daten", My.Resources.RohstoffeDetails_32x32, My.Resources.RohstoffeDetails_32x32, AddressOf BtnRohstoffDetails)
                oGrp.AddButton("BtnRohstoffParameter", "Parameter", "Rohstoffparameter und Nährwert-Angaben", My.Resources.RohstoffeParameter_32x32, My.Resources.RohstoffeParameter_32x32, AddressOf BtnRohstoffParameter)
                oGrp.AddButton("BtnRohstoffVerwendung", "Verwendung", "Verwendung des Rohstoffes in Rezepturen", My.Resources.RohstoffeVerwendung_32x32, My.Resources.RohstoffeVerwendung_32x32, AddressOf BtnRohstoffVerwendung)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Public Sub FormClosed() Implements IBasicFormUserControl.FormClosed
        'Fenster-Einstellungen in winback.ini sichern
        wb_Konfig.SaveFormBoundaries(Me.Top, Me.Left, Me.Width, Me.Height, LayoutFilename, FormName)
        'Anzeige wird in OrgaBack beim Schliessen nicht gesichert !
        'SaveDockBarConfig()
    End Sub

    ''' <summary>
    ''' Diese Function wird aufgerufen, wenn das Fenster geschlossen werden soll.
    ''' </summary>
    ''' <param name="Reason"></param>
    ''' <returns>
    ''' False, wenn das Fenster geschlossen werden darf
    ''' True, wenn das Fenster geöffnet bleiben muss
    ''' </returns>
    ''' <remarks></remarks>
    Public Function FormClosing(Reason As Short) As Boolean Implements IBasicFormUserControl.FormClosing
        Return False
    End Function

    Private Sub wb_Rohstoffe_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        wb_Rohstoffe_Shared.Load_RohstoffTables()

        'Layout-Files in Status-Bar Listbox aktualisieren/einlesen
        GetLayoutFileNames()

        ' DockBar Konfiguration aus xml-Datei lesen
        LoadDockBarConfig()
    End Sub

    Private Sub BtnRohstoffDetails()
        RohstoffDetails = New wb_Rohstoffe_Details
        RohstoffDetails.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub BtnRohstoffParameter()
        RohstoffParameter = New wb_Rohstoffe_Parameter
        RohstoffParameter.Show(DockPanel, DockState.Document)
    End Sub

    Private Sub BtnRohstoffVerwendung()
        RohstoffVerwendung = New wb_Rohstoffe_Verwendung
        RohstoffVerwendung.Show(DockPanel, DockState.Document)
    End Sub
    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        'InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub
    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    ''' <param name="ServiceProvider">ServiceProvider von OrgaSoft.NET</param>
    ''' <remarks></remarks>
    Public Sub New(ServiceProvider As Common.IOrgasoftServiceProvider)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ServiceProvider = ServiceProvider
        _MenuService = TryCast(ServiceProvider.GetService(GetType(Common.IMenuService)), Common.IMenuService)
        _ViewProvider = TryCast(ServiceProvider.GetService(GetType(IViewProvider)), IViewProvider)

    End Sub

    ''' <summary>
    ''' DockBar-Konfiguration sichern
    '''     Diese Einstellungen werden in wb_Main_Menu gelesen und verarbeitet
    ''' </summary>
    Private Sub SaveDockBarConfig(Optional DefaultPath As wb_Global.OrgaBackDockPanelLayoutPath = wb_Global.OrgaBackDockPanelLayoutPath.UserLokal)
        DockPanel.SaveAsXml(DkPnlConfigFileName(DefaultPath))
    End Sub

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
                x.Show(DockPanel, x.DockState)
            Next
        Else
            'Default Fenster-Konfiguration (wenn alles schief geht)
            RohstoffListe = New wb_Rohstoffe_Liste
            RohstoffDetails = New wb_Rohstoffe_Details
            RohstoffListe.Show(DockPanel, DockState.DockLeft)
            RohstoffDetails.Show(DockPanel, DockState.DockTop)
        End If
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString

            Case "WinBack.wb_Rohstoffe_Liste"
                RohstoffListe.CloseButtonVisible = False
                _DockPanelList.Add(RohstoffListe)
                Return RohstoffListe

            Case "WinBack.wb_Rohstoffe_Details"
                RohstoffDetails = New wb_Rohstoffe_Details
                _DockPanelList.Add(RohstoffDetails)
                Return RohstoffDetails

            Case "WinBack.wb_Rohstoffe_Verwendung"
                RohstoffVerwendung = New wb_Rohstoffe_Verwendung
                _DockPanelList.Add(RohstoffVerwendung)
                Return RohstoffVerwendung

            Case "WinBack.wb_Rohstoffe_Parameter"
                RohstoffParameter = New wb_Rohstoffe_Parameter
                _DockPanelList.Add(RohstoffParameter)
                Return RohstoffParameter
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub GetLayoutFileNames()
        'Liste alle Konfigurations-Dateien im Verzeichnis
        Dim ConfigFileNames As New List(Of String)
        'Anzeige ausschalten, wegen Geschwindigkeit
        cbLayouts.Visible = False
        cbLayouts.Items.Clear()

        'Globales Verzeichnis ..\Temp\00
        ConfigFileNames = wb_DockBarPanelGlobal.GetDkPnlConfigNameList(wb_GlobalSettings.DockPanelPath(wb_Global.OrgaBackDockPanelLayoutPath.ProgrammGlobal), FormName)
        For Each x In ConfigFileNames
            'aktueller Layout-Filename
            If LayoutFilename = x Then
                cbLayouts.Text = x
            End If
            cbLayouts.Items.Add(x)
        Next

        'Arbeitsplatz Verzeichnis ..\Temp\xx
        ConfigFileNames = wb_DockBarPanelGlobal.GetDkPnlConfigNameList(wb_GlobalSettings.DockPanelPath(wb_Global.OrgaBackDockPanelLayoutPath.UserLokal), FormName)
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

    Private Sub cbLayouts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLayouts.SelectedIndexChanged
        If LayoutFilename <> cbLayouts.Text Then
            LayoutFilename = cbLayouts.Text
            LoadDockBarConfig()
        End If
    End Sub

    Private Sub BtnReload_Click(sender As Object, e As EventArgs) Handles BtnReload.Click
        LayoutFilename = cbLayouts.Text
        LoadDockBarConfig()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        'Layout wird lokal gespeichert
        SaveDockBarConfig()
    End Sub

    Private Sub BtnSaveAs_Click(sender As Object, e As EventArgs) Handles BtnSaveAs.Click
        Dim DkpPnlConfigSaveAs As New wb_DockBarPanelSaveAs(FormName)
        AddHandler DkpPnlConfigSaveAs.eSaveAs_Click, AddressOf ESaveAs_Click
        DkpPnlConfigSaveAs.ShowDialog(Me)
    End Sub

    Private Sub ESaveAs_Click(sender As Object, FileName As String, DefaultPath As wb_Global.OrgaBackDockPanelLayoutPath)
        'aktuelles Layout unter dem neuen Namen abspeichern
        _LayoutFilename = FileName
        cbLayouts.Text = _LayoutFilename
        'Layout-Files in Status-Bar Listbox aktualisieren/einlesen
        GetLayoutFileNames()
        'Layout sichern
        SaveDockBarConfig(DefaultPath)
    End Sub

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

End Class
