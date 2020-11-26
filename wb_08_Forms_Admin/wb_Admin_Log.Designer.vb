Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_Log
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
        Me.tbLogger = New System.Windows.Forms.TextBox()
        Me.BtnLoadTextFile = New System.Windows.Forms.Button()
        Me.BtnLoadKonfig = New System.Windows.Forms.Button()
        Me.BtnLog4Viewer = New System.Windows.Forms.Button()
        Me.BtnEditKonfig = New System.Windows.Forms.Button()
        Me.tbAktLogger = New System.Windows.Forms.TextBox()
        Me.lblAktLogger = New System.Windows.Forms.Label()
        Me.cbLogAktiv = New System.Windows.Forms.CheckBox()
        Me.cbLogAutoStart = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'tbLogger
        '
        Me.tbLogger.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbLogger.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbLogger.Location = New System.Drawing.Point(12, 12)
        Me.tbLogger.Multiline = True
        Me.tbLogger.Name = "tbLogger"
        Me.tbLogger.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbLogger.Size = New System.Drawing.Size(879, 341)
        Me.tbLogger.TabIndex = 0
        Me.tbLogger.WordWrap = False
        '
        'BtnLoadTextFile
        '
        Me.BtnLoadTextFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnLoadTextFile.Image = Global.WinBack.My.Resources.Resources.IconDlgLog_16x16
        Me.BtnLoadTextFile.Location = New System.Drawing.Point(897, 314)
        Me.BtnLoadTextFile.Name = "BtnLoadTextFile"
        Me.BtnLoadTextFile.Size = New System.Drawing.Size(135, 39)
        Me.BtnLoadTextFile.TabIndex = 4
        Me.BtnLoadTextFile.Text = "Logger Textfile laden"
        Me.BtnLoadTextFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnLoadTextFile.UseVisualStyleBackColor = True
        '
        'BtnLoadKonfig
        '
        Me.BtnLoadKonfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnLoadKonfig.Image = Global.WinBack.My.Resources.Resources.IconSave_24x24
        Me.BtnLoadKonfig.Location = New System.Drawing.Point(897, 139)
        Me.BtnLoadKonfig.Name = "BtnLoadKonfig"
        Me.BtnLoadKonfig.Size = New System.Drawing.Size(135, 39)
        Me.BtnLoadKonfig.TabIndex = 5
        Me.BtnLoadKonfig.Text = "Logger Konfig laden"
        Me.BtnLoadKonfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnLoadKonfig.UseVisualStyleBackColor = True
        '
        'BtnLog4Viewer
        '
        Me.BtnLog4Viewer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnLog4Viewer.Image = Global.WinBack.My.Resources.Resources.Log4View_16x16
        Me.BtnLog4Viewer.Location = New System.Drawing.Point(897, 269)
        Me.BtnLog4Viewer.Name = "BtnLog4Viewer"
        Me.BtnLog4Viewer.Size = New System.Drawing.Size(135, 39)
        Me.BtnLog4Viewer.TabIndex = 6
        Me.BtnLog4Viewer.Text = "Log4View starten"
        Me.BtnLog4Viewer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnLog4Viewer.UseVisualStyleBackColor = True
        '
        'BtnEditKonfig
        '
        Me.BtnEditKonfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnEditKonfig.Image = Global.WinBack.My.Resources.Resources.EditKonfig_16x16
        Me.BtnEditKonfig.Location = New System.Drawing.Point(897, 184)
        Me.BtnEditKonfig.Name = "BtnEditKonfig"
        Me.BtnEditKonfig.Size = New System.Drawing.Size(135, 39)
        Me.BtnEditKonfig.TabIndex = 7
        Me.BtnEditKonfig.Text = "Edit Logger Konfig"
        Me.BtnEditKonfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnEditKonfig.UseVisualStyleBackColor = True
        '
        'tbAktLogger
        '
        Me.tbAktLogger.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbAktLogger.Location = New System.Drawing.Point(897, 28)
        Me.tbAktLogger.Name = "tbAktLogger"
        Me.tbAktLogger.Size = New System.Drawing.Size(135, 20)
        Me.tbAktLogger.TabIndex = 8
        '
        'lblAktLogger
        '
        Me.lblAktLogger.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAktLogger.AutoSize = True
        Me.lblAktLogger.Location = New System.Drawing.Point(897, 12)
        Me.lblAktLogger.Name = "lblAktLogger"
        Me.lblAktLogger.Size = New System.Drawing.Size(84, 13)
        Me.lblAktLogger.TabIndex = 9
        Me.lblAktLogger.Text = "Aktueller Logger"
        '
        'cbLogAktiv
        '
        Me.cbLogAktiv.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbLogAktiv.Location = New System.Drawing.Point(911, 54)
        Me.cbLogAktiv.Name = "cbLogAktiv"
        Me.cbLogAktiv.Size = New System.Drawing.Size(121, 22)
        Me.cbLogAktiv.TabIndex = 10
        Me.cbLogAktiv.Text = "Aktiv"
        Me.cbLogAktiv.UseVisualStyleBackColor = True
        '
        'cbLogAutoStart
        '
        Me.cbLogAutoStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbLogAutoStart.Location = New System.Drawing.Point(911, 73)
        Me.cbLogAutoStart.Name = "cbLogAutoStart"
        Me.cbLogAutoStart.Size = New System.Drawing.Size(121, 22)
        Me.cbLogAutoStart.TabIndex = 11
        Me.cbLogAutoStart.Text = "Autostart"
        Me.cbLogAutoStart.UseVisualStyleBackColor = True
        '
        'wb_Admin_Log
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1044, 365)
        Me.Controls.Add(Me.cbLogAutoStart)
        Me.Controls.Add(Me.cbLogAktiv)
        Me.Controls.Add(Me.lblAktLogger)
        Me.Controls.Add(Me.tbAktLogger)
        Me.Controls.Add(Me.BtnEditKonfig)
        Me.Controls.Add(Me.BtnLog4Viewer)
        Me.Controls.Add(Me.BtnLoadKonfig)
        Me.Controls.Add(Me.BtnLoadTextFile)
        Me.Controls.Add(Me.tbLogger)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "wb_Admin_Log"
        Me.Text = "Ausgabe Log-Daten"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbLogger As Windows.Forms.TextBox
    Friend WithEvents BtnLoadTextFile As Windows.Forms.Button
    Friend WithEvents BtnLoadKonfig As Windows.Forms.Button
    Friend WithEvents BtnLog4Viewer As Windows.Forms.Button
    Friend WithEvents BtnEditKonfig As Windows.Forms.Button
    Friend WithEvents tbAktLogger As Windows.Forms.TextBox
    Friend WithEvents lblAktLogger As Windows.Forms.Label
    Friend WithEvents cbLogAktiv As Windows.Forms.CheckBox
    Friend WithEvents cbLogAutoStart As Windows.Forms.CheckBox
End Class
