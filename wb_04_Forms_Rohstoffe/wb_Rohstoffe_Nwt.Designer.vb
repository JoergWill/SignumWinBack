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
        Me.lblDeklIntern = New System.Windows.Forms.Label()
        Me.lblDeklExtern = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'pnl_Nwt
        '
        Me.pnl_Nwt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnl_Nwt.Location = New System.Drawing.Point(12, 12)
        Me.pnl_Nwt.Name = "pnl_Nwt"
        Me.pnl_Nwt.Size = New System.Drawing.Size(821, 345)
        Me.pnl_Nwt.TabIndex = 0
        '
        'tbDeklarationExtern
        '
        Me.tbDeklarationExtern.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbDeklarationExtern.Location = New System.Drawing.Point(12, 383)
        Me.tbDeklarationExtern.Multiline = True
        Me.tbDeklarationExtern.Name = "tbDeklarationExtern"
        Me.tbDeklarationExtern.Size = New System.Drawing.Size(319, 52)
        Me.tbDeklarationExtern.TabIndex = 42
        '
        'tbDeklarationIntern
        '
        Me.tbDeklarationIntern.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbDeklarationIntern.Location = New System.Drawing.Point(337, 383)
        Me.tbDeklarationIntern.Multiline = True
        Me.tbDeklarationIntern.Name = "tbDeklarationIntern"
        Me.tbDeklarationIntern.Size = New System.Drawing.Size(319, 52)
        Me.tbDeklarationIntern.TabIndex = 43
        '
        'lblDeklIntern
        '
        Me.lblDeklIntern.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDeklIntern.AutoSize = True
        Me.lblDeklIntern.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDeklIntern.Location = New System.Drawing.Point(337, 367)
        Me.lblDeklIntern.Name = "lblDeklIntern"
        Me.lblDeklIntern.Size = New System.Drawing.Size(90, 13)
        Me.lblDeklIntern.TabIndex = 52
        Me.lblDeklIntern.Text = "Deklaration intern"
        '
        'lblDeklExtern
        '
        Me.lblDeklExtern.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDeklExtern.AutoSize = True
        Me.lblDeklExtern.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDeklExtern.Location = New System.Drawing.Point(13, 367)
        Me.lblDeklExtern.Name = "lblDeklExtern"
        Me.lblDeklExtern.Size = New System.Drawing.Size(61, 13)
        Me.lblDeklExtern.TabIndex = 51
        Me.lblDeklExtern.Text = "Deklaration"
        '
        'wb_Rohstoffe_Nwt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(845, 440)
        Me.Controls.Add(Me.lblDeklIntern)
        Me.Controls.Add(Me.lblDeklExtern)
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
    Friend WithEvents lblDeklIntern As Windows.Forms.Label
    Friend WithEvents lblDeklExtern As Windows.Forms.Label
End Class
