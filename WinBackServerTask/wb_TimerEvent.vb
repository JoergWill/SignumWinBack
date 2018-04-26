Imports MySql.Data.MySqlClient

Public Class wb_TimerEvent
    Implements IComparable

    Private _sort As String
    Private _Task As String
    Private _Startzeit As DateTime
    Private _Periode As Int64
    Private _Status As Char

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

    Public Property Startzeit As Date
        Get
            Return _Startzeit
        End Get
        Set(value As Date)
            _Startzeit = value
        End Set
    End Property

    Public Property Periode As Long
        Get
            Return _Periode
        End Get
        Set(value As Long)
            _Periode = value
        End Set
    End Property

    Public Property Status As Char
        Get
            Return _Status
        End Get
        Set(value As Char)
            _Status = value
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
            End Try
        Next
    End Function

    Private Function MySQLdbRead_Fields(Name As String, Value As Object) As Boolean
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Return False
        End If

        'Feldname aus der Datenbank
        Debug.Print("Read AktionsTimer " & Name & " / " & Value)
        'Try
        Select Case Name

            'Nummer(intern)
            Case "AT_idx"
                Sort = Value

            'Bezeichnung(Task)
            Case "AT_Quelle_Typ"
                Task = Value

            'Startzeit
            Case "AT_Startzeit"
                Startzeit = WinBack.wb_sql_Functions.MySQLdatetime(Value)

            'Periode in Minuten
            Case "AT_Periode"
                Periode = Value

            End Select
        'Catch
        'End Try


        Return True
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
