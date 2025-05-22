Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Dashboard_Grid
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
        Me.components = New System.ComponentModel.Container()
        Me.FlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.PopUpDash = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cbDashBoard = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel
        '
        Me.FlowLayoutPanel.ContextMenuStrip = Me.PopUpDash
        Me.FlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel.Name = "FlowLayoutPanel"
        Me.FlowLayoutPanel.Size = New System.Drawing.Size(1229, 523)
        Me.FlowLayoutPanel.TabIndex = 0
        '
        'PopUpDash
        '
        Me.PopUpDash.Name = "PopUpDash"
        Me.PopUpDash.Size = New System.Drawing.Size(61, 4)
        '
        'cbDashBoard
        '
        Me.cbDashBoard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbDashBoard.AutoSize = True
        Me.cbDashBoard.Location = New System.Drawing.Point(1052, 529)
        Me.cbDashBoard.Name = "cbDashBoard"
        Me.cbDashBoard.Size = New System.Drawing.Size(165, 17)
        Me.cbDashBoard.TabIndex = 1
        Me.cbDashBoard.Text = "Beim Programmstart anzeigen"
        Me.cbDashBoard.UseVisualStyleBackColor = True
        '
        'wb_Dashboard_Grid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1229, 551)
        Me.CloseButton = False
        Me.CloseButtonVisible = False
        Me.Controls.Add(Me.cbDashBoard)
        Me.Controls.Add(Me.FlowLayoutPanel)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "wb_Dashboard_Grid"
        Me.Text = "Dashboard"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents cbDashBoard As System.Windows.Forms.CheckBox
    Friend WithEvents PopUpDash As System.Windows.Forms.ContextMenuStrip
End Class
