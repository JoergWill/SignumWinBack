Imports System.Net.Sockets
Imports System.Net
Imports WinBack

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

    Public Delegate Sub addListBoxDelegate(name As String)
    Public Delegate Sub remListBoxDelegate(name As String)
    Public Delegate Sub addText(text As String)

    Public Sub ListBoxadd(name As String)
        lbClientList.Items.Add(name)
    End Sub

    Public Sub ListBoxRemove(name As String)
        lbClientList.Items.Remove(name)
    End Sub

    Public Sub TextBoxadd(text As String)
        tbMessages.Text &= text
    End Sub

    Private Sub NotifyIcon_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon.DoubleClick
        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True
    End Sub

    Private Sub Main_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        If Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = False
        End If
    End Sub

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
            If nwtUpdate.UpdateNext Then
                tbCloud.Text &= nwtUpdate.InfoText & vbNewLine
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
        End Select

        'Timer wieder einschalten
        MainTimer.Enabled = True
    End Sub

    Private Function MainTimer_Check(ByRef x As Integer) As Boolean
        If (cntCounter >= x) And (x > 0) Then
            x = 0
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub RemoveTextTimer_Tick(sender As Object, e As EventArgs) Handles RemoveTextTimer.Tick
        'Lösche Status-Anzeige Backup/Restore
        lblBackupRestoreStatus.Text = ""
        RemoveTextTimer.Enabled = False
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Mysql-Einstellungen (IP-Adresse, User, Passwort)
        wb_Konfig.SqlSetting()
        'IP-Server starten
        Dim listener As New System.Threading.Thread(AddressOf listen) 'initialize a new thread for the listener so our GUI doesn't lag
        listener.IsBackground = True
        listener.Start("22046") 'start the listener, with the port specified as 22046

        'Status-Anzeige Backup/Restore
        lblBackupRestoreStatus.Text = ""
        'Timer löst jede Sekunde aus
        MainTimer.Interval = 1000
        MainTimer.Enabled = True
        'Starte zyklischen Mysql-Ping
        cntMySql = cntCounter + cntCheckMysql + cntStartAfterOneSecond
        'Starte Cloud-Abfrage
        cntCloudUpdate = cntCounter + cntCheckCloud + cntStartAfterTwoSeconds
    End Sub

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
    End Sub

    Private Sub BtnHide_Click(sender As Object, e As EventArgs) Handles BtnHide.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        If MessageBox.Show("Server-Task wirklich beenden ?" & vbNewLine & "Danach werden keine Hintergrund-Dienste mehr ausgeführt",
                           "WinBack Server-Task", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Close()
        End If
    End Sub

    Private Sub BtnClients_Click(sender As Object, e As EventArgs) Handles BtnClients.Click
        Wb_TabControl.SelectedTab = TabPageClients
        Wb_TabControl.Show()
    End Sub

    Private Sub btnMessages_Click(sender As Object, e As EventArgs) Handles btnMessages.Click
        Wb_TabControl.SelectedTab = TabPageMessages
        Wb_TabControl.Show()
    End Sub

    Private Sub BtnAdmin_Click(sender As Object, e As EventArgs) Handles BtnAdmin.Click
        Wb_TabControl.SelectedTab = TabPageAdmin
        Wb_TabControl.Show()
    End Sub

    Private Sub btnCloud_Click(sender As Object, e As EventArgs) Handles btnCloud.Click
        Wb_TabControl.SelectedTab = TabPageCloud
        Wb_TabControl.Show()
    End Sub

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

    Private Sub Backup_Restore_Status(txt As String)
        lblBackupRestoreStatus.Text = txt
        'Text anzeigen
        Application.DoEvents()
    End Sub

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

    Sub disconnected(ByVal client As wb_TcpIPConnection) 'if a client is disconnected, this is raised
        clients.Remove(client) 'remove the client from the hashtable
        lbClientList.Invoke(New remListBoxDelegate(AddressOf ListBoxRemove), New Object() {client.name}) 'remove it from our listbox
    End Sub

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
