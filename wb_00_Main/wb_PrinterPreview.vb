Public Class wb_PrinterPreview
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        LLPreview.PrintAllPages(False)
        Me.Close()
    End Sub

    Private Sub BtnPrintSelect_Click(sender As Object, e As EventArgs) Handles BtnPrintSelect.Click
        LLPreview.PrintAllPages(True)
        Me.Close()
    End Sub

    Private Sub BtnAbbruch_Click(sender As Object, e As EventArgs) Handles BtnAbbruch.Click
        Me.Close()
    End Sub
End Class