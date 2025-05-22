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
    Private _LagerEntnahme As New Dictionary(Of String, Double)

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
    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
    Public Function ImportChargenBestand(KompNr As Integer, Optional InitBestand As Boolean = False) As Integer
        'Aktuelle Komponenten-Nummer setzen
        Akt_KompNr = KompNr

        'wenn zu diesem Rohstoff mehrere Silos existieren 
        If wb_Rohstoffe_Shared.AnzahlSilos(KompNr.ToString) > wb_Global.UNDEFINED Then
            'keine Verbuchung der Bestände (Aufteilung auf die Silos nicht möglich)
            Trace.WriteLine("@I_Keine Verbuchung von Chargen-Beständen für Silo-Rohstoffe!")
            Return Akt_KompNr + 1
        Else
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
                    'Komponenten-Nummer und Lagerort einlesen
                    Lieferungen.MySQLdbRead(winback.MySqlRead)

                    'Prüfen ob ein gültiger Lagerort in Komponenten-Tabelle eingetragen ist
                    If (Lieferungen.LagerOrt <> "") Then

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
                            InventurBestand_II(winback, Lieferungen)
                        Else
                            'alle Datensätze aus dbo.ArtikelLagerkarte
                            ImportBestand_II(winback, Lieferungen)
                        End If
                    Else
                        'Lagerort nicht in winback.Komponenten eingetragen (Fehler in Datenstruktur)
                        Trace.WriteLine("@E_Lagerort nicht in winback.Komponenten.KA_Lagerort eingetragen! Nummer/Bezeichnung/Nr " & Lieferungen.Nummer & "/" & Lieferungen.Bezeichnung & "/" & Lieferungen.Nr)
                        winback.CloseRead()

                        'Datensatz in winback.Lagerorte anlegen
                        Dim KA_Lagerort = "KT102_" + Lieferungen.Nr.ToString
                        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlInsertLagerOrte, KA_Lagerort))
                        'Lagerort in Komponentendaten eintragen
                        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateKomp_KO_Nr, Lieferungen.Nr.ToString, "KA_Lagerort = '" & KA_Lagerort & "'"))

                        'mit der aktuellen Komponenten-Nummer nochmal neu starten
                        Akt_KompNr -= 1
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
        End If

    End Function

    Private Sub ImportBestand_I(winback As wb_Sql, Lieferungen As wb_Lieferungen)
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
                Lieferungen.UpdateLagerorteLfd(winback, Lieferungen.LagerOrt, LagerKarte.Lfd)

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

    ''' <summary>
    ''' Prüft ob seit dem letzten Durchlauf eine neue Inventur-Buchung eingetragen wurde.
    ''' Wenn eine neue Inventur verzeichnet ist, wird für diesen Rohstoff die Lieferungen-Tabelle neu geschrieben!
    ''' </summary>
    ''' <param name="winback"></param>
    ''' <param name="Lieferungen"></param>
    Private Sub ImportBestand_II(winback As wb_Sql, Lieferungen As wb_Lieferungen)
        'Datenbank-Verbindung öffnen OrgaBack-msSQL
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Das Lagerkarten-Objekt nimmt alle Daten aus dbo.ArtikelLagerkarte auf
        Dim LagerKarte As New wb_LagerKarte

        'TODO quick and dirty - SQL-Anweisung ändern und nur auf Inventurbuchungen prüfen - dann gleich raus und Inventur
        ' was pasiert mit der lfdNummer ?

        'alle Datensätze aus der Lagerkarte ab Lieferung Nummer x
        Dim Sql = wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlArtikelLagerKarte, Lieferungen.LfdNr.ToString, Lieferungen.Nummer)
        'wenn Datensätze vorhanden sind
        If orgaback.sqlSelect(Sql) Then
            'die erste Inventurbuchung setzt ein Flag zur Initialisierung der Lieferungen-Tabelle
            Dim FlagInventurBuchung As Boolean = False

            'wenn neue (noch nicht verbuchte) Einträge vorhanden sind
            If orgaback.Read Then
                'Schleife über alle Datensätze
                Do
                    LagerKarte.msSQLdbRead(orgaback.msRead)
                    If LagerKarte.Vorfall = "IV" Then
                        FlagInventurBuchung = True
                    End If
                Loop While orgaback.Read

                'die letzte gültige laufende Nummer aus OrgaBack.lfd wird in winback.Lagerorte eingetragen
                Lieferungen.UpdateLagerorteLfd(winback, Lieferungen.LagerOrt, LagerKarte.Lfd)
                'Tabelle Lieferung neu initialisieren
                If FlagInventurBuchung Then
                    InventurBestand_II(winback, Lieferungen)
                End If

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

    Private Sub InventurBestand_I(winback As wb_Sql, Lieferungen As wb_Lieferungen)
        'Datenbank-Verbindung öffnen OrgaBack-msSQL
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Das Lagerkarten-Objekt nimmt alle Daten aus dbo.ArtikelLagerkarte auf
        Dim LagerKarte As New wb_LagerKarte
        'Liste aller Buchungen mit negativer Liefermenge (Entnahme/Bruch/Schwund/Korrektur/Produktion!)
        _LagerEntnahme.Clear()

        'alle Buchungen ausgehend von der letzten Buchung einlesen
        Dim Sql = wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlArtikelLagerInit, Lieferungen.Nummer)

        If orgaback.sqlSelect(Sql) Then
            'wenn Einträge vorhanden sind
            If orgaback.Read Then
                'der letzte Eintrag enthält den aktuellen Lagerbestand
                LagerKarte.msSQLdbRead(orgaback.msRead)
                Debug.Print("erster Datensatz aus OrgaBack Lfd/Charge = " & LagerKarte.Lfd & "/" & LagerKarte.ChargenNummer)

                'TODO evtl Lieferungen.Bilanzmenge verwenden
                Dim BestandAktuell As Double = LagerKarte.BestandAktuell
                Dim LieferMenge As Double = LagerKarte.LieferMenge
                'laufende Nummer aus OrgaBack
                Dim AktLfdNummer As Integer = LagerKarte.Lfd
                'erster(letzter gültiger) Eintrag aus OrgaBack - LfdNummer merken
                Dim FirstLfdNummer As Integer = AktLfdNummer
                'Tabelle Lieferungen leeren und den aktuellen OrgaBack.Lagerkarten-Datensatz als ersten Datensatz eintragen
                Lieferungen.InitBestand(winback, LagerKarte)
                'Prüfen ob der erste Eintrag aus der Lagerkarte negativ ist
                If LagerKarte.Menge < 0 Then
                    'Zur Liste der Korrekturbuchungen/Entnahme/Bruch/Schwund hinzufügen
                    LagerEntnahmeAdd(LagerKarte)
                End If

                'wenn neue (noch nicht verbuchte) Einträge vorhanden sind
                If orgaback.Read Then
                    'Schleife über alle Datensätze bis die Summe aller Lieferungen die Bestandsmenge erreicht hat
                    Do
                        'Lieferdaten aus der OrgaBack-Lagerkarte
                        LagerKarte.msSQLdbRead(orgaback.msRead)
                        Debug.Print("...    Datensatz aus OrgaBack Lfd/Charge = " & LagerKarte.Lfd & "/" & LagerKarte.ChargenNummer)

                        'negative Inventurbuchungen werden als Entnahme verbucht (Chargen-Nummer wird geprüft)
                        If LagerKarte.Menge > 0 Then

                            'Prüfen ob zu dieser Chargen-Nummer Datensätze in der Liste der Negativ-Buchungen vorhanden sind
                            Dim KorrekturMenge As Double = LagerEntnahmeCheck(LagerKarte)
                            LieferMenge += KorrekturMenge

                            Lieferungen.Verbuchen(winback, LagerKarte, False, True)
                            Debug.Print("Datensatz Zugang aus OrgaBack Lfd/Charge/LieferMenge = " & LagerKarte.Lfd & "/" & LagerKarte.ChargenNummer & "/" & LagerKarte.LieferMenge)
                            'laufende Nummer aus OrgaBack
                            AktLfdNummer = LagerKarte.Lfd

                            'Liefermenge aufaddieren
                            LieferMenge += LagerKarte.LieferMenge
                        Else
                            'Liste aller Inventurbuchungen mit negativer Liefermenge (Entnahme/Bruch/Schwund/Korrektur)
                            LagerEntnahmeAdd(LagerKarte)
                        End If
                        'Wenn die Summe aller Lieferungen die Bestandmenge erreicht hat wird nicht mehr weiter gebucht
                    Loop While orgaback.Read And (BestandAktuell > LieferMenge)
                End If

                'der letze Datensatz wird angepasst - Verbrauchte Menge und Status=aktiv
                Lieferungen.UpdateVerbrauch(winback, Lieferungen.LagerOrt, AktLfdNummer, LieferMenge - BestandAktuell, "2")

                'die letzte gültige laufende Nummer aus OrgaBack.lfd wird in winback.Lagerorte eingetragen
                Lieferungen.Bilanzmenge = BestandAktuell
                Lieferungen.UpdateLagerorteLfd(winback, Lieferungen.LagerOrt, FirstLfdNummer)

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


    'TODO Hier neue Routine schreiben: InventurBestand - Abfrage der Daten aus dbo.ChargenBestand
    '     Siehe Mail von J.Erhardt vom 24.08.2021

    Private Sub InventurBestand_II(winback As wb_Sql, Lieferungen As wb_Lieferungen)
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
                Debug.Print("erster Datensatz aus OrgaBack Lfd/Charge = " & LagerKarte.Lfd & "/" & LagerKarte.ChargenNummer)

                Dim BestandAktuell As Double = LagerKarte.BestandAktuell
                'erster(letzter gültiger) Eintrag aus OrgaBack - LfdNummer merken
                Dim AktLfdNummer As Integer = LagerKarte.Lfd + 1
                'Tabelle Lieferungen leeren(Liefermenge muss immer Null sein !)
                LagerKarte.Menge = 0
                Lieferungen.InitBestand(winback, LagerKarte)

                'dbo.ArtikelLagerkarte wieder schliessen
                orgaback.CloseRead()

                'Die Lieferdaten kommen aus dbo.ChargenBestand (Filiale=0-Gesamtunternehmen)(Einheit=11-kg)
                Sql = wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlArtikelLagerChargen, Lieferungen.Nummer, wb_Global.obEinheitKilogramm)

                If orgaback.sqlSelect(Sql) Then
                    'Schleife über alle Datensätze bis die Summe aller Lieferungen die Bestandsmenge erreicht hat
                    While orgaback.Read
                        'Lieferdaten aus OrgaBack-ChargenBestand
                        LagerKarte.msSQLdbRead(orgaback.msRead)
                        Debug.Print("...    Datensatz aus OrgaBack Lfd/Charge = " & LagerKarte.Lfd & "/" & LagerKarte.ChargenNummer)

                        'Lfd.Nummer berechnen (ist in dbo.ChargenBestand nicht vorhanden)
                        AktLfdNummer -= 1
                        'Datensatz verbuchen (Lieferung als WE)
                        LagerKarte.Lfd = AktLfdNummer
                        LagerKarte.Vorfall = "WE"
                        Lieferungen.Verbuchen(winback, LagerKarte, False, True)
                        Debug.Print("Datensatz Zugang aus OrgaBack Lfd/Charge/LieferMenge = " & LagerKarte.Lfd & "/" & LagerKarte.ChargenNummer & "/" & LagerKarte.LieferMenge)
                    End While
                End If

                'der letze Datensatz wird angepasst - Verbrauchte Menge und Status=aktiv
                Lieferungen.UpdateVerbrauch(winback, Lieferungen.LagerOrt, AktLfdNummer, 0, "2")

                'die letzte gültige laufende Nummer aus OrgaBack.lfd wird in winback.Lagerorte eingetragen
                Lieferungen.Bilanzmenge = BestandAktuell
                Lieferungen.UpdateLagerorteLfd(winback, Lieferungen.LagerOrt, AktLfdNummer)

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

    ''' <summary>
    ''' Dictionary LagerEntnahme aufbauen.
    ''' Wenn die Chargen-Nummer schon vorhanden ist, wird die (Entnahme)Menge addiert, ansonsten wird ein neuer Eintrag erzeugt.
    ''' </summary>
    ''' <param name="Lagerkarte"></param>
    Private Sub LagerEntnahmeAdd(Lagerkarte As wb_LagerKarte)
        Debug.Print("Datensatz Entnahme aus OrgaBack Lfd/Charge/LieferMenge = " & Lagerkarte.Lfd & "/" & Lagerkarte.ChargenNummer & "/" & Lagerkarte.Menge)

        'Prüfen ob die Chargen-Nummer schon einen Eintrag hat
        If _LagerEntnahme.ContainsKey(Lagerkarte.ChargenNummer) Then
            'Menge aufaddieren. (Abbuchung ist negativ!)
            _LagerEntnahme(Lagerkarte.ChargenNummer) += Lagerkarte.Menge
        Else
            'neuen Eintrag erzeugen
            _LagerEntnahme.Add(Lagerkarte.ChargenNummer, Lagerkarte.Menge)
        End If
    End Sub

    ''' <summary>
    ''' Prüft ob zu dieser Chargen-Nummer (Lager-Zugang) ein Eintrag im Dictionary LagerEntnahme existiert.
    ''' Wenn die Chargen-Nummer als Entnahme/Inventur/Verbrauch exisitiert, wird die Liefermenge reduziert
    ''' (TODO) wird die Entnahmemenge aus dem Dictionary als verbraucht in die Lagerkarte eingetragen.
    ''' Die Menge im Dictionary wird korrigiert.
    ''' </summary>
    ''' <param name="Lagerkarte"></param>
    ''' <returns></returns>
    Private Function LagerEntnahmeCheck(ByVal Lagerkarte As wb_LagerKarte) As Double
        'Prüfen ob die Chargen-Nummer schon einen Eintrag hat
        If _LagerEntnahme.ContainsKey(Lagerkarte.ChargenNummer) Then
            'Entnahme-Menge (Summe aller Buchungen) - Entnahme-Menge ist negativ !!
            Dim SummeEntnahmeMenge As Double = -1 * _LagerEntnahme(Lagerkarte.ChargenNummer)
            'LieferMenge aus Lagerkarte
            Dim LieferMenge As Double = Lagerkarte.LieferMenge

            'LieferMenge ist größer als die Entnahme-Menge
            If LieferMenge >= SummeEntnahmeMenge Then
                'Differenzmenge
                Dim DifferenzMenge As Double = LieferMenge - SummeEntnahmeMenge
                'Verbraucht in Lagerkarte eintragen
                Lagerkarte.Verbraucht = DifferenzMenge
                'Dictionary Menge korrigieren (Entnahme-Menge wurde komplett geliefert) - Eintrag löschen
                _LagerEntnahme.Remove(Lagerkarte.ChargenNummer)
                'KorrekturMenge zurückgeben
                Return DifferenzMenge
            Else
                'Liefermenge ist kleiner als die Summe aller Entnahme-Mengen (Verbrauch/Inventur/Abbuchung..)
                Lagerkarte.Verbraucht = LieferMenge
                'Dictionary Menge korrigieren
                _LagerEntnahme(Lagerkarte.ChargenNummer) += LieferMenge
                'KorrekturMenge zurückgeben
                Return LieferMenge
            End If

        Else
            Return 0.0
        End If

    End Function

End Class
