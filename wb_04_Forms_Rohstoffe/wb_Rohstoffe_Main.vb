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

    Public RohstoffListe As New wb_Rohstoffe_Liste
    Public RohstoffDetails As New wb_Rohstoffe_Details
    Public RohstoffVerwendung As New wb_Rohstoffe_Verwendung
    Public RohstoffParameter As New wb_Rohstoffe_Parameter

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
                Debug.Print("DockBarConfigFileName " & DkPnlConfigFileName & "Check Files exists")
                If Not My.Computer.FileSystem.FileExists(DkPnlConfigFileName) Then
                    'vom Default-Ordner kopieren
                    System.IO.File.Copy(DkPnlConfigFileName(True), DkPnlConfigFileName)
                End If
            Catch ex As Exception
            End Try
        End Set
    End Property

    Private ReadOnly Property DkPnlConfigFileName(Optional DefaultPath As Boolean = False) As String
        Get
            Return wb_GlobalSettings.DockPanelPath(DefaultPath) & "wb" & FormName & LayoutFilename & ".xml"
        End Get
    End Property

    Private Function DkPnlConfigName(FileName As String) As String
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

    Public ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("RohstoffVerwaltung", "WinBack-Rohstoffe", "Verwaltung der WinBack-Rohstoffe")
                ' Das neue RibbonTab erhält eine Gruppe
                '                Dim oGrp = oNewTab.AddGroup("GrpRohstoffe", "WinBack Rohstoffe")
                Dim oGrp = oNewTab.AddGroup("GrpRohstoffe", "WinBack Rohstoffe")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnRohstoffDetails", "Details", "weitere Rohstoff-Daten", My.Resources.UserNeu_32x32, My.Resources.UserNeu_32x32, AddressOf BtnRohstoffDetails)
                oGrp.AddButton("BtnRohstoffParameter", "Parameter", "Rohstoffparameter und Nährwert-Angaben", My.Resources.UserNeu_32x32, My.Resources.UserNeu_32x32, AddressOf BtnRohstoffParameter)
                oGrp.AddButton("BtnRohstoffVerwendung", "Verwendung", "Verwendung des Rohstoffes in Rezepturen", My.Resources.UserNeu_32x32, My.Resources.UserNeu_32x32, AddressOf BtnRohstoffVerwendung)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Public Sub FormClosed() Implements IBasicFormUserControl.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
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

        ' DockBar Konfiguration aus XML-Datei lesen
        LoadDockBarConfig()
    End Sub

    Private Sub BtnRohstoffDetails()
        RohstoffDetails.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub BtnRohstoffParameter()
        RohstoffParameter.Show(DockPanel, DockState.Document)
    End Sub
    Private Sub BtnRohstoffVerwendung()
        RohstoffVerwendung.Show(DockPanel, DockState.Document)
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
    ''' Fenster-Einstellungen in winback.ini sichern
    '''     Diese Einstellungen werden in wb_Main_Menu gelesen und verarbeitet
    ''' </summary>
    Private Sub SaveDockBarConfig()
        DockPanel.SaveAsXml(DkPnlConfigFileName)
        wb_Konfig.SaveFormBoundaries(Me.Top, Me.Left, Me.Width, Me.Height, Me.Tag, FormName)
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
                x.Show(DockPanel, x.DockState)
            Next
        Else
            'Default Fenster-Konfiguration (wenn alles schief geht)
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
        ' Directory-Object erstellen
        Dim s As String = wb_GlobalSettings.DockPanelPath(True)
        Dim oDir As New IO.DirectoryInfo(s.TrimEnd("\"))
        ' alle Dateien des Ordners
        Dim oFiles As System.IO.FileInfo() = oDir.GetFiles("*.xml")
        Dim oFile As System.IO.FileInfo
        ' Layout-Name
        Dim LayoutName As String = ""
        cbLayouts.Items.Clear()

        ' Datei-Array durchlaufen und in ListBox übertragen
        For Each oFile In oFiles
            LayoutName = DkPnlConfigName(oFile.Name)
            If LayoutName <> "" Then
                cbLayouts.Items.Add(LayoutName)
                If LayoutName = LayoutFilename Then
                    cbLayouts.Text = LayoutName
                End If
            End If
        Next
    End Sub

    Private Sub BtnReload_Click(sender As Object, e As EventArgs) Handles BtnReload.Click
        LayoutFilename = cbLayouts.Text
        LoadDockBarConfig()
    End Sub
End Class
