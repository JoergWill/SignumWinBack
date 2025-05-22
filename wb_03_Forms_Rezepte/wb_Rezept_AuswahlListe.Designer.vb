<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_Rezept_AuswahlListe
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        BtnCancel = New System.Windows.Forms.Button()
        BtnNew = New System.Windows.Forms.Button()
        BtnClear = New System.Windows.Forms.Button()
        BtnOK = New System.Windows.Forms.Button()
        DataGridView = New wb_DataGridView()
        CType(DataGridView, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' BtnCancel
        ' 
        BtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
        BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        BtnCancel.Location = New System.Drawing.Point(382, 542)
        BtnCancel.Name = "BtnCancel"
        BtnCancel.Size = New System.Drawing.Size(129, 40)
        BtnCancel.TabIndex = 0
        BtnCancel.Text = "Zurück"
        BtnCancel.UseVisualStyleBackColor = True
        ' 
        ' BtnNew
        ' 
        BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
        BtnNew.DialogResult = System.Windows.Forms.DialogResult.Cancel
        BtnNew.Enabled = False
        BtnNew.Location = New System.Drawing.Point(8, 542)
        BtnNew.Name = "BtnNew"
        BtnNew.Size = New System.Drawing.Size(129, 40)
        BtnNew.TabIndex = 3
        BtnNew.Text = "Neu"
        BtnNew.UseVisualStyleBackColor = True
        ' 
        ' BtnClear
        ' 
        BtnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
        BtnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel
        BtnClear.Enabled = False
        BtnClear.Location = New System.Drawing.Point(143, 542)
        BtnClear.Name = "BtnClear"
        BtnClear.Size = New System.Drawing.Size(129, 40)
        BtnClear.TabIndex = 4
        BtnClear.Text = "Zuordnung löschen"
        BtnClear.UseVisualStyleBackColor = True
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
        BtnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        BtnOK.Location = New System.Drawing.Point(517, 542)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New System.Drawing.Size(129, 40)
        BtnOK.TabIndex = 5
        BtnOK.Text = "OK"
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' DataGridView
        ' 
        DataGridView.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True
        DataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False
        DataGridView.DefaultCellStyle = DataGridViewCellStyle4
        DataGridView.Location = New System.Drawing.Point(8, 8)
        DataGridView.Name = "DataGridView"
        DataGridView.Size = New System.Drawing.Size(651, 528)
        DataGridView.SortCol = -1
        DataGridView.TabIndex = 2
        DataGridView.x8859_5_FieldName = ""
        ' 
        ' wb_Rezept_AuswahlListe
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(679, 625)
        Controls.Add(BtnOK)
        Controls.Add(BtnClear)
        Controls.Add(BtnNew)
        Controls.Add(DataGridView)
        Controls.Add(BtnCancel)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "wb_Rezept_AuswahlListe"
        Text = "Auswahl Rezeptur"
        CType(DataGridView, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents DataGridView As wb_DataGridView
    Friend WithEvents BtnNew As System.Windows.Forms.Button
    Friend WithEvents BtnClear As System.Windows.Forms.Button
    Friend WithEvents BtnOK As System.Windows.Forms.Button
End Class
