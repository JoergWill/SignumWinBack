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
        Me.Btn_DatenSicherung = New System.Windows.Forms.Button()
        Me.BackupFileName = New System.Windows.Forms.TextBox()
        Me.Btn_DatenRueckSicherung = New System.Windows.Forms.Button()
        Me.lblBackupFileName = New System.Windows.Forms.Label()
        Me.lblBackupTimeStamp = New System.Windows.Forms.Label()
        Me.BackupTimeStamp = New System.Windows.Forms.TextBox()
        Me.lblRestoreTimeStamp = New System.Windows.Forms.Label()
        Me.RestoreTimeStamp = New System.Windows.Forms.TextBox()
        Me.lblRestoreFileName = New System.Windows.Forms.Label()
        Me.RestoreFileName = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Btn_DatenSicherung
        '
        Me.Btn_DatenSicherung.Image = Global.WinBack.My.Resources.Resources.DatenSicherung_32x32
        Me.Btn_DatenSicherung.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_DatenSicherung.Location = New System.Drawing.Point(12, 16)
        Me.Btn_DatenSicherung.Name = "Btn_DatenSicherung"
        Me.Btn_DatenSicherung.Size = New System.Drawing.Size(114, 47)
        Me.Btn_DatenSicherung.TabIndex = 0
        Me.Btn_DatenSicherung.Text = "Backup"
        Me.Btn_DatenSicherung.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btn_DatenSicherung.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.Btn_DatenSicherung.UseVisualStyleBackColor = True
        '
        'BackupFileName
        '
        Me.BackupFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BackupFileName.BackColor = System.Drawing.SystemColors.Control
        Me.BackupFileName.Location = New System.Drawing.Point(267, 21)
        Me.BackupFileName.Name = "BackupFileName"
        Me.BackupFileName.Size = New System.Drawing.Size(329, 20)
        Me.BackupFileName.TabIndex = 1
        '
        'Btn_DatenRueckSicherung
        '
        Me.Btn_DatenRueckSicherung.Image = Global.WinBack.My.Resources.Resources.DatenRueckSicherung_32x32
        Me.Btn_DatenRueckSicherung.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_DatenRueckSicherung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Btn_DatenRueckSicherung.Location = New System.Drawing.Point(12, 83)
        Me.Btn_DatenRueckSicherung.Name = "Btn_DatenRueckSicherung"
        Me.Btn_DatenRueckSicherung.Size = New System.Drawing.Size(114, 47)
        Me.Btn_DatenRueckSicherung.TabIndex = 3
        Me.Btn_DatenRueckSicherung.Text = "Restore"
        Me.Btn_DatenRueckSicherung.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btn_DatenRueckSicherung.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.Btn_DatenRueckSicherung.UseVisualStyleBackColor = True
        '
        'lblBackupFileName
        '
        Me.lblBackupFileName.AutoSize = True
        Me.lblBackupFileName.Location = New System.Drawing.Point(144, 24)
        Me.lblBackupFileName.Name = "lblBackupFileName"
        Me.lblBackupFileName.Size = New System.Drawing.Size(117, 13)
        Me.lblBackupFileName.TabIndex = 5
        Me.lblBackupFileName.Text = "Letzte Datensicherung:"
        '
        'lblBackupTimeStamp
        '
        Me.lblBackupTimeStamp.AutoSize = True
        Me.lblBackupTimeStamp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblBackupTimeStamp.Location = New System.Drawing.Point(144, 42)
        Me.lblBackupTimeStamp.Name = "lblBackupTimeStamp"
        Me.lblBackupTimeStamp.Size = New System.Drawing.Size(24, 13)
        Me.lblBackupTimeStamp.TabIndex = 7
        Me.lblBackupTimeStamp.Text = "am:"
        '
        'BackupTimeStamp
        '
        Me.BackupTimeStamp.BackColor = System.Drawing.SystemColors.Control
        Me.BackupTimeStamp.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.BackupTimeStamp.Location = New System.Drawing.Point(178, 42)
        Me.BackupTimeStamp.Name = "BackupTimeStamp"
        Me.BackupTimeStamp.Size = New System.Drawing.Size(87, 13)
        Me.BackupTimeStamp.TabIndex = 6
        Me.BackupTimeStamp.TabStop = False
        Me.BackupTimeStamp.Text = "dd.mm.yyyy"
        '
        'lblRestoreTimeStamp
        '
        Me.lblRestoreTimeStamp.AutoSize = True
        Me.lblRestoreTimeStamp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRestoreTimeStamp.Location = New System.Drawing.Point(144, 110)
        Me.lblRestoreTimeStamp.Name = "lblRestoreTimeStamp"
        Me.lblRestoreTimeStamp.Size = New System.Drawing.Size(24, 13)
        Me.lblRestoreTimeStamp.TabIndex = 11
        Me.lblRestoreTimeStamp.Text = "am:"
        '
        'RestoreTimeStamp
        '
        Me.RestoreTimeStamp.BackColor = System.Drawing.SystemColors.Control
        Me.RestoreTimeStamp.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RestoreTimeStamp.Location = New System.Drawing.Point(178, 110)
        Me.RestoreTimeStamp.Name = "RestoreTimeStamp"
        Me.RestoreTimeStamp.Size = New System.Drawing.Size(87, 13)
        Me.RestoreTimeStamp.TabIndex = 10
        Me.RestoreTimeStamp.TabStop = False
        Me.RestoreTimeStamp.Text = "dd.mm.yyyy"
        '
        'lblRestoreFileName
        '
        Me.lblRestoreFileName.AutoSize = True
        Me.lblRestoreFileName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRestoreFileName.Location = New System.Drawing.Point(144, 92)
        Me.lblRestoreFileName.Name = "lblRestoreFileName"
        Me.lblRestoreFileName.Size = New System.Drawing.Size(114, 13)
        Me.lblRestoreFileName.TabIndex = 9
        Me.lblRestoreFileName.Text = "Letzte Rücksicherung:"
        '
        'RestoreFileName
        '
        Me.RestoreFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RestoreFileName.BackColor = System.Drawing.SystemColors.Control
        Me.RestoreFileName.Location = New System.Drawing.Point(267, 89)
        Me.RestoreFileName.Name = "RestoreFileName"
        Me.RestoreFileName.Size = New System.Drawing.Size(329, 20)
        Me.RestoreFileName.TabIndex = 8
        '
        'wb_Admin_Datensicherung
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 149)
        Me.Controls.Add(Me.lblRestoreTimeStamp)
        Me.Controls.Add(Me.RestoreTimeStamp)
        Me.Controls.Add(Me.lblRestoreFileName)
        Me.Controls.Add(Me.RestoreFileName)
        Me.Controls.Add(Me.lblBackupTimeStamp)
        Me.Controls.Add(Me.BackupTimeStamp)
        Me.Controls.Add(Me.lblBackupFileName)
        Me.Controls.Add(Me.Btn_DatenRueckSicherung)
        Me.Controls.Add(Me.BackupFileName)
        Me.Controls.Add(Me.Btn_DatenSicherung)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.MinimumSize = New System.Drawing.Size(624, 188)
        Me.Name = "wb_Admin_Datensicherung"
        Me.Text = "WinBack Daten sichern/rücksichern"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Btn_DatenSicherung As Windows.Forms.Button
    Friend WithEvents BackupFileName As Windows.Forms.TextBox
    Friend WithEvents Btn_DatenRueckSicherung As Windows.Forms.Button
    Friend WithEvents lblBackupFileName As Windows.Forms.Label
    Friend WithEvents lblBackupTimeStamp As Windows.Forms.Label
    Friend WithEvents BackupTimeStamp As Windows.Forms.TextBox
    Friend WithEvents lblRestoreTimeStamp As Windows.Forms.Label
    Friend WithEvents RestoreTimeStamp As Windows.Forms.TextBox
    Friend WithEvents lblRestoreFileName As Windows.Forms.Label
    Friend WithEvents RestoreFileName As Windows.Forms.TextBox
End Class
