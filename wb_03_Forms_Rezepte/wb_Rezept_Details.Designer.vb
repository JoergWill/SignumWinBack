Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rezept_Details
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Rezept_Details))
        Me.tRezeptNummer = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tRezeptName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tRezeptKommentar = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tRezeptGewicht = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tChangeNr = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tChangeDatum = New System.Windows.Forms.TextBox()
        Me.tChangeName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tChargeMin = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tChargeMax = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.tChargeOpt = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cbAnstellgut = New System.Windows.Forms.CheckBox()
        Me.cbLiniengruppe = New WinBack.wb_ComboBox()
        Me.cbVariante = New WinBack.wb_ComboBox()
        Me.SuspendLayout()
        '
        'tRezeptNummer
        '
        resources.ApplyResources(Me.tRezeptNummer, "tRezeptNummer")
        Me.tRezeptNummer.Name = "tRezeptNummer"
        Me.tRezeptNummer.TabStop = False
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'tRezeptName
        '
        resources.ApplyResources(Me.tRezeptName, "tRezeptName")
        Me.tRezeptName.Name = "tRezeptName"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'tRezeptKommentar
        '
        resources.ApplyResources(Me.tRezeptKommentar, "tRezeptKommentar")
        Me.tRezeptKommentar.Name = "tRezeptKommentar"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'tRezeptGewicht
        '
        resources.ApplyResources(Me.tRezeptGewicht, "tRezeptGewicht")
        Me.tRezeptGewicht.Name = "tRezeptGewicht"
        Me.tRezeptGewicht.ReadOnly = True
        Me.tRezeptGewicht.TabStop = False
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'tChangeNr
        '
        resources.ApplyResources(Me.tChangeNr, "tChangeNr")
        Me.tChangeNr.Name = "tChangeNr"
        Me.tChangeNr.ReadOnly = True
        Me.tChangeNr.TabStop = False
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'tChangeDatum
        '
        resources.ApplyResources(Me.tChangeDatum, "tChangeDatum")
        Me.tChangeDatum.Name = "tChangeDatum"
        Me.tChangeDatum.ReadOnly = True
        Me.tChangeDatum.TabStop = False
        '
        'tChangeName
        '
        resources.ApplyResources(Me.tChangeName, "tChangeName")
        Me.tChangeName.Name = "tChangeName"
        Me.tChangeName.ReadOnly = True
        Me.tChangeName.TabStop = False
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'tChargeMin
        '
        resources.ApplyResources(Me.tChargeMin, "tChargeMin")
        Me.tChargeMin.Name = "tChargeMin"
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        '
        'tChargeMax
        '
        resources.ApplyResources(Me.tChargeMax, "tChargeMax")
        Me.tChargeMax.Name = "tChargeMax"
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        '
        'tChargeOpt
        '
        resources.ApplyResources(Me.tChargeOpt, "tChargeOpt")
        Me.tChargeOpt.Name = "tChargeOpt"
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.Name = "Label12"
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.Name = "Label13"
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.Name = "Label15"
        '
        'Label16
        '
        resources.ApplyResources(Me.Label16, "Label16")
        Me.Label16.Name = "Label16"
        '
        'cbAnstellgut
        '
        resources.ApplyResources(Me.cbAnstellgut, "cbAnstellgut")
        Me.cbAnstellgut.Name = "cbAnstellgut"
        Me.cbAnstellgut.UseVisualStyleBackColor = True
        '
        'cbLiniengruppe
        '
        resources.ApplyResources(Me.cbLiniengruppe, "cbLiniengruppe")
        Me.cbLiniengruppe.FormattingEnabled = True
        Me.cbLiniengruppe.Name = "cbLiniengruppe"
        '
        'cbVariante
        '
        resources.ApplyResources(Me.cbVariante, "cbVariante")
        Me.cbVariante.BackColor = System.Drawing.SystemColors.Window
        Me.cbVariante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cbVariante.FormattingEnabled = True
        Me.cbVariante.Name = "cbVariante"
        Me.cbVariante.TabStop = False
        '
        'wb_Rezept_Details
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.cbAnstellgut)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.cbLiniengruppe)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.cbVariante)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.tChargeOpt)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.tChargeMax)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.tChargeMin)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.tChangeName)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.tChangeDatum)
        Me.Controls.Add(Me.tChangeNr)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tRezeptGewicht)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tRezeptKommentar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tRezeptName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tRezeptNummer)
        Me.Controls.Add(Me.Label1)
        Me.Name = "wb_Rezept_Details"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tRezeptNummer As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents tRezeptName As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tRezeptKommentar As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents tRezeptGewicht As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents tChangeNr As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents tChangeDatum As Windows.Forms.TextBox
    Friend WithEvents tChangeName As Windows.Forms.TextBox
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents tChargeMin As Windows.Forms.TextBox
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents tChargeMax As Windows.Forms.TextBox
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents tChargeOpt As Windows.Forms.TextBox
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents Label13 As Windows.Forms.Label
    Friend WithEvents cbVariante As wb_ComboBox
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents Label15 As Windows.Forms.Label
    Friend WithEvents cbLiniengruppe As wb_ComboBox
    Friend WithEvents Label16 As Windows.Forms.Label
    Friend WithEvents cbAnstellgut As Windows.Forms.CheckBox
End Class
