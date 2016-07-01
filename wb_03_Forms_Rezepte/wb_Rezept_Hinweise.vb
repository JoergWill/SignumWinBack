Imports WeifenLuo.WinFormsUI.Docking
Imports Signum.OrgaSoft.AddIn.wb_sql_Functions
Imports Signum.OrgaSoft.AddIn.wb_Global

Public Class wb_Rezept_Hinweise
    Inherits DockContent

    Private Sub wb_Rezept_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Event-Handler (Klick auf Rezept-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Rezept_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    Public Sub DetailInfo()
        tHinweise.Text = ReadHinweise(Hinweise.RezeptHinweise, wb_Rezept_Shared.aktRzNr)
    End Sub

End Class