Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Chargen_Shared

Public Class wb_Statistik_Chargen
    Inherits DockContent
    Private _ListeBerechnet As Boolean = False

    Public Property ListeBerechnet As Boolean
        Get
            Return _ListeBerechnet
        End Get
        Set(value As Boolean)
            _ListeBerechnet = value
            BtnBerechnen.Enabled = Not _ListeBerechnet
        End Set
    End Property

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
        ListeBerechnet = False
        'Button Grafik wird nur für Sauerteig-Chargen angezeigt
        BtnChart.Enabled = (DataGridView.iField("TW_Seg_Nr") = 0)
    End Sub

    Private Sub wb_Chargen_Liste_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Layout sichern
        DataGridView.SaveToDisk("StatistikChargenListe")
    End Sub

    Private Sub cbFilter_CheckedChanged(sender As Object, e As EventArgs) Handles cbFilter.CheckedChanged
        If cbFilter.Checked Then
            SetFilter(sender)
        End If
    End Sub

    Private Sub dtFilterVon_ValueChanged(sender As Object, e As EventArgs) Handles dtFilterVon.ValueChanged
        If (dtFilterVon.Value > wb_Global.wbNODATE) AndAlso (dtFilterBis.Value > wb_Global.wbNODATE) Then
            cbFilter.Checked = True
            SetFilter(sender)
        End If
    End Sub

    Private Sub dtFilterBis_ValueChanged(sender As Object, e As EventArgs) Handles dtFilterBis.ValueChanged
        If (dtFilterVon.Value > wb_Global.wbNODATE) AndAlso (dtFilterBis.Value > wb_Global.wbNODATE) Then
            cbFilter.Checked = True
            SetFilter(sender)
        End If
    End Sub

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S1172:Unused procedure parameters should be removed", Justification:="<Ausstehend>")>
    Private Sub SetFilter(sender)
        If cbFilter.Checked AndAlso (dtFilterVon.Value > wb_Global.wbNODATE) And (dtFilterBis.Value > wb_Global.wbNODATE) Then
            DataGridView.Filter = "TW_Beginn >= #" + dtFilterVon.Value.ToString("yyyy-MM-dd") + "# AND TW_Beginn <= #" & dtFilterBis.Value.ToString("yyyy-MM-dd") + "#"
        End If
    End Sub

    ''' <summary>
    ''' Sortieren Chargen-Details nach Artikel-Bezeichnung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbArtikel_CheckedChanged(sender As Object, e As EventArgs) Handles rbArtikel.CheckedChanged
        If rbArtikel.Checked Then
            wb_Chargen_Shared.SortKriterium = wb_Global.ChargenListeSortKriterium.ArtikelName
            If ListeBerechnet Then
                'Chargen-Details neu aufbauen
                Liste_Click(sender, wb_Global.StatistikType.DontChange)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Sortieren Chargen-Details nach Artikel-Nummer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbArtikelNummer_CheckedChanged(sender As Object, e As EventArgs) Handles rbArtikelNummer.CheckedChanged
        If rbArtikelNummer.Checked Then
            wb_Chargen_Shared.SortKriterium = wb_Global.ChargenListeSortKriterium.ArtikelNummer
            If ListeBerechnet Then
                'Chargen-Details neu aufbauen
                Liste_Click(sender, wb_Global.StatistikType.DontChange)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Sortieren Chargen-Details nach Produktions-Datum
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbProduktion_CheckedChanged(sender As Object, e As EventArgs) Handles rbProduktion.CheckedChanged
        If rbProduktion.Checked Then
            wb_Chargen_Shared.SortKriterium = wb_Global.ChargenListeSortKriterium.Produktion
            If ListeBerechnet Then
                'Chargen-Details neu aufbauen
                Liste_Click(sender, wb_Global.StatistikType.DontChange)
            End If
        End If
    End Sub

    Private Sub DataGridView_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView.CellDoubleClick
        BerechnungStatistik(sender)
        ListeBerechnet = True
    End Sub

    Private Sub BtnBerechnen_Click(sender As Object, e As EventArgs) Handles BtnBerechnen.Click
        BerechnungStatistik(sender)
        ListeBerechnet = True
    End Sub

    Private Sub BtnChart_Click(sender As Object, e As EventArgs) Handles BtnChart.Click
        'wenn die Statistik noch nicht berechnet wurde
        If Not ListeBerechnet Then
            'Berechnung starten
            BerechnungStatistik(sender)
        End If
        'Grafik anzeigen
        wb_Chargen_Shared.Liste_Print(sender, wb_Global.StatistikType.ChargenAuswertung)
    End Sub

    Private Sub BtnDrucken_Click(sender As Object, e As EventArgs) Handles BtnDrucken.Click
        'wenn die Statistik noch nicht berechnet wurde
        If Not ListeBerechnet Then
            'Berechnung starten
            BerechnungStatistik(sender)
        End If
        'Ausdruck starten
        wb_Chargen_Shared.Liste_Print(sender, wb_Global.StatistikType.ChargenAuswertung)
    End Sub

    Private Sub BerechnungStatistik(sender As Object)
        'akutell ausgewählte Tageswechsel-Nummer
        Liste_TagesWechselNummer = DataGridView.iField("TW_Nr")
        'Tageswechsel Segment-Nummer
        Dim TagesWechselSegment As Integer = DataGridView.iField("TW_Seg_Nr")

        'Fenster-Titel Detail-Fenster
        Dim StrtDate As String = DataGridView.Field("TW_Beginn")
        Dim EndeDate As String = DataGridView.Field("TW_Ende")

        'Chargen-Auswertung Sauerteig/Produktion
        If TagesWechselSegment = 0 Then
            'Fenstertitel
            wb_Chargen_Shared.SetFensterTitel(wb_Global.StatistikType.ChargenAuswertungSauerteig, StrtDate, EndeDate)
            'Event auslösen - Aktualisierung der Anzeige in den Detail-Fenstern
            Liste_Click(sender, wb_Global.StatistikType.ChargenAuswertungSauerteig)
        Else
            'Fenstertitel
            wb_Chargen_Shared.SetFensterTitel(wb_Global.StatistikType.ChargenAuswertung, StrtDate, EndeDate)
            'Event auslösen - Aktualisierung der Anzeige in den Detail-Fenstern
            Liste_Click(sender, wb_Global.StatistikType.ChargenAuswertung)
        End If

        'Nach dem Update der Detailfenster wird der Focus wieder zurückgesetzt (Eingabe Suchmaske)
        DataGridView.Focus()
    End Sub

End Class
