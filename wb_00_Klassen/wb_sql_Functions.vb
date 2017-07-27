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

        Select Case My.Settings.WinBackDBType
            Case wb_Sql.dbType.mySql
                Dim MySqlCon As New MySqlConnection(My.Settings.WinBackConString)
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
    ''' Entfernt alle störenden Sonderzeichen aus einem (sql)String
    ''' </summary>
    ''' <param name="s">String mit Sonderzeichen</param>
    ''' <returns>s - String mit umgewandelten Sonderzeichen</returns>
    Public Shared Function removeSonderZeichen(s As String) As String
        'wandelt ' in ''
        Try
            s = s.Replace("'", "''")
        Catch
        End Try
        Return s
    End Function

    ''' <summary>
    ''' Wandelt date(time) in MySQL-DateTime im Format YYY-MM-DD HH:MM:SS um
    ''' </summary>
    ''' <param name="d">date</param>
    ''' <returns>String - im MySQL-DateTime-Format</returns>
    Public Shared Function MySQLdatetime(d As Date) As String
        Return d.ToString("yyyy-MM-dd HH:mm:ss")
    End Function

    ''' <summary>
    ''' Wandelt einen SQL-Datenfeld in Boolean um
    '''     1   -   True
    '''     0   -   False
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
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)

        'Daten aus winback.KomponParams in String einlesen
        winback.sqlSelect(setParams(sqlKompParams, KomponentenNummer, ParameterNummer))
        If winback.Read Then
            getKomponParam = winback.sField("KP_Wert")
            Debug.Print("TA " & getKomponParam.ToString)
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
        Dim winback = New wb_Sql(wb_Konfig.SqlConWinBack, wb_Sql.dbType.mySql)

        'Max-Wert KO-Nr aus Tabelle Komponenten ermitteln
        winback.sqlSelect(sqlMaxKompNummer)
        If winback.Read Then
            getNewKomponNummer = winback.iField("MAX(KO_Nr)") + 1
        Else
            getNewKomponNummer = 1
        End If
        winback.Close()
    End Function

End Class
