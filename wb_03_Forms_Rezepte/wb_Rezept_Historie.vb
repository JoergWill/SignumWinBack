Imports WinBack.wb_Sql_Selects
Imports WeifenLuo.WinFormsUI.Docking
Imports System.Windows.Forms

Public Class wb_Rezept_Historie
    Inherits DockContent
    Private Sub wb_Rezept_Historie_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Rezept-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Rezept_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    Public Sub DetailInfo()
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"Nr", "Datum", "Name"}
        For Each sName In sColNames
            HisDataGridView.ColNames.Add(sName)
        Next

        'DataGrid füllen
        HisDataGridView.LoadData(setParams(sqlRezeptHistr, wb_Rezept_Shared.Rezept.RezeptNr, wb_Rezept_Shared.Rezept.Variante.ToString), "RezeptHistorie", wb_Sql.dbTable.wbdaten)
    End Sub

    Private Sub HisDataGridView_CellDoubleClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles HisDataGridView.CellDoubleClick
        Me.Cursor = Cursors.WaitCursor
        'Beim Erzeugen des Fensters werden die Daten aus der Datenbank gelesen
        Dim AenderungNummer As Integer = HisDataGridView.iField("H_RZ_Aenderung_Nr")
        Dim Rezeptur As New wb_Rezept_Rezeptur(wb_Rezept_Shared.Rezept.RezeptNr, wb_Rezept_Shared.Rezept.Variante, AenderungNummer)
        Rezeptur.Show()
        Me.Cursor = Cursors.Default
    End Sub
End Class

