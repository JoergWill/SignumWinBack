<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_ListeStatistik
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.gbFilter = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbUhrzeitBis = New System.Windows.Forms.CheckBox()
        Me.cbUhrzeitVon = New System.Windows.Forms.CheckBox()
        Me.dtUhrzeitBis = New System.Windows.Forms.DateTimePicker()
        Me.dtUhrzeitVon = New System.Windows.Forms.DateTimePicker()
        Me.cbIstwertNullAusblenden = New System.Windows.Forms.CheckBox()
        Me.cbWasserTempAusblenden = New System.Windows.Forms.CheckBox()
        Me.cbElementeAusListe = New System.Windows.Forms.CheckBox()
        Me.dtFilterVon = New System.Windows.Forms.DateTimePicker()
        Me.dtFilterBis = New System.Windows.Forms.DateTimePicker()
        Me.lblFilterVon = New System.Windows.Forms.Label()
        Me.lblFilterBis = New System.Windows.Forms.Label()
        Me.cbLinien = New System.Windows.Forms.CheckedListBox()
        Me.cbAlleLinien = New System.Windows.Forms.CheckBox()
        Me.BtnListeLeeren = New System.Windows.Forms.Button()
        Me.BtnListeLaden = New System.Windows.Forms.Button()
        Me.BtnListeSpeichern = New System.Windows.Forms.Button()
        Me.BtnListeRemove = New System.Windows.Forms.Button()
        Me.BtnListeAdd = New System.Windows.Forms.Button()
        Me.tpListe = New System.Windows.Forms.Panel()
        Me.gbLinien = New System.Windows.Forms.GroupBox()
        Me.gbRohGruppe = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbRohstoffGrp2 = New WinBack.wb_ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cbRohstoffGrp1 = New WinBack.wb_ComboBox()
        Me.gbFilter.SuspendLayout()
        Me.gbLinien.SuspendLayout()
        Me.gbRohGruppe.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbFilter
        '
        Me.gbFilter.Controls.Add(Me.Label2)
        Me.gbFilter.Controls.Add(Me.Label1)
        Me.gbFilter.Controls.Add(Me.cbUhrzeitBis)
        Me.gbFilter.Controls.Add(Me.cbUhrzeitVon)
        Me.gbFilter.Controls.Add(Me.dtUhrzeitBis)
        Me.gbFilter.Controls.Add(Me.dtUhrzeitVon)
        Me.gbFilter.Controls.Add(Me.cbIstwertNullAusblenden)
        Me.gbFilter.Controls.Add(Me.cbWasserTempAusblenden)
        Me.gbFilter.Controls.Add(Me.cbElementeAusListe)
        Me.gbFilter.Controls.Add(Me.dtFilterVon)
        Me.gbFilter.Controls.Add(Me.dtFilterBis)
        Me.gbFilter.Controls.Add(Me.lblFilterVon)
        Me.gbFilter.Controls.Add(Me.lblFilterBis)
        Me.gbFilter.Location = New System.Drawing.Point(339, 3)
        Me.gbFilter.Name = "gbFilter"
        Me.gbFilter.Size = New System.Drawing.Size(212, 257)
        Me.gbFilter.TabIndex = 21
        Me.gbFilter.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(115, 145)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Uhrzeit bis"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(8, 145)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Uhrzeit von"
        '
        'cbUhrzeitBis
        '
        Me.cbUhrzeitBis.AutoSize = True
        Me.cbUhrzeitBis.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbUhrzeitBis.Location = New System.Drawing.Point(118, 164)
        Me.cbUhrzeitBis.Name = "cbUhrzeitBis"
        Me.cbUhrzeitBis.Size = New System.Drawing.Size(15, 14)
        Me.cbUhrzeitBis.TabIndex = 19
        Me.cbUhrzeitBis.UseVisualStyleBackColor = True
        '
        'cbUhrzeitVon
        '
        Me.cbUhrzeitVon.AutoSize = True
        Me.cbUhrzeitVon.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbUhrzeitVon.Location = New System.Drawing.Point(8, 164)
        Me.cbUhrzeitVon.Name = "cbUhrzeitVon"
        Me.cbUhrzeitVon.Size = New System.Drawing.Size(15, 14)
        Me.cbUhrzeitVon.TabIndex = 18
        Me.cbUhrzeitVon.UseVisualStyleBackColor = True
        '
        'dtUhrzeitBis
        '
        Me.dtUhrzeitBis.CustomFormat = "HH:mm"
        Me.dtUhrzeitBis.Enabled = False
        Me.dtUhrzeitBis.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtUhrzeitBis.Location = New System.Drawing.Point(139, 161)
        Me.dtUhrzeitBis.Name = "dtUhrzeitBis"
        Me.dtUhrzeitBis.ShowUpDown = True
        Me.dtUhrzeitBis.Size = New System.Drawing.Size(64, 20)
        Me.dtUhrzeitBis.TabIndex = 17
        '
        'dtUhrzeitVon
        '
        Me.dtUhrzeitVon.CustomFormat = "HH:mm"
        Me.dtUhrzeitVon.Enabled = False
        Me.dtUhrzeitVon.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtUhrzeitVon.Location = New System.Drawing.Point(29, 161)
        Me.dtUhrzeitVon.Name = "dtUhrzeitVon"
        Me.dtUhrzeitVon.ShowUpDown = True
        Me.dtUhrzeitVon.Size = New System.Drawing.Size(58, 20)
        Me.dtUhrzeitVon.TabIndex = 16
        Me.dtUhrzeitVon.Value = New Date(2020, 3, 24, 0, 0, 0, 0)
        '
        'cbIstwertNullAusblenden
        '
        Me.cbIstwertNullAusblenden.AutoSize = True
        Me.cbIstwertNullAusblenden.Checked = True
        Me.cbIstwertNullAusblenden.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbIstwertNullAusblenden.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbIstwertNullAusblenden.Location = New System.Drawing.Point(8, 228)
        Me.cbIstwertNullAusblenden.Name = "cbIstwertNullAusblenden"
        Me.cbIstwertNullAusblenden.Size = New System.Drawing.Size(144, 17)
        Me.cbIstwertNullAusblenden.TabIndex = 15
        Me.cbIstwertNullAusblenden.Text = "Istwert Null unterdrücken"
        Me.cbIstwertNullAusblenden.UseVisualStyleBackColor = True
        '
        'cbWasserTempAusblenden
        '
        Me.cbWasserTempAusblenden.AutoSize = True
        Me.cbWasserTempAusblenden.Checked = True
        Me.cbWasserTempAusblenden.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbWasserTempAusblenden.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbWasserTempAusblenden.Location = New System.Drawing.Point(8, 205)
        Me.cbWasserTempAusblenden.Name = "cbWasserTempAusblenden"
        Me.cbWasserTempAusblenden.Size = New System.Drawing.Size(170, 17)
        Me.cbWasserTempAusblenden.TabIndex = 14
        Me.cbWasserTempAusblenden.Text = "Wassertemperatur ausblenden"
        Me.cbWasserTempAusblenden.UseVisualStyleBackColor = True
        '
        'cbElementeAusListe
        '
        Me.cbElementeAusListe.AutoSize = True
        Me.cbElementeAusListe.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbElementeAusListe.Location = New System.Drawing.Point(8, 19)
        Me.cbElementeAusListe.Name = "cbElementeAusListe"
        Me.cbElementeAusListe.Size = New System.Drawing.Size(153, 17)
        Me.cbElementeAusListe.TabIndex = 13
        Me.cbElementeAusListe.Text = "Nur Elemente aus der Liste"
        Me.cbElementeAusListe.UseVisualStyleBackColor = True
        '
        'dtFilterVon
        '
        Me.dtFilterVon.Location = New System.Drawing.Point(8, 68)
        Me.dtFilterVon.Name = "dtFilterVon"
        Me.dtFilterVon.Size = New System.Drawing.Size(195, 20)
        Me.dtFilterVon.TabIndex = 8
        '
        'dtFilterBis
        '
        Me.dtFilterBis.Location = New System.Drawing.Point(8, 110)
        Me.dtFilterBis.Name = "dtFilterBis"
        Me.dtFilterBis.Size = New System.Drawing.Size(195, 20)
        Me.dtFilterBis.TabIndex = 10
        '
        'lblFilterVon
        '
        Me.lblFilterVon.AutoSize = True
        Me.lblFilterVon.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFilterVon.Location = New System.Drawing.Point(8, 52)
        Me.lblFilterVon.Name = "lblFilterVon"
        Me.lblFilterVon.Size = New System.Drawing.Size(60, 13)
        Me.lblFilterVon.TabIndex = 11
        Me.lblFilterVon.Text = "Von Datum"
        '
        'lblFilterBis
        '
        Me.lblFilterBis.AutoSize = True
        Me.lblFilterBis.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFilterBis.Location = New System.Drawing.Point(8, 94)
        Me.lblFilterBis.Name = "lblFilterBis"
        Me.lblFilterBis.Size = New System.Drawing.Size(55, 13)
        Me.lblFilterBis.TabIndex = 12
        Me.lblFilterBis.Text = "Bis Datum"
        '
        'cbLinien
        '
        Me.cbLinien.BackColor = System.Drawing.SystemColors.Control
        Me.cbLinien.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.cbLinien.CheckOnClick = True
        Me.cbLinien.Enabled = False
        Me.cbLinien.FormattingEnabled = True
        Me.cbLinien.Location = New System.Drawing.Point(86, 13)
        Me.cbLinien.Name = "cbLinien"
        Me.cbLinien.Size = New System.Drawing.Size(117, 60)
        Me.cbLinien.TabIndex = 16
        '
        'cbAlleLinien
        '
        Me.cbAlleLinien.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.cbAlleLinien.Checked = True
        Me.cbAlleLinien.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAlleLinien.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbAlleLinien.Location = New System.Drawing.Point(8, 13)
        Me.cbAlleLinien.Name = "cbAlleLinien"
        Me.cbAlleLinien.Size = New System.Drawing.Size(81, 32)
        Me.cbAlleLinien.TabIndex = 3
        Me.cbAlleLinien.Text = "Alle Linien anzeigen"
        Me.cbAlleLinien.UseVisualStyleBackColor = True
        '
        'BtnListeLeeren
        '
        Me.BtnListeLeeren.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnListeLeeren.Location = New System.Drawing.Point(221, 567)
        Me.BtnListeLeeren.Name = "BtnListeLeeren"
        Me.BtnListeLeeren.Size = New System.Drawing.Size(103, 43)
        Me.BtnListeLeeren.TabIndex = 20
        Me.BtnListeLeeren.Text = "Liste leeren"
        Me.BtnListeLeeren.UseVisualStyleBackColor = True
        '
        'BtnListeLaden
        '
        Me.BtnListeLaden.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnListeLaden.Location = New System.Drawing.Point(339, 567)
        Me.BtnListeLaden.Name = "BtnListeLaden"
        Me.BtnListeLaden.Size = New System.Drawing.Size(103, 43)
        Me.BtnListeLaden.TabIndex = 19
        Me.BtnListeLaden.Text = "Liste laden"
        Me.BtnListeLaden.UseVisualStyleBackColor = True
        '
        'BtnListeSpeichern
        '
        Me.BtnListeSpeichern.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnListeSpeichern.Location = New System.Drawing.Point(448, 567)
        Me.BtnListeSpeichern.Name = "BtnListeSpeichern"
        Me.BtnListeSpeichern.Size = New System.Drawing.Size(103, 43)
        Me.BtnListeSpeichern.TabIndex = 18
        Me.BtnListeSpeichern.Text = "Liste speichern"
        Me.BtnListeSpeichern.UseVisualStyleBackColor = True
        '
        'BtnListeRemove
        '
        Me.BtnListeRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnListeRemove.Location = New System.Drawing.Point(112, 567)
        Me.BtnListeRemove.Name = "BtnListeRemove"
        Me.BtnListeRemove.Size = New System.Drawing.Size(103, 43)
        Me.BtnListeRemove.TabIndex = 17
        Me.BtnListeRemove.Text = "aus Liste löschen"
        Me.BtnListeRemove.UseVisualStyleBackColor = True
        '
        'BtnListeAdd
        '
        Me.BtnListeAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnListeAdd.Location = New System.Drawing.Point(3, 567)
        Me.BtnListeAdd.Name = "BtnListeAdd"
        Me.BtnListeAdd.Size = New System.Drawing.Size(103, 43)
        Me.BtnListeAdd.TabIndex = 16
        Me.BtnListeAdd.Text = "zur Liste hinzufügen"
        Me.BtnListeAdd.UseVisualStyleBackColor = True
        '
        'tpRezeptListe
        '
        Me.tpListe.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tpListe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tpListe.Location = New System.Drawing.Point(3, 3)
        Me.tpListe.Name = "tpRezeptListe"
        Me.tpListe.Size = New System.Drawing.Size(321, 558)
        Me.tpListe.TabIndex = 15
        '
        'gbLinien
        '
        Me.gbLinien.Controls.Add(Me.cbLinien)
        Me.gbLinien.Controls.Add(Me.cbAlleLinien)
        Me.gbLinien.Location = New System.Drawing.Point(339, 248)
        Me.gbLinien.Name = "gbLinien"
        Me.gbLinien.Size = New System.Drawing.Size(211, 85)
        Me.gbLinien.TabIndex = 22
        Me.gbLinien.TabStop = False
        '
        'gbRohGruppe
        '
        Me.gbRohGruppe.Controls.Add(Me.Label3)
        Me.gbRohGruppe.Controls.Add(Me.cbRohstoffGrp2)
        Me.gbRohGruppe.Controls.Add(Me.Label14)
        Me.gbRohGruppe.Controls.Add(Me.cbRohstoffGrp1)
        Me.gbRohGruppe.Location = New System.Drawing.Point(339, 326)
        Me.gbRohGruppe.Name = "gbRohGruppe"
        Me.gbRohGruppe.Size = New System.Drawing.Size(210, 109)
        Me.gbRohGruppe.TabIndex = 23
        Me.gbRohGruppe.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(5, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 13)
        Me.Label3.TabIndex = 77
        Me.Label3.Text = "Rohstoff-Gruppe 2"
        '
        'cbRohstoffGrp2
        '
        Me.cbRohstoffGrp2.FormattingEnabled = True
        Me.cbRohstoffGrp2.Location = New System.Drawing.Point(8, 77)
        Me.cbRohstoffGrp2.Name = "cbRohstoffGrp2"
        Me.cbRohstoffGrp2.Size = New System.Drawing.Size(195, 21)
        Me.cbRohstoffGrp2.TabIndex = 76
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label14.Location = New System.Drawing.Point(5, 18)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(94, 13)
        Me.Label14.TabIndex = 75
        Me.Label14.Text = "Rohstoff-Gruppe 1"
        '
        'cbRohstoffGrp1
        '
        Me.cbRohstoffGrp1.FormattingEnabled = True
        Me.cbRohstoffGrp1.Location = New System.Drawing.Point(8, 37)
        Me.cbRohstoffGrp1.Name = "cbRohstoffGrp1"
        Me.cbRohstoffGrp1.Size = New System.Drawing.Size(195, 21)
        Me.cbRohstoffGrp1.TabIndex = 74
        '
        'wb_ListeStatistik
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gbRohGruppe)
        Me.Controls.Add(Me.gbLinien)
        Me.Controls.Add(Me.gbFilter)
        Me.Controls.Add(Me.BtnListeLeeren)
        Me.Controls.Add(Me.BtnListeLaden)
        Me.Controls.Add(Me.BtnListeSpeichern)
        Me.Controls.Add(Me.BtnListeRemove)
        Me.Controls.Add(Me.BtnListeAdd)
        Me.Controls.Add(Me.tpListe)
        Me.Name = "wb_ListeStatistik"
        Me.Size = New System.Drawing.Size(554, 613)
        Me.gbFilter.ResumeLayout(False)
        Me.gbFilter.PerformLayout()
        Me.gbLinien.ResumeLayout(False)
        Me.gbRohGruppe.ResumeLayout(False)
        Me.gbRohGruppe.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents gbFilter As System.Windows.Forms.GroupBox
    Friend WithEvents cbIstwertNullAusblenden As System.Windows.Forms.CheckBox
    Friend WithEvents cbWasserTempAusblenden As System.Windows.Forms.CheckBox
    Friend WithEvents cbElementeAusListe As System.Windows.Forms.CheckBox
    Friend WithEvents cbAlleLinien As System.Windows.Forms.CheckBox
    Friend WithEvents dtFilterVon As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFilterBis As System.Windows.Forms.Label
    Friend WithEvents lblFilterVon As System.Windows.Forms.Label
    Friend WithEvents dtFilterBis As System.Windows.Forms.DateTimePicker
    Friend WithEvents BtnListeLeeren As System.Windows.Forms.Button
    Friend WithEvents BtnListeLaden As System.Windows.Forms.Button
    Friend WithEvents BtnListeSpeichern As System.Windows.Forms.Button
    Friend WithEvents BtnListeRemove As System.Windows.Forms.Button
    Friend WithEvents BtnListeAdd As System.Windows.Forms.Button
    Friend WithEvents tpListe As System.Windows.Forms.Panel
    Friend WithEvents cbLinien As System.Windows.Forms.CheckedListBox
    Friend WithEvents gbLinien As System.Windows.Forms.GroupBox
    Friend WithEvents cbUhrzeitBis As System.Windows.Forms.CheckBox
    Friend WithEvents cbUhrzeitVon As System.Windows.Forms.CheckBox
    Friend WithEvents dtUhrzeitBis As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtUhrzeitVon As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gbRohGruppe As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbRohstoffGrp2 As wb_ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cbRohstoffGrp1 As wb_ComboBox
End Class
