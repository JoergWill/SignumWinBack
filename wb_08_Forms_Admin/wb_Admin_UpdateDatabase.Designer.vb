﻿Imports WeifenLuo.WinFormsUI.Docking

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
        Me.tbWinBackOffice = New System.Windows.Forms.TextBox()
        Me.tbWinBackDatabase = New System.Windows.Forms.TextBox()
        Me.BtnUpdateWinBackDataBase = New System.Windows.Forms.Button()
        Me.lblUpdateFilesCount = New System.Windows.Forms.Label()
        Me.pbData = New MetroFramework.Controls.MetroProgressBar()
        Me.pbFiles = New MetroFramework.Controls.MetroProgressBar()
        Me.tbWinbackKundeName = New System.Windows.Forms.TextBox()
        Me.PnlOrgaBack = New System.Windows.Forms.Panel()
        Me.tbOrgaBackMainDB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbOrgaBackAdminDB = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbOrgaBackDBServer = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbMsSQLMain = New System.Windows.Forms.TextBox()
        Me.tbOrgaBackDataBase = New System.Windows.Forms.TextBox()
        Me.tbOrgaBack = New System.Windows.Forms.TextBox()
        Me.lblOrgaBackDataBase = New System.Windows.Forms.Label()
        Me.lblOrgaBack = New System.Windows.Forms.Label()
        Me.tbWBDatenDB = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbWinBackDB = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbWinBackIP = New System.Windows.Forms.TextBox()
        Me.lblMandant = New System.Windows.Forms.Label()
        Me.tbMandant = New System.Windows.Forms.TextBox()
        Me.PnlOrgaBack.SuspendLayout()
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
        'PnlOrgaBack
        '
        Me.PnlOrgaBack.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlOrgaBack.Controls.Add(Me.tbOrgaBackMainDB)
        Me.PnlOrgaBack.Controls.Add(Me.Label3)
        Me.PnlOrgaBack.Controls.Add(Me.tbOrgaBackAdminDB)
        Me.PnlOrgaBack.Controls.Add(Me.Label2)
        Me.PnlOrgaBack.Controls.Add(Me.tbOrgaBackDBServer)
        Me.PnlOrgaBack.Controls.Add(Me.Label1)
        Me.PnlOrgaBack.Controls.Add(Me.tbMsSQLMain)
        Me.PnlOrgaBack.Controls.Add(Me.tbOrgaBackDataBase)
        Me.PnlOrgaBack.Controls.Add(Me.tbOrgaBack)
        Me.PnlOrgaBack.Controls.Add(Me.lblOrgaBackDataBase)
        Me.PnlOrgaBack.Controls.Add(Me.lblOrgaBack)
        Me.PnlOrgaBack.Location = New System.Drawing.Point(1, 149)
        Me.PnlOrgaBack.Name = "PnlOrgaBack"
        Me.PnlOrgaBack.Size = New System.Drawing.Size(468, 129)
        Me.PnlOrgaBack.TabIndex = 21
        '
        'tbOrgaBackMainDB
        '
        Me.tbOrgaBackMainDB.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBackMainDB.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBackMainDB.Location = New System.Drawing.Point(164, 97)
        Me.tbOrgaBackMainDB.Name = "tbOrgaBackMainDB"
        Me.tbOrgaBackMainDB.Size = New System.Drawing.Size(123, 20)
        Me.tbOrgaBackMainDB.TabIndex = 31
        Me.tbOrgaBackMainDB.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 13)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Main-DB OrgaBack"
        '
        'tbOrgaBackAdminDB
        '
        Me.tbOrgaBackAdminDB.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBackAdminDB.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBackAdminDB.Location = New System.Drawing.Point(164, 75)
        Me.tbOrgaBackAdminDB.Name = "tbOrgaBackAdminDB"
        Me.tbOrgaBackAdminDB.Size = New System.Drawing.Size(123, 20)
        Me.tbOrgaBackAdminDB.TabIndex = 29
        Me.tbOrgaBackAdminDB.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 13)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Admin-DB OrgaBack"
        '
        'tbOrgaBackDBServer
        '
        Me.tbOrgaBackDBServer.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBackDBServer.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBackDBServer.Location = New System.Drawing.Point(164, 53)
        Me.tbOrgaBackDBServer.Name = "tbOrgaBackDBServer"
        Me.tbOrgaBackDBServer.Size = New System.Drawing.Size(252, 20)
        Me.tbOrgaBackDBServer.TabIndex = 27
        Me.tbOrgaBackDBServer.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "DB-Server OrgaBack"
        '
        'tbMsSQLMain
        '
        Me.tbMsSQLMain.BackColor = System.Drawing.SystemColors.Control
        Me.tbMsSQLMain.ForeColor = System.Drawing.Color.DarkGray
        Me.tbMsSQLMain.Location = New System.Drawing.Point(293, 27)
        Me.tbMsSQLMain.Name = "tbMsSQLMain"
        Me.tbMsSQLMain.Size = New System.Drawing.Size(123, 20)
        Me.tbMsSQLMain.TabIndex = 25
        Me.tbMsSQLMain.TabStop = False
        '
        'tbOrgaBackDataBase
        '
        Me.tbOrgaBackDataBase.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBackDataBase.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBackDataBase.Location = New System.Drawing.Point(164, 27)
        Me.tbOrgaBackDataBase.Name = "tbOrgaBackDataBase"
        Me.tbOrgaBackDataBase.Size = New System.Drawing.Size(123, 20)
        Me.tbOrgaBackDataBase.TabIndex = 24
        Me.tbOrgaBackDataBase.TabStop = False
        '
        'tbOrgaBack
        '
        Me.tbOrgaBack.BackColor = System.Drawing.SystemColors.Control
        Me.tbOrgaBack.ForeColor = System.Drawing.Color.DarkGray
        Me.tbOrgaBack.Location = New System.Drawing.Point(164, 5)
        Me.tbOrgaBack.Name = "tbOrgaBack"
        Me.tbOrgaBack.Size = New System.Drawing.Size(123, 20)
        Me.tbOrgaBack.TabIndex = 23
        Me.tbOrgaBack.TabStop = False
        '
        'lblOrgaBackDataBase
        '
        Me.lblOrgaBackDataBase.AutoSize = True
        Me.lblOrgaBackDataBase.Location = New System.Drawing.Point(11, 29)
        Me.lblOrgaBackDataBase.Name = "lblOrgaBackDataBase"
        Me.lblOrgaBackDataBase.Size = New System.Drawing.Size(149, 13)
        Me.lblOrgaBackDataBase.TabIndex = 22
        Me.lblOrgaBackDataBase.Text = "Version OrgaBack Datenbank"
        '
        'lblOrgaBack
        '
        Me.lblOrgaBack.AutoSize = True
        Me.lblOrgaBack.Location = New System.Drawing.Point(11, 8)
        Me.lblOrgaBack.Name = "lblOrgaBack"
        Me.lblOrgaBack.Size = New System.Drawing.Size(93, 13)
        Me.lblOrgaBack.TabIndex = 21
        Me.lblOrgaBack.Text = "Version OrgaBack"
        '
        'tbWBDatenDB
        '
        Me.tbWBDatenDB.BackColor = System.Drawing.SystemColors.Control
        Me.tbWBDatenDB.ForeColor = System.Drawing.Color.DarkGray
        Me.tbWBDatenDB.Location = New System.Drawing.Point(165, 83)
        Me.tbWBDatenDB.Name = "tbWBDatenDB"
        Me.tbWBDatenDB.Size = New System.Drawing.Size(123, 20)
        Me.tbWBDatenDB.TabIndex = 35
        Me.tbWBDatenDB.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 86)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 13)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "Archiv-DB WinBack"
        '
        'tbWinBackDB
        '
        Me.tbWinBackDB.BackColor = System.Drawing.SystemColors.Control
        Me.tbWinBackDB.ForeColor = System.Drawing.Color.DarkGray
        Me.tbWinBackDB.Location = New System.Drawing.Point(165, 61)
        Me.tbWinBackDB.Name = "tbWinBackDB"
        Me.tbWinBackDB.Size = New System.Drawing.Size(123, 20)
        Me.tbWinBackDB.TabIndex = 33
        Me.tbWinBackDB.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 13)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Haupt-DB WinBack"
        '
        'tbWinBackIP
        '
        Me.tbWinBackIP.BackColor = System.Drawing.SystemColors.Control
        Me.tbWinBackIP.ForeColor = System.Drawing.Color.DarkGray
        Me.tbWinBackIP.Location = New System.Drawing.Point(294, 61)
        Me.tbWinBackIP.Name = "tbWinBackIP"
        Me.tbWinBackIP.Size = New System.Drawing.Size(123, 20)
        Me.tbWinBackIP.TabIndex = 36
        Me.tbWinBackIP.TabStop = False
        '
        'lblMandant
        '
        Me.lblMandant.AutoSize = True
        Me.lblMandant.Location = New System.Drawing.Point(12, 116)
        Me.lblMandant.Name = "lblMandant"
        Me.lblMandant.Size = New System.Drawing.Size(49, 13)
        Me.lblMandant.TabIndex = 37
        Me.lblMandant.Text = "Mandant"
        '
        'tbMandant
        '
        Me.tbMandant.BackColor = System.Drawing.SystemColors.Control
        Me.tbMandant.ForeColor = System.Drawing.Color.DarkGray
        Me.tbMandant.Location = New System.Drawing.Point(165, 113)
        Me.tbMandant.Name = "tbMandant"
        Me.tbMandant.Size = New System.Drawing.Size(252, 20)
        Me.tbMandant.TabIndex = 38
        Me.tbMandant.TabStop = False
        '
        'wb_Admin_UpdateDatabase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(471, 445)
        Me.Controls.Add(Me.tbMandant)
        Me.Controls.Add(Me.lblMandant)
        Me.Controls.Add(Me.tbWinBackIP)
        Me.Controls.Add(Me.tbWBDatenDB)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbWinBackDB)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.PnlOrgaBack)
        Me.Controls.Add(Me.tbWinbackKundeName)
        Me.Controls.Add(Me.pbFiles)
        Me.Controls.Add(Me.pbData)
        Me.Controls.Add(Me.lblUpdateFilesCount)
        Me.Controls.Add(Me.BtnUpdateWinBackDataBase)
        Me.Controls.Add(Me.tbWinBackDatabase)
        Me.Controls.Add(Me.tbWinBackOffice)
        Me.Controls.Add(Me.lblWinBackDataBase)
        Me.Controls.Add(Me.lblWinBackOffice)
        Me.Name = "wb_Admin_UpdateDatabase"
        Me.Text = "Update"
        Me.PnlOrgaBack.ResumeLayout(False)
        Me.PnlOrgaBack.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblWinBackOffice As System.Windows.Forms.Label
    Friend WithEvents lblWinBackDataBase As System.Windows.Forms.Label
    Friend WithEvents tbWinBackOffice As System.Windows.Forms.TextBox
    Friend WithEvents tbWinBackDatabase As System.Windows.Forms.TextBox
    Friend WithEvents BtnUpdateWinBackDataBase As System.Windows.Forms.Button
    Friend WithEvents lblUpdateFilesCount As System.Windows.Forms.Label
    Friend WithEvents pbData As MetroFramework.Controls.MetroProgressBar
    Friend WithEvents pbFiles As MetroFramework.Controls.MetroProgressBar
    Friend WithEvents tbWinbackKundeName As System.Windows.Forms.TextBox
    Friend WithEvents PnlOrgaBack As System.Windows.Forms.Panel
    Friend WithEvents tbOrgaBackMainDB As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbOrgaBackAdminDB As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbOrgaBackDBServer As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbMsSQLMain As System.Windows.Forms.TextBox
    Friend WithEvents tbOrgaBackDataBase As System.Windows.Forms.TextBox
    Friend WithEvents tbOrgaBack As System.Windows.Forms.TextBox
    Friend WithEvents lblOrgaBackDataBase As System.Windows.Forms.Label
    Friend WithEvents lblOrgaBack As System.Windows.Forms.Label
    Friend WithEvents tbWBDatenDB As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbWinBackDB As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbWinBackIP As System.Windows.Forms.TextBox
    Friend WithEvents lblMandant As System.Windows.Forms.Label
    Friend WithEvents tbMandant As System.Windows.Forms.TextBox
End Class
