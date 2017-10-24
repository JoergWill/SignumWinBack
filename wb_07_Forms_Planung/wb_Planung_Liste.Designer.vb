Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Planung_Liste
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
        Dim ObjectCellBinding1 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding2 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding3 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding4 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding5 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding6 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding7 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding8 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Me.ColStart = New Infralution.Controls.VirtualTree.Column()
        Me.ColCharge = New Infralution.Controls.VirtualTree.Column()
        Me.CoNr = New Infralution.Controls.VirtualTree.Column()
        Me.ColBezeichnung = New Infralution.Controls.VirtualTree.Column()
        Me.CellEditor4 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ColKommentar = New Infralution.Controls.VirtualTree.Column()
        Me.ColLinie = New Infralution.Controls.VirtualTree.Column()
        Me.ColSollwert = New Infralution.Controls.VirtualTree.Column()
        Me.ColEinheit = New Infralution.Controls.VirtualTree.Column()
        Me.BtnVorlage = New System.Windows.Forms.Button()
        Me.BtnBestellungen = New System.Windows.Forms.Button()
        Me.BtVorproduktion = New System.Windows.Forms.Button()
        Me.BtnBackZettelDrucken = New System.Windows.Forms.Button()
        Me.BtnTeigListeExport = New System.Windows.Forms.Button()
        Me.VirtualTree = New Infralution.Controls.VirtualTree.VirtualTree()
        Me.CellEditor2 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.CellEditor1 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CellEditor3 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.UniversalEditBox1 = New Infralution.Controls.UniversalEditBox()
        Me.ObjectRowBinding1 = New Infralution.Controls.VirtualTree.ObjectRowBinding()
        Me.btnNeueCharge = New System.Windows.Forms.Button()
        Me.BtnTeigListeDrucken = New System.Windows.Forms.Button()
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColStart
        '
        Me.ColStart.Caption = "Start"
        Me.ColStart.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColStart.MinWidth = 50
        Me.ColStart.Movable = False
        Me.ColStart.Name = "ColStart"
        Me.ColStart.Sortable = False
        Me.ColStart.Width = 50
        '
        'ColCharge
        '
        Me.ColCharge.Caption = "Charge"
        Me.ColCharge.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColCharge.MinWidth = 100
        Me.ColCharge.Movable = False
        Me.ColCharge.Name = "ColCharge"
        Me.ColCharge.Sortable = False
        '
        'CoNr
        '
        Me.CoNr.Caption = "Nr"
        Me.CoNr.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.CoNr.MinWidth = 100
        Me.CoNr.Name = "CoNr"
        '
        'ColBezeichnung
        '
        Me.ColBezeichnung.Caption = "Bezeichnung"
        Me.ColBezeichnung.CellEditor = Me.CellEditor4
        Me.ColBezeichnung.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColBezeichnung.CellStyle.VertAlignment = System.Drawing.StringAlignment.Center
        Me.ColBezeichnung.MinWidth = 200
        Me.ColBezeichnung.Movable = False
        Me.ColBezeichnung.Name = "ColBezeichnung"
        Me.ColBezeichnung.Sortable = False
        Me.ColBezeichnung.Width = 200
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
        Me.TextBox1.Location = New System.Drawing.Point(-148, -71)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(79, 13)
        Me.TextBox1.TabIndex = 10
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox1.Visible = False
        Me.TextBox1.WordWrap = False
        '
        'ColKommentar
        '
        Me.ColKommentar.AutoFitWeight = 200.0!
        Me.ColKommentar.Caption = "Kommentar"
        Me.ColKommentar.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColKommentar.MinWidth = 100
        Me.ColKommentar.Name = "ColKommentar"
        Me.ColKommentar.Selectable = False
        Me.ColKommentar.Sortable = False
        Me.ColKommentar.Width = 291
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
        Me.ColEinheit.Width = 30
        '
        'BtnVorlage
        '
        Me.BtnVorlage.Location = New System.Drawing.Point(12, 12)
        Me.BtnVorlage.Name = "BtnVorlage"
        Me.BtnVorlage.Size = New System.Drawing.Size(123, 53)
        Me.BtnVorlage.TabIndex = 1
        Me.BtnVorlage.Text = "Vorlage auswählen"
        Me.BtnVorlage.UseVisualStyleBackColor = True
        '
        'BtnBestellungen
        '
        Me.BtnBestellungen.Location = New System.Drawing.Point(141, 12)
        Me.BtnBestellungen.Name = "BtnBestellungen"
        Me.BtnBestellungen.Size = New System.Drawing.Size(123, 53)
        Me.BtnBestellungen.TabIndex = 2
        Me.BtnBestellungen.Text = "Bestellungen einlesen"
        Me.BtnBestellungen.UseVisualStyleBackColor = True
        '
        'BtVorproduktion
        '
        Me.BtVorproduktion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtVorproduktion.Location = New System.Drawing.Point(429, 12)
        Me.BtVorproduktion.Name = "BtVorproduktion"
        Me.BtVorproduktion.Size = New System.Drawing.Size(123, 53)
        Me.BtVorproduktion.TabIndex = 3
        Me.BtVorproduktion.Text = "Vorproduktion berechnen"
        Me.BtVorproduktion.UseVisualStyleBackColor = True
        '
        'BtnBackZettelDrucken
        '
        Me.BtnBackZettelDrucken.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnBackZettelDrucken.Location = New System.Drawing.Point(558, 12)
        Me.BtnBackZettelDrucken.Name = "BtnBackZettelDrucken"
        Me.BtnBackZettelDrucken.Size = New System.Drawing.Size(123, 53)
        Me.BtnBackZettelDrucken.TabIndex = 4
        Me.BtnBackZettelDrucken.Text = "Backzettel drucken"
        Me.BtnBackZettelDrucken.UseVisualStyleBackColor = True
        '
        'BtnTeigListeExport
        '
        Me.BtnTeigListeExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnTeigListeExport.Location = New System.Drawing.Point(816, 12)
        Me.BtnTeigListeExport.Name = "BtnTeigListeExport"
        Me.BtnTeigListeExport.Size = New System.Drawing.Size(123, 53)
        Me.BtnTeigListeExport.TabIndex = 5
        Me.BtnTeigListeExport.Text = "Teigzettel exportieren"
        Me.BtnTeigListeExport.UseVisualStyleBackColor = True
        '
        'VirtualTree
        '
        Me.VirtualTree.AllowMultiSelect = False
        Me.VirtualTree.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VirtualTree.AutoFitColumns = True
        Me.VirtualTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VirtualTree.Columns.Add(Me.ColStart)
        Me.VirtualTree.Columns.Add(Me.ColCharge)
        Me.VirtualTree.Columns.Add(Me.CoNr)
        Me.VirtualTree.Columns.Add(Me.ColBezeichnung)
        Me.VirtualTree.Columns.Add(Me.ColKommentar)
        Me.VirtualTree.Columns.Add(Me.ColLinie)
        Me.VirtualTree.Columns.Add(Me.ColSollwert)
        Me.VirtualTree.Columns.Add(Me.ColEinheit)
        Me.VirtualTree.EditOnKeyPress = True
        Me.VirtualTree.Editors.Add(Me.CellEditor2)
        Me.VirtualTree.Editors.Add(Me.CellEditor1)
        Me.VirtualTree.Editors.Add(Me.CellEditor3)
        Me.VirtualTree.Editors.Add(Me.CellEditor4)
        Me.VirtualTree.HeaderHeight = 24
        Me.VirtualTree.HeaderStyle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VirtualTree.LineStyle = Infralution.Controls.VirtualTree.LineStyle.None
        Me.VirtualTree.Location = New System.Drawing.Point(12, 100)
        Me.VirtualTree.MainColumn = Me.ColStart
        Me.VirtualTree.Name = "VirtualTree"
        Me.VirtualTree.RowBindings.Add(Me.ObjectRowBinding1)
        Me.VirtualTree.RowEvenStyle.BackColor = System.Drawing.Color.White
        Me.VirtualTree.RowStyle.BorderColor = System.Drawing.Color.LightGray
        Me.VirtualTree.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.Cell
        Me.VirtualTree.ShowRootRow = False
        Me.VirtualTree.Size = New System.Drawing.Size(927, 551)
        Me.VirtualTree.StyleTemplate = Infralution.Controls.VirtualTree.StyleTemplate.Vista
        Me.VirtualTree.TabIndex = 11
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
        Me.RichTextBox1.Location = New System.Drawing.Point(-148, -71)
        Me.RichTextBox1.Multiline = False
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.RichTextBox1.Size = New System.Drawing.Size(79, 96)
        Me.RichTextBox1.TabIndex = 7
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
        Me.Label1.Location = New System.Drawing.Point(-186, -53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 10)
        Me.Label1.TabIndex = 8
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
        Me.UniversalEditBox1.Location = New System.Drawing.Point(-148, -71)
        Me.UniversalEditBox1.Name = "UniversalEditBox1"
        Me.UniversalEditBox1.Size = New System.Drawing.Size(195, 19)
        Me.UniversalEditBox1.TabIndex = 9
        Me.UniversalEditBox1.Visible = False
        '
        'ObjectRowBinding1
        '
        ObjectCellBinding1.Column = Me.ColStart
        ObjectCellBinding1.Field = "VirtTreeStart"
        ObjectCellBinding2.Column = Me.ColCharge
        ObjectCellBinding2.Field = "VirtTreeCharge"
        ObjectCellBinding3.Column = Me.CoNr
        ObjectCellBinding3.Field = "VirtTreeNummer"
        ObjectCellBinding4.Column = Me.ColBezeichnung
        ObjectCellBinding4.Field = "VirtTreeBezeichnung"
        ObjectCellBinding5.Column = Me.ColKommentar
        ObjectCellBinding5.Field = "VirtTreeKommentar"
        ObjectCellBinding6.Column = Me.ColLinie
        ObjectCellBinding6.Field = "VirtTreeLinie"
        ObjectCellBinding7.Column = Me.ColSollwert
        ObjectCellBinding7.Field = "VirtTreeSollwert"
        ObjectCellBinding8.Column = Me.ColEinheit
        ObjectCellBinding8.Field = "VirtTreeEinheit"
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding1)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding2)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding3)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding4)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding5)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding6)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding7)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding8)
        Me.ObjectRowBinding1.ChildProperty = "ChildSteps"
        Me.ObjectRowBinding1.Name = "ObjectRowBinding1"
        Me.ObjectRowBinding1.ParentProperty = "ParentStep"
        Me.ObjectRowBinding1.TypeName = "WinBack.wb_Produktionsschritt"
        '
        'btnNeueCharge
        '
        Me.btnNeueCharge.Location = New System.Drawing.Point(270, 12)
        Me.btnNeueCharge.Name = "btnNeueCharge"
        Me.btnNeueCharge.Size = New System.Drawing.Size(123, 53)
        Me.btnNeueCharge.TabIndex = 12
        Me.btnNeueCharge.Text = "Neu"
        Me.btnNeueCharge.UseVisualStyleBackColor = True
        '
        'BtnTeigListeDrucken
        '
        Me.BtnTeigListeDrucken.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnTeigListeDrucken.Location = New System.Drawing.Point(687, 12)
        Me.BtnTeigListeDrucken.Name = "BtnTeigListeDrucken"
        Me.BtnTeigListeDrucken.Size = New System.Drawing.Size(123, 53)
        Me.BtnTeigListeDrucken.TabIndex = 13
        Me.BtnTeigListeDrucken.Text = "Teigliste drucken"
        Me.BtnTeigListeDrucken.UseVisualStyleBackColor = True
        '
        'wb_Planung_Liste
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(951, 663)
        Me.Controls.Add(Me.BtnTeigListeDrucken)
        Me.Controls.Add(Me.btnNeueCharge)
        Me.Controls.Add(Me.VirtualTree)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.UniversalEditBox1)
        Me.Controls.Add(Me.BtnTeigListeExport)
        Me.Controls.Add(Me.BtnBackZettelDrucken)
        Me.Controls.Add(Me.BtVorproduktion)
        Me.Controls.Add(Me.BtnBestellungen)
        Me.Controls.Add(Me.BtnVorlage)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HideOnClose = True
        Me.Name = "wb_Planung_Liste"
        Me.Text = "Planung Liste"
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnVorlage As Windows.Forms.Button
    Friend WithEvents BtnBestellungen As Windows.Forms.Button
    Friend WithEvents BtVorproduktion As Windows.Forms.Button
    Friend WithEvents BtnBackZettelDrucken As Windows.Forms.Button
    Friend WithEvents BtnTeigListeExport As Windows.Forms.Button
    Friend WithEvents VirtualTree As Infralution.Controls.VirtualTree.VirtualTree
    Friend WithEvents ColStart As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColCharge As Infralution.Controls.VirtualTree.Column
    Friend WithEvents CoNr As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColBezeichnung As Infralution.Controls.VirtualTree.Column
    Friend WithEvents CellEditor4 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents TextBox1 As Windows.Forms.TextBox
    Friend WithEvents ColKommentar As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColLinie As Infralution.Controls.VirtualTree.Column
    Friend WithEvents CellEditor2 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents RichTextBox1 As Windows.Forms.RichTextBox
    Friend WithEvents CellEditor1 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents CellEditor3 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents UniversalEditBox1 As Infralution.Controls.UniversalEditBox
    Friend WithEvents ColSollwert As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColEinheit As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ObjectRowBinding1 As Infralution.Controls.VirtualTree.ObjectRowBinding
    Friend WithEvents btnNeueCharge As Windows.Forms.Button
    Friend WithEvents BtnTeigListeDrucken As Windows.Forms.Button
End Class
