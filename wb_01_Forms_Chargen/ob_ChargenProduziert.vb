Imports MySql.Data.MySqlClient
''' <summary>
''' Rückmeldung der produzierten Chargen an OrgaBack
''' Schreibt alle zum Zeitpunkt x produzierten Artikel in die Tabelle dbo.ProduzierteWare
''' Alle Rohstoffe die verbraucht worden sind werden in die Tabelle dbo.ProduzierteWare mit SatzTyp=V geschrieben
''' 
''' Nach dem Schreiben der Daten werden die Artikel und Rohstoffe in WinBack mit wbdaten.BAK_ArbRezepte.B_ARZ_Status = Exp gekennzeichnet
''' </summary>
Public Class ob_ChargenProduziert

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

        'Lesen Chargen-Kopftdaten
        wbdaten = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlExportChargen, TWNr)

        'Datensätze aus Tabelle BAK_ArbRezepte lesen
        If wbdaten.sqlSelect(sql) Then
            If wbdaten.Read Then
                MySQLdbRead_Chargen(wbdaten.MySqlRead)
                wbdaten.Close()
                Return True
            End If
        End If
        wbdaten.Close()
    End Function

    ''' <summary>
    ''' Einlesen aller Datenfelder aus der Datenbank wbdaten in ob_ProduzierteWare
    ''' </summary>
    ''' <param name="sqlReader"></param>
    ''' <returns></returns>
    Private Function MySQLdbRead_Chargen(ByRef sqlReader As MySqlDataReader) As Boolean

        'Schleife über alle Chargen bis alle Datensätze eingelesen sind
        Do
            'Chargendaten - Anzahl der Felder im DataSet
            For i = 0 To sqlReader.FieldCount - 1
                MySQLdbRead_Fields(sqlReader.GetName(i), sqlReader.GetValue(i))
            Next

            'Chargendaten speichern
            'TODO Array oder direkt in die Datenbank

        Loop While sqlReader.Read
        Return True
    End Function

    ''' <summary>
    ''' Aufteilen des SQL-Resultset nach Spalten-Namen auf die Objekt-Eigenschaften
    ''' </summary>
    ''' <param name="Name">String - Spalten-Name aus Datenbank</param>
    ''' <param name="Value">Object - Wert aus Datenbank</param>
    ''' <returns></returns>
    Private Function MySQLdbRead_Fields(Name As String, Value As Object)
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Debug
        Debug.Print("Feld/Value " & Name & "/" & Value.ToString)

        'Feldname aus der Datenbank
        Try
            Select Case Name

                Case Else
                    Debug.Print("Field-Name " & Name & " wird nicht ausgewertet")
            End Select
        Catch ex As Exception
        End Try
        Return True
    End Function





    ''' <summary>
    ''' Markiert alle exportierten Chargendaten als bearbeitet.
    ''' </summary>
    ''' <param name="TWNr"></param>
    ''' <returns></returns>
    Public Function MarkChargenKopf(TWNr As Integer) As Boolean

    End Function


End Class
