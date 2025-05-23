﻿Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rohstoffe_Cloud
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
        Me.lblCloudID = New System.Windows.Forms.Label()
        Me.Wb_TabControl = New WinBack.wb_TabControl()
        Me.tpCloudSuchen = New System.Windows.Forms.TabPage()
        Me.BtnOpenFoodFacts = New System.Windows.Forms.Button()
        Me.BtnRezept = New System.Windows.Forms.Button()
        Me.btnMail = New System.Windows.Forms.Button()
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
        Me.BtnProduktDatenblatt = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDeklExtern = New System.Windows.Forms.Label()
        Me.lblDeklIntern = New System.Windows.Forms.Label()
        Me.tbDeklarationExtern = New System.Windows.Forms.TextBox()
        Me.tbDeklarationIntern = New System.Windows.Forms.TextBox()
        Me.Btn_Result_Back = New System.Windows.Forms.Button()
        Me.btnResult_OK = New System.Windows.Forms.Button()
        Me.btnResult_Akt = New System.Windows.Forms.Button()
        Me.btnDisconnect = New System.Windows.Forms.Button()
        Me.tpRezept = New System.Windows.Forms.TabPage()
        Me.KompRzChargen = New WinBack.wb_KompRzChargen()
        Me.tpKompType = New System.Windows.Forms.TabPage()
        Me.lblKeineNwt = New System.Windows.Forms.Label()
        Me.cbFreigabeProduktion = New System.Windows.Forms.CheckBox()
        Me.Wb_TabControl.SuspendLayout()
        Me.tpCloudSuchen.SuspendLayout()
        Me.tpCloudGefunden.SuspendLayout()
        Me.tpCloudAnzeige.SuspendLayout()
        Me.tpCloudResult.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.tpRezept.SuspendLayout()
        Me.tpKompType.SuspendLayout()
        Me.SuspendLayout()
        '
        'tRohstoffNummer
        '
        Me.tRohstoffNummer.Location = New System.Drawing.Point(12, 24)
        Me.tRohstoffNummer.Name = "tRohstoffNummer"
        Me.tRohstoffNummer.ReadOnly = True
        Me.tRohstoffNummer.Size = New System.Drawing.Size(136, 20)
        Me.tRohstoffNummer.TabIndex = 47
        '
        'lblNummer
        '
        Me.lblNummer.AutoSize = True
        Me.lblNummer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNummer.Location = New System.Drawing.Point(15, 8)
        Me.lblNummer.Name = "lblNummer"
        Me.lblNummer.Size = New System.Drawing.Size(46, 13)
        Me.lblNummer.TabIndex = 48
        Me.lblNummer.Text = "Nummer"
        '
        'tRohstoffName
        '
        Me.tRohstoffName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tRohstoffName.Location = New System.Drawing.Point(165, 24)
        Me.tRohstoffName.Name = "tRohstoffName"
        Me.tRohstoffName.ReadOnly = True
        Me.tRohstoffName.Size = New System.Drawing.Size(556, 20)
        Me.tRohstoffName.TabIndex = 1
        '
        'lblBezeichnung
        '
        Me.lblBezeichnung.AutoSize = True
        Me.lblBezeichnung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblBezeichnung.Location = New System.Drawing.Point(165, 8)
        Me.lblBezeichnung.Name = "lblBezeichnung"
        Me.lblBezeichnung.Size = New System.Drawing.Size(112, 13)
        Me.lblBezeichnung.TabIndex = 46
        Me.lblBezeichnung.Text = "Rohstoff-Bezeichnung"
        '
        'tCloudID
        '
        Me.tCloudID.Location = New System.Drawing.Point(12, 63)
        Me.tCloudID.Name = "tCloudID"
        Me.tCloudID.ReadOnly = True
        Me.tCloudID.Size = New System.Drawing.Size(136, 20)
        Me.tCloudID.TabIndex = 52
        '
        'lblCloudID
        '
        Me.lblCloudID.AutoSize = True
        Me.lblCloudID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCloudID.Location = New System.Drawing.Point(14, 47)
        Me.lblCloudID.Name = "lblCloudID"
        Me.lblCloudID.Size = New System.Drawing.Size(48, 13)
        Me.lblCloudID.TabIndex = 53
        Me.lblCloudID.Text = "Cloud-ID"
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
        Me.Wb_TabControl.Controls.Add(Me.tpKompType)
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
        Me.tpCloudSuchen.Controls.Add(Me.BtnOpenFoodFacts)
        Me.tpCloudSuchen.Controls.Add(Me.BtnRezept)
        Me.tpCloudSuchen.Controls.Add(Me.btnMail)
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
        'BtnOpenFoodFacts
        '
        Me.BtnOpenFoodFacts.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOpenFoodFacts.Image = Global.WinBack.My.Resources.Resources.RohstoffeOpenFoodFacts
        Me.BtnOpenFoodFacts.Location = New System.Drawing.Point(541, 142)
        Me.BtnOpenFoodFacts.Name = "BtnOpenFoodFacts"
        Me.BtnOpenFoodFacts.Size = New System.Drawing.Size(138, 62)
        Me.BtnOpenFoodFacts.TabIndex = 54
        Me.BtnOpenFoodFacts.Text = "OpenFoodFacts"
        Me.BtnOpenFoodFacts.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnOpenFoodFacts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnOpenFoodFacts.UseVisualStyleBackColor = True
        '
        'BtnRezept
        '
        Me.BtnRezept.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnRezept.Image = Global.WinBack.My.Resources.Resources.RezeptListe_32x32
        Me.BtnRezept.Location = New System.Drawing.Point(541, 278)
        Me.BtnRezept.Name = "BtnRezept"
        Me.BtnRezept.Size = New System.Drawing.Size(138, 62)
        Me.BtnRezept.TabIndex = 53
        Me.BtnRezept.Text = "mit Rezept verknüpfen"
        Me.BtnRezept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnRezept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnRezept.UseVisualStyleBackColor = True
        '
        'btnMail
        '
        Me.btnMail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMail.Image = Global.WinBack.My.Resources.Resources.RohstoffeMail_32x32
        Me.btnMail.Location = New System.Drawing.Point(541, 210)
        Me.btnMail.Name = "btnMail"
        Me.btnMail.Size = New System.Drawing.Size(138, 62)
        Me.btnMail.TabIndex = 52
        Me.btnMail.Text = "Anforderung (Mail)"
        Me.btnMail.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnMail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnMail.UseVisualStyleBackColor = True
        '
        'lblHilfeText
        '
        Me.lblHilfeText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHilfeText.ForeColor = System.Drawing.Color.Black
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
        Me.btnDatenLink.Image = Global.WinBack.My.Resources.Resources.RohstoffeDatenLink_93x24
        Me.btnDatenLink.Location = New System.Drawing.Point(541, 74)
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
        Me.tpCloudResult.Controls.Add(Me.BtnProduktDatenblatt)
        Me.tpCloudResult.Controls.Add(Me.TableLayoutPanel1)
        Me.tpCloudResult.Controls.Add(Me.Btn_Result_Back)
        Me.tpCloudResult.Controls.Add(Me.btnResult_OK)
        Me.tpCloudResult.Controls.Add(Me.btnResult_Akt)
        Me.tpCloudResult.Controls.Add(Me.btnDisconnect)
        Me.tpCloudResult.Location = New System.Drawing.Point(4, 23)
        Me.tpCloudResult.Name = "tpCloudResult"
        Me.tpCloudResult.Size = New System.Drawing.Size(724, 352)
        Me.tpCloudResult.TabIndex = 3
        Me.tpCloudResult.Text = "CloudResult"
        '
        'BtnProduktDatenblatt
        '
        Me.BtnProduktDatenblatt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnProduktDatenblatt.Image = Global.WinBack.My.Resources.Resources.MainRezept_32x32
        Me.BtnProduktDatenblatt.Location = New System.Drawing.Point(541, 213)
        Me.BtnProduktDatenblatt.Name = "BtnProduktDatenblatt"
        Me.BtnProduktDatenblatt.Size = New System.Drawing.Size(138, 62)
        Me.BtnProduktDatenblatt.TabIndex = 61
        Me.BtnProduktDatenblatt.Text = "Produkt-Datenblätter"
        Me.BtnProduktDatenblatt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnProduktDatenblatt.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblDeklExtern, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblDeklIntern, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.tbDeklarationExtern, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.tbDeklarationIntern, 0, 3)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(7, 6)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(528, 300)
        Me.TableLayoutPanel1.TabIndex = 60
        '
        'lblDeklExtern
        '
        Me.lblDeklExtern.AutoSize = True
        Me.lblDeklExtern.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDeklExtern.Location = New System.Drawing.Point(3, 0)
        Me.lblDeklExtern.Name = "lblDeklExtern"
        Me.lblDeklExtern.Size = New System.Drawing.Size(61, 13)
        Me.lblDeklExtern.TabIndex = 56
        Me.lblDeklExtern.Text = "Deklaration"
        '
        'lblDeklIntern
        '
        Me.lblDeklIntern.AutoSize = True
        Me.lblDeklIntern.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDeklIntern.Location = New System.Drawing.Point(3, 150)
        Me.lblDeklIntern.Name = "lblDeklIntern"
        Me.lblDeklIntern.Size = New System.Drawing.Size(90, 13)
        Me.lblDeklIntern.TabIndex = 53
        Me.lblDeklIntern.Text = "Deklaration intern"
        '
        'tbDeklarationExtern
        '
        Me.tbDeklarationExtern.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbDeklarationExtern.Location = New System.Drawing.Point(3, 25)
        Me.tbDeklarationExtern.Multiline = True
        Me.tbDeklarationExtern.Name = "tbDeklarationExtern"
        Me.tbDeklarationExtern.Size = New System.Drawing.Size(522, 122)
        Me.tbDeklarationExtern.TabIndex = 54
        '
        'tbDeklarationIntern
        '
        Me.tbDeklarationIntern.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbDeklarationIntern.Location = New System.Drawing.Point(3, 175)
        Me.tbDeklarationIntern.Multiline = True
        Me.tbDeklarationIntern.Name = "tbDeklarationIntern"
        Me.tbDeklarationIntern.Size = New System.Drawing.Size(522, 122)
        Me.tbDeklarationIntern.TabIndex = 52
        '
        'Btn_Result_Back
        '
        Me.Btn_Result_Back.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Result_Back.Location = New System.Drawing.Point(397, 312)
        Me.Btn_Result_Back.Name = "Btn_Result_Back"
        Me.Btn_Result_Back.Size = New System.Drawing.Size(138, 32)
        Me.Btn_Result_Back.TabIndex = 59
        Me.Btn_Result_Back.Text = "Zurück"
        Me.Btn_Result_Back.UseVisualStyleBackColor = True
        '
        'btnResult_OK
        '
        Me.btnResult_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnResult_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnResult_OK.Location = New System.Drawing.Point(541, 312)
        Me.btnResult_OK.Name = "btnResult_OK"
        Me.btnResult_OK.Size = New System.Drawing.Size(138, 32)
        Me.btnResult_OK.TabIndex = 1
        Me.btnResult_OK.Text = "Speichern"
        Me.btnResult_OK.UseVisualStyleBackColor = True
        '
        'btnResult_Akt
        '
        Me.btnResult_Akt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnResult_Akt.Image = Global.WinBack.My.Resources.Resources.HakenGrn_16x16
        Me.btnResult_Akt.Location = New System.Drawing.Point(541, 29)
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
        Me.btnDisconnect.Location = New System.Drawing.Point(541, 122)
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
        Wb_MinMaxOptCharge1.ErrorCheck = False
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
        Wb_MinMaxOptCharge1.StkGewicht = "1000"
        Wb_MinMaxOptCharge1.TeigGewicht = "0"
        Me.KompRzChargen.ArtikelChargen = Wb_MinMaxOptCharge1
        Me.KompRzChargen.DataValid = False
        Me.KompRzChargen.ID = "-1"
        Me.KompRzChargen.Location = New System.Drawing.Point(7, 2)
        Me.KompRzChargen.Name = "KompRzChargen"
        Me.KompRzChargen.RezeptName = ""
        Me.KompRzChargen.RezeptNummer = ""
        Me.KompRzChargen.RzNr = -1
        Me.KompRzChargen.Size = New System.Drawing.Size(400, 342)
        Me.KompRzChargen.TabIndex = 0
        Wb_MinMaxOptCharge2.ErrorCheck = False
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
        Wb_MinMaxOptCharge2.StkGewicht = "1000"
        Wb_MinMaxOptCharge2.TeigGewicht = "0"
        Me.KompRzChargen.TeigChargen = Wb_MinMaxOptCharge2
        '
        'tpKompType
        '
        Me.tpKompType.BackColor = System.Drawing.SystemColors.Control
        Me.tpKompType.Controls.Add(Me.lblKeineNwt)
        Me.tpKompType.Location = New System.Drawing.Point(4, 23)
        Me.tpKompType.Name = "tpKompType"
        Me.tpKompType.Size = New System.Drawing.Size(724, 352)
        Me.tpKompType.TabIndex = 5
        Me.tpKompType.Text = "KompType"
        '
        'lblKeineNwt
        '
        Me.lblKeineNwt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblKeineNwt.ForeColor = System.Drawing.Color.Black
        Me.lblKeineNwt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblKeineNwt.Location = New System.Drawing.Point(11, 128)
        Me.lblKeineNwt.Name = "lblKeineNwt"
        Me.lblKeineNwt.Size = New System.Drawing.Size(477, 219)
        Me.lblKeineNwt.TabIndex = 52
        Me.lblKeineNwt.Text = "Kein Rohstoff"
        '
        'cbFreigabeProduktion
        '
        Me.cbFreigabeProduktion.AutoSize = True
        Me.cbFreigabeProduktion.Location = New System.Drawing.Point(168, 50)
        Me.cbFreigabeProduktion.Name = "cbFreigabeProduktion"
        Me.cbFreigabeProduktion.Size = New System.Drawing.Size(201, 17)
        Me.cbFreigabeProduktion.TabIndex = 54
        Me.cbFreigabeProduktion.Text = "Rohstoff kann (vor)produziert werden"
        Me.cbFreigabeProduktion.UseVisualStyleBackColor = True
        '
        'wb_Rohstoffe_Cloud
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(733, 471)
        Me.Controls.Add(Me.cbFreigabeProduktion)
        Me.Controls.Add(Me.tCloudID)
        Me.Controls.Add(Me.lblCloudID)
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
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.tpRezept.ResumeLayout(False)
        Me.tpKompType.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Wb_TabControl As wb_TabControl
    Friend WithEvents tpCloudSuchen As System.Windows.Forms.TabPage
    Friend WithEvents tpCloudGefunden As System.Windows.Forms.TabPage
    Friend WithEvents tpCloudAnzeige As System.Windows.Forms.TabPage
    Friend WithEvents tpCloudResult As System.Windows.Forms.TabPage
    Friend WithEvents btnCloud As System.Windows.Forms.Button
    Friend WithEvents btnDatenLink As System.Windows.Forms.Button
    Friend WithEvents tSuchtextBezeichnung As System.Windows.Forms.TextBox
    Friend WithEvents lblSuchtextBezeichnung As System.Windows.Forms.Label
    Friend WithEvents tRohstoffNummer As System.Windows.Forms.TextBox
    Friend WithEvents lblNummer As System.Windows.Forms.Label
    Friend WithEvents tRohstoffName As System.Windows.Forms.TextBox
    Friend WithEvents lblBezeichnung As System.Windows.Forms.Label
    Friend WithEvents tSuchtextLieferant As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblHilfeText As System.Windows.Forms.Label
    Friend WithEvents pnlNwtGrid As System.Windows.Forms.Panel
    Friend WithEvents btnFound_Back As System.Windows.Forms.Button
    Friend WithEvents btnFound_Vor As System.Windows.Forms.Button
    Friend WithEvents lblErgebnisText As System.Windows.Forms.Label
    Friend WithEvents btnShow_Back As System.Windows.Forms.Button
    Friend WithEvents btnShow_Vor As System.Windows.Forms.Button
    Friend WithEvents pnlNwt As System.Windows.Forms.Panel
    Friend WithEvents btnResult_Akt As System.Windows.Forms.Button
    Friend WithEvents btnDisconnect As System.Windows.Forms.Button
    Friend WithEvents tCloudID As System.Windows.Forms.TextBox
    Friend WithEvents lblCloudID As System.Windows.Forms.Label
    Friend WithEvents tpRezept As System.Windows.Forms.TabPage
    Friend WithEvents btnResult_OK As System.Windows.Forms.Button
    Friend WithEvents btnMail As System.Windows.Forms.Button
    Friend WithEvents KompRzChargen As wb_KompRzChargen
    Friend WithEvents tpKompType As System.Windows.Forms.TabPage
    Friend WithEvents lblKeineNwt As System.Windows.Forms.Label
    Friend WithEvents Btn_Result_Back As System.Windows.Forms.Button
    Friend WithEvents BtnRezept As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblDeklExtern As System.Windows.Forms.Label
    Friend WithEvents lblDeklIntern As System.Windows.Forms.Label
    Friend WithEvents tbDeklarationExtern As System.Windows.Forms.TextBox
    Friend WithEvents tbDeklarationIntern As System.Windows.Forms.TextBox
    Friend WithEvents BtnProduktDatenblatt As System.Windows.Forms.Button
    Friend WithEvents cbFreigabeProduktion As System.Windows.Forms.CheckBox
    Friend WithEvents BtnOpenFoodFacts As System.Windows.Forms.Button
End Class
