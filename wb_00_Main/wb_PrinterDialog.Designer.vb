<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_PrinterDialog
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
        Me.components = New System.ComponentModel.Container()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.btnEditVorlage = New System.Windows.Forms.Button()
        Me.btnExportExcel = New System.Windows.Forms.Button()
        Me.LLPreview = New combit.ListLabel22.ListLabelPreviewControl(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(208, 356)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(215, 46)
        Me.TableLayoutPanel1.TabIndex = 0
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
        'btnEditVorlage
        '
        Me.btnEditVorlage.Image = Global.WinBack.My.Resources.Resources.ListUndLabel_32x32
        Me.btnEditVorlage.Location = New System.Drawing.Point(12, 359)
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
        Me.btnExportExcel.Location = New System.Drawing.Point(12, 310)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(110, 43)
        Me.btnExportExcel.TabIndex = 2
        Me.btnExportExcel.Text = "Excel Export"
        Me.btnExportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExportExcel.UseVisualStyleBackColor = True
        '
        'LLPreview
        '
        Me.LLPreview.AllowRbuttonUsage = True
        Me.LLPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LLPreview.BackColor = System.Drawing.SystemColors.Control
        Me.LLPreview.CurrentPage = 0
        Me.LLPreview.ForceReadOnly = False
        Me.LLPreview.Location = New System.Drawing.Point(208, 149)
        Me.LLPreview.Name = "LLPreview"
        Me.LLPreview.ShowThumbnails = False
        Me.LLPreview.ShowToolbar = False
        Me.LLPreview.ShowUnprintableArea = True
        Me.LLPreview.Size = New System.Drawing.Size(215, 201)
        Me.LLPreview.SlideshowMode = False
        Me.LLPreview.TabIndex = 3
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
        'wb_PrinterDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 414)
        Me.Controls.Add(Me.LLPreview)
        Me.Controls.Add(Me.btnExportExcel)
        Me.Controls.Add(Me.btnEditVorlage)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wb_PrinterDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Drucken/Exportieren"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents btnEditVorlage As Windows.Forms.Button
    Friend WithEvents btnExportExcel As Windows.Forms.Button
    Friend WithEvents LLPreview As combit.ListLabel22.ListLabelPreviewControl
End Class
