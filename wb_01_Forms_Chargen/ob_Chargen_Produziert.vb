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
    Private Const LIMIT = 100

    ''' <summary>
    ''' Alle Chargen vor dem Stichtag werden als ungültig deklariert und nicht an OrgaBack zurückgemeldet
    ''' (Hauptsächlich nicht produzierte Chargen)
    ''' </summary>
    Public Sub New()
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
    ''' </summary>
    ''' <param name="TWNr"></param>
    ''' <returns></returns>
    Public Function ExportChargen(TWNr As Integer) As Integer
        Dim sql As String
        Dim WriteOK As Boolean = False
        Dim wbdaten As wb_Sql
        Dim WinBackChargenNummer As String = ""
        Dim TageswechselNr As Long = TWNr
        Dim opw_Zeile As New ob_ProduzierteWare(WinBackChargenNummer)

        'Liste löschen
        opw_Liste.Clear()

        'Lesen Chargen-Kopfdaten (Anzahl der Datensätze begrenzt auf LIMIT)
        wbdaten = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlExportChargen, TWNr, LIMIT)

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
                    'Schreiben ist fehlgeschlagen (Artikel nicht vorhanden)
                    If Not WriteOK And o.SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                        'Dummy-Artikel-Nummer
                        o.ArtikelNr = wb_Global.ProduktionDummyArtikel
                        'nochmal mit Dummy-Artikel versuchen
                        WriteOK = SqlWriteProdWare(OrgasoftMain, o)
                        'Insert in dbo.Produzierte Ware war nicht erfolgreich - Fehler-Log
                        If Not WriteOK Then
                            Trace.WriteLine("Fehler beim Schreiben in dbo.ProduzierteWare TW-Nr/Artikel/Charge " & o.TWNr & "/" & o.ArtikelNr & "/" & o.ChargenNummer)
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
        'Letzte gültige Tageswechsel-Nummer
        Return TageswechselNr
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
        If wb_Functions.TypeIstSollMenge(opw.Type, opw.ParamNr) And opw.Gestartet Then
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
    ''' </summary>
    ''' <param name="db"></param>
    ''' <param name="o"></param>
    ''' <returns></returns>
    Private Function SqlWriteProdWare(db As wb_Sql, o As ob_ProduzierteWare) As Boolean
        'Datensatz in dbo.Produzierte Ware schreiben
        Trace.Write("SatzTyp/ChargenNummer/ArtikelNr/Menge Einheit " & o.SatzTyp & " " & o.ChargenNummer & " " & o.ArtikelNr & " " & o.Menge & " " & o.Unit)

        'Der SQL-INSERT-Befehl wird dynamisch erzeugt
        Dim sql As String = o.sFilialNummer & ", '" & o.sProduktionsDatum & "', '" & o.sSatzTyp & "', '" & o.ArtikelNr & "', " & o.Unit & ", " &
                            o.Color & ", '" & o.Size & "', '" & o.sMenge & "', '" & o.ChargenNummer & "', '" & o.sHaltbarkeitsDatum & "'"
        'Insert ausführen - bei sql-Fehler wird keine Exception ausgelöst(Debug)
        If db.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlInsertProduktionsDaten, sql), False) < 0 Then
            ' Rückgabewert kleiner Null - Fehler
            Trace.WriteLine(" FEHLER")
            Return False
        Else
            'Result Insert OK
            Trace.WriteLine(" OK")
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
