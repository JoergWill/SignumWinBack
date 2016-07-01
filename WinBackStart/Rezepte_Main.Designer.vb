<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Rezepte_Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Rezepte_Main))
        Dim DockPanelSkin2 As WeifenLuo.WinFormsUI.Docking.DockPanelSkin = New WeifenLuo.WinFormsUI.Docking.DockPanelSkin()
        Dim AutoHideStripSkin2 As WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin = New WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin()
        Dim DockPanelGradient4 As WeifenLuo.WinFormsUI.Docking.DockPanelGradient = New WeifenLuo.WinFormsUI.Docking.DockPanelGradient()
        Dim TabGradient8 As WeifenLuo.WinFormsUI.Docking.TabGradient = New WeifenLuo.WinFormsUI.Docking.TabGradient()
        Dim DockPaneStripSkin2 As WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin = New WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin()
        Dim DockPaneStripGradient2 As WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient = New WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient()
        Dim TabGradient9 As WeifenLuo.WinFormsUI.Docking.TabGradient = New WeifenLuo.WinFormsUI.Docking.TabGradient()
        Dim DockPanelGradient5 As WeifenLuo.WinFormsUI.Docking.DockPanelGradient = New WeifenLuo.WinFormsUI.Docking.DockPanelGradient()
        Dim TabGradient10 As WeifenLuo.WinFormsUI.Docking.TabGradient = New WeifenLuo.WinFormsUI.Docking.TabGradient()
        Dim DockPaneStripToolWindowGradient2 As WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient = New WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient()
        Dim TabGradient11 As WeifenLuo.WinFormsUI.Docking.TabGradient = New WeifenLuo.WinFormsUI.Docking.TabGradient()
        Dim TabGradient12 As WeifenLuo.WinFormsUI.Docking.TabGradient = New WeifenLuo.WinFormsUI.Docking.TabGradient()
        Dim DockPanelGradient6 As WeifenLuo.WinFormsUI.Docking.DockPanelGradient = New WeifenLuo.WinFormsUI.Docking.DockPanelGradient()
        Dim TabGradient13 As WeifenLuo.WinFormsUI.Docking.TabGradient = New WeifenLuo.WinFormsUI.Docking.TabGradient()
        Dim TabGradient14 As WeifenLuo.WinFormsUI.Docking.TabGradient = New WeifenLuo.WinFormsUI.Docking.TabGradient()
        Me.DockPanel = New WeifenLuo.WinFormsUI.Docking.DockPanel()
        Me.SuspendLayout()
        '
        'DockPanel
        '
        resources.ApplyResources(Me.DockPanel, "DockPanel")
        Me.DockPanel.BackColor = System.Drawing.Color.Gainsboro
        Me.DockPanel.DockBackColor = System.Drawing.Color.Gainsboro
        Me.DockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow
        Me.DockPanel.Name = "DockPanel"
        DockPanelGradient4.EndColor = System.Drawing.SystemColors.ControlLight
        DockPanelGradient4.StartColor = System.Drawing.SystemColors.ControlLight
        AutoHideStripSkin2.DockStripGradient = DockPanelGradient4
        TabGradient8.EndColor = System.Drawing.SystemColors.Control
        TabGradient8.StartColor = System.Drawing.SystemColors.Control
        TabGradient8.TextColor = System.Drawing.SystemColors.ControlDarkDark
        AutoHideStripSkin2.TabGradient = TabGradient8
        AutoHideStripSkin2.TextFont = New System.Drawing.Font("Segoe UI", 9.0!)
        DockPanelSkin2.AutoHideStripSkin = AutoHideStripSkin2
        TabGradient9.EndColor = System.Drawing.SystemColors.ControlLightLight
        TabGradient9.StartColor = System.Drawing.SystemColors.ControlLightLight
        TabGradient9.TextColor = System.Drawing.SystemColors.ControlText
        DockPaneStripGradient2.ActiveTabGradient = TabGradient9
        DockPanelGradient5.EndColor = System.Drawing.SystemColors.Control
        DockPanelGradient5.StartColor = System.Drawing.SystemColors.Control
        DockPaneStripGradient2.DockStripGradient = DockPanelGradient5
        TabGradient10.EndColor = System.Drawing.SystemColors.ControlLight
        TabGradient10.StartColor = System.Drawing.SystemColors.ControlLight
        TabGradient10.TextColor = System.Drawing.SystemColors.ControlText
        DockPaneStripGradient2.InactiveTabGradient = TabGradient10
        DockPaneStripSkin2.DocumentGradient = DockPaneStripGradient2
        DockPaneStripSkin2.TextFont = New System.Drawing.Font("Segoe UI", 9.0!)
        TabGradient11.EndColor = System.Drawing.SystemColors.ActiveCaption
        TabGradient11.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        TabGradient11.StartColor = System.Drawing.SystemColors.GradientActiveCaption
        TabGradient11.TextColor = System.Drawing.SystemColors.ActiveCaptionText
        DockPaneStripToolWindowGradient2.ActiveCaptionGradient = TabGradient11
        TabGradient12.EndColor = System.Drawing.SystemColors.Control
        TabGradient12.StartColor = System.Drawing.SystemColors.Control
        TabGradient12.TextColor = System.Drawing.SystemColors.ControlText
        DockPaneStripToolWindowGradient2.ActiveTabGradient = TabGradient12
        DockPanelGradient6.EndColor = System.Drawing.SystemColors.ControlLight
        DockPanelGradient6.StartColor = System.Drawing.SystemColors.ControlLight
        DockPaneStripToolWindowGradient2.DockStripGradient = DockPanelGradient6
        TabGradient13.EndColor = System.Drawing.SystemColors.InactiveCaption
        TabGradient13.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        TabGradient13.StartColor = System.Drawing.SystemColors.GradientInactiveCaption
        TabGradient13.TextColor = System.Drawing.SystemColors.InactiveCaptionText
        DockPaneStripToolWindowGradient2.InactiveCaptionGradient = TabGradient13
        TabGradient14.EndColor = System.Drawing.Color.Transparent
        TabGradient14.StartColor = System.Drawing.Color.Transparent
        TabGradient14.TextColor = System.Drawing.SystemColors.ControlDarkDark
        DockPaneStripToolWindowGradient2.InactiveTabGradient = TabGradient14
        DockPaneStripSkin2.ToolWindowGradient = DockPaneStripToolWindowGradient2
        DockPanelSkin2.DockPaneStripSkin = DockPaneStripSkin2
        Me.DockPanel.Skin = DockPanelSkin2
        '
        'Rezepte_Main
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DockPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Rezepte_Main"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DockPanel As WeifenLuo.WinFormsUI.Docking.DockPanel
End Class
