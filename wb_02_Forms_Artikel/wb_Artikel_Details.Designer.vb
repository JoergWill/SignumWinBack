Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Artikel_Details
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Artikel_Details))
        Dim Wb_MinMaxOptCharge5 As wb_MinMaxOptCharge = New wb_MinMaxOptCharge()
        Dim Wb_Charge13 As wb_Charge = New wb_Charge()
        Dim Wb_Charge14 As wb_Charge = New wb_Charge()
        Dim Wb_Charge15 As wb_Charge = New wb_Charge()
        Dim Wb_MinMaxOptCharge6 As wb_MinMaxOptCharge = New wb_MinMaxOptCharge()
        Dim Wb_Charge16 As wb_Charge = New wb_Charge()
        Dim Wb_Charge17 As wb_Charge = New wb_Charge()
        Dim Wb_Charge18 As wb_Charge = New wb_Charge()
        tArtikelName = New System.Windows.Forms.TextBox()
        Label2 = New System.Windows.Forms.Label()
        tArtikelNummer = New System.Windows.Forms.TextBox()
        lblNummer = New System.Windows.Forms.Label()
        tArtikelKommentar = New System.Windows.Forms.TextBox()
        Label4 = New System.Windows.Forms.Label()
        KompRzChargen = New wb_KompRzChargen()
        Label1 = New System.Windows.Forms.Label()
        cbArtikelGrp2 = New wb_ComboBox()
        Label14 = New System.Windows.Forms.Label()
        cbArtikelGrp1 = New wb_ComboBox()
        ePreis = New System.Windows.Forms.Label()
        tbArtikelPreis = New System.Windows.Forms.TextBox()
        lblPreis = New System.Windows.Forms.Label()
        tType = New System.Windows.Forms.TextBox()
        lblType = New System.Windows.Forms.Label()
        tZutatenliste = New System.Windows.Forms.TextBox()
        lblZutatenliste = New System.Windows.Forms.Label()
        tMehlZusammensetzung = New System.Windows.Forms.TextBox()
        lblMehlZusammensetzung = New System.Windows.Forms.Label()
        SuspendLayout()
        ' 
        ' tArtikelName
        ' 
        resources.ApplyResources(tArtikelName, "tArtikelName")
        tArtikelName.Name = "tArtikelName"
        ' 
        ' Label2
        ' 
        resources.ApplyResources(Label2, "Label2")
        Label2.Name = "Label2"
        ' 
        ' tArtikelNummer
        ' 
        resources.ApplyResources(tArtikelNummer, "tArtikelNummer")
        tArtikelNummer.Name = "tArtikelNummer"
        tArtikelNummer.ReadOnly = True
        tArtikelNummer.TabStop = False
        ' 
        ' lblNummer
        ' 
        resources.ApplyResources(lblNummer, "lblNummer")
        lblNummer.Name = "lblNummer"
        ' 
        ' tArtikelKommentar
        ' 
        resources.ApplyResources(tArtikelKommentar, "tArtikelKommentar")
        tArtikelKommentar.Name = "tArtikelKommentar"
        ' 
        ' Label4
        ' 
        resources.ApplyResources(Label4, "Label4")
        Label4.Name = "Label4"
        ' 
        ' KompRzChargen
        ' 
        Wb_MinMaxOptCharge5.ErrorCheck = False
        Wb_MinMaxOptCharge5.HasChanged = False
        Wb_Charge13.MengeInkg = "0,000"
        Wb_Charge13.MengeInProzent = "0"
        Wb_Charge13.MengeInStk = "0"
        Wb_Charge13.StkGewicht = "1000"
        Wb_Charge13.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge5.MaxCharge = Wb_Charge13
        Wb_Charge14.MengeInkg = "0,000"
        Wb_Charge14.MengeInProzent = "0"
        Wb_Charge14.MengeInStk = "0"
        Wb_Charge14.StkGewicht = "1000"
        Wb_Charge14.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge5.MinCharge = Wb_Charge14
        Wb_Charge15.MengeInkg = "0,000"
        Wb_Charge15.MengeInProzent = "0"
        Wb_Charge15.MengeInStk = "0"
        Wb_Charge15.StkGewicht = "1000"
        Wb_Charge15.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge5.OptCharge = Wb_Charge15
        Wb_MinMaxOptCharge5.StkGewicht = "1000"
        Wb_MinMaxOptCharge5.TeigGewicht = "0"
        KompRzChargen.ArtikelChargen = Wb_MinMaxOptCharge5
        KompRzChargen.DataValid = False
        KompRzChargen.ID = "-1"
        resources.ApplyResources(KompRzChargen, "KompRzChargen")
        KompRzChargen.Name = "KompRzChargen"
        KompRzChargen.RezeptName = ""
        KompRzChargen.RezeptNummer = ""
        KompRzChargen.RzNr = -1
        Wb_MinMaxOptCharge6.ErrorCheck = False
        Wb_MinMaxOptCharge6.HasChanged = False
        Wb_Charge16.MengeInkg = "0,000"
        Wb_Charge16.MengeInProzent = "0"
        Wb_Charge16.MengeInStk = "0"
        Wb_Charge16.StkGewicht = "1000"
        Wb_Charge16.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge6.MaxCharge = Wb_Charge16
        Wb_Charge17.MengeInkg = "0,000"
        Wb_Charge17.MengeInProzent = "0"
        Wb_Charge17.MengeInStk = "0"
        Wb_Charge17.StkGewicht = "1000"
        Wb_Charge17.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge6.MinCharge = Wb_Charge17
        Wb_Charge18.MengeInkg = "0,000"
        Wb_Charge18.MengeInProzent = "0"
        Wb_Charge18.MengeInStk = "0"
        Wb_Charge18.StkGewicht = "1000"
        Wb_Charge18.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge6.OptCharge = Wb_Charge18
        Wb_MinMaxOptCharge6.StkGewicht = "1000"
        Wb_MinMaxOptCharge6.TeigGewicht = "0"
        KompRzChargen.TeigChargen = Wb_MinMaxOptCharge6
        ' 
        ' Label1
        ' 
        resources.ApplyResources(Label1, "Label1")
        Label1.Name = "Label1"
        ' 
        ' cbArtikelGrp2
        ' 
        resources.ApplyResources(cbArtikelGrp2, "cbArtikelGrp2")
        cbArtikelGrp2.FormattingEnabled = True
        cbArtikelGrp2.Name = "cbArtikelGrp2"
        cbArtikelGrp2.TabStop = False
        ' 
        ' Label14
        ' 
        resources.ApplyResources(Label14, "Label14")
        Label14.Name = "Label14"
        ' 
        ' cbArtikelGrp1
        ' 
        resources.ApplyResources(cbArtikelGrp1, "cbArtikelGrp1")
        cbArtikelGrp1.FormattingEnabled = True
        cbArtikelGrp1.Name = "cbArtikelGrp1"
        cbArtikelGrp1.TabStop = False
        ' 
        ' ePreis
        ' 
        resources.ApplyResources(ePreis, "ePreis")
        ePreis.Name = "ePreis"
        ' 
        ' tbArtikelPreis
        ' 
        tbArtikelPreis.BackColor = Drawing.SystemColors.Control
        resources.ApplyResources(tbArtikelPreis, "tbArtikelPreis")
        tbArtikelPreis.Name = "tbArtikelPreis"
        tbArtikelPreis.ReadOnly = True
        ' 
        ' lblPreis
        ' 
        resources.ApplyResources(lblPreis, "lblPreis")
        lblPreis.Name = "lblPreis"
        ' 
        ' tType
        ' 
        resources.ApplyResources(tType, "tType")
        tType.Name = "tType"
        tType.ReadOnly = True
        tType.TabStop = False
        ' 
        ' lblType
        ' 
        resources.ApplyResources(lblType, "lblType")
        lblType.Name = "lblType"
        ' 
        ' tZutatenliste
        ' 
        resources.ApplyResources(tZutatenliste, "tZutatenliste")
        tZutatenliste.Name = "tZutatenliste"
        ' 
        ' lblZutatenliste
        ' 
        resources.ApplyResources(lblZutatenliste, "lblZutatenliste")
        lblZutatenliste.Name = "lblZutatenliste"
        ' 
        ' tMehlZusammensetzung
        ' 
        resources.ApplyResources(tMehlZusammensetzung, "tMehlZusammensetzung")
        tMehlZusammensetzung.Name = "tMehlZusammensetzung"
        ' 
        ' lblMehlZusammensetzung
        ' 
        resources.ApplyResources(lblMehlZusammensetzung, "lblMehlZusammensetzung")
        lblMehlZusammensetzung.Name = "lblMehlZusammensetzung"
        ' 
        ' wb_Artikel_Details
        ' 
        resources.ApplyResources(Me, "$this")
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Controls.Add(lblMehlZusammensetzung)
        Controls.Add(tMehlZusammensetzung)
        Controls.Add(lblZutatenliste)
        Controls.Add(tZutatenliste)
        Controls.Add(lblType)
        Controls.Add(tType)
        Controls.Add(ePreis)
        Controls.Add(tbArtikelPreis)
        Controls.Add(lblPreis)
        Controls.Add(cbArtikelGrp2)
        Controls.Add(Label14)
        Controls.Add(cbArtikelGrp1)
        Controls.Add(KompRzChargen)
        Controls.Add(tArtikelKommentar)
        Controls.Add(Label4)
        Controls.Add(tArtikelNummer)
        Controls.Add(lblNummer)
        Controls.Add(tArtikelName)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Name = "wb_Artikel_Details"
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents tArtikelName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tArtikelNummer As System.Windows.Forms.TextBox
    Friend WithEvents lblNummer As System.Windows.Forms.Label
    Friend WithEvents tArtikelKommentar As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents KompRzChargen As wb_KompRzChargen
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbArtikelGrp2 As wb_ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cbArtikelGrp1 As wb_ComboBox
    Friend WithEvents ePreis As System.Windows.Forms.Label
    Friend WithEvents tbArtikelPreis As System.Windows.Forms.TextBox
    Friend WithEvents lblPreis As System.Windows.Forms.Label
    Friend WithEvents tType As System.Windows.Forms.TextBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents tZutatenliste As System.Windows.Forms.TextBox
    Friend WithEvents lblZutatenliste As System.Windows.Forms.Label
    Friend WithEvents tMehlZusammensetzung As System.Windows.Forms.TextBox
    Friend WithEvents lblMehlZusammensetzung As System.Windows.Forms.Label
End Class
