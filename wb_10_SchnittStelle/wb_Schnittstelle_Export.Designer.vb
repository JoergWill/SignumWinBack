Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Schnittstelle_Export
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Schnittstelle_Export))
        Label2 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        pgGesamt = New System.Windows.Forms.ProgressBar()
        pgEinzel = New System.Windows.Forms.ProgressBar()
        BtnExport = New System.Windows.Forms.Button()
        Panel4 = New System.Windows.Forms.Panel()
        cbExportT4105C = New System.Windows.Forms.CheckBox()
        BtnExportT4105C = New System.Windows.Forms.Button()
        cbSauertgT1006 = New System.Windows.Forms.CheckBox()
        cbAendrgnT1006 = New System.Windows.Forms.CheckBox()
        pnlT4105C = New System.Windows.Forms.Panel()
        lblTWNummer = New System.Windows.Forms.Label()
        tbTWNummr = New System.Windows.Forms.TextBox()
        cbExportT1006R = New System.Windows.Forms.CheckBox()
        pnlT1006R = New System.Windows.Forms.Panel()
        cbExportT1007R = New System.Windows.Forms.CheckBox()
        BtnExportT1006R = New System.Windows.Forms.Button()
        cbExportT1001A = New System.Windows.Forms.CheckBox()
        BtnExportT1001A = New System.Windows.Forms.Button()
        pnlT1001A = New System.Windows.Forms.Panel()
        BtnExportT1002A = New System.Windows.Forms.Button()
        cbExportT1002A = New System.Windows.Forms.CheckBox()
        cbExportT1002R = New System.Windows.Forms.CheckBox()
        cbExportT1001R = New System.Windows.Forms.CheckBox()
        BtnExportT1001R = New System.Windows.Forms.Button()
        pnlT1001R = New System.Windows.Forms.Panel()
        BtnExportT1002R = New System.Windows.Forms.Button()
        lblInterface = New System.Windows.Forms.Label()
        Panel4.SuspendLayout()
        pnlT4105C.SuspendLayout()
        pnlT1006R.SuspendLayout()
        pnlT1001A.SuspendLayout()
        pnlT1001R.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(44, 131)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(43, 13)
        Label2.TabIndex = 18
        Label2.Text = "Gesamt"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(43, 92)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(53, 13)
        Label1.TabIndex = 17
        Label1.Text = "Fortschritt"
        ' 
        ' pgGesamt
        ' 
        pgGesamt.Location = New System.Drawing.Point(46, 146)
        pgGesamt.Name = "pgGesamt"
        pgGesamt.Size = New System.Drawing.Size(379, 20)
        pgGesamt.TabIndex = 16
        ' 
        ' pgEinzel
        ' 
        pgEinzel.Location = New System.Drawing.Point(47, 107)
        pgEinzel.Name = "pgEinzel"
        pgEinzel.Size = New System.Drawing.Size(175, 20)
        pgEinzel.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        pgEinzel.TabIndex = 15
        ' 
        ' BtnExport
        ' 
        BtnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        BtnExport.Image = CType(resources.GetObject("BtnExport.Image"), Drawing.Image)
        BtnExport.ImageAlign = Drawing.ContentAlignment.BottomRight
        BtnExport.Location = New System.Drawing.Point(47, 15)
        BtnExport.Name = "BtnExport"
        BtnExport.Size = New System.Drawing.Size(176, 57)
        BtnExport.TabIndex = 12
        BtnExport.TabStop = False
        BtnExport.Text = "Export"
        BtnExport.UseVisualStyleBackColor = True
        ' 
        ' Panel4
        ' 
        Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel4.Controls.Add(Label2)
        Panel4.Controls.Add(Label1)
        Panel4.Controls.Add(pgGesamt)
        Panel4.Controls.Add(pgEinzel)
        Panel4.Controls.Add(BtnExport)
        Panel4.Location = New System.Drawing.Point(16, 52)
        Panel4.Name = "Panel4"
        Panel4.Size = New System.Drawing.Size(453, 181)
        Panel4.TabIndex = 25
        Panel4.TabStop = True
        ' 
        ' cbExportT4105C
        ' 
        cbExportT4105C.AutoSize = True
        cbExportT4105C.Location = New System.Drawing.Point(26, 28)
        cbExportT4105C.Name = "cbExportT4105C"
        cbExportT4105C.Size = New System.Drawing.Size(15, 14)
        cbExportT4105C.TabIndex = 11
        cbExportT4105C.TabStop = False
        cbExportT4105C.UseVisualStyleBackColor = True
        ' 
        ' BtnExportT4105C
        ' 
        BtnExportT4105C.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        BtnExportT4105C.Image = CType(resources.GetObject("BtnExportT4105C.Image"), Drawing.Image)
        BtnExportT4105C.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        BtnExportT4105C.Location = New System.Drawing.Point(47, 6)
        BtnExportT4105C.Name = "BtnExportT4105C"
        BtnExportT4105C.Padding = New System.Windows.Forms.Padding(5)
        BtnExportT4105C.Size = New System.Drawing.Size(176, 57)
        BtnExportT4105C.TabIndex = 10
        BtnExportT4105C.TabStop = False
        BtnExportT4105C.Text = "Chargen"
        BtnExportT4105C.UseVisualStyleBackColor = True
        ' 
        ' cbSauertgT1006
        ' 
        cbSauertgT1006.AutoSize = True
        cbSauertgT1006.Location = New System.Drawing.Point(228, 38)
        cbSauertgT1006.Name = "cbSauertgT1006"
        cbSauertgT1006.Size = New System.Drawing.Size(165, 17)
        cbSauertgT1006.TabIndex = 18
        cbSauertgT1006.TabStop = False
        cbSauertgT1006.Text = "Sauerteig (Rezept/Rohstoffe)"
        cbSauertgT1006.UseVisualStyleBackColor = True
        ' 
        ' cbAendrgnT1006
        ' 
        cbAendrgnT1006.AutoSize = True
        cbAendrgnT1006.Location = New System.Drawing.Point(228, 15)
        cbAendrgnT1006.Name = "cbAendrgnT1006"
        cbAendrgnT1006.Size = New System.Drawing.Size(168, 17)
        cbAendrgnT1006.TabIndex = 17
        cbAendrgnT1006.TabStop = False
        cbAendrgnT1006.Text = "Änderungen sofort exportieren"
        cbAendrgnT1006.UseVisualStyleBackColor = True
        ' 
        ' pnlT4105C
        ' 
        pnlT4105C.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        pnlT4105C.Controls.Add(lblTWNummer)
        pnlT4105C.Controls.Add(tbTWNummr)
        pnlT4105C.Controls.Add(cbExportT4105C)
        pnlT4105C.Controls.Add(BtnExportT4105C)
        pnlT4105C.Location = New System.Drawing.Point(16, 446)
        pnlT4105C.Name = "pnlT4105C"
        pnlT4105C.Size = New System.Drawing.Size(453, 70)
        pnlT4105C.TabIndex = 24
        ' 
        ' lblTWNummer
        ' 
        lblTWNummer.AutoSize = True
        lblTWNummer.Location = New System.Drawing.Point(243, 16)
        lblTWNummer.Name = "lblTWNummer"
        lblTWNummer.Size = New System.Drawing.Size(169, 13)
        lblTWNummer.TabIndex = 24
        lblTWNummer.Text = "Export ab TagesWechsel-Nummer"
        ' 
        ' tbTWNummer
        ' 
        tbTWNummr.Location = New System.Drawing.Point(245, 32)
        tbTWNummr.Name = "tbTWNummer"
        tbTWNummr.Size = New System.Drawing.Size(75, 20)
        tbTWNummr.TabIndex = 23
        ' 
        ' cbExportT1006R
        ' 
        cbExportT1006R.AutoSize = True
        cbExportT1006R.Location = New System.Drawing.Point(25, 28)
        cbExportT1006R.Name = "cbExportT1006R"
        cbExportT1006R.Size = New System.Drawing.Size(15, 14)
        cbExportT1006R.TabIndex = 16
        cbExportT1006R.TabStop = False
        cbExportT1006R.UseVisualStyleBackColor = True
        ' 
        ' pnlT1006R
        ' 
        pnlT1006R.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        pnlT1006R.Controls.Add(cbExportT1007R)
        pnlT1006R.Controls.Add(cbSauertgT1006)
        pnlT1006R.Controls.Add(cbAendrgnT1006)
        pnlT1006R.Controls.Add(cbExportT1006R)
        pnlT1006R.Controls.Add(BtnExportT1006R)
        pnlT1006R.Location = New System.Drawing.Point(16, 377)
        pnlT1006R.Name = "pnlT1006R"
        pnlT1006R.Size = New System.Drawing.Size(453, 70)
        pnlT1006R.TabIndex = 23
        ' 
        ' cbExportT1007R
        ' 
        cbExportT1007R.AutoSize = True
        cbExportT1007R.Location = New System.Drawing.Point(25, 48)
        cbExportT1007R.Name = "cbExportT1007R"
        cbExportT1007R.Size = New System.Drawing.Size(15, 14)
        cbExportT1007R.TabIndex = 19
        cbExportT1007R.TabStop = False
        cbExportT1007R.UseVisualStyleBackColor = True
        cbExportT1007R.Visible = False
        ' 
        ' BtnExportT1006R
        ' 
        BtnExportT1006R.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        BtnExportT1006R.Image = CType(resources.GetObject("BtnExportT1006R.Image"), Drawing.Image)
        BtnExportT1006R.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        BtnExportT1006R.Location = New System.Drawing.Point(46, 6)
        BtnExportT1006R.Name = "BtnExportT1006R"
        BtnExportT1006R.Padding = New System.Windows.Forms.Padding(5)
        BtnExportT1006R.Size = New System.Drawing.Size(176, 57)
        BtnExportT1006R.TabIndex = 15
        BtnExportT1006R.TabStop = False
        BtnExportT1006R.Text = "Rezepte"
        BtnExportT1006R.UseVisualStyleBackColor = True
        ' 
        ' cbExportT1001A
        ' 
        cbExportT1001A.AutoSize = True
        cbExportT1001A.Location = New System.Drawing.Point(25, 27)
        cbExportT1001A.Name = "cbExportT1001A"
        cbExportT1001A.Size = New System.Drawing.Size(15, 14)
        cbExportT1001A.TabIndex = 9
        cbExportT1001A.TabStop = False
        cbExportT1001A.UseVisualStyleBackColor = True
        ' 
        ' BtnExportT1001A
        ' 
        BtnExportT1001A.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        BtnExportT1001A.Image = CType(resources.GetObject("BtnExportT1001A.Image"), Drawing.Image)
        BtnExportT1001A.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        BtnExportT1001A.Location = New System.Drawing.Point(46, 5)
        BtnExportT1001A.Name = "BtnExportT1001A"
        BtnExportT1001A.Padding = New System.Windows.Forms.Padding(5)
        BtnExportT1001A.Size = New System.Drawing.Size(176, 57)
        BtnExportT1001A.TabIndex = 8
        BtnExportT1001A.TabStop = False
        BtnExportT1001A.Text = "Artikel"
        BtnExportT1001A.UseVisualStyleBackColor = True
        ' 
        ' pnlT1001A
        ' 
        pnlT1001A.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        pnlT1001A.Controls.Add(BtnExportT1002A)
        pnlT1001A.Controls.Add(cbExportT1002A)
        pnlT1001A.Controls.Add(cbExportT1001A)
        pnlT1001A.Controls.Add(BtnExportT1001A)
        pnlT1001A.Enabled = False
        pnlT1001A.Location = New System.Drawing.Point(16, 239)
        pnlT1001A.Name = "pnlT1001A"
        pnlT1001A.Size = New System.Drawing.Size(453, 70)
        pnlT1001A.TabIndex = 22
        ' 
        ' BtnExportT1002A
        ' 
        BtnExportT1002A.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        BtnExportT1002A.Image = CType(resources.GetObject("BtnExportT1002A.Image"), Drawing.Image)
        BtnExportT1002A.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        BtnExportT1002A.Location = New System.Drawing.Point(249, 5)
        BtnExportT1002A.Name = "BtnExportT1002A"
        BtnExportT1002A.Padding = New System.Windows.Forms.Padding(5)
        BtnExportT1002A.Size = New System.Drawing.Size(176, 57)
        BtnExportT1002A.TabIndex = 17
        BtnExportT1002A.TabStop = False
        BtnExportT1002A.Text = "Artikel-Info/Nährwerte"
        BtnExportT1002A.TextAlign = Drawing.ContentAlignment.MiddleRight
        BtnExportT1002A.UseVisualStyleBackColor = True
        ' 
        ' cbExportT1002A
        ' 
        cbExportT1002A.AutoSize = True
        cbExportT1002A.Location = New System.Drawing.Point(228, 27)
        cbExportT1002A.Name = "cbExportT1002A"
        cbExportT1002A.Size = New System.Drawing.Size(15, 14)
        cbExportT1002A.TabIndex = 16
        cbExportT1002A.TabStop = False
        cbExportT1002A.UseVisualStyleBackColor = True
        ' 
        ' cbExportT1002R
        ' 
        cbExportT1002R.AutoSize = True
        cbExportT1002R.Location = New System.Drawing.Point(229, 27)
        cbExportT1002R.Name = "cbExportT1002R"
        cbExportT1002R.Size = New System.Drawing.Size(15, 14)
        cbExportT1002R.TabIndex = 15
        cbExportT1002R.TabStop = False
        cbExportT1002R.UseVisualStyleBackColor = True
        ' 
        ' cbExportT1001R
        ' 
        cbExportT1001R.AutoSize = True
        cbExportT1001R.Location = New System.Drawing.Point(26, 28)
        cbExportT1001R.Name = "cbExportT1001R"
        cbExportT1001R.Size = New System.Drawing.Size(15, 14)
        cbExportT1001R.TabIndex = 14
        cbExportT1001R.TabStop = False
        cbExportT1001R.UseVisualStyleBackColor = True
        ' 
        ' BtnExportT1001R
        ' 
        BtnExportT1001R.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        BtnExportT1001R.Image = CType(resources.GetObject("BtnExportT1001R.Image"), Drawing.Image)
        BtnExportT1001R.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        BtnExportT1001R.Location = New System.Drawing.Point(47, 6)
        BtnExportT1001R.Name = "BtnExportT1001R"
        BtnExportT1001R.Padding = New System.Windows.Forms.Padding(5)
        BtnExportT1001R.Size = New System.Drawing.Size(176, 57)
        BtnExportT1001R.TabIndex = 13
        BtnExportT1001R.TabStop = False
        BtnExportT1001R.Text = "Rohstoffe"
        BtnExportT1001R.UseVisualStyleBackColor = True
        ' 
        ' pnlT1001R
        ' 
        pnlT1001R.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        pnlT1001R.Controls.Add(BtnExportT1002R)
        pnlT1001R.Controls.Add(cbExportT1002R)
        pnlT1001R.Controls.Add(cbExportT1001R)
        pnlT1001R.Controls.Add(BtnExportT1001R)
        pnlT1001R.Location = New System.Drawing.Point(16, 308)
        pnlT1001R.Name = "pnlT1001R"
        pnlT1001R.Size = New System.Drawing.Size(453, 70)
        pnlT1001R.TabIndex = 21
        ' 
        ' BtnExportT1002R
        ' 
        BtnExportT1002R.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        BtnExportT1002R.Image = CType(resources.GetObject("BtnExportT1001R.Image"), Drawing.Image)
        BtnExportT1002R.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        BtnExportT1002R.Location = New System.Drawing.Point(249, 6)
        BtnExportT1002R.Name = "BtnExportT1002R"
        BtnExportT1002R.Padding = New System.Windows.Forms.Padding(5)
        BtnExportT1002R.Size = New System.Drawing.Size(176, 57)
        BtnExportT1002R.TabIndex = 16
        BtnExportT1002R.TabStop = False
        BtnExportT1002R.Text = "Rohstoffe Nährwerte"
        BtnExportT1002R.TextAlign = Drawing.ContentAlignment.MiddleRight
        BtnExportT1002R.UseVisualStyleBackColor = True
        ' 
        ' lblInterface
        ' 
        lblInterface.AutoSize = True
        lblInterface.Font = New System.Drawing.Font("Arial", 14.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        lblInterface.Location = New System.Drawing.Point(12, 9)
        lblInterface.Name = "lblInterface"
        lblInterface.Size = New System.Drawing.Size(65, 22)
        lblInterface.TabIndex = 20
        lblInterface.Text = "Export"
        ' 
        ' wb_Schnittstelle_Export
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(1001, 746)
        Controls.Add(Panel4)
        Controls.Add(pnlT4105C)
        Controls.Add(pnlT1006R)
        Controls.Add(pnlT1001A)
        Controls.Add(pnlT1001R)
        Controls.Add(lblInterface)
        MinimumSize = New System.Drawing.Size(624, 188)
        Name = "wb_Schnittstelle_Export"
        Text = "Export Daten"
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        pnlT4105C.ResumeLayout(False)
        pnlT4105C.PerformLayout()
        pnlT1006R.ResumeLayout(False)
        pnlT1006R.PerformLayout()
        pnlT1001A.ResumeLayout(False)
        pnlT1001A.PerformLayout()
        pnlT1001R.ResumeLayout(False)
        pnlT1001R.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pgGesamt As System.Windows.Forms.ProgressBar
    Friend WithEvents pgEinzel As System.Windows.Forms.ProgressBar
    Friend WithEvents BtnExport As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents cbExportT4105C As System.Windows.Forms.CheckBox
    Friend WithEvents BtnExportT4105C As System.Windows.Forms.Button
    Friend WithEvents cbSauertgT1006 As System.Windows.Forms.CheckBox
    Friend WithEvents cbAendrgnT1006 As System.Windows.Forms.CheckBox
    Friend WithEvents pnlT4105C As System.Windows.Forms.Panel
    Friend WithEvents cbExportT1006R As System.Windows.Forms.CheckBox
    Friend WithEvents pnlT1006R As System.Windows.Forms.Panel
    Friend WithEvents BtnExportT1006R As System.Windows.Forms.Button
    Friend WithEvents cbExportT1001A As System.Windows.Forms.CheckBox
    Friend WithEvents BtnExportT1001A As System.Windows.Forms.Button
    Friend WithEvents pnlT1001A As System.Windows.Forms.Panel
    Friend WithEvents cbExportT1002R As System.Windows.Forms.CheckBox
    Friend WithEvents cbExportT1001R As System.Windows.Forms.CheckBox
    Friend WithEvents BtnExportT1001R As System.Windows.Forms.Button
    Friend WithEvents pnlT1001R As System.Windows.Forms.Panel
    Friend WithEvents lblInterface As System.Windows.Forms.Label
    Friend WithEvents cbExportT1002A As System.Windows.Forms.CheckBox
    Friend WithEvents BtnExportT1002A As System.Windows.Forms.Button
    Friend WithEvents BtnExportT1002R As System.Windows.Forms.Button
    Friend WithEvents cbExportT1007R As System.Windows.Forms.CheckBox
    Friend WithEvents lblTWNummer As System.Windows.Forms.Label
    Friend WithEvents tbTWNummr As System.Windows.Forms.TextBox
End Class
