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
        Me.CellEditor2 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.UniversalEditBox2 = New Infralution.Controls.UniversalEditBox()
        Me.CellEditor1 = New Infralution.Controls.VirtualTree.CellEditor()
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
        resources.ApplyResources(Me.ColNr, "ColNr")
        Me.ColNr.Movable = False
        Me.ColNr.Name = "ColNr"
        Me.ColNr.Sortable = False
        '
        'ColBezeichnung
        '
        resources.ApplyResources(Me.ColBezeichnung, "ColBezeichnung")
        Me.ColBezeichnung.CellEditor = Me.CellEditor2
        Me.ColBezeichnung.Movable = False
        Me.ColBezeichnung.Name = "ColBezeichnung"
        Me.ColBezeichnung.Sortable = False
        '
        'CellEditor2
        '
        Me.CellEditor2.Control = Me.UniversalEditBox2
        '
        'UniversalEditBox2
        '
        Me.UniversalEditBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.UniversalEditBox2, "UniversalEditBox2")
        Me.UniversalEditBox2.Name = "UniversalEditBox2"
        '
        'CellEditor1
        '
        Me.CellEditor1.CellAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CellEditor1.Control = Me.EnhEdit1
        Me.CellEditor1.UseCellPadding = True
        '
        'EnhEdit1
        '
        Me.EnhEdit1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EnhEdit1.eBackcolor = System.Drawing.Color.Empty
        Me.EnhEdit1.eBorderColor = System.Drawing.Color.Empty
        Me.EnhEdit1.eFirstRun = True
        Me.EnhEdit1.eFont = Nothing
        resources.ApplyResources(Me.EnhEdit1, "EnhEdit1")
        Me.EnhEdit1.Name = "EnhEdit1"
        '
        'ColPreis
        '
        resources.ApplyResources(Me.ColPreis, "ColPreis")
        Me.ColPreis.Name = "ColPreis"
        Me.ColPreis.Resizable = False
        Me.ColPreis.Sortable = False
        '
        'ColSollwert
        '
        resources.ApplyResources(Me.ColSollwert, "ColSollwert")
        Me.ColSollwert.CellEditor = Me.CellEditor2
        Me.ColSollwert.CellStyle.VertAlignment = CType(resources.GetObject("ColSollwert.CellStyle.VertAlignment"), System.Drawing.StringAlignment)
        Me.ColSollwert.Movable = False
        Me.ColSollwert.Name = "ColSollwert"
        Me.ColSollwert.Resizable = False
        Me.ColSollwert.Sortable = False
        '
        'ColEinheit
        '
        resources.ApplyResources(Me.ColEinheit, "ColEinheit")
        Me.ColEinheit.Name = "ColEinheit"
        Me.ColEinheit.Resizable = False
        Me.ColEinheit.Selectable = False
        Me.ColEinheit.Sortable = False
        '
        'ColProzent
        '
        resources.ApplyResources(Me.ColProzent, "ColProzent")
        Me.ColProzent.CellStyle.HorzAlignment = CType(resources.GetObject("ColProzent.CellStyle.HorzAlignment"), System.Drawing.StringAlignment)
        Me.ColProzent.Name = "ColProzent"
        Me.ColProzent.Resizable = False
        Me.ColProzent.Sortable = False
        '
        'StatusStrip
        '
        resources.ApplyResources(Me.StatusStrip, "StatusStrip")
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLeftMargin, Me.ToolStripRezeptChange, Me.ToolStripAllergenLegende})
        Me.StatusStrip.Name = "StatusStrip"
        '
        'ToolStripLeftMargin
        '
        Me.ToolStripLeftMargin.Name = "ToolStripLeftMargin"
        resources.ApplyResources(Me.ToolStripLeftMargin, "ToolStripLeftMargin")
        '
        'ToolStripRezeptChange
        '
        Me.ToolStripRezeptChange.Name = "ToolStripRezeptChange"
        resources.ApplyResources(Me.ToolStripRezeptChange, "ToolStripRezeptChange")
        '
        'ToolStripAllergenLegende
        '
        Me.ToolStripAllergenLegende.Name = "ToolStripAllergenLegende"
        resources.ApplyResources(Me.ToolStripAllergenLegende, "ToolStripAllergenLegende")
        '
        'gbDetail
        '
        resources.ApplyResources(Me.gbDetail, "gbDetail")
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
        Me.gbDetail.Name = "gbDetail"
        Me.gbDetail.TabStop = False
        '
        'tbRzVariante
        '
        Me.tbRzVariante.BackColor = System.Drawing.Color.Silver
        resources.ApplyResources(Me.tbRzVariante, "tbRzVariante")
        Me.tbRzVariante.Name = "tbRzVariante"
        Me.tbRzVariante.ReadOnly = True
        '
        'tbKnetKennlinie
        '
        resources.ApplyResources(Me.tbKnetKennlinie, "tbKnetKennlinie")
        Me.tbKnetKennlinie.Name = "tbKnetKennlinie"
        '
        'lblRzKnKennLinie
        '
        resources.ApplyResources(Me.lblRzKnKennLinie, "lblRzKnKennLinie")
        Me.lblRzKnKennLinie.Name = "lblRzKnKennLinie"
        '
        'lblEinhPreis
        '
        resources.ApplyResources(Me.lblEinhPreis, "lblEinhPreis")
        Me.lblEinhPreis.Name = "lblEinhPreis"
        '
        'lblEinhMehlmenge
        '
        resources.ApplyResources(Me.lblEinhMehlmenge, "lblEinhMehlmenge")
        Me.lblEinhMehlmenge.Name = "lblEinhMehlmenge"
        '
        'tbRzTA
        '
        Me.tbRzTA.BackColor = System.Drawing.Color.Silver
        resources.ApplyResources(Me.tbRzTA, "tbRzTA")
        Me.tbRzTA.Name = "tbRzTA"
        Me.tbRzTA.ReadOnly = True
        '
        'tbRzMehlmenge
        '
        Me.tbRzMehlmenge.BackColor = System.Drawing.Color.Silver
        resources.ApplyResources(Me.tbRzMehlmenge, "tbRzMehlmenge")
        Me.tbRzMehlmenge.Name = "tbRzMehlmenge"
        Me.tbRzMehlmenge.ReadOnly = True
        '
        'tbRzPreis
        '
        Me.tbRzPreis.BackColor = System.Drawing.Color.Silver
        resources.ApplyResources(Me.tbRzPreis, "tbRzPreis")
        Me.tbRzPreis.Name = "tbRzPreis"
        Me.tbRzPreis.ReadOnly = True
        '
        'tbRzAendName
        '
        Me.tbRzAendName.BackColor = System.Drawing.Color.Silver
        resources.ApplyResources(Me.tbRzAendName, "tbRzAendName")
        Me.tbRzAendName.Name = "tbRzAendName"
        Me.tbRzAendName.ReadOnly = True
        '
        'tbRzGewicht
        '
        Me.tbRzGewicht.BackColor = System.Drawing.Color.Silver
        resources.ApplyResources(Me.tbRzGewicht, "tbRzGewicht")
        Me.tbRzGewicht.Name = "tbRzGewicht"
        Me.tbRzGewicht.ReadOnly = True
        '
        'lblRzLinienGruppe
        '
        resources.ApplyResources(Me.lblRzLinienGruppe, "lblRzLinienGruppe")
        Me.lblRzLinienGruppe.Name = "lblRzLinienGruppe"
        '
        'tbRzTeigTemp
        '
        resources.ApplyResources(Me.tbRzTeigTemp, "tbRzTeigTemp")
        Me.tbRzTeigTemp.Name = "tbRzTeigTemp"
        '
        'lblRzTeigTemp
        '
        resources.ApplyResources(Me.lblRzTeigTemp, "lblRzTeigTemp")
        Me.lblRzTeigTemp.Name = "lblRzTeigTemp"
        '
        'tbRzAendDatum
        '
        Me.tbRzAendDatum.BackColor = System.Drawing.Color.Silver
        resources.ApplyResources(Me.tbRzAendDatum, "tbRzAendDatum")
        Me.tbRzAendDatum.Name = "tbRzAendDatum"
        Me.tbRzAendDatum.ReadOnly = True
        '
        'lblRzAendDatum
        '
        resources.ApplyResources(Me.lblRzAendDatum, "lblRzAendDatum")
        Me.lblRzAendDatum.Name = "lblRzAendDatum"
        '
        'tbRzAendNr
        '
        Me.tbRzAendNr.BackColor = System.Drawing.Color.Silver
        resources.ApplyResources(Me.tbRzAendNr, "tbRzAendNr")
        Me.tbRzAendNr.Name = "tbRzAendNr"
        Me.tbRzAendNr.ReadOnly = True
        '
        'lblRzAendNr
        '
        resources.ApplyResources(Me.lblRzAendNr, "lblRzAendNr")
        Me.lblRzAendNr.Name = "lblRzAendNr"
        '
        'lblRzTA
        '
        resources.ApplyResources(Me.lblRzTA, "lblRzTA")
        Me.lblRzTA.Name = "lblRzTA"
        '
        'lblRzAendName
        '
        resources.ApplyResources(Me.lblRzAendName, "lblRzAendName")
        Me.lblRzAendName.Name = "lblRzAendName"
        '
        'lblRzGewicht
        '
        resources.ApplyResources(Me.lblRzGewicht, "lblRzGewicht")
        Me.lblRzGewicht.Name = "lblRzGewicht"
        '
        'lblEinhTeigTemp
        '
        resources.ApplyResources(Me.lblEinhTeigTemp, "lblEinhTeigTemp")
        Me.lblEinhTeigTemp.Name = "lblEinhTeigTemp"
        '
        'lblEinhRzGewicht
        '
        resources.ApplyResources(Me.lblEinhRzGewicht, "lblEinhRzGewicht")
        Me.lblEinhRzGewicht.Name = "lblEinhRzGewicht"
        '
        'lblRzMehlMenge
        '
        resources.ApplyResources(Me.lblRzMehlMenge, "lblRzMehlMenge")
        Me.lblRzMehlMenge.Name = "lblRzMehlMenge"
        '
        'lblRzVariante
        '
        resources.ApplyResources(Me.lblRzVariante, "lblRzVariante")
        Me.lblRzVariante.Name = "lblRzVariante"
        '
        'cbLiniengruppe
        '
        Me.cbLiniengruppe.FormattingEnabled = True
        resources.ApplyResources(Me.cbLiniengruppe, "cbLiniengruppe")
        Me.cbLiniengruppe.Name = "cbLiniengruppe"
        '
        'cbVariante
        '
        Me.cbVariante.FormattingEnabled = True
        resources.ApplyResources(Me.cbVariante, "cbVariante")
        Me.cbVariante.Name = "cbVariante"
        '
        'lblRzKommentar
        '
        resources.ApplyResources(Me.lblRzKommentar, "lblRzKommentar")
        Me.lblRzKommentar.Name = "lblRzKommentar"
        '
        'tbRzKommentar
        '
        resources.ApplyResources(Me.tbRzKommentar, "tbRzKommentar")
        Me.tbRzKommentar.Name = "tbRzKommentar"
        '
        'tbRezeptName
        '
        resources.ApplyResources(Me.tbRezeptName, "tbRezeptName")
        Me.tbRezeptName.Name = "tbRezeptName"
        '
        'lblRzName
        '
        resources.ApplyResources(Me.lblRzName, "lblRzName")
        Me.lblRzName.Name = "lblRzName"
        '
        'tbRzNummer
        '
        resources.ApplyResources(Me.tbRzNummer, "tbRzNummer")
        Me.tbRzNummer.Name = "tbRzNummer"
        '
        'lblRzNummer
        '
        resources.ApplyResources(Me.lblRzNummer, "lblRzNummer")
        Me.lblRzNummer.Name = "lblRzNummer"
        '
        'lblRzPreis
        '
        resources.ApplyResources(Me.lblRzPreis, "lblRzPreis")
        Me.lblRzPreis.Name = "lblRzPreis"
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
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'BtnClose
        '
        resources.ApplyResources(Me.BtnClose, "BtnClose")
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.TabStop = False
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'BtnNwt
        '
        resources.ApplyResources(Me.BtnNwt, "BtnNwt")
        Me.BtnNwt.Name = "BtnNwt"
        Me.BtnNwt.TabStop = False
        Me.BtnNwt.UseVisualStyleBackColor = True
        '
        'BtnHinweise
        '
        resources.ApplyResources(Me.BtnHinweise, "BtnHinweise")
        Me.BtnHinweise.Name = "BtnHinweise"
        Me.BtnHinweise.TabStop = False
        Me.BtnHinweise.UseVisualStyleBackColor = True
        '
        'BtnVerwendung
        '
        resources.ApplyResources(Me.BtnVerwendung, "BtnVerwendung")
        Me.BtnVerwendung.Name = "BtnVerwendung"
        Me.BtnVerwendung.TabStop = False
        Me.BtnVerwendung.UseVisualStyleBackColor = True
        '
        'BtnLoeschen
        '
        resources.ApplyResources(Me.BtnLoeschen, "BtnLoeschen")
        Me.BtnLoeschen.Name = "BtnLoeschen"
        Me.BtnLoeschen.TabStop = False
        Me.BtnLoeschen.UseVisualStyleBackColor = True
        '
        'BtnKopieren
        '
        resources.ApplyResources(Me.BtnKopieren, "BtnKopieren")
        Me.BtnKopieren.Name = "BtnKopieren"
        Me.BtnKopieren.TabStop = False
        Me.BtnKopieren.UseVisualStyleBackColor = True
        '
        'BtnDrucken
        '
        resources.ApplyResources(Me.BtnDrucken, "BtnDrucken")
        Me.BtnDrucken.Name = "BtnDrucken"
        Me.BtnDrucken.TabStop = False
        Me.BtnDrucken.UseVisualStyleBackColor = True
        '
        'VTPopUpMenu
        '
        Me.VTPopUpMenu.Name = "VTPopUpMenu"
        resources.ApplyResources(Me.VTPopUpMenu, "VTPopUpMenu")
        '
        'headerContextMenu
        '
        Me.headerContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sortAscendingMenuItem, Me.sortDescendingMenuItem, Me.separator1MenuItem, Me.bestFitMenuItem, Me.bestFitAllMenuItem, Me.autoFitMenuItem, Me.separator2MenuItem, Me.pinnedMenuItem, Me.separator3MenuItem, Me.showColumnsMenuItem, Me.customizeMenuItem})
        Me.headerContextMenu.Name = "headerContextMenu"
        resources.ApplyResources(Me.headerContextMenu, "headerContextMenu")
        '
        'sortAscendingMenuItem
        '
        resources.ApplyResources(Me.sortAscendingMenuItem, "sortAscendingMenuItem")
        Me.sortAscendingMenuItem.Name = "sortAscendingMenuItem"
        Me.sortAscendingMenuItem.Tag = "sortAscendingMenuItem"
        '
        'sortDescendingMenuItem
        '
        resources.ApplyResources(Me.sortDescendingMenuItem, "sortDescendingMenuItem")
        Me.sortDescendingMenuItem.Name = "sortDescendingMenuItem"
        Me.sortDescendingMenuItem.Tag = "sortDescendingMenuItem"
        '
        'separator1MenuItem
        '
        Me.separator1MenuItem.Name = "separator1MenuItem"
        resources.ApplyResources(Me.separator1MenuItem, "separator1MenuItem")
        '
        'bestFitMenuItem
        '
        resources.ApplyResources(Me.bestFitMenuItem, "bestFitMenuItem")
        Me.bestFitMenuItem.Name = "bestFitMenuItem"
        Me.bestFitMenuItem.Tag = "bestFitMenuItem"
        '
        'bestFitAllMenuItem
        '
        resources.ApplyResources(Me.bestFitAllMenuItem, "bestFitAllMenuItem")
        Me.bestFitAllMenuItem.Name = "bestFitAllMenuItem"
        Me.bestFitAllMenuItem.Tag = "bestFitAllMenuItem"
        '
        'autoFitMenuItem
        '
        resources.ApplyResources(Me.autoFitMenuItem, "autoFitMenuItem")
        Me.autoFitMenuItem.Name = "autoFitMenuItem"
        Me.autoFitMenuItem.Tag = "autoFitMenuItem"
        '
        'separator2MenuItem
        '
        Me.separator2MenuItem.Name = "separator2MenuItem"
        resources.ApplyResources(Me.separator2MenuItem, "separator2MenuItem")
        '
        'pinnedMenuItem
        '
        resources.ApplyResources(Me.pinnedMenuItem, "pinnedMenuItem")
        Me.pinnedMenuItem.Name = "pinnedMenuItem"
        Me.pinnedMenuItem.Tag = "pinnedMenuItem"
        '
        'separator3MenuItem
        '
        Me.separator3MenuItem.Name = "separator3MenuItem"
        resources.ApplyResources(Me.separator3MenuItem, "separator3MenuItem")
        '
        'showColumnsMenuItem
        '
        Me.showColumnsMenuItem.Name = "showColumnsMenuItem"
        resources.ApplyResources(Me.showColumnsMenuItem, "showColumnsMenuItem")
        Me.showColumnsMenuItem.Tag = "showColumnsMenuItem"
        '
        'customizeMenuItem
        '
        resources.ApplyResources(Me.customizeMenuItem, "customizeMenuItem")
        Me.customizeMenuItem.Name = "customizeMenuItem"
        Me.customizeMenuItem.Tag = "customizeMenuItem"
        '
        'Wb_TabControl
        '
        resources.ApplyResources(Me.Wb_TabControl, "Wb_TabControl")
        Me.Wb_TabControl.Controls.Add(Me.tb_Rezeptur)
        Me.Wb_TabControl.Controls.Add(Me.tb_Naehrwerte)
        Me.Wb_TabControl.Controls.Add(Me.tb_Zutaten)
        Me.Wb_TabControl.Controls.Add(Me.tb_Hinweise)
        Me.Wb_TabControl.Controls.Add(Me.tb_Verwendung)
        Me.Wb_TabControl.Multiline = True
        Me.Wb_TabControl.Name = "Wb_TabControl"
        Me.Wb_TabControl.SelectedIndex = 0
        '
        'tb_Rezeptur
        '
        Me.tb_Rezeptur.Controls.Add(Me.VirtualTree)
        resources.ApplyResources(Me.tb_Rezeptur, "tb_Rezeptur")
        Me.tb_Rezeptur.Name = "tb_Rezeptur"
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
        Me.VirtualTree.EditOnKeyPress = True
        Me.VirtualTree.Editors.Add(Me.CellEditor2)
        Me.VirtualTree.Editors.Add(Me.CellEditor1)
        Me.VirtualTree.HeaderContextMenu = Me.headerContextMenu
        Me.VirtualTree.LineStyle = Infralution.Controls.VirtualTree.LineStyle.None
        resources.ApplyResources(Me.VirtualTree, "VirtualTree")
        Me.VirtualTree.MainColumn = Me.ColNr
        Me.VirtualTree.Name = "VirtualTree"
        Me.VirtualTree.RowBindings.Add(Me.ObjectRowBinding1)
        Me.VirtualTree.RowEvenStyle.BackColor = System.Drawing.Color.PowderBlue
        Me.VirtualTree.RowStyle.BorderColor = System.Drawing.Color.LightGray
        Me.VirtualTree.ShowRootRow = False
        Me.VirtualTree.StyleTemplate = Infralution.Controls.VirtualTree.StyleTemplate.Vista
        '
        'ObjectRowBinding1
        '
        Me.ObjectRowBinding1.AllowDrag = True
        Me.ObjectRowBinding1.AllowDropAboveRow = True
        Me.ObjectRowBinding1.AllowDropBelowRow = True
        ObjectCellBinding1.Column = Me.ColNr
        ObjectCellBinding1.Field = "Nummer"
        ObjectCellBinding1.Style.HorzAlignment = CType(resources.GetObject("resource.HorzAlignment"), System.Drawing.StringAlignment)
        ObjectCellBinding2.Column = Me.ColBezeichnung
        ObjectCellBinding2.Editor = Me.CellEditor1
        ObjectCellBinding2.Field = "VirtTreeBezeichnung"
        ObjectCellBinding3.Column = Me.ColPreis
        ObjectCellBinding3.Field = "VirtTreePreis"
        ObjectCellBinding3.Style.HorzAlignment = CType(resources.GetObject("resource.HorzAlignment1"), System.Drawing.StringAlignment)
        ObjectCellBinding4.Column = Me.ColSollwert
        ObjectCellBinding4.Editor = Me.CellEditor1
        ObjectCellBinding4.Field = "VirtTreeSollwert"
        ObjectCellBinding4.Style.HorzAlignment = CType(resources.GetObject("resource.HorzAlignment2"), System.Drawing.StringAlignment)
        ObjectCellBinding5.Column = Me.ColEinheit
        ObjectCellBinding5.Field = "VirtTreeEinheit"
        ObjectCellBinding6.Column = Me.ColProzent
        ObjectCellBinding6.Field = "VirtTreeProzent"
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding1)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding2)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding3)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding4)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding5)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding6)
        Me.ObjectRowBinding1.ChildProperty = "ChildSteps"
        Me.ObjectRowBinding1.Name = "ObjectRowBinding1"
        Me.ObjectRowBinding1.ParentProperty = "ParentStep"
        Me.ObjectRowBinding1.TypeName = "WinBack.wb_Rezeptschritt"
        '
        'tb_Naehrwerte
        '
        resources.ApplyResources(Me.tb_Naehrwerte, "tb_Naehrwerte")
        Me.tb_Naehrwerte.Name = "tb_Naehrwerte"
        Me.tb_Naehrwerte.UseVisualStyleBackColor = True
        '
        'tb_Zutaten
        '
        Me.tb_Zutaten.Controls.Add(Me.SwListeOptimieren)
        Me.tb_Zutaten.Controls.Add(Me.lblListeOptimieren)
        Me.tb_Zutaten.Controls.Add(Me.SwENummern)
        Me.tb_Zutaten.Controls.Add(Me.lblENummern)
        Me.tb_Zutaten.Controls.Add(Me.tb_ZutatenListe)
        Me.tb_Zutaten.Controls.Add(Me.Label2)
        resources.ApplyResources(Me.tb_Zutaten, "tb_Zutaten")
        Me.tb_Zutaten.Name = "tb_Zutaten"
        Me.tb_Zutaten.UseVisualStyleBackColor = True
        '
        'SwListeOptimieren
        '
        resources.ApplyResources(Me.SwListeOptimieren, "SwListeOptimieren")
        Me.SwListeOptimieren.DisplayStatus = False
        Me.SwListeOptimieren.FontSize = MetroFramework.MetroLinkSize.Medium
        Me.SwListeOptimieren.Name = "SwListeOptimieren"
        Me.SwListeOptimieren.UseVisualStyleBackColor = True
        '
        'lblListeOptimieren
        '
        resources.ApplyResources(Me.lblListeOptimieren, "lblListeOptimieren")
        Me.lblListeOptimieren.Name = "lblListeOptimieren"
        '
        'SwENummern
        '
        resources.ApplyResources(Me.SwENummern, "SwENummern")
        Me.SwENummern.Checked = True
        Me.SwENummern.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SwENummern.DisplayStatus = False
        Me.SwENummern.FontSize = MetroFramework.MetroLinkSize.Medium
        Me.SwENummern.Name = "SwENummern"
        Me.SwENummern.UseVisualStyleBackColor = True
        '
        'lblENummern
        '
        resources.ApplyResources(Me.lblENummern, "lblENummern")
        Me.lblENummern.Name = "lblENummern"
        '
        'tb_ZutatenListe
        '
        Me.tb_ZutatenListe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.tb_ZutatenListe, "tb_ZutatenListe")
        Me.tb_ZutatenListe.Name = "tb_ZutatenListe"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'tb_Hinweise
        '
        Me.tb_Hinweise.BackColor = System.Drawing.Color.Transparent
        Me.tb_Hinweise.Controls.Add(Me.TextHinweise)
        resources.ApplyResources(Me.tb_Hinweise, "tb_Hinweise")
        Me.tb_Hinweise.Name = "tb_Hinweise"
        '
        'TextHinweise
        '
        Me.TextHinweise.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.TextHinweise, "TextHinweise")
        Me.TextHinweise.Name = "TextHinweise"
        Me.TextHinweise.TabStop = False
        '
        'tb_Verwendung
        '
        Me.tb_Verwendung.Controls.Add(Me.GridView_RzVerwendung)
        resources.ApplyResources(Me.tb_Verwendung, "tb_Verwendung")
        Me.tb_Verwendung.Name = "tb_Verwendung"
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
        resources.ApplyResources(Me.GridView_RzVerwendung, "GridView_RzVerwendung")
        Me.GridView_RzVerwendung.MultiSelect = False
        Me.GridView_RzVerwendung.Name = "GridView_RzVerwendung"
        Me.GridView_RzVerwendung.ReadOnly = True
        Me.GridView_RzVerwendung.x8859_5_FieldName = ""
        '
        'wb_Rezept_Rezeptur
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.gbDetail)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.Wb_TabControl)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "wb_Rezept_Rezeptur"
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
    Friend WithEvents VirtualTree As Infralution.Controls.VirtualTree.VirtualTree
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
    Friend WithEvents CellEditor2 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents UniversalEditBox2 As Infralution.Controls.UniversalEditBox
    Friend WithEvents VTPopUpMenu As Windows.Forms.ContextMenuStrip
    Friend WithEvents CellEditor1 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents EnhEdit1 As EnhEdit.EnhEdit
End Class
