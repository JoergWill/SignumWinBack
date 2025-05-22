<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_TwinCatADS
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
        Me.BtnConnect = New System.Windows.Forms.Button()
        Me.cbTwinCatADSaktiv = New System.Windows.Forms.CheckBox()
        Me.BtnRead = New System.Windows.Forms.Button()
        Me.BtnWriteEAN = New System.Windows.Forms.Button()
        Me.lblConnected = New System.Windows.Forms.Label()
        Me.lblBehNr = New System.Windows.Forms.Label()
        Me.BtnDBRead = New System.Windows.Forms.Button()
        Me.lblTwinCatString = New System.Windows.Forms.Label()
        Me.lblGebinde = New System.Windows.Forms.Label()
        Me.lblBilanz = New System.Windows.Forms.Label()
        Me.BtnDBReadBilanz = New System.Windows.Forms.Button()
        Me.lblTwinCatString_Bilanz = New System.Windows.Forms.Label()
        Me.BtnWriteLager = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnConnect
        '
        Me.BtnConnect.Location = New System.Drawing.Point(44, 62)
        Me.BtnConnect.Name = "BtnConnect"
        Me.BtnConnect.Size = New System.Drawing.Size(108, 55)
        Me.BtnConnect.TabIndex = 0
        Me.BtnConnect.Text = "Connect"
        Me.BtnConnect.UseVisualStyleBackColor = True
        '
        'cbTwinCatADSaktiv
        '
        Me.cbTwinCatADSaktiv.AutoSize = True
        Me.cbTwinCatADSaktiv.Location = New System.Drawing.Point(44, 39)
        Me.cbTwinCatADSaktiv.Name = "cbTwinCatADSaktiv"
        Me.cbTwinCatADSaktiv.Size = New System.Drawing.Size(116, 17)
        Me.cbTwinCatADSaktiv.TabIndex = 1
        Me.cbTwinCatADSaktiv.Text = "TwinCat ADS aktiv"
        Me.cbTwinCatADSaktiv.UseVisualStyleBackColor = True
        '
        'BtnRead
        '
        Me.BtnRead.Location = New System.Drawing.Point(44, 123)
        Me.BtnRead.Name = "BtnRead"
        Me.BtnRead.Size = New System.Drawing.Size(108, 55)
        Me.BtnRead.TabIndex = 2
        Me.BtnRead.Text = "TwinCat Read"
        Me.BtnRead.UseVisualStyleBackColor = True
        '
        'BtnWriteEAN
        '
        Me.BtnWriteEAN.Location = New System.Drawing.Point(44, 363)
        Me.BtnWriteEAN.Name = "BtnWriteEAN"
        Me.BtnWriteEAN.Size = New System.Drawing.Size(108, 55)
        Me.BtnWriteEAN.TabIndex = 3
        Me.BtnWriteEAN.Text = "Write EAN"
        Me.BtnWriteEAN.UseVisualStyleBackColor = True
        '
        'lblConnected
        '
        Me.lblConnected.AutoSize = True
        Me.lblConnected.ForeColor = System.Drawing.Color.Green
        Me.lblConnected.Location = New System.Drawing.Point(158, 71)
        Me.lblConnected.Name = "lblConnected"
        Me.lblConnected.Size = New System.Drawing.Size(59, 13)
        Me.lblConnected.TabIndex = 4
        Me.lblConnected.Text = "Connected"
        '
        'lblBehNr
        '
        Me.lblBehNr.AutoSize = True
        Me.lblBehNr.ForeColor = System.Drawing.Color.Black
        Me.lblBehNr.Location = New System.Drawing.Point(158, 123)
        Me.lblBehNr.Name = "lblBehNr"
        Me.lblBehNr.Size = New System.Drawing.Size(16, 13)
        Me.lblBehNr.TabIndex = 5
        Me.lblBehNr.Text = "..."
        '
        'BtnDBRead
        '
        Me.BtnDBRead.Location = New System.Drawing.Point(44, 212)
        Me.BtnDBRead.Name = "BtnDBRead"
        Me.BtnDBRead.Size = New System.Drawing.Size(108, 55)
        Me.BtnDBRead.TabIndex = 6
        Me.BtnDBRead.Text = "DB-Read EAN"
        Me.BtnDBRead.UseVisualStyleBackColor = True
        '
        'lblTwinCatString
        '
        Me.lblTwinCatString.AutoSize = True
        Me.lblTwinCatString.ForeColor = System.Drawing.Color.Black
        Me.lblTwinCatString.Location = New System.Drawing.Point(158, 223)
        Me.lblTwinCatString.Name = "lblTwinCatString"
        Me.lblTwinCatString.Size = New System.Drawing.Size(59, 13)
        Me.lblTwinCatString.TabIndex = 7
        Me.lblTwinCatString.Text = "Connected"
        '
        'lblGebinde
        '
        Me.lblGebinde.AutoSize = True
        Me.lblGebinde.ForeColor = System.Drawing.Color.Black
        Me.lblGebinde.Location = New System.Drawing.Point(158, 144)
        Me.lblGebinde.Name = "lblGebinde"
        Me.lblGebinde.Size = New System.Drawing.Size(16, 13)
        Me.lblGebinde.TabIndex = 8
        Me.lblGebinde.Text = "..."
        '
        'lblBilanz
        '
        Me.lblBilanz.AutoSize = True
        Me.lblBilanz.ForeColor = System.Drawing.Color.Black
        Me.lblBilanz.Location = New System.Drawing.Point(158, 165)
        Me.lblBilanz.Name = "lblBilanz"
        Me.lblBilanz.Size = New System.Drawing.Size(16, 13)
        Me.lblBilanz.TabIndex = 9
        Me.lblBilanz.Text = "..."
        '
        'BtnDBReadBilanz
        '
        Me.BtnDBReadBilanz.Location = New System.Drawing.Point(44, 273)
        Me.BtnDBReadBilanz.Name = "BtnDBReadBilanz"
        Me.BtnDBReadBilanz.Size = New System.Drawing.Size(108, 55)
        Me.BtnDBReadBilanz.TabIndex = 10
        Me.BtnDBReadBilanz.Text = "DB-Read Bilanz"
        Me.BtnDBReadBilanz.UseVisualStyleBackColor = True
        '
        'lblTwinCatString_Bilanz
        '
        Me.lblTwinCatString_Bilanz.AutoSize = True
        Me.lblTwinCatString_Bilanz.ForeColor = System.Drawing.Color.Black
        Me.lblTwinCatString_Bilanz.Location = New System.Drawing.Point(158, 284)
        Me.lblTwinCatString_Bilanz.Name = "lblTwinCatString_Bilanz"
        Me.lblTwinCatString_Bilanz.Size = New System.Drawing.Size(59, 13)
        Me.lblTwinCatString_Bilanz.TabIndex = 11
        Me.lblTwinCatString_Bilanz.Text = "Connected"
        '
        'BtnWriteLager
        '
        Me.BtnWriteLager.Location = New System.Drawing.Point(161, 363)
        Me.BtnWriteLager.Name = "BtnWriteLager"
        Me.BtnWriteLager.Size = New System.Drawing.Size(108, 55)
        Me.BtnWriteLager.TabIndex = 12
        Me.BtnWriteLager.Text = "Write Lager"
        Me.BtnWriteLager.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(44, 450)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(108, 55)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "ReadUser"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'wb_Admin_TwinCatADS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(957, 571)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.BtnWriteLager)
        Me.Controls.Add(Me.lblTwinCatString_Bilanz)
        Me.Controls.Add(Me.BtnDBReadBilanz)
        Me.Controls.Add(Me.lblBilanz)
        Me.Controls.Add(Me.lblGebinde)
        Me.Controls.Add(Me.lblTwinCatString)
        Me.Controls.Add(Me.BtnDBRead)
        Me.Controls.Add(Me.lblBehNr)
        Me.Controls.Add(Me.lblConnected)
        Me.Controls.Add(Me.BtnWriteEAN)
        Me.Controls.Add(Me.BtnRead)
        Me.Controls.Add(Me.cbTwinCatADSaktiv)
        Me.Controls.Add(Me.BtnConnect)
        Me.Name = "wb_Admin_TwinCatADS"
        Me.Text = "TwinCat ADS"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnConnect As System.Windows.Forms.Button
    Friend WithEvents cbTwinCatADSaktiv As System.Windows.Forms.CheckBox
    Friend WithEvents BtnRead As System.Windows.Forms.Button
    Friend WithEvents BtnWriteEAN As System.Windows.Forms.Button
    Friend WithEvents lblConnected As System.Windows.Forms.Label
    Friend WithEvents lblBehNr As System.Windows.Forms.Label
    Friend WithEvents BtnDBRead As System.Windows.Forms.Button
    Friend WithEvents lblTwinCatString As System.Windows.Forms.Label
    Friend WithEvents lblGebinde As System.Windows.Forms.Label
    Friend WithEvents lblBilanz As System.Windows.Forms.Label
    Friend WithEvents BtnDBReadBilanz As System.Windows.Forms.Button
    Friend WithEvents lblTwinCatString_Bilanz As System.Windows.Forms.Label
    Friend WithEvents BtnWriteLager As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
