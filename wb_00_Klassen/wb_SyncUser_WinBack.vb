Public Class wb_SyncUser_WinBack
    Inherits wb_Sync

    Friend Overrides Function DBRead() As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        _Data.Clear()

        If winback.sqlSelect(wb_Sql_Selects.sqlUsersListe) Then
            While winback.Read
                _Item = New wb_SyncItem
                _Item.wb_Nummer = winback.iField("IP_Wert1int")
                _Item.wb_Bezeichnung = wb_Language.TextFilter(winback.sField("IP_Wert4str"))
                _Item.wb_Gruppe = wb_Language.TextFilter(winback.sField("IP_ItemID"))
                _Item.SyncOK = wb_Global.SyncState.NOK
                _Item.Sort = _Item.wb_Nummer
                Trace.WriteLine("Read WinBack User Nummer         " & _Item.wb_Nummer.ToString + " User Bezeichnung        " & _Item.wb_Bezeichnung)
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
        Dim User As New wb_User
        Return User.Update(Nr, Text, Nr, Gruppe)
    End Function

    Friend Overrides Function DBInsert(Nr As String, Text As String, Gruppe As String) As Boolean
        Dim User As New wb_User
        Return User.AddNew(Text, Nr, Gruppe)
    End Function
End Class
