Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Dashboard_Main
    Implements IExternalFormUserControl

    'Default-Fenster
    Private DashboardGrid As New wb_Dashboard_Grid

    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        'Initialisierung
        MyBase.New(ServiceProvider)
        'Default-Layout wenn keine Fenster angezeigt werden
        If _DockPanelList.Count = 0 Then
            SetDefaultLayout()
        End If
    End Sub

    ''' <summary>
    ''' Fenster-Name (Caption). Wird von Init() aufgerufen
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormText As String
        Get
            Return "WinBack Dashboard"
        End Get
    End Property

    ''' <summary>
    ''' Eindeutiger Name für die Basis-Form. 
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormName As String
        Get
            Me.Tag = "Dashboard"
            Return "Dashboard"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        DockPanel.Theme = wb_GlobalSettings.Theme
        DashboardGrid.Show(DockPanel, DockState.Document)
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
        'Fenster darf geschlossen werden
        Return False
    End Function

    'Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
    '    Get
    '    End Get
    'End Property

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString

            Case "WinBack.wb_Dashboard_Grid"
                DashboardGrid.CloseButtonVisible = False
                _DockPanelList.Add(DashboardGrid)
                Return DashboardGrid

            Case Else
                Return Nothing
        End Select
    End Function
End Class
