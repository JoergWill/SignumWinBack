Imports Signum.OrgaSoft.AddIn
Imports System.Globalization
Imports System.Threading

Public Class WinBack
    Dim isInitialised As Boolean = False
    Dim MdiChargen As New Chargen_Main
    Dim MdiArtikel As New Artikel_Main
    Dim MdiRezepte As New Rezepte_Main
    Dim MdiRohstoffe As New Rohstoffe_Main
    Dim MdiUser As New User_Main
    Dim MdiLinien As New Linien_Main
    Dim MdiProduktion As New Produktion_Main
    Dim MdiService As New Service_Main

    Private Sub Ribbon_ActiveTabChanged(sender As Object, e As EventArgs) Handles Ribbon.ActiveTabChanged
        If isInitialised Then
            If rbChargen.Active Then
                ChargenMainShow()
            End If
            If rbArtikel.Active Then
                ArtikelMainShow()
            End If
            If rbRezepte.Active Then
                RezepteMainShow()
            End If
            If rbRohstoffe.Active Then
                RohstoffeMainShow()
            End If
            If rbLinien.Active Then
                LinienMainShow()
            End If
            If rbUser.Active Then
                UserMainShow()
            End If
            If rbPlanung.Active Then
                ProduktionMainShow()
            End If
            If rbExtra.Active Then
                ServiceMainShow()
            End If
        End If
    End Sub

    Private Sub CloseAllForms()
        For i = System.Windows.Forms.Application.OpenForms.Count - 1 To 1 Step -1
            Dim form As Form = Application.OpenForms(i)
            form.Close()
        Next i
    End Sub
    Private Sub ChargenMainShow()
        If MdiChargen Is Nothing Then
            MdiChargen = New Chargen_Main
        End If
        If MdiChargen.Visible Then
            MdiChargen.BringToFront()
        Else
            MdiChargen.Show()
        End If
        MdiChargen.MdiParent = Me
        MdiChargen.Dock = DockStyle.Fill
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

    Private Sub RezepteMainShow()
        If MdiRezepte Is Nothing Then
            MdiRezepte = New Rezepte_Main
        End If
        If MdiRezepte.Visible Then
            MdiRezepte.BringToFront()
        Else
            MdiRezepte.Show()
        End If
        MdiRezepte.MdiParent = Me
        MdiRezepte.Dock = DockStyle.Fill
    End Sub

    Private Sub RohstoffeMainShow()
        If MdiRohstoffe Is Nothing Then
            MdiRohstoffe = New Rohstoffe_Main
        End If
        If MdiRohstoffe.Visible Then
            MdiRohstoffe.BringToFront()
        Else
            MdiRohstoffe.Show()
        End If
        MdiRohstoffe.MdiParent = Me
        MdiRohstoffe.Dock = DockStyle.Fill
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

    Private Sub ProduktionMainShow()
        If MdiProduktion Is Nothing Then
            MdiProduktion = New Produktion_Main
        End If
        If MdiProduktion.Visible Then
            MdiProduktion.BringToFront()
        Else
            MdiProduktion.Show()
        End If
        MdiProduktion.MdiParent = Me
        MdiProduktion.Dock = DockStyle.Fill
    End Sub
    Private Sub ServiceMainShow()
        If MdiService Is Nothing Then
            MdiService = New Service_Main
        End If
        If MdiService.Visible Then
            MdiService.BringToFront()
        Else
            MdiService.Show()
        End If
        MdiService.MdiParent = Me
        MdiService.Dock = DockStyle.Fill
    End Sub

    Private Sub rbLinienAdd_Click(sender As Object, e As EventArgs) Handles rbLinienAdd.Click
        MdiLinien.BtnLinienNew()
    End Sub

    'Private Sub rbLinienEdit_Click(sender As Object, e As EventArgs) Handles rbLinienEdit.Click
    '    MdiLinien.BtnLinien()
    'End Sub

    Private Sub rbLinienDel_Click(sender As Object, e As EventArgs) Handles rbLinienDel.Click
        MdiLinien.BtnLinienRemove()
    End Sub

    Private Sub rbLinienAuto_Click(sender As Object, e As EventArgs) Handles rbLinienAuto.Click
        MdiLinien.btnLinienAutoInstall()
    End Sub

    Private Sub WinBack_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Mysql-Einstellungen (IP-Adresse, User, Passwort)
        wb_Konfig.MySqlSetting()
        'Version in Status-Bar anzeigen
        lblVersion.Text = "WinBack V" & My.Application.Info.Version.ToString
        'IP-Adresse in Status-Bar anzeigen
        lblNetworkIP.Text = wb_Konfig.DbType & " " & wb_Konfig.SqlIP
        'Farbschema einstellen
        wb_Konfig.SetColors()
        'Initialisierung beendet
        isInitialised = True
    End Sub

    Private Sub RibbonOrbMenuItem1_Click(sender As Object, e As EventArgs) Handles RibbonOrbMenuItem1.Click
        Debug.Print("TEST")
    End Sub

    'Private Sub rbText_Click(sender As Object, e As EventArgs) Handles rbText.Click
    '    wb_Konfig.SetLanguage("de")
    'End Sub
End Class