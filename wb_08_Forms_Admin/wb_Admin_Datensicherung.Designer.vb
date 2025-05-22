Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_Datensicherung
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Admin_Datensicherung))
        Btn_DatenSicherung = New System.Windows.Forms.Button()
        BackupFileName = New System.Windows.Forms.TextBox()
        Btn_DatenRueckSicherung = New System.Windows.Forms.Button()
        lblBackupFileName = New System.Windows.Forms.Label()
        lblBackupTimeStamp = New System.Windows.Forms.Label()
        BackupTimeStamp = New System.Windows.Forms.TextBox()
        lblRestoreTimeStamp = New System.Windows.Forms.Label()
        RestoreTimeStamp = New System.Windows.Forms.TextBox()
        lblRestoreFileName = New System.Windows.Forms.Label()
        RestoreFileName = New System.Windows.Forms.TextBox()
        cbFormatMySQL3_2 = New System.Windows.Forms.CheckBox()
        ProgressBackup = New System.Windows.Forms.ProgressBar()
        ProgressRestore = New System.Windows.Forms.ProgressBar()
        lblFortschrittSave = New System.Windows.Forms.Label()
        lblFortschrittRestore = New System.Windows.Forms.Label()
        ToolTipMySql = New System.Windows.Forms.ToolTip(components)
        cbFormatMySQL5_0 = New System.Windows.Forms.CheckBox()
        SuspendLayout()
        ' 
        ' Btn_DatenSicherung
        ' 
        Btn_DatenSicherung.Image = CType(resources.GetObject("Btn_DatenSicherung.Image"), Drawing.Image)
        Btn_DatenSicherung.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        Btn_DatenSicherung.Location = New System.Drawing.Point(12, 16)
        Btn_DatenSicherung.Name = "Btn_DatenSicherung"
        Btn_DatenSicherung.Size = New System.Drawing.Size(114, 47)
        Btn_DatenSicherung.TabIndex = 0
        Btn_DatenSicherung.Text = "MySQL Backup"
        Btn_DatenSicherung.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Btn_DatenSicherung.UseVisualStyleBackColor = True
        ' 
        ' BackupFileName
        ' 
        BackupFileName.BackColor = Drawing.SystemColors.Control
        BackupFileName.Location = New System.Drawing.Point(285, 21)
        BackupFileName.Name = "BackupFileName"
        BackupFileName.Size = New System.Drawing.Size(448, 20)
        BackupFileName.TabIndex = 1
        ' 
        ' Btn_DatenRueckSicherung
        ' 
        Btn_DatenRueckSicherung.Image = CType(resources.GetObject("Btn_DatenRueckSicherung.Image"), Drawing.Image)
        Btn_DatenRueckSicherung.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        Btn_DatenRueckSicherung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Btn_DatenRueckSicherung.Location = New System.Drawing.Point(12, 106)
        Btn_DatenRueckSicherung.Name = "Btn_DatenRueckSicherung"
        Btn_DatenRueckSicherung.Size = New System.Drawing.Size(114, 47)
        Btn_DatenRueckSicherung.TabIndex = 3
        Btn_DatenRueckSicherung.Text = "MySQL Restore"
        Btn_DatenRueckSicherung.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Btn_DatenRueckSicherung.UseVisualStyleBackColor = True
        ' 
        ' lblBackupFileName
        ' 
        lblBackupFileName.AutoSize = True
        lblBackupFileName.Location = New System.Drawing.Point(144, 24)
        lblBackupFileName.Name = "lblBackupFileName"
        lblBackupFileName.Size = New System.Drawing.Size(117, 13)
        lblBackupFileName.TabIndex = 5
        lblBackupFileName.Text = "Letzte Datensicherung:"
        ' 
        ' lblBackupTimeStamp
        ' 
        lblBackupTimeStamp.AutoSize = True
        lblBackupTimeStamp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblBackupTimeStamp.Location = New System.Drawing.Point(144, 42)
        lblBackupTimeStamp.Name = "lblBackupTimeStamp"
        lblBackupTimeStamp.Size = New System.Drawing.Size(24, 13)
        lblBackupTimeStamp.TabIndex = 7
        lblBackupTimeStamp.Text = "am:"
        ' 
        ' BackupTimeStamp
        ' 
        BackupTimeStamp.BackColor = Drawing.SystemColors.Control
        BackupTimeStamp.BorderStyle = System.Windows.Forms.BorderStyle.None
        BackupTimeStamp.Location = New System.Drawing.Point(178, 42)
        BackupTimeStamp.Name = "BackupTimeStamp"
        BackupTimeStamp.Size = New System.Drawing.Size(87, 13)
        BackupTimeStamp.TabIndex = 6
        BackupTimeStamp.TabStop = False
        BackupTimeStamp.Text = "dd.mm.yyyy"
        ' 
        ' lblRestoreTimeStamp
        ' 
        lblRestoreTimeStamp.AutoSize = True
        lblRestoreTimeStamp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRestoreTimeStamp.Location = New System.Drawing.Point(144, 133)
        lblRestoreTimeStamp.Name = "lblRestoreTimeStamp"
        lblRestoreTimeStamp.Size = New System.Drawing.Size(24, 13)
        lblRestoreTimeStamp.TabIndex = 11
        lblRestoreTimeStamp.Text = "am:"
        ' 
        ' RestoreTimeStamp
        ' 
        RestoreTimeStamp.BackColor = Drawing.SystemColors.Control
        RestoreTimeStamp.BorderStyle = System.Windows.Forms.BorderStyle.None
        RestoreTimeStamp.Location = New System.Drawing.Point(178, 133)
        RestoreTimeStamp.Name = "RestoreTimeStamp"
        RestoreTimeStamp.Size = New System.Drawing.Size(87, 13)
        RestoreTimeStamp.TabIndex = 10
        RestoreTimeStamp.TabStop = False
        RestoreTimeStamp.Text = "dd.mm.yyyy"
        ' 
        ' lblRestoreFileName
        ' 
        lblRestoreFileName.AutoSize = True
        lblRestoreFileName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        lblRestoreFileName.Location = New System.Drawing.Point(144, 115)
        lblRestoreFileName.Name = "lblRestoreFileName"
        lblRestoreFileName.Size = New System.Drawing.Size(114, 13)
        lblRestoreFileName.TabIndex = 9
        lblRestoreFileName.Text = "Letzte Rücksicherung:"
        ' 
        ' RestoreFileName
        ' 
        RestoreFileName.BackColor = Drawing.SystemColors.Control
        RestoreFileName.Location = New System.Drawing.Point(285, 112)
        RestoreFileName.Name = "RestoreFileName"
        RestoreFileName.Size = New System.Drawing.Size(448, 20)
        RestoreFileName.TabIndex = 8
        ' 
        ' cbFormatMySQL3_2
        ' 
        cbFormatMySQL3_2.AutoSize = True
        cbFormatMySQL3_2.Location = New System.Drawing.Point(285, 46)
        cbFormatMySQL3_2.Name = "cbFormatMySQL3_2"
        cbFormatMySQL3_2.Size = New System.Drawing.Size(114, 17)
        cbFormatMySQL3_2.TabIndex = 12
        cbFormatMySQL3_2.Text = "Format MySQL 3.2"
        ToolTipMySql.SetToolTip(cbFormatMySQL3_2, "Speichern der Datenbank im Format MySQL 3.2" & vbCrLf & "(Sichern von lokaler Datenbank auf WinBack-Server)")
        cbFormatMySQL3_2.UseVisualStyleBackColor = True
        ' 
        ' ProgressBackup
        ' 
        ProgressBackup.Location = New System.Drawing.Point(467, 47)
        ProgressBackup.Name = "ProgressBackup"
        ProgressBackup.Size = New System.Drawing.Size(266, 26)
        ProgressBackup.TabIndex = 13
        ' 
        ' ProgressRestore
        ' 
        ProgressRestore.Location = New System.Drawing.Point(467, 138)
        ProgressRestore.Name = "ProgressRestore"
        ProgressRestore.Size = New System.Drawing.Size(266, 26)
        ProgressRestore.TabIndex = 14
        ' 
        ' lblFortschrittSave
        ' 
        lblFortschrittSave.AutoSize = True
        lblFortschrittSave.Location = New System.Drawing.Point(408, 60)
        lblFortschrittSave.Name = "lblFortschrittSave"
        lblFortschrittSave.Size = New System.Drawing.Size(53, 13)
        lblFortschrittSave.TabIndex = 15
        lblFortschrittSave.Text = "Fortschritt"
        ' 
        ' lblFortschrittRestore
        ' 
        lblFortschrittRestore.AutoSize = True
        lblFortschrittRestore.Location = New System.Drawing.Point(408, 151)
        lblFortschrittRestore.Name = "lblFortschrittRestore"
        lblFortschrittRestore.Size = New System.Drawing.Size(53, 13)
        lblFortschrittRestore.TabIndex = 16
        lblFortschrittRestore.Text = "Fortschritt"
        ' 
        ' cbFormatMySQL5_0
        ' 
        cbFormatMySQL5_0.AutoSize = True
        cbFormatMySQL5_0.Location = New System.Drawing.Point(285, 136)
        cbFormatMySQL5_0.Name = "cbFormatMySQL5_0"
        cbFormatMySQL5_0.Size = New System.Drawing.Size(114, 17)
        cbFormatMySQL5_0.TabIndex = 17
        cbFormatMySQL5_0.Text = "Format MySQL 5.0"
        ToolTipMySql.SetToolTip(cbFormatMySQL5_0, "Lesen der Datenbank im Format MySQL 5.0" & vbCrLf & "(Einlesen in lokale Datenbank)")
        cbFormatMySQL5_0.UseVisualStyleBackColor = True
        ' 
        ' wb_Admin_Datensicherung
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(781, 356)
        Controls.Add(cbFormatMySQL5_0)
        Controls.Add(lblFortschrittRestore)
        Controls.Add(lblFortschrittSave)
        Controls.Add(ProgressRestore)
        Controls.Add(ProgressBackup)
        Controls.Add(cbFormatMySQL3_2)
        Controls.Add(lblRestoreTimeStamp)
        Controls.Add(RestoreTimeStamp)
        Controls.Add(lblRestoreFileName)
        Controls.Add(RestoreFileName)
        Controls.Add(lblBackupTimeStamp)
        Controls.Add(BackupTimeStamp)
        Controls.Add(lblBackupFileName)
        Controls.Add(Btn_DatenRueckSicherung)
        Controls.Add(BackupFileName)
        Controls.Add(Btn_DatenSicherung)
        MinimumSize = New System.Drawing.Size(624, 188)
        Name = "wb_Admin_Datensicherung"
        Text = "WinBack Daten sichern/rücksichern"
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents Btn_DatenSicherung As System.Windows.Forms.Button
    Friend WithEvents BackupFileName As System.Windows.Forms.TextBox
    Friend WithEvents Btn_DatenRueckSicherung As System.Windows.Forms.Button
    Friend WithEvents lblBackupFileName As System.Windows.Forms.Label
    Friend WithEvents lblBackupTimeStamp As System.Windows.Forms.Label
    Friend WithEvents BackupTimeStamp As System.Windows.Forms.TextBox
    Friend WithEvents lblRestoreTimeStamp As System.Windows.Forms.Label
    Friend WithEvents RestoreTimeStamp As System.Windows.Forms.TextBox
    Friend WithEvents lblRestoreFileName As System.Windows.Forms.Label
    Friend WithEvents RestoreFileName As System.Windows.Forms.TextBox
    Friend WithEvents cbFormatMySQL3_2 As System.Windows.Forms.CheckBox
    Friend WithEvents ProgressBackup As System.Windows.Forms.ProgressBar
    Friend WithEvents ProgressRestore As System.Windows.Forms.ProgressBar
    Friend WithEvents lblFortschrittSave As System.Windows.Forms.Label
    Friend WithEvents lblFortschrittRestore As System.Windows.Forms.Label
    Friend WithEvents ToolTipMySql As System.Windows.Forms.ToolTip
    Friend WithEvents cbFormatMySQL5_0 As System.Windows.Forms.CheckBox
End Class
