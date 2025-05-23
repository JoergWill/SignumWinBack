﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.BtnCloud = New System.Windows.Forms.Button()
        Me.BtnTimer = New System.Windows.Forms.Button()
        Me.RemoveTextTimer = New System.Windows.Forms.Timer(Me.components)
        Me.lblServerStatus = New System.Windows.Forms.Label()
        Me.Wb_TabControl = New WinBack.wb_TabControl()
        Me.TabPageClients = New System.Windows.Forms.TabPage()
        Me.lblClients = New System.Windows.Forms.Label()
        Me.lbClientList = New System.Windows.Forms.ListBox()
        Me.TabPageMessages = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbMessages = New System.Windows.Forms.TextBox()
        Me.TabPageAdmin = New System.Windows.Forms.TabPage()
        Me.BtnEditKonfig = New System.Windows.Forms.Button()
        Me.BtnLogFile = New System.Windows.Forms.Button()
        Me.lblServerInfo = New System.Windows.Forms.Label()
        Me.lblBackupRestoreStatus = New System.Windows.Forms.Label()
        Me.lblBackupRestore = New System.Windows.Forms.Label()
        Me.BtnRestore = New System.Windows.Forms.Button()
        Me.BtnBackup = New System.Windows.Forms.Button()
        Me.TabPageCloud = New System.Windows.Forms.TabPage()
        Me.tbCloud = New System.Windows.Forms.TextBox()
        Me.lblCloud = New System.Windows.Forms.Label()
        Me.TabPageTimer = New System.Windows.Forms.TabPage()
        Me.tbAktionsTimer = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Wb_TabControl.SuspendLayout()
        Me.TabPageClients.SuspendLayout()
        Me.TabPageMessages.SuspendLayout()
        Me.TabPageAdmin.SuspendLayout()
        Me.TabPageCloud.SuspendLayout()
        Me.TabPageTimer.SuspendLayout()
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
        Me.BtnClients.Location = New System.Drawing.Point(12, 778)
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
        Me.btnMessages.Location = New System.Drawing.Point(128, 778)
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
        Me.BtnHide.Location = New System.Drawing.Point(244, 778)
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
        Me.BtnAdmin.Location = New System.Drawing.Point(244, 707)
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
        'BtnCloud
        '
        Me.BtnCloud.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnCloud.BackColor = System.Drawing.Color.Gray
        Me.BtnCloud.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.BtnCloud.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCloud.ForeColor = System.Drawing.Color.White
        Me.BtnCloud.Image = Global.WinBackServerTask.My.Resources.Resources.Cloud
        Me.BtnCloud.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnCloud.Location = New System.Drawing.Point(12, 707)
        Me.BtnCloud.Name = "BtnCloud"
        Me.BtnCloud.Size = New System.Drawing.Size(110, 65)
        Me.BtnCloud.TabIndex = 8
        Me.BtnCloud.Text = "Nährwerte-Cloud"
        Me.BtnCloud.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.BtnCloud.UseVisualStyleBackColor = False
        '
        'BtnTimer
        '
        Me.BtnTimer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnTimer.BackColor = System.Drawing.Color.Gray
        Me.BtnTimer.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.BtnTimer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnTimer.ForeColor = System.Drawing.Color.White
        Me.BtnTimer.Image = Global.WinBackServerTask.My.Resources.Resources.Clock
        Me.BtnTimer.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnTimer.Location = New System.Drawing.Point(128, 707)
        Me.BtnTimer.Name = "BtnTimer"
        Me.BtnTimer.Size = New System.Drawing.Size(110, 65)
        Me.BtnTimer.TabIndex = 9
        Me.BtnTimer.Text = "Timer"
        Me.BtnTimer.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.BtnTimer.UseVisualStyleBackColor = False
        '
        'RemoveTextTimer
        '
        Me.RemoveTextTimer.Interval = 30000
        '
        'lblServerStatus
        '
        Me.lblServerStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerStatus.ForeColor = System.Drawing.Color.Red
        Me.lblServerStatus.Location = New System.Drawing.Point(12, 34)
        Me.lblServerStatus.Name = "lblServerStatus"
        Me.lblServerStatus.Size = New System.Drawing.Size(214, 41)
        Me.lblServerStatus.TabIndex = 10
        '
        'Wb_TabControl
        '
        Me.Wb_TabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Wb_TabControl.Controls.Add(Me.TabPageClients)
        Me.Wb_TabControl.Controls.Add(Me.TabPageMessages)
        Me.Wb_TabControl.Controls.Add(Me.TabPageAdmin)
        Me.Wb_TabControl.Controls.Add(Me.TabPageCloud)
        Me.Wb_TabControl.Controls.Add(Me.TabPageTimer)
        Me.Wb_TabControl.Location = New System.Drawing.Point(12, 81)
        Me.Wb_TabControl.Name = "Wb_TabControl"
        Me.Wb_TabControl.SelectedIndex = 0
        Me.Wb_TabControl.Size = New System.Drawing.Size(342, 620)
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
        Me.TabPageClients.Size = New System.Drawing.Size(334, 593)
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
        Me.lbClientList.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbClientList.ForeColor = System.Drawing.Color.White
        Me.lbClientList.FormattingEnabled = True
        Me.lbClientList.ItemHeight = 15
        Me.lbClientList.Location = New System.Drawing.Point(-4, 29)
        Me.lbClientList.Name = "lbClientList"
        Me.lbClientList.Size = New System.Drawing.Size(342, 555)
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
        Me.TabPageMessages.Size = New System.Drawing.Size(334, 593)
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
        Me.tbMessages.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMessages.ForeColor = System.Drawing.Color.White
        Me.tbMessages.Location = New System.Drawing.Point(0, 25)
        Me.tbMessages.Multiline = True
        Me.tbMessages.Name = "tbMessages"
        Me.tbMessages.Size = New System.Drawing.Size(334, 534)
        Me.tbMessages.TabIndex = 0
        Me.tbMessages.TabStop = False
        '
        'TabPageAdmin
        '
        Me.TabPageAdmin.BackColor = System.Drawing.Color.Black
        Me.TabPageAdmin.Controls.Add(Me.BtnEditKonfig)
        Me.TabPageAdmin.Controls.Add(Me.BtnLogFile)
        Me.TabPageAdmin.Controls.Add(Me.lblServerInfo)
        Me.TabPageAdmin.Controls.Add(Me.lblBackupRestoreStatus)
        Me.TabPageAdmin.Controls.Add(Me.lblBackupRestore)
        Me.TabPageAdmin.Controls.Add(Me.BtnRestore)
        Me.TabPageAdmin.Controls.Add(Me.BtnBackup)
        Me.TabPageAdmin.ForeColor = System.Drawing.Color.White
        Me.TabPageAdmin.Location = New System.Drawing.Point(4, 23)
        Me.TabPageAdmin.Name = "TabPageAdmin"
        Me.TabPageAdmin.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAdmin.Size = New System.Drawing.Size(334, 593)
        Me.TabPageAdmin.TabIndex = 2
        Me.TabPageAdmin.Text = "Admin"
        '
        'BtnEditKonfig
        '
        Me.BtnEditKonfig.BackColor = System.Drawing.Color.Gray
        Me.BtnEditKonfig.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.BtnEditKonfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnEditKonfig.ForeColor = System.Drawing.Color.White
        Me.BtnEditKonfig.Image = Global.WinBackServerTask.My.Resources.Resources.LogFile
        Me.BtnEditKonfig.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnEditKonfig.Location = New System.Drawing.Point(228, 25)
        Me.BtnEditKonfig.Name = "BtnEditKonfig"
        Me.BtnEditKonfig.Size = New System.Drawing.Size(110, 65)
        Me.BtnEditKonfig.TabIndex = 24
        Me.BtnEditKonfig.Text = "Edit Konfig"
        Me.BtnEditKonfig.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.BtnEditKonfig.UseVisualStyleBackColor = False
        '
        'BtnLogFile
        '
        Me.BtnLogFile.BackColor = System.Drawing.Color.Gray
        Me.BtnLogFile.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.BtnLogFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnLogFile.ForeColor = System.Drawing.Color.White
        Me.BtnLogFile.Image = Global.WinBackServerTask.My.Resources.Resources.LogFile
        Me.BtnLogFile.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnLogFile.Location = New System.Drawing.Point(-1, 176)
        Me.BtnLogFile.Name = "BtnLogFile"
        Me.BtnLogFile.Size = New System.Drawing.Size(110, 65)
        Me.BtnLogFile.TabIndex = 23
        Me.BtnLogFile.Text = "Log/Messages"
        Me.BtnLogFile.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.BtnLogFile.UseVisualStyleBackColor = False
        '
        'lblServerInfo
        '
        Me.lblServerInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblServerInfo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerInfo.Location = New System.Drawing.Point(1, 553)
        Me.lblServerInfo.Name = "lblServerInfo"
        Me.lblServerInfo.Size = New System.Drawing.Size(333, 40)
        Me.lblServerInfo.TabIndex = 16
        Me.lblServerInfo.Text = "WinBack-Server 10.0.0.1"
        '
        'lblBackupRestoreStatus
        '
        Me.lblBackupRestoreStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBackupRestoreStatus.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackupRestoreStatus.ForeColor = System.Drawing.Color.Chartreuse
        Me.lblBackupRestoreStatus.Location = New System.Drawing.Point(0, 94)
        Me.lblBackupRestoreStatus.Name = "lblBackupRestoreStatus"
        Me.lblBackupRestoreStatus.Size = New System.Drawing.Size(338, 18)
        Me.lblBackupRestoreStatus.TabIndex = 14
        Me.lblBackupRestoreStatus.Text = "Daten Backup/Restore"
        '
        'lblBackupRestore
        '
        Me.lblBackupRestore.AutoSize = True
        Me.lblBackupRestore.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackupRestore.Location = New System.Drawing.Point(-4, 0)
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
        Me.BtnRestore.Location = New System.Drawing.Point(112, 25)
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
        Me.BtnBackup.Location = New System.Drawing.Point(-4, 25)
        Me.BtnBackup.Name = "BtnBackup"
        Me.BtnBackup.Size = New System.Drawing.Size(110, 65)
        Me.BtnBackup.TabIndex = 8
        Me.BtnBackup.Text = "Daten sichern"
        Me.BtnBackup.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.BtnBackup.UseVisualStyleBackColor = False
        '
        'TabPageCloud
        '
        Me.TabPageCloud.BackColor = System.Drawing.Color.Black
        Me.TabPageCloud.Controls.Add(Me.tbCloud)
        Me.TabPageCloud.Controls.Add(Me.lblCloud)
        Me.TabPageCloud.ForeColor = System.Drawing.Color.White
        Me.TabPageCloud.Location = New System.Drawing.Point(4, 23)
        Me.TabPageCloud.Name = "TabPageCloud"
        Me.TabPageCloud.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCloud.Size = New System.Drawing.Size(334, 593)
        Me.TabPageCloud.TabIndex = 3
        Me.TabPageCloud.Text = "Cloud"
        '
        'tbCloud
        '
        Me.tbCloud.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCloud.BackColor = System.Drawing.Color.Black
        Me.tbCloud.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbCloud.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCloud.ForeColor = System.Drawing.Color.White
        Me.tbCloud.Location = New System.Drawing.Point(0, 25)
        Me.tbCloud.Multiline = True
        Me.tbCloud.Name = "tbCloud"
        Me.tbCloud.ReadOnly = True
        Me.tbCloud.Size = New System.Drawing.Size(334, 565)
        Me.tbCloud.TabIndex = 2
        Me.tbCloud.TabStop = False
        Me.tbCloud.WordWrap = False
        '
        'lblCloud
        '
        Me.lblCloud.AutoSize = True
        Me.lblCloud.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCloud.Location = New System.Drawing.Point(-4, 0)
        Me.lblCloud.Name = "lblCloud"
        Me.lblCloud.Size = New System.Drawing.Size(171, 22)
        Me.lblCloud.TabIndex = 1
        Me.lblCloud.Text = "Updates Nährwerte"
        '
        'TabPageTimer
        '
        Me.TabPageTimer.BackColor = System.Drawing.Color.Black
        Me.TabPageTimer.Controls.Add(Me.tbAktionsTimer)
        Me.TabPageTimer.Controls.Add(Me.Label3)
        Me.TabPageTimer.ForeColor = System.Drawing.Color.White
        Me.TabPageTimer.Location = New System.Drawing.Point(4, 23)
        Me.TabPageTimer.Name = "TabPageTimer"
        Me.TabPageTimer.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTimer.Size = New System.Drawing.Size(334, 593)
        Me.TabPageTimer.TabIndex = 4
        Me.TabPageTimer.Text = "Timer"
        '
        'tbAktionsTimer
        '
        Me.tbAktionsTimer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbAktionsTimer.BackColor = System.Drawing.Color.DarkGray
        Me.tbAktionsTimer.ForeColor = System.Drawing.SystemColors.InfoText
        Me.tbAktionsTimer.Location = New System.Drawing.Point(-4, 35)
        Me.tbAktionsTimer.Name = "tbAktionsTimer"
        Me.tbAktionsTimer.Size = New System.Drawing.Size(342, 558)
        Me.tbAktionsTimer.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(-4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(221, 22)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Task/Timer-Einstellungen"
        '
        'Main
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(366, 855)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnHide)
        Me.Controls.Add(Me.btnMessages)
        Me.Controls.Add(Me.BtnClients)
        Me.Controls.Add(Me.lblServerStatus)
        Me.Controls.Add(Me.BtnTimer)
        Me.Controls.Add(Me.BtnCloud)
        Me.Controls.Add(Me.BtnAdmin)
        Me.Controls.Add(Me.Wb_TabControl)
        Me.Controls.Add(Me.BtnExit)
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
        Me.TabPageCloud.ResumeLayout(False)
        Me.TabPageCloud.PerformLayout()
        Me.TabPageTimer.ResumeLayout(False)
        Me.TabPageTimer.PerformLayout()
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
    Friend WithEvents lbClientList As ListBox
    Friend WithEvents tbMessages As TextBox
    Friend WithEvents lblClients As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents BtnCloud As Button
    Friend WithEvents TabPageCloud As TabPage
    Friend WithEvents lblCloud As Label
    Friend WithEvents BtnTimer As Button
    Friend WithEvents RemoveTextTimer As Timer
    Friend WithEvents lblServerStatus As Label
    Public WithEvents tbCloud As TextBox
    Friend WithEvents TabPageTimer As TabPage
    Friend WithEvents Label3 As Label
    Friend WithEvents tbAktionsTimer As Panel
    Friend WithEvents lblBackupRestoreStatus As Label
    Friend WithEvents lblServerInfo As Label
    Friend WithEvents BtnLogFile As Button
    Friend WithEvents BtnEditKonfig As Button
End Class
