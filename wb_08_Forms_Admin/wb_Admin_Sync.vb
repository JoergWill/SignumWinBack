Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Sync


Public Class wb_Admin_Sync
    Inherits DockContent
    Private Sub wb_User_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub BtnSyncStart_Click(sender As Object, e As EventArgs) Handles BtnSync_UserGrp_Start.Click
        ' Nur Test - Ausgabe 
        Sync_UserGruppen(SyncErgebnis, True)
    End Sub

    Private Sub BtnSync_Start_Click(sender As Object, e As EventArgs) Handles BtnSync_Start.Click
        ' Nur Test - Ausgabe 
        Sync_UserGruppen(SyncErgebnis, False)
    End Sub
End Class