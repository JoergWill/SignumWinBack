Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rezept_Hinweise
    Inherits DockContent

    Private Sub wb_Rezept_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Event-Handler (Klick auf Rezept-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Rezept.eListe_Click, AddressOf DetailInfo
    End Sub

    Public Sub DetailInfo()
        tHinweise.Text = wb_sql_Functions.ReadHinweise(wb_sql_Functions.Hinweise.RezeptHinweise, wb_Rezept.aktRzNr)
    End Sub

End Class