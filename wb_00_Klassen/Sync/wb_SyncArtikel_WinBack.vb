﻿Public Class wb_SyncArtikel_WinBack
    Inherits wb_Sync

    Friend Overrides Function DBRead() As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        _Data.Clear()

        'Select KO_Nr, KO_Nr_AlNum, KO_Bezeichnung, KA_RZ_Nr, KO_Kommentar, KO_Type, KA_Kurzname, KA_Matchcode, KA_Grp1, KA_Grp2 FROM Komponenten WHERE KO_Type = 0
        If winback.sqlSelect(wb_Sql_Selects.sqlArtikelLst) Then
            While winback.Read
                _Item = New wb_SyncItem
                _Item.Wb_Nummer = winback.sField("KO_Nr_AlNum")
                _Item.Wb_Bezeichnung = wb_Language.TextFilter(winback.sField("KO_Bezeichnung"))
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

    Friend Overrides Function DBInsert(Nr As String, Text As String, Gruppe As String) As Boolean
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function DBUpdate(Nr As String, Text As String, Gruppe As String) As Boolean
        Throw New NotImplementedException()
    End Function
End Class
