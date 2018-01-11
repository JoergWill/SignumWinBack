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
            'Thread.CurrentThread.CurrentUICulture = New CultureInfo(wb_Language.GetLanguage)
            'Thread.CurrentThread.CurrentCulture = New CultureInfo(wb_Language.GetLanguage)

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
        'MdiArtikel.MdiParent = Me
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

    ''' <summary>
    ''' Neuen Benutzer anlegen.
    ''' </summary>
    Private Sub rbUserNeu_Click(sender As Object, e As EventArgs) Handles rbUserNeu.Click
        MdiUser.BtnUserNew()
    End Sub

    ''' <summary>
    ''' Löscht den aktuellen Benutzer
    ''' </summary>
    Private Sub RnUserRemove_Click(sender As Object, e As EventArgs) Handles RnUserRemove.Click
        MdiUser.BtnUserDelete()
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
        'Programm und Datei-Pfade einstellen
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack

        'Sprache und Versions-Nummer in Status-Bar anzeigen
        ShowStatusBar()

        'Initialisierung beendet
        isInitialised = True

        'TEST PRODUKTION
        'ProduktionMainShow()
    End Sub

    Private Sub WinBack_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ShowLogin()
        wb_AktUser.SetUserRechte(Me)
    End Sub

    Private Sub rbAbmelden_Click(sender As Object, e As EventArgs) Handles rbAbmelden.Click
        ShowLogin()
        wb_AktUser.SetUserRechte(Me)
    End Sub

    Private Sub ChangeLanguage()
        'Prüfen ob die Sprache geändert werden muss
        Dim CurrentLanguage As String = Thread.CurrentThread.CurrentUICulture.Name
        If CurrentLanguage <> wb_Language.GetLanguage Then

            'alle offenen Fenster schliessen
            CloseAllForms()
            'Verarbeitung sperren
            isInitialised = False

            'Aktuelle Position und Größe merken
            Dim pt As Point = Me.Location
            Dim sz As Size = Me.Size
            Dim cz As Size = Me.ClientSize

            'Umschaltung aktive Sprache
            Thread.CurrentThread.CurrentUICulture = New CultureInfo(wb_Language.GetLanguage)
            Thread.CurrentThread.CurrentCulture = New CultureInfo(wb_Language.GetLanguage)

            'Entfernen aller Controls
            Me.Controls.Clear()
            InitializeComponent()

            'Wiederherstellen der Fensterposition
            'Me.Location = pt

            'Verarbeitung wieder freigeben
            isInitialised = True
        End If

        'Sprache und Versions-Nummer in Status-Bar anzeigen
        ShowStatusBar()
    End Sub

    Private Sub ShowLogin()
        'Anmelde-Bildschirm anzeigen
        Dim UserLogin As New Login
        'User-Nummer prüfen
        If Not UserLogin.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            'aktuelle(letzte) Sprache einstellen
            wb_Language.SetLanguage("")
        End If

        'Sprache einstellen
        ChangeLanguage()
        'Hash-Table Texte laden
        wb_Language.LoadTexteTabelle(wb_Language.GetLanguageNr())
    End Sub

    Private Sub ShowStatusBar()
        'Version in Status-Bar anzeigen
        lblVersion.Text = "WinBack V" & wb_GlobalSettings.WinBackVersion
        'IP-Adresse in Status-Bar anzeigen
        lblNetworkIP.Text = wb_GlobalSettings.WinBackDBType.ToString & " " & wb_GlobalSettings.MySQLServerIP

        'Status-Bar - Länderflagge anzeigen
        Dim AktiveSprache As Integer = wb_Language.GetLanguageNr()
        lblLanguage.Image = LanguageFlags.Images(AktiveSprache)
    End Sub

    Private Sub rbSprache_DE_Click(sender As Object, e As EventArgs) Handles RibbonOrbRecentItem0.Click
        wb_Language.SetLanguage("de-DE")
        ChangeLanguage()
    End Sub
    Private Sub rbSprache_HU_Click(sender As Object, e As EventArgs) Handles RibbonOrbRecentItem1.Click
        wb_Language.SetLanguage("hu-HU")
        ChangeLanguage()
    End Sub
    Private Sub rbSprache_NL_Click(sender As Object, e As EventArgs) Handles RibbonOrbRecentItem2.Click
        wb_Language.SetLanguage("nl-NL")
        ChangeLanguage()
    End Sub
    Private Sub rbSprache_EN_Click(sender As Object, e As EventArgs) Handles RibbonOrbRecentItem3.Click
        wb_Language.SetLanguage("en-US")
        ChangeLanguage()
    End Sub
    Private Sub rbSprache_PT_Click(sender As Object, e As EventArgs) Handles RibbonOrbRecentItem4.Click
        wb_Language.SetLanguage("pt-PT")
        ChangeLanguage()
    End Sub
    Private Sub rbSprache_SL_Click(sender As Object, e As EventArgs) Handles RibbonOrbRecentItem5.Click
        wb_Language.SetLanguage("sl-SL")
        ChangeLanguage()
    End Sub
    Private Sub rbSprache_RU_Click(sender As Object, e As EventArgs) Handles RibbonOrbRecentItem6.Click
        wb_Language.SetLanguage("ru-RU")
        ChangeLanguage()
    End Sub
    Private Sub rbSprache_FR_Click(sender As Object, e As EventArgs) Handles RibbonOrbRecentItem7.Click
        wb_Language.SetLanguage("fr-FR")
        ChangeLanguage()
    End Sub
    Private Sub rbSprache_ES_Click(sender As Object, e As EventArgs) Handles RibbonOrbRecentItem8.Click
        wb_Language.SetLanguage("es-ES")
        ChangeLanguage()
    End Sub
    Private Sub rbSprache_SK_Click(sender As Object, e As EventArgs) Handles RibbonOrbRecentItem9.Click
        wb_Language.SetLanguage("sk-SK")
        ChangeLanguage()
    End Sub
    Private Sub rbSprache_RO_Click(sender As Object, e As EventArgs) Handles RibbonOrbRecentItem10.Click
        wb_Language.SetLanguage("ro-RO")
        ChangeLanguage()
    End Sub

End Class