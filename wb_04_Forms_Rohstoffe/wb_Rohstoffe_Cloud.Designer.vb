Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rohstoffe_Cloud
    Inherits DockContent
    'Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Rohstoffe_Cloud))
        Dim Wb_MinMaxOptCharge1 As WinBack.wb_MinMaxOptCharge = New WinBack.wb_MinMaxOptCharge()
        Dim Wb_Charge1 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge2 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge3 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_MinMaxOptCharge2 As WinBack.wb_MinMaxOptCharge = New WinBack.wb_MinMaxOptCharge()
        Dim Wb_Charge4 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge5 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge6 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Me.tRohstoffNummer = New System.Windows.Forms.TextBox()
        Me.lblNummer = New System.Windows.Forms.Label()
        Me.tRohstoffName = New System.Windows.Forms.TextBox()
        Me.lblBezeichnung = New System.Windows.Forms.Label()
        Me.tCloudID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Wb_TabControl = New WinBack.wb_TabControl()
        Me.tpCloudSuchen = New System.Windows.Forms.TabPage()
        Me.lblHilfeText = New System.Windows.Forms.Label()
        Me.tSuchtextLieferant = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tSuchtextBezeichnung = New System.Windows.Forms.TextBox()
        Me.lblSuchtextBezeichnung = New System.Windows.Forms.Label()
        Me.btnDatenLink = New System.Windows.Forms.Button()
        Me.btnCloud = New System.Windows.Forms.Button()
        Me.tpCloudGefunden = New System.Windows.Forms.TabPage()
        Me.pnlNwtGrid = New System.Windows.Forms.Panel()
        Me.btnFound_Back = New System.Windows.Forms.Button()
        Me.btnFound_Vor = New System.Windows.Forms.Button()
        Me.lblErgebnisText = New System.Windows.Forms.Label()
        Me.tpCloudAnzeige = New System.Windows.Forms.TabPage()
        Me.btnShow_Back = New System.Windows.Forms.Button()
        Me.btnShow_Vor = New System.Windows.Forms.Button()
        Me.pnlNwt = New System.Windows.Forms.Panel()
        Me.tpCloudResult = New System.Windows.Forms.TabPage()
        Me.btnResult_Akt = New System.Windows.Forms.Button()
        Me.btnDisconnect = New System.Windows.Forms.Button()
        Me.tpRezept = New System.Windows.Forms.TabPage()
        Me.KompRzChargen = New WinBack.wb_KompRzChargen()
        Me.btnResult_OK = New System.Windows.Forms.Button()
        Me.Wb_TabControl.SuspendLayout()
        Me.tpCloudSuchen.SuspendLayout()
        Me.tpCloudGefunden.SuspendLayout()
        Me.tpCloudAnzeige.SuspendLayout()
        Me.tpCloudResult.SuspendLayout()
        Me.tpRezept.SuspendLayout()
        Me.SuspendLayout()
        '
        'tRohstoffNummer
        '
        Me.tRohstoffNummer.Location = New System.Drawing.Point(20, 27)
        Me.tRohstoffNummer.Name = "tRohstoffNummer"
        Me.tRohstoffNummer.ReadOnly = True
        Me.tRohstoffNummer.Size = New System.Drawing.Size(136, 20)
        Me.tRohstoffNummer.TabIndex = 47
        '
        'lblNummer
        '
        Me.lblNummer.AutoSize = True
        Me.lblNummer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNummer.Location = New System.Drawing.Point(22, 11)
        Me.lblNummer.Name = "lblNummer"
        Me.lblNummer.Size = New System.Drawing.Size(46, 13)
        Me.lblNummer.TabIndex = 48
        Me.lblNummer.Text = "Nummer"
        '
        'tRohstoffName
        '
        Me.tRohstoffName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tRohstoffName.Location = New System.Drawing.Point(164, 27)
        Me.tRohstoffName.Name = "tRohstoffName"
        Me.tRohstoffName.ReadOnly = True
        Me.tRohstoffName.Size = New System.Drawing.Size(525, 20)
        Me.tRohstoffName.TabIndex = 1
        '
        'lblBezeichnung
        '
        Me.lblBezeichnung.AutoSize = True
        Me.lblBezeichnung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblBezeichnung.Location = New System.Drawing.Point(166, 11)
        Me.lblBezeichnung.Name = "lblBezeichnung"
        Me.lblBezeichnung.Size = New System.Drawing.Size(112, 13)
        Me.lblBezeichnung.TabIndex = 46
        Me.lblBezeichnung.Text = "Rohstoff-Bezeichnung"
        '
        'tCloudID
        '
        Me.tCloudID.Location = New System.Drawing.Point(20, 66)
        Me.tCloudID.Name = "tCloudID"
        Me.tCloudID.ReadOnly = True
        Me.tCloudID.Size = New System.Drawing.Size(136, 20)
        Me.tCloudID.TabIndex = 52
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(21, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Cloud-ID"
        '
        'Wb_TabControl
        '
        Me.Wb_TabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Wb_TabControl.Controls.Add(Me.tpCloudSuchen)
        Me.Wb_TabControl.Controls.Add(Me.tpCloudGefunden)
        Me.Wb_TabControl.Controls.Add(Me.tpCloudAnzeige)
        Me.Wb_TabControl.Controls.Add(Me.tpCloudResult)
        Me.Wb_TabControl.Controls.Add(Me.tpRezept)
        Me.Wb_TabControl.Location = New System.Drawing.Point(1, 92)
        Me.Wb_TabControl.Multiline = True
        Me.Wb_TabControl.Name = "Wb_TabControl"
        Me.Wb_TabControl.SelectedIndex = 0
        Me.Wb_TabControl.Size = New System.Drawing.Size(732, 379)
        Me.Wb_TabControl.TabIndex = 0
        '
        'tpCloudSuchen
        '
        Me.tpCloudSuchen.BackColor = System.Drawing.SystemColors.Control
        Me.tpCloudSuchen.Controls.Add(Me.lblHilfeText)
        Me.tpCloudSuchen.Controls.Add(Me.tSuchtextLieferant)
        Me.tpCloudSuchen.Controls.Add(Me.Label1)
        Me.tpCloudSuchen.Controls.Add(Me.tSuchtextBezeichnung)
        Me.tpCloudSuchen.Controls.Add(Me.lblSuchtextBezeichnung)
        Me.tpCloudSuchen.Controls.Add(Me.btnDatenLink)
        Me.tpCloudSuchen.Controls.Add(Me.btnCloud)
        Me.tpCloudSuchen.Location = New System.Drawing.Point(4, 23)
        Me.tpCloudSuchen.Name = "tpCloudSuchen"
        Me.tpCloudSuchen.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCloudSuchen.Size = New System.Drawing.Size(724, 352)
        Me.tpCloudSuchen.TabIndex = 0
        Me.tpCloudSuchen.Text = "CloudSearch"
        '
        'lblHilfeText
        '
        Me.lblHilfeText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHilfeText.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblHilfeText.Location = New System.Drawing.Point(11, 128)
        Me.lblHilfeText.Name = "lblHilfeText"
        Me.lblHilfeText.Size = New System.Drawing.Size(477, 219)
        Me.lblHilfeText.TabIndex = 51
        Me.lblHilfeText.Text = "Hilfetext"
        '
        'tSuchtextLieferant
        '
        Me.tSuchtextLieferant.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tSuchtextLieferant.Location = New System.Drawing.Point(11, 67)
        Me.tSuchtextLieferant.Name = "tSuchtextLieferant"
        Me.tSuchtextLieferant.Size = New System.Drawing.Size(477, 20)
        Me.tSuchtextLieferant.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(8, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(148, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Suchtext (Hersteller/Lieferant)"
        '
        'tSuchtextBezeichnung
        '
        Me.tSuchtextBezeichnung.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tSuchtextBezeichnung.Location = New System.Drawing.Point(11, 28)
        Me.tSuchtextBezeichnung.Name = "tSuchtextBezeichnung"
        Me.tSuchtextBezeichnung.Size = New System.Drawing.Size(477, 20)
        Me.tSuchtextBezeichnung.TabIndex = 1
        '
        'lblSuchtextBezeichnung
        '
        Me.lblSuchtextBezeichnung.AutoSize = True
        Me.lblSuchtextBezeichnung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblSuchtextBezeichnung.Location = New System.Drawing.Point(8, 12)
        Me.lblSuchtextBezeichnung.Name = "lblSuchtextBezeichnung"
        Me.lblSuchtextBezeichnung.Size = New System.Drawing.Size(163, 13)
        Me.lblSuchtextBezeichnung.TabIndex = 48
        Me.lblSuchtextBezeichnung.Text = "Suchtext (Rohstoff-Bezeichnung)"
        '
        'btnDatenLink
        '
        Me.btnDatenLink.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDatenLink.Image = CType(resources.GetObject("btnDatenLink.Image"), System.Drawing.Image)
        Me.btnDatenLink.Location = New System.Drawing.Point(541, 97)
        Me.btnDatenLink.Name = "btnDatenLink"
        Me.btnDatenLink.Size = New System.Drawing.Size(138, 62)
        Me.btnDatenLink.TabIndex = 4
        Me.btnDatenLink.Text = "Datenlink"
        Me.btnDatenLink.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDatenLink.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnDatenLink.UseVisualStyleBackColor = True
        '
        'btnCloud
        '
        Me.btnCloud.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCloud.Image = Global.WinBack.My.Resources.Resources.RohstoffeCloud_32x32
        Me.btnCloud.Location = New System.Drawing.Point(541, 6)
        Me.btnCloud.Name = "btnCloud"
        Me.btnCloud.Size = New System.Drawing.Size(138, 62)
        Me.btnCloud.TabIndex = 3
        Me.btnCloud.Text = "WinBack Cloud"
        Me.btnCloud.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCloud.UseVisualStyleBackColor = True
        '
        'tpCloudGefunden
        '
        Me.tpCloudGefunden.BackColor = System.Drawing.SystemColors.Control
        Me.tpCloudGefunden.Controls.Add(Me.pnlNwtGrid)
        Me.tpCloudGefunden.Controls.Add(Me.btnFound_Back)
        Me.tpCloudGefunden.Controls.Add(Me.btnFound_Vor)
        Me.tpCloudGefunden.Controls.Add(Me.lblErgebnisText)
        Me.tpCloudGefunden.Location = New System.Drawing.Point(4, 23)
        Me.tpCloudGefunden.Name = "tpCloudGefunden"
        Me.tpCloudGefunden.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCloudGefunden.Size = New System.Drawing.Size(724, 352)
        Me.tpCloudGefunden.TabIndex = 1
        Me.tpCloudGefunden.Text = "CloudFound"
        '
        'pnlNwtGrid
        '
        Me.pnlNwtGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlNwtGrid.Location = New System.Drawing.Point(11, 52)
        Me.pnlNwtGrid.Name = "pnlNwtGrid"
        Me.pnlNwtGrid.Size = New System.Drawing.Size(667, 254)
        Me.pnlNwtGrid.TabIndex = 55
        '
        'btnFound_Back
        '
        Me.btnFound_Back.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFound_Back.Location = New System.Drawing.Point(397, 312)
        Me.btnFound_Back.Name = "btnFound_Back"
        Me.btnFound_Back.Size = New System.Drawing.Size(138, 32)
        Me.btnFound_Back.TabIndex = 54
        Me.btnFound_Back.Text = "Zurück"
        Me.btnFound_Back.UseVisualStyleBackColor = True
        '
        'btnFound_Vor
        '
        Me.btnFound_Vor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFound_Vor.Location = New System.Drawing.Point(541, 312)
        Me.btnFound_Vor.Name = "btnFound_Vor"
        Me.btnFound_Vor.Size = New System.Drawing.Size(138, 32)
        Me.btnFound_Vor.TabIndex = 53
        Me.btnFound_Vor.Text = "Weiter"
        Me.btnFound_Vor.UseVisualStyleBackColor = True
        '
        'lblErgebnisText
        '
        Me.lblErgebnisText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblErgebnisText.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblErgebnisText.Location = New System.Drawing.Point(11, 16)
        Me.lblErgebnisText.Name = "lblErgebnisText"
        Me.lblErgebnisText.Size = New System.Drawing.Size(705, 70)
        Me.lblErgebnisText.TabIndex = 52
        Me.lblErgebnisText.Text = "Result"
        '
        'tpCloudAnzeige
        '
        Me.tpCloudAnzeige.BackColor = System.Drawing.SystemColors.Control
        Me.tpCloudAnzeige.Controls.Add(Me.btnShow_Back)
        Me.tpCloudAnzeige.Controls.Add(Me.btnShow_Vor)
        Me.tpCloudAnzeige.Controls.Add(Me.pnlNwt)
        Me.tpCloudAnzeige.Location = New System.Drawing.Point(4, 23)
        Me.tpCloudAnzeige.Name = "tpCloudAnzeige"
        Me.tpCloudAnzeige.Size = New System.Drawing.Size(724, 352)
        Me.tpCloudAnzeige.TabIndex = 2
        Me.tpCloudAnzeige.Text = "CloudShow"
        '
        'btnShow_Back
        '
        Me.btnShow_Back.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShow_Back.Location = New System.Drawing.Point(397, 312)
        Me.btnShow_Back.Name = "btnShow_Back"
        Me.btnShow_Back.Size = New System.Drawing.Size(138, 32)
        Me.btnShow_Back.TabIndex = 58
        Me.btnShow_Back.Text = "Zurück"
        Me.btnShow_Back.UseVisualStyleBackColor = True
        '
        'btnShow_Vor
        '
        Me.btnShow_Vor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShow_Vor.Location = New System.Drawing.Point(541, 312)
        Me.btnShow_Vor.Name = "btnShow_Vor"
        Me.btnShow_Vor.Size = New System.Drawing.Size(138, 32)
        Me.btnShow_Vor.TabIndex = 57
        Me.btnShow_Vor.Text = "Weiter"
        Me.btnShow_Vor.UseVisualStyleBackColor = True
        '
        'pnlNwt
        '
        Me.pnlNwt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlNwt.Location = New System.Drawing.Point(11, 3)
        Me.pnlNwt.Name = "pnlNwt"
        Me.pnlNwt.Size = New System.Drawing.Size(667, 303)
        Me.pnlNwt.TabIndex = 56
        '
        'tpCloudResult
        '
        Me.tpCloudResult.BackColor = System.Drawing.SystemColors.Control
        Me.tpCloudResult.Controls.Add(Me.btnResult_OK)
        Me.tpCloudResult.Controls.Add(Me.btnResult_Akt)
        Me.tpCloudResult.Controls.Add(Me.btnDisconnect)
        Me.tpCloudResult.Location = New System.Drawing.Point(4, 23)
        Me.tpCloudResult.Name = "tpCloudResult"
        Me.tpCloudResult.Size = New System.Drawing.Size(724, 352)
        Me.tpCloudResult.TabIndex = 3
        Me.tpCloudResult.Text = "CloudResult"
        '
        'btnResult_Akt
        '
        Me.btnResult_Akt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnResult_Akt.Image = Global.WinBack.My.Resources.Resources.HakenGrn_16x16
        Me.btnResult_Akt.Location = New System.Drawing.Point(541, 6)
        Me.btnResult_Akt.Name = "btnResult_Akt"
        Me.btnResult_Akt.Size = New System.Drawing.Size(138, 62)
        Me.btnResult_Akt.TabIndex = 5
        Me.btnResult_Akt.Text = "Daten aus der Cloud aktualisieren"
        Me.btnResult_Akt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnResult_Akt.UseVisualStyleBackColor = True
        '
        'btnDisconnect
        '
        Me.btnDisconnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDisconnect.Image = Global.WinBack.My.Resources.Resources.IconDelete_24x24
        Me.btnDisconnect.Location = New System.Drawing.Point(541, 97)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(138, 62)
        Me.btnDisconnect.TabIndex = 4
        Me.btnDisconnect.Text = "Verknüpfung zur Cloud löschen"
        Me.btnDisconnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnDisconnect.UseVisualStyleBackColor = True
        '
        'tpRezept
        '
        Me.tpRezept.BackColor = System.Drawing.SystemColors.Control
        Me.tpRezept.Controls.Add(Me.KompRzChargen)
        Me.tpRezept.Location = New System.Drawing.Point(4, 23)
        Me.tpRezept.Name = "tpRezept"
        Me.tpRezept.Size = New System.Drawing.Size(724, 352)
        Me.tpRezept.TabIndex = 4
        Me.tpRezept.Text = "Rezept"
        '
        'KompRzChargen
        '
        Wb_MinMaxOptCharge1.HasChanged = False
        Wb_Charge1.MengeInkg = "0,000"
        Wb_Charge1.MengeInProzent = "0"
        Wb_Charge1.MengeInStk = "0"
        Wb_Charge1.StkGewicht = "1000"
        Wb_Charge1.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge1.MaxCharge = Wb_Charge1
        Wb_Charge2.MengeInkg = "0,000"
        Wb_Charge2.MengeInProzent = "0"
        Wb_Charge2.MengeInStk = "0"
        Wb_Charge2.StkGewicht = "1000"
        Wb_Charge2.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge1.MinCharge = Wb_Charge2
        Wb_Charge3.MengeInkg = "0,000"
        Wb_Charge3.MengeInProzent = "0"
        Wb_Charge3.MengeInStk = "0"
        Wb_Charge3.StkGewicht = "1000"
        Wb_Charge3.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge1.OptCharge = Wb_Charge3
        Wb_MinMaxOptCharge1.StkGewicht = "0"
        Wb_MinMaxOptCharge1.TeigGewicht = "0"
        Me.KompRzChargen.ArtikelChargen = Wb_MinMaxOptCharge1
        Me.KompRzChargen.ArtikelLiniengruppe = 0
        Me.KompRzChargen.DataValid = False
        Me.KompRzChargen.LinienGruppe = 0
        Me.KompRzChargen.Location = New System.Drawing.Point(7, 0)
        Me.KompRzChargen.Name = "KompRzChargen"
        Me.KompRzChargen.RezeptName = ""
        Me.KompRzChargen.RezeptNummer = ""
        Me.KompRzChargen.RzNr = -1
        Me.KompRzChargen.Size = New System.Drawing.Size(400, 315)
        Me.KompRzChargen.TabIndex = 0
        Wb_MinMaxOptCharge2.HasChanged = False
        Wb_Charge4.MengeInkg = "0,000"
        Wb_Charge4.MengeInProzent = "0"
        Wb_Charge4.MengeInStk = "0"
        Wb_Charge4.StkGewicht = "1000"
        Wb_Charge4.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge2.MaxCharge = Wb_Charge4
        Wb_Charge5.MengeInkg = "0,000"
        Wb_Charge5.MengeInProzent = "0"
        Wb_Charge5.MengeInStk = "0"
        Wb_Charge5.StkGewicht = "1000"
        Wb_Charge5.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge2.MinCharge = Wb_Charge5
        Wb_Charge6.MengeInkg = "0,000"
        Wb_Charge6.MengeInProzent = "0"
        Wb_Charge6.MengeInStk = "0"
        Wb_Charge6.StkGewicht = "1000"
        Wb_Charge6.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge2.OptCharge = Wb_Charge6
        Wb_MinMaxOptCharge2.StkGewicht = "0"
        Wb_MinMaxOptCharge2.TeigGewicht = "0"
        Me.KompRzChargen.TeigChargen = Wb_MinMaxOptCharge2
        '
        'btnResult_OK
        '
        Me.btnResult_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnResult_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnResult_OK.Location = New System.Drawing.Point(541, 312)
        Me.btnResult_OK.Name = "btnResult_OK"
        Me.btnResult_OK.Size = New System.Drawing.Size(138, 32)
        Me.btnResult_OK.TabIndex = 1
        Me.btnResult_OK.Text = "OK"
        Me.btnResult_OK.UseVisualStyleBackColor = True
        '
        'wb_Rohstoffe_Cloud
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(733, 471)
        Me.Controls.Add(Me.tCloudID)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tRohstoffNummer)
        Me.Controls.Add(Me.lblNummer)
        Me.Controls.Add(Me.tRohstoffName)
        Me.Controls.Add(Me.lblBezeichnung)
        Me.Controls.Add(Me.Wb_TabControl)
        Me.Name = "wb_Rohstoffe_Cloud"
        Me.Text = "Nährwerte Rohstoff"
        Me.Wb_TabControl.ResumeLayout(False)
        Me.tpCloudSuchen.ResumeLayout(False)
        Me.tpCloudSuchen.PerformLayout()
        Me.tpCloudGefunden.ResumeLayout(False)
        Me.tpCloudAnzeige.ResumeLayout(False)
        Me.tpCloudResult.ResumeLayout(False)
        Me.tpRezept.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Wb_TabControl As wb_TabControl
    Friend WithEvents tpCloudSuchen As Windows.Forms.TabPage
    Friend WithEvents tpCloudGefunden As Windows.Forms.TabPage
    Friend WithEvents tpCloudAnzeige As Windows.Forms.TabPage
    Friend WithEvents tpCloudResult As Windows.Forms.TabPage
    Friend WithEvents btnCloud As Windows.Forms.Button
    Friend WithEvents btnDatenLink As Windows.Forms.Button
    Friend WithEvents tSuchtextBezeichnung As Windows.Forms.TextBox
    Friend WithEvents lblSuchtextBezeichnung As Windows.Forms.Label
    Friend WithEvents tRohstoffNummer As Windows.Forms.TextBox
    Friend WithEvents lblNummer As Windows.Forms.Label
    Friend WithEvents tRohstoffName As Windows.Forms.TextBox
    Friend WithEvents lblBezeichnung As Windows.Forms.Label
    Friend WithEvents tSuchtextLieferant As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents lblHilfeText As Windows.Forms.Label
    Friend WithEvents pnlNwtGrid As Windows.Forms.Panel
    Friend WithEvents btnFound_Back As Windows.Forms.Button
    Friend WithEvents btnFound_Vor As Windows.Forms.Button
    Friend WithEvents lblErgebnisText As Windows.Forms.Label
    Friend WithEvents btnShow_Back As Windows.Forms.Button
    Friend WithEvents btnShow_Vor As Windows.Forms.Button
    Friend WithEvents pnlNwt As Windows.Forms.Panel
    Friend WithEvents btnResult_Akt As Windows.Forms.Button
    Friend WithEvents btnDisconnect As Windows.Forms.Button
    Friend WithEvents tCloudID As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tpRezept As Windows.Forms.TabPage
    Friend WithEvents KompRzChargen As wb_KompRzChargen
    Friend WithEvents btnResult_OK As Windows.Forms.Button
End Class
