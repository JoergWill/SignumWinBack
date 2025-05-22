Imports WeifenLuo.WinFormsUI.Docking
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Linien_Liste
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
        Me.VNCview = New System.Windows.Forms.ListView()
        Me.PictureVNC = New System.Windows.Forms.PictureBox()
        CType(Me.PictureVNC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'VNCview
        '
        Me.VNCview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VNCview.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.VNCview.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.VNCview.HideSelection = False
        Me.VNCview.Location = New System.Drawing.Point(0, 10)
        Me.VNCview.Margin = New System.Windows.Forms.Padding(6, 20, 6, 6)
        Me.VNCview.Name = "VNCview"
        Me.VNCview.Size = New System.Drawing.Size(691, 591)
        Me.VNCview.TabIndex = 2
        Me.VNCview.UseCompatibleStateImageBehavior = False
        '
        'PictureVNC
        '
        Me.PictureVNC.Image = Global.WinBack.My.Resources.Resources.MainLinien_45x34
        Me.PictureVNC.Location = New System.Drawing.Point(553, 438)
        Me.PictureVNC.Name = "PictureVNC"
        Me.PictureVNC.Size = New System.Drawing.Size(48, 36)
        Me.PictureVNC.TabIndex = 3
        Me.PictureVNC.TabStop = False
        '
        'wb_Linien_Liste
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(691, 601)
        Me.Controls.Add(Me.PictureVNC)
        Me.Controls.Add(Me.VNCview)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "wb_Linien_Liste"
        Me.Text = "WinBack Linien"
        CType(Me.PictureVNC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents VNCview As System.Windows.Forms.ListView
    Friend WithEvents PictureVNC As System.Windows.Forms.PictureBox
End Class
