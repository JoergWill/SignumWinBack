Imports Signum.OrgaSoft.AddIn
Imports System.Globalization
Imports System.Threading

Public Class WinBack
    Dim MdiArtikel As New Artikel_Main
    Dim MdiUser As New User_Main
    Dim MdiLinien As New Linien_Main

    Private Sub rbActiveRibbonChanged(sender As Object, e As EventArgs) Handles rbArtikel.ActiveChanged, rbLinien.ActiveChanged, rbUser.ActiveChanged
        If rbArtikel.Active Then
            ArtikelMainShow()
        End If
        If rbLinien.Active Then
            LinienMainShow()
        End If
        If rbUser.Active Then
            UserMainShow()
        End If
    End Sub

    Private Sub CloseAllForms()
        For i = System.Windows.Forms.Application.OpenForms.Count - 1 To 1 Step -1
            Dim form As Form = Application.OpenForms(i)
            form.Close()
        Next i
    End Sub

    Private Sub ArtikelMainShow()
        If MdiArtikel Is Nothing Then
            MdiArtikel = New Artikel_Main
        End If
        If MdiArtikel.Visible Then
            MdiArtikel.BringToFront()
        Else
            MdiArtikel.Show()
        End If
        MdiArtikel.MdiParent = Me
        MdiArtikel.Dock = DockStyle.Fill
    End Sub

    Private Sub UserMainShow()
        If MdiUser Is Nothing Then
            MdiUser = New User_Main
        End If
        If MdiUser.Visible Then
            MdiUser.BringToFront()
        Else
            MdiUser.Show()
        End If
        MdiUser.MdiParent = Me
        MdiUser.Dock = DockStyle.Fill
    End Sub

    Private Sub LinienMainShow()
        If MdiLinien Is Nothing Then
            MdiLinien = New Linien_Main
        End If
        If MdiLinien.Visible Then
            MdiLinien.BringToFront()
        Else
            MdiLinien.Show()
        End If
        MdiLinien.MdiParent = Me
        MdiLinien.Dock = DockStyle.Fill
    End Sub

    Private Sub rbLinienAdd_Click(sender As Object, e As EventArgs) Handles rbLinienAdd.Click
        MdiLinien.BtnLinienNew()
    End Sub

    Private Sub rbLinienEdit_Click(sender As Object, e As EventArgs) Handles rbLinienEdit.Click
        MdiLinien.BtnLinien()
    End Sub

    Private Sub rbLinienDel_Click(sender As Object, e As EventArgs) Handles rbLinienDel.Click
        MdiLinien.BtnLinienRemove()
    End Sub

    Private Sub rbLinienAuto_Click(sender As Object, e As EventArgs) Handles rbLinienAuto.Click
        MdiLinien.btnLinienAutoInstall()
    End Sub

    Private Sub WinBack_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Mysql-Einstellungen (IP-Adresse, User, Passwort)
        wb_GetKonfig.MySqlSetting()
        'Version in Status-Bar anzeigen
        lblVersion.Text = "WinBack V" & My.Application.Info.Version.ToString
        'IP-Adresse in Status-Bar anzeigen
        lblNetworkIP.Text = wb_GetKonfig.DbType & " " & wb_GetKonfig.SqlIP
        'Farbschema einstellen
        wb_GetKonfig.SetColors()
        'Sprache einstellen
        Debug.Print("Language/Localisation " & Thread.CurrentThread.CurrentCulture.ToString & "/" & Thread.CurrentThread.CurrentUICulture.ToString)
        wb_GetKonfig.SetLanguage()
        Debug.Print("Language/Localisation " & Thread.CurrentThread.CurrentCulture.ToString & "/" & Thread.CurrentThread.CurrentUICulture.ToString)
    End Sub

End Class