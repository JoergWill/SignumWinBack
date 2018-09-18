<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_PrinterPreview
    Inherits System.Windows.Forms.Form

    'Public llPreview As combit.ListLabel22.ListLabelPreviewControl

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
        Me.LLPreview = New combit.ListLabel22.ListLabelPreviewControl(Me.components)
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.BtnAbbruch = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LLPreview
        '
        Me.LLPreview.AllowRbuttonUsage = True
        Me.LLPreview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LLPreview.BackColor = System.Drawing.SystemColors.Control
        Me.LLPreview.CloseMode = combit.ListLabel22.LlPreviewControlCloseMode.DeleteFile
        Me.LLPreview.CurrentPage = 0
        Me.LLPreview.ForceReadOnly = False
        Me.LLPreview.Location = New System.Drawing.Point(12, 12)
        Me.LLPreview.Name = "LLPreview"
        Me.LLPreview.ShowToolbar = False
        Me.LLPreview.ShowUnprintableArea = True
        Me.LLPreview.Size = New System.Drawing.Size(898, 491)
        Me.LLPreview.SlideshowMode = False
        Me.LLPreview.TabIndex = 5
        Me.LLPreview.Text = "Vorschau"
        Me.LLPreview.ToolbarButtons.Exit = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.GotoFirst = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.GotoLast = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.GotoNext = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.GotoPrev = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.MouseModeMove = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.MouseModeZoom = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.NextFile = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.PageRange = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.PreviousFile = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.PrintAllPages = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.PrintCurrentPage = combit.ListLabel22.LlButtonState.[Default]
        Me.LLPreview.ToolbarButtons.PrintToFax = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.SaveAs = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.SearchNext = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.SearchOptions = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.SearchStart = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.SearchText = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.SendTo = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.SlideshowMode = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.ZoomCombo = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.ZoomReset = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.ZoomRevert = combit.ListLabel22.LlButtonState.Invisible
        Me.LLPreview.ToolbarButtons.ZoomTimes2 = combit.ListLabel22.LlButtonState.Invisible
        '
        'BtnPrint
        '
        Me.BtnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPrint.Location = New System.Drawing.Point(711, 509)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(96, 38)
        Me.BtnPrint.TabIndex = 6
        Me.BtnPrint.Text = "Drucken"
        Me.BtnPrint.UseVisualStyleBackColor = True
        '
        'BtnAbbruch
        '
        Me.BtnAbbruch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnAbbruch.Location = New System.Drawing.Point(813, 509)
        Me.BtnAbbruch.Name = "BtnAbbruch"
        Me.BtnAbbruch.Size = New System.Drawing.Size(96, 38)
        Me.BtnAbbruch.TabIndex = 8
        Me.BtnAbbruch.Text = "Abbruch"
        Me.BtnAbbruch.UseVisualStyleBackColor = True
        '
        'wb_PrinterPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(922, 559)
        Me.Controls.Add(Me.BtnAbbruch)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.LLPreview)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "wb_PrinterPreview"
        Me.Text = "Vorschau"
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents LLPreview As combit.ListLabel22.ListLabelPreviewControl
    Friend WithEvents BtnPrint As Windows.Forms.Button
    Friend WithEvents BtnAbbruch As Windows.Forms.Button
End Class
