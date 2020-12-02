Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Planung_Drucken
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Planung_Drucken))
        Me.lblLinieArtikel = New System.Windows.Forms.Label()
        Me.lblLinienGruppe = New System.Windows.Forms.Label()
        Me.clbLiniengruppe = New WinBack.wb_CheckedListBox()
        Me.clbArtikelLinienGruppe = New WinBack.wb_CheckedListBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'lblLinieArtikel
        '
        Me.lblLinieArtikel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLinieArtikel.AutoSize = True
        Me.lblLinieArtikel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblLinieArtikel.Location = New System.Drawing.Point(188, 9)
        Me.lblLinieArtikel.Name = "lblLinieArtikel"
        Me.lblLinieArtikel.Size = New System.Drawing.Size(131, 13)
        Me.lblLinieArtikel.TabIndex = 67
        Me.lblLinieArtikel.Text = "Liniengruppe Aufarbeitung"
        '
        'lblLinienGruppe
        '
        Me.lblLinienGruppe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLinienGruppe.AutoSize = True
        Me.lblLinienGruppe.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblLinienGruppe.Location = New System.Drawing.Point(12, 9)
        Me.lblLinienGruppe.Name = "lblLinienGruppe"
        Me.lblLinienGruppe.Size = New System.Drawing.Size(92, 13)
        Me.lblLinienGruppe.TabIndex = 66
        Me.lblLinienGruppe.Text = "Liniengruppe Teig"
        '
        'clbLiniengruppe
        '
        Me.clbLiniengruppe.FormattingEnabled = True
        Me.clbLiniengruppe.Location = New System.Drawing.Point(12, 31)
        Me.clbLiniengruppe.MultiColumn = True
        Me.clbLiniengruppe.Name = "clbLiniengruppe"
        Me.clbLiniengruppe.SelIndex = 0
        Me.clbLiniengruppe.Size = New System.Drawing.Size(151, 334)
        Me.clbLiniengruppe.TabIndex = 68
        '
        'clbArtikelLinienGruppe
        '
        Me.clbArtikelLinienGruppe.FormattingEnabled = True
        Me.clbArtikelLinienGruppe.Location = New System.Drawing.Point(191, 31)
        Me.clbArtikelLinienGruppe.Name = "clbArtikelLinienGruppe"
        Me.clbArtikelLinienGruppe.SelIndex = 0
        Me.clbArtikelLinienGruppe.Size = New System.Drawing.Size(151, 334)
        Me.clbArtikelLinienGruppe.TabIndex = 65
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(368, 31)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox1.Size = New System.Drawing.Size(106, 95)
        Me.ListBox1.TabIndex = 69
        '
        'wb_Planung_Drucken
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(564, 468)
        Me.ControlBox = False
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.clbLiniengruppe)
        Me.Controls.Add(Me.lblLinieArtikel)
        Me.Controls.Add(Me.lblLinienGruppe)
        Me.Controls.Add(Me.clbArtikelLinienGruppe)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wb_Planung_Drucken"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "Produktions-Unterlagen Sammeldruck"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents clbArtikelLinienGruppe As wb_CheckedListBox
    Friend WithEvents lblLinieArtikel As Windows.Forms.Label
    Friend WithEvents lblLinienGruppe As Windows.Forms.Label
    Friend WithEvents clbLiniengruppe As wb_CheckedListBox
    Friend WithEvents ListBox1 As Windows.Forms.ListBox
End Class
