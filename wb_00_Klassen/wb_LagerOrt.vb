Imports MySql.Data.MySqlClient

Public Class wb_LagerOrt
    Private _Bilanzmenge As String = Nothing
    Private _Mindestmenge As String = Nothing
    Private _Lagerort As String = Nothing
    Private _Kommentar As String = ""
    Private _Aktiv As String = "A"
    Private Shared _ErrorText As String = ""
    Private Shared _TabelleLagerorteOK = False


    Public Shared ReadOnly Property ErrorText As String
        Get
            Return _ErrorText
        End Get
    End Property

    Public Shared ReadOnly Property UpdateDatabaseFile As String
        Get
            Return "2.30_Lagerorte.sql"
        End Get
    End Property

    Public Shared ReadOnly Property CheckDB() As Boolean
        Get
            'Prüfen ob ein Udpdate der Lagerorte-Tabelle erforderlich ist
            Check_DBFelder()
            Return _TabelleLagerorteOK
        End Get
    End Property

    Public Property Bilanzmenge As String
        Get
            If _Lagerort = "" Or _Lagerort Is Nothing Then
                _Bilanzmenge = "0"
            End If
            Return wb_Functions.FormatStr(_Bilanzmenge, 3)
        End Get
        Set(value As String)
            _Bilanzmenge = value
            'Änderung der Bilanzmenge in MySql-Datenbank schreiben
            MySQLdbUpdate()
        End Set
    End Property

    Public Property Mindestmenge As String
        Get
            Return wb_Functions.FormatStr(_Mindestmenge, 3)
        End Get
        Set(value As String)
            _Mindestmenge = value
            'Änderung der Mindestmenge in MySql-Datenbank schreiben
            MySQLdbUpdate()
        End Set
    End Property

    Public ReadOnly Property MindestmengeUnterschritten As Boolean
        Get
            'Umwandeln in Double
            'TODO für weitere Berechnungen (Produktionsplanung) als Property zur Verfügung stellen
            Dim dBilanzmenge As Double = wb_Functions.StrToDouble(Bilanzmenge)
            Dim dMindestmenge As Double = wb_Functions.StrToDouble(Mindestmenge)

            'simpler Vergleich
            Return (dBilanzmenge < dMindestmenge) And (dMindestmenge > 0)
        End Get
    End Property

    ''' <summary>
    ''' Flag Rohstoff Aktiv/Hand/Deaktiviert
    ''' Bei Silo-Umschaltung ist nur ein Rohstoff aktiv, der/die anderen sind deaktiviert
    ''' </summary>
    ''' <returns></returns>
    Public Property Aktiv As String
        Get
            Return _Aktiv
        End Get
        Set(value As String)
            _Aktiv = value
            'Änderung Flag Aktiv/Hand/Aus in MySql-Datenbank schreiben
            MySQLdbUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Kommentarfeld zum Lagerort. Wird in WinBack-Produktion im Materialfenster angezeigt.
    ''' Enthält den gleichen Wert wie das Kommentarfeld zum Rohstoff.
    ''' </summary>
    ''' <returns></returns>
    Public Property Kommentar As String
        Get
            Return _Kommentar
        End Get
        Set(value As String)
            _Kommentar = value
            'Änderung Kommentarfeld MySql-Datenbank schreiben
            MySQLdbUpdate()
        End Set
    End Property

    Public Sub New(Lagerort As String)
        'Initialisierung
        _Bilanzmenge = Nothing
        _Mindestmenge = Nothing

        'Daten aus winback.LagerOrte lesen
        If Lagerort <> "" Then
            If MysqldbRead(Lagerort) Then
                _Lagerort = Lagerort
            End If
        End If
    End Sub

    Public Function MysqldbRead(LagerOrt As String) As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String
        'Suche nach dem ersten Datensatz mit dieser Komponenten-Type
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlLagerOrte, LagerOrt)

        'ersten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                MySQLdbRead(winback.MySqlRead)
                winback.Close()
                Return True
            End If
        End If
        winback.Close()
        Return False
    End Function

    ''' <summary>
    ''' Liest alle Datenfelder aus dem aktuellen Datensatz in das Komponenten-Objekt
    ''' Die Daten werden anhand der Feldbezeichnung in die einzelnen Properties eingetragen.
    ''' 
    ''' Das letzte Datenfeld ist der TimeStamp und wird NICHT eingelesen, da es Probleme mit
    ''' der Konvertierung von MySQLDateTime in DateTime gibt
    ''' (https://bugs.mysql.com/bug.php?id=87120)
    ''' </summary>
    ''' <param name="sqlReader"></param>
    ''' <returns>True wenn kein Fehler aufgetreten ist</returns>
    Public Function MySQLdbRead(ByRef sqlReader As MySqlDataReader) As Boolean
        'Stammdaten - Anzahl der Felder im DataSet
        'FieldCount-2 unterdrückt das Feld TimeStamp
        For i = 0 To sqlReader.FieldCount - 2
            Try
                MySQLdbRead_StammDaten(sqlReader.GetName(i), sqlReader.GetValue(i))
            Catch ex As Exception
                Debug.Print("Exception MySQLdbReadStammdaten" & ex.Message)
                Return False
            End Try
        Next

        Return True
    End Function

    ''' <summary>
    ''' Schreibt den Wert aus Value in die entprechende Property der Klasse. Der Feldname bestimmt das Ziel
    ''' 
    ''' winback.Lagerorte
    '''     LG_Ort
    '''     LG_Bezeichnung
    '''     LG_Weg_Nr
    '''     LG_TempFuehler_Nr
    '''     LG_Bilanzmenge
    '''     LG_Silo_Nr
    '''     LG_aktiv
    '''     LG_Status
    '''     LG_Kommentar
    '''     LG_Befuell_varianten
    '''     LG_Mindestmenge
    '''     LG_max_Dosierfehler
    '''     LG_LF_Nr
    '''     LG_Timestamp
    '''     
    ''' </summary>
    ''' <param name="Name">String - Bezeichnung Datenbankfeld</param>
    ''' <param name="Value">Object - Wert Datenbankfeld(Inhalt)</param>
    ''' <returns></returns>
    Private Function MySQLdbRead_StammDaten(Name As String, Value As Object) As Boolean
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Feldname aus der Datenbank
        'Debug.Print("ReadStammdaten " & Name & "/" & Value)
        Try
            Select Case Name

                'Bilanzmenge
                Case "LG_Bilanzmenge"
                    _Bilanzmenge = Value
               'Mindestmenge
                Case "LG_Mindestmenge"
                    _Mindestmenge = Value
                'Kommentar
                Case "LG_Kommentar"
                    _Kommentar = Value
                'Aktiv/Hand
                Case "LG_aktiv"
                    _Aktiv = Value
            End Select

        Catch ex As Exception
        End Try
        Return True
    End Function


    ''' <summary>
    ''' schreibt alle Datenfelder aus dem LagerOrte-Objekt mit dem angegebenen Lagerort in die Datenbank.
    ''' </summary>
    ''' <returns></returns>
    Public Function MySQLdbUpdate() As Boolean
        If _Lagerort IsNot Nothing And _Lagerort <> "" Then
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            Dim sql As String

            'Update-Statement wird dynamisch erzeugt    
            sql = "LG_Mindestmenge = '" & Mindestmenge & "', LG_Bilanzmenge = '" & Bilanzmenge & "', LG_Kommentar = '" & Kommentar & "', LG_Aktiv = '" & Aktiv & "'"

            'Update ausführen
            'Debug.Print("Lagerorte.MysqldbUpdate " & sql)

            If winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateLagerOrte, _Lagerort, sql)) Then
                winback.Close()
                Return True
            End If

            'Datenbank-Verbindung wieder schliessen
            winback.Close()
        End If
        Return False
    End Function

    ''' <summary>
    ''' Prüft ob das Datenbankfeld winback.Komponenten.KA_zaehlt_zu_NWT_Gesamtmenge vorhanden ist.
    ''' Wenn nicht. MUSS die Datenbank per Update-Script erweitert werden!
    ''' </summary>
    Private Shared Sub Check_DBFelder()
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        'Prüfen ob Datenbankfeld LG_LF_Nummer vorhanden ist und die richtige Größe hat
        If winback.sqlSelect(wb_Sql_Selects.sqlCheck_LG_LF_Nr) Then
            If winback.Read Then
                Dim FieldDesc As String = winback.sField("Type")
                If FieldDesc.Contains("(11)") Then
                    _TabelleLagerorteOK = True
                Else
                    _ErrorText = "Tabelle WinBack.Lagerorte muss erweitert werden! (LG_LF_Nummer muss INTEGER(11) sein!)"
                    _TabelleLagerorteOK = False
                End If
            End If
        End If
        winback.Close()
    End Sub


End Class

