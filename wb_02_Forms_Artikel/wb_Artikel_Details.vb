Imports WinBack.wb_Artikel_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Artikel_Details
    Inherits DockContent

    Private _DeklBezeichungExtern As String = Nothing

    Private Sub wb_Artikel_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Combo-Box(Artikel-Gruppe) mit Werten füllen
        cbArtikelGrp1.Fill(ArtGruppe)
        cbArtikelGrp2.Fill(ArtGruppe)

        'Feld Artikel-Preis ist in Variante OrgaBack readonly
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            tbArtikelPreis.ReadOnly = False
            tbArtikelPreis.BackColor = tArtikelNummer.BackColor
        End If

        'Event-Handler (Klick auf Artikel-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo

        'Beim ersten Aufruf wird der aktuelle Artikel angezeigt. Sonst wird beim Öffnen des Detail-Info-Fensters
        'der Inhalt der Textfelder gelöscht !!
        If Artikel IsNot Nothing Then
            DetailInfo(sender)
        End If
    End Sub

    Private Sub wb_Artikel_Details_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler wb_Artikel_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    ''' <summary>
    ''' Anzeige der Artikel-Details.
    ''' Wird aufgerufen durch Event eListe_Click(). Aktualisiert die Anzeigefelder (Nummer/Text/Kommentar...)
    ''' </summary>
    Private Sub DetailInfo(sender)
        'Textfelder
        tArtikelNummer.Text = Artikel.Nummer
        tArtikelName.Text = Artikel.Bezeichnung
        tArtikelKommentar.Text = Artikel.Kommentar

        'Panel Detail-Daten sichtbar
        pnlDetails.Visible = True

        'Preis und Gebindegröße
        tbArtikelPreis.Text = Artikel.Preis
        tbGebindeGroesse.Text = Artikel.GebindeGroesse

        'Lagerbestand und Mindestmenge
        tbBilanzmenge.Text = Artikel.Bilanzmenge
        tbMindestMenge.Text = Artikel.MindestMenge
        'wenn die Mindestmenge unterschritten ist, rot markieren
        If Artikel.MindestmengeUnterschritten Then
            tbBilanzmenge.BackColor = Drawing.Color.Red
        Else
            tbBilanzmenge.BackColor = Drawing.Color.LightGray
        End If

        'Anzeige Zutatenliste und Bezeichnung
        ShowDeklaration()
        'Flag zählt nicht zum Rezeptgewicht
        cbRezeptGewicht.Checked = Artikel.ZaehltNichtZumRezeptGewicht

        'Auswahlfelder Artikel-Gruppen
        cbArtikelGrp1.SetTextFromKey(Artikel.Gruppe1)
        cbArtikelGrp2.SetTextFromKey(Artikel.Gruppe2)
    End Sub

    ''' <summary>
    ''' Anzeige der Deklarations-Felder. Wenn der Haken bei "Artikel wird nicht deklariert" gesetzt ist,
    ''' werden die Felder ausgeblendet
    ''' </summary>
    Private Sub ShowDeklaration()
        If Artikel.DeklBezeichungExtern = wb_Global.FlagKeineDeklaration Then
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
        tbDeklarationIntern.Text = Artikel.DeklBezeichungIntern
        tbDeklarationExtern.Text = Artikel.DeklBezeichungExtern
    End Sub

    ''' <summary>
    ''' Die Daten in den Eingabe-Feldern (Nummer/Text/Kommentar) haben sich geändert)
    ''' Wird aufgerufen durch [Textfeld].Leave(). Aktualisiert die Datenfelder in wb_Artikel_Global und löst
    ''' dann den Event Edit_Leave() aus.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataHasChanged(sender As Object, e As EventArgs) Handles tArtikelName.Leave, tArtikelNummer.Leave, tArtikelKommentar.Leave, tbArtikelPreis.Leave, tbGebindeGroesse.Leave, cbArtikelGrp2.Leave, cbArtikelGrp1.Leave, cbRezeptGewicht.Click
        'Bezeichnungstexte
        Artikel.Bezeichnung = tArtikelName.Text
        Artikel.Kommentar = tArtikelKommentar.Text
        Artikel.Nummer = tArtikelNummer.Text
        Artikel.GebindeGroesse = tbGebindeGroesse.Text

        'Artikel-Gruppe
        Artikel.Gruppe1 = cbArtikelGrp1.GetKeyFromSelection
        Artikel.Gruppe2 = cbArtikelGrp2.GetKeyFromSelection
        'Artikel zählt nicht zum Rezeptgewicht
        Artikel.ZaehltNichtZumRezeptGewicht = cbRezeptGewicht.Checked

        'Artikel-Preis (nur Prog-Version WinBack)
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack Then
            Artikel.Preis = tbArtikelPreis.Text
        End If

        'Daten wurden geändert - Datensatz speichern
        Edit_Leave(sender)
    End Sub

    ''' <summary>
    ''' Das Eingabefeld "Mindestmenge" wurde verlassen. Der Inhalt wird in die
    ''' Komponenten-Daten eingetragen und in der Datenbank gesichert. (SET)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tbMindestMenge_Leave(sender As Object, e As EventArgs) Handles tbMindestMenge.Leave
        wb_Artikel_Shared.Artikel.MindestMenge = tbMindestMenge.Text
    End Sub

    ''' <summary>
    ''' Das Eingabefeld "externe Deklaration" wurde verlassen. Der Inhalt wird in die
    ''' Komponenten-Daten eingetragen und in der Datenbank gesichert. (SET)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tbDeklarationExtern_Leave(sender As Object, e As EventArgs) Handles tbDeklarationExtern.Leave
        wb_Artikel_Shared.Artikel.DeklBezeichungExtern = tbDeklarationExtern.Text
    End Sub

    ''' <summary>
    ''' Das Eingabefeld "interne Deklaration" wurde verlassen. Der Inhalt wird in die
    ''' Komponenten-Daten eingetragen und in der Datenbank gesichert. (SET)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tbDeklarationIntern_Leave(sender As Object, e As EventArgs) Handles tbDeklarationIntern.Leave
        wb_Artikel_Shared.Artikel.DeklBezeichungIntern = tbDeklarationIntern.Text
    End Sub

    Private Sub cbKeineDeklaration_Click(sender As Object, e As EventArgs) Handles cbKeineDeklaration.Leave
        If cbKeineDeklaration.Checked Then
            'alten DeklarationsText merken(sicherheitshalber)
            If tbDeklarationExtern.Text <> wb_Global.FlagKeineDeklaration Then
                _DeklBezeichungExtern = tbDeklarationExtern.Text
            End If
            wb_Artikel_Shared.Artikel.DeklBezeichungExtern = wb_Global.FlagKeineDeklaration
            'Felder ausblenden
            ShowDeklaration()
        Else
            If _DeklBezeichungExtern IsNot Nothing Then
                'alten Wert wieder eintragen
                wb_Artikel_Shared.Artikel.DeklBezeichungExtern = _DeklBezeichungExtern
            Else
                wb_Artikel_Shared.Artikel.DeklBezeichungExtern = ""
            End If
            'Felder wieder einblenden
            ShowDeklaration()
        End If
    End Sub

End Class