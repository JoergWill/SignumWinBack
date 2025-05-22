<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Login
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        BtnOK = New Button()
        tUserNummer = New TextBox()
        Label1 = New Label()
        BtnAbbruch = New Button()
        pbLogo = New PictureBox()
        CType(pbLogo, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Location = New Point(286, 228)
        BtnOK.Margin = New Padding(6, 5, 6, 5)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(179, 72)
        BtnOK.TabIndex = 1
        BtnOK.Text = "OK"
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' tUserNummer
        ' 
        tUserNummer.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tUserNummer.Location = New Point(93, 23)
        tUserNummer.Margin = New Padding(6, 5, 6, 5)
        tUserNummer.Name = "tUserNummer"
        tUserNummer.PasswordChar = "*"c
        tUserNummer.Size = New Size(367, 31)
        tUserNummer.TabIndex = 0
        tUserNummer.WordWrap = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.ImeMode = ImeMode.NoControl
        Label1.Location = New Point(29, 28)
        Label1.Margin = New Padding(6, 0, 6, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(56, 25)
        Label1.TabIndex = 3
        Label1.Text = "Login"
        ' 
        ' BtnAbbruch
        ' 
        BtnAbbruch.DialogResult = DialogResult.Cancel
        BtnAbbruch.Location = New Point(286, 125)
        BtnAbbruch.Margin = New Padding(6, 5, 6, 5)
        BtnAbbruch.Name = "BtnAbbruch"
        BtnAbbruch.Size = New Size(179, 72)
        BtnAbbruch.TabIndex = 4
        BtnAbbruch.Text = "Abbruch"
        BtnAbbruch.UseVisualStyleBackColor = True
        ' 
        ' pbLogo
        ' 
        pbLogo.Image = CType(resources.GetObject("pbLogo.Image"), Image)
        pbLogo.Location = New Point(33, 97)
        pbLogo.Margin = New Padding(6, 5, 6, 5)
        pbLogo.Name = "pbLogo"
        pbLogo.Size = New Size(240, 240)
        pbLogo.SizeMode = PictureBoxSizeMode.StretchImage
        pbLogo.TabIndex = 5
        pbLogo.TabStop = False
        ' 
        ' Login
        ' 
        AcceptButton = BtnOK
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(499, 422)
        ControlBox = False
        Controls.Add(pbLogo)
        Controls.Add(BtnAbbruch)
        Controls.Add(tUserNummer)
        Controls.Add(Label1)
        Controls.Add(BtnOK)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(6, 5, 6, 5)
        Name = "Login"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "Login"
        CType(pbLogo, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents tUserNummer As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents BtnAbbruch As Button
    Friend WithEvents pbLogo As PictureBox
End Class
