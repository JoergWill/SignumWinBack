Imports Signum.OrgaSoft

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ob_Artikel_ZuordnungRezept
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Wb_MinMaxOptCharge3 As WinBack.wb_MinMaxOptCharge = New WinBack.wb_MinMaxOptCharge()
        Dim Wb_Charge7 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge8 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge9 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_MinMaxOptCharge4 As WinBack.wb_MinMaxOptCharge = New WinBack.wb_MinMaxOptCharge()
        Dim Wb_Charge10 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge11 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge12 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlNoProduction = New System.Windows.Forms.Panel()
        Me.lblProduktion = New System.Windows.Forms.Label()
        Me.KompRzChargen = New WinBack.wb_KompRzChargen()
        Me.pnlNoProduction.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlNoProduction
        '
        Me.pnlNoProduction.BackColor = System.Drawing.Color.Transparent
        Me.pnlNoProduction.Controls.Add(Me.lblProduktion)
        Me.pnlNoProduction.Location = New System.Drawing.Point(406, 0)
        Me.pnlNoProduction.Name = "pnlNoProduction"
        Me.pnlNoProduction.Size = New System.Drawing.Size(400, 314)
        Me.pnlNoProduction.TabIndex = 50
        '
        'lblProduktion
        '
        Me.lblProduktion.BackColor = System.Drawing.Color.Transparent
        Me.lblProduktion.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProduktion.ForeColor = System.Drawing.Color.IndianRed
        Me.lblProduktion.Location = New System.Drawing.Point(0, 105)
        Me.lblProduktion.Name = "lblProduktion"
        Me.lblProduktion.Size = New System.Drawing.Size(400, 89)
        Me.lblProduktion.TabIndex = 50
        Me.lblProduktion.Text = "Keine Verknüpfung zur Produktion"
        Me.lblProduktion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'KompRzChargen
        '
        Wb_MinMaxOptCharge3.ErrorCheck = False
        Wb_MinMaxOptCharge3.HasChanged = False
        Wb_Charge7.MengeInkg = "0,000"
        Wb_Charge7.MengeInProzent = "0"
        Wb_Charge7.MengeInStk = "0"
        Wb_Charge7.StkGewicht = "1000"
        Wb_Charge7.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge3.MaxCharge = Wb_Charge7
        Wb_Charge8.MengeInkg = "0,000"
        Wb_Charge8.MengeInProzent = "0"
        Wb_Charge8.MengeInStk = "0"
        Wb_Charge8.StkGewicht = "1000"
        Wb_Charge8.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge3.MinCharge = Wb_Charge8
        Wb_Charge9.MengeInkg = "0,000"
        Wb_Charge9.MengeInProzent = "0"
        Wb_Charge9.MengeInStk = "0"
        Wb_Charge9.StkGewicht = "1000"
        Wb_Charge9.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge3.OptCharge = Wb_Charge9
        Wb_MinMaxOptCharge3.StkGewicht = "1000"
        Wb_MinMaxOptCharge3.TeigGewicht = "0"
        Me.KompRzChargen.ArtikelChargen = Wb_MinMaxOptCharge3
        Me.KompRzChargen.DataValid = False
        Me.KompRzChargen.Location = New System.Drawing.Point(0, 0)
        Me.KompRzChargen.Name = "KompRzChargen"
        Me.KompRzChargen.RezeptName = ""
        Me.KompRzChargen.RezeptNummer = ""
        Me.KompRzChargen.RzNr = -1
        Me.KompRzChargen.Size = New System.Drawing.Size(400, 314)
        Me.KompRzChargen.TabIndex = 48
        Wb_MinMaxOptCharge4.ErrorCheck = False
        Wb_MinMaxOptCharge4.HasChanged = False
        Wb_Charge10.MengeInkg = "0,000"
        Wb_Charge10.MengeInProzent = "0"
        Wb_Charge10.MengeInStk = "0"
        Wb_Charge10.StkGewicht = "1000"
        Wb_Charge10.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge4.MaxCharge = Wb_Charge10
        Wb_Charge11.MengeInkg = "0,000"
        Wb_Charge11.MengeInProzent = "0"
        Wb_Charge11.MengeInStk = "0"
        Wb_Charge11.StkGewicht = "1000"
        Wb_Charge11.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge4.MinCharge = Wb_Charge11
        Wb_Charge12.MengeInkg = "0,000"
        Wb_Charge12.MengeInProzent = "0"
        Wb_Charge12.MengeInStk = "0"
        Wb_Charge12.StkGewicht = "1000"
        Wb_Charge12.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge4.OptCharge = Wb_Charge12
        Wb_MinMaxOptCharge4.StkGewicht = "1000"
        Wb_MinMaxOptCharge4.TeigGewicht = "0"
        Me.KompRzChargen.TeigChargen = Wb_MinMaxOptCharge4
        '
        'ob_Artikel_ZuordnungRezept
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoScrollMinSize = New System.Drawing.Size(190, 268)
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.pnlNoProduction)
        Me.Controls.Add(Me.KompRzChargen)
        Me.MinimumSize = New System.Drawing.Size(190, 268)
        Me.Name = "ob_Artikel_ZuordnungRezept"
        Me.Size = New System.Drawing.Size(818, 323)
        Me.pnlNoProduction.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip As Windows.Forms.ToolTip
    Friend WithEvents KompRzChargen As wb_KompRzChargen
    Friend WithEvents pnlNoProduction As Windows.Forms.Panel
    Friend WithEvents lblProduktion As Windows.Forms.Label

    'Friend WithEvents PropertyGrid As Signum.OrgaSoft.GUI.Controls.PropertyGrid
End Class
