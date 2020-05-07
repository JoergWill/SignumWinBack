Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_User_Passwort
    Inherits DockContent
    'Inherits System.Windows.Forms.Form

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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tPersonalNr = New System.Windows.Forms.TextBox()
        Me.tUserPassAlt = New System.Windows.Forms.TextBox()
        Me.tUserName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbUserPassNeu_B = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbUserPassNeu_A = New System.Windows.Forms.TextBox()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnAbbruch = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(9, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Benutzer Name:"
        '
        'tPersonalNr
        '
        Me.tPersonalNr.Location = New System.Drawing.Point(290, 22)
        Me.tPersonalNr.Name = "tPersonalNr"
        Me.tPersonalNr.ReadOnly = True
        Me.tPersonalNr.Size = New System.Drawing.Size(180, 20)
        Me.tPersonalNr.TabIndex = 5
        Me.tPersonalNr.TabStop = False
        '
        'tUserPassAlt
        '
        Me.tUserPassAlt.Location = New System.Drawing.Point(12, 63)
        Me.tUserPassAlt.Name = "tUserPassAlt"
        Me.tUserPassAlt.Size = New System.Drawing.Size(97, 20)
        Me.tUserPassAlt.TabIndex = 1
        Me.tUserPassAlt.UseSystemPasswordChar = True
        '
        'tUserName
        '
        Me.tUserName.Location = New System.Drawing.Point(12, 22)
        Me.tUserName.Name = "tUserName"
        Me.tUserName.ReadOnly = True
        Me.tUserName.Size = New System.Drawing.Size(272, 20)
        Me.tUserName.TabIndex = 2
        Me.tUserName.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(9, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "altes Passwort"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(287, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Personal-Nummer"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(9, 128)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(142, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "neues Passwort wiederholen"
        '
        'tbUserPassNeu_B
        '
        Me.tbUserPassNeu_B.Location = New System.Drawing.Point(12, 145)
        Me.tbUserPassNeu_B.Name = "tbUserPassNeu_B"
        Me.tbUserPassNeu_B.Size = New System.Drawing.Size(97, 20)
        Me.tbUserPassNeu_B.TabIndex = 3
        Me.tbUserPassNeu_B.UseSystemPasswordChar = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(9, 87)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "neues Passwort"
        '
        'tbUserPassNeu_A
        '
        Me.tbUserPassNeu_A.Location = New System.Drawing.Point(12, 104)
        Me.tbUserPassNeu_A.Name = "tbUserPassNeu_A"
        Me.tbUserPassNeu_A.Size = New System.Drawing.Size(97, 20)
        Me.tbUserPassNeu_A.TabIndex = 2
        Me.tbUserPassNeu_A.UseSystemPasswordChar = True
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(368, 128)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(102, 40)
        Me.BtnOK.TabIndex = 4
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'BtnAbbruch
        '
        Me.BtnAbbruch.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnAbbruch.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnAbbruch.Location = New System.Drawing.Point(260, 128)
        Me.BtnAbbruch.Name = "BtnAbbruch"
        Me.BtnAbbruch.Size = New System.Drawing.Size(102, 40)
        Me.BtnAbbruch.TabIndex = 5
        Me.BtnAbbruch.Text = "Abbruch"
        Me.BtnAbbruch.UseVisualStyleBackColor = True
        '
        'wb_User_Passwort
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnAbbruch
        Me.ClientSize = New System.Drawing.Size(478, 177)
        Me.CloseButton = False
        Me.CloseButtonVisible = False
        Me.Controls.Add(Me.BtnAbbruch)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tbUserPassNeu_A)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbUserPassNeu_B)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tPersonalNr)
        Me.Controls.Add(Me.tUserPassAlt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tUserName)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wb_User_Passwort"
        Me.Text = "Passwort ändern"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents tUserName As Windows.Forms.TextBox
    Friend WithEvents tUserPassAlt As Windows.Forms.TextBox
    Friend WithEvents tPersonalNr As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tbUserPassNeu_B As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents tbUserPassNeu_A As Windows.Forms.TextBox
    Friend WithEvents BtnOK As Windows.Forms.Button
    Friend WithEvents BtnAbbruch As Windows.Forms.Button
End Class
