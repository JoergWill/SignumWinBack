<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Artikel_Main
    Inherits WinBack_DockMain

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Artikel_Main))
        Me.SuspendLayout()
        '
        'Artikel_Main
        '
        resources.ApplyResources(Me, "$this")
        Me.Name = "Artikel_Main"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

End Class
