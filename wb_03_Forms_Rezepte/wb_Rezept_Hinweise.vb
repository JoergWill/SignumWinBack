Imports WeifenLuo.WinFormsUI.Docking
Imports Signum.OrgaSoft.AddIn.wb_sql_Functions

Public Class wb_Rezept_Hinweise
    Inherits DockContent

    Private Sub wb_Rezept_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Event-Handler (Klick auf Rezept-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Rezept.eListe_Click, AddressOf DetailInfo
    End Sub

    Public Sub DetailInfo()
        tHinweise.Text = ReadHinweise(Hinweise.RezeptHinweise, wb_Rezept.aktRzNr)
    End Sub

End Class