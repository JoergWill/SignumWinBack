Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Admin_Log
    Inherits DockContent

    Dim WithEvents Logger As New wb_TraceListener

    Private Sub showInTextBox(ByVal txt As String) Handles Logger.WriteText
        tbLogger.Text = tbLogger.Text + txt
    End Sub

    Private Sub wb_Admin_Log_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Trace.Listeners.Add(Logger)
    End Sub

    Private Sub wb_Admin_Log_FormClosed(sender As Object, e As Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Trace.Listeners.Remove(Logger)
    End Sub
End Class