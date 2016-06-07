Imports System.Windows.Forms
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_User_Main
    Implements IExternalFormUserControl
    Private UserListe As New wb_User_Liste
    Private UserDetails As New wb_User_Details
    Private UserRechte As New wb_User_Rechte


    Public Function ExecuteCommand(CommandId As String, Parameter As Object) As Object Implements IBasicFormUserControl.ExecuteCommand
        'MessageBox.Show("ExecuteCommand!" & vbCrLf & CommandId, "AddIn", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Return Nothing
    End Function

    ''' <summary>
    ''' Eindeutiger Schlüssel für das Fenster, ggf. Firmenname.AddIn
    ''' </summary>
    Public ReadOnly Property FormKey As String Implements IBasicFormUserControl.FormKey
        Get
            Return "@WinBack Mitarbeiter-Verwaltung"
        End Get
    End Property

    ''' <summary>
    ''' Routine wird aufgerufen, wenn das Fenster geladen wurde und angezeigt werden soll
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Die Caption des Fensters muss mit MyBase.Text gesetzt werde</remarks>
    Public Function Init() As Boolean Implements IBasicFormUserControl.Init
        MyBase.Text = "WinBack Mitarbeiter-Verwaltung"
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
                Dim oNewTab = _MenuService.AddContextTab("UserVerwaltung", "WinBack-Mitarbeiter", "Verwaltung der aktiven WinBack-Mitarbeiter")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpUser", "WinBack Mitarbeiter")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnUserNew", "Neuer Mitarbeiter", "Neuen Mitarbeiter anlegen", My.Resources.UserNeu_32x32, My.Resources.UserNeu_32x32, AddressOf BtnUserNew)
                oGrp.AddButton("BtnUserPasswd", "Passwort ändern", "Neues Mitarbeiter-Passwort vergeben", My.Resources.UserPasswd_32x32, My.Resources.UserPasswd_32x32, AddressOf BtnUserPasswd)
                oGrp.AddButton("BtnUserPrintList", "Mitarbeiter-Liste", "Liste aller Mitarbeiter drucken", My.Resources.UserListe_32x32, My.Resources.UserListe_32x32, AddressOf btnUserPrint)
                oGrp.AddButton("BtnUserGroup", "Mitarbeiter-Gruppen", "Gruppen und Gruppen-Rechte verwalten", My.Resources.UserGruppen_32x32, My.Resources.UserGruppen_32x32, AddressOf BtnUserGroup)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Public Event Close(sender As Object, e As EventArgs) Implements IBasicFormUserControl.Close
    Public Sub FormClosed() Implements IBasicFormUserControl.FormClosed
        'alle erzeugten Fenster wieder schliessen !!!
        UserDetails.Close()
        UserListe.Close()
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

    Private Sub wb_User_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' DockBar Konfiguration aus XML-Datei lesen
        LoadDockBarConfig()
    End Sub

    Private Sub BtnUserNew()
        Throw New NotImplementedException
    End Sub

    Private Sub BtnUserPasswd()
        Throw New NotImplementedException
    End Sub

    Private Sub btnUserPrint()
        Throw New NotImplementedException
    End Sub

    Private Sub BtnUserGroup()
        Throw New NotImplementedException
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
        DockPanel.SaveAsXml(My.Settings.OrgaSoftDockPanelPath & "wbUser.xml")
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml(My.Settings.OrgaSoftDockPanelPath & "wbUser.xml", AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        UserDetails.Show(DockPanel, DockState.DockTop)
        UserDetails.CloseButtonVisible = False
        UserListe.Show(DockPanel, DockState.DockLeft)
        UserListe.CloseButtonVisible = False
        UserRechte.Show(DockPanel, DockState.DockBottom)
        UserRechte.CloseButtonVisible = False
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "UserListe"
                Return UserListe
            Case "UserDetails"
                Return UserDetails
            Case "UserRechte"
                Return UserRechte
            Case Else
                Return Nothing
        End Select
    End Function

End Class
