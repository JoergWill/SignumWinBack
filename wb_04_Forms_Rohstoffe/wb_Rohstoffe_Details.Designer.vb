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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Rohstoffe_Details))
        Me.tRohstoffName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tRohstoffNummer = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tRohstoffKommentar = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlDetails = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDeklExtern = New System.Windows.Forms.Label()
        Me.tbDeklarationExtern = New System.Windows.Forms.TextBox()
        Me.lblDeklIntern = New System.Windows.Forms.Label()
        Me.tbDeklarationIntern = New System.Windows.Forms.TextBox()
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
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cbRohstoffGrp2 = New WinBack.wb_ComboBox()
        Me.cbRohstoffGrp1 = New WinBack.wb_ComboBox()
        Me.pnlDetails.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tRohstoffName
        '
        resources.ApplyResources(Me.tRohstoffName, "tRohstoffName")
        Me.tRohstoffName.Name = "tRohstoffName"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'tRohstoffNummer
        '
        resources.ApplyResources(Me.tRohstoffNummer, "tRohstoffNummer")
        Me.tRohstoffNummer.Name = "tRohstoffNummer"
        Me.tRohstoffNummer.ReadOnly = True
        Me.tRohstoffNummer.TabStop = False
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'tRohstoffKommentar
        '
        resources.ApplyResources(Me.tRohstoffKommentar, "tRohstoffKommentar")
        Me.tRohstoffKommentar.Name = "tRohstoffKommentar"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'pnlDetails
        '
        resources.ApplyResources(Me.pnlDetails, "pnlDetails")
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
        Me.pnlDetails.Name = "pnlDetails"
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.lblDeklExtern, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.tbDeklarationExtern, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblDeklIntern, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.tbDeklarationIntern, 0, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'lblDeklExtern
        '
        resources.ApplyResources(Me.lblDeklExtern, "lblDeklExtern")
        Me.lblDeklExtern.Name = "lblDeklExtern"
        '
        'tbDeklarationExtern
        '
        resources.ApplyResources(Me.tbDeklarationExtern, "tbDeklarationExtern")
        Me.tbDeklarationExtern.Name = "tbDeklarationExtern"
        '
        'lblDeklIntern
        '
        resources.ApplyResources(Me.lblDeklIntern, "lblDeklIntern")
        Me.lblDeklIntern.Name = "lblDeklIntern"
        '
        'tbDeklarationIntern
        '
        resources.ApplyResources(Me.tbDeklarationIntern, "tbDeklarationIntern")
        Me.tbDeklarationIntern.Name = "tbDeklarationIntern"
        '
        'eMindestmenge
        '
        resources.ApplyResources(Me.eMindestmenge, "eMindestmenge")
        Me.eMindestmenge.Name = "eMindestmenge"
        '
        'eBilanzmenge
        '
        resources.ApplyResources(Me.eBilanzmenge, "eBilanzmenge")
        Me.eBilanzmenge.Name = "eBilanzmenge"
        '
        'eGebindegroesse
        '
        resources.ApplyResources(Me.eGebindegroesse, "eGebindegroesse")
        Me.eGebindegroesse.Name = "eGebindegroesse"
        '
        'ePreis
        '
        resources.ApplyResources(Me.ePreis, "ePreis")
        Me.ePreis.Name = "ePreis"
        '
        'tbMindestMenge
        '
        resources.ApplyResources(Me.tbMindestMenge, "tbMindestMenge")
        Me.tbMindestMenge.Name = "tbMindestMenge"
        '
        'lbMindestMenge
        '
        resources.ApplyResources(Me.lbMindestMenge, "lbMindestMenge")
        Me.lbMindestMenge.Name = "lbMindestMenge"
        '
        'tbBilanzmenge
        '
        resources.ApplyResources(Me.tbBilanzmenge, "tbBilanzmenge")
        Me.tbBilanzmenge.Name = "tbBilanzmenge"
        Me.tbBilanzmenge.ReadOnly = True
        Me.tbBilanzmenge.TabStop = False
        '
        'lbBilanzMenge
        '
        resources.ApplyResources(Me.lbBilanzMenge, "lbBilanzMenge")
        Me.lbBilanzMenge.Name = "lbBilanzMenge"
        '
        'cbRezeptGewicht
        '
        resources.ApplyResources(Me.cbRezeptGewicht, "cbRezeptGewicht")
        Me.cbRezeptGewicht.Name = "cbRezeptGewicht"
        Me.cbRezeptGewicht.TabStop = False
        Me.cbRezeptGewicht.UseVisualStyleBackColor = True
        '
        'cbKeineDeklaration
        '
        resources.ApplyResources(Me.cbKeineDeklaration, "cbKeineDeklaration")
        Me.cbKeineDeklaration.Name = "cbKeineDeklaration"
        Me.cbKeineDeklaration.TabStop = False
        Me.cbKeineDeklaration.UseVisualStyleBackColor = True
        '
        'tbGebindeGroesse
        '
        resources.ApplyResources(Me.tbGebindeGroesse, "tbGebindeGroesse")
        Me.tbGebindeGroesse.Name = "tbGebindeGroesse"
        '
        'lblGebindegroesse
        '
        resources.ApplyResources(Me.lblGebindegroesse, "lblGebindegroesse")
        Me.lblGebindegroesse.Name = "lblGebindegroesse"
        '
        'tbRohstoffPreis
        '
        resources.ApplyResources(Me.tbRohstoffPreis, "tbRohstoffPreis")
        Me.tbRohstoffPreis.Name = "tbRohstoffPreis"
        '
        'lblPreis
        '
        resources.ApplyResources(Me.lblPreis, "lblPreis")
        Me.lblPreis.Name = "lblPreis"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        '
        'cbRohstoffGrp2
        '
        Me.cbRohstoffGrp2.FormattingEnabled = True
        resources.ApplyResources(Me.cbRohstoffGrp2, "cbRohstoffGrp2")
        Me.cbRohstoffGrp2.Name = "cbRohstoffGrp2"
        '
        'cbRohstoffGrp1
        '
        Me.cbRohstoffGrp1.FormattingEnabled = True
        resources.ApplyResources(Me.cbRohstoffGrp1, "cbRohstoffGrp1")
        Me.cbRohstoffGrp1.Name = "cbRohstoffGrp1"
        '
        'wb_Rohstoffe_Details
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlDetails)
        Me.Controls.Add(Me.tRohstoffKommentar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tRohstoffNummer)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tRohstoffName)
        Me.Controls.Add(Me.Label2)
        Me.Name = "wb_Rohstoffe_Details"
        Me.pnlDetails.ResumeLayout(False)
        Me.pnlDetails.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tRohstoffName As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tRohstoffNummer As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents tRohstoffKommentar As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
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
End Class
