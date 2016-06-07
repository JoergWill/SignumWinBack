Imports WeifenLuo.WinFormsUI.Docking
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Linien_Details
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Linien_Details))
        Me.Panel = New System.Windows.Forms.Panel()
        Me.tAdresse = New System.Windows.Forms.TextBox()
        Me.tBezeichnung = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel
        '
        resources.ApplyResources(Me.Panel, "Panel")
        Me.Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel.Controls.Add(Me.tAdresse)
        Me.Panel.Controls.Add(Me.tBezeichnung)
        Me.Panel.Controls.Add(Me.Label2)
        Me.Panel.Controls.Add(Me.Label1)
        Me.Panel.ForeColor = System.Drawing.Color.Black
        Me.Panel.Name = "Panel"
        '
        'tAdresse
        '
        resources.ApplyResources(Me.tAdresse, "tAdresse")
        Me.tAdresse.Name = "tAdresse"
        '
        'tBezeichnung
        '
        resources.ApplyResources(Me.tBezeichnung, "tBezeichnung")
        Me.tBezeichnung.Name = "tBezeichnung"
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
        'wb_Linien_Details
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.Controls.Add(Me.Panel)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wb_Linien_Details"
        Me.Panel.ResumeLayout(False)
        Me.Panel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel As Windows.Forms.Panel
    Friend WithEvents tAdresse As Windows.Forms.TextBox
    Friend WithEvents tBezeichnung As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
End Class
