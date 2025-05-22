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
        tRohstoffName = New System.Windows.Forms.TextBox()
        lblBezeichnung = New System.Windows.Forms.Label()
        tRohstoffNummer = New System.Windows.Forms.TextBox()
        lblNummer = New System.Windows.Forms.Label()
        tRohstoffKommentar = New System.Windows.Forms.TextBox()
        lblKommentar = New System.Windows.Forms.Label()
        pnlDetails = New System.Windows.Forms.Panel()
        tAlternativRohstoff = New System.Windows.Forms.TextBox()
        lblAlternativRohstoff = New System.Windows.Forms.Label()
        cbFreigabeProduktion = New System.Windows.Forms.CheckBox()
        tbRezName = New System.Windows.Forms.TextBox()
        tbRezNr = New System.Windows.Forms.TextBox()
        lblRezept = New System.Windows.Forms.Label()
        lbRezNr = New System.Windows.Forms.Label()
        TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        tbDeklarationExtern = New System.Windows.Forms.TextBox()
        lblDeklIntern = New System.Windows.Forms.Label()
        tbDeklarationIntern = New System.Windows.Forms.TextBox()
        cbAufloesen = New System.Windows.Forms.CheckBox()
        cbInterneDeklaration = New System.Windows.Forms.CheckBox()
        lblDeklExtern = New System.Windows.Forms.Label()
        eMindestmenge = New System.Windows.Forms.Label()
        eBilanzmenge = New System.Windows.Forms.Label()
        eGebindegroesse = New System.Windows.Forms.Label()
        ePreis = New System.Windows.Forms.Label()
        tbMindestMenge = New System.Windows.Forms.TextBox()
        lbMindestMenge = New System.Windows.Forms.Label()
        tbBilanzmenge = New System.Windows.Forms.TextBox()
        lbBilanzMenge = New System.Windows.Forms.Label()
        cbKeineDeklaration = New System.Windows.Forms.CheckBox()
        tbGebindeGroesse = New System.Windows.Forms.TextBox()
        lblGebindegroesse = New System.Windows.Forms.Label()
        tbRohstoffPreis = New System.Windows.Forms.TextBox()
        lblPreis = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        cbRohstoffGrp2 = New wb_ComboBox()
        Label14 = New System.Windows.Forms.Label()
        cbRohstoffGrp1 = New wb_ComboBox()
        cbRezeptGewicht = New System.Windows.Forms.CheckBox()
        cbNwtBerechnung = New System.Windows.Forms.CheckBox()
        tID = New System.Windows.Forms.TextBox()
        tType = New System.Windows.Forms.TextBox()
        cbAktiv = New System.Windows.Forms.CheckBox()
        lblID = New System.Windows.Forms.Label()
        lblType = New System.Windows.Forms.Label()
        lblAktiv = New System.Windows.Forms.Label()
        tBemerkung = New System.Windows.Forms.TextBox()
        lblBemerkung = New System.Windows.Forms.Label()
        pnlDetails.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' tRohstoffName
        ' 
        tRohstoffName.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        tRohstoffName.Location = New System.Drawing.Point(165, 24)
        tRohstoffName.Name = "tRohstoffName"
        tRohstoffName.Size = New System.Drawing.Size(737, 20)
        tRohstoffName.TabIndex = 1
        ' 
        ' lblBezeichnung
        ' 
        lblBezeichnung.AutoSize = True
        lblBezeichnung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblBezeichnung.Location = New System.Drawing.Point(162, 8)
        lblBezeichnung.Name = "lblBezeichnung"
        lblBezeichnung.Size = New System.Drawing.Size(112, 13)
        lblBezeichnung.TabIndex = 33
        lblBezeichnung.Text = "Rohstoff-Bezeichnung"
        ' 
        ' tRohstoffNummer
        ' 
        tRohstoffNummer.Location = New System.Drawing.Point(12, 24)
        tRohstoffNummer.Name = "tRohstoffNummer"
        tRohstoffNummer.ReadOnly = True
        tRohstoffNummer.Size = New System.Drawing.Size(136, 20)
        tRohstoffNummer.TabIndex = 34
        tRohstoffNummer.TabStop = False
        ' 
        ' lblNummer
        ' 
        lblNummer.AutoSize = True
        lblNummer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblNummer.Location = New System.Drawing.Point(9, 8)
        lblNummer.Name = "lblNummer"
        lblNummer.Size = New System.Drawing.Size(46, 13)
        lblNummer.TabIndex = 35
        lblNummer.Text = "Nummer"
        ' 
        ' tRohstoffKommentar
        ' 
        tRohstoffKommentar.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        tRohstoffKommentar.Location = New System.Drawing.Point(165, 63)
        tRohstoffKommentar.Name = "tRohstoffKommentar"
        tRohstoffKommentar.Size = New System.Drawing.Size(526, 20)
        tRohstoffKommentar.TabIndex = 2
        ' 
        ' lblKommentar
        ' 
        lblKommentar.AutoSize = True
        lblKommentar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblKommentar.Location = New System.Drawing.Point(162, 47)
        lblKommentar.Name = "lblKommentar"
        lblKommentar.Size = New System.Drawing.Size(60, 13)
        lblKommentar.TabIndex = 37
        lblKommentar.Text = "Kommentar"
        ' 
        ' pnlDetails
        ' 
        pnlDetails.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        pnlDetails.Controls.Add(tAlternativRohstoff)
        pnlDetails.Controls.Add(lblAlternativRohstoff)
        pnlDetails.Controls.Add(cbFreigabeProduktion)
        pnlDetails.Controls.Add(tbRezName)
        pnlDetails.Controls.Add(tbRezNr)
        pnlDetails.Controls.Add(lblRezept)
        pnlDetails.Controls.Add(lbRezNr)
        pnlDetails.Controls.Add(TableLayoutPanel1)
        pnlDetails.Controls.Add(eMindestmenge)
        pnlDetails.Controls.Add(eBilanzmenge)
        pnlDetails.Controls.Add(eGebindegroesse)
        pnlDetails.Controls.Add(ePreis)
        pnlDetails.Controls.Add(tbMindestMenge)
        pnlDetails.Controls.Add(lbMindestMenge)
        pnlDetails.Controls.Add(tbBilanzmenge)
        pnlDetails.Controls.Add(lbBilanzMenge)
        pnlDetails.Controls.Add(cbKeineDeklaration)
        pnlDetails.Controls.Add(tbGebindeGroesse)
        pnlDetails.Controls.Add(lblGebindegroesse)
        pnlDetails.Controls.Add(tbRohstoffPreis)
        pnlDetails.Controls.Add(lblPreis)
        pnlDetails.Controls.Add(cbRohstoffGrp2)
        pnlDetails.Controls.Add(Label14)
        pnlDetails.Controls.Add(cbRohstoffGrp1)
        pnlDetails.Controls.Add(cbRezeptGewicht)
        pnlDetails.Controls.Add(cbNwtBerechnung)
        pnlDetails.Controls.Add(Label1)
        pnlDetails.Location = New System.Drawing.Point(0, 102)
        pnlDetails.Name = "pnlDetails"
        pnlDetails.Size = New System.Drawing.Size(908, 389)
        pnlDetails.TabIndex = 65
        ' 
        ' tAlternativRohstoff
        ' 
        tAlternativRohstoff.Location = New System.Drawing.Point(165, 58)
        tAlternativRohstoff.Name = "tAlternativRohstoff"
        tAlternativRohstoff.Size = New System.Drawing.Size(136, 20)
        tAlternativRohstoff.TabIndex = 94
        tAlternativRohstoff.TabStop = False
        ' 
        ' lblAlternativRohstoff
        ' 
        lblAlternativRohstoff.AutoSize = True
        lblAlternativRohstoff.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblAlternativRohstoff.Location = New System.Drawing.Point(162, 42)
        lblAlternativRohstoff.Name = "lblAlternativRohstoff"
        lblAlternativRohstoff.Size = New System.Drawing.Size(94, 13)
        lblAlternativRohstoff.TabIndex = 93
        lblAlternativRohstoff.Text = "Alternativ-Rohstoff"
        ' 
        ' cbFreigabeProduktion
        ' 
        cbFreigabeProduktion.AutoSize = True
        cbFreigabeProduktion.Location = New System.Drawing.Point(12, 303)
        cbFreigabeProduktion.Name = "cbFreigabeProduktion"
        cbFreigabeProduktion.Size = New System.Drawing.Size(201, 17)
        cbFreigabeProduktion.TabIndex = 92
        cbFreigabeProduktion.Text = "Rohstoff kann (vor)produziert werden"
        cbFreigabeProduktion.UseVisualStyleBackColor = True
        ' 
        ' tbRezName
        ' 
        tbRezName.Location = New System.Drawing.Point(96, 277)
        tbRezName.Name = "tbRezName"
        tbRezName.ReadOnly = True
        tbRezName.Size = New System.Drawing.Size(206, 20)
        tbRezName.TabIndex = 90
        tbRezName.TabStop = False
        ' 
        ' tbRezNr
        ' 
        tbRezNr.Location = New System.Drawing.Point(12, 277)
        tbRezNr.Name = "tbRezNr"
        tbRezNr.ReadOnly = True
        tbRezNr.Size = New System.Drawing.Size(71, 20)
        tbRezNr.TabIndex = 89
        tbRezNr.TabStop = False
        ' 
        ' lblRezept
        ' 
        lblRezept.AutoSize = True
        lblRezept.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRezept.Location = New System.Drawing.Point(93, 261)
        lblRezept.Name = "lblRezept"
        lblRezept.Size = New System.Drawing.Size(50, 13)
        lblRezept.TabIndex = 88
        lblRezept.Text = "Rezeptur"
        ' 
        ' lbRezNr
        ' 
        lbRezNr.AutoSize = True
        lbRezNr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lbRezNr.Location = New System.Drawing.Point(12, 261)
        lbRezNr.Name = "lbRezNr"
        lbRezNr.Size = New System.Drawing.Size(55, 13)
        lbRezNr.TabIndex = 87
        lbRezNr.Text = "Rezept-Nr"
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(tbDeklarationExtern, 0, 1)
        TableLayoutPanel1.Controls.Add(lblDeklIntern, 0, 2)
        TableLayoutPanel1.Controls.Add(tbDeklarationIntern, 0, 3)
        TableLayoutPanel1.Controls.Add(cbAufloesen, 1, 0)
        TableLayoutPanel1.Controls.Add(cbInterneDeklaration, 1, 2)
        TableLayoutPanel1.Controls.Add(lblDeklExtern, 0, 0)
        TableLayoutPanel1.Location = New System.Drawing.Point(331, 3)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 4
        TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F))
        TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F))
        TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F))
        TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F))
        TableLayoutPanel1.Size = New System.Drawing.Size(574, 386)
        TableLayoutPanel1.TabIndex = 66
        ' 
        ' tbDeklarationExtern
        ' 
        TableLayoutPanel1.SetColumnSpan(tbDeklarationExtern, 2)
        tbDeklarationExtern.Dock = System.Windows.Forms.DockStyle.Fill
        tbDeklarationExtern.Location = New System.Drawing.Point(3, 26)
        tbDeklarationExtern.Multiline = True
        tbDeklarationExtern.Name = "tbDeklarationExtern"
        tbDeklarationExtern.Size = New System.Drawing.Size(568, 164)
        tbDeklarationExtern.TabIndex = 68
        ' 
        ' lblDeklIntern
        ' 
        lblDeklIntern.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
        lblDeklIntern.AutoSize = True
        lblDeklIntern.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblDeklIntern.Location = New System.Drawing.Point(3, 193)
        lblDeklIntern.Name = "lblDeklIntern"
        lblDeklIntern.Size = New System.Drawing.Size(90, 23)
        lblDeklIntern.TabIndex = 77
        lblDeklIntern.Text = "Deklaration intern"
        lblDeklIntern.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' tbDeklarationIntern
        ' 
        TableLayoutPanel1.SetColumnSpan(tbDeklarationIntern, 2)
        tbDeklarationIntern.Dock = System.Windows.Forms.DockStyle.Fill
        tbDeklarationIntern.Location = New System.Drawing.Point(3, 219)
        tbDeklarationIntern.Multiline = True
        tbDeklarationIntern.Name = "tbDeklarationIntern"
        tbDeklarationIntern.Size = New System.Drawing.Size(568, 164)
        tbDeklarationIntern.TabIndex = 69
        ' 
        ' cbAufloesen
        ' 
        cbAufloesen.AutoSize = True
        cbAufloesen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        cbAufloesen.Location = New System.Drawing.Point(103, 3)
        cbAufloesen.Name = "cbAufloesen"
        cbAufloesen.Size = New System.Drawing.Size(124, 17)
        cbAufloesen.TabIndex = 80
        cbAufloesen.TabStop = False
        cbAufloesen.Text = "Zutatenliste auflösen"
        cbAufloesen.UseVisualStyleBackColor = True
        ' 
        ' cbInterneDeklaration
        ' 
        cbInterneDeklaration.AutoSize = True
        cbInterneDeklaration.ImeMode = System.Windows.Forms.ImeMode.NoControl
        cbInterneDeklaration.Location = New System.Drawing.Point(103, 196)
        cbInterneDeklaration.Name = "cbInterneDeklaration"
        cbInterneDeklaration.Size = New System.Drawing.Size(145, 17)
        cbInterneDeklaration.TabIndex = 81
        cbInterneDeklaration.TabStop = False
        cbInterneDeklaration.Text = "Interne Deklaration verw."
        cbInterneDeklaration.UseVisualStyleBackColor = True
        ' 
        ' lblDeklExtern
        ' 
        lblDeklExtern.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
        lblDeklExtern.AutoSize = True
        lblDeklExtern.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblDeklExtern.Location = New System.Drawing.Point(3, 0)
        lblDeklExtern.Name = "lblDeklExtern"
        lblDeklExtern.Size = New System.Drawing.Size(61, 23)
        lblDeklExtern.TabIndex = 76
        lblDeklExtern.Text = "Deklaration"
        lblDeklExtern.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' eMindestmenge
        ' 
        eMindestmenge.AutoSize = True
        eMindestmenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        eMindestmenge.Location = New System.Drawing.Point(263, 100)
        eMindestmenge.Name = "eMindestmenge"
        eMindestmenge.Size = New System.Drawing.Size(19, 13)
        eMindestmenge.TabIndex = 86
        eMindestmenge.Text = "kg"
        ' 
        ' eBilanzmenge
        ' 
        eBilanzmenge.AutoSize = True
        eBilanzmenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        eBilanzmenge.Location = New System.Drawing.Point(110, 100)
        eBilanzmenge.Name = "eBilanzmenge"
        eBilanzmenge.Size = New System.Drawing.Size(19, 13)
        eBilanzmenge.TabIndex = 85
        eBilanzmenge.Text = "kg"
        ' 
        ' eGebindegroesse
        ' 
        eGebindegroesse.AutoSize = True
        eGebindegroesse.ImeMode = System.Windows.Forms.ImeMode.NoControl
        eGebindegroesse.Location = New System.Drawing.Point(110, 61)
        eGebindegroesse.Name = "eGebindegroesse"
        eGebindegroesse.Size = New System.Drawing.Size(19, 13)
        eGebindegroesse.TabIndex = 84
        eGebindegroesse.Text = "kg"
        ' 
        ' ePreis
        ' 
        ePreis.AutoSize = True
        ePreis.ImeMode = System.Windows.Forms.ImeMode.NoControl
        ePreis.Location = New System.Drawing.Point(110, 23)
        ePreis.Name = "ePreis"
        ePreis.Size = New System.Drawing.Size(13, 13)
        ePreis.TabIndex = 83
        ePreis.Text = "€"
        ' 
        ' tbMindestMenge
        ' 
        tbMindestMenge.Location = New System.Drawing.Point(165, 97)
        tbMindestMenge.Name = "tbMindestMenge"
        tbMindestMenge.Size = New System.Drawing.Size(96, 20)
        tbMindestMenge.TabIndex = 67
        tbMindestMenge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        ' 
        ' lbMindestMenge
        ' 
        lbMindestMenge.AutoSize = True
        lbMindestMenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lbMindestMenge.Location = New System.Drawing.Point(162, 81)
        lbMindestMenge.Name = "lbMindestMenge"
        lbMindestMenge.Size = New System.Drawing.Size(76, 13)
        lbMindestMenge.TabIndex = 82
        lbMindestMenge.Text = "Mindestmenge"
        ' 
        ' tbBilanzmenge
        ' 
        tbBilanzmenge.Location = New System.Drawing.Point(12, 97)
        tbBilanzmenge.Name = "tbBilanzmenge"
        tbBilanzmenge.ReadOnly = True
        tbBilanzmenge.Size = New System.Drawing.Size(96, 20)
        tbBilanzmenge.TabIndex = 80
        tbBilanzmenge.TabStop = False
        tbBilanzmenge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        ' 
        ' lbBilanzMenge
        ' 
        lbBilanzMenge.AutoSize = True
        lbBilanzMenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lbBilanzMenge.Location = New System.Drawing.Point(9, 81)
        lbBilanzMenge.Name = "lbBilanzMenge"
        lbBilanzMenge.Size = New System.Drawing.Size(72, 13)
        lbBilanzMenge.TabIndex = 81
        lbBilanzMenge.Text = "Lagerbestand"
        ' 
        ' cbKeineDeklaration
        ' 
        cbKeineDeklaration.AutoSize = True
        cbKeineDeklaration.ImeMode = System.Windows.Forms.ImeMode.NoControl
        cbKeineDeklaration.Location = New System.Drawing.Point(165, 7)
        cbKeineDeklaration.Name = "cbKeineDeklaration"
        cbKeineDeklaration.Size = New System.Drawing.Size(160, 17)
        cbKeineDeklaration.TabIndex = 78
        cbKeineDeklaration.TabStop = False
        cbKeineDeklaration.Text = "Rohstoff wird nicht deklariert"
        cbKeineDeklaration.UseVisualStyleBackColor = True
        ' 
        ' tbGebindeGroesse
        ' 
        tbGebindeGroesse.Location = New System.Drawing.Point(12, 58)
        tbGebindeGroesse.Name = "tbGebindeGroesse"
        tbGebindeGroesse.Size = New System.Drawing.Size(96, 20)
        tbGebindeGroesse.TabIndex = 66
        tbGebindeGroesse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        ' 
        ' lblGebindegroesse
        ' 
        lblGebindegroesse.AutoSize = True
        lblGebindegroesse.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblGebindegroesse.Location = New System.Drawing.Point(9, 42)
        lblGebindegroesse.Name = "lblGebindegroesse"
        lblGebindegroesse.Size = New System.Drawing.Size(74, 13)
        lblGebindegroesse.TabIndex = 75
        lblGebindegroesse.Text = "Gebindegröße"
        ' 
        ' tbRohstoffPreis
        ' 
        tbRohstoffPreis.Location = New System.Drawing.Point(12, 19)
        tbRohstoffPreis.Name = "tbRohstoffPreis"
        tbRohstoffPreis.Size = New System.Drawing.Size(96, 20)
        tbRohstoffPreis.TabIndex = 65
        tbRohstoffPreis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        ' 
        ' lblPreis
        ' 
        lblPreis.AutoSize = True
        lblPreis.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblPreis.Location = New System.Drawing.Point(9, 3)
        lblPreis.Name = "lblPreis"
        lblPreis.Size = New System.Drawing.Size(47, 13)
        lblPreis.TabIndex = 74
        lblPreis.Text = "Preis/kg"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label1.Location = New System.Drawing.Point(9, 172)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(51, 13)
        Label1.TabIndex = 73
        Label1.Text = "Gruppe 2"
        ' 
        ' cbRohstoffGrp2
        ' 
        cbRohstoffGrp2.FormattingEnabled = True
        cbRohstoffGrp2.Location = New System.Drawing.Point(12, 188)
        cbRohstoffGrp2.Name = "cbRohstoffGrp2"
        cbRohstoffGrp2.Size = New System.Drawing.Size(290, 21)
        cbRohstoffGrp2.TabIndex = 72
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label14.Location = New System.Drawing.Point(9, 129)
        Label14.Name = "Label14"
        Label14.Size = New System.Drawing.Size(51, 13)
        Label14.TabIndex = 71
        Label14.Text = "Gruppe 1"
        ' 
        ' cbRohstoffGrp1
        ' 
        cbRohstoffGrp1.FormattingEnabled = True
        cbRohstoffGrp1.Location = New System.Drawing.Point(12, 148)
        cbRohstoffGrp1.Name = "cbRohstoffGrp1"
        cbRohstoffGrp1.Size = New System.Drawing.Size(290, 21)
        cbRohstoffGrp1.TabIndex = 70
        ' 
        ' cbRezeptGewicht
        ' 
        cbRezeptGewicht.ImeMode = System.Windows.Forms.ImeMode.NoControl
        cbRezeptGewicht.Location = New System.Drawing.Point(12, 218)
        cbRezeptGewicht.Name = "cbRezeptGewicht"
        cbRezeptGewicht.Size = New System.Drawing.Size(290, 19)
        cbRezeptGewicht.TabIndex = 79
        cbRezeptGewicht.TabStop = False
        cbRezeptGewicht.Text = "zählt nicht zur Rezept-Gesamtmenge (Nassgewicht)"
        cbRezeptGewicht.UseVisualStyleBackColor = True
        ' 
        ' cbNwtBerechnung
        ' 
        cbNwtBerechnung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        cbNwtBerechnung.Location = New System.Drawing.Point(12, 235)
        cbNwtBerechnung.Name = "cbNwtBerechnung"
        cbNwtBerechnung.Size = New System.Drawing.Size(316, 24)
        cbNwtBerechnung.TabIndex = 91
        cbNwtBerechnung.TabStop = False
        cbNwtBerechnung.Text = "Sollmenge in der Nährwertberechnung berücksichtigen"
        cbNwtBerechnung.UseVisualStyleBackColor = True
        ' 
        ' tID
        ' 
        tID.Location = New System.Drawing.Point(12, 63)
        tID.Name = "tID"
        tID.ReadOnly = True
        tID.Size = New System.Drawing.Size(48, 20)
        tID.TabIndex = 66
        tID.TabStop = False
        ' 
        ' tType
        ' 
        tType.Location = New System.Drawing.Point(66, 63)
        tType.Name = "tType"
        tType.ReadOnly = True
        tType.Size = New System.Drawing.Size(48, 20)
        tType.TabIndex = 67
        tType.TabStop = False
        ' 
        ' cbAktiv
        ' 
        cbAktiv.AutoSize = True
        cbAktiv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        cbAktiv.Location = New System.Drawing.Point(128, 66)
        cbAktiv.Name = "cbAktiv"
        cbAktiv.Size = New System.Drawing.Size(15, 14)
        cbAktiv.TabIndex = 79
        cbAktiv.TabStop = False
        cbAktiv.UseVisualStyleBackColor = True
        ' 
        ' lblID
        ' 
        lblID.AutoSize = True
        lblID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblID.Location = New System.Drawing.Point(9, 47)
        lblID.Name = "lblID"
        lblID.Size = New System.Drawing.Size(18, 13)
        lblID.TabIndex = 80
        lblID.Text = "ID"
        ' 
        ' lblType
        ' 
        lblType.AutoSize = True
        lblType.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblType.Location = New System.Drawing.Point(65, 47)
        lblType.Name = "lblType"
        lblType.Size = New System.Drawing.Size(31, 13)
        lblType.TabIndex = 81
        lblType.Text = "Type"
        ' 
        ' lblAktiv
        ' 
        lblAktiv.AutoSize = True
        lblAktiv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblAktiv.Location = New System.Drawing.Point(125, 47)
        lblAktiv.Name = "lblAktiv"
        lblAktiv.Size = New System.Drawing.Size(31, 13)
        lblAktiv.TabIndex = 82
        lblAktiv.Text = "Aktiv"
        ' 
        ' tBemerkung
        ' 
        tBemerkung.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        tBemerkung.Location = New System.Drawing.Point(708, 63)
        tBemerkung.Name = "tBemerkung"
        tBemerkung.Size = New System.Drawing.Size(193, 20)
        tBemerkung.TabIndex = 96
        tBemerkung.TabStop = False
        ' 
        ' lblBemerkung
        ' 
        lblBemerkung.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        lblBemerkung.AutoSize = True
        lblBemerkung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblBemerkung.Location = New System.Drawing.Point(706, 47)
        lblBemerkung.Name = "lblBemerkung"
        lblBemerkung.Size = New System.Drawing.Size(61, 13)
        lblBemerkung.TabIndex = 95
        lblBemerkung.Text = "Bemerkung"
        ' 
        ' wb_Rohstoffe_Details
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(907, 503)
        Controls.Add(tBemerkung)
        Controls.Add(lblBemerkung)
        Controls.Add(lblAktiv)
        Controls.Add(cbAktiv)
        Controls.Add(tType)
        Controls.Add(tID)
        Controls.Add(pnlDetails)
        Controls.Add(tRohstoffKommentar)
        Controls.Add(lblKommentar)
        Controls.Add(tRohstoffNummer)
        Controls.Add(lblNummer)
        Controls.Add(tRohstoffName)
        Controls.Add(lblBezeichnung)
        Controls.Add(lblID)
        Controls.Add(lblType)
        Name = "wb_Rohstoffe_Details"
        Text = "Rohstoff Details"
        pnlDetails.ResumeLayout(False)
        pnlDetails.PerformLayout()
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents tRohstoffName As System.Windows.Forms.TextBox
    Friend WithEvents lblBezeichnung As System.Windows.Forms.Label
    Friend WithEvents tRohstoffNummer As System.Windows.Forms.TextBox
    Friend WithEvents lblNummer As System.Windows.Forms.Label
    Friend WithEvents tRohstoffKommentar As System.Windows.Forms.TextBox
    Friend WithEvents lblKommentar As System.Windows.Forms.Label
    Friend WithEvents pnlDetails As System.Windows.Forms.Panel
    Friend WithEvents eMindestmenge As System.Windows.Forms.Label
    Friend WithEvents eBilanzmenge As System.Windows.Forms.Label
    Friend WithEvents eGebindegroesse As System.Windows.Forms.Label
    Friend WithEvents ePreis As System.Windows.Forms.Label
    Friend WithEvents tbMindestMenge As System.Windows.Forms.TextBox
    Friend WithEvents lbMindestMenge As System.Windows.Forms.Label
    Friend WithEvents tbBilanzmenge As System.Windows.Forms.TextBox
    Friend WithEvents lbBilanzMenge As System.Windows.Forms.Label
    Friend WithEvents cbRezeptGewicht As System.Windows.Forms.CheckBox
    Friend WithEvents cbKeineDeklaration As System.Windows.Forms.CheckBox
    Friend WithEvents lblDeklIntern As System.Windows.Forms.Label
    Friend WithEvents lblDeklExtern As System.Windows.Forms.Label
    Friend WithEvents tbDeklarationIntern As System.Windows.Forms.TextBox
    Friend WithEvents tbDeklarationExtern As System.Windows.Forms.TextBox
    Friend WithEvents tbGebindeGroesse As System.Windows.Forms.TextBox
    Friend WithEvents lblGebindegroesse As System.Windows.Forms.Label
    Friend WithEvents tbRohstoffPreis As System.Windows.Forms.TextBox
    Friend WithEvents lblPreis As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbRohstoffGrp2 As wb_ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cbRohstoffGrp1 As wb_ComboBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tID As System.Windows.Forms.TextBox
    Friend WithEvents tType As System.Windows.Forms.TextBox
    Friend WithEvents cbAktiv As System.Windows.Forms.CheckBox
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents lblAktiv As System.Windows.Forms.Label
    Friend WithEvents cbAufloesen As System.Windows.Forms.CheckBox
    Friend WithEvents cbInterneDeklaration As System.Windows.Forms.CheckBox
    Friend WithEvents tbRezName As System.Windows.Forms.TextBox
    Friend WithEvents tbRezNr As System.Windows.Forms.TextBox
    Friend WithEvents lblRezept As System.Windows.Forms.Label
    Friend WithEvents lbRezNr As System.Windows.Forms.Label
    Friend WithEvents cbNwtBerechnung As System.Windows.Forms.CheckBox
    Friend WithEvents cbFreigabeProduktion As System.Windows.Forms.CheckBox
    Friend WithEvents tAlternativRohstoff As System.Windows.Forms.TextBox
    Friend WithEvents lblAlternativRohstoff As System.Windows.Forms.Label
    Friend WithEvents tBemerkung As System.Windows.Forms.TextBox
    Friend WithEvents lblBemerkung As System.Windows.Forms.Label
End Class
