Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_User_Shared

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_User_GruppenRechte
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
        Me.pnlUserGruppenRechte = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'pnlUserGruppenRechte
        '
        Me.pnlUserGruppenRechte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlUserGruppenRechte.Location = New System.Drawing.Point(0, 0)
        Me.pnlUserGruppenRechte.Name = "pnlUserGruppenRechte"
        Me.pnlUserGruppenRechte.Size = New System.Drawing.Size(722, 372)
        Me.pnlUserGruppenRechte.TabIndex = 0
        '
        'wb_User_GruppenRechte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(722, 372)
        Me.Controls.Add(Me.pnlUserGruppenRechte)
        Me.Name = "wb_User_GruppenRechte"
        Me.Text = "Rechte-Matrix der Benutzer-Gruppen"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlUserGruppenRechte As Windows.Forms.Panel
End Class
