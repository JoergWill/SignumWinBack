Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rohstoffe_Lieferung
    Inherits DockContent
    'Inherits System.Windows.Forms.Form

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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.eMindestmenge = New System.Windows.Forms.Label()
        Me.eBilanzmenge = New System.Windows.Forms.Label()
        Me.eGebindegroesse = New System.Windows.Forms.Label()
        Me.tbMindestMenge = New System.Windows.Forms.TextBox()
        Me.lbMindestMenge = New System.Windows.Forms.Label()
        Me.tbBilanzmenge = New System.Windows.Forms.TextBox()
        Me.lbBilanzMenge = New System.Windows.Forms.Label()
        Me.tbGebindeGroesse = New System.Windows.Forms.TextBox()
        Me.lblGebindegroesse = New System.Windows.Forms.Label()
        Me.LagerDataGridView = New WinBack.wb_DataGridView()
        Me.BtnLagerNull = New System.Windows.Forms.Button()
        CType(Me.LagerDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'eMindestmenge
        '
        Me.eMindestmenge.AutoSize = True
        Me.eMindestmenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.eMindestmenge.Location = New System.Drawing.Point(393, 21)
        Me.eMindestmenge.Name = "eMindestmenge"
        Me.eMindestmenge.Size = New System.Drawing.Size(19, 13)
        Me.eMindestmenge.TabIndex = 95
        Me.eMindestmenge.Text = "kg"
        '
        'eBilanzmenge
        '
        Me.eBilanzmenge.AutoSize = True
        Me.eBilanzmenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.eBilanzmenge.Location = New System.Drawing.Point(248, 21)
        Me.eBilanzmenge.Name = "eBilanzmenge"
        Me.eBilanzmenge.Size = New System.Drawing.Size(19, 13)
        Me.eBilanzmenge.TabIndex = 94
        Me.eBilanzmenge.Text = "kg"
        '
        'eGebindegroesse
        '
        Me.eGebindegroesse.AutoSize = True
        Me.eGebindegroesse.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.eGebindegroesse.Location = New System.Drawing.Point(110, 21)
        Me.eGebindegroesse.Name = "eGebindegroesse"
        Me.eGebindegroesse.Size = New System.Drawing.Size(19, 13)
        Me.eGebindegroesse.TabIndex = 93
        Me.eGebindegroesse.Text = "kg"
        '
        'tbMindestMenge
        '
        Me.tbMindestMenge.Location = New System.Drawing.Point(295, 18)
        Me.tbMindestMenge.Name = "tbMindestMenge"
        Me.tbMindestMenge.Size = New System.Drawing.Size(96, 20)
        Me.tbMindestMenge.TabIndex = 88
        Me.tbMindestMenge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbMindestMenge
        '
        Me.lbMindestMenge.AutoSize = True
        Me.lbMindestMenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbMindestMenge.Location = New System.Drawing.Point(292, 2)
        Me.lbMindestMenge.Name = "lbMindestMenge"
        Me.lbMindestMenge.Size = New System.Drawing.Size(76, 13)
        Me.lbMindestMenge.TabIndex = 92
        Me.lbMindestMenge.Text = "Mindestmenge"
        '
        'tbBilanzmenge
        '
        Me.tbBilanzmenge.Location = New System.Drawing.Point(150, 18)
        Me.tbBilanzmenge.Name = "tbBilanzmenge"
        Me.tbBilanzmenge.ReadOnly = True
        Me.tbBilanzmenge.Size = New System.Drawing.Size(96, 20)
        Me.tbBilanzmenge.TabIndex = 90
        Me.tbBilanzmenge.TabStop = False
        Me.tbBilanzmenge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbBilanzMenge
        '
        Me.lbBilanzMenge.AutoSize = True
        Me.lbBilanzMenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbBilanzMenge.Location = New System.Drawing.Point(147, 2)
        Me.lbBilanzMenge.Name = "lbBilanzMenge"
        Me.lbBilanzMenge.Size = New System.Drawing.Size(72, 13)
        Me.lbBilanzMenge.TabIndex = 91
        Me.lbBilanzMenge.Text = "Lagerbestand"
        '
        'tbGebindeGroesse
        '
        Me.tbGebindeGroesse.Location = New System.Drawing.Point(12, 18)
        Me.tbGebindeGroesse.Name = "tbGebindeGroesse"
        Me.tbGebindeGroesse.Size = New System.Drawing.Size(96, 20)
        Me.tbGebindeGroesse.TabIndex = 87
        Me.tbGebindeGroesse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblGebindegroesse
        '
        Me.lblGebindegroesse.AutoSize = True
        Me.lblGebindegroesse.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblGebindegroesse.Location = New System.Drawing.Point(9, 2)
        Me.lblGebindegroesse.Name = "lblGebindegroesse"
        Me.lblGebindegroesse.Size = New System.Drawing.Size(74, 13)
        Me.lblGebindegroesse.TabIndex = 89
        Me.lblGebindegroesse.Text = "Gebindegröße"
        '
        'LagerDataGridView
        '
        Me.LagerDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.LagerDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.LagerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.LagerDataGridView.DefaultCellStyle = DataGridViewCellStyle5
        Me.LagerDataGridView.Location = New System.Drawing.Point(12, 57)
        Me.LagerDataGridView.Name = "LagerDataGridView"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.LagerDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.LagerDataGridView.Size = New System.Drawing.Size(793, 538)
        Me.LagerDataGridView.SortCol = -1
        Me.LagerDataGridView.TabIndex = 96
        Me.LagerDataGridView.x8859_5_FieldName = ""
        '
        'BtnLagerNull
        '
        Me.BtnLagerNull.Location = New System.Drawing.Point(435, 4)
        Me.BtnLagerNull.Name = "BtnLagerNull"
        Me.BtnLagerNull.Size = New System.Drawing.Size(113, 47)
        Me.BtnLagerNull.TabIndex = 97
        Me.BtnLagerNull.TabStop = False
        Me.BtnLagerNull.Text = "Produktionslager Null setzen"
        Me.BtnLagerNull.UseVisualStyleBackColor = True
        '
        'wb_Rohstoffe_Lieferung
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(817, 607)
        Me.Controls.Add(Me.BtnLagerNull)
        Me.Controls.Add(Me.LagerDataGridView)
        Me.Controls.Add(Me.eMindestmenge)
        Me.Controls.Add(Me.eBilanzmenge)
        Me.Controls.Add(Me.eGebindegroesse)
        Me.Controls.Add(Me.tbMindestMenge)
        Me.Controls.Add(Me.lbMindestMenge)
        Me.Controls.Add(Me.tbBilanzmenge)
        Me.Controls.Add(Me.lbBilanzMenge)
        Me.Controls.Add(Me.tbGebindeGroesse)
        Me.Controls.Add(Me.lblGebindegroesse)
        Me.Name = "wb_Rohstoffe_Lieferung"
        Me.Text = "WinBack Produktions-Lager"
        CType(Me.LagerDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents eMindestmenge As Windows.Forms.Label
    Friend WithEvents eBilanzmenge As Windows.Forms.Label
    Friend WithEvents eGebindegroesse As Windows.Forms.Label
    Friend WithEvents tbMindestMenge As Windows.Forms.TextBox
    Friend WithEvents lbMindestMenge As Windows.Forms.Label
    Friend WithEvents tbBilanzmenge As Windows.Forms.TextBox
    Friend WithEvents lbBilanzMenge As Windows.Forms.Label
    Friend WithEvents tbGebindeGroesse As Windows.Forms.TextBox
    Friend WithEvents lblGebindegroesse As Windows.Forms.Label
    Friend WithEvents LagerDataGridView As wb_DataGridView
    Friend WithEvents BtnLagerNull As Windows.Forms.Button
End Class
