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

    ''' <summary>
    ''' Exportiert die einzelnen Produktions-Chargen-Daten ab der vorgegebenen 
    ''' Tageswechselnummer in dbo.ProduzierteWare. Zurückgegeben wird 
    ''' die letzte ausgegebene Tageswechsel-Nr.
    ''' 
    ''' Es wird immer zuerst der Chargen-Kopf und danach die verbrauchten Rohstoffe ausgegeben
    ''' </summary>
    ''' <param name="TWNr"></param>
    ''' <returns></returns>
    Public Function ExportChargen(TWNr As Integer) As String
        Dim sql As String
        Dim wbdaten As wb_Sql
        Dim ChargenNummer As String = ""
        Dim opw_Zeile As New ob_ProduzierteWare(ChargenNummer)

        'Liste löschen
        opw_Liste.Clear()

        'Lesen Chargen-Kopftdaten
        wbdaten = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlExportChargen, TWNr)

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
                wbdaten.Close()

                Dim i As Integer = 0
                'Chargendaten speichern
                For Each o As ob_ProduzierteWare In opw_Liste
                    i += 1
                    'Wenn die Liste komplett voll ist (LIMIT 1000 Datensätze) dann
                    'wird vor Ende der Liste vor dem nächsten Artikel-Datensatz abgebrochen
                    'Die weiteren Produktionsdaten werden im nächsten Durchlauf abgearbeitet
                    If opw_Liste.Count >= 100 And i > 10 Then
                        If o.SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                            Exit For
                        End If
                    End If

                    Debug.Print("SatzTyp/ChargenNummer/ArtikelNr/Menge Einheit " & o.SatzTyp & " " & o.ChargenNummer & " " & o.ArtikelNr & " " & o.Menge & " " & o.Unit)
                Next

            End If
        End If
        wbdaten.Close()
        Return opw_Zeile.ChargenNummer
    End Function

    Private Function AddtoListe(Reader As MySqlDataReader, ByRef ChargenNummer As String) As ob_ProduzierteWare
        'OrgaBack - ProduzierteWare
        Dim opw As New ob_ProduzierteWare(ChargenNummer)
        'Datensatz aus wbdaten lesen
        opw.MySQLdbRead_Chargen(Reader)

        'nur Zeilen mit Sollwerten sind für die Verbrauchsdaten relevant
        If wb_Functions.TypeIstSollMenge(opw.Type, opw.ParamNr) Then
            'zur Liste hinzufügen
            opw_Liste.Add(opw)
        End If
        Return opw
    End Function

    ''' <summary>
    ''' Markiert alle exportierten Chargendaten als bearbeitet.
    ''' In der Tabelle BAK_ArbRezepte wird das Feld B_ARZ_Status auf EXP gesetzt
    ''' </summary>
    ''' <param name="TWNr"></param>
    ''' <returns></returns>
    Public Function MarkChargenKopf(TWNr As Integer) As Boolean

    End Function


End Class
