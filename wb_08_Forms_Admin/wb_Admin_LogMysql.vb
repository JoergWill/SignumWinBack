Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Admin_LogMysql
    Inherits DockContent

    Private Sub wb_LogMysql_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"ID", "", "DateTime", "Zeile", "Modul", "Funktion", "&Meldung"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlLog4Net, "LogMysql")
    End Sub

    Private Sub wb_Chargen_Liste_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Layout sichern
        DataGridView.SaveToDisk("LogMysql")
    End Sub

End Class
