<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_Planung_Vorlage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Planung_Vorlage))
        BtnLoadVorlage = New System.Windows.Forms.Button()
        BtnCancel = New System.Windows.Forms.Button()
        BtnEditVorlage = New System.Windows.Forms.Button()
        SuspendLayout()
        ' 
        ' BtnLoadVorlage
        ' 
        BtnLoadVorlage.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
        BtnLoadVorlage.Location = New System.Drawing.Point(12, 499)
        BtnLoadVorlage.Name = "BtnLoadVorlage"
        BtnLoadVorlage.Size = New System.Drawing.Size(104, 28)
        BtnLoadVorlage.TabIndex = 0
        BtnLoadVorlage.Text = "Laden"
        BtnLoadVorlage.UseVisualStyleBackColor = True
        ' 
        ' BtnCancel
        ' 
        BtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
        BtnCancel.Location = New System.Drawing.Point(122, 499)
        BtnCancel.Name = "BtnCancel"
        BtnCancel.Size = New System.Drawing.Size(104, 28)
        BtnCancel.TabIndex = 1
        BtnCancel.Text = "Abbruch"
        BtnCancel.UseVisualStyleBackColor = True
        ' 
        ' BtnEditVorlage
        ' 
        BtnEditVorlage.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
        BtnEditVorlage.Location = New System.Drawing.Point(232, 499)
        BtnEditVorlage.Name = "BtnEditVorlage"
        BtnEditVorlage.Size = New System.Drawing.Size(104, 28)
        BtnEditVorlage.TabIndex = 2
        BtnEditVorlage.Text = "Bearbeiten"
        BtnEditVorlage.UseVisualStyleBackColor = True
        ' 
        ' wb_Planung_Vorlage
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(356, 539)
        Controls.Add(BtnEditVorlage)
        Controls.Add(BtnCancel)
        Controls.Add(BtnLoadVorlage)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Drawing.Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "wb_Planung_Vorlage"
        SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Text = "Vorlage Produktionstag laden"
        ResumeLayout(False)

    End Sub

    Friend WithEvents BtnLoadVorlage As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnEditVorlage As System.Windows.Forms.Button
End Class
