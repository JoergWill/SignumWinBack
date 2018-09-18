Imports WinBack

Public Class wb_BestellDatenSchritt

    Private _TourNr As Integer
    Private _ArtikelNummer As String
    Private _Einheit As Integer
    Private _Produktionsmenge As Double
    Private _ChargenTeiler As wb_Global.ModusChargenTeiler = wb_Global.ModusChargenTeiler.OptimalUndRest
    Private _AnzahlVorschlag As Integer
    Private _AuftragsNummer As String = ""
    Private _BestellMenge As Double = wb_Global.UNDEFINED
    Private _SonderText As String = ""
    Private _AnzahlLose As Integer
    Private _Losgroesse As Double
    Private _Losart As String
    Private _Losgroesse2 As Double
    Private _Losart2 As String
    Private _SollwertTeilungText As String = ""

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
            Return _ChargenTeiler
        End Get
        Set(value As wb_Global.ModusChargenTeiler)
            _ChargenTeiler = value
        End Set
    End Property

    Public Property AuftragsNummer As String
        Get
            Return _AuftragsNummer
        End Get
        Set(value As String)
            _AuftragsNummer = value
        End Set
    End Property

    Public Property BestellMenge As Double
        Get
            Return _BestellMenge
        End Get
        Set(value As Double)
            _BestellMenge = value
        End Set
    End Property

    Public Property SonderText As String
        Get
            Return _SonderText
        End Get
        Set(value As String)
            _SonderText = value
        End Set
    End Property

    ''' <summary>
    ''' Der Sollwert-Teilung-Text setzt sich zusammen aus Anzahl der Lose, LosArt und Losgröße
    ''' Der String wird aus 3 Werten zusammengesetzt und formatiert.
    ''' 
    ''' Ist die LostArt (aus StoredProcedure Produktionsplanung) gleich "Stück" oder der Index der
    ''' LosArt (aus Tabelle dbo.LosArt) gleich Null (Default-Wert) wird ein Leerstring
    ''' zurückgeben. 
    ''' Die LosArt "Stück" macht keinen Sinn !!
    ''' </summary>
    ''' <returns></returns>
    Public Property SollwertTeilungText As String
        Get
            'LosArt-Text ermitteln (False bei LosArt=Stück)
            If wb_Einheiten_Global.getLosArtText(_Losart) Then
                'Anzahl der Lose
                _SollwertTeilungText = _AnzahlLose.ToString & " "
                'Losgröße
                _SollwertTeilungText &= _Losart & " à " & Int(_Losgroesse).ToString & " "
                'Einheit (Default 'Stück')
                _SollwertTeilungText &= wb_Einheiten_Global.getobEinheitFromNr(wb_Global.obEinheitStk)
            Else
                'LosArt "Stück" macht keinen Sinn
                _SollwertTeilungText = ""
            End If
            'Formatierter String
            Return _SollwertTeilungText
        End Get
        Set(value As String)
            _SollwertTeilungText = value
        End Set
    End Property

    Public Property Losgroesse2 As Double
        Get
            Return _Losgroesse2
        End Get
        Set(value As Double)
            _Losgroesse2 = value
        End Set
    End Property

    Public Property Losart2 As String
        Get
            Return _Losart2
        End Get
        Set(value As String)
            _Losart2 = value
        End Set
    End Property

    ''' <summary>
    ''' Aufteilen des SQL-Resultset nach Spalten-Namen auf die Objekt-Eigenschaften
    ''' Update pq_Prouktionsauftrag (30.08.2018/JE)
    ''' 
    ''' [dbo].[pq_ProduktionsPlanung]
    '''   FilialNr                  / 1
    '''   LieferDatum               / 20180713
    '''   TourNr                    / 0
    '''   ArtikelNr                 / 267
    '''   Einheit                   / 0
    '''   Farbe                     / 0
    '''   Groesse                   / NULL
    '''   BedarfMenge               / 4000,0000
    '''   Bezeichnung               / Mehrkornbrötchen
    '''   Zusatztexte               / Lorem ipsum
    '''   FrosterBestand            / 0,0000
    '''   FrosterMeldeBestand       / 0,0000
    '''   FrosterMaxBestand         / 0,0000
    '''   Produktionsmenge          / 4000,0000
    '''   AnzahlVorschlag           / 4000,0000
    '''   AnzahlLoseVorschlag       / 4000,0000
    '''   Losgroesse                / 1,0000 (Stück)
    '''   LosArt                    / 1 (Blech)
    '''   Losgroesse2               / 10,0000 (Stück)
    '''   LosArt2                   / 7 (Stikken)
    '''   FrosterEntnahme           / 0,0000
    '''   FrosterEinlagerung        / 0,0000
    '''   PlanungsStatus            / 0
    '''   
    ''' </summary>
    ''' <param name="Name">String - Spalten-Name aus Datenbank</param>
    ''' <param name="Value">Object - Wert aus Datenbank</param>
    ''' <returns></returns>
    Public Function MsSQLdbRead_Fields(Name As String, Value As Object) As Boolean
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Feldname aus der Datenbank
        Try
            'Debug.Print("StoredProcedure Produktionsplanung  " & Name & " / " & Value)
        Catch ex As Exception
        End Try

        Try
            Select Case Name
                'Tour
                Case "TourNr"
                    _TourNr = Value
               'Artikelnummer(alpha)
                Case "ArtikelNr"
                    _ArtikelNummer = Value
               'Zusatztexte(alpha getrennt durch CRLF)
                Case "Zusatztexte"
                    _SonderText = Value
                'Soll-Produktionsmenge in Stück
                Case "Produktionsmenge"
                    _Produktionsmenge = wb_Functions.StrToDouble(Value)
                Case "Einheit"
                    _Einheit = wb_Functions.StrToInt(Value)

                'Anzahl der Lose
                Case "AnzahlLoseVorschlag"
                    _AnzahlLose = wb_Functions.StrToInt(Value)
                'Losgröße
                Case "Losgroesse"
                    _Losgroesse = wb_Functions.StrToDouble(Value)
                Case "LosArt"
                    _Losart = Value

                'Losgröße
                Case "Losgroesse2"
                    Losgroesse2 = wb_Functions.StrToDouble(Value)
                Case "LosArt2"
                    Losart2 = Value

                    'Anzahl Lose Vorschlag
                Case "AnzahlVorschlag"
                    _AnzahlVorschlag = wb_Functions.StrToInt(Value)

            End Select
        Catch ex As Exception
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Setzen der Variablen für UnitTest
    ''' </summary>
    ''' <param name="AnzahlLose"></param>
    ''' <param name="LosArt"></param>
    ''' <param name="Losgroesse"></param>
    ''' <returns></returns>
    Public Function MsSQLdbRead_Fields(AnzahlLose As Integer, LosArt As String, Losgroesse As Double) As Boolean
        _AnzahlLose = AnzahlLose
        _Losart = LosArt
        _Losgroesse = Losgroesse
        Return True
    End Function

End Class
