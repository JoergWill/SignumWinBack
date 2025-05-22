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
        Me.grpLogger = New System.Windows.Forms.GroupBox()
        Me.cbError = New MetroFramework.Controls.MetroCheckBox()
        Me.cbWarn = New MetroFramework.Controls.MetroCheckBox()
        Me.cbInfo = New MetroFramework.Controls.MetroCheckBox()
        Me.cbDebug = New MetroFramework.Controls.MetroCheckBox()
        Me.cbLogAutoStart = New MetroFramework.Controls.MetroCheckBox()
        Me.cbLogAktiv = New MetroFramework.Controls.MetroCheckBox()
        Me.tbAktLogger = New System.Windows.Forms.TextBox()
        Me.grpListLabel = New System.Windows.Forms.GroupBox()
        Me.cbLL_Error = New MetroFramework.Controls.MetroCheckBox()
        Me.cbLL_Warn = New MetroFramework.Controls.MetroCheckBox()
        Me.cbLL_Info = New MetroFramework.Controls.MetroCheckBox()
        Me.cbLL_Other = New MetroFramework.Controls.MetroCheckBox()
        Me.cbLL_PrinterInformation = New MetroFramework.Controls.MetroCheckBox()
        Me.cbLL_Licensing = New MetroFramework.Controls.MetroCheckBox()
        Me.cbLL_ApiCalls = New MetroFramework.Controls.MetroCheckBox()
        Me.cbLL_DotNet = New MetroFramework.Controls.MetroCheckBox()
        Me.cbLL_DataProvider = New MetroFramework.Controls.MetroCheckBox()
        Me.cbLL_Debug = New MetroFramework.Controls.MetroCheckBox()
        Me.BtnLoadFromDataBase = New System.Windows.Forms.Button()
        Me.grpLogger.SuspendLayout()
        Me.grpListLabel.SuspendLayout()
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
        Me.tbLogger.Size = New System.Drawing.Size(856, 579)
        Me.tbLogger.TabIndex = 0
        Me.tbLogger.WordWrap = False
        '
        'BtnLoadTextFile
        '
        Me.BtnLoadTextFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnLoadTextFile.Enabled = False
        Me.BtnLoadTextFile.Image = Global.WinBack.My.Resources.Resources.IconDlgLog_16x16
        Me.BtnLoadTextFile.Location = New System.Drawing.Point(874, 552)
        Me.BtnLoadTextFile.Name = "BtnLoadTextFile"
        Me.BtnLoadTextFile.Size = New System.Drawing.Size(168, 39)
        Me.BtnLoadTextFile.TabIndex = 4
        Me.BtnLoadTextFile.Text = "Logger Textfile laden"
        Me.BtnLoadTextFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnLoadTextFile.UseVisualStyleBackColor = True
        '
        'BtnLoadKonfig
        '
        Me.BtnLoadKonfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnLoadKonfig.Image = Global.WinBack.My.Resources.Resources.IconSave_24x24
        Me.BtnLoadKonfig.Location = New System.Drawing.Point(874, 144)
        Me.BtnLoadKonfig.Name = "BtnLoadKonfig"
        Me.BtnLoadKonfig.Size = New System.Drawing.Size(168, 39)
        Me.BtnLoadKonfig.TabIndex = 5
        Me.BtnLoadKonfig.Text = "Logger Konfig laden"
        Me.BtnLoadKonfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnLoadKonfig.UseVisualStyleBackColor = True
        '
        'BtnLog4Viewer
        '
        Me.BtnLog4Viewer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnLog4Viewer.Image = Global.WinBack.My.Resources.Resources.Log4View_16x16
        Me.BtnLog4Viewer.Location = New System.Drawing.Point(874, 507)
        Me.BtnLog4Viewer.Name = "BtnLog4Viewer"
        Me.BtnLog4Viewer.Size = New System.Drawing.Size(168, 39)
        Me.BtnLog4Viewer.TabIndex = 6
        Me.BtnLog4Viewer.Text = "Log4View starten"
        Me.BtnLog4Viewer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnLog4Viewer.UseVisualStyleBackColor = True
        '
        'BtnEditKonfig
        '
        Me.BtnEditKonfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnEditKonfig.Image = Global.WinBack.My.Resources.Resources.EditKonfig_16x16
        Me.BtnEditKonfig.Location = New System.Drawing.Point(874, 189)
        Me.BtnEditKonfig.Name = "BtnEditKonfig"
        Me.BtnEditKonfig.Size = New System.Drawing.Size(168, 39)
        Me.BtnEditKonfig.TabIndex = 7
        Me.BtnEditKonfig.Text = "Edit Logger Konfig"
        Me.BtnEditKonfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnEditKonfig.UseVisualStyleBackColor = True
        '
        'grpLogger
        '
        Me.grpLogger.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpLogger.Controls.Add(Me.cbError)
        Me.grpLogger.Controls.Add(Me.cbWarn)
        Me.grpLogger.Controls.Add(Me.cbInfo)
        Me.grpLogger.Controls.Add(Me.cbDebug)
        Me.grpLogger.Controls.Add(Me.cbLogAutoStart)
        Me.grpLogger.Controls.Add(Me.cbLogAktiv)
        Me.grpLogger.Controls.Add(Me.tbAktLogger)
        Me.grpLogger.Location = New System.Drawing.Point(874, 12)
        Me.grpLogger.Name = "grpLogger"
        Me.grpLogger.Size = New System.Drawing.Size(168, 126)
        Me.grpLogger.TabIndex = 29
        Me.grpLogger.TabStop = False
        Me.grpLogger.Text = "Aktueller Logger"
        '
        'cbError
        '
        Me.cbError.AutoSize = True
        Me.cbError.BackColor = System.Drawing.SystemColors.Control
        Me.cbError.Checked = True
        Me.cbError.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbError.CustomBackground = True
        Me.cbError.Location = New System.Drawing.Point(104, 105)
        Me.cbError.Name = "cbError"
        Me.cbError.Size = New System.Drawing.Size(55, 15)
        Me.cbError.TabIndex = 41
        Me.cbError.TabStop = False
        Me.cbError.Text = "Fehler"
        Me.cbError.UseVisualStyleBackColor = False
        '
        'cbWarn
        '
        Me.cbWarn.AutoSize = True
        Me.cbWarn.BackColor = System.Drawing.SystemColors.Control
        Me.cbWarn.Checked = True
        Me.cbWarn.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbWarn.CustomBackground = True
        Me.cbWarn.Location = New System.Drawing.Point(104, 84)
        Me.cbWarn.Name = "cbWarn"
        Me.cbWarn.Size = New System.Drawing.Size(51, 15)
        Me.cbWarn.TabIndex = 40
        Me.cbWarn.TabStop = False
        Me.cbWarn.Text = "Warn"
        Me.cbWarn.UseVisualStyleBackColor = False
        '
        'cbInfo
        '
        Me.cbInfo.AutoSize = True
        Me.cbInfo.BackColor = System.Drawing.SystemColors.Control
        Me.cbInfo.Checked = True
        Me.cbInfo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbInfo.CustomBackground = True
        Me.cbInfo.Location = New System.Drawing.Point(104, 63)
        Me.cbInfo.Name = "cbInfo"
        Me.cbInfo.Size = New System.Drawing.Size(44, 15)
        Me.cbInfo.TabIndex = 39
        Me.cbInfo.TabStop = False
        Me.cbInfo.Text = "Info"
        Me.cbInfo.UseVisualStyleBackColor = False
        '
        'cbDebug
        '
        Me.cbDebug.AutoSize = True
        Me.cbDebug.BackColor = System.Drawing.SystemColors.Control
        Me.cbDebug.CustomBackground = True
        Me.cbDebug.Location = New System.Drawing.Point(104, 45)
        Me.cbDebug.Name = "cbDebug"
        Me.cbDebug.Size = New System.Drawing.Size(58, 15)
        Me.cbDebug.TabIndex = 31
        Me.cbDebug.TabStop = False
        Me.cbDebug.Text = "Debug"
        Me.cbDebug.UseVisualStyleBackColor = False
        '
        'cbLogAutoStart
        '
        Me.cbLogAutoStart.AutoSize = True
        Me.cbLogAutoStart.BackColor = System.Drawing.SystemColors.Control
        Me.cbLogAutoStart.Checked = True
        Me.cbLogAutoStart.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbLogAutoStart.CustomBackground = True
        Me.cbLogAutoStart.Location = New System.Drawing.Point(6, 66)
        Me.cbLogAutoStart.Name = "cbLogAutoStart"
        Me.cbLogAutoStart.Size = New System.Drawing.Size(72, 15)
        Me.cbLogAutoStart.TabIndex = 30
        Me.cbLogAutoStart.TabStop = False
        Me.cbLogAutoStart.Text = "Autostart"
        Me.cbLogAutoStart.UseVisualStyleBackColor = False
        '
        'cbLogAktiv
        '
        Me.cbLogAktiv.AutoSize = True
        Me.cbLogAktiv.BackColor = System.Drawing.SystemColors.Control
        Me.cbLogAktiv.Checked = True
        Me.cbLogAktiv.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbLogAktiv.CustomBackground = True
        Me.cbLogAktiv.Location = New System.Drawing.Point(6, 45)
        Me.cbLogAktiv.Name = "cbLogAktiv"
        Me.cbLogAktiv.Size = New System.Drawing.Size(50, 15)
        Me.cbLogAktiv.TabIndex = 29
        Me.cbLogAktiv.TabStop = False
        Me.cbLogAktiv.Text = "Aktiv"
        Me.cbLogAktiv.UseVisualStyleBackColor = False
        '
        'tbAktLogger
        '
        Me.tbAktLogger.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbAktLogger.Location = New System.Drawing.Point(6, 19)
        Me.tbAktLogger.Name = "tbAktLogger"
        Me.tbAktLogger.Size = New System.Drawing.Size(156, 20)
        Me.tbAktLogger.TabIndex = 9
        '
        'grpListLabel
        '
        Me.grpListLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpListLabel.Controls.Add(Me.cbLL_Error)
        Me.grpListLabel.Controls.Add(Me.cbLL_Warn)
        Me.grpListLabel.Controls.Add(Me.cbLL_Info)
        Me.grpListLabel.Controls.Add(Me.cbLL_Other)
        Me.grpListLabel.Controls.Add(Me.cbLL_PrinterInformation)
        Me.grpListLabel.Controls.Add(Me.cbLL_Licensing)
        Me.grpListLabel.Controls.Add(Me.cbLL_ApiCalls)
        Me.grpListLabel.Controls.Add(Me.cbLL_DotNet)
        Me.grpListLabel.Controls.Add(Me.cbLL_DataProvider)
        Me.grpListLabel.Controls.Add(Me.cbLL_Debug)
        Me.grpListLabel.Location = New System.Drawing.Point(874, 305)
        Me.grpListLabel.Name = "grpListLabel"
        Me.grpListLabel.Size = New System.Drawing.Size(168, 151)
        Me.grpListLabel.TabIndex = 30
        Me.grpListLabel.TabStop = False
        Me.grpListLabel.Text = "List&&Label"
        '
        'cbLL_Error
        '
        Me.cbLL_Error.AutoSize = True
        Me.cbLL_Error.BackColor = System.Drawing.SystemColors.Control
        Me.cbLL_Error.Checked = True
        Me.cbLL_Error.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbLL_Error.CustomBackground = True
        Me.cbLL_Error.Location = New System.Drawing.Point(104, 82)
        Me.cbLL_Error.Name = "cbLL_Error"
        Me.cbLL_Error.Size = New System.Drawing.Size(55, 15)
        Me.cbLL_Error.TabIndex = 38
        Me.cbLL_Error.TabStop = False
        Me.cbLL_Error.Text = "Fehler"
        Me.cbLL_Error.UseVisualStyleBackColor = False
        '
        'cbLL_Warn
        '
        Me.cbLL_Warn.AutoSize = True
        Me.cbLL_Warn.BackColor = System.Drawing.SystemColors.Control
        Me.cbLL_Warn.Checked = True
        Me.cbLL_Warn.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbLL_Warn.CustomBackground = True
        Me.cbLL_Warn.Location = New System.Drawing.Point(104, 61)
        Me.cbLL_Warn.Name = "cbLL_Warn"
        Me.cbLL_Warn.Size = New System.Drawing.Size(51, 15)
        Me.cbLL_Warn.TabIndex = 37
        Me.cbLL_Warn.TabStop = False
        Me.cbLL_Warn.Text = "Warn"
        Me.cbLL_Warn.UseVisualStyleBackColor = False
        '
        'cbLL_Info
        '
        Me.cbLL_Info.AutoSize = True
        Me.cbLL_Info.BackColor = System.Drawing.SystemColors.Control
        Me.cbLL_Info.Checked = True
        Me.cbLL_Info.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbLL_Info.CustomBackground = True
        Me.cbLL_Info.Location = New System.Drawing.Point(104, 40)
        Me.cbLL_Info.Name = "cbLL_Info"
        Me.cbLL_Info.Size = New System.Drawing.Size(44, 15)
        Me.cbLL_Info.TabIndex = 36
        Me.cbLL_Info.TabStop = False
        Me.cbLL_Info.Text = "Info"
        Me.cbLL_Info.UseVisualStyleBackColor = False
        '
        'cbLL_Other
        '
        Me.cbLL_Other.AutoSize = True
        Me.cbLL_Other.BackColor = System.Drawing.SystemColors.Control
        Me.cbLL_Other.Checked = True
        Me.cbLL_Other.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbLL_Other.CustomBackground = True
        Me.cbLL_Other.Location = New System.Drawing.Point(6, 124)
        Me.cbLL_Other.Name = "cbLL_Other"
        Me.cbLL_Other.Size = New System.Drawing.Size(73, 15)
        Me.cbLL_Other.TabIndex = 35
        Me.cbLL_Other.TabStop = False
        Me.cbLL_Other.Text = "Sonstiges"
        Me.cbLL_Other.UseVisualStyleBackColor = False
        '
        'cbLL_PrinterInformation
        '
        Me.cbLL_PrinterInformation.AutoSize = True
        Me.cbLL_PrinterInformation.BackColor = System.Drawing.SystemColors.Control
        Me.cbLL_PrinterInformation.Checked = True
        Me.cbLL_PrinterInformation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbLL_PrinterInformation.CustomBackground = True
        Me.cbLL_PrinterInformation.Location = New System.Drawing.Point(6, 103)
        Me.cbLL_PrinterInformation.Name = "cbLL_PrinterInformation"
        Me.cbLL_PrinterInformation.Size = New System.Drawing.Size(130, 15)
        Me.cbLL_PrinterInformation.TabIndex = 34
        Me.cbLL_PrinterInformation.TabStop = False
        Me.cbLL_PrinterInformation.Text = "Drucker Information"
        Me.cbLL_PrinterInformation.UseVisualStyleBackColor = False
        '
        'cbLL_Licensing
        '
        Me.cbLL_Licensing.AutoSize = True
        Me.cbLL_Licensing.BackColor = System.Drawing.SystemColors.Control
        Me.cbLL_Licensing.Checked = True
        Me.cbLL_Licensing.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbLL_Licensing.CustomBackground = True
        Me.cbLL_Licensing.Location = New System.Drawing.Point(6, 82)
        Me.cbLL_Licensing.Name = "cbLL_Licensing"
        Me.cbLL_Licensing.Size = New System.Drawing.Size(55, 15)
        Me.cbLL_Licensing.TabIndex = 33
        Me.cbLL_Licensing.TabStop = False
        Me.cbLL_Licensing.Text = "Lizenz"
        Me.cbLL_Licensing.UseVisualStyleBackColor = False
        '
        'cbLL_ApiCalls
        '
        Me.cbLL_ApiCalls.AutoSize = True
        Me.cbLL_ApiCalls.BackColor = System.Drawing.SystemColors.Control
        Me.cbLL_ApiCalls.Checked = True
        Me.cbLL_ApiCalls.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbLL_ApiCalls.CustomBackground = True
        Me.cbLL_ApiCalls.Location = New System.Drawing.Point(6, 61)
        Me.cbLL_ApiCalls.Name = "cbLL_ApiCalls"
        Me.cbLL_ApiCalls.Size = New System.Drawing.Size(69, 15)
        Me.cbLL_ApiCalls.TabIndex = 32
        Me.cbLL_ApiCalls.TabStop = False
        Me.cbLL_ApiCalls.Text = "API Calls"
        Me.cbLL_ApiCalls.UseVisualStyleBackColor = False
        '
        'cbLL_DotNet
        '
        Me.cbLL_DotNet.AutoSize = True
        Me.cbLL_DotNet.BackColor = System.Drawing.SystemColors.Control
        Me.cbLL_DotNet.Checked = True
        Me.cbLL_DotNet.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbLL_DotNet.CustomBackground = True
        Me.cbLL_DotNet.Location = New System.Drawing.Point(6, 40)
        Me.cbLL_DotNet.Name = "cbLL_DotNet"
        Me.cbLL_DotNet.Size = New System.Drawing.Size(47, 15)
        Me.cbLL_DotNet.TabIndex = 31
        Me.cbLL_DotNet.TabStop = False
        Me.cbLL_DotNet.Text = ".NET"
        Me.cbLL_DotNet.UseVisualStyleBackColor = False
        '
        'cbLL_DataProvider
        '
        Me.cbLL_DataProvider.AutoSize = True
        Me.cbLL_DataProvider.BackColor = System.Drawing.SystemColors.Control
        Me.cbLL_DataProvider.Checked = True
        Me.cbLL_DataProvider.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbLL_DataProvider.CustomBackground = True
        Me.cbLL_DataProvider.Location = New System.Drawing.Point(6, 19)
        Me.cbLL_DataProvider.Name = "cbLL_DataProvider"
        Me.cbLL_DataProvider.Size = New System.Drawing.Size(94, 15)
        Me.cbLL_DataProvider.TabIndex = 30
        Me.cbLL_DataProvider.TabStop = False
        Me.cbLL_DataProvider.Text = "Data Provider"
        Me.cbLL_DataProvider.UseVisualStyleBackColor = False
        '
        'cbLL_Debug
        '
        Me.cbLL_Debug.AutoSize = True
        Me.cbLL_Debug.BackColor = System.Drawing.SystemColors.Control
        Me.cbLL_Debug.Checked = True
        Me.cbLL_Debug.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbLL_Debug.CustomBackground = True
        Me.cbLL_Debug.Location = New System.Drawing.Point(104, 19)
        Me.cbLL_Debug.Name = "cbLL_Debug"
        Me.cbLL_Debug.Size = New System.Drawing.Size(58, 15)
        Me.cbLL_Debug.TabIndex = 29
        Me.cbLL_Debug.TabStop = False
        Me.cbLL_Debug.Text = "Debug"
        Me.cbLL_Debug.UseVisualStyleBackColor = False
        '
        'BtnLoadFromDataBase
        '
        Me.BtnLoadFromDataBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnLoadFromDataBase.Enabled = False
        Me.BtnLoadFromDataBase.Image = Global.WinBack.My.Resources.Resources.MySQL_32x32
        Me.BtnLoadFromDataBase.Location = New System.Drawing.Point(874, 462)
        Me.BtnLoadFromDataBase.Name = "BtnLoadFromDataBase"
        Me.BtnLoadFromDataBase.Size = New System.Drawing.Size(168, 39)
        Me.BtnLoadFromDataBase.TabIndex = 31
        Me.BtnLoadFromDataBase.Text = "winback.Log4Net laden"
        Me.BtnLoadFromDataBase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnLoadFromDataBase.UseVisualStyleBackColor = True
        '
        'wb_Admin_Log
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1044, 603)
        Me.Controls.Add(Me.BtnLoadFromDataBase)
        Me.Controls.Add(Me.grpListLabel)
        Me.Controls.Add(Me.grpLogger)
        Me.Controls.Add(Me.BtnEditKonfig)
        Me.Controls.Add(Me.BtnLog4Viewer)
        Me.Controls.Add(Me.BtnLoadKonfig)
        Me.Controls.Add(Me.BtnLoadTextFile)
        Me.Controls.Add(Me.tbLogger)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "wb_Admin_Log"
        Me.Text = "Ausgabe Log-Daten"
        Me.grpLogger.ResumeLayout(False)
        Me.grpLogger.PerformLayout()
        Me.grpListLabel.ResumeLayout(False)
        Me.grpListLabel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbLogger As System.Windows.Forms.TextBox
    Friend WithEvents BtnLoadTextFile As System.Windows.Forms.Button
    Friend WithEvents BtnLoadKonfig As System.Windows.Forms.Button
    Friend WithEvents BtnLog4Viewer As System.Windows.Forms.Button
    Friend WithEvents BtnEditKonfig As System.Windows.Forms.Button
    Friend WithEvents grpLogger As System.Windows.Forms.GroupBox
    Private WithEvents cbDebug As MetroFramework.Controls.MetroCheckBox
    Private WithEvents cbLogAutoStart As MetroFramework.Controls.MetroCheckBox
    Private WithEvents cbLogAktiv As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents tbAktLogger As System.Windows.Forms.TextBox
    Private WithEvents cbError As MetroFramework.Controls.MetroCheckBox
    Private WithEvents cbWarn As MetroFramework.Controls.MetroCheckBox
    Private WithEvents cbInfo As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents grpListLabel As System.Windows.Forms.GroupBox
    Private WithEvents cbLL_Error As MetroFramework.Controls.MetroCheckBox
    Private WithEvents cbLL_Warn As MetroFramework.Controls.MetroCheckBox
    Private WithEvents cbLL_Info As MetroFramework.Controls.MetroCheckBox
    Private WithEvents cbLL_Other As MetroFramework.Controls.MetroCheckBox
    Private WithEvents cbLL_PrinterInformation As MetroFramework.Controls.MetroCheckBox
    Private WithEvents cbLL_Licensing As MetroFramework.Controls.MetroCheckBox
    Private WithEvents cbLL_ApiCalls As MetroFramework.Controls.MetroCheckBox
    Private WithEvents cbLL_DotNet As MetroFramework.Controls.MetroCheckBox
    Private WithEvents cbLL_DataProvider As MetroFramework.Controls.MetroCheckBox
    Private WithEvents cbLL_Debug As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents BtnLoadFromDataBase As System.Windows.Forms.Button
End Class
