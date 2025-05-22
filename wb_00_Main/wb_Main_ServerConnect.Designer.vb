<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_Main_ServerConnect
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
        Me.tbMessageReceive = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'tbMessageReceive
        '
        Me.tbMessageReceive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbMessageReceive.Location = New System.Drawing.Point(0, 0)
        Me.tbMessageReceive.Multiline = True
        Me.tbMessageReceive.Name = "tbMessageReceive"
        Me.tbMessageReceive.ReadOnly = True
        Me.tbMessageReceive.Size = New System.Drawing.Size(284, 261)
        Me.tbMessageReceive.TabIndex = 0
        '
        'wb_Main_ServerConnect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.tbMessageReceive)
        Me.Name = "wb_Main_ServerConnect"
        Me.ShowInTaskbar = False
        Me.Text = "wb_Main_ServerConnect"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbMessageReceive As System.Windows.Forms.TextBox
End Class
