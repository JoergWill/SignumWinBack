Imports MySql.Data.MySqlClient
Imports System.Data
Imports Microsoft.Data.SqlClient
Imports WinBack
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
    ''' Wandelt einen Double-Wert in einen String. Dezimal-Komma werden ereatzt durch Dezimal-Punkte
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
    ''' Wandelt date(time) in SQL-Time im Format HHMM um
    ''' </summary>
    ''' <param name="d">date</param>
    ''' <returns>String - im MySQL-Time-Format</returns>
    Public Shared Function MsSQLShortTime(d As Date) As String
        Return d.ToString("HH:mm")
    End Function


    ''' <summary>
    ''' Umrechnung der OrgaBack-Einheiten bezogen auf die Artikel-Nummer.
    ''' Liefert den Umrechnungsfaktor von einer Einheit in die andere für eine spezielle Artikelnummer.
    ''' (z.B. Sack in kg)
    ''' </summary>
    ''' <param name="ArtikelNr"></param>
    ''' <param name="VonEinheit"></param>
    ''' <param name="InEinheit"></param>
    ''' <returns></returns>
    Public Shared Function EinheitenUmrechnung(ArtikelNr As String, VonEinheit As Integer, InEinheit As Integer) As Double
        'Datenbankverbindung öffnen MsSQL
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        Dim pl(2) As Microsoft.Data.SqlClient.SqlParameter

        'Artikelnummer
        pl(0) = New Microsoft.Data.SqlClient.SqlParameter
        pl(0).ParameterName = "ArtikelNr"
        pl(0).SqlDbType = SqlDbType.Text
        pl(0).Value = ArtikelNr

        'VonEinheit
        pl(1) = New Microsoft.Data.SqlClient.SqlParameter
        pl(1).ParameterName = "VonEinheit"
        pl(1).SqlDbType = SqlDbType.Int
        pl(1).Value = VonEinheit

        'InEinheit
        pl(2) = New Microsoft.Data.SqlClient.SqlParameter
        pl(2).ParameterName = "InEinheit"
        pl(2).SqlDbType = SqlDbType.Int
        pl(2).Value = InEinheit

        'Stored Procedure ausführen
        Dim Result As Object = orgaback.sqlExecuteScalar("SELECT [dbo].[EinheitenUmrechnung](@ArtikelNr, @VonEinheit, @InEinheit) AS Faktor", pl)
        'Datenbankverbindung wieder schliessen
        orgaback.Close()

        'Ergebnis prüfen (Artikel vorhanden)
        If Result IsNot Nothing Then
            Return wb_Functions.StrToDouble(Result.ToString)
        Else
            Return 1.0
        End If
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
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S3385:""Exit"" statements should not be used", Justification:="<Ausstehend>")>
    Public Shared Function MySQLTableExist(TableName As String) As Boolean
        Dim Result As Boolean = False

        Dim Table As String
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        'Prüfen ob die Tabelle existiert
        If winback.sqlSelect(wb_Sql_Selects.sqlCheckTables) Then
            While winback.Read
                Table = winback.Item(0).ToString
                If Table.ToLower() = TableName.ToLower() Then
                    Result = True
                    Exit While
                End If
            End While
        End If
        winback.Close()
        Return Result
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
        Dim Result As String = DefaultWert

        'Daten aus winback.KomponParams in String einlesen
        winback.sqlSelect(setParams(sqlKompParams, KomponentenNummer, ParameterNummer))
        If winback.Read Then
            Result = winback.sField("KP_Wert")
        End If
        winback.Close()
        Return Result
    End Function

    ''' <summary>
    ''' Update Parameter-Wert in der Tabelle KomponParams.
    ''' </summary>
    ''' <param name="KomponentenNummer"></param>
    ''' <param name="ParameterNummer"></param>
    ''' <param name="Wert"></param>
    ''' <returns></returns>
    Public Shared Function setKomponParam(KomponentenNummer As Integer, ParameterNummer As Integer, Wert As String, Kommentar As String) As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        'Update-Statement wird dynamisch erzeugt (REPLACE INTO KomponParams)
        Dim Count As Integer = winback.sqlCommand(setParams(sqlUpdateKompParams, KomponentenNummer, ParameterNummer, Wert, Kommentar))
        winback.Close()
        Return (Count >= 0)
    End Function

    ''' <summary>
    ''' Ermittelt die nächste frei Tageswechsel-Nummer aus der Tabelle wbdaten.Tageswechsel
    ''' Legt gleich einen neuen Datensatz mit dieser TagesWechsel-Nummer an. Damit ist die TW-
    ''' Nummer gesichert und blockiert!
    ''' 
    ''' Die Datenverbindung bleibt offen und wird nicht geschlossen.
    ''' </summary>
    ''' <param name="wbdaten"></param>
    ''' <returns></returns>
    Friend Shared Function getNewTWNummer(wbdaten As wb_Sql, Linie As Integer, Start As DateTime, Ende As DateTime) As Integer
        'Max-Wert KO-Nr aus Tabelle Komponenten ermitteln
        wbdaten.sqlSelect(sqlMaxTWNummer)
        If wbdaten.Read Then
            'TODO was passiert, wenn für diese Linie schon eine offene TW-Nr exisitiert (TW_Begin definiert, TW_Ende = NULL)
            Dim TWNr As Integer = wbdaten.iField("MAX(TW_Nr)") + 1
            wbdaten.CloseRead()
            wbdaten.sqlCommand(setParams(sqlInsertTWNummer, TWNr, Linie, MySQLdatetime(Start), MySQLdatetime(Ende)))
            Return TWNr
        Else
            Return wb_Global.UNDEFINED
        End If
    End Function

    ''' <summary>
    ''' Ermittelt die nächste freie interne Komponenten-Nummer (KO_Nr) aus der Tabelle Komponenten
    ''' </summary>
    ''' <returns>Integer - nächste freie Komponenten-Nummer</returns>
    Public Shared Function getNewKomponNummer() As Integer
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim Result As Integer = 1

        'Max-Wert KO-Nr aus Tabelle Komponenten ermitteln
        winback.sqlSelect(sqlMaxKompNummer)
        If winback.Read Then
            Result = winback.iField("MAX(KO_Nr)") + 1
        End If
        winback.Close()
        Return Result
    End Function

    ''' <summary>
    ''' Ermittelt die nächste freie interne Rezept-Nummer (RZ_Nr) aus der Tabelle Rezepte
    ''' </summary>
    ''' <returns>Integer - nächste freie Rezept-Nummer</returns>
    Public Shared Function getNewRezeptNummer() As Integer
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim Result As Integer = 1

        'Max-Wert KO-Nr aus Tabelle Komponenten ermitteln
        winback.sqlSelect(sqlMaxRzNummer)
        If winback.Read Then
            Result = winback.iField("MAX(RZ_Nr)") + 1
        End If
        winback.Close()
        Return Result
    End Function

    ''' <summary>
    ''' Ermittelt die letzte Änderungs-Nummer zum Rezept aus der Tabelle Rezepte
    ''' </summary>
    ''' <param name="RzNr"></param>
    ''' <returns></returns>
    Public Shared Function getLastAenderungsIndex(RzNr As Integer) As Integer
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim Result As Integer = 0

        'Max-Wert KO-Nr aus Tabelle Komponenten ermitteln
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlMaxRzAenderung, RzNr.ToString))
        If winback.Read Then
            Result = winback.iField("MAX(RZ_Aenderung_Nr)")
        End If
        winback.Close()
        Return Result
    End Function

    ''' <summary>
    ''' Ermittelt die nächste freie interne Textbaustein-Nummer (IP_Lfd_Nr) aus der Tabelle ItemParameter
    ''' </summary>
    ''' <returns>Integer - nächste freie Textbaustein-Nummer</returns>
    Public Shared Function getNewProdStufeTextNr(TextBausteinType) As Integer
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim Result As Integer = 1

        'Max-Wert KO-Nr aus Tabelle Komponenten ermitteln
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlMaxTextBausteinNr, TextBausteinType))
        If winback.Read Then
            Result = winback.iField("MAX(IP_Lfd_Nr)") + 1
        End If
        winback.Close()
        Return Result
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
        Dim Result As String = ""

        'Select-Statement ausführen
        Try
            winback.sqlSelect(sql)
            If winback.Read Then
                Result = winback.sField(Feldname)
            End If
        Catch ex As Exception
        End Try

        winback.Close()
        Return Result
    End Function

    Public Shared Function GetMySqlVersion() As String
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim Result As String = ""

        'Version ermitteln
        winback.sqlSelect("SELECT VERSION()")
        If winback.Read Then
            Result = winback.sField("VERSION()")
        End If
        winback.Close()
        Return Result
    End Function

End Class
