Public Class wb_User_Liste

    'Event Form wird geladen
    Private Sub wb_User_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "", "&Name", "Gruppe", ""}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        wb_User.LoadGrpTexte()
        'DataGrid füllen
        Dim sql As String = "SELECT IP_ItemTyp, IP_Lfd_Nr, IP_Wert4str, IP_ItemID, IP_Wert1int FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert1int <> 709760"
        DataGridView.LoadData(sql, "UserListe", wb_Sql.dbType.mySql)

        'Event Daten wurden geändert
        AddHandler wb_User.eEdit_Leave, AddressOf UserInfo
    End Sub

    'Event Form wird geschlossen
    Private Sub wb_User_Liste_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        DataGridView.updateDataBase(wb_Sql.dbType.mySql)
        'Layout sichern
        DataGridView.SaveToDisk("UserListe")
    End Sub

    'Event-Handler aus wb_User(Detail)
    'Daten im Detail-Fenster sind geändert worden - in DataViewGrid zurückschreiben
    Private Sub UserInfo()
        DataGridView.Field("IP_Wert4Str") = wb_User.aktUserName
        DataGridView.Field("IP_ItemID") = wb_User.aktUserGroup
        DataGridView.Field("IP_Wert1int") = wb_User.aktUserPass
    End Sub

    'Dat2ensatz-Zeiger wurde geändert
    Private Sub DataGridView_HasChanged() Handles DataGridView.HasChanged
        wb_User.aktUserName = DataGridView.Field("IP_Wert4Str")
        wb_User.aktUserGroup = CInt(DataGridView.Field("IP_ItemID"))
        wb_User.aktUserPass = DataGridView.Field("IP_Wert1int")
        wb_User.Liste_Click(Nothing)
    End Sub

    'Anstelle der Gruppen-Nummer wird die Gruppen-Bezeichnung ausgegeben
    'die Texte kommen aus eine HashTable
    Const GrpIdxColumn As Integer = 3
    Private Sub DataGridView_CellFormatting(sender As Object, e As Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView.CellFormatting
        Try
            If e.ColumnIndex = GrpIdxColumn Then
                If (CInt(e.Value) > 0) Then
                    e.Value = wb_User.GrpTexte(CInt(e.Value)).ToString
                Else
                    e.Value = ""
                End If
            End If
        Catch
        End Try
    End Sub
End Class