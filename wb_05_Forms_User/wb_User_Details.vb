Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_User_Details
    Inherits DockContent
    Private Sub wb_User_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Combo-Box mit Werten füllen
        cbUserGrp.Fill(wb_User_Shared.GrpTexte)

        AddHandler wb_User_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    Private Sub Panel_Leave(sender As Object, e As EventArgs) Handles Panel.Leave
        wb_User_Shared.aktUserName = tUserName.Text
        wb_User_Shared.aktUserPass = tUserPass.Text
        wb_User_Shared.aktUserGroup = cbUserGrp.GetKeyFromSelection()
        wb_User_Shared.Edit_Leave(sender)
    End Sub

    Public Sub DetailInfo()
        'User Name
        tUserName.Text = wb_User_Shared.aktUserName
        'User Passwort
        tUserPass.Text = wb_User_Shared.aktUserPass
        'Eintrag in Combo-Box ausfüllen
        cbUserGrp.SetTextFromKey(wb_User_Shared.aktUserGroup)
    End Sub
End Class