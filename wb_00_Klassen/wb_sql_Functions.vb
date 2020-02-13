Imports MySql.Data.MySqlClient
Imports WinBack.wb_Sql_Selects

''' <summary>
''' Sammlung von Statischen SQL-Funktionen
''' </summary>
Public Class wb_sql_Functions

    ''' <summary>
    ''' MySql-Ping. Verbindung zur Datenbank öffnen und einen Ping absenden.
    ''' Wenn die Verbindung funktioniert wird True zurückgegeben.
    ''' </summary>
    ''' <returns>True - Wenn die Verbindung zur Datenbank funktioniert
    ''' False - Wenn keine Verbindung zur Datenbank aufgebaut werden kann</returns>
    Public Shared Function ping() As Boolean

        Select Case wb_GlobalSettings.WinBackDBType
            Case wb_Sql.dbType.mySql
                Dim MySqlCon As New MySqlConnection(wb_GlobalSettings.SqlConWinBack)
                Try
                    MySqlCon.Open()
                    If MySqlCon.Ping Then
                        MySqlCon.Close()
                        MySqlCon.Dispose()
                        Return True
                    Else
                        MySqlCon.Close()
                        MySqlCon.Dispose()
                        Return False
                    End If
                Catch ex As Exception
                    MySqlCon.Close()
                    MySqlCon.Dispose()
                    Return False
                End Try

            Case wb_Sql.dbType.msSql
                'Dim msCon As New SqlConnection(My.Settings.OrgaBackMainConString)
                Return True
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>
    ''' Wandelt einen Double-Wert in einen String. Dezimal-Komma werden ersatzt durch Dezimal-Punkte
    ''' </summary>
    ''' <param name="d"></param>
    ''' <returns></returns>
    Public Shared Function MsDoubleToString(d As Double) As String
        Return d.ToString.Replace(",", ".")
    End Function

    ''' <summary>
    ''' Wandelt date(time) in MySQL-DateTime im Format YYYY-MM-DD HH:MM:SS um
    ''' </summary>
    ''' <param name="d">date</param>
    ''' <returns>String - im MySQL-DateTime-Format</returns>
    Public Shared Function MySQLdatetime(d As Date) As String
        Return d.ToString("yyyy-MM-dd HH:mm:ss")
    End Function

    ''' <summary>
    ''' Wandelt date(time) in SQL-DateTime im Format YYYYMMDD um
    ''' </summary>
    ''' <param name="d">date</param>
    ''' <returns>String - im MySQL-DateTime-Format</returns>
    Public Shared Function MsSQLShortDate(d As Date) As String
        Return d.ToString("yyyyMMdd")
    End Function

    ''' <summary>
    ''' Wandelt MySQLDateTime in Microsoft-Date um. Wenn die Konvertierung fehl schlägt, wird
    ''' 22.11.1964 zurückgegeben.
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    Public Shared Function MySQLDateTimeToDate(Value As Object) As DateTime
        If TryCast(Value, MySql.Data.Types.MySqlDateTime).Year > 0 Then
            Try
                Return TryCast(Value, MySql.Data.Types.MySqlDateTime).GetDateTime()
            Catch
                Return wb_Global.wbNODATE
            End Try
        End If
        Return wb_Global.wbNODATE
    End Function

    ''' <summary>
    ''' Wandelt einen SQL-Datenfeld in Boolean um
    '''     1   -   True
    '''     0   -   False
    '''   NULL  -   False
    ''' </summary>
    ''' <param name="s">String - Wert</param>
    ''' <returns>Boolean - Result</returns>
    Public Shared Function MySQLBoolean(s As String) As Boolean
        If s = "1" Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Prüft ob eine Tabelle in der WinBack-Datenbank existiert.
    ''' </summary>
    ''' <param name="TableName"></param>
    ''' <returns></returns>
    Public Shared Function MySQLTableExist(TableName As String) As Boolean
        MySQLTableExist = False

        Dim Table As String
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        'Prüfen ob die Tabelle winback.ENummern existiert
        If winback.sqlSelect(wb_Sql_Selects.sqlCheckTables) Then
            While winback.Read
                Table = winback.Item(0).ToString
                If Table.ToLower() = TableName.ToLower() Then
                    MySQLTableExist = True
                    Exit While
                End If
            End While
        End If
        winback.Close()
    End Function

    Public Shared Function ReaderItem(ByRef sqlReader As MySqlDataReader, FieldName As String) As String
        If sqlReader.Item(FieldName) = "" Then
            Return sqlReader.Item(FieldName)
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' Liest der Parameter-Wert aus der Tabelle KomponParams aus.
    ''' Wenn der Wert oder Datensatz nicht existiert, wird ein leeren String zurückgegeben.
    ''' </summary>
    ''' <param name="KomponentenNummer">Integer - Komponenten-Nummer(intern)</param>
    ''' <param name="ParameterNummer">Integer - Parameter-Nummer</param>
    ''' <returns></returns>
    Public Shared Function getKomponParam(KomponentenNummer As Integer, ParameterNummer As Integer, Optional DefaultWert As String = "") As String
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)

        'Daten aus winback.KomponParams in String einlesen
        winback.sqlSelect(setParams(sqlKompParams, KomponentenNummer, ParameterNummer))
        If winback.Read Then
            getKomponParam = winback.sField("KP_Wert")
        Else
            getKomponParam = DefaultWert
        End If
        winback.Close()
    End Function

    ''' <summary>
    ''' Update Parameter-Wert in der Tabelle Kompon-Params.
    ''' </summary>
    ''' <param name="KomponentenNummer"></param>
    ''' <param name="ParameterNummer"></param>
    ''' <param name="Wert"></param>
    ''' <returns></returns>
    Public Shared Function setKomponParam(KomponentenNummer As Integer, ParameterNummer As Integer, Wert As String) As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        'Update-Statement wird dynamisch erzeugt (REPLACE INTO KomponParams)
        Dim Count As Integer = winback.sqlCommand(setParams(sqlUpdateKompParams, KomponentenNummer, ParameterNummer, Wert))
        winback.Close()
        Return (Count >= 0)
    End Function

    ''' <summary>
    ''' Ermittelt die nächste freie interne Komponenten-Nummer (KO_Nr) aus der Tabelle Komponenten
    ''' </summary>
    ''' <returns>Integer - nächste freie Komponenten-Nummer</returns>
    Public Shared Function getNewKomponNummer() As Integer
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'Max-Wert KO-Nr aus Tabelle Komponenten ermitteln
        winback.sqlSelect(sqlMaxKompNummer)
        If winback.Read Then
            getNewKomponNummer = winback.iField("MAX(KO_Nr)") + 1
        Else
            getNewKomponNummer = 1
        End If
        winback.Close()
    End Function

    ''' <summary>
    ''' Ermittelt die nächste freie interne Rezept-Nummer (RZ_Nr) aus der Tabelle Rezepte
    ''' </summary>
    ''' <returns>Integer - nächste freie Rezept-Nummer</returns>
    Public Shared Function getNewRezeptNummer() As Integer
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'Max-Wert KO-Nr aus Tabelle Komponenten ermitteln
        winback.sqlSelect(sqlMaxRzNummer)
        If winback.Read Then
            getNewRezeptNummer = winback.iField("MAX(RZ_Nr)") + 1
        Else
            getNewRezeptNummer = 1
        End If
        winback.Close()
    End Function

    ''' <summary>
    ''' Gibt eine Liste von (internen) Komponenten-Nummern zu einer Artikel/Rohstoff-Nummer zurück.
    ''' Einer alphanumerischen Rohstoff-Nummer können mehrere (interne) Komponenten-Nummern zugeordnet sein:
    '''     - Sauerteig/Produktion
    '''     - Mehl in mehreren Silo's
    '''     - Hand/Sackmehl zu Silo-Mehl
    ''' </summary>
    ''' <param name="AlNum"></param>
    ''' <returns>List of Integer</returns>
    Public Shared Function getKONrFromAlNum(AlNum As String) As List(Of Integer)
        'Interne Komponenten-Nummer
        Dim KO_Nr As New List(Of Integer)
        'Datenbank Verbindung
        Dim winBack As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Sql-Statement Abfrage KONr
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSelectKomp_AlNum, AlNum)
        'Select-Statement ausführen
        Try
            winBack.sqlSelect(sql)
            While winBack.Read
                KO_Nr.Add(winBack.iField("KO_Nr"))
            End While
        Catch ex As Exception
        End Try
        'Datenbank-Verbindung wieder freigeben
        winBack.Close()

        'Wenn ein/mehrere gültiger Datensatz gefunden wurde - KO_Nr zurückgeben (unabhängig von KO_Type)
        Return KO_Nr
    End Function


    Public Shared Function Lookup(Tabelle As String, Feldname As String, Bedingung As String) As String
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String = "SELECT " & Feldname & " FROM " & Tabelle & " WHERE " & Bedingung
        Lookup = ""

        'Select-Statement ausführen
        Try
            winback.sqlSelect(sql)
            If winback.Read Then
                Lookup = winback.sField(Feldname)
            End If
        Catch ex As Exception
        End Try

        winback.Close()
        Return Lookup
    End Function

End Class
