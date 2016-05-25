Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_User_Details
    Inherits DockContent
    Private Sub wb_User_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler wb_User.eListe_Click, AddressOf DetailInfo
        'Combo-Box mit Werten füllen
        For Each item As DictionaryEntry In wb_User.GrpTexte
            cbUserGrp.Items.Add(item.Value)
        Next
    End Sub

    Private Sub Panel_Leave(sender As Object, e As EventArgs) Handles Panel.Leave
        wb_User.aktUserName = tUserName.Text
        wb_User.aktUserPass = tUserPass.Text
        wb_User.aktUserGroup = wb_User.GrpTexte_GetKeyFromText(cbUserGrp.Text)
        wb_User.Edit_Leave(sender)
    End Sub

    Public Sub DetailInfo()
        'User Name
        tUserName.Text = wb_User.aktUserName
        'User Passwort
        tUserPass.Text = wb_User.aktUserPass
        'Eintrag in Combo-Box ausfüllen
        wb_User.GrpTexte_SelectComboBox(cbUserGrp, wb_User.GrpTexte(wb_User.aktUserGroup))
    End Sub
End Class