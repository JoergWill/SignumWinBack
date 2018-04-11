Public Class wb_SyncUserGruppen_WinBack
    Inherits wb_Sync

    Friend Overrides Function DBRead() As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        _Data.Clear()

        If winback.sqlSelect(wb_Sql_Selects.sqlUserGrpTxt) Then
            While winback.Read
                _Item = New wb_SyncItem
                _Item.Wb_Nummer = winback.iField("II_ItemID")
                _Item.Wb_Bezeichnung = wb_Language.TextFilter(winback.sField("II_Kommentar"))
                _Item.SyncOK = wb_Global.SyncState.NOK
                _Item.Sort = _Item.Wb_Nummer
                _Data.Add(_Item)
            End While
            winback.Close()

            CheckData(wb_Global.SyncState.WinBackErr)
            Return True
        End If
        winback.Close()
        Return False
    End Function

    Friend Overrides Function DBUpdate(Nr As String, Text As String, Gruppe As String) As Boolean
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function DBInsert(Nr As String, Text As String, Gruppe As String) As Boolean
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function DBNumber(Nr_Alt As String, Nr_Neu As String, Gruppe As String) As Boolean
        Throw New NotImplementedException()
    End Function
End Class
