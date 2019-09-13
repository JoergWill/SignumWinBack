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
        DataGridView.FirstDisplayedScrollingRowIndex = DataGridView.RowCount - 1
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
        Liste_Click(Nothing)
        'Nach dem Update der Detailfenster wird der Focus wieder zurückgesetzt (Eingabe Suchmaske)
        DataGridView.Focus()
    End Sub

    Private Sub wb_Chargen_Liste_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Layout sichern
        DataGridView.SaveToDisk("StatistikChargenListe")
    End Sub

End Class
