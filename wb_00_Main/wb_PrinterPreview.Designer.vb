<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_PrinterPreview
    Inherits System.Windows.Forms.Form

    'Public llPreview As combit.reporting.ListLabelPreviewControl

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_PrinterPreview))
        LLPreview = New combit.Reporting.ListLabelPreviewControl(components)
        BtnPrint = New System.Windows.Forms.Button()
        BtnAbbruch = New System.Windows.Forms.Button()
        BtnZoomIn = New System.Windows.Forms.Button()
        BtnZoomOut = New System.Windows.Forms.Button()
        BtnZoomFit = New System.Windows.Forms.Button()
        SuspendLayout()
        ' 
        ' LLPreview
        ' 
        LLPreview.AllowRbuttonUsage = True
        LLPreview.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        LLPreview.BackColor = Drawing.SystemColors.Control
        LLPreview.CloseMode = combit.Reporting.LlPreviewControlCloseMode.DeleteFile
        LLPreview.CurrentPage = 0
        LLPreview.ForceReadOnly = False
        LLPreview.Location = New System.Drawing.Point(14, 14)
        LLPreview.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        LLPreview.Name = "LLPreview"
        LLPreview.ShowToolbar = False
        LLPreview.ShowUnprintableArea = True
        LLPreview.Size = New System.Drawing.Size(1048, 567)
        LLPreview.SlideshowMode = False
        LLPreview.TabIndex = 5
        LLPreview.Text = "Vorschau"
        LLPreview.ToolbarButtons.Exit = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.GotoFirst = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.GotoLast = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.GotoNext = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.GotoPrev = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.MouseModeMove = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.MouseModeSelect = combit.Reporting.LlButtonState.Default
        LLPreview.ToolbarButtons.MouseModeZoom = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.NextFile = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.PageRange = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.PreviousFile = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.PrintAllPages = combit.Reporting.LlButtonState.Default
        LLPreview.ToolbarButtons.PrintCurrentPage = combit.Reporting.LlButtonState.Default
        LLPreview.ToolbarButtons.PrintToFax = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.SaveAs = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.SearchNext = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.SearchOptions = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.SearchStart = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.SearchText = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.SendTo = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.SlideshowMode = combit.Reporting.LlButtonState.Invisible
        LLPreview.ToolbarButtons.ZoomCombo = combit.Reporting.LlButtonState.Default
        LLPreview.ToolbarButtons.ZoomReset = combit.Reporting.LlButtonState.Default
        LLPreview.ToolbarButtons.ZoomRevert = combit.Reporting.LlButtonState.Default
        LLPreview.ToolbarButtons.ZoomTimes2 = combit.Reporting.LlButtonState.Default
        ' 
        ' BtnPrint
        ' 
        BtnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
        BtnPrint.Image = CType(resources.GetObject("BtnPrint.Image"), Drawing.Image)
        BtnPrint.Location = New System.Drawing.Point(830, 587)
        BtnPrint.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        BtnPrint.Name = "BtnPrint"
        BtnPrint.Size = New System.Drawing.Size(112, 44)
        BtnPrint.TabIndex = 6
        BtnPrint.Text = "Drucken"
        BtnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnPrint.UseVisualStyleBackColor = True
        ' 
        ' BtnAbbruch
        ' 
        BtnAbbruch.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
        BtnAbbruch.Image = CType(resources.GetObject("BtnAbbruch.Image"), Drawing.Image)
        BtnAbbruch.Location = New System.Drawing.Point(948, 587)
        BtnAbbruch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        BtnAbbruch.Name = "BtnAbbruch"
        BtnAbbruch.Size = New System.Drawing.Size(112, 44)
        BtnAbbruch.TabIndex = 8
        BtnAbbruch.Text = "Abbruch"
        BtnAbbruch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnAbbruch.UseVisualStyleBackColor = True
        ' 
        ' BtnZoomIn
        ' 
        BtnZoomIn.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
        BtnZoomIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        BtnZoomIn.Image = CType(resources.GetObject("BtnZoomIn.Image"), Drawing.Image)
        BtnZoomIn.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        BtnZoomIn.Location = New System.Drawing.Point(308, 587)
        BtnZoomIn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        BtnZoomIn.Name = "BtnZoomIn"
        BtnZoomIn.Size = New System.Drawing.Size(57, 44)
        BtnZoomIn.TabIndex = 9
        BtnZoomIn.Text = "+"
        BtnZoomIn.TextAlign = Drawing.ContentAlignment.MiddleRight
        BtnZoomIn.UseVisualStyleBackColor = True
        ' 
        ' BtnZoomOut
        ' 
        BtnZoomOut.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
        BtnZoomOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.75F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        BtnZoomOut.Image = CType(resources.GetObject("BtnZoomOut.Image"), Drawing.Image)
        BtnZoomOut.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        BtnZoomOut.Location = New System.Drawing.Point(372, 587)
        BtnZoomOut.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        BtnZoomOut.Name = "BtnZoomOut"
        BtnZoomOut.Size = New System.Drawing.Size(57, 44)
        BtnZoomOut.TabIndex = 10
        BtnZoomOut.Text = "-"
        BtnZoomOut.TextAlign = Drawing.ContentAlignment.MiddleRight
        BtnZoomOut.UseVisualStyleBackColor = True
        ' 
        ' BtnZoomFit
        ' 
        BtnZoomFit.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
        BtnZoomFit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        BtnZoomFit.Image = CType(resources.GetObject("BtnZoomFit.Image"), Drawing.Image)
        BtnZoomFit.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        BtnZoomFit.Location = New System.Drawing.Point(436, 587)
        BtnZoomFit.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        BtnZoomFit.Name = "BtnZoomFit"
        BtnZoomFit.Size = New System.Drawing.Size(57, 44)
        BtnZoomFit.TabIndex = 11
        BtnZoomFit.Text = "[ ]"
        BtnZoomFit.TextAlign = Drawing.ContentAlignment.MiddleRight
        BtnZoomFit.UseVisualStyleBackColor = True
        ' 
        ' wb_PrinterPreview
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(1076, 645)
        Controls.Add(BtnZoomFit)
        Controls.Add(BtnZoomOut)
        Controls.Add(BtnZoomIn)
        Controls.Add(BtnAbbruch)
        Controls.Add(BtnPrint)
        Controls.Add(LLPreview)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Name = "wb_PrinterPreview"
        Text = "Vorschau"
        ResumeLayout(False)

    End Sub

    Public WithEvents LLPreview As combit.Reporting.ListLabelPreviewControl
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
    Friend WithEvents BtnAbbruch As System.Windows.Forms.Button
    Friend WithEvents BtnZoomIn As System.Windows.Forms.Button
    Friend WithEvents BtnZoomOut As System.Windows.Forms.Button
    Friend WithEvents BtnZoomFit As System.Windows.Forms.Button
End Class
