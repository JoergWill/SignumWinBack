Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_UpdateDatabase
    Inherits DockContent
    'Inherits System.Windows.Forms.Form

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
        Me.lblWinBackOffice = New System.Windows.Forms.Label()
        Me.lblWinBackDataBase = New System.Windows.Forms.Label()
        Me.lblOrgaBackDataBase = New System.Windows.Forms.Label()
        Me.lblOrgaBack = New System.Windows.Forms.Label()
        Me.tbWinBackOffice = New System.Windows.Forms.TextBox()
        Me.tbWinBackDatabase = New System.Windows.Forms.TextBox()
        Me.tbOrgaBack = New System.Windows.Forms.TextBox()
        Me.tbOrgaBackDataBase = New System.Windows.Forms.TextBox()
        Me.BtnUpdateWinBackDataBase = New System.Windows.Forms.Button()
        Me.lblUpdateFilesCount = New System.Windows.Forms.Label()
        Me.pbData = New MetroFramework.Controls.MetroProgressBar()
        Me.pbFiles = New MetroFramework.Controls.MetroProgressBar()
        Me.tbWinbackKundeName = New System.Windows.Forms.TextBox()
        Me.tbMsSQLMain = New System.Windows.Forms.TextBox()
        Me.tbOrgaBackDBServer = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbOrgaBackAdminDB = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbOrgaBackMainDB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblWinBackOffice
        '
        Me.lblWinBackOffice.AutoSize = True
        Me.lblWinBackOffice.Location = New System.Drawing.Point(12, 9)
        Me.lblWinBackOffice.Name = "lblWinBackOffice"
        Me.lblWinBackOffice.Size = New System.Drawing.Size(120, 13)
        Me.lblWinBackOffice.TabIndex = 0
        Me.lblWinBackOffice.Text = "Version WinBack-Office"
        '
        'lblWinBackDataBase
        '
        Me.lblWinBackDataBase.AutoSize = True
        Me.lblWinBackDataBase.Location = New System.Drawing.Point(12, 31)
        Me.lblWinBackDataBase.Name = "lblWinBackDataBase"
        Me.lblWinBackDataBase.Size = New System.Drawing.Size(145, 13)
        Me.lblWinBackDataBase.TabIndex = 1
        Me.lblWinBackDataBase.Text = "Version WinBack Datenbank"
        '
        'lblOrgaBackDataBase
        '
        Me.lblOrgaBackDataBase.AutoSize = True
        Me.lblOrgaBackDataBase.Location = New System.Drawing.Point(12, 86)
        Me.lblOrgaBackDataBase.Name = "lblOrgaBackDataBase"
        Me.lblOrgaBackDataBase.Size = New System.Drawing.Size(149, 13)
        Me.lblOrgaBackDataBase.TabIndex = 3
        Me.lblOrgaBackDataBase.Text = "Version OrgaBack Datenbank"
        '
        'lblOrgaBack
        '
        Me.lblOrgaBack.AutoSize = True
        Me.lblOrgaBack.Location = New System.Drawing.Point(12, 65)
        Me.lblOrgaBack.Name = "lblOrgaBack"
        Me.lblOrgaBack.Size = New System.Drawing.Size(93, 13)
        Me.lblOrgaBack.TabIndex = 2
        Me.lblOrgaBack.Text = "Version OrgaBack"
        '
        'tbWinBackOffice
        '
        Me.tbWinBackOffice.BackColor = System.Drawing.SystemColors.Control
        Me.tbWinBackOffice.ForeColor = System.Drawing.Color.DarkGray
        Me.tbWinBackOffice.Location = New System.Drawing.Point(165, 6)
        Me.tbWinBackOffice.Name = "tbWinBackOffice"
        Me.tbWinBackOffice.Size = New System.Drawing.Size(123, 20)
        Me.tbWinBackOffice.TabIndex = 5
        Me.tbWinBackOffice.TabStop = False
        '
        'tbWinBackDatabase
        '
        Me.tbWinBackDatabase.BackColor = System.Drawing.SystemColors.Control
        Me.tbWinBackDatabase.ForeColor = System.Drawing.Color.DarkGray
        Me.tbWinBackDatabase.Location = New System.Drawing.Point(165, 28)
        Me.tbWinBackDatabase.Name = "tbWinBackDatabase"
        Me.tbWinBackDatabase.Size = New System.Drawing.Size(123, 20)
        Me.tbWinBackDatabase.TabIndex = 6
        Me.tbWinBackDatabase.TabStop = False
        '
        'tbOrgaBack
        '
        Me.tbOrgaBack.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBack.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBack.Location = New System.Drawing.Point(165, 62)
        Me.tbOrgaBack.Name = "tbOrgaBack"
        Me.tbOrgaBack.Size = New System.Drawing.Size(123, 20)
        Me.tbOrgaBack.TabIndex = 7
        Me.tbOrgaBack.TabStop = False
        '
        'tbOrgaBackDataBase
        '
        Me.tbOrgaBackDataBase.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBackDataBase.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBackDataBase.Location = New System.Drawing.Point(165, 84)
        Me.tbOrgaBackDataBase.Name = "tbOrgaBackDataBase"
        Me.tbOrgaBackDataBase.Size = New System.Drawing.Size(123, 20)
        Me.tbOrgaBackDataBase.TabIndex = 8
        Me.tbOrgaBackDataBase.TabStop = False
        '
        'BtnUpdateWinBackDataBase
        '
        Me.BtnUpdateWinBackDataBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnUpdateWinBackDataBase.Image = Global.WinBack.My.Resources.Resources.UpdateDataBase_32x32
        Me.BtnUpdateWinBackDataBase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnUpdateWinBackDataBase.Location = New System.Drawing.Point(12, 382)
        Me.BtnUpdateWinBackDataBase.Name = "BtnUpdateWinBackDataBase"
        Me.BtnUpdateWinBackDataBase.Size = New System.Drawing.Size(145, 51)
        Me.BtnUpdateWinBackDataBase.TabIndex = 9
        Me.BtnUpdateWinBackDataBase.Text = "Update WinBack Datenbank"
        Me.BtnUpdateWinBackDataBase.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnUpdateWinBackDataBase.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.BtnUpdateWinBackDataBase.UseVisualStyleBackColor = True
        '
        'lblUpdateFilesCount
        '
        Me.lblUpdateFilesCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUpdateFilesCount.AutoSize = True
        Me.lblUpdateFilesCount.Location = New System.Drawing.Point(163, 382)
        Me.lblUpdateFilesCount.Name = "lblUpdateFilesCount"
        Me.lblUpdateFilesCount.Size = New System.Drawing.Size(108, 13)
        Me.lblUpdateFilesCount.TabIndex = 10
        Me.lblUpdateFilesCount.Text = "xx Updates gefunden"
        '
        'pbData
        '
        Me.pbData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pbData.Location = New System.Drawing.Point(166, 419)
        Me.pbData.Name = "pbData"
        Me.pbData.ProgressBarStyle = System.Windows.Forms.ProgressBarStyle.Blocks
        Me.pbData.Size = New System.Drawing.Size(122, 14)
        Me.pbData.TabIndex = 11
        '
        'pbFiles
        '
        Me.pbFiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pbFiles.Location = New System.Drawing.Point(166, 399)
        Me.pbFiles.Name = "pbFiles"
        Me.pbFiles.ProgressBarStyle = System.Windows.Forms.ProgressBarStyle.Blocks
        Me.pbFiles.Size = New System.Drawing.Size(122, 15)
        Me.pbFiles.TabIndex = 12
        '
        'tbWinbackKundeName
        '
        Me.tbWinbackKundeName.BackColor = System.Drawing.SystemColors.Control
        Me.tbWinbackKundeName.ForeColor = System.Drawing.Color.DarkGray
        Me.tbWinbackKundeName.Location = New System.Drawing.Point(294, 28)
        Me.tbWinbackKundeName.Name = "tbWinbackKundeName"
        Me.tbWinbackKundeName.Size = New System.Drawing.Size(123, 20)
        Me.tbWinbackKundeName.TabIndex = 13
        Me.tbWinbackKundeName.TabStop = False
        '
        'tbMsSQLMain
        '
        Me.tbMsSQLMain.BackColor = System.Drawing.SystemColors.Control
        Me.tbMsSQLMain.ForeColor = System.Drawing.Color.DarkGray
        Me.tbMsSQLMain.Location = New System.Drawing.Point(294, 83)
        Me.tbMsSQLMain.Name = "tbMsSQLMain"
        Me.tbMsSQLMain.Size = New System.Drawing.Size(123, 20)
        Me.tbMsSQLMain.TabIndex = 14
        Me.tbMsSQLMain.TabStop = False
        '
        'tbOrgaBackDBServer
        '
        Me.tbOrgaBackDBServer.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBackDBServer.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBackDBServer.Location = New System.Drawing.Point(165, 110)
        Me.tbOrgaBackDBServer.Name = "tbOrgaBackDBServer"
        Me.tbOrgaBackDBServer.Size = New System.Drawing.Size(252, 20)
        Me.tbOrgaBackDBServer.TabIndex = 16
        Me.tbOrgaBackDBServer.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 113)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "DB-Server OrgaBack"
        '
        'tbOrgaBackAdminDB
        '
        Me.tbOrgaBackAdminDB.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBackAdminDB.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBackAdminDB.Location = New System.Drawing.Point(165, 132)
        Me.tbOrgaBackAdminDB.Name = "tbOrgaBackAdminDB"
        Me.tbOrgaBackAdminDB.Size = New System.Drawing.Size(123, 20)
        Me.tbOrgaBackAdminDB.TabIndex = 18
        Me.tbOrgaBackAdminDB.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 135)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Admin-DB OrgaBack"
        '
        'tbOrgaBackMainDB
        '
        Me.tbOrgaBackMainDB.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBackMainDB.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBackMainDB.Location = New System.Drawing.Point(165, 154)
        Me.tbOrgaBackMainDB.Name = "tbOrgaBackMainDB"
        Me.tbOrgaBackMainDB.Size = New System.Drawing.Size(123, 20)
        Me.tbOrgaBackMainDB.TabIndex = 20
        Me.tbOrgaBackMainDB.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 157)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 13)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Main-DB OrgaBack"
        '
        'wb_Admin_UpdateDatabase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(471, 445)
        Me.Controls.Add(Me.tbOrgaBackMainDB)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbOrgaBackAdminDB)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbOrgaBackDBServer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbMsSQLMain)
        Me.Controls.Add(Me.tbWinbackKundeName)
        Me.Controls.Add(Me.pbFiles)
        Me.Controls.Add(Me.pbData)
        Me.Controls.Add(Me.lblUpdateFilesCount)
        Me.Controls.Add(Me.BtnUpdateWinBackDataBase)
        Me.Controls.Add(Me.tbOrgaBackDataBase)
        Me.Controls.Add(Me.tbOrgaBack)
        Me.Controls.Add(Me.tbWinBackDatabase)
        Me.Controls.Add(Me.tbWinBackOffice)
        Me.Controls.Add(Me.lblOrgaBackDataBase)
        Me.Controls.Add(Me.lblOrgaBack)
        Me.Controls.Add(Me.lblWinBackDataBase)
        Me.Controls.Add(Me.lblWinBackOffice)
        Me.Name = "wb_Admin_UpdateDatabase"
        Me.Text = "Update"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblWinBackOffice As Windows.Forms.Label
    Friend WithEvents lblWinBackDataBase As Windows.Forms.Label
    Friend WithEvents lblOrgaBackDataBase As Windows.Forms.Label
    Friend WithEvents lblOrgaBack As Windows.Forms.Label
    Friend WithEvents tbWinBackOffice As Windows.Forms.TextBox
    Friend WithEvents tbWinBackDatabase As Windows.Forms.TextBox
    Friend WithEvents tbOrgaBack As Windows.Forms.TextBox
    Friend WithEvents tbOrgaBackDataBase As Windows.Forms.TextBox
    Friend WithEvents BtnUpdateWinBackDataBase As Windows.Forms.Button
    Friend WithEvents lblUpdateFilesCount As Windows.Forms.Label
    Friend WithEvents pbData As MetroFramework.Controls.MetroProgressBar
    Friend WithEvents pbFiles As MetroFramework.Controls.MetroProgressBar
    Friend WithEvents tbWinbackKundeName As Windows.Forms.TextBox
    Friend WithEvents tbMsSQLMain As Windows.Forms.TextBox
    Friend WithEvents tbOrgaBackDBServer As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents tbOrgaBackAdminDB As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tbOrgaBackMainDB As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
End Class
