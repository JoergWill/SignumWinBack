Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_OrgaBackParams
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
        Me.pnlArtikelGruppen = New System.Windows.Forms.Panel()
        Me.lblSortimente = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlSortimente = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlArtikelGruppen
        '
        Me.pnlArtikelGruppen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlArtikelGruppen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlArtikelGruppen.Location = New System.Drawing.Point(304, 59)
        Me.pnlArtikelGruppen.Name = "pnlArtikelGruppen"
        Me.pnlArtikelGruppen.Size = New System.Drawing.Size(295, 410)
        Me.pnlArtikelGruppen.TabIndex = 2
        '
        'lblSortimente
        '
        Me.lblSortimente.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSortimente.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSortimente.Location = New System.Drawing.Point(3, 0)
        Me.lblSortimente.Name = "lblSortimente"
        Me.lblSortimente.Size = New System.Drawing.Size(218, 56)
        Me.lblSortimente.TabIndex = 3
        Me.lblSortimente.Text = "OrgaBack Sortimente mit Filiale Typ Produktion"
        Me.lblSortimente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(304, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(218, 56)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "OrgaBack Artikelgruppen"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.pnlSortimente, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pnlArtikelGruppen, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblSortimente, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(602, 472)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'pnlSortimente
        '
        Me.pnlSortimente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSortimente.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSortimente.Location = New System.Drawing.Point(3, 59)
        Me.pnlSortimente.Name = "pnlSortimente"
        Me.pnlSortimente.Size = New System.Drawing.Size(295, 410)
        Me.pnlSortimente.TabIndex = 2
        '
        'wb_Admin_OrgaBackParams
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(602, 472)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HideOnClose = True
        Me.Name = "wb_Admin_OrgaBackParams"
        Me.Text = "OrgaBack Einstellungen"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlArtikelGruppen As Windows.Forms.Panel
    Friend WithEvents lblSortimente As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSortimente As Windows.Forms.Panel
End Class
