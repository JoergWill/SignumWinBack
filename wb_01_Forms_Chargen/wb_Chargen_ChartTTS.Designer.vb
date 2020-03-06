Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Chargen_ChartTTS
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Me.chrtTTS = New System.Windows.Forms.DataVisualization.Charting.Chart()
        CType(Me.chrtTTS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chrtTTS
        '
        ChartArea1.Name = "ChartArea1"
        Me.chrtTTS.ChartAreas.Add(ChartArea1)
        Me.chrtTTS.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Name = "Legend1"
        Me.chrtTTS.Legends.Add(Legend1)
        Me.chrtTTS.Location = New System.Drawing.Point(0, 0)
        Me.chrtTTS.Name = "chrtTTS"
        Me.chrtTTS.Size = New System.Drawing.Size(1018, 556)
        Me.chrtTTS.TabIndex = 0
        Me.chrtTTS.Text = "Chart1"
        '
        'wb_Chargen_ChartTTS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1018, 556)
        Me.Controls.Add(Me.chrtTTS)
        Me.Name = "wb_Chargen_ChartTTS"
        Me.Text = "Chargenübersicht grafisch"
        CType(Me.chrtTTS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents chrtTTS As Windows.Forms.DataVisualization.Charting.Chart
End Class
