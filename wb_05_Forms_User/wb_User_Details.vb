Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_User_Shared

Public Class wb_User_Details
    Inherits DockContent
    Private Sub wb_User_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Combo-Box mit Werten füllen
        cbUserGrp.Fill(GrpTexte)

        AddHandler eListe_Click, AddressOf DetailInfo
    End Sub

    Private Sub DataHasChanged(sender As Object, e As EventArgs) Handles tUserPass.Leave, tUserName.Leave, cbUserGrp.Leave
        User.Name = tUserName.Text
        User.Passwort = tUserPass.Text
        User.iGruppe = cbUserGrp.GetKeyFromSelection()
        Edit_Leave(sender)
    End Sub

    Public Sub DetailInfo()
        'User Name
        tUserName.Text = User.Name
        'User Passwort
        tUserPass.Text = User.Passwort
        'Eintrag in Combo-Box ausfüllen
        cbUserGrp.SetTextFromKey(User.iGruppe)
    End Sub
End Class