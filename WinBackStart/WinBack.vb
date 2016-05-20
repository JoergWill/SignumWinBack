Public Class WinBack
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, rbArtikel.ActiveChanged
        Dim am As New Artikel_Main
        am.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click, rbLinien.ActiveChanged
        Dim ln As New Linien_Main
        ln.Show()
    End Sub
End Class