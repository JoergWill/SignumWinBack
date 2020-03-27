Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Statistik_Rezepte
    Inherits DockContent

    ''' <summary>
    ''' Listen-Auswahl initialisieren
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Statistik_Rezepte_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Listen-Fenster initialisieren (Variante Rezepte)
        ListeStatistik.InitAuswahlListen(wb_Global.StatistikType.StatistikRezepte)
    End Sub

    ''' <summary>
    ''' Auswahlfenster Rezepte öffnen und Rezept aus Liste auswählen.
    ''' Das ausgewählte Rezept wird zur Liste hinzugefügt.
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub ListeAdd(Sender As Object, e As EventArgs) Handles ListeStatistik.ListeAdd_Click
        Dim RezeptAuswahl As New wb_Rezept_AuswahlListe
        RezeptAuswahl.BtnClear.Enabled = False

        If RezeptAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim RezListenElement As New wb_StatistikListenElement
            RezListenElement.Nr = RezeptAuswahl.RezeptNr
            RezListenElement.Nummer = RezeptAuswahl.RezeptNummer
            RezListenElement.Bezeichnung = RezeptAuswahl.RezeptName
            ListeStatistik.AddElement(RezListenElement)
        End If
    End Sub

    ''' <summary>
    ''' Auswertung starten.
    ''' Die Berechnung und Anzeige erfolgt im Fenster Chargen_Details. Abhängig vom Statistik-Typ wird die entsprechende Abfrage in wbdaten ausgeführt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnBerechnen_Click(sender As Object, e As EventArgs) Handles BtnBerechnen.Click
        'Filter Datum übernehmen
        wb_Chargen_Shared.FilterVon = ListeStatistik.dtFilterVon.Value
        wb_Chargen_Shared.FilterBis = ListeStatistik.dtFilterBis.Value

        'Filter Uhrzeit übernehmen
        If ListeStatistik.cbUhrzeitVon.Checked Then
            wb_Chargen_Shared.UhrzeitVon = ListeStatistik.dtUhrzeitVon.Value
        Else
            wb_Chargen_Shared.UhrzeitVon = wb_Global.wbNODATE
        End If
        If ListeStatistik.cbUhrzeitBis.Checked Then
            wb_Chargen_Shared.UhrzeitBis = ListeStatistik.dtUhrzeitBis.Value
        Else
            wb_Chargen_Shared.UhrzeitBis = wb_Global.wbNODATE
        End If

        'Liste aller Rezept-Nummern
        ListeStatistik.GetElements(wb_Chargen_Shared.NrListe)
        'Liste aller Linien
        ListeStatistik.GetLinien(wb_Chargen_Shared.NrLinien)

        'Wassertemperatur ausblenden
        wb_Chargen_Shared.WasserTempAusblenden = ListeStatistik.cbWasserTempAusblenden.Checked
        'Istwert Null unterdrücken
        wb_Chargen_Shared.IstwertNullAusblenden = ListeStatistik.cbIstwertNullAusblenden.Checked

        'Auswertung starten
        wb_Chargen_Shared.Liste_Click(sender, wb_Global.StatistikType.StatistikRezepte)
    End Sub

    Private Sub BtnDrucken_Click(sender As Object, e As EventArgs) Handles BtnDrucken.Click
        'Ausdruck starten
        wb_Chargen_Shared.Liste_Print(sender, wb_Global.StatistikType.StatistikRezepte)
    End Sub

    ''' <summary>
    ''' Filter-Einstellungen in winback.ini speichern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Statistik_Rezepte_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        ListeStatistik.SaveAuswahlListen(wb_Global.StatistikType.StatistikRezepte)
    End Sub

End Class