<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_Main_Exception
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Main_Exception))
        BtnExit = New System.Windows.Forms.Button()
        BtnRestart = New System.Windows.Forms.Button()
        BtnContinue = New System.Windows.Forms.Button()
        PnlPicture = New System.Windows.Forms.Panel()
        tbException = New System.Windows.Forms.TextBox()
        lblText = New System.Windows.Forms.Label()
        BtnMail = New System.Windows.Forms.Button()
        BtnShow = New System.Windows.Forms.Button()
        PnlPicture.SuspendLayout()
        SuspendLayout()
        ' 
        ' BtnExit
        ' 
        BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
        BtnExit.DialogResult = System.Windows.Forms.DialogResult.Abort
        BtnExit.Image = CType(resources.GetObject("BtnExit.Image"), Drawing.Image)
        BtnExit.Location = New System.Drawing.Point(431, 383)
        BtnExit.Name = "BtnExit"
        BtnExit.Size = New System.Drawing.Size(120, 45)
        BtnExit.TabIndex = 4
        BtnExit.Text = "WinBack beenden"
        BtnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnExit.UseVisualStyleBackColor = True
        ' 
        ' BtnRestart
        ' 
        BtnRestart.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
        BtnRestart.DialogResult = System.Windows.Forms.DialogResult.Retry
        BtnRestart.Image = CType(resources.GetObject("BtnRestart.Image"), Drawing.Image)
        BtnRestart.ImeMode = System.Windows.Forms.ImeMode.NoControl
        BtnRestart.Location = New System.Drawing.Point(431, 332)
        BtnRestart.Name = "BtnRestart"
        BtnRestart.Size = New System.Drawing.Size(120, 45)
        BtnRestart.TabIndex = 1
        BtnRestart.Text = "WinBack neu starten"
        BtnRestart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnRestart.UseVisualStyleBackColor = True
        ' 
        ' BtnContinue
        ' 
        BtnContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
        BtnContinue.DialogResult = System.Windows.Forms.DialogResult.Ignore
        BtnContinue.Image = CType(resources.GetObject("BtnContinue.Image"), Drawing.Image)
        BtnContinue.ImeMode = System.Windows.Forms.ImeMode.NoControl
        BtnContinue.Location = New System.Drawing.Point(431, 281)
        BtnContinue.Name = "BtnContinue"
        BtnContinue.Size = New System.Drawing.Size(120, 45)
        BtnContinue.TabIndex = 0
        BtnContinue.Text = "WinBack fortsetzen"
        BtnContinue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnContinue.UseVisualStyleBackColor = True
        ' 
        ' PnlPicture
        ' 
        PnlPicture.BackColor = Drawing.Color.Transparent
        PnlPicture.BackgroundImage = CType(resources.GetObject("PnlPicture.BackgroundImage"), Drawing.Image)
        PnlPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        PnlPicture.Controls.Add(tbException)
        PnlPicture.Location = New System.Drawing.Point(12, 10)
        PnlPicture.Name = "PnlPicture"
        PnlPicture.Size = New System.Drawing.Size(484, 370)
        PnlPicture.TabIndex = 4
        ' 
        ' tbException
        ' 
        tbException.Location = New System.Drawing.Point(0, 318)
        tbException.Multiline = True
        tbException.Name = "tbException"
        tbException.ReadOnly = True
        tbException.ScrollBars = System.Windows.Forms.ScrollBars.Both
        tbException.Size = New System.Drawing.Size(412, 52)
        tbException.TabIndex = 6
        tbException.TabStop = False
        tbException.Visible = False
        ' 
        ' lblText
        ' 
        lblText.AutoSize = True
        lblText.Font = New System.Drawing.Font("Arial", 18F)
        lblText.Location = New System.Drawing.Point(8, 12)
        lblText.Name = "lblText"
        lblText.Size = New System.Drawing.Size(472, 27)
        lblText.TabIndex = 5
        lblText.Text = "Uuups, das hätte nicht passieren dürfen...."
        ' 
        ' BtnMail
        ' 
        BtnMail.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
        BtnMail.Image = CType(resources.GetObject("BtnMail.Image"), Drawing.Image)
        BtnMail.ImeMode = System.Windows.Forms.ImeMode.NoControl
        BtnMail.Location = New System.Drawing.Point(12, 383)
        BtnMail.Name = "BtnMail"
        BtnMail.Size = New System.Drawing.Size(116, 45)
        BtnMail.TabIndex = 2
        BtnMail.Text = "Bericht per Mail senden"
        BtnMail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnMail.UseVisualStyleBackColor = True
        ' 
        ' BtnShow
        ' 
        BtnShow.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
        BtnShow.Image = CType(resources.GetObject("BtnShow.Image"), Drawing.Image)
        BtnShow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        BtnShow.Location = New System.Drawing.Point(134, 383)
        BtnShow.Name = "BtnShow"
        BtnShow.Size = New System.Drawing.Size(116, 45)
        BtnShow.TabIndex = 3
        BtnShow.Text = "Fehlerbericht anzeigen"
        BtnShow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnShow.UseVisualStyleBackColor = True
        ' 
        ' wb_Main_Exception
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        ClientSize = New System.Drawing.Size(563, 440)
        ControlBox = False
        Controls.Add(BtnShow)
        Controls.Add(BtnMail)
        Controls.Add(lblText)
        Controls.Add(BtnExit)
        Controls.Add(BtnRestart)
        Controls.Add(BtnContinue)
        Controls.Add(PnlPicture)
        DoubleBuffered = True
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "wb_Main_Exception"
        Text = "Fehler in WinBack AddIn"
        PnlPicture.ResumeLayout(False)
        PnlPicture.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents BtnExit As System.Windows.Forms.Button
    Friend WithEvents BtnContinue As System.Windows.Forms.Button
    Friend WithEvents BtnRestart As System.Windows.Forms.Button
    Friend WithEvents PnlPicture As System.Windows.Forms.Panel
    Friend WithEvents lblText As System.Windows.Forms.Label
    Friend WithEvents BtnMail As System.Windows.Forms.Button
    Friend WithEvents BtnShow As System.Windows.Forms.Button
    Friend WithEvents tbException As System.Windows.Forms.TextBox
End Class
