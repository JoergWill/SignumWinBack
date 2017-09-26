Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_SyncUser_X


Public Class wb_Admin_Sync
    Inherits DockContent

    Public wbUser As New wb_SyncUserGruppen_WinBack
    Public osUser As New wb_SyncUserGruppen_OrgaBack

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        wbUser.DBRead()
        osUser.DBRead()

        wbUser.CheckSync(osUser.Data)
    End Sub
End Class