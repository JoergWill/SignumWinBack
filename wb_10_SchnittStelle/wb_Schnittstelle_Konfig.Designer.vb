Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Schnittstelle_Konfig
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnNewFile = New System.Windows.Forms.Button()
        Me.BtnLoadFile = New System.Windows.Forms.Button()
        Me.cbFormatSchnittstelle = New System.Windows.Forms.ComboBox()
        Me.grpTabelle = New System.Windows.Forms.GroupBox()
        Me.cbEnable = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tbFileNameSchema = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbTabelleName = New System.Windows.Forms.TextBox()
        Me.BtnNewTable = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbTabelle = New System.Windows.Forms.ComboBox()
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.grpTabelleFelder = New System.Windows.Forms.GroupBox()
        Me.tbSonder = New System.Windows.Forms.TextBox()
        Me.cbSonder = New System.Windows.Forms.CheckBox()
        Me.cbTab = New System.Windows.Forms.CheckBox()
        Me.cbSemikolon = New System.Windows.Forms.CheckBox()
        Me.cbKomma = New System.Windows.Forms.CheckBox()
        Me.cbSpace = New System.Windows.Forms.CheckBox()
        Me.BtnLoadTabelle = New System.Windows.Forms.Button()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.grpVerzeichnisse = New System.Windows.Forms.GroupBox()
        Me.BtnExportExplorer = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.BtnImportExplorer = New System.Windows.Forms.Button()
        Me.BtnExportVerz = New System.Windows.Forms.Button()
        Me.BtnImportVerz = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbExportVerz = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbImportVerz = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.grpDefault = New System.Windows.Forms.GroupBox()
        Me.tbDefaultExport = New System.Windows.Forms.TextBox()
        Me.tbDefaultImport = New System.Windows.Forms.TextBox()
        Me.cbDefaultExport = New System.Windows.Forms.CheckBox()
        Me.cbDefaultImport = New System.Windows.Forms.CheckBox()
        Me.pnlFelder = New System.Windows.Forms.Panel()
        Me.GroupBox1.SuspendLayout()
        Me.grpTabelle.SuspendLayout()
        Me.grpTabelleFelder.SuspendLayout()
        Me.grpVerzeichnisse.SuspendLayout()
        Me.grpDefault.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.BtnNewFile)
        Me.GroupBox1.Controls.Add(Me.BtnLoadFile)
        Me.GroupBox1.Controls.Add(Me.cbFormatSchnittstelle)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(374, 83)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Format Schnittstelle"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label1.Location = New System.Drawing.Point(6, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(174, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Format auswählen oder Datei laden"
        '
        'BtnNewFile
        '
        Me.BtnNewFile.Image = Global.WinBack.My.Resources.Resources.IconSaveAs_24x24
        Me.BtnNewFile.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnNewFile.Location = New System.Drawing.Point(276, 19)
        Me.BtnNewFile.Name = "BtnNewFile"
        Me.BtnNewFile.Size = New System.Drawing.Size(84, 46)
        Me.BtnNewFile.TabIndex = 10
        Me.BtnNewFile.Text = "Neu"
        Me.BtnNewFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnNewFile.UseVisualStyleBackColor = True
        '
        'BtnLoadFile
        '
        Me.BtnLoadFile.Image = Global.WinBack.My.Resources.Resources.IconSave_24x24
        Me.BtnLoadFile.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnLoadFile.Location = New System.Drawing.Point(186, 19)
        Me.BtnLoadFile.Name = "BtnLoadFile"
        Me.BtnLoadFile.Size = New System.Drawing.Size(84, 46)
        Me.BtnLoadFile.TabIndex = 9
        Me.BtnLoadFile.Text = "Format laden..."
        Me.BtnLoadFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnLoadFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnLoadFile.UseVisualStyleBackColor = True
        '
        'cbFormatSchnittstelle
        '
        Me.cbFormatSchnittstelle.FormattingEnabled = True
        Me.cbFormatSchnittstelle.Location = New System.Drawing.Point(6, 28)
        Me.cbFormatSchnittstelle.Name = "cbFormatSchnittstelle"
        Me.cbFormatSchnittstelle.Size = New System.Drawing.Size(131, 21)
        Me.cbFormatSchnittstelle.TabIndex = 8
        Me.cbFormatSchnittstelle.TabStop = False
        '
        'grpTabelle
        '
        Me.grpTabelle.Controls.Add(Me.cbEnable)
        Me.grpTabelle.Controls.Add(Me.Label6)
        Me.grpTabelle.Controls.Add(Me.tbFileNameSchema)
        Me.grpTabelle.Controls.Add(Me.Label3)
        Me.grpTabelle.Controls.Add(Me.tbTabelleName)
        Me.grpTabelle.Controls.Add(Me.BtnNewTable)
        Me.grpTabelle.Controls.Add(Me.Label2)
        Me.grpTabelle.Controls.Add(Me.cbTabelle)
        Me.grpTabelle.Location = New System.Drawing.Point(12, 216)
        Me.grpTabelle.Name = "grpTabelle"
        Me.grpTabelle.Size = New System.Drawing.Size(374, 135)
        Me.grpTabelle.TabIndex = 11
        Me.grpTabelle.TabStop = False
        Me.grpTabelle.Text = "Tabelle"
        '
        'cbEnable
        '
        Me.cbEnable.AutoSize = True
        Me.cbEnable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbEnable.Checked = True
        Me.cbEnable.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbEnable.Location = New System.Drawing.Point(143, 30)
        Me.cbEnable.Name = "cbEnable"
        Me.cbEnable.Size = New System.Drawing.Size(59, 17)
        Me.cbEnable.TabIndex = 18
        Me.cbEnable.Text = "Enable"
        Me.cbEnable.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(140, 73)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(111, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Datei Name (Schema)"
        '
        'tbFileNameSchema
        '
        Me.tbFileNameSchema.Location = New System.Drawing.Point(143, 89)
        Me.tbFileNameSchema.Name = "tbFileNameSchema"
        Me.tbFileNameSchema.Size = New System.Drawing.Size(217, 20)
        Me.tbFileNameSchema.TabIndex = 16
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Tabelle Name"
        '
        'tbTabelleName
        '
        Me.tbTabelleName.Location = New System.Drawing.Point(6, 89)
        Me.tbTabelleName.Name = "tbTabelleName"
        Me.tbTabelleName.Size = New System.Drawing.Size(131, 20)
        Me.tbTabelleName.TabIndex = 12
        '
        'BtnNewTable
        '
        Me.BtnNewTable.Image = Global.WinBack.My.Resources.Resources.MainProduktionsPlanung_32x32
        Me.BtnNewTable.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnNewTable.Location = New System.Drawing.Point(276, 20)
        Me.BtnNewTable.Name = "BtnNewTable"
        Me.BtnNewTable.Size = New System.Drawing.Size(84, 46)
        Me.BtnNewTable.TabIndex = 11
        Me.BtnNewTable.Text = "Neu"
        Me.BtnNewTable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnNewTable.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label2.Location = New System.Drawing.Point(6, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(248, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Tabelle auswählen oder Konfigaration neu erstellen"
        '
        'cbTabelle
        '
        Me.cbTabelle.FormattingEnabled = True
        Me.cbTabelle.Location = New System.Drawing.Point(6, 28)
        Me.cbTabelle.Name = "cbTabelle"
        Me.cbTabelle.Size = New System.Drawing.Size(131, 21)
        Me.cbTabelle.TabIndex = 8
        '
        'grpTabelleFelder
        '
        Me.grpTabelleFelder.Controls.Add(Me.tbSonder)
        Me.grpTabelleFelder.Controls.Add(Me.cbSonder)
        Me.grpTabelleFelder.Controls.Add(Me.cbTab)
        Me.grpTabelleFelder.Controls.Add(Me.cbSemikolon)
        Me.grpTabelleFelder.Controls.Add(Me.cbKomma)
        Me.grpTabelleFelder.Controls.Add(Me.cbSpace)
        Me.grpTabelleFelder.Controls.Add(Me.BtnLoadTabelle)
        Me.grpTabelleFelder.Controls.Add(Me.RadioButton4)
        Me.grpTabelleFelder.Controls.Add(Me.RadioButton3)
        Me.grpTabelleFelder.Location = New System.Drawing.Point(392, 216)
        Me.grpTabelleFelder.Name = "grpTabelleFelder"
        Me.grpTabelleFelder.Size = New System.Drawing.Size(204, 135)
        Me.grpTabelleFelder.TabIndex = 12
        Me.grpTabelleFelder.TabStop = False
        Me.grpTabelleFelder.Text = "Tabelle Felder"
        '
        'tbSonder
        '
        Me.tbSonder.Location = New System.Drawing.Point(141, 101)
        Me.tbSonder.Name = "tbSonder"
        Me.tbSonder.Size = New System.Drawing.Size(50, 20)
        Me.tbSonder.TabIndex = 19
        '
        'cbSonder
        '
        Me.cbSonder.AutoSize = True
        Me.cbSonder.Location = New System.Drawing.Point(124, 104)
        Me.cbSonder.Name = "cbSonder"
        Me.cbSonder.Size = New System.Drawing.Size(15, 14)
        Me.cbSonder.TabIndex = 23
        Me.cbSonder.UseVisualStyleBackColor = True
        '
        'cbTab
        '
        Me.cbTab.AutoSize = True
        Me.cbTab.Location = New System.Drawing.Point(124, 82)
        Me.cbTab.Name = "cbTab"
        Me.cbTab.Size = New System.Drawing.Size(47, 17)
        Me.cbTab.TabIndex = 22
        Me.cbTab.Text = "TAB"
        Me.cbTab.UseVisualStyleBackColor = True
        '
        'cbSemikolon
        '
        Me.cbSemikolon.AutoSize = True
        Me.cbSemikolon.Location = New System.Drawing.Point(124, 60)
        Me.cbSemikolon.Name = "cbSemikolon"
        Me.cbSemikolon.Size = New System.Drawing.Size(75, 17)
        Me.cbSemikolon.TabIndex = 21
        Me.cbSemikolon.Text = "Semikolon"
        Me.cbSemikolon.UseVisualStyleBackColor = True
        '
        'cbKomma
        '
        Me.cbKomma.AutoSize = True
        Me.cbKomma.Location = New System.Drawing.Point(124, 38)
        Me.cbKomma.Name = "cbKomma"
        Me.cbKomma.Size = New System.Drawing.Size(61, 17)
        Me.cbKomma.TabIndex = 20
        Me.cbKomma.Text = "Komma"
        Me.cbKomma.UseVisualStyleBackColor = True
        '
        'cbSpace
        '
        Me.cbSpace.AutoSize = True
        Me.cbSpace.Location = New System.Drawing.Point(124, 16)
        Me.cbSpace.Name = "cbSpace"
        Me.cbSpace.Size = New System.Drawing.Size(57, 17)
        Me.cbSpace.TabIndex = 19
        Me.cbSpace.Text = "Space"
        Me.cbSpace.UseVisualStyleBackColor = True
        '
        'BtnLoadTabelle
        '
        Me.BtnLoadTabelle.Image = Global.WinBack.My.Resources.Resources.IconSave_24x24
        Me.BtnLoadTabelle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnLoadTabelle.Location = New System.Drawing.Point(14, 75)
        Me.BtnLoadTabelle.Name = "BtnLoadTabelle"
        Me.BtnLoadTabelle.Size = New System.Drawing.Size(84, 46)
        Me.BtnLoadTabelle.TabIndex = 18
        Me.BtnLoadTabelle.Text = "Datei laden..."
        Me.BtnLoadTabelle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnLoadTabelle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnLoadTabelle.UseVisualStyleBackColor = True
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Checked = True
        Me.RadioButton4.Location = New System.Drawing.Point(14, 33)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(90, 17)
        Me.RadioButton4.TabIndex = 1
        Me.RadioButton4.TabStop = True
        Me.RadioButton4.Text = "Trennzeichen"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(14, 15)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(84, 17)
        Me.RadioButton3.TabIndex = 0
        Me.RadioButton3.Text = "Feste Länge"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'grpVerzeichnisse
        '
        Me.grpVerzeichnisse.Controls.Add(Me.BtnExportExplorer)
        Me.grpVerzeichnisse.Controls.Add(Me.Label7)
        Me.grpVerzeichnisse.Controls.Add(Me.BtnImportExplorer)
        Me.grpVerzeichnisse.Controls.Add(Me.BtnExportVerz)
        Me.grpVerzeichnisse.Controls.Add(Me.BtnImportVerz)
        Me.grpVerzeichnisse.Controls.Add(Me.Label5)
        Me.grpVerzeichnisse.Controls.Add(Me.tbExportVerz)
        Me.grpVerzeichnisse.Controls.Add(Me.Label4)
        Me.grpVerzeichnisse.Controls.Add(Me.tbImportVerz)
        Me.grpVerzeichnisse.Location = New System.Drawing.Point(12, 101)
        Me.grpVerzeichnisse.Name = "grpVerzeichnisse"
        Me.grpVerzeichnisse.Size = New System.Drawing.Size(584, 109)
        Me.grpVerzeichnisse.TabIndex = 13
        Me.grpVerzeichnisse.TabStop = False
        '
        'BtnExportExplorer
        '
        Me.BtnExportExplorer.Image = Global.WinBack.My.Resources.Resources.VirtTreeExpand_16x16
        Me.BtnExportExplorer.Location = New System.Drawing.Point(529, 63)
        Me.BtnExportExplorer.Name = "BtnExportExplorer"
        Me.BtnExportExplorer.Size = New System.Drawing.Size(36, 34)
        Me.BtnExportExplorer.TabIndex = 26
        Me.BtnExportExplorer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnExportExplorer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnExportExplorer.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(526, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Explorer"
        '
        'BtnImportExplorer
        '
        Me.BtnImportExplorer.Image = Global.WinBack.My.Resources.Resources.VirtTreeExpand_16x16
        Me.BtnImportExplorer.Location = New System.Drawing.Point(529, 25)
        Me.BtnImportExplorer.Name = "BtnImportExplorer"
        Me.BtnImportExplorer.Size = New System.Drawing.Size(36, 34)
        Me.BtnImportExplorer.TabIndex = 24
        Me.BtnImportExplorer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnImportExplorer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnImportExplorer.UseVisualStyleBackColor = True
        '
        'BtnExportVerz
        '
        Me.BtnExportVerz.Image = Global.WinBack.My.Resources.Resources.VirtTreeCollapse_16x16
        Me.BtnExportVerz.Location = New System.Drawing.Point(476, 63)
        Me.BtnExportVerz.Name = "BtnExportVerz"
        Me.BtnExportVerz.Size = New System.Drawing.Size(36, 34)
        Me.BtnExportVerz.TabIndex = 23
        Me.BtnExportVerz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnExportVerz.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnExportVerz.UseVisualStyleBackColor = True
        '
        'BtnImportVerz
        '
        Me.BtnImportVerz.Image = Global.WinBack.My.Resources.Resources.VirtTreeCollapse_16x16
        Me.BtnImportVerz.Location = New System.Drawing.Point(476, 25)
        Me.BtnImportVerz.Name = "BtnImportVerz"
        Me.BtnImportVerz.Size = New System.Drawing.Size(36, 34)
        Me.BtnImportVerz.TabIndex = 22
        Me.BtnImportVerz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnImportVerz.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnImportVerz.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 13)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Export-Verzeichnis"
        '
        'tbExportVerz
        '
        Me.tbExportVerz.Location = New System.Drawing.Point(9, 71)
        Me.tbExportVerz.Name = "tbExportVerz"
        Me.tbExportVerz.Size = New System.Drawing.Size(461, 20)
        Me.tbExportVerz.TabIndex = 20
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Import-Verzeichnis"
        '
        'tbImportVerz
        '
        Me.tbImportVerz.Location = New System.Drawing.Point(9, 33)
        Me.tbImportVerz.Name = "tbImportVerz"
        Me.tbImportVerz.Size = New System.Drawing.Size(461, 20)
        Me.tbImportVerz.TabIndex = 18
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog1"
        '
        'grpDefault
        '
        Me.grpDefault.Controls.Add(Me.tbDefaultExport)
        Me.grpDefault.Controls.Add(Me.tbDefaultImport)
        Me.grpDefault.Controls.Add(Me.cbDefaultExport)
        Me.grpDefault.Controls.Add(Me.cbDefaultImport)
        Me.grpDefault.Location = New System.Drawing.Point(392, 12)
        Me.grpDefault.Name = "grpDefault"
        Me.grpDefault.Size = New System.Drawing.Size(204, 83)
        Me.grpDefault.TabIndex = 14
        Me.grpDefault.TabStop = False
        Me.grpDefault.Text = "Standard-Schnittstelle"
        '
        'tbDefaultExport
        '
        Me.tbDefaultExport.Location = New System.Drawing.Point(73, 47)
        Me.tbDefaultExport.Name = "tbDefaultExport"
        Me.tbDefaultExport.ReadOnly = True
        Me.tbDefaultExport.Size = New System.Drawing.Size(118, 20)
        Me.tbDefaultExport.TabIndex = 14
        '
        'tbDefaultImport
        '
        Me.tbDefaultImport.Location = New System.Drawing.Point(73, 21)
        Me.tbDefaultImport.Name = "tbDefaultImport"
        Me.tbDefaultImport.ReadOnly = True
        Me.tbDefaultImport.Size = New System.Drawing.Size(118, 20)
        Me.tbDefaultImport.TabIndex = 13
        '
        'cbDefaultExport
        '
        Me.cbDefaultExport.AutoSize = True
        Me.cbDefaultExport.Location = New System.Drawing.Point(14, 49)
        Me.cbDefaultExport.Name = "cbDefaultExport"
        Me.cbDefaultExport.Size = New System.Drawing.Size(56, 17)
        Me.cbDefaultExport.TabIndex = 1
        Me.cbDefaultExport.Text = "Export"
        Me.cbDefaultExport.UseVisualStyleBackColor = True
        '
        'cbDefaultImport
        '
        Me.cbDefaultImport.AutoSize = True
        Me.cbDefaultImport.Location = New System.Drawing.Point(14, 23)
        Me.cbDefaultImport.Name = "cbDefaultImport"
        Me.cbDefaultImport.Size = New System.Drawing.Size(55, 17)
        Me.cbDefaultImport.TabIndex = 0
        Me.cbDefaultImport.Text = "Import"
        Me.cbDefaultImport.UseVisualStyleBackColor = True
        '
        'pnlFelder
        '
        Me.pnlFelder.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlFelder.Location = New System.Drawing.Point(12, 357)
        Me.pnlFelder.Name = "pnlFelder"
        Me.pnlFelder.Size = New System.Drawing.Size(584, 231)
        Me.pnlFelder.TabIndex = 15
        '
        'wb_Schnittstelle_Konfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 600)
        Me.Controls.Add(Me.pnlFelder)
        Me.Controls.Add(Me.grpDefault)
        Me.Controls.Add(Me.grpVerzeichnisse)
        Me.Controls.Add(Me.grpTabelleFelder)
        Me.Controls.Add(Me.grpTabelle)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.MinimumSize = New System.Drawing.Size(624, 188)
        Me.Name = "wb_Schnittstelle_Konfig"
        Me.Text = "Konfiguration Schnittstelle"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpTabelle.ResumeLayout(False)
        Me.grpTabelle.PerformLayout()
        Me.grpTabelleFelder.ResumeLayout(False)
        Me.grpTabelleFelder.PerformLayout()
        Me.grpVerzeichnisse.ResumeLayout(False)
        Me.grpVerzeichnisse.PerformLayout()
        Me.grpDefault.ResumeLayout(False)
        Me.grpDefault.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents BtnNewFile As Windows.Forms.Button
    Friend WithEvents BtnLoadFile As Windows.Forms.Button
    Friend WithEvents cbFormatSchnittstelle As Windows.Forms.ComboBox
    Friend WithEvents grpTabelle As Windows.Forms.GroupBox
    Friend WithEvents BtnNewTable As Windows.Forms.Button
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents cbTabelle As Windows.Forms.ComboBox
    Friend WithEvents SaveFileDialog As Windows.Forms.SaveFileDialog
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents tbTabelleName As Windows.Forms.TextBox
    Friend WithEvents grpTabelleFelder As Windows.Forms.GroupBox
    Friend WithEvents RadioButton4 As Windows.Forms.RadioButton
    Friend WithEvents RadioButton3 As Windows.Forms.RadioButton
    Friend WithEvents grpVerzeichnisse As Windows.Forms.GroupBox
    Friend WithEvents BtnExportVerz As Windows.Forms.Button
    Friend WithEvents BtnImportVerz As Windows.Forms.Button
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents tbExportVerz As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents tbImportVerz As Windows.Forms.TextBox
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents tbFileNameSchema As Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog As Windows.Forms.OpenFileDialog
    Friend WithEvents BtnLoadTabelle As Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog As Windows.Forms.FolderBrowserDialog
    Friend WithEvents BtnImportExplorer As Windows.Forms.Button
    Friend WithEvents BtnExportExplorer As Windows.Forms.Button
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents grpDefault As Windows.Forms.GroupBox
    Friend WithEvents tbDefaultExport As Windows.Forms.TextBox
    Friend WithEvents tbDefaultImport As Windows.Forms.TextBox
    Friend WithEvents cbDefaultExport As Windows.Forms.CheckBox
    Friend WithEvents cbDefaultImport As Windows.Forms.CheckBox
    Friend WithEvents cbEnable As Windows.Forms.CheckBox
    Friend WithEvents tbSonder As Windows.Forms.TextBox
    Friend WithEvents cbSonder As Windows.Forms.CheckBox
    Friend WithEvents cbTab As Windows.Forms.CheckBox
    Friend WithEvents cbSemikolon As Windows.Forms.CheckBox
    Friend WithEvents cbKomma As Windows.Forms.CheckBox
    Friend WithEvents cbSpace As Windows.Forms.CheckBox
    Friend WithEvents pnlFelder As Windows.Forms.Panel
End Class
