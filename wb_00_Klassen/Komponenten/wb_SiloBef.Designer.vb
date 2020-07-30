<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_SiloBef
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.tbLieferMenge = New System.Windows.Forms.TextBox()
        Me.lbLiefermenge = New System.Windows.Forms.Label()
        Me.tbVerteilt = New System.Windows.Forms.TextBox()
        Me.lbVerteilt = New System.Windows.Forms.Label()
        Me.tbRest = New System.Windows.Forms.TextBox()
        Me.lbRest = New System.Windows.Forms.Label()
        Me.BtnLieferungVerbuchen = New System.Windows.Forms.Button()
        Me.Abbruch = New System.Windows.Forms.Button()
        Me.lblNummer = New System.Windows.Forms.Label()
        Me.lblRohCharge = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'tbLieferMenge
        '
        Me.tbLieferMenge.Location = New System.Drawing.Point(27, 100)
        Me.tbLieferMenge.Name = "tbLieferMenge"
        Me.tbLieferMenge.ReadOnly = True
        Me.tbLieferMenge.Size = New System.Drawing.Size(93, 20)
        Me.tbLieferMenge.TabIndex = 4
        Me.tbLieferMenge.TabStop = False
        Me.tbLieferMenge.Text = "kg"
        Me.tbLieferMenge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbLiefermenge
        '
        Me.lbLiefermenge.AutoSize = True
        Me.lbLiefermenge.BackColor = System.Drawing.Color.Transparent
        Me.lbLiefermenge.Location = New System.Drawing.Point(24, 84)
        Me.lbLiefermenge.Name = "lbLiefermenge"
        Me.lbLiefermenge.Size = New System.Drawing.Size(65, 13)
        Me.lbLiefermenge.TabIndex = 3
        Me.lbLiefermenge.Text = "Liefermenge"
        '
        'tbVerteilt
        '
        Me.tbVerteilt.Location = New System.Drawing.Point(27, 159)
        Me.tbVerteilt.Name = "tbVerteilt"
        Me.tbVerteilt.ReadOnly = True
        Me.tbVerteilt.Size = New System.Drawing.Size(93, 20)
        Me.tbVerteilt.TabIndex = 6
        Me.tbVerteilt.TabStop = False
        Me.tbVerteilt.Text = "kg"
        Me.tbVerteilt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbVerteilt
        '
        Me.lbVerteilt.AutoSize = True
        Me.lbVerteilt.BackColor = System.Drawing.Color.Transparent
        Me.lbVerteilt.Location = New System.Drawing.Point(24, 143)
        Me.lbVerteilt.Name = "lbVerteilt"
        Me.lbVerteilt.Size = New System.Drawing.Size(39, 13)
        Me.lbVerteilt.TabIndex = 5
        Me.lbVerteilt.Text = "Verteilt"
        '
        'tbRest
        '
        Me.tbRest.Location = New System.Drawing.Point(27, 219)
        Me.tbRest.Name = "tbRest"
        Me.tbRest.ReadOnly = True
        Me.tbRest.Size = New System.Drawing.Size(93, 20)
        Me.tbRest.TabIndex = 8
        Me.tbRest.TabStop = False
        Me.tbRest.Text = "kg"
        Me.tbRest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbRest
        '
        Me.lbRest.AutoSize = True
        Me.lbRest.BackColor = System.Drawing.Color.Transparent
        Me.lbRest.Location = New System.Drawing.Point(24, 203)
        Me.lbRest.Name = "lbRest"
        Me.lbRest.Size = New System.Drawing.Size(29, 13)
        Me.lbRest.TabIndex = 7
        Me.lbRest.Text = "Rest"
        '
        'BtnLieferungVerbuchen
        '
        Me.BtnLieferungVerbuchen.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnLieferungVerbuchen.Enabled = False
        Me.BtnLieferungVerbuchen.Location = New System.Drawing.Point(14, 332)
        Me.BtnLieferungVerbuchen.Name = "BtnLieferungVerbuchen"
        Me.BtnLieferungVerbuchen.Size = New System.Drawing.Size(118, 38)
        Me.BtnLieferungVerbuchen.TabIndex = 9
        Me.BtnLieferungVerbuchen.Text = "Lieferung verbuchen"
        Me.BtnLieferungVerbuchen.UseVisualStyleBackColor = True
        '
        'Abbruch
        '
        Me.Abbruch.Location = New System.Drawing.Point(14, 288)
        Me.Abbruch.Name = "Abbruch"
        Me.Abbruch.Size = New System.Drawing.Size(118, 38)
        Me.Abbruch.TabIndex = 10
        Me.Abbruch.TabStop = False
        Me.Abbruch.Text = "Abbrechen"
        Me.Abbruch.UseVisualStyleBackColor = True
        '
        'lblNummer
        '
        Me.lblNummer.AutoSize = True
        Me.lblNummer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNummer.Location = New System.Drawing.Point(6, 25)
        Me.lblNummer.Name = "lblNummer"
        Me.lblNummer.Size = New System.Drawing.Size(42, 13)
        Me.lblNummer.TabIndex = 11
        Me.lblNummer.Text = "20001"
        '
        'lblRohCharge
        '
        Me.lblRohCharge.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRohCharge.Location = New System.Drawing.Point(6, 43)
        Me.lblRohCharge.Name = "lblRohCharge"
        Me.lblRohCharge.Size = New System.Drawing.Size(139, 36)
        Me.lblRohCharge.TabIndex = 12
        Me.lblRohCharge.Text = "Charge"
        '
        'wb_SiloBef
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblRohCharge)
        Me.Controls.Add(Me.lblNummer)
        Me.Controls.Add(Me.Abbruch)
        Me.Controls.Add(Me.BtnLieferungVerbuchen)
        Me.Controls.Add(Me.tbRest)
        Me.Controls.Add(Me.lbRest)
        Me.Controls.Add(Me.tbVerteilt)
        Me.Controls.Add(Me.lbVerteilt)
        Me.Controls.Add(Me.tbLieferMenge)
        Me.Controls.Add(Me.lbLiefermenge)
        Me.Name = "wb_SiloBef"
        Me.Size = New System.Drawing.Size(150, 375)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbLieferMenge As Windows.Forms.TextBox
    Friend WithEvents lbLiefermenge As Windows.Forms.Label
    Friend WithEvents tbVerteilt As Windows.Forms.TextBox
    Friend WithEvents lbVerteilt As Windows.Forms.Label
    Friend WithEvents tbRest As Windows.Forms.TextBox
    Friend WithEvents lbRest As Windows.Forms.Label
    Friend WithEvents BtnLieferungVerbuchen As Windows.Forms.Button
    Friend WithEvents Abbruch As Windows.Forms.Button
    Friend WithEvents lblNummer As Windows.Forms.Label
    Friend WithEvents lblRohCharge As Windows.Forms.Label
End Class
