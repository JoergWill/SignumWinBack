<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Rohstoffe_Main
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Rohstoffe_Main))
        Me.DockPanel = New WeifenLuo.WinFormsUI.Docking.DockPanel()
        Me.SuspendLayout()
        '
        'DockPanel
        '
        resources.ApplyResources(Me.DockPanel, "DockPanel")
        Me.DockPanel.BackColor = System.Drawing.Color.Gainsboro
        Me.DockPanel.DockBackColor = System.Drawing.Color.Gainsboro
        Me.DockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow
        Me.DockPanel.Name = "DockPanel"
        '
        'Rohstoffe_Main
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DockPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Rohstoffe_Main"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DockPanel As WeifenLuo.WinFormsUI.Docking.DockPanel
End Class
