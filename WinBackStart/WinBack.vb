Public Class WinBack
    Dim MdiArtikel As New Artikel_Main
    Dim MdiLinien As New Linien_Main

    Private Sub rbActiveRibbonChanged(sender As Object, e As EventArgs) Handles rbArtikel.ActiveChanged, rbLinien.ActiveChanged
        If rbArtikel.Active Then
            ArtikelMainShow()
        End If
        If rbLinien.Active Then
            LinienMainShow()
        End If
    End Sub

    Private Sub ArtikelMainShow()
        If MdiArtikel Is Nothing Then
            MdiArtikel = New Artikel_Main
        End If
        If MdiArtikel.Visible Then
            MdiArtikel.BringToFront()
        Else
            '           CloseAllForms()
            MdiArtikel.Show()
        End If
        MdiArtikel.MdiParent = Me
        MdiArtikel.Dock = DockStyle.Fill
    End Sub

    Private Sub LinienMainShow()
        If MdiLinien Is Nothing Then
            MdiLinien = New Linien_Main
        End If
        If MdiLinien.Visible Then
            MdiLinien.BringToFront()
        Else
            '            CloseAllForms()
            MdiLinien.Show()
        End If
        MdiLinien.MdiParent = Me
        MdiLinien.Dock = DockStyle.Fill
    End Sub

    Private Sub CloseAllForms()
        For i = System.Windows.Forms.Application.OpenForms.Count - 1 To 1 Step -1
            Dim form As Form = System.Windows.Forms.Application.OpenForms(i)
            form.Close()
        Next i
    End Sub
End Class