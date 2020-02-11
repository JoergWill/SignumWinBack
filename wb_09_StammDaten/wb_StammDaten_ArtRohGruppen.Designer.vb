Imports WeifenLuo.WinFormsUI.Docking
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_StammDaten_ArtRohGruppen
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
        Me.BtnRohstoffGruppeNeu = New System.Windows.Forms.Button()
        Me.BtnArtikelGruppeNeu = New System.Windows.Forms.Button()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.DataGridView = New WinBack.wb_DataGridView()
        Me.BtnLoeschen = New System.Windows.Forms.Button()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnRohstoffGruppeNeu
        '
        Me.BtnRohstoffGruppeNeu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnRohstoffGruppeNeu.Location = New System.Drawing.Point(12, 540)
        Me.BtnRohstoffGruppeNeu.Name = "BtnRohstoffGruppeNeu"
        Me.BtnRohstoffGruppeNeu.Size = New System.Drawing.Size(95, 49)
        Me.BtnRohstoffGruppeNeu.TabIndex = 1
        Me.BtnRohstoffGruppeNeu.Text = "Neue Rohstoff Gruppe"
        Me.BtnRohstoffGruppeNeu.UseVisualStyleBackColor = True
        '
        'BtnArtikelGruppeNeu
        '
        Me.BtnArtikelGruppeNeu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnArtikelGruppeNeu.Location = New System.Drawing.Point(113, 540)
        Me.BtnArtikelGruppeNeu.Name = "BtnArtikelGruppeNeu"
        Me.BtnArtikelGruppeNeu.Size = New System.Drawing.Size(95, 49)
        Me.BtnArtikelGruppeNeu.TabIndex = 2
        Me.BtnArtikelGruppeNeu.Text = "NeueArtikel Gruppe"
        Me.BtnArtikelGruppeNeu.UseVisualStyleBackColor = True
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
        'DataGridView
        '
        Me.DataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.Size = New System.Drawing.Size(691, 534)
        Me.DataGridView.SortCol = -1
        Me.DataGridView.TabIndex = 0
        Me.DataGridView.x8859_5_FieldName = ""
        '
        'BtnLoeschen
        '
        Me.BtnLoeschen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnLoeschen.Location = New System.Drawing.Point(214, 540)
        Me.BtnLoeschen.Name = "BtnLoeschen"
        Me.BtnLoeschen.Size = New System.Drawing.Size(95, 49)
        Me.BtnLoeschen.TabIndex = 3
        Me.BtnLoeschen.Text = "Löschen"
        Me.BtnLoeschen.UseVisualStyleBackColor = True
        '
        'wb_StammDaten_ArtRohGruppen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(691, 601)
        Me.Controls.Add(Me.BtnLoeschen)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.BtnArtikelGruppeNeu)
        Me.Controls.Add(Me.BtnRohstoffGruppeNeu)
        Me.Controls.Add(Me.DataGridView)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "wb_StammDaten_ArtRohGruppen"
        Me.Text = "WinBack Artikel-/Rohstoffgruppen"
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridView As wb_DataGridView
    Friend WithEvents BtnRohstoffGruppeNeu As Windows.Forms.Button
    Friend WithEvents BtnArtikelGruppeNeu As Windows.Forms.Button
    Friend WithEvents BtnOK As Windows.Forms.Button
    Friend WithEvents BtnLoeschen As Windows.Forms.Button
End Class
