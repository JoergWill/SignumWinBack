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
        components = New ComponentModel.Container()
        btnEditVorlage = New System.Windows.Forms.Button()
        btnExportExcel = New System.Windows.Forms.Button()
        PrintDialog = New System.Windows.Forms.PrintDialog()
        GroupBox1 = New System.Windows.Forms.GroupBox()
        lblVorlage = New System.Windows.Forms.Label()
        cbVorlageAuswahl = New System.Windows.Forms.ComboBox()
        cbPrinterAuswahl = New System.Windows.Forms.ComboBox()
        BtnPrinterDialog = New System.Windows.Forms.Button()
        gbVorschau = New System.Windows.Forms.GroupBox()
        LLPreview = New combit.Reporting.ListLabelPreviewControl(components)
        TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Cancel_Button = New System.Windows.Forms.Button()
        OK_Button = New System.Windows.Forms.Button()
        BtnVorschau = New System.Windows.Forms.Button()
        BtnDruckHistorie = New System.Windows.Forms.Button()
        OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        GroupBox1.SuspendLayout()
        gbVorschau.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnEditVorlage
        ' 
        btnEditVorlage.Image = Global.WinBack.My.Resources.ListUndLabel_32x32
        btnEditVorlage.Location = New System.Drawing.Point(21, 330)
        btnEditVorlage.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        btnEditVorlage.Name = "btnEditVorlage"
        btnEditVorlage.Size = New System.Drawing.Size(128, 50)
        btnEditVorlage.TabIndex = 1
        btnEditVorlage.Text = "Designer..."
        btnEditVorlage.TextAlign = Drawing.ContentAlignment.MiddleRight
        btnEditVorlage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        btnEditVorlage.UseVisualStyleBackColor = True
        ' 
        ' btnExportExcel
        ' 
        btnExportExcel.Image = Global.WinBack.My.Resources.Excel_32x32
        btnExportExcel.Location = New System.Drawing.Point(21, 273)
        btnExportExcel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        btnExportExcel.Name = "btnExportExcel"
        btnExportExcel.Size = New System.Drawing.Size(128, 50)
        btnExportExcel.TabIndex = 2
        btnExportExcel.Text = "Excel Export"
        btnExportExcel.TextAlign = Drawing.ContentAlignment.MiddleRight
        btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        btnExportExcel.UseVisualStyleBackColor = True
        ' 
        ' PrintDialog
        ' 
        PrintDialog.AllowPrintToFile = False
        PrintDialog.AllowSomePages = True
        PrintDialog.UseEXDialog = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(lblVorlage)
        GroupBox1.Controls.Add(cbVorlageAuswahl)
        GroupBox1.Controls.Add(cbPrinterAuswahl)
        GroupBox1.Controls.Add(BtnPrinterDialog)
        GroupBox1.Location = New System.Drawing.Point(14, 14)
        GroupBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        GroupBox1.Size = New System.Drawing.Size(476, 112)
        GroupBox1.TabIndex = 6
        GroupBox1.TabStop = False
        GroupBox1.Text = "Drucker"
        ' 
        ' lblVorlage
        ' 
        lblVorlage.AutoSize = True
        lblVorlage.Location = New System.Drawing.Point(209, 60)
        lblVorlage.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblVorlage.Name = "lblVorlage"
        lblVorlage.Size = New System.Drawing.Size(55, 15)
        lblVorlage.TabIndex = 9
        lblVorlage.Text = "Formular"
        lblVorlage.Visible = False
        ' 
        ' cbVorlageAuswahl
        ' 
        cbVorlageAuswahl.FormattingEnabled = True
        cbVorlageAuswahl.Location = New System.Drawing.Point(209, 78)
        cbVorlageAuswahl.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        cbVorlageAuswahl.Name = "cbVorlageAuswahl"
        cbVorlageAuswahl.Size = New System.Drawing.Size(259, 23)
        cbVorlageAuswahl.TabIndex = 8
        cbVorlageAuswahl.Visible = False
        ' 
        ' cbPrinterAuswahl
        ' 
        cbPrinterAuswahl.FormattingEnabled = True
        cbPrinterAuswahl.Location = New System.Drawing.Point(7, 22)
        cbPrinterAuswahl.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        cbPrinterAuswahl.Name = "cbPrinterAuswahl"
        cbPrinterAuswahl.Size = New System.Drawing.Size(461, 23)
        cbPrinterAuswahl.TabIndex = 7
        ' 
        ' BtnPrinterDialog
        ' 
        BtnPrinterDialog.Enabled = False
        BtnPrinterDialog.Location = New System.Drawing.Point(7, 53)
        BtnPrinterDialog.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        BtnPrinterDialog.Name = "BtnPrinterDialog"
        BtnPrinterDialog.Size = New System.Drawing.Size(128, 50)
        BtnPrinterDialog.TabIndex = 6
        BtnPrinterDialog.Text = "Drucken..."
        BtnPrinterDialog.UseVisualStyleBackColor = True
        ' 
        ' gbVorschau
        ' 
        gbVorschau.Controls.Add(LLPreview)
        gbVorschau.Controls.Add(TableLayoutPanel1)
        gbVorschau.Location = New System.Drawing.Point(223, 133)
        gbVorschau.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        gbVorschau.Name = "gbVorschau"
        gbVorschau.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        gbVorschau.Size = New System.Drawing.Size(267, 331)
        gbVorschau.TabIndex = 7
        gbVorschau.TabStop = False
        gbVorschau.Text = "Vorschau"
        ' 
        ' LLPreview
        ' 
        LLPreview.AllowRbuttonUsage = True
        LLPreview.BackColor = Drawing.SystemColors.Control
        LLPreview.CurrentPage = 0
        LLPreview.ForceReadOnly = False
        LLPreview.Location = New System.Drawing.Point(9, 22)
        LLPreview.Name = "LLPreview"
        LLPreview.PageStyle.ShowPageNumbers = False
        LLPreview.ShowThumbnails = False
        LLPreview.ShowToolbar = False
        LLPreview.Size = New System.Drawing.Size(250, 243)
        LLPreview.SlideshowMode = False
        LLPreview.TabIndex = 6
        LLPreview.Text = "ListLabelPreviewControl1"
        LLPreview.ToolbarButtons.Exit = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.GotoFirst = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.GotoLast = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.GotoNext = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.GotoPrev = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.MouseModeMove = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.MouseModeSelect = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.MouseModeZoom = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.NextFile = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.PageRange = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.PreviousFile = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.PrintAllPages = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.PrintCurrentPage = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.PrintToFax = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.SaveAs = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.SearchNext = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.SearchOptions = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.SearchStart = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.SearchText = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.SendTo = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.SlideshowMode = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.ZoomCombo = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.ZoomReset = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.ZoomRevert = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.ZoomTimes2 = combit.Reporting.LlButtonState.Invisible
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F))
        TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F))
        TableLayoutPanel1.Controls.Add(Cancel_Button, 1, 0)
        TableLayoutPanel1.Controls.Add(OK_Button, 0, 0)
        TableLayoutPanel1.Location = New System.Drawing.Point(9, 271)
        TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F))
        TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F))
        TableLayoutPanel1.Size = New System.Drawing.Size(251, 53)
        TableLayoutPanel1.TabIndex = 5
        ' 
        ' Cancel_Button
        ' 
        Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Cancel_Button.Location = New System.Drawing.Point(129, 7)
        Cancel_Button.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Cancel_Button.Name = "Cancel_Button"
        Cancel_Button.Size = New System.Drawing.Size(118, 39)
        Cancel_Button.TabIndex = 1
        Cancel_Button.Text = "Abbrechen"
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        OK_Button.Location = New System.Drawing.Point(4, 7)
        OK_Button.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New System.Drawing.Size(117, 39)
        OK_Button.TabIndex = 0
        OK_Button.Text = "Drucken"
        ' 
        ' BtnVorschau
        ' 
        BtnVorschau.Image = Global.WinBack.My.Resources.EditKonfig_32x32
        BtnVorschau.Location = New System.Drawing.Point(21, 400)
        BtnVorschau.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        BtnVorschau.Name = "BtnVorschau"
        BtnVorschau.Size = New System.Drawing.Size(128, 50)
        BtnVorschau.TabIndex = 8
        BtnVorschau.Text = "Vorschau"
        BtnVorschau.TextAlign = Drawing.ContentAlignment.MiddleRight
        BtnVorschau.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnVorschau.UseVisualStyleBackColor = True
        ' 
        ' BtnDruckHistorie
        ' 
        BtnDruckHistorie.Image = Global.WinBack.My.Resources.AdminTimer_32x32
        BtnDruckHistorie.Location = New System.Drawing.Point(21, 138)
        BtnDruckHistorie.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        BtnDruckHistorie.Name = "BtnDruckHistorie"
        BtnDruckHistorie.Size = New System.Drawing.Size(128, 50)
        BtnDruckHistorie.TabIndex = 9
        BtnDruckHistorie.Text = "Druckhistorie"
        BtnDruckHistorie.TextAlign = Drawing.ContentAlignment.MiddleRight
        BtnDruckHistorie.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnDruckHistorie.UseVisualStyleBackColor = True
        ' 
        ' OpenFileDialog
        ' 
        OpenFileDialog.FileName = "OpenFileDialog"
        ' 
        ' wb_PrinterDialog
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(507, 478)
        Controls.Add(BtnDruckHistorie)
        Controls.Add(BtnVorschau)
        Controls.Add(gbVorschau)
        Controls.Add(GroupBox1)
        Controls.Add(btnExportExcel)
        Controls.Add(btnEditVorlage)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "wb_PrinterDialog"
        ShowInTaskbar = False
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Text = "Drucken/Exportieren"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        gbVorschau.ResumeLayout(False)
        TableLayoutPanel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub
    Friend WithEvents btnEditVorlage As System.Windows.Forms.Button
    Friend WithEvents btnExportExcel As System.Windows.Forms.Button
    Friend WithEvents PrintDialog As System.Windows.Forms.PrintDialog
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbPrinterAuswahl As System.Windows.Forms.ComboBox
    Friend WithEvents BtnPrinterDialog As System.Windows.Forms.Button
    Friend WithEvents gbVorschau As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents BtnVorschau As System.Windows.Forms.Button
    Friend WithEvents BtnDruckHistorie As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblVorlage As System.Windows.Forms.Label
    Friend WithEvents cbVorlageAuswahl As System.Windows.Forms.ComboBox
    Friend WithEvents LLPreview As combit.Reporting.ListLabelPreviewControl
End Class
