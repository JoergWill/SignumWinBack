Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rezept_Liste
    Inherits DockContent
    'Inherits System.Windows.Forms.Form

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
        DataGridView = New wb_DataGridView()
        CType(DataGridView, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView
        ' 
        DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        DataGridView.Location = New System.Drawing.Point(0, 0)
        DataGridView.Name = "DataGridView"
        DataGridView.Size = New System.Drawing.Size(893, 640)
        DataGridView.SortCol = -1
        DataGridView.TabIndex = 1
        DataGridView.x8859_5_FieldName = ""
        ' 
        ' wb_Rezept_Liste
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(893, 640)
        Controls.Add(DataGridView)
        Name = "wb_Rezept_Liste"
        Text = "Rezept Liste"
        CType(DataGridView, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

    Public WithEvents DataGridView As wb_DataGridView
End Class
