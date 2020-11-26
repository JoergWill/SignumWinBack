Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Planung_ListeFehler
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
        Me.pnlErrorList = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'pnlErrorList
        '
        Me.pnlErrorList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlErrorList.Location = New System.Drawing.Point(0, 0)
        Me.pnlErrorList.Name = "pnlErrorList"
        Me.pnlErrorList.Size = New System.Drawing.Size(1090, 496)
        Me.pnlErrorList.TabIndex = 0
        '
        'wb_Planung_ListeFehler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1090, 496)
        Me.Controls.Add(Me.pnlErrorList)
        Me.Name = "wb_Planung_ListeFehler"
        Me.Text = "Fehlerliste Einlesen der Produktionsdaten"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlErrorList As Windows.Forms.Panel
End Class
