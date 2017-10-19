Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.Common
Imports System.Windows.Forms
Imports System.Reflection

''' <summary>
''' Erweitert das Ribon um ein neues Tab WinBack und baut die Menu-Struktur auf.
''' </summary>
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
    Private xLogger As New wb_TraceListener

    ''' <summary>
    ''' Initialisierung Klasse
    ''' </summary>
    Public Sub Initialize() Implements IExtension.Initialize
        'siehe Mail vom 13.Juli 2017 J.Erhardt - laden der dll schläg fehl 
        '
        'Dim currentDomain As AppDomain = AppDomain.CurrentDomain
        'AddHandler currentDomain.AssemblyResolve, AddressOf MyResolveEventHandler

        oViewProvider = TryCast(ServiceProvider.GetService(GetType(IViewProvider)), IViewProvider)
        oMenuService = TryCast(ServiceProvider.GetService(GetType(IMenuService)), IMenuService)
        oSetting = TryCast(ServiceProvider.GetService(GetType(ISettingService)), ISettingService)
        oFactory = TryCast(ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)

        'Debug/Trace-Listener initialisieren
        AddTraceListener()
        'Event User-Login
        AddLogIn()
        'Main-Menu erweitern
        AddMenu()

    End Sub

    ''' <summary>
    ''' MyResolveEventHandler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <returns></returns>
    Private Shared Function MyResolveEventHandler(sender As Object, args As ResolveEventArgs) As Assembly
        Console.WriteLine("Resolving...")
        Return GetType(wb_Main_Menu).Assembly
    End Function

    ''' <summary>
    ''' Event User-Login
    ''' </summary>
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

    ''' <summary>
    ''' Erzeugen Hauptmenu im Ribbon Orgasoft
    ''' </summary>
    Private Sub AddMenu()
        ' Fügt dem Ribbon ein neues RibbonTab 'WinBack' hinzu
        Dim oNewTab = oMenuService.AddTab("wb_MainMenu", "WinBack", "WinBack in OrgaBack integriert")

        ' Das neue RibbonTab 'WinBack' erhält Haupt-Menu-Gruppen 'Stammdaten' 'Auswertung' 'Linien'
        Dim oGrpStammdaten = oNewTab.AddGroup("WinBack", "Stammdaten (Produktion)")
        Dim oGrpChargen = oNewTab.AddGroup("WinBack", "Auswertung (Produktion)")
        Dim oGrpLinien = oNewTab.AddGroup("WinBack", "Linien")
        Dim oGrpPlanung = oNewTab.AddGroup("WinBack", "Produktions-Planung")
        Dim oGrpAdmin = oNewTab.AddGroup("WinBack", "Administration")

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

        'Gruppe Administration
        oGrpAdmin.AddButton("btnAdmin", "WinBack Administration", "", My.Resources.Admin_16x16, My.Resources.Admin_32x32, AddressOf ShowAdminAdministrationForm)

        ' Erweitert ein bestehendes RibbonTab um einen Button (Administration/Mitarbeiter)
        'Dim oTabs = oMenuService.GetTabs
        'Dim oGrps = oTabs(0).GetGroups
        'oGrps(3).AddButton("MenuExtensionBtnUser", "WinBack-Mitarbeiter", "Verwaltung der Mitarbeiter-Rechte in WinBack", My.Resources.MainUser_16x16, My.Resources.MainUser_32x32, AddressOf ShowUserForm)
    End Sub

    ''' <summary>
    ''' alle Trace/Debug-Ausgaben werden auch in der Klasse wb_Admin_Shared in einer Text-Liste gespeichert.
    ''' Nach x Zeilen werden die Einträge in ein Text-File gespeichert.
    ''' Die Klasse xLogger (wb_Trace_Listener) leitet die Meldungen weiter.
    ''' </summary>
    Sub AddTraceListener()
        AddHandler xLogger.WriteText, AddressOf wb_Admin_Shared.GetTraceListenerText
        Trace.Listeners.Add(xLogger)
    End Sub

    ''' <summary>
    ''' Aufruf WinBack-Fenster Artikel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowArtikelForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_Artikel_Main(ServiceProvider), My.Resources.MainArtikel_16x16)
        'Fensterposition aus winback.ini
        wb_DockBarPanelShared.SetFormBoundaries(xForm, "Artikel")
    End Sub

    ''' <summary>
    ''' Aufruf WinBack-Fenster Rohstoffe
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowRohstoffForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_Rohstoffe_Main(ServiceProvider), My.Resources.MainRohstoffe_16x16)
        'Fensterposition aus winback.ini
        wb_DockBarPanelShared.SetFormBoundaries(xForm, "Rohstoffe")
    End Sub

    ''' <summary>
    ''' Aufruf WinBack-Fenster Rezepte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowRezeptForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_Rezept_Main(ServiceProvider), My.Resources.MainRezept_16x16)
        'Fensterposition aus winback.ini
        wb_DockBarPanelShared.SetFormBoundaries(xForm, "Rezepte")
    End Sub

    ''' <summary>
    ''' Aufruf WinBack-Fenster Mitarbeiter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowUserForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_User_Main(ServiceProvider), My.Resources.MainUser_16x16)
        'Fensterposition aus winback.ini
        wb_DockBarPanelShared.SetFormBoundaries(xForm, "User")
    End Sub

    ''' <summary>
    ''' Aufruf WinBack-Fenster Stammdaten
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowStammDatenForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_MainTemplate(ServiceProvider), My.Resources.MainStammdaten_16x16)
    End Sub

    ''' <summary>
    ''' Aufruf WinBack-Fenster Statistik Chargen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowStatistikChargenForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_MainTemplate(ServiceProvider), My.Resources.MainStatistikChargen_16x16)
    End Sub

    ''' <summary>
    ''' Aufruf WinBack-Fenster Statistik Rohstoffe
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowStatistikRohstoffForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_MainTemplate(ServiceProvider), My.Resources.MainStatistikRohstoffe_16x16)
    End Sub

    ''' <summary>
    ''' Aufruf WinBack-Fenster Statistik Rezepte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowStatistikRezeptForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_MainTemplate(ServiceProvider), My.Resources.MainStatistikRezepte_16x16)
    End Sub

    ''' <summary>
    ''' Aufruf WinBack-Fenster Linien
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowLinienForm(sender As Object, e As EventArgs)
        ' CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_Linien_Main(ServiceProvider), My.Resources.MainLinien_45x34)
        'Fensterposition aus winback.ini
        wb_DockBarPanelShared.SetFormBoundaries(xForm, "Linien")
    End Sub

    ''' <summary>
    ''' Aufruf WinBack-Fenster Produktions-Planung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowProduktionsPlanungForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_Planung_Main(ServiceProvider), My.Resources.MainProduktionsPlanung_16x16)
        'Fensterposition aus winback.ini
        wb_DockBarPanelShared.SetFormBoundaries(xForm, "Produktion")
    End Sub

    ''' <summary>
    ''' Aufruf WinBack-Fenster Admin
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowAdminAdministrationForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_Admin_Main(ServiceProvider), My.Resources.Admin_16x16)
        wb_DockBarPanelShared.SetFormBoundaries(xForm, "Admin")
    End Sub

    ''' <summary>
    ''' alle noch offenen Fenster schliessen
    ''' </summary>
    Private Sub CloseAllForms()
        If xForm IsNot Nothing Then
            xForm.Close()
        End If
    End Sub

    ''' <summary>
    ''' Event Login User in OrgaSoft
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub UserLogin(sender As Object, e As EventArgs)
        Dim sName As String

        ' aktuelle angemeldeter Mitarbeiter
        Dim sEmployee As String = TryCast(oSetting.GetSetting("Anmeldung.Mitarbeiter"), String)
        If Not String.IsNullOrEmpty(sEmployee) Then

            Dim oData As IData = oFactory.GetData
            Using oTable = oData.OpenDataTable(Database.Main, "SELECT Vorname, Nachname FROM Mitarbeiter WHERE MitarbeiterKürzel=@M", LockType.ReadOnly, sEmployee)
                sName = CType(oTable.Rows(0)(0), String) & " " & CType(oTable.Rows(0)(1), String)
            End Using

            wb_GlobalSettings.AktUser = sName
            'TODO UserNummer ermitteln und in wb_Global
        End If
    End Sub

    ''' <summary>
    ''' Event Änderung der Sprache in OrgaSoft
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LanguageChange(sender As Object, e As EventArgs)

    End Sub

    'globale System-Konfiguration aus winback.ini einlesen und verfügbar machen
    Private Sub ReadSystemKonfig()
        'Programm-Einstellung OrgaBack
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack
        'Signum.OrgaSoft.Common.Settings.Verzeichnisse.AddInPfad
        wb_GlobalSettings.pAddInPath = TryCast(oSetting.GetSetting("Verzeichnisse.AddInPfad"), String)
        'Signum.OrgaSoft.Common.Settings.Verzeichnisse.ListenPfad
        wb_GlobalSettings.pListenPath = TryCast(oSetting.GetSetting("Verzeichnisse.ListenPfad"), String)
        'Signum.OrgaSoft.Common.Settings.Verzeichnisse.ProgrammPfad
        wb_GlobalSettings.pProgrammPath = TryCast(oSetting.GetSetting("Verzeichnisse.ProgrammPfad"), String)
        'Signum.OrgaSoft.Common.Settings.Verzeichnisse.DatenPfad
        wb_GlobalSettings.pDatenPath = TryCast(oSetting.GetSetting("Verzeichnisse.DatenPfad"), String)
        'Signum.OrgaSoft.Common.Settings.Datenbank.Hauptdatenbank
        wb_GlobalSettings.MsSQLMain = TryCast(oSetting.GetSetting("Datenbank.Hauptdatenbank"), String)
        'Programm-Version OrgaBack
        Dim PVersion = Assembly.GetEntryAssembly().GetName().Version

        Dim asm As Assembly = Assembly.GetExecutingAssembly()
        Dim location As String = asm.Location
        Dim version = asm.GetName.Version
        'TODO Hier fliegt die Relase-Version auf die Nase
        'Try
        '    Dim appName As String = System.IO.Path.GetDirectoryName(location)
        'Catch
        'End Try

        'Debug.Print("STOP")
        'Dim  As String = Signum.OrgaSoft.Common.Settings.Verzeichnisse.AddInPfad

        'Dim sAssemblyName As String = New AssemblyName(args.Name).Name
        'Dim arrFields As String() = args.Name.Split(","c)
        'Dim sAssemblyCulture As String = arrFields(2).Substring(arrFields(2).IndexOf("="c) + 1)

        'Dim sAssemblyFileName As String = sAssemblyName + ".dll"
        'Dim sAssemblyPath As String

        'If sAssemblyName.EndsWith(".resources") Then
        '    Dim sResourceDirectory As String = Path.Combine(sApplicationDirectory, sAssemblyCulture)
        '    sAssemblyPath = Path.Combine(sResourceDirectory, sAssemblyFileName)
        'Else
        '    sAssemblyPath = Path.Combine(sApplicationDirectory, sAssemblyFileName)
        'End If

        'If File.Exists(sAssemblyPath) Then
        '    Return If(Debugger.IsAttached, Reflection.Assembly.LoadFile(sAssemblyPath), Assembly.Load(File.ReadAllBytes(sAssemblyPath)))
        'Else
        '    Return Nothing
        'End If


    End Sub

End Class



