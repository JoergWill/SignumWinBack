Imports WinBack

Public Class wb_BestellDatenSchritt

    Private _TourNr As Integer
    Private _ArtikelNummer As String
    Private _Produktionsmenge As Double
    Private _ChargenTeiler As wb_Global.ModusChargenTeiler

    Public Property TourNr As Integer
        Get
            Return _TourNr
        End Get
        Set(value As Integer)
            _TourNr = value
        End Set
    End Property

    Public Property ArtikelNummer As String
        Get
            Return _ArtikelNummer
        End Get
        Set(value As String)
            _ArtikelNummer = value
        End Set
    End Property

    Public Property Produktionsmenge As Double
        Get
            Return _Produktionsmenge
        End Get
        Set(value As Double)
            _Produktionsmenge = value
        End Set
    End Property

    Public Property ChargenTeiler As wb_Global.ModusChargenTeiler
        Get
            Return wb_Global.ModusChargenTeiler.OptimalUndRest
        End Get
        Set(value As wb_Global.ModusChargenTeiler)
            _ChargenTeiler = value
        End Set
    End Property

    ''' <summary>
    ''' Aufteilen des SQL-Resultset nach Spalten-Namen auf die Objekt-Eigenschaften
    ''' </summary>
    ''' <param name="Name">String - Spalten-Name aus Datenbank</param>
    ''' <param name="Value">Object - Wert aus Datenbank</param>
    ''' <returns></returns>
    Public Function MsSQLdbRead_Fields(Name As String, Value As Object)
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Feldname aus der Datenbank
        Try
            Select Case Name
                'Tour
                Case "TourNr"
                    _TourNr = Value
               'Artikelnummer(alpha)
                Case "ArtikelNr"
                    _ArtikelNummer = Value
                'Soll-Produktionsmenge in Stück
                Case "Produktionsmenge"
                    _Produktionsmenge = wb_Functions.StrToDouble(Value)

            End Select
        Catch ex As Exception
        End Try
        Return True
    End Function

End Class
