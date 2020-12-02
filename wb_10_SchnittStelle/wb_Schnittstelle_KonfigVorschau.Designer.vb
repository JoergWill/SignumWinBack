Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Schnittstelle_KonfigVorschau
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
        Me.pnlVorschau = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'pnlVorschau
        '
        Me.pnlVorschau.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlVorschau.Location = New System.Drawing.Point(0, 0)
        Me.pnlVorschau.Name = "pnlVorschau"
        Me.pnlVorschau.Size = New System.Drawing.Size(608, 475)
        Me.pnlVorschau.TabIndex = 0
        '
        'wb_Schnittstelle_KonfigVorschau
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 475)
        Me.Controls.Add(Me.pnlVorschau)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.MinimumSize = New System.Drawing.Size(624, 188)
        Me.Name = "wb_Schnittstelle_KonfigVorschau"
        Me.Text = "Import Vorschau"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlVorschau As Windows.Forms.Panel
End Class
