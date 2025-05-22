Imports WeifenLuo.WinFormsUI.Docking
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Linien_Details
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Linien_Details))
        Me.Panel = New System.Windows.Forms.Panel()
        Me.cbScannerDefekt = New System.Windows.Forms.CheckBox()
        Me.lbLinienSprache = New System.Windows.Forms.Label()
        Me.lbScanner = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tAdresse = New System.Windows.Forms.TextBox()
        Me.tBezeichnung = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LanguageFlags = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel
        '
        Me.Panel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel.Controls.Add(Me.cbScannerDefekt)
        Me.Panel.Controls.Add(Me.lbLinienSprache)
        Me.Panel.Controls.Add(Me.lbScanner)
        Me.Panel.Controls.Add(Me.Label3)
        Me.Panel.Controls.Add(Me.tAdresse)
        Me.Panel.Controls.Add(Me.tBezeichnung)
        Me.Panel.Controls.Add(Me.Label2)
        Me.Panel.Controls.Add(Me.Label1)
        Me.Panel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Panel.ForeColor = System.Drawing.Color.Black
        Me.Panel.Location = New System.Drawing.Point(12, 12)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(479, 158)
        Me.Panel.TabIndex = 3
        '
        'cbScannerDefekt
        '
        Me.cbScannerDefekt.AutoSize = True
        Me.cbScannerDefekt.Location = New System.Drawing.Point(105, 101)
        Me.cbScannerDefekt.Name = "cbScannerDefekt"
        Me.cbScannerDefekt.Size = New System.Drawing.Size(114, 17)
        Me.cbScannerDefekt.TabIndex = 7
        Me.cbScannerDefekt.Text = "Defekt/Deaktiviert"
        Me.cbScannerDefekt.UseVisualStyleBackColor = True
        '
        'lbLinienSprache
        '
        Me.lbLinienSprache.Image = Global.WinBack.My.Resources.Resources.PageMinus_16x16
        Me.lbLinienSprache.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbLinienSprache.Location = New System.Drawing.Point(102, 68)
        Me.lbLinienSprache.Name = "lbLinienSprache"
        Me.lbLinienSprache.Size = New System.Drawing.Size(117, 26)
        Me.lbLinienSprache.TabIndex = 6
        Me.lbLinienSprache.Text = "Deutsch"
        Me.lbLinienSprache.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbScanner
        '
        Me.lbScanner.AutoSize = True
        Me.lbScanner.Location = New System.Drawing.Point(16, 101)
        Me.lbScanner.Name = "lbScanner"
        Me.lbScanner.Size = New System.Drawing.Size(47, 13)
        Me.lbScanner.TabIndex = 5
        Me.lbScanner.Text = "Scanner"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Sprache"
        '
        'tAdresse
        '
        Me.tAdresse.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tAdresse.Location = New System.Drawing.Point(105, 39)
        Me.tAdresse.Name = "tAdresse"
        Me.tAdresse.Size = New System.Drawing.Size(291, 20)
        Me.tAdresse.TabIndex = 3
        '
        'tBezeichnung
        '
        Me.tBezeichnung.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tBezeichnung.Location = New System.Drawing.Point(105, 16)
        Me.tBezeichnung.Name = "tBezeichnung"
        Me.tBezeichnung.Size = New System.Drawing.Size(291, 20)
        Me.tBezeichnung.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "IP-Adresse:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bezeichnung:"
        '
        'LanguageFlags
        '
        Me.LanguageFlags.ImageStream = CType(resources.GetObject("LanguageFlags.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.LanguageFlags.TransparentColor = System.Drawing.Color.Transparent
        Me.LanguageFlags.Images.SetKeyName(0, "de.gif")
        Me.LanguageFlags.Images.SetKeyName(1, "hu.gif")
        Me.LanguageFlags.Images.SetKeyName(2, "nl.gif")
        Me.LanguageFlags.Images.SetKeyName(3, "us.gif")
        Me.LanguageFlags.Images.SetKeyName(4, "pt.gif")
        Me.LanguageFlags.Images.SetKeyName(5, "si.gif")
        Me.LanguageFlags.Images.SetKeyName(6, "ru.gif")
        Me.LanguageFlags.Images.SetKeyName(7, "fr.gif")
        Me.LanguageFlags.Images.SetKeyName(8, "es.gif")
        Me.LanguageFlags.Images.SetKeyName(9, "sk.gif")
        Me.LanguageFlags.Images.SetKeyName(10, "ro.gif")
        '
        'wb_Linien_Details
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(503, 182)
        Me.Controls.Add(Me.Panel)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wb_Linien_Details"
        Me.Text = "WinBack-Linien Info"
        Me.Panel.ResumeLayout(False)
        Me.Panel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel As System.Windows.Forms.Panel
    Friend WithEvents tAdresse As System.Windows.Forms.TextBox
    Friend WithEvents tBezeichnung As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbScanner As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LanguageFlags As System.Windows.Forms.ImageList
    Friend WithEvents lbLinienSprache As System.Windows.Forms.Label
    Friend WithEvents cbScannerDefekt As System.Windows.Forms.CheckBox
End Class
