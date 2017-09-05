Imports Signum.OrgaSoft

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ob_Artikel_ZuordnungRezept
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
        Me.tRezeptName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tRezeptNr = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnRzptChange = New System.Windows.Forms.Button()
        Me.BtnRzpShow = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'tRezeptName
        '
        Me.tRezeptName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tRezeptName.Location = New System.Drawing.Point(3, 66)
        Me.tRezeptName.Name = "tRezeptName"
        Me.tRezeptName.Size = New System.Drawing.Size(532, 20)
        Me.tRezeptName.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(3, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Rezept-Bezeichnung"
        '
        'tRezeptNr
        '
        Me.tRezeptNr.Location = New System.Drawing.Point(3, 23)
        Me.tRezeptNr.Name = "tRezeptNr"
        Me.tRezeptNr.ReadOnly = True
        Me.tRezeptNr.Size = New System.Drawing.Size(136, 20)
        Me.tRezeptNr.TabIndex = 8
        Me.tRezeptNr.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(3, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Rezept-Nr."
        '
        'BtnRzptChange
        '
        Me.BtnRzptChange.Location = New System.Drawing.Point(145, 12)
        Me.BtnRzptChange.Name = "BtnRzptChange"
        Me.BtnRzptChange.Size = New System.Drawing.Size(100, 40)
        Me.BtnRzptChange.TabIndex = 10
        Me.BtnRzptChange.Text = "Auswählen"
        Me.BtnRzptChange.UseVisualStyleBackColor = True
        '
        'BtnRzpShow
        '
        Me.BtnRzpShow.Location = New System.Drawing.Point(251, 12)
        Me.BtnRzpShow.Name = "BtnRzpShow"
        Me.BtnRzpShow.Size = New System.Drawing.Size(100, 40)
        Me.BtnRzpShow.TabIndex = 11
        Me.BtnRzpShow.Text = "Öffnen"
        Me.BtnRzpShow.UseVisualStyleBackColor = True
        '
        'ob_Artikel_ZuordnungRezept
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoScrollMinSize = New System.Drawing.Size(190, 268)
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.BtnRzpShow)
        Me.Controls.Add(Me.BtnRzptChange)
        Me.Controls.Add(Me.tRezeptName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tRezeptNr)
        Me.Controls.Add(Me.Label1)
        Me.MinimumSize = New System.Drawing.Size(190, 268)
        Me.Name = "ob_Artikel_ZuordnungRezept"
        Me.Size = New System.Drawing.Size(538, 418)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tRezeptName As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tRezeptNr As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents BtnRzptChange As Windows.Forms.Button
    Friend WithEvents BtnRzpShow As Windows.Forms.Button

    'Friend WithEvents PropertyGrid As Signum.OrgaSoft.GUI.Controls.PropertyGrid
End Class
