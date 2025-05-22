Imports WeifenLuo.WinFormsUI.Docking
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Linien_Kneter
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
        Me.components = New System.ComponentModel.Container()
        Me.tlpKneter = New System.Windows.Forms.TableLayoutPanel()
        Me.tTwinCatADS = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'tlpKneter
        '
        Me.tlpKneter.ColumnCount = 1
        Me.tlpKneter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKneter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpKneter.Location = New System.Drawing.Point(0, 0)
        Me.tlpKneter.Name = "tlpKneter"
        Me.tlpKneter.RowCount = 6
        Me.tlpKneter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpKneter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpKneter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpKneter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpKneter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpKneter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpKneter.Size = New System.Drawing.Size(1244, 644)
        Me.tlpKneter.TabIndex = 0
        '
        'tTwinCatADS
        '
        Me.tTwinCatADS.Interval = 1000
        '
        'wb_Linien_Kneter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1244, 644)
        Me.Controls.Add(Me.tlpKneter)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wb_Linien_Kneter"
        Me.Text = "WinBack-Kneter"
        Me.TransparencyKey = System.Drawing.Color.Magenta
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpKneter As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tTwinCatADS As System.Windows.Forms.Timer
End Class
