Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Admin_Main
    Implements IExternalFormUserControl

    'Default-Fenster
    Public AdminLog As New wb_Admin_Log
    'alle anderen Fenster werden zur Laufzeit erzeugt
    Public AdminSync As wb_Admin_Sync
    Public AdminUpdate As wb_Admin_UpdateDatabase
    Public AdminDatensicherung As wb_Admin_Datensicherung
    Public AdminEditIni As wb_Admin_EditIni

    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        MyBase.New(ServiceProvider)
        'verhindert Warnung BC40054
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Fenster-Name (Caption). Wird von Init() aufgerufen
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormText As String
        Get
            Return "WinBack Administration"
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
            Return "Admin"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        AdminLog.Show(DockPanel, DockState.DockBottom)
        AdminLog.CloseButtonVisible = False
    End Sub

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("Administration", "WinBack-Administration", "Einstellungen im WinBack-Office--System")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpAdmin", "WinBack Administration")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("btnSync", "Synchronisation Datenbanken WinBack-OrgaBack", "", My.Resources.MainSync_16x16, My.Resources.MainSync_32x32, AddressOf BtnAdminSyncForm)
                oGrp.AddButton("btnDatensicherung", "Sicherung/Rücksicherung Datenbanken WinBack", "", My.Resources.DatenSicherung_16x16, My.Resources.DatenSicherung_32x32, AddressOf BtnAdminDatensicherung)
                oGrp.AddButton("btnUpdate", "Update/Versionsinformation WinBack", "", My.Resources.UpdateDataBase_16x16, My.Resources.UpdateDataBase_32x32, AddressOf BtnAdminUpdate)
                oGrp.AddButton("btnListLabel", "List&Label Designer", "", My.Resources.ListUndLabel_16x16, My.Resources.ListUndLabel_32x32, AddressOf btnListUndLabelDesigner)
                oGrp.AddButton("btnEditWinBackIni", "Edit Konfiguration", "", My.Resources.EditKonfig_16x16, My.Resources.EditKonfig_32x32, AddressOf btnEditKonfig)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString

            Case "WinBack.wb_Admin_Log"
                AdminLog.CloseButtonVisible = False
                _DockPanelList.Add(AdminLog)
                Return AdminLog

            Case "WinBack.wb_Admin_Sync"
                AdminSync = New wb_Admin_Sync
                _DockPanelList.Add(AdminSync)
                Return AdminSync

            Case "WinBack.wb_Admin_UpdateDatabase"
                AdminUpdate = New wb_Admin_UpdateDatabase
                _DockPanelList.Add(AdminUpdate)
                Return AdminUpdate

            Case "WinBack.wb_Admin_Datensicherung"
                AdminDatensicherung = New wb_Admin_Datensicherung
                _DockPanelList.Add(AdminDatensicherung)
                Return AdminDatensicherung

            Case "WinBack.wb_Admin_EditIni"
                AdminEditIni = New wb_Admin_EditIni
                _DockPanelList.Add(AdminEditIni)
                Return AdminEditIni

            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub BtnAdminSyncForm()
        AdminSync = New wb_Admin_Sync
        AdminSync.Show(DockPanel, DockState.DockTop)
        Trace.WriteLine("AdminSyncForm aufgerufen")
    End Sub

    Private Sub BtnAdminDatensicherung()
        AdminDatensicherung = New wb_Admin_Datensicherung
        AdminDatensicherung.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub btnAdminUpdate()
        AdminUpdate = New wb_Admin_UpdateDatabase
        AdminUpdate.Show(DockPanel, DockState.DockRight)
    End Sub

    Private Sub btnListUndLabelDesigner()
        Dim pDialog As New wb_PrinterDialog 'Drucker-Dialog
        Dim gList As New List(Of Object)
        pDialog.LL.DataSource = gList
        pDialog.ListSubDirectory = ""
        pDialog.ListFileName = "wbStandard.lst"
        pDialog.LL.AutoDialogTitle = "Vorlage"
        pDialog.LL.Design()
    End Sub

    Private Sub btnEditKonfig()
        AdminEditIni = New wb_Admin_EditIni
        AdminEditIni.Show(DockPanel, DockState.DockRight)
    End Sub
End Class
