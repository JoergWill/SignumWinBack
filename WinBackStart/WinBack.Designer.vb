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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinBack))
        Me.Ribbon1 = New System.Windows.Forms.Ribbon()
        Me.rbChargen = New System.Windows.Forms.RibbonTab()
        Me.RibbonPanel1 = New System.Windows.Forms.RibbonPanel()
        Me.rbArtikel = New System.Windows.Forms.RibbonTab()
        Me.rbRezepte = New System.Windows.Forms.RibbonTab()
        Me.rbRohstoffe = New System.Windows.Forms.RibbonTab()
        Me.rbUser = New System.Windows.Forms.RibbonTab()
        Me.rbLinien = New System.Windows.Forms.RibbonTab()
        Me.RibbonPanel2 = New System.Windows.Forms.RibbonPanel()
        Me.RibbonTab1 = New System.Windows.Forms.RibbonTab()
        Me.RibbonOrbMenuItem1 = New System.Windows.Forms.RibbonOrbMenuItem()
        Me.RibbonOrbOptionButton1 = New System.Windows.Forms.RibbonOrbOptionButton()
        Me.RibbonButton1 = New System.Windows.Forms.RibbonButton()
        Me.rbLinienAdd = New System.Windows.Forms.RibbonButton()
        Me.rbLinienEdit = New System.Windows.Forms.RibbonButton()
        Me.rbLinienDel = New System.Windows.Forms.RibbonButton()
        Me.rbLinienAuto = New System.Windows.Forms.RibbonButton()
        Me.RibbonSeparator1 = New System.Windows.Forms.RibbonSeparator()
        Me.SuspendLayout()
        '
        'Ribbon1
        '
        Me.Ribbon1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Ribbon1.Location = New System.Drawing.Point(0, 0)
        Me.Ribbon1.Minimized = False
        Me.Ribbon1.Name = "Ribbon1"
        '
        '
        '
        Me.Ribbon1.OrbDropDown.BorderRoundness = 8
        Me.Ribbon1.OrbDropDown.Location = New System.Drawing.Point(0, 0)
        Me.Ribbon1.OrbDropDown.MenuItems.Add(Me.RibbonOrbMenuItem1)
        Me.Ribbon1.OrbDropDown.MenuItems.Add(Me.RibbonSeparator1)
        Me.Ribbon1.OrbDropDown.Name = ""
        Me.Ribbon1.OrbDropDown.OptionItems.Add(Me.RibbonOrbOptionButton1)
        Me.Ribbon1.OrbDropDown.Size = New System.Drawing.Size(527, 119)
        Me.Ribbon1.OrbDropDown.TabIndex = 0
        Me.Ribbon1.OrbImage = Nothing
        Me.Ribbon1.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2010
        '
        '
        '
        Me.Ribbon1.QuickAcessToolbar.Items.Add(Me.RibbonButton1)
        Me.Ribbon1.RibbonTabFont = New System.Drawing.Font("Trebuchet MS", 9.0!)
        Me.Ribbon1.Size = New System.Drawing.Size(1059, 145)
        Me.Ribbon1.TabIndex = 2
        Me.Ribbon1.Tabs.Add(Me.rbChargen)
        Me.Ribbon1.Tabs.Add(Me.rbArtikel)
        Me.Ribbon1.Tabs.Add(Me.rbRezepte)
        Me.Ribbon1.Tabs.Add(Me.rbRohstoffe)
        Me.Ribbon1.Tabs.Add(Me.rbUser)
        Me.Ribbon1.Tabs.Add(Me.rbLinien)
        Me.Ribbon1.Tabs.Add(Me.RibbonTab1)
        Me.Ribbon1.TabsMargin = New System.Windows.Forms.Padding(12, 26, 20, 0)
        Me.Ribbon1.Text = "Ribbon1"
        Me.Ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Blue
        '
        'rbChargen
        '
        Me.rbChargen.Panels.Add(Me.RibbonPanel1)
        Me.rbChargen.Text = "Chargen"
        '
        'RibbonPanel1
        '
        Me.RibbonPanel1.Text = "RibbonPanel1"
        '
        'rbArtikel
        '
        Me.rbArtikel.Text = "Artikel"
        '
        'rbRezepte
        '
        Me.rbRezepte.Text = "Rezepte"
        '
        'rbRohstoffe
        '
        Me.rbRohstoffe.Text = "Rohstoffe"
        '
        'rbUser
        '
        Me.rbUser.Text = "Benutzer"
        '
        'rbLinien
        '
        Me.rbLinien.Panels.Add(Me.RibbonPanel2)
        Me.rbLinien.Text = "Linien"
        '
        'RibbonPanel2
        '
        Me.RibbonPanel2.Items.Add(Me.rbLinienAdd)
        Me.RibbonPanel2.Items.Add(Me.rbLinienEdit)
        Me.RibbonPanel2.Items.Add(Me.rbLinienDel)
        Me.RibbonPanel2.Items.Add(Me.rbLinienAuto)
        Me.RibbonPanel2.Text = "Linien"
        '
        'RibbonTab1
        '
        Me.RibbonTab1.Text = "RibbonTab1"
        '
        'RibbonOrbMenuItem1
        '
        Me.RibbonOrbMenuItem1.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.RibbonOrbMenuItem1.Image = CType(resources.GetObject("RibbonOrbMenuItem1.Image"), System.Drawing.Image)
        Me.RibbonOrbMenuItem1.SmallImage = CType(resources.GetObject("RibbonOrbMenuItem1.SmallImage"), System.Drawing.Image)
        Me.RibbonOrbMenuItem1.Text = "Datei"
        '
        'RibbonOrbOptionButton1
        '
        Me.RibbonOrbOptionButton1.Image = CType(resources.GetObject("RibbonOrbOptionButton1.Image"), System.Drawing.Image)
        Me.RibbonOrbOptionButton1.SmallImage = CType(resources.GetObject("RibbonOrbOptionButton1.SmallImage"), System.Drawing.Image)
        Me.RibbonOrbOptionButton1.Text = "RibbonOrbOptionButton1"
        '
        'RibbonButton1
        '
        Me.RibbonButton1.Image = CType(resources.GetObject("RibbonButton1.Image"), System.Drawing.Image)
        Me.RibbonButton1.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact
        Me.RibbonButton1.SmallImage = CType(resources.GetObject("RibbonButton1.SmallImage"), System.Drawing.Image)
        Me.RibbonButton1.Text = "RibbonButton1"
        '
        'rbLinienAdd
        '
        Me.rbLinienAdd.Image = Global.WinBack.My.Resources.Resources.LinienNeu_32x32
        Me.rbLinienAdd.SmallImage = CType(resources.GetObject("rbLinienAdd.SmallImage"), System.Drawing.Image)
        Me.rbLinienAdd.Text = "Linie neu"
        Me.rbLinienAdd.ToolTip = "Vnc-Viewer für eine neue Linie anlegen"
        '
        'rbLinienEdit
        '
        Me.rbLinienEdit.Image = Global.WinBack.My.Resources.Resources.LinienBearbeiten_32x32
        Me.rbLinienEdit.SmallImage = CType(resources.GetObject("rbLinienEdit.SmallImage"), System.Drawing.Image)
        Me.rbLinienEdit.Text = "Linie bearbeiten"
        '
        'rbLinienDel
        '
        Me.rbLinienDel.Image = Global.WinBack.My.Resources.Resources.LinienLoeschen_32x32
        Me.rbLinienDel.SmallImage = CType(resources.GetObject("rbLinienDel.SmallImage"), System.Drawing.Image)
        Me.rbLinienDel.Text = "Linie löschen"
        '
        'rbLinienAuto
        '
        Me.rbLinienAuto.Image = Global.WinBack.My.Resources.Resources.LinienAutoInstall_32x32
        Me.rbLinienAuto.SmallImage = CType(resources.GetObject("rbLinienAuto.SmallImage"), System.Drawing.Image)
        Me.rbLinienAuto.Text = "Auto Install"
        '
        'WinBack
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1059, 646)
        Me.Controls.Add(Me.Ribbon1)
        Me.IsMdiContainer = True
        Me.Name = "WinBack"
        Me.Text = "WinBack - UI-Test"
        Me.ResumeLayout(False)

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
    Friend WithEvents RibbonPanel2 As RibbonPanel
    Friend WithEvents rbLinienAdd As RibbonButton
    Friend WithEvents rbLinienEdit As RibbonButton
    Friend WithEvents rbLinienDel As RibbonButton
    Friend WithEvents rbLinienAuto As RibbonButton
    Friend WithEvents RibbonTab1 As RibbonTab
    Friend WithEvents RibbonSeparator1 As RibbonSeparator
End Class
