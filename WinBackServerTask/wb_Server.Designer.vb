<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MainTimer = New System.Windows.Forms.Timer(Me.components)
        Me.BtnClients = New System.Windows.Forms.Button()
        Me.btnMessages = New System.Windows.Forms.Button()
        Me.BtnHide = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.BtnAdmin = New System.Windows.Forms.Button()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.Wb_TabControl = New WinBack.wb_TabControl()
        Me.TabPageClients = New System.Windows.Forms.TabPage()
        Me.lblClients = New System.Windows.Forms.Label()
        Me.lbClientList = New System.Windows.Forms.ListBox()
        Me.TabPageMessages = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbMessages = New System.Windows.Forms.TextBox()
        Me.TabPageAdmin = New System.Windows.Forms.TabPage()
        Me.lblBackupRestoreStatus = New System.Windows.Forms.Label()
        Me.lblBackupRestore = New System.Windows.Forms.Label()
        Me.BtnRestore = New System.Windows.Forms.Button()
        Me.BtnBackup = New System.Windows.Forms.Button()
        Me.Wb_TabControl.SuspendLayout()
        Me.TabPageClients.SuspendLayout()
        Me.TabPageMessages.SuspendLayout()
        Me.TabPageAdmin.SuspendLayout()
        Me.SuspendLayout()
        '
        'NotifyIcon
        '
        Me.NotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon.Icon = CType(resources.GetObject("NotifyIcon.Icon"), System.Drawing.Icon)
        Me.NotifyIcon.Text = "WinBack-ServerTask"
        Me.NotifyIcon.Visible = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(12, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(214, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "WinBack-Server-Task"
        '
        'MainTimer
        '
        Me.MainTimer.Enabled = True
        Me.MainTimer.Interval = 10000
        '
        'BtnClients
        '
        Me.BtnClients.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnClients.BackColor = System.Drawing.Color.Gray
        Me.BtnClients.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.BtnClients.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClients.ForeColor = System.Drawing.Color.White
        Me.BtnClients.Image = Global.WinBackServerTask.My.Resources.Resources.Users
        Me.BtnClients.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnClients.Location = New System.Drawing.Point(12, 611)
        Me.BtnClients.Name = "BtnClients"
        Me.BtnClients.Size = New System.Drawing.Size(110, 65)
        Me.BtnClients.TabIndex = 2
        Me.BtnClients.Text = "Clients"
        Me.BtnClients.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.BtnClients.UseVisualStyleBackColor = False
        '
        'btnMessages
        '
        Me.btnMessages.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMessages.BackColor = System.Drawing.Color.Gray
        Me.btnMessages.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnMessages.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMessages.ForeColor = System.Drawing.Color.White
        Me.btnMessages.Image = Global.WinBackServerTask.My.Resources.Resources.SpeechBubble
        Me.btnMessages.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnMessages.Location = New System.Drawing.Point(128, 611)
        Me.btnMessages.Name = "btnMessages"
        Me.btnMessages.Size = New System.Drawing.Size(110, 65)
        Me.btnMessages.TabIndex = 3
        Me.btnMessages.Text = "Messages"
        Me.btnMessages.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnMessages.UseVisualStyleBackColor = False
        '
        'BtnHide
        '
        Me.BtnHide.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnHide.BackColor = System.Drawing.Color.Gray
        Me.BtnHide.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.BtnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnHide.ForeColor = System.Drawing.Color.White
        Me.BtnHide.Image = Global.WinBackServerTask.My.Resources.Resources.Cancel
        Me.BtnHide.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnHide.Location = New System.Drawing.Point(244, 611)
        Me.BtnHide.Name = "BtnHide"
        Me.BtnHide.Size = New System.Drawing.Size(110, 65)
        Me.BtnHide.TabIndex = 4
        Me.BtnHide.Text = "Hide"
        Me.BtnHide.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.BtnHide.UseVisualStyleBackColor = False
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.Gray
        Me.BtnExit.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Image = Global.WinBackServerTask.My.Resources.Resources.OnOff
        Me.BtnExit.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnExit.Location = New System.Drawing.Point(244, 10)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(110, 65)
        Me.BtnExit.TabIndex = 5
        Me.BtnExit.Text = "Beenden"
        Me.BtnExit.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'BtnAdmin
        '
        Me.BtnAdmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnAdmin.BackColor = System.Drawing.Color.Gray
        Me.BtnAdmin.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.BtnAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAdmin.ForeColor = System.Drawing.Color.White
        Me.BtnAdmin.Image = Global.WinBackServerTask.My.Resources.Resources.Admin
        Me.BtnAdmin.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnAdmin.Location = New System.Drawing.Point(244, 540)
        Me.BtnAdmin.Name = "BtnAdmin"
        Me.BtnAdmin.Size = New System.Drawing.Size(110, 65)
        Me.BtnAdmin.TabIndex = 7
        Me.BtnAdmin.Text = "Administration"
        Me.BtnAdmin.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.BtnAdmin.UseVisualStyleBackColor = False
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "*.sql;*.bz2"
        Me.OpenFileDialog.Title = "Datenrücksicherung WinBack"
        '
        'SaveFileDialog
        '
        Me.SaveFileDialog.FileName = "*.sql;*.bz2"
        Me.SaveFileDialog.Title = "Datensicherung WinBack"
        '
        'Wb_TabControl
        '
        Me.Wb_TabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Wb_TabControl.Controls.Add(Me.TabPageClients)
        Me.Wb_TabControl.Controls.Add(Me.TabPageMessages)
        Me.Wb_TabControl.Controls.Add(Me.TabPageAdmin)
        Me.Wb_TabControl.Location = New System.Drawing.Point(12, 81)
        Me.Wb_TabControl.Multiline = True
        Me.Wb_TabControl.Name = "Wb_TabControl"
        Me.Wb_TabControl.SelectedIndex = 0
        Me.Wb_TabControl.Size = New System.Drawing.Size(342, 453)
        Me.Wb_TabControl.TabIndex = 6
        '
        'TabPageClients
        '
        Me.TabPageClients.BackColor = System.Drawing.Color.Black
        Me.TabPageClients.Controls.Add(Me.lblClients)
        Me.TabPageClients.Controls.Add(Me.lbClientList)
        Me.TabPageClients.ForeColor = System.Drawing.Color.White
        Me.TabPageClients.Location = New System.Drawing.Point(4, 23)
        Me.TabPageClients.Name = "TabPageClients"
        Me.TabPageClients.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageClients.Size = New System.Drawing.Size(334, 426)
        Me.TabPageClients.TabIndex = 0
        Me.TabPageClients.Text = "Clients"
        '
        'lblClients
        '
        Me.lblClients.AutoSize = True
        Me.lblClients.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClients.Location = New System.Drawing.Point(-4, 0)
        Me.lblClients.Name = "lblClients"
        Me.lblClients.Size = New System.Drawing.Size(127, 22)
        Me.lblClients.TabIndex = 0
        Me.lblClients.Text = "Verbindungen"
        '
        'lbClientList
        '
        Me.lbClientList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbClientList.BackColor = System.Drawing.Color.Black
        Me.lbClientList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lbClientList.ForeColor = System.Drawing.Color.White
        Me.lbClientList.FormattingEnabled = True
        Me.lbClientList.Location = New System.Drawing.Point(-4, 29)
        Me.lbClientList.Name = "lbClientList"
        Me.lbClientList.Size = New System.Drawing.Size(342, 390)
        Me.lbClientList.TabIndex = 0
        '
        'TabPageMessages
        '
        Me.TabPageMessages.BackColor = System.Drawing.Color.Black
        Me.TabPageMessages.Controls.Add(Me.Label2)
        Me.TabPageMessages.Controls.Add(Me.tbMessages)
        Me.TabPageMessages.ForeColor = System.Drawing.Color.White
        Me.TabPageMessages.Location = New System.Drawing.Point(4, 23)
        Me.TabPageMessages.Name = "TabPageMessages"
        Me.TabPageMessages.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageMessages.Size = New System.Drawing.Size(334, 426)
        Me.TabPageMessages.TabIndex = 1
        Me.TabPageMessages.Text = "Messages"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(-4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 22)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Meldungen"
        '
        'tbMessages
        '
        Me.tbMessages.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbMessages.BackColor = System.Drawing.Color.Black
        Me.tbMessages.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbMessages.ForeColor = System.Drawing.Color.White
        Me.tbMessages.Location = New System.Drawing.Point(0, 25)
        Me.tbMessages.Multiline = True
        Me.tbMessages.Name = "tbMessages"
        Me.tbMessages.Size = New System.Drawing.Size(334, 398)
        Me.tbMessages.TabIndex = 0
        Me.tbMessages.TabStop = False
        '
        'TabPageAdmin
        '
        Me.TabPageAdmin.BackColor = System.Drawing.Color.Black
        Me.TabPageAdmin.Controls.Add(Me.lblBackupRestoreStatus)
        Me.TabPageAdmin.Controls.Add(Me.lblBackupRestore)
        Me.TabPageAdmin.Controls.Add(Me.BtnRestore)
        Me.TabPageAdmin.Controls.Add(Me.BtnBackup)
        Me.TabPageAdmin.ForeColor = System.Drawing.Color.White
        Me.TabPageAdmin.Location = New System.Drawing.Point(4, 23)
        Me.TabPageAdmin.Name = "TabPageAdmin"
        Me.TabPageAdmin.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAdmin.Size = New System.Drawing.Size(334, 426)
        Me.TabPageAdmin.TabIndex = 2
        Me.TabPageAdmin.Text = "Admin"
        '
        'lblBackupRestoreStatus
        '
        Me.lblBackupRestoreStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBackupRestoreStatus.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackupRestoreStatus.ForeColor = System.Drawing.Color.Chartreuse
        Me.lblBackupRestoreStatus.Location = New System.Drawing.Point(-4, 91)
        Me.lblBackupRestoreStatus.Name = "lblBackupRestoreStatus"
        Me.lblBackupRestoreStatus.Size = New System.Drawing.Size(342, 18)
        Me.lblBackupRestoreStatus.TabIndex = 11
        Me.lblBackupRestoreStatus.Text = "Daten Backup/Restore"
        '
        'lblBackupRestore
        '
        Me.lblBackupRestore.AutoSize = True
        Me.lblBackupRestore.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackupRestore.Location = New System.Drawing.Point(-4, -2)
        Me.lblBackupRestore.Name = "lblBackupRestore"
        Me.lblBackupRestore.Size = New System.Drawing.Size(201, 22)
        Me.lblBackupRestore.TabIndex = 10
        Me.lblBackupRestore.Text = "Daten Backup/Restore"
        '
        'BtnRestore
        '
        Me.BtnRestore.BackColor = System.Drawing.Color.Gray
        Me.BtnRestore.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.BtnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRestore.ForeColor = System.Drawing.Color.White
        Me.BtnRestore.Image = Global.WinBackServerTask.My.Resources.Resources.Restore
        Me.BtnRestore.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnRestore.Location = New System.Drawing.Point(112, 23)
        Me.BtnRestore.Name = "BtnRestore"
        Me.BtnRestore.Size = New System.Drawing.Size(110, 65)
        Me.BtnRestore.TabIndex = 9
        Me.BtnRestore.Text = "Daten Restore"
        Me.BtnRestore.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.BtnRestore.UseVisualStyleBackColor = False
        '
        'BtnBackup
        '
        Me.BtnBackup.BackColor = System.Drawing.Color.Gray
        Me.BtnBackup.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.BtnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnBackup.ForeColor = System.Drawing.Color.White
        Me.BtnBackup.Image = Global.WinBackServerTask.My.Resources.Resources.Backup
        Me.BtnBackup.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnBackup.Location = New System.Drawing.Point(-4, 23)
        Me.BtnBackup.Name = "BtnBackup"
        Me.BtnBackup.Size = New System.Drawing.Size(110, 65)
        Me.BtnBackup.TabIndex = 8
        Me.BtnBackup.Text = "Daten sichern"
        Me.BtnBackup.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.BtnBackup.UseVisualStyleBackColor = False
        '
        'Main
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(366, 688)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnAdmin)
        Me.Controls.Add(Me.Wb_TabControl)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnHide)
        Me.Controls.Add(Me.btnMessages)
        Me.Controls.Add(Me.BtnClients)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main"
        Me.ShowInTaskbar = False
        Me.Text = "WinBack"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.Wb_TabControl.ResumeLayout(False)
        Me.TabPageClients.ResumeLayout(False)
        Me.TabPageClients.PerformLayout()
        Me.TabPageMessages.ResumeLayout(False)
        Me.TabPageMessages.PerformLayout()
        Me.TabPageAdmin.ResumeLayout(False)
        Me.TabPageAdmin.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents NotifyIcon As NotifyIcon
    Friend WithEvents Label1 As Label
    Friend WithEvents MainTimer As Timer
    Friend WithEvents BtnClients As Button
    Friend WithEvents btnMessages As Button
    Friend WithEvents BtnHide As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Wb_TabControl As WinBack.wb_TabControl
    Friend WithEvents TabPageClients As TabPage
    Friend WithEvents TabPageMessages As TabPage
    Friend WithEvents TabPageAdmin As TabPage
    Friend WithEvents BtnAdmin As Button
    Friend WithEvents BtnRestore As Button
    Friend WithEvents BtnBackup As Button
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents SaveFileDialog As SaveFileDialog
    Friend WithEvents lblBackupRestore As Label
    Friend WithEvents lblBackupRestoreStatus As Label
    Friend WithEvents lbClientList As ListBox
    Friend WithEvents tbMessages As TextBox
    Friend WithEvents lblClients As Label
    Friend WithEvents Label2 As Label
End Class
