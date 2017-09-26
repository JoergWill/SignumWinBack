Public Class wb_SyncUserGruppen_WinBack
    Inherits wb_Sync

    Public Overrides Function DBRead() As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        _Data.Clear()

        If winback.sqlSelect(wb_Sql_Selects.sqlUserGrpTxt) Then
            While winback.Read
                _Item = New wb_SyncItem
                _Item.wb_Nummer = winback.iField("II_ItemID")
                _Item.wb_Bezeichnung = wb_Language.TextFilter(winback.sField("II_Kommentar"))
                _Item.SyncOK = wb_Global.SyncState.NOK
                _Item.Sort = _Item.wb_Nummer
                Trace.WriteLine("Read WinBack User-Gruppe Nummer         " & _Item.wb_Nummer.ToString + " User-Gruppe Bezeichnung        " & _Item.wb_Bezeichnung)
                _Data.Add(_Item)
            End While
            winback.Close()
            Return True
        End If
        winback.Close()
        Return False
    End Function

    Public Overrides Function DBUpdate(Nr As String, Text As String) As Boolean
        Throw New NotImplementedException()
    End Function

    Public Overrides Function DBInsert(Nr As String, Text As String) As Boolean
        Throw New NotImplementedException()
    End Function
End Class
