Imports Signum.OrgaSoft

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AddressDockingUserControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PropertyGrid = New Signum.OrgaSoft.GUI.Controls.PropertyGrid()
        Me.SuspendLayout()
        '
        'PropertyGrid
        '
        Me.PropertyGrid.CommandsVisibleIfAvailable = False
        Me.PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid.Location = New System.Drawing.Point(0, 0)
        Me.PropertyGrid.Name = "PropertyGrid"
        Me.PropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.PropertyGrid.Size = New System.Drawing.Size(190, 268)
        Me.PropertyGrid.TabIndex = 0
        Me.PropertyGrid.ToolbarVisible = False
        '
        'AddressDockingUserControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoScrollMinSize = New System.Drawing.Size(190, 268)
        Me.Controls.Add(Me.PropertyGrid)
        Me.MinimumSize = New System.Drawing.Size(190, 268)
        Me.Name = "AddressDockingUserControl"
        Me.Size = New System.Drawing.Size(190, 268)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PropertyGrid As GUI.Controls.PropertyGrid
End Class
