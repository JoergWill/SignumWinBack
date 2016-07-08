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
        'Spalten ohne Bezeichnung werden ausgeblendet.
        'Die Rezept-Variante wird nicht mit ausgegeben, da sonst eine Exception auftritt
        Dim sColNames As New List(Of String) From {"Nr", "&Bezeichnung"}
        For Each sName In sColNames
            HisDataGridView.ColNames.Add(sName)
        Next

        'DataGrid füllen
        HisDataGridView.LoadData(setParams(sqlRohstoffUse, RohStoff.Nr), "RohstoffVerwendung")
    End Sub
End Class