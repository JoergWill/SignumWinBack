Imports Signum.OrgaSoft.AddIn.wb_Sql_Selects
Imports Signum.OrgaSoft.AddIn.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Verwendung
    Inherits DockContent

    Private Sub wb_Rohstoffe_Verwendung_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo

    End Sub
    Public Sub DetailInfo()
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"Nr", "V", "&Bezeichnung"}
        For Each sName In sColNames
            HisDataGridView.ColNames.Add(sName)
        Next

        'DataGrid füllen
        HisDataGridView.LoadData(setParams(sqlRohstoffVerwendung, RohStoff.Nr), "RohstoffVerwendung")
    End Sub
End Class