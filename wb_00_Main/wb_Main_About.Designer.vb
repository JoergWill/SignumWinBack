Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Wb_Main_About
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

    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents LabelProductName As System.Windows.Forms.Label
    Friend WithEvents LabelVersion As System.Windows.Forms.Label

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Wb_Main_About))
        TableLayoutPanel = New TableLayoutPanel()
        BtnHelp = New Button()
        LabelOnline = New Label()
        BtnUpdate = New Button()
        BtnColors = New Button()
        LogoPictureBox = New PictureBox()
        LabelProductName = New Label()
        LabelVersion = New Label()
        LabelCopyright = New Label()
        BtnOK = New Button()
        lbVersionsHistorie = New LinkLabel()
        TableLayoutPanel.SuspendLayout()
        CType(LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TableLayoutPanel
        ' 
        TableLayoutPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TableLayoutPanel.ColumnCount = 5
        TableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 44.5568F))
        TableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 13.924F))
        TableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 13.65082F))
        TableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 13.94438F))
        TableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 13.924F))
        TableLayoutPanel.Controls.Add(BtnHelp, 0, 6)
        TableLayoutPanel.Controls.Add(LabelOnline, 3, 5)
        TableLayoutPanel.Controls.Add(BtnUpdate, 3, 6)
        TableLayoutPanel.Controls.Add(BtnColors, 0, 1)
        TableLayoutPanel.Controls.Add(LogoPictureBox, 0, 0)
        TableLayoutPanel.Controls.Add(LabelProductName, 2, 2)
        TableLayoutPanel.Controls.Add(LabelVersion, 2, 1)
        TableLayoutPanel.Controls.Add(LabelCopyright, 1, 3)
        TableLayoutPanel.Controls.Add(BtnOK, 4, 6)
        TableLayoutPanel.Controls.Add(lbVersionsHistorie, 1, 4)
        TableLayoutPanel.Location = New System.Drawing.Point(0, 2)
        TableLayoutPanel.Name = "TableLayoutPanel"
        TableLayoutPanel.RowCount = 7
        TableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 4.996912F))
        TableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 4.996912F))
        TableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 4.996912F))
        TableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 4.996912F))
        TableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 65.01052F))
        TableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 5.00004F))
        TableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 10.00179F))
        TableLayoutPanel.Size = New System.Drawing.Size(683, 403)
        TableLayoutPanel.TabIndex = 0
        ' 
        ' BtnHelp
        ' 
        BtnHelp.Dock = DockStyle.Fill
        BtnHelp.Location = New System.Drawing.Point(312, 369)
        BtnHelp.Margin = New Padding(8)
        BtnHelp.Name = "BtnHelp"
        BtnHelp.Size = New System.Drawing.Size(79, 26)
        BtnHelp.TabIndex = 7
        BtnHelp.Text = "Hilfe"
        BtnHelp.UseVisualStyleBackColor = True
        ' 
        ' LabelOnline
        ' 
        TableLayoutPanel.SetColumnSpan(LabelOnline, 2)
        LabelOnline.Dock = DockStyle.Fill
        LabelOnline.Location = New System.Drawing.Point(498, 341)
        LabelOnline.Margin = New Padding(6, 0, 3, 0)
        LabelOnline.MaximumSize = New System.Drawing.Size(0, 17)
        LabelOnline.Name = "LabelOnline"
        LabelOnline.Size = New System.Drawing.Size(182, 17)
        LabelOnline.TabIndex = 6
        LabelOnline.Text = "Online"
        LabelOnline.TextAlign = Drawing.ContentAlignment.MiddleLeft
        LabelOnline.Visible = False
        ' 
        ' BtnUpdate
        ' 
        BtnUpdate.Dock = DockStyle.Fill
        BtnUpdate.Enabled = False
        BtnUpdate.Location = New System.Drawing.Point(500, 369)
        BtnUpdate.Margin = New Padding(8)
        BtnUpdate.Name = "BtnUpdate"
        BtnUpdate.Size = New System.Drawing.Size(79, 26)
        BtnUpdate.TabIndex = 4
        BtnUpdate.Text = "Update"
        BtnUpdate.UseVisualStyleBackColor = True
        ' 
        ' BtnColors
        ' 
        TableLayoutPanel.SetColumnSpan(BtnColors, 2)
        BtnColors.Dock = DockStyle.Fill
        BtnColors.Location = New System.Drawing.Point(312, 28)
        BtnColors.Margin = New Padding(8)
        BtnColors.Name = "BtnColors"
        TableLayoutPanel.SetRowSpan(BtnColors, 2)
        BtnColors.Size = New System.Drawing.Size(172, 24)
        BtnColors.TabIndex = 3
        BtnColors.Text = "Farb-Themen"
        BtnColors.UseVisualStyleBackColor = True
        ' 
        ' LogoPictureBox
        ' 
        LogoPictureBox.Dock = DockStyle.Fill
        LogoPictureBox.Image = CType(resources.GetObject("LogoPictureBox.Image"), Drawing.Image)
        LogoPictureBox.Location = New System.Drawing.Point(3, 3)
        LogoPictureBox.Name = "LogoPictureBox"
        TableLayoutPanel.SetRowSpan(LogoPictureBox, 7)
        LogoPictureBox.Size = New System.Drawing.Size(298, 397)
        LogoPictureBox.SizeMode = PictureBoxSizeMode.Zoom
        LogoPictureBox.TabIndex = 0
        LogoPictureBox.TabStop = False
        ' 
        ' LabelProductName
        ' 
        TableLayoutPanel.SetColumnSpan(LabelProductName, 2)
        LabelProductName.Dock = DockStyle.Fill
        LabelProductName.Location = New System.Drawing.Point(498, 40)
        LabelProductName.Margin = New Padding(6, 0, 3, 0)
        LabelProductName.MaximumSize = New System.Drawing.Size(0, 17)
        LabelProductName.Name = "LabelProductName"
        LabelProductName.Size = New System.Drawing.Size(182, 17)
        LabelProductName.TabIndex = 0
        LabelProductName.Text = "Produktname"
        LabelProductName.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' LabelVersion
        ' 
        TableLayoutPanel.SetColumnSpan(LabelVersion, 2)
        LabelVersion.Dock = DockStyle.Fill
        LabelVersion.Location = New System.Drawing.Point(498, 20)
        LabelVersion.Margin = New Padding(6, 0, 3, 0)
        LabelVersion.MaximumSize = New System.Drawing.Size(0, 17)
        LabelVersion.Name = "LabelVersion"
        LabelVersion.Size = New System.Drawing.Size(182, 17)
        LabelVersion.TabIndex = 0
        LabelVersion.Text = "Version"
        LabelVersion.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' LabelCopyright
        ' 
        TableLayoutPanel.SetColumnSpan(LabelCopyright, 4)
        LabelCopyright.Dock = DockStyle.Fill
        LabelCopyright.Location = New System.Drawing.Point(310, 60)
        LabelCopyright.Margin = New Padding(6, 0, 3, 0)
        LabelCopyright.MaximumSize = New System.Drawing.Size(0, 17)
        LabelCopyright.Name = "LabelCopyright"
        LabelCopyright.Size = New System.Drawing.Size(370, 17)
        LabelCopyright.TabIndex = 1
        LabelCopyright.Text = "Copyright"
        LabelCopyright.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' BtnOK
        ' 
        BtnOK.Dock = DockStyle.Fill
        BtnOK.Location = New System.Drawing.Point(595, 369)
        BtnOK.Margin = New Padding(8)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New System.Drawing.Size(80, 26)
        BtnOK.TabIndex = 2
        BtnOK.Text = "OK"
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' lbVersionsHistorie
        ' 
        lbVersionsHistorie.AutoSize = True
        lbVersionsHistorie.BackColor = Drawing.Color.Transparent
        TableLayoutPanel.SetColumnSpan(lbVersionsHistorie, 4)
        lbVersionsHistorie.Dock = DockStyle.Fill
        lbVersionsHistorie.Location = New System.Drawing.Point(312, 88)
        lbVersionsHistorie.Margin = New Padding(8)
        lbVersionsHistorie.Name = "lbVersionsHistorie"
        lbVersionsHistorie.Size = New System.Drawing.Size(363, 245)
        lbVersionsHistorie.TabIndex = 5
        ' 
        ' Wb_Main_About
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(682, 406)
        Controls.Add(TableLayoutPanel)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "Wb_Main_About"
        Padding = New Padding(9)
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "OrgaBack Office"
        TopMost = True
        TableLayoutPanel.ResumeLayout(False)
        TableLayoutPanel.PerformLayout()
        CType(LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

    Friend WithEvents LabelCopyright As Label
    Friend WithEvents BtnOK As Button
    Friend WithEvents BtnUpdate As Button
    Friend WithEvents BtnColors As Button
    Friend WithEvents lbVersionsHistorie As LinkLabel
    Friend WithEvents LabelOnline As Label
    Friend WithEvents BtnHelp As Button
End Class
