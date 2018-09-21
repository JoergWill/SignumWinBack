Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Parameter
    Inherits DockContent

    Private Sub wb_Rohstoffe_Parameter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo
        'Daten vom aktuellen Rohstoff anzeigen
        If RohStoff.Nr > 0 Then
            DetailInfo()
        End If
    End Sub

    Private Sub DetailInfo()
        'Virtual Tree anzeigen
        VirtualTree.DataSource = RohStoff.RootParameter
        'alle Zeilen aufklappen
        VirtualTree.RootRow.ExpandChildren(True)
    End Sub

End Class