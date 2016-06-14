Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rezept_Details
    Inherits DockContent

    Private Sub wb_Rezept_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Combo-Box(Rezept-Varianten) mit Werten füllen
        cbVariante.Fill(wb_Rezept.RzVariante)
        'Combo-Box(Rezept-Varianten) mit Werten füllen
        cbLiniengruppe.Fill(wb_Rezept.LinienGruppe)

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
        tRezeptGewicht.Text = Format(wb_Rezept.aktRzGewicht, "####.000")

        'letzte Änderung Nummer
        tChangeNr.Text = wb_Rezept.aktChangeNr.ToString
        'letzte Änderung Datum
        tChangeDatum.Text = wb_Rezept.aktChangeDatum
        'letzte Änderung Name
        tChangeName.Text = wb_Rezept.aktChangeName

        'Minimal-Charge
        tChargeMin.Text = Format(wb_Rezept.aktChargeMin, "####.000")
        'Maximal-Charge
        tChargeMax.Text = Format(wb_Rezept.aktChargeMax, "####.000")
        'Optimal-Charge
        tChargeOpt.Text = Format(wb_Rezept.aktChargeOpt, "####.000")

        'Eintrag in Combo-Box Liniengruppe ausfüllen
        cbLiniengruppe.SetTextFromKey(wb_Rezept.aktRzLinienGrp)
        'Eintrag in Combo-Box Rezeptvariante ausfüllen
        cbVariante.SetTextFromKey(wb_Rezept.aktRzVariante)

    End Sub

End Class