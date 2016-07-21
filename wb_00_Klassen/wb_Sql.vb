Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
'---------------------------------------------------------
'04.05.2016/ V0.9/JW            :Neuanlage
'Bearbeitet von                 :Will
'
'Änderungen:
'---------------------------------------------------------
'Beschreibung:
'Kapselt die Zugriffe auf die Datenbank
'wahlweise MySQl(winback) oder MSSQL(OrgasoftMain)
'---------------------------------------------------------

Public Class wb_Sql
    Enum dbType
        undef
        mySql
        msSql
    End Enum

    Enum dbTable
        winback
        wbdaten
    End Enum

    Dim _conType As dbType = dbType.undef

    Dim MySqlCon As New MySqlConnection
    Dim MySqlCommand As New MySqlCommand
    Dim MySqlRead As MySqlDataReader

    Dim msCon As New SqlConnection
    Dim msCommand As New SqlCommand
    Dim msRead As SqlDataReader

    'Datenbank-Type zrückgeben MySql/MSSql
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
        End Get
    End Property

    'Datenbankfeld als String auslesen
    ReadOnly Property sField(FieldName As String) As String
        Get
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
        End Get
    End Property

    'Datenbankfeld als Integer auslesen
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

    'SQL-Kommando ausführen
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

    'SQL-Read beenden
    Sub CloseRead()
        Select Case conType
            Case dbType.mySql
                MySqlRead.Close()
            Case dbType.msSql
                msRead.Close()
        End Select
    End Sub

    'Datenbank-Verbindung schliessen
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
