Imports Utezduyar.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Main_Progress
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
        Me.ProgressCircle = New Utezduyar.Windows.Forms.ProgressCircle()
        Me.SuspendLayout()
        '
        'ProgressCircle
        '
        Me.ProgressCircle.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressCircle.ForeColor = System.Drawing.Color.Navy
        Me.ProgressCircle.Location = New System.Drawing.Point(0, 0)
        Me.ProgressCircle.Name = "ProgressCircle"
        Me.ProgressCircle.NumberOfArcs = 12
        Me.ProgressCircle.NumberOfTail = 5
        Me.ProgressCircle.RingColor = System.Drawing.Color.LightGray
        Me.ProgressCircle.Size = New System.Drawing.Size(88, 88)
        Me.ProgressCircle.TabIndex = 0
        '
        'wb_Main_Progress
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(88, 88)
        Me.ControlBox = False
        Me.Controls.Add(Me.ProgressCircle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wb_Main_Progress"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TransparencyKey = System.Drawing.SystemColors.Control
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ProgressCircle As Utezduyar.Windows.Forms.ProgressCircle
End Class
