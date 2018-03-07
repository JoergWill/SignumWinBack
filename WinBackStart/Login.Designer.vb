<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
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
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.tUserNummer = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnAbbruch = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(171, 55)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(107, 37)
        Me.BtnOK.TabIndex = 1
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'tUserNummer
        '
        Me.tUserNummer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tUserNummer.Location = New System.Drawing.Point(56, 12)
        Me.tUserNummer.Name = "tUserNummer"
        Me.tUserNummer.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tUserNummer.Size = New System.Drawing.Size(222, 20)
        Me.tUserNummer.TabIndex = 0
        Me.tUserNummer.WordWrap = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(17, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Login"
        '
        'BtnAbbruch
        '
        Me.BtnAbbruch.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnAbbruch.Location = New System.Drawing.Point(20, 55)
        Me.BtnAbbruch.Name = "BtnAbbruch"
        Me.BtnAbbruch.Size = New System.Drawing.Size(107, 37)
        Me.BtnAbbruch.TabIndex = 4
        Me.BtnAbbruch.Text = "Abbruch"
        Me.BtnAbbruch.UseVisualStyleBackColor = True
        '
        'Login
        '
        Me.AcceptButton = Me.BtnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(299, 107)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnAbbruch)
        Me.Controls.Add(Me.tUserNummer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "Login"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents tUserNummer As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents BtnAbbruch As Button
End Class
