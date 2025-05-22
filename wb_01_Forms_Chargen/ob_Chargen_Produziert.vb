Imports MySql.Data.MySqlClient

''' <summary>
''' Rückmeldung der produzierten Chargen an OrgaBack
''' Schreibt alle zum Zeitpunkt x produzierten Artikel in die Tabelle dbo.ProduzierteWare
''' Alle Rohstoffe die verbraucht worden sind werden in die Tabelle dbo.ProduzierteWare mit SatzTyp=V geschrieben
''' 
''' Nach dem Schreiben der Daten werden die Artikel und Rohstoffe in WinBack mit wbdaten.BAK_ArbRezepte.B_ARZ_Status = Exp gekennzeichnet
''' </summary>
Public Class ob_Chargen_Produziert

    Private opw_Liste As New ArrayList  'OrgaBack produzierte Ware
    Private wpl_Liste As New ArrayList  'WinBack "leere" Chargen (nicht gestartet)
    Private err_Liste As New ArrayList  'Liste aller Fehler/Chargen, die nicht verbucht werden konnten
    Private Const LIMIT = 5000

    Public ReadOnly Property ErrorTrace As ArrayList
        Get
            Return err_Liste
        End Get
    End Property

    ''' <summary>
    ''' Alle Chargen vor dem Stichtag werden als ungültig deklariert und nicht an OrgaBack zurückgemeldet
    ''' (Hauptsächlich nicht produzierte Chargen)
    ''' </summary>
    Public Sub New()
        'Debug.Print("ob_Chargen_Produziert.New")
    End Sub

    ''' <summary>
    ''' Exportiert die einzelnen Produktions-Chargen-Daten ab der vorgegebenen 
    ''' Tageswechselnummer in dbo.ProduzierteWare. Zurückgegeben wird 
    ''' die letzte ausgegebene Tageswechsel-Nr.
    ''' 
    ''' Es wird immer zuerst der Chargen-Kopf und danach die verbrauchten Rohstoffe ausgegeben
    ''' 
    ''' (@1.7.5)    Wenn der Artikel-Datensatz (Satztyp wb_Global.obSatzTyp.ProduzierterArtikel) nicht geschreiben werden kann,
    '''             Dann ist vermutlich der Artikel in OrgaBack nicht vorhanden.
    '''             In diesem Fall wird dann ein Dummy-Artikel geschrieben, damit die Rohstoffe nicht falsch zugeordnet werden.
    '''             Der Dummy-Artikel hat die Nummer 0R9999 (Public Const ProduktionDummyArtikel)
    '''             
    ''' (@V1.8.4)   Sicherheitshalber werden die Daten ab der letzten Tageswechsel-Nummer minus der Anzahl der Segmente gelesen
    '''             Sonst kann es vorkommen, dass neue Daten auf Linie 1 erzeugt werden, während gerade die Tageswechseldaten auf Linie 2
    '''             verarbeitet werden (anderes Segment). Damit würden nie wieder die neuen Daten von Linie 1 verarbeitet werden.
    '''             (Fehler bei Fonk 16.03.2021)
    ''' </summary>
    ''' <param name="TWNr"></param>
    ''' <returns></returns>
    Public Function ExportChargen(TWNr As Integer) As Long
        Dim sql As String
        Dim WriteOK As Boolean = False
        Dim wbdaten As wb_Sql
        Dim WinBackChargenNummer As String = ""
        Dim TageswechselNr As Long = TWNr
        Dim opw_Zeile As New ob_ProduzierteWare(WinBackChargenNummer)
        Dim MaxLinienSegmente As Integer = wb_Linien_Global.MaxSegmente

        'Liste(n) löschen
        opw_Liste.Clear()
        err_Liste.Clear()
        'Debug.Print("ob_Chargen_Produziert.ExportChargen TW-Nr " & TWNr.ToString)

        'Lesen Chargen-Kopfdaten (Anzahl der Datensätze begrenzt auf LIMIT)
        wbdaten = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
        'Sicherheitshalber werden die Daten von der letzten Tageswechselnummer minus der Anzahl der Segmente gelesen (Fehler bei Fonk)
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlExportChargen, TWNr - MaxLinienSegmente, LIMIT)

        'Datensätze aus Tabelle BAK_ArbRezepte lesen
        If wbdaten.sqlSelect(sql) Then
            If wbdaten.Read Then

                'Schleife über alle Chargen bis alle Datensätze eingelesen sind
                Do
                    'Datenzeile dekodieren und zur Liste hinzufügen
                    opw_Zeile = AddtoListe(wbdaten.MySqlRead, WinBackChargenNummer)

                    'Der letzte Datensatz war ein Produktions-Artikel
                    If opw_Zeile.SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                        'Chargen-Nummer merken
                        WinBackChargenNummer = opw_Zeile.WinBackChargenNummer
                        'Datensatz Rohstoff-Verbrauch wird zusätzlich angelegt
                        AddtoListe(wbdaten.MySqlRead, WinBackChargenNummer)
                    End If

                Loop While wbdaten.MySqlRead.Read
                wbdaten.CloseRead()

                'Liste der leeren Chargen markieren
                For Each o As ob_ProduzierteWare In wpl_Liste
                    MarkChargenKopf(wbdaten, o.TWNr, o.WinBackChargenNummer)
                Next

                'Liste der produzierten Chargen abarbeiten
                Dim i As Integer = 0
                WinBackChargenNummer = ""
                'Datenbank-Verbindung mssql öffnen
                Dim OrgasoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)

                'Chargendaten in dbo.ProduzierteWare speichern
                For Each o As ob_ProduzierteWare In opw_Liste

                    'Wenn die Liste komplett voll ist (LIMIT 1000 Datensätze) dann
                    'wird vor Ende der Liste vor dem nächsten Artikel-Datensatz abgebrochen
                    'Die weiteren Produktionsdaten werden im nächsten Durchlauf abgearbeitet
                    i += 1
                    If opw_Liste.Count >= LIMIT And i > LIMIT - 100 Then
                        If o.SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                            Exit For
                        End If
                    End If

                    'Datensatz in dbo.Produzierte Ware schreiben
                    WriteOK = SqlWriteProdWare(OrgasoftMain, o)
                    'Schreiben ist fehlgeschlagen (Artikel nicht vorhanden oder Halbfertigprodukt)
                    If Not WriteOK And o.SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then

                        'Halbfertig-Produkt (Einheit kg)
                        o.Einheit = wb_Global.wbEinheitKilogramm
                        WriteOK = SqlWriteProdWare(OrgasoftMain, o)
                        'Schreiben ist fehlgeschlagen (Artikel nicht vorhanden)
                        If Not WriteOK And o.SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then

                            'Dummy-Artikel-Nummer
                            o.ArtikelNr = wb_GlobalSettings.ProduktionDummyArtikel
                            o.Einheit = wb_Global.wbEinheitStk
                            'nochmal mit Dummy-Artikel versuchen
                            WriteOK = SqlWriteProdWare(OrgasoftMain, o)

                            'Insert in dbo.Produzierte Ware war nicht erfolgreich - Fehler-Log
                            If Not WriteOK Then
                                err_Liste.Add("@E_Fehler beim Schreiben in dbo.ProduzierteWare TW-Nr/Artikel/Charge " & o.TWNr & "/" & o.ArtikelNr & "/" & o.ChargenNummer)
                            End If
                            '    Trace.WriteLine("@E_Fehler beim Schreiben in dbo.ProduzierteWare TW-Nr/Artikel/Charge " & o.TWNr & "/" & o.ArtikelNr & "/" & o.ChargenNummer)
                            'End If
                        End If
                    End If

                    'Datensatz in wbdaten als exportiert markieren
                    If o.SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                        MarkChargenKopf(wbdaten, o.TWNr, o.WinBackChargenNummer)
                        'Chargen- und Tageswechselnummer merken (Markieren der Chargen in wbdaten)
                        WinBackChargenNummer = o.WinBackChargenNummer
                        TageswechselNr = o.TWNr
                    End If

                Next
                'letzten Datensatz auch noch markieren
                MarkChargenKopf(wbdaten, TageswechselNr, WinBackChargenNummer)

                'Datenbank-Verbindung dbo.ProduzierteWare wieder schliessen
                OrgasoftMain.Close()

            End If
        End If

        'Datenbank-Verbindung sicherheitshalber nochmals schliessen
        wbdaten.Close()
        'Letzte gültige Tageswechsel-Nummer (darf aber nicht kleiner sein als der Aufrufparameter, sonst läuft die Routine rückwärts)
        'Debug.Print("Ende ob_Chargen_Produziert.ExportChargen TageswechselNr/TW-Nr " & TageswechselNr.ToString & "/" & TWNr.ToString)
        Return Math.Max(TageswechselNr, TWNr)
    End Function

    Private Function AddtoListe(Reader As MySqlDataReader, ByRef ChargenNummer As String) As ob_ProduzierteWare
        'OrgaBack - ProduzierteWare
        Dim opw As New ob_ProduzierteWare(ChargenNummer)
        'Datensatz aus wbdaten lesen
        opw.MySQLdbRead_Chargen(Reader)

        'Debug-Ausgabe
        'Debug.Write("opw " & opw.TWNr & " " & opw.WinBackChargenNummer & " " & opw.ArtikelNr & "/" & opw.Menge & " " & opw.Unit)

        'nur Zeilen mit Sollwerten sind für die Verbrauchsdaten relevant
        '2018-07-10 Datensätze mit Produktions-Datum 00010101 werden ignoriert (nicht gestartete Chargen)
        '2021-11-23 Datensätze mit Komponenten-Type Wasser werden ignoriert (Problem Einheit Liter/Kilogramm)
        If wb_Functions.TypeIstSollMenge(opw.Type, opw.ParamNr) AndAlso opw.Gestartet AndAlso opw.Type <> wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE AndAlso opw.Type <> wb_Global.KomponTypen.KO_TYPE_EISKOMPONENTE Then
            'zur Liste hinzufügen
            opw_Liste.Add(opw)
            'Debug.WriteLine(" Add to List")
        Else
            'zur Liste der nicht gestarteten Chargen hinzufügen (wird auch als exportiert markiert)
            If opw.SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                wpl_Liste.Add(opw)
                'Debug.WriteLine(" ---------- ")
            End If
        End If
        Return opw
    End Function

    ''' <summary>
    ''' Schreibt einen Datensatz aus ob_ProduzierteWare in die Tabelle dbo.ProduzierteWare
    ''' Die Datenbank-Verbindung muss geöffnet sein.
    ''' 
    ''' INSERT INTO [dbo].[ProduzierteWare] 
    '''     lfdNr                   Laufende Nummer (wird in der DB automatisch generiert - DB.Tabelle Update erforderlich !!)
    '''     FilialNr                Produktions-Filiale
    '''     ProduktionsDatum
    '''     SatzTyp
    '''     ArtikelNr
    '''     Einheit
    '''     Farbe
    '''     Groesse
    '''     Menge
    '''     ChargenNr
    '''     HaltbarkeitsDatum
    '''     ProduktionsUhrzeit
    ''' </summary>
    ''' <param name="db"></param>
    ''' <param name="o"></param>
    ''' <returns></returns>
    Private Function SqlWriteProdWare(db As wb_Sql, o As ob_ProduzierteWare) As Boolean
        'Datensatz in dbo.Produzierte Ware schreiben
        'Trace.Write("@I_SatzTyp/ChargenNummer/ArtikelNr/Menge Einheit " & o.SatzTyp & " " & o.ChargenNummer & " " & o.ArtikelNr & " " & o.Menge & " " & o.Unit)

        'Der SQL-INSERT-Befehl wird dynamisch erzeugt 
        'TODO Wenn in WinBack zur Anmeldung RFID-Chips verwendet werden, sollte auch der aktuelle User mit übertragen werden
        'Dim sql As String = o.sFilialNummer & ", '" & o.sProduktionsDatum & "', '" & o.sSatzTyp & "', '" & o.ArtikelNr & "', " & o.Unit & ", " &
        '                    o.Color & ", '" & o.Size & "', '" & o.sMenge & "', '" & o.ChargenNummer & "', '" & o.sHaltbarkeitsDatum & "'"

        'Der SQL-INSERT-Befehl wird dynamisch erzeugt '(ab Version 1.10.0.0 wird auch die Uhrzeit mit übergeben)
        Dim sql As String = o.sFilialNummer & ", '" & o.sProduktionsDatum & "', '" & o.sProduktionsUhrzeit & "', '" & o.sSatzTyp & "', '" & o.ArtikelNr & "', " & o.Unit & ", " &
                            o.Color & ", '" & o.Size & "', '" & o.sMenge & "', '" & o.ChargenNummer & "', '" & o.sHaltbarkeitsDatum & "'"
        'Insert ausführen - bei sql-Fehler wird keine Exception ausgelöst(Debug)
        If db.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlInsertProduktionsDaten, sql), False, False) < 0 Then
            'Rückgabewert kleiner Null - Fehler
            'Trace.WriteLine(" FEHLER")
            Return False
        Else
            'Result Insert OK
            'Trace.WriteLine(" OK")
            Return True
        End If
    End Function

    ''' <summary>
    ''' Markiert alle exportierten Chargendaten als bearbeitet.
    ''' In der Tabelle BAK_ArbRezepte wird das Feld B_ARZ_Status auf EXP gesetzt
    ''' </summary>
    ''' <param name="TWNr"></param>
    ''' <param name="ChargenNummer"></param>
    ''' <returns></returns>
    Private Function MarkChargenKopf(wb As wb_Sql, TWNr As Integer, ChargenNummer As String) As Boolean
        If ChargenNummer <> "" Then
            'Markiere alle Chargen mit TW-Nummer
            Return wb.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlMarkChargen, TWNr, ChargenNummer))
        Else
            Return False
        End If
    End Function


End Class
