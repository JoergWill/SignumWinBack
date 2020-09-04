Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_User_Main
    Implements IExternalFormUserControl

    'Default-Fenster
    Private UserListe As New wb_User_Liste

    'alle anderen Fenster werden zur Laufzeit erzeugt
    Private UserDetails As wb_User_Details
    Private UserRechte As wb_User_Rechte
    Private UserGruppenRechte As wb_User_GruppenRechte
    Private UserRezGruppenRechte As wb_User_GruppenRechte
    Private UserPasswort As wb_User_Passwort

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
        'User-Liste (ordentlich) schliessen - Speichert die Grid-Einstellungen
        If UserListe IsNot Nothing Then
            UserListe.Close()
        End If
        'Anzeige User_Rechte schliessen
        If UserRechte IsNot Nothing Then
            UserRechte.Close()
        End If
        'User-Details schliessen
        If UserDetails IsNot Nothing Then
            UserDetails.Close()
        End If
        'Fenster User-Gruppen_rechte schliessen
        If UserGruppenRechte IsNot Nothing Then
            UserGruppenRechte.Close()
        End If

        'Fenster darf geschlossen werden
        Return False
    End Function

    Public Overrides Sub SetDefaultLayout()
        Try
            UserListe.Show(DockPanel, DockState.DockLeft)
        Catch
        End Try
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
                oGrp.AddButton("BtnUserListe", "Benutzer Liste", "Liste aller Benutzer", My.Resources.User_32x32, My.Resources.User_32x32, AddressOf BtnUserListe)
                oGrp.AddButton("BtnUserDetails", "Benutzer Details", "Benutzer-Details", My.Resources.UserDetails_32x32, My.Resources.UserDetails_32x32, AddressOf BtnUserDetails)
                oGrp.AddButton("BtnUserRechte", "Benutzer Rechte", "Anzeige aller Benutzer-Rechte", My.Resources.UserBerechtigungen_32x32, My.Resources.UserBerechtigungen_32x32, AddressOf BtnUserRechte)
                oGrp.AddButton("BtnUserPasswd", "Passwort ändern", "Neues Mitarbeiter-Passwort vergeben", My.Resources.UserPasswd_32x32, My.Resources.UserPasswd_32x32, AddressOf BtnUserPasswd)
                oGrp.AddButton("BtnUserPrintList", "Drucken Mitarbeiter-Liste", "Liste aller Mitarbeiter drucken", My.Resources.UserListe_32x32, My.Resources.UserListe_32x32, AddressOf btnUserPrint)
                oGrp.AddButton("BtnUserGroup", "Mitarbeiter-Gruppen", "Gruppen und Gruppen-Rechte verwalten", My.Resources.UserGruppen_32x32, My.Resources.UserGruppen_32x32, AddressOf BtnUserGroup)
                oGrp.AddButton("BtnUserRezGroup", "Mitarbeiter-Rezeptgruppen", "Rezeptgruppen und Benutzergruppen zuordnen", My.Resources.RezeptGruppen_32x32, My.Resources.RezeptGruppen_32x32, AddressOf BtnUserRezGroup)
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

            Case "WinBack.wb_User_GruppenRechte"
                UserGruppenRechte = New wb_User_GruppenRechte
                _DockPanelList.Add(UserGruppenRechte)
                Return UserGruppenRechte

            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub BtnUserDetails()
        If IsNothingOrDisposed(UserDetails) Then
            UserDetails = New wb_User_Details
        End If
        UserDetails.Show(DockPanel)
    End Sub

    Private Sub BtnUserListe()
        If IsNothingOrDisposed(UserListe) Then
            UserListe = New wb_User_Liste
        End If
        UserListe.Show(DockPanel)
    End Sub

    Private Sub BtnUserRechte()
        If IsNothingOrDisposed(UserRechte) Then
            UserRechte = New wb_User_Rechte
        End If
        UserRechte.Show(DockPanel)
    End Sub

    Private Sub BtnUserGroup()
        If IsNothingOrDisposed(UserGruppenRechte) Then
            UserGruppenRechte = New wb_User_GruppenRechte
        End If
        UserGruppenRechte.Show(DockPanel)
    End Sub

    Private Sub BtnUserRezGroup()
        If IsNothingOrDisposed(UserRezGruppenRechte) Then
            UserRezGruppenRechte = New wb_User_GruppenRechte(True)
        End If
        UserRezGruppenRechte.Show(DockPanel)
    End Sub

    Private Sub BtnUserPasswd()
        UserPasswort = New wb_User_Passwort
        UserPasswort.ShowDialog(DockPanel)
        UserPasswort = Nothing
    End Sub

    Private Sub btnUserPrint()
        'Throw New NotImplementedException
    End Sub

End Class
