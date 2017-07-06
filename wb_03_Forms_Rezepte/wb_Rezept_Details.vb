Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rezept_Details
    Inherits DockContent

    Private Sub wb_Rezept_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Combo-Box(Rezept-Varianten) mit Werten füllen
        cbVariante.Fill(wb_Rezept_Shared.RzVariante)
        'Combo-Box(Rezept-Varianten) mit Werten füllen
        cbLiniengruppe.Fill(wb_Rezept_Shared.LinienGruppe)

        'Event-Handler (Klick auf Rezept-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Rezept_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    Public Sub DetailInfo()
        'Rezept-Nummer
        tRezeptNr.Text = wb_Rezept_Shared.aktRzNummer
        'Rezept-Bezeichnung
        tRezeptName.Text = wb_Rezept_Shared.aktRzName
        'Rezept-Kommentar
        tRezeptKommentar.Text = wb_Rezept_Shared.aktRzKommentar
        'Rezept-Gewicht
        tRezeptGewicht.Text = Format(wb_Rezept_Shared.aktRzGewicht, "####.000")

        'letzte Änderung Nummer
        tChangeNr.Text = wb_Rezept_Shared.aktChangeNr.ToString
        'letzte Änderung Datum
        tChangeDatum.Text = wb_Rezept_Shared.aktChangeDatum
        'letzte Änderung Name
        tChangeName.Text = wb_Rezept_Shared.aktChangeName

        'Minimal-Charge
        tChargeMin.Text = Format(wb_Rezept_Shared.aktChargeMin, "####.000")
        'Maximal-Charge
        tChargeMax.Text = Format(wb_Rezept_Shared.aktChargeMax, "####.000")
        'Optimal-Charge
        tChargeOpt.Text = Format(wb_Rezept_Shared.aktChargeOpt, "####.000")

        'Eintrag in Combo-Box Liniengruppe ausfüllen
        cbLiniengruppe.SetTextFromKey(wb_Rezept_Shared.aktRzLinienGrp)
        'Eintrag in Combo-Box Rezeptvariante ausfüllen
        cbVariante.SetTextFromKey(wb_Rezept_Shared.aktRzVariante)

    End Sub

End Class