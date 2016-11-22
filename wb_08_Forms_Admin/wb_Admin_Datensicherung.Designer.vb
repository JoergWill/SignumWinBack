Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_Datensicherung
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Admin_Datensicherung))
        Me.Btn_DatenSicherung = New System.Windows.Forms.Button()
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.SaveFileName = New System.Windows.Forms.TextBox()
        Me.Btn_DatenRueckSicherung = New System.Windows.Forms.Button()
        Me.LoadFileName = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'Btn_DatenSicherung
        '
        Me.Btn_DatenSicherung.Image = Global.WinBack.My.Resources.Resources.DatenSicherung_32x32
        resources.ApplyResources(Me.Btn_DatenSicherung, "Btn_DatenSicherung")
        Me.Btn_DatenSicherung.Name = "Btn_DatenSicherung"
        Me.Btn_DatenSicherung.UseVisualStyleBackColor = True
        '
        'SaveFileDialog
        '
        resources.ApplyResources(Me.SaveFileDialog, "SaveFileDialog")
        '
        'SaveFileName
        '
        resources.ApplyResources(Me.SaveFileName, "SaveFileName")
        Me.SaveFileName.Name = "SaveFileName"
        '
        'Btn_DatenRueckSicherung
        '
        Me.Btn_DatenRueckSicherung.Image = Global.WinBack.My.Resources.Resources.DatenRueckSicherung_32x32
        resources.ApplyResources(Me.Btn_DatenRueckSicherung, "Btn_DatenRueckSicherung")
        Me.Btn_DatenRueckSicherung.Name = "Btn_DatenRueckSicherung"
        Me.Btn_DatenRueckSicherung.UseVisualStyleBackColor = True
        '
        'LoadFileName
        '
        resources.ApplyResources(Me.LoadFileName, "LoadFileName")
        Me.LoadFileName.Name = "LoadFileName"
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "*.sql"
        resources.ApplyResources(Me.OpenFileDialog, "OpenFileDialog")
        '
        'wb_Admin_Datensicherung
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LoadFileName)
        Me.Controls.Add(Me.Btn_DatenRueckSicherung)
        Me.Controls.Add(Me.SaveFileName)
        Me.Controls.Add(Me.Btn_DatenSicherung)
        Me.Name = "wb_Admin_Datensicherung"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Btn_DatenSicherung As Windows.Forms.Button
    Friend WithEvents SaveFileDialog As Windows.Forms.SaveFileDialog
    Friend WithEvents SaveFileName As Windows.Forms.TextBox
    Friend WithEvents Btn_DatenRueckSicherung As Windows.Forms.Button
    Friend WithEvents LoadFileName As Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog As Windows.Forms.OpenFileDialog
End Class
