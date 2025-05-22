<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetMySqlIP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SetMySqlIP))
        Label1 = New Label()
        Label2 = New Label()
        cbAdressen = New ComboBox()
        BtnOK = New Button()
        BtnScan = New Button()
        BtnCancel = New Button()
        Label3 = New Label()
        pbLogo = New PictureBox()
        lblInfo = New Label()
        CType(pbLogo, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.ImeMode = ImeMode.NoControl
        Label1.Location = New Point(16, 54)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(424, 31)
        Label1.TabIndex = 7
        Label1.Text = "Nach der Software-Installation muss noch die IP-Adresse des WinBack-Servers eingetragen werden"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.ImeMode = ImeMode.NoControl
        Label2.Location = New Point(14, 10)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(326, 25)
        Label2.TabIndex = 8
        Label2.Text = "Installation OrgaBack-Produktion"
        ' 
        ' cbAdressen
        ' 
        cbAdressen.FormattingEnabled = True
        cbAdressen.Location = New Point(194, 119)
        cbAdressen.Margin = New Padding(4, 3, 4, 3)
        cbAdressen.Name = "cbAdressen"
        cbAdressen.Size = New Size(172, 23)
        cbAdressen.TabIndex = 9
        ' 
        ' BtnOK
        ' 
        BtnOK.DialogResult = DialogResult.OK
        BtnOK.Enabled = False
        BtnOK.Location = New Point(389, 101)
        BtnOK.Margin = New Padding(4, 3, 4, 3)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(111, 50)
        BtnOK.TabIndex = 10
        BtnOK.Text = "Übernehmen"
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' BtnScan
        ' 
        BtnScan.Location = New Point(389, 157)
        BtnScan.Margin = New Padding(4, 3, 4, 3)
        BtnScan.Name = "BtnScan"
        BtnScan.Size = New Size(111, 50)
        BtnScan.TabIndex = 11
        BtnScan.Text = "Scan"
        BtnScan.UseVisualStyleBackColor = True
        ' 
        ' BtnCancel
        ' 
        BtnCancel.DialogResult = DialogResult.Cancel
        BtnCancel.Location = New Point(389, 213)
        BtnCancel.Margin = New Padding(4, 3, 4, 3)
        BtnCancel.Name = "BtnCancel"
        BtnCancel.Size = New Size(111, 50)
        BtnCancel.TabIndex = 12
        BtnCancel.Text = "Abbrechen"
        BtnCancel.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.ImeMode = ImeMode.NoControl
        Label3.Location = New Point(194, 101)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(63, 15)
        Label3.TabIndex = 13
        Label3.Text = "IP-Adresse"
        ' 
        ' pbLogo
        ' 
        pbLogo.Image = CType(resources.GetObject("pbLogo.Image"), Image)
        pbLogo.Location = New Point(16, 101)
        pbLogo.Margin = New Padding(4, 3, 4, 3)
        pbLogo.Name = "pbLogo"
        pbLogo.Size = New Size(146, 144)
        pbLogo.SizeMode = PictureBoxSizeMode.StretchImage
        pbLogo.TabIndex = 6
        pbLogo.TabStop = False
        ' 
        ' lblInfo
        ' 
        lblInfo.ImeMode = ImeMode.NoControl
        lblInfo.Location = New Point(190, 157)
        lblInfo.Margin = New Padding(4, 0, 4, 0)
        lblInfo.Name = "lblInfo"
        lblInfo.Size = New Size(176, 40)
        lblInfo.TabIndex = 14
        lblInfo.Text = "IP-Adresse eintragen oder scannen ..."
        ' 
        ' SetMySqlIP
        ' 
        AcceptButton = BtnOK
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(559, 357)
        ControlBox = False
        Controls.Add(lblInfo)
        Controls.Add(Label3)
        Controls.Add(BtnCancel)
        Controls.Add(BtnScan)
        Controls.Add(BtnOK)
        Controls.Add(cbAdressen)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(pbLogo)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(4, 3, 4, 3)
        Name = "SetMySqlIP"
        Text = "Abschluss der Installation"
        CType(pbLogo, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents pbLogo As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cbAdressen As ComboBox
    Friend WithEvents BtnOK As Button
    Friend WithEvents BtnScan As Button
    Friend WithEvents BtnCancel As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents lblInfo As Label
End Class
