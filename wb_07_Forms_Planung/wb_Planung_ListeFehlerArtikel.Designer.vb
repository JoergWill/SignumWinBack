<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Planung_ListeFehlerArtikel
    Inherits System.Windows.Forms.Form

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
        Me.tArtikelKommentar = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tArtikelNummer = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tArtikelName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbErrorText = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlCalcChargen = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tRestGr = New System.Windows.Forms.TextBox()
        Me.tRestkg = New System.Windows.Forms.TextBox()
        Me.tRestStk = New System.Windows.Forms.TextBox()
        Me.tChrgGr = New System.Windows.Forms.TextBox()
        Me.tChrgkg = New System.Windows.Forms.TextBox()
        Me.tChrgStk = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tBstkg = New System.Windows.Forms.TextBox()
        Me.tBestStk = New System.Windows.Forms.TextBox()
        Me.lblCalcChargen = New System.Windows.Forms.Label()
        Me.KompRzChargen = New WinBack.wb_KompRzChargen()
        Me.pnlCalcChargen.SuspendLayout()
        Me.SuspendLayout()
        '
        'tArtikelKommentar
        '
        Me.tArtikelKommentar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tArtikelKommentar.Location = New System.Drawing.Point(161, 63)
        Me.tArtikelKommentar.Name = "tArtikelKommentar"
        Me.tArtikelKommentar.ReadOnly = True
        Me.tArtikelKommentar.Size = New System.Drawing.Size(499, 20)
        Me.tArtikelKommentar.TabIndex = 74
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(162, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 73
        Me.Label4.Text = "Kommentar"
        '
        'tArtikelNummer
        '
        Me.tArtikelNummer.Location = New System.Drawing.Point(8, 24)
        Me.tArtikelNummer.Name = "tArtikelNummer"
        Me.tArtikelNummer.ReadOnly = True
        Me.tArtikelNummer.Size = New System.Drawing.Size(100, 20)
        Me.tArtikelNummer.TabIndex = 75
        Me.tArtikelNummer.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(9, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 72
        Me.Label3.Text = "Nummer"
        '
        'tArtikelName
        '
        Me.tArtikelName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tArtikelName.Location = New System.Drawing.Point(161, 24)
        Me.tArtikelName.Name = "tArtikelName"
        Me.tArtikelName.ReadOnly = True
        Me.tArtikelName.Size = New System.Drawing.Size(499, 20)
        Me.tArtikelName.TabIndex = 76
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(162, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 13)
        Me.Label2.TabIndex = 71
        Me.Label2.Text = "Artikel-Bezeichnung"
        '
        'tbErrorText
        '
        Me.tbErrorText.ForeColor = System.Drawing.Color.Red
        Me.tbErrorText.Location = New System.Drawing.Point(415, 120)
        Me.tbErrorText.Multiline = True
        Me.tbErrorText.Name = "tbErrorText"
        Me.tbErrorText.Size = New System.Drawing.Size(245, 97)
        Me.tbErrorText.TabIndex = 77
        Me.tbErrorText.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(412, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 78
        Me.Label1.Text = "Fehler"
        '
        'pnlCalcChargen
        '
        Me.pnlCalcChargen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCalcChargen.Controls.Add(Me.Label7)
        Me.pnlCalcChargen.Controls.Add(Me.tRestGr)
        Me.pnlCalcChargen.Controls.Add(Me.tRestkg)
        Me.pnlCalcChargen.Controls.Add(Me.tRestStk)
        Me.pnlCalcChargen.Controls.Add(Me.tChrgGr)
        Me.pnlCalcChargen.Controls.Add(Me.tChrgkg)
        Me.pnlCalcChargen.Controls.Add(Me.tChrgStk)
        Me.pnlCalcChargen.Controls.Add(Me.Label5)
        Me.pnlCalcChargen.Controls.Add(Me.tBstkg)
        Me.pnlCalcChargen.Controls.Add(Me.tBestStk)
        Me.pnlCalcChargen.Controls.Add(Me.lblCalcChargen)
        Me.pnlCalcChargen.Location = New System.Drawing.Point(415, 231)
        Me.pnlCalcChargen.Name = "pnlCalcChargen"
        Me.pnlCalcChargen.Size = New System.Drawing.Size(245, 122)
        Me.pnlCalcChargen.TabIndex = 79
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(26, 94)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 90
        Me.Label7.Text = "Rest"
        '
        'tRestGr
        '
        Me.tRestGr.Location = New System.Drawing.Point(124, 91)
        Me.tRestGr.Name = "tRestGr"
        Me.tRestGr.Size = New System.Drawing.Size(54, 20)
        Me.tRestGr.TabIndex = 88
        Me.tRestGr.TabStop = False
        Me.tRestGr.Text = "14000 Stk"
        Me.tRestGr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tRestGr.WordWrap = False
        '
        'tRestkg
        '
        Me.tRestkg.Location = New System.Drawing.Point(184, 91)
        Me.tRestkg.Name = "tRestkg"
        Me.tRestkg.Size = New System.Drawing.Size(54, 20)
        Me.tRestkg.TabIndex = 87
        Me.tRestkg.TabStop = False
        Me.tRestkg.Text = "14000 kg"
        Me.tRestkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tRestkg.WordWrap = False
        '
        'tRestStk
        '
        Me.tRestStk.Location = New System.Drawing.Point(57, 91)
        Me.tRestStk.Name = "tRestStk"
        Me.tRestStk.Size = New System.Drawing.Size(54, 20)
        Me.tRestStk.TabIndex = 86
        Me.tRestStk.TabStop = False
        Me.tRestStk.Text = "14000 Stk"
        Me.tRestStk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tRestStk.WordWrap = False
        '
        'tChrgGr
        '
        Me.tChrgGr.Location = New System.Drawing.Point(124, 65)
        Me.tChrgGr.Name = "tChrgGr"
        Me.tChrgGr.Size = New System.Drawing.Size(54, 20)
        Me.tChrgGr.TabIndex = 85
        Me.tChrgGr.TabStop = False
        Me.tChrgGr.Text = "14000 Stk"
        Me.tChrgGr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tChrgGr.WordWrap = False
        '
        'tChrgkg
        '
        Me.tChrgkg.Location = New System.Drawing.Point(184, 65)
        Me.tChrgkg.Name = "tChrgkg"
        Me.tChrgkg.Size = New System.Drawing.Size(54, 20)
        Me.tChrgkg.TabIndex = 84
        Me.tChrgkg.TabStop = False
        Me.tChrgkg.Text = "14000 kg"
        Me.tChrgkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tChrgkg.WordWrap = False
        '
        'tChrgStk
        '
        Me.tChrgStk.Location = New System.Drawing.Point(57, 65)
        Me.tChrgStk.Name = "tChrgStk"
        Me.tChrgStk.Size = New System.Drawing.Size(54, 20)
        Me.tChrgStk.TabIndex = 83
        Me.tChrgStk.TabStop = False
        Me.tChrgStk.Text = "14000 Stk"
        Me.tChrgStk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tChrgStk.WordWrap = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(8, 69)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 82
        Me.Label5.Text = "Chargen"
        '
        'tBstkg
        '
        Me.tBstkg.Location = New System.Drawing.Point(184, 14)
        Me.tBstkg.Name = "tBstkg"
        Me.tBstkg.Size = New System.Drawing.Size(54, 20)
        Me.tBstkg.TabIndex = 81
        Me.tBstkg.TabStop = False
        Me.tBstkg.Text = "14000 kg"
        Me.tBstkg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tBstkg.WordWrap = False
        '
        'tBestStk
        '
        Me.tBestStk.Location = New System.Drawing.Point(57, 14)
        Me.tBestStk.Name = "tBestStk"
        Me.tBestStk.Size = New System.Drawing.Size(54, 20)
        Me.tBestStk.TabIndex = 80
        Me.tBestStk.TabStop = False
        Me.tBestStk.Text = "14000 Stk"
        Me.tBestStk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tBestStk.WordWrap = False
        '
        'lblCalcChargen
        '
        Me.lblCalcChargen.AutoSize = True
        Me.lblCalcChargen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCalcChargen.Location = New System.Drawing.Point(14, 17)
        Me.lblCalcChargen.Name = "lblCalcChargen"
        Me.lblCalcChargen.Size = New System.Drawing.Size(41, 13)
        Me.lblCalcChargen.TabIndex = 79
        Me.lblCalcChargen.Text = "Bestellt"
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
        Me.KompRzChargen.Location = New System.Drawing.Point(2, 89)
        Me.KompRzChargen.Name = "KompRzChargen"
        Me.KompRzChargen.RezeptName = ""
        Me.KompRzChargen.RezeptNummer = ""
        Me.KompRzChargen.RzNr = -1
        Me.KompRzChargen.Size = New System.Drawing.Size(400, 338)
        Me.KompRzChargen.TabIndex = 70
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
        'wb_Planung_ListeFehlerArtikel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(672, 430)
        Me.Controls.Add(Me.pnlCalcChargen)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbErrorText)
        Me.Controls.Add(Me.tArtikelKommentar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tArtikelNummer)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tArtikelName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.KompRzChargen)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wb_Planung_ListeFehlerArtikel"
        Me.ShowInTaskbar = False
        Me.Text = "Artikel-Parameter"
        Me.pnlCalcChargen.ResumeLayout(False)
        Me.pnlCalcChargen.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents KompRzChargen As wb_KompRzChargen
    Friend WithEvents tArtikelKommentar As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents tArtikelNummer As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents tArtikelName As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tbErrorText As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents pnlCalcChargen As Windows.Forms.Panel
    Friend WithEvents lblCalcChargen As Windows.Forms.Label
    Friend WithEvents tRestGr As Windows.Forms.TextBox
    Friend WithEvents tRestkg As Windows.Forms.TextBox
    Friend WithEvents tRestStk As Windows.Forms.TextBox
    Friend WithEvents tChrgGr As Windows.Forms.TextBox
    Friend WithEvents tChrgkg As Windows.Forms.TextBox
    Friend WithEvents tChrgStk As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents tBstkg As Windows.Forms.TextBox
    Friend WithEvents tBestStk As Windows.Forms.TextBox
    Friend WithEvents Label7 As Windows.Forms.Label
End Class
