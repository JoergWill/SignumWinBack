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
        lblTeigChargen = New System.Windows.Forms.Label()
        pTeigChargen = New System.Windows.Forms.Panel()
        tRezOptkg = New System.Windows.Forms.TextBox()
        tRezOptPrz = New System.Windows.Forms.TextBox()
        tRezMaxkg = New System.Windows.Forms.TextBox()
        tRezMaxPrz = New System.Windows.Forms.TextBox()
        tRezMinkg = New System.Windows.Forms.TextBox()
        tRezMinPrz = New System.Windows.Forms.TextBox()
        lblTeigGesamt = New System.Windows.Forms.Label()
        tRezGesamt = New System.Windows.Forms.TextBox()
        lblOpt = New System.Windows.Forms.Label()
        lblMax = New System.Windows.Forms.Label()
        lblGewicht = New System.Windows.Forms.Label()
        lblArtikelChargen = New System.Windows.Forms.Label()
        pArtikelChargen = New System.Windows.Forms.Panel()
        tChrgOptStk = New System.Windows.Forms.TextBox()
        tChrgOptkg = New System.Windows.Forms.TextBox()
        tChrgOptPrz = New System.Windows.Forms.TextBox()
        tChrgMaxStk = New System.Windows.Forms.TextBox()
        tChrgMaxkg = New System.Windows.Forms.TextBox()
        tChrgMaxPrz = New System.Windows.Forms.TextBox()
        tChrgMinStk = New System.Windows.Forms.TextBox()
        tChrgMinkg = New System.Windows.Forms.TextBox()
        tChrgMinPrz = New System.Windows.Forms.TextBox()
        lblProStk = New System.Windows.Forms.Label()
        tStkGewicht = New System.Windows.Forms.TextBox()
        lblMin = New System.Windows.Forms.Label()
        lblLinieArtikel = New System.Windows.Forms.Label()
        lblLinienGruppe = New System.Windows.Forms.Label()
        BtnRzpShow = New System.Windows.Forms.Button()
        BtnRzpt = New System.Windows.Forms.Button()
        tRezeptName = New System.Windows.Forms.TextBox()
        lblRzeptBezeichnung = New System.Windows.Forms.Label()
        tRezeptNr = New System.Windows.Forms.TextBox()
        lblRezeptNr = New System.Windows.Forms.Label()
        tBackverlust = New System.Windows.Forms.TextBox()
        lblBackverlust = New System.Windows.Forms.Label()
        lblProdVorlauf = New System.Windows.Forms.Label()
        tProdVorlauf = New System.Windows.Forms.TextBox()
        BtnCloud = New System.Windows.Forms.Button()
        lblZuschnitt = New System.Windows.Forms.Label()
        tZuschnitt = New System.Windows.Forms.TextBox()
        tVkGewicht = New System.Windows.Forms.TextBox()
        Label1 = New System.Windows.Forms.Label()
        BtnCalcNassGewicht = New System.Windows.Forms.Button()
        BtnUpdateNwt = New System.Windows.Forms.Button()
        cbArtikelLinienGruppe = New wb_ComboBox()
        cbLiniengruppe = New wb_ComboBox()
        pTeigChargen.SuspendLayout()
        pArtikelChargen.SuspendLayout()
        SuspendLayout()
        ' 
        ' lblTeigChargen
        ' 
        lblTeigChargen.AutoSize = True
        lblTeigChargen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblTeigChargen.Location = New System.Drawing.Point(14, 142)
        lblTeigChargen.Name = "lblTeigChargen"
        lblTeigChargen.Size = New System.Drawing.Size(71, 13)
        lblTeigChargen.TabIndex = 65
        lblTeigChargen.Text = "Teig-Chargen"
        ' 
        ' pTeigChargen
        ' 
        pTeigChargen.BackColor = Drawing.Color.Transparent
        pTeigChargen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        pTeigChargen.Controls.Add(tRezOptkg)
        pTeigChargen.Controls.Add(tRezOptPrz)
        pTeigChargen.Controls.Add(tRezMaxkg)
        pTeigChargen.Controls.Add(tRezMaxPrz)
        pTeigChargen.Controls.Add(tRezMinkg)
        pTeigChargen.Controls.Add(tRezMinPrz)
        pTeigChargen.Controls.Add(lblTeigGesamt)
        pTeigChargen.Controls.Add(tRezGesamt)
        pTeigChargen.ForeColor = Drawing.SystemColors.ControlText
        pTeigChargen.Location = New System.Drawing.Point(8, 141)
        pTeigChargen.Name = "pTeigChargen"
        pTeigChargen.Size = New System.Drawing.Size(151, 122)
        pTeigChargen.TabIndex = 64
        ' 
        ' tRezOptkg
        ' 
        tRezOptkg.Location = New System.Drawing.Point(3, 93)
        tRezOptkg.Name = "tRezOptkg"
        tRezOptkg.Size = New System.Drawing.Size(67, 20)
        tRezOptkg.TabIndex = 43
        tRezOptkg.TabStop = False
        tRezOptkg.Text = "2000,000 kg"
        tRezOptkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tRezOptkg.WordWrap = False
        ' 
        ' tRezOptPrz
        ' 
        tRezOptPrz.Location = New System.Drawing.Point(77, 93)
        tRezOptPrz.Name = "tRezOptPrz"
        tRezOptPrz.Size = New System.Drawing.Size(50, 20)
        tRezOptPrz.TabIndex = 44
        tRezOptPrz.TabStop = False
        tRezOptPrz.Text = "999%"
        tRezOptPrz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tRezOptPrz.WordWrap = False
        ' 
        ' tRezMaxkg
        ' 
        tRezMaxkg.Location = New System.Drawing.Point(3, 67)
        tRezMaxkg.Name = "tRezMaxkg"
        tRezMaxkg.Size = New System.Drawing.Size(67, 20)
        tRezMaxkg.TabIndex = 40
        tRezMaxkg.TabStop = False
        tRezMaxkg.Text = "2000,000 kg"
        tRezMaxkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tRezMaxkg.WordWrap = False
        ' 
        ' tRezMaxPrz
        ' 
        tRezMaxPrz.Location = New System.Drawing.Point(77, 67)
        tRezMaxPrz.Name = "tRezMaxPrz"
        tRezMaxPrz.Size = New System.Drawing.Size(50, 20)
        tRezMaxPrz.TabIndex = 41
        tRezMaxPrz.TabStop = False
        tRezMaxPrz.Text = "999%"
        tRezMaxPrz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tRezMaxPrz.WordWrap = False
        ' 
        ' tRezMinkg
        ' 
        tRezMinkg.Location = New System.Drawing.Point(3, 41)
        tRezMinkg.Name = "tRezMinkg"
        tRezMinkg.Size = New System.Drawing.Size(67, 20)
        tRezMinkg.TabIndex = 36
        tRezMinkg.TabStop = False
        tRezMinkg.Text = "2000,000 kg"
        tRezMinkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tRezMinkg.WordWrap = False
        ' 
        ' tRezMinPrz
        ' 
        tRezMinPrz.Location = New System.Drawing.Point(77, 41)
        tRezMinPrz.Name = "tRezMinPrz"
        tRezMinPrz.Size = New System.Drawing.Size(50, 20)
        tRezMinPrz.TabIndex = 38
        tRezMinPrz.TabStop = False
        tRezMinPrz.Text = "999%"
        tRezMinPrz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tRezMinPrz.WordWrap = False
        ' 
        ' lblTeigGesamt
        ' 
        lblTeigGesamt.AutoSize = True
        lblTeigGesamt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblTeigGesamt.Location = New System.Drawing.Point(71, 18)
        lblTeigGesamt.Name = "lblTeigGesamt"
        lblTeigGesamt.Size = New System.Drawing.Size(73, 13)
        lblTeigGesamt.TabIndex = 35
        lblTeigGesamt.Text = "Teig (Gesamt)"
        ' 
        ' tRezGesamt
        ' 
        tRezGesamt.Location = New System.Drawing.Point(3, 15)
        tRezGesamt.Name = "tRezGesamt"
        tRezGesamt.ReadOnly = True
        tRezGesamt.Size = New System.Drawing.Size(67, 20)
        tRezGesamt.TabIndex = 34
        tRezGesamt.TabStop = False
        tRezGesamt.Text = "245,999 kg"
        tRezGesamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tRezGesamt.WordWrap = False
        ' 
        ' lblOpt
        ' 
        lblOpt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblOpt.Location = New System.Drawing.Point(162, 239)
        lblOpt.Name = "lblOpt"
        lblOpt.Size = New System.Drawing.Size(51, 13)
        lblOpt.TabIndex = 63
        lblOpt.Text = "Optimal"
        lblOpt.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' lblMax
        ' 
        lblMax.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblMax.Location = New System.Drawing.Point(162, 213)
        lblMax.Name = "lblMax"
        lblMax.Size = New System.Drawing.Size(51, 13)
        lblMax.TabIndex = 62
        lblMax.Text = "Maximal"
        lblMax.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' lblGewicht
        ' 
        lblGewicht.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblGewicht.Location = New System.Drawing.Point(161, 161)
        lblGewicht.Name = "lblGewicht"
        lblGewicht.Size = New System.Drawing.Size(52, 13)
        lblGewicht.TabIndex = 61
        lblGewicht.Text = "Gewicht"
        lblGewicht.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' lblArtikelChargen
        ' 
        lblArtikelChargen.AutoSize = True
        lblArtikelChargen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblArtikelChargen.Location = New System.Drawing.Point(222, 143)
        lblArtikelChargen.Name = "lblArtikelChargen"
        lblArtikelChargen.Size = New System.Drawing.Size(79, 13)
        lblArtikelChargen.TabIndex = 60
        lblArtikelChargen.Text = "Artikel-Chargen"
        ' 
        ' pArtikelChargen
        ' 
        pArtikelChargen.BackColor = Drawing.Color.Transparent
        pArtikelChargen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        pArtikelChargen.Controls.Add(tChrgOptStk)
        pArtikelChargen.Controls.Add(tChrgOptkg)
        pArtikelChargen.Controls.Add(tChrgOptPrz)
        pArtikelChargen.Controls.Add(tChrgMaxStk)
        pArtikelChargen.Controls.Add(tChrgMaxkg)
        pArtikelChargen.Controls.Add(tChrgMaxPrz)
        pArtikelChargen.Controls.Add(tChrgMinStk)
        pArtikelChargen.Controls.Add(tChrgMinkg)
        pArtikelChargen.Controls.Add(tChrgMinPrz)
        pArtikelChargen.Controls.Add(lblProStk)
        pArtikelChargen.Controls.Add(tStkGewicht)
        pArtikelChargen.Location = New System.Drawing.Point(215, 141)
        pArtikelChargen.Name = "pArtikelChargen"
        pArtikelChargen.Size = New System.Drawing.Size(176, 122)
        pArtikelChargen.TabIndex = 59
        ' 
        ' tChrgOptStk
        ' 
        tChrgOptStk.Location = New System.Drawing.Point(116, 93)
        tChrgOptStk.Name = "tChrgOptStk"
        tChrgOptStk.Size = New System.Drawing.Size(54, 20)
        tChrgOptStk.TabIndex = 45
        tChrgOptStk.TabStop = False
        tChrgOptStk.Text = "14000 Stk"
        tChrgOptStk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tChrgOptStk.WordWrap = False
        ' 
        ' tChrgOptkg
        ' 
        tChrgOptkg.Location = New System.Drawing.Point(3, 93)
        tChrgOptkg.Name = "tChrgOptkg"
        tChrgOptkg.Size = New System.Drawing.Size(67, 20)
        tChrgOptkg.TabIndex = 43
        tChrgOptkg.TabStop = False
        tChrgOptkg.Text = "2000,000 kg"
        tChrgOptkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tChrgOptkg.WordWrap = False
        ' 
        ' tChrgOptPrz
        ' 
        tChrgOptPrz.Location = New System.Drawing.Point(73, 93)
        tChrgOptPrz.Name = "tChrgOptPrz"
        tChrgOptPrz.Size = New System.Drawing.Size(40, 20)
        tChrgOptPrz.TabIndex = 44
        tChrgOptPrz.TabStop = False
        tChrgOptPrz.Text = "999%"
        tChrgOptPrz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tChrgOptPrz.WordWrap = False
        ' 
        ' tChrgMaxStk
        ' 
        tChrgMaxStk.Location = New System.Drawing.Point(116, 67)
        tChrgMaxStk.Name = "tChrgMaxStk"
        tChrgMaxStk.Size = New System.Drawing.Size(54, 20)
        tChrgMaxStk.TabIndex = 42
        tChrgMaxStk.TabStop = False
        tChrgMaxStk.Text = "14000 Stk"
        tChrgMaxStk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tChrgMaxStk.WordWrap = False
        ' 
        ' tChrgMaxkg
        ' 
        tChrgMaxkg.Location = New System.Drawing.Point(3, 67)
        tChrgMaxkg.Name = "tChrgMaxkg"
        tChrgMaxkg.Size = New System.Drawing.Size(67, 20)
        tChrgMaxkg.TabIndex = 40
        tChrgMaxkg.TabStop = False
        tChrgMaxkg.Text = "2000,000 kg"
        tChrgMaxkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tChrgMaxkg.WordWrap = False
        ' 
        ' tChrgMaxPrz
        ' 
        tChrgMaxPrz.Location = New System.Drawing.Point(73, 67)
        tChrgMaxPrz.Name = "tChrgMaxPrz"
        tChrgMaxPrz.Size = New System.Drawing.Size(40, 20)
        tChrgMaxPrz.TabIndex = 41
        tChrgMaxPrz.TabStop = False
        tChrgMaxPrz.Text = "999%"
        tChrgMaxPrz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tChrgMaxPrz.WordWrap = False
        ' 
        ' tChrgMinStk
        ' 
        tChrgMinStk.Location = New System.Drawing.Point(116, 41)
        tChrgMinStk.Name = "tChrgMinStk"
        tChrgMinStk.Size = New System.Drawing.Size(54, 20)
        tChrgMinStk.TabIndex = 39
        tChrgMinStk.TabStop = False
        tChrgMinStk.Text = "14000 Stk"
        tChrgMinStk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tChrgMinStk.WordWrap = False
        ' 
        ' tChrgMinkg
        ' 
        tChrgMinkg.Location = New System.Drawing.Point(3, 41)
        tChrgMinkg.Name = "tChrgMinkg"
        tChrgMinkg.Size = New System.Drawing.Size(67, 20)
        tChrgMinkg.TabIndex = 36
        tChrgMinkg.TabStop = False
        tChrgMinkg.Text = "2000,000 kg"
        tChrgMinkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tChrgMinkg.WordWrap = False
        ' 
        ' tChrgMinPrz
        ' 
        tChrgMinPrz.Location = New System.Drawing.Point(73, 41)
        tChrgMinPrz.Name = "tChrgMinPrz"
        tChrgMinPrz.Size = New System.Drawing.Size(40, 20)
        tChrgMinPrz.TabIndex = 38
        tChrgMinPrz.TabStop = False
        tChrgMinPrz.Text = "999%"
        tChrgMinPrz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tChrgMinPrz.WordWrap = False
        ' 
        ' lblProStk
        ' 
        lblProStk.AutoSize = True
        lblProStk.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblProStk.Location = New System.Drawing.Point(6, 17)
        lblProStk.Name = "lblProStk"
        lblProStk.Size = New System.Drawing.Size(84, 13)
        lblProStk.TabIndex = 35
        lblProStk.Text = "pro Stück (nass)"
        ' 
        ' tStkGewicht
        ' 
        tStkGewicht.Location = New System.Drawing.Point(90, 15)
        tStkGewicht.Name = "tStkGewicht"
        tStkGewicht.Size = New System.Drawing.Size(80, 20)
        tStkGewicht.TabIndex = 34
        tStkGewicht.TabStop = False
        tStkGewicht.Text = "1567 gr "
        tStkGewicht.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tStkGewicht.WordWrap = False
        ' 
        ' lblMin
        ' 
        lblMin.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblMin.Location = New System.Drawing.Point(161, 187)
        lblMin.Name = "lblMin"
        lblMin.Size = New System.Drawing.Size(52, 13)
        lblMin.TabIndex = 58
        lblMin.Text = "Minimal"
        lblMin.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' lblLinieArtikel
        ' 
        lblLinieArtikel.AutoSize = True
        lblLinieArtikel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblLinieArtikel.Location = New System.Drawing.Point(222, 91)
        lblLinieArtikel.Name = "lblLinieArtikel"
        lblLinieArtikel.Size = New System.Drawing.Size(131, 13)
        lblLinieArtikel.TabIndex = 57
        lblLinieArtikel.Text = "Liniengruppe Aufarbeitung"
        ' 
        ' lblLinienGruppe
        ' 
        lblLinienGruppe.AutoSize = True
        lblLinienGruppe.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblLinienGruppe.Location = New System.Drawing.Point(7, 91)
        lblLinienGruppe.Name = "lblLinienGruppe"
        lblLinienGruppe.Size = New System.Drawing.Size(92, 13)
        lblLinienGruppe.TabIndex = 55
        lblLinienGruppe.Text = "Liniengruppe Teig"
        ' 
        ' BtnRzpShow
        ' 
        BtnRzpShow.Location = New System.Drawing.Point(198, 14)
        BtnRzpShow.Name = "BtnRzpShow"
        BtnRzpShow.Size = New System.Drawing.Size(85, 40)
        BtnRzpShow.TabIndex = 2
        BtnRzpShow.Text = "Öffnen"
        BtnRzpShow.UseVisualStyleBackColor = True
        ' 
        ' BtnRzpt
        ' 
        BtnRzpt.Location = New System.Drawing.Point(107, 14)
        BtnRzpt.Name = "BtnRzpt"
        BtnRzpt.Size = New System.Drawing.Size(85, 40)
        BtnRzpt.TabIndex = 1
        BtnRzpt.TabStop = False
        BtnRzpt.Text = "Auswählen"
        BtnRzpt.UseVisualStyleBackColor = True
        ' 
        ' tRezeptName
        ' 
        tRezeptName.Location = New System.Drawing.Point(7, 68)
        tRezeptName.Name = "tRezeptName"
        tRezeptName.ReadOnly = True
        tRezeptName.Size = New System.Drawing.Size(384, 20)
        tRezeptName.TabIndex = 4
        tRezeptName.TabStop = False
        ' 
        ' lblRzeptBezeichnung
        ' 
        lblRzeptBezeichnung.AutoSize = True
        lblRzeptBezeichnung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzeptBezeichnung.Location = New System.Drawing.Point(7, 52)
        lblRzeptBezeichnung.Name = "lblRzeptBezeichnung"
        lblRzeptBezeichnung.Size = New System.Drawing.Size(106, 13)
        lblRzeptBezeichnung.TabIndex = 51
        lblRzeptBezeichnung.Text = "Rezept-Bezeichnung"
        ' 
        ' tRezeptNr
        ' 
        tRezeptNr.Location = New System.Drawing.Point(7, 25)
        tRezeptNr.Name = "tRezeptNr"
        tRezeptNr.ReadOnly = True
        tRezeptNr.Size = New System.Drawing.Size(72, 20)
        tRezeptNr.TabIndex = 50
        tRezeptNr.TabStop = False
        ' 
        ' lblRezeptNr
        ' 
        lblRezeptNr.AutoSize = True
        lblRezeptNr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRezeptNr.Location = New System.Drawing.Point(7, 9)
        lblRezeptNr.Name = "lblRezeptNr"
        lblRezeptNr.Size = New System.Drawing.Size(58, 13)
        lblRezeptNr.TabIndex = 49
        lblRezeptNr.Text = "Rezept-Nr."
        ' 
        ' tBackverlust
        ' 
        tBackverlust.Location = New System.Drawing.Point(219, 279)
        tBackverlust.Name = "tBackverlust"
        tBackverlust.Size = New System.Drawing.Size(66, 20)
        tBackverlust.TabIndex = 66
        tBackverlust.TabStop = False
        tBackverlust.Text = "99.999 %"
        tBackverlust.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tBackverlust.WordWrap = False
        ' 
        ' lblBackverlust
        ' 
        lblBackverlust.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblBackverlust.Location = New System.Drawing.Point(153, 279)
        lblBackverlust.Name = "lblBackverlust"
        lblBackverlust.Size = New System.Drawing.Size(64, 19)
        lblBackverlust.TabIndex = 67
        lblBackverlust.Text = "Backverlust"
        lblBackverlust.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' lblProdVorlauf
        ' 
        lblProdVorlauf.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblProdVorlauf.Location = New System.Drawing.Point(5, 274)
        lblProdVorlauf.Name = "lblProdVorlauf"
        lblProdVorlauf.Size = New System.Drawing.Size(75, 27)
        lblProdVorlauf.TabIndex = 68
        lblProdVorlauf.Text = "Vorlauf Produktion"
        lblProdVorlauf.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' tProdVorlauf
        ' 
        tProdVorlauf.Location = New System.Drawing.Point(86, 279)
        tProdVorlauf.Name = "tProdVorlauf"
        tProdVorlauf.Size = New System.Drawing.Size(50, 20)
        tProdVorlauf.TabIndex = 69
        tProdVorlauf.TabStop = False
        tProdVorlauf.Text = "999 h"
        tProdVorlauf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tProdVorlauf.WordWrap = False
        ' 
        ' BtnCloud
        ' 
        BtnCloud.Location = New System.Drawing.Point(313, 14)
        BtnCloud.Name = "BtnCloud"
        BtnCloud.Size = New System.Drawing.Size(78, 40)
        BtnCloud.TabIndex = 3
        BtnCloud.Text = "Cloud"
        BtnCloud.UseVisualStyleBackColor = True
        ' 
        ' lblZuschnitt
        ' 
        lblZuschnitt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblZuschnitt.Location = New System.Drawing.Point(153, 305)
        lblZuschnitt.Name = "lblZuschnitt"
        lblZuschnitt.Size = New System.Drawing.Size(64, 19)
        lblZuschnitt.TabIndex = 71
        lblZuschnitt.Text = "Zuschnitt"
        lblZuschnitt.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' tZuschnitt
        ' 
        tZuschnitt.Location = New System.Drawing.Point(219, 305)
        tZuschnitt.Name = "tZuschnitt"
        tZuschnitt.Size = New System.Drawing.Size(66, 20)
        tZuschnitt.TabIndex = 70
        tZuschnitt.TabStop = False
        tZuschnitt.Text = "99.999 %"
        tZuschnitt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tZuschnitt.WordWrap = False
        ' 
        ' tVkGewicht
        ' 
        tVkGewicht.Location = New System.Drawing.Point(86, 304)
        tVkGewicht.Name = "tVkGewicht"
        tVkGewicht.ReadOnly = True
        tVkGewicht.Size = New System.Drawing.Size(50, 20)
        tVkGewicht.TabIndex = 72
        tVkGewicht.TabStop = False
        tVkGewicht.Text = "240 gr"
        tVkGewicht.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tVkGewicht.WordWrap = False
        ' 
        ' Label1
        ' 
        Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label1.Location = New System.Drawing.Point(5, 303)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(74, 27)
        Label1.TabIndex = 73
        Label1.Text = "Verkaufs- Gewicht"
        Label1.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' BtnCalcNassGewicht
        ' 
        BtnCalcNassGewicht.Location = New System.Drawing.Point(291, 283)
        BtnCalcNassGewicht.Name = "BtnCalcNassGewicht"
        BtnCalcNassGewicht.Size = New System.Drawing.Size(100, 40)
        BtnCalcNassGewicht.TabIndex = 74
        BtnCalcNassGewicht.Text = "Nass-Gewicht berechnen"
        BtnCalcNassGewicht.UseVisualStyleBackColor = True
        ' 
        ' BtnUpdateNwt
        ' 
        BtnUpdateNwt.Location = New System.Drawing.Point(313, 38)
        BtnUpdateNwt.Name = "BtnUpdateNwt"
        BtnUpdateNwt.Size = New System.Drawing.Size(78, 40)
        BtnUpdateNwt.TabIndex = 75
        BtnUpdateNwt.Text = "Update Nährwerte"
        BtnUpdateNwt.UseVisualStyleBackColor = True
        ' 
        ' cbArtikelLinienGruppe
        ' 
        cbArtikelLinienGruppe.FormattingEnabled = True
        cbArtikelLinienGruppe.Location = New System.Drawing.Point(219, 107)
        cbArtikelLinienGruppe.Name = "cbArtikelLinienGruppe"
        cbArtikelLinienGruppe.Size = New System.Drawing.Size(172, 21)
        cbArtikelLinienGruppe.TabIndex = 6
        cbArtikelLinienGruppe.TabStop = False
        ' 
        ' cbLiniengruppe
        ' 
        cbLiniengruppe.FormattingEnabled = True
        cbLiniengruppe.Location = New System.Drawing.Point(7, 107)
        cbLiniengruppe.Name = "cbLiniengruppe"
        cbLiniengruppe.Size = New System.Drawing.Size(176, 21)
        cbLiniengruppe.TabIndex = 5
        cbLiniengruppe.TabStop = False
        cbLiniengruppe.Text = "LG"
        ' 
        ' wb_KompRzChargen
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Controls.Add(BtnUpdateNwt)
        Controls.Add(BtnCalcNassGewicht)
        Controls.Add(Label1)
        Controls.Add(tVkGewicht)
        Controls.Add(lblZuschnitt)
        Controls.Add(tZuschnitt)
        Controls.Add(BtnCloud)
        Controls.Add(tProdVorlauf)
        Controls.Add(lblProdVorlauf)
        Controls.Add(lblBackverlust)
        Controls.Add(tBackverlust)
        Controls.Add(lblTeigChargen)
        Controls.Add(pTeigChargen)
        Controls.Add(lblOpt)
        Controls.Add(lblMax)
        Controls.Add(lblGewicht)
        Controls.Add(lblArtikelChargen)
        Controls.Add(pArtikelChargen)
        Controls.Add(lblMin)
        Controls.Add(cbArtikelLinienGruppe)
        Controls.Add(cbLiniengruppe)
        Controls.Add(BtnRzpShow)
        Controls.Add(BtnRzpt)
        Controls.Add(tRezeptName)
        Controls.Add(lblRzeptBezeichnung)
        Controls.Add(tRezeptNr)
        Controls.Add(lblRezeptNr)
        Controls.Add(lblLinienGruppe)
        Controls.Add(lblLinieArtikel)
        Name = "wb_KompRzChargen"
        Size = New System.Drawing.Size(400, 336)
        pTeigChargen.ResumeLayout(False)
        pTeigChargen.PerformLayout()
        pArtikelChargen.ResumeLayout(False)
        pArtikelChargen.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents lblTeigChargen As System.Windows.Forms.Label
    Friend WithEvents pTeigChargen As System.Windows.Forms.Panel
    Friend WithEvents tRezOptkg As System.Windows.Forms.TextBox
    Friend WithEvents tRezOptPrz As System.Windows.Forms.TextBox
    Friend WithEvents tRezMaxkg As System.Windows.Forms.TextBox
    Friend WithEvents tRezMaxPrz As System.Windows.Forms.TextBox
    Friend WithEvents tRezMinkg As System.Windows.Forms.TextBox
    Friend WithEvents tRezMinPrz As System.Windows.Forms.TextBox
    Friend WithEvents lblTeigGesamt As System.Windows.Forms.Label
    Friend WithEvents tRezGesamt As System.Windows.Forms.TextBox
    Friend WithEvents lblOpt As System.Windows.Forms.Label
    Friend WithEvents lblMax As System.Windows.Forms.Label
    Friend WithEvents lblGewicht As System.Windows.Forms.Label
    Friend WithEvents lblArtikelChargen As System.Windows.Forms.Label
    Friend WithEvents pArtikelChargen As System.Windows.Forms.Panel
    Friend WithEvents tChrgOptStk As System.Windows.Forms.TextBox
    Friend WithEvents tChrgOptkg As System.Windows.Forms.TextBox
    Friend WithEvents tChrgOptPrz As System.Windows.Forms.TextBox
    Friend WithEvents tChrgMaxStk As System.Windows.Forms.TextBox
    Friend WithEvents tChrgMaxkg As System.Windows.Forms.TextBox
    Friend WithEvents tChrgMaxPrz As System.Windows.Forms.TextBox
    Friend WithEvents tChrgMinStk As System.Windows.Forms.TextBox
    Friend WithEvents tChrgMinkg As System.Windows.Forms.TextBox
    Friend WithEvents tChrgMinPrz As System.Windows.Forms.TextBox
    Friend WithEvents lblProStk As System.Windows.Forms.Label
    Friend WithEvents tStkGewicht As System.Windows.Forms.TextBox
    Friend WithEvents lblMin As System.Windows.Forms.Label
    Friend WithEvents lblLinieArtikel As System.Windows.Forms.Label
    Friend WithEvents cbArtikelLinienGruppe As wb_ComboBox
    Friend WithEvents lblLinienGruppe As System.Windows.Forms.Label
    Friend WithEvents cbLiniengruppe As wb_ComboBox
    Friend WithEvents BtnRzpShow As System.Windows.Forms.Button
    Friend WithEvents BtnRzpt As System.Windows.Forms.Button
    Friend WithEvents tRezeptName As System.Windows.Forms.TextBox
    Friend WithEvents lblRzeptBezeichnung As System.Windows.Forms.Label
    Friend WithEvents tRezeptNr As System.Windows.Forms.TextBox
    Friend WithEvents lblRezeptNr As System.Windows.Forms.Label
    Friend WithEvents tBackverlust As System.Windows.Forms.TextBox
    Friend WithEvents lblBackverlust As System.Windows.Forms.Label
    Friend WithEvents lblProdVorlauf As System.Windows.Forms.Label
    Friend WithEvents tProdVorlauf As System.Windows.Forms.TextBox
    Friend WithEvents BtnCloud As System.Windows.Forms.Button
    Friend WithEvents lblZuschnitt As System.Windows.Forms.Label
    Friend WithEvents tZuschnitt As System.Windows.Forms.TextBox
    Friend WithEvents tVkGewicht As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnCalcNassGewicht As System.Windows.Forms.Button
    Friend WithEvents BtnUpdateNwt As System.Windows.Forms.Button
End Class
