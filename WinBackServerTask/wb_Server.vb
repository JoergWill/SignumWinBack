Imports WinBack.wb_Functions
Imports WinBack.wb_sql_BackupRestore

Public Class Main
    Private cntCounter As Integer
    Private cntClearlblBackupRestoreStatus As Integer = 0

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
        'Maximale Laufzeit = 2147483647/(60*60*24*364) = 68,23 Jahre
        'Bis dahin bin ich in Rente !!
        cntCounter = cntCounter + 1

        'Lösche Status-Anzeige Backup/Restore
        If (cntCounter >= cntClearlblBackupRestoreStatus) And (cntClearlblBackupRestoreStatus > 0) Then
            cntClearlblBackupRestoreStatus = 0
            lblBackupRestoreStatus.Text = ""
        End If

    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Status-Anzeige Backup/Restore
        lblBackupRestoreStatus.Text = ""
        'Timer löst jede Sekunde aus
        MainTimer.Interval = 1000
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

    Private Sub BtnRestore_Click(sender As Object, e As EventArgs) Handles BtnRestore.Click
        If OpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim fileName As String = OpenFileDialog.FileName
            Dim mysql As New WinBack.wb_sql_BackupRestore
            AddHandler mysql.statusChanged, AddressOf Backup_Restore_Status
            'Datenrücksicherung starten
            mysql.datenruecksicherung(fileName)
            'Status-Text nach 10 Sekunden wieder löschen
            RemoveHandler mysql.statusChanged, AddressOf Backup_Restore_Status
            cntClearlblBackupRestoreStatus = cntCounter + 10
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
            cntClearlblBackupRestoreStatus = cntCounter + 10
        End If
    End Sub

    Private Sub Backup_Restore_Status(txt As String)
        lblBackupRestoreStatus.Text = txt
        'Text anzeigen
        Application.DoEvents()
    End Sub
End Class
