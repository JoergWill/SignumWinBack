﻿Imports WinBack.wb_Artikel_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Artikel_Details
    Inherits DockContent

    Private _DeklBezeichungExtern As String = Nothing

    Private Sub wb_Artikel_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Combo-Box(Rohstoff-Gruppe) mit Werten füllen
        cbArtikelGrp1.Fill(ArtGruppe)
        cbArtikelGrp2.Fill(ArtGruppe)

        'Feld Artikel-Preis ist in Variante OrgaBack nicht sichtbar
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            'Preis wird in OrgaBack verwaltet
            tbArtikelPreis.Visible = False
            lblPreis.Visible = False
            ePreis.Visible = False
            'keine Änderung der Bezeichnung
            tArtikelName.ReadOnly = True
        Else
            'Default-Währung (€)
            ePreis.Text = wb_GlobalSettings.osDefaultWaehrung
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
        tbArtikelPreis.Text = Artikel.Preis

        'Auswahlfelder Artikel-Gruppen
        cbArtikelGrp1.SetTextFromKey(Artikel.Gruppe1)
        cbArtikelGrp2.SetTextFromKey(Artikel.Gruppe2)

        'Rezeptzuordnung - Chargengrößen
        KompRzChargen.GetDataFromKomp(Artikel)
        'Anzeigen der Werte
        KompRzChargen.DataValid = True
    End Sub

    ''' <summary>
    ''' Die Daten in den Eingabe-Feldern (Nummer/Text/Kommentar) haben sich geändert)
    ''' Wird aufgerufen durch [Textfeld].Leave(). Aktualisiert die Datenfelder in wb_Artikel_Global und löst
    ''' dann den Event Edit_Leave() aus.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataHasChanged(sender As Object, e As EventArgs) Handles tArtikelName.Leave, tArtikelNummer.Leave, tArtikelKommentar.Leave, cbArtikelGrp1.Leave, cbArtikelGrp2.Leave
        If Artikel.Type <> wb_Global.KomponTypen.KO_TYPE_UNDEFINED Then
            'Bezeichnungstexte
            Artikel.Bezeichnung = tArtikelName.Text
            Artikel.Kommentar = tArtikelKommentar.Text
            Artikel.Nummer = tArtikelNummer.Text

            ''Artikel-Gruppe
            Artikel.Gruppe1 = cbArtikelGrp1.GetKeyFromSelection
            Artikel.Gruppe2 = cbArtikelGrp2.GetKeyFromSelection

            'Artikel-Preis (nur Prog-Version WinBack)
            If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack Then
                Artikel.Preis = tbArtikelPreis.Text
            End If

            'Daten wurden geändert - Datensatz speichern
            Edit_Leave(sender)
        End If
    End Sub

    Private Sub DataInvalidated() Handles KompRzChargen.DataInvalidated
        'Daten wurden geändert - Datensatz speichern
        KompRzChargen.SaveData(Artikel)
        'Update nur Parameter (NICHT Artikelbezeichnung... diese werden nur in GridUpdate aktualisiert)
        Artikel.MySQLdbUpdate(False)
        Edit_Leave(Me)
    End Sub

    ''' <summary>
    ''' Nährwerte des aktuellen Artikels neu berechnen und in OrgaBack-DB schreiben.
    ''' Damit die Nährwerte richtig angezeigt werden, muss der Artikel in OrgaBack neu eingelesen werden !
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Sub UpdateNwt_Click(sender As Object, e As EventArgs) Handles KompRzChargen.UpdateNwt_Click
        'Artikel Nährwerte update
        Dim nwtUpdateArtikel As New wb_nwtUpdateArtikel
        'Start bei Artikelnummer x - 1
        nwtUpdateArtikel.UpdateNext(wb_Artikel_Shared.Artikel.Nr - 1, True)
        nwtUpdateArtikel = Nothing
        'Die Daten sind in WinBack erst nach Laden des Artikels sichtbar
        wb_Artikel_Shared.Liste_Click(sender)
    End Sub

End Class