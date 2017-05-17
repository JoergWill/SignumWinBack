<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_ServerDetails
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
        Me.tbDetails = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'tbDetails
        '
        Me.tbDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbDetails.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbDetails.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDetails.ForeColor = System.Drawing.SystemColors.WindowText
        Me.tbDetails.Location = New System.Drawing.Point(13, 12)
        Me.tbDetails.Multiline = True
        Me.tbDetails.Name = "tbDetails"
        Me.tbDetails.Size = New System.Drawing.Size(693, 462)
        Me.tbDetails.TabIndex = 0
        Me.tbDetails.TabStop = False
        '
        'wb_ServerDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(718, 486)
        Me.Controls.Add(Me.tbDetails)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.MinimizeBox = False
        Me.Name = "wb_ServerDetails"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "wb_ServerDetails"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbDetails As TextBox
End Class
