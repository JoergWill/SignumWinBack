Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_sql_Functions
Imports WinBack.wb_Global

Public Class wb_Rezept_Hinweise
    Inherits DockContent

    Private Sub wb_Rezept_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Event-Handler (Klick auf Rezept-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Rezept_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    Public Sub DetailInfo()
        Dim RezeptHinweise As New wb_Hinweise(Hinweise.RezeptHinweise, wb_Rezept_Shared.aktRzNr)
        tHinweise.Text = RezeptHinweise.Memo
    End Sub

End Class