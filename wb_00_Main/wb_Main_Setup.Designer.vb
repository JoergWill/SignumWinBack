<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Main_Setup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Main_Setup))
        Me.lblAnzahlMandanten = New System.Windows.Forms.Label()
        Me.tbMandanten = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lblUeberschrift = New System.Windows.Forms.Label()
        Me.lblMandantName = New System.Windows.Forms.Label()
        Me.lblOrgaBackAdmin = New System.Windows.Forms.Label()
        Me.tbMandant = New System.Windows.Forms.TextBox()
        Me.tbMandantName = New System.Windows.Forms.TextBox()
        Me.tbOrgaBackAdmin = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BtnScan = New System.Windows.Forms.Button()
        Me.IPListBox = New System.Windows.Forms.CheckedListBox()
        Me.tbMandanten.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblAnzahlMandanten
        '
        Me.lblAnzahlMandanten.AutoSize = True
        Me.lblAnzahlMandanten.Location = New System.Drawing.Point(31, 61)
        Me.lblAnzahlMandanten.Name = "lblAnzahlMandanten"
        Me.lblAnzahlMandanten.Size = New System.Drawing.Size(49, 13)
        Me.lblAnzahlMandanten.TabIndex = 0
        Me.lblAnzahlMandanten.Text = "Mandant"
        '
        'tbMandanten
        '
        Me.tbMandanten.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tbMandanten.Controls.Add(Me.TabPage1)
        Me.tbMandanten.Controls.Add(Me.TabPage2)
        Me.tbMandanten.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tbMandanten.Location = New System.Drawing.Point(0, 350)
        Me.tbMandanten.Name = "tbMandanten"
        Me.tbMandanten.SelectedIndex = 0
        Me.tbMandanten.Size = New System.Drawing.Size(664, 22)
        Me.tbMandanten.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(656, 0)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(656, 0)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'lblUeberschrift
        '
        Me.lblUeberschrift.AutoSize = True
        Me.lblUeberschrift.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUeberschrift.Location = New System.Drawing.Point(12, 9)
        Me.lblUeberschrift.Name = "lblUeberschrift"
        Me.lblUeberschrift.Size = New System.Drawing.Size(195, 24)
        Me.lblUeberschrift.TabIndex = 3
        Me.lblUeberschrift.Text = "WinBack-AddIn-Setup"
        '
        'lblMandantName
        '
        Me.lblMandantName.AutoSize = True
        Me.lblMandantName.Location = New System.Drawing.Point(31, 87)
        Me.lblMandantName.Name = "lblMandantName"
        Me.lblMandantName.Size = New System.Drawing.Size(80, 13)
        Me.lblMandantName.TabIndex = 4
        Me.lblMandantName.Text = "Mandant Name"
        '
        'lblOrgaBackAdmin
        '
        Me.lblOrgaBackAdmin.AutoSize = True
        Me.lblOrgaBackAdmin.Location = New System.Drawing.Point(31, 113)
        Me.lblOrgaBackAdmin.Name = "lblOrgaBackAdmin"
        Me.lblOrgaBackAdmin.Size = New System.Drawing.Size(107, 13)
        Me.lblOrgaBackAdmin.TabIndex = 5
        Me.lblOrgaBackAdmin.Text = "OrgaBack DB-Server"
        '
        'tbMandant
        '
        Me.tbMandant.Location = New System.Drawing.Point(144, 58)
        Me.tbMandant.Name = "tbMandant"
        Me.tbMandant.ReadOnly = True
        Me.tbMandant.Size = New System.Drawing.Size(33, 20)
        Me.tbMandant.TabIndex = 6
        '
        'tbMandantName
        '
        Me.tbMandantName.Location = New System.Drawing.Point(144, 84)
        Me.tbMandantName.Name = "tbMandantName"
        Me.tbMandantName.ReadOnly = True
        Me.tbMandantName.Size = New System.Drawing.Size(159, 20)
        Me.tbMandantName.TabIndex = 7
        '
        'tbOrgaBackAdmin
        '
        Me.tbOrgaBackAdmin.Location = New System.Drawing.Point(144, 110)
        Me.tbOrgaBackAdmin.Name = "tbOrgaBackAdmin"
        Me.tbOrgaBackAdmin.ReadOnly = True
        Me.tbOrgaBackAdmin.Size = New System.Drawing.Size(159, 20)
        Me.tbOrgaBackAdmin.TabIndex = 8
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(144, 179)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(94, 20)
        Me.TextBox1.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 182)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "WinBack MySql-IP"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(144, 213)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(159, 20)
        Me.TextBox2.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 216)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "WinBack Daten"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(144, 239)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(159, 20)
        Me.TextBox3.TabIndex = 14
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(31, 242)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "WinBack Chargen"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(144, 283)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(33, 20)
        Me.TextBox4.TabIndex = 16
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(31, 286)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Gruppe Rohstoffe"
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(144, 309)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(33, 20)
        Me.TextBox5.TabIndex = 18
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(31, 312)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 13)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Gruppe Backwaren"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(144, 136)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.ReadOnly = True
        Me.TextBox6.Size = New System.Drawing.Size(159, 20)
        Me.TextBox6.TabIndex = 20
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(31, 139)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(105, 13)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "OrgaBack Admin-DB"
        '
        'BtnScan
        '
        Me.BtnScan.BackgroundImage = Global.WinBack.My.Resources.Resources.Zoom_32x32
        Me.BtnScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.BtnScan.Location = New System.Drawing.Point(245, 172)
        Me.BtnScan.Name = "BtnScan"
        Me.BtnScan.Size = New System.Drawing.Size(37, 33)
        Me.BtnScan.TabIndex = 21
        Me.BtnScan.UseVisualStyleBackColor = True
        '
        'IPListBox
        '
        Me.IPListBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IPListBox.FormattingEnabled = True
        Me.IPListBox.Location = New System.Drawing.Point(327, 179)
        Me.IPListBox.Name = "IPListBox"
        Me.IPListBox.Size = New System.Drawing.Size(129, 148)
        Me.IPListBox.TabIndex = 22
        '
        'wb_Main_Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(664, 372)
        Me.Controls.Add(Me.IPListBox)
        Me.Controls.Add(Me.BtnScan)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbOrgaBackAdmin)
        Me.Controls.Add(Me.tbMandantName)
        Me.Controls.Add(Me.tbMandant)
        Me.Controls.Add(Me.lblOrgaBackAdmin)
        Me.Controls.Add(Me.lblMandantName)
        Me.Controls.Add(Me.lblUeberschrift)
        Me.Controls.Add(Me.tbMandanten)
        Me.Controls.Add(Me.lblAnzahlMandanten)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "wb_Main_Setup"
        Me.Text = "Setup WinBack-AddIn"
        Me.tbMandanten.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblAnzahlMandanten As Windows.Forms.Label
    Friend WithEvents tbMandanten As Windows.Forms.TabControl
    Friend WithEvents TabPage1 As Windows.Forms.TabPage
    Friend WithEvents TabPage2 As Windows.Forms.TabPage
    Friend WithEvents lblUeberschrift As Windows.Forms.Label
    Friend WithEvents lblMandantName As Windows.Forms.Label
    Friend WithEvents lblOrgaBackAdmin As Windows.Forms.Label
    Friend WithEvents tbMandant As Windows.Forms.TextBox
    Friend WithEvents tbMandantName As Windows.Forms.TextBox
    Friend WithEvents tbOrgaBackAdmin As Windows.Forms.TextBox
    Friend WithEvents TextBox1 As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents TextBox2 As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents TextBox3 As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents TextBox4 As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents TextBox5 As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents TextBox6 As Windows.Forms.TextBox
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents BtnScan As Windows.Forms.Button
    Friend WithEvents IPListBox As Windows.Forms.CheckedListBox
End Class
