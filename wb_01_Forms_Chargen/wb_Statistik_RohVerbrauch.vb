Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Chargen_Shared

Public Class wb_Statistik_RohVerbrauch
    Inherits DockContent

    Private Sub wb_Statistik_RohVerbrauch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Listen-Fenster initialisieren (Variante Rohstoffe-Verbrauch)
        ListeStatistik.InitAuswahlListen(wb_Global.StatistikType.StatistikRohstoffeVerbrauch)
    End Sub

    ''' <summary>
    ''' Auswahlfenster Rezepte öffnen und Rohstoff(e) aus Liste auswählen.
    ''' Die ausgewählten Rohstoffe werden zur Liste hinzugefügt.
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub ListeAdd(Sender As Object, e As EventArgs) Handles ListeStatistik.ListeAdd_Click
        Dim RohstoffAuswahl As New wb_Rohstoff_AuswahlListe
        RohstoffAuswahl.MultiSelect = True

        If RohstoffAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
            For Each rle In RohstoffAuswahl.RohstoffListe
                Dim RohListenElement As New wb_StatistikListenElement
                RohListenElement = rle
                ListeStatistik.AddElement(RohListenElement)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Wenn die Liste neu berechnet werden muss - Button Berechnen aktivieren
    ''' </summary>
    ''' <param name="sender"></param>
    Private Sub ListeBerechnetChanged(sender As Object) Handles ListeStatistik.ListeBerechnet_Changed
        BtnBerechnen.Enabled = Not ListeStatistik.ListeBerechnet
    End Sub

    ''' <summary>
    ''' Auswertung starten.
    ''' Die Berechnung und Anzeige erfolgt im Fenster Chargen_Details. Abhängig vom Statistik-Typ wird die entsprechende Abfrage in wbdaten ausgeführt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnBerechnen_Click(sender As Object, e As EventArgs) Handles BtnBerechnen.Click
        BerechnungStatistik(sender)
        BtnBerechnen.Enabled = False
    End Sub

    ''' <summary>
    ''' Auswertung Drucken
    ''' Wenn die Statistik noch nicht berechnet wurde, zuerst Berechnung starten
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnDrucken_Click(sender As Object, e As EventArgs) Handles BtnDrucken.Click
        'wenn die Statistik noch nicht berechnet wurde
        If Not ListeStatistik.ListeBerechnet Then
            'Berechnung starten
            BerechnungStatistik(sender)
        End If
        'Ausdruck starten
        wb_Chargen_Shared.Liste_Print(sender, wb_Global.StatistikType.StatistikRohstoffeVerbrauch)
    End Sub

    ''' <summary>
    ''' Statistik berechnen. Alle Filter-Einstellungen in wb_Chargen_Shared eintragen
    ''' Start der Berechnung über Event(Liste_Click)
    ''' </summary>
    ''' <param name="sender"></param>
    Private Sub BerechnungStatistik(sender As Object)
        'Filter Datum übernehmen
        wb_Chargen_Shared.FilterVon = ListeStatistik.dtFilterVon.Value
        wb_Chargen_Shared.FilterBis = ListeStatistik.dtFilterBis.Value

        'Fenster-Titel
        Dim Titel As String = "Statistik Rohstoff-Verbrauch vom "
        'Filter Uhrzeit übernehmen
        If ListeStatistik.cbUhrzeitVon.Checked Then
            wb_Chargen_Shared.UhrzeitVon = ListeStatistik.dtUhrzeitVon.Value
            Titel &= FilterVon.ToString("dd.MM.yyyy") & UhrzeitVon.ToString(" hh:mm")
        Else
            wb_Chargen_Shared.UhrzeitVon = wb_Global.wbNODATE
            Titel &= FilterVon.ToString("dd.MM.yyyy")
        End If

        'Fenster-Titel
        Titel &= " bis "
        If ListeStatistik.cbUhrzeitBis.Checked Then
            wb_Chargen_Shared.UhrzeitBis = ListeStatistik.dtUhrzeitBis.Value
            Titel &= FilterBis.ToString("dd.MM.yyyy") & UhrzeitBis.ToString(" hh:mm")
        Else
            wb_Chargen_Shared.UhrzeitBis = wb_Global.wbNODATE
            Titel &= FilterBis.ToString("dd.MM.yyyy")
        End If

        'Liste aller Rezept-Nummern
        ListeStatistik.GetElements(wb_Chargen_Shared.NrListe)
        'Liste aller Linien
        ListeStatistik.GetLinien(wb_Chargen_Shared.NrLinien)
        wb_Chargen_Shared.AlleLinien = ListeStatistik.cbAlleLinien.Checked

        'Wassertemperatur ausblenden
        wb_Chargen_Shared.WasserTempAusblenden = ListeStatistik.cbWasserTempAusblenden.Checked
        'Istwert Null unterdrücken
        wb_Chargen_Shared.IstwertNullAusblenden = ListeStatistik.cbIstwertNullAusblenden.Checked

        'Fenstertitel
        FensterTitel = Titel
        'Auswertung starten
        wb_Chargen_Shared.Liste_Click(sender, wb_Global.StatistikType.StatistikRohstoffeVerbrauch)
    End Sub

    ''' <summary>
    ''' Filter-Einstellungen in winback.ini speichern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Statistik_RohVerbrauch_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        ListeStatistik.SaveAuswahlListen(wb_Global.StatistikType.StatistikRohstoffeVerbrauch)
    End Sub

End Class