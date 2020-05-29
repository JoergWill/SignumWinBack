Imports WeifenLuo.WinFormsUI.Docking
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Linien_Auto
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Linien_Auto))
        Dim ObjectCellBinding1 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding2 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding3 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding4 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding5 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding6 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding7 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding8 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding9 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Me.ColNummer = New Infralution.Controls.VirtualTree.Column()
        Me.ColCharge = New Infralution.Controls.VirtualTree.Column()
        Me.ColBezeichnung = New Infralution.Controls.VirtualTree.Column()
        Me.ColLinie = New Infralution.Controls.VirtualTree.Column()
        Me.ColSollwert = New Infralution.Controls.VirtualTree.Column()
        Me.ColEinheit = New Infralution.Controls.VirtualTree.Column()
        Me.ColIstwert = New Infralution.Controls.VirtualTree.Column()
        Me.ColEinheitIstwert = New Infralution.Controls.VirtualTree.Column()
        Me.ColStartZeit = New Infralution.Controls.VirtualTree.Column()
        Me.VirtualTree = New Infralution.Controls.VirtualTree.VirtualTree()
        Me.CellEditor2 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.CellEditor1 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CellEditor3 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.UniversalEditBox1 = New Infralution.Controls.UniversalEditBox()
        Me.CellEditor4 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.BtnStart = New System.Windows.Forms.Button()
        Me.lblLinie = New System.Windows.Forms.Label()
        Me.BtnAktualisieren = New System.Windows.Forms.Button()
        Me.cbLinien = New WinBack.wb_ComboBox()
        Me.ObjectRowBinding1 = New Infralution.Controls.VirtualTree.ObjectRowBinding()
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColNummer
        '
        Me.ColNummer.Caption = "Nummer"
        Me.ColNummer.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColNummer.MinWidth = 150
        Me.ColNummer.Movable = False
        Me.ColNummer.Name = "ColNummer"
        Me.ColNummer.Sortable = False
        Me.ColNummer.Width = 189
        '
        'ColCharge
        '
        Me.ColCharge.Caption = "Charge"
        Me.ColCharge.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColCharge.MinWidth = 80
        Me.ColCharge.Movable = False
        Me.ColCharge.Name = "ColCharge"
        Me.ColCharge.Sortable = False
        Me.ColCharge.Width = 119
        '
        'ColBezeichnung
        '
        Me.ColBezeichnung.Caption = "Bezeichnung"
        Me.ColBezeichnung.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColBezeichnung.CellStyle.VertAlignment = System.Drawing.StringAlignment.Center
        Me.ColBezeichnung.MinWidth = 250
        Me.ColBezeichnung.Movable = False
        Me.ColBezeichnung.Name = "ColBezeichnung"
        Me.ColBezeichnung.Sortable = False
        Me.ColBezeichnung.Width = 289
        '
        'ColLinie
        '
        Me.ColLinie.Caption = "Linie"
        Me.ColLinie.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColLinie.CellStyle.HorzAlignment = System.Drawing.StringAlignment.Center
        Me.ColLinie.MinWidth = 50
        Me.ColLinie.Name = "ColLinie"
        Me.ColLinie.Resizable = False
        Me.ColLinie.Sortable = False
        Me.ColLinie.Width = 50
        '
        'ColSollwert
        '
        Me.ColSollwert.Caption = "Sollwert"
        Me.ColSollwert.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColSollwert.CellStyle.HorzAlignment = System.Drawing.StringAlignment.Far
        Me.ColSollwert.MinWidth = 100
        Me.ColSollwert.Name = "ColSollwert"
        Me.ColSollwert.Resizable = False
        Me.ColSollwert.Sortable = False
        '
        'ColEinheit
        '
        Me.ColEinheit.Caption = ""
        Me.ColEinheit.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColEinheit.Name = "ColEinheit"
        Me.ColEinheit.Resizable = False
        Me.ColEinheit.Sortable = False
        Me.ColEinheit.Width = 30
        '
        'ColIstwert
        '
        Me.ColIstwert.Caption = "Istwert"
        Me.ColIstwert.CellStyle.HorzAlignment = System.Drawing.StringAlignment.Far
        Me.ColIstwert.MinWidth = 100
        Me.ColIstwert.Name = "ColIstwert"
        Me.ColIstwert.Width = 139
        '
        'ColEinheitIstwert
        '
        Me.ColEinheitIstwert.Caption = Nothing
        Me.ColEinheitIstwert.Name = "ColEinheitIstwert"
        Me.ColEinheitIstwert.Width = 69
        '
        'ColStartZeit
        '
        Me.ColStartZeit.Caption = "Start"
        Me.ColStartZeit.CellStyle.HorzAlignment = System.Drawing.StringAlignment.Center
        Me.ColStartZeit.MinWidth = 100
        Me.ColStartZeit.Name = "ColStartZeit"
        Me.ColStartZeit.Width = 139
        '
        'VirtualTree
        '
        Me.VirtualTree.AllowMultiSelect = False
        Me.VirtualTree.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VirtualTree.AutoFitColumns = True
        Me.VirtualTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VirtualTree.CollapseImage = CType(resources.GetObject("VirtualTree.CollapseImage"), System.Drawing.Image)
        Me.VirtualTree.Columns.Add(Me.ColCharge)
        Me.VirtualTree.Columns.Add(Me.ColNummer)
        Me.VirtualTree.Columns.Add(Me.ColBezeichnung)
        Me.VirtualTree.Columns.Add(Me.ColLinie)
        Me.VirtualTree.Columns.Add(Me.ColStartZeit)
        Me.VirtualTree.Columns.Add(Me.ColSollwert)
        Me.VirtualTree.Columns.Add(Me.ColEinheit)
        Me.VirtualTree.Columns.Add(Me.ColIstwert)
        Me.VirtualTree.Columns.Add(Me.ColEinheitIstwert)
        Me.VirtualTree.EditOnKeyPress = True
        Me.VirtualTree.Editors.Add(Me.CellEditor2)
        Me.VirtualTree.Editors.Add(Me.CellEditor1)
        Me.VirtualTree.Editors.Add(Me.CellEditor3)
        Me.VirtualTree.Editors.Add(Me.CellEditor4)
        Me.VirtualTree.ExpandImage = CType(resources.GetObject("VirtualTree.ExpandImage"), System.Drawing.Image)
        Me.VirtualTree.HeaderHeight = 24
        Me.VirtualTree.HeaderStyle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VirtualTree.LineStyle = Infralution.Controls.VirtualTree.LineStyle.Solid
        Me.VirtualTree.Location = New System.Drawing.Point(-1, 72)
        Me.VirtualTree.MainColumn = Me.ColNummer
        Me.VirtualTree.Name = "VirtualTree"
        Me.VirtualTree.RowBindings.Add(Me.ObjectRowBinding1)
        Me.VirtualTree.RowEvenStyle.BackColor = System.Drawing.Color.White
        Me.VirtualTree.RowStyle.BorderColor = System.Drawing.Color.LightGray
        Me.VirtualTree.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.Cell
        Me.VirtualTree.ShowRootRow = False
        Me.VirtualTree.Size = New System.Drawing.Size(1129, 560)
        Me.VirtualTree.StyleTemplate = Infralution.Controls.VirtualTree.StyleTemplate.Vista
        Me.VirtualTree.TabIndex = 16
        '
        'CellEditor2
        '
        Me.CellEditor2.Control = Me.RichTextBox1
        Me.CellEditor2.UseCellPadding = True
        '
        'RichTextBox1
        '
        Me.RichTextBox1.AcceptsTab = True
        Me.RichTextBox1.AutoWordSelection = True
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBox1.Location = New System.Drawing.Point(-94, -123)
        Me.RichTextBox1.Multiline = False
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.RichTextBox1.Size = New System.Drawing.Size(79, 96)
        Me.RichTextBox1.TabIndex = 12
        Me.RichTextBox1.Text = ""
        Me.RichTextBox1.Visible = False
        '
        'CellEditor1
        '
        Me.CellEditor1.Control = Me.Label1
        '
        'Label1
        '
        Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(-132, -105)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 10)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "dfsdfsdfasdfasf"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CellEditor3
        '
        Me.CellEditor3.Control = Me.UniversalEditBox1
        Me.CellEditor3.UseCellPadding = True
        '
        'UniversalEditBox1
        '
        Me.UniversalEditBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UniversalEditBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UniversalEditBox1.Location = New System.Drawing.Point(-94, -123)
        Me.UniversalEditBox1.Name = "UniversalEditBox1"
        Me.UniversalEditBox1.Size = New System.Drawing.Size(195, 19)
        Me.UniversalEditBox1.TabIndex = 14
        Me.UniversalEditBox1.Visible = False
        '
        'CellEditor4
        '
        Me.CellEditor4.Control = Me.TextBox1
        Me.CellEditor4.UseCellPadding = True
        '
        'TextBox1
        '
        Me.TextBox1.AcceptsReturn = True
        Me.TextBox1.AcceptsTab = True
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.HideSelection = False
        Me.TextBox1.Location = New System.Drawing.Point(-94, -123)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(79, 13)
        Me.TextBox1.TabIndex = 15
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox1.Visible = False
        Me.TextBox1.WordWrap = False
        '
        'BtnStart
        '
        Me.BtnStart.BackColor = System.Drawing.Color.Lime
        Me.BtnStart.Location = New System.Drawing.Point(0, 5)
        Me.BtnStart.Name = "BtnStart"
        Me.BtnStart.Size = New System.Drawing.Size(122, 61)
        Me.BtnStart.TabIndex = 1
        Me.BtnStart.Text = "Start"
        Me.BtnStart.UseVisualStyleBackColor = False
        '
        'lblLinie
        '
        Me.lblLinie.AutoSize = True
        Me.lblLinie.Location = New System.Drawing.Point(128, 28)
        Me.lblLinie.Name = "lblLinie"
        Me.lblLinie.Size = New System.Drawing.Size(81, 13)
        Me.lblLinie.TabIndex = 19
        Me.lblLinie.Text = "Produktionslinie"
        '
        'BtnAktualisieren
        '
        Me.BtnAktualisieren.Location = New System.Drawing.Point(216, 5)
        Me.BtnAktualisieren.Name = "BtnAktualisieren"
        Me.BtnAktualisieren.Size = New System.Drawing.Size(93, 36)
        Me.BtnAktualisieren.TabIndex = 20
        Me.BtnAktualisieren.Text = "Aktualisieren"
        Me.BtnAktualisieren.UseVisualStyleBackColor = True
        '
        'cbLinien
        '
        Me.cbLinien.FormattingEnabled = True
        Me.cbLinien.Location = New System.Drawing.Point(128, 45)
        Me.cbLinien.Name = "cbLinien"
        Me.cbLinien.Size = New System.Drawing.Size(181, 21)
        Me.cbLinien.TabIndex = 18
        Me.cbLinien.TabStop = False
        '
        'ObjectRowBinding1
        '
        ObjectCellBinding1.Column = Me.ColNummer
        ObjectCellBinding1.Field = "VirtTreeNummer"
        ObjectCellBinding2.Column = Me.ColCharge
        ObjectCellBinding2.Field = "VirtTreeCharge"
        ObjectCellBinding3.Column = Me.ColBezeichnung
        ObjectCellBinding3.Field = "VirtTreeBezeichnung"
        ObjectCellBinding4.Column = Me.ColLinie
        ObjectCellBinding4.Field = "VirtTreeLinie"
        ObjectCellBinding5.Column = Me.ColSollwert
        ObjectCellBinding5.Field = "VirtTreeSollwert"
        ObjectCellBinding6.Column = Me.ColEinheit
        ObjectCellBinding6.Field = "VirtTreeEinheit"
        ObjectCellBinding7.Column = Me.ColIstwert
        ObjectCellBinding7.Field = "VirtTreeIstwert"
        ObjectCellBinding8.Column = Me.ColEinheitIstwert
        ObjectCellBinding8.Field = "VirtTreeEinheit"
        ObjectCellBinding9.Column = Me.ColStartZeit
        ObjectCellBinding9.Field = "VirtTreeStart"
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding1)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding2)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding3)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding4)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding5)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding6)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding7)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding8)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding9)
        Me.ObjectRowBinding1.ChildProperty = "ChildSteps"
        Me.ObjectRowBinding1.ExpandedImage = CType(resources.GetObject("ObjectRowBinding1.ExpandedImage"), System.Drawing.Image)
        Me.ObjectRowBinding1.Image = CType(resources.GetObject("ObjectRowBinding1.Image"), System.Drawing.Image)
        Me.ObjectRowBinding1.Name = "ObjectRowBinding1"
        Me.ObjectRowBinding1.ParentProperty = "ParentStep"
        Me.ObjectRowBinding1.TypeName = "WinBack.wb_Produktionsschritt"
        '
        'wb_Linien_Auto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1129, 632)
        Me.Controls.Add(Me.BtnAktualisieren)
        Me.Controls.Add(Me.lblLinie)
        Me.Controls.Add(Me.cbLinien)
        Me.Controls.Add(Me.BtnStart)
        Me.Controls.Add(Me.VirtualTree)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.UniversalEditBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wb_Linien_Auto"
        Me.Text = "WinBack virtuelle Linie"
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents VirtualTree As Infralution.Controls.VirtualTree.VirtualTree
    Friend WithEvents ColCharge As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColNummer As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColBezeichnung As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColLinie As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColStartZeit As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColSollwert As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColEinheit As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColIstwert As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColEinheitIstwert As Infralution.Controls.VirtualTree.Column
    Friend WithEvents CellEditor2 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents RichTextBox1 As Windows.Forms.RichTextBox
    Friend WithEvents CellEditor1 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents CellEditor3 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents UniversalEditBox1 As Infralution.Controls.UniversalEditBox
    Friend WithEvents CellEditor4 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents TextBox1 As Windows.Forms.TextBox
    Friend WithEvents ObjectRowBinding1 As Infralution.Controls.VirtualTree.ObjectRowBinding
    Friend WithEvents BtnStart As Windows.Forms.Button
    Friend WithEvents cbLinien As wb_ComboBox
    Friend WithEvents lblLinie As Windows.Forms.Label
    Friend WithEvents BtnAktualisieren As Windows.Forms.Button
End Class
