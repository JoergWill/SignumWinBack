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
        Me.cbSimulation = New System.Windows.Forms.CheckBox()
        Me.cbLogLevel = New System.Windows.Forms.ComboBox()
        Me.chkDebug = New System.Windows.Forms.CheckBox()
        Me.chkExpert = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbFormatSchnittstelle = New System.Windows.Forms.ComboBox()
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
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
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBox1.SuspendLayout()
        Me.grpVerzeichnisse.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbSimulation)
        Me.GroupBox1.Controls.Add(Me.cbLogLevel)
        Me.GroupBox1.Controls.Add(Me.chkDebug)
        Me.GroupBox1.Controls.Add(Me.chkExpert)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cbFormatSchnittstelle)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(584, 108)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Format Schnittstelle"
        '
        'cbSimulation
        '
        Me.cbSimulation.AutoSize = True
        Me.cbSimulation.Location = New System.Drawing.Point(331, 53)
        Me.cbSimulation.Name = "cbSimulation"
        Me.cbSimulation.Size = New System.Drawing.Size(74, 17)
        Me.cbSimulation.TabIndex = 14
        Me.cbSimulation.TabStop = False
        Me.cbSimulation.Text = "Simulation"
        Me.cbSimulation.UseVisualStyleBackColor = True
        '
        'cbLogLevel
        '
        Me.cbLogLevel.FormattingEnabled = True
        Me.cbLogLevel.Items.AddRange(New Object() {"Info", "Debug", "Max"})
        Me.cbLogLevel.Location = New System.Drawing.Point(476, 28)
        Me.cbLogLevel.Name = "cbLogLevel"
        Me.cbLogLevel.Size = New System.Drawing.Size(84, 21)
        Me.cbLogLevel.TabIndex = 13
        Me.cbLogLevel.TabStop = False
        Me.cbLogLevel.Text = "Info"
        '
        'chkDebug
        '
        Me.chkDebug.AutoSize = True
        Me.chkDebug.Location = New System.Drawing.Point(331, 30)
        Me.chkDebug.Name = "chkDebug"
        Me.chkDebug.Size = New System.Drawing.Size(139, 17)
        Me.chkDebug.TabIndex = 12
        Me.chkDebug.Text = "Erweiterte Log-Ausgabe"
        Me.chkDebug.UseVisualStyleBackColor = True
        '
        'chkExpert
        '
        Me.chkExpert.AutoSize = True
        Me.chkExpert.Location = New System.Drawing.Point(331, 76)
        Me.chkExpert.Name = "chkExpert"
        Me.chkExpert.Size = New System.Drawing.Size(103, 17)
        Me.chkExpert.TabIndex = 11
        Me.chkExpert.Text = "Experten-Modus"
        Me.chkExpert.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label1.Location = New System.Drawing.Point(6, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(153, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Format Schnittstelle auswählen"
        '
        'cbFormatSchnittstelle
        '
        Me.cbFormatSchnittstelle.FormattingEnabled = True
        Me.cbFormatSchnittstelle.Location = New System.Drawing.Point(9, 28)
        Me.cbFormatSchnittstelle.Name = "cbFormatSchnittstelle"
        Me.cbFormatSchnittstelle.Size = New System.Drawing.Size(174, 21)
        Me.cbFormatSchnittstelle.TabIndex = 8
        Me.cbFormatSchnittstelle.TabStop = False
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
        Me.grpVerzeichnisse.Location = New System.Drawing.Point(12, 122)
        Me.grpVerzeichnisse.Name = "grpVerzeichnisse"
        Me.grpVerzeichnisse.Size = New System.Drawing.Size(584, 118)
        Me.grpVerzeichnisse.TabIndex = 13
        Me.grpVerzeichnisse.TabStop = False
        '
        'BtnExportExplorer
        '
        Me.BtnExportExplorer.Image = Global.WinBack.My.Resources.Resources.VirtTreeExpand_16x16
        Me.BtnExportExplorer.Location = New System.Drawing.Point(529, 76)
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
        Me.BtnExportVerz.Enabled = False
        Me.BtnExportVerz.Image = Global.WinBack.My.Resources.Resources.VirtTreeCollapse_16x16
        Me.BtnExportVerz.Location = New System.Drawing.Point(476, 76)
        Me.BtnExportVerz.Name = "BtnExportVerz"
        Me.BtnExportVerz.Size = New System.Drawing.Size(36, 34)
        Me.BtnExportVerz.TabIndex = 23
        Me.BtnExportVerz.TabStop = False
        Me.BtnExportVerz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnExportVerz.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnExportVerz.UseVisualStyleBackColor = True
        '
        'BtnImportVerz
        '
        Me.BtnImportVerz.Enabled = False
        Me.BtnImportVerz.Image = Global.WinBack.My.Resources.Resources.VirtTreeCollapse_16x16
        Me.BtnImportVerz.Location = New System.Drawing.Point(476, 25)
        Me.BtnImportVerz.Name = "BtnImportVerz"
        Me.BtnImportVerz.Size = New System.Drawing.Size(36, 34)
        Me.BtnImportVerz.TabIndex = 22
        Me.BtnImportVerz.TabStop = False
        Me.BtnImportVerz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnImportVerz.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnImportVerz.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 68)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 13)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Export-Verzeichnis"
        '
        'tbExportVerz
        '
        Me.tbExportVerz.Location = New System.Drawing.Point(9, 84)
        Me.tbExportVerz.Name = "tbExportVerz"
        Me.tbExportVerz.ReadOnly = True
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
        Me.tbImportVerz.ReadOnly = True
        Me.tbImportVerz.Size = New System.Drawing.Size(461, 20)
        Me.tbImportVerz.TabIndex = 18
        '
        'wb_Schnittstelle_Konfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(641, 268)
        Me.Controls.Add(Me.grpVerzeichnisse)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.MinimumSize = New System.Drawing.Size(624, 188)
        Me.Name = "wb_Schnittstelle_Konfig"
        Me.Text = "Konfiguration Schnittstelle"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpVerzeichnisse.ResumeLayout(False)
        Me.grpVerzeichnisse.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbFormatSchnittstelle As System.Windows.Forms.ComboBox
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents grpVerzeichnisse As System.Windows.Forms.GroupBox
    Friend WithEvents BtnExportVerz As System.Windows.Forms.Button
    Friend WithEvents BtnImportVerz As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbExportVerz As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbImportVerz As System.Windows.Forms.TextBox
    Friend WithEvents FolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents BtnImportExplorer As System.Windows.Forms.Button
    Friend WithEvents BtnExportExplorer As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkExpert As System.Windows.Forms.CheckBox
    Friend WithEvents chkDebug As System.Windows.Forms.CheckBox
    Friend WithEvents cbLogLevel As System.Windows.Forms.ComboBox
    Friend WithEvents cbSimulation As System.Windows.Forms.CheckBox
End Class
