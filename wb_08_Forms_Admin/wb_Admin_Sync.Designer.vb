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
        Me.lvSyncResult = New System.Windows.Forms.ListView()
        Me.clHeader0 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
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
        Me.tbSyncResult.BackColor = System.Drawing.Color.DarkGray
        Me.tbSyncResult.ForeColor = System.Drawing.SystemColors.InfoText
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
        'lvSyncResult
        '
        resources.ApplyResources(Me.lvSyncResult, "lvSyncResult")
        Me.lvSyncResult.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clHeader0, Me.clHeader1, Me.clHeader2, Me.clHeader3, Me.clHeader4, Me.clHeader5})
        Me.lvSyncResult.Name = "lvSyncResult"
        Me.lvSyncResult.UseCompatibleStateImageBehavior = False
        Me.lvSyncResult.View = System.Windows.Forms.View.Details
        '
        'clHeader0
        '
        resources.ApplyResources(Me.clHeader0, "clHeader0")
        '
        'clHeader1
        '
        resources.ApplyResources(Me.clHeader1, "clHeader1")
        '
        'clHeader2
        '
        resources.ApplyResources(Me.clHeader2, "clHeader2")
        '
        'clHeader3
        '
        resources.ApplyResources(Me.clHeader3, "clHeader3")
        '
        'clHeader4
        '
        resources.ApplyResources(Me.clHeader4, "clHeader4")
        '
        'clHeader5
        '
        resources.ApplyResources(Me.clHeader5, "clHeader5")
        '
        'wb_Admin_Sync
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lvSyncResult)
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
    Friend WithEvents lvSyncResult As Windows.Forms.ListView
    Friend WithEvents clHeader0 As Windows.Forms.ColumnHeader
    Friend WithEvents clHeader1 As Windows.Forms.ColumnHeader
    Friend WithEvents clHeader2 As Windows.Forms.ColumnHeader
    Friend WithEvents clHeader3 As Windows.Forms.ColumnHeader
    Friend WithEvents clHeader4 As Windows.Forms.ColumnHeader
    Friend WithEvents clHeader5 As Windows.Forms.ColumnHeader
End Class
