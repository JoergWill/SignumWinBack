Imports WeifenLuo.WinFormsUI.Docking
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_User_Rechte
    Inherits DockContent
    'Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_User_Rechte))
        Me.TreeView = New System.Windows.Forms.TreeView()
        Me.TreeViewImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'TreeView
        '
        Me.TreeView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView.ImageIndex = 0
        Me.TreeView.ImageList = Me.TreeViewImageList
        Me.TreeView.Location = New System.Drawing.Point(0, 0)
        Me.TreeView.Name = "TreeView"
        Me.TreeView.SelectedImageIndex = 0
        Me.TreeView.Size = New System.Drawing.Size(666, 399)
        Me.TreeView.TabIndex = 0
        '
        'TreeViewImageList
        '
        Me.TreeViewImageList.ImageStream = CType(resources.GetObject("TreeViewImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TreeViewImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.TreeViewImageList.Images.SetKeyName(0, "Leer_16x16.png")
        Me.TreeViewImageList.Images.SetKeyName(1, "HakenGrn_16x16.png")
        Me.TreeViewImageList.Images.SetKeyName(2, "HakenGrn_16x16.png")
        Me.TreeViewImageList.Images.SetKeyName(3, "HakenGlb_16x16.png")
        Me.TreeViewImageList.Images.SetKeyName(4, "HakenRot_16x16.png")
        '
        'wb_User_Rechte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 399)
        Me.Controls.Add(Me.TreeView)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "wb_User_Rechte"
        Me.Text = "Mitarbeiter Rechte"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TreeView As System.Windows.Forms.TreeView
    Friend WithEvents TreeViewImageList As System.Windows.Forms.ImageList
End Class
