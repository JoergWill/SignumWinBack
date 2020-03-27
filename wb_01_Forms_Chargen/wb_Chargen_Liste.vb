Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Chargen_Shared

Public Class wb_Chargen_Liste
    Inherits DockContent

    Private Sub wb_Chargen_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"Tageswechsel", "Anfang", "Ende", "&Linien"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'Default-Einstellungen Filter
        cbFilter.Checked = False
        'Default-Einstellungen Filtern bis (aktuelles Datum)
        dtFilterBis.Value = Now
        'Default-Einstellungen Filtern von (Anzeige aktuelles Jahr)
        dtFilterVon.Value = DateAdd(DateInterval.Year, -1, Now)
        'Sortieren nach Artikel-Nummer (Default)
        rbArtikel.PerformClick()

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = ""
        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlListeTW, "StatistikChargenListe", wb_Sql.dbTable.wbdaten)

        'Event-Handler (Änderungen in den Filter-Parametern -> Anzeige der Liste aktualisieren)
        AddHandler eFilter_Click, AddressOf SetFilter
    End Sub

    Private Sub SetFilter()
        If wb_Chargen_Shared.Filter Then
            DataGridView.Filter = "TW_Beginn >= #" + FilterVon.ToString("yyyy-MM-dd") + "# AND TW_Beginn <= #" & FilterBis.ToString("yyyy-MM-dd") + "#"
        End If
    End Sub

    ''' <summary>
    ''' Zur letzten Zeile im Grid scrollen.
    ''' Entspricht den letzten produzierten Chargen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Chargen_Liste_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If DataGridView.RowCount > 1 Then
            DataGridView.FirstDisplayedScrollingRowIndex = DataGridView.RowCount - 1
        End If
    End Sub

    ''' <summary>
    ''' Die Selektion im DataGridView hat sich geändert. Wenn Daten in den Detail-Fenstern geändert wurden, wird
    ''' diese Änderung vor dem Laden der neuen Daten in der Datenbank gesichert.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView_HasChanged(sender As Object, e As EventArgs) Handles DataGridView.HasChanged
        'akutell ausgewählte Tageswechsel-Nummer
        Liste_TagesWechselNummer = DataGridView.iField("TW_Nr")
        'Event auslösen - Aktualisierung der Anzeige in den Detail-Fenstern
        Liste_Click(Nothing, wb_Global.StatistikType.ChargenAuswertung)
        'Nach dem Update der Detailfenster wird der Focus wieder zurückgesetzt (Eingabe Suchmaske)
        DataGridView.Focus()
    End Sub

    Private Sub wb_Chargen_Liste_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Layout sichern
        DataGridView.SaveToDisk("StatistikChargenListe")
    End Sub

    Private Sub cbFilter_CheckedChanged(sender As Object, e As EventArgs)
        wb_Chargen_Shared.Filter = cbFilter.Checked
        Filter_Click(sender)
    End Sub

    Private Sub dtFilterVon_ValueChanged(sender As Object, e As EventArgs)
        wb_Chargen_Shared.FilterVon = dtFilterVon.Value
        If cbFilter.Checked Then
            Filter_Click(sender)
        End If
    End Sub

    Private Sub dtFilterBis_ValueChanged(sender As Object, e As EventArgs)
        wb_Chargen_Shared.FilterBis = dtFilterBis.Value
        If cbFilter.Checked Then
            Filter_Click(sender)
        End If
    End Sub

    ''' <summary>
    ''' Sortieren Chargen-Details nach Artikel-Bezeichnung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbArtikel_Click(sender As Object, e As EventArgs)
        wb_Chargen_Shared.SortKriterium = wb_Global.ChargenListeSortKriterium.ArtikelName
        'Chargen-Details neu aufbauen
        Liste_Click(sender, wb_Global.StatistikType.DontChange)
    End Sub

    ''' <summary>
    ''' Sortieren Chargen-Details nach Artikel-Nummer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbArtikelNummer_Click(sender As Object, e As EventArgs)
        wb_Chargen_Shared.SortKriterium = wb_Global.ChargenListeSortKriterium.ArtikelNummer
        'Chargen-Details neu aufbauen
        Liste_Click(sender, wb_Global.StatistikType.DontChange)
    End Sub

    ''' <summary>
    ''' Sortieren Chargen-Details nach Produktions-Datum
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbProduktion_Click(sender As Object, e As EventArgs)
        wb_Chargen_Shared.SortKriterium = wb_Global.ChargenListeSortKriterium.Produktion
        'Chargen-Details neu aufbauen
        Liste_Click(sender, wb_Global.StatistikType.DontChange)
    End Sub

End Class
