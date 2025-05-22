Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Schnittstelle_Setup
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
        Me.gHeader = New System.Windows.Forms.GroupBox()
        Me.lblZeichensatz = New System.Windows.Forms.Label()
        Me.tbZeichensatz = New System.Windows.Forms.ComboBox()
        Me.lblFormat = New System.Windows.Forms.Label()
        Me.tbFormat = New System.Windows.Forms.ComboBox()
        Me.lblUser = New System.Windows.Forms.Label()
        Me.tbUser = New System.Windows.Forms.TextBox()
        Me.lblGeändert = New System.Windows.Forms.Label()
        Me.tbDate = New System.Windows.Forms.TextBox()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.tbVersion = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.tbName = New System.Windows.Forms.TextBox()
        Me.gbTabellen = New System.Windows.Forms.GroupBox()
        Me.lblPfad = New System.Windows.Forms.Label()
        Me.tbExportPfad = New System.Windows.Forms.TextBox()
        Me.tbImportPfad = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbExportName = New System.Windows.Forms.TextBox()
        Me.tbImportMaske = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbExportReihenfolge = New System.Windows.Forms.TextBox()
        Me.tbImportReihenfolge = New System.Windows.Forms.TextBox()
        Me.chkExport = New System.Windows.Forms.CheckBox()
        Me.chkImport = New System.Windows.Forms.CheckBox()
        Me.cbTabellen = New System.Windows.Forms.ComboBox()
        Me.pnlFelder = New System.Windows.Forms.Panel()
        Me.gHeader.SuspendLayout()
        Me.gbTabellen.SuspendLayout()
        Me.SuspendLayout()
        '
        'gHeader
        '
        Me.gHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gHeader.Controls.Add(Me.lblZeichensatz)
        Me.gHeader.Controls.Add(Me.tbZeichensatz)
        Me.gHeader.Controls.Add(Me.lblFormat)
        Me.gHeader.Controls.Add(Me.tbFormat)
        Me.gHeader.Controls.Add(Me.lblUser)
        Me.gHeader.Controls.Add(Me.tbUser)
        Me.gHeader.Controls.Add(Me.lblGeändert)
        Me.gHeader.Controls.Add(Me.tbDate)
        Me.gHeader.Controls.Add(Me.lblVersion)
        Me.gHeader.Controls.Add(Me.tbVersion)
        Me.gHeader.Controls.Add(Me.lblName)
        Me.gHeader.Controls.Add(Me.tbName)
        Me.gHeader.Location = New System.Drawing.Point(12, 12)
        Me.gHeader.Name = "gHeader"
        Me.gHeader.Size = New System.Drawing.Size(1023, 119)
        Me.gHeader.TabIndex = 0
        Me.gHeader.TabStop = False
        Me.gHeader.Text = "Format Schnittstelle"
        '
        'lblZeichensatz
        '
        Me.lblZeichensatz.AutoSize = True
        Me.lblZeichensatz.Location = New System.Drawing.Point(371, 24)
        Me.lblZeichensatz.Name = "lblZeichensatz"
        Me.lblZeichensatz.Size = New System.Drawing.Size(65, 13)
        Me.lblZeichensatz.TabIndex = 30
        Me.lblZeichensatz.Text = "Zeichensatz"
        '
        'tbZeichensatz
        '
        Me.tbZeichensatz.FormattingEnabled = True
        Me.tbZeichensatz.Items.AddRange(New Object() {"UTF-8", "ASCII", "ISO-8859-1"})
        Me.tbZeichensatz.Location = New System.Drawing.Point(374, 40)
        Me.tbZeichensatz.Name = "tbZeichensatz"
        Me.tbZeichensatz.Size = New System.Drawing.Size(84, 21)
        Me.tbZeichensatz.TabIndex = 29
        Me.tbZeichensatz.TabStop = False
        Me.tbZeichensatz.Text = "ISO-8859-1"
        '
        'lblFormat
        '
        Me.lblFormat.AutoSize = True
        Me.lblFormat.Location = New System.Drawing.Point(281, 24)
        Me.lblFormat.Name = "lblFormat"
        Me.lblFormat.Size = New System.Drawing.Size(39, 13)
        Me.lblFormat.TabIndex = 28
        Me.lblFormat.Text = "Format"
        '
        'tbFormat
        '
        Me.tbFormat.FormattingEnabled = True
        Me.tbFormat.Items.AddRange(New Object() {"WB", "OP"})
        Me.tbFormat.Location = New System.Drawing.Point(284, 40)
        Me.tbFormat.Name = "tbFormat"
        Me.tbFormat.Size = New System.Drawing.Size(84, 21)
        Me.tbFormat.TabIndex = 27
        Me.tbFormat.TabStop = False
        Me.tbFormat.Text = "CSV"
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Location = New System.Drawing.Point(168, 63)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(25, 13)
        Me.lblUser.TabIndex = 26
        Me.lblUser.Text = "von"
        '
        'tbUser
        '
        Me.tbUser.Location = New System.Drawing.Point(168, 79)
        Me.tbUser.Name = "tbUser"
        Me.tbUser.ReadOnly = True
        Me.tbUser.Size = New System.Drawing.Size(110, 20)
        Me.tbUser.TabIndex = 25
        '
        'lblGeändert
        '
        Me.lblGeändert.AutoSize = True
        Me.lblGeändert.Location = New System.Drawing.Point(87, 63)
        Me.lblGeändert.Name = "lblGeändert"
        Me.lblGeändert.Size = New System.Drawing.Size(51, 13)
        Me.lblGeändert.TabIndex = 24
        Me.lblGeändert.Text = "Geändert"
        '
        'tbDate
        '
        Me.tbDate.Location = New System.Drawing.Point(87, 79)
        Me.tbDate.Name = "tbDate"
        Me.tbDate.ReadOnly = True
        Me.tbDate.Size = New System.Drawing.Size(75, 20)
        Me.tbDate.TabIndex = 23
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(6, 63)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(42, 13)
        Me.lblVersion.TabIndex = 22
        Me.lblVersion.Text = "Version"
        '
        'tbVersion
        '
        Me.tbVersion.Location = New System.Drawing.Point(6, 79)
        Me.tbVersion.Name = "tbVersion"
        Me.tbVersion.Size = New System.Drawing.Size(75, 20)
        Me.tbVersion.TabIndex = 21
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(6, 24)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(69, 13)
        Me.lblName.TabIndex = 20
        Me.lblName.Text = "Bezeichnung"
        '
        'tbName
        '
        Me.tbName.Location = New System.Drawing.Point(6, 40)
        Me.tbName.Name = "tbName"
        Me.tbName.Size = New System.Drawing.Size(272, 20)
        Me.tbName.TabIndex = 0
        '
        'gbTabellen
        '
        Me.gbTabellen.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbTabellen.Controls.Add(Me.lblPfad)
        Me.gbTabellen.Controls.Add(Me.tbExportPfad)
        Me.gbTabellen.Controls.Add(Me.tbImportPfad)
        Me.gbTabellen.Controls.Add(Me.Label2)
        Me.gbTabellen.Controls.Add(Me.tbExportName)
        Me.gbTabellen.Controls.Add(Me.tbImportMaske)
        Me.gbTabellen.Controls.Add(Me.Label1)
        Me.gbTabellen.Controls.Add(Me.tbExportReihenfolge)
        Me.gbTabellen.Controls.Add(Me.tbImportReihenfolge)
        Me.gbTabellen.Controls.Add(Me.chkExport)
        Me.gbTabellen.Controls.Add(Me.chkImport)
        Me.gbTabellen.Controls.Add(Me.cbTabellen)
        Me.gbTabellen.Location = New System.Drawing.Point(12, 137)
        Me.gbTabellen.Name = "gbTabellen"
        Me.gbTabellen.Size = New System.Drawing.Size(1023, 130)
        Me.gbTabellen.TabIndex = 1
        Me.gbTabellen.TabStop = False
        Me.gbTabellen.Text = "Tabelle"
        '
        'lblPfad
        '
        Me.lblPfad.AutoSize = True
        Me.lblPfad.Location = New System.Drawing.Point(371, 58)
        Me.lblPfad.Name = "lblPfad"
        Me.lblPfad.Size = New System.Drawing.Size(29, 13)
        Me.lblPfad.TabIndex = 38
        Me.lblPfad.Text = "Pfad"
        '
        'tbExportPfad
        '
        Me.tbExportPfad.Location = New System.Drawing.Point(374, 98)
        Me.tbExportPfad.Name = "tbExportPfad"
        Me.tbExportPfad.Size = New System.Drawing.Size(227, 20)
        Me.tbExportPfad.TabIndex = 37
        '
        'tbImportPfad
        '
        Me.tbImportPfad.Location = New System.Drawing.Point(374, 75)
        Me.tbImportPfad.Name = "tbImportPfad"
        Me.tbImportPfad.Size = New System.Drawing.Size(227, 20)
        Me.tbImportPfad.TabIndex = 36
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(138, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Maske"
        '
        'tbExportName
        '
        Me.tbExportName.Location = New System.Drawing.Point(141, 98)
        Me.tbExportName.Name = "tbExportName"
        Me.tbExportName.Size = New System.Drawing.Size(227, 20)
        Me.tbExportName.TabIndex = 34
        '
        'tbImportMaske
        '
        Me.tbImportMaske.Location = New System.Drawing.Point(141, 75)
        Me.tbImportMaske.Name = "tbImportMaske"
        Me.tbImportMaske.Size = New System.Drawing.Size(227, 20)
        Me.tbImportMaske.TabIndex = 33
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(67, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Reihenfolge"
        '
        'tbExportReihenfolge
        '
        Me.tbExportReihenfolge.Location = New System.Drawing.Point(70, 97)
        Me.tbExportReihenfolge.Name = "tbExportReihenfolge"
        Me.tbExportReihenfolge.Size = New System.Drawing.Size(37, 20)
        Me.tbExportReihenfolge.TabIndex = 32
        '
        'tbImportReihenfolge
        '
        Me.tbImportReihenfolge.Location = New System.Drawing.Point(70, 74)
        Me.tbImportReihenfolge.Name = "tbImportReihenfolge"
        Me.tbImportReihenfolge.Size = New System.Drawing.Size(37, 20)
        Me.tbImportReihenfolge.TabIndex = 31
        '
        'chkExport
        '
        Me.chkExport.AutoSize = True
        Me.chkExport.Location = New System.Drawing.Point(9, 100)
        Me.chkExport.Name = "chkExport"
        Me.chkExport.Size = New System.Drawing.Size(56, 17)
        Me.chkExport.TabIndex = 30
        Me.chkExport.Text = "Export"
        Me.chkExport.UseVisualStyleBackColor = True
        '
        'chkImport
        '
        Me.chkImport.AutoSize = True
        Me.chkImport.Location = New System.Drawing.Point(9, 77)
        Me.chkImport.Name = "chkImport"
        Me.chkImport.Size = New System.Drawing.Size(55, 17)
        Me.chkImport.TabIndex = 29
        Me.chkImport.Text = "Import"
        Me.chkImport.UseVisualStyleBackColor = True
        '
        'cbTabellen
        '
        Me.cbTabellen.FormattingEnabled = True
        Me.cbTabellen.Location = New System.Drawing.Point(9, 19)
        Me.cbTabellen.Name = "cbTabellen"
        Me.cbTabellen.Size = New System.Drawing.Size(98, 21)
        Me.cbTabellen.TabIndex = 28
        Me.cbTabellen.TabStop = False
        '
        'pnlFelder
        '
        Me.pnlFelder.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlFelder.Location = New System.Drawing.Point(12, 280)
        Me.pnlFelder.Name = "pnlFelder"
        Me.pnlFelder.Size = New System.Drawing.Size(1023, 266)
        Me.pnlFelder.TabIndex = 2
        '
        'wb_Schnittstelle_Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1047, 558)
        Me.Controls.Add(Me.pnlFelder)
        Me.Controls.Add(Me.gbTabellen)
        Me.Controls.Add(Me.gHeader)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.MinimumSize = New System.Drawing.Size(624, 188)
        Me.Name = "wb_Schnittstelle_Setup"
        Me.Text = "Setup Schnittstelle"
        Me.gHeader.ResumeLayout(False)
        Me.gHeader.PerformLayout()
        Me.gbTabellen.ResumeLayout(False)
        Me.gbTabellen.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents gHeader As System.Windows.Forms.GroupBox
    Friend WithEvents tbName As System.Windows.Forms.TextBox
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents tbVersion As System.Windows.Forms.TextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents tbUser As System.Windows.Forms.TextBox
    Friend WithEvents lblGeändert As System.Windows.Forms.Label
    Friend WithEvents tbDate As System.Windows.Forms.TextBox
    Friend WithEvents lblFormat As System.Windows.Forms.Label
    Friend WithEvents tbFormat As System.Windows.Forms.ComboBox
    Friend WithEvents lblZeichensatz As System.Windows.Forms.Label
    Friend WithEvents tbZeichensatz As System.Windows.Forms.ComboBox
    Friend WithEvents gbTabellen As System.Windows.Forms.GroupBox
    Friend WithEvents cbTabellen As System.Windows.Forms.ComboBox
    Friend WithEvents chkExport As System.Windows.Forms.CheckBox
    Friend WithEvents chkImport As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbExportReihenfolge As System.Windows.Forms.TextBox
    Friend WithEvents tbImportReihenfolge As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbExportName As System.Windows.Forms.TextBox
    Friend WithEvents tbImportMaske As System.Windows.Forms.TextBox
    Friend WithEvents lblPfad As System.Windows.Forms.Label
    Friend WithEvents tbExportPfad As System.Windows.Forms.TextBox
    Friend WithEvents tbImportPfad As System.Windows.Forms.TextBox
    Friend WithEvents pnlFelder As System.Windows.Forms.Panel
End Class
