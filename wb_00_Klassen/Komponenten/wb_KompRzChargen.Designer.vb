<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_KompRzChargen
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
        Me.lblTeigChargen = New System.Windows.Forms.Label()
        Me.pTeigChargen = New System.Windows.Forms.Panel()
        Me.tRezOptkg = New System.Windows.Forms.TextBox()
        Me.tRezOptPrz = New System.Windows.Forms.TextBox()
        Me.tRezMaxkg = New System.Windows.Forms.TextBox()
        Me.tRezMaxPrz = New System.Windows.Forms.TextBox()
        Me.tRezMinkg = New System.Windows.Forms.TextBox()
        Me.tRezMinPrz = New System.Windows.Forms.TextBox()
        Me.lblTeigGesamt = New System.Windows.Forms.Label()
        Me.tRezGesamt = New System.Windows.Forms.TextBox()
        Me.lblOpt = New System.Windows.Forms.Label()
        Me.lblMax = New System.Windows.Forms.Label()
        Me.lblGewicht = New System.Windows.Forms.Label()
        Me.lblArtikelChargen = New System.Windows.Forms.Label()
        Me.pArtikelChargen = New System.Windows.Forms.Panel()
        Me.tChrgOptStk = New System.Windows.Forms.TextBox()
        Me.tChrgOptkg = New System.Windows.Forms.TextBox()
        Me.tChrgOptPrz = New System.Windows.Forms.TextBox()
        Me.tChrgMaxStk = New System.Windows.Forms.TextBox()
        Me.tChrgMaxkg = New System.Windows.Forms.TextBox()
        Me.tChrgMaxPrz = New System.Windows.Forms.TextBox()
        Me.tChrgMinStk = New System.Windows.Forms.TextBox()
        Me.tChrgMinkg = New System.Windows.Forms.TextBox()
        Me.tChrgMinPrz = New System.Windows.Forms.TextBox()
        Me.lblProStk = New System.Windows.Forms.Label()
        Me.tStkGewicht = New System.Windows.Forms.TextBox()
        Me.lblMin = New System.Windows.Forms.Label()
        Me.lblLinieArtikel = New System.Windows.Forms.Label()
        Me.lblLinienGruppe = New System.Windows.Forms.Label()
        Me.BtnRzpShow = New System.Windows.Forms.Button()
        Me.BtnRzpt = New System.Windows.Forms.Button()
        Me.tRezeptName = New System.Windows.Forms.TextBox()
        Me.lblRzeptBezeichnung = New System.Windows.Forms.Label()
        Me.tRezeptNr = New System.Windows.Forms.TextBox()
        Me.lblRezeptNr = New System.Windows.Forms.Label()
        Me.tBackverlust = New System.Windows.Forms.TextBox()
        Me.lblBackverlust = New System.Windows.Forms.Label()
        Me.lblProdVorlauf = New System.Windows.Forms.Label()
        Me.tProdVorlauf = New System.Windows.Forms.TextBox()
        Me.BtnCloud = New System.Windows.Forms.Button()
        Me.cbArtikelLinienGruppe = New WinBack.wb_ComboBox()
        Me.cbLiniengruppe = New WinBack.wb_ComboBox()
        Me.lblZuschnitt = New System.Windows.Forms.Label()
        Me.tZuschnitt = New System.Windows.Forms.TextBox()
        Me.tVkGewicht = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnCalcNassGewicht = New System.Windows.Forms.Button()
        Me.BtnUpdateNwt = New System.Windows.Forms.Button()
        Me.pTeigChargen.SuspendLayout()
        Me.pArtikelChargen.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTeigChargen
        '
        Me.lblTeigChargen.AutoSize = True
        Me.lblTeigChargen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTeigChargen.Location = New System.Drawing.Point(18, 142)
        Me.lblTeigChargen.Name = "lblTeigChargen"
        Me.lblTeigChargen.Size = New System.Drawing.Size(71, 13)
        Me.lblTeigChargen.TabIndex = 65
        Me.lblTeigChargen.Text = "Teig-Chargen"
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
        Me.pTeigChargen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pTeigChargen.Location = New System.Drawing.Point(8, 141)
        Me.pTeigChargen.Name = "pTeigChargen"
        Me.pTeigChargen.Size = New System.Drawing.Size(151, 122)
        Me.pTeigChargen.TabIndex = 64
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
        'lblOpt
        '
        Me.lblOpt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblOpt.Location = New System.Drawing.Point(162, 239)
        Me.lblOpt.Name = "lblOpt"
        Me.lblOpt.Size = New System.Drawing.Size(51, 13)
        Me.lblOpt.TabIndex = 63
        Me.lblOpt.Text = "Optimal"
        Me.lblOpt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMax
        '
        Me.lblMax.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblMax.Location = New System.Drawing.Point(162, 213)
        Me.lblMax.Name = "lblMax"
        Me.lblMax.Size = New System.Drawing.Size(51, 13)
        Me.lblMax.TabIndex = 62
        Me.lblMax.Text = "Maximal"
        Me.lblMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblGewicht
        '
        Me.lblGewicht.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblGewicht.Location = New System.Drawing.Point(161, 161)
        Me.lblGewicht.Name = "lblGewicht"
        Me.lblGewicht.Size = New System.Drawing.Size(52, 13)
        Me.lblGewicht.TabIndex = 61
        Me.lblGewicht.Text = "Gewicht"
        Me.lblGewicht.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblArtikelChargen
        '
        Me.lblArtikelChargen.AutoSize = True
        Me.lblArtikelChargen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblArtikelChargen.Location = New System.Drawing.Point(225, 143)
        Me.lblArtikelChargen.Name = "lblArtikelChargen"
        Me.lblArtikelChargen.Size = New System.Drawing.Size(79, 13)
        Me.lblArtikelChargen.TabIndex = 60
        Me.lblArtikelChargen.Text = "Artikel-Chargen"
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
        Me.pArtikelChargen.Location = New System.Drawing.Point(215, 141)
        Me.pArtikelChargen.Name = "pArtikelChargen"
        Me.pArtikelChargen.Size = New System.Drawing.Size(176, 122)
        Me.pArtikelChargen.TabIndex = 59
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
        'lblProStk
        '
        Me.lblProStk.AutoSize = True
        Me.lblProStk.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblProStk.Location = New System.Drawing.Point(9, 17)
        Me.lblProStk.Name = "lblProStk"
        Me.lblProStk.Size = New System.Drawing.Size(84, 13)
        Me.lblProStk.TabIndex = 35
        Me.lblProStk.Text = "pro Stück (nass)"
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
        'lblMin
        '
        Me.lblMin.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblMin.Location = New System.Drawing.Point(161, 187)
        Me.lblMin.Name = "lblMin"
        Me.lblMin.Size = New System.Drawing.Size(52, 13)
        Me.lblMin.TabIndex = 58
        Me.lblMin.Text = "Minimal"
        Me.lblMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLinieArtikel
        '
        Me.lblLinieArtikel.AutoSize = True
        Me.lblLinieArtikel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblLinieArtikel.Location = New System.Drawing.Point(222, 91)
        Me.lblLinieArtikel.Name = "lblLinieArtikel"
        Me.lblLinieArtikel.Size = New System.Drawing.Size(131, 13)
        Me.lblLinieArtikel.TabIndex = 57
        Me.lblLinieArtikel.Text = "Liniengruppe Aufarbeitung"
        '
        'lblLinienGruppe
        '
        Me.lblLinienGruppe.AutoSize = True
        Me.lblLinienGruppe.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblLinienGruppe.Location = New System.Drawing.Point(7, 91)
        Me.lblLinienGruppe.Name = "lblLinienGruppe"
        Me.lblLinienGruppe.Size = New System.Drawing.Size(92, 13)
        Me.lblLinienGruppe.TabIndex = 55
        Me.lblLinienGruppe.Text = "Liniengruppe Teig"
        '
        'BtnRzpShow
        '
        Me.BtnRzpShow.Location = New System.Drawing.Point(221, 14)
        Me.BtnRzpShow.Name = "BtnRzpShow"
        Me.BtnRzpShow.Size = New System.Drawing.Size(78, 40)
        Me.BtnRzpShow.TabIndex = 2
        Me.BtnRzpShow.Text = "Öffnen"
        Me.BtnRzpShow.UseVisualStyleBackColor = True
        '
        'BtnRzpt
        '
        Me.BtnRzpt.Location = New System.Drawing.Point(113, 14)
        Me.BtnRzpt.Name = "BtnRzpt"
        Me.BtnRzpt.Size = New System.Drawing.Size(104, 40)
        Me.BtnRzpt.TabIndex = 1
        Me.BtnRzpt.TabStop = False
        Me.BtnRzpt.Text = "Auswählen"
        Me.BtnRzpt.UseVisualStyleBackColor = True
        '
        'tRezeptName
        '
        Me.tRezeptName.Location = New System.Drawing.Point(7, 68)
        Me.tRezeptName.Name = "tRezeptName"
        Me.tRezeptName.ReadOnly = True
        Me.tRezeptName.Size = New System.Drawing.Size(384, 20)
        Me.tRezeptName.TabIndex = 4
        Me.tRezeptName.TabStop = False
        '
        'lblRzeptBezeichnung
        '
        Me.lblRzeptBezeichnung.AutoSize = True
        Me.lblRzeptBezeichnung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzeptBezeichnung.Location = New System.Drawing.Point(7, 52)
        Me.lblRzeptBezeichnung.Name = "lblRzeptBezeichnung"
        Me.lblRzeptBezeichnung.Size = New System.Drawing.Size(106, 13)
        Me.lblRzeptBezeichnung.TabIndex = 51
        Me.lblRzeptBezeichnung.Text = "Rezept-Bezeichnung"
        '
        'tRezeptNr
        '
        Me.tRezeptNr.Location = New System.Drawing.Point(7, 25)
        Me.tRezeptNr.Name = "tRezeptNr"
        Me.tRezeptNr.ReadOnly = True
        Me.tRezeptNr.Size = New System.Drawing.Size(72, 20)
        Me.tRezeptNr.TabIndex = 50
        Me.tRezeptNr.TabStop = False
        '
        'lblRezeptNr
        '
        Me.lblRezeptNr.AutoSize = True
        Me.lblRezeptNr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRezeptNr.Location = New System.Drawing.Point(7, 9)
        Me.lblRezeptNr.Name = "lblRezeptNr"
        Me.lblRezeptNr.Size = New System.Drawing.Size(58, 13)
        Me.lblRezeptNr.TabIndex = 49
        Me.lblRezeptNr.Text = "Rezept-Nr."
        '
        'tBackverlust
        '
        Me.tBackverlust.Location = New System.Drawing.Point(219, 279)
        Me.tBackverlust.Name = "tBackverlust"
        Me.tBackverlust.Size = New System.Drawing.Size(66, 20)
        Me.tBackverlust.TabIndex = 66
        Me.tBackverlust.TabStop = False
        Me.tBackverlust.Text = "99.999 %"
        Me.tBackverlust.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tBackverlust.WordWrap = False
        '
        'lblBackverlust
        '
        Me.lblBackverlust.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblBackverlust.Location = New System.Drawing.Point(153, 279)
        Me.lblBackverlust.Name = "lblBackverlust"
        Me.lblBackverlust.Size = New System.Drawing.Size(64, 19)
        Me.lblBackverlust.TabIndex = 67
        Me.lblBackverlust.Text = "Backverlust"
        Me.lblBackverlust.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblProdVorlauf
        '
        Me.lblProdVorlauf.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblProdVorlauf.Location = New System.Drawing.Point(5, 274)
        Me.lblProdVorlauf.Name = "lblProdVorlauf"
        Me.lblProdVorlauf.Size = New System.Drawing.Size(75, 27)
        Me.lblProdVorlauf.TabIndex = 68
        Me.lblProdVorlauf.Text = "Vorlauf Produktion"
        Me.lblProdVorlauf.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tProdVorlauf
        '
        Me.tProdVorlauf.Location = New System.Drawing.Point(86, 279)
        Me.tProdVorlauf.Name = "tProdVorlauf"
        Me.tProdVorlauf.Size = New System.Drawing.Size(50, 20)
        Me.tProdVorlauf.TabIndex = 69
        Me.tProdVorlauf.TabStop = False
        Me.tProdVorlauf.Text = "999 h"
        Me.tProdVorlauf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tProdVorlauf.WordWrap = False
        '
        'BtnCloud
        '
        Me.BtnCloud.Location = New System.Drawing.Point(313, 14)
        Me.BtnCloud.Name = "BtnCloud"
        Me.BtnCloud.Size = New System.Drawing.Size(78, 40)
        Me.BtnCloud.TabIndex = 3
        Me.BtnCloud.Text = "Cloud"
        Me.BtnCloud.UseVisualStyleBackColor = True
        '
        'cbArtikelLinienGruppe
        '
        Me.cbArtikelLinienGruppe.FormattingEnabled = True
        Me.cbArtikelLinienGruppe.Location = New System.Drawing.Point(219, 107)
        Me.cbArtikelLinienGruppe.Name = "cbArtikelLinienGruppe"
        Me.cbArtikelLinienGruppe.Size = New System.Drawing.Size(172, 21)
        Me.cbArtikelLinienGruppe.TabIndex = 6
        Me.cbArtikelLinienGruppe.TabStop = False
        '
        'cbLiniengruppe
        '
        Me.cbLiniengruppe.FormattingEnabled = True
        Me.cbLiniengruppe.Location = New System.Drawing.Point(7, 107)
        Me.cbLiniengruppe.Name = "cbLiniengruppe"
        Me.cbLiniengruppe.Size = New System.Drawing.Size(176, 21)
        Me.cbLiniengruppe.TabIndex = 5
        Me.cbLiniengruppe.TabStop = False
        Me.cbLiniengruppe.Text = "LG"
        '
        'lblZuschnitt
        '
        Me.lblZuschnitt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblZuschnitt.Location = New System.Drawing.Point(153, 305)
        Me.lblZuschnitt.Name = "lblZuschnitt"
        Me.lblZuschnitt.Size = New System.Drawing.Size(64, 19)
        Me.lblZuschnitt.TabIndex = 71
        Me.lblZuschnitt.Text = "Zuschnitt"
        Me.lblZuschnitt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tZuschnitt
        '
        Me.tZuschnitt.Location = New System.Drawing.Point(219, 305)
        Me.tZuschnitt.Name = "tZuschnitt"
        Me.tZuschnitt.Size = New System.Drawing.Size(66, 20)
        Me.tZuschnitt.TabIndex = 70
        Me.tZuschnitt.TabStop = False
        Me.tZuschnitt.Text = "99.999 %"
        Me.tZuschnitt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tZuschnitt.WordWrap = False
        '
        'tVkGewicht
        '
        Me.tVkGewicht.Location = New System.Drawing.Point(86, 304)
        Me.tVkGewicht.Name = "tVkGewicht"
        Me.tVkGewicht.ReadOnly = True
        Me.tVkGewicht.Size = New System.Drawing.Size(50, 20)
        Me.tVkGewicht.TabIndex = 72
        Me.tVkGewicht.TabStop = False
        Me.tVkGewicht.Text = "240 gr"
        Me.tVkGewicht.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tVkGewicht.WordWrap = False
        '
        'Label1
        '
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(5, 303)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 27)
        Me.Label1.TabIndex = 73
        Me.Label1.Text = "Verkaufs- Gewicht"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BtnCalcNassGewicht
        '
        Me.BtnCalcNassGewicht.Location = New System.Drawing.Point(291, 283)
        Me.BtnCalcNassGewicht.Name = "BtnCalcNassGewicht"
        Me.BtnCalcNassGewicht.Size = New System.Drawing.Size(100, 40)
        Me.BtnCalcNassGewicht.TabIndex = 74
        Me.BtnCalcNassGewicht.Text = "Nass-Gewicht berechnen"
        Me.BtnCalcNassGewicht.UseVisualStyleBackColor = True
        '
        'BtnUpdateNwt
        '
        Me.BtnUpdateNwt.Location = New System.Drawing.Point(313, 60)
        Me.BtnUpdateNwt.Name = "BtnUpdateNwt"
        Me.BtnUpdateNwt.Size = New System.Drawing.Size(78, 40)
        Me.BtnUpdateNwt.TabIndex = 75
        Me.BtnUpdateNwt.Text = "Update Nährwerte"
        Me.BtnUpdateNwt.UseVisualStyleBackColor = True
        '
        'wb_KompRzChargen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BtnUpdateNwt)
        Me.Controls.Add(Me.BtnCalcNassGewicht)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tVkGewicht)
        Me.Controls.Add(Me.lblZuschnitt)
        Me.Controls.Add(Me.tZuschnitt)
        Me.Controls.Add(Me.BtnCloud)
        Me.Controls.Add(Me.tProdVorlauf)
        Me.Controls.Add(Me.lblProdVorlauf)
        Me.Controls.Add(Me.lblBackverlust)
        Me.Controls.Add(Me.tBackverlust)
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
        Me.Controls.Add(Me.BtnRzpt)
        Me.Controls.Add(Me.tRezeptName)
        Me.Controls.Add(Me.lblRzeptBezeichnung)
        Me.Controls.Add(Me.tRezeptNr)
        Me.Controls.Add(Me.lblRezeptNr)
        Me.Name = "wb_KompRzChargen"
        Me.Size = New System.Drawing.Size(400, 336)
        Me.pTeigChargen.ResumeLayout(False)
        Me.pTeigChargen.PerformLayout()
        Me.pArtikelChargen.ResumeLayout(False)
        Me.pArtikelChargen.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTeigChargen As Windows.Forms.Label
    Friend WithEvents pTeigChargen As Windows.Forms.Panel
    Friend WithEvents tRezOptkg As Windows.Forms.TextBox
    Friend WithEvents tRezOptPrz As Windows.Forms.TextBox
    Friend WithEvents tRezMaxkg As Windows.Forms.TextBox
    Friend WithEvents tRezMaxPrz As Windows.Forms.TextBox
    Friend WithEvents tRezMinkg As Windows.Forms.TextBox
    Friend WithEvents tRezMinPrz As Windows.Forms.TextBox
    Friend WithEvents lblTeigGesamt As Windows.Forms.Label
    Friend WithEvents tRezGesamt As Windows.Forms.TextBox
    Friend WithEvents lblOpt As Windows.Forms.Label
    Friend WithEvents lblMax As Windows.Forms.Label
    Friend WithEvents lblGewicht As Windows.Forms.Label
    Friend WithEvents lblArtikelChargen As Windows.Forms.Label
    Friend WithEvents pArtikelChargen As Windows.Forms.Panel
    Friend WithEvents tChrgOptStk As Windows.Forms.TextBox
    Friend WithEvents tChrgOptkg As Windows.Forms.TextBox
    Friend WithEvents tChrgOptPrz As Windows.Forms.TextBox
    Friend WithEvents tChrgMaxStk As Windows.Forms.TextBox
    Friend WithEvents tChrgMaxkg As Windows.Forms.TextBox
    Friend WithEvents tChrgMaxPrz As Windows.Forms.TextBox
    Friend WithEvents tChrgMinStk As Windows.Forms.TextBox
    Friend WithEvents tChrgMinkg As Windows.Forms.TextBox
    Friend WithEvents tChrgMinPrz As Windows.Forms.TextBox
    Friend WithEvents lblProStk As Windows.Forms.Label
    Friend WithEvents tStkGewicht As Windows.Forms.TextBox
    Friend WithEvents lblMin As Windows.Forms.Label
    Friend WithEvents lblLinieArtikel As Windows.Forms.Label
    Friend WithEvents cbArtikelLinienGruppe As wb_ComboBox
    Friend WithEvents lblLinienGruppe As Windows.Forms.Label
    Friend WithEvents cbLiniengruppe As wb_ComboBox
    Friend WithEvents BtnRzpShow As Windows.Forms.Button
    Friend WithEvents BtnRzpt As Windows.Forms.Button
    Friend WithEvents tRezeptName As Windows.Forms.TextBox
    Friend WithEvents lblRzeptBezeichnung As Windows.Forms.Label
    Friend WithEvents tRezeptNr As Windows.Forms.TextBox
    Friend WithEvents lblRezeptNr As Windows.Forms.Label
    Friend WithEvents tBackverlust As Windows.Forms.TextBox
    Friend WithEvents lblBackverlust As Windows.Forms.Label
    Friend WithEvents lblProdVorlauf As Windows.Forms.Label
    Friend WithEvents tProdVorlauf As Windows.Forms.TextBox
    Friend WithEvents BtnCloud As Windows.Forms.Button
    Friend WithEvents lblZuschnitt As Windows.Forms.Label
    Friend WithEvents tZuschnitt As Windows.Forms.TextBox
    Friend WithEvents tVkGewicht As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents BtnCalcNassGewicht As Windows.Forms.Button
    Friend WithEvents BtnUpdateNwt As Windows.Forms.Button
End Class
