<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_Rohstoffe_SiloParameter
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
        Me.lblSiloParameter = New System.Windows.Forms.Label()
        Me.BtnSiloVorher = New System.Windows.Forms.Button()
        Me.BtnSiloDanach = New System.Windows.Forms.Button()
        Me.PnlSiloParameter = New System.Windows.Forms.Panel()
        Me.PnlWaagenParameter = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'lblSiloParameter
        '
        Me.lblSiloParameter.AutoSize = True
        Me.lblSiloParameter.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSiloParameter.Location = New System.Drawing.Point(18, 14)
        Me.lblSiloParameter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSiloParameter.Name = "lblSiloParameter"
        Me.lblSiloParameter.Size = New System.Drawing.Size(341, 32)
        Me.lblSiloParameter.TabIndex = 2
        Me.lblSiloParameter.Text = "00000 Weizen xxx - Silo 2"
        '
        'BtnSiloVorher
        '
        Me.BtnSiloVorher.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSiloVorher.Location = New System.Drawing.Point(1804, 12)
        Me.BtnSiloVorher.Name = "BtnSiloVorher"
        Me.BtnSiloVorher.Size = New System.Drawing.Size(170, 65)
        Me.BtnSiloVorher.TabIndex = 5
        Me.BtnSiloVorher.Text = "Vorheriges Silo"
        Me.BtnSiloVorher.UseVisualStyleBackColor = True
        '
        'BtnSiloDanach
        '
        Me.BtnSiloDanach.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSiloDanach.Location = New System.Drawing.Point(1980, 12)
        Me.BtnSiloDanach.Name = "BtnSiloDanach"
        Me.BtnSiloDanach.Size = New System.Drawing.Size(170, 65)
        Me.BtnSiloDanach.TabIndex = 7
        Me.BtnSiloDanach.Text = "Nächstes Silo"
        Me.BtnSiloDanach.UseVisualStyleBackColor = True
        '
        'PnlSiloParameter
        '
        Me.PnlSiloParameter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlSiloParameter.Location = New System.Drawing.Point(13, 102)
        Me.PnlSiloParameter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PnlSiloParameter.Name = "PnlSiloParameter"
        Me.PnlSiloParameter.Size = New System.Drawing.Size(2137, 252)
        Me.PnlSiloParameter.TabIndex = 8
        '
        'PnlWaagenParameter
        '
        Me.PnlWaagenParameter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlWaagenParameter.Location = New System.Drawing.Point(13, 364)
        Me.PnlWaagenParameter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PnlWaagenParameter.Name = "PnlWaagenParameter"
        Me.PnlWaagenParameter.Size = New System.Drawing.Size(2136, 1028)
        Me.PnlWaagenParameter.TabIndex = 9
        '
        'wb_Rohstoffe_SiloParameter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2162, 1406)
        Me.Controls.Add(Me.PnlWaagenParameter)
        Me.Controls.Add(Me.PnlSiloParameter)
        Me.Controls.Add(Me.BtnSiloDanach)
        Me.Controls.Add(Me.BtnSiloVorher)
        Me.Controls.Add(Me.lblSiloParameter)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "wb_Rohstoffe_SiloParameter"
        Me.Text = "Silo-Parameter"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSiloParameter As System.Windows.Forms.Label
    Friend WithEvents BtnSiloVorher As System.Windows.Forms.Button
    Friend WithEvents BtnSiloDanach As System.Windows.Forms.Button
    Friend WithEvents PnlSiloParameter As System.Windows.Forms.Panel
    Friend WithEvents PnlWaagenParameter As System.Windows.Forms.Panel
End Class
