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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_User_Details))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel = New System.Windows.Forms.Panel()
        Me.cbUserGrp = New Signum.OrgaSoft.AddIn.wb_ComboBox()
        Me.tUserPass = New System.Windows.Forms.TextBox()
        Me.tUserName = New System.Windows.Forms.TextBox()
        Me.Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'Panel
        '
        resources.ApplyResources(Me.Panel, "Panel")
        Me.Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel.Controls.Add(Me.cbUserGrp)
        Me.Panel.Controls.Add(Me.tUserPass)
        Me.Panel.Controls.Add(Me.tUserName)
        Me.Panel.Controls.Add(Me.Label2)
        Me.Panel.Controls.Add(Me.Label1)
        Me.Panel.ForeColor = System.Drawing.Color.Black
        Me.Panel.Name = "Panel"
        '
        'cbUserGrp
        '
        resources.ApplyResources(Me.cbUserGrp, "cbUserGrp")
        Me.cbUserGrp.FormattingEnabled = True
        Me.cbUserGrp.Name = "cbUserGrp"
        '
        'tUserPass
        '
        resources.ApplyResources(Me.tUserPass, "tUserPass")
        Me.tUserPass.Name = "tUserPass"
        '
        'tUserName
        '
        resources.ApplyResources(Me.tUserName, "tUserName")
        Me.tUserName.Name = "tUserName"
        '
        'wb_User_Details
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel)
        Me.Name = "wb_User_Details"
        Me.Panel.ResumeLayout(False)
        Me.Panel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Panel As Windows.Forms.Panel
    Friend WithEvents tUserName As Windows.Forms.TextBox
    Friend WithEvents tUserPass As Windows.Forms.TextBox
    Friend WithEvents cbUserGrp As wb_ComboBox
End Class
