<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WinBack
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Ribbon1 = New System.Windows.Forms.Ribbon()
        Me.rbChargen = New System.Windows.Forms.RibbonTab()
        Me.RibbonPanel1 = New System.Windows.Forms.RibbonPanel()
        Me.rbArtikel = New System.Windows.Forms.RibbonTab()
        Me.rbRezepte = New System.Windows.Forms.RibbonTab()
        Me.rbRohstoffe = New System.Windows.Forms.RibbonTab()
        Me.rbUser = New System.Windows.Forms.RibbonTab()
        Me.rbLinien = New System.Windows.Forms.RibbonTab()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 159)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(98, 37)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Artikel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(136, 159)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(98, 37)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Linien"
        Me.Button2.UseVisualStyleBackColor = True
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
        Me.Ribbon1.OrbDropDown.Name = ""
        Me.Ribbon1.OrbDropDown.Size = New System.Drawing.Size(527, 72)
        Me.Ribbon1.OrbDropDown.TabIndex = 0
        Me.Ribbon1.OrbImage = Nothing
        Me.Ribbon1.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2010
        Me.Ribbon1.RibbonTabFont = New System.Drawing.Font("Trebuchet MS", 9.0!)
        Me.Ribbon1.Size = New System.Drawing.Size(781, 131)
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
        'WinBack
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 496)
        Me.Controls.Add(Me.Ribbon1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "WinBack"
        Me.Text = "WinBack - UI-Test"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Ribbon1 As Ribbon
    Friend WithEvents rbChargen As RibbonTab
    Friend WithEvents RibbonPanel1 As RibbonPanel
    Friend WithEvents rbArtikel As RibbonTab
    Friend WithEvents rbRezepte As RibbonTab
    Friend WithEvents rbRohstoffe As RibbonTab
    Friend WithEvents rbUser As RibbonTab
    Friend WithEvents rbLinien As RibbonTab
End Class
