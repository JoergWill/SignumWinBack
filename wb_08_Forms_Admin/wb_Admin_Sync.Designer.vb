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
        Me.btnSyncUserGruppen = New System.Windows.Forms.Button()
        Me.tbSyncResult = New System.Windows.Forms.Panel()
        Me.btnSyncUser = New System.Windows.Forms.Button()
        Me.btnSyncStart = New System.Windows.Forms.Button()
        Me.btnExportExcel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnSyncUserGruppen
        '
        resources.ApplyResources(Me.btnSyncUserGruppen, "btnSyncUserGruppen")
        Me.btnSyncUserGruppen.Name = "btnSyncUserGruppen"
        Me.btnSyncUserGruppen.UseVisualStyleBackColor = True
        '
        'tbSyncResult
        '
        resources.ApplyResources(Me.tbSyncResult, "tbSyncResult")
        Me.tbSyncResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbSyncResult.Name = "tbSyncResult"
        '
        'btnSyncUser
        '
        resources.ApplyResources(Me.btnSyncUser, "btnSyncUser")
        Me.btnSyncUser.Name = "btnSyncUser"
        Me.btnSyncUser.UseVisualStyleBackColor = True
        '
        'btnSyncStart
        '
        resources.ApplyResources(Me.btnSyncStart, "btnSyncStart")
        Me.btnSyncStart.Name = "btnSyncStart"
        Me.btnSyncStart.UseVisualStyleBackColor = True
        '
        'btnExportExcel
        '
        resources.ApplyResources(Me.btnExportExcel, "btnExportExcel")
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.UseVisualStyleBackColor = True
        '
        'wb_Admin_Sync
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnExportExcel)
        Me.Controls.Add(Me.btnSyncStart)
        Me.Controls.Add(Me.btnSyncUser)
        Me.Controls.Add(Me.tbSyncResult)
        Me.Controls.Add(Me.btnSyncUserGruppen)
        Me.Name = "wb_Admin_Sync"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnSyncUserGruppen As Windows.Forms.Button
    Friend WithEvents tbSyncResult As Windows.Forms.Panel
    Friend WithEvents btnSyncUser As Windows.Forms.Button
    Friend WithEvents btnSyncStart As Windows.Forms.Button
    Friend WithEvents btnExportExcel As Windows.Forms.Button
End Class
