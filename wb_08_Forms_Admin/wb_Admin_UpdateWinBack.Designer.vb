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
        BtnCheckUpdate = New System.Windows.Forms.Button()
        tbOrgaBackUpdate = New System.Windows.Forms.TextBox()
        tbWinbackUpdate = New System.Windows.Forms.TextBox()
        tbOrgaBack = New System.Windows.Forms.TextBox()
        tbWinBack = New System.Windows.Forms.TextBox()
        lblOrgaBack = New System.Windows.Forms.Label()
        lblWinBack = New System.Windows.Forms.Label()
        BtnUpdate = New System.Windows.Forms.Button()
        tbWinBackBit = New System.Windows.Forms.TextBox()
        tbOSBit = New System.Windows.Forms.TextBox()
        lblOs = New System.Windows.Forms.Label()
        tbOrgaBackBit = New System.Windows.Forms.TextBox()
        tbWindowsVersion = New System.Windows.Forms.TextBox()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        SuspendLayout()
        ' 
        ' BtnCheckUpdate
        ' 
        BtnCheckUpdate.Location = New System.Drawing.Point(212, 117)
        BtnCheckUpdate.Name = "BtnCheckUpdate"
        BtnCheckUpdate.Size = New System.Drawing.Size(123, 33)
        BtnCheckUpdate.TabIndex = 0
        BtnCheckUpdate.Text = "Update prüfen"
        BtnCheckUpdate.UseVisualStyleBackColor = True
        ' 
        ' tbOrgaBackUpdate
        ' 
        tbOrgaBackUpdate.BackColor = Drawing.SystemColors.Control
        tbOrgaBackUpdate.ForeColor = Drawing.Color.Black
        tbOrgaBackUpdate.Location = New System.Drawing.Point(341, 78)
        tbOrgaBackUpdate.Name = "tbOrgaBackUpdate"
        tbOrgaBackUpdate.Size = New System.Drawing.Size(123, 20)
        tbOrgaBackUpdate.TabIndex = 20
        tbOrgaBackUpdate.TabStop = False
        ' 
        ' tbWinbackUpdate
        ' 
        tbWinbackUpdate.BackColor = Drawing.SystemColors.Control
        tbWinbackUpdate.ForeColor = Drawing.Color.Black
        tbWinbackUpdate.Location = New System.Drawing.Point(341, 52)
        tbWinbackUpdate.Name = "tbWinbackUpdate"
        tbWinbackUpdate.Size = New System.Drawing.Size(123, 20)
        tbWinbackUpdate.TabIndex = 19
        tbWinbackUpdate.TabStop = False
        ' 
        ' tbOrgaBack
        ' 
        tbOrgaBack.BackColor = Drawing.SystemColors.Control
        tbOrgaBack.ForeColor = Drawing.Color.Black
        tbOrgaBack.Location = New System.Drawing.Point(212, 78)
        tbOrgaBack.Name = "tbOrgaBack"
        tbOrgaBack.Size = New System.Drawing.Size(123, 20)
        tbOrgaBack.TabIndex = 18
        tbOrgaBack.TabStop = False
        ' 
        ' tbWinBack
        ' 
        tbWinBack.BackColor = Drawing.SystemColors.Control
        tbWinBack.ForeColor = Drawing.Color.Black
        tbWinBack.Location = New System.Drawing.Point(212, 52)
        tbWinBack.Name = "tbWinBack"
        tbWinBack.Size = New System.Drawing.Size(123, 20)
        tbWinBack.TabIndex = 17
        tbWinBack.TabStop = False
        ' 
        ' lblOrgaBack
        ' 
        lblOrgaBack.AutoSize = True
        lblOrgaBack.Location = New System.Drawing.Point(12, 81)
        lblOrgaBack.Name = "lblOrgaBack"
        lblOrgaBack.Size = New System.Drawing.Size(151, 13)
        lblOrgaBack.TabIndex = 16
        lblOrgaBack.Text = "Version OrgaBack (notwendig)"
        ' 
        ' lblWinBack
        ' 
        lblWinBack.AutoSize = True
        lblWinBack.Location = New System.Drawing.Point(12, 55)
        lblWinBack.Name = "lblWinBack"
        lblWinBack.Size = New System.Drawing.Size(124, 13)
        lblWinBack.TabIndex = 15
        lblWinBack.Text = "Version OrgaBack-Office"
        ' 
        ' BtnUpdate
        ' 
        BtnUpdate.Location = New System.Drawing.Point(341, 117)
        BtnUpdate.Name = "BtnUpdate"
        BtnUpdate.Size = New System.Drawing.Size(123, 33)
        BtnUpdate.TabIndex = 21
        BtnUpdate.Text = "Update"
        BtnUpdate.UseVisualStyleBackColor = True
        ' 
        ' tbWinBackBit
        ' 
        tbWinBackBit.BackColor = Drawing.SystemColors.Control
        tbWinBackBit.ForeColor = Drawing.Color.Black
        tbWinBackBit.Location = New System.Drawing.Point(167, 52)
        tbWinBackBit.Name = "tbWinBackBit"
        tbWinBackBit.Size = New System.Drawing.Size(39, 20)
        tbWinBackBit.TabIndex = 22
        tbWinBackBit.TabStop = False
        ' 
        ' tbOSBit
        ' 
        tbOSBit.BackColor = Drawing.SystemColors.Control
        tbOSBit.ForeColor = Drawing.Color.Black
        tbOSBit.Location = New System.Drawing.Point(167, 11)
        tbOSBit.Name = "tbOSBit"
        tbOSBit.Size = New System.Drawing.Size(39, 20)
        tbOSBit.TabIndex = 24
        tbOSBit.TabStop = False
        ' 
        ' lblOs
        ' 
        lblOs.AutoSize = True
        lblOs.Location = New System.Drawing.Point(12, 14)
        lblOs.Name = "lblOs"
        lblOs.Size = New System.Drawing.Size(82, 13)
        lblOs.TabIndex = 23
        lblOs.Text = "Betriebs-System"
        ' 
        ' tbOrgaBackBit
        ' 
        tbOrgaBackBit.BackColor = Drawing.SystemColors.Control
        tbOrgaBackBit.ForeColor = Drawing.Color.Black
        tbOrgaBackBit.Location = New System.Drawing.Point(167, 78)
        tbOrgaBackBit.Name = "tbOrgaBackBit"
        tbOrgaBackBit.Size = New System.Drawing.Size(39, 20)
        tbOrgaBackBit.TabIndex = 25
        tbOrgaBackBit.TabStop = False
        ' 
        ' tbWindowsVersion
        ' 
        tbWindowsVersion.BackColor = Drawing.SystemColors.Control
        tbWindowsVersion.ForeColor = Drawing.Color.Black
        tbWindowsVersion.Location = New System.Drawing.Point(212, 11)
        tbWindowsVersion.Name = "tbWindowsVersion"
        tbWindowsVersion.Size = New System.Drawing.Size(252, 20)
        tbWindowsVersion.TabIndex = 26
        tbWindowsVersion.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(215, 36)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(48, 13)
        Label1.TabIndex = 27
        Label1.Text = "Installiert"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(344, 36)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(78, 13)
        Label2.TabIndex = 28
        Label2.Text = "Update(Online)"
        ' 
        ' wb_Admin_UpdateWinBack
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(535, 162)
        Controls.Add(tbWindowsVersion)
        Controls.Add(tbOrgaBackBit)
        Controls.Add(tbOSBit)
        Controls.Add(lblOs)
        Controls.Add(tbWinBackBit)
        Controls.Add(BtnUpdate)
        Controls.Add(tbOrgaBackUpdate)
        Controls.Add(tbWinbackUpdate)
        Controls.Add(tbOrgaBack)
        Controls.Add(tbWinBack)
        Controls.Add(lblOrgaBack)
        Controls.Add(lblWinBack)
        Controls.Add(BtnCheckUpdate)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Name = "wb_Admin_UpdateWinBack"
        Text = "Update WinBack"
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents BtnCheckUpdate As System.Windows.Forms.Button
    Friend WithEvents tbOrgaBackUpdate As System.Windows.Forms.TextBox
    Friend WithEvents tbWinbackUpdate As System.Windows.Forms.TextBox
    Friend WithEvents tbOrgaBack As System.Windows.Forms.TextBox
    Friend WithEvents tbWinBack As System.Windows.Forms.TextBox
    Friend WithEvents lblOrgaBack As System.Windows.Forms.Label
    Friend WithEvents lblWinBack As System.Windows.Forms.Label
    Friend WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents tbWinBackBit As System.Windows.Forms.TextBox
    Friend WithEvents tbOSBit As System.Windows.Forms.TextBox
    Friend WithEvents lblOs As System.Windows.Forms.Label
    Friend WithEvents tbOrgaBackBit As System.Windows.Forms.TextBox
    Friend WithEvents tbWindowsVersion As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
