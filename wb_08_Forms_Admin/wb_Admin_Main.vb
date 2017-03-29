Imports System.Windows.Forms
Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Admin_Main
    Implements IExternalFormUserControl
    Public AdminSync As New wb_Admin_Sync
    Public AdminDatensicherung As New wb_Admin_Datensicherung
    Public AdminLog As New wb_Admin_Log

    Public Function ExecuteCommand(CommandId As String, Parameter As Object) As Object Implements IBasicFormUserControl.ExecuteCommand
        'MessageBox.Show("ExecuteCommand!" & vbCrLf & CommandId, "AddIn", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Return Nothing
    End Function

    ''' <summary>
    ''' Eindeutiger Schlüssel für das Fenster, ggf. Firmenname.AddIn
    ''' </summary>
    Public ReadOnly Property FormKey As String Implements IBasicFormUserControl.FormKey
        Get
            Return "@WinBack Administration"
        End Get
    End Property

    ''' <summary>
    ''' Routine wird aufgerufen, wenn das Fenster geladen wurde und angezeigt werden soll
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Die Caption des Fensters muss mit MyBase.Text gesetzt werde</remarks>
    Public Function Init() As Boolean Implements IBasicFormUserControl.Init
        MyBase.Text = "WinBack Administration"
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

    Private _ContextTabs As List(Of GUI.ITab)
    Public ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("Administration", "WinBack-Administration", "Einstellungen im WinBack-Office--System")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpAdmin", "WinBack Administration")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("btnSync", "Synchronisation Datenbanken WinBack-OrgaBack", "", My.Resources.MainSync_16x16, My.Resources.MainSync_32x32, AddressOf BtnAdminSyncForm)
                oGrp.AddButton("btnDatensicherung", "Sicherung/Rücksicherung Datenbanken WinBack", "", My.Resources.Datensicherung_16x16, My.Resources.Datensicherung_32x32, AddressOf BtnAdminDatensicherung)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Public Event Close(sender As Object, e As EventArgs) Implements IBasicFormUserControl.Close
    Public Sub FormClosed() Implements IBasicFormUserControl.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'alle erzeugten Fenster wieder schliessen
        '        RohstoffDetails.Close()
        '        RohstoffListe.Close()
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

    Private Sub wb_Admin_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' DockBar Konfiguration aus XML-Datei lesen
        LoadDockBarConfig()
    End Sub

    Private Sub BtnAdminSyncForm()
        AdminSync.Show(DockPanel, DockState.DockTop)
    End Sub
    Private Sub BtnAdminDatensicherung()
        AdminDatensicherung.Show(DockPanel, DockState.DockTop)
    End Sub


    Private _ServiceProvider As Common.IOrgasoftServiceProvider
    Private _MenuService As Common.IMenuService
    Private _ViewProvider As IViewProvider

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

    Private Sub SaveDockBarConfig()
        DockPanel.SaveAsXml(wb_Konfig.DockPanelPath & "wbAdmin.xml")
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml(wb_Konfig.DockPanelPath & "wbAdmin.xml", AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        AdminLog.Show(DockPanel, DockState.DockBottom)
        AdminLog.CloseButtonVisible = False
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "AdminLog"
                Return AdminLog
            Case Else
                Return Nothing
        End Select
    End Function

End Class
