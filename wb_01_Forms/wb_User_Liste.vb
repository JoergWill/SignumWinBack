Public Class wb_User_Liste
    'Anstelle der Gruppen-Nummer wird die Gruppen-Bezeichnung ausgegeben
    Const GrpIdxColumn As Integer = 3

    Private Sub wb_User_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Data_Load()
        'Event Daten wurden geändert
        AddHandler wb_User.eEdit_Leave, AddressOf UserInfo
    End Sub

    Private Sub Data_Load()
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "", "&Name", "Gruppe", ""}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next
        'DataGrid füllen
        Dim sql As String = "SELECT IP_ItemTyp, IP_Lfd_Nr, IP_Wert4str, IP_ItemID, IP_Wert1int FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert1int <> 709760"
        DataGridView.LoadData(sql, "UserListe", wb_Sql.dbType.mySql)

    End Sub

    Private Sub wb_User_Liste_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        DataGridView.updateDataBase(wb_Sql.dbType.mySql)
        'Layout sichern
        DataGridView.SaveToDisk("UserListe")
    End Sub

    'Datensatz-Zeiger wurde geändert
    Private Sub DataGridView_HasChanged() Handles DataGridView.HasChanged
        wb_User.aktUserName = DataGridView.Field("IP_Wert4Str")
        wb_User.aktUserGroup = CInt(DataGridView.Field("IP_ItemID"))
        wb_User.aktUserPass = DataGridView.Field("IP_Wert1int")
        wb_User.Liste_Click(Nothing)
    End Sub

    'Daten im Detail-Fenster sind geändert worden - in DataViewGrid zurückschreiben
    Private Sub UserInfo()
        DataGridView.Field("IP_Wert4Str") = wb_User.aktUserName
        DataGridView.Field("IP_ItemID") = wb_User.aktUserGroup
        DataGridView.Field("IP_Wert1int") = wb_User.aktUserPass
    End Sub

    'Anstelle der Gruppen-Nummer wird die Gruppen-Bezeichnung ausgegeben
    'die Texte kommen aus eine HashTable
    Private Sub DataGridView_CellFormatting(sender As Object, e As Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView.CellFormatting
        Try
            If e.ColumnIndex = GrpIdxColumn Then
                e.Value = wb_User.GrpTexte(CInt(e.Value)).ToString
            End If
        Catch
        End Try
    End Sub
End Class