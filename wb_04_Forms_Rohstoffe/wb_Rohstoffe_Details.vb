Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Details
    Inherits DockContent

    Private Sub wb_Rohstoffe_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Combo-Box(Rohstoff-Gruppe) mit Werten füllen
        cbRohstoffGrp1.Fill(RohGruppe)
        cbRohstoffGrp2.Fill(RohGruppe)

        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo
    End Sub

    ''' <summary>
    ''' Anzeige der Rohstoff-Details.
    ''' Wird aufgerufen durch Event eListe_Click(). Aktualisiert die Anzeigefelder (Nummer/Text/Kommentar...)
    ''' </summary>
    Private Sub DetailInfo()
        tRohstoffNummer.Text = RohStoff.Nummer
        tRohstoffName.Text = RohStoff.Bezeichnung
        tRohstoffKommentar.Text = RohStoff.Kommentar
        tRohstoffPreis.Text = RohStoff.Preis

        cbRohstoffGrp1.SetTextFromKey(RohStoff.Gruppe1)
        cbRohstoffGrp2.SetTextFromKey(RohStoff.Gruppe2)
    End Sub

    ''' <summary>
    ''' Die Daten in den Eingabe-Feldern (Nummer/Text/Kommentar) haben sich geändert)
    ''' Wird aufgerufen durch [Textfeld].Leave(). Aktualisiert die Datenfelder in wb_Rohstoff_Global und löst
    ''' dann den Event Edit_Leave() aus.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataHasChanged(sender As Object, e As EventArgs) Handles tRohstoffName.Leave, tRohstoffNummer.Leave, tRohstoffKommentar.Leave, tRohstoffPreis.Leave
        RohStoff.Bezeichnung = tRohstoffName.Text
        RohStoff.Kommentar = tRohstoffKommentar.Text
        RohStoff.Nummer = tRohstoffNummer.Text
        Edit_Leave(sender)
    End Sub

End Class