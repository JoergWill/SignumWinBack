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
        Dim Wb_MinMaxOptCharge1 As WinBack.wb_MinMaxOptCharge = New WinBack.wb_MinMaxOptCharge()
        Dim Wb_Charge1 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge2 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge3 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_MinMaxOptCharge2 As WinBack.wb_MinMaxOptCharge = New WinBack.wb_MinMaxOptCharge()
        Dim Wb_Charge4 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge5 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge6 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Me.tArtikelName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tArtikelNummer = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tArtikelKommentar = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.KompRzChargen = New WinBack.wb_KompRzChargen()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbArtikelGrp2 = New WinBack.wb_ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cbArtikelGrp1 = New WinBack.wb_ComboBox()
        Me.ePreis = New System.Windows.Forms.Label()
        Me.tbArtikelPreis = New System.Windows.Forms.TextBox()
        Me.lblPreis = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'tArtikelName
        '
        resources.ApplyResources(Me.tArtikelName, "tArtikelName")
        Me.tArtikelName.Name = "tArtikelName"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'tArtikelNummer
        '
        resources.ApplyResources(Me.tArtikelNummer, "tArtikelNummer")
        Me.tArtikelNummer.Name = "tArtikelNummer"
        Me.tArtikelNummer.ReadOnly = True
        Me.tArtikelNummer.TabStop = False
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'tArtikelKommentar
        '
        resources.ApplyResources(Me.tArtikelKommentar, "tArtikelKommentar")
        Me.tArtikelKommentar.Name = "tArtikelKommentar"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
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
        resources.ApplyResources(Me.KompRzChargen, "KompRzChargen")
        Me.KompRzChargen.Name = "KompRzChargen"
        Me.KompRzChargen.RezeptName = ""
        Me.KompRzChargen.RezeptNummer = ""
        Me.KompRzChargen.RzNr = -1
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
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'cbArtikelGrp2
        '
        resources.ApplyResources(Me.cbArtikelGrp2, "cbArtikelGrp2")
        Me.cbArtikelGrp2.FormattingEnabled = True
        Me.cbArtikelGrp2.Name = "cbArtikelGrp2"
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        '
        'cbArtikelGrp1
        '
        resources.ApplyResources(Me.cbArtikelGrp1, "cbArtikelGrp1")
        Me.cbArtikelGrp1.FormattingEnabled = True
        Me.cbArtikelGrp1.Name = "cbArtikelGrp1"
        '
        'ePreis
        '
        resources.ApplyResources(Me.ePreis, "ePreis")
        Me.ePreis.Name = "ePreis"
        '
        'tbArtikelPreis
        '
        Me.tbArtikelPreis.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.tbArtikelPreis, "tbArtikelPreis")
        Me.tbArtikelPreis.Name = "tbArtikelPreis"
        Me.tbArtikelPreis.ReadOnly = True
        '
        'lblPreis
        '
        resources.ApplyResources(Me.lblPreis, "lblPreis")
        Me.lblPreis.Name = "lblPreis"
        '
        'wb_Artikel_Details
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ePreis)
        Me.Controls.Add(Me.tbArtikelPreis)
        Me.Controls.Add(Me.lblPreis)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbArtikelGrp2)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.cbArtikelGrp1)
        Me.Controls.Add(Me.KompRzChargen)
        Me.Controls.Add(Me.tArtikelKommentar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tArtikelNummer)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tArtikelName)
        Me.Controls.Add(Me.Label2)
        Me.Name = "wb_Artikel_Details"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tArtikelName As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tArtikelNummer As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents tArtikelKommentar As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents KompRzChargen As wb_KompRzChargen
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents cbArtikelGrp2 As wb_ComboBox
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents cbArtikelGrp1 As wb_ComboBox
    Friend WithEvents ePreis As Windows.Forms.Label
    Friend WithEvents tbArtikelPreis As Windows.Forms.TextBox
    Friend WithEvents lblPreis As Windows.Forms.Label
End Class
