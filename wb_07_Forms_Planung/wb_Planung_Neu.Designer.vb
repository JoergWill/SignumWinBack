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
        BtnCancel = New System.Windows.Forms.Button()
        BtnOK = New System.Windows.Forms.Button()
        tArtikelNummer = New System.Windows.Forms.TextBox()
        Label3 = New System.Windows.Forms.Label()
        tArtikelName = New System.Windows.Forms.TextBox()
        Label2 = New System.Windows.Forms.Label()
        tRezeptNummer = New System.Windows.Forms.TextBox()
        Label1 = New System.Windows.Forms.Label()
        tRezeptName = New System.Windows.Forms.TextBox()
        Label4 = New System.Windows.Forms.Label()
        tGesMengeKg = New System.Windows.Forms.TextBox()
        Label5 = New System.Windows.Forms.Label()
        tGesMengeStk = New System.Windows.Forms.TextBox()
        Label6 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        tMengeOptimal = New System.Windows.Forms.TextBox()
        TextBox6 = New System.Windows.Forms.TextBox()
        Label11 = New System.Windows.Forms.Label()
        tAnzOptimal = New System.Windows.Forms.TextBox()
        Label13 = New System.Windows.Forms.Label()
        Label14 = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        Label16 = New System.Windows.Forms.Label()
        Label17 = New System.Windows.Forms.Label()
        tMengeRest = New System.Windows.Forms.TextBox()
        tAnzRest = New System.Windows.Forms.TextBox()
        lblLinienGruppe = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
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
        Panel1 = New System.Windows.Forms.Panel()
        Label12 = New System.Windows.Forms.Label()
        cbChargenTeiler = New wb_CheckedListBox()
        lblChargenResult = New System.Windows.Forms.Label()
        cbVariante = New wb_ComboBox()
        cbLiniengruppe = New wb_ComboBox()
        cbAufloesen = New System.Windows.Forms.CheckBox()
        lblAufloesen = New System.Windows.Forms.Label()
        pTeigChargen.SuspendLayout()
        pArtikelChargen.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' BtnCancel
        ' 
        BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        BtnCancel.Location = New System.Drawing.Point(12, 417)
        BtnCancel.Name = "BtnCancel"
        BtnCancel.Size = New System.Drawing.Size(104, 28)
        BtnCancel.TabIndex = 99
        BtnCancel.Text = "Abbruch"
        BtnCancel.UseVisualStyleBackColor = True
        ' 
        ' BtnOK
        ' 
        BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        BtnOK.Location = New System.Drawing.Point(466, 417)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New System.Drawing.Size(104, 28)
        BtnOK.TabIndex = 4
        BtnOK.Text = "OK"
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' tArtikelNummer
        ' 
        tArtikelNummer.BackColor = Drawing.SystemColors.Window
        tArtikelNummer.Location = New System.Drawing.Point(12, 22)
        tArtikelNummer.Name = "tArtikelNummer"
        tArtikelNummer.Size = New System.Drawing.Size(100, 20)
        tArtikelNummer.TabIndex = 1
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label3.Location = New System.Drawing.Point(13, 6)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(53, 13)
        Label3.TabIndex = 70
        Label3.Text = "Artikel-Nr."
        ' 
        ' tArtikelName
        ' 
        tArtikelName.Location = New System.Drawing.Point(118, 22)
        tArtikelName.Name = "tArtikelName"
        tArtikelName.Size = New System.Drawing.Size(293, 20)
        tArtikelName.TabIndex = 2
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label2.Location = New System.Drawing.Point(119, 6)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(101, 13)
        Label2.TabIndex = 69
        Label2.Text = "Artikel-Bezeichnung"
        ' 
        ' tRezeptNummer
        ' 
        tRezeptNummer.BackColor = Drawing.SystemColors.Window
        tRezeptNummer.Location = New System.Drawing.Point(12, 60)
        tRezeptNummer.Name = "tRezeptNummer"
        tRezeptNummer.Size = New System.Drawing.Size(100, 20)
        tRezeptNummer.TabIndex = 11
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label1.Location = New System.Drawing.Point(13, 44)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(58, 13)
        Label1.TabIndex = 74
        Label1.Text = "Rezept-Nr."
        ' 
        ' tRezeptName
        ' 
        tRezeptName.Location = New System.Drawing.Point(118, 60)
        tRezeptName.Name = "tRezeptName"
        tRezeptName.Size = New System.Drawing.Size(293, 20)
        tRezeptName.TabIndex = 12
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label4.Location = New System.Drawing.Point(119, 44)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(106, 13)
        Label4.TabIndex = 73
        Label4.Text = "Rezept-Bezeichnung"
        ' 
        ' tGesMengeKg
        ' 
        tGesMengeKg.BackColor = Drawing.SystemColors.Window
        tGesMengeKg.Location = New System.Drawing.Point(446, 60)
        tGesMengeKg.Name = "tGesMengeKg"
        tGesMengeKg.Size = New System.Drawing.Size(100, 20)
        tGesMengeKg.TabIndex = 13
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label5.Location = New System.Drawing.Point(447, 44)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(75, 13)
        Label5.TabIndex = 78
        Label5.Text = "Gesamtmenge"
        ' 
        ' tGesMengeStk
        ' 
        tGesMengeStk.BackColor = Drawing.SystemColors.Window
        tGesMengeStk.Location = New System.Drawing.Point(446, 22)
        tGesMengeStk.Name = "tGesMengeStk"
        tGesMengeStk.Size = New System.Drawing.Size(100, 20)
        tGesMengeStk.TabIndex = 3
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label6.Location = New System.Drawing.Point(447, 6)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(74, 13)
        Label6.TabIndex = 77
        Label6.Text = "Gesamt-Stück"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label7.Location = New System.Drawing.Point(546, 25)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(23, 13)
        Label7.TabIndex = 79
        Label7.Text = "Stk"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label8.Location = New System.Drawing.Point(548, 63)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(19, 13)
        Label8.TabIndex = 80
        Label8.Text = "kg"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label9.Location = New System.Drawing.Point(548, 124)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(19, 13)
        Label9.TabIndex = 87
        Label9.Text = "kg"
        ' 
        ' tMengeOptimal
        ' 
        tMengeOptimal.BackColor = Drawing.SystemColors.Window
        tMengeOptimal.Location = New System.Drawing.Point(446, 121)
        tMengeOptimal.Name = "tMengeOptimal"
        tMengeOptimal.Size = New System.Drawing.Size(100, 20)
        tMengeOptimal.TabIndex = 8
        ' 
        ' TextBox6
        ' 
        TextBox6.BackColor = Drawing.SystemColors.Window
        TextBox6.Location = New System.Drawing.Point(12, 121)
        TextBox6.Name = "TextBox6"
        TextBox6.Size = New System.Drawing.Size(100, 20)
        TextBox6.TabIndex = 5
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label11.Location = New System.Drawing.Point(13, 105)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(63, 13)
        Label11.TabIndex = 85
        Label11.Text = "Auftrags-Nr."
        ' 
        ' tAnzOptimal
        ' 
        tAnzOptimal.Location = New System.Drawing.Point(337, 121)
        tAnzOptimal.Name = "tAnzOptimal"
        tAnzOptimal.Size = New System.Drawing.Size(74, 20)
        tAnzOptimal.TabIndex = 7
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label13.Location = New System.Drawing.Point(411, 124)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(23, 13)
        Label13.TabIndex = 88
        Label13.Text = "Stk"
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label14.Location = New System.Drawing.Point(339, 104)
        Label14.Name = "Label14"
        Label14.Size = New System.Drawing.Size(81, 13)
        Label14.TabIndex = 89
        Label14.Text = "Optimalchargen"
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label15.Location = New System.Drawing.Point(339, 145)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(68, 13)
        Label15.TabIndex = 95
        Label15.Text = "Restchargen"
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label16.Location = New System.Drawing.Point(411, 165)
        Label16.Name = "Label16"
        Label16.Size = New System.Drawing.Size(23, 13)
        Label16.TabIndex = 94
        Label16.Text = "Stk"
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label17.Location = New System.Drawing.Point(548, 165)
        Label17.Name = "Label17"
        Label17.Size = New System.Drawing.Size(19, 13)
        Label17.TabIndex = 93
        Label17.Text = "kg"
        ' 
        ' tMengeRest
        ' 
        tMengeRest.BackColor = Drawing.SystemColors.Window
        tMengeRest.Location = New System.Drawing.Point(446, 162)
        tMengeRest.Name = "tMengeRest"
        tMengeRest.Size = New System.Drawing.Size(100, 20)
        tMengeRest.TabIndex = 8
        ' 
        ' tAnzRest
        ' 
        tAnzRest.Location = New System.Drawing.Point(337, 162)
        tAnzRest.Name = "tAnzRest"
        tAnzRest.Size = New System.Drawing.Size(74, 20)
        tAnzRest.TabIndex = 7
        ' 
        ' lblLinienGruppe
        ' 
        lblLinienGruppe.AutoSize = True
        lblLinienGruppe.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblLinienGruppe.Location = New System.Drawing.Point(118, 104)
        lblLinienGruppe.Name = "lblLinienGruppe"
        lblLinienGruppe.Size = New System.Drawing.Size(122, 13)
        lblLinienGruppe.TabIndex = 97
        lblLinienGruppe.Text = "Liniengruppe Produktion"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label10.Location = New System.Drawing.Point(118, 145)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(79, 13)
        Label10.TabIndex = 100
        Label10.Text = "Rezeptvariante"
        ' 
        ' lblTeigChargen
        ' 
        lblTeigChargen.AutoSize = True
        lblTeigChargen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblTeigChargen.Location = New System.Drawing.Point(0, 0)
        lblTeigChargen.Name = "lblTeigChargen"
        lblTeigChargen.Size = New System.Drawing.Size(71, 13)
        lblTeigChargen.TabIndex = 108
        lblTeigChargen.Text = "Teig-Chargen"
        ' 
        ' pTeigChargen
        ' 
        pTeigChargen.BackColor = Drawing.Color.Transparent
        pTeigChargen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        pTeigChargen.Controls.Add(lblTeigChargen)
        pTeigChargen.Controls.Add(tRezOptkg)
        pTeigChargen.Controls.Add(tRezOptPrz)
        pTeigChargen.Controls.Add(tRezMaxkg)
        pTeigChargen.Controls.Add(tRezMaxPrz)
        pTeigChargen.Controls.Add(tRezMinkg)
        pTeigChargen.Controls.Add(tRezMinPrz)
        pTeigChargen.Controls.Add(lblTeigGesamt)
        pTeigChargen.Controls.Add(tRezGesamt)
        pTeigChargen.ForeColor = Drawing.SystemColors.ControlText
        pTeigChargen.Location = New System.Drawing.Point(7, 218)
        pTeigChargen.Name = "pTeigChargen"
        pTeigChargen.Size = New System.Drawing.Size(151, 122)
        pTeigChargen.TabIndex = 107
        ' 
        ' tRezOptkg
        ' 
        tRezOptkg.Location = New System.Drawing.Point(3, 93)
        tRezOptkg.Name = "tRezOptkg"
        tRezOptkg.ReadOnly = True
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
        tRezOptPrz.ReadOnly = True
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
        tRezMaxkg.ReadOnly = True
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
        tRezMaxPrz.ReadOnly = True
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
        tRezMinkg.ReadOnly = True
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
        tRezMinPrz.ReadOnly = True
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
        lblOpt.Location = New System.Drawing.Point(164, 315)
        lblOpt.Name = "lblOpt"
        lblOpt.Size = New System.Drawing.Size(51, 13)
        lblOpt.TabIndex = 106
        lblOpt.Text = "Optimal"
        lblOpt.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' lblMax
        ' 
        lblMax.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblMax.Location = New System.Drawing.Point(164, 289)
        lblMax.Name = "lblMax"
        lblMax.Size = New System.Drawing.Size(51, 13)
        lblMax.TabIndex = 105
        lblMax.Text = "Maximal"
        lblMax.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' lblGewicht
        ' 
        lblGewicht.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblGewicht.Location = New System.Drawing.Point(163, 237)
        lblGewicht.Name = "lblGewicht"
        lblGewicht.Size = New System.Drawing.Size(52, 13)
        lblGewicht.TabIndex = 104
        lblGewicht.Text = "Gewicht"
        lblGewicht.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' lblArtikelChargen
        ' 
        lblArtikelChargen.AutoSize = True
        lblArtikelChargen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblArtikelChargen.Location = New System.Drawing.Point(9, 4)
        lblArtikelChargen.Name = "lblArtikelChargen"
        lblArtikelChargen.Size = New System.Drawing.Size(79, 13)
        lblArtikelChargen.TabIndex = 103
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
        pArtikelChargen.Controls.Add(lblArtikelChargen)
        pArtikelChargen.Controls.Add(tChrgMaxkg)
        pArtikelChargen.Controls.Add(tChrgMaxPrz)
        pArtikelChargen.Controls.Add(tChrgMinStk)
        pArtikelChargen.Controls.Add(tChrgMinkg)
        pArtikelChargen.Controls.Add(tChrgMinPrz)
        pArtikelChargen.Controls.Add(lblProStk)
        pArtikelChargen.Controls.Add(tStkGewicht)
        pArtikelChargen.Location = New System.Drawing.Point(221, 219)
        pArtikelChargen.Name = "pArtikelChargen"
        pArtikelChargen.Size = New System.Drawing.Size(176, 122)
        pArtikelChargen.TabIndex = 102
        ' 
        ' tChrgOptStk
        ' 
        tChrgOptStk.Location = New System.Drawing.Point(116, 93)
        tChrgOptStk.Name = "tChrgOptStk"
        tChrgOptStk.ReadOnly = True
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
        tChrgOptkg.ReadOnly = True
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
        tChrgOptPrz.ReadOnly = True
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
        tChrgMaxStk.ReadOnly = True
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
        tChrgMaxkg.ReadOnly = True
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
        tChrgMaxPrz.ReadOnly = True
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
        tChrgMinStk.ReadOnly = True
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
        tChrgMinkg.ReadOnly = True
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
        tChrgMinPrz.ReadOnly = True
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
        lblProStk.Location = New System.Drawing.Point(9, 17)
        lblProStk.Name = "lblProStk"
        lblProStk.Size = New System.Drawing.Size(84, 13)
        lblProStk.TabIndex = 35
        lblProStk.Text = "pro Stück (nass)"
        ' 
        ' tStkGewicht
        ' 
        tStkGewicht.Location = New System.Drawing.Point(116, 15)
        tStkGewicht.Name = "tStkGewicht"
        tStkGewicht.ReadOnly = True
        tStkGewicht.Size = New System.Drawing.Size(54, 20)
        tStkGewicht.TabIndex = 34
        tStkGewicht.TabStop = False
        tStkGewicht.Text = "1567 gr "
        tStkGewicht.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        tStkGewicht.WordWrap = False
        ' 
        ' lblMin
        ' 
        lblMin.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblMin.Location = New System.Drawing.Point(163, 263)
        lblMin.Name = "lblMin"
        lblMin.Size = New System.Drawing.Size(52, 13)
        lblMin.TabIndex = 101
        lblMin.Text = "Minimal"
        lblMin.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Label12)
        Panel1.Controls.Add(cbChargenTeiler)
        Panel1.Location = New System.Drawing.Point(403, 219)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(167, 122)
        Panel1.TabIndex = 109
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label12.Location = New System.Drawing.Point(3, 5)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(129, 13)
        Label12.TabIndex = 110
        Label12.Text = "wenn möglich aufteilen in:"
        ' 
        ' cbChargenTeiler
        ' 
        cbChargenTeiler.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        cbChargenTeiler.BackColor = Drawing.SystemColors.ActiveBorder
        cbChargenTeiler.BorderStyle = System.Windows.Forms.BorderStyle.None
        cbChargenTeiler.CheckOnClick = True
        cbChargenTeiler.FormattingEnabled = True
        cbChargenTeiler.Location = New System.Drawing.Point(6, 32)
        cbChargenTeiler.Name = "cbChargenTeiler"
        cbChargenTeiler.SelIndex = 0
        cbChargenTeiler.Size = New System.Drawing.Size(162, 90)
        cbChargenTeiler.TabIndex = 109
        cbChargenTeiler.TabStop = False
        ' 
        ' lblChargenResult
        ' 
        lblChargenResult.Location = New System.Drawing.Point(13, 359)
        lblChargenResult.Name = "lblChargenResult"
        lblChargenResult.Size = New System.Drawing.Size(384, 45)
        lblChargenResult.TabIndex = 110
        lblChargenResult.Text = "Ergebnis der Chargen-Aufteilung"
        ' 
        ' cbVariante
        ' 
        cbVariante.FormattingEnabled = True
        cbVariante.Location = New System.Drawing.Point(118, 162)
        cbVariante.Name = "cbVariante"
        cbVariante.Size = New System.Drawing.Size(197, 21)
        cbVariante.TabIndex = 6
        cbVariante.TabStop = False
        cbVariante.Text = "RV"
        ' 
        ' cbLiniengruppe
        ' 
        cbLiniengruppe.FormattingEnabled = True
        cbLiniengruppe.Location = New System.Drawing.Point(118, 121)
        cbLiniengruppe.Name = "cbLiniengruppe"
        cbLiniengruppe.Size = New System.Drawing.Size(197, 21)
        cbLiniengruppe.TabIndex = 6
        cbLiniengruppe.TabStop = False
        cbLiniengruppe.Text = "LG"
        ' 
        ' cbAufloesen
        ' 
        cbAufloesen.AutoSize = True
        cbAufloesen.Location = New System.Drawing.Point(14, 164)
        cbAufloesen.Name = "cbAufloesen"
        cbAufloesen.Size = New System.Drawing.Size(15, 14)
        cbAufloesen.TabIndex = 111
        cbAufloesen.UseVisualStyleBackColor = True
        ' 
        ' lblAufloesen
        ' 
        lblAufloesen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblAufloesen.Location = New System.Drawing.Point(29, 157)
        lblAufloesen.Name = "lblAufloesen"
        lblAufloesen.Size = New System.Drawing.Size(86, 32)
        lblAufloesen.TabIndex = 10
        lblAufloesen.Text = "Vorproduktion auflösen"
        ' 
        ' wb_Planung_Neu
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(598, 492)
        ControlBox = False
        Controls.Add(lblAufloesen)
        Controls.Add(cbAufloesen)
        Controls.Add(lblChargenResult)
        Controls.Add(Panel1)
        Controls.Add(pTeigChargen)
        Controls.Add(lblOpt)
        Controls.Add(lblMax)
        Controls.Add(lblGewicht)
        Controls.Add(pArtikelChargen)
        Controls.Add(lblMin)
        Controls.Add(Label10)
        Controls.Add(cbVariante)
        Controls.Add(lblLinienGruppe)
        Controls.Add(cbLiniengruppe)
        Controls.Add(Label15)
        Controls.Add(Label16)
        Controls.Add(Label17)
        Controls.Add(tMengeRest)
        Controls.Add(tAnzRest)
        Controls.Add(Label14)
        Controls.Add(Label13)
        Controls.Add(Label9)
        Controls.Add(tMengeOptimal)
        Controls.Add(TextBox6)
        Controls.Add(Label11)
        Controls.Add(tAnzOptimal)
        Controls.Add(Label8)
        Controls.Add(Label7)
        Controls.Add(tGesMengeKg)
        Controls.Add(Label5)
        Controls.Add(tGesMengeStk)
        Controls.Add(Label6)
        Controls.Add(tRezeptNummer)
        Controls.Add(Label1)
        Controls.Add(tRezeptName)
        Controls.Add(Label4)
        Controls.Add(tArtikelNummer)
        Controls.Add(Label3)
        Controls.Add(tArtikelName)
        Controls.Add(Label2)
        Controls.Add(BtnCancel)
        Controls.Add(BtnOK)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        MaximizeBox = False
        MinimizeBox = False
        Name = "wb_Planung_Neu"
        Text = "Chargen anlegen/ändern"
        pTeigChargen.ResumeLayout(False)
        pTeigChargen.PerformLayout()
        pArtikelChargen.ResumeLayout(False)
        pArtikelChargen.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents tArtikelNummer As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tArtikelName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tRezeptNummer As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tRezeptName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tGesMengeKg As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tGesMengeStk As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tMengeOptimal As System.Windows.Forms.TextBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tAnzOptimal As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents tMengeRest As System.Windows.Forms.TextBox
    Friend WithEvents tAnzRest As System.Windows.Forms.TextBox
    Friend WithEvents lblLinienGruppe As System.Windows.Forms.Label
    Friend WithEvents cbLiniengruppe As wb_ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cbVariante As wb_ComboBox
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cbChargenTeiler As wb_CheckedListBox
    Friend WithEvents lblChargenResult As System.Windows.Forms.Label
    Friend WithEvents cbAufloesen As System.Windows.Forms.CheckBox
    Friend WithEvents lblAufloesen As System.Windows.Forms.Label
End Class
