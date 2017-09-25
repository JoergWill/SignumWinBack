<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rezept_Main
    Inherits Global.WinBack.wb_DockBarPanelMain

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
        components = New System.ComponentModel.Container()
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    End Sub

    'Inherits System.Windows.Forms.UserControl

    ''UserControl overrides dispose to clean up the component list.
    '<System.Diagnostics.DebuggerNonUserCode()>
    'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    '    Try
    '        If disposing AndAlso components IsNot Nothing Then
    '            components.Dispose()
    '        End If
    '    Finally
    '        MyBase.Dispose(disposing)
    '    End Try
    'End Sub

    ''Required by the Windows Form Designer
    'Private components As System.ComponentModel.IContainer

    ''NOTE: The following procedure is required by the Windows Form Designer
    ''It can be modified using the Windows Form Designer.  
    ''Do not modify it using the code editor.
    '<System.Diagnostics.DebuggerStepThrough()>
    'Private Sub InitializeComponent()
    '    Me.DockPanel = New WeifenLuo.WinFormsUI.Docking.DockPanel()
    '    Me.SuspendLayout()
    '    '
    '    'DockPanel
    '    '
    '    Me.DockPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
    '        Or System.Windows.Forms.AnchorStyles.Left) _
    '        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.DockPanel.BackColor = System.Drawing.Color.Gainsboro
    '    Me.DockPanel.DockBackColor = System.Drawing.Color.Gainsboro
    '    Me.DockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow
    '    Me.DockPanel.Location = New System.Drawing.Point(0, 0)
    '    Me.DockPanel.Name = "DockPanel"
    '    Me.DockPanel.Size = New System.Drawing.Size(758, 419)
    '    Me.DockPanel.TabIndex = 4
    '    '
    '    'wb_Rezept_Main
    '    '
    '    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    '    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    '    Me.Controls.Add(Me.DockPanel)
    '    Me.MinimumSize = New System.Drawing.Size(532, 178)
    '    Me.Name = "wb_Rezept_Main"
    '    Me.Size = New System.Drawing.Size(761, 422)
    '    Me.ResumeLayout(False)

    'End Sub

    'Friend WithEvents DockPanel As WeifenLuo.WinFormsUI.Docking.DockPanel
End Class
