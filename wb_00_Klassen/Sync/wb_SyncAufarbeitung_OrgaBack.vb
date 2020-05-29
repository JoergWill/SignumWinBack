Public Class wb_SyncAufarbeitung_OrgaBack
    Inherits wb_Sync
    Private _GruppenNummer As Integer = wb_Global.UNDEFINED

    Public ReadOnly Property GruppenNummer As Integer
        Get
            Return _GruppenNummer
        End Get
    End Property

    Public Sub New()
        'Gruppen-Nummer für Artikel-Zusatzgruppe MFF200
        GetGruppenNummer()
    End Sub

    ''' <summary>
    ''' Ermittelt die GruppenNummer der Artikel-Zusatzgruppe für das MFF200 (Aufarbeitungsplatz)
    ''' Ist das MFF200 kein Gruppenfeld oder nicht definiert, wird -1 zurückgegeben
    ''' </summary>
    Private Sub GetGruppenNummer()
        'GruppenNummer für MFF200 (Aufarbeitungs-Platz) ermitteln
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        If orgaback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlArtikelZusGruppenNr, wb_Global.MFF_ProduktionsLinie)) Then
            If orgaback.Read Then
                'GruppenNummer
                _GruppenNummer = orgaback.sField("GruppenNr")
                'GruppenNummer Null ist nicht erlaubt !
                If _GruppenNummer = 0 Then
                    _GruppenNummer = wb_Global.UNDEFINED
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Liest alle Einträge aus der Tabelle dbo.ArtikelMultifunktionsfeld mit der GruppenNr des MFF200 (Aufarbeitungsplatz)
    ''' </summary>
    ''' <returns></returns>
    Friend Overrides Function DBRead() As Boolean
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        _Data.Clear()

        'SELECT ArtikelNr,Kurztext,Sortiment,StdEinheit FROM [dbo].[Artikel] ORDER BY ArtikelNr
        If orgaback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlAufarbeitungsPl, GruppenNummer)) Then
            While orgaback.Read
                _Item = New wb_SyncItem
                'Aufarbeitungsplatz-Nummer
                _Item.Os_Gruppe = orgaback.sField("Hierarchie")
                'in Linien-Gruppe umrechnen
                _Item.Os_Nummer = (wb_Functions.StrToInt(_Item.Os_Gruppe) + wb_Global.OffsetBackorte).ToString
                'Aufarbeitungsplatz Bezeichnung
                _Item.Os_Bezeichnung = orgaback.sField("Bezeichnung")

                _Item.SyncOK = wb_Global.SyncState.NOK
                _Item.Sort = _Item.Os_Nummer
                _Data.Add(_Item)
            End While
            orgaback.Close()

            CheckData(wb_Global.SyncState.OrgaBackErr)
            Return True
        End If
        orgaback.Close()
        Return False
    End Function

    ''' <summary>
    ''' Insert in Tabelle dbo.ArtikelMultifunktionsfeld
    ''' Enthält die Gruppen-Einträge für MFF200 (Aufarbeitungsplatz) in OrgaBack
    ''' Es wird Gruppe und Bezeichnungstext geschrieben. Die GruppenNr ist vierstellig mit führenden Nullen und
    ''' hat gegenüber WinBack einen Offset von 100.
    ''' </summary>
    ''' <param name="Nr"></param>
    ''' <param name="Text"></param>
    ''' <param name="Gruppe"></param>
    ''' <returns></returns>
    Friend Overrides Function DBInsert(Nr As String, Text As String, Gruppe As String) As Boolean
        'Verbindung zu MsSQL-Datenbank
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'GruppenNr aus der Liniengruppen-Nummer berechnen
        Gruppe = Right("0000" & (wb_Functions.StrToInt(Nr) - wb_Global.OffsetBackorte), 4)
        'Update OrgaBackMain.dbo.MitarbeiterMultiFunktionsFeld
        DBInsert = orgaback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlInsertAufarbeitungsPl, _GruppenNummer, Gruppe, Text))
        'Datenbank-Verbindung wieder schliessen
        orgaback.Close()
    End Function

    ''' <summary>
    ''' Update Tabelle dbo.ArtikelMultifunktionsfeld
    ''' Enthält die Gruppen-Einträge für MFF200 (Aufarbeitungsplatz) in OrgaBack
    ''' Es wird nur entsprechend der Nummer der Bezeichnungstext angepasst. Die Nummern sind vierstellig mit führenden Nullen und
    ''' haben gegenüber WinBack einen Offset von 100.
    ''' </summary>
    ''' <param name="Nr"></param>
    ''' <param name="Text"></param>
    ''' <param name="Gruppe"></param>
    ''' <returns></returns>
    Friend Overrides Function DBUpdate(Nr As String, Text As String, Gruppe As String) As Boolean
        'Verbindung zu MsSQL-Datenbank
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Update OrgaBackMain.dbo.MitarbeiterMultiFunktionsFeld
        DBUpdate = orgaback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlUpdateAufarbeitungsPl, _GruppenNummer, Gruppe, Text))
        'Datenbank-Verbindung wieder schliessen
        orgaback.Close()
    End Function

    Friend Overrides Function DBNumber(Nr_Alt As String, Nr_Neu As String, Gruppe As String, Text As String) As Boolean
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function DBDelete(Index As Integer) As Boolean
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function DBCheckData() As Boolean
        Throw New NotImplementedException()
    End Function

End Class
