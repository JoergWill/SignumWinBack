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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Rezept_Rezeptur))
        Dim ObjectCellBinding1 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding2 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding3 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding4 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding5 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding6 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ColNr = New Infralution.Controls.VirtualTree.Column()
        Me.ColBezeichnung = New Infralution.Controls.VirtualTree.Column()
        Me.EnhEdit = New Infralution.Controls.VirtualTree.CellEditor()
        Me.EnhEdit_Rezept = New EnhEdit.EnhEdit(Me.components)
        Me.EnhEditText = New Infralution.Controls.VirtualTree.CellEditor()
        Me.EnhEdit1 = New EnhEdit.EnhEdit(Me.components)
        Me.ColPreis = New Infralution.Controls.VirtualTree.Column()
        Me.ColSollwert = New Infralution.Controls.VirtualTree.Column()
        Me.ColEinheit = New Infralution.Controls.VirtualTree.Column()
        Me.ColProzent = New Infralution.Controls.VirtualTree.Column()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripLeftMargin = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripRezeptChange = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripAllergenLegende = New System.Windows.Forms.ToolStripStatusLabel()
        Me.gbDetail = New System.Windows.Forms.GroupBox()
        Me.tbRzVariante = New System.Windows.Forms.TextBox()
        Me.tbKnetKennlinie = New System.Windows.Forms.TextBox()
        Me.lblRzKnKennLinie = New System.Windows.Forms.Label()
        Me.lblEinhPreis = New System.Windows.Forms.Label()
        Me.lblEinhMehlmenge = New System.Windows.Forms.Label()
        Me.tbRzTA = New System.Windows.Forms.TextBox()
        Me.tbRzMehlmenge = New System.Windows.Forms.TextBox()
        Me.tbRzPreis = New System.Windows.Forms.TextBox()
        Me.tbRzAendName = New System.Windows.Forms.TextBox()
        Me.tbRzGewicht = New System.Windows.Forms.TextBox()
        Me.lblRzLinienGruppe = New System.Windows.Forms.Label()
        Me.tbRzTeigTemp = New System.Windows.Forms.TextBox()
        Me.lblRzTeigTemp = New System.Windows.Forms.Label()
        Me.tbRzAendDatum = New System.Windows.Forms.TextBox()
        Me.lblRzAendDatum = New System.Windows.Forms.Label()
        Me.tbRzAendNr = New System.Windows.Forms.TextBox()
        Me.lblRzAendNr = New System.Windows.Forms.Label()
        Me.lblRzTA = New System.Windows.Forms.Label()
        Me.lblRzAendName = New System.Windows.Forms.Label()
        Me.lblRzGewicht = New System.Windows.Forms.Label()
        Me.lblEinhTeigTemp = New System.Windows.Forms.Label()
        Me.lblEinhRzGewicht = New System.Windows.Forms.Label()
        Me.lblRzMehlMenge = New System.Windows.Forms.Label()
        Me.lblRzVariante = New System.Windows.Forms.Label()
        Me.cbLiniengruppe = New WinBack.wb_ComboBox()
        Me.cbVariante = New WinBack.wb_ComboBox()
        Me.lblRzKommentar = New System.Windows.Forms.Label()
        Me.tbRzKommentar = New System.Windows.Forms.TextBox()
        Me.tbRezeptName = New System.Windows.Forms.TextBox()
        Me.lblRzName = New System.Windows.Forms.Label()
        Me.tbRzNummer = New System.Windows.Forms.TextBox()
        Me.lblRzNummer = New System.Windows.Forms.Label()
        Me.lblRzPreis = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.BtnNwt = New System.Windows.Forms.Button()
        Me.BtnHinweise = New System.Windows.Forms.Button()
        Me.BtnVerwendung = New System.Windows.Forms.Button()
        Me.BtnLoeschen = New System.Windows.Forms.Button()
        Me.BtnKopieren = New System.Windows.Forms.Button()
        Me.BtnDrucken = New System.Windows.Forms.Button()
        Me.VTPopUpMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.headerContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.sortAscendingMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.sortDescendingMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.separator1MenuItem = New System.Windows.Forms.ToolStripSeparator()
        Me.bestFitMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.bestFitAllMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.autoFitMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.separator2MenuItem = New System.Windows.Forms.ToolStripSeparator()
        Me.pinnedMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.separator3MenuItem = New System.Windows.Forms.ToolStripSeparator()
        Me.showColumnsMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.customizeMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Wb_TabControl = New WinBack.wb_TabControl()
        Me.tb_Rezeptur = New System.Windows.Forms.TabPage()
        Me.VirtualTree = New Infralution.Controls.VirtualTree.VirtualTree()
        Me.ObjectRowBinding1 = New Infralution.Controls.VirtualTree.ObjectRowBinding()
        Me.tb_Naehrwerte = New System.Windows.Forms.TabPage()
        Me.tb_Zutaten = New System.Windows.Forms.TabPage()
        Me.BtnExcelNwtDetails = New System.Windows.Forms.Button()
        Me.BtnExcelNwt = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbMehlZusammenSetzung = New System.Windows.Forms.TextBox()
        Me.SwListeOptimieren = New MetroFramework.Controls.MetroToggle()
        Me.lblListeOptimieren = New System.Windows.Forms.Label()
        Me.SwENummern = New MetroFramework.Controls.MetroToggle()
        Me.lblENummern = New System.Windows.Forms.Label()
        Me.tb_ZutatenListe = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tb_Hinweise = New System.Windows.Forms.TabPage()
        Me.TextHinweise = New System.Windows.Forms.TextBox()
        Me.tb_Verwendung = New System.Windows.Forms.TabPage()
        Me.GridView_RzVerwendung = New WinBack.wb_DataGridView()
        Me.StatusStrip.SuspendLayout()
        Me.gbDetail.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.headerContextMenu.SuspendLayout()
        Me.Wb_TabControl.SuspendLayout()
        Me.tb_Rezeptur.SuspendLayout()
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tb_Zutaten.SuspendLayout()
        Me.tb_Hinweise.SuspendLayout()
        Me.tb_Verwendung.SuspendLayout()
        CType(Me.GridView_RzVerwendung, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'ColBezeichnung
        '
        Me.ColBezeichnung.AutoFitWeight = 200.0!
        Me.ColBezeichnung.Caption = "Bezeichnung"
        Me.ColBezeichnung.CellEditor = Me.EnhEdit
        Me.ColBezeichnung.MinWidth = 300
        Me.ColBezeichnung.Movable = False
        Me.ColBezeichnung.Name = "ColBezeichnung"
        Me.ColBezeichnung.Sortable = False
        Me.ColBezeichnung.Width = 468
        '
        'EnhEdit
        '
        Me.EnhEdit.CellAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.EnhEdit.Control = Me.EnhEdit_Rezept
        Me.EnhEdit.UseCellHeight = False
        Me.EnhEdit.UseCellPadding = True
        '
        'EnhEdit_Rezept
        '
        Me.EnhEdit_Rezept.AutoScalePreview = False
        Me.EnhEdit_Rezept.AutoSize = False
        Me.EnhEdit_Rezept.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EnhEdit_Rezept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EnhEdit_Rezept.eFormat = wb_Format.FUndefined
        Me.EnhEdit_Rezept.eOG = "0"
        Me.EnhEdit_Rezept.eUG = "0"
        Me.EnhEdit_Rezept.Init = True
        Me.EnhEdit_Rezept.Location = New System.Drawing.Point(0, 0)
        Me.EnhEdit_Rezept.Name = "EnhEdit_Rezept"
        Me.EnhEdit_Rezept.Size = New System.Drawing.Size(100, 17)
        Me.EnhEdit_Rezept.TabIndex = 0
        Me.EnhEdit_Rezept.Visible = False
        '
        'EnhEditText
        '
        Me.EnhEditText.Control = Me.EnhEdit1
        '
        'EnhEdit1
        '
        Me.EnhEdit1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EnhEdit1.eFormat = wb_Format.FUndefined
        Me.EnhEdit1.eOG = "0"
        Me.EnhEdit1.eUG = "0"
        Me.EnhEdit1.Init = True
        Me.EnhEdit1.Location = New System.Drawing.Point(0, 0)
        Me.EnhEdit1.Name = "EnhEdit1"
        Me.EnhEdit1.Size = New System.Drawing.Size(195, 17)
        Me.EnhEdit1.TabIndex = 0
        Me.EnhEdit1.Visible = False
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
        Me.ColSollwert.CellEditor = Me.EnhEdit
        Me.ColSollwert.CellEvenStyle.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.ColSollwert.CellEvenStyle.HorzAlignment = System.Drawing.StringAlignment.Center
        Me.ColSollwert.CellOddStyle.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.ColSollwert.CellOddStyle.HorzAlignment = System.Drawing.StringAlignment.Center
        Me.ColSollwert.CellStyle.VertAlignment = System.Drawing.StringAlignment.Center
        Me.ColSollwert.MinWidth = 100
        Me.ColSollwert.Movable = False
        Me.ColSollwert.Name = "ColSollwert"
        Me.ColSollwert.Resizable = False
        Me.ColSollwert.Sortable = False
        Me.ColSollwert.ToolTip = "Sollwert"
        Me.ColSollwert.Width = 126
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
        Me.ToolStripAllergenLegende.Size = New System.Drawing.Size(520, 17)
        Me.ToolStripAllergenLegende.Text = "Allergen-Kennzeichnung   K - keine Angaben / N - nicht enthalten  / T - Spuren vo" &
    "n / C -Contains"
        Me.ToolStripAllergenLegende.Visible = False
        '
        'gbDetail
        '
        Me.gbDetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbDetail.Controls.Add(Me.tbRzVariante)
        Me.gbDetail.Controls.Add(Me.tbKnetKennlinie)
        Me.gbDetail.Controls.Add(Me.lblRzKnKennLinie)
        Me.gbDetail.Controls.Add(Me.lblEinhPreis)
        Me.gbDetail.Controls.Add(Me.lblEinhMehlmenge)
        Me.gbDetail.Controls.Add(Me.tbRzTA)
        Me.gbDetail.Controls.Add(Me.tbRzMehlmenge)
        Me.gbDetail.Controls.Add(Me.tbRzPreis)
        Me.gbDetail.Controls.Add(Me.tbRzAendName)
        Me.gbDetail.Controls.Add(Me.tbRzGewicht)
        Me.gbDetail.Controls.Add(Me.lblRzLinienGruppe)
        Me.gbDetail.Controls.Add(Me.tbRzTeigTemp)
        Me.gbDetail.Controls.Add(Me.lblRzTeigTemp)
        Me.gbDetail.Controls.Add(Me.tbRzAendDatum)
        Me.gbDetail.Controls.Add(Me.lblRzAendDatum)
        Me.gbDetail.Controls.Add(Me.tbRzAendNr)
        Me.gbDetail.Controls.Add(Me.lblRzAendNr)
        Me.gbDetail.Controls.Add(Me.lblRzTA)
        Me.gbDetail.Controls.Add(Me.lblRzAendName)
        Me.gbDetail.Controls.Add(Me.lblRzGewicht)
        Me.gbDetail.Controls.Add(Me.lblEinhTeigTemp)
        Me.gbDetail.Controls.Add(Me.lblEinhRzGewicht)
        Me.gbDetail.Controls.Add(Me.lblRzMehlMenge)
        Me.gbDetail.Controls.Add(Me.lblRzVariante)
        Me.gbDetail.Controls.Add(Me.cbLiniengruppe)
        Me.gbDetail.Controls.Add(Me.cbVariante)
        Me.gbDetail.Controls.Add(Me.lblRzKommentar)
        Me.gbDetail.Controls.Add(Me.tbRzKommentar)
        Me.gbDetail.Controls.Add(Me.tbRezeptName)
        Me.gbDetail.Controls.Add(Me.lblRzName)
        Me.gbDetail.Controls.Add(Me.tbRzNummer)
        Me.gbDetail.Controls.Add(Me.lblRzNummer)
        Me.gbDetail.Controls.Add(Me.lblRzPreis)
        Me.gbDetail.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.gbDetail.Location = New System.Drawing.Point(12, 1)
        Me.gbDetail.Name = "gbDetail"
        Me.gbDetail.Size = New System.Drawing.Size(953, 145)
        Me.gbDetail.TabIndex = 32
        Me.gbDetail.TabStop = False
        '
        'tbRzVariante
        '
        Me.tbRzVariante.BackColor = System.Drawing.Color.Silver
        Me.tbRzVariante.Location = New System.Drawing.Point(543, 49)
        Me.tbRzVariante.Name = "tbRzVariante"
        Me.tbRzVariante.ReadOnly = True
        Me.tbRzVariante.Size = New System.Drawing.Size(39, 25)
        Me.tbRzVariante.TabIndex = 69
        Me.tbRzVariante.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbKnetKennlinie
        '
        Me.tbKnetKennlinie.Location = New System.Drawing.Point(870, 18)
        Me.tbKnetKennlinie.Name = "tbKnetKennlinie"
        Me.tbKnetKennlinie.Size = New System.Drawing.Size(51, 25)
        Me.tbKnetKennlinie.TabIndex = 68
        '
        'lblRzKnKennLinie
        '
        Me.lblRzKnKennLinie.AutoSize = True
        Me.lblRzKnKennLinie.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzKnKennLinie.Location = New System.Drawing.Point(801, 21)
        Me.lblRzKnKennLinie.Name = "lblRzKnKennLinie"
        Me.lblRzKnKennLinie.Size = New System.Drawing.Size(67, 17)
        Me.lblRzKnKennLinie.TabIndex = 67
        Me.lblRzKnKennLinie.Text = "KnKennl:"
        '
        'lblEinhPreis
        '
        Me.lblEinhPreis.AutoSize = True
        Me.lblEinhPreis.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblEinhPreis.Location = New System.Drawing.Point(922, 52)
        Me.lblEinhPreis.Name = "lblEinhPreis"
        Me.lblEinhPreis.Size = New System.Drawing.Size(16, 17)
        Me.lblEinhPreis.TabIndex = 66
        Me.lblEinhPreis.Text = "€"
        '
        'lblEinhMehlmenge
        '
        Me.lblEinhMehlmenge.AutoSize = True
        Me.lblEinhMehlmenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblEinhMehlmenge.Location = New System.Drawing.Point(922, 83)
        Me.lblEinhMehlmenge.Name = "lblEinhMehlmenge"
        Me.lblEinhMehlmenge.Size = New System.Drawing.Size(23, 17)
        Me.lblEinhMehlmenge.TabIndex = 65
        Me.lblEinhMehlmenge.Text = "kg"
        '
        'tbRzTA
        '
        Me.tbRzTA.BackColor = System.Drawing.Color.Silver
        Me.tbRzTA.Location = New System.Drawing.Point(870, 111)
        Me.tbRzTA.Name = "tbRzTA"
        Me.tbRzTA.ReadOnly = True
        Me.tbRzTA.Size = New System.Drawing.Size(51, 25)
        Me.tbRzTA.TabIndex = 64
        Me.tbRzTA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbRzMehlmenge
        '
        Me.tbRzMehlmenge.BackColor = System.Drawing.Color.Silver
        Me.tbRzMehlmenge.Location = New System.Drawing.Point(870, 80)
        Me.tbRzMehlmenge.Name = "tbRzMehlmenge"
        Me.tbRzMehlmenge.ReadOnly = True
        Me.tbRzMehlmenge.Size = New System.Drawing.Size(51, 25)
        Me.tbRzMehlmenge.TabIndex = 63
        Me.tbRzMehlmenge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbRzPreis
        '
        Me.tbRzPreis.BackColor = System.Drawing.Color.Silver
        Me.tbRzPreis.Location = New System.Drawing.Point(870, 49)
        Me.tbRzPreis.Name = "tbRzPreis"
        Me.tbRzPreis.ReadOnly = True
        Me.tbRzPreis.Size = New System.Drawing.Size(51, 25)
        Me.tbRzPreis.TabIndex = 62
        Me.tbRzPreis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbRzAendName
        '
        Me.tbRzAendName.BackColor = System.Drawing.Color.Silver
        Me.tbRzAendName.Location = New System.Drawing.Point(588, 111)
        Me.tbRzAendName.Name = "tbRzAendName"
        Me.tbRzAendName.ReadOnly = True
        Me.tbRzAendName.Size = New System.Drawing.Size(238, 25)
        Me.tbRzAendName.TabIndex = 61
        '
        'tbRzGewicht
        '
        Me.tbRzGewicht.BackColor = System.Drawing.Color.Silver
        Me.tbRzGewicht.Location = New System.Drawing.Point(588, 80)
        Me.tbRzGewicht.Name = "tbRzGewicht"
        Me.tbRzGewicht.ReadOnly = True
        Me.tbRzGewicht.Size = New System.Drawing.Size(91, 25)
        Me.tbRzGewicht.TabIndex = 60
        '
        'lblRzLinienGruppe
        '
        Me.lblRzLinienGruppe.AutoSize = True
        Me.lblRzLinienGruppe.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzLinienGruppe.Location = New System.Drawing.Point(473, 21)
        Me.lblRzLinienGruppe.Name = "lblRzLinienGruppe"
        Me.lblRzLinienGruppe.Size = New System.Drawing.Size(95, 17)
        Me.lblRzLinienGruppe.TabIndex = 59
        Me.lblRzLinienGruppe.Text = "Liniengruppe:"
        '
        'tbRzTeigTemp
        '
        Me.tbRzTeigTemp.Location = New System.Drawing.Point(348, 18)
        Me.tbRzTeigTemp.Name = "tbRzTeigTemp"
        Me.tbRzTeigTemp.Size = New System.Drawing.Size(86, 25)
        Me.tbRzTeigTemp.TabIndex = 58
        Me.tbRzTeigTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblRzTeigTemp
        '
        Me.lblRzTeigTemp.AutoSize = True
        Me.lblRzTeigTemp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzTeigTemp.Location = New System.Drawing.Point(271, 21)
        Me.lblRzTeigTemp.Name = "lblRzTeigTemp"
        Me.lblRzTeigTemp.Size = New System.Drawing.Size(71, 17)
        Me.lblRzTeigTemp.TabIndex = 57
        Me.lblRzTeigTemp.Text = "Teigtemp:"
        '
        'tbRzAendDatum
        '
        Me.tbRzAendDatum.BackColor = System.Drawing.Color.Silver
        Me.tbRzAendDatum.Location = New System.Drawing.Point(274, 111)
        Me.tbRzAendDatum.Name = "tbRzAendDatum"
        Me.tbRzAendDatum.ReadOnly = True
        Me.tbRzAendDatum.Size = New System.Drawing.Size(160, 25)
        Me.tbRzAendDatum.TabIndex = 56
        '
        'lblRzAendDatum
        '
        Me.lblRzAendDatum.AutoSize = True
        Me.lblRzAendDatum.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzAendDatum.Location = New System.Drawing.Point(228, 114)
        Me.lblRzAendDatum.Name = "lblRzAendDatum"
        Me.lblRzAendDatum.Size = New System.Drawing.Size(40, 17)
        Me.lblRzAendDatum.TabIndex = 55
        Me.lblRzAendDatum.Text = "vom:"
        Me.lblRzAendDatum.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'tbRzAendNr
        '
        Me.tbRzAendNr.BackColor = System.Drawing.Color.Silver
        Me.tbRzAendNr.Location = New System.Drawing.Point(115, 111)
        Me.tbRzAendNr.Name = "tbRzAendNr"
        Me.tbRzAendNr.ReadOnly = True
        Me.tbRzAendNr.Size = New System.Drawing.Size(51, 25)
        Me.tbRzAendNr.TabIndex = 54
        Me.tbRzAendNr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblRzAendNr
        '
        Me.lblRzAendNr.AutoSize = True
        Me.lblRzAendNr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzAendNr.Location = New System.Drawing.Point(11, 114)
        Me.lblRzAendNr.Name = "lblRzAendNr"
        Me.lblRzAendNr.Size = New System.Drawing.Size(74, 17)
        Me.lblRzAendNr.TabIndex = 53
        Me.lblRzAendNr.Text = "Änderung:"
        '
        'lblRzTA
        '
        Me.lblRzTA.AutoSize = True
        Me.lblRzTA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzTA.Location = New System.Drawing.Point(835, 114)
        Me.lblRzTA.Name = "lblRzTA"
        Me.lblRzTA.Size = New System.Drawing.Size(29, 17)
        Me.lblRzTA.TabIndex = 52
        Me.lblRzTA.Text = "TA:"
        '
        'lblRzAendName
        '
        Me.lblRzAendName.AutoSize = True
        Me.lblRzAendName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzAendName.Location = New System.Drawing.Point(473, 114)
        Me.lblRzAendName.Name = "lblRzAendName"
        Me.lblRzAendName.Size = New System.Drawing.Size(115, 17)
        Me.lblRzAendName.TabIndex = 51
        Me.lblRzAendName.Text = "Änderung durch:"
        '
        'lblRzGewicht
        '
        Me.lblRzGewicht.AutoSize = True
        Me.lblRzGewicht.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzGewicht.Location = New System.Drawing.Point(473, 83)
        Me.lblRzGewicht.Name = "lblRzGewicht"
        Me.lblRzGewicht.Size = New System.Drawing.Size(109, 17)
        Me.lblRzGewicht.TabIndex = 50
        Me.lblRzGewicht.Text = "Rezeptgewicht:"
        '
        'lblEinhTeigTemp
        '
        Me.lblEinhTeigTemp.AutoSize = True
        Me.lblEinhTeigTemp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblEinhTeigTemp.Location = New System.Drawing.Point(436, 21)
        Me.lblEinhTeigTemp.Name = "lblEinhTeigTemp"
        Me.lblEinhTeigTemp.Size = New System.Drawing.Size(25, 17)
        Me.lblEinhTeigTemp.TabIndex = 49
        Me.lblEinhTeigTemp.Text = "°C"
        '
        'lblEinhRzGewicht
        '
        Me.lblEinhRzGewicht.AutoSize = True
        Me.lblEinhRzGewicht.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblEinhRzGewicht.Location = New System.Drawing.Point(681, 83)
        Me.lblEinhRzGewicht.Name = "lblEinhRzGewicht"
        Me.lblEinhRzGewicht.Size = New System.Drawing.Size(23, 17)
        Me.lblEinhRzGewicht.TabIndex = 48
        Me.lblEinhRzGewicht.Text = "kg"
        '
        'lblRzMehlMenge
        '
        Me.lblRzMehlMenge.AutoSize = True
        Me.lblRzMehlMenge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzMehlMenge.Location = New System.Drawing.Point(777, 83)
        Me.lblRzMehlMenge.Name = "lblRzMehlMenge"
        Me.lblRzMehlMenge.Size = New System.Drawing.Size(87, 17)
        Me.lblRzMehlMenge.TabIndex = 47
        Me.lblRzMehlMenge.Text = "Mehlmenge:"
        '
        'lblRzVariante
        '
        Me.lblRzVariante.AutoSize = True
        Me.lblRzVariante.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzVariante.Location = New System.Drawing.Point(473, 52)
        Me.lblRzVariante.Name = "lblRzVariante"
        Me.lblRzVariante.Size = New System.Drawing.Size(64, 17)
        Me.lblRzVariante.TabIndex = 46
        Me.lblRzVariante.Text = "Variante:"
        '
        'cbLiniengruppe
        '
        Me.cbLiniengruppe.FormattingEnabled = True
        Me.cbLiniengruppe.Location = New System.Drawing.Point(588, 18)
        Me.cbLiniengruppe.Name = "cbLiniengruppe"
        Me.cbLiniengruppe.Size = New System.Drawing.Size(207, 25)
        Me.cbLiniengruppe.TabIndex = 45
        '
        'cbVariante
        '
        Me.cbVariante.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cbVariante.FormattingEnabled = True
        Me.cbVariante.Location = New System.Drawing.Point(588, 49)
        Me.cbVariante.Name = "cbVariante"
        Me.cbVariante.Size = New System.Drawing.Size(207, 26)
        Me.cbVariante.TabIndex = 44
        '
        'lblRzKommentar
        '
        Me.lblRzKommentar.AutoSize = True
        Me.lblRzKommentar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzKommentar.Location = New System.Drawing.Point(11, 83)
        Me.lblRzKommentar.Name = "lblRzKommentar"
        Me.lblRzKommentar.Size = New System.Drawing.Size(89, 17)
        Me.lblRzKommentar.TabIndex = 43
        Me.lblRzKommentar.Text = "Kommentar:"
        '
        'tbRzKommentar
        '
        Me.tbRzKommentar.Location = New System.Drawing.Point(115, 80)
        Me.tbRzKommentar.Name = "tbRzKommentar"
        Me.tbRzKommentar.Size = New System.Drawing.Size(319, 25)
        Me.tbRzKommentar.TabIndex = 42
        '
        'tbRezeptName
        '
        Me.tbRezeptName.Location = New System.Drawing.Point(115, 49)
        Me.tbRezeptName.Name = "tbRezeptName"
        Me.tbRezeptName.Size = New System.Drawing.Size(319, 25)
        Me.tbRezeptName.TabIndex = 41
        '
        'lblRzName
        '
        Me.lblRzName.AutoSize = True
        Me.lblRzName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzName.Location = New System.Drawing.Point(11, 52)
        Me.lblRzName.Name = "lblRzName"
        Me.lblRzName.Size = New System.Drawing.Size(102, 17)
        Me.lblRzName.TabIndex = 40
        Me.lblRzName.Text = "Rezept Name:"
        '
        'tbRzNummer
        '
        Me.tbRzNummer.Location = New System.Drawing.Point(115, 18)
        Me.tbRzNummer.Name = "tbRzNummer"
        Me.tbRzNummer.Size = New System.Drawing.Size(105, 25)
        Me.tbRzNummer.TabIndex = 39
        '
        'lblRzNummer
        '
        Me.lblRzNummer.AutoSize = True
        Me.lblRzNummer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzNummer.Location = New System.Drawing.Point(11, 21)
        Me.lblRzNummer.Name = "lblRzNummer"
        Me.lblRzNummer.Size = New System.Drawing.Size(69, 17)
        Me.lblRzNummer.TabIndex = 38
        Me.lblRzNummer.Text = "Nummer:"
        '
        'lblRzPreis
        '
        Me.lblRzPreis.AutoSize = True
        Me.lblRzPreis.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRzPreis.Location = New System.Drawing.Point(818, 52)
        Me.lblRzPreis.Name = "lblRzPreis"
        Me.lblRzPreis.Size = New System.Drawing.Size(46, 17)
        Me.lblRzPreis.TabIndex = 37
        Me.lblRzPreis.Text = "Preis:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BtnClose)
        Me.GroupBox1.Controls.Add(Me.BtnNwt)
        Me.GroupBox1.Controls.Add(Me.BtnHinweise)
        Me.GroupBox1.Controls.Add(Me.BtnVerwendung)
        Me.GroupBox1.Controls.Add(Me.BtnLoeschen)
        Me.GroupBox1.Controls.Add(Me.BtnKopieren)
        Me.GroupBox1.Controls.Add(Me.BtnDrucken)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 138)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(953, 75)
        Me.GroupBox1.TabIndex = 33
        Me.GroupBox1.TabStop = False
        '
        'BtnClose
        '
        Me.BtnClose.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.BtnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnClose.Location = New System.Drawing.Point(809, 15)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(135, 52)
        Me.BtnClose.TabIndex = 15
        Me.BtnClose.TabStop = False
        Me.BtnClose.Text = "Schliessen"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'BtnNwt
        '
        Me.BtnNwt.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.BtnNwt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnNwt.Location = New System.Drawing.Point(675, 15)
        Me.BtnNwt.Name = "BtnNwt"
        Me.BtnNwt.Size = New System.Drawing.Size(135, 52)
        Me.BtnNwt.TabIndex = 14
        Me.BtnNwt.TabStop = False
        Me.BtnNwt.Text = "Nährwerte"
        Me.BtnNwt.UseVisualStyleBackColor = True
        '
        'BtnHinweise
        '
        Me.BtnHinweise.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.BtnHinweise.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnHinweise.Location = New System.Drawing.Point(541, 15)
        Me.BtnHinweise.Name = "BtnHinweise"
        Me.BtnHinweise.Size = New System.Drawing.Size(135, 52)
        Me.BtnHinweise.TabIndex = 13
        Me.BtnHinweise.TabStop = False
        Me.BtnHinweise.Text = "Hinweise"
        Me.BtnHinweise.UseVisualStyleBackColor = True
        '
        'BtnVerwendung
        '
        Me.BtnVerwendung.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.BtnVerwendung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnVerwendung.Location = New System.Drawing.Point(407, 15)
        Me.BtnVerwendung.Name = "BtnVerwendung"
        Me.BtnVerwendung.Size = New System.Drawing.Size(135, 52)
        Me.BtnVerwendung.TabIndex = 12
        Me.BtnVerwendung.TabStop = False
        Me.BtnVerwendung.Text = "Verwendung"
        Me.BtnVerwendung.UseVisualStyleBackColor = True
        '
        'BtnLoeschen
        '
        Me.BtnLoeschen.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.BtnLoeschen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnLoeschen.Location = New System.Drawing.Point(273, 15)
        Me.BtnLoeschen.Name = "BtnLoeschen"
        Me.BtnLoeschen.Size = New System.Drawing.Size(135, 52)
        Me.BtnLoeschen.TabIndex = 11
        Me.BtnLoeschen.TabStop = False
        Me.BtnLoeschen.Text = "Löschen"
        Me.BtnLoeschen.UseVisualStyleBackColor = True
        '
        'BtnKopieren
        '
        Me.BtnKopieren.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.BtnKopieren.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnKopieren.Location = New System.Drawing.Point(9, 15)
        Me.BtnKopieren.Name = "BtnKopieren"
        Me.BtnKopieren.Size = New System.Drawing.Size(131, 52)
        Me.BtnKopieren.TabIndex = 10
        Me.BtnKopieren.TabStop = False
        Me.BtnKopieren.Text = "Kopieren"
        Me.BtnKopieren.UseVisualStyleBackColor = True
        '
        'BtnDrucken
        '
        Me.BtnDrucken.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.BtnDrucken.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnDrucken.Location = New System.Drawing.Point(139, 15)
        Me.BtnDrucken.Name = "BtnDrucken"
        Me.BtnDrucken.Size = New System.Drawing.Size(135, 52)
        Me.BtnDrucken.TabIndex = 9
        Me.BtnDrucken.TabStop = False
        Me.BtnDrucken.Text = "Drucken"
        Me.BtnDrucken.UseVisualStyleBackColor = True
        '
        'VTPopUpMenu
        '
        Me.VTPopUpMenu.Name = "VTPopUpMenu"
        Me.VTPopUpMenu.Size = New System.Drawing.Size(61, 4)
        '
        'headerContextMenu
        '
        Me.headerContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sortAscendingMenuItem, Me.sortDescendingMenuItem, Me.separator1MenuItem, Me.bestFitMenuItem, Me.bestFitAllMenuItem, Me.autoFitMenuItem, Me.separator2MenuItem, Me.pinnedMenuItem, Me.separator3MenuItem, Me.showColumnsMenuItem, Me.customizeMenuItem})
        Me.headerContextMenu.Name = "headerContextMenu"
        Me.headerContextMenu.Size = New System.Drawing.Size(277, 198)
        '
        'sortAscendingMenuItem
        '
        Me.sortAscendingMenuItem.Image = CType(resources.GetObject("sortAscendingMenuItem.Image"), System.Drawing.Image)
        Me.sortAscendingMenuItem.Name = "sortAscendingMenuItem"
        Me.sortAscendingMenuItem.Size = New System.Drawing.Size(276, 22)
        Me.sortAscendingMenuItem.Tag = "sortAscendingMenuItem"
        Me.sortAscendingMenuItem.Text = "Aufsteigend sortieren"
        '
        'sortDescendingMenuItem
        '
        Me.sortDescendingMenuItem.Image = CType(resources.GetObject("sortDescendingMenuItem.Image"), System.Drawing.Image)
        Me.sortDescendingMenuItem.Name = "sortDescendingMenuItem"
        Me.sortDescendingMenuItem.Size = New System.Drawing.Size(276, 22)
        Me.sortDescendingMenuItem.Tag = "sortDescendingMenuItem"
        Me.sortDescendingMenuItem.Text = "Absteigend sortieren"
        '
        'separator1MenuItem
        '
        Me.separator1MenuItem.Name = "separator1MenuItem"
        Me.separator1MenuItem.Size = New System.Drawing.Size(273, 6)
        '
        'bestFitMenuItem
        '
        Me.bestFitMenuItem.Image = CType(resources.GetObject("bestFitMenuItem.Image"), System.Drawing.Image)
        Me.bestFitMenuItem.Name = "bestFitMenuItem"
        Me.bestFitMenuItem.Size = New System.Drawing.Size(276, 22)
        Me.bestFitMenuItem.Tag = "bestFitMenuItem"
        Me.bestFitMenuItem.Text = "Beste Anpassung"
        '
        'bestFitAllMenuItem
        '
        Me.bestFitAllMenuItem.Image = CType(resources.GetObject("bestFitAllMenuItem.Image"), System.Drawing.Image)
        Me.bestFitAllMenuItem.Name = "bestFitAllMenuItem"
        Me.bestFitAllMenuItem.Size = New System.Drawing.Size(276, 22)
        Me.bestFitAllMenuItem.Tag = "bestFitAllMenuItem"
        Me.bestFitAllMenuItem.Text = "Beste Anpassung alles"
        '
        'autoFitMenuItem
        '
        Me.autoFitMenuItem.Image = CType(resources.GetObject("autoFitMenuItem.Image"), System.Drawing.Image)
        Me.autoFitMenuItem.Name = "autoFitMenuItem"
        Me.autoFitMenuItem.Size = New System.Drawing.Size(276, 22)
        Me.autoFitMenuItem.Tag = "autoFitMenuItem"
        Me.autoFitMenuItem.Text = "Automatisch anpassen (nicht scrollen)"
        '
        'separator2MenuItem
        '
        Me.separator2MenuItem.Name = "separator2MenuItem"
        Me.separator2MenuItem.Size = New System.Drawing.Size(273, 6)
        '
        'pinnedMenuItem
        '
        Me.pinnedMenuItem.Image = CType(resources.GetObject("pinnedMenuItem.Image"), System.Drawing.Image)
        Me.pinnedMenuItem.Name = "pinnedMenuItem"
        Me.pinnedMenuItem.Size = New System.Drawing.Size(276, 22)
        Me.pinnedMenuItem.Tag = "pinnedMenuItem"
        Me.pinnedMenuItem.Text = "Angesteckt"
        '
        'separator3MenuItem
        '
        Me.separator3MenuItem.Name = "separator3MenuItem"
        Me.separator3MenuItem.Size = New System.Drawing.Size(273, 6)
        '
        'showColumnsMenuItem
        '
        Me.showColumnsMenuItem.Name = "showColumnsMenuItem"
        Me.showColumnsMenuItem.Size = New System.Drawing.Size(276, 22)
        Me.showColumnsMenuItem.Tag = "showColumnsMenuItem"
        Me.showColumnsMenuItem.Text = "Spalte anzeigen/verstecken"
        '
        'customizeMenuItem
        '
        Me.customizeMenuItem.Image = CType(resources.GetObject("customizeMenuItem.Image"), System.Drawing.Image)
        Me.customizeMenuItem.Name = "customizeMenuItem"
        Me.customizeMenuItem.Size = New System.Drawing.Size(276, 22)
        Me.customizeMenuItem.Tag = "customizeMenuItem"
        Me.customizeMenuItem.Text = "Spaltenauswähler"
        '
        'Wb_TabControl
        '
        Me.Wb_TabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Wb_TabControl.Controls.Add(Me.tb_Rezeptur)
        Me.Wb_TabControl.Controls.Add(Me.tb_Naehrwerte)
        Me.Wb_TabControl.Controls.Add(Me.tb_Zutaten)
        Me.Wb_TabControl.Controls.Add(Me.tb_Hinweise)
        Me.Wb_TabControl.Controls.Add(Me.tb_Verwendung)
        Me.Wb_TabControl.Location = New System.Drawing.Point(13, 212)
        Me.Wb_TabControl.Multiline = True
        Me.Wb_TabControl.Name = "Wb_TabControl"
        Me.Wb_TabControl.SelectedIndex = 0
        Me.Wb_TabControl.Size = New System.Drawing.Size(951, 486)
        Me.Wb_TabControl.TabIndex = 23
        '
        'tb_Rezeptur
        '
        Me.tb_Rezeptur.Controls.Add(Me.VirtualTree)
        Me.tb_Rezeptur.Location = New System.Drawing.Point(4, 23)
        Me.tb_Rezeptur.Name = "tb_Rezeptur"
        Me.tb_Rezeptur.Padding = New System.Windows.Forms.Padding(3)
        Me.tb_Rezeptur.Size = New System.Drawing.Size(943, 459)
        Me.tb_Rezeptur.TabIndex = 0
        Me.tb_Rezeptur.Text = "Rezeptur"
        Me.tb_Rezeptur.UseVisualStyleBackColor = True
        '
        'VirtualTree
        '
        Me.VirtualTree.AllowMultiSelect = False
        Me.VirtualTree.AutoFitColumns = True
        Me.VirtualTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VirtualTree.Columns.Add(Me.ColNr)
        Me.VirtualTree.Columns.Add(Me.ColBezeichnung)
        Me.VirtualTree.Columns.Add(Me.ColPreis)
        Me.VirtualTree.Columns.Add(Me.ColSollwert)
        Me.VirtualTree.Columns.Add(Me.ColEinheit)
        Me.VirtualTree.Columns.Add(Me.ColProzent)
        Me.VirtualTree.ContextMenuStrip = Me.VTPopUpMenu
        Me.VirtualTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VirtualTree.EditOnKeyPress = True
        Me.VirtualTree.Editors.Add(Me.EnhEdit)
        Me.VirtualTree.Editors.Add(Me.EnhEditText)
        Me.VirtualTree.HeaderContextMenu = Me.headerContextMenu
        Me.VirtualTree.HeaderHeight = 24
        Me.VirtualTree.HeaderStyle.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.VirtualTree.LineStyle = Infralution.Controls.VirtualTree.LineStyle.None
        Me.VirtualTree.Location = New System.Drawing.Point(3, 3)
        Me.VirtualTree.Name = "VirtualTree"
        Me.VirtualTree.RowBindings.Add(Me.ObjectRowBinding1)
        Me.VirtualTree.RowEvenStyle.BackColor = System.Drawing.Color.PowderBlue
        Me.VirtualTree.RowSelectedStyle.AlphaBlend = CType(255, Byte)
        Me.VirtualTree.RowSelectedStyle.BorderColor = System.Drawing.Color.Transparent
        Me.VirtualTree.RowSelectedStyle.GradientColor = System.Drawing.Color.White
        Me.VirtualTree.RowSelectedStyle.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.VirtualTree.RowStyle.BorderColor = System.Drawing.Color.LightGray
        Me.VirtualTree.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.Cell
        Me.VirtualTree.ShowRootRow = False
        Me.VirtualTree.Size = New System.Drawing.Size(937, 453)
        Me.VirtualTree.StyleTemplate = Infralution.Controls.VirtualTree.StyleTemplate.Vista
        Me.VirtualTree.TabIndex = 6
        '
        'ObjectRowBinding1
        '
        Me.ObjectRowBinding1.AllowDrag = True
        Me.ObjectRowBinding1.AllowDropAboveRow = True
        Me.ObjectRowBinding1.AllowDropBelowRow = True
        ObjectCellBinding1.Column = Me.ColNr
        ObjectCellBinding1.Field = "Nummer"
        ObjectCellBinding1.Style.Font = New System.Drawing.Font("Arial", 12.0!)
        ObjectCellBinding1.Style.HorzAlignment = System.Drawing.StringAlignment.Near
        ObjectCellBinding2.Column = Me.ColBezeichnung
        ObjectCellBinding2.Editor = Me.EnhEditText
        ObjectCellBinding2.Field = "VirtTreeBezeichnung"
        ObjectCellBinding2.Style.Font = New System.Drawing.Font("Arial", 12.0!)
        ObjectCellBinding3.Column = Me.ColPreis
        ObjectCellBinding3.Field = "VirtTreePreis"
        ObjectCellBinding3.Format = "{0:C}"
        ObjectCellBinding3.Style.Font = New System.Drawing.Font("Arial", 12.0!)
        ObjectCellBinding3.Style.HorzAlignment = System.Drawing.StringAlignment.Far
        ObjectCellBinding4.Column = Me.ColSollwert
        ObjectCellBinding4.Editor = Me.EnhEdit
        ObjectCellBinding4.Field = "VirtTreeSollwert"
        ObjectCellBinding4.Format = "{0:N3}"
        ObjectCellBinding4.Style.Font = New System.Drawing.Font("Arial", 12.0!)
        ObjectCellBinding4.Style.HorzAlignment = System.Drawing.StringAlignment.Far
        ObjectCellBinding5.Column = Me.ColEinheit
        ObjectCellBinding5.Field = "VirtTreeEinheit"
        ObjectCellBinding5.Style.Font = New System.Drawing.Font("Arial", 12.0!)
        ObjectCellBinding6.Column = Me.ColProzent
        ObjectCellBinding6.Field = "VirtTreeProzent"
        ObjectCellBinding6.Style.Font = New System.Drawing.Font("Arial", 12.0!)
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
        Me.ObjectRowBinding1.Style.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.ObjectRowBinding1.TypeName = "WinBack.wb_Rezeptschritt"
        '
        'tb_Naehrwerte
        '
        Me.tb_Naehrwerte.Location = New System.Drawing.Point(4, 23)
        Me.tb_Naehrwerte.Name = "tb_Naehrwerte"
        Me.tb_Naehrwerte.Padding = New System.Windows.Forms.Padding(3)
        Me.tb_Naehrwerte.Size = New System.Drawing.Size(943, 459)
        Me.tb_Naehrwerte.TabIndex = 1
        Me.tb_Naehrwerte.Text = "Nährwerte"
        Me.tb_Naehrwerte.UseVisualStyleBackColor = True
        '
        'tb_Zutaten
        '
        Me.tb_Zutaten.Controls.Add(Me.BtnExcelNwtDetails)
        Me.tb_Zutaten.Controls.Add(Me.BtnExcelNwt)
        Me.tb_Zutaten.Controls.Add(Me.Label1)
        Me.tb_Zutaten.Controls.Add(Me.tbMehlZusammenSetzung)
        Me.tb_Zutaten.Controls.Add(Me.SwListeOptimieren)
        Me.tb_Zutaten.Controls.Add(Me.lblListeOptimieren)
        Me.tb_Zutaten.Controls.Add(Me.SwENummern)
        Me.tb_Zutaten.Controls.Add(Me.lblENummern)
        Me.tb_Zutaten.Controls.Add(Me.tb_ZutatenListe)
        Me.tb_Zutaten.Controls.Add(Me.Label2)
        Me.tb_Zutaten.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.tb_Zutaten.Location = New System.Drawing.Point(4, 23)
        Me.tb_Zutaten.Name = "tb_Zutaten"
        Me.tb_Zutaten.Size = New System.Drawing.Size(943, 459)
        Me.tb_Zutaten.TabIndex = 4
        Me.tb_Zutaten.Text = "ZutatenListe"
        Me.tb_Zutaten.UseVisualStyleBackColor = True
        '
        'BtnExcelNwtDetails
        '
        Me.BtnExcelNwtDetails.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.BtnExcelNwtDetails.Image = Global.WinBack.My.Resources.Resources.Excel_32x32
        Me.BtnExcelNwtDetails.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnExcelNwtDetails.Location = New System.Drawing.Point(671, 216)
        Me.BtnExcelNwtDetails.Name = "BtnExcelNwtDetails"
        Me.BtnExcelNwtDetails.Size = New System.Drawing.Size(135, 52)
        Me.BtnExcelNwtDetails.TabIndex = 25
        Me.BtnExcelNwtDetails.TabStop = False
        Me.BtnExcelNwtDetails.Text = "Berechnung Details"
        Me.BtnExcelNwtDetails.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnExcelNwtDetails.UseVisualStyleBackColor = True
        '
        'BtnExcelNwt
        '
        Me.BtnExcelNwt.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.BtnExcelNwt.Image = Global.WinBack.My.Resources.Resources.Excel_32x32
        Me.BtnExcelNwt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnExcelNwt.Location = New System.Drawing.Point(671, 158)
        Me.BtnExcelNwt.Name = "BtnExcelNwt"
        Me.BtnExcelNwt.Size = New System.Drawing.Size(135, 52)
        Me.BtnExcelNwt.TabIndex = 24
        Me.BtnExcelNwt.TabStop = False
        Me.BtnExcelNwt.Text = "Berechnung Nährwerte"
        Me.BtnExcelNwt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnExcelNwt.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(6, 361)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(180, 18)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Mehl-Zusammensetzung"
        '
        'tbMehlZusammenSetzung
        '
        Me.tbMehlZusammenSetzung.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbMehlZusammenSetzung.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.tbMehlZusammenSetzung.Location = New System.Drawing.Point(9, 382)
        Me.tbMehlZusammenSetzung.Multiline = True
        Me.tbMehlZusammenSetzung.Name = "tbMehlZusammenSetzung"
        Me.tbMehlZusammenSetzung.ReadOnly = True
        Me.tbMehlZusammenSetzung.Size = New System.Drawing.Size(633, 60)
        Me.tbMehlZusammenSetzung.TabIndex = 22
        '
        'SwListeOptimieren
        '
        Me.SwListeOptimieren.AutoSize = True
        Me.SwListeOptimieren.DisplayStatus = False
        Me.SwListeOptimieren.FontSize = MetroFramework.MetroLinkSize.Medium
        Me.SwListeOptimieren.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.SwListeOptimieren.Location = New System.Drawing.Point(825, 104)
        Me.SwListeOptimieren.Name = "SwListeOptimieren"
        Me.SwListeOptimieren.Size = New System.Drawing.Size(50, 21)
        Me.SwListeOptimieren.TabIndex = 21
        Me.SwListeOptimieren.Text = "Aus"
        Me.SwListeOptimieren.UseVisualStyleBackColor = True
        '
        'lblListeOptimieren
        '
        Me.lblListeOptimieren.AutoSize = True
        Me.lblListeOptimieren.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.lblListeOptimieren.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblListeOptimieren.Location = New System.Drawing.Point(668, 107)
        Me.lblListeOptimieren.Name = "lblListeOptimieren"
        Me.lblListeOptimieren.Size = New System.Drawing.Size(111, 17)
        Me.lblListeOptimieren.TabIndex = 20
        Me.lblListeOptimieren.Text = "Liste optimieren"
        '
        'SwENummern
        '
        Me.SwENummern.AutoSize = True
        Me.SwENummern.Checked = True
        Me.SwENummern.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SwENummern.DisplayStatus = False
        Me.SwENummern.FontSize = MetroFramework.MetroLinkSize.Medium
        Me.SwENummern.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.SwENummern.Location = New System.Drawing.Point(825, 68)
        Me.SwENummern.Name = "SwENummern"
        Me.SwENummern.Size = New System.Drawing.Size(50, 21)
        Me.SwENummern.TabIndex = 19
        Me.SwENummern.Text = "An"
        Me.SwENummern.UseVisualStyleBackColor = True
        '
        'lblENummern
        '
        Me.lblENummern.AutoSize = True
        Me.lblENummern.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.lblENummern.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblENummern.Location = New System.Drawing.Point(668, 71)
        Me.lblENummern.Name = "lblENummern"
        Me.lblENummern.Size = New System.Drawing.Size(151, 17)
        Me.lblENummern.TabIndex = 18
        Me.lblENummern.Text = "E-Nummern anzeigen"
        '
        'tb_ZutatenListe
        '
        Me.tb_ZutatenListe.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tb_ZutatenListe.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.tb_ZutatenListe.Location = New System.Drawing.Point(9, 55)
        Me.tb_ZutatenListe.Multiline = True
        Me.tb_ZutatenListe.Name = "tb_ZutatenListe"
        Me.tb_ZutatenListe.ReadOnly = True
        Me.tb_ZutatenListe.Size = New System.Drawing.Size(633, 303)
        Me.tb_ZutatenListe.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(6, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 18)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Zutaten-Liste"
        '
        'tb_Hinweise
        '
        Me.tb_Hinweise.BackColor = System.Drawing.Color.Transparent
        Me.tb_Hinweise.Controls.Add(Me.TextHinweise)
        Me.tb_Hinweise.Location = New System.Drawing.Point(4, 23)
        Me.tb_Hinweise.Name = "tb_Hinweise"
        Me.tb_Hinweise.Size = New System.Drawing.Size(943, 459)
        Me.tb_Hinweise.TabIndex = 2
        Me.tb_Hinweise.Text = "Hinweise"
        '
        'TextHinweise
        '
        Me.TextHinweise.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextHinweise.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextHinweise.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TextHinweise.Location = New System.Drawing.Point(0, 0)
        Me.TextHinweise.Multiline = True
        Me.TextHinweise.Name = "TextHinweise"
        Me.TextHinweise.Size = New System.Drawing.Size(943, 459)
        Me.TextHinweise.TabIndex = 0
        Me.TextHinweise.TabStop = False
        '
        'tb_Verwendung
        '
        Me.tb_Verwendung.Controls.Add(Me.GridView_RzVerwendung)
        Me.tb_Verwendung.Location = New System.Drawing.Point(4, 23)
        Me.tb_Verwendung.Name = "tb_Verwendung"
        Me.tb_Verwendung.Size = New System.Drawing.Size(943, 459)
        Me.tb_Verwendung.TabIndex = 3
        Me.tb_Verwendung.Text = "Verwendung"
        Me.tb_Verwendung.UseVisualStyleBackColor = True
        '
        'GridView_RzVerwendung
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView_RzVerwendung.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GridView_RzVerwendung.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridView_RzVerwendung.DefaultCellStyle = DataGridViewCellStyle2
        Me.GridView_RzVerwendung.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridView_RzVerwendung.Location = New System.Drawing.Point(0, 0)
        Me.GridView_RzVerwendung.MultiSelect = False
        Me.GridView_RzVerwendung.Name = "GridView_RzVerwendung"
        Me.GridView_RzVerwendung.ReadOnly = True
        Me.GridView_RzVerwendung.Size = New System.Drawing.Size(943, 459)
        Me.GridView_RzVerwendung.SortCol = -1
        Me.GridView_RzVerwendung.TabIndex = 0
        Me.GridView_RzVerwendung.x8859_5_FieldName = ""
        '
        'wb_Rezept_Rezeptur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(976, 731)
        Me.Controls.Add(Me.gbDetail)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.Wb_TabControl)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "wb_Rezept_Rezeptur"
        Me.Text = "Rezeptur"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.gbDetail.ResumeLayout(False)
        Me.gbDetail.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.headerContextMenu.ResumeLayout(False)
        Me.Wb_TabControl.ResumeLayout(False)
        Me.tb_Rezeptur.ResumeLayout(False)
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tb_Zutaten.ResumeLayout(False)
        Me.tb_Zutaten.PerformLayout()
        Me.tb_Hinweise.ResumeLayout(False)
        Me.tb_Hinweise.PerformLayout()
        Me.tb_Verwendung.ResumeLayout(False)
        CType(Me.GridView_RzVerwendung, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Wb_TabControl As wb_TabControl
    Friend WithEvents tb_Rezeptur As Windows.Forms.TabPage
    Friend WithEvents tb_Naehrwerte As Windows.Forms.TabPage
    Friend WithEvents ColNr As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColBezeichnung As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColPreis As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColSollwert As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColEinheit As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColProzent As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ObjectRowBinding1 As Infralution.Controls.VirtualTree.ObjectRowBinding
    Friend WithEvents tb_Hinweise As Windows.Forms.TabPage
    Friend WithEvents tb_Verwendung As Windows.Forms.TabPage
    Friend WithEvents StatusStrip As Windows.Forms.StatusStrip
    Friend WithEvents ToolStripAllergenLegende As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLeftMargin As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TextHinweise As Windows.Forms.TextBox
    Friend WithEvents ToolStripRezeptChange As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents gbDetail As Windows.Forms.GroupBox
    Friend WithEvents cbLiniengruppe As wb_ComboBox
    Friend WithEvents cbVariante As wb_ComboBox
    Friend WithEvents lblRzKommentar As Windows.Forms.Label
    Friend WithEvents tbRzKommentar As Windows.Forms.TextBox
    Friend WithEvents tbRezeptName As Windows.Forms.TextBox
    Friend WithEvents lblRzName As Windows.Forms.Label
    Friend WithEvents tbRzNummer As Windows.Forms.TextBox
    Friend WithEvents lblRzNummer As Windows.Forms.Label
    Friend WithEvents lblRzPreis As Windows.Forms.Label
    Friend WithEvents lblRzTA As Windows.Forms.Label
    Friend WithEvents lblRzAendName As Windows.Forms.Label
    Friend WithEvents lblRzGewicht As Windows.Forms.Label
    Friend WithEvents lblEinhTeigTemp As Windows.Forms.Label
    Friend WithEvents lblEinhRzGewicht As Windows.Forms.Label
    Friend WithEvents lblRzMehlMenge As Windows.Forms.Label
    Friend WithEvents lblRzVariante As Windows.Forms.Label
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents BtnClose As Windows.Forms.Button
    Friend WithEvents BtnNwt As Windows.Forms.Button
    Friend WithEvents BtnHinweise As Windows.Forms.Button
    Friend WithEvents BtnVerwendung As Windows.Forms.Button
    Friend WithEvents BtnLoeschen As Windows.Forms.Button
    Friend WithEvents BtnKopieren As Windows.Forms.Button
    Friend WithEvents BtnDrucken As Windows.Forms.Button
    Friend WithEvents tbKnetKennlinie As Windows.Forms.TextBox
    Friend WithEvents lblRzKnKennLinie As Windows.Forms.Label
    Friend WithEvents lblEinhPreis As Windows.Forms.Label
    Friend WithEvents lblEinhMehlmenge As Windows.Forms.Label
    Friend WithEvents tbRzTA As Windows.Forms.TextBox
    Friend WithEvents tbRzMehlmenge As Windows.Forms.TextBox
    Friend WithEvents tbRzPreis As Windows.Forms.TextBox
    Friend WithEvents tbRzAendName As Windows.Forms.TextBox
    Friend WithEvents tbRzGewicht As Windows.Forms.TextBox
    Friend WithEvents lblRzLinienGruppe As Windows.Forms.Label
    Friend WithEvents tbRzTeigTemp As Windows.Forms.TextBox
    Friend WithEvents lblRzTeigTemp As Windows.Forms.Label
    Friend WithEvents tbRzAendDatum As Windows.Forms.TextBox
    Friend WithEvents lblRzAendDatum As Windows.Forms.Label
    Friend WithEvents tbRzAendNr As Windows.Forms.TextBox
    Friend WithEvents lblRzAendNr As Windows.Forms.Label
    Friend WithEvents tbRzVariante As Windows.Forms.TextBox
    Friend WithEvents GridView_RzVerwendung As wb_DataGridView
    Friend WithEvents tb_Zutaten As Windows.Forms.TabPage
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tb_ZutatenListe As Windows.Forms.TextBox
    Friend WithEvents SwListeOptimieren As MetroFramework.Controls.MetroToggle
    Friend WithEvents lblListeOptimieren As Windows.Forms.Label
    Friend WithEvents SwENummern As MetroFramework.Controls.MetroToggle
    Friend WithEvents lblENummern As Windows.Forms.Label
    Friend WithEvents headerContextMenu As Windows.Forms.ContextMenuStrip
    Friend WithEvents sortAscendingMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents sortDescendingMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents separator1MenuItem As Windows.Forms.ToolStripSeparator
    Friend WithEvents bestFitMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents bestFitAllMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents autoFitMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents separator2MenuItem As Windows.Forms.ToolStripSeparator
    Friend WithEvents pinnedMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents separator3MenuItem As Windows.Forms.ToolStripSeparator
    Friend WithEvents showColumnsMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents customizeMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents VTPopUpMenu As Windows.Forms.ContextMenuStrip
    Friend WithEvents EnhEdit As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents EnhEdit_Rezept As EnhEdit.EnhEdit
    Private WithEvents VirtualTree As Infralution.Controls.VirtualTree.VirtualTree
    Friend WithEvents EnhEditText As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents EnhEdit1 As EnhEdit.EnhEdit
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents tbMehlZusammenSetzung As Windows.Forms.TextBox
    Friend WithEvents BtnExcelNwtDetails As Windows.Forms.Button
    Friend WithEvents BtnExcelNwt As Windows.Forms.Button
End Class
