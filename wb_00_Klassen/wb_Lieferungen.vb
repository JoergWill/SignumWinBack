Imports MySql.Data.MySqlClient
Imports WinBack.wb_Global
Imports WinBack.wb_Functions
Imports WinBack

Public Class wb_Lieferungen
    Private KO_Nr As Integer
    Private KO_Type As KomponTypen
    Private KO_Nr_AlNum As String
    Private KO_Bezeichnung As String
    Private LG_Ort As String
    Private LG_Bilanzmenge As String
    Private LF_Nr As Integer = wb_Global.UNDEFINED
    Private LG_LF_Nr As Integer = wb_Global.UNDEFINED

    Public Property Nr As Integer
        Set(value As Integer)
            KO_Nr = value
        End Set
        Get
            Return KO_Nr
        End Get
    End Property

    Public ReadOnly Property Type As KomponTypen
        Get
            Return KO_Type
        End Get
    End Property

    Public ReadOnly Property Bezeichnung As String
        Get
            Return KO_Bezeichnung
        End Get
    End Property

    ''' <summary>
    ''' Rohstoff/Artikel-Nummer (alpha-numerisch)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Nummer As String
        Get
            Return KO_Nr_AlNum
        End Get
    End Property

    Public Property Bilanzmenge As String
        Get
            Return LG_Bilanzmenge
        End Get
        Set(value As String)
            LG_Bilanzmenge = value
        End Set
    End Property

    Public ReadOnly Property LagerOrt As String
        Get
            Return LG_Ort
        End Get
    End Property

    Public Property OrgaBack_LfdNr As Integer
        Get
            Return LG_LF_Nr
        End Get
        Set(value As Integer)
            LG_LF_Nr = value
        End Set
    End Property

    ''' <summary>
    ''' Abhängig vom Vorfallkürzel wird der aktuelle Datensatz aus dbo.ArtikelLagerkarte in winback.Lieferungen verbucht:
    '''     
    '''     WE  -   Wareneingang
    '''     WBA -   WinBack Produktion automatische Abbuchung
    '''     
    ''' </summary>
    ''' <param name="winback"></param>
    Public Sub Verbuchen(winback As wb_Sql, LagerKarte As wb_LagerKarte)
        Select Case LagerKarte.Vorfall
            Case "WE"
                'Wareneingang in winback.Lieferungen verbuchen
                Verbuchen_WE(winback, LagerKarte, wb_GlobalSettings.RohChargen_ErfassungAktiv)



        End Select

    End Sub

    ''' <summary>
    ''' Fügt einen neuen Datensatz (Wareneingang) an die Tabelle Lieferungen an. 
    ''' 
    '''     LF_LG_Ort           winback.Komponenten.LG_Ort
    '''     LF_Nr               dbo.ArtikelLagerkarte.Lfd
    '''     LF_Datum            dbo.ArtikelLagerkarte.Datum + dbo.ArtikelLagerkarte.Uhrzeit  
    '''     LF_Menge            dbo.ArtikelLagerkarte.Menge
    '''     LF_Lieferant
    '''     LF_gebucht          1 (neue Lieferung)
    '''     LF_Bemerkung        dbo.ArtikelLagerkarte.VorfallKürzel + dbo.ArtikelLagerkarte.Modul 
    '''     LF_Lager            0 (Produktion)
    '''     LF_BF_Charge        dbo.ArtikelLagerkarte.ChargenNummer
    '''     LF_Liniengruppe     NULL
    '''     LF_BF_Frist         NULL
    '''     LF_Verbrauch        0
    '''     LF_User_Nr          dbo.ArtikelLagerkarte.Mitarbeiter
    '''     LF_Preis            dbo.ArtikelLagerkarte.Preis
    '''     LF_PreisEinheit     0
    '''     LF_Timestamp
    '''     TODO Chargenerfassungs-Variante beachten !!
    ''' 
    ''' </summary>
    ''' <param name="winback"></param>
    ''' <param name="LagerKarte"></param>
    ''' <param name="Chargenerfassung"></param>
    Private Sub Verbuchen_WE(winback As wb_Sql, LagerKarte As wb_LagerKarte, Chargenerfassung As Boolean)
        'der INSERT-Befehl wird dynamisch erzeugt
        Dim sql_insert As String = "'" & LG_Ort & "', " & LagerKarte.Lfd & ", '" & LagerKarte.Datum & " " & LagerKarte.Uhrzeit & "', " &
                                   "'" & LagerKarte.Menge & "', " & vbNull & ", '1', '" & LagerKarte.Vorfall & "-" & LagerKarte.Modul & "', " &
                                   "0" & ", '" & LagerKarte.ChargenNummer & "', " & vbNull & ", " & vbNull & ", " & "0" & ", " &
                                   "0" & ", '" & LagerKarte.Preis & "', " & "0"

        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlInsertWE, sql_insert)
        'INSERT ausführen
        winback.sqlCommand(sql)

    End Sub

    ''' <summary>
    ''' Setzt die laufende Nummer (Lagerorte.LG_LF_Nr) auf den letzten verarbeiteten Eintrag aus OrgaBack.
    ''' Bei der nächsten Abfrage werden dann nur neue, nicht verarbeitete Einträge aus OrgaBack gelesen.
    ''' </summary>
    ''' <param name="winback"></param>
    Public Sub UpdateLagerorteLfd(winback As wb_Sql, lfd As Integer)
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateLagerort, lfd, LG_Ort)
        winback.sqlCommand(sql)
    End Sub

    ''' <summary>
    ''' Liest alle Datenfelder aus dem aktuellen Datensatz in das Lieferungen-Objekt
    ''' Die Daten werden anhand der Feldbezeichnung in die einzelnen Properties eingetragen.
    ''' 
    ''' Das letzte Datenfeld ist der TimeStamp und wird NICHT eingelesen, da es Probleme mit
    ''' der Konvertierung von MySQLDateTime in DateTime gibt
    ''' (https://bugs.mysql.com/bug.php?id=87120)
    ''' </summary>
    ''' <param name="sqlReader"></param>
    ''' <returns>True wenn kein Fehler aufgetreten ist</returns>
    Public Function MySQLdbRead(ByRef sqlReader As MySqlDataReader) As Boolean
        'Schleife über alle Datensätze
        Do
            'Parameter - Anzahl der Felder im DataSet
            'FieldCount-2 unterdrückt das Feld TimeStamp
            For i = 0 To sqlReader.FieldCount - 2
                Try
                    MySQLdbRead_Daten(sqlReader.GetName(i), sqlReader.GetValue(i))
                Catch ex As Exception
                    Debug.Print("Exception MySQLdbRead " & sqlReader.GetName(i))
                End Try
            Next
        Loop While sqlReader.Read
        Return True
    End Function

    ''' <summary>
    ''' Schreibt den Wert aus Value in die entprechende Property der Klasse. Der Feldname bestimmt das Ziel
    ''' </summary>
    ''' <param name="Name">String - Bezeichnung Datenbankfeld</param>
    ''' <param name="Value">Object - Wert Datenbankfeld(Inhalt)</param>
    ''' <returns></returns>
    Private Function MySQLdbRead_Daten(Name As String, Value As Object) As Boolean
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Feldname aus der Datenbank
        Debug.Print("ReadLieferungen " & Name & "/" & Value)
        Try
            Select Case Name

                'Nummer(intern)
                Case "KO_Nr"
                    Nr = CInt(Value)
               'Type
                Case "KO_Type"
                    KO_Type = IntToKomponType(CInt(Value))
                'Bezeichnung
                Case "KO_Bezeichnung"
                    KO_Bezeichnung = wb_Functions.MySqlToUtf8(Value)
                'Nummer(alphanumerisch)
                Case "KO_Nr_AlNum"
                    KO_Nr_AlNum = Value

                'Laufende Nummer entspricht der Lfd in dbo.ArtikelLagerKarte
                'Achtung(!) muss von SMALLINT auf INT erweitert werden
                Case "LG_LF_Nr"
                    LG_LF_Nr = CInt(Value)
                'Lieferung-Nummer
                Case "LF_Nr"
                    LF_Nr = CInt(Value)
                'Lagerort
                Case "LG_Ort"
                    LG_Ort = Value
                'Bilanzmenge
                Case "LG_Bilanzmenge"
                    LG_Bilanzmenge = Value

            End Select

        Catch ex As Exception
        End Try
        Return True
    End Function

End Class

'LF_LG_Ort         
'LF_Nr                
'LF_Datum          
'LF_Menge
'LF_Lieferant
'LF_gebucht
'LF_Bemerkung
'LF_Lager
'LF_BF_Charge
'LF_Liniengruppe
'LF_BF_Frist
'LF_Verbrauch
'LF_User_Nr
'LF_Preis
'LF_PreisEinheit
'LF_Timestamp
