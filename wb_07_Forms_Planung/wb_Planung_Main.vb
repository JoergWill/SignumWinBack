Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Planung_Main
    Implements IExternalFormUserControl

    'Default-Fenster
    Public PlanungListe As New wb_Planung_Liste
    Public PlanungTeiler As New wb_Planung_Teiler

#Region "Signum"
    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        MyBase.New(ServiceProvider)
    End Sub

    ''' <summary>
    ''' Fenster-Name (Caption). Wird von Init() aufgerufen
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormText As String
        Get
            Return "WinBack Produktions-Planung"
        End Get
    End Property

    ''' <summary>
    ''' Eindeutiger Name für die Basis-Form. 
    ''' Unter diesem Namen werden die Einstellungen in der winback.ini gespeichert.
    ''' 
    ''' Die DockPanel-Konfiguration wird gespeichert unter wbXXXXYYYY.xml, dabei ist XXXX der FormName und YYYY der Layout-Name.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormName As String
        Get
            Return "Produktion"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        PlanungListe.Show(DockPanel, DockState.DockLeft)
    End Sub

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("Teig-Herstellung", "Teig-Herstellung", "WinBack Teig-Herstellung")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpPlanung", "Teigliste/Backzettel")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnExportProdListen", "Export/Drucken", "Backzettel und Teigliste drucken. Produktionsplan an WinBack senden", My.Resources.RohstoffeDetails_32x32, My.Resources.RohstoffeDetails_32x32, AddressOf BtnProdListeExport)
                oGrp.AddButton("BtnProdTeiler", "Einstellungen Optimierung", "Einstellungen zur Optimierung der Teigliste", My.Resources.EditKonfig_16x16, My.Resources.EditKonfig_32x32, AddressOf BtnPlanungTeiler)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString

            Case "WinBack.wb_Planung_Liste"
                PlanungListe.CloseButtonVisible = False
                _DockPanelList.Add(PlanungListe)
                Return PlanungListe

            Case "WinBack.wb_Planung_Teiler"
                PlanungTeiler = New wb_Planung_Teiler
                _DockPanelList.Add(PlanungTeiler)
                Return PlanungTeiler

            Case Else
                Return Nothing
        End Select
    End Function
#End Region

    Public Overrides Function Init() As Boolean Implements IBasicFormUserControl.Init
        'Init aus der Basis-Klasse aufrufen (zuerst)
        Init = MyBase.Init()
    End Function

    Private Sub BtnProdListeExport()
        'TODO in Planung Liste den Export anstossen
    End Sub

    Private Sub BtnPlanungTeiler()
        PlanungTeiler = New wb_Planung_Teiler
        PlanungTeiler.Show(DockPanel, DockState.DockTop)
    End Sub
End Class
