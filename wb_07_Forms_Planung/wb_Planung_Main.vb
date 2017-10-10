Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Planung_Main
    Implements IExternalFormUserControl

    'Default-Fenster
    Public PlanungListe As New wb_Planung_Liste

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
                Dim oNewTab = _MenuService.AddContextTab("Planung", "Planung", "WinBack Produktionsplanung")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpPlanung", "Produktions-Liste/Backzettel")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnExportProdListe", "Export", "Produktionsplan an WinBack senden", My.Resources.RohstoffeDetails_32x32, My.Resources.RohstoffeDetails_32x32, AddressOf BtnProdListeExport)
                oGrp.AddButton("BtnPrintProdListe", "Drucken", "Backzettel drucken", My.Resources.RohstoffeParameter_32x32, My.Resources.RohstoffeParameter_32x32, AddressOf BtnProdListePrint)
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

            Case Else
                Return Nothing
        End Select
    End Function
#End Region

    Public Overrides Function Init() As Boolean Implements IBasicFormUserControl.Init
        'Init aus der Basis-Klasse aufrufen (zuerst)
        Init = MyBase.Init()
        'Bestelldaten aus Stored-Procedure lsen
        GetOrderData()

    End Function

    Private Sub BtnProdListeExport()
    End Sub

    Private Sub BtnProdListePrint()
    End Sub

    ''' <summary>
    ''' Ermittelt die Bestelldaten aus dem OrgaBack Backzettel
    ''' </summary>
    Private Sub GetOrderData()
        Dim oFactory As IFactoryService = TryCast(_ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)
        Dim oSetting As ISettingService = TryCast(_ServiceProvider.GetService(GetType(ISettingService)), ISettingService)
        Dim oData As IData = oFactory.GetData

        'TODO Query muss überarbeitet werden - Backzettel wird in OrgaBack erzeugt (31.08.2017)
        Using oTable = oData.OpenQuery(Database.Main, "pq_Produktionsauftrag", LockType.ReadOnly, (1), (-1), ("FB"), ("KB"), ("20170830"))
            Debug.Print("Anzahl der Spalten " & oTable.Columns.Count)
            Debug.Print("Anzahl der Zeilen  " & oTable.Rows.Count)

            For Each cRow As DataRow In oTable.Rows
                For Each cCol As DataColumn In oTable.Columns
                    Debug.Print("Produktions-Auftrag " & cCol.ColumnName & " = " & cRow(cCol.Ordinal))
                Next
            Next
        End Using


    End Sub
End Class
