Public Class wb_SyncRohstoffe_OrgaBack
    Inherits wb_Sync

    Friend Overrides Function DBRead() As Boolean
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        _Data.Clear()

        'SELECT ArtikelNr,Kurztext,Sortiment FROM [dbo].[Artikel] ORDER BY ArtikelNr
        If orgaback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlArtikel, wb_GlobalSettings.OsGrpRohstoffe)) Then
            While orgaback.Read
                _Item = New wb_SyncItem
                _Item.Os_Nummer = orgaback.sField("ArtikelNr")
                _Item.Os_Bezeichnung = orgaback.sField("Kurztext")
                _Item.Os_Gruppe = ""
                _Item.SyncOK = wb_Global.SyncState.NOK
                _Item.Sort = _Item.Os_Nummer
                'Nur Artikel aus dem Sortiment(Produktions-Filiale) übernehmen
                If wb_Filiale.SortimentIstProduktion(orgaback.sField("Sortiment")) Then
                    _Data.Add(_Item)
                End If
            End While
            orgaback.Close()

            CheckData(wb_Global.SyncState.OrgaBackErr)
            Return True
        End If
        orgaback.Close()
        Return False
    End Function

    Friend Overrides Function DBInsert(Nr As String, Text As String, Gruppe As String) As Boolean
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function DBUpdate(Nr As String, Text As String, Gruppe As String) As Boolean
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function DBNumber(Nr_Alt As String, Nr_Neu As String, Gruppe As String) As Boolean
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function DBDelete(Index As Integer) As Boolean
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function DBCheckData() As Boolean
        Throw New NotImplementedException()
    End Function
End Class
