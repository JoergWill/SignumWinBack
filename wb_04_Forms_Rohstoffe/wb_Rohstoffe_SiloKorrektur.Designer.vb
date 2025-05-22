<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rohstoffe_SiloKorrektur
    Inherits System.Windows.Forms.Form

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
        Me.lblNummer = New System.Windows.Forms.Label()
        Me.lblRohName = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.tbIst = New System.Windows.Forms.TextBox()
        Me.lbIst = New System.Windows.Forms.Label()
        Me.tbBestNeu = New System.Windows.Forms.TextBox()
        Me.lbBefMenge = New System.Windows.Forms.Label()
        Me.BtnBestandKorrektur = New System.Windows.Forms.Button()
        Me.BtnNullSetzen = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblNummer
        '
        Me.lblNummer.AutoSize = True
        Me.lblNummer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNummer.Location = New System.Drawing.Point(16, 35)
        Me.lblNummer.Name = "lblNummer"
        Me.lblNummer.Size = New System.Drawing.Size(42, 13)
        Me.lblNummer.TabIndex = 8
        Me.lblNummer.Text = "20001"
        '
        'lblRohName
        '
        Me.lblRohName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRohName.Location = New System.Drawing.Point(16, 53)
        Me.lblRohName.Name = "lblRohName"
        Me.lblRohName.Size = New System.Drawing.Size(139, 36)
        Me.lblRohName.TabIndex = 9
        Me.lblRohName.Text = "Weizen 550"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(17, 16)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(40, 13)
        Me.lblName.TabIndex = 7
        Me.lblName.Text = "Silo X"
        '
        'tbIst
        '
        Me.tbIst.ForeColor = System.Drawing.SystemColors.WindowText
        Me.tbIst.Location = New System.Drawing.Point(20, 127)
        Me.tbIst.Name = "tbIst"
        Me.tbIst.ReadOnly = True
        Me.tbIst.Size = New System.Drawing.Size(93, 20)
        Me.tbIst.TabIndex = 1
        Me.tbIst.TabStop = False
        Me.tbIst.Text = "kg"
        Me.tbIst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbIst
        '
        Me.lbIst.AutoSize = True
        Me.lbIst.Location = New System.Drawing.Point(17, 111)
        Me.lbIst.Name = "lbIst"
        Me.lbIst.Size = New System.Drawing.Size(50, 13)
        Me.lbIst.TabIndex = 10
        Me.lbIst.Text = "Istmenge"
        '
        'tbBestNeu
        '
        Me.tbBestNeu.Location = New System.Drawing.Point(142, 127)
        Me.tbBestNeu.Name = "tbBestNeu"
        Me.tbBestNeu.Size = New System.Drawing.Size(93, 20)
        Me.tbBestNeu.TabIndex = 1
        Me.tbBestNeu.Text = "  kg"
        Me.tbBestNeu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbBefMenge
        '
        Me.lbBefMenge.AutoSize = True
        Me.lbBefMenge.BackColor = System.Drawing.Color.Transparent
        Me.lbBefMenge.Location = New System.Drawing.Point(139, 111)
        Me.lbBefMenge.Name = "lbBefMenge"
        Me.lbBefMenge.Size = New System.Drawing.Size(67, 13)
        Me.lbBefMenge.TabIndex = 12
        Me.lbBefMenge.Text = "Bestand neu"
        '
        'BtnBestandKorrektur
        '
        Me.BtnBestandKorrektur.Location = New System.Drawing.Point(267, 116)
        Me.BtnBestandKorrektur.Name = "BtnBestandKorrektur"
        Me.BtnBestandKorrektur.Size = New System.Drawing.Size(86, 46)
        Me.BtnBestandKorrektur.TabIndex = 2
        Me.BtnBestandKorrektur.Text = "Bestand korrigieren"
        Me.BtnBestandKorrektur.UseVisualStyleBackColor = True
        '
        'BtnNullSetzen
        '
        Me.BtnNullSetzen.Location = New System.Drawing.Point(267, 64)
        Me.BtnNullSetzen.Name = "BtnNullSetzen"
        Me.BtnNullSetzen.Size = New System.Drawing.Size(86, 46)
        Me.BtnNullSetzen.TabIndex = 3
        Me.BtnNullSetzen.Text = "Null setzen"
        Me.BtnNullSetzen.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Location = New System.Drawing.Point(267, 12)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(86, 46)
        Me.BtnCancel.TabIndex = 4
        Me.BtnCancel.Text = "Abbruch"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'wb_Rohstoffe_SiloKorrektur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(378, 181)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnNullSetzen)
        Me.Controls.Add(Me.BtnBestandKorrektur)
        Me.Controls.Add(Me.tbBestNeu)
        Me.Controls.Add(Me.lbBefMenge)
        Me.Controls.Add(Me.tbIst)
        Me.Controls.Add(Me.lbIst)
        Me.Controls.Add(Me.lblNummer)
        Me.Controls.Add(Me.lblRohName)
        Me.Controls.Add(Me.lblName)
        Me.Name = "wb_Rohstoffe_SiloKorrektur"
        Me.Text = "Silo-Korrektur"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblNummer As System.Windows.Forms.Label
    Friend WithEvents lblRohName As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents tbIst As System.Windows.Forms.TextBox
    Friend WithEvents lbIst As System.Windows.Forms.Label
    Friend WithEvents tbBestNeu As System.Windows.Forms.TextBox
    Friend WithEvents lbBefMenge As System.Windows.Forms.Label
    Friend WithEvents BtnBestandKorrektur As System.Windows.Forms.Button
    Friend WithEvents BtnNullSetzen As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
End Class
