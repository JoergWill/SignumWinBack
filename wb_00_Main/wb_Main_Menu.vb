
'---------------------------------------------------------
'15.04.2016/ V0.9/JW            :Neuanlage
'Bearbeitet von                 :Will
'
'Änderungen:
'---------------------------------------------------------
'Beschreibung:
'Erweitert das Ribon um ein neues Tab WinBack und baut die
'    Menu-Struktur auf.
'---------------------------------------------------------

Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.Common
Imports System.Windows.Forms
Imports System.IO

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Erweitert das Ribbon um ein neues Tab(WinBack)")>
Public Class wb_Main_Menu
    Implements IExtension

    Public Property InfoContainer As Common.IInfoContainer Implements Extensibility.IExtension.InfoContainer
    Public Property ServiceProvider As Common.IOrgasoftServiceProvider Implements Extensibility.IExtension.ServiceProvider

    Private oViewProvider As IViewProvider
    Private oMenuService As IMenuService
    Private oSetting As ISettingService
    Private oFactory As IFactoryService

    Private xForm As Form


    '---------------------------------------------------------
    '15.04.2016/ V0.9/JW            :Neuanlage
    'Bearbeitet von                 :Will
    '
    'Änderungen:
    '---------------------------------------------------------
    'Beschreibung:
    'Initialisierung Klasse
    '---------------------------------------------------------
    Public Sub Initialize() Implements IExtension.Initialize

        oViewProvider = TryCast(ServiceProvider.GetService(GetType(IViewProvider)), IViewProvider)
        oMenuService = TryCast(ServiceProvider.GetService(GetType(IMenuService)), IMenuService)
        oSetting = TryCast(ServiceProvider.GetService(GetType(ISettingService)), ISettingService)
        oFactory = TryCast(ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)

        'Event User-Login
        AddLogIn()
        'Main-Menu erweitern
        AddMenu()

    End Sub

    '---------------------------------------------------------
    '15.04.2016/ V0.9/JW            :Neuanlage
    'Bearbeitet von                 :Will
    '
    'Änderungen:
    '---------------------------------------------------------
    'Beschreibung:
    'Event User-Login
    '---------------------------------------------------------
    Private Sub AddLogIn()
        Dim IEB As IEventBroker = TryCast(ServiceProvider.GetService(GetType(IEventBroker)), IEventBroker)
        If IEB IsNot Nothing Then
            AddHandler IEB.LoginChanged, AddressOf UserLogin
            AddHandler IEB.LanguageChanged, AddressOf LanguageChange
        End If

        ' Beim Laden des AddIns ist schon ein Mitarbeiter angemeldet
        UserLogin(Me, EventArgs.Empty)
        'globale System-Einstellungen lesen
        ReadSystemKonfig()
    End Sub

    '---------------------------------------------------------
    '15.04.2016/ V0.9/JW            :Neuanlage
    'Bearbeitet von                 :Will
    '
    'Änderungen:
    '---------------------------------------------------------
    'Beschreibung:
    'Erzeugen Hauptmenu im Ribbon Orgasoft
    '---------------------------------------------------------
    Private Sub AddMenu()
        ' Fügt dem Ribbon ein neues RibbonTab 'WinBack' hinzu
        Dim oNewTab = oMenuService.AddTab("wb_MainMenu", "WinBack", "WinBack in OrgaBack integriert")

        ' Das neue RibbonTab 'WinBack' erhält Haupt-Menu-Gruppen 'Stammdaten' 'Auswertung' 'Linien'
        Dim oGrpStammdaten = oNewTab.AddGroup("WinBack", "Stammdaten (Produktion)")
        Dim oGrpChargen = oNewTab.AddGroup("WinBack", "Auswertung (Produktion)")
        Dim oGrpLinien = oNewTab.AddGroup("WinBack", "Linien")
        Dim oGrpPlanung = oNewTab.AddGroup("WinBack", "Produktions-Planung")

        'Gruppe Stammdaten
        oGrpStammdaten.AddButton("btnArtikelStamm", "Artikel", "WinBack Artikelstammdaten", My.Resources.MainArtikel_16x16, My.Resources.MainArtikel_32x32, AddressOf ShowArtikelForm)
        oGrpStammdaten.AddButton("btnRohstoffStamm", "Rohstoffe", "WinBack Rohstoff-Stammdaten", My.Resources.MainRohstoffe_16x16, My.Resources.MainRohstoffe_32x32, AddressOf ShowRohstoffForm)
        oGrpStammdaten.AddButton("btnRezeptStamm", "Rezepte", "WinBack Rezeptverwaltung", My.Resources.MainRezept_16x16, My.Resources.MainRezept_32x32, AddressOf ShowRezeptForm)
        oGrpStammdaten.AddButton("btnBenutzer", "Mitarbeiter", "WinBack Benutzer und Benutzergruppen", My.Resources.MainUser_16x16, My.Resources.MainUser_32x32, AddressOf ShowUserForm)
        oGrpStammdaten.AddButton("btnKonstanten", "Schlüsseldaten", "Stammdaten WinBack - Rohstoff/Artikelgruppen, Rezeptvarianten, Produktionsstufen...", My.Resources.MainStammdaten_16x16, My.Resources.MainStammdaten_32x32, AddressOf ShowStammDatenForm)

        'Gruppe Auswertung
        oGrpChargen.AddButton("btnStatistikChargen", "Statistik Chargen", "WinBack Auswertung Produktions-Chargen", My.Resources.MainStatistikChargen_16x16, My.Resources.MainStatistikChargen_32x32, AddressOf ShowStatistikChargenForm)
        oGrpChargen.AddButton("btnStatistikRohstoffe", "Verbrauch Rohstoffe", "WinBack Auswertung Rohstoff Verbrauch", My.Resources.MainStatistikRohstoffe_16x16, My.Resources.MainStatistikRohstoffe_32x32, AddressOf ShowStatistikRohstoffForm)
        oGrpChargen.AddButton("btnStatistikRezepte", "Statistik Rezepte", "WinBack Statistik - Auswertung der produzierten Teige", My.Resources.MainStatistikRezepte_16x16, My.Resources.MainStatistikRezepte_32x32, AddressOf ShowStatistikRezeptForm)

        'Gruppe Linien
        oGrpLinien.AddButton("btnLinien", "Produktions-Linien", "WinBack Produktion Linie 1...", My.Resources.MainLinien_32x32, My.Resources.MainLinien_32x32, AddressOf ShowLinienForm)

        'Gruppe Produktions-Planung
        oGrpPlanung.AddButton("btnProdPlan", "WinBack Produktions-Planung", "", My.Resources.MainProduktionsPlanung_16x16, My.Resources.MainProduktionsPlanung_32x32, AddressOf ShowProduktionsPlanungForm)

        ' Erweitert ein bestehendes RibbonTab um einen Button (Administration/Mitarbeiter)
        Dim oTabs = oMenuService.GetTabs
        Dim oGrps = oTabs(0).GetGroups
        oGrps(3).AddButton("MenuExtensionBtnUser", "WinBack-Mitarbeiter", "Verwaltung der Mitarbeiter-Rechte in WinBack", My.Resources.MainUser_16x16, My.Resources.MainUser_32x32, AddressOf ShowUserForm)
    End Sub

    '---------------------------------------------------------
    '15.04.2016/ V0.9/JW            :Neuanlage
    'Bearbeitet von                 :Will
    '
    'Änderungen:
    '---------------------------------------------------------
    'Beschreibung:
    'Aufruf WinBack-Fenster
    '---------------------------------------------------------

    'Artikel
    Private Sub ShowArtikelForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_Artikel_Main(ServiceProvider), My.Resources.MainArtikel_16x16)
    End Sub
    'Rohstoff
    Private Sub ShowRohstoffForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_Rohstoffe_Main(ServiceProvider), My.Resources.MainRohstoffe_16x16)
    End Sub
    'Rezepte
    Private Sub ShowRezeptForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_Rezept_Main(ServiceProvider), My.Resources.MainRezept_16x16)
    End Sub
    'Mitarbeiter
    Private Sub ShowUserForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_User_Main(ServiceProvider), My.Resources.MainUser_16x16)
    End Sub

    'Stammdaten
    Private Sub ShowStammDatenForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_MainTemplate(ServiceProvider), My.Resources.MainStammdaten_16x16)
    End Sub

    'Statistik Chargen
    Private Sub ShowStatistikChargenForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_MainTemplate(ServiceProvider), My.Resources.MainStatistikChargen_16x16)
    End Sub
    'Statistik Rohstoffe
    Private Sub ShowStatistikRohstoffForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_MainTemplate(ServiceProvider), My.Resources.MainStatistikRohstoffe_16x16)
    End Sub
    'Statistik Rezepte
    Private Sub ShowStatistikRezeptForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_MainTemplate(ServiceProvider), My.Resources.MainStatistikRezepte_16x16)
    End Sub

    'Produktion - Linien
    Private Sub ShowLinienForm(sender As Object, e As EventArgs)
        ' CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_Linien_Main(ServiceProvider), My.Resources.MainLinien_45x34)
    End Sub

    'Produktion - Planung
    Private Sub ShowProduktionsPlanungForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_MainTemplate(ServiceProvider), My.Resources.MainProduktionsPlanung_16x16)
    End Sub

    'alle noch offenen Fenster schliessen
    Private Sub CloseAllForms()
        If xForm IsNot Nothing Then
            xForm.Close()
        End If
    End Sub

    '---------------------------------------------------------
    '19.04.2016/ V0.9/JW            :Neuanlage
    'Bearbeitet von                 :Will
    '
    'Änderungen:
    '---------------------------------------------------------
    'Beschreibung:
    'Event Bearbeitung
    '---------------------------------------------------------

    'Event Login User in OrgaSoft
    Private Sub UserLogin(sender As Object, e As EventArgs)
        Dim sName As String

        ' aktuelle angemeldeter Mitarbeiter
        Dim sEmployee As String = TryCast(oSetting.GetSetting("Anmeldung.Mitarbeiter"), String)
        If Not String.IsNullOrEmpty(sEmployee) Then

            Dim oData As IData = oFactory.GetData
            Using oTable = oData.OpenDataTable(Database.Main, "SELECT Vorname, Nachname FROM Mitarbeiter WHERE MitarbeiterKürzel=@M", LockType.ReadOnly, sEmployee)
                sName = CType(oTable.Rows(0)(0), String) & " " & CType(oTable.Rows(0)(1), String)
            End Using

            My.Settings.AktUser = sName
        End If
    End Sub

    'Event Änderung der Sprache in OrgaSoft
    Private Sub LanguageChange(sender As Object, e As EventArgs)

    End Sub

    'globale System-Konfiguration aus winback.ini einlesen und verfügbar machen
    Private Sub ReadSystemKonfig()
        'Mysql-Einstellungen (IP-Adresse, User, Passwort)
        wb_Konfig.MySqlSetting()
        wb_Konfig.SetColors()
    End Sub

End Class



