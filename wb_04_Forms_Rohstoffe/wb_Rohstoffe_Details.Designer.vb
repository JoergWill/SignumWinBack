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
        Me.tRohstoffName = New System.Windows.Forms.TextBox()
        Me.lblBezeichnung = New System.Windows.Forms.Label()
        Me.tRohstoffNummer = New System.Windows.Forms.TextBox()
        Me.lblNummer = New System.Windows.Forms.Label()
        Me.tRohstoffKommentar = New System.Windows.Forms.TextBox()
        Me.lblKommentar = New System.Windows.Forms.Label()
        Me.pnlDetails = New System.Windows.Forms.Panel()
        Me.tbRezName = New System.Windows.Forms.TextBox()
        Me.tbRezNr = New System.Windows.Forms.TextBox()
        Me.lblRezept = New System.Windows.Forms.Label()
        Me.lbRezNr = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.tbDeklarationExtern = New System.Windows.Forms.TextBox()
        Me.lblDeklIntern = New System.Windows.Forms.Label()
        Me.tbDeklarationIntern = New System.Windows.Forms.TextBox()
        Me.cbAufloesen = New System.Windows.Forms.CheckBox()
        Me.cbInterneDeklaration = New System.Windows.Forms.CheckBox()
        Me.lblDeklExtern = New System.Windows.Forms.Label()
        Me.eMindestmenge = New System.Windows.Forms.Label()
        Me.eBilanzmenge = New System.Windows.Forms.Label()
        Me.eGebindegroesse = New System.Windows.Forms.Label()
        Me.ePreis = New System.Windows.Forms.Label()
        Me.tbMindestMenge = New System.Windows.Forms.TextBox()
        Me.lbMindestMenge = New System.Windows.Forms.Label()
        Me.tbBilanzmenge = New System.Windows.Forms.TextBox()
        Me.lbBilanzMenge = New System.Windows.Forms.Label()
        Me.cbRezeptGewicht = New System.Windows.Forms.CheckBox()
        Me.cbKeineDeklaration = New System.Windows.Forms.CheckBox()
        Me.tbGebindeGroesse = New System.Windows.Forms.TextBox()
        Me.lblGebindegroesse = New System.Windows.Forms.Label()
        Me.tbRohstoffPreis = New System.Windows.Forms.TextBox()
        Me.lblPreis = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbRohstoffGrp2 = New WinBack.wb_ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cbRohstoffGrp1 = New WinBack.wb_ComboBox()
        Me.tID = New System.Windows.Forms.TextBox()
        Me.tType = New System.Windows.Forms.TextBox()
        Me.cbAktiv = New System.Windows.Forms.CheckBox()
        Me.lblID = New System.Windows.Forms.Label()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblAktiv = New System.Windows.Forms.Label()
        Me.pnlDetails.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tRohstoffName
        '
        Me.tRohstoffName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tRohstoffName.Location = New System.Drawing.Point(165, 24)
        Me.tRohstoffName.Name = "tRohstoffName"
        Me.tRohstoffName.Size = New System.Drawing.Size(493, 20)
        Me.tRohstoffName.TabIndex = 1
        '
        'lblBezeichnung
        '
        Me.lblBezeichnung.AutoSize = True
        Me.lblBezeichnung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblBezeichnung.Location = New System.Drawing.Point(162, 8)
        Me.lblBezeichnung.Name = "lblBezeichnung"
        Me.lblBezeichnung.Size = New System.Drawing.Size(112, 13)
        Me.lblBezeichnung.TabIndex = 33
        Me.lblBezeichnung.Text = "Rohstoff-Bezeichnung"
        '
        'tRohstoffNummer
        '
        Me.tRohstoffNummer.Location = New System.Drawing.Point(12, 24)
        Me.tRohstoffNummer.Name = "tRohstoffNummer"
        Me.tRohstoffNummer.ReadOnly = True
        Me.tRohstoffNummer.Size = New System.Drawing.Size(136, 20)
        Me.tRohstoffNummer.TabIndex = 34
        Me.tRohstoffNummer.TabStop = False
        '
        'lblNummer
        '
        Me.lblNummer.AutoSize = True
        Me.lblNummer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNummer.Location = New System.Drawing.Point(9, 8)
        Me.lblNummer.Name = "lblNummer"
        Me.lblNummer.Size = New System.Drawing.Size(46, 13)
        Me.lblNummer.TabIndex = 35
        Me.lblNummer.Text = "Nummer"
        '
        'tRohstoffKommentar
        '
        Me.tRohstoffKommentar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tRohstoffKommentar.Location = New System.Drawing.Point(165, 63)
        Me.tRohstoffKommentar.Name = "tRohstoffKommentar"
        Me.tRohstoffKommentar.Size = New System.Drawing.Size(493, 20)
        Me.tRohstoffKommentar.TabIndex = 2
        '
        'lblKommentar
        '
        Me.lblKommentar.AutoSize = True
        Me.lblKommentar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblKommentar.Location = New System.Drawing.Point(162, 47)
        Me.lblKommentar.Name = "lblKommentar"
        Me.lblKommentar.Size = New System.Drawing.Size(60, 13)
        Me.lblKommentar.TabIndex = 37
        Me.lblKommentar.Text = "Kommentar"
        '
        'pnlDetails
        '
        Me.pnlDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlDetails.Controls.Add(Me.tbRezName)
        Me.pnlDetails.Controls.Add(Me.tbRezNr)
        Me.pnlDetails.Controls.Add(Me.lblRezept)
        Me.pnlDetails.Controls.Add(Me.lbRezNr)
        Me.pnlDetails.Controls.Add(Me.TableLayoutPanel1)
        Me.pnlDetails.Controls.Add(Me.eMindestmenge)
        Me.pnlDetails.Controls.Add(Me.eBilanzmenge)
        Me.pnlDetails.Controls.Add(Me.eGebindegroesse)
        Me.pnlDetails.Controls.Add(Me.ePreis)
        Me.pnlDetails.Controls.Add(Me.tbMindestMenge)
        Me.pnlDetails.Controls.Add(Me.lbMindestMenge)
        Me.pnlDetails.Controls.Add(Me.tbBilanzmenge)
        Me.pnlDetails.Controls.Add(Me.lbBilanzMenge)
        Me.pnlDetails.Controls.Add(Me.cbRezeptGewicht)
        Me.pnlDetails.Controls.Add(Me.cbKeineDeklaration)
        Me.pnlDetails.Controls.Add(Me.tbGebindeGroesse)
        Me.pnlDetails.Controls.Add(Me.lblGebindegroesse)
        Me.pnlDetails.Controls.Add(Me.tbRohstoffPreis)
        Me.pnlDetails.Controls.Add(Me.lblPreis)
        Me.pnlDetails.Controls.Add(Me.Label1)
        Me.pnlDetails.Controls.Add(Me.cbRohstoffGrp2)
        Me.pnlDetails.Controls.Add(Me.Label14)
        Me.pnlDetails.Controls.Add(Me.cbRohstoffGrp1)
        Me.pnlDetails.Location = New System.Drawing.Point(0, 102)
        Me.pnlDetails.Name = "pnlDetails"
        Me.pnlDetails.Size = New System.Drawing.Size(664, 299)
        Me.pnlDetails.TabIndex = 65
        '
        'tbRezName
        '
        Me.tbRezName.Location = New System.Drawing.Point(96, 237)
        Me.tbRezName.Name = "tbRezName"
        Me.tbRezName.ReadOnly = True
        Me.tbRezName.Size = New System.Drawing.Size(206, 20)
        Me.tbRezName.TabIndex = 90
        Me.tbRezName.TabStop = False
        '
        'tbRezNr
        '
        Me.tbRezNr.Location = New System.Drawing.Point(12, 237)
        Me.tbRezNr.Name = "tbRezNr"
        Me.tbRezNr.ReadOnly = True
        Me.tbRezNr.Size = New System.Drawing.Size(71, 20)
        Me.tbRezNr.TabIndex = 89
        Me.tbRezNr.TabStop = False
        '
        'lblRezept
        '
        Me.lblRezept.AutoSize = True
        Me.lblRezept.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRezept.Location = New System.Drawing.Point(93, 221)
        Me.lblRezept.Name = "lblRezept"
        Me.lblRezept.Size = New System.Drawing.Size(50, 13)
        Me.lblRezept.TabIndex = 88
        Me.lblRezept.Text = "Rezeptur"
        '
        'lbRezNr
        '
        Me.lbRezNr.AutoSize = True
        Me.lbRezNr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbRezNr.Location = New System.Drawing.Point(12, 221)
        Me.lbRezNr.Name = "lbRezNr"
        Me.lbRezNr.Size = New System.Drawing.Size(55, 13)
        Me.lbRezNr.TabIndex = 87
        Me.lbRezNr.Text = "Rezept-Nr"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.tbDeklarationExtern, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblDeklIntern, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.tbDeklarationIntern, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.cbAufloesen, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cbInterneDeklaration, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblDeklExtern, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(331, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(330, 296)
        Me.TableLayoutPanel1.TabIndex = 66
        '
        'tbDeklarationExtern
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbDeklarationExtern, 2)
        Me.tbDeklarationExtern.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbDeklarationExtern.Location = New System.Drawing.Point(3, 26)
        Me.tbDeklarationExtern.Multiline = True
        Me.tbDeklarationExtern.Name = "tbDeklarationExtern"
        Me.tbDeklarationExtern.Size = New System.Drawing.Size(324, 119)
        Me.tbDeklarationExtern.TabIndex = 68
        '
        'lblDeklIntern
        '
        Me.lblDeklIntern.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDeklIntern.AutoSize = True
        Me.lblDeklIntern.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDeklIntern.Location = New System.Drawing.Point(3, 148)
        Me.lblDeklIntern.Name = "lblDeklIntern"
        Me.lblDeklIntern.Size = New System.Drawing.Size(90, 23)
        Me.lblDeklIntern.TabIndex = 77
        Me.lblDeklIntern.Text = "Deklaration intern"
        Me.lblDeklIntern.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbDeklarationIntern
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbDeklarationIntern, 2)
        Me.tbDeklarationIntern.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbDeklarationIntern.Location = New System.Drawing.Point(3, 174)
        Me.tbDeklarationIntern.Multiline = True
        Me.tbDeklarationIntern.Name = "tbDeklarationIntern"
        Me.tbDeklarationIntern.Size = New System.Drawing.Size(324, 119)
        Me.tbDeklarationIntern.TabIndex = 69
        '
        'cbAufloesen
        '
        Me.cbAufloesen.AutoSize = True
        Me.cbAufloesen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbAufloesen.Location = New System.Drawing.Point(103, 3)
        Me.cbAufloesen.Name = "cbAufloesen"
        Me.cbAufloesen.Size = New System.Drawing.Size(124, 17)
        Me.cbAufloesen.TabIndex = 80
        Me.cbAufloesen.TabStop = False
        Me.cbAufloesen.Text = "Zutatenliste auflösen"
        Me.cbAufloesen.UseVisualStyleBackColor = True
        '
        'cbInterneDeklaration
        '
        Me.cbInterneDeklaration.AutoSize = True
        Me.cbInterneDeklaration.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbInterneDeklaration.Location = New System.Drawing.Point(103, 151)
        Me.cbInterneDeklaration.Name = "cbInterneDeklaration"
        Me.cbInterneDeklaration.Size = New System.Drawing.Size(145, 17)
        Me.cbInterneDeklaration.TabIndex = 81
        Me.cbInterneDeklaration.TabStop = False
        Me.cbInterneDeklaration.Text = "Interne Deklaration verw."
        Me.cbInterneDeklaration.UseVisualStyleBackColor = True
        '
        'lblDeklExtern
        '
        Me.lblDeklExtern.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDeklExtern.AutoSize = True
        Me.lblDeklExtern.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDeklExtern.Location = New System.Drawing.Point(3, 0)
        Me.lblDeklExtern.Name = "lblDeklExtern"
        Me.lblDeklExtern.Size = New System.Drawing.Size(61, 23)
        Me.lblDeklExtern.TabIndex = 76
        Me.lblDeklExtern.Text = "Deklaration"
        Me.lblDeklExtern.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'eMindestmenge
        '
        Me.eMindestmenge.AutoSize = True
        Me.eMindestmenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.eMindestmenge.Location = New System.Drawing.Point(263, 100)
        Me.eMindestmenge.Name = "eMindestmenge"
        Me.eMindestmenge.Size = New System.Drawing.Size(19, 13)
        Me.eMindestmenge.TabIndex = 86
        Me.eMindestmenge.Text = "kg"
        '
        'eBilanzmenge
        '
        Me.eBilanzmenge.AutoSize = True
        Me.eBilanzmenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.eBilanzmenge.Location = New System.Drawing.Point(110, 100)
        Me.eBilanzmenge.Name = "eBilanzmenge"
        Me.eBilanzmenge.Size = New System.Drawing.Size(19, 13)
        Me.eBilanzmenge.TabIndex = 85
        Me.eBilanzmenge.Text = "kg"
        '
        'eGebindegroesse
        '
        Me.eGebindegroesse.AutoSize = True
        Me.eGebindegroesse.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.eGebindegroesse.Location = New System.Drawing.Point(110, 61)
        Me.eGebindegroesse.Name = "eGebindegroesse"
        Me.eGebindegroesse.Size = New System.Drawing.Size(19, 13)
        Me.eGebindegroesse.TabIndex = 84
        Me.eGebindegroesse.Text = "kg"
        '
        'ePreis
        '
        Me.ePreis.AutoSize = True
        Me.ePreis.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ePreis.Location = New System.Drawing.Point(110, 23)
        Me.ePreis.Name = "ePreis"
        Me.ePreis.Size = New System.Drawing.Size(13, 13)
        Me.ePreis.TabIndex = 83
        Me.ePreis.Text = "€"
        '
        'tbMindestMenge
        '
        Me.tbMindestMenge.Location = New System.Drawing.Point(165, 97)
        Me.tbMindestMenge.Name = "tbMindestMenge"
        Me.tbMindestMenge.Size = New System.Drawing.Size(96, 20)
        Me.tbMindestMenge.TabIndex = 67
        Me.tbMindestMenge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbMindestMenge
        '
        Me.lbMindestMenge.AutoSize = True
        Me.lbMindestMenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbMindestMenge.Location = New System.Drawing.Point(162, 81)
        Me.lbMindestMenge.Name = "lbMindestMenge"
        Me.lbMindestMenge.Size = New System.Drawing.Size(76, 13)
        Me.lbMindestMenge.TabIndex = 82
        Me.lbMindestMenge.Text = "Mindestmenge"
        '
        'tbBilanzmenge
        '
        Me.tbBilanzmenge.Location = New System.Drawing.Point(12, 97)
        Me.tbBilanzmenge.Name = "tbBilanzmenge"
        Me.tbBilanzmenge.ReadOnly = True
        Me.tbBilanzmenge.Size = New System.Drawing.Size(96, 20)
        Me.tbBilanzmenge.TabIndex = 80
        Me.tbBilanzmenge.TabStop = False
        Me.tbBilanzmenge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbBilanzMenge
        '
        Me.lbBilanzMenge.AutoSize = True
        Me.lbBilanzMenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbBilanzMenge.Location = New System.Drawing.Point(9, 81)
        Me.lbBilanzMenge.Name = "lbBilanzMenge"
        Me.lbBilanzMenge.Size = New System.Drawing.Size(72, 13)
        Me.lbBilanzMenge.TabIndex = 81
        Me.lbBilanzMenge.Text = "Lagerbestand"
        '
        'cbRezeptGewicht
        '
        Me.cbRezeptGewicht.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbRezeptGewicht.Location = New System.Drawing.Point(165, 33)
        Me.cbRezeptGewicht.Name = "cbRezeptGewicht"
        Me.cbRezeptGewicht.Size = New System.Drawing.Size(160, 40)
        Me.cbRezeptGewicht.TabIndex = 79
        Me.cbRezeptGewicht.TabStop = False
        Me.cbRezeptGewicht.Text = "zählt nicht zur Rezept- Gesamtmenge"
        Me.cbRezeptGewicht.UseVisualStyleBackColor = True
        '
        'cbKeineDeklaration
        '
        Me.cbKeineDeklaration.AutoSize = True
        Me.cbKeineDeklaration.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbKeineDeklaration.Location = New System.Drawing.Point(165, 19)
        Me.cbKeineDeklaration.Name = "cbKeineDeklaration"
        Me.cbKeineDeklaration.Size = New System.Drawing.Size(160, 17)
        Me.cbKeineDeklaration.TabIndex = 78
        Me.cbKeineDeklaration.TabStop = False
        Me.cbKeineDeklaration.Text = "Rohstoff wird nicht deklariert"
        Me.cbKeineDeklaration.UseVisualStyleBackColor = True
        '
        'tbGebindeGroesse
        '
        Me.tbGebindeGroesse.Location = New System.Drawing.Point(12, 58)
        Me.tbGebindeGroesse.Name = "tbGebindeGroesse"
        Me.tbGebindeGroesse.Size = New System.Drawing.Size(96, 20)
        Me.tbGebindeGroesse.TabIndex = 66
        Me.tbGebindeGroesse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblGebindegroesse
        '
        Me.lblGebindegroesse.AutoSize = True
        Me.lblGebindegroesse.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblGebindegroesse.Location = New System.Drawing.Point(9, 42)
        Me.lblGebindegroesse.Name = "lblGebindegroesse"
        Me.lblGebindegroesse.Size = New System.Drawing.Size(74, 13)
        Me.lblGebindegroesse.TabIndex = 75
        Me.lblGebindegroesse.Text = "Gebindegröße"
        '
        'tbRohstoffPreis
        '
        Me.tbRohstoffPreis.Location = New System.Drawing.Point(12, 19)
        Me.tbRohstoffPreis.Name = "tbRohstoffPreis"
        Me.tbRohstoffPreis.Size = New System.Drawing.Size(96, 20)
        Me.tbRohstoffPreis.TabIndex = 65
        Me.tbRohstoffPreis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPreis
        '
        Me.lblPreis.AutoSize = True
        Me.lblPreis.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPreis.Location = New System.Drawing.Point(9, 3)
        Me.lblPreis.Name = "lblPreis"
        Me.lblPreis.Size = New System.Drawing.Size(47, 13)
        Me.lblPreis.TabIndex = 74
        Me.lblPreis.Text = "Preis/kg"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(9, 172)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 73
        Me.Label1.Text = "Gruppe 2"
        '
        'cbRohstoffGrp2
        '
        Me.cbRohstoffGrp2.FormattingEnabled = True
        Me.cbRohstoffGrp2.Location = New System.Drawing.Point(12, 188)
        Me.cbRohstoffGrp2.Name = "cbRohstoffGrp2"
        Me.cbRohstoffGrp2.Size = New System.Drawing.Size(290, 21)
        Me.cbRohstoffGrp2.TabIndex = 72
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label14.Location = New System.Drawing.Point(9, 129)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(51, 13)
        Me.Label14.TabIndex = 71
        Me.Label14.Text = "Gruppe 1"
        '
        'cbRohstoffGrp1
        '
        Me.cbRohstoffGrp1.FormattingEnabled = True
        Me.cbRohstoffGrp1.Location = New System.Drawing.Point(12, 148)
        Me.cbRohstoffGrp1.Name = "cbRohstoffGrp1"
        Me.cbRohstoffGrp1.Size = New System.Drawing.Size(290, 21)
        Me.cbRohstoffGrp1.TabIndex = 70
        '
        'tID
        '
        Me.tID.Location = New System.Drawing.Point(12, 63)
        Me.tID.Name = "tID"
        Me.tID.ReadOnly = True
        Me.tID.Size = New System.Drawing.Size(48, 20)
        Me.tID.TabIndex = 66
        Me.tID.TabStop = False
        '
        'tType
        '
        Me.tType.Location = New System.Drawing.Point(66, 63)
        Me.tType.Name = "tType"
        Me.tType.ReadOnly = True
        Me.tType.Size = New System.Drawing.Size(48, 20)
        Me.tType.TabIndex = 67
        Me.tType.TabStop = False
        '
        'cbAktiv
        '
        Me.cbAktiv.AutoSize = True
        Me.cbAktiv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbAktiv.Location = New System.Drawing.Point(128, 66)
        Me.cbAktiv.Name = "cbAktiv"
        Me.cbAktiv.Size = New System.Drawing.Size(15, 14)
        Me.cbAktiv.TabIndex = 79
        Me.cbAktiv.TabStop = False
        Me.cbAktiv.UseVisualStyleBackColor = True
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        Me.lblID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblID.Location = New System.Drawing.Point(9, 47)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(18, 13)
        Me.lblID.TabIndex = 80
        Me.lblID.Text = "ID"
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblType.Location = New System.Drawing.Point(65, 47)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(31, 13)
        Me.lblType.TabIndex = 81
        Me.lblType.Text = "Type"
        '
        'lblAktiv
        '
        Me.lblAktiv.AutoSize = True
        Me.lblAktiv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAktiv.Location = New System.Drawing.Point(125, 47)
        Me.lblAktiv.Name = "lblAktiv"
        Me.lblAktiv.Size = New System.Drawing.Size(31, 13)
        Me.lblAktiv.TabIndex = 82
        Me.lblAktiv.Text = "Aktiv"
        '
        'wb_Rohstoffe_Details
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(663, 413)
        Me.Controls.Add(Me.lblAktiv)
        Me.Controls.Add(Me.lblType)
        Me.Controls.Add(Me.lblID)
        Me.Controls.Add(Me.cbAktiv)
        Me.Controls.Add(Me.tType)
        Me.Controls.Add(Me.tID)
        Me.Controls.Add(Me.pnlDetails)
        Me.Controls.Add(Me.tRohstoffKommentar)
        Me.Controls.Add(Me.lblKommentar)
        Me.Controls.Add(Me.tRohstoffNummer)
        Me.Controls.Add(Me.lblNummer)
        Me.Controls.Add(Me.tRohstoffName)
        Me.Controls.Add(Me.lblBezeichnung)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "wb_Rohstoffe_Details"
        Me.Text = "Rohstoff Details"
        Me.pnlDetails.ResumeLayout(False)
        Me.pnlDetails.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tRohstoffName As Windows.Forms.TextBox
    Friend WithEvents lblBezeichnung As Windows.Forms.Label
    Friend WithEvents tRohstoffNummer As Windows.Forms.TextBox
    Friend WithEvents lblNummer As Windows.Forms.Label
    Friend WithEvents tRohstoffKommentar As Windows.Forms.TextBox
    Friend WithEvents lblKommentar As Windows.Forms.Label
    Friend WithEvents pnlDetails As Windows.Forms.Panel
    Friend WithEvents eMindestmenge As Windows.Forms.Label
    Friend WithEvents eBilanzmenge As Windows.Forms.Label
    Friend WithEvents eGebindegroesse As Windows.Forms.Label
    Friend WithEvents ePreis As Windows.Forms.Label
    Friend WithEvents tbMindestMenge As Windows.Forms.TextBox
    Friend WithEvents lbMindestMenge As Windows.Forms.Label
    Friend WithEvents tbBilanzmenge As Windows.Forms.TextBox
    Friend WithEvents lbBilanzMenge As Windows.Forms.Label
    Friend WithEvents cbRezeptGewicht As Windows.Forms.CheckBox
    Friend WithEvents cbKeineDeklaration As Windows.Forms.CheckBox
    Friend WithEvents lblDeklIntern As Windows.Forms.Label
    Friend WithEvents lblDeklExtern As Windows.Forms.Label
    Friend WithEvents tbDeklarationIntern As Windows.Forms.TextBox
    Friend WithEvents tbDeklarationExtern As Windows.Forms.TextBox
    Friend WithEvents tbGebindeGroesse As Windows.Forms.TextBox
    Friend WithEvents lblGebindegroesse As Windows.Forms.Label
    Friend WithEvents tbRohstoffPreis As Windows.Forms.TextBox
    Friend WithEvents lblPreis As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents cbRohstoffGrp2 As wb_ComboBox
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents cbRohstoffGrp1 As wb_ComboBox
    Friend WithEvents TableLayoutPanel1 As Windows.Forms.TableLayoutPanel
    Friend WithEvents tID As Windows.Forms.TextBox
    Friend WithEvents tType As Windows.Forms.TextBox
    Friend WithEvents cbAktiv As Windows.Forms.CheckBox
    Friend WithEvents lblID As Windows.Forms.Label
    Friend WithEvents lblType As Windows.Forms.Label
    Friend WithEvents lblAktiv As Windows.Forms.Label
    Friend WithEvents cbAufloesen As Windows.Forms.CheckBox
    Friend WithEvents cbInterneDeklaration As Windows.Forms.CheckBox
    Friend WithEvents tbRezName As Windows.Forms.TextBox
    Friend WithEvents tbRezNr As Windows.Forms.TextBox
    Friend WithEvents lblRezept As Windows.Forms.Label
    Friend WithEvents lbRezNr As Windows.Forms.Label
End Class
