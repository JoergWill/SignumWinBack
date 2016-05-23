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
        Me.RibbonButton1 = New System.Windows.Forms.RibbonButton()
        Me.RibbonOrbOptionButton1 = New System.Windows.Forms.RibbonOrbOptionButton()
        Me.RibbonOrbMenuItem1 = New System.Windows.Forms.RibbonOrbMenuItem()
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
        Me.Ribbon1.OrbDropDown.Name = ""
        Me.Ribbon1.OrbDropDown.OptionItems.Add(Me.RibbonOrbOptionButton1)
        Me.Ribbon1.OrbDropDown.Size = New System.Drawing.Size(527, 116)
        Me.Ribbon1.OrbDropDown.TabIndex = 0
        Me.Ribbon1.OrbImage = Nothing
        Me.Ribbon1.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2010
        '
        '
        '
        Me.Ribbon1.QuickAcessToolbar.Items.Add(Me.RibbonButton1)
        Me.Ribbon1.RibbonTabFont = New System.Drawing.Font("Trebuchet MS", 9.0!)
        Me.Ribbon1.Size = New System.Drawing.Size(781, 138)
        Me.Ribbon1.TabIndex = 2
        Me.Ribbon1.Tabs.Add(Me.rbChargen)
        Me.Ribbon1.Tabs.Add(Me.rbArtikel)
        Me.Ribbon1.Tabs.Add(Me.rbRezepte)
        Me.Ribbon1.Tabs.Add(Me.rbRohstoffe)
        Me.Ribbon1.Tabs.Add(Me.rbUser)
        Me.Ribbon1.Tabs.Add(Me.rbLinien)
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
        Me.rbLinien.Text = "Linien"
        '
        'RibbonButton1
        '
        Me.RibbonButton1.Image = CType(resources.GetObject("RibbonButton1.Image"), System.Drawing.Image)
        Me.RibbonButton1.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact
        Me.RibbonButton1.SmallImage = CType(resources.GetObject("RibbonButton1.SmallImage"), System.Drawing.Image)
        Me.RibbonButton1.Text = "RibbonButton1"
        '
        'RibbonOrbOptionButton1
        '
        Me.RibbonOrbOptionButton1.Image = CType(resources.GetObject("RibbonOrbOptionButton1.Image"), System.Drawing.Image)
        Me.RibbonOrbOptionButton1.SmallImage = CType(resources.GetObject("RibbonOrbOptionButton1.SmallImage"), System.Drawing.Image)
        Me.RibbonOrbOptionButton1.Text = "RibbonOrbOptionButton1"
        '
        'RibbonOrbMenuItem1
        '
        Me.RibbonOrbMenuItem1.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.RibbonOrbMenuItem1.Image = CType(resources.GetObject("RibbonOrbMenuItem1.Image"), System.Drawing.Image)
        Me.RibbonOrbMenuItem1.SmallImage = CType(resources.GetObject("RibbonOrbMenuItem1.SmallImage"), System.Drawing.Image)
        Me.RibbonOrbMenuItem1.Text = "Datei"
        '
        'WinBack
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 496)
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
End Class
