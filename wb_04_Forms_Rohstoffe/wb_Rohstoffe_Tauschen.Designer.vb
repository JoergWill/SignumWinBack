Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rohstoffe_Tauschen
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
        Me.lbRohstoffOrg = New System.Windows.Forms.Label()
        Me.tbRohNameOrg = New System.Windows.Forms.TextBox()
        Me.tbRohNrNeu = New System.Windows.Forms.TextBox()
        Me.tbRohNrOrg = New System.Windows.Forms.TextBox()
        Me.lbRohstoffNeu = New System.Windows.Forms.Label()
        Me.tbRohNameNeu = New System.Windows.Forms.TextBox()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnAbbruch = New System.Windows.Forms.Button()
        Me.cbTauschen = New System.Windows.Forms.CheckBox()
        Me.lblTauschen = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lbRohstoffOrg
        '
        Me.lbRohstoffOrg.AutoSize = True
        Me.lbRohstoffOrg.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbRohstoffOrg.Location = New System.Drawing.Point(9, 5)
        Me.lbRohstoffOrg.Name = "lbRohstoffOrg"
        Me.lbRohstoffOrg.Size = New System.Drawing.Size(85, 13)
        Me.lbRohstoffOrg.TabIndex = 0
        Me.lbRohstoffOrg.Text = "Original Rohstoff"
        '
        'tbRohNameOrg
        '
        Me.tbRohNameOrg.Location = New System.Drawing.Point(115, 22)
        Me.tbRohNameOrg.Name = "tbRohNameOrg"
        Me.tbRohNameOrg.ReadOnly = True
        Me.tbRohNameOrg.Size = New System.Drawing.Size(355, 20)
        Me.tbRohNameOrg.TabIndex = 5
        Me.tbRohNameOrg.TabStop = False
        '
        'tbRohNrNeu
        '
        Me.tbRohNrNeu.Location = New System.Drawing.Point(12, 63)
        Me.tbRohNrNeu.Name = "tbRohNrNeu"
        Me.tbRohNrNeu.Size = New System.Drawing.Size(97, 20)
        Me.tbRohNrNeu.TabIndex = 1
        '
        'tbRohNrOrg
        '
        Me.tbRohNrOrg.Location = New System.Drawing.Point(12, 22)
        Me.tbRohNrOrg.Name = "tbRohNrOrg"
        Me.tbRohNrOrg.ReadOnly = True
        Me.tbRohNrOrg.Size = New System.Drawing.Size(97, 20)
        Me.tbRohNrOrg.TabIndex = 2
        Me.tbRohNrOrg.TabStop = False
        '
        'lbRohstoffNeu
        '
        Me.lbRohstoffNeu.AutoSize = True
        Me.lbRohstoffNeu.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbRohstoffNeu.Location = New System.Drawing.Point(9, 46)
        Me.lbRohstoffNeu.Name = "lbRohstoffNeu"
        Me.lbRohstoffNeu.Size = New System.Drawing.Size(77, 13)
        Me.lbRohstoffNeu.TabIndex = 6
        Me.lbRohstoffNeu.Text = "ersetzen durch"
        '
        'tbRohNameNeu
        '
        Me.tbRohNameNeu.Location = New System.Drawing.Point(115, 63)
        Me.tbRohNameNeu.Name = "tbRohNameNeu"
        Me.tbRohNameNeu.Size = New System.Drawing.Size(355, 20)
        Me.tbRohNameNeu.TabIndex = 2
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(368, 128)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(102, 40)
        Me.BtnOK.TabIndex = 4
        Me.BtnOK.Text = "Rohstoff ersetzen"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'BtnAbbruch
        '
        Me.BtnAbbruch.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnAbbruch.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnAbbruch.Location = New System.Drawing.Point(260, 128)
        Me.BtnAbbruch.Name = "BtnAbbruch"
        Me.BtnAbbruch.Size = New System.Drawing.Size(102, 40)
        Me.BtnAbbruch.TabIndex = 5
        Me.BtnAbbruch.Text = "Abbruch"
        Me.BtnAbbruch.UseVisualStyleBackColor = True
        '
        'cbTauschen
        '
        Me.cbTauschen.AutoSize = True
        Me.cbTauschen.Location = New System.Drawing.Point(12, 141)
        Me.cbTauschen.Name = "cbTauschen"
        Me.cbTauschen.Size = New System.Drawing.Size(134, 17)
        Me.cbTauschen.TabIndex = 7
        Me.cbTauschen.Text = "Rohstoffe TAUSCHEN"
        Me.cbTauschen.UseVisualStyleBackColor = True
        '
        'lblTauschen
        '
        Me.lblTauschen.ForeColor = System.Drawing.Color.Red
        Me.lblTauschen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTauschen.Location = New System.Drawing.Point(9, 93)
        Me.lblTauschen.Name = "lblTauschen"
        Me.lblTauschen.Size = New System.Drawing.Size(353, 40)
        Me.lblTauschen.TabIndex = 8
        Me.lblTauschen.Text = "Mit dieser Funktion werden die Rohstoffe in allen Rezepturen getauscht ! Rohstoff" &
    " A wird zu B und umgekehrt"
        Me.lblTauschen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTauschen.Visible = False
        '
        'wb_Rohstoffe_Tauschen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnAbbruch
        Me.ClientSize = New System.Drawing.Size(478, 177)
        Me.CloseButton = False
        Me.CloseButtonVisible = False
        Me.Controls.Add(Me.cbTauschen)
        Me.Controls.Add(Me.BtnAbbruch)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.tbRohNameNeu)
        Me.Controls.Add(Me.lbRohstoffNeu)
        Me.Controls.Add(Me.tbRohNameOrg)
        Me.Controls.Add(Me.tbRohNrNeu)
        Me.Controls.Add(Me.lbRohstoffOrg)
        Me.Controls.Add(Me.tbRohNrOrg)
        Me.Controls.Add(Me.lblTauschen)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wb_Rohstoffe_Tauschen"
        Me.Text = "Einen Rohstoff in allen Rezepten ersetzen"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbRohstoffOrg As Windows.Forms.Label
    Friend WithEvents tbRohNrOrg As Windows.Forms.TextBox
    Friend WithEvents tbRohNrNeu As Windows.Forms.TextBox
    Friend WithEvents tbRohNameOrg As Windows.Forms.TextBox
    Friend WithEvents lbRohstoffNeu As Windows.Forms.Label
    Friend WithEvents tbRohNameNeu As Windows.Forms.TextBox
    Friend WithEvents BtnOK As Windows.Forms.Button
    Friend WithEvents BtnAbbruch As Windows.Forms.Button
    Friend WithEvents cbTauschen As Windows.Forms.CheckBox
    Friend WithEvents lblTauschen As Windows.Forms.Label
End Class
