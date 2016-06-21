Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rezept_Historie
    Inherits DockContent
    Private Sub wb_Rezept_Historie_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Rezept-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Rezept.eListe_Click, AddressOf DetailInfo
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
        Dim sql As String = "SELECT H_RZ_Aenderung_Nr, H_RZ_Aenderung_Datum, H_RZ_Aenderung_Name FROM His_Rezepte " &
                            "WHERE H_RZ_Nr=" & wb_Rezept.aktRzNr.ToString & " AND H_RZ_Variante_Nr=" & wb_Rezept.aktRzVariante.ToString
        HisDataGridView.LoadData(sql, "RezeptHistorie", wb_Sql.dbType.mySql, wb_Sql.dbTable.wbdaten)
    End Sub

End Class

