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
        Me.BtnLoadVorlage = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnEditVorlage = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnLoadVorlage
        '
        Me.BtnLoadVorlage.Location = New System.Drawing.Point(12, 439)
        Me.BtnLoadVorlage.Name = "BtnLoadVorlage"
        Me.BtnLoadVorlage.Size = New System.Drawing.Size(104, 28)
        Me.BtnLoadVorlage.TabIndex = 0
        Me.BtnLoadVorlage.Text = "Laden"
        Me.BtnLoadVorlage.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(122, 439)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(104, 28)
        Me.BtnCancel.TabIndex = 1
        Me.BtnCancel.Text = "Abbruch"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnEditVorlage
        '
        Me.BtnEditVorlage.Location = New System.Drawing.Point(232, 439)
        Me.BtnEditVorlage.Name = "BtnEditVorlage"
        Me.BtnEditVorlage.Size = New System.Drawing.Size(104, 28)
        Me.BtnEditVorlage.TabIndex = 2
        Me.BtnEditVorlage.Text = "Bearbeiten"
        Me.BtnEditVorlage.UseVisualStyleBackColor = True
        '
        'wb_Planung_Vorlage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(350, 479)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnEditVorlage)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnLoadVorlage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wb_Planung_Vorlage"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "Vorlage Produktionstag laden"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnLoadVorlage As Windows.Forms.Button
    Friend WithEvents BtnCancel As Windows.Forms.Button
    Friend WithEvents BtnEditVorlage As Windows.Forms.Button
End Class
