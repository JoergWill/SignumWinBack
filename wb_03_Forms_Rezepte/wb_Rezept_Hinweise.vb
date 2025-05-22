Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_sql_Functions
Imports WinBack.wb_Global

Public Class wb_Rezept_Hinweise
    Inherits DockContent
    Dim Rezepthinweise As wb_Hinweise

    Private Sub wb_Rezept_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Event-Handler (Klick auf Rezept-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Rezept_Shared.eListe_Click, AddressOf DetailInfo

        'Beim Laden des Formulars schon mal die Hinweise laden
        If wb_Rezept_Shared.Rezept.RezeptNr > 0 Then
            DetailInfo()
        End If
    End Sub

    Public Sub DetailInfo()
        Rezepthinweise = New wb_Hinweise(Hinweise.RezeptHinweise, wb_Rezept_Shared.Rezept.RezeptNr)
        tHinweise.Text = RezeptHinweise.Memo
    End Sub

    ''' <summary>
    ''' Hinweise-Feld wurde wieder verlassen. Änderungen speichern.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tHinweise_Leave(sender As Object, e As EventArgs) Handles tHinweise.Leave
        If Rezepthinweise IsNot Nothing Then
            Rezepthinweise.Memo = tHinweise.Text
            Rezepthinweise.Write()
        End If
    End Sub
End Class