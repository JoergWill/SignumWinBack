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
    Dim nwtUpdate As New wb_nwtUpdate
    Dim ServerTaskState As ServerTaskErrors = ServerTaskErrors.OK

    Private cntCounter As Integer
    Private cntMySql As Integer = 0
    Private cntCloudUpdate As Integer = 0
    Private maxCloudTxtLines As Integer = 10

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
    ''' Anzeige der letzen 20 Nährwert-Updates. 
    ''' Am Ende der Liste wird automatisch der älteste Eintrag gelöscht.
    ''' </summary>
    ''' <param name="tbx">Textbox</param>
    ''' <param name="s">String</param>
    Public Sub ScrollTextBox(ByRef tbx As TextBox, s As String)
        If tbx.Lines.Count > maxCloudTxtLines Then
            Dim str As String = tbx.Text
            tbx.Text = str.Substring(str.IndexOf(vbCrLf) + 2)
        End If
        tbx.Text &= s
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
    '''  - Ping MySQL
    '''  - Update Nährwerte
    '''  - Anzeige Uhrzeit
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
        If MainTimer_Check(cntCloudUpdate) Then
            'Datensatz wurde aus der Cloud aktualisiert
            If nwtUpdate.UpdateNext Then
                'Info-Text ausgeben
                ScrollTextBox(tbCloud, nwtUpdate.InfoText & vbNewLine)
            End If
            cntCloudUpdate = cntCounter + cntCheckCloud
        End If

        'Uhrzeit/Fehler anzeigen - Main-Timer OK
        Select Case ServerTaskState
            Case ServerTaskErrors.NO_PING_TO_MYSQL
                lblServerStatus.Text = "Keine Verbindung zur WinBack-Datenbank"
                lblServerStatus.ForeColor = Color.Red

            Case Else
                lblServerStatus.Text = DateTime.Now.ToLongTimeString
                lblServerStatus.ForeColor = Color.LimeGreen
                ScrollTextBox(tbCloud, DateTime.Now.ToLongTimeString & " " & "TEST" & vbNewLine)
        End Select

        'Timer wieder einschalten
        MainTimer.Enabled = True
        'TODO Timer-Aufrufe überprüfen
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
    ''' Initialisierung Server-Task.
    ''' - MySQL-Einstellungen aus ini-Datei laden
    ''' - Hash-Tables initialisieren
    ''' - IP-Server starten
    ''' - Main-Timer starten
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Mysql-Einstellungen (IP-Adresse, User, Passwort)
        wb_Konfig.SqlSetting()
        'Initialisierung Texte-Tabelle
        wb_Konfig.LoadTexteTabelle(wb_Konfig.GetLanguageNr())

        'IP-Server starten
        Dim listener As New System.Threading.Thread(AddressOf listen) 'initialize a new thread for the listener so our GUI doesn't lag
        listener.IsBackground = True
        listener.Start(wb_Global.WinBackServerTaskPort) 'start the listener, with the port specified as 22046

        'Status-Anzeige Backup/Restore
        lblBackupRestoreStatus.Text = ""
        'Timer löst jede Sekunde aus
        MainTimer.Interval = 250 'TODO 1000
        MainTimer.Enabled = True
        'Starte zyklischen Mysql-Ping
        cntMySql = cntCounter + cntCheckMysql + cntStartAfterOneSecond
        'Starte Cloud-Abfrage
        cntCloudUpdate = cntCounter + cntCheckCloud + cntStartAfterTwoSeconds
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
        maxCloudTxtLines = (DesktopSize.Height - 300) / tbCloud.Font.Height
        If maxCloudTxtLines < 20 Then
            maxCloudTxtLines = 20
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
            AddHandler mysql.statusChanged, AddressOf Backup_Restore_Status
            'Datenrücksicherung starten
            mysql.datenruecksicherung(fileName)
            'Status-Text nach 10 Sekunden wieder löschen
            RemoveHandler mysql.statusChanged, AddressOf Backup_Restore_Status
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
            AddHandler mysql.statusChanged, AddressOf Backup_Restore_Status
            'Datensicherung starten
            mysql.datensicherung(FileName)
            'Status-Text nach 10 Sekunden wieder löschen
            RemoveHandler mysql.statusChanged, AddressOf Backup_Restore_Status
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

End Class
