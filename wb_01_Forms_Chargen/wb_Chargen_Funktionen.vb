Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Chargen_Shared

Public Class wb_Chargen_Funktionen
    Inherits DockContent

    Private Sub wb_Chargen_Funktionen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Default-Einstellungen Filter
        cbFilter.Checked = False
        'Default-Einstellungen Filtern bis (aktuelles Datum)
        dtFilterBis.Value = Now
        'Default-Einstellungen Filtern von (Anzeige aktuelles Jahr)
        dtFilterVon.Value = DateAdd(DateInterval.Year, -1, Now)
        'Sortieren nach Artikel-Nummer (Default)
        rbArtikel.PerformClick()
    End Sub

    Private Sub cbFilter_CheckedChanged(sender As Object, e As EventArgs) Handles cbFilter.CheckedChanged
        wb_Chargen_Shared.Filter = cbFilter.Checked
        Filter_Click(sender)
    End Sub

    Private Sub dtFilterVon_ValueChanged(sender As Object, e As EventArgs) Handles dtFilterVon.ValueChanged
        wb_Chargen_Shared.FilterVon = dtFilterVon.Value
        If cbFilter.Checked Then
            Filter_Click(sender)
        End If
    End Sub

    Private Sub dtFilterBis_ValueChanged(sender As Object, e As EventArgs) Handles dtFilterBis.ValueChanged
        wb_Chargen_Shared.FilterBis = dtFilterBis.Value
        If cbFilter.Checked Then
            Filter_Click(sender)
        End If
    End Sub

    ''' <summary>
    ''' Anzeige der produzierte Chargen zu diesem Produktionstag (TW-Nummer)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnDetails_Click(sender As Object, e As EventArgs) Handles BtnDetails.Click
        Liste_Click(sender)
    End Sub

    ''' <summary>
    ''' Sortieren Chargen-Details nach Artikel-Bezeichnung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbArtikel_Click(sender As Object, e As EventArgs) Handles rbArtikel.Click
        wb_Chargen_Shared.SortKriterium = wb_Global.ChargenListeSortKriterium.ArtikelName
        'Chargen-Details neu aufbauen
        Liste_Click(sender)
    End Sub

    ''' <summary>
    ''' Sortieren Chargen-Details nach Artikel-Nummer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbArtikelNummer_Click(sender As Object, e As EventArgs) Handles rbArtikelNummer.Click
        wb_Chargen_Shared.SortKriterium = wb_Global.ChargenListeSortKriterium.ArtikelNummer
        'Chargen-Details neu aufbauen
        Liste_Click(sender)
    End Sub

    ''' <summary>
    ''' Sortieren Chargen-Details nach Produktions-Datum
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbProduktion_Click(sender As Object, e As EventArgs) Handles rbProduktion.Click
        wb_Chargen_Shared.SortKriterium = wb_Global.ChargenListeSortKriterium.Produktion
        'Chargen-Details neu aufbauen
        Liste_Click(sender)
    End Sub

End Class