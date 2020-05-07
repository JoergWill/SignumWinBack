Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_User_Shared

Public Class wb_User_Details
    Inherits DockContent

    Private _UserGrpHasChanged As Boolean = False

    Private Sub wb_User_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Combo-Box mit Werten füllen
        GrpLoad(sender)

        'In OrgaBack können die Benutzer-Namen nicht geändert werden
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            tUserName.ReadOnly = True
        End If

        AddHandler eListe_Click, AddressOf DetailInfo
        AddHandler eData_Reload, AddressOf GrpLoad
    End Sub

    Private Sub GrpLoad(sender As Object)
        'Combo-Box mit Werten füllen
        cbUserGrp.Fill(GrpTexte)
    End Sub

    Private Sub DataHasChanged(sender As Object, e As EventArgs) Handles tUserName.Leave
        User.Name = tUserName.Text
        User.PersonalNr = tPersonalNr.Text
        User.Passwort = tUserPass.Text
        User.iGruppe = cbUserGrp.GetKeyFromSelection()
        Edit_Leave(sender)
    End Sub

    Public Sub DetailInfo(sender As Object)
        'User Name
        tUserName.Text = User.Name
        'User Personalnummer
        tPersonalNr.Text = User.PersonalNr
        'User Passwort
        tUserPass.Text = User.Passwort
        'Eintrag in Combo-Box ausfüllen
        cbUserGrp.SetTextFromKey(User.iGruppe)
    End Sub

    Private Sub wb_User_Details_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler wb_User_Shared.eListe_Click, AddressOf DetailInfo
        RemoveHandler wb_User_Shared.eData_Reload, AddressOf GrpLoad
    End Sub

    Private Sub cbUserGrp_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbUserGrp.SelectionChangeCommitted
        _UserGrpHasChanged = True
        lblName.Focus()
    End Sub

    Private Sub cbUserGrp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbUserGrp.SelectedIndexChanged
        If _UserGrpHasChanged Then
            _UserGrpHasChanged = False
            DataHasChanged(sender, e)
        End If
    End Sub
End Class