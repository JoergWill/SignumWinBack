Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_UpdateWinBack
    Inherits DockContent
    'Inherits System.Windows.Forms.Form

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
        Me.BtnCheckUpdate = New System.Windows.Forms.Button()
        Me.tbOrgaBackUpdate = New System.Windows.Forms.TextBox()
        Me.tbWinbackUpdate = New System.Windows.Forms.TextBox()
        Me.tbOrgaBack = New System.Windows.Forms.TextBox()
        Me.tbWinBack = New System.Windows.Forms.TextBox()
        Me.lblOrgaBack = New System.Windows.Forms.Label()
        Me.lblWinBack = New System.Windows.Forms.Label()
        Me.BtnUpdate = New System.Windows.Forms.Button()
        Me.tbWinBackBit = New System.Windows.Forms.TextBox()
        Me.tbOSBit = New System.Windows.Forms.TextBox()
        Me.lblOs = New System.Windows.Forms.Label()
        Me.tbOrgaBackBit = New System.Windows.Forms.TextBox()
        Me.tbWindowsVersion = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'BtnCheckUpdate
        '
        Me.BtnCheckUpdate.Location = New System.Drawing.Point(212, 117)
        Me.BtnCheckUpdate.Name = "BtnCheckUpdate"
        Me.BtnCheckUpdate.Size = New System.Drawing.Size(123, 33)
        Me.BtnCheckUpdate.TabIndex = 0
        Me.BtnCheckUpdate.Text = "Update prüfen"
        Me.BtnCheckUpdate.UseVisualStyleBackColor = True
        '
        'tbOrgaBackUpdate
        '
        Me.tbOrgaBackUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBackUpdate.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBackUpdate.Location = New System.Drawing.Point(341, 63)
        Me.tbOrgaBackUpdate.Name = "tbOrgaBackUpdate"
        Me.tbOrgaBackUpdate.Size = New System.Drawing.Size(123, 20)
        Me.tbOrgaBackUpdate.TabIndex = 20
        Me.tbOrgaBackUpdate.TabStop = False
        '
        'tbWinbackUpdate
        '
        Me.tbWinbackUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.tbWinbackUpdate.ForeColor = System.Drawing.Color.DarkGray
        Me.tbWinbackUpdate.Location = New System.Drawing.Point(341, 37)
        Me.tbWinbackUpdate.Name = "tbWinbackUpdate"
        Me.tbWinbackUpdate.Size = New System.Drawing.Size(123, 20)
        Me.tbWinbackUpdate.TabIndex = 19
        Me.tbWinbackUpdate.TabStop = False
        '
        'tbOrgaBack
        '
        Me.tbOrgaBack.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBack.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBack.Location = New System.Drawing.Point(212, 63)
        Me.tbOrgaBack.Name = "tbOrgaBack"
        Me.tbOrgaBack.Size = New System.Drawing.Size(123, 20)
        Me.tbOrgaBack.TabIndex = 18
        Me.tbOrgaBack.TabStop = False
        '
        'tbWinBack
        '
        Me.tbWinBack.BackColor = System.Drawing.SystemColors.Control
        Me.tbWinBack.ForeColor = System.Drawing.Color.DarkGray
        Me.tbWinBack.Location = New System.Drawing.Point(212, 37)
        Me.tbWinBack.Name = "tbWinBack"
        Me.tbWinBack.Size = New System.Drawing.Size(123, 20)
        Me.tbWinBack.TabIndex = 17
        Me.tbWinBack.TabStop = False
        '
        'lblOrgaBack
        '
        Me.lblOrgaBack.AutoSize = True
        Me.lblOrgaBack.Location = New System.Drawing.Point(12, 66)
        Me.lblOrgaBack.Name = "lblOrgaBack"
        Me.lblOrgaBack.Size = New System.Drawing.Size(151, 13)
        Me.lblOrgaBack.TabIndex = 16
        Me.lblOrgaBack.Text = "Version OrgaBack (notwendig)"
        '
        'lblWinBack
        '
        Me.lblWinBack.AutoSize = True
        Me.lblWinBack.Location = New System.Drawing.Point(12, 40)
        Me.lblWinBack.Name = "lblWinBack"
        Me.lblWinBack.Size = New System.Drawing.Size(134, 13)
        Me.lblWinBack.TabIndex = 15
        Me.lblWinBack.Text = "Version WinBack (Internet)"
        '
        'BtnUpdate
        '
        Me.BtnUpdate.Location = New System.Drawing.Point(341, 117)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(123, 33)
        Me.BtnUpdate.TabIndex = 21
        Me.BtnUpdate.Text = "Update"
        Me.BtnUpdate.UseVisualStyleBackColor = True
        '
        'tbWinBackBit
        '
        Me.tbWinBackBit.BackColor = System.Drawing.SystemColors.Control
        Me.tbWinBackBit.ForeColor = System.Drawing.Color.DarkGray
        Me.tbWinBackBit.Location = New System.Drawing.Point(167, 11)
        Me.tbWinBackBit.Name = "tbWinBackBit"
        Me.tbWinBackBit.Size = New System.Drawing.Size(39, 20)
        Me.tbWinBackBit.TabIndex = 22
        Me.tbWinBackBit.TabStop = False
        '
        'tbOSBit
        '
        Me.tbOSBit.BackColor = System.Drawing.SystemColors.Control
        Me.tbOSBit.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOSBit.Location = New System.Drawing.Point(167, 37)
        Me.tbOSBit.Name = "tbOSBit"
        Me.tbOSBit.Size = New System.Drawing.Size(39, 20)
        Me.tbOSBit.TabIndex = 24
        Me.tbOSBit.TabStop = False
        '
        'lblOs
        '
        Me.lblOs.AutoSize = True
        Me.lblOs.Location = New System.Drawing.Point(12, 14)
        Me.lblOs.Name = "lblOs"
        Me.lblOs.Size = New System.Drawing.Size(82, 13)
        Me.lblOs.TabIndex = 23
        Me.lblOs.Text = "Betriebs-System"
        '
        'tbOrgaBackBit
        '
        Me.tbOrgaBackBit.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBackBit.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBackBit.Location = New System.Drawing.Point(167, 63)
        Me.tbOrgaBackBit.Name = "tbOrgaBackBit"
        Me.tbOrgaBackBit.Size = New System.Drawing.Size(39, 20)
        Me.tbOrgaBackBit.TabIndex = 25
        Me.tbOrgaBackBit.TabStop = False
        '
        'tbWindowsVersion
        '
        Me.tbWindowsVersion.BackColor = System.Drawing.SystemColors.Control
        Me.tbWindowsVersion.ForeColor = System.Drawing.Color.DarkGray
        Me.tbWindowsVersion.Location = New System.Drawing.Point(212, 11)
        Me.tbWindowsVersion.Name = "tbWindowsVersion"
        Me.tbWindowsVersion.Size = New System.Drawing.Size(252, 20)
        Me.tbWindowsVersion.TabIndex = 26
        Me.tbWindowsVersion.TabStop = False
        '
        'wb_Admin_UpdateWinBack
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(535, 162)
        Me.Controls.Add(Me.tbWindowsVersion)
        Me.Controls.Add(Me.tbOrgaBackBit)
        Me.Controls.Add(Me.tbOSBit)
        Me.Controls.Add(Me.lblOs)
        Me.Controls.Add(Me.tbWinBackBit)
        Me.Controls.Add(Me.BtnUpdate)
        Me.Controls.Add(Me.tbOrgaBackUpdate)
        Me.Controls.Add(Me.tbWinbackUpdate)
        Me.Controls.Add(Me.tbOrgaBack)
        Me.Controls.Add(Me.tbWinBack)
        Me.Controls.Add(Me.lblOrgaBack)
        Me.Controls.Add(Me.lblWinBack)
        Me.Controls.Add(Me.BtnCheckUpdate)
        Me.Name = "wb_Admin_UpdateWinBack"
        Me.Text = "Update WinBack"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnCheckUpdate As Windows.Forms.Button
    Friend WithEvents tbOrgaBackUpdate As Windows.Forms.TextBox
    Friend WithEvents tbWinbackUpdate As Windows.Forms.TextBox
    Friend WithEvents tbOrgaBack As Windows.Forms.TextBox
    Friend WithEvents tbWinBack As Windows.Forms.TextBox
    Friend WithEvents lblOrgaBack As Windows.Forms.Label
    Friend WithEvents lblWinBack As Windows.Forms.Label
    Friend WithEvents BtnUpdate As Windows.Forms.Button
    Friend WithEvents tbWinBackBit As Windows.Forms.TextBox
    Friend WithEvents tbOSBit As Windows.Forms.TextBox
    Friend WithEvents lblOs As Windows.Forms.Label
    Friend WithEvents tbOrgaBackBit As Windows.Forms.TextBox
    Friend WithEvents tbWindowsVersion As Windows.Forms.TextBox
End Class
