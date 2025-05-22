Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.Common
Imports System.Windows.Forms
Imports System.Reflection
Imports System.Threading

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

    'ab VERSION 3.6.1
    'Private xForm As Form
    Private xForm As GUI.IForm
    Private xLogger As New wb_TraceListener
    Private oRecipeProvider As New ob_RecipeProvider
    Private ServerConnect As New wb_Main_ServerConnect

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
        Trace.WriteLine("@E_" & e.Exception.ToString)
        'Dialog-Fenster mit Fehlermeldung anzeigen
        ExceptionHandler(StackTrace, Message, False)
    End Sub
    Sub MyUnhandledExceptionHandler(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        'Stacktrace und Fehlermeldung ermitteln
        Dim StackTrace As String = DirectCast(e.ExceptionObject, System.Exception).StackTrace
        Dim Message As String = DirectCast(e.ExceptionObject, System.Exception).Message
        'Fehlermeldung im Log ausgeben
        Trace.WriteLine("@E_" & e.ExceptionObject.ToString)
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
    ''' <param name="UnhandledException"></param>
    <CodeAnalysis.SuppressMessage("Style", "IDE0060:Nicht verwendete Parameter entfernen", Justification:="<Ausstehend>")>
    Private Sub ExceptionHandler(StackTrace As String, Message As String, UnhandledException As Boolean)
        'Prüfen ob die Exception von WinBack oder von OrgaBack kommt
        If StackTrace.Contains("WinBack") Then

            'Dialog-Fenster mit Fehlermeldung anzeigen 
            Dim MainException As New wb_Main_Exception(StackTrace, Message)

            'abhängig vom Dialog-Result 
            Select Case MainException.ShowDialog()
                Case DialogResult.Abort
                    'WinBack-AddIn beenden
                    Trace.WriteLine("@I_DialogResult.Abort - WinBack/OrgaBack beenden")
                    wb_Functions.ExitProgram()
                Case DialogResult.Retry
                    'WinBack-AddIn restart
                    Trace.WriteLine("@I_DialogResult.Retry - WinBack/OrgaBack neu starten")
                    wb_Functions.Restart()
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
    ''' AssemblyResolve wird definiert in WinBackAddIn.Erweiterte Kompilierungsoptionen
    ''' </summary>
    Public Sub Initialize() Implements IExtension.Initialize

        AddHandler System.Windows.Forms.Application.ThreadException, AddressOf MyThreadExceptionHandler
        AddHandler System.AppDomain.CurrentDomain.UnhandledException, AddressOf MyUnhandledExceptionHandler

        'AssemblyResolve wird definiert in WinBackAddIn.Erweiterte Kompilierungsoptionen
        If wb_Global.AssemblyResolve Then
            'Die eigenen dll-Files in sep. Verzeichnis verlagern
            AddHandler System.AppDomain.CurrentDomain.AssemblyResolve, AddressOf MyAssemblyResolve
        End If

        'Event-Handler Aufruf einer WinBack-Main-Form
        AddHandler wb_Main_Shared.eOpenForm, AddressOf OpenWinBackForm

        'Event-Handler SendMessage an WinBack-Background-Task
        AddHandler wb_Main_Shared.eSendMessage, AddressOf SendMessageToServer

        oViewProvider = TryCast(ServiceProvider.GetService(GetType(IViewProvider)), IViewProvider)
        oMenuService = TryCast(ServiceProvider.GetService(GetType(IMenuService)), IMenuService)
        oSetting = TryCast(ServiceProvider.GetService(GetType(ISettingService)), ISettingService)
        oFactory = TryCast(ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)

        'Debug/Trace-Listener initialisieren
        AddTraceListener()

        'Event User-Login - Read System-Konfig
        AddEventBroker()

        'erster Start WinBack-AddIn nach Setup - Einstellungen Mandanten
        'Dim AddInSetup As New wb_Main_Setup
        'AddInSetup.ShowDialog()

        'Main-Menu erweitern
        AddMenu()

        'Dashboad anzeigen
        If wb_GlobalSettings.ShowDashboard Then
            ShowMainDashboardForm(Nothing, Nothing)
        End If

        'Check Server-Background-Task. Das Fenster läuft unsichtbar im Hintergrund
        If Not Debugger.IsAttached Then
            ServerConnect.Show()
            wb_GlobalSettings.WinBackBackgroudTaskConnected = ServerConnect.Connect(wb_GlobalSettings.MsSQLServerIP, wb_Global.WinBackServerTaskPort, False)
        End If

        'Wenn der Admin angemeldet ist - wird das System geprüft und im Fehlerfall eine Meldung ausgegeben
        If wb_AktUser.SuperUser Then
            CheckSystem()
        End If
    End Sub

    Private Sub CheckSystem()
        'Check winback-Datenbank
        Dim AdminCheck As New wb_Admin_CheckDatabase()
        If Not AdminCheck.CheckDatabase Then
            MsgBox("Die aktuelle WinBack-Datenbank ist nicht kompatibel mit dieser AddIn-Version." & vbCrLf & "Bitte WinBack-Datenbank updaten !!", MsgBoxStyle.Critical, "WinBack-AddIn")
        End If
        'Prüfen ob der WinBack-Backgroud-Task läuft
        If Not wb_GlobalSettings.WinBackBackgroudTaskConnected AndAlso Not Debugger.IsAttached Then
            MsgBox("Der WinBack-Server-Task läuft nicht." & vbCrLf & "Bitte den Prozess auf dem Server " & wb_GlobalSettings.MsSQLServerIP & " starten !!", MsgBoxStyle.Critical, "WinBack-AddIn")
        End If
    End Sub

    ''' <summary>
    ''' Meldung an WinBack-Server-Background-Task senden
    ''' </summary>
    ''' <param name="Message"></param>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S1172:Unused procedure parameters should be removed", Justification:="<Ausstehend>")>
    Private Sub SendMessageToServer(sender As Object, Message As String)
        If wb_GlobalSettings.WinBackBackgroudTaskConnected Then
            ServerConnect.SendData(Message)
        End If
    End Sub

    Private Function MyAssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
        Return wb_Main_Shared.MyAssemblyResolve(sender, args, GetType(ob_Main_Menu).Assembly)
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
            'Mail JE vom 30.09.2021 ab Version 3.4 !!
            'AddHandler IEB.StockCorrectionExecuted, AddressOf StockCorrectionExecuted

        End If

        'globale System-Einstellungen lesen
        ReadSystemKonfig()
        'beim Laden des AddIns ist schon ein Mitarbeiter angemeldet
        UserLogin(Me, EventArgs.Empty)
    End Sub

    ''' <summary>
    ''' Erzeugen Hauptmenu im Ribbon Orgasoft. Wenn ein Demo-Mandant erkannt wird (Text Demo im WinBack-Kunden-Namen) wird an das
    ''' Hauptmenu der Text (Test) angehängt.
    ''' Abhängig von den Benutzer-Rechten in WinBack
    '''     
    ''' 2022-03-11 Text WinBack-Produktion geändert in Produktion
    '''      IP_ItemID           Tag
    ''' --+-------------------+-------+-------------------------
    '''      0 = Produktion      100
    '''      1 = Chargen         101
    '''      2 = Artikel         102
    '''      3 = Rezepte         103
    '''      4 = Rohstoffe       104
    '''      5 = Service         105
    '''      6 = Installation    106
    '''      7 = Hilfe           107
    '''      
    '''      Module WinBack-Büro
    ''' --+-------------------+-------+-------------------------
    '''      20 = Benutzerverw    120
    '''      21 = Vnc             121
    '''      22 = Statistik       122
    '''      23 = Rezepthistorie  123
    '''      24 = Material Import 124
    '''      25 = Excel Export    125
    '''      26 = Bakelink        126
    '''      27 = Bestellwesen    127
    '''      28 = Inhalts-Stoffe  128
    '''      29 = Cloud           129 (ohne Recht 29 Cloud nur mit Einschränkungen)
    '''      30 = Prod.Planung    130
    '''      31 = Produktion      131 verbuchen der Backliste
    '''     
    ''' </summary>
    Private Sub AddMenu()
        ' Erweitert ein bestehendes RibbonTab um einen Button (Administration/Mitarbeiter)
        Dim oTabs = oMenuService.GetTabs
        Dim oGrps = oTabs(0).GetGroups
        Dim MainMenuTitel As String = "Produktion"

        'Kennung WinBack-Demo-Mandant
        If wb_GlobalSettings.WinBackDemoMandant Then
            MainMenuTitel &= "(Test)"
        End If
        'Fügt dem Ribbon ein neues RibbonTab 'WinBack' hinzu
        Dim oNewTab = oMenuService.AddTab("wb_MainMenu", MainMenuTitel, "WinBack in OrgaBack integriert")

        'Das neue RibbonTab 'WinBack' erhält Haupt-Menu-Gruppen 'Stammdaten' 'Admin' 'Linien' 'Chargen' und 'Planung'
        Dim oGrpStammdaten = oNewTab.AddGroup("WinBackDaten", "Stammdaten (Produktion)")
        Dim oGrpAdmin = oNewTab.AddGroup("WinBackAdmin", "Administration")
        Dim oGrpLinien = oNewTab.AddGroup("WinBackLinien", "Linien")
        Dim oGrpChargen = oNewTab.AddGroup("WinBackChargen", "Auswertung (Produktion)")
        Dim oGrpPlanung = oNewTab.AddGroup("WinBackPlanung", "Teig-Herstellung")
        Dim oGrpProgramm = oNewTab.AddGroup("WinBackProgramm", "Programm")

        'Gruppe Stammdaten Button Artikel (Tag 102-Artikelverwaltung)
        ob_Main_Shared.AddMenuButton(oGrpStammdaten, "btnArtikelStamm", "Artikel", "WinBack Artikelstammdaten", My.Resources.MainArtikel_16x16, My.Resources.MainArtikel_32x32, AddressOf ShowArtikelForm, 102)
        'Gruppe Stammdaten Button Rezepte (Tag 103-Rezeptverwaltung)
        ob_Main_Shared.AddMenuButton(oGrpStammdaten, "btnRezeptStamm", "Rezepte", "WinBack Rezeptverwaltung", My.Resources.MainRezept_16x16, My.Resources.MainRezept_32x32, AddressOf ShowRezeptForm, 103)
        'Gruppe Stammdaten Button Rohstoffe (Tag 104-Rohstoffverwaltung)
        ob_Main_Shared.AddMenuButton(oGrpStammdaten, "btnRohstoffStamm", "Rohstoffe", "WinBack Rohstoff-Stammdaten", My.Resources.MainRohstoffe_16x16, My.Resources.MainRohstoffe_32x32, AddressOf ShowRohstoffForm, 104)
        'Gruppe Stammdaten Button Schlüsseldaten (Tag 106-Installation)
        ob_Main_Shared.AddMenuButton(oGrpStammdaten, "btnKonstanten", "Schlüsseldaten", "Stammdaten WinBack - Rohstoff/Artikelgruppen, Rezeptvarianten, Produktionsstufen...", My.Resources.MainStammdaten_16x16, My.Resources.MainStammdaten_32x32, AddressOf ShowStammDatenForm, 106)
        'Gruppe Stammdaten Button Mitarbeiter (Tag 120-Benutzerverwaltung)
        ob_Main_Shared.AddMenuButton(oGrpStammdaten, "btnBenutzer", "Mitarbeiter", "WinBack Benutzer und Benutzergruppen", My.Resources.MainUser_16x16, My.Resources.MainUser_32x32, AddressOf ShowUserForm, 120)

        'Gruppe OrgaBack-Mitarbeiter Button Mitarbeiter (Tag 120-Benutzerverwaltung)
        ob_Main_Shared.AddMenuButton(oGrps(3), "MenuExtensionBtnUser", "Produktion-Mitarbeiter", "WinBack Benutzer und Benutzergruppen", My.Resources.MainUser_16x16, My.Resources.MainUser_32x32, AddressOf ShowUserForm, 120)

        'Gruppe Administration (Tag 106-Installation)
        ob_Main_Shared.AddMenuButton(oGrpAdmin, "btnAdmin", "WinBack Administration", "", My.Resources.Admin_16x16, My.Resources.Admin_32x32, AddressOf ShowAdminAdministrationForm, 106)

        'Gruppe Linien (Tag 121-Vnc)
        ob_Main_Shared.AddMenuButton(oGrpLinien, "btnLinien", "Produktions-Linien", "WinBack Produktion Linie 1...", My.Resources.MainLinien_32x32, My.Resources.MainLinien_32x32, AddressOf ShowLinienForm, 121)
        'Gruppe Auswertung (Tag 122-Statistik)
        ob_Main_Shared.AddMenuButton(oGrpChargen, "btnStatistik", "Statistik Produktion", "WinBack Auswertung Produktions-Chargen", My.Resources.MainStatistikChargen_16x16, My.Resources.MainStatistikChargen_32x32, AddressOf ShowStatistikForm, 122)
        'Gruppe Produktions-Planung (Tag 130-Produktionsplanung)
        ob_Main_Shared.AddMenuButton(oGrpPlanung, "btnProdPlan", "WinBack Teig-Herstellung", "", My.Resources.MainProduktionsPlanung_16x16, My.Resources.MainProduktionsPlanung_32x32, AddressOf ShowProduktionsPlanungForm, 130)
        'Gruppe Programm (Tag 140-Programm)
        ob_Main_Shared.AddMenuButton(oGrpProgramm, "btnDashBoard", "WinBack Dashboard", "", My.Resources.MainDashBoard_32x32, My.Resources.MainDashBoard_32x32, AddressOf ShowDashboardForm, 140)

    End Sub

    ''' <summary>
    ''' alle Trace/Debug-Ausgaben werden auch in der Klasse wb_Admin_Shared in einer Text-Liste gespeichert.
    ''' Nach x Zeilen werden die Einträge in ein Text-File gespeichert.
    ''' Die Klasse xLogger (wb_Trace_Listener) leitet die Meldungen weiter.
    ''' </summary>
    Sub AddTraceListener()
        AddHandler xLogger.WriteText, AddressOf wb_Admin_Shared.GetTraceListenerText
        System.Diagnostics.Trace.Listeners.Add(xLogger)
    End Sub

    Public Sub OpenWinBackForm(sender As Object, FormName As String)
        Select Case FormName
            Case "Artikel"
                ShowArtikelForm(sender, Nothing)
            Case "Rohstoffe"
                ShowRohstoffForm(sender, Nothing)
            Case "Rezepte"
                ShowRezeptForm(sender, Nothing)
            Case "Linien"
                ShowLinienForm(sender, Nothing)
            Case "User"
                ShowUserForm(sender, Nothing)
            Case "Statistik"
                ShowStatistikForm(sender, Nothing)
            Case "Admin"
                ShowAdminAdministrationForm(sender, Nothing)
        End Select
    End Sub

    ''' <summary>
    ''' Aufruf WinBack-Fenster Dashboard
    ''' </summary>
    Private Sub ShowMainDashboardForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_Dashboard_Main(ServiceProvider), My.Resources.MainArtikel_16x16)
        'Fensterposition aus winback.ini
        wb_DockBarPanelShared.SetFormBoundaries(xForm, "Dashboard")
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
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S108:Nested blocks of code should not be left empty", Justification:="<Ausstehend>")>
    Private Sub ShowRohstoffForm(sender As Object, e As EventArgs)
        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_Rohstoffe_Main(ServiceProvider), My.Resources.MainRohstoffe_16x16)
        'Fensterposition aus winback.ini
        wb_DockBarPanelShared.SetFormBoundaries(xForm, "Rohstoffe")

        'Aufruf aus der Rezeptverwaltung (Popup Rohstoff)
        Try
            If sender IsNot Nothing AndAlso TryCast(sender, Object).Name = "wb_Rezept_Rezeptur" Then
                'Komponenten-Nummer des aktuellen Rezeptschrittes in der Rezeptur
                Dim KoNr As Integer = TryCast(sender, wb_Rezept_Rezeptur).AktRezeptschritt.RohNr
                'Rohstoff suchen und anzeigen
                wb_Rohstoffe_Shared.Select_Data(sender, KoNr)
            End If
        Catch ex As Exception
        End Try
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
    ''' Aufruf WinBack-Fenster Dashboard
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowDashboardForm(sender As Object, e As EventArgs)

        CloseAllForms()
        xForm = oViewProvider.OpenForm(New wb_Dashboard_Main(ServiceProvider), My.Resources.MainDashBoard_32x32)
        'Fensterposition aus winback.ini
        wb_DockBarPanelShared.SetFormBoundaries(xForm, "DashBoard")
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
        Dim FilialZuordnung As String

        ' aktuelle angemeldeter Mitarbeiter
        Dim sEmployee As String = TryCast(oSetting.GetSetting("Anmeldung.Mitarbeiter"), String)

        If Not String.IsNullOrEmpty(sEmployee) Then
            wb_GlobalSettings.OrgaBackEmployee = sEmployee

            Dim oData As IData = oFactory.GetData
            Using oTable = oData.OpenDataTable(Database.Main, "SELECT Vorname, Nachname, FilialZuordnung FROM Mitarbeiter WHERE MitarbeiterKürzel=@M", LockType.ReadOnly, sEmployee)
                sName = CType(oTable.Rows(0)(0), String) & " " & CType(oTable.Rows(0)(1), String)
                FilialZuordnung = CType(oTable.Rows(0)(2), String)
            End Using

            wb_GlobalSettings.AktUserLogin(sName)
            Dim oTabs = oMenuService.GetTabs
            Dim bProduktion = wb_Filiale.FilialeIstProduktion(FilialZuordnung)

            'Buttons abhängig von den Benutzer-Rechten aus/einblenden
            ob_Main_Shared.CheckMenu(oTabs, bProduktion)
        End If
    End Sub

    ''' <summary>
    ''' Event Bestandskorrektur in OrgaSoft
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub StockCorrectionExecuted(sender As Object, e As IExecutionInfoEventArgs)
        Debug.Print("StockCorrectionExecuted NOT IMPLEMENTED")
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
    ''' Event Änderung der Sprache in OrgaSoft
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LanguageChanged(sender As Object, e As EventArgs)
        Debug.Print("LanguageChanged NOT IMPLEMENTED")
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
    ''' 
    ''' Die Einträge in Settings müssen in ob_Admin_OrgaBackSettings definiert werden!
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
        'Dummy-Artikel 0R9999
        wb_GlobalSettings.ProduktionDummyArtikel = TryCast(oSetting.ReadSetting("AddIn", "WinBack", "Artikelnummer_SonderArtikel", "0R9999"), String)

        'Artikel-Gruppe Backwaren (Default 10) - Integer oder RegEx-Ausdruck der einen Artikelgruppen-Bereich beschreibt
        wb_GlobalSettings.OsGrpBackwaren = TryCast(oSetting.ReadSetting("AddIn", "WinBack", "GruppeBackwaren", ""), String)
        'Artikel-Gruppe Rohstoffe (Default 20) - Integer oder RegEx-Ausdruck der einen Artikelgruppen-Bereich beschreibt
        wb_GlobalSettings.OsGrpRohstoffe = TryCast(oSetting.ReadSetting("AddIn", "WinBack", "GruppeRohstoffe", ""), String)

        'Nährwertberechnung Zutatenliste aus interner Deklaration
        wb_GlobalSettings.NwtInterneDeklaration = (TryCast(oSetting.ReadSetting("AddIn", "WinBack", "InterneDeklaration", ""), String) = "1")
        'Nährwertberechnung Zutatenliste mit/ohne E-Nummern
        wb_GlobalSettings.NwtENummerZutatenListe = (TryCast(oSetting.ReadSetting("AddIn", "WinBack", "ENummernInZutatenListe", ""), String) = "1")
        'Nährwertberechnung Zutatenliste optimieren (doppelte Einträge entfernen...)
        wb_GlobalSettings.NwtOptimizeZutatenListe = (TryCast(oSetting.ReadSetting("AddIn", "WinBack", "OptimierenZutatenListe", ""), String) = "1")
        'Nährwertberechnung "Keine Angabe" bei Allergenen wird ignoriert (Kein Fehler in Rezeptur)
        wb_GlobalSettings.NwtAllergeneNoDefinitionErr = (TryCast(oSetting.ReadSetting("AddIn", "WinBack", "AllergenKeineAngabeError", ""), String) = "1")

        'Nährwerte Cloud - IP-Adresse
        wb_GlobalSettings.WinBackCloud_Url = TryCast(oSetting.ReadSetting("AddIn", "WinBack", "WinBackCloudURL", ""), String)
        'Nährwerte Cloud - Pass
        wb_GlobalSettings.WinBackCloud_Pass = TryCast(oSetting.ReadSetting("AddIn", "WinBack", "WinBackCloudPass", ""), String)
        'Wenn dasCloud-Passwort in den Einstellungen gesetzt wurde, wird der Eintrag beim nächsten Start gelöscht !
        oSetting.WriteSetting("AddIn", "WinBack", "WinBackCloudPass", "")

        'Datenlink Cloud - CAT
        wb_GlobalSettings.Datenlink_CAT = TryCast(oSetting.ReadSetting("AddIn", "WinBack", "datenlink_CT", ""), String)
        'Datenlink Cloud - PAT
        wb_GlobalSettings.Datenlink_PAT = TryCast(oSetting.ReadSetting("AddIn", "WinBack", "datenlink_AT", ""), String)

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
    End Sub

End Class



