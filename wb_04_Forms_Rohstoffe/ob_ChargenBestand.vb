Imports WinBack.wb_Sql_Selects

Public Class ob_ChargenBestand
    Private _Akt_KomNr As Integer = 0

    Public Property Akt_KompNr As Integer
        Get
            Return _Akt_KomNr
        End Get
        Set(value As Integer)
            _Akt_KomNr = value
        End Set
    End Property

    ''' <summary>
    ''' Liest aus der Tabelle dbo.ArtikelLagerkarte alle Datensätze
    '''     mit ArtikelNr = 
    '''     mit FilialeNr = ProduktionsFilialeNr
    '''     Type Rohstoff (Einheit in kg(OrgaBack))
    '''     Bestand größer Null
    '''     
    ''' Die Einträge werden in der Tabelle Lieferungen in Winback verarbeitet.
    ''' Kennungen im Feld LF_gebucht
    '''     V - Bestellvorschlag automatisch
    '''     N - Bestellung (noch nicht gedruckt)
    '''     B - Bestellt (gedruckt/gefaxt)
    '''     G - Geliefert (Bestellung erledigt)
    '''     L - Lieferant zugeordnet zu dieser Komponente
    '''     X - Lieferant gelöscht (für diese Komponente)
    '''     
    '''     1 - Lieferung (Neue Lieferungen werden immer mit '1' als gebucht vermerkt)
    '''     2 - Charge aktiv (WinBack produziert aktuell mit den Daten aus dieser Lieferung)
    '''     3 - Geliefert, Charge komplett verbraucht (auch Inventurbuchung)
    '''     
    ''' Der aktuelle Bestand in WinBack steht in der Tabelle Lagerorte.LG_Bilanzmenge und muss nach dem Import der Lieferungen 
    ''' angepasst werden.
    ''' Da in der Tabelle Lieferungen von Winback auch die Halbfertigprodukte aufgelistet werden, wird winback.Komponenten-Lagerorte-Lieferungen sortiert nach
    ''' Artikelnummer einzeln durchlaufen. Die aktuelle Artikelnummer(intern) wird als Result zurückgegeben.
    ''' Nach Verbuchung der Lieferungen und Korrektur der Daten werden in der Tabelle Lieferungen alle "erledigten" Einträge gelöscht.
    ''' 
    ''' In Spalte winback.Lieferungen.LG_LF_Nr wird die Nummer der letzten verbuchten Wareneingänge aus OrgaBack dbo.ArtikelLagerKarte.Lfd eingetragen. Damit
    ''' werden Doppelbuchungen vermieden und der Resultset aus der Abfrage in OrgaBack wird kleiner.
    ''' </summary>
    ''' <param name="KompNr"></param>
    ''' <returns></returns>
    Public Function ImportChargenBestand(KompNr As Integer) As Integer
        'Aktuelle Komponenten-Nummer setzen
        Akt_KompNr = KompNr
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Lieferungen-Objekt nimmt die aktuellen Daten auf
        Dim Lieferungen As New wb_Lieferungen

        'nächsten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(setParams(sqlRohstoffLagerort, KompNr.ToString)) Then
            'Lesen alle Lieferungen zu dieser Komponente
            If winback.Read Then
                Lieferungen.MySQLdbRead(winback.MySqlRead)
                'aktuell bearbeitete Komponenten-Nummer
                Akt_KompNr = Lieferungen.Nr
                'Verbindung (Lesen) wieder schliessen
                winback.CloseRead()
                'Datenbank-Verbindung öffnen OrgaBack-msSQL
                Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
                'Das Lagerkarten-Objekt nimmt alle Daten aus dbo.ArtikelLagerkarte auf
                Dim LagerKarte As New wb_LagerKarte(KompNr)

                'auf neue Einträge in der Artikel-Lagerkarte aus OrgaBack prüfen
                If orgaback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlArtikelLagerKarte, Lieferungen.OrgaBack_LfdNr.ToString, Lieferungen.Nummer)) Then
                    'wenn neue (noch nicht verbuchte) Einträge vorhanden sind
                    If orgaback.Read Then
                        'Schleife über alle Datensätze
                        Do
                            'Datensätze einlesen
                            LagerKarte.msSQLdbRead(orgaback.msRead)
                            'und verbuchen
                            Lieferungen.Verbuchen(winback, LagerKarte)
                        Loop While orgaback.Read
                        'die letzte gültige laufende Nummer aus OrgaBack.lfd wird in winback.Lagerorte eingetragen
                        Lieferungen.UpdateLagerorteLfd(winback, LagerKarte.Lfd)
                    End If
                End If

                'Datenbank-Verbindung wieder schliessen
                orgaback.Close()
            Else
                'EOF() - ReStart bei KO_Nr = 0
                Akt_KompNr = 0
            End If
        End If

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
        'Rückgabewert ist die aktuell bearbeitete Komponenten-Nummer
        Return Akt_KompNr
    End Function
End Class
