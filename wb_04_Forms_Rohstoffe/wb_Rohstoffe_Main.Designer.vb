<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rohstoffe_Main
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.DockPanel = New WeifenLuo.WinFormsUI.Docking.DockPanel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnSaveAs = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cbLayouts = New System.Windows.Forms.ComboBox()
        Me.BtnReload = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'DockPanel
        '
        Me.DockPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DockPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.DockPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow
        Me.DockPanel.ForeColor = System.Drawing.Color.Black
        Me.DockPanel.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel.Name = "DockPanel"
        Me.DockPanel.Size = New System.Drawing.Size(1056, 601)
        Me.DockPanel.TabIndex = 4
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackgroundImage = Global.WinBack.My.Resources.Resources.StatusStripBackground
        Me.StatusStrip1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 604)
        Me.StatusStrip1.MinimumSize = New System.Drawing.Size(0, 35)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1059, 35)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.BackgroundImage = Global.WinBack.My.Resources.Resources.StatusStripBackground
        Me.BtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BtnSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnSave.FlatAppearance.BorderSize = 0
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.Image = Global.WinBack.My.Resources.Resources.IconSave_24x24
        Me.BtnSave.Location = New System.Drawing.Point(959, 604)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(25, 35)
        Me.BtnSave.TabIndex = 6
        Me.BtnSave.TabStop = False
        Me.ToolTip.SetToolTip(Me.BtnSave, "Aktuelles Layout speichern")
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'BtnSaveAs
        '
        Me.BtnSaveAs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSaveAs.BackgroundImage = Global.WinBack.My.Resources.Resources.StatusStripBackground
        Me.BtnSaveAs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BtnSaveAs.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnSaveAs.FlatAppearance.BorderSize = 0
        Me.BtnSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveAs.Image = Global.WinBack.My.Resources.Resources.IconSaveAs_24x24
        Me.BtnSaveAs.Location = New System.Drawing.Point(987, 604)
        Me.BtnSaveAs.Name = "BtnSaveAs"
        Me.BtnSaveAs.Size = New System.Drawing.Size(25, 35)
        Me.BtnSaveAs.TabIndex = 7
        Me.BtnSaveAs.TabStop = False
        Me.ToolTip.SetToolTip(Me.BtnSaveAs, "Aktuelles Layout unter " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "anderem Namen speichern")
        Me.BtnSaveAs.UseVisualStyleBackColor = True
        '
        'BtnDelete
        '
        Me.BtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnDelete.BackgroundImage = Global.WinBack.My.Resources.Resources.StatusStripBackground
        Me.BtnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BtnDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnDelete.FlatAppearance.BorderSize = 0
        Me.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDelete.Image = Global.WinBack.My.Resources.Resources.IconDelete_24x24
        Me.BtnDelete.Location = New System.Drawing.Point(1015, 604)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(25, 39)
        Me.BtnDelete.TabIndex = 8
        Me.BtnDelete.TabStop = False
        Me.ToolTip.SetToolTip(Me.BtnDelete, "Aktuelles Layout löschen")
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'cbLayouts
        '
        Me.cbLayouts.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbLayouts.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbLayouts.FormattingEnabled = True
        Me.cbLayouts.ItemHeight = 15
        Me.cbLayouts.Location = New System.Drawing.Point(779, 609)
        Me.cbLayouts.Name = "cbLayouts"
        Me.cbLayouts.Size = New System.Drawing.Size(146, 23)
        Me.cbLayouts.TabIndex = 9
        Me.cbLayouts.TabStop = False
        Me.ToolTip.SetToolTip(Me.cbLayouts, "Liste aller gespeicherten Layouts")
        '
        'BtnReload
        '
        Me.BtnReload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnReload.BackgroundImage = Global.WinBack.My.Resources.Resources.StatusStripBackground
        Me.BtnReload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BtnReload.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnReload.FlatAppearance.BorderSize = 0
        Me.BtnReload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver
        Me.BtnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnReload.Image = Global.WinBack.My.Resources.Resources.IconReload_24x24
        Me.BtnReload.Location = New System.Drawing.Point(931, 604)
        Me.BtnReload.Name = "BtnReload"
        Me.BtnReload.Size = New System.Drawing.Size(25, 34)
        Me.BtnReload.TabIndex = 10
        Me.BtnReload.TabStop = False
        Me.ToolTip.SetToolTip(Me.BtnReload, "Layout laden")
        Me.BtnReload.UseVisualStyleBackColor = True
        '
        'wb_Rohstoffe_Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BtnReload)
        Me.Controls.Add(Me.cbLayouts)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnSaveAs)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.DockPanel)
        Me.MinimumSize = New System.Drawing.Size(532, 178)
        Me.Name = "wb_Rohstoffe_Main"
        Me.Size = New System.Drawing.Size(1059, 639)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DockPanel As WeifenLuo.WinFormsUI.Docking.DockPanel
    Friend WithEvents StatusStrip1 As Windows.Forms.StatusStrip
    Friend WithEvents BtnSave As Windows.Forms.Button
    Friend WithEvents BtnSaveAs As Windows.Forms.Button
    Friend WithEvents BtnDelete As Windows.Forms.Button
    Friend WithEvents ToolTip As Windows.Forms.ToolTip
    Friend WithEvents cbLayouts As Windows.Forms.ComboBox
    Friend WithEvents BtnReload As Windows.Forms.Button
End Class
