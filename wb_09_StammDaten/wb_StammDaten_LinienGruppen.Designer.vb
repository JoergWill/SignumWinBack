Imports WeifenLuo.WinFormsUI.Docking
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_StammDaten_LinienGruppen
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
        Me.BtnLinienGruppeNeu = New System.Windows.Forms.Button()
        Me.BtnNeueAufarbeitung = New System.Windows.Forms.Button()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.DataGridView = New WinBack.wb_DataGridView()
        Me.BtnLoeschen = New System.Windows.Forms.Button()
        Me.BtnSync = New System.Windows.Forms.Button()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnLinienGruppeNeu
        '
        Me.BtnLinienGruppeNeu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnLinienGruppeNeu.Location = New System.Drawing.Point(12, 540)
        Me.BtnLinienGruppeNeu.Name = "BtnLinienGruppeNeu"
        Me.BtnLinienGruppeNeu.Size = New System.Drawing.Size(95, 49)
        Me.BtnLinienGruppeNeu.TabIndex = 1
        Me.BtnLinienGruppeNeu.Text = "Neue Liniengruppe"
        Me.BtnLinienGruppeNeu.UseVisualStyleBackColor = True
        '
        'BtnNeueAufarbeitung
        '
        Me.BtnNeueAufarbeitung.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnNeueAufarbeitung.Location = New System.Drawing.Point(113, 540)
        Me.BtnNeueAufarbeitung.Name = "BtnNeueAufarbeitung"
        Me.BtnNeueAufarbeitung.Size = New System.Drawing.Size(95, 49)
        Me.BtnNeueAufarbeitung.TabIndex = 2
        Me.BtnNeueAufarbeitung.Text = "Neuer Backort (Aufarbeitung)"
        Me.BtnNeueAufarbeitung.UseVisualStyleBackColor = True
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
        Me.BtnLoeschen.Location = New System.Drawing.Point(315, 540)
        Me.BtnLoeschen.Name = "BtnLoeschen"
        Me.BtnLoeschen.Size = New System.Drawing.Size(95, 49)
        Me.BtnLoeschen.TabIndex = 3
        Me.BtnLoeschen.Text = "Löschen"
        Me.BtnLoeschen.UseVisualStyleBackColor = True
        '
        'BtnSync
        '
        Me.BtnSync.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnSync.Location = New System.Drawing.Point(214, 540)
        Me.BtnSync.Name = "BtnSync"
        Me.BtnSync.Size = New System.Drawing.Size(95, 49)
        Me.BtnSync.TabIndex = 5
        Me.BtnSync.Text = "OrgaBack Aufarb.Plätze"
        Me.BtnSync.UseVisualStyleBackColor = True
        '
        'wb_StammDaten_LinienGruppen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(691, 601)
        Me.Controls.Add(Me.BtnSync)
        Me.Controls.Add(Me.BtnLoeschen)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.BtnNeueAufarbeitung)
        Me.Controls.Add(Me.BtnLinienGruppeNeu)
        Me.Controls.Add(Me.DataGridView)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "wb_StammDaten_LinienGruppen"
        Me.Text = "WinBack Liniengruppen/Aufarbeitungsplätze"
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridView As wb_DataGridView
    Friend WithEvents BtnLinienGruppeNeu As Windows.Forms.Button
    Friend WithEvents BtnNeueAufarbeitung As Windows.Forms.Button
    Friend WithEvents BtnOK As Windows.Forms.Button
    Friend WithEvents BtnLoeschen As Windows.Forms.Button
    Friend WithEvents BtnSync As Windows.Forms.Button
End Class
