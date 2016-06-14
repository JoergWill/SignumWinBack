Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rezept_Details
    Inherits DockContent

    Private Sub wb_Rezept_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Rezept-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Rezept.eListe_Click, AddressOf DetailInfo
    End Sub

    Public Sub DetailInfo()
        'Rezept-Nummer
        tRezeptNr.Text = wb_Rezept.aktRzNummer
        'Rezept-Bezeichnung
        tRezeptName.Text = wb_Rezept.aktRzName
        'Rezept-Kommentar
        tRezeptKommentar.Text = wb_Rezept.aktRzKommentar
        'Rezept-Gewicht
        tRezeptGewicht.Text = wb_Rezept.aktRzGewicht
    End Sub

End Class