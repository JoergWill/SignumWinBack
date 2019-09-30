Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Artikel_Hinweise
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
        Me.VorschauPDF = New System.Windows.Forms.PictureBox()
        Me.BtnLoadPdf = New System.Windows.Forms.Button()
        Me.OpenPdfFile = New System.Windows.Forms.OpenFileDialog()
        Me.BtnRotatePdf = New System.Windows.Forms.Button()
        Me.BtnTransferPdf = New System.Windows.Forms.Button()
        CType(Me.VorschauPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'VorschauPDF
        '
        Me.VorschauPDF.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.VorschauPDF.Location = New System.Drawing.Point(0, 0)
        Me.VorschauPDF.Name = "VorschauPDF"
        Me.VorschauPDF.Size = New System.Drawing.Size(861, 588)
        Me.VorschauPDF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.VorschauPDF.TabIndex = 0
        Me.VorschauPDF.TabStop = False
        '
        'BtnLoadPdf
        '
        Me.BtnLoadPdf.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnLoadPdf.Location = New System.Drawing.Point(867, 12)
        Me.BtnLoadPdf.Name = "BtnLoadPdf"
        Me.BtnLoadPdf.Size = New System.Drawing.Size(133, 42)
        Me.BtnLoadPdf.TabIndex = 1
        Me.BtnLoadPdf.Text = "pdf Laden"
        Me.BtnLoadPdf.UseVisualStyleBackColor = True
        '
        'OpenPdfFile
        '
        Me.OpenPdfFile.DefaultExt = "pdf"
        Me.OpenPdfFile.FileName = "*.pdf"
        Me.OpenPdfFile.Title = "Artikel-Verarbeitungs-Hinweis"
        '
        'BtnRotatePdf
        '
        Me.BtnRotatePdf.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnRotatePdf.Location = New System.Drawing.Point(867, 60)
        Me.BtnRotatePdf.Name = "BtnRotatePdf"
        Me.BtnRotatePdf.Size = New System.Drawing.Size(133, 42)
        Me.BtnRotatePdf.TabIndex = 2
        Me.BtnRotatePdf.Text = "pdf Drehen"
        Me.BtnRotatePdf.UseVisualStyleBackColor = True
        '
        'BtnTransferPdf
        '
        Me.BtnTransferPdf.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnTransferPdf.Location = New System.Drawing.Point(867, 108)
        Me.BtnTransferPdf.Name = "BtnTransferPdf"
        Me.BtnTransferPdf.Size = New System.Drawing.Size(133, 42)
        Me.BtnTransferPdf.TabIndex = 3
        Me.BtnTransferPdf.Text = "pdf Übertragen"
        Me.BtnTransferPdf.UseVisualStyleBackColor = True
        '
        'wb_Artikel_Hinweise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1012, 588)
        Me.Controls.Add(Me.BtnTransferPdf)
        Me.Controls.Add(Me.BtnRotatePdf)
        Me.Controls.Add(Me.BtnLoadPdf)
        Me.Controls.Add(Me.VorschauPDF)
        Me.Name = "wb_Artikel_Hinweise"
        Me.Text = "Artikel-Verabeitungshinweise"
        CType(Me.VorschauPDF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents VorschauPDF As Windows.Forms.PictureBox
    Friend WithEvents BtnLoadPdf As Windows.Forms.Button
    Friend WithEvents OpenPdfFile As Windows.Forms.OpenFileDialog
    Friend WithEvents BtnRotatePdf As Windows.Forms.Button
    Friend WithEvents BtnTransferPdf As Windows.Forms.Button
End Class
