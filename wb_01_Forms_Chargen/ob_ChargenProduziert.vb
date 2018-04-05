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
    Public Function ExportChargen(TWNr As Integer) As Integer
        Dim sql As String
        Dim wbdaten As wb_Sql
        Dim ChargenNummer As String = ""

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
                    Dim opw As New ob_ProduzierteWare(ChargenNummer)
                    opw.MySQLdbRead_Chargen(wbdaten.MySqlRead)
                    opw_Liste.Add(opw)

                    'Der letzte Datensatz war ein Produktions-Artikel
                    If opw.SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                        'Chargen-Nummer merken
                        ChargenNummer = opw.ChargenNummer

                        'Datensatz Rohstoff-Verbrauch wird zusätzlich angelegt
                        Dim opw_ As New ob_ProduzierteWare(ChargenNummer)
                        opw_.MySQLdbRead_Chargen(wbdaten.MySqlRead)
                        opw_Liste.Add(opw_)

                    End If
                Loop While wbdaten.MySqlRead.Read
                wbdaten.Close()

                'Chargendaten speichern
                For Each o As ob_ProduzierteWare In opw_Liste
                    Debug.Print("SatzTyp/ChargenNummer/ArtikelNr/Menge Einheit" & o.SatzTyp & o.ChargenNummer & o.ArtikelNr & o.Menge & o.Unit)
                Next

                Return True
            End If
        End If
        wbdaten.Close()
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
