<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WinBack
    Inherits System.Windows.Forms.RibbonForm

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
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.lblVersion = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblNetworkIP = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblLanguage = New System.Windows.Forms.ToolStripStatusLabel()
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
        Me.rbRohstoffeLieferungen = New System.Windows.Forms.RibbonButton()
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
        Me.RbUserRemove = New System.Windows.Forms.RibbonButton()
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
        Me.rbDatensicherung = New System.Windows.Forms.RibbonButton()
        Me.rbAdminWinBackIni = New System.Windows.Forms.RibbonButton()
        Me.rsAdmin = New System.Windows.Forms.RibbonSeparator()
        Me.rbAdminUpdate = New System.Windows.Forms.RibbonButton()
        Me.LanguageFlags = New System.Windows.Forms.ImageList(Me.components)
        Me.rTab = New System.Windows.Forms.Ribbon()
        Me.rbAbmelden = New System.Windows.Forms.RibbonButton()
        Me.rbEnde = New System.Windows.Forms.RibbonButton()
        Me.rbInfo = New System.Windows.Forms.RibbonButton()
        Me.RibbonOrbRecentItem0 = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.RibbonButton1 = New System.Windows.Forms.RibbonButton()
        Me.RibbonOrbRecentItem1 = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.RibbonOrbRecentItem2 = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.RibbonOrbRecentItem3 = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.RibbonOrbRecentItem4 = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.RibbonOrbRecentItem5 = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.RibbonOrbRecentItem6 = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.RibbonOrbRecentItem7 = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.RibbonOrbRecentItem8 = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.RibbonOrbRecentItem9 = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.RibbonOrbRecentItem10 = New System.Windows.Forms.RibbonOrbRecentItem()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip
        '
        resources.ApplyResources(Me.StatusStrip, "StatusStrip")
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblVersion, Me.lblNetworkIP, Me.lblLanguage})
        Me.StatusStrip.Name = "StatusStrip"
        '
        'lblVersion
        '
        resources.ApplyResources(Me.lblVersion, "lblVersion")
        Me.lblVersion.Name = "lblVersion"
        '
        'lblNetworkIP
        '
        resources.ApplyResources(Me.lblNetworkIP, "lblNetworkIP")
        Me.lblNetworkIP.Name = "lblNetworkIP"
        '
        'lblLanguage
        '
        resources.ApplyResources(Me.lblLanguage, "lblLanguage")
        Me.lblLanguage.Image = Global.WinBack.My.Resources.Resources.LangDE
        Me.lblLanguage.Name = "lblLanguage"
        '
        'rbSeparator
        '
        resources.ApplyResources(Me.rbSeparator, "rbSeparator")
        '
        'rbChargen
        '
        Me.rbChargen.Panels.Add(Me.rpChargen)
        Me.rbChargen.Panels.Add(Me.rpChargenExport)
        Me.rbChargen.Panels.Add(Me.rpStatRohStoffe)
        Me.rbChargen.Panels.Add(Me.rpStatRezepte)
        Me.rbChargen.Tag = "101"
        resources.ApplyResources(Me.rbChargen, "rbChargen")
        '
        'rpChargen
        '
        Me.rpChargen.ButtonMoreEnabled = False
        Me.rpChargen.ButtonMoreVisible = False
        Me.rpChargen.Items.Add(Me.rbChargenListe)
        Me.rpChargen.Items.Add(Me.rbChargenDetails)
        resources.ApplyResources(Me.rpChargen, "rpChargen")
        '
        'rbChargenListe
        '
        Me.rbChargenListe.Image = CType(resources.GetObject("rbChargenListe.Image"), System.Drawing.Image)
        Me.rbChargenListe.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbChargenListe.SmallImage = CType(resources.GetObject("rbChargenListe.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbChargenListe, "rbChargenListe")
        '
        'rbChargenDetails
        '
        Me.rbChargenDetails.Image = CType(resources.GetObject("rbChargenDetails.Image"), System.Drawing.Image)
        Me.rbChargenDetails.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbChargenDetails.SmallImage = CType(resources.GetObject("rbChargenDetails.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbChargenDetails, "rbChargenDetails")
        '
        'rpChargenExport
        '
        Me.rpChargenExport.ButtonMoreVisible = False
        Me.rpChargenExport.Items.Add(Me.rbChargenExcelSumme)
        Me.rpChargenExport.Items.Add(Me.rbChargenExcelEinzel)
        Me.rpChargenExport.Items.Add(Me.rbChargenExcelDetails)
        resources.ApplyResources(Me.rpChargenExport, "rpChargenExport")
        '
        'rbChargenExcelSumme
        '
        Me.rbChargenExcelSumme.Image = CType(resources.GetObject("rbChargenExcelSumme.Image"), System.Drawing.Image)
        Me.rbChargenExcelSumme.MinimumSize = New System.Drawing.Size(60, 0)
        Me.rbChargenExcelSumme.SmallImage = CType(resources.GetObject("rbChargenExcelSumme.SmallImage"), System.Drawing.Image)
        Me.rbChargenExcelSumme.Tag = "125"
        resources.ApplyResources(Me.rbChargenExcelSumme, "rbChargenExcelSumme")
        '
        'rbChargenExcelEinzel
        '
        Me.rbChargenExcelEinzel.Image = CType(resources.GetObject("rbChargenExcelEinzel.Image"), System.Drawing.Image)
        Me.rbChargenExcelEinzel.MinimumSize = New System.Drawing.Size(60, 0)
        Me.rbChargenExcelEinzel.SmallImage = CType(resources.GetObject("rbChargenExcelEinzel.SmallImage"), System.Drawing.Image)
        Me.rbChargenExcelEinzel.Tag = "125"
        resources.ApplyResources(Me.rbChargenExcelEinzel, "rbChargenExcelEinzel")
        '
        'rbChargenExcelDetails
        '
        Me.rbChargenExcelDetails.Image = CType(resources.GetObject("rbChargenExcelDetails.Image"), System.Drawing.Image)
        Me.rbChargenExcelDetails.MinimumSize = New System.Drawing.Size(60, 0)
        Me.rbChargenExcelDetails.SmallImage = CType(resources.GetObject("rbChargenExcelDetails.SmallImage"), System.Drawing.Image)
        Me.rbChargenExcelDetails.Tag = "125"
        resources.ApplyResources(Me.rbChargenExcelDetails, "rbChargenExcelDetails")
        '
        'rpStatRohStoffe
        '
        Me.rpStatRohStoffe.ButtonMoreEnabled = False
        Me.rpStatRohStoffe.ButtonMoreVisible = False
        Me.rpStatRohStoffe.Items.Add(Me.rbStatRohstoffe)
        Me.rpStatRohStoffe.Items.Add(Me.rbStatRohstoffeDetail)
        resources.ApplyResources(Me.rpStatRohStoffe, "rpStatRohStoffe")
        '
        'rbStatRohstoffe
        '
        Me.rbStatRohstoffe.Image = CType(resources.GetObject("rbStatRohstoffe.Image"), System.Drawing.Image)
        Me.rbStatRohstoffe.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbStatRohstoffe.SmallImage = CType(resources.GetObject("rbStatRohstoffe.SmallImage"), System.Drawing.Image)
        Me.rbStatRohstoffe.Tag = "122"
        resources.ApplyResources(Me.rbStatRohstoffe, "rbStatRohstoffe")
        '
        'rbStatRohstoffeDetail
        '
        Me.rbStatRohstoffeDetail.Image = CType(resources.GetObject("rbStatRohstoffeDetail.Image"), System.Drawing.Image)
        Me.rbStatRohstoffeDetail.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbStatRohstoffeDetail.SmallImage = CType(resources.GetObject("rbStatRohstoffeDetail.SmallImage"), System.Drawing.Image)
        Me.rbStatRohstoffeDetail.Tag = "122"
        resources.ApplyResources(Me.rbStatRohstoffeDetail, "rbStatRohstoffeDetail")
        '
        'rpStatRezepte
        '
        Me.rpStatRezepte.ButtonMoreVisible = False
        Me.rpStatRezepte.Items.Add(Me.rbStatRezepte)
        resources.ApplyResources(Me.rpStatRezepte, "rpStatRezepte")
        '
        'rbStatRezepte
        '
        Me.rbStatRezepte.Image = CType(resources.GetObject("rbStatRezepte.Image"), System.Drawing.Image)
        Me.rbStatRezepte.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbStatRezepte.SmallImage = CType(resources.GetObject("rbStatRezepte.SmallImage"), System.Drawing.Image)
        Me.rbStatRezepte.Tag = "122"
        resources.ApplyResources(Me.rbStatRezepte, "rbStatRezepte")
        '
        'rbArtikel
        '
        Me.rbArtikel.Panels.Add(Me.rpArtikelBearbeiten)
        Me.rbArtikel.Panels.Add(Me.rbArtikelAnsicht)
        Me.rbArtikel.Panels.Add(Me.rpArtikelDrucken)
        Me.rbArtikel.Tag = "102"
        resources.ApplyResources(Me.rbArtikel, "rbArtikel")
        '
        'rpArtikelBearbeiten
        '
        Me.rpArtikelBearbeiten.ButtonMoreVisible = False
        Me.rpArtikelBearbeiten.Items.Add(Me.rbArtikelNeu)
        Me.rpArtikelBearbeiten.Items.Add(Me.rbArtikelBearbeiten)
        Me.rpArtikelBearbeiten.Items.Add(Me.rbArtikelRemove)
        Me.rpArtikelBearbeiten.Items.Add(Me.rbArtikelCopy)
        resources.ApplyResources(Me.rpArtikelBearbeiten, "rpArtikelBearbeiten")
        '
        'rbArtikelNeu
        '
        Me.rbArtikelNeu.Image = CType(resources.GetObject("rbArtikelNeu.Image"), System.Drawing.Image)
        Me.rbArtikelNeu.MinimumSize = New System.Drawing.Size(65, 0)
        Me.rbArtikelNeu.SmallImage = CType(resources.GetObject("rbArtikelNeu.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelNeu, "rbArtikelNeu")
        '
        'rbArtikelBearbeiten
        '
        Me.rbArtikelBearbeiten.Image = Global.WinBack.My.Resources.Resources.ArtikelBearbeiten_32x32
        Me.rbArtikelBearbeiten.MinimumSize = New System.Drawing.Size(65, 0)
        Me.rbArtikelBearbeiten.SmallImage = CType(resources.GetObject("rbArtikelBearbeiten.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelBearbeiten, "rbArtikelBearbeiten")
        '
        'rbArtikelRemove
        '
        Me.rbArtikelRemove.Image = CType(resources.GetObject("rbArtikelRemove.Image"), System.Drawing.Image)
        Me.rbArtikelRemove.MinimumSize = New System.Drawing.Size(60, 0)
        Me.rbArtikelRemove.SmallImage = CType(resources.GetObject("rbArtikelRemove.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelRemove, "rbArtikelRemove")
        '
        'rbArtikelCopy
        '
        Me.rbArtikelCopy.Image = CType(resources.GetObject("rbArtikelCopy.Image"), System.Drawing.Image)
        Me.rbArtikelCopy.MinimumSize = New System.Drawing.Size(60, 0)
        Me.rbArtikelCopy.SmallImage = CType(resources.GetObject("rbArtikelCopy.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelCopy, "rbArtikelCopy")
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
        resources.ApplyResources(Me.rbArtikelAnsicht, "rbArtikelAnsicht")
        '
        'rbArtikelListe
        '
        Me.rbArtikelListe.Image = Global.WinBack.My.Resources.Resources.ArtikelListe_32x32
        Me.rbArtikelListe.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelListe.SmallImage = CType(resources.GetObject("rbArtikelListe.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelListe, "rbArtikelListe")
        '
        'rbArtikelDetails
        '
        Me.rbArtikelDetails.Image = Global.WinBack.My.Resources.Resources.ArtikelDetails_32x32
        Me.rbArtikelDetails.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelDetails.SmallImage = CType(resources.GetObject("rbArtikelDetails.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelDetails, "rbArtikelDetails")
        '
        'rbArtikelSep
        '
        resources.ApplyResources(Me.rbArtikelSep, "rbArtikelSep")
        '
        'rbArtikelParameter
        '
        Me.rbArtikelParameter.Image = Global.WinBack.My.Resources.Resources.ArtikelParameter_32x32
        Me.rbArtikelParameter.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelParameter.SmallImage = CType(resources.GetObject("rbArtikelParameter.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelParameter, "rbArtikelParameter")
        '
        'rbArtikelHinweise
        '
        Me.rbArtikelHinweise.Image = Global.WinBack.My.Resources.Resources.ArtikelHinweise_32x32
        Me.rbArtikelHinweise.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelHinweise.SmallImage = CType(resources.GetObject("rbArtikelHinweise.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelHinweise, "rbArtikelHinweise")
        '
        'rbArtikelDeklaration
        '
        Me.rbArtikelDeklaration.Image = Global.WinBack.My.Resources.Resources.ArtikelDeklaration_32x32
        Me.rbArtikelDeklaration.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelDeklaration.SmallImage = CType(resources.GetObject("rbArtikelDeklaration.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelDeklaration, "rbArtikelDeklaration")
        '
        'rbArtikelProduktInfo
        '
        Me.rbArtikelProduktInfo.Image = CType(resources.GetObject("rbArtikelProduktInfo.Image"), System.Drawing.Image)
        Me.rbArtikelProduktInfo.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelProduktInfo.SmallImage = CType(resources.GetObject("rbArtikelProduktInfo.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelProduktInfo, "rbArtikelProduktInfo")
        '
        'rbArtikelKalkulation
        '
        Me.rbArtikelKalkulation.Image = Global.WinBack.My.Resources.Resources.ArtikelKalkulation_32x32
        Me.rbArtikelKalkulation.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelKalkulation.SmallImage = CType(resources.GetObject("rbArtikelKalkulation.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelKalkulation, "rbArtikelKalkulation")
        '
        'rpArtikelDrucken
        '
        Me.rpArtikelDrucken.ButtonMoreVisible = False
        Me.rpArtikelDrucken.Items.Add(Me.rbArtikelPrint)
        resources.ApplyResources(Me.rpArtikelDrucken, "rpArtikelDrucken")
        '
        'rbArtikelPrint
        '
        Me.rbArtikelPrint.DropDownItems.Add(Me.rbPrintArtikelStammblatt)
        Me.rbArtikelPrint.DropDownItems.Add(Me.rbPrintArtikelListe)
        Me.rbArtikelPrint.DropDownItems.Add(Me.rbPrintArtikelProduktInfo)
        Me.rbArtikelPrint.DropDownItems.Add(Me.rbPrintArtikelProduktInfoMore)
        Me.rbArtikelPrint.Image = CType(resources.GetObject("rbArtikelPrint.Image"), System.Drawing.Image)
        Me.rbArtikelPrint.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelPrint.SmallImage = CType(resources.GetObject("rbArtikelPrint.SmallImage"), System.Drawing.Image)
        Me.rbArtikelPrint.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        resources.ApplyResources(Me.rbArtikelPrint, "rbArtikelPrint")
        '
        'rbPrintArtikelStammblatt
        '
        Me.rbPrintArtikelStammblatt.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbPrintArtikelStammblatt.Image = CType(resources.GetObject("rbPrintArtikelStammblatt.Image"), System.Drawing.Image)
        Me.rbPrintArtikelStammblatt.SmallImage = CType(resources.GetObject("rbPrintArtikelStammblatt.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbPrintArtikelStammblatt, "rbPrintArtikelStammblatt")
        '
        'rbPrintArtikelListe
        '
        Me.rbPrintArtikelListe.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbPrintArtikelListe.Image = CType(resources.GetObject("rbPrintArtikelListe.Image"), System.Drawing.Image)
        Me.rbPrintArtikelListe.SmallImage = CType(resources.GetObject("rbPrintArtikelListe.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbPrintArtikelListe, "rbPrintArtikelListe")
        '
        'rbPrintArtikelProduktInfo
        '
        Me.rbPrintArtikelProduktInfo.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbPrintArtikelProduktInfo.Image = CType(resources.GetObject("rbPrintArtikelProduktInfo.Image"), System.Drawing.Image)
        Me.rbPrintArtikelProduktInfo.SmallImage = CType(resources.GetObject("rbPrintArtikelProduktInfo.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbPrintArtikelProduktInfo, "rbPrintArtikelProduktInfo")
        '
        'rbPrintArtikelProduktInfoMore
        '
        Me.rbPrintArtikelProduktInfoMore.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbPrintArtikelProduktInfoMore.Image = CType(resources.GetObject("rbPrintArtikelProduktInfoMore.Image"), System.Drawing.Image)
        Me.rbPrintArtikelProduktInfoMore.SmallImage = CType(resources.GetObject("rbPrintArtikelProduktInfoMore.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbPrintArtikelProduktInfoMore, "rbPrintArtikelProduktInfoMore")
        '
        'rbRezepte
        '
        Me.rbRezepte.Panels.Add(Me.rpRezeptBearbeiten)
        Me.rbRezepte.Panels.Add(Me.rpRezeptAnsicht)
        Me.rbRezepte.Panels.Add(Me.rpRezeptDrucken)
        Me.rbRezepte.Tag = "103"
        resources.ApplyResources(Me.rbRezepte, "rbRezepte")
        '
        'rpRezeptBearbeiten
        '
        Me.rpRezeptBearbeiten.ButtonMoreVisible = False
        Me.rpRezeptBearbeiten.Items.Add(Me.rbRezeptNeu)
        Me.rpRezeptBearbeiten.Items.Add(Me.rbRezeptBearbeiten)
        resources.ApplyResources(Me.rpRezeptBearbeiten, "rpRezeptBearbeiten")
        '
        'rbRezeptNeu
        '
        Me.rbRezeptNeu.Image = Global.WinBack.My.Resources.Resources.RezeptNeu_32x32
        Me.rbRezeptNeu.MinimumSize = New System.Drawing.Size(65, 0)
        Me.rbRezeptNeu.SmallImage = CType(resources.GetObject("rbRezeptNeu.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptNeu, "rbRezeptNeu")
        '
        'rbRezeptBearbeiten
        '
        Me.rbRezeptBearbeiten.Image = Global.WinBack.My.Resources.Resources.RezeptBearbeiten_32x32
        Me.rbRezeptBearbeiten.MinimumSize = New System.Drawing.Size(65, 0)
        Me.rbRezeptBearbeiten.SmallImage = CType(resources.GetObject("rbRezeptBearbeiten.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptBearbeiten, "rbRezeptBearbeiten")
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
        resources.ApplyResources(Me.rpRezeptAnsicht, "rpRezeptAnsicht")
        '
        'rbRezeptListe
        '
        Me.rbRezeptListe.Image = Global.WinBack.My.Resources.Resources.RezeptListe_32x32
        Me.rbRezeptListe.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptListe.SmallImage = CType(resources.GetObject("rbRezeptListe.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptListe, "rbRezeptListe")
        '
        'rbRezeptDetails
        '
        Me.rbRezeptDetails.Image = Global.WinBack.My.Resources.Resources.RezeptDetails_32x32
        Me.rbRezeptDetails.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptDetails.SmallImage = CType(resources.GetObject("rbRezeptDetails.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptDetails, "rbRezeptDetails")
        '
        'rbRezeptHinweis
        '
        Me.rbRezeptHinweis.Image = Global.WinBack.My.Resources.Resources.RezeptHinweise_32x32
        Me.rbRezeptHinweis.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptHinweis.SmallImage = CType(resources.GetObject("rbRezeptHinweis.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptHinweis, "rbRezeptHinweis")
        '
        'rbRezeptVerwendung
        '
        Me.rbRezeptVerwendung.Image = Global.WinBack.My.Resources.Resources.RezeptVerwendung_32x32
        Me.rbRezeptVerwendung.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptVerwendung.SmallImage = CType(resources.GetObject("rbRezeptVerwendung.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptVerwendung, "rbRezeptVerwendung")
        '
        'rbRezeptHistorie
        '
        Me.rbRezeptHistorie.Image = Global.WinBack.My.Resources.Resources.RezeptHistorie_32x32
        Me.rbRezeptHistorie.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptHistorie.SmallImage = CType(resources.GetObject("rbRezeptHistorie.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptHistorie, "rbRezeptHistorie")
        '
        'rbRezeptSep
        '
        resources.ApplyResources(Me.rbRezeptSep, "rbRezeptSep")
        '
        'rlArtikel
        '
        resources.ApplyResources(Me.rlArtikel, "rlArtikel")
        '
        'cbRezeptAnsicht
        '
        Me.cbRezeptAnsicht.AllowTextEdit = False
        Me.cbRezeptAnsicht.Checked = True
        Me.cbRezeptAnsicht.DrawIconsBar = False
        Me.cbRezeptAnsicht.DropDownItems.Add(Me.rlProduktion)
        Me.cbRezeptAnsicht.DropDownItems.Add(Me.rlSauerteig)
        resources.ApplyResources(Me.cbRezeptAnsicht, "cbRezeptAnsicht")
        Me.cbRezeptAnsicht.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center
        Me.cbRezeptAnsicht.TextBoxText = "Produktion"
        Me.cbRezeptAnsicht.TextBoxWidth = 105
        '
        'rlProduktion
        '
        resources.ApplyResources(Me.rlProduktion, "rlProduktion")
        Me.rlProduktion.Value = "1"
        '
        'rlSauerteig
        '
        resources.ApplyResources(Me.rlSauerteig, "rlSauerteig")
        Me.rlSauerteig.Value = "2"
        '
        'rpRezeptDrucken
        '
        Me.rpRezeptDrucken.ButtonMoreVisible = False
        Me.rpRezeptDrucken.Items.Add(Me.rbRezeptDrucken)
        resources.ApplyResources(Me.rpRezeptDrucken, "rpRezeptDrucken")
        '
        'rbRezeptDrucken
        '
        Me.rbRezeptDrucken.DropDownItems.Add(Me.rbRezeptDruckenListe)
        Me.rbRezeptDrucken.DropDownItems.Add(Me.rbRezeptDruckenStammblatt)
        Me.rbRezeptDrucken.Image = Global.WinBack.My.Resources.Resources.RezeptDrucken_32x32
        Me.rbRezeptDrucken.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptDrucken.SmallImage = CType(resources.GetObject("rbRezeptDrucken.SmallImage"), System.Drawing.Image)
        Me.rbRezeptDrucken.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        resources.ApplyResources(Me.rbRezeptDrucken, "rbRezeptDrucken")
        '
        'rbRezeptDruckenListe
        '
        Me.rbRezeptDruckenListe.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbRezeptDruckenListe.Image = CType(resources.GetObject("rbRezeptDruckenListe.Image"), System.Drawing.Image)
        Me.rbRezeptDruckenListe.SmallImage = CType(resources.GetObject("rbRezeptDruckenListe.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptDruckenListe, "rbRezeptDruckenListe")
        '
        'rbRezeptDruckenStammblatt
        '
        Me.rbRezeptDruckenStammblatt.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbRezeptDruckenStammblatt.Image = CType(resources.GetObject("rbRezeptDruckenStammblatt.Image"), System.Drawing.Image)
        Me.rbRezeptDruckenStammblatt.SmallImage = CType(resources.GetObject("rbRezeptDruckenStammblatt.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptDruckenStammblatt, "rbRezeptDruckenStammblatt")
        '
        'rbRohstoffe
        '
        Me.rbRohstoffe.Panels.Add(Me.rpRohstoffeBearbeiten)
        Me.rbRohstoffe.Panels.Add(Me.rpRohstoffeAnsicht)
        Me.rbRohstoffe.Panels.Add(Me.rpRohstoffeImport)
        Me.rbRohstoffe.Panels.Add(Me.rbRohstoffeDrucken)
        Me.rbRohstoffe.Tag = "104"
        resources.ApplyResources(Me.rbRohstoffe, "rbRohstoffe")
        '
        'rpRohstoffeBearbeiten
        '
        Me.rpRohstoffeBearbeiten.ButtonMoreVisible = False
        Me.rpRohstoffeBearbeiten.Items.Add(Me.rbRohstoffeNeu)
        Me.rpRohstoffeBearbeiten.Items.Add(Me.rbRohstoffeBearbeiten)
        Me.rpRohstoffeBearbeiten.Items.Add(Me.rbRohstoffeLöschen)
        resources.ApplyResources(Me.rpRohstoffeBearbeiten, "rpRohstoffeBearbeiten")
        '
        'rbRohstoffeNeu
        '
        Me.rbRohstoffeNeu.Image = Global.WinBack.My.Resources.Resources.RohstoffNeu_32x32
        Me.rbRohstoffeNeu.MinimumSize = New System.Drawing.Size(65, 0)
        Me.rbRohstoffeNeu.SmallImage = CType(resources.GetObject("rbRohstoffeNeu.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRohstoffeNeu, "rbRohstoffeNeu")
        '
        'rbRohstoffeBearbeiten
        '
        Me.rbRohstoffeBearbeiten.Image = Global.WinBack.My.Resources.Resources.RohstoffBearbeiten_32x32
        Me.rbRohstoffeBearbeiten.MinimumSize = New System.Drawing.Size(65, 0)
        Me.rbRohstoffeBearbeiten.SmallImage = CType(resources.GetObject("rbRohstoffeBearbeiten.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRohstoffeBearbeiten, "rbRohstoffeBearbeiten")
        '
        'rbRohstoffeLöschen
        '
        Me.rbRohstoffeLöschen.Image = Global.WinBack.My.Resources.Resources.RohstoffLoeschen_32x32
        Me.rbRohstoffeLöschen.MinimumSize = New System.Drawing.Size(65, 0)
        Me.rbRohstoffeLöschen.SmallImage = CType(resources.GetObject("rbRohstoffeLöschen.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRohstoffeLöschen, "rbRohstoffeLöschen")
        '
        'rpRohstoffeAnsicht
        '
        Me.rpRohstoffeAnsicht.ButtonMoreVisible = False
        Me.rpRohstoffeAnsicht.Items.Add(Me.rbRohstoffeListe)
        Me.rpRohstoffeAnsicht.Items.Add(Me.rbRohstoffeDetails)
        Me.rpRohstoffeAnsicht.Items.Add(Me.rbRohstoffeVerwendung)
        Me.rpRohstoffeAnsicht.Items.Add(Me.rbRohstoffeLieferungen)
        Me.rpRohstoffeAnsicht.Items.Add(Me.rsRohstoffeSep)
        Me.rpRohstoffeAnsicht.Items.Add(Me.rbRohstoffeAnsicht)
        Me.rpRohstoffeAnsicht.Items.Add(Me.rlRohstoffe)
        Me.rpRohstoffeAnsicht.Items.Add(Me.cbRohstoffeAnsicht)
        resources.ApplyResources(Me.rpRohstoffeAnsicht, "rpRohstoffeAnsicht")
        '
        'rbRohstoffeListe
        '
        Me.rbRohstoffeListe.Image = Global.WinBack.My.Resources.Resources.RohstoffeListe_32x32
        Me.rbRohstoffeListe.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRohstoffeListe.SmallImage = CType(resources.GetObject("rbRohstoffeListe.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRohstoffeListe, "rbRohstoffeListe")
        '
        'rbRohstoffeDetails
        '
        Me.rbRohstoffeDetails.Image = Global.WinBack.My.Resources.Resources.RohstoffeDetails_32x32
        Me.rbRohstoffeDetails.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRohstoffeDetails.SmallImage = CType(resources.GetObject("rbRohstoffeDetails.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRohstoffeDetails, "rbRohstoffeDetails")
        '
        'rbRohstoffeVerwendung
        '
        Me.rbRohstoffeVerwendung.Image = Global.WinBack.My.Resources.Resources.RezeptVerwendung_32x32
        Me.rbRohstoffeVerwendung.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRohstoffeVerwendung.SmallImage = CType(resources.GetObject("rbRohstoffeVerwendung.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRohstoffeVerwendung, "rbRohstoffeVerwendung")
        '
        'rbRohstoffeLieferungen
        '
        Me.rbRohstoffeLieferungen.Image = Global.WinBack.My.Resources.Resources.RohstoffeLieferung_32x32
        Me.rbRohstoffeLieferungen.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRohstoffeLieferungen.SmallImage = CType(resources.GetObject("rbRohstoffeLieferungen.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRohstoffeLieferungen, "rbRohstoffeLieferungen")
        '
        'rsRohstoffeSep
        '
        resources.ApplyResources(Me.rsRohstoffeSep, "rsRohstoffeSep")
        '
        'rbRohstoffeAnsicht
        '
        Me.rbRohstoffeAnsicht.Image = CType(resources.GetObject("rbRohstoffeAnsicht.Image"), System.Drawing.Image)
        Me.rbRohstoffeAnsicht.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRohstoffeAnsicht.SmallImage = CType(resources.GetObject("rbRohstoffeAnsicht.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRohstoffeAnsicht, "rbRohstoffeAnsicht")
        '
        'rlRohstoffe
        '
        resources.ApplyResources(Me.rlRohstoffe, "rlRohstoffe")
        '
        'cbRohstoffeAnsicht
        '
        Me.cbRohstoffeAnsicht.DropDownItems.Add(Me.rlRohstoffeAlle)
        Me.cbRohstoffeAnsicht.DropDownItems.Add(Me.rlRohstoffeHand)
        Me.cbRohstoffeAnsicht.DropDownItems.Add(Me.rlRohstoffeAuto)
        Me.cbRohstoffeAnsicht.DropDownItems.Add(Me.rlRohstoffeSauerteig)
        Me.cbRohstoffeAnsicht.DropDownItems.Add(Me.rlRohstoffeInstall)
        resources.ApplyResources(Me.cbRohstoffeAnsicht, "cbRohstoffeAnsicht")
        Me.cbRohstoffeAnsicht.TextBoxText = "Alle"
        '
        'rlRohstoffeAlle
        '
        resources.ApplyResources(Me.rlRohstoffeAlle, "rlRohstoffeAlle")
        Me.rlRohstoffeAlle.Value = "1"
        '
        'rlRohstoffeHand
        '
        resources.ApplyResources(Me.rlRohstoffeHand, "rlRohstoffeHand")
        Me.rlRohstoffeHand.Value = "2"
        '
        'rlRohstoffeAuto
        '
        resources.ApplyResources(Me.rlRohstoffeAuto, "rlRohstoffeAuto")
        Me.rlRohstoffeAuto.Value = "3"
        '
        'rlRohstoffeSauerteig
        '
        resources.ApplyResources(Me.rlRohstoffeSauerteig, "rlRohstoffeSauerteig")
        Me.rlRohstoffeSauerteig.Value = "4"
        '
        'rlRohstoffeInstall
        '
        resources.ApplyResources(Me.rlRohstoffeInstall, "rlRohstoffeInstall")
        Me.rlRohstoffeInstall.Value = "5"
        '
        'rpRohstoffeImport
        '
        Me.rpRohstoffeImport.ButtonMoreVisible = False
        Me.rpRohstoffeImport.Items.Add(Me.rbRohstoffeImportText)
        Me.rpRohstoffeImport.Items.Add(Me.rbRohstoffeImportCloud)
        resources.ApplyResources(Me.rpRohstoffeImport, "rpRohstoffeImport")
        '
        'rbRohstoffeImportText
        '
        Me.rbRohstoffeImportText.Image = Global.WinBack.My.Resources.Resources.RohstoffeImport_32x32
        Me.rbRohstoffeImportText.MinimumSize = New System.Drawing.Size(80, 0)
        Me.rbRohstoffeImportText.SmallImage = CType(resources.GetObject("rbRohstoffeImportText.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRohstoffeImportText, "rbRohstoffeImportText")
        '
        'rbRohstoffeImportCloud
        '
        Me.rbRohstoffeImportCloud.Image = Global.WinBack.My.Resources.Resources.RohstoffeCloud_32x32
        Me.rbRohstoffeImportCloud.MinimumSize = New System.Drawing.Size(80, 0)
        Me.rbRohstoffeImportCloud.SmallImage = CType(resources.GetObject("rbRohstoffeImportCloud.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRohstoffeImportCloud, "rbRohstoffeImportCloud")
        '
        'rbRohstoffeDrucken
        '
        Me.rbRohstoffeDrucken.ButtonMoreVisible = False
        Me.rbRohstoffeDrucken.Items.Add(Me.rbRohstoffeDruck)
        resources.ApplyResources(Me.rbRohstoffeDrucken, "rbRohstoffeDrucken")
        '
        'rbRohstoffeDruck
        '
        Me.rbRohstoffeDruck.Image = Global.WinBack.My.Resources.Resources.RohstoffeDrucken_32x32
        Me.rbRohstoffeDruck.SmallImage = CType(resources.GetObject("rbRohstoffeDruck.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRohstoffeDruck, "rbRohstoffeDruck")
        '
        'rbUser
        '
        Me.rbUser.Panels.Add(Me.rpUser)
        Me.rbUser.Panels.Add(Me.rpUserGruppen)
        Me.rbUser.Panels.Add(Me.rpDrucken)
        Me.rbUser.Tag = "120"
        resources.ApplyResources(Me.rbUser, "rbUser")
        '
        'rpUser
        '
        Me.rpUser.ButtonMoreVisible = False
        Me.rpUser.Items.Add(Me.rbUserNeu)
        Me.rpUser.Items.Add(Me.rbUserBearbeiten)
        Me.rpUser.Items.Add(Me.RbUserRemove)
        Me.rpUser.Items.Add(Me.rbUserChangePass)
        Me.rpUser.Tag = ""
        resources.ApplyResources(Me.rpUser, "rpUser")
        '
        'rbUserNeu
        '
        Me.rbUserNeu.Image = Global.WinBack.My.Resources.Resources.UserNeu_32x32
        Me.rbUserNeu.SmallImage = CType(resources.GetObject("rbUserNeu.SmallImage"), System.Drawing.Image)
        Me.rbUserNeu.Tag = ""
        resources.ApplyResources(Me.rbUserNeu, "rbUserNeu")
        '
        'rbUserBearbeiten
        '
        Me.rbUserBearbeiten.Image = Global.WinBack.My.Resources.Resources.UserBearbeiten_32x32
        Me.rbUserBearbeiten.SmallImage = CType(resources.GetObject("rbUserBearbeiten.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbUserBearbeiten, "rbUserBearbeiten")
        '
        'RbUserRemove
        '
        Me.RbUserRemove.Image = Global.WinBack.My.Resources.Resources.UserLoeschen_32x32
        Me.RbUserRemove.SmallImage = CType(resources.GetObject("RbUserRemove.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RbUserRemove, "RbUserRemove")
        '
        'rbUserChangePass
        '
        Me.rbUserChangePass.Image = Global.WinBack.My.Resources.Resources.UserPasswd_32x32
        Me.rbUserChangePass.SmallImage = CType(resources.GetObject("rbUserChangePass.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbUserChangePass, "rbUserChangePass")
        '
        'rpUserGruppen
        '
        Me.rpUserGruppen.ButtonMoreVisible = False
        Me.rpUserGruppen.Items.Add(Me.rbListe)
        Me.rpUserGruppen.Items.Add(Me.rbUserDetails)
        Me.rpUserGruppen.Items.Add(Me.rbUserRechte)
        Me.rpUserGruppen.Items.Add(Me.rbUserGruppenRechte)
        Me.rpUserGruppen.Tag = ""
        resources.ApplyResources(Me.rpUserGruppen, "rpUserGruppen")
        '
        'rbListe
        '
        Me.rbListe.Image = Global.WinBack.My.Resources.Resources.User_32x32
        Me.rbListe.SmallImage = CType(resources.GetObject("rbListe.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbListe, "rbListe")
        '
        'rbUserDetails
        '
        Me.rbUserDetails.Image = Global.WinBack.My.Resources.Resources.UserDetails_32x32
        Me.rbUserDetails.SmallImage = CType(resources.GetObject("rbUserDetails.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbUserDetails, "rbUserDetails")
        '
        'rbUserRechte
        '
        Me.rbUserRechte.Image = Global.WinBack.My.Resources.Resources.UserBerechtigungen_32x32
        Me.rbUserRechte.SmallImage = CType(resources.GetObject("rbUserRechte.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbUserRechte, "rbUserRechte")
        '
        'rbUserGruppenRechte
        '
        Me.rbUserGruppenRechte.Image = Global.WinBack.My.Resources.Resources.UserGruppen_32x32
        Me.rbUserGruppenRechte.SmallImage = CType(resources.GetObject("rbUserGruppenRechte.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbUserGruppenRechte, "rbUserGruppenRechte")
        '
        'rpDrucken
        '
        Me.rpDrucken.ButtonMoreVisible = False
        Me.rpDrucken.Items.Add(Me.rbUserDrucken)
        resources.ApplyResources(Me.rpDrucken, "rpDrucken")
        '
        'rbUserDrucken
        '
        Me.rbUserDrucken.Image = Global.WinBack.My.Resources.Resources.UserListe_32x32
        Me.rbUserDrucken.SmallImage = CType(resources.GetObject("rbUserDrucken.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbUserDrucken, "rbUserDrucken")
        '
        'rbLinien
        '
        Me.rbLinien.Panels.Add(Me.rpLinienBearbeiten)
        Me.rbLinien.Panels.Add(Me.rpLinienDrucken)
        Me.rbLinien.Tag = "121"
        resources.ApplyResources(Me.rbLinien, "rbLinien")
        '
        'rpLinienBearbeiten
        '
        Me.rpLinienBearbeiten.ButtonMoreVisible = False
        Me.rpLinienBearbeiten.Items.Add(Me.rbLinienAdd)
        Me.rpLinienBearbeiten.Items.Add(Me.rbLinienDel)
        Me.rpLinienBearbeiten.Items.Add(Me.rsLinien)
        Me.rpLinienBearbeiten.Items.Add(Me.rbLinienAuto)
        resources.ApplyResources(Me.rpLinienBearbeiten, "rpLinienBearbeiten")
        '
        'rbLinienAdd
        '
        Me.rbLinienAdd.Image = Global.WinBack.My.Resources.Resources.LinienNeu_32x32
        Me.rbLinienAdd.SmallImage = CType(resources.GetObject("rbLinienAdd.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbLinienAdd, "rbLinienAdd")
        '
        'rbLinienDel
        '
        Me.rbLinienDel.Image = Global.WinBack.My.Resources.Resources.LinienLoeschen_32x32
        Me.rbLinienDel.SmallImage = CType(resources.GetObject("rbLinienDel.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbLinienDel, "rbLinienDel")
        '
        'rsLinien
        '
        resources.ApplyResources(Me.rsLinien, "rsLinien")
        '
        'rbLinienAuto
        '
        Me.rbLinienAuto.Image = Global.WinBack.My.Resources.Resources.LinienAutoInstall_32x32
        Me.rbLinienAuto.SmallImage = CType(resources.GetObject("rbLinienAuto.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbLinienAuto, "rbLinienAuto")
        '
        'rpLinienDrucken
        '
        Me.rpLinienDrucken.ButtonMoreEnabled = False
        Me.rpLinienDrucken.ButtonMoreVisible = False
        Me.rpLinienDrucken.Items.Add(Me.rbLinienDrucken)
        resources.ApplyResources(Me.rpLinienDrucken, "rpLinienDrucken")
        '
        'rbLinienDrucken
        '
        Me.rbLinienDrucken.Image = Global.WinBack.My.Resources.Resources.LinienDrucken_32x32
        Me.rbLinienDrucken.SmallImage = CType(resources.GetObject("rbLinienDrucken.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbLinienDrucken, "rbLinienDrucken")
        '
        'rbPlanung
        '
        Me.rbPlanung.Panels.Add(Me.rpProduktionAnsicht)
        Me.rbPlanung.Tag = "130"
        resources.ApplyResources(Me.rbPlanung, "rbPlanung")
        '
        'rpProduktionAnsicht
        '
        Me.rpProduktionAnsicht.ButtonMoreVisible = False
        Me.rpProduktionAnsicht.Items.Add(Me.rbProduktionPlanung)
        resources.ApplyResources(Me.rpProduktionAnsicht, "rpProduktionAnsicht")
        '
        'rbProduktionPlanung
        '
        Me.rbProduktionPlanung.Image = Global.WinBack.My.Resources.Resources.PlanungMain_32x32
        Me.rbProduktionPlanung.SmallImage = CType(resources.GetObject("rbProduktionPlanung.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbProduktionPlanung, "rbProduktionPlanung")
        '
        'rbExtra
        '
        Me.rbExtra.Panels.Add(Me.rbAdminAnsicht)
        Me.rbExtra.Tag = "105"
        resources.ApplyResources(Me.rbExtra, "rbExtra")
        '
        'rbAdminAnsicht
        '
        Me.rbAdminAnsicht.ButtonMoreVisible = False
        Me.rbAdminAnsicht.Items.Add(Me.rbDatensicherung)
        Me.rbAdminAnsicht.Items.Add(Me.rbAdminWinBackIni)
        Me.rbAdminAnsicht.Items.Add(Me.rsAdmin)
        Me.rbAdminAnsicht.Items.Add(Me.rbAdminUpdate)
        resources.ApplyResources(Me.rbAdminAnsicht, "rbAdminAnsicht")
        '
        'rbDatensicherung
        '
        Me.rbDatensicherung.Image = Global.WinBack.My.Resources.Resources.AdminMain_32x32
        Me.rbDatensicherung.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbDatensicherung.SmallImage = CType(resources.GetObject("rbDatensicherung.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbDatensicherung, "rbDatensicherung")
        '
        'rbAdminWinBackIni
        '
        Me.rbAdminWinBackIni.Image = Global.WinBack.My.Resources.Resources.AdminEditKonfig_32x32
        Me.rbAdminWinBackIni.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbAdminWinBackIni.SmallImage = CType(resources.GetObject("rbAdminWinBackIni.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbAdminWinBackIni, "rbAdminWinBackIni")
        '
        'rsAdmin
        '
        resources.ApplyResources(Me.rsAdmin, "rsAdmin")
        '
        'rbAdminUpdate
        '
        Me.rbAdminUpdate.Image = Global.WinBack.My.Resources.Resources.AdminUpdateDataBase_32x32
        Me.rbAdminUpdate.MinimumSize = New System.Drawing.Size(90, 0)
        Me.rbAdminUpdate.SmallImage = CType(resources.GetObject("rbAdminUpdate.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbAdminUpdate, "rbAdminUpdate")
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
        resources.ApplyResources(Me.rTab, "rTab")
        Me.rTab.AllowDrop = True
        Me.rTab.BackColor = System.Drawing.SystemColors.Control
        Me.rTab.Minimized = False
        Me.rTab.Name = "rTab"
        '
        '
        '
        Me.rTab.OrbDropDown.AccessibleDescription = resources.GetString("rTab.OrbDropDown.AccessibleDescription")
        Me.rTab.OrbDropDown.AccessibleName = resources.GetString("rTab.OrbDropDown.AccessibleName")
        Me.rTab.OrbDropDown.Anchor = CType(resources.GetObject("rTab.OrbDropDown.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.rTab.OrbDropDown.AutoSizeContentButtons = False
        Me.rTab.OrbDropDown.BackgroundImage = CType(resources.GetObject("rTab.OrbDropDown.BackgroundImage"), System.Drawing.Image)
        Me.rTab.OrbDropDown.BackgroundImageLayout = CType(resources.GetObject("rTab.OrbDropDown.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.rTab.OrbDropDown.BorderRoundness = 8
        Me.rTab.OrbDropDown.ContentButtonsMinWidth = 120
        Me.rTab.OrbDropDown.ContentRecentItemsMinWidth = 75
        Me.rTab.OrbDropDown.Dock = CType(resources.GetObject("rTab.OrbDropDown.Dock"), System.Windows.Forms.DockStyle)
        Me.rTab.OrbDropDown.Font = CType(resources.GetObject("rTab.OrbDropDown.Font"), System.Drawing.Font)
        Me.rTab.OrbDropDown.ImeMode = CType(resources.GetObject("rTab.OrbDropDown.ImeMode"), System.Windows.Forms.ImeMode)
        Me.rTab.OrbDropDown.Location = CType(resources.GetObject("rTab.OrbDropDown.Location"), System.Drawing.Point)
        Me.rTab.OrbDropDown.MaximumSize = CType(resources.GetObject("rTab.OrbDropDown.MaximumSize"), System.Drawing.Size)
        Me.rTab.OrbDropDown.MenuItems.Add(Me.rbAbmelden)
        Me.rTab.OrbDropDown.MenuItems.Add(Me.rbEnde)
        Me.rTab.OrbDropDown.MenuItems.Add(Me.rbSeparator)
        Me.rTab.OrbDropDown.MenuItems.Add(Me.rbInfo)
        Me.rTab.OrbDropDown.Name = ""
        Me.rTab.OrbDropDown.RecentItems.Add(Me.RibbonOrbRecentItem0)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.RibbonOrbRecentItem1)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.RibbonOrbRecentItem2)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.RibbonOrbRecentItem3)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.RibbonOrbRecentItem4)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.RibbonOrbRecentItem5)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.RibbonOrbRecentItem6)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.RibbonOrbRecentItem7)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.RibbonOrbRecentItem8)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.RibbonOrbRecentItem9)
        Me.rTab.OrbDropDown.RecentItems.Add(Me.RibbonOrbRecentItem10)
        Me.rTab.OrbDropDown.RecentItemsCaption = "Sprache"
        Me.rTab.OrbDropDown.RightToLeft = CType(resources.GetObject("rTab.OrbDropDown.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.rTab.OrbDropDown.Size = CType(resources.GetObject("rTab.OrbDropDown.Size"), System.Drawing.Size)
        Me.rTab.OrbDropDown.TabIndex = CType(resources.GetObject("rTab.OrbDropDown.TabIndex"), Integer)
        Me.rTab.OrbImage = Global.WinBack.My.Resources.Resources.WinBack_32x32
        Me.rTab.OrbText = "Datei"
        '
        '
        '
        Me.rTab.QuickAcessToolbar.Checked = True
        Me.rTab.QuickAcessToolbar.DropDownButtonVisible = False
        Me.rTab.QuickAcessToolbar.Text = resources.GetString("rTab.QuickAcessToolbar.Text")
        Me.rTab.QuickAcessToolbar.ToolTip = resources.GetString("rTab.QuickAcessToolbar.ToolTip")
        Me.rTab.QuickAcessToolbar.ToolTipImage = CType(resources.GetObject("rTab.QuickAcessToolbar.ToolTipImage"), System.Drawing.Image)
        Me.rTab.QuickAcessToolbar.Visible = False
        Me.rTab.RibbonTabFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rTab.Tabs.Add(Me.rbChargen)
        Me.rTab.Tabs.Add(Me.rbArtikel)
        Me.rTab.Tabs.Add(Me.rbRezepte)
        Me.rTab.Tabs.Add(Me.rbRohstoffe)
        Me.rTab.Tabs.Add(Me.rbUser)
        Me.rTab.Tabs.Add(Me.rbLinien)
        Me.rTab.Tabs.Add(Me.rbPlanung)
        Me.rTab.Tabs.Add(Me.rbExtra)
        Me.rTab.TabsMargin = New System.Windows.Forms.Padding(12, 26, 20, 0)
        Me.rTab.ThemeColor = System.Windows.Forms.RibbonTheme.Black
        '
        'rbAbmelden
        '
        Me.rbAbmelden.Image = CType(resources.GetObject("rbAbmelden.Image"), System.Drawing.Image)
        Me.rbAbmelden.SmallImage = CType(resources.GetObject("rbAbmelden.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbAbmelden, "rbAbmelden")
        '
        'rbEnde
        '
        Me.rbEnde.Image = CType(resources.GetObject("rbEnde.Image"), System.Drawing.Image)
        Me.rbEnde.SmallImage = CType(resources.GetObject("rbEnde.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbEnde, "rbEnde")
        '
        'rbInfo
        '
        Me.rbInfo.Image = CType(resources.GetObject("rbInfo.Image"), System.Drawing.Image)
        Me.rbInfo.SmallImage = CType(resources.GetObject("rbInfo.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbInfo, "rbInfo")
        '
        'RibbonOrbRecentItem0
        '
        Me.RibbonOrbRecentItem0.Checked = True
        Me.RibbonOrbRecentItem0.CheckOnClick = True
        Me.RibbonOrbRecentItem0.DropDownItems.Add(Me.RibbonButton1)
        Me.RibbonOrbRecentItem0.Image = CType(resources.GetObject("RibbonOrbRecentItem0.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem0.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem0.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem0, "RibbonOrbRecentItem0")
        Me.RibbonOrbRecentItem0.ToolTipImage = Global.WinBack.My.Resources.Resources.LangDE
        '
        'RibbonButton1
        '
        Me.RibbonButton1.Image = CType(resources.GetObject("RibbonButton1.Image"), System.Drawing.Image)
        Me.RibbonButton1.SmallImage = CType(resources.GetObject("RibbonButton1.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonButton1, "RibbonButton1")
        '
        'RibbonOrbRecentItem1
        '
        Me.RibbonOrbRecentItem1.Image = CType(resources.GetObject("RibbonOrbRecentItem1.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem1.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem1.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem1, "RibbonOrbRecentItem1")
        Me.RibbonOrbRecentItem1.ToolTipImage = Global.WinBack.My.Resources.Resources.LangHU
        '
        'RibbonOrbRecentItem2
        '
        Me.RibbonOrbRecentItem2.Image = CType(resources.GetObject("RibbonOrbRecentItem2.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem2.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem2.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem2, "RibbonOrbRecentItem2")
        Me.RibbonOrbRecentItem2.ToolTipImage = Global.WinBack.My.Resources.Resources.LangNL
        '
        'RibbonOrbRecentItem3
        '
        Me.RibbonOrbRecentItem3.Image = CType(resources.GetObject("RibbonOrbRecentItem3.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem3.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem3.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem3, "RibbonOrbRecentItem3")
        Me.RibbonOrbRecentItem3.ToolTipImage = Global.WinBack.My.Resources.Resources.LangUS
        '
        'RibbonOrbRecentItem4
        '
        Me.RibbonOrbRecentItem4.Image = CType(resources.GetObject("RibbonOrbRecentItem4.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem4.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem4.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem4, "RibbonOrbRecentItem4")
        Me.RibbonOrbRecentItem4.ToolTipImage = Global.WinBack.My.Resources.Resources.LangPT
        '
        'RibbonOrbRecentItem5
        '
        Me.RibbonOrbRecentItem5.Image = CType(resources.GetObject("RibbonOrbRecentItem5.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem5.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem5.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem5, "RibbonOrbRecentItem5")
        Me.RibbonOrbRecentItem5.ToolTipImage = Global.WinBack.My.Resources.Resources.LangSI
        '
        'RibbonOrbRecentItem6
        '
        Me.RibbonOrbRecentItem6.Image = CType(resources.GetObject("RibbonOrbRecentItem6.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem6.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem6.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem6, "RibbonOrbRecentItem6")
        Me.RibbonOrbRecentItem6.ToolTipImage = Global.WinBack.My.Resources.Resources.LangRU
        '
        'RibbonOrbRecentItem7
        '
        Me.RibbonOrbRecentItem7.Image = CType(resources.GetObject("RibbonOrbRecentItem7.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem7.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem7.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem7, "RibbonOrbRecentItem7")
        Me.RibbonOrbRecentItem7.ToolTipImage = Global.WinBack.My.Resources.Resources.LangFR
        '
        'RibbonOrbRecentItem8
        '
        Me.RibbonOrbRecentItem8.Image = CType(resources.GetObject("RibbonOrbRecentItem8.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem8.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem8.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem8, "RibbonOrbRecentItem8")
        Me.RibbonOrbRecentItem8.ToolTipImage = Global.WinBack.My.Resources.Resources.LangES
        '
        'RibbonOrbRecentItem9
        '
        Me.RibbonOrbRecentItem9.Image = CType(resources.GetObject("RibbonOrbRecentItem9.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem9.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem9.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem9, "RibbonOrbRecentItem9")
        Me.RibbonOrbRecentItem9.ToolTipImage = Global.WinBack.My.Resources.Resources.LangSK
        '
        'RibbonOrbRecentItem10
        '
        Me.RibbonOrbRecentItem10.Image = CType(resources.GetObject("RibbonOrbRecentItem10.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem10.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem10.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem10, "RibbonOrbRecentItem10")
        Me.RibbonOrbRecentItem10.ToolTipImage = Global.WinBack.My.Resources.Resources.LangRO
        '
        'WinBack
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.rTab)
        Me.Controls.Add(Me.StatusStrip)
        Me.IsMdiContainer = True
        Me.Name = "WinBack"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents RbUserRemove As RibbonButton
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
    Friend WithEvents RibbonOrbRecentItem0 As RibbonOrbRecentItem
    Friend WithEvents RibbonOrbRecentItem1 As RibbonOrbRecentItem
    Friend WithEvents rbSeparator As RibbonSeparator
    Friend WithEvents rbInfo As RibbonButton
    Friend WithEvents RibbonButton1 As RibbonButton
    Friend WithEvents RibbonOrbRecentItem2 As RibbonOrbRecentItem
    Friend WithEvents RibbonOrbRecentItem3 As RibbonOrbRecentItem
    Friend WithEvents RibbonOrbRecentItem4 As RibbonOrbRecentItem
    Friend WithEvents RibbonOrbRecentItem5 As RibbonOrbRecentItem
    Friend WithEvents RibbonOrbRecentItem6 As RibbonOrbRecentItem
    Friend WithEvents RibbonOrbRecentItem7 As RibbonOrbRecentItem
    Friend WithEvents RibbonOrbRecentItem8 As RibbonOrbRecentItem
    Friend WithEvents RibbonOrbRecentItem9 As RibbonOrbRecentItem
    Friend WithEvents RibbonOrbRecentItem10 As RibbonOrbRecentItem
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
    Friend WithEvents rbRohstoffeLieferungen As RibbonButton
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
End Class
