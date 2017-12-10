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
        Me.lblLinienGruppe = New System.Windows.Forms.Label()
        Me.lblLinieArtikel = New System.Windows.Forms.Label()
        Me.cbArtikelLinienGruppe = New WinBack.wb_ComboBox()
        Me.cbLiniengruppe = New WinBack.wb_ComboBox()
        Me.tStkGewicht = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TextBox11 = New System.Windows.Forms.TextBox()
        Me.TextBox12 = New System.Windows.Forms.TextBox()
        Me.TextBox14 = New System.Windows.Forms.TextBox()
        Me.TextBox15 = New System.Windows.Forms.TextBox()
        Me.TextBox17 = New System.Windows.Forms.TextBox()
        Me.TextBox18 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBox19 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tRezeptName
        '
        Me.tRezeptName.Location = New System.Drawing.Point(3, 66)
        Me.tRezeptName.Name = "tRezeptName"
        Me.tRezeptName.ReadOnly = True
        Me.tRezeptName.Size = New System.Drawing.Size(368, 20)
        Me.tRezeptName.TabIndex = 6
        Me.tRezeptName.TabStop = False
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
        Me.tRezeptNr.Size = New System.Drawing.Size(156, 20)
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
        Me.BtnRzptChange.Location = New System.Drawing.Point(165, 12)
        Me.BtnRzptChange.Name = "BtnRzptChange"
        Me.BtnRzptChange.Size = New System.Drawing.Size(100, 40)
        Me.BtnRzptChange.TabIndex = 10
        Me.BtnRzptChange.Text = "Auswählen"
        Me.BtnRzptChange.UseVisualStyleBackColor = True
        '
        'BtnRzpShow
        '
        Me.BtnRzpShow.Location = New System.Drawing.Point(271, 12)
        Me.BtnRzpShow.Name = "BtnRzpShow"
        Me.BtnRzpShow.Size = New System.Drawing.Size(100, 40)
        Me.BtnRzpShow.TabIndex = 11
        Me.BtnRzpShow.Text = "Öffnen"
        Me.BtnRzpShow.UseVisualStyleBackColor = True
        '
        'lblLinienGruppe
        '
        Me.lblLinienGruppe.AutoSize = True
        Me.lblLinienGruppe.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblLinienGruppe.Location = New System.Drawing.Point(3, 89)
        Me.lblLinienGruppe.Name = "lblLinienGruppe"
        Me.lblLinienGruppe.Size = New System.Drawing.Size(129, 13)
        Me.lblLinienGruppe.TabIndex = 31
        Me.lblLinienGruppe.Text = "Liniengruppe Teig-Rezept"
        '
        'lblLinieArtikel
        '
        Me.lblLinieArtikel.AutoSize = True
        Me.lblLinieArtikel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblLinieArtikel.Location = New System.Drawing.Point(201, 89)
        Me.lblLinieArtikel.Name = "lblLinieArtikel"
        Me.lblLinieArtikel.Size = New System.Drawing.Size(137, 13)
        Me.lblLinieArtikel.TabIndex = 33
        Me.lblLinieArtikel.Text = "Liniengruppe Artikel-Rezept"
        '
        'cbArtikelLinienGruppe
        '
        Me.cbArtikelLinienGruppe.FormattingEnabled = True
        Me.cbArtikelLinienGruppe.Location = New System.Drawing.Point(204, 105)
        Me.cbArtikelLinienGruppe.Name = "cbArtikelLinienGruppe"
        Me.cbArtikelLinienGruppe.Size = New System.Drawing.Size(167, 21)
        Me.cbArtikelLinienGruppe.TabIndex = 32
        Me.cbArtikelLinienGruppe.TabStop = False
        '
        'cbLiniengruppe
        '
        Me.cbLiniengruppe.FormattingEnabled = True
        Me.cbLiniengruppe.Location = New System.Drawing.Point(3, 105)
        Me.cbLiniengruppe.Name = "cbLiniengruppe"
        Me.cbLiniengruppe.Size = New System.Drawing.Size(145, 21)
        Me.cbLiniengruppe.TabIndex = 30
        Me.cbLiniengruppe.TabStop = False
        '
        'tStkGewicht
        '
        Me.tStkGewicht.Location = New System.Drawing.Point(106, 15)
        Me.tStkGewicht.Name = "tStkGewicht"
        Me.tStkGewicht.Size = New System.Drawing.Size(54, 20)
        Me.tStkGewicht.TabIndex = 34
        Me.tStkGewicht.TabStop = False
        Me.tStkGewicht.Text = "1567 gr "
        Me.tStkGewicht.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tStkGewicht.WordWrap = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(9, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 13)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "pro Stück (nass)"
        '
        'Label4
        '
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(150, 200)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Minimal"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(3, 41)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(57, 20)
        Me.TextBox1.TabIndex = 36
        Me.TextBox1.TabStop = False
        Me.TextBox1.Text = "2000,0 kg"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox1.WordWrap = False
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(66, 41)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(34, 20)
        Me.TextBox2.TabIndex = 38
        Me.TextBox2.TabStop = False
        Me.TextBox2.Text = "999%"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox2.WordWrap = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TextBox7)
        Me.Panel1.Controls.Add(Me.TextBox8)
        Me.Panel1.Controls.Add(Me.TextBox9)
        Me.Panel1.Controls.Add(Me.TextBox4)
        Me.Panel1.Controls.Add(Me.TextBox5)
        Me.Panel1.Controls.Add(Me.TextBox6)
        Me.Panel1.Controls.Add(Me.TextBox3)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.TextBox2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.tStkGewicht)
        Me.Panel1.Location = New System.Drawing.Point(204, 154)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(167, 122)
        Me.Panel1.TabIndex = 40
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(214, 148)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Artikel-Chargen"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(106, 41)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(54, 20)
        Me.TextBox3.TabIndex = 39
        Me.TextBox3.TabStop = False
        Me.TextBox3.Text = "14000 Stk"
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox3.WordWrap = False
        '
        'Label6
        '
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(150, 174)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "Gewicht"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(106, 67)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(54, 20)
        Me.TextBox4.TabIndex = 42
        Me.TextBox4.TabStop = False
        Me.TextBox4.Text = "14000 Stk"
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox4.WordWrap = False
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(3, 67)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(57, 20)
        Me.TextBox5.TabIndex = 40
        Me.TextBox5.TabStop = False
        Me.TextBox5.Text = "2000,0 kg"
        Me.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox5.WordWrap = False
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(66, 67)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(34, 20)
        Me.TextBox6.TabIndex = 41
        Me.TextBox6.TabStop = False
        Me.TextBox6.Text = "999%"
        Me.TextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox6.WordWrap = False
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(106, 93)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(54, 20)
        Me.TextBox7.TabIndex = 45
        Me.TextBox7.TabStop = False
        Me.TextBox7.Text = "14000 Stk"
        Me.TextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox7.WordWrap = False
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(3, 93)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(57, 20)
        Me.TextBox8.TabIndex = 43
        Me.TextBox8.TabStop = False
        Me.TextBox8.Text = "2000,0 kg"
        Me.TextBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox8.WordWrap = False
        '
        'TextBox9
        '
        Me.TextBox9.Location = New System.Drawing.Point(66, 93)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(34, 20)
        Me.TextBox9.TabIndex = 44
        Me.TextBox9.TabStop = False
        Me.TextBox9.Text = "999%"
        Me.TextBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox9.WordWrap = False
        '
        'Label7
        '
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(151, 226)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 13)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "Maximal"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(151, 252)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 13)
        Me.Label8.TabIndex = 44
        Me.Label8.Text = "Optimal"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.TextBox11)
        Me.Panel2.Controls.Add(Me.TextBox12)
        Me.Panel2.Controls.Add(Me.TextBox14)
        Me.Panel2.Controls.Add(Me.TextBox15)
        Me.Panel2.Controls.Add(Me.TextBox17)
        Me.Panel2.Controls.Add(Me.TextBox18)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.TextBox19)
        Me.Panel2.Location = New System.Drawing.Point(4, 154)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(144, 122)
        Me.Panel2.TabIndex = 46
        '
        'TextBox11
        '
        Me.TextBox11.Location = New System.Drawing.Point(3, 93)
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Size = New System.Drawing.Size(57, 20)
        Me.TextBox11.TabIndex = 43
        Me.TextBox11.TabStop = False
        Me.TextBox11.Text = "2000,0 kg"
        Me.TextBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox11.WordWrap = False
        '
        'TextBox12
        '
        Me.TextBox12.Location = New System.Drawing.Point(66, 93)
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.Size = New System.Drawing.Size(34, 20)
        Me.TextBox12.TabIndex = 44
        Me.TextBox12.TabStop = False
        Me.TextBox12.Text = "999%"
        Me.TextBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox12.WordWrap = False
        '
        'TextBox14
        '
        Me.TextBox14.Location = New System.Drawing.Point(3, 67)
        Me.TextBox14.Name = "TextBox14"
        Me.TextBox14.Size = New System.Drawing.Size(57, 20)
        Me.TextBox14.TabIndex = 40
        Me.TextBox14.TabStop = False
        Me.TextBox14.Text = "2000,0 kg"
        Me.TextBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox14.WordWrap = False
        '
        'TextBox15
        '
        Me.TextBox15.Location = New System.Drawing.Point(66, 67)
        Me.TextBox15.Name = "TextBox15"
        Me.TextBox15.Size = New System.Drawing.Size(34, 20)
        Me.TextBox15.TabIndex = 41
        Me.TextBox15.TabStop = False
        Me.TextBox15.Text = "999%"
        Me.TextBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox15.WordWrap = False
        '
        'TextBox17
        '
        Me.TextBox17.Location = New System.Drawing.Point(3, 41)
        Me.TextBox17.Name = "TextBox17"
        Me.TextBox17.Size = New System.Drawing.Size(57, 20)
        Me.TextBox17.TabIndex = 36
        Me.TextBox17.TabStop = False
        Me.TextBox17.Text = "2000,0 kg"
        Me.TextBox17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox17.WordWrap = False
        '
        'TextBox18
        '
        Me.TextBox18.Location = New System.Drawing.Point(66, 41)
        Me.TextBox18.Name = "TextBox18"
        Me.TextBox18.Size = New System.Drawing.Size(34, 20)
        Me.TextBox18.TabIndex = 38
        Me.TextBox18.TabStop = False
        Me.TextBox18.Text = "999%"
        Me.TextBox18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox18.WordWrap = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label9.Location = New System.Drawing.Point(66, 18)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 13)
        Me.Label9.TabIndex = 35
        Me.Label9.Text = "Teig (Gesamt)"
        '
        'TextBox19
        '
        Me.TextBox19.Location = New System.Drawing.Point(3, 15)
        Me.TextBox19.Name = "TextBox19"
        Me.TextBox19.Size = New System.Drawing.Size(57, 20)
        Me.TextBox19.TabIndex = 34
        Me.TextBox19.TabStop = False
        Me.TextBox19.Text = "245,9 kg"
        Me.TextBox19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox19.WordWrap = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label10.Location = New System.Drawing.Point(14, 149)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 13)
        Me.Label10.TabIndex = 47
        Me.Label10.Text = "Teig-Chargen"
        '
        'ob_Artikel_ZuordnungRezept
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoScrollMinSize = New System.Drawing.Size(190, 268)
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblLinieArtikel)
        Me.Controls.Add(Me.cbArtikelLinienGruppe)
        Me.Controls.Add(Me.lblLinienGruppe)
        Me.Controls.Add(Me.cbLiniengruppe)
        Me.Controls.Add(Me.BtnRzpShow)
        Me.Controls.Add(Me.BtnRzptChange)
        Me.Controls.Add(Me.tRezeptName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tRezeptNr)
        Me.Controls.Add(Me.Label1)
        Me.MinimumSize = New System.Drawing.Size(190, 268)
        Me.Name = "ob_Artikel_ZuordnungRezept"
        Me.Size = New System.Drawing.Size(378, 280)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tRezeptName As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tRezeptNr As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents BtnRzptChange As Windows.Forms.Button
    Friend WithEvents BtnRzpShow As Windows.Forms.Button
    Friend WithEvents lblLinienGruppe As Windows.Forms.Label
    Friend WithEvents cbLiniengruppe As wb_ComboBox
    Friend WithEvents cbArtikelLinienGruppe As wb_ComboBox
    Friend WithEvents lblLinieArtikel As Windows.Forms.Label
    Friend WithEvents tStkGewicht As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents TextBox1 As Windows.Forms.TextBox
    Friend WithEvents TextBox2 As Windows.Forms.TextBox
    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents TextBox7 As Windows.Forms.TextBox
    Friend WithEvents TextBox8 As Windows.Forms.TextBox
    Friend WithEvents TextBox9 As Windows.Forms.TextBox
    Friend WithEvents TextBox4 As Windows.Forms.TextBox
    Friend WithEvents TextBox5 As Windows.Forms.TextBox
    Friend WithEvents TextBox6 As Windows.Forms.TextBox
    Friend WithEvents TextBox3 As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents TextBox11 As Windows.Forms.TextBox
    Friend WithEvents TextBox12 As Windows.Forms.TextBox
    Friend WithEvents TextBox14 As Windows.Forms.TextBox
    Friend WithEvents TextBox15 As Windows.Forms.TextBox
    Friend WithEvents TextBox17 As Windows.Forms.TextBox
    Friend WithEvents TextBox18 As Windows.Forms.TextBox
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents TextBox19 As Windows.Forms.TextBox
    Friend WithEvents Label10 As Windows.Forms.Label

    'Friend WithEvents PropertyGrid As Signum.OrgaSoft.GUI.Controls.PropertyGrid
End Class
