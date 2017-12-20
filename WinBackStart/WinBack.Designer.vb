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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinBack))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblVersion = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblNetworkIP = New System.Windows.Forms.ToolStripStatusLabel()
        Me.rTab = New System.Windows.Forms.Ribbon()
        Me.rbSprache_DE = New System.Windows.Forms.RibbonButton()
        Me.rbSprache_EN = New System.Windows.Forms.RibbonButton()
        Me.rbSprache_FR = New System.Windows.Forms.RibbonButton()
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
        Me.rbSprache_RU = New System.Windows.Forms.RibbonButton()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblVersion, Me.lblNetworkIP})
        resources.ApplyResources(Me.StatusStrip1, "StatusStrip1")
        Me.StatusStrip1.Name = "StatusStrip1"
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
        'rTab
        '
        Me.rTab.AllowDrop = True
        resources.ApplyResources(Me.rTab, "rTab")
        Me.rTab.Minimized = False
        Me.rTab.Name = "rTab"
        '
        '
        '
        Me.rTab.OrbDropDown.BorderRoundness = 8
        Me.rTab.OrbDropDown.Location = CType(resources.GetObject("rTab.OrbDropDown.Location"), System.Drawing.Point)
        Me.rTab.OrbDropDown.Name = ""
        Me.rTab.OrbDropDown.Size = CType(resources.GetObject("rTab.OrbDropDown.Size"), System.Drawing.Size)
        Me.rTab.OrbDropDown.TabIndex = CType(resources.GetObject("rTab.OrbDropDown.TabIndex"), Integer)
        Me.rTab.OrbImage = Nothing
        Me.rTab.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2010
        '
        '
        '
        Me.rTab.QuickAcessToolbar.DropDownButtonItems.Add(Me.rbSprache_DE)
        Me.rTab.QuickAcessToolbar.DropDownButtonItems.Add(Me.rbSprache_EN)
        Me.rTab.QuickAcessToolbar.DropDownButtonItems.Add(Me.rbSprache_FR)
        Me.rTab.QuickAcessToolbar.DropDownButtonItems.Add(Me.rbSprache_RU)
        Me.rTab.RibbonTabFont = New System.Drawing.Font("Trebuchet MS", 9.0!)
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
        'rbSprache_DE
        '
        Me.rbSprache_DE.CheckedGroup = "cgSprache"
        Me.rbSprache_DE.CheckOnClick = True
        Me.rbSprache_DE.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbSprache_DE.Image = CType(resources.GetObject("rbSprache_DE.Image"), System.Drawing.Image)
        Me.rbSprache_DE.SmallImage = CType(resources.GetObject("rbSprache_DE.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbSprache_DE, "rbSprache_DE")
        '
        'rbSprache_EN
        '
        Me.rbSprache_EN.Checked = True
        Me.rbSprache_EN.CheckedGroup = "cgSprache"
        Me.rbSprache_EN.CheckOnClick = True
        Me.rbSprache_EN.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbSprache_EN.Image = CType(resources.GetObject("rbSprache_EN.Image"), System.Drawing.Image)
        Me.rbSprache_EN.SmallImage = CType(resources.GetObject("rbSprache_EN.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbSprache_EN, "rbSprache_EN")
        '
        'rbSprache_FR
        '
        Me.rbSprache_FR.CheckedGroup = "cgSprache"
        Me.rbSprache_FR.CheckOnClick = True
        Me.rbSprache_FR.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbSprache_FR.Image = CType(resources.GetObject("rbSprache_FR.Image"), System.Drawing.Image)
        Me.rbSprache_FR.SmallImage = CType(resources.GetObject("rbSprache_FR.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbSprache_FR, "rbSprache_FR")
        '
        'rbChargen
        '
        Me.rbChargen.Panels.Add(Me.rpChargen)
        Me.rbChargen.Panels.Add(Me.rpChargenExport)
        Me.rbChargen.Panels.Add(Me.rpStatRohStoffe)
        Me.rbChargen.Panels.Add(Me.rpStatRezepte)
        Me.rbChargen.Tag = "rbChargen"
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
        resources.ApplyResources(Me.rbChargenExcelSumme, "rbChargenExcelSumme")
        '
        'rbChargenExcelEinzel
        '
        Me.rbChargenExcelEinzel.Image = CType(resources.GetObject("rbChargenExcelEinzel.Image"), System.Drawing.Image)
        Me.rbChargenExcelEinzel.SmallImage = CType(resources.GetObject("rbChargenExcelEinzel.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbChargenExcelEinzel, "rbChargenExcelEinzel")
        '
        'rbChargenExcelDetails
        '
        Me.rbChargenExcelDetails.Image = CType(resources.GetObject("rbChargenExcelDetails.Image"), System.Drawing.Image)
        Me.rbChargenExcelDetails.SmallImage = CType(resources.GetObject("rbChargenExcelDetails.SmallImage"), System.Drawing.Image)
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
        resources.ApplyResources(Me.rbStatRohstoffe, "rbStatRohstoffe")
        '
        'rbStatRohstoffeDetail
        '
        Me.rbStatRohstoffeDetail.Image = CType(resources.GetObject("rbStatRohstoffeDetail.Image"), System.Drawing.Image)
        Me.rbStatRohstoffeDetail.SmallImage = CType(resources.GetObject("rbStatRohstoffeDetail.SmallImage"), System.Drawing.Image)
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
        resources.ApplyResources(Me.rbStatRezepte, "rbStatRezepte")
        '
        'rbArtikel
        '
        Me.rbArtikel.Panels.Add(Me.rpArtikel)
        Me.rbArtikel.Panels.Add(Me.rpArtikelListe)
        Me.rbArtikel.Panels.Add(Me.rpArtikelInfo)
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
        resources.ApplyResources(Me.rbArtikelProduktInfo, "rbArtikelProduktInfo")
        '
        'rbArtikelProduktInfoPrint
        '
        Me.rbArtikelProduktInfoPrint.Image = CType(resources.GetObject("rbArtikelProduktInfoPrint.Image"), System.Drawing.Image)
        Me.rbArtikelProduktInfoPrint.SmallImage = CType(resources.GetObject("rbArtikelProduktInfoPrint.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbArtikelProduktInfoPrint, "rbArtikelProduktInfoPrint")
        '
        'rbRezepte
        '
        resources.ApplyResources(Me.rbRezepte, "rbRezepte")
        '
        'rbRohstoffe
        '
        resources.ApplyResources(Me.rbRohstoffe, "rbRohstoffe")
        '
        'rbUser
        '
        Me.rbUser.Panels.Add(Me.rpUser)
        Me.rbUser.Panels.Add(Me.rpUserGruppen)
        resources.ApplyResources(Me.rbUser, "rbUser")
        '
        'rpUser
        '
        Me.rpUser.Items.Add(Me.rbUserNeu)
        Me.rpUser.Items.Add(Me.RnUserRemove)
        Me.rpUser.Items.Add(Me.rsUser)
        Me.rpUser.Items.Add(Me.rbUserListe)
        resources.ApplyResources(Me.rpUser, "rpUser")
        '
        'rbUserNeu
        '
        Me.rbUserNeu.Image = Global.WinBack.My.Resources.Resources.UserNeu_32x32
        Me.rbUserNeu.SmallImage = CType(resources.GetObject("rbUserNeu.SmallImage"), System.Drawing.Image)
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
        resources.ApplyResources(Me.rbPlanung, "rbPlanung")
        '
        'rbExtra
        '
        resources.ApplyResources(Me.rbExtra, "rbExtra")
        '
        'rbSprache_RU
        '
        Me.rbSprache_RU.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.rbSprache_RU.Image = CType(resources.GetObject("rbSprache_RU.Image"), System.Drawing.Image)
        Me.rbSprache_RU.SmallImage = CType(resources.GetObject("rbSprache_RU.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbSprache_RU, "rbSprache_RU")
        '
        'WinBack
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.rTab)
        Me.Controls.Add(Me.StatusStrip1)
        Me.IsMdiContainer = True
        Me.Name = "WinBack"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As StatusStrip
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
    Friend WithEvents rbSprache_DE As RibbonButton
    Friend WithEvents rbSprache_EN As RibbonButton
    Friend WithEvents rbSprache_FR As RibbonButton
    Friend WithEvents rbSprache_RU As RibbonButton
End Class
