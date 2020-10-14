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
    Public OrgaBackParams As wb_Admin_OrgaBackParams
    Public AdminCheck As wb_Admin_CheckDatabase
    Public AdminUpdate As wb_Admin_UpdateDatabase
    Public AdminUpdateWinBack As wb_Admin_UpdateWinBack
    Public AdminDatensicherung As wb_Admin_Datensicherung
    Public AdminEditIni As wb_Admin_EditIni

    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        MyBase.New(ServiceProvider)
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
                oGrp.AddButton("btnOrgaBack", "Einstellungen OrgaBack", "", My.Resources.OrgaBackSortiment_32x32, My.Resources.OrgaBackSortiment_32x32, AddressOf BtnOrgaBack)
                oGrp.AddButton("btnDatensicherung", "Sicherung/Rücksicherung Datenbanken WinBack", "", My.Resources.DatenSicherung_16x16, My.Resources.DatenSicherung_32x32, AddressOf BtnAdminDatensicherung)
                oGrp.AddButton("btnCheckDatabase", "Datenbank und Konfiguration prüfen", "", My.Resources.MainStatistikRohstoffe_16x16, My.Resources.MainStatistikRohstoffe_32x32, AddressOf btnAdminCheck)
                oGrp.AddButton("btnUpdate", "Update/Versionsinformation WinBack", "", My.Resources.UpdateDataBase_16x16, My.Resources.UpdateDataBase_32x32, AddressOf btnAdminUpdate)
                oGrp.AddButton("btnUpdateWinBack", "Update WinBack-AddIn", "", My.Resources.UpdateWinBack_32x32, My.Resources.UpdateWinBack_32x32, AddressOf btnAdminUpdateWinBack)
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

            Case "WinBack.wb_Admin_OrgaBackParams"
                OrgaBackParams = New wb_Admin_OrgaBackParams
                _DockPanelList.Add(OrgaBackParams)
                Return OrgaBackParams

            Case "WinBack.wb_Admin_UpdateDatabase"
                AdminCheck = New wb_Admin_CheckDatabase
                _DockPanelList.Add(AdminCheck)
                Return AdminCheck

            Case "WinBack.wb_Admin_UpdateDatabase"
                AdminUpdate = New wb_Admin_UpdateDatabase
                _DockPanelList.Add(AdminUpdate)
                Return AdminUpdate

            Case "WinBack.wb_Admin_UpdateWinBack"
                AdminUpdateWinBack = New wb_Admin_UpdateWinBack
                _DockPanelList.Add(AdminUpdateWinBack)
                Return AdminUpdateWinBack

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
        If IsNothingOrDisposed(AdminSync) Then
            AdminSync = New wb_Admin_Sync
        End If
        AdminSync.Show(DockPanel, DockState.DockTop)
    End Sub
    Private Sub BtnOrgaBack()
        If IsNothingOrDisposed(OrgaBackParams) Then
            OrgaBackParams = New wb_Admin_OrgaBackParams
        End If
        OrgaBackParams.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub BtnAdminDatensicherung()
        If IsNothingOrDisposed(AdminDatensicherung) Then
            AdminDatensicherung = New wb_Admin_Datensicherung
        End If
        AdminDatensicherung.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub btnAdminCheck()
        If IsNothingOrDisposed(AdminCheck) Then
            AdminCheck = New wb_Admin_CheckDatabase
        End If
        AdminCheck.Show(DockPanel, DockState.DockRight)
    End Sub

    Private Sub btnAdminUpdate()
        If IsNothingOrDisposed(AdminUpdate) Then
            AdminUpdate = New wb_Admin_UpdateDatabase
        End If
        AdminUpdate.Show(DockPanel, DockState.DockRight)
    End Sub

    Private Sub btnAdminUpdateWinBack()
        If IsNothingOrDisposed(AdminUpdateWinBack) Then
            AdminUpdateWinBack = New wb_Admin_UpdateWinBack
        End If
        AdminUpdateWinBack.Show(DockPanel, DockState.DockRight)
    End Sub

    Private Sub btnListUndLabelDesigner()
        Dim pDialog As New wb_PrinterDialog(True) 'Drucker-Dialog
        Dim gList As New List(Of Object)
        pDialog.LL.DataSource = gList
        pDialog.ListSubDirectory = ""
        pDialog.ListFileName = "wbStandard.lst"
        pDialog.LL.AutoDialogTitle = "Vorlage"
        pDialog.ShowDialog()
    End Sub

    Private Sub btnEditKonfig()
        If IsNothingOrDisposed(AdminEditIni) Then
            AdminEditIni = New wb_Admin_EditIni
        End If
        AdminEditIni.Show(DockPanel, DockState.DockRight)
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
    Public Overrides Function FormClosing(Reason As Short) As Boolean Implements IBasicFormUserControl.FormClosing
        'Fenster ordentlich wieder schliesen
        wb_Functions.CloseAndDisposeSubForm(AdminLog)
        wb_Functions.CloseAndDisposeSubForm(AdminSync)
        wb_Functions.CloseAndDisposeSubForm(OrgaBackParams)
        wb_Functions.CloseAndDisposeSubForm(AdminCheck)
        wb_Functions.CloseAndDisposeSubForm(AdminUpdate)
        wb_Functions.CloseAndDisposeSubForm(AdminUpdateWinBack)
        wb_Functions.CloseAndDisposeSubForm(AdminDatensicherung)
        wb_Functions.CloseAndDisposeSubForm(AdminEditIni)
        wb_Functions.CloseAndDisposeSubForm(AdminSync)

        'Fenster darf geschlossen werden
        Return False
    End Function

End Class
