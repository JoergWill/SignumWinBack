<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rezept_Rezeptur
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
        Me.components = New System.ComponentModel.Container()
        Dim ObjectCellBinding1 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding2 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding3 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding4 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding5 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding6 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Me.BtnDrucken = New System.Windows.Forms.Button()
        Me.BtnKopieren = New System.Windows.Forms.Button()
        Me.BtnLoeschen = New System.Windows.Forms.Button()
        Me.BtnVerwendung = New System.Windows.Forms.Button()
        Me.BtnHinweise = New System.Windows.Forms.Button()
        Me.BtnNwt = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripLeftMargin = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripRezeptChange = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripAllergenLegende = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Wb_TabControl = New WinBack.wb_TabControl()
        Me.tb_Rezeptur = New System.Windows.Forms.TabPage()
        Me.VirtualTree = New Infralution.Controls.VirtualTree.VirtualTree()
        Me.ColNr = New Infralution.Controls.VirtualTree.Column()
        Me.ColBezeichung = New Infralution.Controls.VirtualTree.Column()
        Me.ColPreis = New Infralution.Controls.VirtualTree.Column()
        Me.ColSollwert = New Infralution.Controls.VirtualTree.Column()
        Me.CellEditor4 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ColEinheit = New Infralution.Controls.VirtualTree.Column()
        Me.ColProzent = New Infralution.Controls.VirtualTree.Column()
        Me.CellEditor2 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.CellEditor1 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CellEditor3 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.UniversalEditBox1 = New Infralution.Controls.UniversalEditBox()
        Me.ObjectRowBinding1 = New Infralution.Controls.VirtualTree.ObjectRowBinding()
        Me.tb_Naehrwerte = New System.Windows.Forms.TabPage()
        Me.tb_Hinweise = New System.Windows.Forms.TabPage()
        Me.TextHinweise = New System.Windows.Forms.TextBox()
        Me.tb_Verwendung = New System.Windows.Forms.TabPage()
        Me.StatusStrip.SuspendLayout()
        Me.Wb_TabControl.SuspendLayout()
        Me.tb_Rezeptur.SuspendLayout()
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tb_Hinweise.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnDrucken
        '
        Me.BtnDrucken.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDrucken.Location = New System.Drawing.Point(142, 200)
        Me.BtnDrucken.Name = "BtnDrucken"
        Me.BtnDrucken.Size = New System.Drawing.Size(138, 52)
        Me.BtnDrucken.TabIndex = 2
        Me.BtnDrucken.TabStop = False
        Me.BtnDrucken.Text = "Drucken"
        Me.BtnDrucken.UseVisualStyleBackColor = True
        '
        'BtnKopieren
        '
        Me.BtnKopieren.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnKopieren.Location = New System.Drawing.Point(12, 200)
        Me.BtnKopieren.Name = "BtnKopieren"
        Me.BtnKopieren.Size = New System.Drawing.Size(131, 52)
        Me.BtnKopieren.TabIndex = 3
        Me.BtnKopieren.TabStop = False
        Me.BtnKopieren.Text = "Kopieren"
        Me.BtnKopieren.UseVisualStyleBackColor = True
        '
        'BtnLoeschen
        '
        Me.BtnLoeschen.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLoeschen.Location = New System.Drawing.Point(279, 200)
        Me.BtnLoeschen.Name = "BtnLoeschen"
        Me.BtnLoeschen.Size = New System.Drawing.Size(138, 52)
        Me.BtnLoeschen.TabIndex = 4
        Me.BtnLoeschen.TabStop = False
        Me.BtnLoeschen.Text = "Löschen"
        Me.BtnLoeschen.UseVisualStyleBackColor = True
        '
        'BtnVerwendung
        '
        Me.BtnVerwendung.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnVerwendung.Location = New System.Drawing.Point(416, 200)
        Me.BtnVerwendung.Name = "BtnVerwendung"
        Me.BtnVerwendung.Size = New System.Drawing.Size(138, 52)
        Me.BtnVerwendung.TabIndex = 5
        Me.BtnVerwendung.TabStop = False
        Me.BtnVerwendung.Text = "Verwendung"
        Me.BtnVerwendung.UseVisualStyleBackColor = True
        '
        'BtnHinweise
        '
        Me.BtnHinweise.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnHinweise.Location = New System.Drawing.Point(553, 200)
        Me.BtnHinweise.Name = "BtnHinweise"
        Me.BtnHinweise.Size = New System.Drawing.Size(138, 52)
        Me.BtnHinweise.TabIndex = 6
        Me.BtnHinweise.TabStop = False
        Me.BtnHinweise.Text = "Hinweise"
        Me.BtnHinweise.UseVisualStyleBackColor = True
        '
        'BtnNwt
        '
        Me.BtnNwt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNwt.Location = New System.Drawing.Point(690, 200)
        Me.BtnNwt.Name = "BtnNwt"
        Me.BtnNwt.Size = New System.Drawing.Size(138, 52)
        Me.BtnNwt.TabIndex = 7
        Me.BtnNwt.TabStop = False
        Me.BtnNwt.Text = "Nährwerte"
        Me.BtnNwt.UseVisualStyleBackColor = True
        '
        'BtnClose
        '
        Me.BtnClose.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(827, 200)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(138, 52)
        Me.BtnClose.TabIndex = 8
        Me.BtnClose.TabStop = False
        Me.BtnClose.Text = "Schliessen"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(172, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Label2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(172, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Label3"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(172, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Label4"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(172, 106)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Label5"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(90, 21)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Preis"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(90, 51)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 13)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Gewicht"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(90, 80)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 13)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Gesamt Mehl"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(90, 106)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(21, 13)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "TA"
        '
        'StatusStrip
        '
        Me.StatusStrip.AutoSize = False
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLeftMargin, Me.ToolStripRezeptChange, Me.ToolStripAllergenLegende})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 709)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(976, 22)
        Me.StatusStrip.TabIndex = 24
        Me.StatusStrip.Text = "StatusStrip"
        '
        'ToolStripLeftMargin
        '
        Me.ToolStripLeftMargin.Name = "ToolStripLeftMargin"
        Me.ToolStripLeftMargin.Size = New System.Drawing.Size(16, 17)
        Me.ToolStripLeftMargin.Text = "   "
        '
        'ToolStripRezeptChange
        '
        Me.ToolStripRezeptChange.Name = "ToolStripRezeptChange"
        Me.ToolStripRezeptChange.Size = New System.Drawing.Size(128, 17)
        Me.ToolStripRezeptChange.Text = "Rezept wurde geändert"
        Me.ToolStripRezeptChange.Visible = False
        '
        'ToolStripAllergenLegende
        '
        Me.ToolStripAllergenLegende.Name = "ToolStripAllergenLegende"
        Me.ToolStripAllergenLegende.Size = New System.Drawing.Size(521, 17)
        Me.ToolStripAllergenLegende.Text = "Allergen-Kennzeichnung   K - keine Angaben / N - nicht enthalten  / T - Spuren vo" &
    "n / C -Contains"
        Me.ToolStripAllergenLegende.Visible = False
        '
        'Wb_TabControl
        '
        Me.Wb_TabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Wb_TabControl.Controls.Add(Me.tb_Rezeptur)
        Me.Wb_TabControl.Controls.Add(Me.tb_Naehrwerte)
        Me.Wb_TabControl.Controls.Add(Me.tb_Hinweise)
        Me.Wb_TabControl.Controls.Add(Me.tb_Verwendung)
        Me.Wb_TabControl.Location = New System.Drawing.Point(13, 258)
        Me.Wb_TabControl.Multiline = True
        Me.Wb_TabControl.Name = "Wb_TabControl"
        Me.Wb_TabControl.SelectedIndex = 0
        Me.Wb_TabControl.Size = New System.Drawing.Size(951, 439)
        Me.Wb_TabControl.TabIndex = 23
        '
        'tb_Rezeptur
        '
        Me.tb_Rezeptur.Controls.Add(Me.VirtualTree)
        Me.tb_Rezeptur.Controls.Add(Me.RichTextBox1)
        Me.tb_Rezeptur.Controls.Add(Me.TextBox1)
        Me.tb_Rezeptur.Controls.Add(Me.Label1)
        Me.tb_Rezeptur.Controls.Add(Me.UniversalEditBox1)
        Me.tb_Rezeptur.Location = New System.Drawing.Point(4, 23)
        Me.tb_Rezeptur.Name = "tb_Rezeptur"
        Me.tb_Rezeptur.Padding = New System.Windows.Forms.Padding(3)
        Me.tb_Rezeptur.Size = New System.Drawing.Size(943, 412)
        Me.tb_Rezeptur.TabIndex = 0
        Me.tb_Rezeptur.Text = "Rezeptur"
        Me.tb_Rezeptur.UseVisualStyleBackColor = True
        '
        'VirtualTree
        '
        Me.VirtualTree.AllowMultiSelect = False
        Me.VirtualTree.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VirtualTree.AutoFitColumns = True
        Me.VirtualTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VirtualTree.Columns.Add(Me.ColNr)
        Me.VirtualTree.Columns.Add(Me.ColBezeichung)
        Me.VirtualTree.Columns.Add(Me.ColPreis)
        Me.VirtualTree.Columns.Add(Me.ColSollwert)
        Me.VirtualTree.Columns.Add(Me.ColEinheit)
        Me.VirtualTree.Columns.Add(Me.ColProzent)
        Me.VirtualTree.EditOnKeyPress = True
        Me.VirtualTree.Editors.Add(Me.CellEditor2)
        Me.VirtualTree.Editors.Add(Me.CellEditor1)
        Me.VirtualTree.Editors.Add(Me.CellEditor3)
        Me.VirtualTree.Editors.Add(Me.CellEditor4)
        Me.VirtualTree.HeaderHeight = 24
        Me.VirtualTree.HeaderStyle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VirtualTree.LineStyle = Infralution.Controls.VirtualTree.LineStyle.None
        Me.VirtualTree.Location = New System.Drawing.Point(-4, 0)
        Me.VirtualTree.MainColumn = Me.ColNr
        Me.VirtualTree.Name = "VirtualTree"
        Me.VirtualTree.RowBindings.Add(Me.ObjectRowBinding1)
        Me.VirtualTree.RowEvenStyle.BackColor = System.Drawing.Color.PowderBlue
        Me.VirtualTree.RowStyle.BorderColor = System.Drawing.Color.LightGray
        Me.VirtualTree.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.Cell
        Me.VirtualTree.ShowRootRow = False
        Me.VirtualTree.Size = New System.Drawing.Size(948, 416)
        Me.VirtualTree.StyleTemplate = Infralution.Controls.VirtualTree.StyleTemplate.Vista
        Me.VirtualTree.TabIndex = 6
        '
        'ColNr
        '
        Me.ColNr.Caption = "Nummer"
        Me.ColNr.MinWidth = 150
        Me.ColNr.Movable = False
        Me.ColNr.Name = "ColNr"
        Me.ColNr.Sortable = False
        Me.ColNr.Width = 150
        '
        'ColBezeichung
        '
        Me.ColBezeichung.AutoFitWeight = 200.0!
        Me.ColBezeichung.Caption = "Bezeichnung"
        Me.ColBezeichung.MinWidth = 300
        Me.ColBezeichung.Movable = False
        Me.ColBezeichung.Name = "ColBezeichung"
        Me.ColBezeichung.Sortable = False
        Me.ColBezeichung.Width = 479
        '
        'ColPreis
        '
        Me.ColPreis.Caption = "Preis"
        Me.ColPreis.MinWidth = 100
        Me.ColPreis.Name = "ColPreis"
        Me.ColPreis.Resizable = False
        Me.ColPreis.Sortable = False
        '
        'ColSollwert
        '
        Me.ColSollwert.Caption = Nothing
        Me.ColSollwert.CellEditor = Me.CellEditor4
        Me.ColSollwert.CellStyle.VertAlignment = System.Drawing.StringAlignment.Center
        Me.ColSollwert.MinWidth = 100
        Me.ColSollwert.Movable = False
        Me.ColSollwert.Name = "ColSollwert"
        Me.ColSollwert.Resizable = False
        Me.ColSollwert.Sortable = False
        Me.ColSollwert.Width = 126
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
        Me.TextBox1.Location = New System.Drawing.Point(-302, -253)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 13)
        Me.TextBox1.TabIndex = 5
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox1.Visible = False
        Me.TextBox1.WordWrap = False
        '
        'ColEinheit
        '
        Me.ColEinheit.Caption = Nothing
        Me.ColEinheit.MinWidth = 40
        Me.ColEinheit.Name = "ColEinheit"
        Me.ColEinheit.Resizable = False
        Me.ColEinheit.Selectable = False
        Me.ColEinheit.Sortable = False
        Me.ColEinheit.Width = 40
        '
        'ColProzent
        '
        Me.ColProzent.Caption = Nothing
        Me.ColProzent.CellStyle.HorzAlignment = System.Drawing.StringAlignment.Far
        Me.ColProzent.MinWidth = 50
        Me.ColProzent.Name = "ColProzent"
        Me.ColProzent.Resizable = False
        Me.ColProzent.Sortable = False
        Me.ColProzent.Width = 50
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
        Me.RichTextBox1.Location = New System.Drawing.Point(-302, -253)
        Me.RichTextBox1.Multiline = False
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.RichTextBox1.Size = New System.Drawing.Size(100, 96)
        Me.RichTextBox1.TabIndex = 2
        Me.RichTextBox1.Text = ""
        Me.RichTextBox1.Visible = False
        '
        'CellEditor1
        '
        Me.CellEditor1.Control = Me.Label1
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(-202, -153)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 10)
        Me.Label1.TabIndex = 3
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
        Me.UniversalEditBox1.Location = New System.Drawing.Point(-302, -253)
        Me.UniversalEditBox1.Name = "UniversalEditBox1"
        Me.UniversalEditBox1.Size = New System.Drawing.Size(195, 19)
        Me.UniversalEditBox1.TabIndex = 4
        Me.UniversalEditBox1.Visible = False
        '
        'ObjectRowBinding1
        '
        Me.ObjectRowBinding1.AllowDrag = True
        Me.ObjectRowBinding1.AllowDropAboveRow = True
        Me.ObjectRowBinding1.AllowDropBelowRow = True
        ObjectCellBinding1.Column = Me.ColNr
        ObjectCellBinding1.Field = "Nummer"
        ObjectCellBinding1.Style.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ObjectCellBinding1.Style.HorzAlignment = System.Drawing.StringAlignment.Near
        ObjectCellBinding2.Column = Me.ColBezeichung
        ObjectCellBinding2.Field = "VirtTreeBezeichnung"
        ObjectCellBinding2.Style.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ObjectCellBinding3.Column = Me.ColPreis
        ObjectCellBinding3.Field = "VirtTreePreis"
        ObjectCellBinding3.Format = "{0:C}"
        ObjectCellBinding3.Style.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ObjectCellBinding3.Style.HorzAlignment = System.Drawing.StringAlignment.Far
        ObjectCellBinding4.Column = Me.ColSollwert
        ObjectCellBinding4.Field = "VirtTreeSollwert"
        ObjectCellBinding4.Format = "{0:N3}"
        ObjectCellBinding4.Style.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ObjectCellBinding4.Style.HorzAlignment = System.Drawing.StringAlignment.Far
        ObjectCellBinding5.Column = Me.ColEinheit
        ObjectCellBinding5.Field = "VirtTreeEinheit"
        ObjectCellBinding5.Style.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ObjectCellBinding6.Column = Me.ColProzent
        ObjectCellBinding6.Field = "VirtTreeProzent"
        ObjectCellBinding6.Style.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding1)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding2)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding3)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding4)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding5)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding6)
        Me.ObjectRowBinding1.ChildProperty = "ChildSteps"
        Me.ObjectRowBinding1.Height = 24
        Me.ObjectRowBinding1.Name = "ObjectRowBinding1"
        Me.ObjectRowBinding1.ParentProperty = "ParentStep"
        Me.ObjectRowBinding1.Style.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ObjectRowBinding1.TypeName = "WinBack.wb_Rezeptschritt"
        '
        'tb_Naehrwerte
        '
        Me.tb_Naehrwerte.Location = New System.Drawing.Point(4, 23)
        Me.tb_Naehrwerte.Name = "tb_Naehrwerte"
        Me.tb_Naehrwerte.Padding = New System.Windows.Forms.Padding(3)
        Me.tb_Naehrwerte.Size = New System.Drawing.Size(943, 412)
        Me.tb_Naehrwerte.TabIndex = 1
        Me.tb_Naehrwerte.Text = "Nährwerte"
        Me.tb_Naehrwerte.UseVisualStyleBackColor = True
        '
        'tb_Hinweise
        '
        Me.tb_Hinweise.BackColor = System.Drawing.Color.Transparent
        Me.tb_Hinweise.Controls.Add(Me.TextHinweise)
        Me.tb_Hinweise.Location = New System.Drawing.Point(4, 23)
        Me.tb_Hinweise.Name = "tb_Hinweise"
        Me.tb_Hinweise.Size = New System.Drawing.Size(943, 412)
        Me.tb_Hinweise.TabIndex = 2
        Me.tb_Hinweise.Text = "Hinweise"
        '
        'TextHinweise
        '
        Me.TextHinweise.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextHinweise.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextHinweise.Location = New System.Drawing.Point(0, 0)
        Me.TextHinweise.Multiline = True
        Me.TextHinweise.Name = "TextHinweise"
        Me.TextHinweise.Size = New System.Drawing.Size(943, 412)
        Me.TextHinweise.TabIndex = 0
        Me.TextHinweise.TabStop = False
        '
        'tb_Verwendung
        '
        Me.tb_Verwendung.Location = New System.Drawing.Point(4, 23)
        Me.tb_Verwendung.Name = "tb_Verwendung"
        Me.tb_Verwendung.Size = New System.Drawing.Size(943, 412)
        Me.tb_Verwendung.TabIndex = 3
        Me.tb_Verwendung.Text = "Verwendung"
        Me.tb_Verwendung.UseVisualStyleBackColor = True
        '
        'wb_Rezept_Rezeptur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(976, 731)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.Wb_TabControl)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.BtnNwt)
        Me.Controls.Add(Me.BtnHinweise)
        Me.Controls.Add(Me.BtnVerwendung)
        Me.Controls.Add(Me.BtnLoeschen)
        Me.Controls.Add(Me.BtnKopieren)
        Me.Controls.Add(Me.BtnDrucken)
        Me.Name = "wb_Rezept_Rezeptur"
        Me.Text = "Rezeptur"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.Wb_TabControl.ResumeLayout(False)
        Me.tb_Rezeptur.ResumeLayout(False)
        Me.tb_Rezeptur.PerformLayout()
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tb_Hinweise.ResumeLayout(False)
        Me.tb_Hinweise.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnDrucken As Windows.Forms.Button
    Friend WithEvents BtnKopieren As Windows.Forms.Button
    Friend WithEvents BtnLoeschen As Windows.Forms.Button
    Friend WithEvents BtnVerwendung As Windows.Forms.Button
    Friend WithEvents BtnHinweise As Windows.Forms.Button
    Friend WithEvents BtnNwt As Windows.Forms.Button
    Friend WithEvents BtnClose As Windows.Forms.Button
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents Wb_TabControl As wb_TabControl
    Friend WithEvents tb_Rezeptur As Windows.Forms.TabPage
    Friend WithEvents tb_Naehrwerte As Windows.Forms.TabPage
    Friend WithEvents VirtualTree As Infralution.Controls.VirtualTree.VirtualTree
    Friend WithEvents ColNr As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColBezeichung As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColPreis As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColSollwert As Infralution.Controls.VirtualTree.Column
    Friend WithEvents CellEditor4 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents TextBox1 As Windows.Forms.TextBox
    Friend WithEvents ColEinheit As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColProzent As Infralution.Controls.VirtualTree.Column
    Friend WithEvents CellEditor2 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents RichTextBox1 As Windows.Forms.RichTextBox
    Friend WithEvents CellEditor1 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents CellEditor3 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents UniversalEditBox1 As Infralution.Controls.UniversalEditBox
    Friend WithEvents ObjectRowBinding1 As Infralution.Controls.VirtualTree.ObjectRowBinding
    Friend WithEvents tb_Hinweise As Windows.Forms.TabPage
    Friend WithEvents tb_Verwendung As Windows.Forms.TabPage
    Friend WithEvents StatusStrip As Windows.Forms.StatusStrip
    Friend WithEvents ToolStripAllergenLegende As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLeftMargin As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TextHinweise As Windows.Forms.TextBox
    Friend WithEvents ToolStripRezeptChange As Windows.Forms.ToolStripStatusLabel
End Class
