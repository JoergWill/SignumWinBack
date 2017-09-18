Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_Log
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
        Me.tbLogger = New System.Windows.Forms.TextBox()
        Me.cbLogTextFile = New MetroFramework.Controls.MetroToggle()
        Me.lblLogTxtFile = New System.Windows.Forms.Label()
        Me.MetroToolTip = New MetroFramework.Components.MetroToolTip()
        Me.BtnLoadTextFile = New System.Windows.Forms.Button()
        Me.cbLogDataBase = New MetroFramework.Controls.MetroToggle()
        Me.lblLogDataBase = New System.Windows.Forms.Label()
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
        'cbLogTextFile
        '
        Me.cbLogTextFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbLogTextFile.AutoSize = True
        Me.cbLogTextFile.DisplayStatus = False
        Me.cbLogTextFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbLogTextFile.Location = New System.Drawing.Point(897, 281)
        Me.cbLogTextFile.Name = "cbLogTextFile"
        Me.cbLogTextFile.Size = New System.Drawing.Size(50, 17)
        Me.cbLogTextFile.TabIndex = 2
        Me.cbLogTextFile.TabStop = False
        Me.cbLogTextFile.Text = "Aus"
        Me.MetroToolTip.SetToolTip(Me.cbLogTextFile, "alle Meldungen auch in eine Textdatei schreiben")
        Me.cbLogTextFile.UseVisualStyleBackColor = True
        '
        'lblLogTxtFile
        '
        Me.lblLogTxtFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLogTxtFile.Location = New System.Drawing.Point(953, 275)
        Me.lblLogTxtFile.Name = "lblLogTxtFile"
        Me.lblLogTxtFile.Size = New System.Drawing.Size(79, 29)
        Me.lblLogTxtFile.TabIndex = 3
        Me.lblLogTxtFile.Text = "Meldungen in Textdatei"
        '
        'BtnLoadTextFile
        '
        Me.BtnLoadTextFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnLoadTextFile.Location = New System.Drawing.Point(897, 314)
        Me.BtnLoadTextFile.Name = "BtnLoadTextFile"
        Me.BtnLoadTextFile.Size = New System.Drawing.Size(135, 39)
        Me.BtnLoadTextFile.TabIndex = 4
        Me.BtnLoadTextFile.Text = "Textfile laden"
        Me.MetroToolTip.SetToolTip(Me.BtnLoadTextFile, "Textfile aller Log-Daten laden (kann evtlentuell einige Zeit beanspruchen)")
        Me.BtnLoadTextFile.UseVisualStyleBackColor = True
        '
        'cbLogDataBase
        '
        Me.cbLogDataBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbLogDataBase.AutoSize = True
        Me.cbLogDataBase.DisplayStatus = False
        Me.cbLogDataBase.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbLogDataBase.Location = New System.Drawing.Point(897, 244)
        Me.cbLogDataBase.Name = "cbLogDataBase"
        Me.cbLogDataBase.Size = New System.Drawing.Size(50, 17)
        Me.cbLogDataBase.TabIndex = 5
        Me.cbLogDataBase.TabStop = False
        Me.cbLogDataBase.Text = "Aus"
        Me.MetroToolTip.SetToolTip(Me.cbLogDataBase, "alle Meldungen auch in der Datenbank sichern")
        Me.cbLogDataBase.UseVisualStyleBackColor = True
        '
        'lblLogDataBase
        '
        Me.lblLogDataBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLogDataBase.Location = New System.Drawing.Point(953, 238)
        Me.lblLogDataBase.Name = "lblLogDataBase"
        Me.lblLogDataBase.Size = New System.Drawing.Size(79, 29)
        Me.lblLogDataBase.TabIndex = 6
        Me.lblLogDataBase.Text = "Meldungen in Datenbank"
        '
        'wb_Admin_Log
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1044, 365)
        Me.Controls.Add(Me.lblLogDataBase)
        Me.Controls.Add(Me.cbLogDataBase)
        Me.Controls.Add(Me.BtnLoadTextFile)
        Me.Controls.Add(Me.lblLogTxtFile)
        Me.Controls.Add(Me.cbLogTextFile)
        Me.Controls.Add(Me.tbLogger)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "wb_Admin_Log"
        Me.Text = "Ausgabe Log-Daten"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbLogger As Windows.Forms.TextBox
    Friend WithEvents cbLogTextFile As MetroFramework.Controls.MetroToggle
    Friend WithEvents MetroToolTip As MetroFramework.Components.MetroToolTip
    Friend WithEvents lblLogTxtFile As Windows.Forms.Label
    Friend WithEvents BtnLoadTextFile As Windows.Forms.Button
    Friend WithEvents lblLogDataBase As Windows.Forms.Label
    Friend WithEvents cbLogDataBase As MetroFramework.Controls.MetroToggle
End Class
