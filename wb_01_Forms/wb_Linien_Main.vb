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


Public Class wb_Linien_Main
    Implements IExternalFormUserControl

    Private WithEvents LinienListe As New wb_Linien_Liste
    Private WithEvents LinenDetails As New wb_Linien_Details

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
        SaveDockBarConfig()
        LinienDetailInfoHasChanged()
        LinienListe.SaveItems()
        Return False
    End Function

    Public Event Close(sender As Object, e As EventArgs) Implements IBasicFormUserControl.Close
    Public Sub FormClosed() Implements IBasicFormUserControl.FormClosed
        SaveDockBarConfig()
        LinienDetailInfoHasChanged()
        LinienListe.SaveItems()
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
        ' DockBar Konfiguration aus XML-Datei lesen
        LoadDockBarConfig()
    End Sub

    Private Sub BtnLinien()

    End Sub

    Private Sub DetailInfo() Handles LinienListe.ItemSelected
        LinenDetails.aktBezeichnung = LinienListe.aktBezeichnung
        LinenDetails.aktAdresse = LinienListe.aktAdresse
    End Sub

    Private Sub LinienDetailInfoHasChanged() Handles LinenDetails.DetailInfoHasChanged
        LinienListe.aktBezeichnung = LinenDetails.aktBezeichnung
        LinienListe.aktAdresse = LinenDetails.aktAdresse
    End Sub

    Private Sub SaveDockBarConfig()
        DockPanel.SaveAsXml("C:\Users\will.WINBACK\AppData\Roaming\WinBack\test.xml")
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml("C:\Users\will.WINBACK\AppData\Roaming\WinBack\test.xml", AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        LinenDetails.Show(DockPanel, DockState.DockTop)
        LinienListe.Show(DockPanel, DockState.DockLeft)
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "LinienListe"
                Return LinienListe
            Case "LinenDetails"
                Return LinenDetails
            Case Else
                Return Nothing
        End Select
    End Function

End Class

