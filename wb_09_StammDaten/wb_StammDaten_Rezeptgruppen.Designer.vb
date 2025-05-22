Imports WeifenLuo.WinFormsUI.Docking
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_StammDaten_Rezeptgruppen
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
        Me.BtnRezeptGruppeNeu = New System.Windows.Forms.Button()
        Me.DataGridView = New WinBack.wb_DataGridView()
        Me.BtnRezeptGruppeLoeschen = New System.Windows.Forms.Button()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnOK
        '
        Me.BtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnOK.Location = New System.Drawing.Point(577, 540)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(102, 49)
        Me.BtnOK.TabIndex = 0
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'BtnRezeptGruppeNeu
        '
        Me.BtnRezeptGruppeNeu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnRezeptGruppeNeu.Location = New System.Drawing.Point(12, 540)
        Me.BtnRezeptGruppeNeu.Name = "BtnRezeptGruppeNeu"
        Me.BtnRezeptGruppeNeu.Size = New System.Drawing.Size(95, 49)
        Me.BtnRezeptGruppeNeu.TabIndex = 2
        Me.BtnRezeptGruppeNeu.Text = "Neue Rezeptgruppe"
        Me.BtnRezeptGruppeNeu.UseVisualStyleBackColor = True
        '
        'DataGridView
        '
        Me.DataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.RowHeadersWidth = 62
        Me.DataGridView.Size = New System.Drawing.Size(691, 534)
        Me.DataGridView.SortCol = -1
        Me.DataGridView.TabIndex = 0
        Me.DataGridView.x8859_5_FieldName = ""
        '
        'BtnRezeptGruppeLoeschen
        '
        Me.BtnRezeptGruppeLoeschen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnRezeptGruppeLoeschen.Location = New System.Drawing.Point(113, 540)
        Me.BtnRezeptGruppeLoeschen.Name = "BtnRezeptGruppeLoeschen"
        Me.BtnRezeptGruppeLoeschen.Size = New System.Drawing.Size(95, 49)
        Me.BtnRezeptGruppeLoeschen.TabIndex = 3
        Me.BtnRezeptGruppeLoeschen.Text = "Rezeptgruppe löschen"
        Me.BtnRezeptGruppeLoeschen.UseVisualStyleBackColor = True
        '
        'wb_StammDaten_Rezeptgruppen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(691, 601)
        Me.Controls.Add(Me.BtnRezeptGruppeLoeschen)
        Me.Controls.Add(Me.BtnRezeptGruppeNeu)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.DataGridView)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "wb_StammDaten_Rezeptgruppen"
        Me.Text = "WinBack Rezeptgruppen"
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridView As wb_DataGridView
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents BtnRezeptGruppeNeu As System.Windows.Forms.Button
    Friend WithEvents BtnRezeptGruppeLoeschen As System.Windows.Forms.Button
End Class
