Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Details
    Inherits DockContent

    Private _DeklBezeichungExtern As String = Nothing

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
        'Textfelder
        tRohstoffNummer.Text = RohStoff.Nummer
        tRohstoffName.Text = RohStoff.Bezeichnung
        tRohstoffKommentar.Text = RohStoff.Kommentar
        tRohstoffPreis.Text = RohStoff.Preis
        tbGebindeGroesse.Text = RohStoff.GebindeGroesse

        'Anzeige Zutatenliste und Bezeichnung
        ShowDeklaration()
        'Flag zählt nicht zum Rezeptgewicht
        cbRezeptGewicht.Checked = RohStoff.ZaehltNichtZumRezeptGewicht

        'Auswahlfelder Rohstoff-Gruppen
        cbRohstoffGrp1.SetTextFromKey(RohStoff.Gruppe1)
        cbRohstoffGrp2.SetTextFromKey(RohStoff.Gruppe2)
    End Sub

    ''' <summary>
    ''' Anzeige der Deklarations-Felder. Wenn der Haken bei "Rohstoff wird nicht deklariert" gesetzt ist,
    ''' werden die Felder ausgeblendet
    ''' </summary>
    Private Sub ShowDeklaration()
        If RohStoff.DeklBezeichungExtern = wb_Global.FlagKeineDeklaration Then
            lblDeklIntern.Visible = False
            tbDeklarationIntern.Visible = False
            lblDeklExtern.Visible = False
            tbDeklarationExtern.Visible = False
            cbKeineDeklaration.Checked = True
        Else
            lblDeklIntern.Visible = True
            tbDeklarationIntern.Visible = True
            lblDeklExtern.Visible = True
            tbDeklarationExtern.Visible = True
            cbKeineDeklaration.Checked = False
        End If
        tbDeklarationIntern.Text = RohStoff.DeklBezeichungIntern
        tbDeklarationExtern.Text = RohStoff.DeklBezeichungExtern
    End Sub

    ''' <summary>
    ''' Die Daten in den Eingabe-Feldern (Nummer/Text/Kommentar) haben sich geändert)
    ''' Wird aufgerufen durch [Textfeld].Leave(). Aktualisiert die Datenfelder in wb_Rohstoff_Global und löst
    ''' dann den Event Edit_Leave() aus.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataHasChanged(sender As Object, e As EventArgs) Handles tRohstoffName.Leave, tRohstoffNummer.Leave, tRohstoffKommentar.Leave, tRohstoffPreis.Leave, cbRohstoffGrp2.Leave, cbRohstoffGrp1.Leave, tbGebindeGroesse.Leave, cbRezeptGewicht.Click
        'Bezeichnungstexte
        RohStoff.Bezeichnung = tRohstoffName.Text
        RohStoff.Kommentar = tRohstoffKommentar.Text
        RohStoff.Nummer = tRohstoffNummer.Text
        RohStoff.GebindeGroesse = tbGebindeGroesse.Text

        'Rohstoff-Gruppe
        RohStoff.Gruppe1 = cbRohstoffGrp1.GetKeyFromSelection
        RohStoff.Gruppe2 = cbRohstoffGrp2.GetKeyFromSelection
        'Rohstoff zählt nicht zum Rezeptgewicht
        RohStoff.ZaehltNichtZumRezeptGewicht = cbRezeptGewicht.Checked

        'Daten wurden geändert - Datensatz speichern
        Edit_Leave(sender)
    End Sub

    ''' <summary>
    ''' Das Eingabefeld "externe Deklaration" wurde verlassen. Der Inhalt wird in die
    ''' Komponenten-Daten eingetragen und in der Datenbank gesichert. (SET)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tbDeklarationExtern_Leave(sender As Object, e As EventArgs) Handles tbDeklarationExtern.Leave
        wb_Rohstoffe_Shared.RohStoff.DeklBezeichungExtern = tbDeklarationExtern.Text
    End Sub

    ''' <summary>
    ''' Das Eingabefeld "interne Deklaration" wurde verlassen. Der Inhalt wird in die
    ''' Komponenten-Daten eingetragen und in der Datenbank gesichert. (SET)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tbDeklarationIntern_Leave(sender As Object, e As EventArgs) Handles tbDeklarationIntern.Leave
        wb_Rohstoffe_Shared.RohStoff.DeklBezeichungIntern = tbDeklarationIntern.Text
    End Sub

    Private Sub cbKeineDeklaration_Click(sender As Object, e As EventArgs) Handles cbKeineDeklaration.Click
        If cbKeineDeklaration.Checked Then
            'alten DeklarationsText merken(sicherheitshalber)
            If tbDeklarationExtern.Text <> wb_Global.FlagKeineDeklaration Then
                _DeklBezeichungExtern = tbDeklarationExtern.Text
            End If
            wb_Rohstoffe_Shared.RohStoff.DeklBezeichungExtern = wb_Global.FlagKeineDeklaration
            'Felder ausblenden
            ShowDeklaration()
        Else
            If _DeklBezeichungExtern IsNot Nothing Then
                'alten Wert wieder eintragen
                wb_Rohstoffe_Shared.RohStoff.DeklBezeichungExtern = _DeklBezeichungExtern
            Else
                wb_Rohstoffe_Shared.RohStoff.DeklBezeichungExtern = ""
            End If
            'Felder wieder einblenden
            ShowDeklaration()
        End If
    End Sub
End Class