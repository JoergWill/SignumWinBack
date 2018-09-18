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
        Dim Wb_MinMaxOptCharge1 As WinBack.wb_MinMaxOptCharge = New WinBack.wb_MinMaxOptCharge()
        Dim Wb_Charge1 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge2 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge3 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_MinMaxOptCharge2 As WinBack.wb_MinMaxOptCharge = New WinBack.wb_MinMaxOptCharge()
        Dim Wb_Charge4 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge5 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Dim Wb_Charge6 As WinBack.wb_Charge = New WinBack.wb_Charge()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.KompRzChargen = New WinBack.wb_KompRzChargen()
        Me.SuspendLayout()
        '
        'KompRzChargen
        '
        Wb_MinMaxOptCharge1.ErrorCheck = False
        Wb_MinMaxOptCharge1.HasChanged = False
        Wb_Charge1.MengeInkg = "0,000"
        Wb_Charge1.MengeInProzent = "0"
        Wb_Charge1.MengeInStk = "0"
        Wb_Charge1.StkGewicht = "1000"
        Wb_Charge1.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge1.MaxCharge = Wb_Charge1
        Wb_Charge2.MengeInkg = "0,000"
        Wb_Charge2.MengeInProzent = "0"
        Wb_Charge2.MengeInStk = "0"
        Wb_Charge2.StkGewicht = "1000"
        Wb_Charge2.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge1.MinCharge = Wb_Charge2
        Wb_Charge3.MengeInkg = "0,000"
        Wb_Charge3.MengeInProzent = "0"
        Wb_Charge3.MengeInStk = "0"
        Wb_Charge3.StkGewicht = "1000"
        Wb_Charge3.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge1.OptCharge = Wb_Charge3
        Wb_MinMaxOptCharge1.StkGewicht = "0"
        Wb_MinMaxOptCharge1.TeigGewicht = "0"
        Me.KompRzChargen.ArtikelChargen = Wb_MinMaxOptCharge1
        Me.KompRzChargen.DataValid = False
        Me.KompRzChargen.Location = New System.Drawing.Point(0, 0)
        Me.KompRzChargen.Name = "KompRzChargen"
        Me.KompRzChargen.RezeptName = ""
        Me.KompRzChargen.RezeptNummer = ""
        Me.KompRzChargen.RzNr = -1
        Me.KompRzChargen.Size = New System.Drawing.Size(400, 314)
        Me.KompRzChargen.TabIndex = 48
        Wb_MinMaxOptCharge2.ErrorCheck = False
        Wb_MinMaxOptCharge2.HasChanged = False
        Wb_Charge4.MengeInkg = "0,000"
        Wb_Charge4.MengeInProzent = "0"
        Wb_Charge4.MengeInStk = "0"
        Wb_Charge4.StkGewicht = "1000"
        Wb_Charge4.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge2.MaxCharge = Wb_Charge4
        Wb_Charge5.MengeInkg = "0,000"
        Wb_Charge5.MengeInProzent = "0"
        Wb_Charge5.MengeInStk = "0"
        Wb_Charge5.StkGewicht = "1000"
        Wb_Charge5.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge2.MinCharge = Wb_Charge5
        Wb_Charge6.MengeInkg = "0,000"
        Wb_Charge6.MengeInProzent = "0"
        Wb_Charge6.MengeInStk = "0"
        Wb_Charge6.StkGewicht = "1000"
        Wb_Charge6.TeigGewicht = "0,000"
        Wb_MinMaxOptCharge2.OptCharge = Wb_Charge6
        Wb_MinMaxOptCharge2.StkGewicht = "0"
        Wb_MinMaxOptCharge2.TeigGewicht = "0"
        Me.KompRzChargen.TeigChargen = Wb_MinMaxOptCharge2
        '
        'ob_Artikel_ZuordnungRezept
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoScrollMinSize = New System.Drawing.Size(190, 268)
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.KompRzChargen)
        Me.MinimumSize = New System.Drawing.Size(190, 268)
        Me.Name = "ob_Artikel_ZuordnungRezept"
        Me.Size = New System.Drawing.Size(404, 323)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip As Windows.Forms.ToolTip
    Friend WithEvents KompRzChargen As wb_KompRzChargen

    'Friend WithEvents PropertyGrid As Signum.OrgaSoft.GUI.Controls.PropertyGrid
End Class
