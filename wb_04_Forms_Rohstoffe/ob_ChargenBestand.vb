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
        'Lieferungen-Objekt nimmt die aktuellen Daten auf
        Dim Lieferungen As New wb_Lieferungen
        'Abfrage nächster Datensatz zu dieser Komp-Nr
        Dim sql = "KO_Nr > " & KompNr.ToString

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

                'auf neue Einträge in der Artikel-Lagerkarte aus OrgaBack prüfen
                If InitBestand Then
                    'letzte Datensätze aus dbo.ArtikelLagerkarte
                    InventurBestand(winback, Lieferungen)
                Else
                    'alle Datensätze aus dbo.ArtikelLagerkarte
                    ImportBestand(winback, Lieferungen)
                End If
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

    Private Sub ImportBestand(winback As wb_Sql, Lieferungen As wb_Lieferungen)
        'Datenbank-Verbindung öffnen OrgaBack-msSQL
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Das Lagerkarten-Objekt nimmt alle Daten aus dbo.ArtikelLagerkarte auf
        Dim LagerKarte As New wb_LagerKarte

        'alle Datensätze aus der Lagerkarte ab Lieferung Nummer x
        Dim Sql = wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlArtikelLagerKarte, Lieferungen.LfdNr.ToString, Lieferungen.Nummer)
        'wenn Datensätze vorhanden sind
        If orgaback.sqlSelect(Sql) Then
            'die erste Inventurbuchung setzt alle Buchungen in WinBack.Lieferungen auf Status eledigt(3)
            Dim FlagErsteInventurBuchung As Boolean = True

            'wenn neue (noch nicht verbuchte) Einträge vorhanden sind
            If orgaback.Read Then
                'Schleife über alle Datensätze
                Do
                    LagerKarte.msSQLdbRead(orgaback.msRead)
                    Lieferungen.Verbuchen(winback, LagerKarte, FlagErsteInventurBuchung, False)
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
        'Speicher wieder freigeben
        LagerKarte = Nothing
    End Sub

    Private Sub InventurBestand(winback As wb_Sql, Lieferungen As wb_Lieferungen)
        'Datenbank-Verbindung öffnen OrgaBack-msSQL
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Das Lagerkarten-Objekt nimmt alle Daten aus dbo.ArtikelLagerkarte auf
        Dim LagerKarte As New wb_LagerKarte

        'alle Buchungen ausgehend von der letzten Buchung einlesen
        Dim Sql = wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlArtikelLagerInit, Lieferungen.Nummer)

        If orgaback.sqlSelect(Sql) Then
            'wenn Einträge vorhanden sind
            If orgaback.Read Then
                'der letzte Eintrag enthält den aktuellen Lagerbestand
                LagerKarte.msSQLdbRead(orgaback.msRead)

                Debug.Print("aktueller Datensatz aus OrgaBack Lfd = " & LagerKarte.Lfd)

                'TODO evtl Lieferungen.Bilanzmenge verwenden
                Dim BestandAktuell As Double = LagerKarte.BestandAktuell
                Dim LieferMenge As Double = LagerKarte.LieferMenge
                Lieferungen.InitBestand(winback, LagerKarte)

                'Schleife über alle Datensätze bis die Summe aller Lieferungen die Bestandsmenge erreicht hat
                Do While orgaback.Read And (BestandAktuell > LieferMenge)
                    LagerKarte.msSQLdbRead(orgaback.msRead)

                    'negative Inventurbuchungen werden momentan nicht verarbeitet
                    If LagerKarte.Menge > 0 Then
                        Debug.Print("Datensatz aus OrgaBack Lfd = " & LagerKarte.Lfd)
                        Lieferungen.Verbuchen(winback, LagerKarte, False, True)

                        'Liefermenge aufaddieren
                        LieferMenge += LagerKarte.LieferMenge
                    End If
                Loop

                'der letze Datensatz wird angepasst - Verbauchte Menge und Status=aktiv
                Lieferungen.UpdateVerbrauch(winback, Lieferungen.LagerOrt, LagerKarte.Lfd, LieferMenge - BestandAktuell, "2")

                'die letzte gültige laufende Nummer aus OrgaBack.lfd wird in winback.Lagerorte eingetragen
                Lieferungen.Bilanzmenge = BestandAktuell
                Lieferungen.UpdateLagerorteLfd(winback, LagerKarte.Lfd)

                'Daten zur Anzeige im Grid
                _Lfd = LagerKarte.Lfd
                _Datum = LagerKarte.Datum
                _Bestand = BestandAktuell
                _ChargenNr = LagerKarte.ChargenNummer
                _Vorfall = LagerKarte.Vorfall
            End If
        End If

        'Datenbank-Verbindung wieder schliessen
        orgaback.Close()
    End Sub

End Class
