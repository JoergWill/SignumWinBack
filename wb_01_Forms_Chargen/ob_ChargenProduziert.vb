Imports MySql.Data.MySqlClient

''' <summary>
''' Rückmeldung der produzierten Chargen an OrgaBack
''' Schreibt alle zum Zeitpunkt x produzierten Artikel in die Tabelle dbo.ProduzierteWare
''' Alle Rohstoffe die verbraucht worden sind werden in die Tabelle dbo.ProduzierteWare mit SatzTyp=V geschrieben
''' 
''' Nach dem Schreiben der Daten werden die Artikel und Rohstoffe in WinBack mit wbdaten.BAK_ArbRezepte.B_ARZ_Status = Exp gekennzeichnet
''' </summary>
Public Class ob_ChargenProduziert

    Private opw_Liste As New ArrayList
    Private Const LIMIT = 1000
    Private ProdDatumGueltig As DateTime

    ''' <summary>
    ''' Alle Chargen vor dem Stichtag werden als ungültig deklariert und nicht an OrgaBack zurückgemeldet
    ''' (Hauptsächlich nicht produzierte Chargen)
    ''' </summary>
    Public Sub New()
        ProdDatumGueltig = Convert.ToDateTime("22.11.1964")
    End Sub



    ''' <summary>
    ''' Exportiert die einzelnen Produktions-Chargen-Daten ab der vorgegebenen 
    ''' Tageswechselnummer in dbo.ProduzierteWare. Zurückgegeben wird 
    ''' die letzte ausgegebene Tageswechsel-Nr.
    ''' 
    ''' Es wird immer zuerst der Chargen-Kopf und danach die verbrauchten Rohstoffe ausgegeben
    ''' </summary>
    ''' <param name="TWNr"></param>
    ''' <returns></returns>
    Public Function ExportChargen(TWNr As Integer) As Integer
        Dim sql As String
        Dim wbdaten As wb_Sql
        Dim ChargenNummer As String = ""
        Dim TageswechselNr As Long = wb_Global.UNDEFINED
        Dim opw_Zeile As New ob_ProduzierteWare(ChargenNummer)

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
                    opw_Zeile = AddtoListe(wbdaten.MySqlRead, ChargenNummer)

                    'Der letzte Datensatz war ein Produktions-Artikel
                    If opw_Zeile.SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                        'Chargen-Nummer merken
                        ChargenNummer = opw_Zeile.ChargenNummer
                        'Datensatz Rohstoff-Verbrauch wird zusätzlich angelegt
                        AddtoListe(wbdaten.MySqlRead, ChargenNummer)
                    End If

                Loop While wbdaten.MySqlRead.Read
                wbdaten.CloseRead()

                'Liste der produzierten Chargen abarbeiten
                Dim i As Integer = 0
                ChargenNummer = ""
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
                    If Not SqlWriteProdWare(OrgasoftMain, o) Then
                        'Insert in dbo.Produzierte Ware war nicht erfolgreich - Fehler-Log
                        Trace.WriteLine("Fehler beim Schreiben in dbo.ProduzierteWare TW-Nr/Artikel/Charge " & o.TWNr & "/" & o.ArtikelNr & "/" & o.ChargenNummer)
                    End If

                    'Datensatz in wbdaten als exportiert markieren
                    If o.SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                        MarkChargenKopf(wbdaten, TageswechselNr, ChargenNummer)
                        'Chargen- und Tageswechselnummer merken (Markieren der Chargen in wbdaten)
                        ChargenNummer = o.ChargenNummer
                        TageswechselNr = o.TWNr
                    End If

                Next
                'letzten Datensatz auch noch markieren
                MarkChargenKopf(wbdaten, TageswechselNr, ChargenNummer)

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

        'nur Zeilen mit Sollwerten sind für die Verbrauchsdaten relevant
        '2018-07-10 Datensätze mit Produktions-Datum 00010101 werden ignoriert (nicht gestartete Chargen)
        If wb_Functions.TypeIstSollMenge(opw.Type, opw.ParamNr) And ProduktionDatumGueltig(opw.ProduktionsDatum) Then
            'zur Liste hinzufügen
            opw_Liste.Add(opw)
        End If
        Return opw
    End Function

    ''' <summary>
    ''' Prüft auf gültiges Produktions-Datum
    ''' (Chargen wurde produziert)
    ''' </summary>
    ''' <param name="d"></param>
    ''' <returns></returns>
    Private Function ProduktionDatumGueltig(d As DateTime) As Boolean
        If d < ProdDatumGueltig Then
            Return False
        Else
            Return True
        End If
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
        Debug.Print("SatzTyp/ChargenNummer/ArtikelNr/Menge Einheit " & o.SatzTyp & " " & o.ChargenNummer & " " & o.ArtikelNr & " " & o.Menge & " " & o.Unit)

        'Der SQL-INSERT-Befehl wird dynamisch erzeugt
        Dim sql As String = o.sFilialNummer & ", '" & o.sProduktionsDatum & "', '" & o.sSatzTyp & "', '" & o.ArtikelNr & "', " & o.Unit & ", " &
                            o.Color & ", '" & o.Size & "', '" & o.sMenge & "', '" & o.ChargenNummer & "', '" & o.sHaltbarkeitsDatum & "'"
        'Insert ausführen
        If db.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlInsertProduktionsDaten, sql)) < 0 Then
            ' Rückgabewert kleiner Null - Fehler
            Return False
        Else
            'Result Insert OK
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
