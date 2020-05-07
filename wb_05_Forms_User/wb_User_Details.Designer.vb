Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_User_Details
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_User_Details))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.tPersonalNr = New System.Windows.Forms.TextBox()
        Me.tUserPass = New System.Windows.Forms.TextBox()
        Me.tUserName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbUserGrp = New WinBack.wb_ComboBox()
        Me.SuspendLayout()
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'lblName
        '
        resources.ApplyResources(Me.lblName, "lblName")
        Me.lblName.Name = "lblName"
        '
        'tPersonalNr
        '
        resources.ApplyResources(Me.tPersonalNr, "tPersonalNr")
        Me.tPersonalNr.Name = "tPersonalNr"
        Me.tPersonalNr.ReadOnly = True
        '
        'tUserPass
        '
        resources.ApplyResources(Me.tUserPass, "tUserPass")
        Me.tUserPass.Name = "tUserPass"
        Me.tUserPass.ReadOnly = True
        '
        'tUserName
        '
        resources.ApplyResources(Me.tUserName, "tUserName")
        Me.tUserName.Name = "tUserName"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'cbUserGrp
        '
        Me.cbUserGrp.FormattingEnabled = True
        resources.ApplyResources(Me.cbUserGrp, "cbUserGrp")
        Me.cbUserGrp.Name = "cbUserGrp"
        Me.cbUserGrp.TabStop = False
        '
        'wb_User_Details
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tPersonalNr)
        Me.Controls.Add(Me.cbUserGrp)
        Me.Controls.Add(Me.tUserPass)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.tUserName)
        Me.Controls.Add(Me.Label2)
        Me.Name = "wb_User_Details"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents lblName As Windows.Forms.Label
    Friend WithEvents tUserName As Windows.Forms.TextBox
    Friend WithEvents tUserPass As Windows.Forms.TextBox
    Friend WithEvents cbUserGrp As wb_ComboBox
    Friend WithEvents tPersonalNr As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
End Class
