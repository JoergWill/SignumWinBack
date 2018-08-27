Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rohstoffe_Nwt
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
        Me.pnl_Nwt = New System.Windows.Forms.Panel()
        Me.tbDeklarationExtern = New System.Windows.Forms.TextBox()
        Me.tbDeklarationIntern = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'pnl_Nwt
        '
        Me.pnl_Nwt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnl_Nwt.Location = New System.Drawing.Point(12, 12)
        Me.pnl_Nwt.Name = "pnl_Nwt"
        Me.pnl_Nwt.Size = New System.Drawing.Size(1138, 473)
        Me.pnl_Nwt.TabIndex = 0
        '
        'tbDeklarationExtern
        '
        Me.tbDeklarationExtern.Location = New System.Drawing.Point(12, 512)
        Me.tbDeklarationExtern.Multiline = True
        Me.tbDeklarationExtern.Name = "tbDeklarationExtern"
        Me.tbDeklarationExtern.Size = New System.Drawing.Size(319, 52)
        Me.tbDeklarationExtern.TabIndex = 42
        '
        'tbDeklarationIntern
        '
        Me.tbDeklarationIntern.Location = New System.Drawing.Point(12, 570)
        Me.tbDeklarationIntern.Multiline = True
        Me.tbDeklarationIntern.Name = "tbDeklarationIntern"
        Me.tbDeklarationIntern.Size = New System.Drawing.Size(319, 52)
        Me.tbDeklarationIntern.TabIndex = 43
        '
        'wb_Rohstoffe_Nwt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1162, 649)
        Me.Controls.Add(Me.tbDeklarationIntern)
        Me.Controls.Add(Me.tbDeklarationExtern)
        Me.Controls.Add(Me.pnl_Nwt)
        Me.Name = "wb_Rohstoffe_Nwt"
        Me.Text = "wb_Rohstoffe_Nwt"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnl_Nwt As Windows.Forms.Panel
    Friend WithEvents tbDeklarationExtern As Windows.Forms.TextBox
    Friend WithEvents tbDeklarationIntern As Windows.Forms.TextBox
End Class
