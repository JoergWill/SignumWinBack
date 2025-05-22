<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WinBack_DockMain
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
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.tsVersion = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsIpAdresse = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsKundeName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnSaveAs = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cbLayouts = New System.Windows.Forms.ComboBox()
        Me.BtnReload = New System.Windows.Forms.Button()
        Me.wbDockPanel = New WeifenLuo.WinFormsUI.Docking.DockPanel()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip
        '
        Me.StatusStrip.BackgroundImage = Global.WinBack.My.Resources.Resources.StatusStripBackground
        Me.StatusStrip.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsVersion, Me.tsIpAdresse, Me.tsKundeName})
        Me.StatusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.StatusStrip.Location = New System.Drawing.Point(0, 604)
        Me.StatusStrip.MinimumSize = New System.Drawing.Size(0, 35)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1059, 35)
        Me.StatusStrip.TabIndex = 5
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'tsVersion
        '
        Me.tsVersion.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsVersion.Name = "tsVersion"
        Me.tsVersion.Size = New System.Drawing.Size(171, 30)
        Me.tsVersion.Text = "OrgaBack-Produktion V1.2.0.0"
        '
        'tsIpAdresse
        '
        Me.tsIpAdresse.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsIpAdresse.Name = "tsIpAdresse"
        Me.tsIpAdresse.Size = New System.Drawing.Size(80, 30)
        Me.tsIpAdresse.Text = "(172.16.17.5)"
        '
        'tsKundeName
        '
        Me.tsKundeName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsKundeName.Name = "tsKundeName"
        Me.tsKundeName.Size = New System.Drawing.Size(66, 30)
        Me.tsKundeName.Text = "TestKunde"
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
        'wbDockPanel
        '
        Me.wbDockPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.wbDockPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.wbDockPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.wbDockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow
        Me.wbDockPanel.ForeColor = System.Drawing.Color.Black
        Me.wbDockPanel.Location = New System.Drawing.Point(0, 0)
        Me.wbDockPanel.Name = "wbDockPanel"
        Me.wbDockPanel.Size = New System.Drawing.Size(1056, 601)
        Me.wbDockPanel.TabIndex = 4
        '
        'WinBack_DockMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnSaveAs)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.BtnReload)
        Me.Controls.Add(Me.cbLayouts)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.wbDockPanel)
        Me.MinimumSize = New System.Drawing.Size(532, 178)
        Me.Name = "WinBack_DockMain"
        Me.Size = New System.Drawing.Size(1059, 639)
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents wbDockPanel As WeifenLuo.WinFormsUI.Docking.DockPanel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents BtnSaveAs As System.Windows.Forms.Button
    Friend WithEvents BtnDelete As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents cbLayouts As System.Windows.Forms.ComboBox
    Friend WithEvents BtnReload As System.Windows.Forms.Button
    Friend WithEvents tsVersion As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsIpAdresse As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsKundeName As System.Windows.Forms.ToolStripStatusLabel
End Class
