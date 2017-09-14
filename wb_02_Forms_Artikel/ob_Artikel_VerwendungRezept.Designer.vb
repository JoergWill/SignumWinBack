<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ob_Artikel_VerwendungRezept
    Inherits System.Windows.Forms.UserControl

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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.HisDataGridView = New WinBack.wb_DataGridView()
        CType(Me.HisDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'HisDataGridView
        '
        Me.HisDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.HisDataGridView.Location = New System.Drawing.Point(3, 3)
        Me.HisDataGridView.Name = "HisDataGridView"
        Me.HisDataGridView.Size = New System.Drawing.Size(727, 386)
        Me.HisDataGridView.TabIndex = 0
        '
        'ob_Artikel_VerwendungRezept
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.HisDataGridView)
        Me.Name = "ob_Artikel_VerwendungRezept"
        Me.Size = New System.Drawing.Size(733, 392)
        CType(Me.HisDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents HisDataGridView As wb_DataGridView
End Class
