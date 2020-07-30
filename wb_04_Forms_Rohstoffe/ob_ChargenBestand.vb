Imports WinBack.wb_Sql_Selects

Public Class ob_ChargenBestand
    Private _Akt_KompNr As Integer = 0
    Private _Nummer As String = ""
    Private _Bezeichnung As String = ""
    Private _Lfd As Integer = wb_Global.UNDEFINED
    Private _Datum As String
    Private _Bestand As Double
    Private _ChargenNr As String
    Private _Vorfall As String = ""

    Public Property Akt_KompNr As Integer
        Get
            Return _Akt_KompNr
        End Get
        Set(value As Integer)
            _Akt_KompNr = value
        End Set
    End Property

    Public ReadOnly Property Nummer As String
        Get
            Return _Nummer
        End Get
    End Property

    Public ReadOnly Property Bezeichnung As String
        Get
            Return _Bezeichnung
        End Get
    End Property

    Public ReadOnly Property Lfd As String
        Get
            If _Lfd > 0 Then
                Return _Lfd.ToString
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property Vorfall As String
        Get
            Return _Vorfall
        End Get
    End Property

    Public ReadOnly Property Datum As String
        Get
            Return _Datum
        End Get
    End Property

    Public ReadOnly Property Bestand As String
        Get
            Return wb_Functions.FormatStr(_Bestand, 2)
        End Get
    End Property

    Public ReadOnly Property ChargenNr As String
        Get
            Return _ChargenNr
        End Get
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
    Public Function ImportChargenBestand(KompNr As Integer, Optional InitBestand As Boolean = False) As Integer
        'Aktuelle Komponenten-Nummer setzen
        Akt_KompNr = KompNr

        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String
        'Lieferungen-Objekt nimmt die aktuellen Daten auf
        Dim Lieferungen As New wb_Lieferungen

        'Abfrage nächster Datensatz zu dieser Komp-Nr
        sql = "KO_Nr > " & KompNr.ToString

        'nächsten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(setParams(sqlRohstoffLagerort, sql)) Then
            'Lesen Lagerort/Rohstoffdaten zu dieser Komponente
            If winback.Read Then
                Lieferungen.MySQLdbRead(winback.MySqlRead)

                'aktuell bearbeitete Komponenten-Nummer
                _Akt_KompNr = Lieferungen.Nr
                _Nummer = Lieferungen.Nummer
                _Bezeichnung = Lieferungen.Bezeichnung
                'restliche Variablen initialisieren
                _Lfd = wb_Global.UNDEFINED
                _Datum = ""
                _Bestand = 0.0
                _ChargenNr = ""
                _Vorfall = ""

                'Verbindung (Lesen) wieder schliessen
                winback.CloseRead()
                'Datenbank-Verbindung öffnen OrgaBack-msSQL
                Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
                'Das Lagerkarten-Objekt nimmt alle Daten aus dbo.ArtikelLagerkarte auf
                Dim LagerKarte As New wb_LagerKarte

                'auf neue Einträge in der Artikel-Lagerkarte aus OrgaBack prüfen
                If InitBestand Then
                    'letzten Datensatz aus dbo.ArtikelLagerkarte
                    sql = wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlArtikelLagerInit, Lieferungen.Nummer)
                Else
                    'alle Datensätze aus dbo.ArtikelLagerkarte
                    sql = wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlArtikelLagerKarte, Lieferungen.LfdNr.ToString, Lieferungen.Nummer)
                End If
                If orgaback.sqlSelect(sql) Then
                    'wenn neue (noch nicht verbuchte) Einträge vorhanden sind
                    If orgaback.Read Then
                        'Schleife über alle Datensätze
                        Do
                            'Datensätze einlesen
                            LagerKarte.msSQLdbRead(orgaback.msRead)
                            'und verbuchen
                            If InitBestand Then
                                Lieferungen.InitBestand(winback, LagerKarte)
                            Else
                                Lieferungen.Verbuchen(winback, LagerKarte)
                            End If
                        Loop While orgaback.Read
                        'die letzte gültige laufende Nummer aus OrgaBack.lfd wird in winback.Lagerorte eingetragen
                        Lieferungen.UpdateLagerorteLfd(winback, LagerKarte.Lfd)
                        'Daten zur Anzeige im Grid
                        _Lfd = LagerKarte.Lfd
                        _Datum = LagerKarte.Datum
                        _Bestand = LagerKarte.Menge
                        _ChargenNr = LagerKarte.ChargenNummer
                        _Vorfall = LagerKarte.Vorfall
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
