Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rohstoffe_Cloud
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Rohstoffe_Cloud))
        Me.Wb_TabControl = New WinBack.wb_TabControl()
        Me.tpCloudSuchen = New System.Windows.Forms.TabPage()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnCloud = New System.Windows.Forms.Button()
        Me.lblUeberschrift = New System.Windows.Forms.Label()
        Me.tRohstoffNummer = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tRohstoffName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tpCloudGefunden = New System.Windows.Forms.TabPage()
        Me.tpCloudAnzeige = New System.Windows.Forms.TabPage()
        Me.tpCloudLoeschen = New System.Windows.Forms.TabPage()
        Me.tRohstoffLieferant = New System.Windows.Forms.TextBox()
        Me.lblLieferant = New System.Windows.Forms.Label()
        Me.Wb_TabControl.SuspendLayout()
        Me.tpCloudSuchen.SuspendLayout()
        Me.SuspendLayout()
        '
        'Wb_TabControl
        '
        Me.Wb_TabControl.Controls.Add(Me.tpCloudSuchen)
        Me.Wb_TabControl.Controls.Add(Me.tpCloudGefunden)
        Me.Wb_TabControl.Controls.Add(Me.tpCloudAnzeige)
        Me.Wb_TabControl.Controls.Add(Me.tpCloudLoeschen)
        Me.Wb_TabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Wb_TabControl.Location = New System.Drawing.Point(0, 0)
        Me.Wb_TabControl.Multiline = True
        Me.Wb_TabControl.Name = "Wb_TabControl"
        Me.Wb_TabControl.SelectedIndex = 0
        Me.Wb_TabControl.Size = New System.Drawing.Size(813, 434)
        Me.Wb_TabControl.TabIndex = 0
        '
        'tpCloudSuchen
        '
        Me.tpCloudSuchen.Controls.Add(Me.tRohstoffLieferant)
        Me.tpCloudSuchen.Controls.Add(Me.lblLieferant)
        Me.tpCloudSuchen.Controls.Add(Me.Button1)
        Me.tpCloudSuchen.Controls.Add(Me.lblUeberschrift)
        Me.tpCloudSuchen.Controls.Add(Me.tRohstoffNummer)
        Me.tpCloudSuchen.Controls.Add(Me.Label3)
        Me.tpCloudSuchen.Controls.Add(Me.tRohstoffName)
        Me.tpCloudSuchen.Controls.Add(Me.Label2)
        Me.tpCloudSuchen.Controls.Add(Me.btnCloud)
        Me.tpCloudSuchen.Location = New System.Drawing.Point(4, 23)
        Me.tpCloudSuchen.Name = "tpCloudSuchen"
        Me.tpCloudSuchen.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCloudSuchen.Size = New System.Drawing.Size(805, 407)
        Me.tpCloudSuchen.TabIndex = 0
        Me.tpCloudSuchen.Text = "CloudSearch"
        Me.tpCloudSuchen.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(616, 258)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(138, 62)
        Me.Button1.TabIndex = 42
        Me.Button1.Text = "Datenlink"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnCloud
        '
        Me.btnCloud.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCloud.Image = Global.WinBack.My.Resources.Resources.RohstoffeCloud_32x32
        Me.btnCloud.Location = New System.Drawing.Point(616, 167)
        Me.btnCloud.Name = "btnCloud"
        Me.btnCloud.Size = New System.Drawing.Size(138, 62)
        Me.btnCloud.TabIndex = 41
        Me.btnCloud.Text = "WinBack Cloud"
        Me.btnCloud.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCloud.UseVisualStyleBackColor = True
        '
        'lblUeberschrift
        '
        Me.lblUeberschrift.AutoSize = True
        Me.lblUeberschrift.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUeberschrift.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblUeberschrift.Location = New System.Drawing.Point(24, 13)
        Me.lblUeberschrift.Name = "lblUeberschrift"
        Me.lblUeberschrift.Size = New System.Drawing.Size(187, 24)
        Me.lblUeberschrift.TabIndex = 40
        Me.lblUeberschrift.Text = "Import der Nährwerte"
        '
        'tRohstoffNummer
        '
        Me.tRohstoffNummer.Location = New System.Drawing.Point(28, 70)
        Me.tRohstoffNummer.Name = "tRohstoffNummer"
        Me.tRohstoffNummer.Size = New System.Drawing.Size(136, 20)
        Me.tRohstoffNummer.TabIndex = 38
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(25, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "Nummer"
        '
        'tRohstoffName
        '
        Me.tRohstoffName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tRohstoffName.Location = New System.Drawing.Point(185, 70)
        Me.tRohstoffName.Name = "tRohstoffName"
        Me.tRohstoffName.Size = New System.Drawing.Size(588, 20)
        Me.tRohstoffName.TabIndex = 36
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(182, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 13)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Rohstoff-Bezeichnung"
        '
        'tpCloudGefunden
        '
        Me.tpCloudGefunden.Location = New System.Drawing.Point(4, 23)
        Me.tpCloudGefunden.Name = "tpCloudGefunden"
        Me.tpCloudGefunden.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCloudGefunden.Size = New System.Drawing.Size(805, 407)
        Me.tpCloudGefunden.TabIndex = 1
        Me.tpCloudGefunden.Text = "CloudFound"
        Me.tpCloudGefunden.UseVisualStyleBackColor = True
        '
        'tpCloudAnzeige
        '
        Me.tpCloudAnzeige.Location = New System.Drawing.Point(4, 23)
        Me.tpCloudAnzeige.Name = "tpCloudAnzeige"
        Me.tpCloudAnzeige.Size = New System.Drawing.Size(805, 407)
        Me.tpCloudAnzeige.TabIndex = 2
        Me.tpCloudAnzeige.Text = "CloudShow"
        Me.tpCloudAnzeige.UseVisualStyleBackColor = True
        '
        'tpCloudLoeschen
        '
        Me.tpCloudLoeschen.Location = New System.Drawing.Point(4, 23)
        Me.tpCloudLoeschen.Name = "tpCloudLoeschen"
        Me.tpCloudLoeschen.Size = New System.Drawing.Size(805, 407)
        Me.tpCloudLoeschen.TabIndex = 3
        Me.tpCloudLoeschen.Text = "CloudDisconnect"
        Me.tpCloudLoeschen.UseVisualStyleBackColor = True
        '
        'tRohstoffLieferant
        '
        Me.tRohstoffLieferant.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tRohstoffLieferant.Location = New System.Drawing.Point(185, 109)
        Me.tRohstoffLieferant.Name = "tRohstoffLieferant"
        Me.tRohstoffLieferant.Size = New System.Drawing.Size(588, 20)
        Me.tRohstoffLieferant.TabIndex = 43
        '
        'lblLieferant
        '
        Me.lblLieferant.AutoSize = True
        Me.lblLieferant.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblLieferant.Location = New System.Drawing.Point(182, 93)
        Me.lblLieferant.Name = "lblLieferant"
        Me.lblLieferant.Size = New System.Drawing.Size(91, 13)
        Me.lblLieferant.TabIndex = 44
        Me.lblLieferant.Text = "Rohstoff-Lieferant"
        '
        'wb_Rohstoffe_Cloud
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(813, 434)
        Me.Controls.Add(Me.Wb_TabControl)
        Me.Name = "wb_Rohstoffe_Cloud"
        Me.Text = "Nährwerte Verbindung zur Cloud"
        Me.Wb_TabControl.ResumeLayout(False)
        Me.tpCloudSuchen.ResumeLayout(False)
        Me.tpCloudSuchen.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Wb_TabControl As wb_TabControl
    Friend WithEvents tpCloudSuchen As Windows.Forms.TabPage
    Friend WithEvents tpCloudGefunden As Windows.Forms.TabPage
    Friend WithEvents tpCloudAnzeige As Windows.Forms.TabPage
    Friend WithEvents tpCloudLoeschen As Windows.Forms.TabPage
    Friend WithEvents lblUeberschrift As Windows.Forms.Label
    Friend WithEvents tRohstoffNummer As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents tRohstoffName As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents btnCloud As Windows.Forms.Button
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents tRohstoffLieferant As Windows.Forms.TextBox
    Friend WithEvents lblLieferant As Windows.Forms.Label
End Class
