Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rohstoffe_Details
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
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tRohstoffName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tRohstoffNummer = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tRohstoffKommentar = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbRohstoffGrp2 = New WinBack.wb_ComboBox()
        Me.cbRohstoffGrp1 = New WinBack.wb_ComboBox()
        Me.tRohstoffPreis = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label14.Location = New System.Drawing.Point(9, 128)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(51, 13)
        Me.Label14.TabIndex = 29
        Me.Label14.Text = "Gruppe 1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(9, 166)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Gruppe 2"
        '
        'tRohstoffName
        '
        Me.tRohstoffName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tRohstoffName.Location = New System.Drawing.Point(165, 24)
        Me.tRohstoffName.Name = "tRohstoffName"
        Me.tRohstoffName.Size = New System.Drawing.Size(486, 20)
        Me.tRohstoffName.TabIndex = 32
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(162, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Rohstoff-Bezeichnung"
        '
        'tRohstoffNummer
        '
        Me.tRohstoffNummer.Location = New System.Drawing.Point(12, 24)
        Me.tRohstoffNummer.Name = "tRohstoffNummer"
        Me.tRohstoffNummer.Size = New System.Drawing.Size(136, 20)
        Me.tRohstoffNummer.TabIndex = 34
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(9, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "Nummer"
        '
        'tRohstoffKommentar
        '
        Me.tRohstoffKommentar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tRohstoffKommentar.Location = New System.Drawing.Point(165, 63)
        Me.tRohstoffKommentar.Name = "tRohstoffKommentar"
        Me.tRohstoffKommentar.Size = New System.Drawing.Size(486, 20)
        Me.tRohstoffKommentar.TabIndex = 36
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(162, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Kommentar"
        '
        'cbRohstoffGrp2
        '
        Me.cbRohstoffGrp2.FormattingEnabled = True
        Me.cbRohstoffGrp2.Location = New System.Drawing.Point(12, 182)
        Me.cbRohstoffGrp2.Name = "cbRohstoffGrp2"
        Me.cbRohstoffGrp2.Size = New System.Drawing.Size(310, 21)
        Me.cbRohstoffGrp2.TabIndex = 30
        '
        'cbRohstoffGrp1
        '
        Me.cbRohstoffGrp1.FormattingEnabled = True
        Me.cbRohstoffGrp1.Location = New System.Drawing.Point(12, 144)
        Me.cbRohstoffGrp1.Name = "cbRohstoffGrp1"
        Me.cbRohstoffGrp1.Size = New System.Drawing.Size(310, 21)
        Me.cbRohstoffGrp1.TabIndex = 28
        '
        'tRohstoffPreis
        '
        Me.tRohstoffPreis.Location = New System.Drawing.Point(12, 94)
        Me.tRohstoffPreis.Name = "tRohstoffPreis"
        Me.tRohstoffPreis.Size = New System.Drawing.Size(96, 20)
        Me.tRohstoffPreis.TabIndex = 38
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(9, 78)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 39
        Me.Label5.Text = "Preis/kg"
        '
        'wb_Rohstoffe_Details
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(663, 449)
        Me.Controls.Add(Me.tRohstoffPreis)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tRohstoffKommentar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tRohstoffNummer)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tRohstoffName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbRohstoffGrp2)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.cbRohstoffGrp1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "wb_Rohstoffe_Details"
        Me.Text = "Rohstoff Details"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents cbRohstoffGrp1 As wb_ComboBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents cbRohstoffGrp2 As wb_ComboBox
    Friend WithEvents tRohstoffName As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tRohstoffNummer As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents tRohstoffKommentar As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents tRohstoffPreis As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
End Class
