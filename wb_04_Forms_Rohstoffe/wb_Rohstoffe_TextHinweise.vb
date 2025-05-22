Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Global

Public Class wb_Rohstoffe_TextHinweise
    Inherits DockContent
    Dim Rohstoffhinweise As wb_Hinweise

    Private Sub wb_Rohstoffe_TextHinweise_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Rohstoffe_Shared.eListe_Click, AddressOf DetailInfo

        'Beim Laden des Formulars schon mal die Hinweise laden
        If wb_Rohstoffe_Shared.RohStoff.Nr > 0 Then
            DetailInfo()
        End If
    End Sub

    Public Sub DetailInfo()
        If wb_Rohstoffe_Shared.RohStoff.RzNr > 0 Then
            Rohstoffhinweise = New wb_Hinweise(Hinweise.ArtikelHinweise, wb_Rohstoffe_Shared.RohStoff.Nr)
            tHinweise.Enabled = True
            tHinweise.Text = Rohstoffhinweise.Memo
        Else
            tHinweise.Text = ""
            tHinweise.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Hinweise-Feld wurde wieder verlassen. Änderungen speichern.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tHinweise_Leave(sender As Object, e As EventArgs) Handles tHinweise.Leave
        If Rohstoffhinweise IsNot Nothing AndAlso wb_Rohstoffe_Shared.RohStoff.RzNr > 0 Then
            Rohstoffhinweise.Memo = tHinweise.Text
            Rohstoffhinweise.Write()
        End If
    End Sub
End Class