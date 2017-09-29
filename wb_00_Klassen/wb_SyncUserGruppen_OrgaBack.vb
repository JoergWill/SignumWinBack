Public Class wb_SyncUserGruppen_OrgaBack
    Inherits wb_Sync

    Friend Overrides Function DBRead() As Boolean
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

            CheckData(wb_Global.SyncState.OrgaBackErr)
            Return True
        End If
        orgaback.Close()
        Return False
    End Function

    Friend Overrides Function DBInsert(Nr As String, Text As String, Gruppe As String) As Boolean
        'Gruppen-Nummer formatieren
        Dim sGrpNr As String
        sGrpNr = Strings.Right("0000" + Nr.ToString, 4)

        'Verbindung zu MsSQL-Datenbank
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Update OrgaBackMain.dbo.MitarbeiterMultiFunktionsFeld
        DBInsert = orgaback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlUpdateMitarbeiterGruppen, sGrpNr, Text))
        'Datenbank-Verbindung wieder schliessen
        orgaback.Close()
    End Function

    Friend Overrides Function DBUpdate(Nr As String, Text As String, Gruppe As String) As Boolean
        'Gruppen-Nummer formatieren
        Dim sGrpNr As String
        sGrpNr = Strings.Right("0000" + Nr.ToString, 4)

        'Verbindung zu MsSQL-Datenbank
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Update OrgaBackMain.dbo.MitarbeiterMultiFunktionsFeld
        DBUpdate = orgaback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlInsertMitarbeiterGruppen, sGrpNr, Text, 1))
        'Datenbank-Verbindung wieder schliessen
        orgaback.Close()
    End Function
End Class
