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
