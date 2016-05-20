Public Class WinBack

    Private Sub rbActiveRibbonChanged(sender As Object, e As EventArgs) Handles rbArtikel.ActiveChanged, rbLinien.ActiveChanged
        If rbArtikel.Active Then
            ArtikelMainShow()
        End If
        If rbLinien.Active Then
            LinienMainShow()
        End If
    End Sub

    Private Sub ArtikelMainShow()
        Dim MdiArtikel As New Artikel_Main
        If MdiArtikel.Visible Then
            MdiArtikel.BringToFront()
        Else
            CloseAllForms()
            MdiArtikel.MdiParent = Me
            MdiArtikel.Show()
        End If
    End Sub

    Private Sub LinienMainShow()
        Dim MdiLinien As New Linien_Main
        If MdiLinien.Visible Then
            MdiLinien.BringToFront()
        Else
            CloseAllForms()
            MdiLinien.MdiParent = Me
            MdiLinien.Show()
        End If
    End Sub

    Private Sub CloseAllForms()
        For i = System.Windows.Forms.Application.OpenForms.Count - 1 To 1 Step -1
            Dim form As Form = System.Windows.Forms.Application.OpenForms(i)
            form.Close()
        Next i
    End Sub
End Class