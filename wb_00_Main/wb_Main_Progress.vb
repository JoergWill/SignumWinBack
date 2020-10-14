Public Class wb_Main_Progress
    Private Sub wb_Main_Progress_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.Width = 88
        Me.Height = 88
        wb_Main_Shared.MainProgressVisible = True
    End Sub

    Private Sub wb_Main_Progress_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Me.ProgressCircle.Rotate = False
        Me.ProgressCircle.Dispose()
        wb_Main_Shared.MainProgressVisible = False
    End Sub
End Class