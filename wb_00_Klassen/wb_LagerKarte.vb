Imports WinBack.wb_Global
Imports WinBack.wb_Functions
Imports System.Data.SqlClient

Public Class wb_LagerKarte

    Private _Lfd As Integer
    Private _Vorfall As String = ""
    Private _VorfallNr As String = ""
    Private _Modul As String
    Private _Datum As String
    Private _Uhrzeit As String
    Private _Menge As String
    Private _BestandVorher As String
    Private _ChargenNummer As String = ""
    Private _Mitarbeiter As String
    Private _Preis As String

    Public ReadOnly Property Lfd As String
        Get
            Return _Lfd
        End Get
    End Property

    Public ReadOnly Property Vorfall As String
        Get
            If _Vorfall = "" Then
                Select Case _Modul
                    Case "Inventur-Bestandskorrektur"
                        Return "IV"
                    Case Else
                        Return "--"
                End Select
            Else
                Return _Vorfall
            End If
        End Get
    End Property

    Public ReadOnly Property Modul As String
        Get
            Return _Modul
        End Get
    End Property

    ''' <summary>
    ''' Gibt das Datenfeld dbo.Lagerkarte.Datum im Format YYYY-MM-DD aus
    ''' Ist in der Datenbank im Format YYYYMMDD gespeichert. (Als String unabhängig von der Ländereinstellung)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Datum As String
        Get
            Return Left(_Datum, 4) & "-" & Mid(_Datum, 5, 2) & "-" & Right(_Datum, 2)
        End Get
    End Property

    ''' <summary>
    ''' Gibt das Datenfeld dbo.Lagerkarte.Uhrzeit im Format hh:mm:ss aus
    ''' Ist in der Datenbank im Format HH:MM gespeichert
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Uhrzeit As String
        Get
            Return wb_Functions.FormatTimeStr(_Uhrzeit)
        End Get
    End Property

    Public ReadOnly Property Menge As Double
        Get
            Return wb_Functions.StrToDouble(_Menge)
        End Get
    End Property

    Public ReadOnly Property BestandVorher As Double
        Get
            Return wb_Functions.StrToDouble(_BestandVorher)
        End Get
    End Property

    Public ReadOnly Property BestandAktuell As Double
        Get
            Return BestandVorher + Menge
        End Get
    End Property

    Public ReadOnly Property LieferMenge As Double
        Get
            If Vorfall = "IV" Or Vorfall = "WE" Then
                'TODO was passiert bei negativer Inventur-Menge
                If Menge > 0 Then
                    Return Menge
                Else
                    Return 0.0
                End If
            Else
                Return 0.0
            End If
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

    Public ReadOnly Property Gebucht As String
        Get
            If (Menge > 0) Then
                Return "1"
            Else
                Return "3"
            End If
        End Get
    End Property

    Public Property VorfallNr As String
        Get
            Return _VorfallNr
        End Get
        Set(value As String)
            _VorfallNr = value
        End Set
    End Property

    ''' <summary>
    ''' Berechnet den aktuellen Lager-Bestand aus Menge und BestandVorher. Der Wert wird im
    ''' Datenfeld Menge abgespeichert.
    ''' Die Vorfallart wird zu WB geändert !
    ''' </summary>
    Public Sub InitBestand()
        If Vorfall <> "WB" Then
            '_Menge = wb_Functions.FormatStr((Menge + BestandVorher).ToString, 2)
            _Menge = 0
            _Vorfall = "WB"
        End If
    End Sub

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
        'Debug.Print("Read OrgaBack ArtikelLaufkarte " & Name & "/" & Value)
        Try
            Select Case Name
                'laufende Nummer
                Case "Lfd"
                    _Lfd = CInt(Value)
                Case "VorfallKürzel"
                    _Vorfall = Value
                Case "VorfallNr"
                    _VorfallNr = Value
                Case "Modul"
                    _Modul = Value
                Case "Datum"
                    _Datum = Value
                Case "Uhrzeit"
                    _Uhrzeit = Value
                Case "Menge"
                    _Menge = Value
                Case "BestandVorher"
                    _BestandVorher = Value
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

