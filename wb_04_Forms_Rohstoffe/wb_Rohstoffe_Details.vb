Imports Signum.OrgaSoft.AddIn.wb_Rohstoffe_Shared
Imports Signum.OrgaSoft.AddIn.wb_Functions
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

    Public Sub DetailInfo()
        tRohstoffNummer.Text = RohStoff.Nummer
        tRohstoffName.Text = RohStoff.Bezeichung
        tRohstoffKommentar.Text = RohStoff.Kommentar
        tRohstoffPreis.Text = RohStoff.Preis

        cbRohstoffGrp1.SetTextFromKey(RohStoff.Gruppe1)
        cbRohstoffGrp2.SetTextFromKey(RohStoff.Gruppe2)
    End Sub
End Class