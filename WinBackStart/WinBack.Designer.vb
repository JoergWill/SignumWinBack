<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WinBack
    Inherits System.Windows.Forms.RibbonForm
    Private _FirstRun As Boolean = True

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinBack))
        Me.rbSeparator = New System.Windows.Forms.RibbonSeparator()
        Me.rbChargen = New System.Windows.Forms.RibbonTab()
        Me.rpChargen = New System.Windows.Forms.RibbonPanel()
        Me.rbChargenListe = New System.Windows.Forms.RibbonButton()
        Me.rbChargenDetails = New System.Windows.Forms.RibbonButton()
        Me.rpChargenExport = New System.Windows.Forms.RibbonPanel()
        Me.rbChargenExcelSumme = New System.Windows.Forms.RibbonButton()
        Me.rbChargenExcelEinzel = New System.Windows.Forms.RibbonButton()
        Me.rbChargenExcelDetails = New System.Windows.Forms.RibbonButton()
        Me.rpStatRohStoffe = New System.Windows.Forms.RibbonPanel()
        Me.rbStatRohstoffe = New System.Windows.Forms.RibbonButton()
        Me.rbStatRohstoffeDetail = New System.Windows.Forms.RibbonButton()
        Me.rpStatRezepte = New System.Windows.Forms.RibbonPanel()
        Me.rbStatRezepte = New System.Windows.Forms.RibbonButton()
        Me.rbArtikel = New System.Windows.Forms.RibbonTab()
        Me.rpArtikelBearbeiten = New System.Windows.Forms.RibbonPanel()
        Me.rbArtikelNeu = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelBearbeiten = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelRemove = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelCopy = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelAnsicht = New System.Windows.Forms.RibbonPanel()
        Me.rbArtikelListe = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelDetails = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelSep = New System.Windows.Forms.RibbonSeparator()
        Me.rbArtikelParameter = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelHinweise = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelDeklaration = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelProduktInfo = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelKalkulation = New System.Windows.Forms.RibbonButton()
        Me.rpArtikelDrucken = New System.Windows.Forms.RibbonPanel()
        Me.rbArtikelPrint = New System.Windows.Forms.RibbonButton()
        Me.rbPrintArtikelStammblatt = New System.Windows.Forms.RibbonButton()
        Me.rbPrintArtikelListe = New System.Windows.Forms.RibbonButton()
        Me.rbPrintArtikelProduktInfo = New System.Windows.Forms.RibbonButton()
        Me.rbPrintArtikelProduktInfoMore = New System.Windows.Forms.RibbonButton()
        Me.rbRezepte = New System.Windows.Forms.RibbonTab()
        Me.rpRezeptBearbeiten = New System.Windows.Forms.RibbonPanel()
        Me.rbRezeptNeu = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptBearbeiten = New System.Windows.Forms.RibbonButton()
        Me.rpRezeptAnsicht = New System.Windows.Forms.RibbonPanel()
        Me.rbRezeptListe = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptDetails = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptHinweis = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptVerwendung = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptHistorie = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptSep = New System.Windows.Forms.RibbonSeparator()
        Me.rlArtikel = New System.Windows.Forms.RibbonLabel()
        Me.cbRezeptAnsicht = New System.Windows.Forms.RibbonComboBox()
        Me.rlProduktion = New System.Windows.Forms.RibbonLabel()
        Me.rlSauerteig = New System.Windows.Forms.RibbonLabel()
        Me.rpRezeptDrucken = New System.Windows.Forms.RibbonPanel()
        Me.rbRezeptDrucken = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptDruckenListe = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptDruckenStammblatt = New System.Windows.Forms.RibbonButton()
        Me.rbRohstoffe = New System.Windows.Forms.RibbonTab()
        Me.rpRohstoffeBearbeiten = New System.Windows.Forms.RibbonPanel()
        Me.rbRohstoffeNeu = New System.Windows.Forms.RibbonButton()
        Me.rbRohstoffeBearbeiten = New System.Windows.Forms.RibbonButton()
        Me.rbRohstoffeLöschen = New System.Windows.Forms.RibbonButton()
        Me.rpRohstoffeAnsicht = New System.Windows.Forms.RibbonPanel()
        Me.rbRohstoffeListe = New System.Windows.Forms.RibbonButton()
        Me.rbRohstoffeDetails = New System.Windows.Forms.RibbonButton()
        Me.rbRohstoffeVerwendung = New System.Windows.Forms.RibbonButton()
        Me.rbRohstoffeParameter = New System.Windows.Forms.RibbonButton()
        Me.rbRohstoffNwt = New System.Windows.Forms.RibbonButton()
        Me.rsRohstoffeSep = New System.Windows.Forms.RibbonSeparator()
        Me.rbRohstoffeAnsicht = New System.Windows.Forms.RibbonButton()
        Me.rlRohstoffe = New System.Windows.Forms.RibbonLabel()
        Me.cbRohstoffeAnsicht = New System.Windows.Forms.RibbonComboBox()
        Me.rlRohstoffeAlle = New System.Windows.Forms.RibbonLabel()
        Me.rlRohstoffeHand = New System.Windows.Forms.RibbonLabel()
        Me.rlRohstoffeAuto = New System.Windows.Forms.RibbonLabel()
        Me.rlRohstoffeSauerteig = New System.Windows.Forms.RibbonLabel()
        Me.rlRohstoffeInstall = New System.Windows.Forms.RibbonLabel()
        Me.rpRohstoffeImport = New System.Windows.Forms.RibbonPanel()
        Me.rbRohstoffeImportText = New System.Windows.Forms.RibbonButton()
        Me.rbRohstoffeImportCloud = New System.Windows.Forms.RibbonButton()
        Me.rbRohstoffeDrucken = New System.Windows.Forms.RibbonPanel()
        Me.rbRohstoffeDruck = New System.Windows.Forms.RibbonButton()
        Me.rbUser = New System.Windows.Forms.RibbonTab()
        Me.rpUser = New System.Windows.Forms.RibbonPanel()
        Me.rbUserNeu = New System.Windows.Forms.RibbonButton()
        Me.rbUserBearbeiten = New System.Windows.Forms.RibbonButton()
        Me.rbUserRemove = New System.Windows.Forms.RibbonButton()
        Me.rbUserChangePass = New System.Windows.Forms.RibbonButton()
        Me.rpUserGruppen = New System.Windows.Forms.RibbonPanel()
        Me.rbListe = New System.Windows.Forms.RibbonButton()
        Me.rbUserDetails = New System.Windows.Forms.RibbonButton()
        Me.rbUserRechte = New System.Windows.Forms.RibbonButton()
        Me.rbUserGruppenRechte = New System.Windows.Forms.RibbonButton()
        Me.rpDrucken = New System.Windows.Forms.RibbonPanel()
        Me.rbUserDrucken = New System.Windows.Forms.RibbonButton()
        Me.rbLinien = New System.Windows.Forms.RibbonTab()
        Me.rpLinienBearbeiten = New System.Windows.Forms.RibbonPanel()
        Me.rbLinienAdd = New System.Windows.Forms.RibbonButton()
        Me.rbLinienDel = New System.Windows.Forms.RibbonButton()
        Me.rsLinien = New System.Windows.Forms.RibbonSeparator()
        Me.rbLinienAuto = New System.Windows.Forms.RibbonButton()
        Me.rpLinienDrucken = New System.Windows.Forms.RibbonPanel()
        Me.rbLinienDrucken = New System.Windows.Forms.RibbonButton()
        Me.rbPlanung = New System.Windows.Forms.RibbonTab()
        Me.rpProduktionAnsicht = New System.Windows.Forms.RibbonPanel()
        Me.rbProduktionPlanung = New System.Windows.Forms.RibbonButton()
        Me.rbExtra = New System.Windows.Forms.RibbonTab()
        Me.rbAdminAnsicht = New System.Windows.Forms.RibbonPanel()
        Me.rbLog = New System.Windows.Forms.RibbonButton()
        Me.rbDatensicherung = New System.Windows.Forms.RibbonButton()
        Me.rbAdminWinBackIni = New System.Windows.Forms.RibbonButton()
        Me.rsAdmin = New System.Windows.Forms.RibbonSeparator()
        Me.rbAdminUpdate = New System.Windows.Forms.RibbonButton()
        Me.rbAdminWinBack = New System.Windows.Forms.RibbonButton()
        Me.LanguageFlags = New System.Windows.Forms.ImageList(Me.components)
        Me.rTab = New System.Windows.Forms.Ribbon()
        Me.rbAbmelden = New System.Windows.Forms.RibbonButton()
        Me.rbEnde = New System.Windows.Forms.RibbonButton()
        Me.rbInfo = New System.Windows.Forms.RibbonButton()
        Me.rbDE = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.rbHU = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.rbNL = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.rbEN = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.rbPT = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.rbSL = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.rbRU = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.rbFR = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.rbES = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.rbSK = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.rbRO = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.rbAbout = New System.Windows.Forms.RibbonTab()
        Me.rbAnmelden = New System.Windows.Forms.RibbonPanel()
        Me.rbLogin = New System.Windows.Forms.RibbonButton()
        Me.rbClose = New System.Windows.Forms.RibbonButton()
        Me.rbVersion = New System.Windows.Forms.RibbonPanel()
        Me.rbVersionInfo = New System.Windows.Forms.RibbonButton()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.lblVersion = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblNetworkIP = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblLanguage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.BtnReload = New System.Windows.Forms.Button()
        Me.cbLayouts = New System.Windows.Forms.ComboBox()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnSaveAs = New System.Windows.Forms.Button()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'rbSeparator
        '
        Me.rbSeparator.Name = "rbSeparator"
        '
        'rbChargen
        '
        Me.rbChargen.Name = "rbChargen"
        Me.rbChargen.Panels.Add(Me.rpChargen)
        Me.rbChargen.Panels.Add(Me.rpChargenExport)
        Me.rbChargen.Panels.Add(Me.rpStatRohStoffe)
        Me.rbChargen.Panels.Add(Me.rpStatRezepte)
        Me.rbChargen.Tag = "101"
        Me.rbChargen.Text = "Statistik"
        Me.rbChargen.ToolTip = ""
        '
        'rpChargen
        '
        Me.rpChargen.ButtonMoreEnabled = False
        Me.rpChargen.ButtonMoreVisible = False
        Me.rpChargen.Items.Add(Me.rbChargenListe)
        Me.rpChargen.Items.Add(Me.rbChargenDetails)
        Me.rpChargen.Name = "rpChargen"
        Me.rpChargen.Text = "Chargen"
        '
        'rbChargenListe
        '
        Me.rbChargenListe.Image = CType(resources.GetObject("rbChargenListe.Image"), System.Drawing.Image)
        Me.rbChargenListe.LargeImage = CType(resources.GetObject("rbChargenListe.LargeImage"), System.Drawing.Image)
        Me.rbChargenListe.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbChargenListe.Name = "rbChargenListe"
        Me.rbChargenListe.SmallImage = CType(resources.GetObject("rbChargenListe.SmallImage"), System.Drawing.Image)
        Me.rbChargenListe.Text = "Produktion Tage"
        '
        'rbChargenDetails
        '
        Me.rbChargenDetails.Image = CType(resources.GetObject("rbChargenDetails.Image"), System.Drawing.Image)
        Me.rbChargenDetails.LargeImage = CType(resources.GetObject("rbChargenDetails.LargeImage"), System.Drawing.Image)
        Me.rbChargenDetails.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbChargenDetails.Name = "rbChargenDetails"
        Me.rbChargenDetails.SmallImage = CType(resources.GetObject("rbChargenDetails.SmallImage"), System.Drawing.Image)
        Me.rbChargenDetails.Text = "Chargen Details"
        '
        'rpChargenExport
        '
        Me.rpChargenExport.ButtonMoreVisible = False
        Me.rpChargenExport.Items.Add(Me.rbChargenExcelSumme)
        Me.rpChargenExport.Items.Add(Me.rbChargenExcelEinzel)
        Me.rpChargenExport.Items.Add(Me.rbChargenExcelDetails)
        Me.rpChargenExport.Name = "rpChargenExport"
        Me.rpChargenExport.Text = "Export Chargendaten nach Excel"
        '
        'rbChargenExcelSumme
        '
        Me.rbChargenExcelSumme.Image = CType(resources.GetObject("rbChargenExcelSumme.Image"), System.Drawing.Image)
        Me.rbChargenExcelSumme.LargeImage = CType(resources.GetObject("rbChargenExcelSumme.LargeImage"), System.Drawing.Image)
        Me.rbChargenExcelSumme.MinimumSize = New System.Drawing.Size(60, 0)
        Me.rbChargenExcelSumme.Name = "rbChargenExcelSumme"
        Me.rbChargenExcelSumme.SmallImage = CType(resources.GetObject("rbChargenExcelSumme.SmallImage"), System.Drawing.Image)
        Me.rbChargenExcelSumme.Tag = "125"
        Me.rbChargenExcelSumme.Text = "Summe Chargen"
        '
        'rbChargenExcelEinzel
        '
        Me.rbChargenExcelEinzel.Image = CType(resources.GetObject("rbChargenExcelEinzel.Image"), System.Drawing.Image)
        Me.rbChargenExcelEinzel.LargeImage = CType(resources.GetObject("rbChargenExcelEinzel.LargeImage"), System.Drawing.Image)
        Me.rbChargenExcelEinzel.MinimumSize = New System.Drawing.Size(60, 0)
        Me.rbChargenExcelEinzel.Name = "rbChargenExcelEinzel"
        Me.rbChargenExcelEinzel.SmallImage = CType(resources.GetObject("rbChargenExcelEinzel.SmallImage"), System.Drawing.Image)
        Me.rbChargenExcelEinzel.Tag = "125"
        Me.rbChargenExcelEinzel.Text = "Einzel chargen"
        '
        'rbChargenExcelDetails
        '
        Me.rbChargenExcelDetails.Image = CType(resources.GetObject("rbChargenExcelDetails.Image"), System.Drawing.Image)
        Me.rbChargenExcelDetails.LargeImage = CType(resources.GetObject("rbChargenExcelDetails.LargeImage"), System.Drawing.Image)
        Me.rbChargenExcelDetails.MinimumSize = New System.Drawing.Size(60, 0)
        Me.rbChargenExcelDetails.Name = "rbChargenExcelDetails"
        Me.rbChargenExcelDetails.SmallImage = CType(resources.GetObject("rbChargenExcelDetails.SmallImage"), System.Drawing.Image)
        Me.rbChargenExcelDetails.Tag = "125"
        Me.rbChargenExcelDetails.Text = "Chargen Details"
        '
        'rpStatRohStoffe
        '
        Me.rpStatRohStoffe.ButtonMoreEnabled = False
        Me.rpStatRohStoffe.ButtonMoreVisible = False
        Me.rpStatRohStoffe.Items.Add(Me.rbStatRohstoffe)
        Me.rpStatRohStoffe.Items.Add(Me.rbStatRohstoffeDetail)
        Me.rpStatRohStoffe.Name = "rpStatRohStoffe"
        Me.rpStatRohStoffe.Text = "Statistik Rohstoffe"
        '
        'rbStatRohstoffe
        '
        Me.rbStatRohstoffe.Image = CType(resources.GetObject("rbStatRohstoffe.Image"), System.Drawing.Image)
        Me.rbStatRohstoffe.LargeImage = CType(resources.GetObject("rbStatRohstoffe.LargeImage"), System.Drawing.Image)
        Me.rbStatRohstoffe.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbStatRohstoffe.Name = "rbStatRohstoffe"
        Me.rbStatRohstoffe.SmallImage = CType(resources.GetObject("rbStatRohstoffe.SmallImage"), System.Drawing.Image)
        Me.rbStatRohstoffe.Tag = "122"
        Me.rbStatRohstoffe.Text = "Auswertung schnell"
        '
        'rbStatRohstoffeDetail
        '
        Me.rbStatRohstoffeDetail.Image = CType(resources.GetObject("rbStatRohstoffeDetail.Image"), System.Drawing.Image)
        Me.rbStatRohstoffeDetail.LargeImage = CType(resources.GetObject("rbStatRohstoffeDetail.LargeImage"), System.Drawing.Image)
        Me.rbStatRohstoffeDetail.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbStatRohstoffeDetail.Name = "rbStatRohstoffeDetail"
        Me.rbStatRohstoffeDetail.SmallImage = CType(resources.GetObject("rbStatRohstoffeDetail.SmallImage"), System.Drawing.Image)
        Me.rbStatRohstoffeDetail.Tag = "122"
        Me.rbStatRohstoffeDetail.Text = "Auswertung Detail"
        '
        'rpStatRezepte
        '
        Me.rpStatRezepte.ButtonMoreVisible = False
        Me.rpStatRezepte.Items.Add(Me.rbStatRezepte)
        Me.rpStatRezepte.Name = "rpStatRezepte"
        Me.rpStatRezepte.Text = "Statistik Rezepte"
        '
        'rbStatRezepte
        '
        Me.rbStatRezepte.Image = CType(resources.GetObject("rbStatRezepte.Image"), System.Drawing.Image)
        Me.rbStatRezepte.LargeImage = CType(resources.GetObject("rbStatRezepte.LargeImage"), System.Drawing.Image)
        Me.rbStatRezepte.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbStatRezepte.Name = "rbStatRezepte"
        Me.rbStatRezepte.SmallImage = CType(resources.GetObject("rbStatRezepte.SmallImage"), System.Drawing.Image)
        Me.rbStatRezepte.Tag = "122"
        Me.rbStatRezepte.Text = "Auswertung Rezepte"
        '
        'rbArtikel
        '
        Me.rbArtikel.Name = "rbArtikel"
        Me.rbArtikel.Panels.Add(Me.rpArtikelBearbeiten)
        Me.rbArtikel.Panels.Add(Me.rbArtikelAnsicht)
        Me.rbArtikel.Panels.Add(Me.rpArtikelDrucken)
        Me.rbArtikel.Tag = "102"
        Me.rbArtikel.Text = "Artikel"
        '
        'rpArtikelBearbeiten
        '
        Me.rpArtikelBearbeiten.ButtonMoreVisible = False
        Me.rpArtikelBearbeiten.Items.Add(Me.rbArtikelNeu)
        Me.rpArtikelBearbeiten.Items.Add(Me.rbArtikelBearbeiten)
        Me.rpArtikelBearbeiten.Items.Add(Me.rbArtikelRemove)
        Me.rpArtikelBearbeiten.Items.Add(Me.rbArtikelCopy)
        Me.rpArtikelBearbeiten.Name = "rpArtikelBearbeiten"
        Me.rpArtikelBearbeiten.Text = "Bearbeiten"
        '
        'rbArtikelNeu
        '
        Me.rbArtikelNeu.Image = CType(resources.GetObject("rbArtikelNeu.Image"), System.Drawing.Image)
        Me.rbArtikelNeu.LargeImage = CType(resources.GetObject("rbArtikelNeu.LargeImage"), System.Drawing.Image)
        Me.rbArtikelNeu.MinimumSize = New System.Drawing.Size(65, 0)
        Me.rbArtikelNeu.Name = "rbArtikelNeu"
        Me.rbArtikelNeu.SmallImage = CType(resources.GetObject("rbArtikelNeu.SmallImage"), System.Drawing.Image)
        Me.rbArtikelNeu.Text = "Artikel Neu"
        '
        'rbArtikelBearbeiten
        '
        Me.rbArtikelBearbeiten.Image = Global.WinBack.My.Resources.Resources.ArtikelBearbeiten_32x32
        Me.rbArtikelBearbeiten.LargeImage = Global.WinBack.My.Resources.Resources.ArtikelBearbeiten_32x32
        Me.rbArtikelBearbeiten.MinimumSize = New System.Drawing.Size(65, 0)
        Me.rbArtikelBearbeiten.Name = "rbArtikelBearbeiten"
        Me.rbArtikelBearbeiten.SmallImage = CType(resources.GetObject("rbArtikelBearbeiten.SmallImage"), System.Drawing.Image)
        Me.rbArtikelBearbeiten.Text = "Artikel Bearbeiten"
        '
        'rbArtikelRemove
        '
        Me.rbArtikelRemove.Image = CType(resources.GetObject("rbArtikelRemove.Image"), System.Drawing.Image)
        Me.rbArtikelRemove.LargeImage = CType(resources.GetObject("rbArtikelRemove.LargeImage"), System.Drawing.Image)
        Me.rbArtikelRemove.MinimumSize = New System.Drawing.Size(60, 0)
        Me.rbArtikelRemove.Name = "rbArtikelRemove"
        Me.rbArtikelRemove.SmallImage = CType(resources.GetObject("rbArtikelRemove.SmallImage"), System.Drawing.Image)
        Me.rbArtikelRemove.Text = "Artikel Löschen"
        '
        'rbArtikelCopy
        '
        Me.rbArtikelCopy.Image = CType(resources.GetObject("rbArtikelCopy.Image"), System.Drawing.Image)
        Me.rbArtikelCopy.LargeImage = CType(resources.GetObject("rbArtikelCopy.LargeImage"), System.Drawing.Image)
        Me.rbArtikelCopy.MinimumSize = New System.Drawing.Size(60, 0)
        Me.rbArtikelCopy.Name = "rbArtikelCopy"
        Me.rbArtikelCopy.SmallImage = CType(resources.GetObject("rbArtikelCopy.SmallImage"), System.Drawing.Image)
        Me.rbArtikelCopy.Text = "Artikel Kopieren"
        '
        'rbArtikelAnsicht
        '
        Me.rbArtikelAnsicht.ButtonMoreVisible = False
        Me.rbArtikelAnsicht.Items.Add(Me.rbArtikelListe)
        Me.rbArtikelAnsicht.Items.Add(Me.rbArtikelDetails)
        Me.rbArtikelAnsicht.Items.Add(Me.rbArtikelSep)
        Me.rbArtikelAnsicht.Items.Add(Me.rbArtikelParameter)
        Me.rbArtikelAnsicht.Items.Add(Me.rbArtikelHinweise)
        Me.rbArtikelAnsicht.Items.Add(Me.rbArtikelDeklaration)
        Me.rbArtikelAnsicht.Items.Add(Me.rbArtikelProduktInfo)
        Me.rbArtikelAnsicht.Items.Add(Me.rbArtikelKalkulation)
        Me.rbArtikelAnsicht.Name = "rbArtikelAnsicht"
        Me.rbArtikelAnsicht.Text = "Ansicht"
        '
        'rbArtikelListe
        '
        Me.rbArtikelListe.Image = Global.WinBack.My.Resources.Resources.ArtikelListe_32x32
        Me.rbArtikelListe.LargeImage = Global.WinBack.My.Resources.Resources.ArtikelListe_32x32
        Me.rbArtikelListe.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelListe.Name = "rbArtikelListe"
        Me.rbArtikelListe.SmallImage = CType(resources.GetObject("rbArtikelListe.SmallImage"), System.Drawing.Image)
        Me.rbArtikelListe.Text = "Artikel Liste"
        '
        'rbArtikelDetails
        '
        Me.rbArtikelDetails.Image = Global.WinBack.My.Resources.Resources.ArtikelDetails_32x32
        Me.rbArtikelDetails.LargeImage = Global.WinBack.My.Resources.Resources.ArtikelDetails_32x32
        Me.rbArtikelDetails.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelDetails.Name = "rbArtikelDetails"
        Me.rbArtikelDetails.SmallImage = CType(resources.GetObject("rbArtikelDetails.SmallImage"), System.Drawing.Image)
        Me.rbArtikelDetails.Text = "Detail Fenster"
        Me.rbArtikelDetails.Value = "OPENDETAILS"
        '
        'rbArtikelSep
        '
        Me.rbArtikelSep.Name = "rbArtikelSep"
        '
        'rbArtikelParameter
        '
        Me.rbArtikelParameter.Image = Global.WinBack.My.Resources.Resources.ArtikelParameter_32x32
        Me.rbArtikelParameter.LargeImage = Global.WinBack.My.Resources.Resources.ArtikelParameter_32x32
        Me.rbArtikelParameter.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelParameter.Name = "rbArtikelParameter"
        Me.rbArtikelParameter.SmallImage = CType(resources.GetObject("rbArtikelParameter.SmallImage"), System.Drawing.Image)
        Me.rbArtikelParameter.Text = "Parameter Fenster"
        '
        'rbArtikelHinweise
        '
        Me.rbArtikelHinweise.Image = Global.WinBack.My.Resources.Resources.ArtikelHinweise_32x32
        Me.rbArtikelHinweise.LargeImage = Global.WinBack.My.Resources.Resources.ArtikelHinweise_32x32
        Me.rbArtikelHinweise.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelHinweise.Name = "rbArtikelHinweise"
        Me.rbArtikelHinweise.SmallImage = CType(resources.GetObject("rbArtikelHinweise.SmallImage"), System.Drawing.Image)
        Me.rbArtikelHinweise.Text = "Artikel Hinweise"
        Me.rbArtikelHinweise.Value = "OPENHINWEISE"
        '
        'rbArtikelDeklaration
        '
        Me.rbArtikelDeklaration.Image = Global.WinBack.My.Resources.Resources.ArtikelDeklaration_32x32
        Me.rbArtikelDeklaration.LargeImage = Global.WinBack.My.Resources.Resources.ArtikelDeklaration_32x32
        Me.rbArtikelDeklaration.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelDeklaration.Name = "rbArtikelDeklaration"
        Me.rbArtikelDeklaration.SmallImage = CType(resources.GetObject("rbArtikelDeklaration.SmallImage"), System.Drawing.Image)
        Me.rbArtikelDeklaration.Text = "Artikel Deklaration"
        '
        'rbArtikelProduktInfo
        '
        Me.rbArtikelProduktInfo.Image = CType(resources.GetObject("rbArtikelProduktInfo.Image"), System.Drawing.Image)
        Me.rbArtikelProduktInfo.LargeImage = CType(resources.GetObject("rbArtikelProduktInfo.LargeImage"), System.Drawing.Image)
        Me.rbArtikelProduktInfo.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelProduktInfo.Name = "rbArtikelProduktInfo"
        Me.rbArtikelProduktInfo.SmallImage = CType(resources.GetObject("rbArtikelProduktInfo.SmallImage"), System.Drawing.Image)
        Me.rbArtikelProduktInfo.Text = "Produkt Info"
        '
        'rbArtikelKalkulation
        '
        Me.rbArtikelKalkulation.Image = Global.WinBack.My.Resources.Resources.ArtikelKalkulation_32x32
        Me.rbArtikelKalkulation.LargeImage = Global.WinBack.My.Resources.Resources.ArtikelKalkulation_32x32
        Me.rbArtikelKalkulation.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelKalkulation.Name = "rbArtikelKalkulation"
        Me.rbArtikelKalkulation.SmallImage = CType(resources.GetObject("rbArtikelKalkulation.SmallImage"), System.Drawing.Image)
        Me.rbArtikelKalkulation.Text = "Kalkulation Artikel"
        '
        'rpArtikelDrucken
        '
        Me.rpArtikelDrucken.ButtonMoreVisible = False
        Me.rpArtikelDrucken.Items.Add(Me.rbArtikelPrint)
        Me.rpArtikelDrucken.Name = "rpArtikelDrucken"
        Me.rpArtikelDrucken.Text = "Drucken"
        '
        'rbArtikelPrint
        '
        Me.rbArtikelPrint.DropDownItems.Add(Me.rbPrintArtikelStammblatt)
        Me.rbArtikelPrint.DropDownItems.Add(Me.rbPrintArtikelListe)
        Me.rbArtikelPrint.DropDownItems.Add(Me.rbPrintArtikelProduktInfo)
        Me.rbArtikelPrint.DropDownItems.Add(Me.rbPrintArtikelProduktInfoMore)
        Me.rbArtikelPrint.Image = CType(resources.GetObject("rbArtikelPrint.Image"), System.Drawing.Image)
        Me.rbArtikelPrint.LargeImage = CType(resources.GetObject("rbArtikelPrint.LargeImage"), System.Drawing.Image)
        Me.rbArtikelPrint.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelPrint.Name = "rbArtikelPrint"
        Me.rbArtikelPrint.SmallImage = CType(resources.GetObject("rbArtikelPrint.SmallImage"), System.Drawing.Image)
        Me.rbArtikelPrint.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.rbArtikelPrint.Text = "Artikel drucken"
        '
        'rbPrintArtikelStammblatt
        '
        Me.rbPrintArtikelStammblatt.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbPrintArtikelStammblatt.Image = CType(resources.GetObject("rbPrintArtikelStammblatt.Image"), System.Drawing.Image)
        Me.rbPrintArtikelStammblatt.LargeImage = CType(resources.GetObject("rbPrintArtikelStammblatt.LargeImage"), System.Drawing.Image)
        Me.rbPrintArtikelStammblatt.Name = "rbPrintArtikelStammblatt"
        Me.rbPrintArtikelStammblatt.SmallImage = CType(resources.GetObject("rbPrintArtikelStammblatt.SmallImage"), System.Drawing.Image)
        Me.rbPrintArtikelStammblatt.Text = "Stammdatenblatt"
        '
        'rbPrintArtikelListe
        '
        Me.rbPrintArtikelListe.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbPrintArtikelListe.Image = CType(resources.GetObject("rbPrintArtikelListe.Image"), System.Drawing.Image)
        Me.rbPrintArtikelListe.LargeImage = CType(resources.GetObject("rbPrintArtikelListe.LargeImage"), System.Drawing.Image)
        Me.rbPrintArtikelListe.Name = "rbPrintArtikelListe"
        Me.rbPrintArtikelListe.SmallImage = CType(resources.GetObject("rbPrintArtikelListe.SmallImage"), System.Drawing.Image)
        Me.rbPrintArtikelListe.Text = "Liste aller Artikel"
        '
        'rbPrintArtikelProduktInfo
        '
        Me.rbPrintArtikelProduktInfo.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbPrintArtikelProduktInfo.Image = CType(resources.GetObject("rbPrintArtikelProduktInfo.Image"), System.Drawing.Image)
        Me.rbPrintArtikelProduktInfo.LargeImage = CType(resources.GetObject("rbPrintArtikelProduktInfo.LargeImage"), System.Drawing.Image)
        Me.rbPrintArtikelProduktInfo.Name = "rbPrintArtikelProduktInfo"
        Me.rbPrintArtikelProduktInfo.SmallImage = CType(resources.GetObject("rbPrintArtikelProduktInfo.SmallImage"), System.Drawing.Image)
        Me.rbPrintArtikelProduktInfo.Text = "Produktinformation"
        '
        'rbPrintArtikelProduktInfoMore
        '
        Me.rbPrintArtikelProduktInfoMore.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbPrintArtikelProduktInfoMore.Image = CType(resources.GetObject("rbPrintArtikelProduktInfoMore.Image"), System.Drawing.Image)
        Me.rbPrintArtikelProduktInfoMore.LargeImage = CType(resources.GetObject("rbPrintArtikelProduktInfoMore.LargeImage"), System.Drawing.Image)
        Me.rbPrintArtikelProduktInfoMore.Name = "rbPrintArtikelProduktInfoMore"
        Me.rbPrintArtikelProduktInfoMore.SmallImage = CType(resources.GetObject("rbPrintArtikelProduktInfoMore.SmallImage"), System.Drawing.Image)
        Me.rbPrintArtikelProduktInfoMore.Text = "Produktinfo ..."
        '
        'rbRezepte
        '
        Me.rbRezepte.Name = "rbRezepte"
        Me.rbRezepte.Panels.Add(Me.rpRezeptBearbeiten)
        Me.rbRezepte.Panels.Add(Me.rpRezeptAnsicht)
        Me.rbRezepte.Panels.Add(Me.rpRezeptDrucken)
        Me.rbRezepte.Tag = "103"
        Me.rbRezepte.Text = "Rezepte"
        '
        'rpRezeptBearbeiten
        '
        Me.rpRezeptBearbeiten.ButtonMoreVisible = False
        Me.rpRezeptBearbeiten.Items.Add(Me.rbRezeptNeu)
        Me.rpRezeptBearbeiten.Items.Add(Me.rbRezeptBearbeiten)
        Me.rpRezeptBearbeiten.Name = "rpRezeptBearbeiten"
        Me.rpRezeptBearbeiten.Text = "Rezept"
        '
        'rbRezeptNeu
        '
        Me.rbRezeptNeu.Image = Global.WinBack.My.Resources.Resources.RezeptNeu_32x32
        Me.rbRezeptNeu.LargeImage = Global.WinBack.My.Resources.Resources.RezeptNeu_32x32
        Me.rbRezeptNeu.MinimumSize = New System.Drawing.Size(65, 0)
        Me.rbRezeptNeu.Name = "rbRezeptNeu"
        Me.rbRezeptNeu.SmallImage = CType(resources.GetObject("rbRezeptNeu.SmallImage"), System.Drawing.Image)
        Me.rbRezeptNeu.Text = "Rezept Neu"
        Me.rbRezeptNeu.Value = "NEW"
        '
        'rbRezeptBearbeiten
        '
        Me.rbRezeptBearbeiten.Image = Global.WinBack.My.Resources.Resources.RezeptBearbeiten_32x32
        Me.rbRezeptBearbeiten.LargeImage = Global.WinBack.My.Resources.Resources.RezeptBearbeiten_32x32
        Me.rbRezeptBearbeiten.MinimumSize = New System.Drawing.Size(65, 0)
        Me.rbRezeptBearbeiten.Name = "rbRezeptBearbeiten"
        Me.rbRezeptBearbeiten.SmallImage = CType(resources.GetObject("rbRezeptBearbeiten.SmallImage"), System.Drawing.Image)
        Me.rbRezeptBearbeiten.Text = "Rezept bearbeiten"
        '
        'rpRezeptAnsicht
        '
        Me.rpRezeptAnsicht.ButtonMoreVisible = False
        Me.rpRezeptAnsicht.Image = Global.WinBack.My.Resources.Resources.LinienNeu_32x32
        Me.rpRezeptAnsicht.Items.Add(Me.rbRezeptListe)
        Me.rpRezeptAnsicht.Items.Add(Me.rbRezeptDetails)
        Me.rpRezeptAnsicht.Items.Add(Me.rbRezeptHinweis)
        Me.rpRezeptAnsicht.Items.Add(Me.rbRezeptVerwendung)
        Me.rpRezeptAnsicht.Items.Add(Me.rbRezeptHistorie)
        Me.rpRezeptAnsicht.Items.Add(Me.rbRezeptSep)
        Me.rpRezeptAnsicht.Items.Add(Me.rlArtikel)
        Me.rpRezeptAnsicht.Items.Add(Me.cbRezeptAnsicht)
        Me.rpRezeptAnsicht.Name = "rpRezeptAnsicht"
        Me.rpRezeptAnsicht.Text = "Ansicht"
        '
        'rbRezeptListe
        '
        Me.rbRezeptListe.Image = Global.WinBack.My.Resources.Resources.RezeptListe_32x32
        Me.rbRezeptListe.LargeImage = Global.WinBack.My.Resources.Resources.RezeptListe_32x32
        Me.rbRezeptListe.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptListe.Name = "rbRezeptListe"
        Me.rbRezeptListe.SmallImage = CType(resources.GetObject("rbRezeptListe.SmallImage"), System.Drawing.Image)
        Me.rbRezeptListe.Text = "Rezept Liste"
        '
        'rbRezeptDetails
        '
        Me.rbRezeptDetails.Image = Global.WinBack.My.Resources.Resources.RezeptDetails_32x32
        Me.rbRezeptDetails.LargeImage = Global.WinBack.My.Resources.Resources.RezeptDetails_32x32
        Me.rbRezeptDetails.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptDetails.Name = "rbRezeptDetails"
        Me.rbRezeptDetails.SmallImage = CType(resources.GetObject("rbRezeptDetails.SmallImage"), System.Drawing.Image)
        Me.rbRezeptDetails.Text = "Detail Fenster"
        Me.rbRezeptDetails.Value = "OPENDETAILS"
        '
        'rbRezeptHinweis
        '
        Me.rbRezeptHinweis.Image = Global.WinBack.My.Resources.Resources.RezeptHinweise_32x32
        Me.rbRezeptHinweis.LargeImage = Global.WinBack.My.Resources.Resources.RezeptHinweise_32x32
        Me.rbRezeptHinweis.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptHinweis.Name = "rbRezeptHinweis"
        Me.rbRezeptHinweis.SmallImage = CType(resources.GetObject("rbRezeptHinweis.SmallImage"), System.Drawing.Image)
        Me.rbRezeptHinweis.Text = "Verarbeitungs Hinweise"
        Me.rbRezeptHinweis.Value = "OPENHINWEISE"
        '
        'rbRezeptVerwendung
        '
        Me.rbRezeptVerwendung.Image = Global.WinBack.My.Resources.Resources.RezeptVerwendung_32x32
        Me.rbRezeptVerwendung.LargeImage = Global.WinBack.My.Resources.Resources.RezeptVerwendung_32x32
        Me.rbRezeptVerwendung.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptVerwendung.Name = "rbRezeptVerwendung"
        Me.rbRezeptVerwendung.SmallImage = CType(resources.GetObject("rbRezeptVerwendung.SmallImage"), System.Drawing.Image)
        Me.rbRezeptVerwendung.Text = "Verwendung in Artikel"
        '
        'rbRezeptHistorie
        '
        Me.rbRezeptHistorie.Image = Global.WinBack.My.Resources.Resources.RezeptHistorie_32x32
        Me.rbRezeptHistorie.LargeImage = Global.WinBack.My.Resources.Resources.RezeptHistorie_32x32
        Me.rbRezeptHistorie.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptHistorie.Name = "rbRezeptHistorie"
        Me.rbRezeptHistorie.SmallImage = CType(resources.GetObject("rbRezeptHistorie.SmallImage"), System.Drawing.Image)
        Me.rbRezeptHistorie.Text = "Änderungs Historie"
        Me.rbRezeptHistorie.Value = "OPENHISTORIE"
        '
        'rbRezeptSep
        '
        Me.rbRezeptSep.Name = "rbRezeptSep"
        '
        'rlArtikel
        '
        Me.rlArtikel.Name = "rlArtikel"
        Me.rlArtikel.Text = "Filter"
        '
        'cbRezeptAnsicht
        '
        Me.cbRezeptAnsicht.AllowTextEdit = False
        Me.cbRezeptAnsicht.Checked = True
        Me.cbRezeptAnsicht.DrawIconsBar = False
        Me.cbRezeptAnsicht.DropDownItems.Add(Me.rlProduktion)
        Me.cbRezeptAnsicht.DropDownItems.Add(Me.rlSauerteig)
        Me.cbRezeptAnsicht.Name = "cbRezeptAnsicht"
        Me.cbRezeptAnsicht.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center
        Me.cbRezeptAnsicht.TextBoxText = "Produktion"
        Me.cbRezeptAnsicht.TextBoxWidth = 105
        '
        'rlProduktion
        '
        Me.rlProduktion.Name = "rlProduktion"
        Me.rlProduktion.Text = "Produktion"
        Me.rlProduktion.Value = "1"
        '
        'rlSauerteig
        '
        Me.rlSauerteig.Name = "rlSauerteig"
        Me.rlSauerteig.Text = "Sauerteig"
        Me.rlSauerteig.Value = "2"
        '
        'rpRezeptDrucken
        '
        Me.rpRezeptDrucken.ButtonMoreVisible = False
        Me.rpRezeptDrucken.Items.Add(Me.rbRezeptDrucken)
        Me.rpRezeptDrucken.Name = "rpRezeptDrucken"
        Me.rpRezeptDrucken.Text = "Drucken"
        '
        'rbRezeptDrucken
        '
        Me.rbRezeptDrucken.DropDownItems.Add(Me.rbRezeptDruckenListe)
        Me.rbRezeptDrucken.DropDownItems.Add(Me.rbRezeptDruckenStammblatt)
        Me.rbRezeptDrucken.Image = Global.WinBack.My.Resources.Resources.RezeptDrucken_32x32
        Me.rbRezeptDrucken.LargeImage = Global.WinBack.My.Resources.Resources.RezeptDrucken_32x32
        Me.rbRezeptDrucken.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptDrucken.Name = "rbRezeptDrucken"
        Me.rbRezeptDrucken.SmallImage = CType(resources.GetObject("rbRezeptDrucken.SmallImage"), System.Drawing.Image)
        Me.rbRezeptDrucken.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.rbRezeptDrucken.Text = "Rezept Drucken"
        '
        'rbRezeptDruckenListe
        '
        Me.rbRezeptDruckenListe.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbRezeptDruckenListe.Image = CType(resources.GetObject("rbRezeptDruckenListe.Image"), System.Drawing.Image)
        Me.rbRezeptDruckenListe.LargeImage = CType(resources.GetObject("rbRezeptDruckenListe.LargeImage"), System.Drawing.Image)
        Me.rbRezeptDruckenListe.Name = "rbRezeptDruckenListe"
        Me.rbRezeptDruckenListe.SmallImage = CType(resources.GetObject("rbRezeptDruckenListe.SmallImage"), System.Drawing.Image)
        Me.rbRezeptDruckenListe.Text = "Liste aller Rezepte"
        '
        'rbRezeptDruckenStammblatt
        '
        Me.rbRezeptDruckenStammblatt.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbRezeptDruckenStammblatt.Image = CType(resources.GetObject("rbRezeptDruckenStammblatt.Image"), System.Drawing.Image)
        Me.rbRezeptDruckenStammblatt.LargeImage = CType(resources.GetObject("rbRezeptDruckenStammblatt.LargeImage"), System.Drawing.Image)
        Me.rbRezeptDruckenStammblatt.Name = "rbRezeptDruckenStammblatt"
        Me.rbRezeptDruckenStammblatt.SmallImage = CType(resources.GetObject("rbRezeptDruckenStammblatt.SmallImage"), System.Drawing.Image)
        Me.rbRezeptDruckenStammblatt.Text = "Rezept Stammblatt"
        '
        'rbRohstoffe
        '
        Me.rbRohstoffe.Name = "rbRohstoffe"
        Me.rbRohstoffe.Panels.Add(Me.rpRohstoffeBearbeiten)
        Me.rbRohstoffe.Panels.Add(Me.rpRohstoffeAnsicht)
        Me.rbRohstoffe.Panels.Add(Me.rpRohstoffeImport)
        Me.rbRohstoffe.Panels.Add(Me.rbRohstoffeDrucken)
        Me.rbRohstoffe.Tag = "104"
        Me.rbRohstoffe.Text = "Rohstoffe"
        '
        'rpRohstoffeBearbeiten
        '
        Me.rpRohstoffeBearbeiten.ButtonMoreVisible = False
        Me.rpRohstoffeBearbeiten.Items.Add(Me.rbRohstoffeNeu)
        Me.rpRohstoffeBearbeiten.Items.Add(Me.rbRohstoffeBearbeiten)
        Me.rpRohstoffeBearbeiten.Items.Add(Me.rbRohstoffeLöschen)
        Me.rpRohstoffeBearbeiten.Name = "rpRohstoffeBearbeiten"
        Me.rpRohstoffeBearbeiten.Text = "Rohstoffe"
        '
        'rbRohstoffeNeu
        '
        Me.rbRohstoffeNeu.Image = Global.WinBack.My.Resources.Resources.RohstoffNeu_32x32
        Me.rbRohstoffeNeu.LargeImage = Global.WinBack.My.Resources.Resources.RohstoffNeu_32x32
        Me.rbRohstoffeNeu.MinimumSize = New System.Drawing.Size(65, 0)
        Me.rbRohstoffeNeu.Name = "rbRohstoffeNeu"
        Me.rbRohstoffeNeu.SmallImage = CType(resources.GetObject("rbRohstoffeNeu.SmallImage"), System.Drawing.Image)
        Me.rbRohstoffeNeu.Text = "Rohstoff Neu"
        Me.rbRohstoffeNeu.Value = "NEW"
        '
        'rbRohstoffeBearbeiten
        '
        Me.rbRohstoffeBearbeiten.Image = Global.WinBack.My.Resources.Resources.RohstoffBearbeiten_32x32
        Me.rbRohstoffeBearbeiten.LargeImage = Global.WinBack.My.Resources.Resources.RohstoffBearbeiten_32x32
        Me.rbRohstoffeBearbeiten.MinimumSize = New System.Drawing.Size(65, 0)
        Me.rbRohstoffeBearbeiten.Name = "rbRohstoffeBearbeiten"
        Me.rbRohstoffeBearbeiten.SmallImage = CType(resources.GetObject("rbRohstoffeBearbeiten.SmallImage"), System.Drawing.Image)
        Me.rbRohstoffeBearbeiten.Text = "Rohstoff Bearbeiten"
        '
        'rbRohstoffeLöschen
        '
        Me.rbRohstoffeLöschen.Image = Global.WinBack.My.Resources.Resources.RohstoffLoeschen_32x32
        Me.rbRohstoffeLöschen.LargeImage = Global.WinBack.My.Resources.Resources.RohstoffLoeschen_32x32
        Me.rbRohstoffeLöschen.MinimumSize = New System.Drawing.Size(65, 0)
        Me.rbRohstoffeLöschen.Name = "rbRohstoffeLöschen"
        Me.rbRohstoffeLöschen.SmallImage = CType(resources.GetObject("rbRohstoffeLöschen.SmallImage"), System.Drawing.Image)
        Me.rbRohstoffeLöschen.Text = "Rohstoff(e) Löschen"
        Me.rbRohstoffeLöschen.Value = "DELETE"
        '
        'rpRohstoffeAnsicht
        '
        Me.rpRohstoffeAnsicht.ButtonMoreVisible = False
        Me.rpRohstoffeAnsicht.Items.Add(Me.rbRohstoffeListe)
        Me.rpRohstoffeAnsicht.Items.Add(Me.rbRohstoffeDetails)
        Me.rpRohstoffeAnsicht.Items.Add(Me.rbRohstoffeVerwendung)
        Me.rpRohstoffeAnsicht.Items.Add(Me.rbRohstoffeParameter)
        Me.rpRohstoffeAnsicht.Items.Add(Me.rbRohstoffNwt)
        Me.rpRohstoffeAnsicht.Items.Add(Me.rsRohstoffeSep)
        Me.rpRohstoffeAnsicht.Items.Add(Me.rbRohstoffeAnsicht)
        Me.rpRohstoffeAnsicht.Items.Add(Me.rlRohstoffe)
        Me.rpRohstoffeAnsicht.Items.Add(Me.cbRohstoffeAnsicht)
        Me.rpRohstoffeAnsicht.Name = "rpRohstoffeAnsicht"
        Me.rpRohstoffeAnsicht.Text = "Ansicht"
        '
        'rbRohstoffeListe
        '
        Me.rbRohstoffeListe.Image = Global.WinBack.My.Resources.Resources.RohstoffeListe_32x32
        Me.rbRohstoffeListe.LargeImage = Global.WinBack.My.Resources.Resources.RohstoffeListe_32x32
        Me.rbRohstoffeListe.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRohstoffeListe.Name = "rbRohstoffeListe"
        Me.rbRohstoffeListe.SmallImage = CType(resources.GetObject("rbRohstoffeListe.SmallImage"), System.Drawing.Image)
        Me.rbRohstoffeListe.Text = "Rohstoffe Liste"
        '
        'rbRohstoffeDetails
        '
        Me.rbRohstoffeDetails.Image = Global.WinBack.My.Resources.Resources.RohstoffeDetails_32x32
        Me.rbRohstoffeDetails.LargeImage = Global.WinBack.My.Resources.Resources.RohstoffeDetails_32x32
        Me.rbRohstoffeDetails.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRohstoffeDetails.Name = "rbRohstoffeDetails"
        Me.rbRohstoffeDetails.SmallImage = CType(resources.GetObject("rbRohstoffeDetails.SmallImage"), System.Drawing.Image)
        Me.rbRohstoffeDetails.Text = "Fenster Details"
        Me.rbRohstoffeDetails.Value = "OPENDETAILS"
        '
        'rbRohstoffeVerwendung
        '
        Me.rbRohstoffeVerwendung.Image = Global.WinBack.My.Resources.Resources.RezeptVerwendung_32x32
        Me.rbRohstoffeVerwendung.LargeImage = Global.WinBack.My.Resources.Resources.RezeptVerwendung_32x32
        Me.rbRohstoffeVerwendung.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRohstoffeVerwendung.Name = "rbRohstoffeVerwendung"
        Me.rbRohstoffeVerwendung.SmallImage = CType(resources.GetObject("rbRohstoffeVerwendung.SmallImage"), System.Drawing.Image)
        Me.rbRohstoffeVerwendung.Text = "Verwendung in Rezepten"
        Me.rbRohstoffeVerwendung.Value = "OPENVERWENDUNG"
        '
        'rbRohstoffeParameter
        '
        Me.rbRohstoffeParameter.Image = Global.WinBack.My.Resources.Resources.RohstoffeParameter_32x32
        Me.rbRohstoffeParameter.LargeImage = Global.WinBack.My.Resources.Resources.RohstoffeParameter_32x32
        Me.rbRohstoffeParameter.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRohstoffeParameter.Name = "rbRohstoffeParameter"
        Me.rbRohstoffeParameter.SmallImage = CType(resources.GetObject("rbRohstoffeParameter.SmallImage"), System.Drawing.Image)
        Me.rbRohstoffeParameter.Tag = ""
        Me.rbRohstoffeParameter.Text = "Rohstoff Parameter"
        Me.rbRohstoffeParameter.Value = "OPENPARAMETER"
        '
        'rbRohstoffNwt
        '
        Me.rbRohstoffNwt.Image = Global.WinBack.My.Resources.Resources.RohstoffeNwt_32x32
        Me.rbRohstoffNwt.LargeImage = Global.WinBack.My.Resources.Resources.RohstoffeNwt_32x32
        Me.rbRohstoffNwt.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRohstoffNwt.Name = "rbRohstoffNwt"
        Me.rbRohstoffNwt.SmallImage = CType(resources.GetObject("rbRohstoffNwt.SmallImage"), System.Drawing.Image)
        Me.rbRohstoffNwt.Text = "Rohstoff Nährwerte"
        Me.rbRohstoffNwt.Value = "NWT"
        '
        'rsRohstoffeSep
        '
        Me.rsRohstoffeSep.Name = "rsRohstoffeSep"
        '
        'rbRohstoffeAnsicht
        '
        Me.rbRohstoffeAnsicht.Image = CType(resources.GetObject("rbRohstoffeAnsicht.Image"), System.Drawing.Image)
        Me.rbRohstoffeAnsicht.LargeImage = CType(resources.GetObject("rbRohstoffeAnsicht.LargeImage"), System.Drawing.Image)
        Me.rbRohstoffeAnsicht.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRohstoffeAnsicht.Name = "rbRohstoffeAnsicht"
        Me.rbRohstoffeAnsicht.SmallImage = CType(resources.GetObject("rbRohstoffeAnsicht.SmallImage"), System.Drawing.Image)
        '
        'rlRohstoffe
        '
        Me.rlRohstoffe.Name = "rlRohstoffe"
        Me.rlRohstoffe.Text = "Filter"
        '
        'cbRohstoffeAnsicht
        '
        Me.cbRohstoffeAnsicht.DropDownItems.Add(Me.rlRohstoffeAlle)
        Me.cbRohstoffeAnsicht.DropDownItems.Add(Me.rlRohstoffeHand)
        Me.cbRohstoffeAnsicht.DropDownItems.Add(Me.rlRohstoffeAuto)
        Me.cbRohstoffeAnsicht.DropDownItems.Add(Me.rlRohstoffeSauerteig)
        Me.cbRohstoffeAnsicht.DropDownItems.Add(Me.rlRohstoffeInstall)
        Me.cbRohstoffeAnsicht.Name = "cbRohstoffeAnsicht"
        Me.cbRohstoffeAnsicht.TextBoxText = "Alle"
        '
        'rlRohstoffeAlle
        '
        Me.rlRohstoffeAlle.Name = "rlRohstoffeAlle"
        Me.rlRohstoffeAlle.Text = "Alle"
        Me.rlRohstoffeAlle.Value = "1"
        '
        'rlRohstoffeHand
        '
        Me.rlRohstoffeHand.Name = "rlRohstoffeHand"
        Me.rlRohstoffeHand.Text = "Hand"
        Me.rlRohstoffeHand.Value = "2"
        '
        'rlRohstoffeAuto
        '
        Me.rlRohstoffeAuto.Name = "rlRohstoffeAuto"
        Me.rlRohstoffeAuto.Text = "Auto"
        Me.rlRohstoffeAuto.Value = "3"
        '
        'rlRohstoffeSauerteig
        '
        Me.rlRohstoffeSauerteig.Name = "rlRohstoffeSauerteig"
        Me.rlRohstoffeSauerteig.Text = "Sauerteig"
        Me.rlRohstoffeSauerteig.Value = "4"
        '
        'rlRohstoffeInstall
        '
        Me.rlRohstoffeInstall.Name = "rlRohstoffeInstall"
        Me.rlRohstoffeInstall.Text = "Installation"
        Me.rlRohstoffeInstall.Value = "5"
        '
        'rpRohstoffeImport
        '
        Me.rpRohstoffeImport.ButtonMoreVisible = False
        Me.rpRohstoffeImport.Items.Add(Me.rbRohstoffeImportText)
        Me.rpRohstoffeImport.Items.Add(Me.rbRohstoffeImportCloud)
        Me.rpRohstoffeImport.Name = "rpRohstoffeImport"
        Me.rpRohstoffeImport.Text = "Import"
        '
        'rbRohstoffeImportText
        '
        Me.rbRohstoffeImportText.Image = Global.WinBack.My.Resources.Resources.RohstoffeImport_32x32
        Me.rbRohstoffeImportText.LargeImage = Global.WinBack.My.Resources.Resources.RohstoffeImport_32x32
        Me.rbRohstoffeImportText.MinimumSize = New System.Drawing.Size(80, 0)
        Me.rbRohstoffeImportText.Name = "rbRohstoffeImportText"
        Me.rbRohstoffeImportText.SmallImage = CType(resources.GetObject("rbRohstoffeImportText.SmallImage"), System.Drawing.Image)
        Me.rbRohstoffeImportText.Text = "Import aus Textdatei"
        '
        'rbRohstoffeImportCloud
        '
        Me.rbRohstoffeImportCloud.Image = Global.WinBack.My.Resources.Resources.RohstoffeCloud_32x32
        Me.rbRohstoffeImportCloud.LargeImage = Global.WinBack.My.Resources.Resources.RohstoffeCloud_32x32
        Me.rbRohstoffeImportCloud.MinimumSize = New System.Drawing.Size(80, 0)
        Me.rbRohstoffeImportCloud.Name = "rbRohstoffeImportCloud"
        Me.rbRohstoffeImportCloud.SmallImage = CType(resources.GetObject("rbRohstoffeImportCloud.SmallImage"), System.Drawing.Image)
        Me.rbRohstoffeImportCloud.Text = "Nährwert Cloud"
        Me.rbRohstoffeImportCloud.Value = "CLOUD"
        '
        'rbRohstoffeDrucken
        '
        Me.rbRohstoffeDrucken.ButtonMoreVisible = False
        Me.rbRohstoffeDrucken.Items.Add(Me.rbRohstoffeDruck)
        Me.rbRohstoffeDrucken.Name = "rbRohstoffeDrucken"
        Me.rbRohstoffeDrucken.Text = "Drucken"
        '
        'rbRohstoffeDruck
        '
        Me.rbRohstoffeDruck.Image = Global.WinBack.My.Resources.Resources.RohstoffeDrucken_32x32
        Me.rbRohstoffeDruck.LargeImage = Global.WinBack.My.Resources.Resources.RohstoffeDrucken_32x32
        Me.rbRohstoffeDruck.Name = "rbRohstoffeDruck"
        Me.rbRohstoffeDruck.SmallImage = CType(resources.GetObject("rbRohstoffeDruck.SmallImage"), System.Drawing.Image)
        Me.rbRohstoffeDruck.Text = "Rohstoffe Liste"
        '
        'rbUser
        '
        Me.rbUser.Name = "rbUser"
        Me.rbUser.Panels.Add(Me.rpUser)
        Me.rbUser.Panels.Add(Me.rpUserGruppen)
        Me.rbUser.Panels.Add(Me.rpDrucken)
        Me.rbUser.Tag = "120"
        Me.rbUser.Text = "Mitarbeiter"
        '
        'rpUser
        '
        Me.rpUser.ButtonMoreVisible = False
        Me.rpUser.Items.Add(Me.rbUserNeu)
        Me.rpUser.Items.Add(Me.rbUserBearbeiten)
        Me.rpUser.Items.Add(Me.rbUserRemove)
        Me.rpUser.Items.Add(Me.rbUserChangePass)
        Me.rpUser.Name = "rpUser"
        Me.rpUser.Tag = ""
        Me.rpUser.Text = "Mitarbeiter"
        '
        'rbUserNeu
        '
        Me.rbUserNeu.Image = Global.WinBack.My.Resources.Resources.UserNeu_32x32
        Me.rbUserNeu.LargeImage = Global.WinBack.My.Resources.Resources.UserNeu_32x32
        Me.rbUserNeu.Name = "rbUserNeu"
        Me.rbUserNeu.SmallImage = CType(resources.GetObject("rbUserNeu.SmallImage"), System.Drawing.Image)
        Me.rbUserNeu.Tag = ""
        Me.rbUserNeu.Text = "Neu anlegen"
        Me.rbUserNeu.Value = "NEW"
        '
        'rbUserBearbeiten
        '
        Me.rbUserBearbeiten.Image = Global.WinBack.My.Resources.Resources.UserBearbeiten_32x32
        Me.rbUserBearbeiten.LargeImage = Global.WinBack.My.Resources.Resources.UserBearbeiten_32x32
        Me.rbUserBearbeiten.Name = "rbUserBearbeiten"
        Me.rbUserBearbeiten.SmallImage = CType(resources.GetObject("rbUserBearbeiten.SmallImage"), System.Drawing.Image)
        Me.rbUserBearbeiten.Text = "Daten Bearbeiten"
        '
        'rbUserRemove
        '
        Me.rbUserRemove.Image = Global.WinBack.My.Resources.Resources.UserLoeschen_32x32
        Me.rbUserRemove.LargeImage = Global.WinBack.My.Resources.Resources.UserLoeschen_32x32
        Me.rbUserRemove.Name = "rbUserRemove"
        Me.rbUserRemove.SmallImage = CType(resources.GetObject("rbUserRemove.SmallImage"), System.Drawing.Image)
        Me.rbUserRemove.Text = "Benutzer löschen"
        Me.rbUserRemove.Value = "DELETE"
        '
        'rbUserChangePass
        '
        Me.rbUserChangePass.Image = Global.WinBack.My.Resources.Resources.UserPasswd_32x32
        Me.rbUserChangePass.LargeImage = Global.WinBack.My.Resources.Resources.UserPasswd_32x32
        Me.rbUserChangePass.Name = "rbUserChangePass"
        Me.rbUserChangePass.SmallImage = CType(resources.GetObject("rbUserChangePass.SmallImage"), System.Drawing.Image)
        Me.rbUserChangePass.Text = "Passwort ändern"
        Me.rbUserChangePass.Value = "USER_PASSWD"
        '
        'rpUserGruppen
        '
        Me.rpUserGruppen.ButtonMoreVisible = False
        Me.rpUserGruppen.Items.Add(Me.rbListe)
        Me.rpUserGruppen.Items.Add(Me.rbUserDetails)
        Me.rpUserGruppen.Items.Add(Me.rbUserRechte)
        Me.rpUserGruppen.Items.Add(Me.rbUserGruppenRechte)
        Me.rpUserGruppen.Name = "rpUserGruppen"
        Me.rpUserGruppen.Tag = ""
        Me.rpUserGruppen.Text = "Ansicht"
        '
        'rbListe
        '
        Me.rbListe.Image = Global.WinBack.My.Resources.Resources.User_32x32
        Me.rbListe.LargeImage = Global.WinBack.My.Resources.Resources.User_32x32
        Me.rbListe.Name = "rbListe"
        Me.rbListe.SmallImage = CType(resources.GetObject("rbListe.SmallImage"), System.Drawing.Image)
        Me.rbListe.Text = "Liste aller Mitarbeiter"
        '
        'rbUserDetails
        '
        Me.rbUserDetails.Image = Global.WinBack.My.Resources.Resources.UserDetails_32x32
        Me.rbUserDetails.LargeImage = Global.WinBack.My.Resources.Resources.UserDetails_32x32
        Me.rbUserDetails.Name = "rbUserDetails"
        Me.rbUserDetails.SmallImage = CType(resources.GetObject("rbUserDetails.SmallImage"), System.Drawing.Image)
        Me.rbUserDetails.Text = "Fenster Details"
        Me.rbUserDetails.Value = "OPENDETAILS"
        '
        'rbUserRechte
        '
        Me.rbUserRechte.Image = Global.WinBack.My.Resources.Resources.UserBerechtigungen_32x32
        Me.rbUserRechte.LargeImage = Global.WinBack.My.Resources.Resources.UserBerechtigungen_32x32
        Me.rbUserRechte.Name = "rbUserRechte"
        Me.rbUserRechte.SmallImage = CType(resources.GetObject("rbUserRechte.SmallImage"), System.Drawing.Image)
        Me.rbUserRechte.Text = "Anzeige Berechtigungen"
        Me.rbUserRechte.Value = "OPENPARAMETER"
        '
        'rbUserGruppenRechte
        '
        Me.rbUserGruppenRechte.Image = Global.WinBack.My.Resources.Resources.UserGruppen_32x32
        Me.rbUserGruppenRechte.LargeImage = Global.WinBack.My.Resources.Resources.UserGruppen_32x32
        Me.rbUserGruppenRechte.Name = "rbUserGruppenRechte"
        Me.rbUserGruppenRechte.SmallImage = CType(resources.GetObject("rbUserGruppenRechte.SmallImage"), System.Drawing.Image)
        Me.rbUserGruppenRechte.Text = "Gruppen Rechte"
        '
        'rpDrucken
        '
        Me.rpDrucken.ButtonMoreVisible = False
        Me.rpDrucken.Items.Add(Me.rbUserDrucken)
        Me.rpDrucken.Name = "rpDrucken"
        Me.rpDrucken.Text = "Drucken"
        '
        'rbUserDrucken
        '
        Me.rbUserDrucken.Image = Global.WinBack.My.Resources.Resources.UserListe_32x32
        Me.rbUserDrucken.LargeImage = Global.WinBack.My.Resources.Resources.UserListe_32x32
        Me.rbUserDrucken.Name = "rbUserDrucken"
        Me.rbUserDrucken.SmallImage = CType(resources.GetObject("rbUserDrucken.SmallImage"), System.Drawing.Image)
        Me.rbUserDrucken.Text = "Liste Drucken"
        Me.rbUserDrucken.Value = "USER_DRUCKEN"
        '
        'rbLinien
        '
        Me.rbLinien.Name = "rbLinien"
        Me.rbLinien.Panels.Add(Me.rpLinienBearbeiten)
        Me.rbLinien.Panels.Add(Me.rpLinienDrucken)
        Me.rbLinien.Tag = "121"
        Me.rbLinien.Text = "Linien"
        '
        'rpLinienBearbeiten
        '
        Me.rpLinienBearbeiten.ButtonMoreVisible = False
        Me.rpLinienBearbeiten.Items.Add(Me.rbLinienAdd)
        Me.rpLinienBearbeiten.Items.Add(Me.rbLinienDel)
        Me.rpLinienBearbeiten.Items.Add(Me.rsLinien)
        Me.rpLinienBearbeiten.Items.Add(Me.rbLinienAuto)
        Me.rpLinienBearbeiten.Name = "rpLinienBearbeiten"
        Me.rpLinienBearbeiten.Text = "Linien"
        '
        'rbLinienAdd
        '
        Me.rbLinienAdd.Image = Global.WinBack.My.Resources.Resources.LinienNeu_32x32
        Me.rbLinienAdd.LargeImage = Global.WinBack.My.Resources.Resources.LinienNeu_32x32
        Me.rbLinienAdd.Name = "rbLinienAdd"
        Me.rbLinienAdd.SmallImage = CType(resources.GetObject("rbLinienAdd.SmallImage"), System.Drawing.Image)
        Me.rbLinienAdd.Text = "Neu anlegen"
        Me.rbLinienAdd.Value = "LINIE_NEU"
        '
        'rbLinienDel
        '
        Me.rbLinienDel.Image = Global.WinBack.My.Resources.Resources.LinienLoeschen_32x32
        Me.rbLinienDel.LargeImage = Global.WinBack.My.Resources.Resources.LinienLoeschen_32x32
        Me.rbLinienDel.Name = "rbLinienDel"
        Me.rbLinienDel.SmallImage = CType(resources.GetObject("rbLinienDel.SmallImage"), System.Drawing.Image)
        Me.rbLinienDel.Text = "Linie löschen"
        Me.rbLinienDel.Value = "LINIE_DEL"
        '
        'rsLinien
        '
        Me.rsLinien.Name = "rsLinien"
        '
        'rbLinienAuto
        '
        Me.rbLinienAuto.Image = Global.WinBack.My.Resources.Resources.LinienAutoInstall_32x32
        Me.rbLinienAuto.LargeImage = Global.WinBack.My.Resources.Resources.LinienAutoInstall_32x32
        Me.rbLinienAuto.Name = "rbLinienAuto"
        Me.rbLinienAuto.SmallImage = CType(resources.GetObject("rbLinienAuto.SmallImage"), System.Drawing.Image)
        Me.rbLinienAuto.Text = "Auto Install"
        Me.rbLinienAuto.Value = "LINIE_AUTOINSTALL"
        '
        'rpLinienDrucken
        '
        Me.rpLinienDrucken.ButtonMoreEnabled = False
        Me.rpLinienDrucken.ButtonMoreVisible = False
        Me.rpLinienDrucken.Items.Add(Me.rbLinienDrucken)
        Me.rpLinienDrucken.Name = "rpLinienDrucken"
        Me.rpLinienDrucken.Text = "Drucken"
        '
        'rbLinienDrucken
        '
        Me.rbLinienDrucken.Image = Global.WinBack.My.Resources.Resources.LinienDrucken_32x32
        Me.rbLinienDrucken.LargeImage = Global.WinBack.My.Resources.Resources.LinienDrucken_32x32
        Me.rbLinienDrucken.Name = "rbLinienDrucken"
        Me.rbLinienDrucken.SmallImage = CType(resources.GetObject("rbLinienDrucken.SmallImage"), System.Drawing.Image)
        Me.rbLinienDrucken.Text = "Liste Drucken"
        Me.rbLinienDrucken.Value = "LINIE_DRUCKEN"
        '
        'rbPlanung
        '
        Me.rbPlanung.Name = "rbPlanung"
        Me.rbPlanung.Panels.Add(Me.rpProduktionAnsicht)
        Me.rbPlanung.Tag = "130"
        Me.rbPlanung.Text = "Planung"
        '
        'rpProduktionAnsicht
        '
        Me.rpProduktionAnsicht.ButtonMoreVisible = False
        Me.rpProduktionAnsicht.Items.Add(Me.rbProduktionPlanung)
        Me.rpProduktionAnsicht.Name = "rpProduktionAnsicht"
        Me.rpProduktionAnsicht.Text = "Ansicht"
        '
        'rbProduktionPlanung
        '
        Me.rbProduktionPlanung.Image = Global.WinBack.My.Resources.Resources.PlanungMain_32x32
        Me.rbProduktionPlanung.LargeImage = Global.WinBack.My.Resources.Resources.PlanungMain_32x32
        Me.rbProduktionPlanung.Name = "rbProduktionPlanung"
        Me.rbProduktionPlanung.SmallImage = CType(resources.GetObject("rbProduktionPlanung.SmallImage"), System.Drawing.Image)
        Me.rbProduktionPlanung.Text = "Herstellung Planung"
        '
        'rbExtra
        '
        Me.rbExtra.Name = "rbExtra"
        Me.rbExtra.Panels.Add(Me.rbAdminAnsicht)
        Me.rbExtra.Tag = "105"
        Me.rbExtra.Text = "Administration"
        '
        'rbAdminAnsicht
        '
        Me.rbAdminAnsicht.ButtonMoreVisible = False
        Me.rbAdminAnsicht.Items.Add(Me.rbLog)
        Me.rbAdminAnsicht.Items.Add(Me.rbDatensicherung)
        Me.rbAdminAnsicht.Items.Add(Me.rbAdminWinBackIni)
        Me.rbAdminAnsicht.Items.Add(Me.rsAdmin)
        Me.rbAdminAnsicht.Items.Add(Me.rbAdminUpdate)
        Me.rbAdminAnsicht.Items.Add(Me.rbAdminWinBack)
        Me.rbAdminAnsicht.Name = "rbAdminAnsicht"
        Me.rbAdminAnsicht.Text = "Ansicht"
        '
        'rbLog
        '
        Me.rbLog.Image = Global.WinBack.My.Resources.Resources.AdminLog_32x32
        Me.rbLog.LargeImage = Global.WinBack.My.Resources.Resources.AdminLog_32x32
        Me.rbLog.Name = "rbLog"
        Me.rbLog.SmallImage = CType(resources.GetObject("rbLog.SmallImage"), System.Drawing.Image)
        Me.rbLog.Text = "Log-/Fehler Ausgaben"
        Me.rbLog.Value = "LOGGER"
        '
        'rbDatensicherung
        '
        Me.rbDatensicherung.Image = Global.WinBack.My.Resources.Resources.AdminMain_32x32
        Me.rbDatensicherung.LargeImage = Global.WinBack.My.Resources.Resources.AdminMain_32x32
        Me.rbDatensicherung.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbDatensicherung.Name = "rbDatensicherung"
        Me.rbDatensicherung.SmallImage = CType(resources.GetObject("rbDatensicherung.SmallImage"), System.Drawing.Image)
        Me.rbDatensicherung.Text = "Daten Sicherung"
        '
        'rbAdminWinBackIni
        '
        Me.rbAdminWinBackIni.Image = Global.WinBack.My.Resources.Resources.AdminEditKonfig_32x32
        Me.rbAdminWinBackIni.LargeImage = Global.WinBack.My.Resources.Resources.AdminEditKonfig_32x32
        Me.rbAdminWinBackIni.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbAdminWinBackIni.Name = "rbAdminWinBackIni"
        Me.rbAdminWinBackIni.SmallImage = CType(resources.GetObject("rbAdminWinBackIni.SmallImage"), System.Drawing.Image)
        Me.rbAdminWinBackIni.Text = "Edit Konfiguration"
        Me.rbAdminWinBackIni.Value = "EDITKONFIG"
        '
        'rsAdmin
        '
        Me.rsAdmin.Name = "rsAdmin"
        '
        'rbAdminUpdate
        '
        Me.rbAdminUpdate.Image = Global.WinBack.My.Resources.Resources.AdminUpdateDataBase_32x32
        Me.rbAdminUpdate.LargeImage = Global.WinBack.My.Resources.Resources.AdminUpdateDataBase_32x32
        Me.rbAdminUpdate.MinimumSize = New System.Drawing.Size(90, 0)
        Me.rbAdminUpdate.Name = "rbAdminUpdate"
        Me.rbAdminUpdate.SmallImage = CType(resources.GetObject("rbAdminUpdate.SmallImage"), System.Drawing.Image)
        Me.rbAdminUpdate.Text = "Update Datenbank"
        Me.rbAdminUpdate.Value = "CHECKDATABASEUPDATE"
        '
        'rbAdminWinBack
        '
        Me.rbAdminWinBack.Image = CType(resources.GetObject("rbAdminWinBack.Image"), System.Drawing.Image)
        Me.rbAdminWinBack.LargeImage = CType(resources.GetObject("rbAdminWinBack.LargeImage"), System.Drawing.Image)
        Me.rbAdminWinBack.Name = "rbAdminWinBack"
        Me.rbAdminWinBack.SmallImage = CType(resources.GetObject("rbAdminWinBack.SmallImage"), System.Drawing.Image)
        Me.rbAdminWinBack.Text = "Update Programm"
        Me.rbAdminWinBack.Value = "CHECKWINBACKUPDATE"
        '
        'LanguageFlags
        '
        Me.LanguageFlags.ImageStream = CType(resources.GetObject("LanguageFlags.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.LanguageFlags.TransparentColor = System.Drawing.Color.Transparent
        Me.LanguageFlags.Images.SetKeyName(0, "de.gif")
        Me.LanguageFlags.Images.SetKeyName(1, "hu.gif")
        Me.LanguageFlags.Images.SetKeyName(2, "nl.gif")
        Me.LanguageFlags.Images.SetKeyName(3, "us.gif")
        Me.LanguageFlags.Images.SetKeyName(4, "pt.gif")
        Me.LanguageFlags.Images.SetKeyName(5, "si.gif")
        Me.LanguageFlags.Images.SetKeyName(6, "ru.gif")
        Me.LanguageFlags.Images.SetKeyName(7, "fr.gif")
        Me.LanguageFlags.Images.SetKeyName(8, "es.gif")
        Me.LanguageFlags.Images.SetKeyName(9, "sk.gif")
        Me.LanguageFlags.Images.SetKeyName(10, "ro.gif")
        '
        'rTab
        '
        Me.rTab.AllowDrop = True
        Me.rTab.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.rTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.rTab.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.rTab.Location = New System.Drawing.Point(0, 0)
        Me.rTab.Minimized = False
        Me.rTab.Name = "rTab"
        '
        '
        '
        Me.rTab.OrbDropDown.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.rTab.OrbDropDown.AutoSizeContentButtons = False
        Me.rTab.OrbDropDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.rTab.OrbDropDown.BorderRoundness = 8
        Me.rTab.OrbDropDown.ContentButtonsMinWidth = 120
        Me.rTab.OrbDropDown.ContentRecentItemsMinWidth = 75
        Me.rTab.OrbDropDown.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.rTab.OrbDropDown.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rTab.OrbDropDown.Location = New System.Drawing.Point(0, 0)
        Me.rTab.OrbDropDown.MenuItems.Add(Me.rbAbmelden)
        Me.rTab.OrbDropDown.MenuItems.Add(Me.rbEnde)
        Me.rTab.OrbDropDown.MenuItems.Add(Me.rbSeparator)
        Me.rTab.OrbDropDown.MenuItems.Add(Me.rbInfo)
        Me.rTab.OrbDropDown.Name = ""
        Me.rTab.OrbDropDown.RecentItems.Add(Me.rbDE)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.rbHU)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.rbNL)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.rbEN)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.rbPT)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.rbSL)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.rbRU)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.rbFR)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.rbES)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.rbSK)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.rbRO)
        Me.rTab.OrbDropDown.RecentItemsCaption = "Sprache"
        Me.rTab.OrbDropDown.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rTab.OrbDropDown.Size = New System.Drawing.Size(300, 339)
        Me.rTab.OrbDropDown.TabIndex = 0
        Me.rTab.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013
        Me.rTab.OrbText = "Datei"
        Me.rTab.PanelCaptionHeight = 20
        Me.rTab.RibbonTabFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rTab.Size = New System.Drawing.Size(1024, 163)
        Me.rTab.TabIndex = 6
        Me.rTab.Tabs.Add(Me.rbChargen)
        Me.rTab.Tabs.Add(Me.rbArtikel)
        Me.rTab.Tabs.Add(Me.rbRezepte)
        Me.rTab.Tabs.Add(Me.rbRohstoffe)
        Me.rTab.Tabs.Add(Me.rbUser)
        Me.rTab.Tabs.Add(Me.rbLinien)
        Me.rTab.Tabs.Add(Me.rbPlanung)
        Me.rTab.Tabs.Add(Me.rbExtra)
        Me.rTab.Tabs.Add(Me.rbAbout)
        Me.rTab.TabsMargin = New System.Windows.Forms.Padding(5, 26, 20, 0)
        Me.rTab.TabSpacing = 4
        Me.rTab.Text = "WinBack"
        Me.rTab.ThemeColor = System.Windows.Forms.RibbonTheme.Black
        '
        'rbAbmelden
        '
        Me.rbAbmelden.Image = CType(resources.GetObject("rbAbmelden.Image"), System.Drawing.Image)
        Me.rbAbmelden.LargeImage = CType(resources.GetObject("rbAbmelden.LargeImage"), System.Drawing.Image)
        Me.rbAbmelden.Name = "rbAbmelden"
        Me.rbAbmelden.SmallImage = CType(resources.GetObject("rbAbmelden.SmallImage"), System.Drawing.Image)
        Me.rbAbmelden.Text = "Anmelden"
        '
        'rbEnde
        '
        Me.rbEnde.Image = CType(resources.GetObject("rbEnde.Image"), System.Drawing.Image)
        Me.rbEnde.LargeImage = CType(resources.GetObject("rbEnde.LargeImage"), System.Drawing.Image)
        Me.rbEnde.Name = "rbEnde"
        Me.rbEnde.SmallImage = CType(resources.GetObject("rbEnde.SmallImage"), System.Drawing.Image)
        Me.rbEnde.Text = "Beenden"
        '
        'rbInfo
        '
        Me.rbInfo.Image = CType(resources.GetObject("rbInfo.Image"), System.Drawing.Image)
        Me.rbInfo.LargeImage = CType(resources.GetObject("rbInfo.LargeImage"), System.Drawing.Image)
        Me.rbInfo.Name = "rbInfo"
        Me.rbInfo.SmallImage = CType(resources.GetObject("rbInfo.SmallImage"), System.Drawing.Image)
        Me.rbInfo.Text = "Info"
        '
        'rbDE
        '
        Me.rbDE.Image = CType(resources.GetObject("rbDE.Image"), System.Drawing.Image)
        Me.rbDE.LargeImage = CType(resources.GetObject("rbDE.LargeImage"), System.Drawing.Image)
        Me.rbDE.Name = "rbDE"
        Me.rbDE.SmallImage = CType(resources.GetObject("rbDE.SmallImage"), System.Drawing.Image)
        Me.rbDE.Text = "Deutsch"
        Me.rbDE.ToolTip = "Umschaltung der aktiven Sprache"
        Me.rbDE.ToolTipImage = Global.WinBack.My.Resources.Resources.LangDE
        Me.rbDE.Value = "de-DE"
        '
        'rbHU
        '
        Me.rbHU.Image = CType(resources.GetObject("rbHU.Image"), System.Drawing.Image)
        Me.rbHU.LargeImage = CType(resources.GetObject("rbHU.LargeImage"), System.Drawing.Image)
        Me.rbHU.Name = "rbHU"
        Me.rbHU.SmallImage = CType(resources.GetObject("rbHU.SmallImage"), System.Drawing.Image)
        Me.rbHU.Text = "Ungarisch"
        Me.rbHU.ToolTip = "Umschaltung der aktiven Sprache"
        Me.rbHU.ToolTipImage = Global.WinBack.My.Resources.Resources.LangHU
        Me.rbHU.Value = "hu-HU"
        '
        'rbNL
        '
        Me.rbNL.Image = CType(resources.GetObject("rbNL.Image"), System.Drawing.Image)
        Me.rbNL.LargeImage = CType(resources.GetObject("rbNL.LargeImage"), System.Drawing.Image)
        Me.rbNL.Name = "rbNL"
        Me.rbNL.SmallImage = CType(resources.GetObject("rbNL.SmallImage"), System.Drawing.Image)
        Me.rbNL.Text = "Niederländisch"
        Me.rbNL.ToolTip = "Umschaltung der aktiven Sprache"
        Me.rbNL.ToolTipImage = Global.WinBack.My.Resources.Resources.LangNL
        Me.rbNL.Value = "nl-NL"
        '
        'rbEN
        '
        Me.rbEN.Image = CType(resources.GetObject("rbEN.Image"), System.Drawing.Image)
        Me.rbEN.LargeImage = CType(resources.GetObject("rbEN.LargeImage"), System.Drawing.Image)
        Me.rbEN.Name = "rbEN"
        Me.rbEN.SmallImage = CType(resources.GetObject("rbEN.SmallImage"), System.Drawing.Image)
        Me.rbEN.Text = "Englisch"
        Me.rbEN.ToolTip = "Umschaltung der aktiven Sprache"
        Me.rbEN.ToolTipImage = Global.WinBack.My.Resources.Resources.LangUS
        Me.rbEN.Value = "en-US"
        '
        'rbPT
        '
        Me.rbPT.Image = CType(resources.GetObject("rbPT.Image"), System.Drawing.Image)
        Me.rbPT.LargeImage = CType(resources.GetObject("rbPT.LargeImage"), System.Drawing.Image)
        Me.rbPT.Name = "rbPT"
        Me.rbPT.SmallImage = CType(resources.GetObject("rbPT.SmallImage"), System.Drawing.Image)
        Me.rbPT.Text = "Portugiesisch"
        Me.rbPT.ToolTip = "Umschaltung der aktiven Sprache"
        Me.rbPT.ToolTipImage = Global.WinBack.My.Resources.Resources.LangPT
        Me.rbPT.Value = "pt-PT"
        '
        'rbSL
        '
        Me.rbSL.Image = CType(resources.GetObject("rbSL.Image"), System.Drawing.Image)
        Me.rbSL.LargeImage = CType(resources.GetObject("rbSL.LargeImage"), System.Drawing.Image)
        Me.rbSL.Name = "rbSL"
        Me.rbSL.SmallImage = CType(resources.GetObject("rbSL.SmallImage"), System.Drawing.Image)
        Me.rbSL.Text = "Slovenisch"
        Me.rbSL.ToolTip = "Umschaltung der aktiven Sprache"
        Me.rbSL.ToolTipImage = Global.WinBack.My.Resources.Resources.LangSI
        Me.rbSL.Value = "sl-SL"
        '
        'rbRU
        '
        Me.rbRU.Image = CType(resources.GetObject("rbRU.Image"), System.Drawing.Image)
        Me.rbRU.LargeImage = CType(resources.GetObject("rbRU.LargeImage"), System.Drawing.Image)
        Me.rbRU.Name = "rbRU"
        Me.rbRU.SmallImage = CType(resources.GetObject("rbRU.SmallImage"), System.Drawing.Image)
        Me.rbRU.Text = "Russisch"
        Me.rbRU.ToolTip = "Umschaltung der aktiven Sprache"
        Me.rbRU.ToolTipImage = Global.WinBack.My.Resources.Resources.LangRU
        Me.rbRU.Value = "ru-RU"
        '
        'rbFR
        '
        Me.rbFR.Image = CType(resources.GetObject("rbFR.Image"), System.Drawing.Image)
        Me.rbFR.LargeImage = CType(resources.GetObject("rbFR.LargeImage"), System.Drawing.Image)
        Me.rbFR.Name = "rbFR"
        Me.rbFR.SmallImage = CType(resources.GetObject("rbFR.SmallImage"), System.Drawing.Image)
        Me.rbFR.Text = "Französisch"
        Me.rbFR.ToolTip = "Umschaltung der aktiven Sprache"
        Me.rbFR.ToolTipImage = Global.WinBack.My.Resources.Resources.LangFR
        Me.rbFR.Value = "fr-FR"
        '
        'rbES
        '
        Me.rbES.Image = CType(resources.GetObject("rbES.Image"), System.Drawing.Image)
        Me.rbES.LargeImage = CType(resources.GetObject("rbES.LargeImage"), System.Drawing.Image)
        Me.rbES.Name = "rbES"
        Me.rbES.SmallImage = CType(resources.GetObject("rbES.SmallImage"), System.Drawing.Image)
        Me.rbES.Text = "Spanisch"
        Me.rbES.ToolTip = "Umschaltung der aktiven Sprache"
        Me.rbES.ToolTipImage = Global.WinBack.My.Resources.Resources.LangES
        Me.rbES.Value = "es-ES"
        '
        'rbSK
        '
        Me.rbSK.Image = CType(resources.GetObject("rbSK.Image"), System.Drawing.Image)
        Me.rbSK.LargeImage = CType(resources.GetObject("rbSK.LargeImage"), System.Drawing.Image)
        Me.rbSK.Name = "rbSK"
        Me.rbSK.SmallImage = CType(resources.GetObject("rbSK.SmallImage"), System.Drawing.Image)
        Me.rbSK.Text = "Slovakisch"
        Me.rbSK.ToolTip = "Umschaltung der aktiven Sprache"
        Me.rbSK.ToolTipImage = Global.WinBack.My.Resources.Resources.LangSK
        Me.rbSK.Value = "sk-SK"
        '
        'rbRO
        '
        Me.rbRO.Image = CType(resources.GetObject("rbRO.Image"), System.Drawing.Image)
        Me.rbRO.LargeImage = CType(resources.GetObject("rbRO.LargeImage"), System.Drawing.Image)
        Me.rbRO.Name = "rbRO"
        Me.rbRO.SmallImage = CType(resources.GetObject("rbRO.SmallImage"), System.Drawing.Image)
        Me.rbRO.Text = "Rumänisch"
        Me.rbRO.ToolTip = "Umschaltung der aktiven Sprache"
        Me.rbRO.ToolTipImage = Global.WinBack.My.Resources.Resources.LangRO
        Me.rbRO.Value = "ro_RO"
        '
        'rbAbout
        '
        Me.rbAbout.Name = "rbAbout"
        Me.rbAbout.Panels.Add(Me.rbAnmelden)
        Me.rbAbout.Panels.Add(Me.rbVersion)
        Me.rbAbout.Text = "Info"
        '
        'rbAnmelden
        '
        Me.rbAnmelden.ButtonMoreEnabled = False
        Me.rbAnmelden.ButtonMoreVisible = False
        Me.rbAnmelden.Items.Add(Me.rbLogin)
        Me.rbAnmelden.Items.Add(Me.rbClose)
        Me.rbAnmelden.Name = "rbAnmelden"
        Me.rbAnmelden.Text = "Anmelden"
        '
        'rbLogin
        '
        Me.rbLogin.Image = Global.WinBack.My.Resources.Resources.User_32x32
        Me.rbLogin.LargeImage = Global.WinBack.My.Resources.Resources.User_32x32
        Me.rbLogin.MinimumSize = New System.Drawing.Size(90, 0)
        Me.rbLogin.Name = "rbLogin"
        Me.rbLogin.SmallImage = CType(resources.GetObject("rbLogin.SmallImage"), System.Drawing.Image)
        Me.rbLogin.Text = "Benutzer wechseln"
        Me.rbLogin.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center
        '
        'rbClose
        '
        Me.rbClose.Image = CType(resources.GetObject("rbClose.Image"), System.Drawing.Image)
        Me.rbClose.LargeImage = CType(resources.GetObject("rbClose.LargeImage"), System.Drawing.Image)
        Me.rbClose.MinimumSize = New System.Drawing.Size(90, 0)
        Me.rbClose.Name = "rbClose"
        Me.rbClose.SmallImage = CType(resources.GetObject("rbClose.SmallImage"), System.Drawing.Image)
        Me.rbClose.Text = "WinBack Beenden"
        '
        'rbVersion
        '
        Me.rbVersion.ButtonMoreEnabled = False
        Me.rbVersion.ButtonMoreVisible = False
        Me.rbVersion.Items.Add(Me.rbVersionInfo)
        Me.rbVersion.Name = "rbVersion"
        Me.rbVersion.Text = "Info"
        '
        'rbVersionInfo
        '
        Me.rbVersionInfo.Image = Global.WinBack.My.Resources.Resources.WinBack_32x32
        Me.rbVersionInfo.LargeImage = Global.WinBack.My.Resources.Resources.WinBack_32x32
        Me.rbVersionInfo.MinimumSize = New System.Drawing.Size(90, 0)
        Me.rbVersionInfo.Name = "rbVersionInfo"
        Me.rbVersionInfo.SmallImage = CType(resources.GetObject("rbVersionInfo.SmallImage"), System.Drawing.Image)
        Me.rbVersionInfo.Text = "Programm Version"
        Me.rbVersionInfo.Value = ""
        '
        'StatusStrip
        '
        Me.StatusStrip.AutoSize = False
        Me.StatusStrip.BackgroundImage = Global.WinBack.My.Resources.Resources.StatusStripBackground
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblVersion, Me.lblNetworkIP, Me.lblLanguage})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 733)
        Me.StatusStrip.MinimumSize = New System.Drawing.Size(0, 35)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1024, 35)
        Me.StatusStrip.TabIndex = 4
        Me.StatusStrip.Text = "StatusStrip"
        '
        'lblVersion
        '
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(45, 30)
        Me.lblVersion.Text = "Version"
        '
        'lblNetworkIP
        '
        Me.lblNetworkIP.BackColor = System.Drawing.Color.Transparent
        Me.lblNetworkIP.Name = "lblNetworkIP"
        Me.lblNetworkIP.Size = New System.Drawing.Size(60, 30)
        Me.lblNetworkIP.Text = "DBVersion"
        '
        'lblLanguage
        '
        Me.lblLanguage.BackColor = System.Drawing.Color.Transparent
        Me.lblLanguage.Image = Global.WinBack.My.Resources.Resources.LangDE
        Me.lblLanguage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.lblLanguage.Name = "lblLanguage"
        Me.lblLanguage.Size = New System.Drawing.Size(66, 30)
        Me.lblLanguage.Text = "Deutsch"
        '
        'BtnReload
        '
        Me.BtnReload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnReload.BackgroundImage = Global.WinBack.My.Resources.Resources.StatusStripBackground
        Me.BtnReload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BtnReload.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnReload.FlatAppearance.BorderSize = 0
        Me.BtnReload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver
        Me.BtnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnReload.Image = Global.WinBack.My.Resources.Resources.IconReload_24x24
        Me.BtnReload.Location = New System.Drawing.Point(894, 733)
        Me.BtnReload.Name = "BtnReload"
        Me.BtnReload.Size = New System.Drawing.Size(25, 34)
        Me.BtnReload.TabIndex = 15
        Me.BtnReload.TabStop = False
        Me.BtnReload.UseVisualStyleBackColor = True
        '
        'cbLayouts
        '
        Me.cbLayouts.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbLayouts.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Italic)
        Me.cbLayouts.FormattingEnabled = True
        Me.cbLayouts.ItemHeight = 15
        Me.cbLayouts.Location = New System.Drawing.Point(742, 739)
        Me.cbLayouts.Name = "cbLayouts"
        Me.cbLayouts.Size = New System.Drawing.Size(146, 23)
        Me.cbLayouts.TabIndex = 14
        Me.cbLayouts.TabStop = False
        '
        'BtnDelete
        '
        Me.BtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnDelete.BackgroundImage = Global.WinBack.My.Resources.Resources.StatusStripBackground
        Me.BtnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BtnDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnDelete.FlatAppearance.BorderSize = 0
        Me.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDelete.Image = Global.WinBack.My.Resources.Resources.IconDelete_24x24
        Me.BtnDelete.Location = New System.Drawing.Point(978, 734)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(25, 32)
        Me.BtnDelete.TabIndex = 13
        Me.BtnDelete.TabStop = False
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'BtnSaveAs
        '
        Me.BtnSaveAs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSaveAs.BackgroundImage = Global.WinBack.My.Resources.Resources.StatusStripBackground
        Me.BtnSaveAs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BtnSaveAs.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnSaveAs.FlatAppearance.BorderSize = 0
        Me.BtnSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveAs.Image = Global.WinBack.My.Resources.Resources.IconSaveAs_24x24
        Me.BtnSaveAs.Location = New System.Drawing.Point(950, 734)
        Me.BtnSaveAs.Name = "BtnSaveAs"
        Me.BtnSaveAs.Size = New System.Drawing.Size(25, 33)
        Me.BtnSaveAs.TabIndex = 12
        Me.BtnSaveAs.TabStop = False
        Me.BtnSaveAs.UseVisualStyleBackColor = True
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.BackgroundImage = Global.WinBack.My.Resources.Resources.StatusStripBackground
        Me.BtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BtnSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnSave.FlatAppearance.BorderSize = 0
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.Image = Global.WinBack.My.Resources.Resources.IconSave_24x24
        Me.BtnSave.Location = New System.Drawing.Point(922, 734)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(25, 33)
        Me.BtnSave.TabIndex = 11
        Me.BtnSave.TabStop = False
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'WinBack
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BackgroundImage = Global.WinBack.My.Resources.Resources.WinBackLogo_945x514
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.BtnReload)
        Me.Controls.Add(Me.cbLayouts)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnSaveAs)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.rTab)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "WinBack"
        Me.Text = "WinBack-Office"
        Me.TransparencyKey = System.Drawing.Color.Transparent
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents lblVersion As ToolStripStatusLabel
    Friend WithEvents lblNetworkIP As ToolStripStatusLabel
    Friend WithEvents rTab As Ribbon
    Friend WithEvents rbChargen As RibbonTab
    Friend WithEvents rpChargen As RibbonPanel
    Friend WithEvents rpChargenExport As RibbonPanel
    Friend WithEvents rbArtikel As RibbonTab
    Friend WithEvents rbRezepte As RibbonTab
    Friend WithEvents rbRohstoffe As RibbonTab
    Friend WithEvents rbChargenListe As RibbonButton
    Friend WithEvents rbChargenDetails As RibbonButton
    Friend WithEvents rbChargenExcelSumme As RibbonButton
    Friend WithEvents rbChargenExcelEinzel As RibbonButton
    Friend WithEvents rbChargenExcelDetails As RibbonButton
    Friend WithEvents rpArtikelBearbeiten As RibbonPanel
    Friend WithEvents rbArtikelNeu As RibbonButton
    Friend WithEvents rbArtikelCopy As RibbonButton
    Friend WithEvents rbArtikelBearbeiten As RibbonButton
    Friend WithEvents rpArtikelDrucken As RibbonPanel
    Friend WithEvents rbUser As RibbonTab
    Friend WithEvents rpUser As RibbonPanel
    Friend WithEvents rbUserNeu As RibbonButton
    Friend WithEvents rbUserRemove As RibbonButton
    Friend WithEvents rpUserGruppen As RibbonPanel
    Friend WithEvents rbUserDetails As RibbonButton
    Friend WithEvents rbLinien As RibbonTab
    Friend WithEvents rpLinienBearbeiten As RibbonPanel
    Friend WithEvents rbLinienAdd As RibbonButton
    Friend WithEvents rbLinienDel As RibbonButton
    Friend WithEvents rbLinienAuto As RibbonButton
    Friend WithEvents rsLinien As RibbonSeparator
    Friend WithEvents rbPlanung As RibbonTab
    Friend WithEvents RibbonSeparator4 As RibbonSeparator
    Friend WithEvents rbExtra As RibbonTab
    Friend WithEvents rpStatRohStoffe As RibbonPanel
    Friend WithEvents rbStatRohstoffe As RibbonButton
    Friend WithEvents rbStatRohstoffeDetail As RibbonButton
    Friend WithEvents rpStatRezepte As RibbonPanel
    Friend WithEvents rbStatRezepte As RibbonButton
    Friend WithEvents rbAbmelden As RibbonButton
    Friend WithEvents rbEnde As RibbonButton
    Friend WithEvents rbHU As RibbonOrbRecentItem
    Friend WithEvents rbSeparator As RibbonSeparator
    Friend WithEvents rbInfo As RibbonButton
    Friend WithEvents rbDE As RibbonOrbRecentItem
    Friend WithEvents rbNL As RibbonOrbRecentItem
    Friend WithEvents rbEN As RibbonOrbRecentItem
    Friend WithEvents rbPT As RibbonOrbRecentItem
    Friend WithEvents rbSL As RibbonOrbRecentItem
    Friend WithEvents rbRU As RibbonOrbRecentItem
    Friend WithEvents rbFR As RibbonOrbRecentItem
    Friend WithEvents rbES As RibbonOrbRecentItem
    Friend WithEvents rbSK As RibbonOrbRecentItem
    Friend WithEvents rbRO As RibbonOrbRecentItem
    Friend WithEvents lblLanguage As ToolStripStatusLabel
    Friend WithEvents LanguageFlags As ImageList
    Friend WithEvents RibbonPanel1 As RibbonPanel
    Friend WithEvents rbArtikelAnsicht As RibbonPanel
    Friend WithEvents rbArtikelListe As RibbonButton
    Friend WithEvents rbArtikelDetails As RibbonButton
    Friend WithEvents rbArtikelParameter As RibbonButton
    Friend WithEvents rbArtikelHinweise As RibbonButton
    Friend WithEvents rbArtikelDeklaration As RibbonButton
    Friend WithEvents rbArtikelProduktInfo As RibbonButton
    Friend WithEvents rbArtikelKalkulation As RibbonButton
    Friend WithEvents rbArtikelPrint As RibbonButton
    Private WithEvents rbArtikelRemove As RibbonButton
    Friend WithEvents rbPrintArtikelStammblatt As RibbonButton
    Friend WithEvents rbPrintArtikelProduktInfo As RibbonButton
    Friend WithEvents rbPrintArtikelProduktInfoMore As RibbonButton
    Friend WithEvents rbPrintArtikelListe As RibbonButton
    Friend WithEvents rbArtikelSep As RibbonSeparator
    Friend WithEvents rpRezeptBearbeiten As RibbonPanel
    Friend WithEvents rpRezeptAnsicht As RibbonPanel
    Friend WithEvents rpRezeptDrucken As RibbonPanel
    Friend WithEvents rbRezeptNeu As RibbonButton
    Friend WithEvents rbRezeptBearbeiten As RibbonButton
    Friend WithEvents rbRezeptListe As RibbonButton
    Friend WithEvents rbRezeptDetails As RibbonButton
    Friend WithEvents rbRezeptHinweis As RibbonButton
    Friend WithEvents rbRezeptVerwendung As RibbonButton
    Friend WithEvents rbRezeptHistorie As RibbonButton
    Friend WithEvents rbRezeptDrucken As RibbonButton
    Friend WithEvents rbRezeptSep As RibbonSeparator
    Friend WithEvents cbRezeptAnsicht As RibbonComboBox
    Friend WithEvents rlProduktion As RibbonLabel
    Friend WithEvents rlSauerteig As RibbonLabel
    Friend WithEvents rlArtikel As RibbonLabel
    Friend WithEvents rbRezeptDruckenListe As RibbonButton
    Friend WithEvents rbRezeptDruckenStammblatt As RibbonButton
    Friend WithEvents rpRohstoffeBearbeiten As RibbonPanel
    Friend WithEvents rpRohstoffeAnsicht As RibbonPanel
    Friend WithEvents rbRohstoffeDrucken As RibbonPanel
    Friend WithEvents rbRohstoffeNeu As RibbonButton
    Friend WithEvents rbRohstoffeBearbeiten As RibbonButton
    Friend WithEvents rbRohstoffeLöschen As RibbonButton
    Friend WithEvents rpRohstoffeImport As RibbonPanel
    Friend WithEvents rbRohstoffeListe As RibbonButton
    Friend WithEvents rbRohstoffeDetails As RibbonButton
    Friend WithEvents rbRohstoffeVerwendung As RibbonButton
    Friend WithEvents rbRohstoffeParameter As RibbonButton
    Friend WithEvents rbRohstoffeAnsicht As RibbonButton
    Friend WithEvents rbRohstoffeImportText As RibbonButton
    Friend WithEvents rbRohstoffeImportCloud As RibbonButton
    Friend WithEvents rbRohstoffeDruck As RibbonButton
    Friend WithEvents rsRohstoffeSep As RibbonSeparator
    Friend WithEvents rlRohstoffe As RibbonLabel
    Friend WithEvents cbRohstoffeAnsicht As RibbonComboBox
    Friend WithEvents rlRohstoffeAlle As RibbonLabel
    Friend WithEvents rlRohstoffeHand As RibbonLabel
    Friend WithEvents rlRohstoffeAuto As RibbonLabel
    Friend WithEvents rlRohstoffeSauerteig As RibbonLabel
    Friend WithEvents rlRohstoffeInstall As RibbonLabel
    Friend WithEvents rbUserBearbeiten As RibbonButton
    Friend WithEvents rbUserChangePass As RibbonButton
    Friend WithEvents rbListe As RibbonButton
    Friend WithEvents rbUserRechte As RibbonButton
    Friend WithEvents rbUserGruppenRechte As RibbonButton
    Friend WithEvents rpDrucken As RibbonPanel
    Friend WithEvents rbUserDrucken As RibbonButton
    Friend WithEvents rpLinienDrucken As RibbonPanel
    Friend WithEvents rbLinienDrucken As RibbonButton
    Friend WithEvents rpProduktionAnsicht As RibbonPanel
    Friend WithEvents rbProduktionPlanung As RibbonButton
    Friend WithEvents rbAdminAnsicht As RibbonPanel
    Friend WithEvents rbDatensicherung As RibbonButton
    Friend WithEvents rbAdminWinBackIni As RibbonButton
    Friend WithEvents rsAdmin As RibbonSeparator
    Friend WithEvents rbAdminUpdate As RibbonButton
    Friend WithEvents BtnReload As Button
    Friend WithEvents cbLayouts As ComboBox
    Friend WithEvents BtnDelete As Button
    Friend WithEvents BtnSaveAs As Button
    Friend WithEvents BtnSave As Button
    Friend WithEvents rbAbout As RibbonTab
    Friend WithEvents rbAnmelden As RibbonPanel
    Friend WithEvents rbVersion As RibbonPanel
    Friend WithEvents rbLogin As RibbonButton
    Friend WithEvents rbClose As RibbonButton
    Friend WithEvents rbVersionInfo As RibbonButton
    Friend WithEvents rbRohstoffNwt As RibbonButton
    Friend WithEvents rbAdminWinBack As RibbonButton
    Friend WithEvents rbLog As RibbonButton
End Class
