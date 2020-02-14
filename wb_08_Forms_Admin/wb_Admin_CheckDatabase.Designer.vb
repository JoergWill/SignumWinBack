Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_CheckDatabase
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
        Me.BtnStartCheck = New System.Windows.Forms.Button()
        Me.rtLogger = New System.Windows.Forms.RichTextBox()
        Me.BtnDBUpdates = New System.Windows.Forms.Button()
        Me.BtnDoUpdate = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnStartCheck
        '
        Me.BtnStartCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnStartCheck.Image = Global.WinBack.My.Resources.Resources.IconDlgLog_16x16
        Me.BtnStartCheck.Location = New System.Drawing.Point(897, 12)
        Me.BtnStartCheck.Name = "BtnStartCheck"
        Me.BtnStartCheck.Size = New System.Drawing.Size(135, 39)
        Me.BtnStartCheck.TabIndex = 4
        Me.BtnStartCheck.Text = "Systemcheck Starten"
        Me.BtnStartCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnStartCheck.UseVisualStyleBackColor = True
        '
        'rtLogger
        '
        Me.rtLogger.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtLogger.BackColor = System.Drawing.Color.White
        Me.rtLogger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtLogger.Location = New System.Drawing.Point(12, 12)
        Me.rtLogger.Name = "rtLogger"
        Me.rtLogger.ReadOnly = True
        Me.rtLogger.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtLogger.Size = New System.Drawing.Size(879, 341)
        Me.rtLogger.TabIndex = 5
        Me.rtLogger.TabStop = False
        Me.rtLogger.Text = ""
        '
        'BtnDBUpdates
        '
        Me.BtnDBUpdates.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnDBUpdates.Enabled = False
        Me.BtnDBUpdates.Image = Global.WinBack.My.Resources.Resources.DatenRueckSicherung_16x16
        Me.BtnDBUpdates.Location = New System.Drawing.Point(897, 269)
        Me.BtnDBUpdates.Name = "BtnDBUpdates"
        Me.BtnDBUpdates.Size = New System.Drawing.Size(135, 39)
        Me.BtnDBUpdates.TabIndex = 6
        Me.BtnDBUpdates.Text = "Datenbank Updates laden"
        Me.BtnDBUpdates.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnDBUpdates.UseVisualStyleBackColor = True
        '
        'BtnDoUpdate
        '
        Me.BtnDoUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnDoUpdate.Enabled = False
        Me.BtnDoUpdate.Image = Global.WinBack.My.Resources.Resources.UpdateDataBase_16x16
        Me.BtnDoUpdate.Location = New System.Drawing.Point(897, 314)
        Me.BtnDoUpdate.Name = "BtnDoUpdate"
        Me.BtnDoUpdate.Size = New System.Drawing.Size(135, 39)
        Me.BtnDoUpdate.TabIndex = 7
        Me.BtnDoUpdate.Text = "Datenbank Updates durchführen"
        Me.BtnDoUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnDoUpdate.UseVisualStyleBackColor = True
        '
        'wb_Admin_CheckDatabase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1044, 365)
        Me.Controls.Add(Me.BtnDoUpdate)
        Me.Controls.Add(Me.BtnDBUpdates)
        Me.Controls.Add(Me.rtLogger)
        Me.Controls.Add(Me.BtnStartCheck)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "wb_Admin_CheckDatabase"
        Me.Text = "WinBack System-Check"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnStartCheck As Windows.Forms.Button
    Friend WithEvents rtLogger As Windows.Forms.RichTextBox
    Friend WithEvents BtnDBUpdates As Windows.Forms.Button
    Friend WithEvents BtnDoUpdate As Windows.Forms.Button
End Class
