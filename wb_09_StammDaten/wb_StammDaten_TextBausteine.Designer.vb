Imports WeifenLuo.WinFormsUI.Docking
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_StammDaten_TextBausteine
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
        Me.BtnLoeschen = New System.Windows.Forms.Button()
        Me.BtnTextBausteinNeu = New System.Windows.Forms.Button()
        Me.BtnFilterProdStufen = New System.Windows.Forms.Button()
        Me.BtnFilterKessel = New System.Windows.Forms.Button()
        Me.BtnFilterTexte = New System.Windows.Forms.Button()
        Me.DataGridView = New WinBack.wb_DataGridView()
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
        'BtnLoeschen
        '
        Me.BtnLoeschen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnLoeschen.Location = New System.Drawing.Point(113, 540)
        Me.BtnLoeschen.Name = "BtnLoeschen"
        Me.BtnLoeschen.Size = New System.Drawing.Size(95, 49)
        Me.BtnLoeschen.TabIndex = 6
        Me.BtnLoeschen.Text = "Löschen"
        Me.BtnLoeschen.UseVisualStyleBackColor = True
        '
        'BtnTextBausteinNeu
        '
        Me.BtnTextBausteinNeu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnTextBausteinNeu.Location = New System.Drawing.Point(12, 540)
        Me.BtnTextBausteinNeu.Name = "BtnTextBausteinNeu"
        Me.BtnTextBausteinNeu.Size = New System.Drawing.Size(95, 49)
        Me.BtnTextBausteinNeu.TabIndex = 5
        Me.BtnTextBausteinNeu.Text = "Neuer Textbaustein"
        Me.BtnTextBausteinNeu.UseVisualStyleBackColor = True
        '
        'BtnFilterProdStufen
        '
        Me.BtnFilterProdStufen.Location = New System.Drawing.Point(12, 12)
        Me.BtnFilterProdStufen.Name = "BtnFilterProdStufen"
        Me.BtnFilterProdStufen.Size = New System.Drawing.Size(108, 24)
        Me.BtnFilterProdStufen.TabIndex = 7
        Me.BtnFilterProdStufen.Text = "Produktions-Stufen"
        Me.BtnFilterProdStufen.UseVisualStyleBackColor = True
        '
        'BtnFilterKessel
        '
        Me.BtnFilterKessel.Location = New System.Drawing.Point(126, 12)
        Me.BtnFilterKessel.Name = "BtnFilterKessel"
        Me.BtnFilterKessel.Size = New System.Drawing.Size(108, 24)
        Me.BtnFilterKessel.TabIndex = 8
        Me.BtnFilterKessel.Text = "Kessel"
        Me.BtnFilterKessel.UseVisualStyleBackColor = True
        '
        'BtnFilterTexte
        '
        Me.BtnFilterTexte.Location = New System.Drawing.Point(240, 12)
        Me.BtnFilterTexte.Name = "BtnFilterTexte"
        Me.BtnFilterTexte.Size = New System.Drawing.Size(108, 24)
        Me.BtnFilterTexte.TabIndex = 9
        Me.BtnFilterTexte.Text = "Texte"
        Me.BtnFilterTexte.UseVisualStyleBackColor = True
        '
        'DataGridView
        '
        Me.DataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Location = New System.Drawing.Point(0, 51)
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.Size = New System.Drawing.Size(691, 483)
        Me.DataGridView.SortCol = -1
        Me.DataGridView.TabIndex = 0
        Me.DataGridView.x8859_5_FieldName = ""
        '
        'wb_StammDaten_TextBausteine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(691, 601)
        Me.Controls.Add(Me.BtnFilterTexte)
        Me.Controls.Add(Me.BtnFilterKessel)
        Me.Controls.Add(Me.BtnFilterProdStufen)
        Me.Controls.Add(Me.BtnLoeschen)
        Me.Controls.Add(Me.BtnTextBausteinNeu)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.DataGridView)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "wb_StammDaten_TextBausteine"
        Me.Text = "Texte Produktions-Stufe"
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridView As wb_DataGridView
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents BtnLoeschen As System.Windows.Forms.Button
    Friend WithEvents BtnTextBausteinNeu As System.Windows.Forms.Button
    Friend WithEvents BtnFilterProdStufen As System.Windows.Forms.Button
    Friend WithEvents BtnFilterKessel As System.Windows.Forms.Button
    Friend WithEvents BtnFilterTexte As System.Windows.Forms.Button
End Class
