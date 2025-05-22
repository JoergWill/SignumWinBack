<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Artikel_AuswahlListe
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True
        DataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle2.ForeColor = Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False
        DataGridView.DefaultCellStyle = DataGridViewCellStyle2
        DataGridView.Location = New System.Drawing.Point(8, 8)
        DataGridView.Name = "DataGridView"
        DataGridView.Size = New System.Drawing.Size(651, 528)
        DataGridView.SortCol = -1
        DataGridView.TabIndex = 2
        DataGridView.x8859_5_FieldName = ""
        ' 
        ' wb_Artikel_AuswahlListe
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(679, 625)
        Controls.Add(BtnOK)
        Controls.Add(BtnClear)
        Controls.Add(BtnNew)
        Controls.Add(DataGridView)
        Controls.Add(BtnCancel)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "wb_Artikel_AuswahlListe"
        Text = "Auswahl Artikel"
        CType(DataGridView, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents DataGridView As wb_DataGridView
    Friend WithEvents BtnNew As System.Windows.Forms.Button
    Friend WithEvents BtnClear As System.Windows.Forms.Button
    Friend WithEvents BtnOK As System.Windows.Forms.Button
End Class
