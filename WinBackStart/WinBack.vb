Imports System.Globalization
Imports System.Threading

Public Class WinBack
    Private isInitialised As Boolean = False
    Private _LayoutFilename As String = Nothing

    Private AktForm As Object
    Dim MdiChargen As Chargen_Main
    Dim MdiArtikel As Artikel_Main
    Dim MdiRezepte As Rezepte_Main
    Dim MdiRohstoffe As Rohstoffe_Main
    Dim MdiUser As User_Main
    Dim MdiLinien As Linien_Main
    Dim MdiPlanung As Planung_Main
    Dim MdiAdmin As Admin_Main

#Region "MainMenu"
    ''' <summary>
    ''' Verzweigung Hauptmenu.
    ''' Entsprechend der Haupt-Menu-Tabs werden die einzelnen Main-Forms erzeugt und angezeigt.
    ''' 
    ''' Wenn die Form noch nicht erzeugt wurde, wird in der Prozedur MainFormShow über Activator.CreateInstance() die
    ''' Klasse vom entsprechenen Typ erzeugt. (Entspricht xxx = New yyy)
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Ribbon_ActiveTabChanged(sender As Object, e As EventArgs) Handles rTab.ActiveTabChanged
        If isInitialised Then

            'Chargen/Statistik
            If rbChargen.Active Then
                MainFormShow(MdiChargen, GetType(Chargen_Main))
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
        End If
    End Sub

    ''' <summary>
    ''' Anzeigen der Hauptmenu-Fenster. Läd die Layout-Namen in das Auswahl-Fenster und setzt das Layout.
    ''' </summary>
    ''' <param name="oForm">Form-Name</param>
    ''' <param name="oType">Form-Type</param>
    Private Sub MainFormShow(ByRef oForm As Form, oType As Type)
        'Prüfen ob die Form schon existiert
        If oForm Is Nothing OrElse oForm.IsDisposed Then
            'wenn nicht - jetzt erzeugen (entspricht New !!)
            oForm = Activator.CreateInstance(oType)
        End If
        'Aktive Form
        AktForm = oForm

        'wenn die Form im Hintergrund liegt
        If oForm.Visible Then
            'Layout-Name abfragen aus Form
            DkPnlConfigFileName = DirectCast(AktForm, IMainMenu).DkPnlConfigFileName
            'Auswahl-Box mit Layoutnamen laden (Der letzte Layout-Name wir aus der Ini-Datei geladen)
            GetLayoutFileNames(LayoutFilename)
            'nach vorne holen und anzeigen
            oForm.BringToFront()
        Else
            'Layout muss neu geladen werden
            _LayoutFilename = Nothing
            'Auswahl-Box mit Layoutnamen laden (Der letzte Layout-Name wir aus der Ini-Datei geladen)
            GetLayoutFileNames(LayoutFilename)
            'Layout-Filename laden
            AktFormSendCommand("SETDKPNLFILENAME", DkPnlConfigFileName)
            'anzeigen
            oForm.Show()
        End If

        'Mdi-Fenster
        oForm.MdiParent = Me
        oForm.Dock = DockStyle.Fill
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
            Return DirectCast(AktForm, IMainMenu).ExecuteCmd(Cmd, Prm)
        Catch
            Return False
        End Try
    End Function
#End Region

    Private Sub WinBack_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Programm und Datei-Pfade einstellen
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack

        'Programm-Parameter auslesen
        ProcessParameter()
        'Sprache und Versions-Nummer in Status-Bar anzeigen
        ShowStatusBar()

        'Initialisierung beendet
        rTab.ActiveTab = rbAbout
        isInitialised = True

        'TEST REZEPTE
        'rTab.ActiveTab = rbRezepte
    End Sub

    ''' <summary>
    ''' Parameter bei Programm-Start auswerten.
    ''' Der erste Parameter enthält immer den Programm-Namen und Pfad
    ''' 
    ''' Gültige Parameter
    '''     -uXXX   Anmeldung mit User-Nummer(XXX)
    '''     -w      Anmeldung als Master-User
    '''     -m      Mandant
    '''     
    ''' </summary>
    Private Sub ProcessParameter()
        'Prozess-Parameter aus der Kommandozeile
        Dim Parameter As String() = Environment.GetCommandLineArgs().ToArray

        'wenn Parameter angegeben sind
        If Parameter.Length > 1 Then
            For i = 1 To Parameter.Length - 1
                Select Case Strings.Left(Parameter(i), 2)

                    'User Login
                    Case "-u"
                        Dim User As String = Strings.Mid(Parameter(i), 3)
                        Dim UserNummer As Integer = wb_Functions.StrToInt(User)
                        If Not wb_GlobalSettings.AktUserLogin(UserNummer) Then
                            MsgBox("Programmstart mit unbekanntem Benutzer. Bitte Parameter prüfen!", MsgBoxStyle.Critical)
                        End If

                    'User-Login(Master)
                    Case "-w"
                        Dim LoginMaster As Integer = Int(wb_Credentials.WinBackMasterUser)
                        If Not wb_GlobalSettings.AktUserLogin(LoginMaster) Then
                            MsgBox("Benutzer nicht gefunden. Bitte Parameter prüfen!", MsgBoxStyle.Critical)
                        Else
                            wb_AktUser.SuperUser = True
                        End If

                        'Falscher Parameter angegeben
                    Case Else
                        MsgBox("Unbekannter Start-Parameter: " & Parameter(i), MsgBoxStyle.Critical)

                End Select
            Next
        End If
    End Sub

    ''' <summary>
    ''' Fenster ist angezeigt - Wenn noch kein Benutzer angemeldet ist (Start-Parameter) wird das Login-Fenster angezeigt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub WinBack_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Wenn noch kein Benutzer angemeldet ist (Parameter -u)
        If wb_AktUser.UserNr < 0 Then
            ShowLogin()
        Else
            wb_Language.LoadTexteTabelle(wb_Language.GetLanguageNr())
        End If
        wb_AktUser.SetUserRechte(Me)
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
    End Sub

    ''' <summary>
    ''' Programm Ende
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbEnde_Click(sender As Object, e As EventArgs) Handles rbEnde.Click, rbClose.Click
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
    Private Sub rbSprache_Click(sender As Object, e As EventArgs) Handles rbDE.Click, rbSL.Click, rbSK.Click, rbRU.Click, rbRO.Click, rbPT.Click, rbNL.Click, rbHU.Click, rbFR.Click, rbES.Click, rbEN.Click
        Dim Language As String = DirectCast(sender, RibbonButton).Value
        wb_Language.SetLanguage(Language)
        ChangeLanguage()
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

            'Me.Location = pt
            'Me.Size = sz

            'Verarbeitung wieder freigeben
            isInitialised = True
        End If

        'Sprache und Versions-Nummer in Status-Bar anzeigen
        ShowStatusBar()
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
            LayoutFilename = wb_DockBarPanelMain.DkPnlConfigName(value, FormName)
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
                If Not My.Computer.FileSystem.FileExists(DkPnlConfigFileName) Then
                    'vom Default-Ordner kopieren
                    If My.Computer.FileSystem.FileExists(DkPnlConfigFileName(wb_Global.OrgaBackDockPanelLayoutPath.ProgrammGlobal)) Then
                        System.IO.File.Copy(DkPnlConfigFileName(wb_Global.OrgaBackDockPanelLayoutPath.ProgrammGlobal), DkPnlConfigFileName)
                    End If
                End If
            Catch ex As Exception
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
            Return AktForm.Text
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
        ConfigFileNames = wb_DockBarPanelMain.GetDkPnlConfigNameList(wb_GlobalSettings.DockPanelPath(wb_Global.OrgaBackDockPanelLayoutPath.ProgrammGlobal), FormName)
        For Each x In ConfigFileNames
            'aktueller Layout-Filename
            If LayoutFilename = x Then
                cbLayouts.Text = x
            End If
            cbLayouts.Items.Add(x)
        Next

        'Arbeitsplatz Verzeichnis ..\Temp\xx
        ConfigFileNames = wb_DockBarPanelMain.GetDkPnlConfigNameList(wb_GlobalSettings.DockPanelPath(wb_Global.OrgaBackDockPanelLayoutPath.UserLokal), FormName)
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
    Private Sub rbListe_Click(sender As Object, e As EventArgs) Handles rbArtikelListe.Click, rbRohstoffeListe.Click, rbRezeptListe.Click, rbListe.Click
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
                                                                          rbUserDetails.Click, rbUserBearbeiten.Click, rbRezeptBearbeiten.Click, rbRezeptDetails.Click
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
                                                                          rbUserNeu.Click, rbUserRemove.Click, rbUserRechte.Click, rbUserChangePass.Click, rbUserDrucken.Click,
                                                                          rbRohstoffeNeu.Click, rbRohstoffeLöschen.Click, rbRohstoffeVerwendung.Click,
                                                                          rbRezeptNeu.Click, rbRezeptHistorie.Click, rbRezeptHinweis.Click
        Dim Cmd As String = DirectCast(sender, RibbonButton).Value
        If Cmd <> "" Then
            AktFormSendCommand(Cmd, "")
        End If
    End Sub

    Private Sub StatusStrip_Resize(sender As Object, e As EventArgs) Handles StatusStrip.Resize
        Debug.Print("Resize StatusBar " & StatusStrip.Top & "/" & StatusStrip.Left & "//" & StatusStrip.Width & "/" & StatusStrip.Height)
    End Sub
End Class