<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MainTimer = New System.Windows.Forms.Timer(Me.components)
        Me.lblCounter = New System.Windows.Forms.Label()
        Me.BtnClients = New System.Windows.Forms.Button()
        Me.btnMessages = New System.Windows.Forms.Button()
        Me.BtnHide = New System.Windows.Forms.Button()
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
        Me.Label1.Location = New System.Drawing.Point(8, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(214, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "WinBack-Server-Task"
        '
        'MainTimer
        '
        Me.MainTimer.Enabled = True
        Me.MainTimer.Interval = 1000
        '
        'lblCounter
        '
        Me.lblCounter.AutoSize = True
        Me.lblCounter.ForeColor = System.Drawing.Color.White
        Me.lblCounter.Location = New System.Drawing.Point(172, 39)
        Me.lblCounter.Name = "lblCounter"
        Me.lblCounter.Size = New System.Drawing.Size(31, 13)
        Me.lblCounter.TabIndex = 1
        Me.lblCounter.Text = "0000"
        '
        'BtnClients
        '
        Me.BtnClients.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnClients.BackColor = System.Drawing.Color.Gray
        Me.BtnClients.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.BtnClients.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClients.ForeColor = System.Drawing.Color.White
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
        Me.BtnHide.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnHide.Location = New System.Drawing.Point(244, 611)
        Me.BtnHide.Name = "BtnHide"
        Me.BtnHide.Size = New System.Drawing.Size(110, 65)
        Me.BtnHide.TabIndex = 4
        Me.BtnHide.Text = "Hide"
        Me.BtnHide.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.BtnHide.UseVisualStyleBackColor = False
        '
        'Main
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(366, 688)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnHide)
        Me.Controls.Add(Me.btnMessages)
        Me.Controls.Add(Me.BtnClients)
        Me.Controls.Add(Me.lblCounter)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main"
        Me.ShowInTaskbar = False
        Me.Text = "WinBack"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents NotifyIcon As NotifyIcon
    Friend WithEvents Label1 As Label
    Friend WithEvents MainTimer As Timer
    Friend WithEvents lblCounter As Label
    Friend WithEvents BtnClients As Button
    Friend WithEvents btnMessages As Button
    Friend WithEvents BtnHide As Button
End Class
