Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Chargen_Funktionen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Chargen_Funktionen))
        Me.dtFilterVon = New System.Windows.Forms.DateTimePicker()
        Me.cbFilter = New System.Windows.Forms.CheckBox()
        Me.rbArtikel = New System.Windows.Forms.RadioButton()
        Me.GrpBoxSort = New System.Windows.Forms.GroupBox()
        Me.rbProduktion = New System.Windows.Forms.RadioButton()
        Me.rbArtikelNummer = New System.Windows.Forms.RadioButton()
        Me.dtFilterBis = New System.Windows.Forms.DateTimePicker()
        Me.lblFilterVon = New System.Windows.Forms.Label()
        Me.lblFilterBis = New System.Windows.Forms.Label()
        Me.cbAlleLinien = New System.Windows.Forms.CheckBox()
        Me.BtnDetails = New System.Windows.Forms.Button()
        Me.GrpBoxSort.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtFilterVon
        '
        resources.ApplyResources(Me.dtFilterVon, "dtFilterVon")
        Me.dtFilterVon.Name = "dtFilterVon"
        '
        'cbFilter
        '
        resources.ApplyResources(Me.cbFilter, "cbFilter")
        Me.cbFilter.Name = "cbFilter"
        Me.cbFilter.UseVisualStyleBackColor = True
        '
        'rbArtikel
        '
        resources.ApplyResources(Me.rbArtikel, "rbArtikel")
        Me.rbArtikel.Checked = True
        Me.rbArtikel.Name = "rbArtikel"
        Me.rbArtikel.TabStop = True
        Me.rbArtikel.UseVisualStyleBackColor = True
        '
        'GrpBoxSort
        '
        Me.GrpBoxSort.Controls.Add(Me.rbProduktion)
        Me.GrpBoxSort.Controls.Add(Me.rbArtikelNummer)
        Me.GrpBoxSort.Controls.Add(Me.rbArtikel)
        resources.ApplyResources(Me.GrpBoxSort, "GrpBoxSort")
        Me.GrpBoxSort.Name = "GrpBoxSort"
        Me.GrpBoxSort.TabStop = False
        '
        'rbProduktion
        '
        resources.ApplyResources(Me.rbProduktion, "rbProduktion")
        Me.rbProduktion.Name = "rbProduktion"
        Me.rbProduktion.UseVisualStyleBackColor = True
        '
        'rbArtikelNummer
        '
        resources.ApplyResources(Me.rbArtikelNummer, "rbArtikelNummer")
        Me.rbArtikelNummer.Name = "rbArtikelNummer"
        Me.rbArtikelNummer.UseVisualStyleBackColor = True
        '
        'dtFilterBis
        '
        resources.ApplyResources(Me.dtFilterBis, "dtFilterBis")
        Me.dtFilterBis.Name = "dtFilterBis"
        '
        'lblFilterVon
        '
        resources.ApplyResources(Me.lblFilterVon, "lblFilterVon")
        Me.lblFilterVon.Name = "lblFilterVon"
        '
        'lblFilterBis
        '
        resources.ApplyResources(Me.lblFilterBis, "lblFilterBis")
        Me.lblFilterBis.Name = "lblFilterBis"
        '
        'cbAlleLinien
        '
        resources.ApplyResources(Me.cbAlleLinien, "cbAlleLinien")
        Me.cbAlleLinien.Name = "cbAlleLinien"
        Me.cbAlleLinien.UseVisualStyleBackColor = True
        '
        'BtnDetails
        '
        resources.ApplyResources(Me.BtnDetails, "BtnDetails")
        Me.BtnDetails.Name = "BtnDetails"
        Me.BtnDetails.UseVisualStyleBackColor = True
        '
        'wb_Chargen_Funktionen
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.BtnDetails)
        Me.Controls.Add(Me.cbAlleLinien)
        Me.Controls.Add(Me.lblFilterBis)
        Me.Controls.Add(Me.lblFilterVon)
        Me.Controls.Add(Me.dtFilterBis)
        Me.Controls.Add(Me.GrpBoxSort)
        Me.Controls.Add(Me.cbFilter)
        Me.Controls.Add(Me.dtFilterVon)
        Me.Name = "wb_Chargen_Funktionen"
        Me.GrpBoxSort.ResumeLayout(False)
        Me.GrpBoxSort.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dtFilterVon As Windows.Forms.DateTimePicker
    Friend WithEvents cbFilter As Windows.Forms.CheckBox
    Friend WithEvents rbArtikel As Windows.Forms.RadioButton
    Friend WithEvents GrpBoxSort As Windows.Forms.GroupBox
    Friend WithEvents rbArtikelNummer As Windows.Forms.RadioButton
    Friend WithEvents rbProduktion As Windows.Forms.RadioButton
    Friend WithEvents dtFilterBis As Windows.Forms.DateTimePicker
    Friend WithEvents lblFilterVon As Windows.Forms.Label
    Friend WithEvents lblFilterBis As Windows.Forms.Label
    Friend WithEvents cbAlleLinien As Windows.Forms.CheckBox
    Friend WithEvents BtnDetails As Windows.Forms.Button
End Class
