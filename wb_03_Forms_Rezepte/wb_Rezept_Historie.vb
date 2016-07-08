Imports WinBack.wb_Sql_Selects
Imports WeifenLuo.WinFormsUI.Docking

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
        HisDataGridView.LoadData(setParams(sqlRezeptHistr, wb_Rezept_Shared.aktRzNr.ToString, wb_Rezept_Shared.aktRzVariante.ToString),
                                 "RezeptHistorie", wb_Sql.dbTable.wbdaten)
    End Sub

End Class

