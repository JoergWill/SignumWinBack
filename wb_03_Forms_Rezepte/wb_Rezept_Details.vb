Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rezept_Details
    Inherits DockContent

    Private Sub wb_Rezept_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Combo-Box(Rezept-Varianten) mit Werten füllen
        cbVariante.Fill(wb_Rezept_Shared.RzVariante)
        'Combo-Box(Rezept-Varianten) mit Werten füllen
        cbLiniengruppe.Fill(wb_Linien_Global.LinienGruppen)

        'Event-Handler (Klick auf Rezept-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Rezept_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    Public Sub DetailInfo()
        'Rezept-Nummer
        tRezeptNummer.Text = wb_Rezept_Shared.Rezept.RezeptNummer
        'Rezept-Bezeichnung
        tRezeptName.Text = wb_Rezept_Shared.Rezept.RezeptBezeichnung
        'Rezept-Kommentar
        tRezeptKommentar.Text = wb_Rezept_Shared.Rezept.RezeptKommentar
        'Rezept-Gewicht
        tRezeptGewicht.Text = Format(wb_Rezept_Shared.Rezept.RezeptGewicht, "####.000")

        'letzte Änderung Nummer
        tChangeNr.Text = wb_Rezept_Shared.Rezept.AenderungNummer.ToString
        'letzte Änderung Datum
        tChangeDatum.Text = wb_Rezept_Shared.Rezept.AenderungNummer
        'letzte Änderung Name
        tChangeName.Text = wb_Rezept_Shared.Rezept.AenderungName

        'Minimal-Charge
        tChargeMin.Text = Format(wb_Rezept_Shared.Rezept.MinChargekg, "####.000")
        'Maximal-Charge
        tChargeMax.Text = Format(wb_Rezept_Shared.Rezept.MaxChargekg, "####.000")
        'Optimal-Charge
        tChargeOpt.Text = Format(wb_Rezept_Shared.Rezept.OptChargekg, "####.000")

        'Eintrag in Combo-Box Liniengruppe ausfüllen
        cbLiniengruppe.SetTextFromKey(wb_Rezept_Shared.Rezept.LinienGruppe)
        'Eintrag in Combo-Box Rezeptvariante ausfüllen
        cbVariante.SetTextFromKey(wb_Rezept_Shared.Rezept.LinienGruppe)

    End Sub
    Private Sub DataHasChanged(sender As Object, e As EventArgs) Handles tRezeptNummer.Leave, tRezeptName.Leave, tRezeptKommentar.Leave
        wb_Rezept_Shared.Rezept.RezeptBezeichnung = tRezeptName.Text
        wb_Rezept_Shared.Rezept.RezeptKommentar = tRezeptKommentar.Text
        wb_Rezept_Shared.Rezept.RezeptNummer = tRezeptNummer.Text
        wb_Rezept_Shared.Edit_Leave(sender)
    End Sub

End Class