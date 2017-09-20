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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Admin_Datensicherung))
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
        resources.ApplyResources(Me.Btn_DatenSicherung, "Btn_DatenSicherung")
        Me.Btn_DatenSicherung.Name = "Btn_DatenSicherung"
        Me.Btn_DatenSicherung.UseVisualStyleBackColor = True
        '
        'BackupFileName
        '
        resources.ApplyResources(Me.BackupFileName, "BackupFileName")
        Me.BackupFileName.BackColor = System.Drawing.SystemColors.Control
        Me.BackupFileName.Name = "BackupFileName"
        '
        'Btn_DatenRueckSicherung
        '
        Me.Btn_DatenRueckSicherung.Image = Global.WinBack.My.Resources.Resources.DatenRueckSicherung_32x32
        resources.ApplyResources(Me.Btn_DatenRueckSicherung, "Btn_DatenRueckSicherung")
        Me.Btn_DatenRueckSicherung.Name = "Btn_DatenRueckSicherung"
        Me.Btn_DatenRueckSicherung.UseVisualStyleBackColor = True
        '
        'lblBackupFileName
        '
        resources.ApplyResources(Me.lblBackupFileName, "lblBackupFileName")
        Me.lblBackupFileName.Name = "lblBackupFileName"
        '
        'lblBackupTimeStamp
        '
        resources.ApplyResources(Me.lblBackupTimeStamp, "lblBackupTimeStamp")
        Me.lblBackupTimeStamp.Name = "lblBackupTimeStamp"
        '
        'BackupTimeStamp
        '
        Me.BackupTimeStamp.BackColor = System.Drawing.SystemColors.Control
        Me.BackupTimeStamp.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.BackupTimeStamp, "BackupTimeStamp")
        Me.BackupTimeStamp.Name = "BackupTimeStamp"
        Me.BackupTimeStamp.TabStop = False
        '
        'lblRestoreTimeStamp
        '
        resources.ApplyResources(Me.lblRestoreTimeStamp, "lblRestoreTimeStamp")
        Me.lblRestoreTimeStamp.Name = "lblRestoreTimeStamp"
        '
        'RestoreTimeStamp
        '
        Me.RestoreTimeStamp.BackColor = System.Drawing.SystemColors.Control
        Me.RestoreTimeStamp.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.RestoreTimeStamp, "RestoreTimeStamp")
        Me.RestoreTimeStamp.Name = "RestoreTimeStamp"
        Me.RestoreTimeStamp.TabStop = False
        '
        'lblRestoreFileName
        '
        resources.ApplyResources(Me.lblRestoreFileName, "lblRestoreFileName")
        Me.lblRestoreFileName.Name = "lblRestoreFileName"
        '
        'RestoreFileName
        '
        resources.ApplyResources(Me.RestoreFileName, "RestoreFileName")
        Me.RestoreFileName.BackColor = System.Drawing.SystemColors.Control
        Me.RestoreFileName.Name = "RestoreFileName"
        '
        'wb_Admin_Datensicherung
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
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
        Me.Name = "wb_Admin_Datensicherung"
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
