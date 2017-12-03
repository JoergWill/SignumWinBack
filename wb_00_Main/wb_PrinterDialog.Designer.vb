<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_PrinterDialog
    Inherits System.Windows.Forms.Form

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
        Me.btnEditVorlage = New System.Windows.Forms.Button()
        Me.btnExportExcel = New System.Windows.Forms.Button()
        Me.PrintDialog = New System.Windows.Forms.PrintDialog()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbPrinterAuswahl = New System.Windows.Forms.ComboBox()
        Me.BtnPrinterDialog = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.LLPreview = New combit.ListLabel22.ListLabelPreviewControl(Me.components)
        Me.BtnVorschau = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnEditVorlage
        '
        Me.btnEditVorlage.Image = Global.WinBack.My.Resources.Resources.ListUndLabel_32x32
        Me.btnEditVorlage.Location = New System.Drawing.Point(18, 286)
        Me.btnEditVorlage.Name = "btnEditVorlage"
        Me.btnEditVorlage.Size = New System.Drawing.Size(110, 43)
        Me.btnEditVorlage.TabIndex = 1
        Me.btnEditVorlage.Text = "Designer..."
        Me.btnEditVorlage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditVorlage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEditVorlage.UseVisualStyleBackColor = True
        '
        'btnExportExcel
        '
        Me.btnExportExcel.Image = Global.WinBack.My.Resources.Resources.Excel_32x32
        Me.btnExportExcel.Location = New System.Drawing.Point(18, 237)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(110, 43)
        Me.btnExportExcel.TabIndex = 2
        Me.btnExportExcel.Text = "Excel Export"
        Me.btnExportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExportExcel.UseVisualStyleBackColor = True
        '
        'PrintDialog
        '
        Me.PrintDialog.AllowPrintToFile = False
        Me.PrintDialog.AllowSomePages = True
        Me.PrintDialog.UseEXDialog = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbPrinterAuswahl)
        Me.GroupBox1.Controls.Add(Me.BtnPrinterDialog)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(408, 97)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Drucker"
        '
        'cbPrinterAuswahl
        '
        Me.cbPrinterAuswahl.FormattingEnabled = True
        Me.cbPrinterAuswahl.Location = New System.Drawing.Point(6, 19)
        Me.cbPrinterAuswahl.Name = "cbPrinterAuswahl"
        Me.cbPrinterAuswahl.Size = New System.Drawing.Size(396, 21)
        Me.cbPrinterAuswahl.TabIndex = 7
        '
        'BtnPrinterDialog
        '
        Me.BtnPrinterDialog.Enabled = False
        Me.BtnPrinterDialog.Location = New System.Drawing.Point(6, 46)
        Me.BtnPrinterDialog.Name = "BtnPrinterDialog"
        Me.BtnPrinterDialog.Size = New System.Drawing.Size(110, 43)
        Me.BtnPrinterDialog.TabIndex = 6
        Me.BtnPrinterDialog.Text = "Drucken..."
        Me.BtnPrinterDialog.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupBox2.Controls.Add(Me.LLPreview)
        Me.GroupBox2.Location = New System.Drawing.Point(191, 115)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(229, 287)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Vorschau"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(8, 235)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(215, 46)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(110, 6)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(102, 34)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Abbrechen"
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 6)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(101, 34)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Drucken"
        '
        'LLPreview
        '
        Me.LLPreview.AllowRbuttonUsage = True
        Me.LLPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LLPreview.BackColor = System.Drawing.SystemColors.Control
        Me.LLPreview.CurrentPage = 0
        Me.LLPreview.ForceReadOnly = False
        Me.LLPreview.Location = New System.Drawing.Point(8, 28)
        Me.LLPreview.Name = "LLPreview"
        Me.LLPreview.ShowThumbnails = False
        Me.LLPreview.ShowToolbar = False
        Me.LLPreview.ShowUnprintableArea = True
        Me.LLPreview.Size = New System.Drawing.Size(215, 201)
        Me.LLPreview.SlideshowMode = False
        Me.LLPreview.TabIndex = 4
        Me.LLPreview.Text = "Vorschau"
        Me.LLPreview.ToolbarButtons.Exit = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.GotoFirst = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.GotoLast = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.GotoNext = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.GotoPrev = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.MouseModeMove = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.MouseModeZoom = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.NextFile = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.PageRange = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.PreviousFile = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.PrintAllPages = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.PrintCurrentPage = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.PrintToFax = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.SaveAs = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.SearchNext = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.SearchOptions = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.SearchStart = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.SearchText = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.SendTo = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.SlideshowMode = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.ZoomCombo = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.ZoomReset = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.ZoomRevert = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.ZoomTimes2 = combit.ListLabel22.LlButtonState.[Default]
        '
        'BtnVorschau
        '
        Me.BtnVorschau.Image = Global.WinBack.My.Resources.Resources.EditKonfig_32x32
        Me.BtnVorschau.Location = New System.Drawing.Point(18, 347)
        Me.BtnVorschau.Name = "BtnVorschau"
        Me.BtnVorschau.Size = New System.Drawing.Size(110, 43)
        Me.BtnVorschau.TabIndex = 8
        Me.BtnVorschau.Text = "Vorschau"
        Me.BtnVorschau.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnVorschau.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnVorschau.UseVisualStyleBackColor = True
        '
        'wb_PrinterDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 414)
        Me.Controls.Add(Me.BtnVorschau)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnExportExcel)
        Me.Controls.Add(Me.btnEditVorlage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wb_PrinterDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Drucken/Exportieren"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnEditVorlage As Windows.Forms.Button
    Friend WithEvents btnExportExcel As Windows.Forms.Button
    Friend WithEvents PrintDialog As Windows.Forms.PrintDialog
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents cbPrinterAuswahl As Windows.Forms.ComboBox
    Friend WithEvents BtnPrinterDialog As Windows.Forms.Button
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel1 As Windows.Forms.TableLayoutPanel
    Friend WithEvents Cancel_Button As Windows.Forms.Button
    Friend WithEvents OK_Button As Windows.Forms.Button
    Friend WithEvents LLPreview As combit.ListLabel22.ListLabelPreviewControl
    Friend WithEvents BtnVorschau As Windows.Forms.Button
End Class
