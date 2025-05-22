<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_Rohstoffe_Dokumente
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
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.lbDokumente = New System.Windows.Forms.ListBox()
        Me.PnlBearbeiten = New System.Windows.Forms.Panel()
        Me.lblBild = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BtnOpenPage = New System.Windows.Forms.Button()
        Me.BtnPagePlus = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnPageMinus = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnRotateL = New System.Windows.Forms.Button()
        Me.BtnRotateR = New System.Windows.Forms.Button()
        Me.VorschauPDF = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel.SuspendLayout()
        Me.PnlBearbeiten.SuspendLayout()
        CType(Me.VorschauPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel
        '
        Me.TableLayoutPanel.ColumnCount = 2
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel.Controls.Add(Me.lbDokumente, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.PnlBearbeiten, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.VorschauPDF, 1, 0)
        Me.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 2
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel.Size = New System.Drawing.Size(885, 577)
        Me.TableLayoutPanel.TabIndex = 2
        '
        'lbDokumente
        '
        Me.lbDokumente.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbDokumente.FormattingEnabled = True
        Me.lbDokumente.Location = New System.Drawing.Point(3, 3)
        Me.lbDokumente.Name = "lbDokumente"
        Me.TableLayoutPanel.SetRowSpan(Me.lbDokumente, 2)
        Me.lbDokumente.Size = New System.Drawing.Size(194, 571)
        Me.lbDokumente.TabIndex = 1
        '
        'PnlBearbeiten
        '
        Me.PnlBearbeiten.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlBearbeiten.Controls.Add(Me.lblBild)
        Me.PnlBearbeiten.Controls.Add(Me.Button1)
        Me.PnlBearbeiten.Controls.Add(Me.BtnOpenPage)
        Me.PnlBearbeiten.Controls.Add(Me.BtnPagePlus)
        Me.PnlBearbeiten.Controls.Add(Me.Label2)
        Me.PnlBearbeiten.Controls.Add(Me.BtnPageMinus)
        Me.PnlBearbeiten.Controls.Add(Me.Label1)
        Me.PnlBearbeiten.Controls.Add(Me.BtnRotateL)
        Me.PnlBearbeiten.Controls.Add(Me.BtnRotateR)
        Me.PnlBearbeiten.Location = New System.Drawing.Point(203, 500)
        Me.PnlBearbeiten.Name = "PnlBearbeiten"
        Me.PnlBearbeiten.Size = New System.Drawing.Size(679, 74)
        Me.PnlBearbeiten.TabIndex = 84
        '
        'lblBild
        '
        Me.lblBild.AutoSize = True
        Me.lblBild.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblBild.Location = New System.Drawing.Point(9, 4)
        Me.lblBild.Name = "lblBild"
        Me.lblBild.Size = New System.Drawing.Size(56, 13)
        Me.lblBild.TabIndex = 80
        Me.lblBild.Text = "Dokument"
        '
        'Button1
        '
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button1.Location = New System.Drawing.Point(95, 22)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(77, 42)
        Me.Button1.TabIndex = 79
        Me.Button1.TabStop = False
        Me.Button1.Text = "Drucken"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BtnOpenPage
        '
        Me.BtnOpenPage.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnOpenPage.Location = New System.Drawing.Point(12, 22)
        Me.BtnOpenPage.Name = "BtnOpenPage"
        Me.BtnOpenPage.Size = New System.Drawing.Size(77, 42)
        Me.BtnOpenPage.TabIndex = 78
        Me.BtnOpenPage.TabStop = False
        Me.BtnOpenPage.Text = "Öffnen"
        Me.BtnOpenPage.UseVisualStyleBackColor = True
        '
        'BtnPagePlus
        '
        Me.BtnPagePlus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPagePlus.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnPagePlus.Location = New System.Drawing.Point(592, 22)
        Me.BtnPagePlus.Name = "BtnPagePlus"
        Me.BtnPagePlus.Size = New System.Drawing.Size(77, 42)
        Me.BtnPagePlus.TabIndex = 77
        Me.BtnPagePlus.TabStop = False
        Me.BtnPagePlus.Text = "vor >>"
        Me.BtnPagePlus.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(506, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 13)
        Me.Label2.TabIndex = 76
        Me.Label2.Text = "Seite"
        '
        'BtnPageMinus
        '
        Me.BtnPageMinus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPageMinus.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnPageMinus.Location = New System.Drawing.Point(509, 22)
        Me.BtnPageMinus.Name = "BtnPageMinus"
        Me.BtnPageMinus.Size = New System.Drawing.Size(77, 42)
        Me.BtnPageMinus.TabIndex = 75
        Me.BtnPageMinus.TabStop = False
        Me.BtnPageMinus.Text = "<< zurück"
        Me.BtnPageMinus.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(410, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 74
        Me.Label1.Text = "Bild drehen"
        '
        'BtnRotateL
        '
        Me.BtnRotateL.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnRotateL.Image = Global.WinBack.My.Resources.Resources.Rotate90L_32x32
        Me.BtnRotateL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnRotateL.Location = New System.Drawing.Point(413, 22)
        Me.BtnRotateL.Name = "BtnRotateL"
        Me.BtnRotateL.Size = New System.Drawing.Size(42, 42)
        Me.BtnRotateL.TabIndex = 73
        Me.BtnRotateL.TabStop = False
        Me.BtnRotateL.UseVisualStyleBackColor = True
        '
        'BtnRotateR
        '
        Me.BtnRotateR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnRotateR.Image = Global.WinBack.My.Resources.Resources.Rotate90R_32x32
        Me.BtnRotateR.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnRotateR.Location = New System.Drawing.Point(461, 22)
        Me.BtnRotateR.Name = "BtnRotateR"
        Me.BtnRotateR.Size = New System.Drawing.Size(42, 42)
        Me.BtnRotateR.TabIndex = 2
        Me.BtnRotateR.TabStop = False
        Me.BtnRotateR.UseVisualStyleBackColor = True
        '
        'VorschauPDF
        '
        Me.VorschauPDF.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VorschauPDF.Location = New System.Drawing.Point(203, 3)
        Me.VorschauPDF.Name = "VorschauPDF"
        Me.VorschauPDF.Size = New System.Drawing.Size(679, 491)
        Me.VorschauPDF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.VorschauPDF.TabIndex = 2
        Me.VorschauPDF.TabStop = False
        '
        'wb_Rohstoffe_Dokumente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(885, 577)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.Name = "wb_Rohstoffe_Dokumente"
        Me.Text = "Produkt-Datenblätter zum Rohstoff"
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.PnlBearbeiten.ResumeLayout(False)
        Me.PnlBearbeiten.PerformLayout()
        CType(Me.VorschauPDF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbDokumente As System.Windows.Forms.ListBox
    Friend WithEvents VorschauPDF As System.Windows.Forms.PictureBox
    Friend WithEvents PnlBearbeiten As System.Windows.Forms.Panel
    Friend WithEvents BtnPagePlus As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BtnPageMinus As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnRotateL As System.Windows.Forms.Button
    Friend WithEvents BtnRotateR As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lblBild As System.Windows.Forms.Label
    Friend WithEvents BtnOpenPage As System.Windows.Forms.Button
End Class
