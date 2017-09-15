Imports WinBack.wb_Sql_Selects
Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Verwendung
    Inherits DockContent

    Private Sub wb_Rohstoffe_Verwendung_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo

    End Sub

    Public Sub DetailInfo()
        'DataGrid füllen
        HisDataGridView.LoadVerwendung(RohStoff.Nr)
    End Sub
End Class