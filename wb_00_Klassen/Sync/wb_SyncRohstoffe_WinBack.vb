Public Class wb_SyncRohstoffe_WinBack
    Inherits wb_Sync

    Friend Overrides Function DBRead() As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim Wb_NummerOld As String = wb_Global.UNDEFINED
        _Data.Clear()

        Dim sql As String = wb_Sql_Selects.sqlRohstoffLst
        If Not wb_GlobalSettings.SauerteigAnlage Then
            sql = sql + " AND (KO_Type > 100 AND KO_Type < 109) AND KA_Aktiv = 1 ORDER BY KO_Nr_AlNum, KO_Type, KO_Nr"
        Else
            sql = sql + " AND ((KO_Type > 100 AND KO_Type < 109) OR " &
                        "      (KO_Type >= 1 AND KO_Type <= 3)) AND KA_Aktiv = 1 ORDER KO_Nr_AlNum, BY KO_Type, KO_Nr"
        End If

        'Select KO_Nr, KO_Nr_AlNum, KO_Bezeichnung, KA_RZ_Nr, KO_Kommentar, KO_Type, KA_Kurzname, KA_Matchcode, KA_Grp1, KA_Grp2 FROM Komponenten WHERE KO_Type <> 0
        If winback.sqlSelect(sql) Then
            While winback.Read
                _Item = New wb_SyncItem
                'Index (interne Komponenten-Nummer)
                _Item.Wb_Index = winback.iField("KO_Nr")
                'Komponenten-Nummer (alphanumerisch)
                _Item.Wb_Nummer = winback.sField("KO_Nr_AlNum")
                'Komponenten-Bezeichnung
                _Item.Wb_Bezeichnung = wb_Language.TextFilter(winback.sField("KO_Bezeichnung"))
                'Komponenten-Type
                _Item.Wb_Type = wb_Functions.IntToKomponType(winback.iField("KO_Type"))

                _Item.SyncOK = wb_Global.SyncState.NOK
                _Item.Sort = _Item.Wb_Nummer

                'doppelte Einträge werden nicht in die Liste der Rohstoffe(WinBack) übernommen
                If _Item.Wb_Nummer <> Wb_NummerOld Then
                    _Data.Add(_Item)
                    Wb_NummerOld = _Item.Wb_Nummer
                End If
            End While
            winback.Close()

            CheckData(wb_Global.SyncState.WinBackErr)
            Return True
        End If
        winback.Close()
        Return False
    End Function

    ''' <summary>
    ''' Markiert alle Rohstoffe, die in keiner Rezeptur verwendet werden.
    ''' Berücksichtigt werden nur Handkomponenten (Type=102)
    ''' 
    ''' Der entsprechende Datensatz wird als WinBackNotUsed markiert wenn in OrgaBack kein entsprechender Datensatz angelegt ist.
    ''' </summary>
    ''' <returns></returns>
    Friend Overrides Function DBCheckData() As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim KompNr As Integer

        If winback.sqlSelect(wb_Sql_Selects.sqlKompNotUsed) Then
            While winback.Read
                'Komponenten-Nummer (Index)
                KompNr = winback.iField("KO_Nr")
                'Komponenten-Nummer in Array-List suchen
                For Each x As wb_SyncItem In _Data
                    'Komponenten-Nummer (Index) gefunden
                    If x.Wb_Index = KompNr Then
                        'Nur Datensätze die nicht auch in OrgaBack verwendet werden
                        If x.SyncOK = wb_Global.SyncState.OrgaBackMiss Then
                            x.SyncOK = wb_Global.SyncState.WinBackNotUsed
                            Exit For
                        End If
                    End If
                Next
            End While
        End If

        winback.Close()
        Return False
    End Function

    Friend Overrides Function DBInsert(Nr As String, Text As String, Gruppe As String) As Boolean
        'Rohstoff in WinBack neu anlegen
        Dim Komponente As New wb_Komponente
        'Rohstoff neu anlegen (legt auch den Datensatz in winback.Lagerorte an)
        Dim KompNrNeu As Integer = Komponente.MySQLdbNew(wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE)
        'Daten aus OrgaBack
        Komponente.Bezeichnung = Text
        Komponente.Nummer = Nr
        'Datensatz schreiben
        DBInsert = Komponente.MySQLdbUpdate()
        Trace.WriteLine("@I_Insert into WinBack Rohstoff Nummer" & Nr)
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
        Trace.WriteLine("@I_Update WinBack Rohstoff Bezeichnung" & sql)
        winback.Close()
    End Function

    Friend Overrides Function DBNumber(Nr_Alt As String, Nr_Neu As String, Gruppe As String, Text As String) As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateSyncKompAlNr, Nr_Alt, Nr_Neu)
        'Update ausführen
        DBNumber = (winback.sqlCommand(sql) > 0)
        Trace.WriteLine("@I_Update WinBack Rohstoff Nummer" & sql)
        winback.Close()
    End Function

    Friend Overrides Function DBDelete(Index As Integer) As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelSyncKoNr, Index)
        'Update ausführen
        DBDelete = (winback.sqlCommand(sql) > 0)
        Trace.WriteLine("@I_Delete WinBack Rohstoff Nummer" & sql)
        winback.Close()
    End Function
End Class
