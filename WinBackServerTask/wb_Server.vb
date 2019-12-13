Imports System.Net.Sockets
Imports System.Net
Imports WinBack

''' <summary>
''' WinBack-Server Taks
''' Startet die verschiedenen Hintergrund-Dienste auf dem Windows-OrgaBack-Server
'''  - MySQL-Ping
'''  - Update Nährwerte aus der Cloud
'''  - Anzeige Uhrzeit (Server läuft Kontrolle)
'''  Stellt die Dienste für IP-Server/Client bereit.
'''  - Update der Rohstoffe
'''  Backup/Restore der MySQL-Datenbank
'''  Konfiguration
''' </summary>
Public Class Main
    Const cntStartAfterOneSecond = 1
    Const cntStartAfterTwoSeconds = 2

    Const cntCheckMysql = 90
    Const cntCheckCloud = 5
    Const cntCheckAktionsTimer = 60

    Enum ServerTaskErrors
        OK
        NO_PING_TO_MYSQL
        NO_CONNECT_TO_NWT_CLOUD
    End Enum

    Dim clients As New Hashtable 'new database (hashtable) to hold the clients
    Dim Export As New ob_Chargen_Produziert
    Dim Import As New ob_ChargenBestand
    Dim ServerTaskState As ServerTaskErrors = ServerTaskErrors.OK

    Dim nwtUpdateKomponenten As New wb_nwtUpdate
    Dim nwtUpdateArtikel As New wb_nwtUpdateArtikel

    Dim Kocher As New wb_Kocher_Sync

    Private nwtUpdateKomponentenOrgaBack As Boolean = False
    Private nwtUpdateArtikelOrgaBack As Boolean = False

    Private cntCounter As Integer
    Private cntMySql As Integer = 0
    Private maxCloudTxtLines As Integer = 10

    Private WithEvents AktionsTimerGrid As wb_TimerGridView
    Private tArray As New ArrayList
    Private AktTimerEvent As wb_TimerEvent
    Private WithEvents EditTimer As wb_TimerEdit
    Private AktUpdateNummer As String = ""

    Private xLogger As New wb_TraceListener

    Public Delegate Sub addListBoxDelegate(name As String)
    Public Delegate Sub remListBoxDelegate(name As String)
    Public Delegate Sub addText(text As String)

    ''' <summary>
    ''' Client in Anzeige anfügen(Connect)
    ''' </summary>
    ''' <param name="name">(String) CLient-Name</param>
    Public Sub ListBoxadd(name As String)
        lbClientList.Items.Add(name)
    End Sub

    ''' <summary>
    ''' Client aus Anzeige entfernen(Disconnect)
    ''' </summary>
    ''' <param name="name">(String) CLient-Name</param>
    Public Sub ListBoxRemove(name As String)
        lbClientList.Items.Remove(name)
    End Sub

    ''' <summary>
    ''' Anzeige der letzen x Nährwert-Updates. 
    ''' Am Ende der Liste wird automatisch der älteste Eintrag gelöscht.
    ''' Die Länge wird bei Programmstart ermittelt und in maxCloudTxtLines abgelegt.
    ''' </summary>
    ''' <param name="tbx">Textbox</param>
    ''' <param name="s">String</param>
    Public Sub ScrollTextBox(ByRef tbx As TextBox, s As String)
        If tbx.Lines.Count > maxCloudTxtLines Then
            Dim str As String = tbx.Text
            tbx.Text = str.Substring(str.IndexOf(vbCrLf) + 2)
        End If
        tbx.Text &= s
        Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Zeile in TextBox anfügen. Wird in eine separate
    ''' Funktion ausgelagert, damit ein Invoke möglich wird.
    ''' </summary>
    ''' <param name="text">String</param>
    Public Sub TextBoxadd(text As String)
        tbMessages.Text &= text
    End Sub

    ''' <summary>
    ''' Click auf das Icon in der Taskleiste. Zeigt das Fenster
    ''' maximiert an.
    ''' </summary>
    ''' <param name="sender">Sender</param>
    ''' <param name="e">Event</param>
    Private Sub NotifyIcon_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon.MouseClick
        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True
    End Sub

    ''' <summary>
    ''' Doppelclick auf das Icon in der Taskleiste. Zeigt das Fenster
    ''' maximiert an.
    ''' </summary>
    ''' <param name="sender">Sender</param>
    ''' <param name="e">Event</param>
    Private Sub NotifyIcon_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon.DoubleClick
        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True
    End Sub

    ''' <summary>
    ''' Fenster maximiert anzeigen. Icon wird aus der Taskbar entfernt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Main_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        If Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = False
        End If
    End Sub

    ''' <summary>
    ''' Main-Timer. Ruft alle zeitgesteuerten Funktionen auf.
    '''  - Anzeige Uhrzeit
    '''  - Ping MySQL
    '''  - Update Nährwerte aus der Cloud       office_nwt
    '''  - Update markierte Artikel(Nährwerte)  office_artikel
    '''  - Import Rohstoffe Pistor-Liste        office_pistor
    '''  - Produzierte Ware und Verbräuche      office_chargen
    '''  - Lieferungen Rohstoffe                office_bestand
    '''  - Update Rezepte Kocher/Röster         kocher_check
    '''  - Check Update                         office_update
    '''  
    ''' CntCounter wird mit jedem Aufruf um Eins erhöht. Die entsprechende Task stoppt
    ''' den Timer und setzt seinen Aufruf nach Erledigung der Aufgabe wieder neu.
    ''' Danach wird der Timer wieder gestartet.
    ''' Bleibt der Timer-Aufruf aus, kann ein Restart ausgelöst werden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MainTimer_Tick(sender As Object, e As EventArgs) Handles MainTimer.Tick
        'Counter zählt jede Sekunde um Eins hoch
        'Maximale Laufzeit = 2147483647/(60*60*24*364) = 68,23 Jahre (Bis dahin bin ich in Rente)
        cntCounter = cntCounter + 1
        'Timer abschalten 
        MainTimer.Enabled = False

        'Check MySql(Ping)
        If MainTimer_Check(cntMySql) Then
            If Not wb_sql_Functions.ping Then
                ServerTaskState = ServerTaskErrors.NO_PING_TO_MYSQL
            Else
                ServerTaskState = ServerTaskErrors.OK
            End If
            cntMySql = cntCounter + cntCheckMysql
        End If

        'Abfrage Update Nährwert-Cloud
        If MainTimer_Check("office_nwt") Then
            'Daten im Grid aktualisieren
            RefreshAktionsTimer()

            'letzte Komponenten-Nummer aus Aktions-Timer-Tabelle
            Dim AktKONr As Integer = wb_Functions.StrToInt(AktTimerEvent.Str2)

            'Sonderfunktion alle Rohrstoffe in OrgaBack aktualisieren
            If AktKONr = wb_Global.obUpdateAll Then
                AktKONr = 0
                nwtUpdateKomponentenOrgaBack = True
            End If

            'Datensatz wurde aus der Cloud aktualisiert
            If nwtUpdateKomponenten.UpdateNext(AktKONr, nwtUpdateKomponentenOrgaBack) Then
                'Info-Text ausgeben
                ScrollTextBox(tbCloud, nwtUpdateKomponenten.InfoText & vbNewLine)
            End If
            'Nach Ende Update Nährwerte neue Startzeit setzen und letzte Komponenten-Nummer merken
            AktTimerEvent.Str2 = nwtUpdateKomponenten.AktKO_Nr.ToString
            AktTimerEvent.Endezeit = Now
            AktTimerEvent.MySQLdbUpdate_Fields()
            AktUpdateNummer = " (" & AktTimerEvent.Str2 & ")"
            'Daten im Grid aktualisieren
            RefreshAktionsTimer()
        End If


        'Abfrage Update markierte Artikel (Nährwerte und Zutatenliste)
        If MainTimer_Check("office_artikel") Then
            'Daten im Grid aktualisieren
            RefreshAktionsTimer()

            'letzte Komponenten-Nummer aus Aktions-Timer-Tabelle
            Dim AktKONr As Integer = wb_Functions.StrToInt(AktTimerEvent.Str2)

            'Sonderfunktion alle Rohrstoffe in OrgaBack aktualisieren
            If AktKONr = wb_Global.obUpdateAll Then
                AktKONr = 0
                nwtUpdateArtikelOrgaBack = True
            End If

            'Datensatz wurde aktualisiert
            If nwtUpdateArtikel.UpdateNext(AktKONr, nwtUpdateArtikelOrgaBack) Then
                'Info-Text ausgeben
                ScrollTextBox(tbCloud, nwtUpdateArtikel.InfoText & vbNewLine)
            End If
            'Nach Ende Update Nährwerte neue Startzeit setzen und letzte Komponenten-Nummer merken
            AktTimerEvent.Str2 = nwtUpdateArtikel.AktKO_Nr.ToString
            AktTimerEvent.Endezeit = Now
            AktTimerEvent.MySQLdbUpdate_Fields()
            AktUpdateNummer = " <" & AktTimerEvent.Str2 & ">"
            'Daten im Grid aktualisieren
            RefreshAktionsTimer()
        End If

        'Abfrage Import Pistor-Liste (
        If MainTimer_Check("office_pistor") Then
            'Import csv-File Format Pistor
            Dim nwtPistor As New wb_nwtPistor

            'Daten im Grid aktualisieren
            RefreshAktionsTimer()

            'Datensätze einlesen 
            While nwtPistor.ReadNext()
                'Info-Text ausgeben
                ScrollTextBox(tbCloud, nwtPistor.InfoText & vbNewLine)
            End While


            'Nach Ende Update Nährwerte neue Startzeit setzen
            AktTimerEvent.Endezeit = Now
            AktTimerEvent.MySQLdbUpdate_Fields()
            'Daten im Grid aktualisieren
            RefreshAktionsTimer()
            'Speicher wieder freigaben
            nwtPistor = Nothing
        End If

        'Abfrage produzierte Chargen und verbrauchte Rohstoffe
        If MainTimer_Check("office_chargen") Then
            'Daten im Grid aktualisieren
            RefreshAktionsTimer()

            'Export Chargen ab TW-Nr.x
            Dim TWNr As Integer = wb_Functions.StrToInt(AktTimerEvent.Str2)
            AktTimerEvent.Str2 = Export.ExportChargen(TWNr).ToString
            'Nach Ende Export neue Startzeit setzen
            AktTimerEvent.Endezeit = Now
            AktTimerEvent.MySQLdbUpdate_Fields()
            'Daten im Grid aktualisieren
            RefreshAktionsTimer()
        End If

        'Abfrage Rohstoffe Bestand und Chargen-Nummern
        If MainTimer_Check("office_bestand") Then
            'Daten im Grid aktualisieren
            RefreshAktionsTimer()

            'Import aus Tabelle dbo.ChargenBestand
            Dim KompNr As Integer = wb_Functions.StrToInt(AktTimerEvent.Str2)
            AktTimerEvent.Str2 = Import.ImportChargenBestand(KompNr)
            'Nach Ende Export neue Startzeit setzen
            AktTimerEvent.Endezeit = Now
            AktTimerEvent.MySQLdbUpdate_Fields()
            'Daten im Grid aktualisieren
            RefreshAktionsTimer()
        End If

        'Abfrage FTP-Verzeichnis/Verbindung Kocher/Röster
        If MainTimer_Check("kocher_check") Then
            'Daten im Grid aktualisieren
            RefreshAktionsTimer()

            'Check Kocher-Nr
            Dim KNr As Integer = wb_Functions.StrToInt(AktTimerEvent.Str2)
            AktTimerEvent.Str2 = Kocher.CheckKocher(KNr)
            'Nach Ende Check neue Startzeit setzen
            AktTimerEvent.Endezeit = Now
            AktTimerEvent.MySQLdbUpdate_Fields()
            'Daten im Grid aktualisieren
            RefreshAktionsTimer()
        End If

        'Check Update WinBack-AddIn/Server-Task
        If MainTimer_Check("office_update") Then
            'Daten im Grid aktualisieren
            RefreshAktionsTimer()

            'Prüfen ob eine neue Version zum Download verfügbar ist (OrgaBack.txt)


            'Nach Ende Export neue Startzeit setzen
            AktTimerEvent.Endezeit = Now
            AktTimerEvent.MySQLdbUpdate_Fields()
            'Daten im Grid aktualisieren
            RefreshAktionsTimer()
        End If

        'Uhrzeit/Fehler anzeigen - Main-Timer OK
        Select Case ServerTaskState
            Case ServerTaskErrors.NO_PING_TO_MYSQL
                lblServerStatus.Text = "Keine Verbindung zur WinBack-Datenbank"
                lblServerStatus.ForeColor = Color.Red

            Case Else
                lblServerStatus.Text = DateTime.Now.ToLongTimeString
                'wenn die Anzeige der Nährwert-Cloud geöffnet ist, wird die aktuell bearbeitete Komponenten-Nummer angezeigt
                If tbCloud.Visible Then
                    lblServerStatus.Text &= " " & AktUpdateNummer
                End If
                lblServerStatus.ForeColor = Color.LimeGreen
        End Select

        'Timer wieder einschalten
        MainTimer.Enabled = True
        'Beim Schliessen des Detail-Fensters bleiben markierte Textblöcke übrig. Markierung wieder löschen
        tbCloud.Select(0, 0)
    End Sub

    ''' <summary>
    ''' Prüft ob der Timer-Wert erreicht oder überschritten ist.
    ''' Wenn der Timer-Sollwert auf Null steht wird False zurückgegeben
    ''' </summary>
    ''' <param name="x">True wenn der Timer-Wert erreicht ist</param>
    ''' <returns></returns>
    Private Function MainTimer_Check(ByRef x As Integer) As Boolean
        If (cntCounter >= x) And (x > 0) Then
            x = 0
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Prüft ob die im Array angebene Zeit für diesen Task erreicht ist.
    ''' Wenn der Task gestartet werden soll, wird True zurückgegeben und der Timer-Wert im
    ''' Array neu gesetzt.
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Private Function MainTimer_Check(s As String) As Boolean
        For Each AktTimerEvent In tArray
            If AktTimerEvent.Check(s) Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Löscht die Status-Anzeige nach 10 Sekunden. Danach wird wieder die Uhrzeit 
    ''' angezeigt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RemoveTextTimer_Tick(sender As Object, e As EventArgs) Handles RemoveTextTimer.Tick
        'Lösche Status-Anzeige Backup/Restore
        lblBackupRestoreStatus.Text = ""
        RemoveTextTimer.Enabled = False
    End Sub

    ''' <summary>
    ''' Zeigt das aktualisierte Array der Timer-Einstellungen aus der Tabelle winback.AktionsTimer
    ''' </summary>
    Private Sub ShowAktionsTimer()
        'Daten im Grid anzeigen
        AktionsTimerGrid.ScrollBars = ScrollBars.Vertical
        AktionsTimerGrid.BackgroundColor = Me.BackColor
        AktionsTimerGrid.GridLocation(tbAktionsTimer)
        AktionsTimerGrid.PerformLayout()
    End Sub

    Private Sub RefreshAktionsTimer()
        'Refresh Tabelle nur wenn die entsprechende Seite sichtbar ist
        If TabPageTimer.Visible Then
            AktionsTimerGrid.FillGrid()
            'Zeit zum Zeichnen
            System.Threading.Thread.Sleep(100)
        End If
    End Sub

    Private Sub AktionsTimerDrawCell(ByVal sender As Object, ByVal e As DataGridViewCellPaintingEventArgs) Handles AktionsTimerGrid.CellPainting
        Dim Grid = DirectCast(sender, DataGridView)
        If e.ColumnIndex = wb_TimerGridView.COLSTAT And (e.RowIndex >= 0) Then
            Dim _Brush As New SolidBrush(Color.Yellow)
            Select Case e.Value
                Case wb_Global.wbAktionsTimerStatus.Disabled
                    _Brush.Color = Color.Red
                Case wb_Global.wbAktionsTimerStatus.Enabled
                    _Brush.Color = Color.Yellow
                Case wb_Global.wbAktionsTimerStatus.Running
                    _Brush.Color = Color.Green
                Case Else
                    Return
            End Select
            e.Graphics.FillRectangle(_Brush, e.CellBounds)
            e.Handled = True
        Else
            Return
        End If
    End Sub

    ''' <summary>
    ''' Doppelclick auf Aktions-Timer-Tabelle.
    ''' Edit-Fenster Start-Zeit, Zyklus und Sonderfunktionen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AktionsTimerDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles AktionsTimerGrid.CellMouseDoubleClick
        Dim Grid = DirectCast(sender, DataGridView)

        If (e.RowIndex >= 0) Then
            'wenn das Fenster noch nicht vorhanden ist - erzeugen
            If EditTimer Is Nothing Then
                EditTimer = New wb_TimerEdit
            End If

            'Index (Zeiger auf Datensatz im Grid/Array)
            EditTimer.Index = e.RowIndex
            'Task-Name
            EditTimer.TimerName = DirectCast(tArray(e.RowIndex), wb_TimerEvent).Task
            'Timer Bezeichnung
            EditTimer.TimerBezeichnung = DirectCast(tArray(e.RowIndex), wb_TimerEvent).Task
            'Task-Start Datum/Uhrzeit
            EditTimer.TimerStart = DirectCast(tArray(e.RowIndex), wb_TimerEvent).Startzeit
            'Task-Zyklus
            EditTimer.TimerZyklus = DirectCast(tArray(e.RowIndex), wb_TimerEvent).Periode
            'aktueller Index
            EditTimer.TimerAktIndex = DirectCast(tArray(e.RowIndex), wb_TimerEvent).Str2
            'Task aktiv
            If DirectCast(tArray(e.RowIndex), wb_TimerEvent).Status = wb_Global.wbAktionsTimerStatus.Disabled Then
                EditTimer.TimerAktiv = False
            Else
                EditTimer.TimerAktiv = True
            End If

            'Eingabe-Form anzeigen
            EditTimer.Show()
        End If
    End Sub

    ''' <summary>
    ''' Fenster Edit Timer-Events wird geschlossen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub EditTimerEventClosing(Sender As Object, e As EventArgs) Handles EditTimer.Closing
        'Index (Zeiger auf Datensatz im Grid/Array)
        Dim i As Integer = EditTimer.Index

        'Daten im Array aktualisieren - Startzeit
        DirectCast(tArray(i), wb_TimerEvent).Startzeit = EditTimer.TimerStart
        'Daten im Array aktualisieren - Zyklus
        DirectCast(tArray(i), wb_TimerEvent).Periode = EditTimer.TimerZyklus
        'Daten im Array aktualisieren - aktueller Index
        DirectCast(tArray(i), wb_TimerEvent).Str2 = EditTimer.TimerAktIndex
        'Timer aktiv (Wenn der Timer aktuell läuft, wird er erst nach dem Ende des Task auf inaktiv gesetzt
        DirectCast(tArray(i), wb_TimerEvent).Aktiv = EditTimer.TimerAktiv

        'Anzeige der Tabelle aktualisieren
        RefreshAktionsTimer()
        'Daten in MySQl sichern
        DirectCast(tArray(i), wb_TimerEvent).MySQLdbUpdate_Fields()
        'Eingabe-Fenster wieder freigeben
        EditTimer = Nothing
    End Sub

    ''' <summary>
    ''' Läd die Daten aus der Tabelle winback.AktionsTimer in tArray
    ''' </summary>
    Private Sub LoadAktionsTimer()
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        If winback.sqlSelect(wb_Sql_Selects.sqlAktionsTimer) Then
            While winback.Read
                Dim _Item As New wb_TimerEvent
                _Item.MySQLdbRead(winback.MySqlRead)
                tArray.Add(_Item)
            End While
        End If

        winback.Close()
        winback = Nothing
    End Sub

    ''' <summary>
    ''' Initialisierung Server-Task.
    ''' - MySQL-Einstellungen aus ini-Datei laden
    ''' - Hash-Tables initialisieren
    ''' - IP-Server starten
    ''' - Main-Timer starten
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Programm und Datei-Pfade einstellen
        If wb_Functions.OrgaBackIsInstalled Then
            wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OBServerTask
        Else
            wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WBServerTask
        End If

        'WinBack-Mandant aus Kommandozeile
        'TODO Im Echtbetrieb prüfen !!!
        Main_CommandLineAuswerten()

        'Debug/Trace-Listener initialisieren
        AddTraceListener()

        'Initialisierung Texte-Tabelle
        wb_Language.LoadTexteTabelle(wb_Language.GetLanguageNr())

        'IP-Server starten
        Dim listener As New System.Threading.Thread(AddressOf listen) 'initialize a new thread for the listener so our GUI doesn't lag
        listener.IsBackground = True
        listener.Start(wb_Global.WinBackServerTaskPort) 'start the listener, with the port specified as 22046

        'Liste der Tabellen-Überschriften
        '   Spalten mit & dienen als Ausgleich der Breite
        '   Spalten mit # enthalten Datum/Uhrzeit-Angaben
        Dim sColNames As New List(Of String)
        sColNames.AddRange({"", "&Task", "Startzeit", "Periode", "Status"})
        LoadAktionsTimer()
        AktionsTimerGrid = New wb_TimerGridView(tArray, sColNames)
        'Tabelle darf editiert werden
        AktionsTimerGrid.ReadOnly = True

        'Status-Anzeige Backup/Restore
        lblBackupRestoreStatus.Text = ""
        'Timer löst jede Sekunde aus
        MainTimer.Interval = 1000
        MainTimer.Enabled = True
        'Starte zyklischen Mysql-Ping
        cntMySql = cntCounter + cntCheckMysql + cntStartAfterOneSecond
    End Sub

    ''' <summary>
    ''' Kommandozeile auswerten (Programm-Start)
    '''     /M: Mandant
    '''     /I: Pfad zur winback.ini
    ''' </summary>
    Private Sub Main_CommandLineAuswerten()
        For Each s As String In My.Application.CommandLineArgs
            'die ersten 3 Zeichen entsprechen der Funktion
            Select Case s.Substring(0, 3)
                Case "/M:"
                    Dim MandantNr As String = s.Substring(3)
                    wb_GlobalSettings.MandantNr = wb_Functions.StrToInt(MandantNr)
                Case "/I:"
                    Dim WinbackIni As String = s.Substring(3)
                    wb_GlobalSettings.pWinBackIniPath = WinbackIni & "\WinBack.ini"
                Case Else
                    Trace.WriteLine("Fehler in Command_line-Argument " & s)
            End Select
        Next
    End Sub

    ''' <summary>
    ''' Anzeige Fenster Server-Task
    ''' Das Fenster wird auf maximale Bildschirm-Größe aufgezogen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Main_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ' TabControl - HideTabs
        Wb_TabControl.HideTabs = True
        Wb_TabControl.Hide()

        ' Breite Fenster
        Const fBreite = 366
        ' Bildschirmmauflösung ermitteln
        Dim DesktopSize As Size
        DesktopSize = System.Windows.Forms.SystemInformation.PrimaryMonitorSize
        ' Fenster vertikal mximimieren
        Me.Height = DesktopSize.Height + 5
        Me.Top = 0
        Me.Width = fBreite
        Me.Left = DesktopSize.Width - fBreite + 7
        'Anzahl der Zeichen in der Textbox (Ausgabe Nährwert-Cloud)
        maxCloudTxtLines = (DesktopSize.Height - 260) / tbCloud.Font.Height

        'Anzeige Server-IP und Mandanten-Info
        lblServerInfo.Text = "MySQL " & wb_GlobalSettings.MySQLServerIP & vbCr & wb_GlobalSettings.MsSQLMain
        'Status-Anzeige im Admin-Fenster verschieben
        lblServerInfo.Top = Me.Height - 300

        'im Debug-Modus Fenster sofort anzeigen
        If Debugger.IsAttached Then
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    ''' <summary>
    ''' Button Hide
    ''' Server-Task Fenster minimieren. Symbol in Task-Bar anzeigen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnHide_Click(sender As Object, e As EventArgs) Handles BtnHide.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    ''' <summary>
    ''' Botton Exit
    ''' Programm-Ende nach Abfrage.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        If MessageBox.Show("Server-Task wirklich beenden ?" & vbNewLine & "Danach werden keine Hintergrund-Dienste mehr ausgeführt",
                           "WinBack Server-Task", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Close()
        End If
    End Sub

    ''' <summary>
    ''' Button Clients
    ''' Anzeige aller verbundenen Client-Connections 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnClients_Click(sender As Object, e As EventArgs) Handles BtnClients.Click
        Wb_TabControl.SelectedTab = TabPageClients
        Wb_TabControl.Show()
    End Sub

    ''' <summary>
    ''' Button Messages
    ''' Anzeige der Meldungen von den Clients
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnMessages_Click(sender As Object, e As EventArgs) Handles btnMessages.Click
        Wb_TabControl.SelectedTab = TabPageMessages
        Wb_TabControl.Show()
    End Sub

    Private Sub BtnAdmin_Click(sender As Object, e As EventArgs) Handles BtnAdmin.Click
        Wb_TabControl.SelectedTab = TabPageAdmin
        Wb_TabControl.Show()
    End Sub

    ''' <summary>
    ''' Button Timer
    ''' Anzeige und Einstellungen aller Timer-Ereignisse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnTimer_Click(sender As Object, e As EventArgs) Handles BtnTimer.Click
        Wb_TabControl.SelectedTab = TabPageTimer
        Wb_TabControl.Show()

        ShowAktionsTimer()
        RefreshAktionsTimer()
    End Sub

    ''' <summary>
    ''' Button Cloud
    ''' Anzeige der Informationen aus der Nährwert-Aktualisierungs-Task
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCloud_Click(sender As Object, e As EventArgs) Handles btnCloud.Click
        Wb_TabControl.SelectedTab = TabPageCloud
        Wb_TabControl.Show()
    End Sub

    ''' <summary>
    ''' Button Restore
    ''' Datenrücksicherung von Datei in lokale WinBack-SQL
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnRestore_Click(sender As Object, e As EventArgs) Handles BtnRestore.Click
        If OpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim fileName As String = OpenFileDialog.FileName
            Dim mysql As New WinBack.wb_sql_BackupRestore
            '            AddHandler mysql.statusChanged, AddressOf Backup_Restore_Status
            'Datenrücksicherung starten
            mysql.datenruecksicherung(fileName)
            'Status-Text nach 10 Sekunden wieder löschen
            '            RemoveHandler mysql.statusChanged, AddressOf Backup_Restore_Status
            RemoveTextTimer.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Button Datensicherung
    ''' Datensicherung der lokalen WinBack-SQL in Datei
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnBackup_Click(sender As Object, e As EventArgs) Handles BtnBackup.Click
        If SaveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim FileName As String = SaveFileDialog.FileName
            Dim mysql As New WinBack.wb_sql_BackupRestore
            '            AddHandler mysql.statusChanged, AddressOf Backup_Restore_Status
            'Datensicherung starten
            mysql.datensicherung(FileName)
            'Status-Text nach 10 Sekunden wieder löschen
            '            RemoveHandler mysql.statusChanged, AddressOf Backup_Restore_Status
            'TODO Handler wieder einbauen !!!
            RemoveTextTimer.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Anzeige Status-Fenster Backup/Restore MySQL
    ''' </summary>
    ''' <param name="txt"></param>
    Private Sub Backup_Restore_Status(txt As String)
        lblBackupRestoreStatus.Text = txt
        'Text anzeigen
        Application.DoEvents()
    End Sub

    ''' <summary>
    ''' IP-Listenener. Wartet auf eingehende Verbindungen von IP-CLients. Für jeden
    ''' Client wird eine neue IP-Verbindung erzeugt.
    ''' </summary>
    ''' <param name="port"></param>
    Sub listen(ByVal port As Integer)
        Try
            Dim t As New TcpListener(IPAddress.Any, port) 'declare a new tcplistener
            t.Start() 'start the listener
            Do
                Dim client As New WinBack.wb_TcpIPConnection(t.AcceptTcpClient) 'initialize a new connected client
                AddHandler client.gotmessage, AddressOf recieved 'add the handler which will raise an event when a message is recieved
                AddHandler client.disconnected, AddressOf disconnected 'add the handler which will raise an event when the client disconnects
            Loop Until False
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Client.GotMessage wird hier verarbeitet
    ''' </summary>
    ''' <param name="msg">String - Empfangene Nachricht vom CLient</param>
    ''' <param name="client">TcpIPConnection - Client Instanz</param>
    Sub recieved(ByVal msg As String, ByVal client As wb_TcpIPConnection)
        Dim message() As String = msg.Split("|") 'make an array with elements of the message recieved
        Select Case message(0) 'process by the first element in the array
            Case "CHAT" 'if it's CHAT
                tbMessages.Invoke(New addText(AddressOf TextBoxadd), New Object() {client.name & " says: " & " " & message(1) & vbNewLine})
                sendallbutone(client.name & " says" & message(1), client.name) 'this will update all clients with the new message
                '                                       and it will not send the message to the client it recieved it from :)
            Case "LOGIN" 'A client has connected
                clients.Add(client, client.name) 'add the client to our database (a hashtable)
                lbClientList.Invoke(New addListBoxDelegate(AddressOf ListBoxadd), New Object() {client.name}) 'add the client to the listbox to display the new user
        End Select
    End Sub

    ''' <summary>
    ''' Client hat die Verbindung geschlossen. Der entsprechende Client wird aus der Liste entfernt.
    ''' Die ListBox wird aktualisiert.
    ''' </summary>
    ''' <param name="client"></param>
    Sub disconnected(ByVal client As wb_TcpIPConnection) 'if a client is disconnected, this is raised
        clients.Remove(client) 'remove the client from the hashtable
        lbClientList.Invoke(New remListBoxDelegate(AddressOf ListBoxRemove), New Object() {client.name}) 'remove it from our listbox
    End Sub

    ''' <summary>
    ''' Sende eine Message an alle verbundenen Clients
    ''' </summary>
    ''' <param name="message">String - Message</param>
    Sub senddata(ByVal message As String) 'this sends a message to all connected clients
        Dim entry As DictionaryEntry 'declare a variable of type dictionary entry
        Try
            For Each entry In clients 'for each dictionary entry in the hashtable with all clients (clients)
                Dim cli As wb_TcpIPConnection = CType(entry.Key, wb_TcpIPConnection) ' cast the hashtable entry to a connection class
                cli.senddata(message) 'send the message to it
            Next  'go to the next client
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Sende eine empfangene Message an alle anderen Clients
    ''' </summary>
    ''' <param name="message"></param>
    ''' <param name="exemptclientname"></param>
    Sub sendallbutone(ByVal message As String, ByVal exemptclientname As String) 'this sends to all clients except the one specified
        Dim entry As DictionaryEntry 'declare a variable of type dictionary entry
        Try
            For Each entry In clients 'for each dictionary entry in the hashtable with all clients (clients)
                If entry.Value <> exemptclientname Then 'if the entry IS NOT the exempt client name
                    Dim cli As wb_TcpIPConnection = CType(entry.Key, wb_TcpIPConnection) ' cast the hashtable entry to a connection class
                    cli.senddata(message) 'send the message to it
                End If
            Next
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Doppelclick auf die Nährwerte-Cloud-Ausgabe öffnet ein Fenster mit detaillierten Angaben zum ausgewählten Rohstoff
    ''' (Ausgabe der Daten aus Hinweise.NaehrwertUpdate)
    ''' 
    ''' Auswertung der aktuellen Zeile aus http://stackoverflow.com/questions/10746121/how-to-select-a-line-in-a-richtextbox-on-a-mouse-click
    ''' First you need to map the mouse position to a character index
    ''' Then you need to map the character index to a line
    ''' Then you need to find out where the line starts
    ''' Then you need to find out where it ends, which is the start of the next line minus one
    ''' Then you need to make the selection
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tbCloud_DoubleClick(sender As Object, e As EventArgs) Handles tbCloud.DoubleClick
        Dim Box = DirectCast(sender, TextBox)
        Dim idx = Box.GetCharIndexFromPosition(DirectCast(e, MouseEventArgs).Location)
        Dim line = Box.GetLineFromCharIndex(idx)
        Box.SelectionStart = Box.GetFirstCharIndexFromLine(line)
        Box.SelectionLength = Box.GetFirstCharIndexFromLine(line + 1) - 1 - Box.SelectionStart
        'Selektierter Text
        Dim x() As String = Box.SelectedText.Split(" ")
        'Die Rohstoff-Nummer steht an zweiter Stelle
        'Nährwert-Updates zum Rohstoff im Detail aus DB lesen
        Dim h As New wb_Hinweise(wb_Global.Hinweise.NaehrwertUpdate, CInt(Val(x(1).Substring(1))))
        'Selektion wieder aufheben
        Box.Select(0, 0)

        'Fenster mit Detail-Informationen modal einblenden
        Dim DetailAnsicht As New wb_ServerDetails
        DetailAnsicht.Text = "Detail-Bericht Update Artikel/Rohstoff-Nährwerte und Allergene"
        DetailAnsicht.tbDetails.Text = h.Memo
        DetailAnsicht.tbDetails.Select(0, 0)
        DetailAnsicht.ShowDialog()
    End Sub

    ''' <summary>
    ''' alle Trace/Debug-Ausgaben werden auch in der Klasse wb_Admin_Shared in einer Text-Liste gespeichert.
    ''' Nach x Zeilen werden die Einträge in ein Text-File gespeichert.
    ''' Die Klasse xLogger (wb_Trace_Listener) leitet die Meldungen weiter.
    ''' </summary>
    Sub AddTraceListener()
        AddHandler xLogger.WriteText, AddressOf wb_Admin_Shared.GetTraceListenerText
        Trace.Listeners.Add(xLogger)

        'Meldung Programm-Start (initialisiert wb_Admin_Shared)
        Trace.WriteLine("Programmstart WinBackServer-Task")
    End Sub

    ''' <summary>
    ''' Anzeigefenster mit Ausgabe der Log-Daten
    ''' Es wird das gleiche Fenster wie auch beim WinBack-AddIn verwendet.
    ''' 
    ''' Anzeige der Programm-Ausgaben Debug.Print und Trace.Writeln
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnLogFile_Click(sender As Object, e As EventArgs) Handles BtnLogFile.Click
        Dim Admin_Log As New wb_Admin_Log
        Admin_Log.Show()
    End Sub

    ''' <summary>
    ''' Anzeigefenster mit Editor winback.ini
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnEditKonfig_Click(sender As Object, e As EventArgs) Handles BtnEditKonfig.Click
        Dim Admin_EditIni As New wb_Admin_EditIni
        Admin_EditIni.ShowDialog()
    End Sub
End Class