Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_Sync
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Admin_Sync))
        Me.BtnSync_UserGrp_Start = New System.Windows.Forms.Button()
        Me.SyncErgebnis = New System.Windows.Forms.ListBox()
        Me.BtnSync_Start = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnSync_UserGrp_Start
        '
        resources.ApplyResources(Me.BtnSync_UserGrp_Start, "BtnSync_UserGrp_Start")
        Me.BtnSync_UserGrp_Start.Name = "BtnSync_UserGrp_Start"
        Me.BtnSync_UserGrp_Start.UseVisualStyleBackColor = True
        '
        'SyncErgebnis
        '
        resources.ApplyResources(Me.SyncErgebnis, "SyncErgebnis")
        Me.SyncErgebnis.FormattingEnabled = True
        Me.SyncErgebnis.Name = "SyncErgebnis"
        '
        'BtnSync_Start
        '
        resources.ApplyResources(Me.BtnSync_Start, "BtnSync_Start")
        Me.BtnSync_Start.Name = "BtnSync_Start"
        Me.BtnSync_Start.UseVisualStyleBackColor = True
        '
        'wb_Admin_Sync
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BtnSync_Start)
        Me.Controls.Add(Me.SyncErgebnis)
        Me.Controls.Add(Me.BtnSync_UserGrp_Start)
        Me.Name = "wb_Admin_Sync"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnSync_UserGrp_Start As Windows.Forms.Button
    Friend WithEvents SyncErgebnis As Windows.Forms.ListBox
    Friend WithEvents BtnSync_Start As Windows.Forms.Button
End Class
