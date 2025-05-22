Imports System.Globalization
Imports System.Reflection
Imports System.Threading
Imports System.IO.Compression
Imports System.IO

Public Class OrgaBackOffice
    Private isInitialised As Boolean = False
    Private _LayoutFilename As String = Nothing
    Private _Controls As New Dictionary(Of String, wb_Global.controlSizeandLocation)
    Private _WinBackWindowState As FormWindowState
    Private xLogger As New wb_TraceListener
    Private WithEvents About As New Wb_Main_About
    Private AktForm As Object
    Private AktTab As RibbonTab

    Dim MdiChargen As Chargen_Main
    Dim MdiStammDaten As StammDaten_Main
    Dim MdiArtikel As Artikel_Main
    Dim MdiRezepte As Rezepte_Main
    Dim MdiRohstoffe As Rohstoffe_Main
    Dim MdiUser As User_Main
    Dim MdiLinien As Linien_Main
    Dim MdiPlanung As Planung_Main
    Dim MdiAdmin As Admin_Main
    Dim MdiSchnittstelle As Schnittstelle_Main
    Dim MdiDashboard As Dashboard_Main

#Region "MainMenu"
    ''' <summary>
    ''' Verzweigung Hauptmenu.
    ''' Entsprechend der Haupt-Menu-Tabs werden die einzelnen Main-Forms erzeugt und angezeigt.
    ''' 
    ''' Wenn die Form noch nicht erzeugt wurde, wird in der Prozedur MainFormShow über Activator.CreateInstance() die
    ''' Klasse vom entsprechenen Typ erzeugt. (Entspricht xxx = New yyy)
    ''' 
    ''' Abhängig von den Benutzer-Rechten in WinBack
    '''     
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
    '''     10 = Rezepte ReadOnly
    '''     11 = Rohstoffe ReadOnly
    '''     12 = Rohstoffe Flags ändern
    '''     13 = Rezepte V1 ReadOnly
    '''     14 = Im Rezept nur spezielle Rohstoffe(Flag) ändern
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
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Ribbon_ActiveTabChanged(sender As Object, e As EventArgs) Handles rTab.ActiveTabChanged

        'Programm initialisiert
        If isInitialised Then
            'Prüfen ob der Tab aktiv ist (Benutzer-Rechte)
            If rTab.ActiveTab.Enabled Then
                'Chargen/Statistik
                If rbChargen.Active Then
                    MainFormShow(MdiChargen, GetType(Chargen_Main))
                End If
                'Verwalten Stammdaten
                If rbStammdaten.Active Then
                    MainFormShow(MdiStammDaten, GetType(StammDaten_Main))
                End If
                'Artikelverwaltung
                If rbArtikel.Active Then
                    MainFormShow(MdiArtikel, GetType(Artikel_Main))
                End If
                'Rezeptverwaltung
                If rbRezepte.Active Then
                    MainFormShow(MdiRezepte, GetType(Rezepte_Main))
                End If
                'Rohstoff-Verwaltung
                If rbRohstoffe.Active Then
                    MainFormShow(MdiRohstoffe, GetType(Rohstoffe_Main))
                End If
                'Linien/VNC
                If rbLinien.Active Then
                    MainFormShow(MdiLinien, GetType(Linien_Main))
                End If
                'Benutzerverwaltung
                If rbUser.Active Then
                    MainFormShow(MdiUser, GetType(User_Main))
                End If
                'Produktions-Planung
                If rbPlanung.Active Then
                    MainFormShow(MdiPlanung, GetType(Planung_Main))
                End If
                'Service/Administration
                If rbExtra.Active Then
                    MainFormShow(MdiAdmin, GetType(Admin_Main))
                End If
                'Schnittstelle
                If rbSchnittstelle.Active Then
                    MainFormShow(MdiSchnittstelle, GetType(Schnittstelle_Main))
                End If

                'Info/Test
                If rbAbout.Active Then
                    'alle offenen Fenster schliessen
                    Me.ActiveMdiChild?.Close()
                    'wirklich alle Fenster schliessen !
                    CloseAllForms()

                    If Debugger.IsAttached Then

                        'TEST CLOUD ROHSTOFFE
                        'MainFormShow(MdiRohstoffe, GetType(Rohstoffe_Main))
                        'TEST REZEPTVERWALTUNG
                        'MainFormShow(MdiRezepte, GetType(Rezepte_Main))
                        'TEST USERVERWALTUNG
                        'MainFormShow(MdiUser, GetType(User_Main))
                        'TEST PRODUKTIONSPLANUNG
                        'MainFormShow(MdiPlanung, GetType(Planung_Main))
                    End If
                End If
                'aktuellen RibbonTab merken
                AktTab = rTab.ActiveTab
            Else
                'zurück zum 'alten' Tab
                If AktTab IsNot Nothing Then
                    rTab.ActiveTab = AktTab
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Anzeigen der Hauptmenu-Fenster. Läd die Layout-Namen in das Auswahl-Fenster und setzt das Layout.
    ''' </summary>
    ''' <param name="oForm">Form-Name</param>
    ''' <param name="oType">Form-Type</param>
    Private Sub MainFormShow(ByRef oForm As Form, oType As Type)

        Try
            'Trace.WriteLine("@I_MainFormShow")
            'Prüfen ob die Form schon existiert
            If oForm Is Nothing OrElse oForm.IsDisposed Then
                'wenn nicht - jetzt erzeugen (entspricht New !!)
                oForm = Activator.CreateInstance(oType)
                'Trace.WriteLine("@I_MainFormShow " & oForm.Name)
            End If
        Catch ex As Exception
            Trace.WriteLine("@E_MainFormShow " & ex.Message)
            Exit Sub
        End Try

        'Aktive Form
        AktForm = oForm
        'Aktiver Tab
        AktTab = rTab.ActiveTab

        'wenn die Form im Hintergrund liegt
        If oForm.Visible Then
            'Layout-Name abfragen aus Form
            DkPnlConfigFileName = DirectCast(AktForm, IMainMenu).DkPnlConfigFileName
            'Auswahl-Box mit Layoutnamen laden (Der letzte Layout-Name wird aus der Ini-Datei geladen)
            GetLayoutFileNames(LayoutFilename)
            'nach vorne holen und anzeigen
            'Mdi-Fenster
            oForm.MdiParent = Me
            oForm.Dock = DockStyle.Fill
            oForm.BringToFront()
        Else
            'Layout muss neu geladen werden
            _LayoutFilename = Nothing
            'Auswahl-Box mit Layoutnamen laden (Der letzte Layout-Name wird aus der Ini-Datei geladen)
            GetLayoutFileNames(LayoutFilename)
            'Layout-Filename laden
            AktFormSendCommand("SETDKPNLFILENAME", DkPnlConfigFileName)
            'anzeigen
            'Mdi-Fenster
            oForm.MdiParent = Me
            oForm.Dock = DockStyle.Fill
            oForm.Show()
        End If

    End Sub

    ''' <summary>
    ''' Kommando an die aktive MDI-Form senden. 
    ''' Das Formular muss IMainMenu implementieren.
    ''' </summary>
    ''' <param name="Cmd"></param>
    ''' <param name="Prm"></param>
    ''' <returns></returns>
    Private Function AktFormSendCommand(Cmd As String, Prm As String) As Boolean
        Try
            If AktForm IsNot Nothing Then
                Return DirectCast(AktForm, IMainMenu).ExecuteCmd(Cmd, Prm)
            Else
                Return False
            End If
        Catch
            Return False
        End Try
    End Function
#End Region

    Private Sub WinBack_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Programm und Datei-Pfade einstellen
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack

        'alle Trace / Debug - Ausgaben werden auch in der Klasse wb_Admin_Shared in einer Text-Liste gespeichert.
        'Nach x Zeilen werden die Einträge in ein Text-File gespeichert.
        'Die Klasse xLogger (wb_Trace_Listener) leitet die Meldungen weiter.
        AddHandler xLogger.WriteText, AddressOf wb_Admin_Shared.GetTraceListenerText
        System.Diagnostics.Trace.Listeners.Add(xLogger)
        'Meldung Programm-Start (initialisiert wb_Admin_Shared)
        Trace.WriteLine("Programmstart WinBack-Office")

        'AssemblyResolve wird definiert in WinBackAddIn.Erweiterte Kompilierungsoptionen
        If wb_Global.AssemblyResolve Then
            'Die eigenen dll-Files in sep. Verzeichnis verlagern
            AddHandler System.AppDomain.CurrentDomain.AssemblyResolve, AddressOf MyAssemblyResolve
        End If

        'Fremdsprachen die nicht installiert sind ausblenden
        CheckLanguageResources()

        'Programm-Parameter auslesen
        wb_Functions.ProcessParameter()

    End Sub

    Private Function MyAssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
        Return wb_Main_Shared.MyAssemblyResolve(sender, args, GetType(OrgaBackOffice).Assembly)
    End Function

    ''' <summary>
    ''' Fenster ist angezeigt - Wenn noch kein Benutzer angemeldet ist (Start-Parameter) wird das Login-Fenster angezeigt.
    ''' Je nach Benutzer-Rechten und Programm-Konfiguration werden die entsprechenden Tabs eingeblendet.
    ''' 
    ''' Die Benutzer-Rechte stehen in der Tabelle winback.ItemParameter(GruppenNummer)
    ''' Die Programm-Konfiguration in der Tabelle winback.ItemParameter(GruppenNummer = -1)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub WinBack_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Sofort mit maximiertem Fenster starten
        Me.WindowState = FormWindowState.Maximized

        'First-Run nach Setup oder Update
        If WinBackFirstRun() AndAlso CheckMySqlAndIni() Then
            'Wenn noch kein Benutzer angemeldet ist (Parameter -u)
            If wb_AktUser.UserNr < 0 Then
                ShowLogin()
            Else
                wb_Language.LoadTexteTabelle(wb_Language.GetLanguageNr())
            End If

            'Menu-Buttons je nach Benutzer-Rechten aktivieren/deaktivieren
            wb_AktUser.SetUserRechte(Me)
            'Statusbar aktualisieren (User/Mandant/Version)
            SetStatusBarInfo()

            'externe Programme prüfen (TeamViewer/Putty..)
            CheckExternalPrograms()

            'Start mit Info-Tab
            rTab.ActiveTab = rbAbout
            'Meldung Programm-Start (initialisiert wb_Admin_Shared)
            Trace.WriteLine("Programmstart OrgaBack-Office")

            'Event-Handler Aufruf einer WinBack-Main-Form
            AddHandler wb_Main_Shared.eOpenForm, AddressOf OpenWinBackForm

            'Initialisierung beendet
            isInitialised = True

            'TESTTESTTEST
            'Dim Test As New Test_Main
            'MainFormShow(Test, GetType(Test_Main))

            'Dashboard anzeigen
            If wb_GlobalSettings.ShowDashboard Then
                MainFormShow(MdiDashboard, GetType(Dashboard_Main))
            End If
        Else
            'FirstRun ist fehlgeschlagen - Programm beenden
            ExitMainForm()
        End If
    End Sub

    ''' <summary>
    ''' Prüfen ob externe Programme auf diesem Rechner installiert sind
    '''     Teamviewer
    '''     Putty
    ''' </summary>
    Private Sub CheckExternalPrograms()
        'Prüfen ob TeamViewer installiert ist
        rbRunTeamViewer.Enabled = wb_Functions.CheckExternalProgram(wb_Global.TeamViewerExe)
        'Prüfen ob Putty installiert ist
        rbPutty.Enabled = wb_Functions.CheckExternalProgram(wb_Global.PuttyExe)
    End Sub

    ''' <summary>
    ''' Aktualisiert die Texte in der Status-Zeile
    ''' User/Mandant - IP-Adresse - Programm-Version
    ''' </summary>
    Private Sub SetStatusBarInfo()
        tslblName.Text = wb_GlobalSettings.AktUserName & "/" & wb_GlobalSettings.MandantName
        tslblIP.Text = "(" & wb_GlobalSettings.MySQLServerIP & ")"
        tslblVersion.Text = "Version " & wb_GlobalSettings.WinBackVersion
    End Sub

    Private Function CheckMySqlAndIni() As Boolean
        'Prüfen ob die winback.ini vorhanden ist
        Dim TestWinBackIni As New wb_IniFile With {
            .SilentMode = True
        }
        If TestWinBackIni.TestIniPfadExists("winback", "eMySQLServerIP") Then

            'Prüfen ob der WinBack-Server ereichbar ist
            If wb_sql_Functions.ping Then
                'Sprache und Versions-Nummer in Status-Bar anzeigen
                ShowStatusBar()

                'Programm-Fenster maximieren
                Me.WindowState = FormWindowState.Maximized
            Else
                'Fehlermeldung ausgeben
                MsgBox("Keine Verbindung zum WinBack-Server (" & wb_GlobalSettings.IPBasisAdresse & ")" & vbCrLf & "Das Programm wird beendet", MsgBoxStyle.Critical, "OrgaBack-Produktion Start")
                'Programm beenden
                Return False
            End If
        Else
            'Fehlermeldung ausgeben
            MsgBox("Die WinBack.ini-Datei konnte nicht gefunden werden" & vbCrLf & "Das Programm wird beendet", MsgBoxStyle.Critical, "OrgaBack-Produktion Start")
            'Programm beenden
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Öffnet eine Main-Form entsprechend dem übergebenen Form-Namen.
    ''' Wird aufgerufen über den Event eOpenForm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="FormName"></param>
    Public Sub OpenWinBackForm(sender As Object, FormName As String)
        Select Case FormName
            Case "Artikel"
                rTab.ActiveTab = rbArtikel
            Case "Rohstoffe"
                rTab.ActiveTab = rbRohstoffe
            Case "Rezepte"
                rTab.ActiveTab = rbRezepte
            Case "Linien"
                rTab.ActiveTab = rbLinien
            Case "User"
                rTab.ActiveTab = rbUser
            Case "Statistik"
                rTab.ActiveTab = rbChargen
            Case "Admin"
                rTab.ActiveTab = rbExtra
        End Select
    End Sub

    ''' <summary>
    ''' Benutzer wechseln - Anmelden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbAbmelden_Click(sender As Object, e As EventArgs) Handles rbAbmelden.Click, rbLogin.Click
        'alle offenen Fenster schliessen
        CloseAllForms()
        'Login-Fenster
        ShowLogin()
        wb_AktUser.SetUserRechte(Me)
        'externe Programme prüfen (Buttons deaktivieren)
        CheckExternalPrograms()
        'Statusbar aktualisieren (User/Mandant/Version)
        SetStatusBarInfo()
    End Sub

    ''' <summary>
    ''' Programm Ende
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbEnde_Click(sender As Object, e As EventArgs) Handles rbEnde.Click, rbClose.Click
        ExitMainForm()
    End Sub

    Private Sub ExitMainForm()
        RemoveHandler wb_Main_Shared.eOpenForm, AddressOf OpenWinBackForm
        Close()
    End Sub

    ''' <summary>
    ''' Anmelde-Fenster modal anzeigen
    ''' </summary>
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
        lblNetworkIP.Text = wb_GlobalSettings.WinBackDBType.ToString & " " & wb_GlobalSettings.MySQLServerIP & " " & wb_GlobalSettings.MandantName

        'Status-Bar - Länderflagge anzeigen
        Dim AktiveSprache As Integer = wb_Language.GetLanguageNr()
        lblLanguage.Image = LanguageFlags.Images(AktiveSprache)
        lblLanguage.Text = wb_GlobalSettings.AktUserName
    End Sub

    ''' <summary>
    ''' Wir beim Programm-Start noch VOR den Login aufgerufen. Die Prozess-Parameter sind schon ausgewertet.
    ''' 
    ''' Prüft ob nach Neu-Installation oder Update noch weitere Anpassungen notwendig sind.
    '''     Auspacken von zip-Files (Update)
    '''     Eintragen der WinBack-IP-adressen (Neu-Installation)
    ''' </summary>
    Private Function WinBackFirstRun() As Boolean

        'Redistributables für List&Label
        WinBackUpdateCheckZip(wb_GlobalSettings.pRedistPath, wb_Global.FileUpdateMode.UpdateAlways)
        'Vorlagen für List&Label - vorhandene Dateien werden nicht überschrieben
        WinBackUpdateCheckZip(wb_GlobalSettings.pListenPath, wb_Global.FileUpdateMode.UpdateNever)
        'Datenbank-Updates
        WinBackUpdateCheckZip(wb_GlobalSettings.pDBUpdatePath, wb_Global.FileUpdateMode.UpdateNever)

        'IP-Adresse aus der WinBack.ini lesen
        If wb_GlobalSettings.MySQLServerIP = "" OrElse wb_GlobalSettings.MySQLServerIP = "NotDefined" Then
            'Die WinBack-MySQL-ServerIP ist nicht definiert - Erster Start nach Setup-Installation
            Dim SetIP As New SetMySqlIP
            If SetIP.ShowDialog() = DialogResult.OK Then
                wb_GlobalSettings.MySQLMasterServerIP = SetIP.MySQLServerIP
                MsgBox("Die Konfiguration wurde erfolgreich beendet" & vbCrLf & "OrgaBack-Produktion wird gestartet", MsgBoxStyle.Information)
                Return True
            Else
                MsgBox("Die Konfiguration wurde nicht abgeschlossen" & vbCrLf & "OrgaBack-Produktion wird beendet", MsgBoxStyle.Exclamation)
                Return False
            End If
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Beim Update über WinBack-Setup.msi bleiben die einzelnen Verzeichnisse erhalten. Es werden nur die explizit im Setup
    ''' angegebenen Dateien ersetzt.
    ''' Damit ist sichergestellt, dass kundenspezifische Änderungen z.B. in den Formular-Vorlagen (ListUndLabel) nicht überschrieben
    ''' werden. Nach einem Update sind in den Verzeichnissen die aktuellen (evtl. neuen) Dateien als .zip-File vom Setup-Programm
    ''' abgelegt worden.
    ''' Das zip-File wird ausgepackt und für jede Datei wird verglichen/entschieden, welche Version behalten werden soll.
    ''' </summary>
    ''' <param name="Dir"></param>
    Private Sub WinBackUpdateCheckZip(Dir As String, Mode As wb_Global.FileUpdateMode, Optional Filter As String = "*.*")
        'Prüfen ob das Verzeichnis exisitiert
        If IO.Directory.Exists(Dir) Then
            'Verzeichnis in dem gesucht wird
            Dim ZipDir As New IO.DirectoryInfo(Dir)
            'Verzeichnis-Name (wird der Unter-Ordner im Temp-Verzeichnis)
            Dim ZipTmp As String = ZipDir.Name
            'Prüfen ob ein zip-File im Verzeichnis exisitiert
            If IO.Directory.Exists(ZipDir.FullName) Then
                Dim ZFiles As IO.FileInfo() = ZipDir.GetFiles("*.zip", IO.SearchOption.TopDirectoryOnly)
                'Wenn Zip-File gefunden
                For Each ZFile In ZFiles
                    'eventuell vorhandene Rest-Fragmente vorher löschen
                    If IO.Directory.Exists(wb_GlobalSettings.pWindowsTempPath & ZipTmp) Then
                        IO.Directory.Delete(wb_GlobalSettings.pWindowsTempPath & ZipTmp, True)
                    End If
                    'zip-File im Windows-Temp-Verzeichnis auspacken
                    Dim TmpDir As New System.IO.DirectoryInfo(wb_GlobalSettings.pWindowsTempPath & ZipTmp)
                    ZipFile.ExtractToDirectory(ZFile.FullName, TmpDir.FullName)

                    'alle Dateien im Windows-Temp-XXX-Verzeichnis
                    Dim TmpFiles As System.IO.FileInfo() = TmpDir.GetFiles(Filter, IO.SearchOption.AllDirectories)
                    For Each TFile In TmpFiles
                        CopyIfAllowed(Dir, TmpDir, TFile, Mode)
                    Next
                    'Zip-Datei löschen (sonst wird beim nächsten Start wieder alles ausgepackt)
#If Not DEBUG Then
            IO.File.Delete(ZFile.FullName)
#End If
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Kopiert eine Datei aus dem TmpDir(ausgepacktes zip-File) in das entsprechende Zielverzeichnis.
    ''' Je nach Modus (immer/nie/neuer/bak) wird die Datei im Zielverzeichnis überschrieben.
    ''' 
    ''' Damit wird sichergestellt, dass Änderungen, die kundeseitig vorgenommen worden sind, nicht 
    ''' überschrieben werden.
    ''' 
    ''' Im Fall von Datenbank-Updates werden die .sql-Files aus dem zip-File nur dann kopiert, wenn
    ''' im Zielverzeichnis nicht schon eine Datei mit der Endung .sql.bak exisitiert.
    ''' </summary>
    ''' <param name="Dir"></param>
    ''' <param name="TmpDir"></param>
    ''' <param name="TFile"></param>
    ''' <param name="mode"></param>
    ''' <returns></returns>
    Private Function CopyIfAllowed(Dir As String, TmpDir As IO.DirectoryInfo, TFile As IO.FileInfo, mode As wb_Global.FileUpdateMode) As Boolean
        'Flag Datei überschreiben
        Dim Result As Boolean = True

        'Zielverzeichnis aus TFile ermitteln
        Dim NewFolderName As String = TFile.DirectoryName.Replace(TmpDir.FullName, Dir)
        'Prüfen, ob das Unterverzeichnis überhaupt existiert - Wenn nicht, wird es angelegt
        wb_Functions.FolderExists(NewFolderName, True)

        'Ziel-Datei
        Dim CmpFileName As String = Dir & Mid(TFile.FullName, TmpDir.FullName.Length + 2)
        'prüfen ob die Datei im Zielverzeichnis existiert
        If IO.File.Exists(CmpFileName) OrElse IO.File.Exists(CmpFileName & ".bak") Then

            'abhängig vom Modus
            Select Case mode
                Case wb_Global.FileUpdateMode.UpdateAlways
                    'immer überschreiben
                    Result = True
                Case wb_Global.FileUpdateMode.UpdateIfNewer
                    'Diese Datei existiert schon im Zielverzeichnis - Datei-Info(Zeitstempel) ermitteln
                    Result = CheckCreationTime(CmpFileName, TFile.CreationTime)
                Case wb_Global.FileUpdateMode.UpdateNever
                    'nie überschreiben
                    Result = False
                Case wb_Global.FileUpdateMode.UpdateBak
                    'Diese Datei(.bak) existiert schon im Zielverzeichnis - Datei-Info(Zeitstempel) ermitteln
                    Result = CheckCreationTime(CmpFileName & ".bak", TFile.CreationTime)
            End Select

        End If

        'Datei kopieren
        If Result Then
            'Datei kopieren (überschreiben ja/nein)
            IO.File.Copy(TFile.FullName, CmpFileName, Result)
        End If
        Return Result
    End Function

    Private Function CheckCreationTime(FileName As String, CmpDate As Date) As Boolean
        Dim CmpFile As New IO.FileInfo(FileName)
        'vergleichen mit den Dateien im Original-Verzeichnis
        If (CmpFile.CreationTime < CmpDate) Then
            Return True
        Else
            Return False
        End If
    End Function

#Region "Sprachumschaltung"
    ''' <summary>
    ''' Ribbon-Buttons Sprachumschaltung. Sammelt alle Click-Events aller Buttons und schaltet auf die 
    ''' entsprechende Sprache um.
    ''' 
    ''' Die Information, auf welche Sprache umgeschaltet werden soll, steht in RibbonButton.Value des
    ''' jeweiligen Buttons. (Im Hauptmenu). Die Nummern entsprechen der Numeririerung in WinBack-Produktion.
    ''' 
    '''     de-DE   0   Deutsch
    '''     hu-HU   1   Ungarisch
    '''     nl-NL   2   Niederländisch
    '''     en-US   3   Englisch(US)
    '''     pt-PT   4   Portugisisch
    '''     sl-SL   5   Slovenisch
    '''     ru-RU   6   Russisch
    '''     fr-FR   7   Französisch
    '''     sk-SK   8   Slovakisch
    '''     ro-RO   9   Rumänisch
    '''     
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbSprache_Click(sender As Object, e As EventArgs) Handles rbSL.Click, rbSK.Click, rbRU.Click, rbRO.Click, rbPT.Click, rbNL.Click, rbHU.Click, rbFR.Click, rbES.Click, rbEN.Click, rbDE.Click
        Dim Language As String = DirectCast(sender, RibbonButton).Value
        wb_Language.SetLanguage(Language)
        ChangeLanguage()
    End Sub

    ''' <summary>
    ''' Prüft, welche Sprachen installiert sind und blendet nicht installierte Sprachen aus.
    ''' die Resourcen-Files liegen unterhalb des Programm-Verzeichnis in en,ru,sl...
    ''' </summary>
    Private Sub CheckLanguageResources()
        For Each c As RibbonItem In rTab.OrbDropDown.RecentItems
            If IO.Directory.Exists(wb_GlobalSettings.pProgrammPath & c.Name.Substring(2)) Then
                c.Enabled = True
            Else
                c.Enabled = False
            End If
        Next
    End Sub

    Private Sub ChangeLanguage()
        'Prüfen ob die Sprache geändert werden muss
        Dim CurrentLanguage As String = Thread.CurrentThread.CurrentUICulture.Name
        'TODO Sprachumschaltung ist fehlerhaft - Muss geprüft werden !
        If CurrentLanguage <> wb_Language.GetLanguage AndAlso False Then

            'alle offenen Fenster schliessen
            CloseAllForms()
            'Verarbeitung sperren
            isInitialised = False

            'Umschaltung aktive Sprache
            Thread.CurrentThread.CurrentUICulture = New CultureInfo(wb_Language.GetLanguage)
            Thread.CurrentThread.CurrentCulture = New CultureInfo(wb_Language.GetLanguage)

            'Größe und Position aller Controls merken und Controls entfernen
            RemoveSaveControls()
            'Control neu initialisieren
            InitializeComponent()
            'Größe und Position alle Controls wiederherstellen
            ResizeControls()

            'Verarbeitung wieder freigeben
            isInitialised = True
        End If

        'Sprache und Versions-Nummer in Status-Bar anzeigen
        ShowStatusBar()
    End Sub

    ''' <summary>
    ''' Speichert die Größe und Position aller Controls. Danach werden alle Controls gelöscht.
    ''' Größe und Position des Main-Windows werden unter WinBack im Dictionary gespeichert.
    ''' </summary>
    Private Sub RemoveSaveControls()
        Dim clp As wb_Global.controlSizeandLocation
        'Liste löschen (Dictonary)
        _Controls.Clear()

        'Liste aller Controls mit Name und Location
        For Each ctl As Control In Me.Controls
            clp = New wb_Global.controlSizeandLocation With {
                .cLocation = ctl.Location,
                .cSize = ctl.Size
            }
            _Controls.Add(ctl.Name, clp)
        Next

        'alle Controls aus der Liste löschen
        For Each c As String In _Controls.Keys
            Me.Controls.RemoveByKey(c)
        Next

        'Main Window
        clp = New wb_Global.controlSizeandLocation With {
            .cLocation = Me.Location,
            .cSize = Me.Size
        }
        _Controls.Add("WinBack", clp)
        _WinBackWindowState = Me.WindowState
    End Sub

    ''' <summary>
    ''' Stellt Größe und Position aller Controls nach dem Neuzeichenen durch InitializeComponent
    ''' wieder her. Zunächst wird das Main-Window (WinBack) wiederhergestellt.
    ''' 
    ''' Die Daten müssen zuvor mit RemoveSaveControls im Dictionary gepeichert werden.
    ''' </summary>
    Private Sub ResizeControls()
        Dim clp As wb_Global.controlSizeandLocation
        'Main Window
        If _Controls.TryGetValue("WinBack", clp) Then
            Me.Size = clp.cSize
            Me.Location = clp.cLocation
            Me.WindowState = _WinBackWindowState
        End If
        'Resize all Controls
        For Each ctl As Control In Me.Controls
            If _Controls.TryGetValue(ctl.Name, clp) Then
                ctl.Size = clp.cSize
                ctl.Location = clp.cLocation
            End If
        Next
    End Sub

    ''' <summary>
    ''' Alle geöffneten Formulare schliessen (Umschaltung Sprache)
    ''' innerhalb der Schleife muss nochmals geprüft werden, ob sich der Wert für OpenForms.Count geändert hat,
    ''' da Docking-Fenster, die ausserhalb der Main-Form geöffnet werden (freie Fenster) mitgezählt werden. Diese
    ''' werden beim Schliessen der Hauptform mit geschlossen.
    ''' </summary>
    Private Sub CloseAllForms()
        Dim iOpenForms As Integer = System.Windows.Forms.Application.OpenForms.Count - 1
        For i = iOpenForms To 1 Step -1
            If i < System.Windows.Forms.Application.OpenForms.Count Then
                Dim oForm As Form = Application.OpenForms(i)
                If oForm.Name <> "WinBack" Then
                    oForm.Parent = Nothing
                    oForm.Close()
                    oForm.Dispose()
                    oForm = Nothing
                End If
            End If
        Next i
    End Sub
#End Region
#Region "DockBarPanel"
    ''' <summary>
    ''' Erzeugt den File-Namen für die Konfig-Datei aus Layout-File-Name und Fom-Name.
    ''' Ohne Angaben wird der lokale Pfad zurückgegeben (..\Temp\xx, wobei xx die Arbeitsplatz-Nummer ist).
    ''' Optional der Globale-Pfad (..\Temp\00)
    ''' </summary>
    ''' <param name="DefaultPath"></param>
    ''' <returns></returns>
    Public Property DkPnlConfigFileName(Optional DefaultPath As wb_Global.OrgaBackDockPanelLayoutPath = wb_Global.OrgaBackDockPanelLayoutPath.UserLokal) As String
        Get
            Return wb_GlobalSettings.DockPanelPath(DefaultPath) & "wb" & FormName & LayoutFilename & ".xml"
        End Get
        Set(value As String)
            'LayoutFilename setzen
            LayoutFilename = wb_Functions.DkPnlConfigName(value, FormName)
        End Set
    End Property

    ''' <summary>
    ''' Die Information kommt aus der winback.ini und wird in der Routine wb_DockBarPanelShared.SetFormBoundaries ausgelesen (in wb_Main_Menu)
    ''' </summary>
    ''' <returns>String - Layout-Filename</returns>
    Public Property LayoutFilename As String
        Get
            If _LayoutFilename Is Nothing Then
                'Dock-Panel-Layout Filename aus winback.ini
                Dim IniFile As New Global.WinBack.wb_IniFile
                _LayoutFilename = IniFile.ReadString(FormName, "LayoutFileName", "Default")
                'Dispose
                IniFile = Nothing
            End If
            Return _LayoutFilename
        End Get
        Set(value As String)
            'neuen Wert setzen
            _LayoutFilename = value
            'Das Default-Layout kann nicht gelöscht werden
            BtnDelete.Enabled = Not (_LayoutFilename = "Default")
            Try
                'Wenn dieses Layout im Arbeitsplatz-Ordner nicht vorhanden ist
                If Not IO.File.Exists(DkPnlConfigFileName) Then
                    'vom Default-Ordner kopieren
                    If IO.File.Exists(DkPnlConfigFileName(wb_Global.OrgaBackDockPanelLayoutPath.ProgrammGlobal)) Then
                        System.IO.File.Copy(DkPnlConfigFileName(wb_Global.OrgaBackDockPanelLayoutPath.ProgrammGlobal), DkPnlConfigFileName)
                    End If
                End If
            Catch ex As Exception
                Debug.Print("@E_Fehler beim Laden des Layouts " & _LayoutFilename)
            End Try
        End Set
    End Property

    ''' <summary>
    ''' Form-Name ermitteln aus dem TEXT der aktuellen(aktiven) Form
    ''' (Der Layout-Filename wird gebildet aus Form-Name und Layout-Name)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FormName As String
        Get
            If AktForm IsNot Nothing Then
                Return AktForm.Text
            Else
                Return ""
            End If
        End Get
    End Property

    ''' <summary>
    ''' Füllt die ListBox cbLayouts mit den Layout-Bezeichnungen. Die Bezeichnungen werden aus den FormNamen
    ''' gebildet.
    ''' </summary>
    Private Sub GetLayoutFileNames(LayoutFilename As String)
        'Liste alle Konfigurations-Dateien im Verzeichnis
        Dim ConfigFileNames As New List(Of String)
        'Anzeige ausschalten, wegen Geschwindigkeit
        cbLayouts.Visible = False
        cbLayouts.Items.Clear()

        'Globales Verzeichnis ..\Temp\00
        ConfigFileNames = wb_Functions.GetDkPnlConfigNameList(wb_GlobalSettings.DockPanelPath(wb_Global.OrgaBackDockPanelLayoutPath.ProgrammGlobal), FormName)
        For Each x In ConfigFileNames
            'aktueller Layout-Filename
            If LayoutFilename = x Then
                cbLayouts.Text = x
            End If
            cbLayouts.Items.Add(x)
        Next

        'Arbeitsplatz Verzeichnis ..\Temp\xx
        ConfigFileNames = wb_Functions.GetDkPnlConfigNameList(wb_GlobalSettings.DockPanelPath(wb_Global.OrgaBackDockPanelLayoutPath.UserLokal), FormName)
        For Each x In ConfigFileNames
            'nur noch neue Einträge hinzufügen
            If cbLayouts.FindStringExact(x) = ListBox.NoMatches Then
                'aktueller Layout-Filename
                If LayoutFilename = x Then
                    cbLayouts.Text = x
                End If
                cbLayouts.Items.Add(x)
            End If
        Next

        'Sortieren
        cbLayouts.Sorted = True
        'und wieder anzeigen
        cbLayouts.Visible = True
    End Sub

    ''' <summary>
    ''' Layout-Auswahl-Box geändert. 
    ''' Es wurde ein neues Layout ausgewählt. Aus dem Layout-Namen und dem Form-Namen wird der DockPanelConfig-Filename gebildet und das
    ''' entsprechende Layout geladen.
    ''' Das Laden des Layout passiert in der aktiven Form über den Aufruf SendCommand(RUNDKPNLFILENAME)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cbLayouts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLayouts.SelectedIndexChanged
        If LayoutFilename <> cbLayouts.Text Then
            LayoutFilename = cbLayouts.Text
            AktFormSendCommand("RUNDKPNLFILENAME", DkPnlConfigFileName)
        End If
    End Sub

    ''' <summary>
    ''' Button "Reload". Layout neu aus Datei laden.
    ''' Das Laden des Layout passiert in der aktiven Form über den Aufruf SendCommand(RUNDKPNLFILENAME)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnReload_Click(sender As Object, e As EventArgs) Handles BtnReload.Click
        LayoutFilename = cbLayouts.Text
        AktFormSendCommand("RUNDKPNLFILENAME", DkPnlConfigFileName)
    End Sub

    ''' <summary>
    ''' Button "Save". Das Layout wird unter dem aktuellen Namen lokal gespeichert.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        'Layout wird lokal gespeichert
        If AktFormSendCommand("SAVEDKPNLFILENAME", DkPnlConfigFileName) Then
            'Meldung ausgeben
            MessageBox.Show("Layout " & LayoutFilename & " gesichert", "Layout sichern", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ''' <summary>
    ''' Button "Save As". Öffnet das Fenster DockPanelConfigSaveAs. Auswahl des Layout-Namens
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnSaveAs_Click(sender As Object, e As EventArgs) Handles BtnSaveAs.Click
        Dim DkpPnlConfigSaveAs As New wb_DockBarPanelSaveAs(FormName)
        AddHandler DkpPnlConfigSaveAs.eSaveAs_Click, AddressOf ESaveAs_Click
        DkpPnlConfigSaveAs.ShowDialog(Me)
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        'Sicherheits-Abfrage
        If MessageBox.Show("Soll das Layout " & LayoutFilename & " wirklich gelöscht werden ",
                           "Layout löschen", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            'aus der Auswahl-Liste entfernen
            cbLayouts.Items.Remove(cbLayouts.SelectedItem)
            'Layout-File wird lokal gelöscht
            System.IO.File.Delete(DkPnlConfigFileName(wb_Global.OrgaBackDockPanelLayoutPath.UserLokal))
            'Layout-File wird global gelöscht
            System.IO.File.Delete(DkPnlConfigFileName(wb_Global.OrgaBackDockPanelLayoutPath.ProgrammGlobal))

            'Default-Layout laden
            LayoutFilename = "Default"
            cbLayouts.Text = LayoutFilename
            BtnReload_Click(sender, e)
        End If

    End Sub

    ''' <summary>
    ''' Speichert das Layout unter dem angegebene  Namen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="FileName"></param>
    ''' <param name="DefaultPath"></param>
    Private Sub ESaveAs_Click(sender As Object, FileName As String, DefaultPath As wb_Global.OrgaBackDockPanelLayoutPath)
        'aktuelles Layout unter dem neuen Namen abspeichern
        _LayoutFilename = FileName
        'Layout-Files in Status-Bar Listbox aktualisieren/einlesen
        GetLayoutFileNames(FileName)
        'Layout sichern
        AktFormSendCommand("SAVEDKPNLFILENAME", DkPnlConfigFileName)
    End Sub

#End Region

    ''' <summary>
    ''' Button Ansicht-Liste
    ''' Ruft das Detail-Fenster der aktuell angezeigten MDI-Form auf. Über ExecuteCmd wird der entsprechenden
    ''' Form das Kommando übertragen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbListe_Click(sender As Object, e As EventArgs) Handles rbArtikelListe.Click, rbRohstoffeListe.Click, rbRezeptListe.Click, rbListe.Click, rbChargenListe.Click, rbDatensicherung.Click, rbProduktionPlanung.Click, RibbonButton4.Click, rbLinienListe.Click
        AktFormSendCommand("OPENLISTE", "")
    End Sub

    ''' <summary>
    ''' Button Ansicht-Details und Button Bearbeiten
    ''' Ruft das Detail-Fenster der aktuell angezeigten MDI-Form auf. Über ExecuteCmd wird der entsprechenden
    ''' Form das Kommando übertragen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbDetails_Click(sender As Object, e As EventArgs) Handles rbArtikelDetails.Click, rbArtikelBearbeiten.Click,
                                                                          rbUserDetails.Click, rbUserBearbeiten.Click,
                                                                          rbRohstoffeDetails.Click, rbRohstoffeBearbeiten.Click,
                                                                          rbUserDetails.Click, rbUserBearbeiten.Click, rbRezeptBearbeiten.Click, rbRezeptDetails.Click, rbFormat.Click, rbLinienDetails.Click
        AktFormSendCommand("OPENDETAILS", "")
    End Sub

    ''' <summary>
    ''' Button SendCommand
    ''' Sendet ein Kommando an die aktuelle MDI-Form. Der Kommando-String steht in Btn.Value. Über ExecuteCmd wird der entsprechenden
    ''' Form das Kommando übertragen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbCommand_Click(sender As Object, e As EventArgs) Handles rbLinienAdd.Click, rbLinienDel.Click, rbLinienDrucken.Click, rbLinienAuto.Click,
        rbStatRohstoffe.Click, rbRohstoffeNeu.Click, rbRohstoffeLöschen.Click, rbRohstoffeParameter.Click, rbRohstoffeVerwendung.Click, rbRohstoffeLieferungen.Click,
        rbStatRohstoffeDetail.Click, rbRohstoffNwt.Click, rbRohstoffSilos.Click, rbRohstoffeImportCloud.Click, rbRohstoffeDruck.Click,
        rbStatRezepte.Click, rbRezeptNeu.Click, rbRezeptHistorie.Click, rbRezeptHinweis.Click, rbRezeptVerwendung.Click, rbRezeptDruckenStammblatt.Click, rbRezeptDruckenListe.Click,
        rbUserNeu.Click, rbUserRemove.Click, rbUserRechte.Click, rbUserChangePass.Click, rbUserDrucken.Click, rbUserGruppenRechte.Click,
        rbAdminWinBack.Click, rbAdminUpdate.Click, rbAdminWinBackIni.Click, rbLog.Click, rbCheckDataBase.Click, rbAdminMasterWinBackIni.Click, rbAdminXNumber.Click,
        rbArtikelHinweise.Click, rbArtikelParameter.Click, rbArtikelRemove.Click, rbArtikelNeu.Click, rbPrintArtikelListe.Click, rbArtikelDeklaration.Click,
        rbVarianten.Click, rbStoffe.Click, rbLinienGruppen.Click, rbGruppen.Click, rbRezeptGruppen.Click, rbImport.Click, rbExport.Click, rbSetup.Click,
        rbProduktionTeiler.Click, rbFehlerListe.Click, rbDrucken.Click, RibbonButton5.Click,
        rbKonfiguration.Click, rbPrintArtikelStammblatt.Click, rbPrintArtikelProduktInfo.Click, rbArtikelTextHinweise.Click, rbTextHinweise.Click, rbVorschau.Click
        Dim Cmd As String = DirectCast(sender, RibbonButton).Value
        If Cmd <> "" Then
            AktFormSendCommand(Cmd, "")
        End If
    End Sub

    ''' <summary>
    ''' DropDown Auswahl-Liste(Filter)
    ''' Sendet das Kommando SETFILTER an die aktuelle MDI-Form. Die aktuelle Auswahl(Index) wird als Parameter übergeben.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbDropDownItem_Click(sender As Object, e As RibbonItemEventArgs) Handles cbRohstoffeAnsicht.DropDownItemClicked
        AktFormSendCommand("SETFILTER", e.Item.Value)
    End Sub

    Private Sub rbVersionInfo_Click(sender As Object, e As EventArgs) Handles rbVersionInfo.Click, rbInfo.Click
        About.ShowDialog()
    End Sub

    Private Sub rbDashboard_Click(sender As Object, e As EventArgs) Handles rbDashboard.Click
        MainFormShow(MdiDashboard, GetType(Dashboard_Main))
    End Sub

    ''' <summary>
    ''' Teamviewer starten
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbRunTeamViewer_Click(sender As Object, e As EventArgs) Handles rbRunTeamViewer.Click
        If Not wb_Functions.RunExternalProgramm(wb_Global.TeamViewerExe) Then
            MsgBox("Fehler beim Starten von TeamViewer", MsgBoxStyle.Exclamation, "WinBack-Fernwartung")
        End If
    End Sub

    ''' <summary>
    ''' Putty starten
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbPutty_Click(sender As Object, e As EventArgs) Handles rbPutty.Click
        If Not wb_Functions.RunExternalProgramm(wb_Global.PuttyExe) Then
            MsgBox("Fehler beim Starten von Putty", MsgBoxStyle.Exclamation, "WinBack-Server Terminal")
        End If
    End Sub

    'Private Sub LoadColorTable(FileName As String) Handles About.Ab_LoadColorTable

    '    If RibbonTheme.IsDefined(GetType(RibbonTheme), FileName) Then
    '        rTab.ThemeColor = DirectCast([Enum].Parse(GetType(RibbonTheme), FileName), RibbonTheme)
    '    ElseIf FileName = "SaveAsDefault" Then
    '        SaveColorTable()
    '    Else
    '        Dim content As String = System.IO.File.ReadAllText(wb_GlobalSettings.pColorThemePath)
    '        TryCast(rTab.Renderer, RibbonProfessionalRenderer).ColorTable.ReadThemeIniFile(content)
    '        'alte Version - Fehler BC40000 depricated
    '        'Theme.ColorTable.ReadThemeIniFile(content)
    '    End If

    '    'Ribbon Tab - Farben updaten
    '    rTab.Refresh()
    '    Me.Refresh()
    '    Me.BackColor = Color.FromArgb(226, 225, 227)
    'End Sub

    Private Sub SaveColorTable()
        Dim ct = DirectCast(rTab.Renderer, RibbonProfessionalRenderer).ColorTable
        ct.ThemeName = "WinBack"
        ct.ThemeAuthor = "JWill"
        ct.ThemeAuthorEmail = "jw@winback.de"
        ct.ThemeAuthorWebsite = "www.winback.de"
        ct.ThemeDateCreated = DateTime.Now()
        Dim content As String = ct.WriteThemeIniFile()
        System.IO.File.WriteAllText(wb_GlobalSettings.pColorThemePath, content)
    End Sub

    Private Sub WinBack_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MdiDashboard?.Close()
    End Sub

    Private Sub rTab_Click(sender As Object, e As EventArgs) Handles rTab.Click

    End Sub
End Class