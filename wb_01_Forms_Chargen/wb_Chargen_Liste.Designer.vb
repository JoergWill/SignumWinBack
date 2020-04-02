Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Chargen_Liste
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.gbFilter = New System.Windows.Forms.GroupBox()
        Me.cbAlleLinien = New System.Windows.Forms.CheckBox()
        Me.lblFilterBis = New System.Windows.Forms.Label()
        Me.lblFilterVon = New System.Windows.Forms.Label()
        Me.dtFilterBis = New System.Windows.Forms.DateTimePicker()
        Me.cbFilter = New System.Windows.Forms.CheckBox()
        Me.dtFilterVon = New System.Windows.Forms.DateTimePicker()
        Me.GrpBoxSort = New System.Windows.Forms.GroupBox()
        Me.rbProduktion = New System.Windows.Forms.RadioButton()
        Me.rbArtikelNummer = New System.Windows.Forms.RadioButton()
        Me.rbArtikel = New System.Windows.Forms.RadioButton()
        Me.BtnDrucken = New System.Windows.Forms.Button()
        Me.BtnBerechnen = New System.Windows.Forms.Button()
        Me.DataGridView = New WinBack.wb_DataGridView()
        Me.gbFilter.SuspendLayout()
        Me.GrpBoxSort.SuspendLayout()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbFilter
        '
        Me.gbFilter.Controls.Add(Me.cbAlleLinien)
        Me.gbFilter.Controls.Add(Me.lblFilterBis)
        Me.gbFilter.Controls.Add(Me.lblFilterVon)
        Me.gbFilter.Controls.Add(Me.dtFilterBis)
        Me.gbFilter.Controls.Add(Me.cbFilter)
        Me.gbFilter.Controls.Add(Me.dtFilterVon)
        Me.gbFilter.Location = New System.Drawing.Point(373, 0)
        Me.gbFilter.Name = "gbFilter"
        Me.gbFilter.Size = New System.Drawing.Size(212, 180)
        Me.gbFilter.TabIndex = 5
        Me.gbFilter.TabStop = False
        '
        'cbAlleLinien
        '
        Me.cbAlleLinien.AutoSize = True
        Me.cbAlleLinien.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbAlleLinien.Location = New System.Drawing.Point(6, 141)
        Me.cbAlleLinien.Name = "cbAlleLinien"
        Me.cbAlleLinien.Size = New System.Drawing.Size(102, 17)
        Me.cbAlleLinien.TabIndex = 13
        Me.cbAlleLinien.Text = "alle Linien laden"
        Me.cbAlleLinien.UseVisualStyleBackColor = True
        '
        'lblFilterBis
        '
        Me.lblFilterBis.AutoSize = True
        Me.lblFilterBis.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFilterBis.Location = New System.Drawing.Point(6, 86)
        Me.lblFilterBis.Name = "lblFilterBis"
        Me.lblFilterBis.Size = New System.Drawing.Size(55, 13)
        Me.lblFilterBis.TabIndex = 12
        Me.lblFilterBis.Text = "Bis Datum"
        '
        'lblFilterVon
        '
        Me.lblFilterVon.AutoSize = True
        Me.lblFilterVon.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFilterVon.Location = New System.Drawing.Point(6, 40)
        Me.lblFilterVon.Name = "lblFilterVon"
        Me.lblFilterVon.Size = New System.Drawing.Size(60, 13)
        Me.lblFilterVon.TabIndex = 11
        Me.lblFilterVon.Text = "Von Datum"
        '
        'dtFilterBis
        '
        Me.dtFilterBis.Location = New System.Drawing.Point(6, 102)
        Me.dtFilterBis.Name = "dtFilterBis"
        Me.dtFilterBis.Size = New System.Drawing.Size(200, 20)
        Me.dtFilterBis.TabIndex = 10
        '
        'cbFilter
        '
        Me.cbFilter.AutoSize = True
        Me.cbFilter.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbFilter.Location = New System.Drawing.Point(6, 11)
        Me.cbFilter.Name = "cbFilter"
        Me.cbFilter.Size = New System.Drawing.Size(79, 17)
        Me.cbFilter.TabIndex = 9
        Me.cbFilter.Text = "Liste Filtern"
        Me.cbFilter.UseVisualStyleBackColor = True
        '
        'dtFilterVon
        '
        Me.dtFilterVon.Location = New System.Drawing.Point(6, 56)
        Me.dtFilterVon.Name = "dtFilterVon"
        Me.dtFilterVon.Size = New System.Drawing.Size(200, 20)
        Me.dtFilterVon.TabIndex = 8
        '
        'GrpBoxSort
        '
        Me.GrpBoxSort.Controls.Add(Me.rbProduktion)
        Me.GrpBoxSort.Controls.Add(Me.rbArtikelNummer)
        Me.GrpBoxSort.Controls.Add(Me.rbArtikel)
        Me.GrpBoxSort.Location = New System.Drawing.Point(373, 176)
        Me.GrpBoxSort.Name = "GrpBoxSort"
        Me.GrpBoxSort.Size = New System.Drawing.Size(212, 92)
        Me.GrpBoxSort.TabIndex = 6
        Me.GrpBoxSort.TabStop = False
        Me.GrpBoxSort.Text = "Sortiert nach"
        '
        'rbProduktion
        '
        Me.rbProduktion.AutoSize = True
        Me.rbProduktion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rbProduktion.Location = New System.Drawing.Point(6, 65)
        Me.rbProduktion.Name = "rbProduktion"
        Me.rbProduktion.Size = New System.Drawing.Size(141, 17)
        Me.rbProduktion.TabIndex = 5
        Me.rbProduktion.Text = "Produktions-Reihenfolge"
        Me.rbProduktion.UseVisualStyleBackColor = True
        '
        'rbArtikelNummer
        '
        Me.rbArtikelNummer.AutoSize = True
        Me.rbArtikelNummer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rbArtikelNummer.Location = New System.Drawing.Point(6, 42)
        Me.rbArtikelNummer.Name = "rbArtikelNummer"
        Me.rbArtikelNummer.Size = New System.Drawing.Size(96, 17)
        Me.rbArtikelNummer.TabIndex = 4
        Me.rbArtikelNummer.Text = "Artikel-Nummer"
        Me.rbArtikelNummer.UseVisualStyleBackColor = True
        '
        'rbArtikel
        '
        Me.rbArtikel.AutoSize = True
        Me.rbArtikel.Checked = True
        Me.rbArtikel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rbArtikel.Location = New System.Drawing.Point(6, 19)
        Me.rbArtikel.Name = "rbArtikel"
        Me.rbArtikel.Size = New System.Drawing.Size(119, 17)
        Me.rbArtikel.TabIndex = 3
        Me.rbArtikel.TabStop = True
        Me.rbArtikel.Text = "Artikel-Bezeichnung"
        Me.rbArtikel.UseVisualStyleBackColor = True
        '
        'BtnDrucken
        '
        Me.BtnDrucken.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnDrucken.Location = New System.Drawing.Point(373, 551)
        Me.BtnDrucken.Name = "BtnDrucken"
        Me.BtnDrucken.Size = New System.Drawing.Size(212, 43)
        Me.BtnDrucken.TabIndex = 20
        Me.BtnDrucken.Text = "Drucken"
        Me.BtnDrucken.UseVisualStyleBackColor = True
        '
        'BtnBerechnen
        '
        Me.BtnBerechnen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnBerechnen.Location = New System.Drawing.Point(373, 600)
        Me.BtnBerechnen.Name = "BtnBerechnen"
        Me.BtnBerechnen.Size = New System.Drawing.Size(212, 43)
        Me.BtnBerechnen.TabIndex = 19
        Me.BtnBerechnen.Text = "Berechnen"
        Me.BtnBerechnen.UseVisualStyleBackColor = True
        '
        'DataGridView
        '
        Me.DataGridView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView.Name = "DataGridView"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView.Size = New System.Drawing.Size(367, 655)
        Me.DataGridView.SortCol = -1
        Me.DataGridView.TabIndex = 4
        Me.DataGridView.x8859_5_FieldName = ""
        '
        'wb_Chargen_Liste
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(595, 655)
        Me.CloseButton = False
        Me.CloseButtonVisible = False
        Me.Controls.Add(Me.BtnDrucken)
        Me.Controls.Add(Me.BtnBerechnen)
        Me.Controls.Add(Me.GrpBoxSort)
        Me.Controls.Add(Me.gbFilter)
        Me.Controls.Add(Me.DataGridView)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HideOnClose = True
        Me.Name = "wb_Chargen_Liste"
        Me.Text = "Chargen Liste"
        Me.gbFilter.ResumeLayout(False)
        Me.gbFilter.PerformLayout()
        Me.GrpBoxSort.ResumeLayout(False)
        Me.GrpBoxSort.PerformLayout()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridView As wb_DataGridView
    Friend WithEvents gbFilter As Windows.Forms.GroupBox
    Friend WithEvents lblFilterBis As Windows.Forms.Label
    Friend WithEvents lblFilterVon As Windows.Forms.Label
    Friend WithEvents dtFilterBis As Windows.Forms.DateTimePicker
    Friend WithEvents cbFilter As Windows.Forms.CheckBox
    Friend WithEvents dtFilterVon As Windows.Forms.DateTimePicker
    Friend WithEvents cbAlleLinien As Windows.Forms.CheckBox
    Friend WithEvents GrpBoxSort As Windows.Forms.GroupBox
    Friend WithEvents rbProduktion As Windows.Forms.RadioButton
    Friend WithEvents rbArtikelNummer As Windows.Forms.RadioButton
    Friend WithEvents rbArtikel As Windows.Forms.RadioButton
    Friend WithEvents BtnDrucken As Windows.Forms.Button
    Friend WithEvents BtnBerechnen As Windows.Forms.Button
End Class
