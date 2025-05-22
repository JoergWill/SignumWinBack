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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Admin_CheckDatabase))
        BtnStartCheck = New System.Windows.Forms.Button()
        rtLogger = New System.Windows.Forms.RichTextBox()
        BtnDBUpdates = New System.Windows.Forms.Button()
        BtnDoUpdate = New System.Windows.Forms.Button()
        SuspendLayout()
        ' 
        ' BtnStartCheck
        ' 
        BtnStartCheck.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        BtnStartCheck.Image = CType(resources.GetObject("BtnStartCheck.Image"), Drawing.Image)
        BtnStartCheck.Location = New System.Drawing.Point(897, 12)
        BtnStartCheck.Name = "BtnStartCheck"
        BtnStartCheck.Size = New System.Drawing.Size(135, 39)
        BtnStartCheck.TabIndex = 4
        BtnStartCheck.Text = "Systemcheck Starten"
        BtnStartCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnStartCheck.UseVisualStyleBackColor = True
        ' 
        ' rtLogger
        ' 
        rtLogger.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        rtLogger.BackColor = Drawing.Color.White
        rtLogger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        rtLogger.Location = New System.Drawing.Point(12, 12)
        rtLogger.Name = "rtLogger"
        rtLogger.ReadOnly = True
        rtLogger.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        rtLogger.Size = New System.Drawing.Size(879, 341)
        rtLogger.TabIndex = 5
        rtLogger.TabStop = False
        rtLogger.Text = ""
        ' 
        ' BtnDBUpdates
        ' 
        BtnDBUpdates.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        BtnDBUpdates.Enabled = False
        BtnDBUpdates.Image = CType(resources.GetObject("BtnDBUpdates.Image"), Drawing.Image)
        BtnDBUpdates.Location = New System.Drawing.Point(897, 269)
        BtnDBUpdates.Name = "BtnDBUpdates"
        BtnDBUpdates.Size = New System.Drawing.Size(135, 39)
        BtnDBUpdates.TabIndex = 6
        BtnDBUpdates.Text = "Datenbank Updates laden"
        BtnDBUpdates.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnDBUpdates.UseVisualStyleBackColor = True
        ' 
        ' BtnDoUpdate
        ' 
        BtnDoUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        BtnDoUpdate.Enabled = False
        BtnDoUpdate.Image = CType(resources.GetObject("BtnDoUpdate.Image"), Drawing.Image)
        BtnDoUpdate.Location = New System.Drawing.Point(897, 314)
        BtnDoUpdate.Name = "BtnDoUpdate"
        BtnDoUpdate.Size = New System.Drawing.Size(135, 39)
        BtnDoUpdate.TabIndex = 7
        BtnDoUpdate.Text = "Datenbank Updates durchführen"
        BtnDoUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        BtnDoUpdate.UseVisualStyleBackColor = True
        ' 
        ' wb_Admin_CheckDatabase
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(1044, 365)
        Controls.Add(BtnDoUpdate)
        Controls.Add(BtnDBUpdates)
        Controls.Add(rtLogger)
        Controls.Add(BtnStartCheck)
        Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        Name = "wb_Admin_CheckDatabase"
        Text = "WinBack System-Check"
        ResumeLayout(False)

    End Sub
    Friend WithEvents BtnStartCheck As System.Windows.Forms.Button
    Friend WithEvents rtLogger As System.Windows.Forms.RichTextBox
    Friend WithEvents BtnDBUpdates As System.Windows.Forms.Button
    Friend WithEvents BtnDoUpdate As System.Windows.Forms.Button
End Class
