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
        Me.OpenPdfFile = New System.Windows.Forms.OpenFileDialog()
        Me.BtnRotateR = New System.Windows.Forms.Button()
        Me.BtnRotateL = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnTransferPdf = New System.Windows.Forms.Button()
        Me.lblArtikelHinweis = New System.Windows.Forms.Label()
        Me.BtnLoadPdf = New System.Windows.Forms.Button()
        Me.cbAufloesung = New System.Windows.Forms.ComboBox()
        Me.tHinweisName = New System.Windows.Forms.TextBox()
        Me.lblAufloesung = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.VorschauPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'VorschauPDF
        '
        Me.VorschauPDF.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.VorschauPDF.Location = New System.Drawing.Point(3, 3)
        Me.VorschauPDF.Name = "VorschauPDF"
        Me.TableLayoutPanel1.SetRowSpan(Me.VorschauPDF, 2)
        Me.VorschauPDF.Size = New System.Drawing.Size(804, 548)
        Me.VorschauPDF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.VorschauPDF.TabIndex = 0
        Me.VorschauPDF.TabStop = False
        '
        'OpenPdfFile
        '
        Me.OpenPdfFile.DefaultExt = "pdf"
        Me.OpenPdfFile.FileName = "*.pdf"
        Me.OpenPdfFile.Title = "Artikel-Verarbeitungs-Hinweis"
        '
        'BtnRotateR
        '
        Me.BtnRotateR.Image = Global.WinBack.My.Resources.Resources.Rotate90R_32x32
        Me.BtnRotateR.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnRotateR.Location = New System.Drawing.Point(54, 22)
        Me.BtnRotateR.Name = "BtnRotateR"
        Me.BtnRotateR.Size = New System.Drawing.Size(42, 42)
        Me.BtnRotateR.TabIndex = 2
        Me.BtnRotateR.UseVisualStyleBackColor = True
        '
        'BtnRotateL
        '
        Me.BtnRotateL.Image = Global.WinBack.My.Resources.Resources.Rotate90L_32x32
        Me.BtnRotateL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnRotateL.Location = New System.Drawing.Point(6, 22)
        Me.BtnRotateL.Name = "BtnRotateL"
        Me.BtnRotateL.Size = New System.Drawing.Size(42, 42)
        Me.BtnRotateL.TabIndex = 73
        Me.BtnRotateL.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 74
        Me.Label1.Text = "Bild drehen"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.VorschauPDF, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1010, 554)
        Me.TableLayoutPanel1.TabIndex = 75
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.BtnTransferPdf)
        Me.Panel2.Controls.Add(Me.lblArtikelHinweis)
        Me.Panel2.Controls.Add(Me.BtnLoadPdf)
        Me.Panel2.Controls.Add(Me.cbAufloesung)
        Me.Panel2.Controls.Add(Me.tHinweisName)
        Me.Panel2.Controls.Add(Me.lblAufloesung)
        Me.Panel2.Location = New System.Drawing.Point(813, 436)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(194, 115)
        Me.Panel2.TabIndex = 83
        '
        'BtnTransferPdf
        '
        Me.BtnTransferPdf.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnTransferPdf.Image = Global.WinBack.My.Resources.Resources.IconDlgRestart_16x16
        Me.BtnTransferPdf.Location = New System.Drawing.Point(89, 60)
        Me.BtnTransferPdf.Name = "BtnTransferPdf"
        Me.BtnTransferPdf.Size = New System.Drawing.Size(97, 42)
        Me.BtnTransferPdf.TabIndex = 77
        Me.BtnTransferPdf.Text = "Übertragen"
        Me.BtnTransferPdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnTransferPdf.UseVisualStyleBackColor = True
        '
        'lblArtikelHinweis
        '
        Me.lblArtikelHinweis.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblArtikelHinweis.AutoSize = True
        Me.lblArtikelHinweis.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblArtikelHinweis.Location = New System.Drawing.Point(6, 14)
        Me.lblArtikelHinweis.Name = "lblArtikelHinweis"
        Me.lblArtikelHinweis.Size = New System.Drawing.Size(144, 13)
        Me.lblArtikelHinweis.TabIndex = 82
        Me.lblArtikelHinweis.Text = "Artikel Verarbeitungs-Hinweis"
        '
        'BtnLoadPdf
        '
        Me.BtnLoadPdf.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnLoadPdf.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLoadPdf.Location = New System.Drawing.Point(156, 23)
        Me.BtnLoadPdf.Name = "BtnLoadPdf"
        Me.BtnLoadPdf.Size = New System.Drawing.Size(30, 31)
        Me.BtnLoadPdf.TabIndex = 76
        Me.BtnLoadPdf.Text = "..."
        Me.BtnLoadPdf.UseVisualStyleBackColor = True
        '
        'cbAufloesung
        '
        Me.cbAufloesung.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbAufloesung.FormattingEnabled = True
        Me.cbAufloesung.Items.AddRange(New Object() {"Default", "96 dpi", "150 dpi", "300 dpi", "600 dpi"})
        Me.cbAufloesung.Location = New System.Drawing.Point(9, 76)
        Me.cbAufloesung.Name = "cbAufloesung"
        Me.cbAufloesung.Size = New System.Drawing.Size(57, 21)
        Me.cbAufloesung.TabIndex = 80
        Me.cbAufloesung.TabStop = False
        Me.cbAufloesung.Text = "Default"
        '
        'tHinweisName
        '
        Me.tHinweisName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tHinweisName.Location = New System.Drawing.Point(9, 30)
        Me.tHinweisName.Name = "tHinweisName"
        Me.tHinweisName.Size = New System.Drawing.Size(140, 20)
        Me.tHinweisName.TabIndex = 78
        '
        'lblAufloesung
        '
        Me.lblAufloesung.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblAufloesung.AutoSize = True
        Me.lblAufloesung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAufloesung.Location = New System.Drawing.Point(6, 60)
        Me.lblAufloesung.Name = "lblAufloesung"
        Me.lblAufloesung.Size = New System.Drawing.Size(77, 13)
        Me.lblAufloesung.TabIndex = 81
        Me.lblAufloesung.Text = "Auflösung (dpi)"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.BtnRotateL)
        Me.Panel1.Controls.Add(Me.BtnRotateR)
        Me.Panel1.Location = New System.Drawing.Point(813, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(194, 94)
        Me.Panel1.TabIndex = 83
        '
        'wb_Artikel_Hinweise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1010, 554)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "wb_Artikel_Hinweise"
        Me.Text = "Artikel-Verarbeitungshinweise"
        CType(Me.VorschauPDF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents VorschauPDF As Windows.Forms.PictureBox
    Friend WithEvents OpenPdfFile As Windows.Forms.OpenFileDialog
    Friend WithEvents BtnRotateR As Windows.Forms.Button
    Friend WithEvents BtnRotateL As Windows.Forms.Button
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents BtnTransferPdf As Windows.Forms.Button
    Friend WithEvents lblArtikelHinweis As Windows.Forms.Label
    Friend WithEvents BtnLoadPdf As Windows.Forms.Button
    Friend WithEvents cbAufloesung As Windows.Forms.ComboBox
    Friend WithEvents tHinweisName As Windows.Forms.TextBox
    Friend WithEvents lblAufloesung As Windows.Forms.Label
    Friend WithEvents Panel1 As Windows.Forms.Panel
End Class
