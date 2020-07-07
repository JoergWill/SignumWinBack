<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Silo
    Inherits System.Windows.Forms.UserControl

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
        Me.lbMax = New System.Windows.Forms.Label()
        Me.lbIst = New System.Windows.Forms.Label()
        Me.tbMax = New System.Windows.Forms.TextBox()
        Me.tbIst = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblNummer = New System.Windows.Forms.Label()
        Me.lblRohName = New System.Windows.Forms.Label()
        Me.pnlSilo = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.pnlSilo.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbMax
        '
        Me.lbMax.AutoSize = True
        Me.lbMax.BackColor = System.Drawing.Color.Transparent
        Me.lbMax.Location = New System.Drawing.Point(12, 56)
        Me.lbMax.Name = "lbMax"
        Me.lbMax.Size = New System.Drawing.Size(45, 13)
        Me.lbMax.TabIndex = 0
        Me.lbMax.Text = "Maximal"
        '
        'lbIst
        '
        Me.lbIst.AutoSize = True
        Me.lbIst.Location = New System.Drawing.Point(12, 214)
        Me.lbIst.Name = "lbIst"
        Me.lbIst.Size = New System.Drawing.Size(50, 13)
        Me.lbIst.TabIndex = 1
        Me.lbIst.Text = "Istmenge"
        '
        'tbMax
        '
        Me.tbMax.Location = New System.Drawing.Point(15, 72)
        Me.tbMax.Name = "tbMax"
        Me.tbMax.ReadOnly = True
        Me.tbMax.Size = New System.Drawing.Size(93, 20)
        Me.tbMax.TabIndex = 2
        Me.tbMax.TabStop = False
        Me.tbMax.Text = "kg"
        Me.tbMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbIst
        '
        Me.tbIst.Location = New System.Drawing.Point(15, 230)
        Me.tbIst.Name = "tbIst"
        Me.tbIst.ReadOnly = True
        Me.tbIst.Size = New System.Drawing.Size(93, 20)
        Me.tbIst.TabIndex = 3
        Me.tbIst.TabStop = False
        Me.tbIst.Text = "kg"
        Me.tbIst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(12, 275)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(40, 13)
        Me.lblName.TabIndex = 4
        Me.lblName.Text = "Silo X"
        '
        'lblNummer
        '
        Me.lblNummer.AutoSize = True
        Me.lblNummer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNummer.Location = New System.Drawing.Point(12, 2)
        Me.lblNummer.Name = "lblNummer"
        Me.lblNummer.Size = New System.Drawing.Size(42, 13)
        Me.lblNummer.TabIndex = 5
        Me.lblNummer.Text = "20001"
        '
        'lblRohName
        '
        Me.lblRohName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRohName.Location = New System.Drawing.Point(12, 15)
        Me.lblRohName.Name = "lblRohName"
        Me.lblRohName.Size = New System.Drawing.Size(131, 36)
        Me.lblRohName.TabIndex = 6
        Me.lblRohName.Text = "Weizen 550"
        '
        'pnlSilo
        '
        Me.pnlSilo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlSilo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pnlSilo.ColumnCount = 1
        Me.pnlSilo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.pnlSilo.Controls.Add(Me.TextBox1, 0, 1)
        Me.pnlSilo.Location = New System.Drawing.Point(70, 54)
        Me.pnlSilo.Name = "pnlSilo"
        Me.pnlSilo.RowCount = 2
        Me.pnlSilo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.pnlSilo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.pnlSilo.Size = New System.Drawing.Size(75, 240)
        Me.pnlSilo.TabIndex = 7
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.Lime
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(3, 123)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(69, 114)
        Me.TextBox1.TabIndex = 0
        '
        'wb_Silo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblRohName)
        Me.Controls.Add(Me.lblNummer)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.tbIst)
        Me.Controls.Add(Me.tbMax)
        Me.Controls.Add(Me.lbIst)
        Me.Controls.Add(Me.lbMax)
        Me.Controls.Add(Me.pnlSilo)
        Me.Name = "wb_Silo"
        Me.Size = New System.Drawing.Size(150, 297)
        Me.pnlSilo.ResumeLayout(False)
        Me.pnlSilo.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbMax As Windows.Forms.Label
    Friend WithEvents lbIst As Windows.Forms.Label
    Friend WithEvents tbMax As Windows.Forms.TextBox
    Friend WithEvents tbIst As Windows.Forms.TextBox
    Friend WithEvents lblName As Windows.Forms.Label
    Friend WithEvents lblNummer As Windows.Forms.Label
    Friend WithEvents lblRohName As Windows.Forms.Label
    Friend WithEvents pnlSilo As Windows.Forms.TableLayoutPanel
    Friend WithEvents TextBox1 As Windows.Forms.TextBox
End Class
