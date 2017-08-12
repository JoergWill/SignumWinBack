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
    Public AdminDatensicherung As wb_Admin_Datensicherung

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
                oGrp.AddButton("btnDatensicherung", "Sicherung/Rücksicherung Datenbanken WinBack", "", My.Resources.DatenSicherung_16x16, My.Resources.DatenSicherung_32x32, AddressOf BtnAdminDatensicherung)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString

                '        Case "AdminLog"
    '            Return AdminLog

            Case "WinBack.wb_Admin_Log"
                AdminLog.CloseButtonVisible = False
                _DockPanelList.Add(AdminLog)
                Return AdminLog

            Case "WinBack.wb_Admin_Sync"
                AdminSync = New wb_Admin_Sync
                _DockPanelList.Add(AdminSync)
                Return AdminSync

            Case "WinBack.wb_Admin_Datensicherung"
                AdminDatensicherung = New wb_Admin_Datensicherung
                _DockPanelList.Add(AdminDatensicherung)
                Return AdminDatensicherung
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub BtnAdminSyncForm()
        AdminSync = New wb_Admin_Sync
        AdminSync.Show(DockPanel, DockState.DockTop)
    End Sub
    Private Sub BtnAdminDatensicherung()
        AdminDatensicherung = New wb_Admin_Datensicherung
        AdminDatensicherung.Show(DockPanel, DockState.DockTop)
    End Sub

End Class
