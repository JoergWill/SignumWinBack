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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_Nwt
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.pnl_Nwt, 2)
        Me.pnl_Nwt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Nwt.Location = New System.Drawing.Point(3, 3)
        Me.pnl_Nwt.MinimumSize = New System.Drawing.Size(100, 100)
        Me.pnl_Nwt.Name = "pnl_Nwt"
        Me.pnl_Nwt.Size = New System.Drawing.Size(839, 394)
        Me.pnl_Nwt.TabIndex = 0
        '
        'tbDeklarationExtern
        '
        Me.tbDeklarationExtern.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbDeklarationExtern.Location = New System.Drawing.Point(3, 423)
        Me.tbDeklarationExtern.Multiline = True
        Me.tbDeklarationExtern.Name = "tbDeklarationExtern"
        Me.tbDeklarationExtern.Size = New System.Drawing.Size(416, 52)
        Me.tbDeklarationExtern.TabIndex = 42
        '
        'tbDeklarationIntern
        '
        Me.tbDeklarationIntern.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbDeklarationIntern.Location = New System.Drawing.Point(425, 423)
        Me.tbDeklarationIntern.Multiline = True
        Me.tbDeklarationIntern.Name = "tbDeklarationIntern"
        Me.tbDeklarationIntern.Size = New System.Drawing.Size(417, 52)
        Me.tbDeklarationIntern.TabIndex = 43
        '
        'lblDeklIntern
        '
        Me.lblDeklIntern.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDeklIntern.AutoSize = True
        Me.lblDeklIntern.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDeklIntern.Location = New System.Drawing.Point(425, 407)
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
        Me.lblDeklExtern.Location = New System.Drawing.Point(3, 407)
        Me.lblDeklExtern.Name = "lblDeklExtern"
        Me.lblDeklExtern.Size = New System.Drawing.Size(61, 13)
        Me.lblDeklExtern.TabIndex = 51
        Me.lblDeklExtern.Text = "Deklaration"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.pnl_Nwt, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblDeklIntern, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblDeklExtern, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.tbDeklarationExtern, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.tbDeklarationIntern, 1, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 400.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(845, 440)
        Me.TableLayoutPanel1.TabIndex = 53
        '
        'wb_Rohstoffe_Nwt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(845, 440)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "wb_Rohstoffe_Nwt"
        Me.Text = "Rohstoffe Nährwerte"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnl_Nwt As Windows.Forms.Panel
    Friend WithEvents tbDeklarationExtern As Windows.Forms.TextBox
    Friend WithEvents tbDeklarationIntern As Windows.Forms.TextBox
    Friend WithEvents lblDeklIntern As Windows.Forms.Label
    Friend WithEvents lblDeklExtern As Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As Windows.Forms.TableLayoutPanel
End Class
