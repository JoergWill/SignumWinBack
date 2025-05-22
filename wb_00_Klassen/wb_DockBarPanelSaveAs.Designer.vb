<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_DockBarPanelSaveAs
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_DockBarPanelSaveAs))
        Me.BtnSpeichern = New System.Windows.Forms.Button()
        Me.BtnAbbruch = New System.Windows.Forms.Button()
        Me.BtnExport = New System.Windows.Forms.Button()
        Me.clLayouts = New System.Windows.Forms.CheckedListBox()
        Me.lblBezeichnung = New System.Windows.Forms.Label()
        Me.lblSprache = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbBezeichnung = New System.Windows.Forms.TextBox()
        Me.cbGlobal = New System.Windows.Forms.CheckBox()
        Me.tbSprachen = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'BtnSpeichern
        '
        Me.BtnSpeichern.Location = New System.Drawing.Point(11, 354)
        Me.BtnSpeichern.Name = "BtnSpeichern"
        Me.BtnSpeichern.Size = New System.Drawing.Size(104, 28)
        Me.BtnSpeichern.TabIndex = 0
        Me.BtnSpeichern.Text = "Speichern"
        Me.BtnSpeichern.UseVisualStyleBackColor = True
        '
        'BtnAbbruch
        '
        Me.BtnAbbruch.Location = New System.Drawing.Point(121, 354)
        Me.BtnAbbruch.Name = "BtnAbbruch"
        Me.BtnAbbruch.Size = New System.Drawing.Size(104, 28)
        Me.BtnAbbruch.TabIndex = 1
        Me.BtnAbbruch.Text = "Abbruch"
        Me.BtnAbbruch.UseVisualStyleBackColor = True
        '
        'BtnExport
        '
        Me.BtnExport.Location = New System.Drawing.Point(231, 354)
        Me.BtnExport.Name = "BtnExport"
        Me.BtnExport.Size = New System.Drawing.Size(104, 28)
        Me.BtnExport.TabIndex = 2
        Me.BtnExport.Text = "Export"
        Me.BtnExport.UseVisualStyleBackColor = True
        '
        'clLayouts
        '
        Me.clLayouts.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clLayouts.FormattingEnabled = True
        Me.clLayouts.Location = New System.Drawing.Point(15, 114)
        Me.clLayouts.Name = "clLayouts"
        Me.clLayouts.Size = New System.Drawing.Size(320, 228)
        Me.clLayouts.TabIndex = 3
        '
        'lblBezeichnung
        '
        Me.lblBezeichnung.AutoSize = True
        Me.lblBezeichnung.Location = New System.Drawing.Point(12, 80)
        Me.lblBezeichnung.Name = "lblBezeichnung"
        Me.lblBezeichnung.Size = New System.Drawing.Size(72, 13)
        Me.lblBezeichnung.TabIndex = 4
        Me.lblBezeichnung.Text = "Bezeichnung:"
        '
        'lblSprache
        '
        Me.lblSprache.AutoSize = True
        Me.lblSprache.Location = New System.Drawing.Point(12, 50)
        Me.lblSprache.Name = "lblSprache"
        Me.lblSprache.Size = New System.Drawing.Size(50, 13)
        Me.lblSprache.TabIndex = 5
        Me.lblSprache.Text = "Sprache:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Global:"
        '
        'tbBezeichnung
        '
        Me.tbBezeichnung.Location = New System.Drawing.Point(83, 77)
        Me.tbBezeichnung.Name = "tbBezeichnung"
        Me.tbBezeichnung.Size = New System.Drawing.Size(252, 20)
        Me.tbBezeichnung.TabIndex = 7
        '
        'cbGlobal
        '
        Me.cbGlobal.AutoSize = True
        Me.cbGlobal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbGlobal.Location = New System.Drawing.Point(83, 22)
        Me.cbGlobal.Name = "cbGlobal"
        Me.cbGlobal.Size = New System.Drawing.Size(15, 14)
        Me.cbGlobal.TabIndex = 8
        Me.cbGlobal.UseVisualStyleBackColor = True
        '
        'tbSprachen
        '
        Me.tbSprachen.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.tbSprachen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbSprachen.Enabled = False
        Me.tbSprachen.ForeColor = System.Drawing.Color.Black
        Me.tbSprachen.Location = New System.Drawing.Point(83, 47)
        Me.tbSprachen.Name = "tbSprachen"
        Me.tbSprachen.ReadOnly = True
        Me.tbSprachen.Size = New System.Drawing.Size(252, 20)
        Me.tbSprachen.TabIndex = 9
        Me.tbSprachen.Text = "- Alle Sprachen"
        '
        'wb_DockBarPanelSaveAs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(350, 394)
        Me.Controls.Add(Me.tbSprachen)
        Me.Controls.Add(Me.cbGlobal)
        Me.Controls.Add(Me.tbBezeichnung)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblSprache)
        Me.Controls.Add(Me.lblBezeichnung)
        Me.Controls.Add(Me.clLayouts)
        Me.Controls.Add(Me.BtnExport)
        Me.Controls.Add(Me.BtnAbbruch)
        Me.Controls.Add(Me.BtnSpeichern)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wb_DockBarPanelSaveAs"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Layout speichern als..."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSpeichern As System.Windows.Forms.Button
    Friend WithEvents BtnAbbruch As System.Windows.Forms.Button
    Friend WithEvents BtnExport As System.Windows.Forms.Button
    Friend WithEvents clLayouts As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblBezeichnung As System.Windows.Forms.Label
    Friend WithEvents lblSprache As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbBezeichnung As System.Windows.Forms.TextBox
    Friend WithEvents cbGlobal As System.Windows.Forms.CheckBox
    Friend WithEvents tbSprachen As System.Windows.Forms.TextBox
End Class
