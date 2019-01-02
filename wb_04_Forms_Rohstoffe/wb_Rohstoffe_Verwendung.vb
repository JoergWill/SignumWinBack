Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Verwendung
    Inherits DockContent

    Private Sub wb_Rohstoffe_Verwendung_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo

        'Beim ersten Aufruf wird der aktuelle Rohstoff angezeigt
        If RohStoff IsNot Nothing Then
            DetailInfo(sender)
        End If
    End Sub

    Private Sub wb_Rohstoffe_Verwendung_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler wb_Rohstoffe_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    Public Sub DetailInfo(sender)
        'DataGrid füllen
        HisDataGridView.LoadVerwendung(RohStoff.Nr)
    End Sub
End Class