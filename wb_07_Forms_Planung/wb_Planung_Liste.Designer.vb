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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Planung_Liste))
        Dim ObjectCellBinding1 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding2 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding3 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding4 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding5 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding6 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding7 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding8 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding9 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding10 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding11 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding12 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        ColNummer = New Infralution.Controls.VirtualTree.Column()
        ColCharge = New Infralution.Controls.VirtualTree.Column()
        ColTour = New Infralution.Controls.VirtualTree.Column()
        ColBezeichnung = New Infralution.Controls.VirtualTree.Column()
        ColKommentar = New Infralution.Controls.VirtualTree.Column()
        ColLinie = New Infralution.Controls.VirtualTree.Column()
        ColSollwert = New Infralution.Controls.VirtualTree.Column()
        ColEinheit = New Infralution.Controls.VirtualTree.Column()
        ColBestand = New Infralution.Controls.VirtualTree.Column()
        ColEinheitBestand = New Infralution.Controls.VirtualTree.Column()
        ColStartZeit = New Infralution.Controls.VirtualTree.Column()
        ColType = New Infralution.Controls.VirtualTree.Column()
        CellEditor4 = New Infralution.Controls.VirtualTree.CellEditor()
        TextBox1 = New System.Windows.Forms.TextBox()
        BtnVorlage = New System.Windows.Forms.Button()
        BtnBestellungen = New System.Windows.Forms.Button()
        BtVorproduktion = New System.Windows.Forms.Button()
        BtnBackZettelDrucken = New System.Windows.Forms.Button()
        BtnTeigListeExport = New System.Windows.Forms.Button()
        VirtualTree = New Infralution.Controls.VirtualTree.VirtualTree()
        CellEditor2 = New Infralution.Controls.VirtualTree.CellEditor()
        RichTextBox1 = New System.Windows.Forms.RichTextBox()
        CellEditor1 = New Infralution.Controls.VirtualTree.CellEditor()
        Label1 = New System.Windows.Forms.Label()
        CellEditor3 = New Infralution.Controls.VirtualTree.CellEditor()
        UniversalEditBox1 = New Infralution.Controls.UniversalEditBox()
        ObjectRowBinding1 = New Infralution.Controls.VirtualTree.ObjectRowBinding()
        btnNeueCharge = New System.Windows.Forms.Button()
        BtnTeigListeDrucken = New System.Windows.Forms.Button()
        dtBestellungen = New System.Windows.Forms.DateTimePicker()
        lblLinieArtikel = New System.Windows.Forms.Label()
        lblLinienGruppe = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        BtnCheckLager = New System.Windows.Forms.Button()
        BtnLagerEntnahmeListe = New System.Windows.Forms.Button()
        BtnOfenlisteDrucken = New System.Windows.Forms.Button()
        cbSupressOptimiert = New System.Windows.Forms.CheckBox()
        cbArtikelLinienGruppe = New wb_ComboBox()
        cbLiniengruppe = New wb_ComboBox()
        cbProduktionsFiliale = New wb_ComboBox()
        CType(VirtualTree, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ColNummer
        ' 
        ColNummer.Caption = "Nummer"
        ColNummer.CellEvenStyle.BackColor = Drawing.Color.White
        ColNummer.MinWidth = 150
        ColNummer.Movable = False
        ColNummer.Name = "ColNummer"
        ColNummer.Sortable = False
        ColNummer.Width = 150
        ' 
        ' ColCharge
        ' 
        ColCharge.Caption = "Charge"
        ColCharge.CellEvenStyle.BackColor = Drawing.Color.White
        ColCharge.MinWidth = 80
        ColCharge.Movable = False
        ColCharge.Name = "ColCharge"
        ColCharge.Sortable = False
        ColCharge.Width = 80
        ' 
        ' ColTour
        ' 
        ColTour.Caption = "Fahrt"
        ColTour.CellEvenStyle.BackColor = Drawing.Color.White
        ColTour.CellStyle.GradientMode = Drawing.Drawing2D.LinearGradientMode.Vertical
        ColTour.CellStyle.HorzAlignment = Drawing.StringAlignment.Center
        ColTour.MinWidth = 50
        ColTour.Name = "ColTour"
        ColTour.Sortable = False
        ColTour.Width = 50
        ' 
        ' ColBezeichnung
        ' 
        ColBezeichnung.Caption = "Bezeichnung"
        ColBezeichnung.CellEvenStyle.BackColor = Drawing.Color.White
        ColBezeichnung.CellStyle.VertAlignment = Drawing.StringAlignment.Center
        ColBezeichnung.MinWidth = 250
        ColBezeichnung.Movable = False
        ColBezeichnung.Name = "ColBezeichnung"
        ColBezeichnung.Sortable = False
        ColBezeichnung.Width = 250
        ' 
        ' ColKommentar
        ' 
        ColKommentar.AutoFitWeight = 200F
        ColKommentar.Caption = "Kommentar"
        ColKommentar.CellEvenStyle.BackColor = Drawing.Color.White
        ColKommentar.MinWidth = 100
        ColKommentar.Name = "ColKommentar"
        ColKommentar.Selectable = False
        ColKommentar.Sortable = False
        ColKommentar.Width = 136
        ' 
        ' ColLinie
        ' 
        ColLinie.Caption = "Linie"
        ColLinie.CellEvenStyle.BackColor = Drawing.Color.White
        ColLinie.CellStyle.HorzAlignment = Drawing.StringAlignment.Center
        ColLinie.MinWidth = 50
        ColLinie.Name = "ColLinie"
        ColLinie.Resizable = False
        ColLinie.Sortable = False
        ColLinie.Width = 50
        ' 
        ' ColSollwert
        ' 
        ColSollwert.Caption = "Sollwert"
        ColSollwert.CellEvenStyle.BackColor = Drawing.Color.White
        ColSollwert.CellStyle.HorzAlignment = Drawing.StringAlignment.Far
        ColSollwert.MinWidth = 100
        ColSollwert.Name = "ColSollwert"
        ColSollwert.Resizable = False
        ColSollwert.Sortable = False
        ' 
        ' ColEinheit
        ' 
        ColEinheit.Caption = ""
        ColEinheit.CellEvenStyle.BackColor = Drawing.Color.White
        ColEinheit.Name = "ColEinheit"
        ColEinheit.Resizable = False
        ColEinheit.Sortable = False
        ColEinheit.Width = 30
        ' 
        ' ColBestand
        ' 
        ColBestand.Caption = "Bestand"
        ColBestand.CellStyle.HorzAlignment = Drawing.StringAlignment.Far
        ColBestand.MinWidth = 100
        ColBestand.Name = "ColBestand"
        ' 
        ' ColEinheitBestand
        ' 
        ColEinheitBestand.Caption = Nothing
        ColEinheitBestand.Name = "ColEinheitBestand"
        ColEinheitBestand.Width = 30
        ' 
        ' ColStartZeit
        ' 
        ColStartZeit.Caption = "Start"
        ColStartZeit.CellStyle.HorzAlignment = Drawing.StringAlignment.Center
        ColStartZeit.MinWidth = 100
        ColStartZeit.Name = "ColStartZeit"
        ' 
        ' ColType
        ' 
        ColType.Active = False
        ColType.Caption = Nothing
        ColType.Name = "ColType"
        ColType.Width = 30
        ' 
        ' CellEditor4
        ' 
        CellEditor4.Control = TextBox1
        CellEditor4.UseCellPadding = True
        ' 
        ' TextBox1
        ' 
        TextBox1.AcceptsReturn = True
        TextBox1.AcceptsTab = True
        TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        TextBox1.HideSelection = False
        TextBox1.Location = New System.Drawing.Point(-148, -71)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New System.Drawing.Size(79, 13)
        TextBox1.TabIndex = 10
        TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        TextBox1.Visible = False
        TextBox1.WordWrap = False
        ' 
        ' BtnVorlage
        ' 
        BtnVorlage.Enabled = False
        BtnVorlage.Location = New System.Drawing.Point(12, 12)
        BtnVorlage.Name = "BtnVorlage"
        BtnVorlage.Size = New System.Drawing.Size(123, 53)
        BtnVorlage.TabIndex = 1
        BtnVorlage.Text = "Vorlage auswählen"
        BtnVorlage.UseVisualStyleBackColor = True
        ' 
        ' BtnBestellungen
        ' 
        BtnBestellungen.Location = New System.Drawing.Point(141, 12)
        BtnBestellungen.Name = "BtnBestellungen"
        BtnBestellungen.Size = New System.Drawing.Size(123, 53)
        BtnBestellungen.TabIndex = 2
        BtnBestellungen.Text = "Bestellungen einlesen"
        BtnBestellungen.UseVisualStyleBackColor = True
        ' 
        ' BtVorproduktion
        ' 
        BtVorproduktion.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        BtVorproduktion.Location = New System.Drawing.Point(618, 12)
        BtVorproduktion.Name = "BtVorproduktion"
        BtVorproduktion.Size = New System.Drawing.Size(90, 53)
        BtVorproduktion.TabIndex = 3
        BtVorproduktion.Text = "Vorproduktion berechnen"
        BtVorproduktion.UseVisualStyleBackColor = True
        ' 
        ' BtnBackZettelDrucken
        ' 
        BtnBackZettelDrucken.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        BtnBackZettelDrucken.Location = New System.Drawing.Point(906, 12)
        BtnBackZettelDrucken.Name = "BtnBackZettelDrucken"
        BtnBackZettelDrucken.Size = New System.Drawing.Size(90, 53)
        BtnBackZettelDrucken.TabIndex = 4
        BtnBackZettelDrucken.Text = "Aufarbeitung drucken"
        BtnBackZettelDrucken.UseVisualStyleBackColor = True
        ' 
        ' BtnTeigListeExport
        ' 
        BtnTeigListeExport.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        BtnTeigListeExport.Location = New System.Drawing.Point(714, 12)
        BtnTeigListeExport.Name = "BtnTeigListeExport"
        BtnTeigListeExport.Size = New System.Drawing.Size(90, 53)
        BtnTeigListeExport.TabIndex = 5
        BtnTeigListeExport.Text = "Teigzettel exportieren"
        BtnTeigListeExport.UseVisualStyleBackColor = True
        ' 
        ' VirtualTree
        ' 
        VirtualTree.AllowMultiSelect = False
        VirtualTree.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        VirtualTree.AutoFitColumns = True
        VirtualTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        VirtualTree.CollapseImage = CType(resources.GetObject("VirtualTree.CollapseImage"), Drawing.Image)
        VirtualTree.Columns.Add(ColCharge)
        VirtualTree.Columns.Add(ColNummer)
        VirtualTree.Columns.Add(ColBezeichnung)
        VirtualTree.Columns.Add(ColKommentar)
        VirtualTree.Columns.Add(ColLinie)
        VirtualTree.Columns.Add(ColTour)
        VirtualTree.Columns.Add(ColStartZeit)
        VirtualTree.Columns.Add(ColSollwert)
        VirtualTree.Columns.Add(ColEinheit)
        VirtualTree.Columns.Add(ColBestand)
        VirtualTree.Columns.Add(ColEinheitBestand)
        VirtualTree.Columns.Add(ColType)
        VirtualTree.EditOnKeyPress = True
        VirtualTree.Editors.Add(CellEditor2)
        VirtualTree.Editors.Add(CellEditor1)
        VirtualTree.Editors.Add(CellEditor3)
        VirtualTree.Editors.Add(CellEditor4)
        VirtualTree.ExpandImage = CType(resources.GetObject("VirtualTree.ExpandImage"), Drawing.Image)
        VirtualTree.HeaderHeight = 24
        VirtualTree.HeaderStyle.Font = New System.Drawing.Font("Arial", 9.75F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        VirtualTree.LineStyle = Infralution.Controls.VirtualTree.LineStyle.Solid
        VirtualTree.Location = New System.Drawing.Point(12, 120)
        VirtualTree.MainColumn = ColNummer
        VirtualTree.Name = "VirtualTree"
        VirtualTree.RowBindings.Add(ObjectRowBinding1)
        VirtualTree.RowEvenStyle.BackColor = Drawing.Color.White
        VirtualTree.RowStyle.BorderColor = Drawing.Color.LightGray
        VirtualTree.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.Cell
        VirtualTree.ShowRootRow = False
        VirtualTree.Size = New System.Drawing.Size(1080, 551)
        VirtualTree.StyleTemplate = Infralution.Controls.VirtualTree.StyleTemplate.Vista
        VirtualTree.TabIndex = 11
        ' 
        ' CellEditor2
        ' 
        CellEditor2.Control = RichTextBox1
        CellEditor2.UseCellPadding = True
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.AcceptsTab = True
        RichTextBox1.AutoWordSelection = True
        RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        RichTextBox1.Font = New System.Drawing.Font("Arial", 12F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        RichTextBox1.Location = New System.Drawing.Point(-148, -71)
        RichTextBox1.Multiline = False
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        RichTextBox1.Size = New System.Drawing.Size(79, 96)
        RichTextBox1.TabIndex = 7
        RichTextBox1.Text = ""
        RichTextBox1.Visible = False
        ' 
        ' CellEditor1
        ' 
        CellEditor1.Control = Label1
        ' 
        ' Label1
        ' 
        Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label1.Location = New System.Drawing.Point(-186, -53)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(10, 10)
        Label1.TabIndex = 8
        Label1.Text = "dfsdfsdfasdfasf"
        Label1.TextAlign = Drawing.ContentAlignment.MiddleRight
        ' 
        ' CellEditor3
        ' 
        CellEditor3.Control = UniversalEditBox1
        CellEditor3.UseCellPadding = True
        ' 
        ' UniversalEditBox1
        ' 
        UniversalEditBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        UniversalEditBox1.Dock = System.Windows.Forms.DockStyle.Fill
        UniversalEditBox1.Location = New System.Drawing.Point(-148, -71)
        UniversalEditBox1.Name = "UniversalEditBox1"
        UniversalEditBox1.Size = New System.Drawing.Size(195, 19)
        UniversalEditBox1.TabIndex = 9
        UniversalEditBox1.Visible = False
        ' 
        ' ObjectRowBinding1
        ' 
        ObjectCellBinding1.Column = ColNummer
        ObjectCellBinding1.Field = "VirtTreeNummer"
        ObjectCellBinding2.Column = ColCharge
        ObjectCellBinding2.Field = "VirtTreeCharge"
        ObjectCellBinding3.Column = ColTour
        ObjectCellBinding3.Field = "VirtTreeTour"
        ObjectCellBinding4.Column = ColBezeichnung
        ObjectCellBinding4.Field = "VirtTreeBezeichnung"
        ObjectCellBinding5.Column = ColKommentar
        ObjectCellBinding5.Field = "VirtTreeKommentar"
        ObjectCellBinding6.Column = ColLinie
        ObjectCellBinding6.Field = "VirtTreeLinie"
        ObjectCellBinding7.Column = ColSollwert
        ObjectCellBinding7.Field = "VirtTreeSollwert"
        ObjectCellBinding8.Column = ColEinheit
        ObjectCellBinding8.Field = "VirtTreeEinheit"
        ObjectCellBinding9.Column = ColBestand
        ObjectCellBinding9.Field = "VirtTreeBestand"
        ObjectCellBinding10.Column = ColEinheitBestand
        ObjectCellBinding10.Field = "VirtTreeEinheitBestand"
        ObjectCellBinding11.Column = ColStartZeit
        ObjectCellBinding11.Field = "VirtTreeStart"
        ObjectCellBinding12.Column = ColType
        ObjectCellBinding12.Field = "Typ"
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding1)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding2)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding3)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding4)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding5)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding6)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding7)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding8)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding9)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding10)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding11)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding12)
        ObjectRowBinding1.ChildProperty = "ChildSteps"
        ObjectRowBinding1.ExpandedImage = CType(resources.GetObject("ObjectRowBinding1.ExpandedImage"), Drawing.Image)
        ObjectRowBinding1.Image = CType(resources.GetObject("ObjectRowBinding1.Image"), Drawing.Image)
        ObjectRowBinding1.Name = "ObjectRowBinding1"
        ObjectRowBinding1.ParentProperty = "ParentStep"
        ObjectRowBinding1.TypeName = "WinBack.wb_Produktionsschritt"
        ' 
        ' btnNeueCharge
        ' 
        btnNeueCharge.Location = New System.Drawing.Point(270, 12)
        btnNeueCharge.Name = "btnNeueCharge"
        btnNeueCharge.Size = New System.Drawing.Size(123, 53)
        btnNeueCharge.TabIndex = 12
        btnNeueCharge.Text = "Neu"
        btnNeueCharge.UseVisualStyleBackColor = True
        ' 
        ' BtnTeigListeDrucken
        ' 
        BtnTeigListeDrucken.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        BtnTeigListeDrucken.Location = New System.Drawing.Point(810, 12)
        BtnTeigListeDrucken.Name = "BtnTeigListeDrucken"
        BtnTeigListeDrucken.Size = New System.Drawing.Size(90, 53)
        BtnTeigListeDrucken.TabIndex = 13
        BtnTeigListeDrucken.Text = "Teigliste drucken"
        BtnTeigListeDrucken.UseVisualStyleBackColor = True
        ' 
        ' dtBestellungen
        ' 
        dtBestellungen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        dtBestellungen.Location = New System.Drawing.Point(196, 89)
        dtBestellungen.Name = "dtBestellungen"
        dtBestellungen.Size = New System.Drawing.Size(197, 20)
        dtBestellungen.TabIndex = 14
        ' 
        ' lblLinieArtikel
        ' 
        lblLinieArtikel.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        lblLinieArtikel.AutoSize = True
        lblLinieArtikel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblLinieArtikel.Location = New System.Drawing.Point(923, 72)
        lblLinieArtikel.Name = "lblLinieArtikel"
        lblLinieArtikel.Size = New System.Drawing.Size(131, 13)
        lblLinieArtikel.TabIndex = 61
        lblLinieArtikel.Text = "Liniengruppe Aufarbeitung"
        ' 
        ' lblLinienGruppe
        ' 
        lblLinienGruppe.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        lblLinienGruppe.AutoSize = True
        lblLinienGruppe.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblLinienGruppe.Location = New System.Drawing.Point(714, 72)
        lblLinienGruppe.Name = "lblLinienGruppe"
        lblLinienGruppe.Size = New System.Drawing.Size(92, 13)
        lblLinienGruppe.TabIndex = 60
        lblLinienGruppe.Text = "Liniengruppe Teig"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label2.Location = New System.Drawing.Point(17, 72)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(92, 13)
        Label2.TabIndex = 62
        Label2.Text = "Produktions-Filiale"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label3.Location = New System.Drawing.Point(199, 72)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(73, 13)
        Label3.TabIndex = 63
        Label3.Text = "Produktion für"
        ' 
        ' BtnCheckLager
        ' 
        BtnCheckLager.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        BtnCheckLager.Location = New System.Drawing.Point(426, 12)
        BtnCheckLager.Name = "BtnCheckLager"
        BtnCheckLager.Size = New System.Drawing.Size(90, 53)
        BtnCheckLager.TabIndex = 64
        BtnCheckLager.Text = "Lagerbestand prüfen"
        BtnCheckLager.UseVisualStyleBackColor = True
        ' 
        ' BtnLagerEntnahmeListe
        ' 
        BtnLagerEntnahmeListe.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        BtnLagerEntnahmeListe.Location = New System.Drawing.Point(522, 12)
        BtnLagerEntnahmeListe.Name = "BtnLagerEntnahmeListe"
        BtnLagerEntnahmeListe.Size = New System.Drawing.Size(90, 53)
        BtnLagerEntnahmeListe.TabIndex = 65
        BtnLagerEntnahmeListe.Text = "Lager- Entnahme"
        BtnLagerEntnahmeListe.UseVisualStyleBackColor = True
        ' 
        ' BtnOfenlisteDrucken
        ' 
        BtnOfenlisteDrucken.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        BtnOfenlisteDrucken.Location = New System.Drawing.Point(1002, 12)
        BtnOfenlisteDrucken.Name = "BtnOfenlisteDrucken"
        BtnOfenlisteDrucken.Size = New System.Drawing.Size(90, 53)
        BtnOfenlisteDrucken.TabIndex = 66
        BtnOfenlisteDrucken.Text = "Ofenliste drucken"
        BtnOfenlisteDrucken.UseVisualStyleBackColor = True
        ' 
        ' cbSupressOptimiert
        ' 
        cbSupressOptimiert.AutoSize = True
        cbSupressOptimiert.Enabled = False
        cbSupressOptimiert.Location = New System.Drawing.Point(426, 91)
        cbSupressOptimiert.Name = "cbSupressOptimiert"
        cbSupressOptimiert.Size = New System.Drawing.Size(233, 17)
        cbSupressOptimiert.TabIndex = 67
        cbSupressOptimiert.Text = "Zusammengefasste Chargen nicht anzeigen"
        cbSupressOptimiert.UseVisualStyleBackColor = True
        ' 
        ' cbArtikelLinienGruppe
        ' 
        cbArtikelLinienGruppe.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        cbArtikelLinienGruppe.FormattingEnabled = True
        cbArtikelLinienGruppe.Location = New System.Drawing.Point(920, 89)
        cbArtikelLinienGruppe.Name = "cbArtikelLinienGruppe"
        cbArtikelLinienGruppe.Size = New System.Drawing.Size(172, 21)
        cbArtikelLinienGruppe.TabIndex = 59
        cbArtikelLinienGruppe.TabStop = False
        ' 
        ' cbLiniengruppe
        ' 
        cbLiniengruppe.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        cbLiniengruppe.FormattingEnabled = True
        cbLiniengruppe.Location = New System.Drawing.Point(714, 89)
        cbLiniengruppe.Name = "cbLiniengruppe"
        cbLiniengruppe.Size = New System.Drawing.Size(176, 21)
        cbLiniengruppe.TabIndex = 58
        cbLiniengruppe.TabStop = False
        ' 
        ' cbProduktionsFiliale
        ' 
        cbProduktionsFiliale.FormattingEnabled = True
        cbProduktionsFiliale.Location = New System.Drawing.Point(12, 89)
        cbProduktionsFiliale.Name = "cbProduktionsFiliale"
        cbProduktionsFiliale.Size = New System.Drawing.Size(178, 21)
        cbProduktionsFiliale.TabIndex = 15
        ' 
        ' wb_Planung_Liste
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(1104, 663)
        Controls.Add(cbSupressOptimiert)
        Controls.Add(BtnOfenlisteDrucken)
        Controls.Add(BtnLagerEntnahmeListe)
        Controls.Add(BtnCheckLager)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(lblLinieArtikel)
        Controls.Add(cbArtikelLinienGruppe)
        Controls.Add(lblLinienGruppe)
        Controls.Add(cbLiniengruppe)
        Controls.Add(cbProduktionsFiliale)
        Controls.Add(dtBestellungen)
        Controls.Add(BtnTeigListeDrucken)
        Controls.Add(btnNeueCharge)
        Controls.Add(VirtualTree)
        Controls.Add(RichTextBox1)
        Controls.Add(TextBox1)
        Controls.Add(Label1)
        Controls.Add(UniversalEditBox1)
        Controls.Add(BtnTeigListeExport)
        Controls.Add(BtnBackZettelDrucken)
        Controls.Add(BtVorproduktion)
        Controls.Add(BtnBestellungen)
        Controls.Add(BtnVorlage)
        Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        HideOnClose = True
        Name = "wb_Planung_Liste"
        Text = "Teig-Herstellung WinBack"
        CType(VirtualTree, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents BtnVorlage As System.Windows.Forms.Button
    Friend WithEvents BtnBestellungen As System.Windows.Forms.Button
    Friend WithEvents BtVorproduktion As System.Windows.Forms.Button
    Friend WithEvents BtnBackZettelDrucken As System.Windows.Forms.Button
    Friend WithEvents BtnTeigListeExport As System.Windows.Forms.Button
    Friend WithEvents ColNummer As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColCharge As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColTour As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColBezeichnung As Infralution.Controls.VirtualTree.Column
    Friend WithEvents CellEditor4 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ColKommentar As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColLinie As Infralution.Controls.VirtualTree.Column
    Friend WithEvents CellEditor2 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents CellEditor1 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CellEditor3 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents UniversalEditBox1 As Infralution.Controls.UniversalEditBox
    Friend WithEvents ColSollwert As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColEinheit As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ObjectRowBinding1 As Infralution.Controls.VirtualTree.ObjectRowBinding
    Friend WithEvents btnNeueCharge As System.Windows.Forms.Button
    Friend WithEvents BtnTeigListeDrucken As System.Windows.Forms.Button
    Friend WithEvents dtBestellungen As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbProduktionsFiliale As wb_ComboBox
    Friend WithEvents ColBestand As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColEinheitBestand As Infralution.Controls.VirtualTree.Column
    Friend WithEvents lblLinieArtikel As System.Windows.Forms.Label
    Friend WithEvents cbArtikelLinienGruppe As wb_ComboBox
    Friend WithEvents lblLinienGruppe As System.Windows.Forms.Label
    Friend WithEvents cbLiniengruppe As wb_ComboBox
    Private WithEvents VirtualTree As Infralution.Controls.VirtualTree.VirtualTree
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ColStartZeit As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColType As Infralution.Controls.VirtualTree.Column
    Friend WithEvents BtnCheckLager As System.Windows.Forms.Button
    Friend WithEvents BtnLagerEntnahmeListe As System.Windows.Forms.Button
    Friend WithEvents BtnOfenlisteDrucken As System.Windows.Forms.Button
    Friend WithEvents cbSupressOptimiert As System.Windows.Forms.CheckBox
End Class
