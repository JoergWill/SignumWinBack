<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_Timer
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent
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
        Me.tbAktionsTimer = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'tbAktionsTimer
        '
        Me.tbAktionsTimer.BackColor = System.Drawing.Color.DarkGray
        Me.tbAktionsTimer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbAktionsTimer.ForeColor = System.Drawing.SystemColors.InfoText
        Me.tbAktionsTimer.Location = New System.Drawing.Point(0, 0)
        Me.tbAktionsTimer.Name = "tbAktionsTimer"
        Me.tbAktionsTimer.Size = New System.Drawing.Size(627, 723)
        Me.tbAktionsTimer.TabIndex = 13
        '
        'wb_Admin_Timer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(627, 723)
        Me.Controls.Add(Me.tbAktionsTimer)
        Me.Name = "wb_Admin_Timer"
        Me.Text = "WinBack Scheduler"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tbAktionsTimer As Windows.Forms.Panel
End Class
