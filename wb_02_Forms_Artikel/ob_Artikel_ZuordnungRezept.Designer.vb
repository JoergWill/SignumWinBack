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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ob_Artikel_ZuordnungRezept))
        Me.tRezeptName = New System.Windows.Forms.TextBox()
        Me.lblRzeptBezeichnung = New System.Windows.Forms.Label()
        Me.tRezeptNr = New System.Windows.Forms.TextBox()
        Me.lblRezeptNr = New System.Windows.Forms.Label()
        Me.BtnRzptChange = New System.Windows.Forms.Button()
        Me.BtnRzpShow = New System.Windows.Forms.Button()
        Me.lblLinienGruppe = New System.Windows.Forms.Label()
        Me.lblLinieArtikel = New System.Windows.Forms.Label()
        Me.tStkGewicht = New System.Windows.Forms.TextBox()
        Me.lblProStk = New System.Windows.Forms.Label()
        Me.lblMin = New System.Windows.Forms.Label()
        Me.tChrgMinkg = New System.Windows.Forms.TextBox()
        Me.tChrgMinPrz = New System.Windows.Forms.TextBox()
        Me.pArtikelChargen = New System.Windows.Forms.Panel()
        Me.tChrgOptStk = New System.Windows.Forms.TextBox()
        Me.tChrgOptkg = New System.Windows.Forms.TextBox()
        Me.tChrgOptPrz = New System.Windows.Forms.TextBox()
        Me.tChrgMaxStk = New System.Windows.Forms.TextBox()
        Me.tChrgMaxkg = New System.Windows.Forms.TextBox()
        Me.tChrgMaxPrz = New System.Windows.Forms.TextBox()
        Me.tChrgMinStk = New System.Windows.Forms.TextBox()
        Me.lblArtikelChargen = New System.Windows.Forms.Label()
        Me.lblGewicht = New System.Windows.Forms.Label()
        Me.lblMax = New System.Windows.Forms.Label()
        Me.lblOpt = New System.Windows.Forms.Label()
        Me.pTeigChargen = New System.Windows.Forms.Panel()
        Me.tRezOptkg = New System.Windows.Forms.TextBox()
        Me.tRezOptPrz = New System.Windows.Forms.TextBox()
        Me.tRezMaxkg = New System.Windows.Forms.TextBox()
        Me.tRezMaxPrz = New System.Windows.Forms.TextBox()
        Me.tRezMinkg = New System.Windows.Forms.TextBox()
        Me.tRezMinPrz = New System.Windows.Forms.TextBox()
        Me.lblTeigGesamt = New System.Windows.Forms.Label()
        Me.tRezGesamt = New System.Windows.Forms.TextBox()
        Me.lblTeigChargen = New System.Windows.Forms.Label()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cbArtikelLinienGruppe = New WinBack.wb_ComboBox()
        Me.cbLiniengruppe = New WinBack.wb_ComboBox()
        Me.pArtikelChargen.SuspendLayout()
        Me.pTeigChargen.SuspendLayout()
        Me.SuspendLayout()
        '
        'tRezeptName
        '
        Me.tRezeptName.Location = New System.Drawing.Point(3, 66)
        Me.tRezeptName.Name = "tRezeptName"
        Me.tRezeptName.ReadOnly = True
        Me.tRezeptName.Size = New System.Drawing.Size(384, 20)
        Me.tRezeptName.TabIndex = 6
        Me.tRezeptName.TabStop = False
        '
        'lblRzeptBezeichnung
        '
        Me.lblRzeptBezeichnung.AutoSize = True
        Me.lblRzeptBezeichnung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzeptBezeichnung.Location = New System.Drawing.Point(3, 50)
        Me.lblRzeptBezeichnung.Name = "lblRzeptBezeichnung"
        Me.lblRzeptBezeichnung.Size = New System.Drawing.Size(106, 13)
        Me.lblRzeptBezeichnung.TabIndex = 9
        Me.lblRzeptBezeichnung.Text = "Rezept-Bezeichnung"
        '
        'tRezeptNr
        '
        Me.tRezeptNr.Location = New System.Drawing.Point(3, 23)
        Me.tRezeptNr.Name = "tRezeptNr"
        Me.tRezeptNr.ReadOnly = True
        Me.tRezeptNr.Size = New System.Drawing.Size(72, 20)
        Me.tRezeptNr.TabIndex = 8
        Me.tRezeptNr.TabStop = False
        '
        'lblRezeptNr
        '
        Me.lblRezeptNr.AutoSize = True
        Me.lblRezeptNr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRezeptNr.Location = New System.Drawing.Point(3, 7)
        Me.lblRezeptNr.Name = "lblRezeptNr"
        Me.lblRezeptNr.Size = New System.Drawing.Size(58, 13)
        Me.lblRezeptNr.TabIndex = 7
        Me.lblRezeptNr.Text = "Rezept-Nr."
        '
        'BtnRzptChange
        '
        Me.BtnRzptChange.Location = New System.Drawing.Point(82, 12)
        Me.BtnRzptChange.Name = "BtnRzptChange"
        Me.BtnRzptChange.Size = New System.Drawing.Size(100, 40)
        Me.BtnRzptChange.TabIndex = 10
        Me.BtnRzptChange.Text = "Auswählen"
        Me.ToolTip.SetToolTip(Me.BtnRzptChange, "Zuordnung einer (Teig)Rezeptur zum Artikel")
        Me.BtnRzptChange.UseVisualStyleBackColor = True
        '
        'BtnRzpShow
        '
        Me.BtnRzpShow.Location = New System.Drawing.Point(188, 12)
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
        Me.lblLinieArtikel.Location = New System.Drawing.Point(218, 89)
        Me.lblLinieArtikel.Name = "lblLinieArtikel"
        Me.lblLinieArtikel.Size = New System.Drawing.Size(137, 13)
        Me.lblLinieArtikel.TabIndex = 33
        Me.lblLinieArtikel.Text = "Liniengruppe Artikel-Rezept"
        '
        'tStkGewicht
        '
        Me.tStkGewicht.Location = New System.Drawing.Point(116, 15)
        Me.tStkGewicht.Name = "tStkGewicht"
        Me.tStkGewicht.Size = New System.Drawing.Size(54, 20)
        Me.tStkGewicht.TabIndex = 34
        Me.tStkGewicht.TabStop = False
        Me.tStkGewicht.Text = "1567 gr "
        Me.tStkGewicht.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tStkGewicht.WordWrap = False
        '
        'lblProStk
        '
        Me.lblProStk.AutoSize = True
        Me.lblProStk.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblProStk.Location = New System.Drawing.Point(9, 18)
        Me.lblProStk.Name = "lblProStk"
        Me.lblProStk.Size = New System.Drawing.Size(84, 13)
        Me.lblProStk.TabIndex = 35
        Me.lblProStk.Text = "pro Stück (nass)"
        '
        'lblMin
        '
        Me.lblMin.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblMin.Location = New System.Drawing.Point(157, 200)
        Me.lblMin.Name = "lblMin"
        Me.lblMin.Size = New System.Drawing.Size(52, 13)
        Me.lblMin.TabIndex = 37
        Me.lblMin.Text = "Minimal"
        Me.lblMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tChrgMinkg
        '
        Me.tChrgMinkg.Location = New System.Drawing.Point(3, 41)
        Me.tChrgMinkg.Name = "tChrgMinkg"
        Me.tChrgMinkg.Size = New System.Drawing.Size(67, 20)
        Me.tChrgMinkg.TabIndex = 36
        Me.tChrgMinkg.TabStop = False
        Me.tChrgMinkg.Text = "2000,000 kg"
        Me.tChrgMinkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tChrgMinkg.WordWrap = False
        '
        'tChrgMinPrz
        '
        Me.tChrgMinPrz.Location = New System.Drawing.Point(73, 41)
        Me.tChrgMinPrz.Name = "tChrgMinPrz"
        Me.tChrgMinPrz.Size = New System.Drawing.Size(40, 20)
        Me.tChrgMinPrz.TabIndex = 38
        Me.tChrgMinPrz.TabStop = False
        Me.tChrgMinPrz.Text = "999%"
        Me.tChrgMinPrz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tChrgMinPrz.WordWrap = False
        '
        'pArtikelChargen
        '
        Me.pArtikelChargen.BackColor = System.Drawing.Color.Transparent
        Me.pArtikelChargen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pArtikelChargen.Controls.Add(Me.tChrgOptStk)
        Me.pArtikelChargen.Controls.Add(Me.tChrgOptkg)
        Me.pArtikelChargen.Controls.Add(Me.tChrgOptPrz)
        Me.pArtikelChargen.Controls.Add(Me.tChrgMaxStk)
        Me.pArtikelChargen.Controls.Add(Me.tChrgMaxkg)
        Me.pArtikelChargen.Controls.Add(Me.tChrgMaxPrz)
        Me.pArtikelChargen.Controls.Add(Me.tChrgMinStk)
        Me.pArtikelChargen.Controls.Add(Me.tChrgMinkg)
        Me.pArtikelChargen.Controls.Add(Me.tChrgMinPrz)
        Me.pArtikelChargen.Controls.Add(Me.lblProStk)
        Me.pArtikelChargen.Controls.Add(Me.tStkGewicht)
        Me.pArtikelChargen.Location = New System.Drawing.Point(211, 154)
        Me.pArtikelChargen.Name = "pArtikelChargen"
        Me.pArtikelChargen.Size = New System.Drawing.Size(176, 122)
        Me.pArtikelChargen.TabIndex = 40
        Me.ToolTip.SetToolTip(Me.pArtikelChargen, resources.GetString("pArtikelChargen.ToolTip"))
        '
        'tChrgOptStk
        '
        Me.tChrgOptStk.Location = New System.Drawing.Point(116, 93)
        Me.tChrgOptStk.Name = "tChrgOptStk"
        Me.tChrgOptStk.Size = New System.Drawing.Size(54, 20)
        Me.tChrgOptStk.TabIndex = 45
        Me.tChrgOptStk.TabStop = False
        Me.tChrgOptStk.Text = "14000 Stk"
        Me.tChrgOptStk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tChrgOptStk.WordWrap = False
        '
        'tChrgOptkg
        '
        Me.tChrgOptkg.Location = New System.Drawing.Point(3, 93)
        Me.tChrgOptkg.Name = "tChrgOptkg"
        Me.tChrgOptkg.Size = New System.Drawing.Size(67, 20)
        Me.tChrgOptkg.TabIndex = 43
        Me.tChrgOptkg.TabStop = False
        Me.tChrgOptkg.Text = "2000,000 kg"
        Me.tChrgOptkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tChrgOptkg.WordWrap = False
        '
        'tChrgOptPrz
        '
        Me.tChrgOptPrz.Location = New System.Drawing.Point(73, 93)
        Me.tChrgOptPrz.Name = "tChrgOptPrz"
        Me.tChrgOptPrz.Size = New System.Drawing.Size(40, 20)
        Me.tChrgOptPrz.TabIndex = 44
        Me.tChrgOptPrz.TabStop = False
        Me.tChrgOptPrz.Text = "999%"
        Me.tChrgOptPrz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tChrgOptPrz.WordWrap = False
        '
        'tChrgMaxStk
        '
        Me.tChrgMaxStk.Location = New System.Drawing.Point(116, 67)
        Me.tChrgMaxStk.Name = "tChrgMaxStk"
        Me.tChrgMaxStk.Size = New System.Drawing.Size(54, 20)
        Me.tChrgMaxStk.TabIndex = 42
        Me.tChrgMaxStk.TabStop = False
        Me.tChrgMaxStk.Text = "14000 Stk"
        Me.tChrgMaxStk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tChrgMaxStk.WordWrap = False
        '
        'tChrgMaxkg
        '
        Me.tChrgMaxkg.Location = New System.Drawing.Point(3, 67)
        Me.tChrgMaxkg.Name = "tChrgMaxkg"
        Me.tChrgMaxkg.Size = New System.Drawing.Size(67, 20)
        Me.tChrgMaxkg.TabIndex = 40
        Me.tChrgMaxkg.TabStop = False
        Me.tChrgMaxkg.Text = "2000,000 kg"
        Me.tChrgMaxkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tChrgMaxkg.WordWrap = False
        '
        'tChrgMaxPrz
        '
        Me.tChrgMaxPrz.Location = New System.Drawing.Point(73, 67)
        Me.tChrgMaxPrz.Name = "tChrgMaxPrz"
        Me.tChrgMaxPrz.Size = New System.Drawing.Size(40, 20)
        Me.tChrgMaxPrz.TabIndex = 41
        Me.tChrgMaxPrz.TabStop = False
        Me.tChrgMaxPrz.Text = "999%"
        Me.tChrgMaxPrz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tChrgMaxPrz.WordWrap = False
        '
        'tChrgMinStk
        '
        Me.tChrgMinStk.Location = New System.Drawing.Point(116, 41)
        Me.tChrgMinStk.Name = "tChrgMinStk"
        Me.tChrgMinStk.Size = New System.Drawing.Size(54, 20)
        Me.tChrgMinStk.TabIndex = 39
        Me.tChrgMinStk.TabStop = False
        Me.tChrgMinStk.Text = "14000 Stk"
        Me.tChrgMinStk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tChrgMinStk.WordWrap = False
        '
        'lblArtikelChargen
        '
        Me.lblArtikelChargen.AutoSize = True
        Me.lblArtikelChargen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblArtikelChargen.Location = New System.Drawing.Point(221, 148)
        Me.lblArtikelChargen.Name = "lblArtikelChargen"
        Me.lblArtikelChargen.Size = New System.Drawing.Size(79, 13)
        Me.lblArtikelChargen.TabIndex = 41
        Me.lblArtikelChargen.Text = "Artikel-Chargen"
        '
        'lblGewicht
        '
        Me.lblGewicht.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblGewicht.Location = New System.Drawing.Point(157, 174)
        Me.lblGewicht.Name = "lblGewicht"
        Me.lblGewicht.Size = New System.Drawing.Size(52, 13)
        Me.lblGewicht.TabIndex = 42
        Me.lblGewicht.Text = "Gewicht"
        Me.lblGewicht.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMax
        '
        Me.lblMax.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblMax.Location = New System.Drawing.Point(158, 226)
        Me.lblMax.Name = "lblMax"
        Me.lblMax.Size = New System.Drawing.Size(51, 13)
        Me.lblMax.TabIndex = 43
        Me.lblMax.Text = "Maximal"
        Me.lblMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOpt
        '
        Me.lblOpt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblOpt.Location = New System.Drawing.Point(158, 252)
        Me.lblOpt.Name = "lblOpt"
        Me.lblOpt.Size = New System.Drawing.Size(51, 13)
        Me.lblOpt.TabIndex = 44
        Me.lblOpt.Text = "Optimal"
        Me.lblOpt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pTeigChargen
        '
        Me.pTeigChargen.BackColor = System.Drawing.Color.Transparent
        Me.pTeigChargen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pTeigChargen.Controls.Add(Me.tRezOptkg)
        Me.pTeigChargen.Controls.Add(Me.tRezOptPrz)
        Me.pTeigChargen.Controls.Add(Me.tRezMaxkg)
        Me.pTeigChargen.Controls.Add(Me.tRezMaxPrz)
        Me.pTeigChargen.Controls.Add(Me.tRezMinkg)
        Me.pTeigChargen.Controls.Add(Me.tRezMinPrz)
        Me.pTeigChargen.Controls.Add(Me.lblTeigGesamt)
        Me.pTeigChargen.Controls.Add(Me.tRezGesamt)
        Me.pTeigChargen.Location = New System.Drawing.Point(4, 154)
        Me.pTeigChargen.Name = "pTeigChargen"
        Me.pTeigChargen.Size = New System.Drawing.Size(151, 122)
        Me.pTeigChargen.TabIndex = 46
        Me.ToolTip.SetToolTip(Me.pTeigChargen, "Wenn in der Produktions-Planung Teige zusammen-" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "gefasst werden können, wird die " &
        "Teiggrößen für die " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Aufteilung der Gesamt-Teigmenge anhand der Teig-" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Chargen-G" &
        "rößen vorgenommen.")
        '
        'tRezOptkg
        '
        Me.tRezOptkg.Location = New System.Drawing.Point(3, 93)
        Me.tRezOptkg.Name = "tRezOptkg"
        Me.tRezOptkg.Size = New System.Drawing.Size(67, 20)
        Me.tRezOptkg.TabIndex = 43
        Me.tRezOptkg.TabStop = False
        Me.tRezOptkg.Text = "2000,000 kg"
        Me.tRezOptkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tRezOptkg.WordWrap = False
        '
        'tRezOptPrz
        '
        Me.tRezOptPrz.Location = New System.Drawing.Point(77, 93)
        Me.tRezOptPrz.Name = "tRezOptPrz"
        Me.tRezOptPrz.Size = New System.Drawing.Size(50, 20)
        Me.tRezOptPrz.TabIndex = 44
        Me.tRezOptPrz.TabStop = False
        Me.tRezOptPrz.Text = "999%"
        Me.tRezOptPrz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tRezOptPrz.WordWrap = False
        '
        'tRezMaxkg
        '
        Me.tRezMaxkg.Location = New System.Drawing.Point(3, 67)
        Me.tRezMaxkg.Name = "tRezMaxkg"
        Me.tRezMaxkg.Size = New System.Drawing.Size(67, 20)
        Me.tRezMaxkg.TabIndex = 40
        Me.tRezMaxkg.TabStop = False
        Me.tRezMaxkg.Text = "2000,000 kg"
        Me.tRezMaxkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tRezMaxkg.WordWrap = False
        '
        'tRezMaxPrz
        '
        Me.tRezMaxPrz.Location = New System.Drawing.Point(77, 67)
        Me.tRezMaxPrz.Name = "tRezMaxPrz"
        Me.tRezMaxPrz.Size = New System.Drawing.Size(50, 20)
        Me.tRezMaxPrz.TabIndex = 41
        Me.tRezMaxPrz.TabStop = False
        Me.tRezMaxPrz.Text = "999%"
        Me.tRezMaxPrz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tRezMaxPrz.WordWrap = False
        '
        'tRezMinkg
        '
        Me.tRezMinkg.Location = New System.Drawing.Point(3, 41)
        Me.tRezMinkg.Name = "tRezMinkg"
        Me.tRezMinkg.Size = New System.Drawing.Size(67, 20)
        Me.tRezMinkg.TabIndex = 36
        Me.tRezMinkg.TabStop = False
        Me.tRezMinkg.Text = "2000,000 kg"
        Me.tRezMinkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tRezMinkg.WordWrap = False
        '
        'tRezMinPrz
        '
        Me.tRezMinPrz.Location = New System.Drawing.Point(77, 41)
        Me.tRezMinPrz.Name = "tRezMinPrz"
        Me.tRezMinPrz.Size = New System.Drawing.Size(50, 20)
        Me.tRezMinPrz.TabIndex = 38
        Me.tRezMinPrz.TabStop = False
        Me.tRezMinPrz.Text = "999%"
        Me.tRezMinPrz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tRezMinPrz.WordWrap = False
        '
        'lblTeigGesamt
        '
        Me.lblTeigGesamt.AutoSize = True
        Me.lblTeigGesamt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTeigGesamt.Location = New System.Drawing.Point(71, 18)
        Me.lblTeigGesamt.Name = "lblTeigGesamt"
        Me.lblTeigGesamt.Size = New System.Drawing.Size(73, 13)
        Me.lblTeigGesamt.TabIndex = 35
        Me.lblTeigGesamt.Text = "Teig (Gesamt)"
        '
        'tRezGesamt
        '
        Me.tRezGesamt.Location = New System.Drawing.Point(3, 15)
        Me.tRezGesamt.Name = "tRezGesamt"
        Me.tRezGesamt.ReadOnly = True
        Me.tRezGesamt.Size = New System.Drawing.Size(67, 20)
        Me.tRezGesamt.TabIndex = 34
        Me.tRezGesamt.TabStop = False
        Me.tRezGesamt.Text = "245,999 kg"
        Me.tRezGesamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tRezGesamt.WordWrap = False
        '
        'lblTeigChargen
        '
        Me.lblTeigChargen.AutoSize = True
        Me.lblTeigChargen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTeigChargen.Location = New System.Drawing.Point(14, 149)
        Me.lblTeigChargen.Name = "lblTeigChargen"
        Me.lblTeigChargen.Size = New System.Drawing.Size(71, 13)
        Me.lblTeigChargen.TabIndex = 47
        Me.lblTeigChargen.Text = "Teig-Chargen"
        '
        'cbArtikelLinienGruppe
        '
        Me.cbArtikelLinienGruppe.FormattingEnabled = True
        Me.cbArtikelLinienGruppe.Location = New System.Drawing.Point(215, 105)
        Me.cbArtikelLinienGruppe.Name = "cbArtikelLinienGruppe"
        Me.cbArtikelLinienGruppe.Size = New System.Drawing.Size(172, 21)
        Me.cbArtikelLinienGruppe.TabIndex = 32
        Me.cbArtikelLinienGruppe.TabStop = False
        Me.ToolTip.SetToolTip(Me.cbArtikelLinienGruppe, "Produktions-Ort für den Artikel z.B." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Brötchen-Maschine, Aufarbeitung...")
        '
        'cbLiniengruppe
        '
        Me.cbLiniengruppe.FormattingEnabled = True
        Me.cbLiniengruppe.Location = New System.Drawing.Point(3, 105)
        Me.cbLiniengruppe.Name = "cbLiniengruppe"
        Me.cbLiniengruppe.Size = New System.Drawing.Size(176, 21)
        Me.cbLiniengruppe.TabIndex = 30
        Me.cbLiniengruppe.TabStop = False
        Me.ToolTip.SetToolTip(Me.cbLiniengruppe, "Produktions-Liniengruppe aus der" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Teig-Rezeptur")
        '
        'ob_Artikel_ZuordnungRezept
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoScrollMinSize = New System.Drawing.Size(190, 268)
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.lblTeigChargen)
        Me.Controls.Add(Me.pTeigChargen)
        Me.Controls.Add(Me.lblOpt)
        Me.Controls.Add(Me.lblMax)
        Me.Controls.Add(Me.lblGewicht)
        Me.Controls.Add(Me.lblArtikelChargen)
        Me.Controls.Add(Me.pArtikelChargen)
        Me.Controls.Add(Me.lblMin)
        Me.Controls.Add(Me.lblLinieArtikel)
        Me.Controls.Add(Me.cbArtikelLinienGruppe)
        Me.Controls.Add(Me.lblLinienGruppe)
        Me.Controls.Add(Me.cbLiniengruppe)
        Me.Controls.Add(Me.BtnRzpShow)
        Me.Controls.Add(Me.BtnRzptChange)
        Me.Controls.Add(Me.tRezeptName)
        Me.Controls.Add(Me.lblRzeptBezeichnung)
        Me.Controls.Add(Me.tRezeptNr)
        Me.Controls.Add(Me.lblRezeptNr)
        Me.MinimumSize = New System.Drawing.Size(190, 268)
        Me.Name = "ob_Artikel_ZuordnungRezept"
        Me.Size = New System.Drawing.Size(392, 280)
        Me.pArtikelChargen.ResumeLayout(False)
        Me.pArtikelChargen.PerformLayout()
        Me.pTeigChargen.ResumeLayout(False)
        Me.pTeigChargen.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tRezeptName As Windows.Forms.TextBox
    Friend WithEvents lblRzeptBezeichnung As Windows.Forms.Label
    Friend WithEvents tRezeptNr As Windows.Forms.TextBox
    Friend WithEvents lblRezeptNr As Windows.Forms.Label
    Friend WithEvents BtnRzptChange As Windows.Forms.Button
    Friend WithEvents BtnRzpShow As Windows.Forms.Button
    Friend WithEvents lblLinienGruppe As Windows.Forms.Label
    Friend WithEvents cbLiniengruppe As wb_ComboBox
    Friend WithEvents cbArtikelLinienGruppe As wb_ComboBox
    Friend WithEvents lblLinieArtikel As Windows.Forms.Label
    Friend WithEvents tStkGewicht As Windows.Forms.TextBox
    Friend WithEvents lblProStk As Windows.Forms.Label
    Friend WithEvents lblMin As Windows.Forms.Label
    Friend WithEvents tChrgMinkg As Windows.Forms.TextBox
    Friend WithEvents tChrgMinPrz As Windows.Forms.TextBox
    Friend WithEvents pArtikelChargen As Windows.Forms.Panel
    Friend WithEvents tChrgOptStk As Windows.Forms.TextBox
    Friend WithEvents tChrgOptkg As Windows.Forms.TextBox
    Friend WithEvents tChrgOptPrz As Windows.Forms.TextBox
    Friend WithEvents tChrgMaxStk As Windows.Forms.TextBox
    Friend WithEvents tChrgMaxkg As Windows.Forms.TextBox
    Friend WithEvents tChrgMaxPrz As Windows.Forms.TextBox
    Friend WithEvents tChrgMinStk As Windows.Forms.TextBox
    Friend WithEvents lblArtikelChargen As Windows.Forms.Label
    Friend WithEvents lblGewicht As Windows.Forms.Label
    Friend WithEvents lblMax As Windows.Forms.Label
    Friend WithEvents lblOpt As Windows.Forms.Label
    Friend WithEvents pTeigChargen As Windows.Forms.Panel
    Friend WithEvents tRezOptkg As Windows.Forms.TextBox
    Friend WithEvents tRezOptPrz As Windows.Forms.TextBox
    Friend WithEvents tRezMaxkg As Windows.Forms.TextBox
    Friend WithEvents tRezMaxPrz As Windows.Forms.TextBox
    Friend WithEvents tRezMinkg As Windows.Forms.TextBox
    Friend WithEvents tRezMinPrz As Windows.Forms.TextBox
    Friend WithEvents lblTeigGesamt As Windows.Forms.Label
    Friend WithEvents tRezGesamt As Windows.Forms.TextBox
    Friend WithEvents lblTeigChargen As Windows.Forms.Label
    Friend WithEvents ToolTip As Windows.Forms.ToolTip

    'Friend WithEvents PropertyGrid As Signum.OrgaSoft.GUI.Controls.PropertyGrid
End Class
