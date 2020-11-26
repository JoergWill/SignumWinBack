Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Planung_Teiler
    Inherits DockContent
    'Inherits System.Windows.Forms.Form

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
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.grpChargenTeiler = New System.Windows.Forms.GroupBox()
        Me.cbChargenTeiler = New WinBack.wb_CheckedListBox()
        Me.gbTeigeOptimieren = New System.Windows.Forms.GroupBox()
        Me.cbTeigOptimierung = New WinBack.wb_CheckedListBox()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.grpChargenTeiler.SuspendLayout()
        Me.gbTeigeOptimieren.SuspendLayout()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.grpChargenTeiler)
        Me.FlowLayoutPanel1.Controls.Add(Me.gbTeigeOptimieren)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(236, 349)
        Me.FlowLayoutPanel1.TabIndex = 2
        '
        'grpChargenTeiler
        '
        Me.grpChargenTeiler.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpChargenTeiler.Controls.Add(Me.cbChargenTeiler)
        Me.grpChargenTeiler.Location = New System.Drawing.Point(3, 3)
        Me.grpChargenTeiler.Name = "grpChargenTeiler"
        Me.grpChargenTeiler.Size = New System.Drawing.Size(214, 157)
        Me.grpChargenTeiler.TabIndex = 1
        Me.grpChargenTeiler.TabStop = False
        Me.grpChargenTeiler.Text = "Chargen-Aufteilung"
        '
        'cbChargenTeiler
        '
        Me.cbChargenTeiler.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbChargenTeiler.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.cbChargenTeiler.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.cbChargenTeiler.CheckOnClick = True
        Me.cbChargenTeiler.FormattingEnabled = True
        Me.cbChargenTeiler.Location = New System.Drawing.Point(15, 31)
        Me.cbChargenTeiler.Name = "cbChargenTeiler"
        Me.cbChargenTeiler.SelIndex = 0
        Me.cbChargenTeiler.Size = New System.Drawing.Size(190, 120)
        Me.cbChargenTeiler.TabIndex = 3
        Me.cbChargenTeiler.TabStop = False
        '
        'gbTeigeOptimieren
        '
        Me.gbTeigeOptimieren.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbTeigeOptimieren.Controls.Add(Me.cbTeigOptimierung)
        Me.gbTeigeOptimieren.Location = New System.Drawing.Point(3, 166)
        Me.gbTeigeOptimieren.Name = "gbTeigeOptimieren"
        Me.gbTeigeOptimieren.Size = New System.Drawing.Size(214, 157)
        Me.gbTeigeOptimieren.TabIndex = 2
        Me.gbTeigeOptimieren.TabStop = False
        Me.gbTeigeOptimieren.Text = "Teige zusammenfassen"
        '
        'cbTeigOptimierung
        '
        Me.cbTeigOptimierung.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbTeigOptimierung.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.cbTeigOptimierung.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.cbTeigOptimierung.CheckOnClick = True
        Me.cbTeigOptimierung.FormattingEnabled = True
        Me.cbTeigOptimierung.Location = New System.Drawing.Point(15, 31)
        Me.cbTeigOptimierung.Name = "cbTeigOptimierung"
        Me.cbTeigOptimierung.SelIndex = 0
        Me.cbTeigOptimierung.Size = New System.Drawing.Size(193, 150)
        Me.cbTeigOptimierung.TabIndex = 3
        Me.cbTeigOptimierung.TabStop = False
        '
        'wb_Planung_Teiler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(236, 349)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Name = "wb_Planung_Teiler"
        Me.Text = "Einstellungen Optimierung"
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.grpChargenTeiler.ResumeLayout(False)
        Me.gbTeigeOptimieren.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FlowLayoutPanel1 As Windows.Forms.FlowLayoutPanel
    Friend WithEvents grpChargenTeiler As Windows.Forms.GroupBox
    Friend WithEvents cbChargenTeiler As wb_CheckedListBox
    Friend WithEvents gbTeigeOptimieren As Windows.Forms.GroupBox
    Friend WithEvents cbTeigOptimierung As wb_CheckedListBox
End Class
