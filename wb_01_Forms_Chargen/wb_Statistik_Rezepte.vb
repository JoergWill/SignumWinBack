Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Chargen_Shared

Public Class wb_Statistik_Rezepte
    Inherits DockContent

    ''' <summary>
    ''' Listen-Auswahl initialisieren
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Statistik_Rezepte_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        'Drop-Down-Listen initialisieren
        ListeStatistik.InitGruppenListen(wb_Global.StatistikType.StatistikRezepte)
        'Listen-Fenster initialisieren (Variante Rezepte)
        ListeStatistik.InitAuswahlListen(wb_Global.StatistikType.StatistikRezepte)
    End Sub

    ''' <summary>
    ''' Auswahlfenster Rezepte öffnen und Rezept aus Liste auswählen.
    ''' Die ausgewählten Rezepte werden zur Liste hinzugefügt.
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub ListeAdd(Sender As Object, e As EventArgs) Handles ListeStatistik.ListeAdd_Click
        Dim RezeptAuswahl As New wb_Rezept_AuswahlListe
        RezeptAuswahl.BtnClear.Enabled = False
        RezeptAuswahl.MultiSelect = True

        If RezeptAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
            For Each rle In RezeptAuswahl.RezeptListe
                Dim RezListenElement As New wb_StatistikListenElement
                RezListenElement = rle
                ListeStatistik.AddElement(RezListenElement)
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
        wb_Chargen_Shared.Liste_Print(sender, wb_Global.StatistikType.StatistikRezepte)
    End Sub

    ''' <summary>
    ''' Statistik berechnen. Alle Filter-Einstellungen in wb_Chargen_Shared eintragen
    ''' Start der Berechnung über Event(Liste_Click)
    ''' </summary>
    ''' <param name="sender"></param>
    Private Sub BerechnungStatistik(sender As Object)
        'Liste aller Rezept-Nummern
        ListeStatistik.GetElements(wb_Chargen_Shared.NrListe)
        'Liste aller Linien
        ListeStatistik.GetLinien(wb_Chargen_Shared.NrLinien)

        'Fenstertitel
        wb_Chargen_Shared.SetFensterTitel(wb_Global.StatistikType.StatistikRezepte)
        'Auswertung starten
        wb_Chargen_Shared.Liste_Click(sender, wb_Global.StatistikType.StatistikRezepte)
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