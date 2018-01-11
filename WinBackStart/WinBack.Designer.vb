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
        Me.rpRezeptAnsicht = New System.Windows.Forms.RibbonPanel()
        Me.rpRezeptDrucken = New System.Windows.Forms.RibbonPanel()
        Me.rbRohstoffe = New System.Windows.Forms.RibbonTab()
        Me.rbUser = New System.Windows.Forms.RibbonTab()
        Me.rpUser = New System.Windows.Forms.RibbonPanel()
        Me.rbUserNeu = New System.Windows.Forms.RibbonButton()
        Me.RnUserRemove = New System.Windows.Forms.RibbonButton()
        Me.rsUser = New System.Windows.Forms.RibbonSeparator()
        Me.rbUserListe = New System.Windows.Forms.RibbonButton()
        Me.rpUserGruppen = New System.Windows.Forms.RibbonPanel()
        Me.rbUserRechte = New System.Windows.Forms.RibbonButton()
        Me.rbLinien = New System.Windows.Forms.RibbonTab()
        Me.rpLinien = New System.Windows.Forms.RibbonPanel()
        Me.rbLinienAdd = New System.Windows.Forms.RibbonButton()
        Me.rbLinienDel = New System.Windows.Forms.RibbonButton()
        Me.rbLinienAuto = New System.Windows.Forms.RibbonButton()
        Me.rsLinien = New System.Windows.Forms.RibbonSeparator()
        Me.rbLinienPrint = New System.Windows.Forms.RibbonButton()
        Me.rbPlanung = New System.Windows.Forms.RibbonTab()
        Me.rbExtra = New System.Windows.Forms.RibbonTab()
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
        Me.rbRezeptNeu = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptBearbeiten = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptListe = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptDetails = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptHinweis = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptVerwendung = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptHistorie = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptDrucken = New System.Windows.Forms.RibbonButton()
        Me.rbRezeptListeDrucken = New System.Windows.Forms.RibbonButton()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblVersion, Me.lblNetworkIP, Me.lblLanguage})
        resources.ApplyResources(Me.StatusStrip, "StatusStrip")
        Me.StatusStrip.Name = "StatusStrip"
        '
        'lblVersion
        '
        Me.lblVersion.Name = "lblVersion"
        resources.ApplyResources(Me.lblVersion, "lblVersion")
        '
        'lblNetworkIP
        '
        Me.lblNetworkIP.Name = "lblNetworkIP"
        resources.ApplyResources(Me.lblNetworkIP, "lblNetworkIP")
        '
        'lblLanguage
        '
        Me.lblLanguage.Image = Global.WinBack.My.Resources.Resources.LangDE
        resources.ApplyResources(Me.lblLanguage, "lblLanguage")
        Me.lblLanguage.Name = "lblLanguage"
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
        Me.rpChargen.Items.Add(Me.rbChargenListe)
        Me.rpChargen.Items.Add(Me.rbChargenDetails)
        resources.ApplyResources(Me.rpChargen, "rpChargen")
        '
        'rbChargenListe
        '
        Me.rbChargenListe.Image = CType(resources.GetObject("rbChargenListe.Image"), System.Drawing.Image)
        Me.rbChargenListe.SmallImage = CType(resources.GetObject("rbChargenListe.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbChargenListe, "rbChargenListe")
        '
        'rbChargenDetails
        '
        Me.rbChargenDetails.Image = CType(resources.GetObject("rbChargenDetails.Image"), System.Drawing.Image)
        Me.rbChargenDetails.SmallImage = CType(resources.GetObject("rbChargenDetails.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbChargenDetails, "rbChargenDetails")
        '
        'rpChargenExport
        '
        Me.rpChargenExport.Items.Add(Me.rbChargenExcelSumme)
        Me.rpChargenExport.Items.Add(Me.rbChargenExcelEinzel)
        Me.rpChargenExport.Items.Add(Me.rbChargenExcelDetails)
        resources.ApplyResources(Me.rpChargenExport, "rpChargenExport")
        '
        'rbChargenExcelSumme
        '
        Me.rbChargenExcelSumme.Image = CType(resources.GetObject("rbChargenExcelSumme.Image"), System.Drawing.Image)
        Me.rbChargenExcelSumme.SmallImage = CType(resources.GetObject("rbChargenExcelSumme.SmallImage"), System.Drawing.Image)
        Me.rbChargenExcelSumme.Tag = "125"
        resources.ApplyResources(Me.rbChargenExcelSumme, "rbChargenExcelSumme")
        '
        'rbChargenExcelEinzel
        '
        Me.rbChargenExcelEinzel.Image = CType(resources.GetObject("rbChargenExcelEinzel.Image"), System.Drawing.Image)
        Me.rbChargenExcelEinzel.SmallImage = CType(resources.GetObject("rbChargenExcelEinzel.SmallImage"), System.Drawing.Image)
        Me.rbChargenExcelEinzel.Tag = "125"
        resources.ApplyResources(Me.rbChargenExcelEinzel, "rbChargenExcelEinzel")
        '
        'rbChargenExcelDetails
        '
        Me.rbChargenExcelDetails.Image = CType(resources.GetObject("rbChargenExcelDetails.Image"), System.Drawing.Image)
        Me.rbChargenExcelDetails.SmallImage = CType(resources.GetObject("rbChargenExcelDetails.SmallImage"), System.Drawing.Image)
        Me.rbChargenExcelDetails.Tag = "125"
        resources.ApplyResources(Me.rbChargenExcelDetails, "rbChargenExcelDetails")
        '
        'rpStatRohStoffe
        '
        Me.rpStatRohStoffe.Items.Add(Me.rbStatRohstoffe)
        Me.rpStatRohStoffe.Items.Add(Me.rbStatRohstoffeDetail)
        resources.ApplyResources(Me.rpStatRohStoffe, "rpStatRohStoffe")
        '
        'rbStatRohstoffe
        '
        Me.rbStatRohstoffe.Image = CType(resources.GetObject("rbStatRohstoffe.Image"), System.Drawing.Image)
        Me.rbStatRohstoffe.SmallImage = CType(resources.GetObject("rbStatRohstoffe.SmallImage"), System.Drawing.Image)
        Me.rbStatRohstoffe.Tag = "122"
        resources.ApplyResources(Me.rbStatRohstoffe, "rbStatRohstoffe")
        '
        'rbStatRohstoffeDetail
        '
        Me.rbStatRohstoffeDetail.Image = CType(resources.GetObject("rbStatRohstoffeDetail.Image"), System.Drawing.Image)
        Me.rbStatRohstoffeDetail.SmallImage = CType(resources.GetObject("rbStatRohstoffeDetail.SmallImage"), System.Drawing.Image)
        Me.rbStatRohstoffeDetail.Tag = "122"
        resources.ApplyResources(Me.rbStatRohstoffeDetail, "rbStatRohstoffeDetail")
        '
        'rpStatRezepte
        '
        Me.rpStatRezepte.Items.Add(Me.rbStatRezepte)
        resources.ApplyResources(Me.rpStatRezepte, "rpStatRezepte")
        '
        'rbStatRezepte
        '
        Me.rbStatRezepte.Image = CType(resources.GetObject("rbStatRezepte.Image"), System.Drawing.Image)
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
        Me.rbArtikelNeu.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelNeu.SmallImage = CType(resources.GetObject("rbArtikelNeu.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelNeu, "rbArtikelNeu")
        '
        'rbArtikelBearbeiten
        '
        Me.rbArtikelBearbeiten.Image = Global.WinBack.My.Resources.Resources.ArtikelBearbeiten_32x32
        Me.rbArtikelBearbeiten.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelBearbeiten.SmallImage = CType(resources.GetObject("rbArtikelBearbeiten.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelBearbeiten, "rbArtikelBearbeiten")
        '
        'rbArtikelRemove
        '
        Me.rbArtikelRemove.Image = Global.WinBack.My.Resources.Resources.ArtikelLoeschen_32x32
        Me.rbArtikelRemove.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbArtikelRemove.SmallImage = CType(resources.GetObject("rbArtikelRemove.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelRemove, "rbArtikelRemove")
        '
        'rbArtikelCopy
        '
        Me.rbArtikelCopy.Image = Global.WinBack.My.Resources.Resources.ArtikelKopieren_32x32
        Me.rbArtikelCopy.MinimumSize = New System.Drawing.Size(70, 0)
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
        Me.rbArtikelProduktInfo.Image = Global.WinBack.My.Resources.Resources.ArtikelProduktInfo_32x32
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
        Me.rbArtikelPrint.Image = Global.WinBack.My.Resources.Resources.ArtikelDrucken_32x32
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
        'rpRezeptAnsicht
        '
        Me.rpRezeptAnsicht.ButtonMoreVisible = False
        Me.rpRezeptAnsicht.Items.Add(Me.rbRezeptListe)
        Me.rpRezeptAnsicht.Items.Add(Me.rbRezeptDetails)
        Me.rpRezeptAnsicht.Items.Add(Me.rbRezeptHinweis)
        Me.rpRezeptAnsicht.Items.Add(Me.rbRezeptVerwendung)
        Me.rpRezeptAnsicht.Items.Add(Me.rbRezeptHistorie)
        resources.ApplyResources(Me.rpRezeptAnsicht, "rpRezeptAnsicht")
        '
        'rpRezeptDrucken
        '
        Me.rpRezeptDrucken.ButtonMoreVisible = False
        Me.rpRezeptDrucken.Items.Add(Me.rbRezeptDrucken)
        Me.rpRezeptDrucken.Items.Add(Me.rbRezeptListeDrucken)
        resources.ApplyResources(Me.rpRezeptDrucken, "rpRezeptDrucken")
        '
        'rbRohstoffe
        '
        Me.rbRohstoffe.Tag = "104"
        resources.ApplyResources(Me.rbRohstoffe, "rbRohstoffe")
        '
        'rbUser
        '
        Me.rbUser.Panels.Add(Me.rpUser)
        Me.rbUser.Panels.Add(Me.rpUserGruppen)
        Me.rbUser.Tag = "120"
        resources.ApplyResources(Me.rbUser, "rbUser")
        '
        'rpUser
        '
        Me.rpUser.Items.Add(Me.rbUserNeu)
        Me.rpUser.Items.Add(Me.RnUserRemove)
        Me.rpUser.Items.Add(Me.rsUser)
        Me.rpUser.Items.Add(Me.rbUserListe)
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
        'RnUserRemove
        '
        Me.RnUserRemove.Image = CType(resources.GetObject("RnUserRemove.Image"), System.Drawing.Image)
        Me.RnUserRemove.SmallImage = CType(resources.GetObject("RnUserRemove.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RnUserRemove, "RnUserRemove")
        '
        'rbUserListe
        '
        Me.rbUserListe.Image = CType(resources.GetObject("rbUserListe.Image"), System.Drawing.Image)
        Me.rbUserListe.SmallImage = CType(resources.GetObject("rbUserListe.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbUserListe, "rbUserListe")
        '
        'rpUserGruppen
        '
        Me.rpUserGruppen.Items.Add(Me.rbUserRechte)
        Me.rpUserGruppen.Tag = ""
        resources.ApplyResources(Me.rpUserGruppen, "rpUserGruppen")
        '
        'rbUserRechte
        '
        Me.rbUserRechte.Image = Global.WinBack.My.Resources.Resources.UserPasswd_32x32
        Me.rbUserRechte.SmallImage = CType(resources.GetObject("rbUserRechte.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbUserRechte, "rbUserRechte")
        '
        'rbLinien
        '
        Me.rbLinien.Panels.Add(Me.rpLinien)
        Me.rbLinien.Tag = "121"
        resources.ApplyResources(Me.rbLinien, "rbLinien")
        '
        'rpLinien
        '
        Me.rpLinien.Items.Add(Me.rbLinienAdd)
        Me.rpLinien.Items.Add(Me.rbLinienDel)
        Me.rpLinien.Items.Add(Me.rbLinienAuto)
        Me.rpLinien.Items.Add(Me.rsLinien)
        Me.rpLinien.Items.Add(Me.rbLinienPrint)
        resources.ApplyResources(Me.rpLinien, "rpLinien")
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
        'rbLinienAuto
        '
        Me.rbLinienAuto.Image = Global.WinBack.My.Resources.Resources.LinienAutoInstall_32x32
        Me.rbLinienAuto.SmallImage = CType(resources.GetObject("rbLinienAuto.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbLinienAuto, "rbLinienAuto")
        '
        'rbLinienPrint
        '
        Me.rbLinienPrint.Image = CType(resources.GetObject("rbLinienPrint.Image"), System.Drawing.Image)
        Me.rbLinienPrint.SmallImage = CType(resources.GetObject("rbLinienPrint.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbLinienPrint, "rbLinienPrint")
        '
        'rbPlanung
        '
        Me.rbPlanung.Tag = "130"
        resources.ApplyResources(Me.rbPlanung, "rbPlanung")
        '
        'rbExtra
        '
        Me.rbExtra.Tag = "105"
        resources.ApplyResources(Me.rbExtra, "rbExtra")
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
        Me.rTab.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.rTab, "rTab")
        Me.rTab.Minimized = False
        Me.rTab.Name = "rTab"
        '
        '
        '
        Me.rTab.OrbDropDown.Anchor = CType(resources.GetObject("rTab.OrbDropDown.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.rTab.OrbDropDown.AutoSizeContentButtons = False
        Me.rTab.OrbDropDown.BackgroundImageLayout = CType(resources.GetObject("rTab.OrbDropDown.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.rTab.OrbDropDown.BorderRoundness = 8
        Me.rTab.OrbDropDown.ContentButtonsMinWidth = 120
        Me.rTab.OrbDropDown.ContentRecentItemsMinWidth = 75
        Me.rTab.OrbDropDown.Font = CType(resources.GetObject("rTab.OrbDropDown.Font"), System.Drawing.Font)
        Me.rTab.OrbDropDown.ImeMode = CType(resources.GetObject("rTab.OrbDropDown.ImeMode"), System.Windows.Forms.ImeMode)
        Me.rTab.OrbDropDown.Location = CType(resources.GetObject("rTab.OrbDropDown.Location"), System.Drawing.Point)
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
        Me.RibbonOrbRecentItem2.ToolTipImage = Global.WinBack.My.Resources.Resources.nl
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
        'rbRezeptNeu
        '
        Me.rbRezeptNeu.Image = CType(resources.GetObject("rbRezeptNeu.Image"), System.Drawing.Image)
        Me.rbRezeptNeu.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptNeu.SmallImage = CType(resources.GetObject("rbRezeptNeu.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptNeu, "rbRezeptNeu")
        '
        'rbRezeptBearbeiten
        '
        Me.rbRezeptBearbeiten.Image = CType(resources.GetObject("rbRezeptBearbeiten.Image"), System.Drawing.Image)
        Me.rbRezeptBearbeiten.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptBearbeiten.SmallImage = CType(resources.GetObject("rbRezeptBearbeiten.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptBearbeiten, "rbRezeptBearbeiten")
        '
        'rbRezeptListe
        '
        Me.rbRezeptListe.Image = CType(resources.GetObject("rbRezeptListe.Image"), System.Drawing.Image)
        Me.rbRezeptListe.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptListe.SmallImage = CType(resources.GetObject("rbRezeptListe.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptListe, "rbRezeptListe")
        '
        'rbRezeptDetails
        '
        Me.rbRezeptDetails.Image = CType(resources.GetObject("rbRezeptDetails.Image"), System.Drawing.Image)
        Me.rbRezeptDetails.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptDetails.SmallImage = CType(resources.GetObject("rbRezeptDetails.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptDetails, "rbRezeptDetails")
        '
        'rbRezeptHinweis
        '
        Me.rbRezeptHinweis.Image = CType(resources.GetObject("rbRezeptHinweis.Image"), System.Drawing.Image)
        Me.rbRezeptHinweis.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptHinweis.SmallImage = CType(resources.GetObject("rbRezeptHinweis.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptHinweis, "rbRezeptHinweis")
        '
        'rbRezeptVerwendung
        '
        Me.rbRezeptVerwendung.Image = CType(resources.GetObject("rbRezeptVerwendung.Image"), System.Drawing.Image)
        Me.rbRezeptVerwendung.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptVerwendung.SmallImage = CType(resources.GetObject("rbRezeptVerwendung.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptVerwendung, "rbRezeptVerwendung")
        '
        'rbRezeptHistorie
        '
        Me.rbRezeptHistorie.Image = CType(resources.GetObject("rbRezeptHistorie.Image"), System.Drawing.Image)
        Me.rbRezeptHistorie.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptHistorie.SmallImage = CType(resources.GetObject("rbRezeptHistorie.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptHistorie, "rbRezeptHistorie")
        '
        'rbRezeptDrucken
        '
        Me.rbRezeptDrucken.Image = CType(resources.GetObject("rbRezeptDrucken.Image"), System.Drawing.Image)
        Me.rbRezeptDrucken.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptDrucken.SmallImage = CType(resources.GetObject("rbRezeptDrucken.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptDrucken, "rbRezeptDrucken")
        '
        'rbRezeptListeDrucken
        '
        Me.rbRezeptListeDrucken.Image = CType(resources.GetObject("rbRezeptListeDrucken.Image"), System.Drawing.Image)
        Me.rbRezeptListeDrucken.MinimumSize = New System.Drawing.Size(70, 0)
        Me.rbRezeptListeDrucken.SmallImage = CType(resources.GetObject("rbRezeptListeDrucken.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbRezeptListeDrucken, "rbRezeptListeDrucken")
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
    Friend WithEvents RnUserRemove As RibbonButton
    Friend WithEvents rsUser As RibbonSeparator
    Friend WithEvents rbUserListe As RibbonButton
    Friend WithEvents rpUserGruppen As RibbonPanel
    Friend WithEvents rbUserRechte As RibbonButton
    Friend WithEvents rbLinien As RibbonTab
    Friend WithEvents rpLinien As RibbonPanel
    Friend WithEvents rbLinienAdd As RibbonButton
    Friend WithEvents rbLinienDel As RibbonButton
    Friend WithEvents rbLinienAuto As RibbonButton
    Friend WithEvents rsLinien As RibbonSeparator
    Friend WithEvents rbLinienPrint As RibbonButton
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
    Friend WithEvents rbRezeptListeDrucken As RibbonButton
End Class
