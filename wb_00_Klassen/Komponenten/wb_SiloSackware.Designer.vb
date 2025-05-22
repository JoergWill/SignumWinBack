<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_SiloSackware
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
        Me.components = New System.ComponentModel.Container()
        Me.lbIst = New System.Windows.Forms.Label()
        Me.tbIst = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblNummer = New System.Windows.Forms.Label()
        Me.lblRohName = New System.Windows.Forms.Label()
        Me.BtnSiloTauschen = New System.Windows.Forms.Button()
        Me.ToolTipSiloBef = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtnSiloNull = New System.Windows.Forms.Button()
        Me.tbBefMenge = New System.Windows.Forms.TextBox()
        Me.lbBefMenge = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbIst
        '
        Me.lbIst.AutoSize = True
        Me.lbIst.Location = New System.Drawing.Point(24, 242)
        Me.lbIst.Name = "lbIst"
        Me.lbIst.Size = New System.Drawing.Size(50, 13)
        Me.lbIst.TabIndex = 1
        Me.lbIst.Text = "Istmenge"
        '
        'tbIst
        '
        Me.tbIst.ForeColor = System.Drawing.SystemColors.WindowText
        Me.tbIst.Location = New System.Drawing.Point(27, 258)
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
        Me.lblName.Location = New System.Drawing.Point(6, 303)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(63, 13)
        Me.lblName.TabIndex = 4
        Me.lblName.Text = "Sackware"
        '
        'lblNummer
        '
        Me.lblNummer.AutoSize = True
        Me.lblNummer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNummer.Location = New System.Drawing.Point(6, 25)
        Me.lblNummer.Name = "lblNummer"
        Me.lblNummer.Size = New System.Drawing.Size(42, 13)
        Me.lblNummer.TabIndex = 5
        Me.lblNummer.Text = "20001"
        '
        'lblRohName
        '
        Me.lblRohName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRohName.Location = New System.Drawing.Point(6, 43)
        Me.lblRohName.Name = "lblRohName"
        Me.lblRohName.Size = New System.Drawing.Size(139, 36)
        Me.lblRohName.TabIndex = 6
        Me.lblRohName.Text = "Weizen 550"
        '
        'BtnSiloTauschen
        '
        Me.BtnSiloTauschen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSiloTauschen.ForeColor = System.Drawing.Color.Black
        Me.BtnSiloTauschen.Location = New System.Drawing.Point(83, 2)
        Me.BtnSiloTauschen.Name = "BtnSiloTauschen"
        Me.BtnSiloTauschen.Size = New System.Drawing.Size(62, 38)
        Me.BtnSiloTauschen.TabIndex = 8
        Me.BtnSiloTauschen.Text = "Rohstoff tauschen"
        Me.ToolTipSiloBef.SetToolTip(Me.BtnSiloTauschen, "Ändert den Rohstoff für dieses Silo." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Der bisherige Rohstoff darf in keiner Rezep" &
        "tur mehr verwendet werden." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.BtnSiloTauschen.UseVisualStyleBackColor = True
        '
        'BtnSiloNull
        '
        Me.BtnSiloNull.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSiloNull.ForeColor = System.Drawing.Color.Black
        Me.BtnSiloNull.Location = New System.Drawing.Point(83, 332)
        Me.BtnSiloNull.Name = "BtnSiloNull"
        Me.BtnSiloNull.Size = New System.Drawing.Size(62, 38)
        Me.BtnSiloNull.TabIndex = 11
        Me.BtnSiloNull.Text = "Null setzen"
        Me.ToolTipSiloBef.SetToolTip(Me.BtnSiloNull, "Silo-Füllstand auf Null setzen" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.BtnSiloNull.UseVisualStyleBackColor = True
        '
        'tbBefMenge
        '
        Me.tbBefMenge.Location = New System.Drawing.Point(27, 219)
        Me.tbBefMenge.Name = "tbBefMenge"
        Me.tbBefMenge.Size = New System.Drawing.Size(93, 20)
        Me.tbBefMenge.TabIndex = 10
        Me.tbBefMenge.TabStop = False
        Me.tbBefMenge.Text = "kg"
        Me.tbBefMenge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTipSiloBef.SetToolTip(Me.tbBefMenge, "Doppelclick übernimmt die gesamte Liefermenge")
        Me.tbBefMenge.Visible = False
        '
        'lbBefMenge
        '
        Me.lbBefMenge.AutoSize = True
        Me.lbBefMenge.BackColor = System.Drawing.Color.Transparent
        Me.lbBefMenge.Location = New System.Drawing.Point(24, 203)
        Me.lbBefMenge.Name = "lbBefMenge"
        Me.lbBefMenge.Size = New System.Drawing.Size(51, 13)
        Me.lbBefMenge.TabIndex = 9
        Me.lbBefMenge.Text = "Lieferung"
        Me.lbBefMenge.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.WinBack.My.Resources.Resources.Sackware_45x43
        Me.PictureBox1.Location = New System.Drawing.Point(27, 322)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(50, 50)
        Me.PictureBox1.TabIndex = 12
        Me.PictureBox1.TabStop = False
        '
        'wb_SiloSackware
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblNummer)
        Me.Controls.Add(Me.BtnSiloNull)
        Me.Controls.Add(Me.tbBefMenge)
        Me.Controls.Add(Me.lbBefMenge)
        Me.Controls.Add(Me.BtnSiloTauschen)
        Me.Controls.Add(Me.lblRohName)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.tbIst)
        Me.Controls.Add(Me.lbIst)
        Me.Name = "wb_SiloSackware"
        Me.Size = New System.Drawing.Size(150, 375)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbIst As System.Windows.Forms.Label
    Friend WithEvents tbIst As System.Windows.Forms.TextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblNummer As System.Windows.Forms.Label
    Friend WithEvents lblRohName As System.Windows.Forms.Label
    Friend WithEvents BtnSiloTauschen As System.Windows.Forms.Button
    Friend WithEvents ToolTipSiloBef As System.Windows.Forms.ToolTip
    Friend WithEvents tbBefMenge As System.Windows.Forms.TextBox
    Friend WithEvents lbBefMenge As System.Windows.Forms.Label
    Friend WithEvents BtnSiloNull As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
