Imports System.Windows.Forms
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rezept_Main
    Implements IExternalFormUserControl
    Public RezeptListe As New wb_Rezept_Liste
    Public RezeptDetails As New wb_Rezept_Details
    Public RezeptHinweise As New wb_Rezept_Hinweise
    Public RezeptHistorie As New wb_Rezept_Historie

    Public Function ExecuteCommand(CommandId As String, Parameter As Object) As Object Implements IBasicFormUserControl.ExecuteCommand
        'MessageBox.Show("ExecuteCommand!" & vbCrLf & CommandId, "AddIn", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Return Nothing
    End Function

    ''' <summary>
    ''' Eindeutiger Schlüssel für das Fenster, ggf. Firmenname.AddIn
    ''' </summary>
    Public ReadOnly Property FormKey As String Implements IBasicFormUserControl.FormKey
        Get
            Return "@WinBack Rezept-Verwaltung"
        End Get
    End Property

    ''' <summary>
    ''' Routine wird aufgerufen, wenn das Fenster geladen wurde und angezeigt werden soll
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Die Caption des Fensters muss mit MyBase.Text gesetzt werde</remarks>
    Public Function Init() As Boolean Implements IBasicFormUserControl.Init
        MyBase.Text = "WinBack Rezept-Verwaltung"
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
                Dim oNewTab = _MenuService.AddContextTab("RezeptVerwaltung", "WinBack-Rezepte", "Verwaltung der WinBack-Rezepturen")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpRezepte", "WinBack Rezepte")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnRezeptPrintList", "Rezept-Liste", "Liste aller Rezepte drucken", My.Resources.UserListe_32x32, My.Resources.UserListe_32x32, AddressOf btnUserPrint)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Public Event Close(sender As Object, e As EventArgs) Implements IBasicFormUserControl.Close
    Public Sub FormClosed() Implements IBasicFormUserControl.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'alle erzeugten Fenster wieder schliessen !!!
        RezeptDetails.Close()
        RezeptListe.Close()
        RezeptHinweise.Close()
        RezeptHistorie.Close()
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
        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        wb_User_Shared.LoadGrpTexte()
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
        DockPanel.SaveAsXml(wb_Konfig.DockPanelPath & "wbUser.xml")
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml(wb_Konfig.DockPanelPath & "wbUser.xml", AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        RezeptHinweise.Show(DockPanel, DockState.Document)
        RezeptHinweise.CloseButtonVisible = False
        RezeptHistorie.Show(DockPanel, DockState.Document)
        RezeptHistorie.CloseButtonVisible = False
        RezeptDetails.Show(DockPanel, DockState.DockTop)
        RezeptDetails.CloseButtonVisible = False
        RezeptListe.Show(DockPanel, DockState.DockLeft)
        RezeptListe.CloseButtonVisible = False
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "RezeptListe"
                Return RezeptListe
            Case "RezeptDetails"
                Return RezeptDetails
            Case "RezeptHinweise"
                Return RezeptHinweise
            Case "RezeptHistorie"
                Return RezeptHistorie
            Case Else
                Return Nothing
        End Select
    End Function

End Class
