Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Schnittstelle_SetupVorschau
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
        Me.grpVerzeichnisse = New System.Windows.Forms.GroupBox()
        Me.cbCalculate = New System.Windows.Forms.CheckBox()
        Me.BtnImportFile = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbImportFile = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.pnlVorschau = New System.Windows.Forms.Panel()
        Me.grpVerzeichnisse.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpVerzeichnisse
        '
        Me.grpVerzeichnisse.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpVerzeichnisse.Controls.Add(Me.cbCalculate)
        Me.grpVerzeichnisse.Controls.Add(Me.BtnImportFile)
        Me.grpVerzeichnisse.Controls.Add(Me.Label4)
        Me.grpVerzeichnisse.Controls.Add(Me.tbImportFile)
        Me.grpVerzeichnisse.Location = New System.Drawing.Point(12, 12)
        Me.grpVerzeichnisse.Name = "grpVerzeichnisse"
        Me.grpVerzeichnisse.Size = New System.Drawing.Size(584, 69)
        Me.grpVerzeichnisse.TabIndex = 13
        Me.grpVerzeichnisse.TabStop = False
        Me.grpVerzeichnisse.Text = "Test-Daten"
        '
        'cbCalculate
        '
        Me.cbCalculate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbCalculate.AutoSize = True
        Me.cbCalculate.Location = New System.Drawing.Point(374, 36)
        Me.cbCalculate.Name = "cbCalculate"
        Me.cbCalculate.Size = New System.Drawing.Size(109, 17)
        Me.cbCalculate.TabIndex = 26
        Me.cbCalculate.Text = "Werte berechnen"
        Me.cbCalculate.UseVisualStyleBackColor = True
        '
        'BtnImportFile
        '
        Me.BtnImportFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnImportFile.Image = Global.WinBack.My.Resources.Resources.VirtTreeCollapse_16x16
        Me.BtnImportFile.Location = New System.Drawing.Point(489, 25)
        Me.BtnImportFile.Name = "BtnImportFile"
        Me.BtnImportFile.Size = New System.Drawing.Size(36, 34)
        Me.BtnImportFile.TabIndex = 22
        Me.BtnImportFile.TabStop = False
        Me.BtnImportFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnImportFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnImportFile.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Import-Datei"
        '
        'tbImportFile
        '
        Me.tbImportFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbImportFile.Location = New System.Drawing.Point(9, 33)
        Me.tbImportFile.Name = "tbImportFile"
        Me.tbImportFile.ReadOnly = True
        Me.tbImportFile.Size = New System.Drawing.Size(355, 20)
        Me.tbImportFile.TabIndex = 18
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog1"
        '
        'pnlVorschau
        '
        Me.pnlVorschau.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlVorschau.AutoScroll = True
        Me.pnlVorschau.AutoScrollMinSize = New System.Drawing.Size(2000, 100)
        Me.pnlVorschau.Location = New System.Drawing.Point(12, 87)
        Me.pnlVorschau.Name = "pnlVorschau"
        Me.pnlVorschau.Size = New System.Drawing.Size(584, 243)
        Me.pnlVorschau.TabIndex = 14
        '
        'wb_Schnittstelle_SetupVorschau
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 342)
        Me.Controls.Add(Me.pnlVorschau)
        Me.Controls.Add(Me.grpVerzeichnisse)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.MinimumSize = New System.Drawing.Size(624, 188)
        Me.Name = "wb_Schnittstelle_SetupVorschau"
        Me.Text = "Schnittstelle Import Vorschau"
        Me.grpVerzeichnisse.ResumeLayout(False)
        Me.grpVerzeichnisse.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpVerzeichnisse As System.Windows.Forms.GroupBox
    Friend WithEvents BtnImportFile As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbImportFile As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents pnlVorschau As System.Windows.Forms.Panel
    Friend WithEvents cbCalculate As System.Windows.Forms.CheckBox
End Class
