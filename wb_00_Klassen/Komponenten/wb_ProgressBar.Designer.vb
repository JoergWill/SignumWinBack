<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_ProgressBar
    Inherits System.Windows.Forms.Label

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'wb_ProgressBar
        '
        Me.Name = "wb_ProgressBar"
        Me.Size = New System.Drawing.Size(548, 150)
        Me.ProgressBar = New System.Windows.Forms.TextBox()
        '        '
        'TextBox1
        '
        Me.ProgressBar.BackColor = System.Drawing.Color.Lime
        Me.ProgressBar.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ProgressBar.Location = New System.Drawing.Point(Me.Location.X, Me.Location.Y)
        Me.ProgressBar.Name = "TextBox1"
        Me.ProgressBar.ReadOnly = True
        Me.ProgressBar.Multiline = False
        Me.ProgressBar.Size = Me.Size
        Me.ProgressBar.TabStop = False
        Me.ProgressBar.Text = ""
        Me.ProgressBar.WordWrap = False

        Me.Controls.Add(Me.ProgressBar)

        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ProgressBar As System.Windows.Forms.TextBox

End Class
