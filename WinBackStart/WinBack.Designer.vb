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
        Me.rTab = New System.Windows.Forms.Ribbon()
        Me.rbAbmelden = New System.Windows.Forms.RibbonButton()
        Me.rbEnde = New System.Windows.Forms.RibbonButton()
        Me.rbSeparator = New System.Windows.Forms.RibbonSeparator()
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
        Me.rpArtikel = New System.Windows.Forms.RibbonPanel()
        Me.rbArtikelNeu = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelRemove = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelCopy = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelPrint = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelCalc = New System.Windows.Forms.RibbonButton()
        Me.rpArtikelListe = New System.Windows.Forms.RibbonPanel()
        Me.rbArtikelListePrint = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelListeExport = New System.Windows.Forms.RibbonButton()
        Me.rpArtikelInfo = New System.Windows.Forms.RibbonPanel()
        Me.rbArtikelProduktInfo = New System.Windows.Forms.RibbonButton()
        Me.rbArtikelProduktInfoPrint = New System.Windows.Forms.RibbonButton()
        Me.rbRezepte = New System.Windows.Forms.RibbonTab()
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
        Me.lblLanguage.Image = Global.WinBack.My.Resources.Resources.de
        resources.ApplyResources(Me.lblLanguage, "lblLanguage")
        Me.lblLanguage.Name = "lblLanguage"
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
        Me.RibbonOrbRecentItem0.ToolTipImage = Global.WinBack.My.Resources.Resources.de
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
        Me.RibbonOrbRecentItem1.ToolTipImage = Global.WinBack.My.Resources.Resources.hu
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
        Me.RibbonOrbRecentItem3.ToolTipImage = Global.WinBack.My.Resources.Resources.us
        '
        'RibbonOrbRecentItem4
        '
        Me.RibbonOrbRecentItem4.Image = CType(resources.GetObject("RibbonOrbRecentItem4.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem4.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem4.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem4, "RibbonOrbRecentItem4")
        Me.RibbonOrbRecentItem4.ToolTipImage = Global.WinBack.My.Resources.Resources.pt
        '
        'RibbonOrbRecentItem5
        '
        Me.RibbonOrbRecentItem5.Image = CType(resources.GetObject("RibbonOrbRecentItem5.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem5.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem5.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem5, "RibbonOrbRecentItem5")
        Me.RibbonOrbRecentItem5.ToolTipImage = Global.WinBack.My.Resources.Resources.si
        '
        'RibbonOrbRecentItem6
        '
        Me.RibbonOrbRecentItem6.Image = CType(resources.GetObject("RibbonOrbRecentItem6.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem6.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem6.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem6, "RibbonOrbRecentItem6")
        Me.RibbonOrbRecentItem6.ToolTipImage = Global.WinBack.My.Resources.Resources.ru
        '
        'RibbonOrbRecentItem7
        '
        Me.RibbonOrbRecentItem7.Image = CType(resources.GetObject("RibbonOrbRecentItem7.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem7.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem7.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem7, "RibbonOrbRecentItem7")
        Me.RibbonOrbRecentItem7.ToolTipImage = Global.WinBack.My.Resources.Resources.fr
        '
        'RibbonOrbRecentItem8
        '
        Me.RibbonOrbRecentItem8.Image = CType(resources.GetObject("RibbonOrbRecentItem8.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem8.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem8.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem8, "RibbonOrbRecentItem8")
        Me.RibbonOrbRecentItem8.ToolTipImage = Global.WinBack.My.Resources.Resources.es
        '
        'RibbonOrbRecentItem9
        '
        Me.RibbonOrbRecentItem9.Image = CType(resources.GetObject("RibbonOrbRecentItem9.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem9.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem9.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem9, "RibbonOrbRecentItem9")
        Me.RibbonOrbRecentItem9.ToolTipImage = Global.WinBack.My.Resources.Resources.sk
        '
        'RibbonOrbRecentItem10
        '
        Me.RibbonOrbRecentItem10.Image = CType(resources.GetObject("RibbonOrbRecentItem10.Image"), System.Drawing.Image)
        Me.RibbonOrbRecentItem10.SmallImage = CType(resources.GetObject("RibbonOrbRecentItem10.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbRecentItem10, "RibbonOrbRecentItem10")
        Me.RibbonOrbRecentItem10.ToolTipImage = Global.WinBack.My.Resources.Resources.ro
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
        Me.rbArtikel.Panels.Add(Me.rpArtikel)
        Me.rbArtikel.Panels.Add(Me.rpArtikelListe)
        Me.rbArtikel.Panels.Add(Me.rpArtikelInfo)
        Me.rbArtikel.Tag = "102"
        resources.ApplyResources(Me.rbArtikel, "rbArtikel")
        '
        'rpArtikel
        '
        Me.rpArtikel.Items.Add(Me.rbArtikelNeu)
        Me.rpArtikel.Items.Add(Me.rbArtikelRemove)
        Me.rpArtikel.Items.Add(Me.rbArtikelCopy)
        Me.rpArtikel.Items.Add(Me.rbArtikelPrint)
        Me.rpArtikel.Items.Add(Me.rbArtikelCalc)
        resources.ApplyResources(Me.rpArtikel, "rpArtikel")
        '
        'rbArtikelNeu
        '
        Me.rbArtikelNeu.Image = CType(resources.GetObject("rbArtikelNeu.Image"), System.Drawing.Image)
        Me.rbArtikelNeu.SmallImage = CType(resources.GetObject("rbArtikelNeu.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelNeu, "rbArtikelNeu")
        '
        'rbArtikelRemove
        '
        Me.rbArtikelRemove.Image = CType(resources.GetObject("rbArtikelRemove.Image"), System.Drawing.Image)
        Me.rbArtikelRemove.SmallImage = CType(resources.GetObject("rbArtikelRemove.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelRemove, "rbArtikelRemove")
        '
        'rbArtikelCopy
        '
        Me.rbArtikelCopy.Image = CType(resources.GetObject("rbArtikelCopy.Image"), System.Drawing.Image)
        Me.rbArtikelCopy.SmallImage = CType(resources.GetObject("rbArtikelCopy.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelCopy, "rbArtikelCopy")
        '
        'rbArtikelPrint
        '
        Me.rbArtikelPrint.Image = CType(resources.GetObject("rbArtikelPrint.Image"), System.Drawing.Image)
        Me.rbArtikelPrint.SmallImage = CType(resources.GetObject("rbArtikelPrint.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelPrint, "rbArtikelPrint")
        '
        'rbArtikelCalc
        '
        Me.rbArtikelCalc.Image = CType(resources.GetObject("rbArtikelCalc.Image"), System.Drawing.Image)
        Me.rbArtikelCalc.SmallImage = CType(resources.GetObject("rbArtikelCalc.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelCalc, "rbArtikelCalc")
        '
        'rpArtikelListe
        '
        Me.rpArtikelListe.Items.Add(Me.rbArtikelListePrint)
        Me.rpArtikelListe.Items.Add(Me.rbArtikelListeExport)
        resources.ApplyResources(Me.rpArtikelListe, "rpArtikelListe")
        '
        'rbArtikelListePrint
        '
        Me.rbArtikelListePrint.Image = CType(resources.GetObject("rbArtikelListePrint.Image"), System.Drawing.Image)
        Me.rbArtikelListePrint.SmallImage = CType(resources.GetObject("rbArtikelListePrint.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelListePrint, "rbArtikelListePrint")
        '
        'rbArtikelListeExport
        '
        Me.rbArtikelListeExport.Image = CType(resources.GetObject("rbArtikelListeExport.Image"), System.Drawing.Image)
        Me.rbArtikelListeExport.SmallImage = CType(resources.GetObject("rbArtikelListeExport.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelListeExport, "rbArtikelListeExport")
        '
        'rpArtikelInfo
        '
        Me.rpArtikelInfo.Items.Add(Me.rbArtikelProduktInfo)
        Me.rpArtikelInfo.Items.Add(Me.rbArtikelProduktInfoPrint)
        resources.ApplyResources(Me.rpArtikelInfo, "rpArtikelInfo")
        '
        'rbArtikelProduktInfo
        '
        Me.rbArtikelProduktInfo.Image = CType(resources.GetObject("rbArtikelProduktInfo.Image"), System.Drawing.Image)
        Me.rbArtikelProduktInfo.SmallImage = CType(resources.GetObject("rbArtikelProduktInfo.SmallImage"), System.Drawing.Image)
        Me.rbArtikelProduktInfo.Tag = "128"
        resources.ApplyResources(Me.rbArtikelProduktInfo, "rbArtikelProduktInfo")
        '
        'rbArtikelProduktInfoPrint
        '
        Me.rbArtikelProduktInfoPrint.Image = CType(resources.GetObject("rbArtikelProduktInfoPrint.Image"), System.Drawing.Image)
        Me.rbArtikelProduktInfoPrint.SmallImage = CType(resources.GetObject("rbArtikelProduktInfoPrint.SmallImage"), System.Drawing.Image)
        Me.rbArtikelProduktInfoPrint.Tag = "128"
        resources.ApplyResources(Me.rbArtikelProduktInfoPrint, "rbArtikelProduktInfoPrint")
        '
        'rbRezepte
        '
        Me.rbRezepte.Tag = "103"
        resources.ApplyResources(Me.rbRezepte, "rbRezepte")
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
    Friend WithEvents rpArtikel As RibbonPanel
    Friend WithEvents rbArtikelNeu As RibbonButton
    Friend WithEvents rbArtikelRemove As RibbonButton
    Friend WithEvents rbArtikelCopy As RibbonButton
    Friend WithEvents rbArtikelPrint As RibbonButton
    Friend WithEvents rbArtikelCalc As RibbonButton
    Friend WithEvents rpArtikelListe As RibbonPanel
    Friend WithEvents rbArtikelListePrint As RibbonButton
    Friend WithEvents rbArtikelListeExport As RibbonButton
    Friend WithEvents rpArtikelInfo As RibbonPanel
    Friend WithEvents rbArtikelProduktInfo As RibbonButton
    Friend WithEvents rbArtikelProduktInfoPrint As RibbonButton
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
End Class
