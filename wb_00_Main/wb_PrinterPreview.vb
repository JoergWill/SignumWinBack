Public Class wb_PrinterPreview

    ''' <summary>
    ''' Alle Seiten drucken. Ohne Abfrage der Print-Options
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        LLPreview.PrintAllPages(False)
        Me.Close()
    End Sub

    ''' <summary>
    ''' Vorschaufenster wieder schliessen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnAbbruch_Click(sender As Object, e As EventArgs) Handles BtnAbbruch.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Zoom - Ansicht vergrößern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnZoomIn_Click(sender As Object, e As EventArgs) Handles BtnZoomIn.Click
        LLPreview.ZoomTimes2()
    End Sub

    ''' <summary>
    ''' Zoom - Ansicht verkleinern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnZoomOut_Click(sender As Object, e As EventArgs) Handles BtnZoomOut.Click
        LLPreview.ZoomRevert()
    End Sub

    ''' <summary>
    ''' Zoom auf Seitengröße
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnZoomFit_Click(sender As Object, e As EventArgs) Handles BtnZoomFit.Click
        LLPreview.ZoomReset()
    End Sub
End Class