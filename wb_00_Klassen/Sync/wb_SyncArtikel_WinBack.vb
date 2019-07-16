Public Class wb_SyncArtikel_WinBack
    Inherits wb_Sync

    Friend Overrides Function DBRead() As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim KomponType As wb_Global.KomponTypen
        _Data.Clear()

        'Select KO_Nr, KO_Nr_AlNum, KO_Bezeichnung, KA_RZ_Nr, KO_Kommentar, KO_Type, KA_Kurzname, KA_Matchcode, KA_Grp1, KA_Grp2 FROM Komponenten WHERE KO_Type = 0
        If winback.sqlSelect(wb_Sql_Selects.sqlArtikelLst) Then
            While winback.Read
                _Item = New wb_SyncItem
                'Artikelnummer
                _Item.Wb_Index = winback.iField("KO_Nr")
                'Bezeichnungstext
                _Item.Wb_Nummer = winback.sField("KO_Nr_AlNum")
                'Bezeichnungstext
                _Item.Wb_Bezeichnung = wb_Language.TextFilter(winback.sField("KO_Bezeichnung"))
                'Standard-Einheit aus Komponenten-Type
                KomponType = wb_Functions.IntToKomponType(winback.iField("KO_Type"))
                _Item.Wb_Gruppe = wb_Einheiten_Global.getEinheitFromKompType(KomponType)

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

    Friend Overrides Function DBCheckData() As Boolean
        'Throw New NotImplementedException()
        Return False
    End Function

    Friend Overrides Function DBInsert(Nr As String, Text As String, Gruppe As String) As Boolean
        'Artikel in WinBack neu anlegen
        Dim Komponente As New wb_Komponente
        'Artikel neu anlegen 
        Dim KompNrNeu As Integer = Komponente.MySQLdbNew(wb_Global.KomponTypen.KO_TYPE_ARTIKEL)
        'Daten aus OrgaBack
        Komponente.Bezeichnung = Text
        Komponente.Nummer = Nr
        'Datensatz schreiben
        DBInsert = Komponente.MySQLdbUpdate()
        Trace.WriteLine("@I_Insert into WinBack Artikel Nummer" & Nr)
        'Speicher wieder freigeben
        Komponente = Nothing
        'Anlegen erfolgreich
        Return DBInsert
    End Function

    Friend Overrides Function DBUpdate(Nr As String, Text As String, Gruppe As String) As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateSyncKomp, Nr, wb_Functions.XRemoveSonderZeichen(Text))
        'Update ausführen
        DBUpdate = (winback.sqlCommand(sql) > 0)
        Trace.WriteLine("@I_Update WinBack Artikel Bezeichnung" & sql)
        winback.Close()
    End Function

    Friend Overrides Function DBNumber(Nr_Alt As String, Nr_Neu As String, Gruppe As String) As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateSyncKompAlNr, Nr_Alt, Nr_Neu)
        'Update ausführen
        DBNumber = (winback.sqlCommand(sql) > 0)
        Trace.WriteLine("@I_Update WinBack Artikel Nummer" & sql)
        winback.Close()
    End Function

    Friend Overrides Function DBDelete(Index As Integer) As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelSyncKoNr, Index)
        'Update ausführen
        DBDelete = (winback.sqlCommand(sql) > 0)
        Trace.WriteLine("@I_Delete WinBack Artikel Nummer" & sql)
        winback.Close()
    End Function
End Class

