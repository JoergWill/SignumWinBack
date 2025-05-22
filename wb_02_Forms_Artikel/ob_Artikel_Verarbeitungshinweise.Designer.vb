<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ob_Artikel_Verarbeitungshinweise
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.BtnPagePlus = New System.Windows.Forms.Button()
        Me.BtnPageMinus = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnRotateL = New System.Windows.Forms.Button()
        Me.BtnRotateR = New System.Windows.Forms.Button()
        Me.BtnTransferPdf = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblArtikelHinweis = New System.Windows.Forms.Label()
        Me.BtnLoadPdf = New System.Windows.Forms.Button()
        Me.cbAufloesung = New System.Windows.Forms.ComboBox()
        Me.tHinweisName = New System.Windows.Forms.TextBox()
        Me.lblAufloesung = New System.Windows.Forms.Label()
        Me.OpenPdfFile = New System.Windows.Forms.OpenFileDialog()
        Me.VorschauPDF = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.VorschauPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnPagePlus
        '
        Me.BtnPagePlus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPagePlus.Image = Global.WinBack.My.Resources.Resources.PagePlus_16x16
        Me.BtnPagePlus.Location = New System.Drawing.Point(280, 12)
        Me.BtnPagePlus.Name = "BtnPagePlus"
        Me.BtnPagePlus.Size = New System.Drawing.Size(30, 30)
        Me.BtnPagePlus.TabIndex = 77
        Me.ToolTip.SetToolTip(Me.BtnPagePlus, "Seite vor")
        Me.BtnPagePlus.UseVisualStyleBackColor = True
        '
        'BtnPageMinus
        '
        Me.BtnPageMinus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPageMinus.Image = Global.WinBack.My.Resources.Resources.PageMinus_16x16
        Me.BtnPageMinus.Location = New System.Drawing.Point(244, 12)
        Me.BtnPageMinus.Name = "BtnPageMinus"
        Me.BtnPageMinus.Size = New System.Drawing.Size(30, 30)
        Me.BtnPageMinus.TabIndex = 75
        Me.ToolTip.SetToolTip(Me.BtnPageMinus, "Seite zurück")
        Me.BtnPageMinus.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.BtnPagePlus)
        Me.Panel1.Controls.Add(Me.BtnPageMinus)
        Me.Panel1.Controls.Add(Me.BtnRotateL)
        Me.Panel1.Controls.Add(Me.BtnRotateR)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(403, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(327, 54)
        Me.Panel1.TabIndex = 83
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(68, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 27)
        Me.Label1.TabIndex = 82
        Me.Label1.Text = "Ansicht drehen"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'BtnRotateL
        '
        Me.BtnRotateL.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnRotateL.Image = Global.WinBack.My.Resources.Resources.PageRotateL_16x16
        Me.BtnRotateL.Location = New System.Drawing.Point(120, 12)
        Me.BtnRotateL.Name = "BtnRotateL"
        Me.BtnRotateL.Size = New System.Drawing.Size(30, 30)
        Me.BtnRotateL.TabIndex = 73
        Me.ToolTip.SetToolTip(Me.BtnRotateL, "Bild gegen den Uhrzeigersinn drehen")
        Me.BtnRotateL.UseVisualStyleBackColor = True
        '
        'BtnRotateR
        '
        Me.BtnRotateR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnRotateR.Image = Global.WinBack.My.Resources.Resources.PageRotateR_16x16
        Me.BtnRotateR.Location = New System.Drawing.Point(156, 12)
        Me.BtnRotateR.Name = "BtnRotateR"
        Me.BtnRotateR.Size = New System.Drawing.Size(30, 30)
        Me.BtnRotateR.TabIndex = 2
        Me.ToolTip.SetToolTip(Me.BtnRotateR, "Bild im Uhrzeigersinn drehen")
        Me.BtnRotateR.UseVisualStyleBackColor = True
        '
        'BtnTransferPdf
        '
        Me.BtnTransferPdf.Image = Global.WinBack.My.Resources.Resources.IconDlgRestart_16x16
        Me.BtnTransferPdf.Location = New System.Drawing.Point(283, 6)
        Me.BtnTransferPdf.Name = "BtnTransferPdf"
        Me.BtnTransferPdf.Size = New System.Drawing.Size(97, 42)
        Me.BtnTransferPdf.TabIndex = 77
        Me.BtnTransferPdf.Text = "Übertragen"
        Me.BtnTransferPdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip.SetToolTip(Me.BtnTransferPdf, "Verarbeitungs-Hinweis in die Produtkion übertragen")
        Me.BtnTransferPdf.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.BtnTransferPdf)
        Me.Panel2.Controls.Add(Me.lblArtikelHinweis)
        Me.Panel2.Controls.Add(Me.BtnLoadPdf)
        Me.Panel2.Controls.Add(Me.cbAufloesung)
        Me.Panel2.Controls.Add(Me.tHinweisName)
        Me.Panel2.Controls.Add(Me.lblAufloesung)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(394, 54)
        Me.Panel2.TabIndex = 83
        '
        'lblArtikelHinweis
        '
        Me.lblArtikelHinweis.AutoSize = True
        Me.lblArtikelHinweis.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblArtikelHinweis.Location = New System.Drawing.Point(6, 6)
        Me.lblArtikelHinweis.Name = "lblArtikelHinweis"
        Me.lblArtikelHinweis.Size = New System.Drawing.Size(144, 13)
        Me.lblArtikelHinweis.TabIndex = 82
        Me.lblArtikelHinweis.Text = "Artikel Verarbeitungs-Hinweis"
        '
        'BtnLoadPdf
        '
        Me.BtnLoadPdf.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLoadPdf.Location = New System.Drawing.Point(155, 14)
        Me.BtnLoadPdf.Name = "BtnLoadPdf"
        Me.BtnLoadPdf.Size = New System.Drawing.Size(30, 30)
        Me.BtnLoadPdf.TabIndex = 76
        Me.BtnLoadPdf.Text = "..."
        Me.BtnLoadPdf.UseVisualStyleBackColor = True
        '
        'cbAufloesung
        '
        Me.cbAufloesung.FormattingEnabled = True
        Me.cbAufloesung.Items.AddRange(New Object() {"Default", "96 dpi", "150 dpi", "300 dpi", "600 dpi"})
        Me.cbAufloesung.Location = New System.Drawing.Point(203, 22)
        Me.cbAufloesung.Name = "cbAufloesung"
        Me.cbAufloesung.Size = New System.Drawing.Size(57, 21)
        Me.cbAufloesung.TabIndex = 80
        Me.cbAufloesung.TabStop = False
        Me.cbAufloesung.Text = "Default"
        '
        'tHinweisName
        '
        Me.tHinweisName.Location = New System.Drawing.Point(9, 22)
        Me.tHinweisName.Name = "tHinweisName"
        Me.tHinweisName.Size = New System.Drawing.Size(140, 20)
        Me.tHinweisName.TabIndex = 78
        '
        'lblAufloesung
        '
        Me.lblAufloesung.AutoSize = True
        Me.lblAufloesung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAufloesung.Location = New System.Drawing.Point(200, 6)
        Me.lblAufloesung.Name = "lblAufloesung"
        Me.lblAufloesung.Size = New System.Drawing.Size(77, 13)
        Me.lblAufloesung.TabIndex = 81
        Me.lblAufloesung.Text = "Auflösung (dpi)"
        '
        'OpenPdfFile
        '
        Me.OpenPdfFile.DefaultExt = "pdf"
        Me.OpenPdfFile.FileName = "*.pdf"
        Me.OpenPdfFile.Title = "Artikel-Verarbeitungs-Hinweis"
        '
        'VorschauPDF
        '
        Me.VorschauPDF.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.VorschauPDF, 2)
        Me.VorschauPDF.Location = New System.Drawing.Point(3, 63)
        Me.VorschauPDF.Name = "VorschauPDF"
        Me.VorschauPDF.Size = New System.Drawing.Size(727, 562)
        Me.VorschauPDF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.VorschauPDF.TabIndex = 0
        Me.VorschauPDF.TabStop = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.VorschauPDF, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(733, 628)
        Me.TableLayoutPanel1.TabIndex = 76
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(192, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 27)
        Me.Label2.TabIndex = 83
        Me.Label2.Text = "Seite"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ob_Artikel_Verarbeitungshinweise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ob_Artikel_Verarbeitungshinweise"
        Me.Size = New System.Drawing.Size(733, 628)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.VorschauPDF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnPagePlus As System.Windows.Forms.Button
    Friend WithEvents BtnPageMinus As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BtnRotateL As System.Windows.Forms.Button
    Friend WithEvents BtnRotateR As System.Windows.Forms.Button
    Friend WithEvents BtnTransferPdf As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblArtikelHinweis As System.Windows.Forms.Label
    Friend WithEvents BtnLoadPdf As System.Windows.Forms.Button
    Friend WithEvents cbAufloesung As System.Windows.Forms.ComboBox
    Friend WithEvents tHinweisName As System.Windows.Forms.TextBox
    Friend WithEvents lblAufloesung As System.Windows.Forms.Label
    Friend WithEvents OpenPdfFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents VorschauPDF As System.Windows.Forms.PictureBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
