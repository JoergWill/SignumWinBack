Imports WinBack.wb_Global
Imports WinBack.wb_Functions
Imports System.Data.SqlClient

Public Class wb_LagerKarte

    Private _Lfd As Integer
    Private _Vorfall As String
    Private _Modul As String
    Private _Datum As String
    Private _Uhrzeit As String
    Private _Menge As String
    Private _ChargenNummer As String
    Private _Mitarbeiter As String
    Private _Preis As String


    Public Sub New(KompNr As Integer)

    End Sub

    Public ReadOnly Property Lfd As String
        Get
            Return _Lfd
        End Get
    End Property

    Public ReadOnly Property Vorfall As String
        Get
            Return _Vorfall
        End Get
    End Property

    Public ReadOnly Property Modul As String
        Get
            Return _Modul
        End Get
    End Property

    Public ReadOnly Property Datum As String
        Get
            Return _Datum
        End Get
    End Property

    Public ReadOnly Property Uhrzeit As String
        Get
            Return _Uhrzeit
        End Get
    End Property

    Public ReadOnly Property Menge As String
        Get
            Return _Menge
        End Get
    End Property

    Public ReadOnly Property ChargenNummer As String
        Get
            Return _ChargenNummer
        End Get
    End Property

    Public ReadOnly Property Mitarbeiter As String
        Get
            Return _Mitarbeiter
        End Get
    End Property

    Public ReadOnly Property Preis As String
        Get
            Return _Preis
        End Get
    End Property

    Public Function msSQLdbRead(ByRef sqlReader As SqlDataReader) As Boolean
        'Parameter - Anzahl der Felder im DataSet
        For i = 0 To sqlReader.FieldCount - 1
            Try
                MsSQLdbRead_Daten(sqlReader.GetName(i), sqlReader.GetValue(i))
            Catch ex As Exception
                Debug.Print("Exception MySQLdbRead " & sqlReader.GetName(i))
            End Try
        Next
        Return True
    End Function

    ''' <summary>
    ''' Schreibt den Wert aus Value in die entprechende Property der Klasse. Der Feldname bestimmt das Ziel
    ''' </summary>
    ''' <param name="Name">String - Bezeichnung Datenbankfeld</param>
    ''' <param name="Value">Object - Wert Datenbankfeld(Inhalt)</param>
    ''' <returns></returns>
    Private Function MsSQLdbRead_Daten(Name As String, Value As Object) As Boolean
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Feldname aus der Datenbank
        Debug.Print("Read OrgaBack ArtikelLaufkarte " & Name & "/" & Value)
        Try
            Select Case Name
                'laufende Nummer
                Case "Lfd"
                    _Lfd = CInt(Value)
                Case "VorfallKürzel"
                    _Vorfall = Value
                Case "Modul"
                    _Modul = Value
                Case "Datum"
                    _Datum = Value
                Case "Uhrzeit"
                    _Uhrzeit = Value
                Case "Menge"
                    _Menge = Value
                Case "ChargenNr"
                    _ChargenNummer = Value
                Case "Mitarbeiter"
                    _Mitarbeiter = Value
                Case "Preis"
                    _Preis = Value

            End Select
        Catch ex As Exception
        End Try
        Return True
    End Function

End Class

'   [Lfd]
'   [ArtikelNr]
'   [Einheit]
'   [Farbe]
'   [Grösse]
'   [Datum]
'   [Mitarbeiter]
'   [Filiale]
'   [Menge]
'   [BestandVorher]
'   [LagerCode]
'   [Preis]
'   [KorrNr]
'   [VorfallKürzel]
'   [VorfallNr]
'   [Seriennummer]
'   [Modul]
'   [ChargenNr]
'   [DocumentId]
'   [DocumentIdEingang]
'   [Uhrzeit]
'   [ZusatzInfo1]
'   [ZusatzInfo2]
'   [ZusatzInfo3]

