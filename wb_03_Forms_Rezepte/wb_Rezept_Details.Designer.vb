Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rezept_Details
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
        Me.tRezeptNummer = New System.Windows.Forms.TextBox()
        Me.lblNummer = New System.Windows.Forms.Label()
        Me.tRezeptName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tRezeptKommentar = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tRezeptGewicht = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tChangeNr = New System.Windows.Forms.TextBox()
        Me.lblChangeDate = New System.Windows.Forms.Label()
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
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cbRezeptGruppe = New WinBack.wb_ComboBox()
        Me.cbLiniengruppe = New WinBack.wb_ComboBox()
        Me.cbVariante = New WinBack.wb_ComboBox()
        Me.SuspendLayout()
        '
        'tRezeptNummer
        '
        Me.tRezeptNummer.Location = New System.Drawing.Point(12, 37)
        Me.tRezeptNummer.Name = "tRezeptNummer"
        Me.tRezeptNummer.Size = New System.Drawing.Size(136, 20)
        Me.tRezeptNummer.TabIndex = 4
        Me.tRezeptNummer.TabStop = False
        '
        'lblNummer
        '
        Me.lblNummer.AutoSize = True
        Me.lblNummer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNummer.Location = New System.Drawing.Point(9, 21)
        Me.lblNummer.Name = "lblNummer"
        Me.lblNummer.Size = New System.Drawing.Size(58, 13)
        Me.lblNummer.TabIndex = 3
        Me.lblNummer.Text = "Rezept-Nr."
        '
        'tRezeptName
        '
        Me.tRezeptName.Location = New System.Drawing.Point(154, 37)
        Me.tRezeptName.Name = "tRezeptName"
        Me.tRezeptName.Size = New System.Drawing.Size(381, 20)
        Me.tRezeptName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(151, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Rezept-Bezeichnung"
        '
        'tRezeptKommentar
        '
        Me.tRezeptKommentar.Location = New System.Drawing.Point(12, 78)
        Me.tRezeptKommentar.Name = "tRezeptKommentar"
        Me.tRezeptKommentar.Size = New System.Drawing.Size(415, 20)
        Me.tRezeptKommentar.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(9, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Kommentar"
        '
        'tRezeptGewicht
        '
        Me.tRezeptGewicht.Location = New System.Drawing.Point(458, 78)
        Me.tRezeptGewicht.Name = "tRezeptGewicht"
        Me.tRezeptGewicht.ReadOnly = True
        Me.tRezeptGewicht.Size = New System.Drawing.Size(77, 20)
        Me.tRezeptGewicht.TabIndex = 10
        Me.tRezeptGewicht.TabStop = False
        Me.tRezeptGewicht.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(455, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Rezept-Gewicht"
        '
        'tChangeNr
        '
        Me.tChangeNr.Location = New System.Drawing.Point(12, 204)
        Me.tChangeNr.Name = "tChangeNr"
        Me.tChangeNr.ReadOnly = True
        Me.tChangeNr.Size = New System.Drawing.Size(57, 20)
        Me.tChangeNr.TabIndex = 12
        Me.tChangeNr.TabStop = False
        '
        'lblChangeDate
        '
        Me.lblChangeDate.AutoSize = True
        Me.lblChangeDate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblChangeDate.Location = New System.Drawing.Point(9, 188)
        Me.lblChangeDate.Name = "lblChangeDate"
        Me.lblChangeDate.Size = New System.Drawing.Size(85, 13)
        Me.lblChangeDate.TabIndex = 11
        Me.lblChangeDate.Text = "Letzte Änderung"
        '
        'tChangeDatum
        '
        Me.tChangeDatum.Location = New System.Drawing.Point(75, 204)
        Me.tChangeDatum.Name = "tChangeDatum"
        Me.tChangeDatum.ReadOnly = True
        Me.tChangeDatum.Size = New System.Drawing.Size(352, 20)
        Me.tChangeDatum.TabIndex = 13
        Me.tChangeDatum.TabStop = False
        '
        'tChangeName
        '
        Me.tChangeName.Location = New System.Drawing.Point(12, 246)
        Me.tChangeName.Name = "tChangeName"
        Me.tChangeName.ReadOnly = True
        Me.tChangeName.Size = New System.Drawing.Size(415, 20)
        Me.tChangeName.TabIndex = 15
        Me.tChangeName.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(9, 230)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "von:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(541, 78)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(19, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "kg"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(541, 162)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(19, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "kg"
        '
        'tChargeMin
        '
        Me.tChargeMin.Location = New System.Drawing.Point(458, 162)
        Me.tChargeMin.Name = "tChargeMin"
        Me.tChargeMin.Size = New System.Drawing.Size(77, 20)
        Me.tChargeMin.TabIndex = 5
        Me.tChargeMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label9.Location = New System.Drawing.Point(455, 146)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Min-Charge"
        '
        'tChargeMax
        '
        Me.tChargeMax.Location = New System.Drawing.Point(458, 204)
        Me.tChargeMax.Name = "tChargeMax"
        Me.tChargeMax.Size = New System.Drawing.Size(77, 20)
        Me.tChargeMax.TabIndex = 6
        Me.tChargeMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label10.Location = New System.Drawing.Point(455, 188)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(64, 13)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Max-Charge"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label11.Location = New System.Drawing.Point(541, 246)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(0, 13)
        Me.Label11.TabIndex = 24
        '
        'tChargeOpt
        '
        Me.tChargeOpt.Location = New System.Drawing.Point(458, 246)
        Me.tChargeOpt.Name = "tChargeOpt"
        Me.tChargeOpt.Size = New System.Drawing.Size(77, 20)
        Me.tChargeOpt.TabIndex = 7
        Me.tChargeOpt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label12.Location = New System.Drawing.Point(455, 230)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(61, 13)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "Opt-Charge"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label13.Location = New System.Drawing.Point(541, 204)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(19, 13)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "kg"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label14.Location = New System.Drawing.Point(9, 105)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(46, 13)
        Me.Label14.TabIndex = 27
        Me.Label14.Text = "Variante"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label15.Location = New System.Drawing.Point(9, 145)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(68, 13)
        Me.Label15.TabIndex = 29
        Me.Label15.Text = "Liniengruppe"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label16.Location = New System.Drawing.Point(541, 249)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(19, 13)
        Me.Label16.TabIndex = 30
        Me.Label16.Text = "kg"
        '
        'cbAnstellgut
        '
        Me.cbAnstellgut.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbAnstellgut.Location = New System.Drawing.Point(267, 116)
        Me.cbAnstellgut.Name = "cbAnstellgut"
        Me.cbAnstellgut.Size = New System.Drawing.Size(160, 31)
        Me.cbAnstellgut.TabIndex = 32
        Me.cbAnstellgut.Text = "Anstellgut für den nächsten Tag aufheben"
        Me.cbAnstellgut.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label17.Location = New System.Drawing.Point(9, 272)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(77, 13)
        Me.Label17.TabIndex = 46
        Me.Label17.Text = "Rezeptgruppe:"
        '
        'cbRezeptGruppe
        '
        Me.cbRezeptGruppe.Location = New System.Drawing.Point(12, 288)
        Me.cbRezeptGruppe.Name = "cbRezeptGruppe"
        Me.cbRezeptGruppe.Size = New System.Drawing.Size(245, 21)
        Me.cbRezeptGruppe.TabIndex = 45
        Me.cbRezeptGruppe.TabStop = False
        '
        'cbLiniengruppe
        '
        Me.cbLiniengruppe.FormattingEnabled = True
        Me.cbLiniengruppe.Location = New System.Drawing.Point(12, 162)
        Me.cbLiniengruppe.Name = "cbLiniengruppe"
        Me.cbLiniengruppe.Size = New System.Drawing.Size(415, 21)
        Me.cbLiniengruppe.TabIndex = 28
        Me.cbLiniengruppe.TabStop = False
        '
        'cbVariante
        '
        Me.cbVariante.BackColor = System.Drawing.SystemColors.Window
        Me.cbVariante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cbVariante.Enabled = False
        Me.cbVariante.FormattingEnabled = True
        Me.cbVariante.Location = New System.Drawing.Point(12, 121)
        Me.cbVariante.Name = "cbVariante"
        Me.cbVariante.Size = New System.Drawing.Size(245, 21)
        Me.cbVariante.TabIndex = 26
        Me.cbVariante.TabStop = False
        '
        'wb_Rezept_Details
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.ClientSize = New System.Drawing.Size(572, 334)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.cbRezeptGruppe)
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
        Me.Controls.Add(Me.lblChangeDate)
        Me.Controls.Add(Me.tRezeptGewicht)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tRezeptKommentar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tRezeptName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tRezeptNummer)
        Me.Controls.Add(Me.lblNummer)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "wb_Rezept_Details"
        Me.Text = "Rezept Details"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tRezeptNummer As System.Windows.Forms.TextBox
    Friend WithEvents lblNummer As System.Windows.Forms.Label
    Friend WithEvents tRezeptName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tRezeptKommentar As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tRezeptGewicht As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tChangeNr As System.Windows.Forms.TextBox
    Friend WithEvents lblChangeDate As System.Windows.Forms.Label
    Friend WithEvents tChangeDatum As System.Windows.Forms.TextBox
    Friend WithEvents tChangeName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tChargeMin As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tChargeMax As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tChargeOpt As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbVariante As wb_ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cbLiniengruppe As wb_ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cbAnstellgut As System.Windows.Forms.CheckBox
    Friend WithEvents cbRezeptGruppe As wb_ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
End Class
