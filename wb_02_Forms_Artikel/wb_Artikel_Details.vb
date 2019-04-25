Imports WinBack.wb_Artikel_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Artikel_Details
    Inherits DockContent

    Private _DeklBezeichungExtern As String = Nothing

    Private Sub wb_Artikel_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Combo-Box(Artikel-Gruppe) mit Werten füllen
        'cbArtikelGrp1.Fill(ArtGruppe)
        'cbArtikelGrp2.Fill(ArtGruppe)

        ''Feld Artikel-Preis ist in Variante OrgaBack readonly
        'If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
        '    tbArtikelPreis.ReadOnly = False
        '    tbArtikelPreis.BackColor = tArtikelNummer.BackColor
        'End If

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

        ''Preis und Gebindegröße
        'tbArtikelPreis.Text = Artikel.Preis
        'tbGebindeGroesse.Text = Artikel.GebindeGroesse

        ''Auswahlfelder Artikel-Gruppen
        'cbArtikelGrp1.SetTextFromKey(Artikel.Gruppe1)
        'cbArtikelGrp2.SetTextFromKey(Artikel.Gruppe2)

        'Rezeptzuordnung - Chargengrößen
        KompRzChargen.GetDataFromKomp(Artikel)
    End Sub

    ''' <summary>
    ''' Die Daten in den Eingabe-Feldern (Nummer/Text/Kommentar) haben sich geändert)
    ''' Wird aufgerufen durch [Textfeld].Leave(). Aktualisiert die Datenfelder in wb_Artikel_Global und löst
    ''' dann den Event Edit_Leave() aus.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataHasChanged(sender As Object, e As EventArgs) Handles tArtikelName.Leave, tArtikelNummer.Leave, tArtikelKommentar.Leave
        'Bezeichnungstexte
        Artikel.Bezeichnung = tArtikelName.Text
        Artikel.Kommentar = tArtikelKommentar.Text
        Artikel.Nummer = tArtikelNummer.Text

        ''Artikel-Gruppe
        'Artikel.Gruppe1 = cbArtikelGrp1.GetKeyFromSelection
        'Artikel.Gruppe2 = cbArtikelGrp2.GetKeyFromSelection

        ''Artikel-Preis (nur Prog-Version WinBack)
        'If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack Then
        '    Artikel.Preis = tbArtikelPreis.Text
        'End If

        'Daten wurden geändert - Datensatz speichern
        Edit_Leave(sender)
    End Sub

End Class