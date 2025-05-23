﻿Imports Microsoft.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports WinBack

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

    Public Structure StoredProceduresParameter
        Public Parameter As String
        Public Value As String
    End Structure

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

    ReadOnly Property FieldCount As Integer
        Get
            Select Case conType
                Case dbType.mySql
                    Return MySqlRead.FieldCount
                Case dbType.msSql
                    Return msRead.FieldCount
                Case Else
                    Return wb_Global.UNDEFINED
            End Select
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

    ReadOnly Property Item(Index As Integer) As Object
        Get
            Try
                Select Case conType
                    Case dbType.mySql
                        Return MySqlRead.Item(Index)
                    Case Else
                        Return Nothing
                End Select
            Catch ex As Exception
                Trace.WriteLine("@E_ITEM(Index) NOT FOUND " & Index)
                Throw New System.Exception("ITEM(Index) NOT FOUND " & Index)
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
            'Verbindung über msSql
                    Case dbType.msSql
                        Return msRead(FieldName).ToString
                    Case Else
                        Return ""
                End Select
            Catch
                Trace.WriteLine("@E_STRING-FIELD NOT FOUND " & FieldName)
                Throw New System.Exception("STRING-FIELD NOT FOUND " & FieldName)
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
            'Verbindung über msSql
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
    ReadOnly Property iField(FieldName As String, Optional i As Integer = 0) As Integer
        Get
            Try
                Select Case conType
            'Verbindung über mySql
                    Case dbType.mySql
                        Dim s = MySqlRead(FieldName).ToString
                        If s IsNot Nothing AndAlso s <> "" Then
                            Return Int(s)
                        Else
                            Return i
                        End If
            'Verbindung über msSql
                    Case dbType.msSql
                        Dim s = msRead(FieldName).ToString
                        If s IsNot Nothing AndAlso s <> "" Then
                            Return Int(s)
                        Else
                            Return i
                        End If
                    Case Else
                        Return i
                End Select
            Catch
                Return i
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
            Dim Stack As New StackTrace
            If (Not Stack.ToString.Contains("WinBackServerTask") OrElse Debugger.IsAttached) AndAlso Not (wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest) Then
                'TODO bei Fehler sollte die Möglichkeit bestehen, die IP-Adresse oder die WinBack.ini auszuwählen
                MsgBox("Connection Error: " & ex.Message.ToString & vbCrLf & "ConnectionString: " & ConString)
            Else
                Trace.WriteLine("@E_Connection Error: " & ex.Message.ToString & vbCrLf & "ConnectionString: " & ConString)
            End If
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
            Dim Stack As New StackTrace
            If (Not Stack.ToString.Contains("WinBackServerTask") OrElse Debugger.IsAttached) AndAlso Not (wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest) Then
                MsgBox("Problem beim Laden der Daten aus DB: SQL= " & sql & Chr(10) & "Fehler-Meldung: " & ex.Message.ToString)
            Else
                Trace.WriteLine("@E_Problem beim Laden der Daten aus DB: SQL= " & sql & Chr(10) & "Fehler-Meldung: " & ex.Message.ToString)
            End If
            Return False
        End Try
    End Function

    ''' <summary>
    '''SQL-ExecuteScalar Statement ausführen
    ''' </summary>
    ''' <param name="sql">Select-Statement</param>
    ''' <returns>False im Fehlerfall</returns>
    Function sqlExecuteScalar(sql As String, Optional Params() As SqlParameter = Nothing) As Object
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
                    'Parameter (falls erforderlich)
                    If Params IsNot Nothing Then
                        For i = 0 To Params.Length - 1
                            msCommand.Parameters.Add(Params(i))
                        Next
                    End If
                    'Kommando ausführen
                    Return msCommand.ExecuteScalar
                Case Else
                    Return False
            End Select
        Catch ex As Exception
            Dim Stack As New StackTrace
            If (Not Stack.ToString.Contains("WinBackServerTask") OrElse Debugger.IsAttached) AndAlso Not (wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest) Then
                MsgBox("Problem beim Laden der Daten aus DB: SQL= " & sql & Chr(10) & "Fehler-Meldung: " & ex.Message.ToString)
            Else
                Trace.WriteLine("@E_Problem beim Laden der Daten aus DB: SQL= " & sql & Chr(10) & "Fehler-Meldung: " & ex.Message.ToString)
            End If
            Return False
        End Try
    End Function

    ''' <summary>
    ''' SQL-Kommando ausführen
    ''' </summary>
    ''' <param name="sql">SQL-Kommando</param>
    ''' <returns>Anzahl der Datensätze
    ''' -1 falls ein Fehler aufgetreten ist</returns>
    Function sqlCommand(sql As String, Optional ShowException As Boolean = True, Optional WriteTraceExeption As Boolean = True, Optional Timeout As Integer = 0) As Integer
        If sql <> "" Then
            Try
                Select Case conType
                    Case dbType.mySql
                        'MySql-Kommando ausführen
                        MySqlCommand = New MySqlCommand(sql, MySqlCon) With {.CommandText = sql}
                        'Falls das Kommando länger dauert - Timeout definieren
                        If Timeout > 0 Then
                            MySqlCommand.CommandTimeout = Timeout
                        End If
                        'SQL-Kommando ausführen
                        Return MySqlCommand.ExecuteNonQuery()
                    Case dbType.msSql
                        'MsSql-Kommando ausführen
                        msCommand = New SqlCommand(sql, msCon) With {.CommandText = sql}
                        'Falls das Kommando länger dauert - Timeout definieren
                        If Timeout > 0 Then
                            msCommand.CommandTimeout = Timeout
                        End If
                        'SQL-Kommando ausführen
                        Return msCommand.ExecuteNonQuery()
                    Case Else
                        Return -1
                End Select
            Catch ex As Exception
                Dim Stack As New StackTrace
                If (Not Stack.ToString.Contains("WinBackServerTask") OrElse Debugger.IsAttached) AndAlso Not (wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest) AndAlso ShowException Then
                    MsgBox("Fehler " & ex.Message.ToString & Chr(10) & "bei SQL-Kommando: " & sql)
                Else
                    If WriteTraceExeption Then
                        Trace.WriteLine("@E_Fehler " & ex.Message.ToString & Chr(10) & "bei SQL-Kommando: " & sql)
                    End If
                End If
                Return -1
            End Try
        Else
            Return 0
        End If
    End Function

    Function sqlExecStoredProcedure(sql As String, Parameter As Array) As Boolean
        'sql-Stored Procedure ausführen
        Try
            Select Case conType
                Case dbType.mySql
                    Return False
                Case dbType.msSql
                    'sql-Kommando ausführen
                    msCommand = New SqlCommand(sql, msCon) With {.CommandType = Data.CommandType.StoredProcedure}
                    'Parameter for Stored-Procedure
                    For Each p As StoredProceduresParameter In Parameter
                        msCommand.Parameters.AddWithValue(p.Parameter, p.Value)
                    Next
                    msRead = msCommand.ExecuteReader
                    Return True
                Case Else
                    Return False
            End Select
        Catch ex As Exception
            Dim Stack As New StackTrace
            If (Not Stack.ToString.Contains("WinBackServerTask") OrElse Debugger.IsAttached) AndAlso Not (wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest) Then
                MsgBox("Problem beim Laden der Daten aus DB: SQL= " & sql & Chr(10) & "Fehler-Meldung: " & ex.Message.ToString)
                Return False
            Else
                Trace.WriteLine("@E_" & ex.Message.ToString & Chr(10) & "bei SQL-Kommando: " & sql)
                Return False
            End If
        End Try
    End Function

    ''' <summary>
    ''' SQL-Read beenden
    ''' </summary>
    Sub CloseRead()
        Select Case conType
            Case dbType.mySql
                Try
                    If MySqlRead IsNot Nothing Then
                        MySqlRead.Close()
                    End If
                Catch
                End Try
            Case dbType.msSql
                Try
                    If msRead IsNot Nothing Then
                        msRead.Close()
                    End If
                Catch
                End Try
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
