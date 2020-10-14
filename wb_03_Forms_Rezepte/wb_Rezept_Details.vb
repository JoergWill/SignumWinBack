Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rezept_Details
    Inherits DockContent
    Private OnErrorSetFocus As Object

    Private Sub wb_Rezept_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Combo-Box(Rezept-Varianten) mit Werten füllen
        cbVariante.Fill(wb_Rezept_Shared.RzVariante)
        'Combo-Box(Liniengruppen) mit Werten füllen
        cbLiniengruppe.Fill(wb_Linien_Global.RezeptLinienGruppen)
        'Combo-Box(Rezeptgruppe) mit Werten füllen
        cbRezeptGruppe.Fill(wb_Rezept_Shared.RzGruppe)

        'Event-Handler (Klick auf Rezept-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Rezept_Shared.eListe_Click, AddressOf DetailInfo
        'Event-Handler (Fehler bei Eingabe Rezept(Teig)-Chargengrößen
        AddHandler wb_Rezept_Shared.Rezept.TeigChargen.OnError, AddressOf OnErrorMinMaxOptTeig
    End Sub

    Public Sub DetailInfo(sender As Object)
        'Rezept-Nummer
        tRezeptNummer.Text = wb_Rezept_Shared.Rezept.RezeptNummer
        'Rezept-Bezeichnung
        tRezeptName.Text = wb_Rezept_Shared.Rezept.RezeptBezeichnung
        'Rezept-Kommentar
        tRezeptKommentar.Text = wb_Rezept_Shared.Rezept.RezeptKommentar
        'Rezept-Gewicht
        tRezeptGewicht.Text = Format(wb_Rezept_Shared.Rezept.RezeptGewicht, "####.000")

        'letzte Änderung Nummer
        If wb_Rezept_Shared.Rezept.AenderungNummer > 0 Then
            tChangeNr.Text = wb_Rezept_Shared.Rezept.AenderungNummer.ToString
        Else
            tChangeNr.Text = "Neu"
        End If

        'Rezept-Chargengrößen
        MinMaxRezeptShowValues()

        'letzte Änderung Datum
        tChangeDatum.Text = wb_Rezept_Shared.Rezept.AenderungDatum
        'letzte Änderung Name
        tChangeName.Text = wb_Rezept_Shared.Rezept.AenderungName

        'Eintrag in Combo-Box Liniengruppe ausfüllen
        cbLiniengruppe.SetTextFromKey(wb_Rezept_Shared.Rezept.LinienGruppe)
        'Eintrag in Combo-Box Rezeptvariante ausfüllen
        cbVariante.SetTextFromKey(wb_Rezept_Shared.Rezept.Variante)
        'Eintrag in Combo-Box Rezeptgruppe ausfüllen
        cbRezeptGruppe.SetTextFromKey(wb_Rezept_Shared.Rezept.RezeptGruppe)
        'Checkbox Anstellgut
        cbAnstellgut.Checked = wb_Rezept_Shared.Rezept.AnstellGutReWork

    End Sub

    Private Sub MinMaxRezeptShowValues()
        'Minimal-Charge
        tChargeMin.Text = wb_Rezept_Shared.Rezept.TeigChargen.MinCharge.MengeInkg
        'Maximal-Charge
        tChargeMax.Text = wb_Rezept_Shared.Rezept.TeigChargen.MaxCharge.MengeInkg
        'Optimal-Charge
        tChargeOpt.Text = wb_Rezept_Shared.Rezept.TeigChargen.OptCharge.MengeInkg
    End Sub

    Private Sub DataHasChanged(sender As Object, e As EventArgs) Handles tRezeptNummer.Leave, tRezeptName.Leave, tRezeptKommentar.Leave, cbRezeptGruppe.Leave, cbLiniengruppe.Leave
        If wb_Rezept_Shared.Rezept.RezeptNr > 0 Then
            wb_Rezept_Shared.Rezept.RezeptBezeichnung = tRezeptName.Text
            wb_Rezept_Shared.Rezept.RezeptKommentar = tRezeptKommentar.Text
            wb_Rezept_Shared.Rezept.RezeptNummer = tRezeptNummer.Text
            wb_Rezept_Shared.Rezept.LinienGruppe = cbLiniengruppe.GetKeyFromSelection()
            wb_Rezept_Shared.Rezept.RezeptGruppe = cbRezeptGruppe.GetKeyFromSelection()
            'geändete Daten speichern
            wb_Rezept_Shared.Edit_Leave(sender)
        End If
    End Sub

    Private Sub tChargeMin_Leave(sender As Object, e As EventArgs) Handles tChargeMin.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        wb_Rezept_Shared.Rezept.TeigChargen.MinCharge.MengeInkg = tChargeMin.Text
        'geändete Daten speichern
        wb_Rezept_Shared.Rezept.DataHasChanged = True
        wb_Rezept_Shared.Edit_Leave(sender)
    End Sub

    Private Sub tChargeMax_Leave(sender As Object, e As EventArgs) Handles tChargeMax.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        wb_Rezept_Shared.Rezept.TeigChargen.MaxCharge.MengeInkg = tChargeMax.Text
        'geändete Daten speichern
        wb_Rezept_Shared.Rezept.DataHasChanged = True
        wb_Rezept_Shared.Edit_Leave(sender)
    End Sub

    Private Sub tChargeOpt_Leave(sender As Object, e As EventArgs) Handles tChargeOpt.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        wb_Rezept_Shared.Rezept.TeigChargen.OptCharge.MengeInkg = tChargeOpt.Text
        'geändete Daten speichern
        wb_Rezept_Shared.Rezept.DataHasChanged = True
        wb_Rezept_Shared.Edit_Leave(sender)
    End Sub

    Private Sub OnErrorMinMaxOptTeig(Sender As Object)
        If wb_Rezept_Shared.Rezept.TeigChargen.ErrorCode <> wb_Global.MinMaxOptChargenError.NoError Then
            If OnErrorSetFocus IsNot Nothing Then
                'Eingabe-Focus auf das auslösende Objekt setzen
                OnErrorSetFocus.Focus()
            End If
            'Fehlermeldung entsprechend der Eingabe-Felder ausgeben
            MsgBox(wb_Functions.MinMaxOptChargeToString(wb_Rezept_Shared.Rezept.TeigChargen.ErrorCode), MsgBoxStyle.Exclamation, "Fehler bei der Eingabe der Rezept-Chargengrößen")
        End If
        'Felder neu zeichnen
        MinMaxRezeptShowValues()
    End Sub

    Private Sub wb_Rezept_Details_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Event-Handler wieder freigeben
        RemoveHandler wb_Rezept_Shared.eListe_Click, AddressOf DetailInfo
        RemoveHandler wb_Rezept_Shared.Rezept.TeigChargen.OnError, AddressOf OnErrorMinMaxOptTeig
    End Sub

    Private Sub cbAnstellgut_Click(sender As Object, e As EventArgs) Handles cbAnstellgut.Click
        wb_Rezept_Shared.Rezept.AnstellGutReWork = cbAnstellgut.Checked
        wb_Rezept_Shared.Rezept.DataHasChanged = True
        wb_Rezept_Shared.Edit_Leave(sender)
    End Sub
End Class