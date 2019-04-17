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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.tbOrgaBackUpdate = New System.Windows.Forms.TextBox()
        Me.tbWinbackUpdate = New System.Windows.Forms.TextBox()
        Me.tbOrgaBack = New System.Windows.Forms.TextBox()
        Me.tbWinBack = New System.Windows.Forms.TextBox()
        Me.lblOrgaBack = New System.Windows.Forms.Label()
        Me.lblWinBack = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(326, 107)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(90, 21)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "CheckMe"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'tbOrgaBackUpdate
        '
        Me.tbOrgaBackUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBackUpdate.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBackUpdate.Location = New System.Drawing.Point(293, 44)
        Me.tbOrgaBackUpdate.Name = "tbOrgaBackUpdate"
        Me.tbOrgaBackUpdate.Size = New System.Drawing.Size(123, 20)
        Me.tbOrgaBackUpdate.TabIndex = 20
        Me.tbOrgaBackUpdate.TabStop = False
        '
        'tbWinbackUpdate
        '
        Me.tbWinbackUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.tbWinbackUpdate.ForeColor = System.Drawing.Color.DarkGray
        Me.tbWinbackUpdate.Location = New System.Drawing.Point(293, 18)
        Me.tbWinbackUpdate.Name = "tbWinbackUpdate"
        Me.tbWinbackUpdate.Size = New System.Drawing.Size(123, 20)
        Me.tbWinbackUpdate.TabIndex = 19
        Me.tbWinbackUpdate.TabStop = False
        '
        'tbOrgaBack
        '
        Me.tbOrgaBack.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBack.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBack.Location = New System.Drawing.Point(164, 44)
        Me.tbOrgaBack.Name = "tbOrgaBack"
        Me.tbOrgaBack.Size = New System.Drawing.Size(123, 20)
        Me.tbOrgaBack.TabIndex = 18
        Me.tbOrgaBack.TabStop = False
        '
        'tbWinBack
        '
        Me.tbWinBack.BackColor = System.Drawing.SystemColors.Control
        Me.tbWinBack.ForeColor = System.Drawing.Color.DarkGray
        Me.tbWinBack.Location = New System.Drawing.Point(164, 18)
        Me.tbWinBack.Name = "tbWinBack"
        Me.tbWinBack.Size = New System.Drawing.Size(123, 20)
        Me.tbWinBack.TabIndex = 17
        Me.tbWinBack.TabStop = False
        '
        'lblOrgaBack
        '
        Me.lblOrgaBack.AutoSize = True
        Me.lblOrgaBack.Location = New System.Drawing.Point(11, 46)
        Me.lblOrgaBack.Name = "lblOrgaBack"
        Me.lblOrgaBack.Size = New System.Drawing.Size(151, 13)
        Me.lblOrgaBack.TabIndex = 16
        Me.lblOrgaBack.Text = "Version OrgaBack (notwendig)"
        '
        'lblWinBack
        '
        Me.lblWinBack.AutoSize = True
        Me.lblWinBack.Location = New System.Drawing.Point(11, 21)
        Me.lblWinBack.Name = "lblWinBack"
        Me.lblWinBack.Size = New System.Drawing.Size(134, 13)
        Me.lblWinBack.TabIndex = 15
        Me.lblWinBack.Text = "Version WinBack (Internet)"
        '
        'wb_Admin_UpdateWinBack
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(457, 162)
        Me.Controls.Add(Me.tbOrgaBackUpdate)
        Me.Controls.Add(Me.tbWinbackUpdate)
        Me.Controls.Add(Me.tbOrgaBack)
        Me.Controls.Add(Me.tbWinBack)
        Me.Controls.Add(Me.lblOrgaBack)
        Me.Controls.Add(Me.lblWinBack)
        Me.Controls.Add(Me.Button1)
        Me.Name = "wb_Admin_UpdateWinBack"
        Me.Text = "Update WinBack"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents tbOrgaBackUpdate As Windows.Forms.TextBox
    Friend WithEvents tbWinbackUpdate As Windows.Forms.TextBox
    Friend WithEvents tbOrgaBack As Windows.Forms.TextBox
    Friend WithEvents tbWinBack As Windows.Forms.TextBox
    Friend WithEvents lblOrgaBack As Windows.Forms.Label
    Friend WithEvents lblWinBack As Windows.Forms.Label
End Class
