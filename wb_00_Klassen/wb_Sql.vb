Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

''' <summary>
''' Kapselt die Zugriffe auf die Datenbank
''' wahlweise MySQl(winback) oder MSSQL(OrgasoftMain)
''' </summary>
Public Class wb_Sql

    Dim MySqlCon As New MySqlConnection
    Dim MySqlCommand As New MySqlCommand
    Public MySqlRead As MySqlDataReader

    Dim msCon As New SqlConnection
    Dim msCommand As New SqlCommand
    Public msRead As SqlDataReader

    ''' <summary>
    ''' WinBack-Datenbank-Type (mysql/Microsoft-SQL)
    ''' </summary>
    Enum dbType
        undef
        mySql
        msSql
    End Enum
    Dim _conType As dbType = dbType.undef

    ''' <summary>
    ''' WinBack-Datenbanken
    ''' </summary>
    Enum dbTable
        winback
        wbdaten
    End Enum

    ''' <summary>
    ''' Datenbank-Type
    ''' </summary>
    ''' <returns>MySql/MSSql</returns>
    ReadOnly Property conType As dbType
        Get
            Return _conType
        End Get
    End Property

    ''' <summary>
    ''' Einen Datensatz lesen
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property Read As Boolean
        Get
            Try
                Select Case conType
            'Verbindung über mySql
                    Case dbType.mySql
                        Return MySqlRead.Read
            'Verbindung über msSql
                    Case dbType.msSql
                        Return msRead.Read
                    Case Else
                        Return False
                End Select
            Catch ex As Exception
                Debug.Print("Problem beim Read der Daten Fehler-Meldung: " & ex.Message.ToString)
                Return False
            End Try
        End Get
    End Property

    ''' <summary>
    ''' Datenbankfeld als String auslesen
    ''' </summary>
    ''' <param name="FieldName">DB-Field-Name</param>
    ''' <returns>String Datenfeld-Inhalt</returns>
    ReadOnly Property sField(FieldName As String) As String
        Get
            Try
                Select Case conType
            'Verbindung über mySql
                    Case dbType.mySql
                        Return MySqlRead(FieldName).ToString
            'Verbindung über mysSql
                    Case dbType.msSql
                        Return msRead(FieldName).ToString
                    Case Else
                        Return ""
                End Select
            Catch
                Return "STRING-FIELD NOT FOUND " & FieldName
            End Try
        End Get
    End Property

    ''' <summary>
    ''' Datenbankfeld als Datum auslesen
    ''' </summary>
    ''' <param name="FieldName"></param>
    ''' <returns></returns>
    Public ReadOnly Property dField(FieldName As String) As Date
        Get
            Try
                Select Case conType
            'Verbindung über mySql
                    Case dbType.mySql
                        Return MySqlRead(FieldName)
            'Verbindung über mysSql
                    Case dbType.msSql
                        Return Convert.ToDateTime(msRead(FieldName).ToString)
                    Case Else
                        Return ""
                End Select
            Catch
                Return #1964/11/22 12:00:00#
            End Try
        End Get
    End Property

    ''' <summary>
    ''' Datenbankfeld als Integer auslesen
    ''' </summary>
    ''' <param name="FieldName">DB-Field-Name</param>
    ''' <returns>Integer Datenfeld-Inhalt</returns>
    ReadOnly Property iField(FieldName As String) As Integer
        Get
            Try
                Select Case conType
            'Verbindung über mySql
                    Case dbType.mySql
                        Return Int(MySqlRead(FieldName).ToString)
            'Verbindung über msSql
                    Case dbType.msSql
                        Return Int(msRead(FieldName).ToString)
                    Case Else
                        Return 0
                End Select
            Catch
                Return 0
            End Try
        End Get
    End Property

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="ConString">Verbindungs-String</param>
    ''' <param name="db">DB-Type (MySQL/MSsql)</param>
    Sub New(ConString As String, db As dbType)
        'Datenbank-Verbindung schliessen falls noch offen
        Close()
        Try
            'Datenbank-Verbindung abhängig von der Datenbank-Type öffnen
            Select Case db

            'Verbindung über mySql
                Case dbType.mySql
                    'Verbindung zur MySQL-Datenbank
                    _conType = dbType.mySql
                    MySqlCon = New MySqlConnection(ConString)
                    MySqlCon.Open()

            ' Verbindung über msSQL
                Case dbType.msSql
                    _conType = dbType.msSql
                    msCon = New SqlConnection(ConString)
                    msCon.Open()

            End Select
        Catch ex As Exception
            MsgBox("Connection Error: " & ex.Message.ToString)
        End Try
    End Sub

    ''' <summary>
    '''SQL-Select-Statement ausführen
    ''' </summary>
    ''' <param name="sql">Select-Statement</param>
    ''' <returns>False im Fehlerfall</returns>
    Function sqlSelect(sql As String) As Boolean
        'sql-Select ausführen
        Try
            Select Case conType
                Case dbType.mySql
                    'sql-Kommando ausführen
                    MySqlCommand = New MySqlCommand(sql, MySqlCon)
                    MySqlRead = MySqlCommand.ExecuteReader()
                    Return True
                Case dbType.msSql
                    'sql-Kommando ausführen
                    msCommand = New SqlCommand(sql, msCon)
                    msRead = msCommand.ExecuteReader()
                    Return True
                Case Else
                    Return False
            End Select
        Catch ex As Exception
            MsgBox("Problem beim Laden der Daten aus DB: SQL= " & sql & Chr(10) & "Fehler-Meldung: " & ex.Message.ToString)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' SQL-Kommando ausführen
    ''' </summary>
    ''' <param name="sql">SQL-Kommando</param>
    ''' <returns>Anzahl der Datensätze
    ''' -1 falls ein Fehler aufgetreten ist</returns>
    Function sqlCommand(sql As String) As Integer
        Try
            Select Case conType
                Case dbType.mySql
                    'sql-Kommando ausführen
                    MySqlCommand = New MySqlCommand(sql, MySqlCon)
                    MySqlCommand.CommandText = sql
                    Return MySqlCommand.ExecuteNonQuery()
                Case dbType.msSql
                    msCommand = New SqlCommand(sql, msCon)
                    msCommand.CommandText = sql
                    Return msCommand.ExecuteNonQuery()
                Case Else
                    Return -1
            End Select
        Catch ex As Exception
            MsgBox("Fehler " & ex.Message.ToString & Chr(10) & "bei SQL-Kommando: " & sql)
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' SQL-Read beenden
    ''' </summary>
    Sub CloseRead()
        Select Case conType
            Case dbType.mySql
                MySqlRead.Close()
            Case dbType.msSql
                msRead.Close()
        End Select
    End Sub

    ''' <summary>
    '''Datenbank-Verbindung schliessen
    ''' </summary>
    Sub Close()
        Try
            Select Case conType
                Case dbType.mySql
                    MySqlCon.Close()
                Case dbType.msSql
                    msCon.Close()
            End Select
        Catch
        End Try
    End Sub
End Class
