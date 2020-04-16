<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_Planung_Neu
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.tArtikelNummer = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tArtikelName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tRezeptNummer = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tRezeptName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tGesMengeKg = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tGesMengeStk = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tMengeOptimal = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.tAnzOptimal = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.tMengeRest = New System.Windows.Forms.TextBox()
        Me.tAnzRest = New System.Windows.Forms.TextBox()
        Me.lblLinienGruppe = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblChargenResult = New System.Windows.Forms.Label()
        Me.cbChargenTeiler = New WinBack.wb_CheckedListBox()
        Me.cbVariante = New WinBack.wb_ComboBox()
        Me.cbLiniengruppe = New WinBack.wb_ComboBox()
        Me.pTeigChargen.SuspendLayout()
        Me.pArtikelChargen.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Location = New System.Drawing.Point(12, 429)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(104, 28)
        Me.BtnCancel.TabIndex = 3
        Me.BtnCancel.Text = "Abbruch"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnOK
        '
        Me.BtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnOK.Location = New System.Drawing.Point(463, 429)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(104, 28)
        Me.BtnOK.TabIndex = 2
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'tArtikelNummer
        '
        Me.tArtikelNummer.BackColor = System.Drawing.SystemColors.Window
        Me.tArtikelNummer.Location = New System.Drawing.Point(12, 22)
        Me.tArtikelNummer.Name = "tArtikelNummer"
        Me.tArtikelNummer.Size = New System.Drawing.Size(100, 20)
        Me.tArtikelNummer.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(13, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 70
        Me.Label3.Text = "Artikel-Nr."
        '
        'tArtikelName
        '
        Me.tArtikelName.Location = New System.Drawing.Point(118, 22)
        Me.tArtikelName.Name = "tArtikelName"
        Me.tArtikelName.Size = New System.Drawing.Size(293, 20)
        Me.tArtikelName.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(119, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 13)
        Me.Label2.TabIndex = 69
        Me.Label2.Text = "Artikel-Bezeichnung"
        '
        'tRezeptNummer
        '
        Me.tRezeptNummer.BackColor = System.Drawing.SystemColors.Window
        Me.tRezeptNummer.Location = New System.Drawing.Point(12, 60)
        Me.tRezeptNummer.Name = "tRezeptNummer"
        Me.tRezeptNummer.Size = New System.Drawing.Size(100, 20)
        Me.tRezeptNummer.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(13, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 74
        Me.Label1.Text = "Rezept-Nr."
        '
        'tRezeptName
        '
        Me.tRezeptName.Location = New System.Drawing.Point(118, 60)
        Me.tRezeptName.Name = "tRezeptName"
        Me.tRezeptName.Size = New System.Drawing.Size(293, 20)
        Me.tRezeptName.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(119, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(106, 13)
        Me.Label4.TabIndex = 73
        Me.Label4.Text = "Rezept-Bezeichnung"
        '
        'tGesMengeKg
        '
        Me.tGesMengeKg.BackColor = System.Drawing.SystemColors.Window
        Me.tGesMengeKg.Location = New System.Drawing.Point(446, 60)
        Me.tGesMengeKg.Name = "tGesMengeKg"
        Me.tGesMengeKg.Size = New System.Drawing.Size(100, 20)
        Me.tGesMengeKg.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(447, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 13)
        Me.Label5.TabIndex = 78
        Me.Label5.Text = "Gesamtmenge"
        '
        'tGesMengeStk
        '
        Me.tGesMengeStk.BackColor = System.Drawing.SystemColors.Window
        Me.tGesMengeStk.Location = New System.Drawing.Point(446, 22)
        Me.tGesMengeStk.Name = "tGesMengeStk"
        Me.tGesMengeStk.Size = New System.Drawing.Size(100, 20)
        Me.tGesMengeStk.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(447, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 13)
        Me.Label6.TabIndex = 77
        Me.Label6.Text = "Gesamt-Stück"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(546, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(23, 13)
        Me.Label7.TabIndex = 79
        Me.Label7.Text = "Stk"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(548, 63)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(19, 13)
        Me.Label8.TabIndex = 80
        Me.Label8.Text = "kg"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label9.Location = New System.Drawing.Point(548, 124)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(19, 13)
        Me.Label9.TabIndex = 87
        Me.Label9.Text = "kg"
        '
        'tMengeOptimal
        '
        Me.tMengeOptimal.BackColor = System.Drawing.SystemColors.Window
        Me.tMengeOptimal.Location = New System.Drawing.Point(446, 121)
        Me.tMengeOptimal.Name = "tMengeOptimal"
        Me.tMengeOptimal.Size = New System.Drawing.Size(100, 20)
        Me.tMengeOptimal.TabIndex = 83
        '
        'TextBox6
        '
        Me.TextBox6.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox6.Location = New System.Drawing.Point(12, 121)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(100, 20)
        Me.TextBox6.TabIndex = 81
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label11.Location = New System.Drawing.Point(13, 105)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(63, 13)
        Me.Label11.TabIndex = 85
        Me.Label11.Text = "Auftrags-Nr."
        '
        'tAnzOptimal
        '
        Me.tAnzOptimal.Location = New System.Drawing.Point(337, 121)
        Me.tAnzOptimal.Name = "tAnzOptimal"
        Me.tAnzOptimal.Size = New System.Drawing.Size(74, 20)
        Me.tAnzOptimal.TabIndex = 82
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label13.Location = New System.Drawing.Point(411, 124)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(23, 13)
        Me.Label13.TabIndex = 88
        Me.Label13.Text = "Stk"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label14.Location = New System.Drawing.Point(339, 105)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(81, 13)
        Me.Label14.TabIndex = 89
        Me.Label14.Text = "Optimalchargen"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label15.Location = New System.Drawing.Point(339, 146)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(68, 13)
        Me.Label15.TabIndex = 95
        Me.Label15.Text = "Restchargen"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label16.Location = New System.Drawing.Point(411, 165)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(23, 13)
        Me.Label16.TabIndex = 94
        Me.Label16.Text = "Stk"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label17.Location = New System.Drawing.Point(548, 165)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(19, 13)
        Me.Label17.TabIndex = 93
        Me.Label17.Text = "kg"
        '
        'tMengeRest
        '
        Me.tMengeRest.BackColor = System.Drawing.SystemColors.Window
        Me.tMengeRest.Location = New System.Drawing.Point(446, 162)
        Me.tMengeRest.Name = "tMengeRest"
        Me.tMengeRest.Size = New System.Drawing.Size(100, 20)
        Me.tMengeRest.TabIndex = 91
        '
        'tAnzRest
        '
        Me.tAnzRest.Location = New System.Drawing.Point(337, 162)
        Me.tAnzRest.Name = "tAnzRest"
        Me.tAnzRest.Size = New System.Drawing.Size(74, 20)
        Me.tAnzRest.TabIndex = 90
        '
        'lblLinienGruppe
        '
        Me.lblLinienGruppe.AutoSize = True
        Me.lblLinienGruppe.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblLinienGruppe.Location = New System.Drawing.Point(118, 105)
        Me.lblLinienGruppe.Name = "lblLinienGruppe"
        Me.lblLinienGruppe.Size = New System.Drawing.Size(122, 13)
        Me.lblLinienGruppe.TabIndex = 97
        Me.lblLinienGruppe.Text = "Liniengruppe Produktion"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label10.Location = New System.Drawing.Point(118, 145)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(79, 13)
        Me.Label10.TabIndex = 100
        Me.Label10.Text = "Rezeptvariante"
        '
        'lblTeigChargen
        '
        Me.lblTeigChargen.AutoSize = True
        Me.lblTeigChargen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTeigChargen.Location = New System.Drawing.Point(0, 0)
        Me.lblTeigChargen.Name = "lblTeigChargen"
        Me.lblTeigChargen.Size = New System.Drawing.Size(71, 13)
        Me.lblTeigChargen.TabIndex = 108
        Me.lblTeigChargen.Text = "Teig-Chargen"
        '
        'pTeigChargen
        '
        Me.pTeigChargen.BackColor = System.Drawing.Color.Transparent
        Me.pTeigChargen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pTeigChargen.Controls.Add(Me.lblTeigChargen)
        Me.pTeigChargen.Controls.Add(Me.tRezOptkg)
        Me.pTeigChargen.Controls.Add(Me.tRezOptPrz)
        Me.pTeigChargen.Controls.Add(Me.tRezMaxkg)
        Me.pTeigChargen.Controls.Add(Me.tRezMaxPrz)
        Me.pTeigChargen.Controls.Add(Me.tRezMinkg)
        Me.pTeigChargen.Controls.Add(Me.tRezMinPrz)
        Me.pTeigChargen.Controls.Add(Me.lblTeigGesamt)
        Me.pTeigChargen.Controls.Add(Me.tRezGesamt)
        Me.pTeigChargen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pTeigChargen.Location = New System.Drawing.Point(7, 218)
        Me.pTeigChargen.Name = "pTeigChargen"
        Me.pTeigChargen.Size = New System.Drawing.Size(151, 122)
        Me.pTeigChargen.TabIndex = 107
        '
        'tRezOptkg
        '
        Me.tRezOptkg.Location = New System.Drawing.Point(3, 93)
        Me.tRezOptkg.Name = "tRezOptkg"
        Me.tRezOptkg.ReadOnly = True
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
        Me.tRezOptPrz.ReadOnly = True
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
        Me.tRezMaxkg.ReadOnly = True
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
        Me.tRezMaxPrz.ReadOnly = True
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
        Me.tRezMinkg.ReadOnly = True
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
        Me.tRezMinPrz.ReadOnly = True
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
        Me.lblOpt.Location = New System.Drawing.Point(164, 315)
        Me.lblOpt.Name = "lblOpt"
        Me.lblOpt.Size = New System.Drawing.Size(51, 13)
        Me.lblOpt.TabIndex = 106
        Me.lblOpt.Text = "Optimal"
        Me.lblOpt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMax
        '
        Me.lblMax.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblMax.Location = New System.Drawing.Point(164, 289)
        Me.lblMax.Name = "lblMax"
        Me.lblMax.Size = New System.Drawing.Size(51, 13)
        Me.lblMax.TabIndex = 105
        Me.lblMax.Text = "Maximal"
        Me.lblMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblGewicht
        '
        Me.lblGewicht.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblGewicht.Location = New System.Drawing.Point(163, 237)
        Me.lblGewicht.Name = "lblGewicht"
        Me.lblGewicht.Size = New System.Drawing.Size(52, 13)
        Me.lblGewicht.TabIndex = 104
        Me.lblGewicht.Text = "Gewicht"
        Me.lblGewicht.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblArtikelChargen
        '
        Me.lblArtikelChargen.AutoSize = True
        Me.lblArtikelChargen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblArtikelChargen.Location = New System.Drawing.Point(9, 4)
        Me.lblArtikelChargen.Name = "lblArtikelChargen"
        Me.lblArtikelChargen.Size = New System.Drawing.Size(79, 13)
        Me.lblArtikelChargen.TabIndex = 103
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
        Me.pArtikelChargen.Controls.Add(Me.lblArtikelChargen)
        Me.pArtikelChargen.Controls.Add(Me.tChrgMaxkg)
        Me.pArtikelChargen.Controls.Add(Me.tChrgMaxPrz)
        Me.pArtikelChargen.Controls.Add(Me.tChrgMinStk)
        Me.pArtikelChargen.Controls.Add(Me.tChrgMinkg)
        Me.pArtikelChargen.Controls.Add(Me.tChrgMinPrz)
        Me.pArtikelChargen.Controls.Add(Me.lblProStk)
        Me.pArtikelChargen.Controls.Add(Me.tStkGewicht)
        Me.pArtikelChargen.Location = New System.Drawing.Point(221, 219)
        Me.pArtikelChargen.Name = "pArtikelChargen"
        Me.pArtikelChargen.Size = New System.Drawing.Size(176, 122)
        Me.pArtikelChargen.TabIndex = 102
        '
        'tChrgOptStk
        '
        Me.tChrgOptStk.Location = New System.Drawing.Point(116, 93)
        Me.tChrgOptStk.Name = "tChrgOptStk"
        Me.tChrgOptStk.ReadOnly = True
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
        Me.tChrgOptkg.ReadOnly = True
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
        Me.tChrgOptPrz.ReadOnly = True
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
        Me.tChrgMaxStk.ReadOnly = True
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
        Me.tChrgMaxkg.ReadOnly = True
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
        Me.tChrgMaxPrz.ReadOnly = True
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
        Me.tChrgMinStk.ReadOnly = True
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
        Me.tChrgMinkg.ReadOnly = True
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
        Me.tChrgMinPrz.ReadOnly = True
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
        Me.tStkGewicht.ReadOnly = True
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
        Me.lblMin.Location = New System.Drawing.Point(163, 263)
        Me.lblMin.Name = "lblMin"
        Me.lblMin.Size = New System.Drawing.Size(52, 13)
        Me.lblMin.TabIndex = 101
        Me.lblMin.Text = "Minimal"
        Me.lblMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.cbChargenTeiler)
        Me.Panel1.Location = New System.Drawing.Point(403, 219)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(167, 122)
        Me.Panel1.TabIndex = 109
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label12.Location = New System.Drawing.Point(3, 5)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(129, 13)
        Me.Label12.TabIndex = 110
        Me.Label12.Text = "wenn möglich aufteilen in:"
        '
        'lblChargenResult
        '
        Me.lblChargenResult.Location = New System.Drawing.Point(13, 359)
        Me.lblChargenResult.Name = "lblChargenResult"
        Me.lblChargenResult.Size = New System.Drawing.Size(384, 45)
        Me.lblChargenResult.TabIndex = 110
        Me.lblChargenResult.Text = "Ergebnis der Chargen-Aufteilung"
        '
        'cbChargenTeiler
        '
        Me.cbChargenTeiler.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbChargenTeiler.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.cbChargenTeiler.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.cbChargenTeiler.CheckOnClick = True
        Me.cbChargenTeiler.FormattingEnabled = True
        Me.cbChargenTeiler.Location = New System.Drawing.Point(6, 32)
        Me.cbChargenTeiler.Name = "cbChargenTeiler"
        Me.cbChargenTeiler.SelIndex = 0
        Me.cbChargenTeiler.Size = New System.Drawing.Size(162, 90)
        Me.cbChargenTeiler.TabIndex = 109
        Me.cbChargenTeiler.TabStop = False
        '
        'cbVariante
        '
        Me.cbVariante.FormattingEnabled = True
        Me.cbVariante.Location = New System.Drawing.Point(118, 162)
        Me.cbVariante.Name = "cbVariante"
        Me.cbVariante.Size = New System.Drawing.Size(197, 21)
        Me.cbVariante.TabIndex = 99
        Me.cbVariante.TabStop = False
        Me.cbVariante.Text = "RV"
        '
        'cbLiniengruppe
        '
        Me.cbLiniengruppe.FormattingEnabled = True
        Me.cbLiniengruppe.Location = New System.Drawing.Point(118, 121)
        Me.cbLiniengruppe.Name = "cbLiniengruppe"
        Me.cbLiniengruppe.Size = New System.Drawing.Size(197, 21)
        Me.cbLiniengruppe.TabIndex = 96
        Me.cbLiniengruppe.TabStop = False
        Me.cbLiniengruppe.Text = "LG"
        '
        'wb_Planung_Neu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(579, 469)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblChargenResult)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pTeigChargen)
        Me.Controls.Add(Me.lblOpt)
        Me.Controls.Add(Me.lblMax)
        Me.Controls.Add(Me.lblGewicht)
        Me.Controls.Add(Me.pArtikelChargen)
        Me.Controls.Add(Me.lblMin)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cbVariante)
        Me.Controls.Add(Me.lblLinienGruppe)
        Me.Controls.Add(Me.cbLiniengruppe)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.tMengeRest)
        Me.Controls.Add(Me.tAnzRest)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.tMengeOptimal)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.tAnzOptimal)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.tGesMengeKg)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tGesMengeStk)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.tRezeptNummer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tRezeptName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tArtikelNummer)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tArtikelName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wb_Planung_Neu"
        Me.Text = "Chargen anlegen/ändern"
        Me.pTeigChargen.ResumeLayout(False)
        Me.pTeigChargen.PerformLayout()
        Me.pArtikelChargen.ResumeLayout(False)
        Me.pArtikelChargen.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnCancel As Windows.Forms.Button
    Friend WithEvents BtnOK As Windows.Forms.Button
    Friend WithEvents tArtikelNummer As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents tArtikelName As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tRezeptNummer As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents tRezeptName As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents tGesMengeKg As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents tGesMengeStk As Windows.Forms.TextBox
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents tMengeOptimal As Windows.Forms.TextBox
    Friend WithEvents TextBox6 As Windows.Forms.TextBox
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents tAnzOptimal As Windows.Forms.TextBox
    Friend WithEvents Label13 As Windows.Forms.Label
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents Label15 As Windows.Forms.Label
    Friend WithEvents Label16 As Windows.Forms.Label
    Friend WithEvents Label17 As Windows.Forms.Label
    Friend WithEvents tMengeRest As Windows.Forms.TextBox
    Friend WithEvents tAnzRest As Windows.Forms.TextBox
    Friend WithEvents lblLinienGruppe As Windows.Forms.Label
    Friend WithEvents cbLiniengruppe As wb_ComboBox
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents cbVariante As wb_ComboBox
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
    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents cbChargenTeiler As wb_CheckedListBox
    Friend WithEvents lblChargenResult As Windows.Forms.Label
End Class
