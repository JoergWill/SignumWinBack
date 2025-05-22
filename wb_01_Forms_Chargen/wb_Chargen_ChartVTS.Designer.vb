Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Chargen_ChartVTS
    Inherits DockContent
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Me.chrtVTS = New System.Windows.Forms.DataVisualization.Charting.Chart()
        CType(Me.chrtVTS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chrtVTS
        '
        ChartArea1.Name = "ChartArea1"
        Me.chrtVTS.ChartAreas.Add(ChartArea1)
        Me.chrtVTS.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Name = "Legend1"
        Me.chrtVTS.Legends.Add(Legend1)
        Me.chrtVTS.Location = New System.Drawing.Point(0, 0)
        Me.chrtVTS.Name = "chrtVTS"
        Me.chrtVTS.Size = New System.Drawing.Size(1169, 649)
        Me.chrtVTS.TabIndex = 0
        Me.chrtVTS.Text = "Chart1"
        '
        'wb_Chargen_ChartVTS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1169, 649)
        Me.Controls.Add(Me.chrtVTS)
        Me.Name = "wb_Chargen_ChartVTS"
        Me.Text = "Sauerteigherstellung grafisch"
        CType(Me.chrtVTS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents chrtVTS As System.Windows.Forms.DataVisualization.Charting.Chart
End Class
