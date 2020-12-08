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
        Me.pnlLinienGruppe = New System.Windows.Forms.Panel()
        Me.pnlAufarbeitung = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnPrintTeigZettel = New System.Windows.Forms.Button()
        Me.BtnPrintAufarbeitung = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblLinieArtikel
        '
        Me.lblLinieArtikel.AutoSize = True
        Me.lblLinieArtikel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblLinieArtikel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblLinieArtikel.Location = New System.Drawing.Point(243, 7)
        Me.lblLinieArtikel.Name = "lblLinieArtikel"
        Me.lblLinieArtikel.Size = New System.Drawing.Size(253, 13)
        Me.lblLinieArtikel.TabIndex = 67
        Me.lblLinieArtikel.Text = "Aufarbeitung"
        '
        'lblLinienGruppe
        '
        Me.lblLinienGruppe.AutoSize = True
        Me.lblLinienGruppe.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblLinienGruppe.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblLinienGruppe.Location = New System.Drawing.Point(18, 7)
        Me.lblLinienGruppe.Name = "lblLinienGruppe"
        Me.lblLinienGruppe.Size = New System.Drawing.Size(205, 13)
        Me.lblLinienGruppe.TabIndex = 66
        Me.lblLinienGruppe.Text = "Teigzettel"
        '
        'pnlLinienGruppe
        '
        Me.pnlLinienGruppe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLinienGruppe.Location = New System.Drawing.Point(18, 23)
        Me.pnlLinienGruppe.Name = "pnlLinienGruppe"
        Me.pnlLinienGruppe.Size = New System.Drawing.Size(205, 319)
        Me.pnlLinienGruppe.TabIndex = 68
        '
        'pnlAufarbeitung
        '
        Me.pnlAufarbeitung.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAufarbeitung.Location = New System.Drawing.Point(243, 23)
        Me.pnlAufarbeitung.Name = "pnlAufarbeitung"
        Me.pnlAufarbeitung.Size = New System.Drawing.Size(253, 319)
        Me.pnlAufarbeitung.TabIndex = 69
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 19.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.BtnPrintTeigZettel, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblLinieArtikel, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblLinienGruppe, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pnlLinienGruppe, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.pnlAufarbeitung, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnPrintAufarbeitung, 3, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(519, 391)
        Me.TableLayoutPanel1.TabIndex = 71
        '
        'BtnPrintTeigZettel
        '
        Me.BtnPrintTeigZettel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BtnPrintTeigZettel.Location = New System.Drawing.Point(18, 348)
        Me.BtnPrintTeigZettel.Name = "BtnPrintTeigZettel"
        Me.BtnPrintTeigZettel.Size = New System.Drawing.Size(205, 40)
        Me.BtnPrintTeigZettel.TabIndex = 73
        Me.BtnPrintTeigZettel.Text = "Drucken Teigzettel"
        Me.BtnPrintTeigZettel.UseVisualStyleBackColor = True
        '
        'BtnPrintAufarbeitung
        '
        Me.BtnPrintAufarbeitung.Location = New System.Drawing.Point(243, 348)
        Me.BtnPrintAufarbeitung.Name = "BtnPrintAufarbeitung"
        Me.BtnPrintAufarbeitung.Size = New System.Drawing.Size(253, 40)
        Me.BtnPrintAufarbeitung.TabIndex = 74
        Me.BtnPrintAufarbeitung.Text = "Drucken Aufarbeitung"
        Me.BtnPrintAufarbeitung.UseVisualStyleBackColor = True
        '
        'wb_Planung_Drucken
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(519, 391)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wb_Planung_Drucken"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "Produktions-Unterlagen Sammeldruck"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblLinieArtikel As Windows.Forms.Label
    Friend WithEvents lblLinienGruppe As Windows.Forms.Label
    Friend WithEvents pnlLinienGruppe As Windows.Forms.Panel
    Friend WithEvents pnlAufarbeitung As Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As Windows.Forms.TableLayoutPanel
    Friend WithEvents BtnPrintTeigZettel As Windows.Forms.Button
    Friend WithEvents BtnPrintAufarbeitung As Windows.Forms.Button
End Class
