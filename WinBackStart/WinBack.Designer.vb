<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WinBack
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
        'Sprachumschaltung (07.06.2016/JW)
        My.Application.ChangeUICulture(Signum.OrgaSoft.AddIn.wb_Konfig.GetLanguage)

        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinBack))
        Me.Ribbon1 = New System.Windows.Forms.Ribbon()
        Me.RibbonOrbMenuItem1 = New System.Windows.Forms.RibbonOrbMenuItem()
        Me.RibbonSeparator1 = New System.Windows.Forms.RibbonSeparator()
        Me.RibbonOrbOptionButton1 = New System.Windows.Forms.RibbonOrbOptionButton()
        Me.RibbonButton1 = New System.Windows.Forms.RibbonButton()
        Me.rbChargen = New System.Windows.Forms.RibbonTab()
        Me.RibbonPanel1 = New System.Windows.Forms.RibbonPanel()
        Me.rbArtikel = New System.Windows.Forms.RibbonTab()
        Me.rbRezepte = New System.Windows.Forms.RibbonTab()
        Me.rbRohstoffe = New System.Windows.Forms.RibbonTab()
        Me.rbUser = New System.Windows.Forms.RibbonTab()
        Me.rpUser = New System.Windows.Forms.RibbonPanel()
        Me.rbUserNeu = New System.Windows.Forms.RibbonButton()
        Me.rbPasswort = New System.Windows.Forms.RibbonButton()
        Me.rbListeDrucken = New System.Windows.Forms.RibbonButton()
        Me.rbUserGruppen = New System.Windows.Forms.RibbonButton()
        Me.rbLinien = New System.Windows.Forms.RibbonTab()
        Me.rPLinien = New System.Windows.Forms.RibbonPanel()
        Me.rbLinienAdd = New System.Windows.Forms.RibbonButton()
        Me.rbLinienEdit = New System.Windows.Forms.RibbonButton()
        Me.rbLinienDel = New System.Windows.Forms.RibbonButton()
        Me.rbLinienAuto = New System.Windows.Forms.RibbonButton()
        Me.RibbonTab1 = New System.Windows.Forms.RibbonTab()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblVersion = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblNetworkIP = New System.Windows.Forms.ToolStripStatusLabel()
        Me.RibbonPanel2 = New System.Windows.Forms.RibbonPanel()
        Me.rbText = New System.Windows.Forms.RibbonButton()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Ribbon1
        '
        resources.ApplyResources(Me.Ribbon1, "Ribbon1")
        Me.Ribbon1.Minimized = False
        Me.Ribbon1.Name = "Ribbon1"
        '
        '
        '
        Me.Ribbon1.OrbDropDown.BorderRoundness = 8
        Me.Ribbon1.OrbDropDown.Location = CType(resources.GetObject("Ribbon1.OrbDropDown.Location"), System.Drawing.Point)
        Me.Ribbon1.OrbDropDown.MenuItems.Add(Me.RibbonOrbMenuItem1)
        Me.Ribbon1.OrbDropDown.MenuItems.Add(Me.RibbonSeparator1)
        Me.Ribbon1.OrbDropDown.Name = ""
        Me.Ribbon1.OrbDropDown.OptionItems.Add(Me.RibbonOrbOptionButton1)
        Me.Ribbon1.OrbDropDown.Size = CType(resources.GetObject("Ribbon1.OrbDropDown.Size"), System.Drawing.Size)
        Me.Ribbon1.OrbDropDown.TabIndex = CType(resources.GetObject("Ribbon1.OrbDropDown.TabIndex"), Integer)
        Me.Ribbon1.OrbImage = Nothing
        Me.Ribbon1.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2010
        '
        '
        '
        Me.Ribbon1.QuickAcessToolbar.Items.Add(Me.RibbonButton1)
        Me.Ribbon1.RibbonTabFont = New System.Drawing.Font("Trebuchet MS", 9.0!)
        Me.Ribbon1.Tabs.Add(Me.rbChargen)
        Me.Ribbon1.Tabs.Add(Me.rbArtikel)
        Me.Ribbon1.Tabs.Add(Me.rbRezepte)
        Me.Ribbon1.Tabs.Add(Me.rbRohstoffe)
        Me.Ribbon1.Tabs.Add(Me.rbUser)
        Me.Ribbon1.Tabs.Add(Me.rbLinien)
        Me.Ribbon1.Tabs.Add(Me.RibbonTab1)
        Me.Ribbon1.TabsMargin = New System.Windows.Forms.Padding(12, 26, 20, 0)
        Me.Ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Blue
        '
        'RibbonOrbMenuItem1
        '
        Me.RibbonOrbMenuItem1.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.RibbonOrbMenuItem1.Image = CType(resources.GetObject("RibbonOrbMenuItem1.Image"), System.Drawing.Image)
        Me.RibbonOrbMenuItem1.SmallImage = CType(resources.GetObject("RibbonOrbMenuItem1.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbMenuItem1, "RibbonOrbMenuItem1")
        '
        'RibbonOrbOptionButton1
        '
        Me.RibbonOrbOptionButton1.Image = CType(resources.GetObject("RibbonOrbOptionButton1.Image"), System.Drawing.Image)
        Me.RibbonOrbOptionButton1.SmallImage = CType(resources.GetObject("RibbonOrbOptionButton1.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonOrbOptionButton1, "RibbonOrbOptionButton1")
        '
        'RibbonButton1
        '
        Me.RibbonButton1.Image = CType(resources.GetObject("RibbonButton1.Image"), System.Drawing.Image)
        Me.RibbonButton1.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact
        Me.RibbonButton1.SmallImage = CType(resources.GetObject("RibbonButton1.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.RibbonButton1, "RibbonButton1")
        '
        'rbChargen
        '
        Me.rbChargen.Panels.Add(Me.RibbonPanel1)
        resources.ApplyResources(Me.rbChargen, "rbChargen")
        '
        'RibbonPanel1
        '
        resources.ApplyResources(Me.RibbonPanel1, "RibbonPanel1")
        '
        'rbArtikel
        '
        resources.ApplyResources(Me.rbArtikel, "rbArtikel")
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
        resources.ApplyResources(Me.rbUser, "rbUser")
        '
        'rpUser
        '
        Me.rpUser.Items.Add(Me.rbUserNeu)
        Me.rpUser.Items.Add(Me.rbPasswort)
        Me.rpUser.Items.Add(Me.rbListeDrucken)
        Me.rpUser.Items.Add(Me.rbUserGruppen)
        resources.ApplyResources(Me.rpUser, "rpUser")
        '
        'rbUserNeu
        '
        Me.rbUserNeu.Image = Global.WinBack.My.Resources.Resources.UserNeu_32x32
        Me.rbUserNeu.SmallImage = CType(resources.GetObject("rbUserNeu.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbUserNeu, "rbUserNeu")
        '
        'rbPasswort
        '
        Me.rbPasswort.Image = Global.WinBack.My.Resources.Resources.UserPasswd_32x32
        Me.rbPasswort.SmallImage = CType(resources.GetObject("rbPasswort.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbPasswort, "rbPasswort")
        '
        'rbListeDrucken
        '
        Me.rbListeDrucken.Image = Global.WinBack.My.Resources.Resources.UserListe_32x32
        Me.rbListeDrucken.SmallImage = CType(resources.GetObject("rbListeDrucken.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbListeDrucken, "rbListeDrucken")
        '
        'rbUserGruppen
        '
        Me.rbUserGruppen.Image = Global.WinBack.My.Resources.Resources.UserGruppen_32x32
        Me.rbUserGruppen.SmallImage = CType(resources.GetObject("rbUserGruppen.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbUserGruppen, "rbUserGruppen")
        '
        'rbLinien
        '
        Me.rbLinien.Panels.Add(Me.rPLinien)
        resources.ApplyResources(Me.rbLinien, "rbLinien")
        '
        'rPLinien
        '
        Me.rPLinien.Items.Add(Me.rbLinienAdd)
        Me.rPLinien.Items.Add(Me.rbLinienEdit)
        Me.rPLinien.Items.Add(Me.rbLinienDel)
        Me.rPLinien.Items.Add(Me.rbLinienAuto)
        resources.ApplyResources(Me.rPLinien, "rPLinien")
        '
        'rbLinienAdd
        '
        Me.rbLinienAdd.Image = Global.WinBack.My.Resources.Resources.LinienNeu_32x32
        Me.rbLinienAdd.SmallImage = CType(resources.GetObject("rbLinienAdd.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbLinienAdd, "rbLinienAdd")
        '
        'rbLinienEdit
        '
        Me.rbLinienEdit.Image = Global.WinBack.My.Resources.Resources.LinienBearbeiten_32x32
        Me.rbLinienEdit.SmallImage = CType(resources.GetObject("rbLinienEdit.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbLinienEdit, "rbLinienEdit")
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
        'RibbonTab1
        '
        Me.RibbonTab1.Panels.Add(Me.RibbonPanel2)
        resources.ApplyResources(Me.RibbonTab1, "RibbonTab1")
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
        'RibbonPanel2
        '
        Me.RibbonPanel2.Items.Add(Me.rbText)
        resources.ApplyResources(Me.RibbonPanel2, "RibbonPanel2")
        '
        'rbText
        '
        Me.rbText.Image = CType(resources.GetObject("rbText.Image"), System.Drawing.Image)
        Me.rbText.SmallImage = CType(resources.GetObject("rbText.SmallImage"), System.Drawing.Image)
        resources.ApplyResources(Me.rbText, "rbText")
        '
        'WinBack
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Ribbon1)
        Me.IsMdiContainer = True
        Me.Name = "WinBack"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Ribbon1 As Ribbon
    Friend WithEvents rbChargen As RibbonTab
    Friend WithEvents RibbonPanel1 As RibbonPanel
    Friend WithEvents rbArtikel As RibbonTab
    Friend WithEvents rbRezepte As RibbonTab
    Friend WithEvents rbRohstoffe As RibbonTab
    Friend WithEvents rbUser As RibbonTab
    Friend WithEvents rbLinien As RibbonTab
    Friend WithEvents RibbonButton1 As RibbonButton
    Friend WithEvents RibbonOrbOptionButton1 As RibbonOrbOptionButton
    Friend WithEvents RibbonOrbMenuItem1 As RibbonOrbMenuItem
    Friend WithEvents rPLinien As RibbonPanel
    Friend WithEvents rbLinienAdd As RibbonButton
    Friend WithEvents rbLinienEdit As RibbonButton
    Friend WithEvents rbLinienDel As RibbonButton
    Friend WithEvents rbLinienAuto As RibbonButton
    Friend WithEvents RibbonTab1 As RibbonTab
    Friend WithEvents RibbonSeparator1 As RibbonSeparator
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblVersion As ToolStripStatusLabel
    Friend WithEvents lblNetworkIP As ToolStripStatusLabel
    Friend WithEvents rpUser As RibbonPanel
    Friend WithEvents rbUserNeu As RibbonButton
    Friend WithEvents rbPasswort As RibbonButton
    Friend WithEvents rbListeDrucken As RibbonButton
    Friend WithEvents rbUserGruppen As RibbonButton
    Friend WithEvents RibbonPanel2 As RibbonPanel
    Friend WithEvents rbText As RibbonButton
End Class
