Imports EnhEdit.EnhEdit_Global

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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Rezept_Rezeptur))
        Dim ObjectCellBinding1 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding2 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding3 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding4 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding5 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding6 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        ColNr = New Infralution.Controls.VirtualTree.Column()
        ColBezeichnung = New Infralution.Controls.VirtualTree.Column()
        EnhEdit = New Infralution.Controls.VirtualTree.CellEditor()
        EnhEdit_Rezept = New EnhEdit.EnhEdit(components)
        EnhEditText = New Infralution.Controls.VirtualTree.CellEditor()
        EnhEdit1 = New EnhEdit.EnhEdit(components)
        ColPreis = New Infralution.Controls.VirtualTree.Column()
        ColSollwert = New Infralution.Controls.VirtualTree.Column()
        ColEinheit = New Infralution.Controls.VirtualTree.Column()
        ColProzent = New Infralution.Controls.VirtualTree.Column()
        StatusStrip = New System.Windows.Forms.StatusStrip()
        ToolStripLeftMargin = New System.Windows.Forms.ToolStripStatusLabel()
        ToolStripFormat = New System.Windows.Forms.ToolStripStatusLabel()
        ToolStripRezeptChange = New System.Windows.Forms.ToolStripStatusLabel()
        ToolStripAllergenLegende = New System.Windows.Forms.ToolStripStatusLabel()
        gbDetail = New System.Windows.Forms.GroupBox()
        tbRzVariante = New System.Windows.Forms.TextBox()
        tbKnetKennlinie = New System.Windows.Forms.TextBox()
        lblRzKnKennLinie = New System.Windows.Forms.Label()
        lblEinhPreis = New System.Windows.Forms.Label()
        lblEinhMehlmenge = New System.Windows.Forms.Label()
        tbRzTA = New System.Windows.Forms.TextBox()
        tbRzMehlmenge = New System.Windows.Forms.TextBox()
        tbRzPreis = New System.Windows.Forms.TextBox()
        tbRzAendName = New System.Windows.Forms.TextBox()
        tbRzGewicht = New System.Windows.Forms.TextBox()
        lblRzLinienGruppe = New System.Windows.Forms.Label()
        tbRzTeigTemp = New System.Windows.Forms.TextBox()
        lblRzTeigTemp = New System.Windows.Forms.Label()
        tbRzAendDatum = New System.Windows.Forms.TextBox()
        lblRzAendDatum = New System.Windows.Forms.Label()
        tbRzAendNr = New System.Windows.Forms.TextBox()
        lblRzAendNr = New System.Windows.Forms.Label()
        lblRzTA = New System.Windows.Forms.Label()
        lblRzAendName = New System.Windows.Forms.Label()
        lblRzGewicht = New System.Windows.Forms.Label()
        lblEinhTeigTemp = New System.Windows.Forms.Label()
        lblEinhRzGewicht = New System.Windows.Forms.Label()
        lblRzMehlMenge = New System.Windows.Forms.Label()
        lblRzVariante = New System.Windows.Forms.Label()
        cbLiniengruppe = New wb_ComboBox()
        cbVariante = New wb_ComboBox()
        lblRzKommentar = New System.Windows.Forms.Label()
        tbRzKommentar = New System.Windows.Forms.TextBox()
        tbRezeptName = New System.Windows.Forms.TextBox()
        lblRzName = New System.Windows.Forms.Label()
        tbRzNummer = New System.Windows.Forms.TextBox()
        lblRzNummer = New System.Windows.Forms.Label()
        lblRzPreis = New System.Windows.Forms.Label()
        GroupBox1 = New System.Windows.Forms.GroupBox()
        BtnClose = New System.Windows.Forms.Button()
        BtnNwt = New System.Windows.Forms.Button()
        BtnHinweise = New System.Windows.Forms.Button()
        BtnVerwendung = New System.Windows.Forms.Button()
        BtnLoeschen = New System.Windows.Forms.Button()
        BtnKopieren = New System.Windows.Forms.Button()
        BtnDrucken = New System.Windows.Forms.Button()
        VTPopUpMenu = New System.Windows.Forms.ContextMenuStrip(components)
        headerContextMenu = New System.Windows.Forms.ContextMenuStrip(components)
        sortAscendingMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        sortDescendingMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        separator1MenuItem = New System.Windows.Forms.ToolStripSeparator()
        bestFitMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        bestFitAllMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        autoFitMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        separator2MenuItem = New System.Windows.Forms.ToolStripSeparator()
        pinnedMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        separator3MenuItem = New System.Windows.Forms.ToolStripSeparator()
        showColumnsMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        customizeMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Wb_TabControl = New wb_TabControl()
        tb_Rezeptur = New System.Windows.Forms.TabPage()
        VirtualTree = New Infralution.Controls.VirtualTree.VirtualTree()
        ObjectRowBinding1 = New Infralution.Controls.VirtualTree.ObjectRowBinding()
        tb_Naehrwerte = New System.Windows.Forms.TabPage()
        tb_Zutaten = New System.Windows.Forms.TabPage()
        BtnZutatenListeNeu = New System.Windows.Forms.Button()
        BtnExcelNwtDetails = New System.Windows.Forms.Button()
        BtnExcelNwt = New System.Windows.Forms.Button()
        Label1 = New System.Windows.Forms.Label()
        tbMehlZusammenSetzung = New System.Windows.Forms.TextBox()
        SwListeOptimieren = New MetroFramework.Controls.MetroToggle()
        lblListeOptimieren = New System.Windows.Forms.Label()
        SwENummern = New MetroFramework.Controls.MetroToggle()
        lblENummern = New System.Windows.Forms.Label()
        tb_ZutatenListe = New System.Windows.Forms.TextBox()
        Label2 = New System.Windows.Forms.Label()
        tb_Hinweise = New System.Windows.Forms.TabPage()
        TextHinweise = New System.Windows.Forms.TextBox()
        tb_Verwendung = New System.Windows.Forms.TabPage()
        GridView_RzVerwendung = New wb_DataGridView()
        StatusStrip.SuspendLayout()
        gbDetail.SuspendLayout()
        GroupBox1.SuspendLayout()
        headerContextMenu.SuspendLayout()
        Wb_TabControl.SuspendLayout()
        tb_Rezeptur.SuspendLayout()
        CType(VirtualTree, ComponentModel.ISupportInitialize).BeginInit()
        tb_Zutaten.SuspendLayout()
        tb_Hinweise.SuspendLayout()
        tb_Verwendung.SuspendLayout()
        CType(GridView_RzVerwendung, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ColNr
        ' 
        ColNr.Caption = "Nummer"
        ColNr.MinWidth = 150
        ColNr.Movable = False
        ColNr.Name = "ColNr"
        ColNr.Sortable = False
        ColNr.Width = 150
        ' 
        ' ColBezeichnung
        ' 
        ColBezeichnung.AutoFitWeight = 200F
        ColBezeichnung.Caption = "Bezeichnung"
        ColBezeichnung.CellEditor = EnhEdit
        ColBezeichnung.MinWidth = 300
        ColBezeichnung.Movable = False
        ColBezeichnung.Name = "ColBezeichnung"
        ColBezeichnung.Sortable = False
        ColBezeichnung.Width = 468
        ' 
        ' EnhEdit
        ' 
        EnhEdit.CellAlignment = Drawing.ContentAlignment.MiddleCenter
        EnhEdit.Control = EnhEdit_Rezept
        EnhEdit.UseCellColors = False
        EnhEdit.UseCellHeight = False
        EnhEdit.UseCellPadding = True
        ' 
        ' EnhEdit_Rezept
        ' 
        EnhEdit_Rezept.AutoScalePreview = False
        EnhEdit_Rezept.AutoSize = False
        EnhEdit_Rezept.BackColor = Drawing.Color.Orange
        EnhEdit_Rezept.BorderStyle = System.Windows.Forms.BorderStyle.None
        EnhEdit_Rezept.Dock = System.Windows.Forms.DockStyle.Fill
        EnhEdit_Rezept.Init = True
        EnhEdit_Rezept.Location = New System.Drawing.Point(0, 0)
        EnhEdit_Rezept.Name = "EnhEdit_Rezept"
        EnhEdit_Rezept.Size = New System.Drawing.Size(100, 17)
        EnhEdit_Rezept.TabIndex = 0
        EnhEdit_Rezept.Visible = False
        ' 
        ' EnhEditText
        ' 
        EnhEditText.Control = EnhEdit1
        EnhEditText.UseCellColors = False
        EnhEditText.UseCellHeight = False
        ' 
        ' EnhEdit1
        ' 
        EnhEdit1.BackColor = Drawing.Color.Orange
        EnhEdit1.BorderStyle = System.Windows.Forms.BorderStyle.None
        EnhEdit1.DropDownBackColor = Drawing.SystemColors.Control
        EnhEdit1.Init = True
        EnhEdit1.Location = New System.Drawing.Point(0, 0)
        EnhEdit1.Name = "EnhEdit1"
        EnhEdit1.Size = New System.Drawing.Size(195, 20)
        EnhEdit1.TabIndex = 0
        EnhEdit1.Visible = False
        ' 
        ' ColPreis
        ' 
        ColPreis.Caption = "Preis"
        ColPreis.MinWidth = 100
        ColPreis.Name = "ColPreis"
        ColPreis.Resizable = False
        ColPreis.Sortable = False
        ' 
        ' ColSollwert
        ' 
        ColSollwert.Caption = Nothing
        ColSollwert.CellEditor = EnhEdit
        ColSollwert.CellEvenStyle.Font = New System.Drawing.Font("Arial", 9.75F)
        ColSollwert.CellEvenStyle.HorzAlignment = Drawing.StringAlignment.Center
        ColSollwert.CellOddStyle.Font = New System.Drawing.Font("Arial", 9.75F)
        ColSollwert.CellOddStyle.HorzAlignment = Drawing.StringAlignment.Center
        ColSollwert.CellStyle.VertAlignment = Drawing.StringAlignment.Center
        ColSollwert.MinWidth = 100
        ColSollwert.Movable = False
        ColSollwert.Name = "ColSollwert"
        ColSollwert.Resizable = False
        ColSollwert.Sortable = False
        ColSollwert.ToolTip = "Sollwert"
        ColSollwert.Width = 126
        ' 
        ' ColEinheit
        ' 
        ColEinheit.Caption = Nothing
        ColEinheit.MinWidth = 40
        ColEinheit.Name = "ColEinheit"
        ColEinheit.Resizable = False
        ColEinheit.Selectable = False
        ColEinheit.Sortable = False
        ColEinheit.Width = 40
        ' 
        ' ColProzent
        ' 
        ColProzent.Caption = Nothing
        ColProzent.CellStyle.HorzAlignment = Drawing.StringAlignment.Far
        ColProzent.MinWidth = 50
        ColProzent.Name = "ColProzent"
        ColProzent.Resizable = False
        ColProzent.Sortable = False
        ColProzent.Width = 50
        ' 
        ' StatusStrip
        ' 
        StatusStrip.AutoSize = False
        StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ToolStripLeftMargin, ToolStripFormat, ToolStripRezeptChange, ToolStripAllergenLegende})
        StatusStrip.Location = New System.Drawing.Point(0, 709)
        StatusStrip.Name = "StatusStrip"
        StatusStrip.Size = New System.Drawing.Size(976, 22)
        StatusStrip.TabIndex = 24
        StatusStrip.Text = "StatusStrip"
        ' 
        ' ToolStripLeftMargin
        ' 
        ToolStripLeftMargin.Name = "ToolStripLeftMargin"
        ToolStripLeftMargin.Size = New System.Drawing.Size(16, 17)
        ToolStripLeftMargin.Text = "   "
        ' 
        ' ToolStripFormat
        ' 
        ToolStripFormat.Name = "ToolStripFormat"
        ToolStripFormat.Size = New System.Drawing.Size(34, 17)
        ToolStripFormat.Text = "Num"
        ToolStripFormat.Visible = False
        ' 
        ' ToolStripRezeptChange
        ' 
        ToolStripRezeptChange.Name = "ToolStripRezeptChange"
        ToolStripRezeptChange.Size = New System.Drawing.Size(128, 17)
        ToolStripRezeptChange.Text = "Rezept wurde geändert"
        ToolStripRezeptChange.Visible = False
        ' 
        ' ToolStripAllergenLegende
        ' 
        ToolStripAllergenLegende.Name = "ToolStripAllergenLegende"
        ToolStripAllergenLegende.Size = New System.Drawing.Size(520, 17)
        ToolStripAllergenLegende.Text = "Allergen-Kennzeichnung   K - keine Angaben / N - nicht enthalten  / T - Spuren von / C -Contains"
        ToolStripAllergenLegende.Visible = False
        ' 
        ' gbDetail
        ' 
        gbDetail.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        gbDetail.Controls.Add(tbRzVariante)
        gbDetail.Controls.Add(tbKnetKennlinie)
        gbDetail.Controls.Add(lblRzKnKennLinie)
        gbDetail.Controls.Add(lblEinhPreis)
        gbDetail.Controls.Add(lblEinhMehlmenge)
        gbDetail.Controls.Add(tbRzTA)
        gbDetail.Controls.Add(tbRzMehlmenge)
        gbDetail.Controls.Add(tbRzPreis)
        gbDetail.Controls.Add(tbRzAendName)
        gbDetail.Controls.Add(tbRzGewicht)
        gbDetail.Controls.Add(lblRzLinienGruppe)
        gbDetail.Controls.Add(tbRzTeigTemp)
        gbDetail.Controls.Add(lblRzTeigTemp)
        gbDetail.Controls.Add(tbRzAendDatum)
        gbDetail.Controls.Add(lblRzAendDatum)
        gbDetail.Controls.Add(tbRzAendNr)
        gbDetail.Controls.Add(lblRzAendNr)
        gbDetail.Controls.Add(lblRzTA)
        gbDetail.Controls.Add(lblRzAendName)
        gbDetail.Controls.Add(lblRzGewicht)
        gbDetail.Controls.Add(lblEinhTeigTemp)
        gbDetail.Controls.Add(lblEinhRzGewicht)
        gbDetail.Controls.Add(lblRzMehlMenge)
        gbDetail.Controls.Add(lblRzVariante)
        gbDetail.Controls.Add(cbLiniengruppe)
        gbDetail.Controls.Add(cbVariante)
        gbDetail.Controls.Add(lblRzKommentar)
        gbDetail.Controls.Add(tbRzKommentar)
        gbDetail.Controls.Add(tbRezeptName)
        gbDetail.Controls.Add(lblRzName)
        gbDetail.Controls.Add(tbRzNummer)
        gbDetail.Controls.Add(lblRzNummer)
        gbDetail.Controls.Add(lblRzPreis)
        gbDetail.Font = New System.Drawing.Font("Arial", 11.25F)
        gbDetail.Location = New System.Drawing.Point(12, 1)
        gbDetail.Name = "gbDetail"
        gbDetail.Size = New System.Drawing.Size(953, 145)
        gbDetail.TabIndex = 32
        gbDetail.TabStop = False
        ' 
        ' tbRzVariante
        ' 
        tbRzVariante.BackColor = Drawing.Color.Silver
        tbRzVariante.Location = New System.Drawing.Point(543, 49)
        tbRzVariante.Name = "tbRzVariante"
        tbRzVariante.ReadOnly = True
        tbRzVariante.Size = New System.Drawing.Size(39, 25)
        tbRzVariante.TabIndex = 69
        tbRzVariante.TabStop = False
        tbRzVariante.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        ' 
        ' tbKnetKennlinie
        ' 
        tbKnetKennlinie.Location = New System.Drawing.Point(870, 18)
        tbKnetKennlinie.Name = "tbKnetKennlinie"
        tbKnetKennlinie.Size = New System.Drawing.Size(51, 25)
        tbKnetKennlinie.TabIndex = 7
        ' 
        ' lblRzKnKennLinie
        ' 
        lblRzKnKennLinie.AutoSize = True
        lblRzKnKennLinie.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzKnKennLinie.Location = New System.Drawing.Point(801, 21)
        lblRzKnKennLinie.Name = "lblRzKnKennLinie"
        lblRzKnKennLinie.Size = New System.Drawing.Size(67, 17)
        lblRzKnKennLinie.TabIndex = 67
        lblRzKnKennLinie.Text = "KnKennl:"
        ' 
        ' lblEinhPreis
        ' 
        lblEinhPreis.AutoSize = True
        lblEinhPreis.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblEinhPreis.Location = New System.Drawing.Point(922, 52)
        lblEinhPreis.Name = "lblEinhPreis"
        lblEinhPreis.Size = New System.Drawing.Size(16, 17)
        lblEinhPreis.TabIndex = 66
        lblEinhPreis.Text = "€"
        ' 
        ' lblEinhMehlmenge
        ' 
        lblEinhMehlmenge.AutoSize = True
        lblEinhMehlmenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblEinhMehlmenge.Location = New System.Drawing.Point(922, 83)
        lblEinhMehlmenge.Name = "lblEinhMehlmenge"
        lblEinhMehlmenge.Size = New System.Drawing.Size(23, 17)
        lblEinhMehlmenge.TabIndex = 65
        lblEinhMehlmenge.Text = "kg"
        ' 
        ' tbRzTA
        ' 
        tbRzTA.AcceptsReturn = True
        tbRzTA.BackColor = Drawing.Color.Silver
        tbRzTA.Location = New System.Drawing.Point(870, 111)
        tbRzTA.Name = "tbRzTA"
        tbRzTA.ReadOnly = True
        tbRzTA.Size = New System.Drawing.Size(51, 25)
        tbRzTA.TabIndex = 64
        tbRzTA.TabStop = False
        tbRzTA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        ' 
        ' tbRzMehlmenge
        ' 
        tbRzMehlmenge.BackColor = Drawing.Color.Silver
        tbRzMehlmenge.Location = New System.Drawing.Point(870, 80)
        tbRzMehlmenge.Name = "tbRzMehlmenge"
        tbRzMehlmenge.ReadOnly = True
        tbRzMehlmenge.Size = New System.Drawing.Size(51, 25)
        tbRzMehlmenge.TabIndex = 63
        tbRzMehlmenge.TabStop = False
        tbRzMehlmenge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        ' 
        ' tbRzPreis
        ' 
        tbRzPreis.BackColor = Drawing.Color.Silver
        tbRzPreis.Location = New System.Drawing.Point(870, 49)
        tbRzPreis.Name = "tbRzPreis"
        tbRzPreis.ReadOnly = True
        tbRzPreis.Size = New System.Drawing.Size(51, 25)
        tbRzPreis.TabIndex = 62
        tbRzPreis.TabStop = False
        tbRzPreis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        ' 
        ' tbRzAendName
        ' 
        tbRzAendName.BackColor = Drawing.Color.Silver
        tbRzAendName.Location = New System.Drawing.Point(588, 111)
        tbRzAendName.Name = "tbRzAendName"
        tbRzAendName.ReadOnly = True
        tbRzAendName.Size = New System.Drawing.Size(238, 25)
        tbRzAendName.TabIndex = 61
        tbRzAendName.TabStop = False
        ' 
        ' tbRzGewicht
        ' 
        tbRzGewicht.AcceptsReturn = True
        tbRzGewicht.BackColor = Drawing.Color.Silver
        tbRzGewicht.Location = New System.Drawing.Point(588, 80)
        tbRzGewicht.Name = "tbRzGewicht"
        tbRzGewicht.ReadOnly = True
        tbRzGewicht.Size = New System.Drawing.Size(91, 25)
        tbRzGewicht.TabIndex = 60
        tbRzGewicht.TabStop = False
        ' 
        ' lblRzLinienGruppe
        ' 
        lblRzLinienGruppe.AutoSize = True
        lblRzLinienGruppe.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzLinienGruppe.Location = New System.Drawing.Point(473, 21)
        lblRzLinienGruppe.Name = "lblRzLinienGruppe"
        lblRzLinienGruppe.Size = New System.Drawing.Size(95, 17)
        lblRzLinienGruppe.TabIndex = 59
        lblRzLinienGruppe.Text = "Liniengruppe:"
        ' 
        ' tbRzTeigTemp
        ' 
        tbRzTeigTemp.Location = New System.Drawing.Point(348, 18)
        tbRzTeigTemp.Name = "tbRzTeigTemp"
        tbRzTeigTemp.Size = New System.Drawing.Size(86, 25)
        tbRzTeigTemp.TabIndex = 4
        tbRzTeigTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        ' 
        ' lblRzTeigTemp
        ' 
        lblRzTeigTemp.AutoSize = True
        lblRzTeigTemp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzTeigTemp.Location = New System.Drawing.Point(271, 21)
        lblRzTeigTemp.Name = "lblRzTeigTemp"
        lblRzTeigTemp.Size = New System.Drawing.Size(71, 17)
        lblRzTeigTemp.TabIndex = 57
        lblRzTeigTemp.Text = "Teigtemp:"
        ' 
        ' tbRzAendDatum
        ' 
        tbRzAendDatum.BackColor = Drawing.Color.Silver
        tbRzAendDatum.Location = New System.Drawing.Point(274, 111)
        tbRzAendDatum.Name = "tbRzAendDatum"
        tbRzAendDatum.ReadOnly = True
        tbRzAendDatum.Size = New System.Drawing.Size(160, 25)
        tbRzAendDatum.TabIndex = 56
        tbRzAendDatum.TabStop = False
        ' 
        ' lblRzAendDatum
        ' 
        lblRzAendDatum.AutoSize = True
        lblRzAendDatum.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzAendDatum.Location = New System.Drawing.Point(228, 114)
        lblRzAendDatum.Name = "lblRzAendDatum"
        lblRzAendDatum.Size = New System.Drawing.Size(40, 17)
        lblRzAendDatum.TabIndex = 55
        lblRzAendDatum.Text = "vom:"
        lblRzAendDatum.TextAlign = Drawing.ContentAlignment.TopRight
        ' 
        ' tbRzAendNr
        ' 
        tbRzAendNr.BackColor = Drawing.Color.Silver
        tbRzAendNr.Location = New System.Drawing.Point(115, 111)
        tbRzAendNr.Name = "tbRzAendNr"
        tbRzAendNr.ReadOnly = True
        tbRzAendNr.Size = New System.Drawing.Size(51, 25)
        tbRzAendNr.TabIndex = 54
        tbRzAendNr.TabStop = False
        tbRzAendNr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        ' 
        ' lblRzAendNr
        ' 
        lblRzAendNr.AutoSize = True
        lblRzAendNr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzAendNr.Location = New System.Drawing.Point(11, 114)
        lblRzAendNr.Name = "lblRzAendNr"
        lblRzAendNr.Size = New System.Drawing.Size(74, 17)
        lblRzAendNr.TabIndex = 53
        lblRzAendNr.Text = "Änderung:"
        ' 
        ' lblRzTA
        ' 
        lblRzTA.AutoSize = True
        lblRzTA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzTA.Location = New System.Drawing.Point(835, 114)
        lblRzTA.Name = "lblRzTA"
        lblRzTA.Size = New System.Drawing.Size(29, 17)
        lblRzTA.TabIndex = 52
        lblRzTA.Text = "TA:"
        ' 
        ' lblRzAendName
        ' 
        lblRzAendName.AutoSize = True
        lblRzAendName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzAendName.Location = New System.Drawing.Point(473, 114)
        lblRzAendName.Name = "lblRzAendName"
        lblRzAendName.Size = New System.Drawing.Size(115, 17)
        lblRzAendName.TabIndex = 51
        lblRzAendName.Text = "Änderung durch:"
        ' 
        ' lblRzGewicht
        ' 
        lblRzGewicht.AutoSize = True
        lblRzGewicht.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzGewicht.Location = New System.Drawing.Point(473, 83)
        lblRzGewicht.Name = "lblRzGewicht"
        lblRzGewicht.Size = New System.Drawing.Size(109, 17)
        lblRzGewicht.TabIndex = 50
        lblRzGewicht.Text = "Rezeptgewicht:"
        ' 
        ' lblEinhTeigTemp
        ' 
        lblEinhTeigTemp.AutoSize = True
        lblEinhTeigTemp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblEinhTeigTemp.Location = New System.Drawing.Point(436, 21)
        lblEinhTeigTemp.Name = "lblEinhTeigTemp"
        lblEinhTeigTemp.Size = New System.Drawing.Size(25, 17)
        lblEinhTeigTemp.TabIndex = 49
        lblEinhTeigTemp.Text = "°C"
        ' 
        ' lblEinhRzGewicht
        ' 
        lblEinhRzGewicht.AutoSize = True
        lblEinhRzGewicht.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblEinhRzGewicht.Location = New System.Drawing.Point(681, 83)
        lblEinhRzGewicht.Name = "lblEinhRzGewicht"
        lblEinhRzGewicht.Size = New System.Drawing.Size(23, 17)
        lblEinhRzGewicht.TabIndex = 48
        lblEinhRzGewicht.Text = "kg"
        ' 
        ' lblRzMehlMenge
        ' 
        lblRzMehlMenge.AutoSize = True
        lblRzMehlMenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzMehlMenge.Location = New System.Drawing.Point(777, 83)
        lblRzMehlMenge.Name = "lblRzMehlMenge"
        lblRzMehlMenge.Size = New System.Drawing.Size(83, 17)
        lblRzMehlMenge.TabIndex = 47
        lblRzMehlMenge.Text = "Mehlmenge"
        ' 
        ' lblRzVariante
        ' 
        lblRzVariante.AutoSize = True
        lblRzVariante.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzVariante.Location = New System.Drawing.Point(473, 52)
        lblRzVariante.Name = "lblRzVariante"
        lblRzVariante.Size = New System.Drawing.Size(64, 17)
        lblRzVariante.TabIndex = 46
        lblRzVariante.Text = "Variante:"
        ' 
        ' cbLiniengruppe
        ' 
        cbLiniengruppe.FormattingEnabled = True
        cbLiniengruppe.Location = New System.Drawing.Point(588, 18)
        cbLiniengruppe.Name = "cbLiniengruppe"
        cbLiniengruppe.Size = New System.Drawing.Size(207, 25)
        cbLiniengruppe.TabIndex = 5
        ' 
        ' cbVariante
        ' 
        cbVariante.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        cbVariante.FormattingEnabled = True
        cbVariante.Location = New System.Drawing.Point(588, 49)
        cbVariante.Name = "cbVariante"
        cbVariante.Size = New System.Drawing.Size(207, 26)
        cbVariante.TabIndex = 6
        ' 
        ' lblRzKommentar
        ' 
        lblRzKommentar.AutoSize = True
        lblRzKommentar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzKommentar.Location = New System.Drawing.Point(11, 83)
        lblRzKommentar.Name = "lblRzKommentar"
        lblRzKommentar.Size = New System.Drawing.Size(89, 17)
        lblRzKommentar.TabIndex = 43
        lblRzKommentar.Text = "Kommentar:"
        ' 
        ' tbRzKommentar
        ' 
        tbRzKommentar.Location = New System.Drawing.Point(115, 80)
        tbRzKommentar.Name = "tbRzKommentar"
        tbRzKommentar.Size = New System.Drawing.Size(319, 25)
        tbRzKommentar.TabIndex = 3
        ' 
        ' tbRezeptName
        ' 
        tbRezeptName.Location = New System.Drawing.Point(115, 49)
        tbRezeptName.Name = "tbRezeptName"
        tbRezeptName.Size = New System.Drawing.Size(319, 25)
        tbRezeptName.TabIndex = 2
        ' 
        ' lblRzName
        ' 
        lblRzName.AutoSize = True
        lblRzName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzName.Location = New System.Drawing.Point(11, 52)
        lblRzName.Name = "lblRzName"
        lblRzName.Size = New System.Drawing.Size(102, 17)
        lblRzName.TabIndex = 40
        lblRzName.Text = "Rezept Name:"
        ' 
        ' tbRzNummer
        ' 
        tbRzNummer.Location = New System.Drawing.Point(115, 18)
        tbRzNummer.Name = "tbRzNummer"
        tbRzNummer.Size = New System.Drawing.Size(105, 25)
        tbRzNummer.TabIndex = 1
        ' 
        ' lblRzNummer
        ' 
        lblRzNummer.AutoSize = True
        lblRzNummer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzNummer.Location = New System.Drawing.Point(11, 21)
        lblRzNummer.Name = "lblRzNummer"
        lblRzNummer.Size = New System.Drawing.Size(69, 17)
        lblRzNummer.TabIndex = 38
        lblRzNummer.Text = "Nummer:"
        ' 
        ' lblRzPreis
        ' 
        lblRzPreis.AutoSize = True
        lblRzPreis.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRzPreis.Location = New System.Drawing.Point(818, 52)
        lblRzPreis.Name = "lblRzPreis"
        lblRzPreis.Size = New System.Drawing.Size(46, 17)
        lblRzPreis.TabIndex = 37
        lblRzPreis.Text = "Preis:"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(BtnClose)
        GroupBox1.Controls.Add(BtnNwt)
        GroupBox1.Controls.Add(BtnHinweise)
        GroupBox1.Controls.Add(BtnVerwendung)
        GroupBox1.Controls.Add(BtnLoeschen)
        GroupBox1.Controls.Add(BtnKopieren)
        GroupBox1.Controls.Add(BtnDrucken)
        GroupBox1.Location = New System.Drawing.Point(13, 138)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(953, 75)
        GroupBox1.TabIndex = 33
        GroupBox1.TabStop = False
        ' 
        ' BtnClose
        ' 
        BtnClose.Font = New System.Drawing.Font("Arial", 9.75F)
        BtnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl
        BtnClose.Location = New System.Drawing.Point(809, 15)
        BtnClose.Name = "BtnClose"
        BtnClose.Size = New System.Drawing.Size(135, 52)
        BtnClose.TabIndex = 15
        BtnClose.TabStop = False
        BtnClose.Text = "Schliessen"
        BtnClose.UseVisualStyleBackColor = True
        ' 
        ' BtnNwt
        ' 
        BtnNwt.Font = New System.Drawing.Font("Arial", 9.75F)
        BtnNwt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        BtnNwt.Location = New System.Drawing.Point(675, 15)
        BtnNwt.Name = "BtnNwt"
        BtnNwt.Size = New System.Drawing.Size(135, 52)
        BtnNwt.TabIndex = 14
        BtnNwt.TabStop = False
        BtnNwt.Text = "Nährwerte"
        BtnNwt.UseVisualStyleBackColor = True
        ' 
        ' BtnHinweise
        ' 
        BtnHinweise.Font = New System.Drawing.Font("Arial", 9.75F)
        BtnHinweise.ImeMode = System.Windows.Forms.ImeMode.NoControl
        BtnHinweise.Location = New System.Drawing.Point(541, 15)
        BtnHinweise.Name = "BtnHinweise"
        BtnHinweise.Size = New System.Drawing.Size(135, 52)
        BtnHinweise.TabIndex = 13
        BtnHinweise.TabStop = False
        BtnHinweise.Text = "Hinweise"
        BtnHinweise.UseVisualStyleBackColor = True
        ' 
        ' BtnVerwendung
        ' 
        BtnVerwendung.Font = New System.Drawing.Font("Arial", 9.75F)
        BtnVerwendung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        BtnVerwendung.Location = New System.Drawing.Point(407, 15)
        BtnVerwendung.Name = "BtnVerwendung"
        BtnVerwendung.Size = New System.Drawing.Size(135, 52)
        BtnVerwendung.TabIndex = 12
        BtnVerwendung.TabStop = False
        BtnVerwendung.Text = "Verwendung"
        BtnVerwendung.UseVisualStyleBackColor = True
        ' 
        ' BtnLoeschen
        ' 
        BtnLoeschen.Font = New System.Drawing.Font("Arial", 9.75F)
        BtnLoeschen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        BtnLoeschen.Location = New System.Drawing.Point(273, 15)
        BtnLoeschen.Name = "BtnLoeschen"
        BtnLoeschen.Size = New System.Drawing.Size(135, 52)
        BtnLoeschen.TabIndex = 11
        BtnLoeschen.TabStop = False
        BtnLoeschen.Text = "Löschen"
        BtnLoeschen.UseVisualStyleBackColor = True
        ' 
        ' BtnKopieren
        ' 
        BtnKopieren.Font = New System.Drawing.Font("Arial", 9.75F)
        BtnKopieren.ImeMode = System.Windows.Forms.ImeMode.NoControl
        BtnKopieren.Location = New System.Drawing.Point(9, 15)
        BtnKopieren.Name = "BtnKopieren"
        BtnKopieren.Size = New System.Drawing.Size(131, 52)
        BtnKopieren.TabIndex = 10
        BtnKopieren.TabStop = False
        BtnKopieren.Text = "Kopieren"
        BtnKopieren.UseVisualStyleBackColor = True
        ' 
        ' BtnDrucken
        ' 
        BtnDrucken.Font = New System.Drawing.Font("Arial", 9.75F)
        BtnDrucken.ImeMode = System.Windows.Forms.ImeMode.NoControl
        BtnDrucken.Location = New System.Drawing.Point(139, 15)
        BtnDrucken.Name = "BtnDrucken"
        BtnDrucken.Size = New System.Drawing.Size(135, 52)
        BtnDrucken.TabIndex = 9
        BtnDrucken.TabStop = False
        BtnDrucken.Text = "Drucken"
        BtnDrucken.UseVisualStyleBackColor = True
        ' 
        ' VTPopUpMenu
        ' 
        VTPopUpMenu.Name = "VTPopUpMenu"
        VTPopUpMenu.Size = New System.Drawing.Size(61, 4)
        ' 
        ' headerContextMenu
        ' 
        headerContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {sortAscendingMenuItem, sortDescendingMenuItem, separator1MenuItem, bestFitMenuItem, bestFitAllMenuItem, autoFitMenuItem, separator2MenuItem, pinnedMenuItem, separator3MenuItem, showColumnsMenuItem, customizeMenuItem})
        headerContextMenu.Name = "headerContextMenu"
        headerContextMenu.Size = New System.Drawing.Size(277, 198)
        ' 
        ' sortAscendingMenuItem
        ' 
        sortAscendingMenuItem.Image = CType(resources.GetObject("sortAscendingMenuItem.Image"), Drawing.Image)
        sortAscendingMenuItem.Name = "sortAscendingMenuItem"
        sortAscendingMenuItem.Size = New System.Drawing.Size(276, 22)
        sortAscendingMenuItem.Tag = "sortAscendingMenuItem"
        sortAscendingMenuItem.Text = "Aufsteigend sortieren"
        ' 
        ' sortDescendingMenuItem
        ' 
        sortDescendingMenuItem.Image = CType(resources.GetObject("sortDescendingMenuItem.Image"), Drawing.Image)
        sortDescendingMenuItem.Name = "sortDescendingMenuItem"
        sortDescendingMenuItem.Size = New System.Drawing.Size(276, 22)
        sortDescendingMenuItem.Tag = "sortDescendingMenuItem"
        sortDescendingMenuItem.Text = "Absteigend sortieren"
        ' 
        ' separator1MenuItem
        ' 
        separator1MenuItem.Name = "separator1MenuItem"
        separator1MenuItem.Size = New System.Drawing.Size(273, 6)
        ' 
        ' bestFitMenuItem
        ' 
        bestFitMenuItem.Image = CType(resources.GetObject("bestFitMenuItem.Image"), Drawing.Image)
        bestFitMenuItem.Name = "bestFitMenuItem"
        bestFitMenuItem.Size = New System.Drawing.Size(276, 22)
        bestFitMenuItem.Tag = "bestFitMenuItem"
        bestFitMenuItem.Text = "Beste Anpassung"
        ' 
        ' bestFitAllMenuItem
        ' 
        bestFitAllMenuItem.Image = CType(resources.GetObject("bestFitAllMenuItem.Image"), Drawing.Image)
        bestFitAllMenuItem.Name = "bestFitAllMenuItem"
        bestFitAllMenuItem.Size = New System.Drawing.Size(276, 22)
        bestFitAllMenuItem.Tag = "bestFitAllMenuItem"
        bestFitAllMenuItem.Text = "Beste Anpassung alles"
        ' 
        ' autoFitMenuItem
        ' 
        autoFitMenuItem.Image = CType(resources.GetObject("autoFitMenuItem.Image"), Drawing.Image)
        autoFitMenuItem.Name = "autoFitMenuItem"
        autoFitMenuItem.Size = New System.Drawing.Size(276, 22)
        autoFitMenuItem.Tag = "autoFitMenuItem"
        autoFitMenuItem.Text = "Automatisch anpassen (nicht scrollen)"
        ' 
        ' separator2MenuItem
        ' 
        separator2MenuItem.Name = "separator2MenuItem"
        separator2MenuItem.Size = New System.Drawing.Size(273, 6)
        ' 
        ' pinnedMenuItem
        ' 
        pinnedMenuItem.Image = CType(resources.GetObject("pinnedMenuItem.Image"), Drawing.Image)
        pinnedMenuItem.Name = "pinnedMenuItem"
        pinnedMenuItem.Size = New System.Drawing.Size(276, 22)
        pinnedMenuItem.Tag = "pinnedMenuItem"
        pinnedMenuItem.Text = "Angesteckt"
        ' 
        ' separator3MenuItem
        ' 
        separator3MenuItem.Name = "separator3MenuItem"
        separator3MenuItem.Size = New System.Drawing.Size(273, 6)
        ' 
        ' showColumnsMenuItem
        ' 
        showColumnsMenuItem.Name = "showColumnsMenuItem"
        showColumnsMenuItem.Size = New System.Drawing.Size(276, 22)
        showColumnsMenuItem.Tag = "showColumnsMenuItem"
        showColumnsMenuItem.Text = "Spalte anzeigen/verstecken"
        ' 
        ' customizeMenuItem
        ' 
        customizeMenuItem.Image = CType(resources.GetObject("customizeMenuItem.Image"), Drawing.Image)
        customizeMenuItem.Name = "customizeMenuItem"
        customizeMenuItem.Size = New System.Drawing.Size(276, 22)
        customizeMenuItem.Tag = "customizeMenuItem"
        customizeMenuItem.Text = "Spaltenauswähler"
        ' 
        ' Wb_TabControl
        ' 
        Wb_TabControl.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        Wb_TabControl.Controls.Add(tb_Rezeptur)
        Wb_TabControl.Controls.Add(tb_Naehrwerte)
        Wb_TabControl.Controls.Add(tb_Zutaten)
        Wb_TabControl.Controls.Add(tb_Hinweise)
        Wb_TabControl.Controls.Add(tb_Verwendung)
        Wb_TabControl.Location = New System.Drawing.Point(13, 212)
        Wb_TabControl.Multiline = True
        Wb_TabControl.Name = "Wb_TabControl"
        Wb_TabControl.SelectedIndex = 0
        Wb_TabControl.Size = New System.Drawing.Size(951, 486)
        Wb_TabControl.TabIndex = 23
        ' 
        ' tb_Rezeptur
        ' 
        tb_Rezeptur.Controls.Add(VirtualTree)
        tb_Rezeptur.Location = New System.Drawing.Point(4, 23)
        tb_Rezeptur.Name = "tb_Rezeptur"
        tb_Rezeptur.Padding = New System.Windows.Forms.Padding(3)
        tb_Rezeptur.Size = New System.Drawing.Size(943, 459)
        tb_Rezeptur.TabIndex = 0
        tb_Rezeptur.Text = "Rezeptur"
        tb_Rezeptur.UseVisualStyleBackColor = True
        ' 
        ' VirtualTree
        ' 
        VirtualTree.AllowMultiSelect = False
        VirtualTree.AutoFitColumns = True
        VirtualTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        VirtualTree.Columns.Add(ColNr)
        VirtualTree.Columns.Add(ColBezeichnung)
        VirtualTree.Columns.Add(ColPreis)
        VirtualTree.Columns.Add(ColSollwert)
        VirtualTree.Columns.Add(ColEinheit)
        VirtualTree.Columns.Add(ColProzent)
        VirtualTree.ContextMenuStrip = VTPopUpMenu
        VirtualTree.Dock = System.Windows.Forms.DockStyle.Fill
        VirtualTree.EditOnKeyPress = True
        VirtualTree.Editors.Add(EnhEdit)
        VirtualTree.Editors.Add(EnhEditText)
        VirtualTree.HeaderContextMenu = headerContextMenu
        VirtualTree.HeaderHeight = 24
        VirtualTree.HeaderStyle.Font = New System.Drawing.Font("Arial", 12F)
        VirtualTree.LineStyle = Infralution.Controls.VirtualTree.LineStyle.None
        VirtualTree.Location = New System.Drawing.Point(3, 3)
        VirtualTree.Name = "VirtualTree"
        VirtualTree.RowBindings.Add(ObjectRowBinding1)
        VirtualTree.RowEvenStyle.BackColor = Drawing.Color.PowderBlue
        VirtualTree.RowSelectedStyle.AlphaBlend = CByte(255)
        VirtualTree.RowSelectedStyle.BackColor = Drawing.Color.Orange
        VirtualTree.RowSelectedStyle.BorderColor = Drawing.Color.Transparent
        VirtualTree.RowSelectedStyle.BorderWidth = 0
        VirtualTree.RowSelectedStyle.GradientColor = Drawing.Color.Orange
        VirtualTree.RowSelectedStyle.GradientMode = Drawing.Drawing2D.LinearGradientMode.Vertical
        VirtualTree.RowSelectedUnfocusedStyle.BackColor = Drawing.Color.Transparent
        VirtualTree.RowStyle.BorderColor = Drawing.Color.LightGray
        VirtualTree.RowStyle.BorderWidth = 0
        VirtualTree.ShowRootRow = False
        VirtualTree.Size = New System.Drawing.Size(937, 453)
        VirtualTree.StyleTemplate = Infralution.Controls.VirtualTree.StyleTemplate.Vista
        VirtualTree.TabIndex = 6
        ' 
        ' ObjectRowBinding1
        ' 
        ObjectRowBinding1.AllowDrag = True
        ObjectRowBinding1.AllowDropAboveRow = True
        ObjectRowBinding1.AllowDropBelowRow = True
        ObjectCellBinding1.Column = ColNr
        ObjectCellBinding1.Field = "Nummer"
        ObjectCellBinding1.Style.Font = New System.Drawing.Font("Arial", 12F)
        ObjectCellBinding1.Style.HorzAlignment = Drawing.StringAlignment.Near
        ObjectCellBinding2.Column = ColBezeichnung
        ObjectCellBinding2.Editor = EnhEditText
        ObjectCellBinding2.Field = "VirtTreeBezeichnung"
        ObjectCellBinding2.Style.Font = New System.Drawing.Font("Arial", 12F)
        ObjectCellBinding3.Column = ColPreis
        ObjectCellBinding3.Field = "VirtTreePreis"
        ObjectCellBinding3.Format = "{0:C}"
        ObjectCellBinding3.Style.Font = New System.Drawing.Font("Arial", 12F)
        ObjectCellBinding3.Style.HorzAlignment = Drawing.StringAlignment.Far
        ObjectCellBinding4.Column = ColSollwert
        ObjectCellBinding4.Editor = EnhEdit
        ObjectCellBinding4.Field = "VirtTreeSollwert"
        ObjectCellBinding4.Format = "{0:N3}"
        ObjectCellBinding4.Style.Font = New System.Drawing.Font("Arial", 12F)
        ObjectCellBinding4.Style.HorzAlignment = Drawing.StringAlignment.Far
        ObjectCellBinding5.Column = ColEinheit
        ObjectCellBinding5.Field = "VirtTreeEinheit"
        ObjectCellBinding5.Style.Font = New System.Drawing.Font("Arial", 12F)
        ObjectCellBinding6.Column = ColProzent
        ObjectCellBinding6.Field = "VirtTreeProzent"
        ObjectCellBinding6.Style.Font = New System.Drawing.Font("Arial", 12F)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding1)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding2)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding3)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding4)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding5)
        ObjectRowBinding1.CellBindings.Add(ObjectCellBinding6)
        ObjectRowBinding1.ChildProperty = "ChildSteps"
        ObjectRowBinding1.Height = 24
        ObjectRowBinding1.Name = "ObjectRowBinding1"
        ObjectRowBinding1.ParentProperty = "ParentStep"
        ObjectRowBinding1.Style.Font = New System.Drawing.Font("Arial", 12F)
        ObjectRowBinding1.TypeName = "WinBack.wb_Rezeptschritt"
        ' 
        ' tb_Naehrwerte
        ' 
        tb_Naehrwerte.Location = New System.Drawing.Point(4, 23)
        tb_Naehrwerte.Name = "tb_Naehrwerte"
        tb_Naehrwerte.Padding = New System.Windows.Forms.Padding(3)
        tb_Naehrwerte.Size = New System.Drawing.Size(943, 459)
        tb_Naehrwerte.TabIndex = 1
        tb_Naehrwerte.Text = "Nährwerte"
        tb_Naehrwerte.UseVisualStyleBackColor = True
        ' 
        ' tb_Zutaten
        ' 
        tb_Zutaten.Controls.Add(BtnZutatenListeNeu)
        tb_Zutaten.Controls.Add(BtnExcelNwtDetails)
        tb_Zutaten.Controls.Add(BtnExcelNwt)
        tb_Zutaten.Controls.Add(Label1)
        tb_Zutaten.Controls.Add(tbMehlZusammenSetzung)
        tb_Zutaten.Controls.Add(SwListeOptimieren)
        tb_Zutaten.Controls.Add(lblListeOptimieren)
        tb_Zutaten.Controls.Add(SwENummern)
        tb_Zutaten.Controls.Add(lblENummern)
        tb_Zutaten.Controls.Add(tb_ZutatenListe)
        tb_Zutaten.Controls.Add(Label2)
        tb_Zutaten.Font = New System.Drawing.Font("Arial", 11.25F)
        tb_Zutaten.Location = New System.Drawing.Point(4, 23)
        tb_Zutaten.Name = "tb_Zutaten"
        tb_Zutaten.Size = New System.Drawing.Size(943, 459)
        tb_Zutaten.TabIndex = 4
        tb_Zutaten.Text = "ZutatenListe"
        tb_Zutaten.UseVisualStyleBackColor = True
        ' 
        ' BtnZutatenListeNeu
        ' 
        BtnZutatenListeNeu.Font = New System.Drawing.Font("Arial", 9.75F)
        BtnZutatenListeNeu.Image = CType(resources.GetObject("BtnZutatenListeNeu.Image"), Drawing.Image)
        BtnZutatenListeNeu.ImeMode = System.Windows.Forms.ImeMode.NoControl
        BtnZutatenListeNeu.Location = New System.Drawing.Point(671, 161)
        BtnZutatenListeNeu.Name = "BtnZutatenListeNeu"
        BtnZutatenListeNeu.Size = New System.Drawing.Size(135, 52)
        BtnZutatenListeNeu.TabIndex = 26
        BtnZutatenListeNeu.TabStop = False
        BtnZutatenListeNeu.Text = "Zutatenliste neu berechnen"
        BtnZutatenListeNeu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnZutatenListeNeu.UseVisualStyleBackColor = True
        ' 
        ' BtnExcelNwtDetails
        ' 
        BtnExcelNwtDetails.Font = New System.Drawing.Font("Arial", 9.75F)
        BtnExcelNwtDetails.Image = CType(resources.GetObject("BtnExcelNwtDetails.Image"), Drawing.Image)
        BtnExcelNwtDetails.ImeMode = System.Windows.Forms.ImeMode.NoControl
        BtnExcelNwtDetails.Location = New System.Drawing.Point(671, 306)
        BtnExcelNwtDetails.Name = "BtnExcelNwtDetails"
        BtnExcelNwtDetails.Size = New System.Drawing.Size(135, 52)
        BtnExcelNwtDetails.TabIndex = 25
        BtnExcelNwtDetails.TabStop = False
        BtnExcelNwtDetails.Text = "Berechnung Details"
        BtnExcelNwtDetails.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnExcelNwtDetails.UseVisualStyleBackColor = True
        ' 
        ' BtnExcelNwt
        ' 
        BtnExcelNwt.Font = New System.Drawing.Font("Arial", 9.75F)
        BtnExcelNwt.Image = CType(resources.GetObject("BtnExcelNwt.Image"), Drawing.Image)
        BtnExcelNwt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        BtnExcelNwt.Location = New System.Drawing.Point(671, 248)
        BtnExcelNwt.Name = "BtnExcelNwt"
        BtnExcelNwt.Size = New System.Drawing.Size(135, 52)
        BtnExcelNwt.TabIndex = 24
        BtnExcelNwt.TabStop = False
        BtnExcelNwt.Text = "Berechnung Nährwerte"
        BtnExcelNwt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnExcelNwt.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold)
        Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label1.Location = New System.Drawing.Point(6, 361)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(180, 18)
        Label1.TabIndex = 23
        Label1.Text = "Mehl-Zusammensetzung"
        ' 
        ' tbMehlZusammenSetzung
        ' 
        tbMehlZusammenSetzung.BorderStyle = System.Windows.Forms.BorderStyle.None
        tbMehlZusammenSetzung.Font = New System.Drawing.Font("Arial", 11.25F)
        tbMehlZusammenSetzung.Location = New System.Drawing.Point(9, 382)
        tbMehlZusammenSetzung.Multiline = True
        tbMehlZusammenSetzung.Name = "tbMehlZusammenSetzung"
        tbMehlZusammenSetzung.ReadOnly = True
        tbMehlZusammenSetzung.Size = New System.Drawing.Size(633, 60)
        tbMehlZusammenSetzung.TabIndex = 22
        ' 
        ' SwListeOptimieren
        ' 
        SwListeOptimieren.AutoSize = True
        SwListeOptimieren.DisplayStatus = False
        SwListeOptimieren.FontSize = MetroFramework.MetroLinkSize.Medium
        SwListeOptimieren.ImeMode = System.Windows.Forms.ImeMode.NoControl
        SwListeOptimieren.Location = New System.Drawing.Point(825, 104)
        SwListeOptimieren.Name = "SwListeOptimieren"
        SwListeOptimieren.Size = New System.Drawing.Size(50, 21)
        SwListeOptimieren.TabIndex = 21
        SwListeOptimieren.Text = "Aus"
        SwListeOptimieren.UseVisualStyleBackColor = True
        ' 
        ' lblListeOptimieren
        ' 
        lblListeOptimieren.AutoSize = True
        lblListeOptimieren.Font = New System.Drawing.Font("Arial", 11.25F)
        lblListeOptimieren.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblListeOptimieren.Location = New System.Drawing.Point(668, 107)
        lblListeOptimieren.Name = "lblListeOptimieren"
        lblListeOptimieren.Size = New System.Drawing.Size(111, 17)
        lblListeOptimieren.TabIndex = 20
        lblListeOptimieren.Text = "Liste optimieren"
        ' 
        ' SwENummern
        ' 
        SwENummern.AutoSize = True
        SwENummern.Checked = True
        SwENummern.CheckState = System.Windows.Forms.CheckState.Checked
        SwENummern.DisplayStatus = False
        SwENummern.FontSize = MetroFramework.MetroLinkSize.Medium
        SwENummern.ImeMode = System.Windows.Forms.ImeMode.NoControl
        SwENummern.Location = New System.Drawing.Point(825, 68)
        SwENummern.Name = "SwENummern"
        SwENummern.Size = New System.Drawing.Size(50, 21)
        SwENummern.TabIndex = 19
        SwENummern.Text = "An"
        SwENummern.UseVisualStyleBackColor = True
        ' 
        ' lblENummern
        ' 
        lblENummern.AutoSize = True
        lblENummern.Font = New System.Drawing.Font("Arial", 11.25F)
        lblENummern.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblENummern.Location = New System.Drawing.Point(668, 71)
        lblENummern.Name = "lblENummern"
        lblENummern.Size = New System.Drawing.Size(151, 17)
        lblENummern.TabIndex = 18
        lblENummern.Text = "E-Nummern anzeigen"
        ' 
        ' tb_ZutatenListe
        ' 
        tb_ZutatenListe.BorderStyle = System.Windows.Forms.BorderStyle.None
        tb_ZutatenListe.Font = New System.Drawing.Font("Arial", 11.25F)
        tb_ZutatenListe.Location = New System.Drawing.Point(9, 55)
        tb_ZutatenListe.Multiline = True
        tb_ZutatenListe.Name = "tb_ZutatenListe"
        tb_ZutatenListe.ReadOnly = True
        tb_ZutatenListe.Size = New System.Drawing.Size(633, 303)
        tb_ZutatenListe.TabIndex = 15
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold)
        Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label2.Location = New System.Drawing.Point(6, 35)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(101, 18)
        Label2.TabIndex = 1
        Label2.Text = "Zutaten-Liste"
        ' 
        ' tb_Hinweise
        ' 
        tb_Hinweise.BackColor = Drawing.Color.Transparent
        tb_Hinweise.Controls.Add(TextHinweise)
        tb_Hinweise.Location = New System.Drawing.Point(4, 23)
        tb_Hinweise.Name = "tb_Hinweise"
        tb_Hinweise.Size = New System.Drawing.Size(943, 459)
        tb_Hinweise.TabIndex = 2
        tb_Hinweise.Text = "Hinweise"
        ' 
        ' TextHinweise
        ' 
        TextHinweise.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        TextHinweise.Dock = System.Windows.Forms.DockStyle.Fill
        TextHinweise.Font = New System.Drawing.Font("Arial", 11.25F)
        TextHinweise.Location = New System.Drawing.Point(0, 0)
        TextHinweise.Multiline = True
        TextHinweise.Name = "TextHinweise"
        TextHinweise.Size = New System.Drawing.Size(943, 459)
        TextHinweise.TabIndex = 0
        TextHinweise.TabStop = False
        ' 
        ' tb_Verwendung
        ' 
        tb_Verwendung.Controls.Add(GridView_RzVerwendung)
        tb_Verwendung.Location = New System.Drawing.Point(4, 23)
        tb_Verwendung.Name = "tb_Verwendung"
        tb_Verwendung.Size = New System.Drawing.Size(943, 459)
        tb_Verwendung.TabIndex = 3
        tb_Verwendung.Text = "Verwendung"
        tb_Verwendung.UseVisualStyleBackColor = True
        ' 
        ' GridView_RzVerwendung
        ' 
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True
        GridView_RzVerwendung.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        GridView_RzVerwendung.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle2.ForeColor = Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False
        GridView_RzVerwendung.DefaultCellStyle = DataGridViewCellStyle2
        GridView_RzVerwendung.Dock = System.Windows.Forms.DockStyle.Fill
        GridView_RzVerwendung.Location = New System.Drawing.Point(0, 0)
        GridView_RzVerwendung.MultiSelect = False
        GridView_RzVerwendung.Name = "GridView_RzVerwendung"
        GridView_RzVerwendung.ReadOnly = True
        GridView_RzVerwendung.Size = New System.Drawing.Size(943, 459)
        GridView_RzVerwendung.SortCol = -1
        GridView_RzVerwendung.TabIndex = 0
        GridView_RzVerwendung.x8859_5_FieldName = ""
        ' 
        ' wb_Rezept_Rezeptur
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        BackColor = Drawing.SystemColors.Control
        ClientSize = New System.Drawing.Size(976, 731)
        Controls.Add(gbDetail)
        Controls.Add(StatusStrip)
        Controls.Add(Wb_TabControl)
        Controls.Add(GroupBox1)
        Name = "wb_Rezept_Rezeptur"
        Text = "Rezeptur"
        StatusStrip.ResumeLayout(False)
        StatusStrip.PerformLayout()
        gbDetail.ResumeLayout(False)
        gbDetail.PerformLayout()
        GroupBox1.ResumeLayout(False)
        headerContextMenu.ResumeLayout(False)
        Wb_TabControl.ResumeLayout(False)
        tb_Rezeptur.ResumeLayout(False)
        CType(VirtualTree, ComponentModel.ISupportInitialize).EndInit()
        tb_Zutaten.ResumeLayout(False)
        tb_Zutaten.PerformLayout()
        tb_Hinweise.ResumeLayout(False)
        tb_Hinweise.PerformLayout()
        tb_Verwendung.ResumeLayout(False)
        CType(GridView_RzVerwendung, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub
    Friend WithEvents Wb_TabControl As wb_TabControl
    Friend WithEvents tb_Rezeptur As System.Windows.Forms.TabPage
    Friend WithEvents tb_Naehrwerte As System.Windows.Forms.TabPage
    Friend WithEvents ColNr As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColBezeichnung As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColPreis As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColSollwert As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColEinheit As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColProzent As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ObjectRowBinding1 As Infralution.Controls.VirtualTree.ObjectRowBinding
    Friend WithEvents tb_Hinweise As System.Windows.Forms.TabPage
    Friend WithEvents tb_Verwendung As System.Windows.Forms.TabPage
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripAllergenLegende As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLeftMargin As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TextHinweise As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripRezeptChange As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents gbDetail As System.Windows.Forms.GroupBox
    Friend WithEvents cbLiniengruppe As wb_ComboBox
    Friend WithEvents cbVariante As wb_ComboBox
    Friend WithEvents lblRzKommentar As System.Windows.Forms.Label
    Friend WithEvents tbRzKommentar As System.Windows.Forms.TextBox
    Friend WithEvents tbRezeptName As System.Windows.Forms.TextBox
    Friend WithEvents lblRzName As System.Windows.Forms.Label
    Friend WithEvents tbRzNummer As System.Windows.Forms.TextBox
    Friend WithEvents lblRzNummer As System.Windows.Forms.Label
    Friend WithEvents lblRzPreis As System.Windows.Forms.Label
    Friend WithEvents lblRzTA As System.Windows.Forms.Label
    Friend WithEvents lblRzAendName As System.Windows.Forms.Label
    Friend WithEvents lblRzGewicht As System.Windows.Forms.Label
    Friend WithEvents lblEinhTeigTemp As System.Windows.Forms.Label
    Friend WithEvents lblEinhRzGewicht As System.Windows.Forms.Label
    Friend WithEvents lblRzMehlMenge As System.Windows.Forms.Label
    Friend WithEvents lblRzVariante As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents BtnNwt As System.Windows.Forms.Button
    Friend WithEvents BtnHinweise As System.Windows.Forms.Button
    Friend WithEvents BtnVerwendung As System.Windows.Forms.Button
    Friend WithEvents BtnLoeschen As System.Windows.Forms.Button
    Friend WithEvents BtnKopieren As System.Windows.Forms.Button
    Friend WithEvents BtnDrucken As System.Windows.Forms.Button
    Friend WithEvents tbKnetKennlinie As System.Windows.Forms.TextBox
    Friend WithEvents lblRzKnKennLinie As System.Windows.Forms.Label
    Friend WithEvents lblEinhPreis As System.Windows.Forms.Label
    Friend WithEvents lblEinhMehlmenge As System.Windows.Forms.Label
    Friend WithEvents tbRzTA As System.Windows.Forms.TextBox
    Friend WithEvents tbRzMehlmenge As System.Windows.Forms.TextBox
    Friend WithEvents tbRzPreis As System.Windows.Forms.TextBox
    Friend WithEvents tbRzAendName As System.Windows.Forms.TextBox
    Friend WithEvents tbRzGewicht As System.Windows.Forms.TextBox
    Friend WithEvents lblRzLinienGruppe As System.Windows.Forms.Label
    Friend WithEvents tbRzTeigTemp As System.Windows.Forms.TextBox
    Friend WithEvents lblRzTeigTemp As System.Windows.Forms.Label
    Friend WithEvents tbRzAendDatum As System.Windows.Forms.TextBox
    Friend WithEvents lblRzAendDatum As System.Windows.Forms.Label
    Friend WithEvents tbRzAendNr As System.Windows.Forms.TextBox
    Friend WithEvents lblRzAendNr As System.Windows.Forms.Label
    Friend WithEvents tbRzVariante As System.Windows.Forms.TextBox
    Friend WithEvents GridView_RzVerwendung As wb_DataGridView
    Friend WithEvents tb_Zutaten As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tb_ZutatenListe As System.Windows.Forms.TextBox
    Friend WithEvents SwListeOptimieren As MetroFramework.Controls.MetroToggle
    Friend WithEvents lblListeOptimieren As System.Windows.Forms.Label
    Friend WithEvents SwENummern As MetroFramework.Controls.MetroToggle
    Friend WithEvents lblENummern As System.Windows.Forms.Label
    Friend WithEvents headerContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents sortAscendingMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sortDescendingMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents separator1MenuItem As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents bestFitMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bestFitAllMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents autoFitMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents separator2MenuItem As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents pinnedMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents separator3MenuItem As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents showColumnsMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents customizeMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VTPopUpMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EnhEdit As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents EnhEdit_Rezept As EnhEdit.EnhEdit
    Private WithEvents VirtualTree As Infralution.Controls.VirtualTree.VirtualTree
    Friend WithEvents EnhEditText As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents EnhEdit1 As EnhEdit.EnhEdit
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbMehlZusammenSetzung As System.Windows.Forms.TextBox
    Friend WithEvents BtnExcelNwtDetails As System.Windows.Forms.Button
    Friend WithEvents BtnExcelNwt As System.Windows.Forms.Button
    Friend WithEvents BtnZutatenListeNeu As System.Windows.Forms.Button
    Friend WithEvents ToolStripFormat As System.Windows.Forms.ToolStripStatusLabel
End Class
