Public Class wb_PrinterPreview
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        LLPreview.PrintAllPages(False)
        Me.Close()
    End Sub

    Private Sub BtnAbbruch_Click(sender As Object, e As EventArgs) Handles BtnAbbruch.Click
        Me.Close()
    End Sub

    Private Sub BtnZoomIn_Click(sender As Object, e As EventArgs) Handles BtnZoomIn.Click
        LLPreview.ZoomTimes2()
    End Sub

    Private Sub BtnZoomOut_Click(sender As Object, e As EventArgs) Handles BtnZoomOut.Click
        LLPreview.ZoomRevert()
    End Sub

    Private Sub BtnZoomFit_Click(sender As Object, e As EventArgs) Handles BtnZoomFit.Click
        LLPreview.ZoomReset()
    End Sub
End Class