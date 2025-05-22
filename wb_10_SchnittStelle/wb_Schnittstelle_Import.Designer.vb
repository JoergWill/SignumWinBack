Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Schnittstelle_Import
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Schnittstelle_Import))
        lblInterface = New System.Windows.Forms.Label()
        pnlT1006 = New System.Windows.Forms.Panel()
        cbTxtVrbT1006 = New System.Windows.Forms.CheckBox()
        cbImportT1006 = New System.Windows.Forms.CheckBox()
        BtnImportT1006 = New System.Windows.Forms.Button()
        pnlT1001 = New System.Windows.Forms.Panel()
        cbImportT1001 = New System.Windows.Forms.CheckBox()
        BtnImportT1001 = New System.Windows.Forms.Button()
        pnlT1007 = New System.Windows.Forms.Panel()
        cbWsrSveT1007 = New System.Windows.Forms.CheckBox()
        cbRzpWrtT1007 = New System.Windows.Forms.CheckBox()
        cbImportT1007 = New System.Windows.Forms.CheckBox()
        BtnImportT1007 = New System.Windows.Forms.Button()
        pnlT4107 = New System.Windows.Forms.Panel()
        cbImportT4107 = New System.Windows.Forms.CheckBox()
        BtnImportT4107 = New System.Windows.Forms.Button()
        Panel4 = New System.Windows.Forms.Panel()
        Label2 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        pgGesamt = New System.Windows.Forms.ProgressBar()
        pgEinzel = New System.Windows.Forms.ProgressBar()
        cbRemoveImportFiles = New System.Windows.Forms.CheckBox()
        cbAutoImport = New System.Windows.Forms.CheckBox()
        BtnImport = New System.Windows.Forms.Button()
        pnlT1101 = New System.Windows.Forms.Panel()
        cbFtpTrnT1101 = New System.Windows.Forms.CheckBox()
        cbArtRzpT1101 = New System.Windows.Forms.CheckBox()
        cbImportT1101 = New System.Windows.Forms.CheckBox()
        BtnImportT1101 = New System.Windows.Forms.Button()
        pnlT1006.SuspendLayout()
        pnlT1001.SuspendLayout()
        pnlT1007.SuspendLayout()
        pnlT4107.SuspendLayout()
        Panel4.SuspendLayout()
        pnlT1101.SuspendLayout()
        SuspendLayout()
        ' 
        ' lblInterface
        ' 
        lblInterface.AutoSize = True
        lblInterface.Font = New System.Drawing.Font("Arial", 14.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        lblInterface.Location = New System.Drawing.Point(12, 9)
        lblInterface.Name = "lblInterface"
        lblInterface.Size = New System.Drawing.Size(65, 22)
        lblInterface.TabIndex = 1
        lblInterface.Text = "Import"
        ' 
        ' pnlT1006
        ' 
        pnlT1006.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        pnlT1006.Controls.Add(cbTxtVrbT1006)
        pnlT1006.Controls.Add(cbImportT1006)
        pnlT1006.Controls.Add(BtnImportT1006)
        pnlT1006.Location = New System.Drawing.Point(16, 308)
        pnlT1006.Name = "pnlT1006"
        pnlT1006.Size = New System.Drawing.Size(432, 70)
        pnlT1006.TabIndex = 15
        ' 
        ' cbTxtVrbT1006
        ' 
        cbTxtVrbT1006.AutoSize = True
        cbTxtVrbT1006.Location = New System.Drawing.Point(229, 13)
        cbTxtVrbT1006.Name = "cbTxtVrbT1006"
        cbTxtVrbT1006.Size = New System.Drawing.Size(178, 17)
        cbTxtVrbT1006.TabIndex = 15
        cbTxtVrbT1006.Text = "Texte als Verarbeitungshinweise"
        cbTxtVrbT1006.UseVisualStyleBackColor = True
        ' 
        ' cbImportT1006
        ' 
        cbImportT1006.AutoSize = True
        cbImportT1006.Location = New System.Drawing.Point(26, 28)
        cbImportT1006.Name = "cbImportT1006"
        cbImportT1006.Size = New System.Drawing.Size(15, 14)
        cbImportT1006.TabIndex = 14
        cbImportT1006.UseVisualStyleBackColor = True
        ' 
        ' BtnImportT1006
        ' 
        BtnImportT1006.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        BtnImportT1006.Image = CType(resources.GetObject("BtnImportT1006.Image"), Drawing.Image)
        BtnImportT1006.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        BtnImportT1006.Location = New System.Drawing.Point(47, 6)
        BtnImportT1006.Name = "BtnImportT1006"
        BtnImportT1006.Padding = New System.Windows.Forms.Padding(5)
        BtnImportT1006.Size = New System.Drawing.Size(176, 57)
        BtnImportT1006.TabIndex = 13
        BtnImportT1006.Text = "Rezeptkopf"
        BtnImportT1006.UseVisualStyleBackColor = True
        ' 
        ' pnlT1001
        ' 
        pnlT1001.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        pnlT1001.Controls.Add(cbImportT1001)
        pnlT1001.Controls.Add(BtnImportT1001)
        pnlT1001.Location = New System.Drawing.Point(16, 239)
        pnlT1001.Name = "pnlT1001"
        pnlT1001.Size = New System.Drawing.Size(432, 70)
        pnlT1001.TabIndex = 16
        ' 
        ' cbImportT1001
        ' 
        cbImportT1001.AutoSize = True
        cbImportT1001.Location = New System.Drawing.Point(25, 27)
        cbImportT1001.Name = "cbImportT1001"
        cbImportT1001.Size = New System.Drawing.Size(15, 14)
        cbImportT1001.TabIndex = 9
        cbImportT1001.UseVisualStyleBackColor = True
        ' 
        ' BtnImportT1001
        ' 
        BtnImportT1001.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        BtnImportT1001.Image = CType(resources.GetObject("BtnImportT1001.Image"), Drawing.Image)
        BtnImportT1001.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        BtnImportT1001.Location = New System.Drawing.Point(46, 5)
        BtnImportT1001.Name = "BtnImportT1001"
        BtnImportT1001.Padding = New System.Windows.Forms.Padding(5)
        BtnImportT1001.Size = New System.Drawing.Size(176, 57)
        BtnImportT1001.TabIndex = 8
        BtnImportT1001.Text = "Artikel/Rohstoffe"
        BtnImportT1001.UseVisualStyleBackColor = True
        ' 
        ' pnlT1007
        ' 
        pnlT1007.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        pnlT1007.Controls.Add(cbWsrSveT1007)
        pnlT1007.Controls.Add(cbRzpWrtT1007)
        pnlT1007.Controls.Add(cbImportT1007)
        pnlT1007.Controls.Add(BtnImportT1007)
        pnlT1007.Location = New System.Drawing.Point(16, 377)
        pnlT1007.Name = "pnlT1007"
        pnlT1007.Size = New System.Drawing.Size(432, 70)
        pnlT1007.TabIndex = 17
        ' 
        ' cbWsrSveT1007
        ' 
        cbWsrSveT1007.AutoSize = True
        cbWsrSveT1007.Location = New System.Drawing.Point(228, 36)
        cbWsrSveT1007.Name = "cbWsrSveT1007"
        cbWsrSveT1007.Size = New System.Drawing.Size(161, 17)
        cbWsrSveT1007.TabIndex = 18
        cbWsrSveT1007.Text = "Wassertemperatur speichern"
        cbWsrSveT1007.UseVisualStyleBackColor = True
        ' 
        ' cbRzpWrtT1007
        ' 
        cbRzpWrtT1007.AutoSize = True
        cbRzpWrtT1007.Location = New System.Drawing.Point(228, 13)
        cbRzpWrtT1007.Name = "cbRzpWrtT1007"
        cbRzpWrtT1007.Size = New System.Drawing.Size(154, 17)
        cbRzpWrtT1007.TabIndex = 17
        cbRzpWrtT1007.Text = "Änderungen überschreiben"
        cbRzpWrtT1007.UseVisualStyleBackColor = True
        ' 
        ' cbImportT1007
        ' 
        cbImportT1007.AutoSize = True
        cbImportT1007.Location = New System.Drawing.Point(25, 28)
        cbImportT1007.Name = "cbImportT1007"
        cbImportT1007.Size = New System.Drawing.Size(15, 14)
        cbImportT1007.TabIndex = 16
        cbImportT1007.UseVisualStyleBackColor = True
        ' 
        ' BtnImportT1007
        ' 
        BtnImportT1007.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        BtnImportT1007.Image = CType(resources.GetObject("BtnImportT1007.Image"), Drawing.Image)
        BtnImportT1007.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        BtnImportT1007.Location = New System.Drawing.Point(46, 6)
        BtnImportT1007.Name = "BtnImportT1007"
        BtnImportT1007.Padding = New System.Windows.Forms.Padding(5)
        BtnImportT1007.Size = New System.Drawing.Size(176, 57)
        BtnImportT1007.TabIndex = 15
        BtnImportT1007.Text = "Rezepturen"
        BtnImportT1007.UseVisualStyleBackColor = True
        ' 
        ' pnlT4107
        ' 
        pnlT4107.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        pnlT4107.Controls.Add(cbImportT4107)
        pnlT4107.Controls.Add(BtnImportT4107)
        pnlT4107.Location = New System.Drawing.Point(16, 446)
        pnlT4107.Name = "pnlT4107"
        pnlT4107.Size = New System.Drawing.Size(432, 69)
        pnlT4107.TabIndex = 18
        ' 
        ' cbImportT4107
        ' 
        cbImportT4107.AutoSize = True
        cbImportT4107.Location = New System.Drawing.Point(26, 28)
        cbImportT4107.Name = "cbImportT4107"
        cbImportT4107.Size = New System.Drawing.Size(15, 14)
        cbImportT4107.TabIndex = 11
        cbImportT4107.UseVisualStyleBackColor = True
        ' 
        ' BtnImportT4107
        ' 
        BtnImportT4107.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        BtnImportT4107.Image = CType(resources.GetObject("BtnImportT4107.Image"), Drawing.Image)
        BtnImportT4107.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        BtnImportT4107.Location = New System.Drawing.Point(47, 6)
        BtnImportT4107.Name = "BtnImportT4107"
        BtnImportT4107.Padding = New System.Windows.Forms.Padding(5)
        BtnImportT4107.Size = New System.Drawing.Size(176, 57)
        BtnImportT4107.TabIndex = 10
        BtnImportT4107.Text = "Lieferungen/Lager"
        BtnImportT4107.UseVisualStyleBackColor = True
        ' 
        ' Panel4
        ' 
        Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel4.Controls.Add(Label2)
        Panel4.Controls.Add(Label1)
        Panel4.Controls.Add(pgGesamt)
        Panel4.Controls.Add(pgEinzel)
        Panel4.Controls.Add(cbRemoveImportFiles)
        Panel4.Controls.Add(cbAutoImport)
        Panel4.Controls.Add(BtnImport)
        Panel4.Location = New System.Drawing.Point(16, 52)
        Panel4.Name = "Panel4"
        Panel4.Size = New System.Drawing.Size(432, 181)
        Panel4.TabIndex = 19
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
        pgGesamt.Size = New System.Drawing.Size(361, 20)
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
        ' cbRemoveImportFiles
        ' 
        cbRemoveImportFiles.AutoSize = True
        cbRemoveImportFiles.Location = New System.Drawing.Point(229, 46)
        cbRemoveImportFiles.Name = "cbRemoveImportFiles"
        cbRemoveImportFiles.Size = New System.Drawing.Size(162, 17)
        cbRemoveImportFiles.TabIndex = 14
        cbRemoveImportFiles.Text = "Dateien nach Import löschen"
        cbRemoveImportFiles.UseVisualStyleBackColor = True
        ' 
        ' cbAutoImport
        ' 
        cbAutoImport.AutoSize = True
        cbAutoImport.Location = New System.Drawing.Point(229, 23)
        cbAutoImport.Name = "cbAutoImport"
        cbAutoImport.Size = New System.Drawing.Size(139, 17)
        cbAutoImport.TabIndex = 13
        cbAutoImport.Text = "Automatisch Importieren"
        cbAutoImport.UseVisualStyleBackColor = True
        ' 
        ' BtnImport
        ' 
        BtnImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        BtnImport.Image = CType(resources.GetObject("BtnImport.Image"), Drawing.Image)
        BtnImport.ImageAlign = Drawing.ContentAlignment.TopRight
        BtnImport.Location = New System.Drawing.Point(47, 16)
        BtnImport.Name = "BtnImport"
        BtnImport.Size = New System.Drawing.Size(176, 57)
        BtnImport.TabIndex = 12
        BtnImport.Text = "Import"
        BtnImport.UseVisualStyleBackColor = True
        ' 
        ' pnlT1101
        ' 
        pnlT1101.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        pnlT1101.Controls.Add(cbFtpTrnT1101)
        pnlT1101.Controls.Add(cbArtRzpT1101)
        pnlT1101.Controls.Add(cbImportT1101)
        pnlT1101.Controls.Add(BtnImportT1101)
        pnlT1101.Location = New System.Drawing.Point(16, 514)
        pnlT1101.Name = "pnlT1101"
        pnlT1101.Size = New System.Drawing.Size(432, 70)
        pnlT1101.TabIndex = 27
        ' 
        ' cbFtpTrnT1101
        ' 
        cbFtpTrnT1101.AutoSize = True
        cbFtpTrnT1101.Location = New System.Drawing.Point(228, 38)
        cbFtpTrnT1101.Name = "cbFtpTrnT1101"
        cbFtpTrnT1101.Size = New System.Drawing.Size(138, 17)
        cbFtpTrnT1101.TabIndex = 17
        cbFtpTrnT1101.Text = "Automatisch übertragen"
        cbFtpTrnT1101.UseVisualStyleBackColor = True
        ' 
        ' cbArtRzpT1101
        ' 
        cbArtRzpT1101.AutoSize = True
        cbArtRzpT1101.Location = New System.Drawing.Point(228, 15)
        cbArtRzpT1101.Name = "cbArtRzpT1101"
        cbArtRzpT1101.Size = New System.Drawing.Size(135, 17)
        cbArtRzpT1101.TabIndex = 16
        cbArtRzpT1101.Text = "Artikel/Rezeptbezogen"
        cbArtRzpT1101.UseVisualStyleBackColor = True
        ' 
        ' cbImportT1101
        ' 
        cbImportT1101.AutoSize = True
        cbImportT1101.Location = New System.Drawing.Point(25, 27)
        cbImportT1101.Name = "cbImportT1101"
        cbImportT1101.Size = New System.Drawing.Size(15, 14)
        cbImportT1101.TabIndex = 9
        cbImportT1101.UseVisualStyleBackColor = True
        ' 
        ' BtnImportT1101
        ' 
        BtnImportT1101.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        BtnImportT1101.Image = CType(resources.GetObject("BtnImportT1101.Image"), Drawing.Image)
        BtnImportT1101.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        BtnImportT1101.Location = New System.Drawing.Point(46, 5)
        BtnImportT1101.Name = "BtnImportT1101"
        BtnImportT1101.Padding = New System.Windows.Forms.Padding(5)
        BtnImportT1101.Size = New System.Drawing.Size(176, 57)
        BtnImportT1101.TabIndex = 8
        BtnImportT1101.Text = "Backzettel"
        BtnImportT1101.UseVisualStyleBackColor = True
        ' 
        ' wb_Schnittstelle_Import
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(878, 670)
        Controls.Add(pnlT1101)
        Controls.Add(Panel4)
        Controls.Add(pnlT4107)
        Controls.Add(pnlT1007)
        Controls.Add(pnlT1001)
        Controls.Add(pnlT1006)
        Controls.Add(lblInterface)
        MinimumSize = New System.Drawing.Size(624, 188)
        Name = "wb_Schnittstelle_Import"
        Text = "Import Daten"
        pnlT1006.ResumeLayout(False)
        pnlT1006.PerformLayout()
        pnlT1001.ResumeLayout(False)
        pnlT1001.PerformLayout()
        pnlT1007.ResumeLayout(False)
        pnlT1007.PerformLayout()
        pnlT4107.ResumeLayout(False)
        pnlT4107.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        pnlT1101.ResumeLayout(False)
        pnlT1101.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents lblInterface As System.Windows.Forms.Label
    Friend WithEvents pnlT1006 As System.Windows.Forms.Panel
    Friend WithEvents cbTxtVrbT1006 As System.Windows.Forms.CheckBox
    Friend WithEvents cbImportT1006 As System.Windows.Forms.CheckBox
    Friend WithEvents BtnImportT1006 As System.Windows.Forms.Button
    Friend WithEvents pnlT1001 As System.Windows.Forms.Panel
    Friend WithEvents cbImportT1001 As System.Windows.Forms.CheckBox
    Friend WithEvents BtnImportT1001 As System.Windows.Forms.Button
    Friend WithEvents pnlT1007 As System.Windows.Forms.Panel
    Friend WithEvents pnlT4107 As System.Windows.Forms.Panel
    Friend WithEvents cbWsrSveT1007 As System.Windows.Forms.CheckBox
    Friend WithEvents cbRzpWrtT1007 As System.Windows.Forms.CheckBox
    Friend WithEvents cbImportT1007 As System.Windows.Forms.CheckBox
    Friend WithEvents BtnImportT1007 As System.Windows.Forms.Button
    Friend WithEvents cbImportT4107 As System.Windows.Forms.CheckBox
    Friend WithEvents BtnImportT4107 As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pgGesamt As System.Windows.Forms.ProgressBar
    Friend WithEvents pgEinzel As System.Windows.Forms.ProgressBar
    Friend WithEvents cbRemoveImportFiles As System.Windows.Forms.CheckBox
    Friend WithEvents cbAutoImport As System.Windows.Forms.CheckBox
    Friend WithEvents BtnImport As System.Windows.Forms.Button
    Friend WithEvents pnlT1101 As System.Windows.Forms.Panel
    Friend WithEvents cbFtpTrnT1101 As System.Windows.Forms.CheckBox
    Friend WithEvents cbArtRzpT1101 As System.Windows.Forms.CheckBox
    Friend WithEvents cbImportT1101 As System.Windows.Forms.CheckBox
    Friend WithEvents BtnImportT1101 As System.Windows.Forms.Button
End Class
