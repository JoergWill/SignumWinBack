Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rezept_Liste
    Inherits DockContent

    'Event Form wird geladen
    Private Sub wb_Rezept_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "Nummer", "&Name", "Variante", "", "", "", "", "", "", "", "", ""}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid füllen
        Dim sql As String = "SELECT RZ_Nr, RZ_Nr_AlNum, RZ_Bezeichnung, RZ_Variante_Nr, RZ_Kommentar, RZ_Gewicht, " &
                            "RZ_Aenderung_Nr, RZ_Aenderung_Datum, RZ_Aenderung_Name, RZ_Liniengruppe, " &
                            "RZ_Charge_Min, RZ_Charge_Max, RZ_Charge_Opt FROM Rezepte"
        DataGridView.LoadData(sql, "RezeptListe", wb_Sql.dbType.mySql)

        'Event Daten wurden geändert
        '        AddHandler wb_User.eEdit_Leave, AddressOf UserInfo

    End Sub

    'Event Form wird geschlossen
    Private Sub wb_Rezept_Liste_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        DataGridView.updateDataBase(wb_Sql.dbType.mySql)
        'Layout sichern
        DataGridView.SaveToDisk("RezeptListe")
    End Sub

    Private Sub DataGridView_HasChanged(sender As Object, e As EventArgs) Handles DataGridView.HasChanged
        wb_Rezept.aktRzNr = CInt(DataGridView.Field("RZ_Nr"))
        wb_Rezept.aktRzNummer = DataGridView.Field("RZ_Nr_AlNum")
        wb_Rezept.aktRzName = DataGridView.Field("RZ_Bezeichnung")
        wb_Rezept.aktRzKommentar = DataGridView.Field("RZ_Kommentar")
        wb_Rezept.aktRzGewicht = DataGridView.Field("RZ_Gewicht")
        wb_Rezept.aktRzVariante = CInt(DataGridView.Field("RZ_Variante_Nr"))
        wb_Rezept.aktRzLinienGrp = CInt(DataGridView.Field("RZ_Liniengruppe"))

        wb_Rezept.aktChangeNr = CInt(DataGridView.Field("RZ_Aenderung_Nr"))
        wb_Rezept.aktChangeDatum = DataGridView.Field("RZ_Aenderung_Datum")
        wb_Rezept.aktChangeName = DataGridView.Field("RZ_Aenderung_Name")

        wb_Rezept.Liste_Click(Nothing)
    End Sub
End Class