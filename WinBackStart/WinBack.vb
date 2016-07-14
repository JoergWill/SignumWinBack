Imports System.Globalization
Imports System.Threading
Imports System.Resources

Public Class WinBack
    Dim isInitialised As Boolean = False
    Dim MdiChargen As Chargen_Main
    Dim MdiArtikel As Artikel_Main
    Dim MdiRezepte As Rezepte_Main
    Dim MdiRohstoffe As Rohstoffe_Main
    Dim MdiUser As User_Main
    Dim MdiLinien As Linien_Main
    Dim MdiProduktion As Produktion_Main
    Dim MdiService As Service_Main

    Private Sub Ribbon_ActiveTabChanged(sender As Object, e As EventArgs) Handles rTab.ActiveTabChanged
        If isInitialised Then

            ' CloseAllForms()

            'Umschaltung aktive Sprache
            Thread.CurrentThread.CurrentUICulture = New CultureInfo(wb_Konfig.GetLanguage)
            Thread.CurrentThread.CurrentCulture = New CultureInfo(wb_Konfig.GetLanguage)

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
            If form.Name <> "WinBack" Then
                form.Parent = Nothing
                form.Close()
                form.Dispose()
                form = Nothing
            End If
        Next i
    End Sub

    Private Sub ChargenMainShow()
        If MdiChargen Is Nothing OrElse MdiChargen.IsDisposed Then
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
        If MdiArtikel Is Nothing OrElse MdiArtikel.IsDisposed Then
            MdiArtikel = New Artikel_Main
        End If
        If MdiArtikel.Visible Then
            MdiArtikel.BringToFront()
            MdiArtikel.ArtikelListe.RefreshData()
        Else
            MdiArtikel.Show()
        End If
        MdiArtikel.MdiParent = Me
        MdiArtikel.Dock = DockStyle.Fill
    End Sub

    Private Sub RezepteMainShow()
        If MdiRezepte Is Nothing OrElse MdiRezepte.IsDisposed Then
            MdiRezepte = New Rezepte_Main
        End If
        If MdiRezepte.Visible Then
            MdiRezepte.BringToFront()
            MdiRezepte.RezeptListe.RefreshData()
        Else
            MdiRezepte.Show()
        End If
        MdiRezepte.MdiParent = Me
        MdiRezepte.Dock = DockStyle.Fill
    End Sub

    Private Sub RohstoffeMainShow()
        If MdiRohstoffe Is Nothing OrElse MdiRohstoffe.IsDisposed Then
            MdiRohstoffe = New Rohstoffe_Main
        End If
        If MdiRohstoffe.Visible Then
            MdiRohstoffe.BringToFront()
            MdiRohstoffe.RohstoffListe.RefreshData()
        Else
            MdiRohstoffe.Show()
        End If
        MdiRohstoffe.MdiParent = Me
        MdiRohstoffe.Dock = DockStyle.Fill
    End Sub

    Private Sub UserMainShow()
        If MdiUser Is Nothing OrElse MdiUser.IsDisposed Then
            MdiUser = New User_Main
        End If
        If MdiUser.Visible Then
            MdiUser.BringToFront()
            MdiUser.UserListe.RefreshData()
        Else
            MdiUser.Show()
        End If
        MdiUser.MdiParent = Me
        MdiUser.Dock = DockStyle.Fill
    End Sub

    Private Sub rbUserNeu_Click(sender As Object, e As EventArgs) Handles rbUserNeu.Click
        wb_User_Shared.User.AddNew()
        MdiUser.UserListe.RefreshData()
    End Sub

    Private Sub LinienMainShow()
        If MdiLinien Is Nothing OrElse MdiLinien.IsDisposed Then
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
        If MdiProduktion Is Nothing OrElse MdiProduktion.IsDisposed Then
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
        If MdiService Is Nothing OrElse MdiService.IsDisposed Then
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
        wb_Konfig.SqlSetting()
        'Version in Status-Bar anzeigen
        lblVersion.Text = "WinBack V" & My.Application.Info.Version.ToString
        'IP-Adresse in Status-Bar anzeigen
        lblNetworkIP.Text = wb_Konfig.DbType & " " & wb_Konfig.SqlIP
        'Programm und Datei-Pfade einstellen
        wb_Konfig.SetPath(wb_Global.ProgVariante.WinBack)
        'Farbschema einstellen
        wb_Konfig.SetColors()
        'aktuelle(letzte) Sprache einstellen
        wb_Konfig.SetLanguage("")
        'Hash-Table Texte laden
        wb_Konfig.LoadTexteTabelle(wb_Konfig.GetLanguageNr())

        'Initialisierung beendet
        isInitialised = True
    End Sub

    'Umschaltung aktive Sprache
    Private Sub rbSprache_DE_Click(sender As Object, e As EventArgs) Handles rbSprache_DE.Click
        wb_Konfig.SetLanguage("de-DE")
        changeLanguage()
    End Sub
    Private Sub rbSprache_EN_Click(sender As Object, e As EventArgs) Handles rbSprache_EN.Click
        wb_Konfig.SetLanguage("en-US")
        changeLanguage()
    End Sub
    Private Sub changeLanguage()
        'alle offenen Fenster schliessen
        CloseAllForms()
        'Umschaltung aktive Sprache
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(wb_Konfig.GetLanguage)
        Thread.CurrentThread.CurrentCulture = New CultureInfo(wb_Konfig.GetLanguage)
        'alle Texte in WinBack neu aufbauen
        ChangeAllControls(Me)
    End Sub

    Sub ChangeAllControls(ByVal m_Control As Control)
        ' Neues Objekt, dass die sprachlich passenden Strings für das Form 
        ' bereit stellt
        Dim Resource As ResourceManager = New ResourceManager(Me.GetType())

        For Each ctrl As Control In m_Control.Controls
            'If ctrl.Controls.Count > 0 Then
            '    ChangeAllControls(ctrl)
            'End If

            If ctrl.GetType().Equals(GetType(StatusStrip)) Then
                For Each item As ToolStripItem In DirectCast(ctrl, ToolStrip).Items
                    Debug.Print("ToolStrip " & item.Text)
                    Debug.Print("ToolStrip " & item.Name)
                Next
            End If

            If ctrl.GetType().Equals(GetType(ToolStrip)) Then
                For Each item As ToolStripItem In DirectCast(ctrl, ToolStrip).Items
                    Debug.Print("ToolStrip " & item.Text)
                Next
            End If

            If ctrl.GetType().Equals(GetType(Ribbon)) Then
                For Each rTab As RibbonTab In DirectCast(ctrl, Ribbon).Tabs
                    Debug.Print("RibbonTab " & rTab.Text)

                    For Each rPnl As RibbonPanel In rTab.Panels
                        Debug.Print("      RibbonPanel " & rPnl.Text)

                        'For Each rBtn As RibbonItem In rPnl.Items
                        '    If rBtn.GetType().Equals(GetType(RibbonItem)) Then
                        '        Debug.Print("          RibbonButton " & rBtn.Text)
                        '        If rBtn.Tag Then
                        '        End If
                        'Next
                    Next
                Next
            End If

            Debug.Print("Control " & ctrl.Text)
        Next
    End Sub

End Class