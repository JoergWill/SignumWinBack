'---------------------------------------------------------
'19.04.2016/ V0.9/JW            :Neuanlage
'Bearbeitet von                 :Will
'
'Änderungen:
'---------------------------------------------------------
'Beschreibung:
'Anzeige der WinBack-Produktions-Linien über
'VNC-Viewer
'---------------------------------------------------------

Imports System.Windows.Forms
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports Signum.OrgaSoft.AddIn.WinBack.wb_Konfig
Imports WeifenLuo.WinFormsUI.Docking


Public Class wb_MainLinien
    Implements IExternalFormUserControl

    Private _ServiceProvider As Common.IOrgasoftServiceProvider
    Private _MenuService As Common.IMenuService
    Private _ViewProvider As IViewProvider

    Private _ContextTabs As List(Of GUI.ITab)

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
    ''' Routine wird aufgerufen, wenn das Fenster geladen wurde und angezeigt werden soll
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Die Caption des Fensters muss mit MyBase.Text gesetzt werde</remarks>
    Public Function Init() As Boolean Implements IBasicFormUserControl.Init
        MyBase.Text = "WinBack Produktions-Linien"
        Return True
    End Function

    Public Function ExecuteCommand(CommandId As String, Parameter As Object) As Object Implements IBasicFormUserControl.ExecuteCommand
        Return Nothing
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
    Public Function FormClosing(Reason As Short) As Boolean Implements IBasicFormUserControl.FormClosing
        SaveInKonfig()
        Return False
    End Function
    Public Event Close(sender As Object, e As EventArgs) Implements IBasicFormUserControl.Close

    Public Sub FormClosed() Implements IBasicFormUserControl.FormClosed
        SaveInKonfig()
    End Sub

    ''' <summary>
    ''' Eindeutiger Schlüssel für das Fenster, ggf. Firmenname.AddIn
    ''' </summary>
    Public ReadOnly Property FormKey As String Implements IBasicFormUserControl.FormKey
        Get
            Return "@WinBack.MainLinien"
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

    ''' <summary>
    ''' Erzeugt neue Tabs im Ribbon-Control
    ''' </summary>
    Public ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("LinienVerwaltung", "WinBack-Produktion Linien", "Winback-Produktion Linien verwalten")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpLinien", "WinBack Linien")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnLinienNew", "Linie Neu", "Neue Linie anlegen", My.Resources.LinienNeu_32x32, My.Resources.LinienNeu_32x32, AddressOf BtnLinien)
                oGrp.AddButton("BtnLinienRename", "Umbenennen", "Linie umbenennen", My.Resources.LinienBearbeiten_32x32, My.Resources.LinienBearbeiten_32x32, AddressOf BtnLinien)
                oGrp.AddButton("BtnLinienRemove", "Löschen", "Linie löschen", My.Resources.LinienLoeschen_32x32, My.Resources.LinienLoeschen_32x32, AddressOf BtnLinien)
                oGrp.AddButton("BtnLinienAutoInstall", "AutoInstall", "Alle Linien automatisch installieren", My.Resources.LinienAutoInstall_32x32, My.Resources.LinienAutoInstall_32x32, AddressOf BtnLinien)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Private Sub wb_MainLinien_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Anzeige alle konfigurierten VNC-Linien
        ' Auslesen aus .ini-Datei

        Dim IniFile As New Signum.OrgaSoft.AddIn.WinBack.wb_Konfig
        Dim i As Integer = 0
        Dim IPAdresse, IPComment As String
        Dim ListItem As ListViewItem

        Do
            i += 1
            IPAdresse = IniFile.ReadString("VNC", "IP" & i.ToString)
            IPComment = IniFile.ReadString("VNC", "Comment" & i.ToString)
            If IPAdresse IsNot "" Then
                ListItem = VNCview.Items.Add(IPAdresse, IPComment, 0)
            Else
                Exit Do
            End If
        Loop

    End Sub
    Private Sub BtnLinien()

    End Sub

    Private Sub VNCview_Click(sender As Object, e As EventArgs) Handles VNCview.Click
        If (VNCview.SelectedItems(0).Index <VNCview.Items.Count) And (VNCview.SelectedItems(0).Index >= 0) Then
            tBezeichnung.Text = VNCview.SelectedItems.Item(0).Text
            tAdresse.Text = VNCview.SelectedItems.Item(0).Name
        End If
    End Sub

    Private Sub VNCview_DoubleClick(sender As Object, e As EventArgs) Handles VNCview.DoubleClick
        Dim cmdLinie, cmdParam As String
        Dim p As New Process

        'VNC-Viewer starten
        cmdLinie = "c:\Programme\Winback\vncviewer.exe"
        If (VNCview.SelectedItems(0).Index <VNCview.Items.Count) And (VNCview.SelectedItems(0).Index >= 0) Then
            cmdParam = VNCview.SelectedItems.Item(0).Name() & " /password herbst"

            p.StartInfo.FileName = cmdLinie
            p.StartInfo.Arguments = cmdParam
            p.StartInfo.UseShellExecute = False
            p.StartInfo.RedirectStandardOutput = False
            p.StartInfo.CreateNoWindow = False
            p.Start()

        End If
    End Sub

    Private Sub Panel1_Leave(sender As Object, e As EventArgs) Handles Panel1.Leave
        Dim i As Integer
        If (VNCview.SelectedItems(0).Index < VNCview.Items.Count) And (VNCview.SelectedItems(0).Index >= 0) Then
            i = VNCview.SelectedItems(0).Index
            VNCview.Items(i).Text = tBezeichnung.Text
            VNCview.Items(i).Name = tAdresse.Text
        End If
    End Sub

    Private Sub SaveInKonfig()
        Dim IniFile As New Signum.OrgaSoft.AddIn.WinBack.wb_Konfig
        Dim i As Integer
        For i = 1 To VNCview.Items.Count
            IniFile.WriteString("VNC", "IP" & i.ToString, VNCview.Items(i - 1).Name)
            If IniFile.WriteResult = False Then
                Exit For
            End If
            IniFile.WriteString("VNC", "Comment" & i.ToString, VNCview.Items(i - 1).Text)
        Next
    End Sub
End Class

