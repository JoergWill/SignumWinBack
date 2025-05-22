Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_ThemeControl
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
        Me.ThemeColorGridView = New System.Windows.Forms.DataGridView()
        Me.ColorDialog = New System.Windows.Forms.ColorDialog()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnDefault_Black = New System.Windows.Forms.Button()
        Me.BtnSaveAndClose = New System.Windows.Forms.Button()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnSaveAsDefault = New System.Windows.Forms.Button()
        CType(Me.ThemeColorGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ThemeColorGridView
        '
        Me.ThemeColorGridView.AllowUserToAddRows = False
        Me.ThemeColorGridView.AllowUserToDeleteRows = False
        Me.ThemeColorGridView.AllowUserToResizeRows = False
        Me.ThemeColorGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ThemeColorGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.ThemeColorGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.ThemeColorGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ThemeColorGridView.ColumnHeadersVisible = False
        Me.ThemeColorGridView.Location = New System.Drawing.Point(12, 12)
        Me.ThemeColorGridView.MultiSelect = False
        Me.ThemeColorGridView.Name = "ThemeColorGridView"
        Me.ThemeColorGridView.RowHeadersVisible = False
        Me.ThemeColorGridView.Size = New System.Drawing.Size(357, 551)
        Me.ThemeColorGridView.TabIndex = 0
        '
        'ColorDialog
        '
        Me.ColorDialog.AnyColor = True
        Me.ColorDialog.FullOpen = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.Location = New System.Drawing.Point(388, 531)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(112, 32)
        Me.BtnCancel.TabIndex = 1
        Me.BtnCancel.Text = "OK"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnDefault_Black
        '
        Me.BtnDefault_Black.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnDefault_Black.Location = New System.Drawing.Point(388, 12)
        Me.BtnDefault_Black.Name = "BtnDefault_Black"
        Me.BtnDefault_Black.Size = New System.Drawing.Size(112, 32)
        Me.BtnDefault_Black.TabIndex = 2
        Me.BtnDefault_Black.Tag = "Black"
        Me.BtnDefault_Black.Text = "Black"
        Me.BtnDefault_Black.UseVisualStyleBackColor = True
        '
        'BtnSaveAndClose
        '
        Me.BtnSaveAndClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSaveAndClose.Enabled = False
        Me.BtnSaveAndClose.Location = New System.Drawing.Point(388, 474)
        Me.BtnSaveAndClose.Name = "BtnSaveAndClose"
        Me.BtnSaveAndClose.Size = New System.Drawing.Size(112, 51)
        Me.BtnSaveAndClose.TabIndex = 4
        Me.BtnSaveAndClose.Text = "Speichern und Schliessen"
        Me.BtnSaveAndClose.UseVisualStyleBackColor = True
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.Enabled = False
        Me.BtnSave.Location = New System.Drawing.Point(388, 436)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(112, 32)
        Me.BtnSave.TabIndex = 5
        Me.BtnSave.Text = "Speichern"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'BtnSaveAsDefault
        '
        Me.BtnSaveAsDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSaveAsDefault.Enabled = False
        Me.BtnSaveAsDefault.Location = New System.Drawing.Point(388, 315)
        Me.BtnSaveAsDefault.Name = "BtnSaveAsDefault"
        Me.BtnSaveAsDefault.Size = New System.Drawing.Size(112, 51)
        Me.BtnSaveAsDefault.TabIndex = 7
        Me.BtnSaveAsDefault.Tag = "SaveAsDefault"
        Me.BtnSaveAsDefault.Text = "Speichern als Default"
        Me.BtnSaveAsDefault.UseVisualStyleBackColor = True
        '
        'Admin_ThemeControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(512, 575)
        Me.Controls.Add(Me.BtnSaveAsDefault)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.BtnSaveAndClose)
        Me.Controls.Add(Me.BtnDefault_Black)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.ThemeColorGridView)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Admin_ThemeControl"
        Me.Padding = New System.Windows.Forms.Padding(9)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Farben definieren (Farb-Thema)"
        Me.TopMost = True
        CType(Me.ThemeColorGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ThemeColorGridView As DataGridView
    Friend WithEvents ColorDialog As ColorDialog
    Friend WithEvents BtnCancel As Button
    Friend WithEvents BtnDefault_Black As Button
    Friend WithEvents BtnSaveAndClose As Button
    Friend WithEvents BtnSave As Button
    Friend WithEvents BtnSaveAsDefault As Button
End Class
