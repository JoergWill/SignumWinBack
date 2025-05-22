Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_XNumber
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
        Me.lblSortimente = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnImportRezepte = New System.Windows.Forms.Button()
        Me.BtnImportRohstoffe = New System.Windows.Forms.Button()
        Me.BtnTemplateRezepte = New System.Windows.Forms.Button()
        Me.BtnTemplateRohstoffe = New System.Windows.Forms.Button()
        Me.BtnImportArtikel = New System.Windows.Forms.Button()
        Me.lblLog = New System.Windows.Forms.Label()
        Me.BtnTemplateArtikel = New System.Windows.Forms.Button()
        Me.tbLogger = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbGelesen = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSortimente
        '
        Me.lblSortimente.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSortimente.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSortimente.Location = New System.Drawing.Point(3, 0)
        Me.lblSortimente.Name = "lblSortimente"
        Me.lblSortimente.Size = New System.Drawing.Size(144, 50)
        Me.lblSortimente.TabIndex = 3
        Me.lblSortimente.Text = "Template erzeugen"
        Me.lblSortimente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(163, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 50)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Nummern ändern"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.BtnImportRezepte, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnImportRohstoffe, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnTemplateRezepte, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnTemplateRohstoffe, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnImportArtikel, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblLog, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblSortimente, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnTemplateArtikel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.tbLogger, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 4)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(680, 348)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'BtnImportRezepte
        '
        Me.BtnImportRezepte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnImportRezepte.Enabled = False
        Me.BtnImportRezepte.Location = New System.Drawing.Point(165, 155)
        Me.BtnImportRezepte.Margin = New System.Windows.Forms.Padding(5)
        Me.BtnImportRezepte.Name = "BtnImportRezepte"
        Me.BtnImportRezepte.Size = New System.Drawing.Size(150, 40)
        Me.BtnImportRezepte.TabIndex = 15
        Me.BtnImportRezepte.TabStop = False
        Me.BtnImportRezepte.Text = "Import Rezept-Nummern"
        Me.BtnImportRezepte.UseVisualStyleBackColor = True
        '
        'BtnImportRohstoffe
        '
        Me.BtnImportRohstoffe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnImportRohstoffe.Enabled = False
        Me.BtnImportRohstoffe.Location = New System.Drawing.Point(165, 105)
        Me.BtnImportRohstoffe.Margin = New System.Windows.Forms.Padding(5)
        Me.BtnImportRohstoffe.Name = "BtnImportRohstoffe"
        Me.BtnImportRohstoffe.Size = New System.Drawing.Size(150, 40)
        Me.BtnImportRohstoffe.TabIndex = 14
        Me.BtnImportRohstoffe.TabStop = False
        Me.BtnImportRohstoffe.Text = "Import Rohstoff-Nummern"
        Me.BtnImportRohstoffe.UseVisualStyleBackColor = True
        '
        'BtnTemplateRezepte
        '
        Me.BtnTemplateRezepte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnTemplateRezepte.Location = New System.Drawing.Point(5, 155)
        Me.BtnTemplateRezepte.Margin = New System.Windows.Forms.Padding(5)
        Me.BtnTemplateRezepte.Name = "BtnTemplateRezepte"
        Me.BtnTemplateRezepte.Size = New System.Drawing.Size(150, 40)
        Me.BtnTemplateRezepte.TabIndex = 13
        Me.BtnTemplateRezepte.TabStop = False
        Me.BtnTemplateRezepte.Text = "Template-Rezepte"
        Me.BtnTemplateRezepte.UseVisualStyleBackColor = True
        '
        'BtnTemplateRohstoffe
        '
        Me.BtnTemplateRohstoffe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnTemplateRohstoffe.Location = New System.Drawing.Point(5, 105)
        Me.BtnTemplateRohstoffe.Margin = New System.Windows.Forms.Padding(5)
        Me.BtnTemplateRohstoffe.Name = "BtnTemplateRohstoffe"
        Me.BtnTemplateRohstoffe.Size = New System.Drawing.Size(150, 40)
        Me.BtnTemplateRohstoffe.TabIndex = 11
        Me.BtnTemplateRohstoffe.TabStop = False
        Me.BtnTemplateRohstoffe.Text = "Template-Rohstoffe"
        Me.BtnTemplateRohstoffe.UseVisualStyleBackColor = True
        '
        'BtnImportArtikel
        '
        Me.BtnImportArtikel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnImportArtikel.Enabled = False
        Me.BtnImportArtikel.Location = New System.Drawing.Point(165, 55)
        Me.BtnImportArtikel.Margin = New System.Windows.Forms.Padding(5)
        Me.BtnImportArtikel.Name = "BtnImportArtikel"
        Me.BtnImportArtikel.Size = New System.Drawing.Size(150, 40)
        Me.BtnImportArtikel.TabIndex = 7
        Me.BtnImportArtikel.TabStop = False
        Me.BtnImportArtikel.Text = "Import Artikel-Nummern"
        Me.BtnImportArtikel.UseVisualStyleBackColor = True
        '
        'lblLog
        '
        Me.lblLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLog.Location = New System.Drawing.Point(323, 0)
        Me.lblLog.Name = "lblLog"
        Me.lblLog.Size = New System.Drawing.Size(354, 50)
        Me.lblLog.TabIndex = 6
        Me.lblLog.Text = "Log-Ausgabe"
        Me.lblLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnTemplateArtikel
        '
        Me.BtnTemplateArtikel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnTemplateArtikel.Location = New System.Drawing.Point(5, 55)
        Me.BtnTemplateArtikel.Margin = New System.Windows.Forms.Padding(5)
        Me.BtnTemplateArtikel.Name = "BtnTemplateArtikel"
        Me.BtnTemplateArtikel.Size = New System.Drawing.Size(150, 40)
        Me.BtnTemplateArtikel.TabIndex = 5
        Me.BtnTemplateArtikel.TabStop = False
        Me.BtnTemplateArtikel.Text = "Template-Artikel"
        Me.BtnTemplateArtikel.UseVisualStyleBackColor = True
        '
        'tbLogger
        '
        Me.tbLogger.BackColor = System.Drawing.Color.White
        Me.tbLogger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbLogger.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbLogger.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbLogger.Location = New System.Drawing.Point(323, 53)
        Me.tbLogger.Multiline = True
        Me.tbLogger.Name = "tbLogger"
        Me.tbLogger.ReadOnly = True
        Me.TableLayoutPanel1.SetRowSpan(Me.tbLogger, 4)
        Me.tbLogger.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbLogger.Size = New System.Drawing.Size(354, 292)
        Me.tbLogger.TabIndex = 8
        Me.tbLogger.TabStop = False
        '
        'Panel1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Panel1, 2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.cbGelesen)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 203)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(314, 142)
        Me.Panel1.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(2, 77)
        Me.Label2.Margin = New System.Windows.Forms.Padding(5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(307, 33)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Diese Funktion ändert alle Nummern (Artikel/Rohstoff/Rezept) in WinBack anhand ei" &
    "ner Umsetzungstabelle."
        '
        'cbGelesen
        '
        Me.cbGelesen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbGelesen.AutoSize = True
        Me.cbGelesen.Location = New System.Drawing.Point(5, 120)
        Me.cbGelesen.Margin = New System.Windows.Forms.Padding(5)
        Me.cbGelesen.Name = "cbGelesen"
        Me.cbGelesen.Size = New System.Drawing.Size(142, 17)
        Me.cbGelesen.TabIndex = 11
        Me.cbGelesen.Text = "Gelesen und verstanden"
        Me.cbGelesen.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(2, 41)
        Me.Label3.Margin = New System.Windows.Forms.Padding(5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(307, 33)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "ACHTUNG"
        '
        'wb_Admin_XNumber
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(680, 348)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HideOnClose = True
        Me.Name = "wb_Admin_XNumber"
        Me.Text = "Anpassen WinBack-Nummern"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblSortimente As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BtnImportArtikel As System.Windows.Forms.Button
    Friend WithEvents lblLog As System.Windows.Forms.Label
    Friend WithEvents BtnTemplateArtikel As System.Windows.Forms.Button
    Friend WithEvents tbLogger As System.Windows.Forms.TextBox
    Friend WithEvents BtnTemplateRezepte As System.Windows.Forms.Button
    Friend WithEvents BtnTemplateRohstoffe As System.Windows.Forms.Button
    Friend WithEvents BtnImportRezepte As System.Windows.Forms.Button
    Friend WithEvents BtnImportRohstoffe As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbGelesen As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
