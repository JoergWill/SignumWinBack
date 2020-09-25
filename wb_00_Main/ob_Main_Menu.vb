Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.Common
Imports System.Windows.Forms
Imports System.Reflection
Imports System.Threading
Imports Signum.OrgaSoft.GUI

''' <summary>
''' Erweitert das Ribon um ein neues Tab WinBack und baut die Menu-Struktur auf.
''' </summary>
<Export(GetType(IExtension))>
<ExportMetadata("Description", "Erweitert das Ribbon um ein neues Tab(WinBack)")>
Public Class ob_Main_Menu
    Implements IExtension

    Public Property InfoContainer As Common.IInfoContainer Implements Extensibility.IExtension.InfoContainer
    Public Property ServiceProvider As Common.IOrgasoftServiceProvider Implements Extensibility.IExtension.ServiceProvider

    Private oViewProvider As IViewProvider
    Private oMenuService As IMenuService
    Private oSetting As ISettingService
    Private oFactory As IFactoryService

    Private xForm As Form
    Private xLogger As New wb_TraceListener
    Private oRecipeProvider As New ob_RecipeProvider

    ''' <summary>
    ''' Zentraler Excception-Handler für das AddIn.
    ''' Alle nicht behandelten Exceptions werden hier abgefangen. Die Exception und der Stack-Trace werden in das Log geschrieben.
    ''' 
    ''' Im Debug-Modus(Visual-Studio) werden die Exceptions als "unhandledException" abgefangen, zur Laufzeit (in OrgaBack) treten
    ''' die Exceptions als "ThreadException" auf
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Sub MyThreadExceptionHandler(ByVal sender As Object, ByVal e As ThreadExceptionEventArgs)
        'Stacktrace und Fehlermeldung ermitteln
        Dim StackTrace As String = DirectCast(e.Exception, System.Exception).StackTrace
        Dim Message As String = DirectCast(e.Exception, System.Exception).Message
        'Fehlermeldung im Log ausgeben
        Trace.WriteLine(e.Exception)
        'Dialog-Fenster mit Fehlermeldung anzeigen
        ExceptionHandler(StackTrace, Message, False)
    End Sub
    Sub MyUnhandledExceptionHandler(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        'Stacktrace und Fehlermeldung ermitteln
        Dim StackTrace As String = DirectCast(e.ExceptionObject, System.Exception).StackTrace
        Dim Message As String = DirectCast(e.ExceptionObject, System.Exception).Message
        'Fehlermeldung im Log ausgeben
        Trace.WriteLine(e.ExceptionObject)
        'Dialog-Fenster mit Fehlermeldung anzeigen
        ExceptionHandler(StackTrace, Message, True)
    End Sub

    ''' <summary>
    ''' Zeigt ein modales Dialog-Fenster mit der Fehlermeldung an.
    ''' Auswahl Abbrechen, neu starten, fortsetzen.
    ''' Die Fehlermeldung, Stacktrace und Log-File können per Mail an software@winback.de gesendet werden.
    ''' </summary>
    ''' <param name="StackTrace"></param>
    ''' <param name="Message"></param>
    ''' <param name="UnhadledException"></param>
    Private Sub ExceptionHandler(StackTrace As String, Message As String, UnhadledException As Boolean)
        'Prüfen ob die Exception von WinBack oder von OrgaBack kommt
        If StackTrace.Contains("WinBack") Then

            'Dialog-Fenster mit Fehlermeldung anzeigen 
            Dim MainException As New wb_Main_Exception(StackTrace, Message)

            'abhängig vom Dialog-Result 
            Select Case MainException.ShowDialog()
                Case DialogResult.Abort
                    'WinBack-AddIn beenden
                    Trace.WriteLine("@I_DialogResult.Abort - WinBack/OrgaBack beenden")
                    Application.Exit()
                Case DialogResult.Retry
                    'WinBack-AddIn restart
                    Trace.WriteLine("@I_DialogResult.Retry - WinBack/OrgaBack neu starten")
                    Application.Restart()
                Case DialogResult.Ignore
                    'WinBack-AddIn fortsetzen
                    Trace.WriteLine("@I_DialogResult.Ignore - WinBack-AddIn fortsetzen")
                Case Else
                    'WinBack-AddIn fortsetzen
                    Trace.WriteLine("@I_DialogResult.xxx - WinBack-AddIn fortsetzen")

            End Select
        End If
    End Sub

    ''' <summary>
    ''' Initialisierung Klasse
    ''' </summary>
    Public Sub Initialize() Implements IExtension.Initialize

        AddHandler System.Windows.Forms.Application.ThreadException, AddressOf MyThreadExceptionHandler
        AddHandler System.AppDomain.CurrentDomain.UnhandledException, AddressOf MyUnhandledExceptionHandler

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
        'Event User-Login - Read System-Konfig
        AddEventBroker()

        'erster Start WinBack-AddIn nach Setup - Einstellungen Mandanten
        'TODO - Hier TEST-AUFRUF
        'Dim AddInSetup As New wb_Main_Setup
        'AddInSetup.ShowDialog()

        'Main-Menu erweitern
        AddMenu()
        'Check winback-Datenbank
        Dim AdminCheck As New wb_Admin_CheckDatabase()
        If Not AdminCheck.CheckDatabase Then
            MsgBox("Die aktuelle WinBack-Datenbank ist nicht kompatibel mit dieser AddIn-Version." & "Bitte WinBack-Datenbank updaten !!", MsgBoxStyle.Critical, "WinBack-AddIn")
        End If

    End Sub

    ''' <summary>
    ''' MyResolveEventHandler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <returns></returns>
    Private Shared Function MyResolveEventHandler(sender As Object, args As ResolveEventArgs) As Assembly
        Console.WriteLine("Resolving...")
        Return GetType(ob_Main_Menu).Assembly
    End Function

    ''' <summary>
    ''' Event User-Login, Änderung der Sprache-Einstellungen
    ''' Event ProcessChanged, wird von OrgaBack bei Vorfällen aufgerufen.
    ''' </summary>
    Private Sub AddEventBroker()
        Dim IEB As IEventBroker = TryCast(ServiceProvider.GetService(GetType(IEventBroker)), IEventBroker)
        If IEB IsNot Nothing Then
            AddHandler IEB.LoginChanged, AddressOf UserLogin
            AddHandler IEB.LanguageChanged, AddressOf LanguageChanged
            AddHandler IEB.ProcessChanged, AddressOf ProcessChanged
        End If

        'globale System-Einstellungen lesen
        ReadSystemKonfig()
        'beim Laden des AddIns ist schon ein Mitarbeiter angemeldet
        UserLogin(Me, EventArgs.Empty)
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
        Dim oGrpPlanung = oNewTab.AddGroup("WinBack", "Teig-Herstellung")
        Dim oGrpAdmin = oNewTab.AddGroup("WinBack", "Administration")

        'Gruppe Stammdaten
        oGrpStammdaten.AddButton("btnArtikelStamm", "Artikel", "WinBack Artikelstammdaten", My.Resources.MainArtikel_16x16, My.Resources.MainArtikel_32x32, AddressOf ShowArtikelForm)
        oGrpStammdaten.AddButton("btnRohstoffStamm", "Rohstoffe", "WinBack Rohstoff-Stammdaten", My.Resources.MainRohstoffe_16x16, My.Resources.MainRohstoffe_32x32, AddressOf ShowRohstoffForm)
        oGrpStammdaten.AddButton("btnRezeptStamm", "Rezepte", "WinBack Rezeptverwaltung", My.Resources.MainRezept_16x16, My.Resources.MainRezept_32x32, AddressOf ShowRezeptForm)
        oGrpStammdaten.AddButton("btnBenutzer", "Mitarbeiter", "WinBack Benutzer und Benutzergruppen", My.Resources.MainUser_16x16, My.Resources.MainUser_32x32, AddressOf ShowUserForm)
        oGrpStammdaten.AddButton("btnKonstanten", "Schlüsseldaten", "Stammdaten WinBack - Rohstoff/Artikelgruppen, Rezeptvarianten, Produktionsstufen...", My.Resources.MainStammdaten_16x16, My.Resources.MainStammdaten_32x32, AddressOf ShowStammDatenForm)

        'Gruppe Auswertung
        oGrpChargen.AddButton("btnStatistik", "Statistik Produktion", "WinBack Auswertung Produktions-Chargen", My.Resources.MainStatistikChargen_16x16, My.Resources.MainStatistikChargen_32x32, AddressOf ShowStatistikForm)

        'Gruppe Linien
        oGrpLinien.AddButton("btnLinien", "Produktions-Linien", "WinBack Produktion Linie 1...", My.Resources.MainLinien_32x32, My.Resources.MainLinien_32x32, AddressOf ShowLinienForm)

        'Gruppe Produktions-Planung
        oGrpPlanung.AddButton("btnProdPlan", "WinBack Teig-Herstellung", "", My.Resources.MainProduktionsPlanung_16x16, My.Resources.MainProduktionsPlanung_32x32, AddressOf ShowProduktionsPlanungForm)

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
        xForm = oViewProvider.OpenForm(New wb_StammDaten_Main(ServiceProvider), My.Resources.MainStammdaten_16x16)
        wb_DockBarPanelShared.SetFormBoundaries(xForm, "Stammdaten")
    End Sub

    ''' <summary>
    ''' Aufruf WinBack-Fenster Statistik
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowStatistikForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_Chargen_Main(ServiceProvider), My.Resources.MainStatistikChargen_16x16)
        wb_DockBarPanelShared.SetFormBoundaries(xForm, "Statistik")
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

        'Produktions-Daten beim Öffnen des WinBack-Formulars (nicht)einlesen
        wb_GlobalSettings.ProdPlanReadOnOpen = False

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

            wb_GlobalSettings.AktUserLogin(sName)
        End If
    End Sub

    ''' <summary>
    ''' Event Änderung der Sprache in OrgaSoft
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LanguageChanged(sender As Object, e As EventArgs)

    End Sub

    ''' <summary>
    ''' Ermittelt zu den entsprechenden Vorfällen in OrgaBack die Vorfallart(ProcessCode) und die Vorfallnummer(ProcessNumber). Anhand der ProcessAction
    ''' kann unterschieden werden, wie der Vorfall bearbeitet wird.
    ''' 
    '''     ProcessAction
    '''     None       = 0
    '''     Booked = 1
    '''     Voided = 2
    '''     Saved = 3
    '''     Created = 4
    '''     Printed = 5
    '''     Approval1 = 6
    '''     Approval2 = 7
    '''     Approval3 = 8
    '''     ApprovalRemoved = 9
    '''     Cancelled = 10
    '''     
    ''' Die detaillierten Daten stehen in der Tabelle [dbo].[GeschäftsvorfallPosition]
    ''' 
    ''' Wird momentan verwendet für den Vorfall WE(Booked) um bei Silo-Rohstoffen die Verteilung auf die einzelnen Silos abzufragen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ProcessChanged(sender As Object, e As ERP.IProcessChangedEventArgs)
        Try
            'Vorfall-Kürzel
            Dim ProcessCode As String = e.ProcessCode
            'Vorfall-Nummer
            Dim ProcessNumber As String = e.ProcessNumber
            'Vorfall-Aktion
            Dim ProcessAction As ERP.ProcessChangedAction = e.Action

            'TEST Ausgabe
            Debug.Print("WinBack.ob_Main.ProcessChanged " & ProcessCode & "/" & ProcessNumber & "/" & ProcessAction.ToString)

            'Verteiler OrgaBack - Vorfälle
            Select Case ProcessCode

            'Wareneingang
                Case "WE"
                    'Vorfall WarenEingang in WinBack verbuchen
                    ShowWarenEingangForm(ProcessCode, ProcessNumber, ProcessAction)

                Case Else
                    'alle anderen Vorfälle
                    Debug.Print("WinBack.ob_Main.ProcessChanged " & ProcessCode & "/" & ProcessNumber & "/" & ProcessAction.ToString)

            End Select
        Catch ex As Exception
            Trace.Write("Fehler bei ProcessChanged. OrgaBack-Version kompatibel?")
        End Try
    End Sub

    ''' <summary>
    ''' Verarbeitung Wareneingang und Aufruf Rohstoffe-Silo-Dialogfenster
    ''' 
    ''' Der Aufruf erfolgt über den EventBroker durch den Event ProcessChanged. Jeder OrgaBack-Vorfall WE wird hier verarbeitet.
    ''' Wenn der Wareneingang nicht eindeutig verbucht werden kann weil zu einer Rohstoff-Nummer mehrere Silos vorhanden sind, wird
    ''' ein entsprechendes Dialog-Fenster geöffnet um die Lieferung auf die einzelnen WinBack-Rohstoffe zu verteilen.
    ''' 
    ''' Die Funktions-Aufrufe sind verschachtelt, damit die Erzeugung der Dialog-Fenster im Main(AddIn)Prozess bleibt.
    ''' </summary>
    ''' <param name="ProcessCode"></param>
    ''' <param name="ProcessNumber"></param>
    ''' <param name="ProcessAction"></param>
    Private Sub ShowWarenEingangForm(ProcessCode As String, ProcessNumber As String, ProcessAction As String)
        If ProcessAction = ERP.ProcessChangedAction.Booked Then
            'Daten aus [dbo].[GeschäftsvorfallPosition]
            Dim Wareneingang As New wb_OrgaBackProcess_WE(ProcessCode, ProcessNumber)

            'Liste der Wareneingänge verbuchen - bei Silo-Rohstoffen wird ein Dialog-Fenster angezeigt. (Silo-Auswahl/Verteilung)
            For Each ProcPosition As wb_OrgaBackProcessPosition In Wareneingang.ProcPositions
                'Wareneingang verbuchen. Bei Rohstoffen die nicht eindeutig sind (mehrere Silos) wird False zurückgegeben
                If Not Wareneingang.DoAction(ProcPosition.PositionNummer, ProcessAction) Then
                    'Dialogfenster Silo-Füllstände und Befüllung
                    Dim Rohstoffe_Silo As New wb_Rohstoffe_Silo
                    'Anzeige Fenster Silo-Befüllung für diese Prozess-Komponente(Befüllgun)
                    Rohstoffe_Silo.ShowBefuellungDialog(ProcPosition)
                    'Wareneingang verbuchen. Die Verteilung auf die einzelnen Silos wird in der Liste angegeben
                    Wareneingang.DoAction(ProcPosition.PositionNummer, ProcessAction, Rohstoffe_Silo.SiloVerteilung)
                    'Speicher wieder freigaben
                    Rohstoffe_Silo.Dispose()
                End If
            Next

        End If
    End Sub

    ''' <summary>
    ''' Globale Systemkonfiguration aus dem oSetting-Objekt auslesen und in winback.ini schreiben.
    ''' Damit können auch andere Tasks die Einstellungen lesen (Server-Task...)
    ''' </summary>
    Private Sub ReadSystemKonfig()
        'Programm-Einstellung OrgaBack
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack
        'Signum.OrgaSoft.Common.Settings.Verzeichnisse.AddInPfad
        wb_GlobalSettings.pAddInPath = TryCast(oSetting.GetSetting("Verzeichnisse.AddInPfad"), String)
        'Signum.OrgaSoft.Common.Settings.Verzeichnisse.ListenPfad
        wb_GlobalSettings.pListenPath = TryCast(oSetting.GetSetting("Verzeichnisse.ListenPfad"), String)
        'Signum.OrgaSoft.Common.Settings.Verzeichnisse.TempPath
        wb_GlobalSettings.pTempPath = TryCast(oSetting.GetSetting("Verzeichnisse.TempPfad"), String)
        'Signum.OrgaSoft.Common.Settings.Verzeichnisse.ProgrammPfad
        wb_GlobalSettings.pProgrammPath = TryCast(oSetting.GetSetting("Verzeichnisse.ProgrammPfad"), String)
        'Signum.OrgaSoft.Common.Settings.Verzeichnisse.DatenPfad
        wb_GlobalSettings.pDatenPath = TryCast(oSetting.GetSetting("Verzeichnisse.DatenPfad"), String)

        'Signum.OrgaSoft.Common.Settings.Datenbank.VerwaltungsDatenbank
        wb_GlobalSettings.MsSQLAdmn = TryCast(oSetting.GetSetting("Datenbank.VerwaltungsDatenbank"), String)
        'Signum.OrgaSoft.Common.Settings.Datenbank.Hauptdatenbank
        wb_GlobalSettings.MsSQLMain = TryCast(oSetting.GetSetting("Datenbank.Hauptdatenbank"), String)
        'WinBack-Mandant entspricht der Mandant-Nummer in OrgaBack (kann erst ermittelt werden, wenn die Admin-Datenbank definiert ist !)
        wb_GlobalSettings.MandantNr = wb_GlobalSettings.OrgaBackMandantNr

        'MsSQL-DB-User 
        wb_GlobalSettings.MsSQLUserId = TryCast(oSetting.ReadSetting("AddIn", "WinBack", "MSSQL_UserID", ""), String)
        'MsSQL-DB-Passwort
        wb_GlobalSettings.MsSQLPasswd = TryCast(oSetting.ReadSetting("AddIn", "WinBack", "MSSQL_Passwd", ""), String)
        'Wenn das MsSQL-DB-Passwort in den Einstellungen gesetzt wurde, wird der Eintrag beim nächsten Start gelöscht !
        oSetting.WriteSetting("AddIn", "WinBack", "MSSQL_Passwd", "")

        'MySQL-Server-IP 
        wb_GlobalSettings.MySQLServerIP = TryCast(oSetting.ReadSetting("AddIn", "WinBack", "MySQLServerIP", ""), String)
        'MySQL-Main-DB
        wb_GlobalSettings.MySQLWinBack = TryCast(oSetting.ReadSetting("AddIn", "WinBack", "MySQLDatabase", ""), String)
        'MySQL-Archiv-DB
        wb_GlobalSettings.MySQLWbDaten = TryCast(oSetting.ReadSetting("AddIn", "WinBack", "MySQLDatabaseDaten", ""), String)

        'Artikel-Gruppe Backwaren (Default 10)
        wb_GlobalSettings.OsGrpBackwaren = TryCast(oSetting.ReadSetting("AddIn", "WinBack", "GruppeBackwaren", ""), String)
        'Artikel-Gruppe Rohstoffe (Default 20)
        wb_GlobalSettings.OsGrpRohstoffe = TryCast(oSetting.ReadSetting("AddIn", "WinBack", "GruppeRohstoffe", ""), String)

        'Nährwertberechnung Zutatenliste aus interner Deklaration
        'TODO Funktioniert nicht - URSACHE?
        'wb_GlobalSettings.NwtInterneDeklaration = (TryCast(oSetting.ReadSetting("AddIn", "WinBack", "InterneDeklaration", ""), String) = "1")


        'Signum Default-Land
        wb_GlobalSettings.osLaendercode = TryCast(oSetting.GetSetting("Festwerte.DefaultLand"), String)
        'Signum Default-Sprache
        wb_GlobalSettings.osSprachcode = TryCast(oSetting.GetSetting("Festwerte.DefaultSprache"), String)
        'Signum Default-Währung
        wb_GlobalSettings.osDefaultWaehrung = TryCast(oSetting.GetSetting("Festwerte.DefaultWährung"), String)

        'Signum Docking-Theme
        Try
            wb_GlobalSettings.OrgaBackTheme = oSetting.GetSetting("Desktop.DockingTheme")
        Catch
            wb_GlobalSettings.OrgaBackTheme = wb_Global.UNDEFINED
        End Try

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



