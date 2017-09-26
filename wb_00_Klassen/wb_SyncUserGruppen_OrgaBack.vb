Public Class wb_SyncUserGruppen_OrgaBack
    Inherits wb_Sync

    Public Overrides Function DBRead() As Boolean
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        _Data.Clear()

        If orgaback.sqlSelect(wb_Sql_Selects.mssqlMitarbeiterGruppen) Then
            While orgaback.Read
                _Item = New wb_SyncItem
                _Item.os_Nummer = orgaback.iField("Hierarchie")
                _Item.os_Bezeichnung = orgaback.sField("Bezeichnung")
                _Item.SyncOK = wb_Global.SyncState.NOK
                _Item.Sort = _Item.os_Nummer
                Trace.WriteLine("Read OrgaBack Mitarbeiter-Gruppe Nummer " & _Item.os_Nummer.ToString + " Mitarbeiter-Gruppe Bezeichnung " & _Item.os_Bezeichnung)
                _Data.Add(_Item)
            End While
            orgaback.Close()
            Return True
        End If
        orgaback.Close()
        Return False
    End Function

    Public Overrides Function DBInsert(Nr As String, Text As String) As Boolean
        Throw New NotImplementedException()
    End Function

    Public Overrides Function DBUpdate(Nr As String, Text As String) As Boolean
        Throw New NotImplementedException()
    End Function
End Class
