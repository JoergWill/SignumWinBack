Imports MySql.Data.MySqlClient
Imports WinBack

Public Class wb_TimerEvent
    Implements IComparable

    Private _sort As String
    Private _Task As String = ""
    Private _Kommentar As String = ""
    Private _Str1 As String = ""
    Private _Str2 As String = ""
    Private _Startzeit As DateTime
    Private _Endezeit As DateTime
    Private _Periode As Int64
    Private _Status As Integer
    Private _Running As Boolean = False
    Private _RunNow As Boolean = False

    ''' <summary>
    ''' Prüft ob der übergebene Task-Name übereinstimmt und ob der Event ausgelöst werden soll
    ''' Wenn die Startzeit kleiner als die aktuelle Uhrzeit ist wird True zurückgegeben und die
    ''' Startzeit neu berechnet.
    ''' </summary>
    ''' <param name="sTask"></param>
    ''' <returns></returns>
    Public Function Check(sTask As String) As Boolean
        If (sTask = _Task) And _Startzeit < Now And Status = wb_Global.wbAktionsTimerStatus.Enabled Then
            'Startzeit neu berechnen
            Startzeit = _Startzeit.AddSeconds(_Periode)
            'wenn die neue Startzeit in der Vergangenheit liegt wird neu berechnet
            If _Startzeit < Now Then
                Dim i As Integer = (Now - Startzeit).TotalSeconds / _Periode
                _Startzeit = _Startzeit.AddSeconds(_Periode * (i + 1))
            End If

            'Task auf aktiv setzen
            _Running = True
            _RunNow = False
            'Event auslösen
            Return True
        ElseIf (sTask = _Task) And RunNow Then
            'One-Shot
            _Running = True
            _RunNow = False
            'Event auslösen
            Return True
        Else
            Return False
        End If
    End Function

    Public Property Sort As String
        Get
            Return _sort
        End Get
        Set(value As String)
            _sort = value
        End Set
    End Property

    Public Property Task As String
        Get
            Return _Task
        End Get
        Set(value As String)
            _Task = value
        End Set
    End Property

    Public ReadOnly Property Bezeichnung As String
        Get
            If _Str1 = "" Then
                If _Kommentar = "" Then
                    Return _Task
                Else
                    Return _Kommentar
                End If
            Else
                Return _Str1
            End If
        End Get
    End Property

    Public Property Startzeit As DateTime
        Get
            Return _Startzeit
        End Get
        Set(value As Date)
            _Startzeit = value
        End Set
    End Property

    Public ReadOnly Property sStartzeit As String
        Get
            Return Startzeit.ToString("dd-MM-yy HH:mm")
        End Get
    End Property


    Public Property Periode As Long
        Get
            Return _Periode
        End Get
        Set(value As Long)
            _Periode = value
        End Set
    End Property

    ''' <summary>
    ''' Setzt das (interne)Flag _Running. 
    ''' Dient nur zur Anzeige der Task-Aktivität (Abfrage Status)
    ''' </summary>
    Public WriteOnly Property SetRunFlag As Boolean
        Set(value As Boolean)
            If value Then
                _Running = True
            Else
                _Running = False
            End If
        End Set
    End Property


    Public ReadOnly Property Status As wb_Global.wbAktionsTimerStatus
        Get
            If _Running Then
                Return wb_Global.wbAktionsTimerStatus.Running
            End If
            If _Status > 0 Then
                Return wb_Global.wbAktionsTimerStatus.Enabled
            Else
                Return wb_Global.wbAktionsTimerStatus.Disabled
            End If
        End Get
    End Property

    Public WriteOnly Property Aktiv As Boolean
        Set
            If Value Then
                _Status = 1
            Else
                _Status = 0
            End If
        End Set
    End Property

    Public Property Str2 As String
        Get
            Return _Str2
        End Get
        Set(value As String)
            _Str2 = value
        End Set
    End Property

    Public Property Endezeit As Date
        Get
            Return _Endezeit
        End Get
        Set(value As Date)
            _Endezeit = value
            _Running = False
        End Set
    End Property

    Public Property RunNow As Boolean
        Get
            Return _RunNow
        End Get
        Set(value As Boolean)
            _RunNow = value
        End Set
    End Property

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        Throw New NotImplementedException()
    End Function

    Public Function MySQLdbRead(ByRef sqlReader As MySqlDataReader) As Boolean
        'Stammdaten - Anzahl der Felder im DataSet
        'FieldCount-2 unterdrückt das Feld TimeStamp
        For i = 0 To sqlReader.FieldCount - 2
            Try
                MySQLdbRead_Fields(sqlReader.GetName(i), sqlReader.GetValue(i))
            Catch ex As Exception
                Return False
            End Try
        Next
        Return True
    End Function

    Private Function MySQLdbRead_Fields(Name As String, Value As Object) As Boolean
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Return False
        End If

        'Feldname aus der Datenbank
        'Debug.Print("Read AktionsTimer " & Name & " / " & Value)
        Try
            Select Case Name

            'Nummer(intern)
                Case "AT_idx"
                    Sort = Value

            'Bezeichnung(Task)
                Case "AT_Quelle_Typ"
                    Task = Value

            'Kommentar
                Case "AT_Kommentar"
                    _Kommentar = Value

            'Startzeit
                Case "AT_Startzeit"
                    Startzeit = WinBack.wb_sql_Functions.MySQLdatetime(Value)

            'Status
                Case "AT_Ziel_Aktion"
                    _Status = Value

            'String 1
                Case "AT_Str1"
                    _Str1 = Value

            'String 2
                Case "AT_Str2"
                    Str2 = Value

            'Periode in Minuten
                Case "AT_Periode"
                    Periode = Value

            End Select

        Catch
        End Try


        Return True
    End Function

    Public Function MySQLdbUpdate_Fields() As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        'sql-Kommando UPDATE bilden
        Dim sqlData As String = "AT_Startzeit = '" & wb_sql_Functions.MySQLdatetime(_Startzeit) &
                                "', AT_Ziel_Aktion = '" & _Status &
                                "', AT_Endezeit = '" & wb_sql_Functions.MySQLdatetime(Endezeit) &
                                "', AT_Periode = " & _Periode & ", AT_Str2 = '" & _Str2 & "'"
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdtAktionsTimer, sqlData, Sort)
        'Update ausführen
        Dim Result As Boolean = (winback.sqlCommand(sql) > 0)
        'Db-Verbimdung wieder schliessen
        winback.Close()
        Return Result
    End Function

    'AT_idx
    'AT_Quelle_Art
    'AT_Quelle_Nr
    'AT_Quelle_Typ
    'AT_Quelle_ID
    'AT_Ziel_Art
    'AT_Ziel_Nr
    'AT_Ziel_Aktion
    'AT_Ziel_Verzoegerung
    'AT_TimingArt
    'AT_Startzeit
    'AT_Endezeit
    'AT_Periode
    'AT_Str1
    'AT_Str2
    'AT_Kommentar
    'AT_Timestamp

End Class
