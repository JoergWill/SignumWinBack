Imports WeifenLuo.WinFormsUI.Docking
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_StammDaten_Konfiguration
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
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.PanelHTML = New System.Windows.Forms.Panel()
        Me.WebBrowser = New System.Windows.Forms.WebBrowser()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cbKonfigGruppen = New WinBack.wb_ComboBox()
        Me.DataGridView = New WinBack.wb_DataGridView()
        Me.PanelHTML.SuspendLayout()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnOK
        '
        Me.BtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnOK.Location = New System.Drawing.Point(973, 540)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(102, 49)
        Me.BtnOK.TabIndex = 0
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'PanelHTML
        '
        Me.PanelHTML.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelHTML.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelHTML.Controls.Add(Me.WebBrowser)
        Me.PanelHTML.Location = New System.Drawing.Point(653, 5)
        Me.PanelHTML.Name = "PanelHTML"
        Me.PanelHTML.Size = New System.Drawing.Size(423, 529)
        Me.PanelHTML.TabIndex = 2
        '
        'WebBrowser
        '
        Me.WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser.Location = New System.Drawing.Point(0, 0)
        Me.WebBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser.Name = "WebBrowser"
        Me.WebBrowser.ScrollBarsEnabled = False
        Me.WebBrowser.Size = New System.Drawing.Size(421, 527)
        Me.WebBrowser.TabIndex = 2
        Me.WebBrowser.TabStop = False
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label15.Location = New System.Drawing.Point(655, 539)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(29, 13)
        Me.Label15.TabIndex = 31
        Me.Label15.Text = "Filter"
        '
        'cbKonfigGruppen
        '
        Me.cbKonfigGruppen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbKonfigGruppen.FormattingEnabled = True
        Me.cbKonfigGruppen.Location = New System.Drawing.Point(658, 555)
        Me.cbKonfigGruppen.Name = "cbKonfigGruppen"
        Me.cbKonfigGruppen.Size = New System.Drawing.Size(201, 21)
        Me.cbKonfigGruppen.TabIndex = 30
        Me.cbKonfigGruppen.TabStop = False
        '
        'DataGridView
        '
        Me.DataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Location = New System.Drawing.Point(5, 5)
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.Size = New System.Drawing.Size(643, 594)
        Me.DataGridView.SortCol = -1
        Me.DataGridView.TabIndex = 0
        Me.DataGridView.x8859_5_FieldName = ""
        '
        'wb_StammDaten_Konfiguration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1081, 601)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.cbKonfigGruppen)
        Me.Controls.Add(Me.PanelHTML)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.DataGridView)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "wb_StammDaten_Konfiguration"
        Me.Text = "WinBack Konfiguration"
        Me.PanelHTML.ResumeLayout(False)
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView As wb_DataGridView
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents PanelHTML As System.Windows.Forms.Panel
    Friend WithEvents WebBrowser As System.Windows.Forms.WebBrowser
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cbKonfigGruppen As wb_ComboBox
End Class
