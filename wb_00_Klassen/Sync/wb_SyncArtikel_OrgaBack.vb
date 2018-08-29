Public Class wb_SyncArtikel_OrgaBack
    Inherits wb_Sync

    Friend Overrides Function DBRead() As Boolean
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        _Data.Clear()

        'SELECT ArtikelNr,Kurztext,Sortiment,StdEinheit FROM [dbo].[Artikel] ORDER BY ArtikelNr
        If orgaback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlArtikel, wb_GlobalSettings.OsGrpBackwaren)) Then
            While orgaback.Read
                _Item = New wb_SyncItem
                'Artikelnummer
                _Item.Os_Nummer = orgaback.sField("ArtikelNr")
                'Bezeichnungstext
                _Item.Os_Bezeichnung = orgaback.sField("Kurztext")
                'Standard-Einheit aus OrgaBack
                _Item.Os_Gruppe = orgaback.sField("StdEinheit")

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
