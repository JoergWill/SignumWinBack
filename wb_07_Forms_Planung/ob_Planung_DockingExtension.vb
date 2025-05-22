Imports System.Reflection
Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.GUI

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Erweiterung des Produktions-Dockingfensters um ein Fenster WinBack-Teigherstellung.")>
Public Class ob_Planung_DockingExtension
    Implements IDockingExtension

    Private _MenuService As IMenuService
    Private _ViewProvider As IViewProvider
    Private _ContextTabs As List(Of GUI.ITab)
    Private _SubForms As New Dictionary(Of String, IBasicFormUserControl)
    Private _Extendee As IFrameWorkClass

    Private bContextTabInitialized As Boolean = False
    Public Property InfoContainer As IInfoContainer Implements IExtension.InfoContainer
    Public Property ServiceProvider As IOrgasoftServiceProvider Implements IExtension.ServiceProvider
#Region "Signum"
    ''' <summary>
    ''' Falls die Extension ein eigenes Context-Ribbon zum bestehenden Ribbon hinzufügen möchte, kann sie dieses hier zurückliefern
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ContextTabs As ITab() Implements IDockingExtension.ContextTabs
        Get
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Klasse, deren Docking-Layout erweitert werden soll (Produktion)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ExtendedType As ObjectEnum Implements IDockingExtension.ExtendedType
        Get
            'Return Nothing
            'ab Signum.OrgaSoft.Contracts.dll Version 3.0.6530
            Return ObjectEnum.ProductionPlanning
        End Get
    End Property

    ''' <summary>
    ''' Referenz auf die Framework-Klasse, die im Docking-Fenster derzeit angezeigt wird
    ''' </summary>
    Public Property Extendee As IFrameWorkClass Implements IDockingExtension.Extendee
        Get
            Return _Extendee
        End Get
        Set(value As IFrameWorkClass)
            _Extendee = value
        End Set
    End Property

    ''' <summary>
    ''' Initialisierung des AddIns beim Starten von Orgasoft.
    ''' </summary>
    ''' <remarks>
    ''' Achtung: Der FormController ist zu diesem Zeitpunkt noch nicht verfügbar!
    ''' Dieser wird erst erzeugt und gesetzt, wenn das Fenster auch angezeigt werden soll.
    ''' </remarks>
    Public Sub Initialize() Implements IExtension.Initialize
        'in wb_Main registrieren
        wb_Main_Shared.RegisterAddIn("ob_Planung_DockingExtension")
        'AssemblyResolve wird definiert in WinBackAddIn.Erweiterte Kompilierungsoptionen
        If wb_Global.AssemblyResolve Then
            'Die eigenen dll-Files in sep. Verzeichnis verlagern
            AddHandler System.AppDomain.CurrentDomain.AssemblyResolve, AddressOf MyAssemblyResolve
        End If

        _MenuService = TryCast(ServiceProvider.GetService(GetType(IMenuService)), IMenuService)
        _ViewProvider = TryCast(ServiceProvider.GetService(GetType(IViewProvider)), IViewProvider)
    End Sub

    Private Function MyAssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
        Return wb_Main_Shared.MyAssemblyResolve(sender, args, GetType(ob_Planung_DockingExtension).Assembly)
    End Function

    ''' <summary>
    ''' Liefert zu einem FormKey eine Instanz des UserControls zurück
    ''' </summary>
    ''' <param name="FormKey"></param>
    ''' <returns></returns>
    Public Function ProvideInstance(FormKey As String) As IBasicFormUserControl Implements IDockingExtension.ProvideInstance
        Return Nothing
    End Function

    ''' <summary>
    ''' Liste aller FormKeys, für die das AddIn Unterfenster definiert
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SubFormKeys As String() Implements IDockingExtension.SubFormKeys
        Get
            Return _SubForms.Keys.ToArray
        End Get
    End Property

    ''' <summary>
    ''' Liefert eine Instanz des FormControllers, über den das AddIn eigene Fenster öffnen und mit anderen Fenstern kommunizieren kann
    ''' </summary>
    ''' <returns></returns>
    Public Property FormController As IFormControllerBasic Implements IDockingExtension.FormController
#End Region
    ''' <summary>
    ''' Diese Routine wird immer aufgerufen, wenn ein DockingController vom passenden Typ erzeugt wird. 
    ''' Hier können Einträge in die bestehenden Context-Tabs hinzugefügt werden. 
    ''' Achtung: Das Hinzufügen darf nur beim ersten Mal passieren, die Context-Tabs werden gecached!
    ''' </summary>
    Public Sub InitializeContextTabs() Implements IDockingExtension.InitializeContextTabs
        If Not bContextTabInitialized Then
            'einmalige Ausführung sicherstellen
            bContextTabInitialized = True
            'fügt einen Tab im Artkel-Ribbon(rtabArtikel) hinzu
            Dim oSystemTab = From oTab In Me.FormController.ContextualTabs Where oTab.Name = "rtabProduktionsplanung" Select oTab
            If oSystemTab IsNot Nothing AndAlso oSystemTab.Count > 0 Then
                oSystemTab(0).GetGroups(0).AddButton("WinBackProduktionBtn", "Teigherstellung", "Produktion Chargen-Optimierung und Teig-Herstellung", My.Resources.MainProduktionsPlanung_16x16, My.Resources.MainProduktionsPlanung_32x32, AddressOf Produktion)
            End If
        End If
    End Sub

    Private Sub Produktion()
        'Produktions-Liste in OrgaBack speichern (eventuelle Änderungen werden in pq_Produktionsauftrag übernommen
        TryCast(_Extendee, INavigationClass).Update()

        'Lieferdatum merken
        wb_GlobalSettings.ProdPlanDatum = Extendee.PropertyValueCollection(1).Value
        'Produktions-Filiale merken
        wb_GlobalSettings.ProdPlanfiliale = Extendee.PropertyValueCollection(0).Value
        'Produktions-Daten beim Öffnen des WinBack-Formulars einlesen
        wb_GlobalSettings.ProdPlanReadOnOpen = True

        'WinBack-Produktions-Planung (Teig-Herstellung)
        Dim xForm As IForm = _ViewProvider.OpenForm(New wb_Planung_Main(ServiceProvider), My.Resources.MainProduktionsPlanung_16x16)
        'Fensterposition aus winback.ini
        wb_DockBarPanelShared.SetFormBoundaries(xForm, "Produktion")
    End Sub
End Class
