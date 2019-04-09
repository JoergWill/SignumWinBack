Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_User_Main
    Implements IExternalFormUserControl

    'Default-Fenster
    Private UserListe As New wb_User_Liste
    Private UserDetails As New wb_User_Details

    'alle anderen Fenster werden zur Laufzeit erzeugt
    Private UserRechte As wb_User_Rechte

    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        MyBase.New(ServiceProvider)
    End Sub

    ''' <summary>
    ''' Fenster-Name (Caption). Wird von Init() aufgerufen
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormText As String
        Get
            Return "WinBack Mitarbeiter-Verwaltung"
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
            Return "User"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        UserListe.Show(DockPanel, DockState.DockLeft)
        UserDetails.Show(DockPanel, DockState.DockTop)
    End Sub

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("UserVerwaltung", "WinBack-Mitarbeiter", "Verwaltung der aktiven WinBack-Mitarbeiter")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpUser", "WinBack Mitarbeiter")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnUserPasswd", "Passwort ändern", "Neues Mitarbeiter-Passwort vergeben", My.Resources.UserPasswd_32x32, My.Resources.UserPasswd_32x32, AddressOf BtnUserPasswd)
                oGrp.AddButton("BtnUserPrintList", "Drucken Mitarbeiter-Liste", "Liste aller Mitarbeiter drucken", My.Resources.UserListe_32x32, My.Resources.UserListe_32x32, AddressOf btnUserPrint)
                oGrp.AddButton("BtnUserGroup", "Mitarbeiter-Gruppen", "Gruppen und Gruppen-Rechte verwalten", My.Resources.UserGruppen_32x32, My.Resources.UserGruppen_32x32, AddressOf BtnUserGroup)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString

            Case "WinBack.wb_User_Liste"
                UserListe.CloseButtonVisible = False
                _DockPanelList.Add(UserListe)
                Return UserListe

            Case "WinBack.wb_User_Details"
                UserDetails = New wb_User_Details
                _DockPanelList.Add(UserDetails)
                Return UserDetails

            Case "WinBack.wb_User_Rechte"
                UserRechte = New wb_User_Rechte
                _DockPanelList.Add(UserRechte)
                Return UserRechte

            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub BtnUserPasswd()
        Throw New NotImplementedException
    End Sub

    Private Sub btnUserPrint()
        Throw New NotImplementedException
    End Sub

    Private Sub BtnUserGroup()
        Throw New NotImplementedException
    End Sub

End Class
