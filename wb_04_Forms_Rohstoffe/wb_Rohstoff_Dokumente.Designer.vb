<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_Rohstoff_Dokumente
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
        Me.lbDokumente = New System.Windows.Forms.ListBox()
        Me.VorschauPDF = New System.Windows.Forms.PictureBox()
        CType(Me.VorschauPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbDokumente
        '
        Me.lbDokumente.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbDokumente.FormattingEnabled = True
        Me.lbDokumente.Location = New System.Drawing.Point(0, 0)
        Me.lbDokumente.Name = "lbDokumente"
        Me.lbDokumente.Size = New System.Drawing.Size(230, 504)
        Me.lbDokumente.TabIndex = 0
        '
        'VorschauPDF
        '
        Me.VorschauPDF.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VorschauPDF.Location = New System.Drawing.Point(230, 0)
        Me.VorschauPDF.Name = "VorschauPDF"
        Me.VorschauPDF.Size = New System.Drawing.Size(591, 504)
        Me.VorschauPDF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.VorschauPDF.TabIndex = 1
        Me.VorschauPDF.TabStop = False
        '
        'wb_Rohstoff_Dokumente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(821, 504)
        Me.Controls.Add(Me.VorschauPDF)
        Me.Controls.Add(Me.lbDokumente)
        Me.Name = "wb_Rohstoff_Dokumente"
        Me.Text = "Produkt-Datenblätter zum Rohstoff"
        CType(Me.VorschauPDF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lbDokumente As Windows.Forms.ListBox
    Friend WithEvents VorschauPDF As Windows.Forms.PictureBox
End Class
