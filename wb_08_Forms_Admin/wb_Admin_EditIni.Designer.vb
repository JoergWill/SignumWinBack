<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_EditIni
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent
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
        Me.tbIniFile = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblPathToWinBackIni = New System.Windows.Forms.Label()
        Me.lblMandant = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'tbIniFile
        '
        Me.tbIniFile.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbIniFile.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbIniFile.Location = New System.Drawing.Point(12, 12)
        Me.tbIniFile.Multiline = True
        Me.tbIniFile.Name = "tbIniFile"
        Me.tbIniFile.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbIniFile.Size = New System.Drawing.Size(933, 495)
        Me.tbIniFile.TabIndex = 0
        Me.tbIniFile.TabStop = False
        Me.tbIniFile.WordWrap = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Enabled = False
        Me.btnSave.Image = Global.WinBack.My.Resources.Resources.IconSave_24x24
        Me.btnSave.Location = New System.Drawing.Point(715, 513)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(112, 46)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Speichern"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Image = Global.WinBack.My.Resources.Resources.IconDelete_24x24
        Me.btnCancel.Location = New System.Drawing.Point(833, 513)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(112, 46)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Abbruch"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblPathToWinBackIni
        '
        Me.lblPathToWinBackIni.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPathToWinBackIni.Location = New System.Drawing.Point(12, 513)
        Me.lblPathToWinBackIni.Name = "lblPathToWinBackIni"
        Me.lblPathToWinBackIni.Size = New System.Drawing.Size(697, 19)
        Me.lblPathToWinBackIni.TabIndex = 3
        Me.lblPathToWinBackIni.Text = "WinBack.ini"
        '
        'lblMandant
        '
        Me.lblMandant.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMandant.Location = New System.Drawing.Point(12, 530)
        Me.lblMandant.Name = "lblMandant"
        Me.lblMandant.Size = New System.Drawing.Size(697, 19)
        Me.lblMandant.TabIndex = 4
        Me.lblMandant.Text = "Mandant/Version/Name"
        '
        'wb_Admin_EditIni
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(957, 571)
        Me.Controls.Add(Me.lblMandant)
        Me.Controls.Add(Me.lblPathToWinBackIni)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.tbIniFile)
        Me.Name = "wb_Admin_EditIni"
        Me.Text = "System-Konfiguration"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbIniFile As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblPathToWinBackIni As System.Windows.Forms.Label
    Friend WithEvents lblMandant As System.Windows.Forms.Label
End Class
