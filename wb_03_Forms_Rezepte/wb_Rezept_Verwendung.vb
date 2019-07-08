Imports WinBack.wb_Rezept_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rezept_Verwendung
    Inherits DockContent

    Private Sub wb_Rezept_Verwendung_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Rezept-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo

        'Beim ersten Aufruf wird die Rezeptnummer
        If Rezept.RezeptNr <> wb_Global.UNDEFINED Then
            DetailInfo(sender)
        End If
    End Sub

    Private Sub wb_Rohstoffe_Verwendung_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler wb_Rezept_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    Public Sub DetailInfo(sender)
        'DataGrid füllen
        HisDataGridView.LoadRezeptVerwendung(Rezept.RezeptNr)
    End Sub
End Class