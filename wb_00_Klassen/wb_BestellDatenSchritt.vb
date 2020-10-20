Imports WinBack

Public Class wb_BestellDatenSchritt

    Private _TourNr As Integer
    Private _ArtikelNummer As String
    Private _Einheit As Integer
    Private _DispositionsArt As Integer
    Private _Produktionsmenge As Double
    Private _AusbackMenge As Double
    Private _MengeInProduktion As Double
    Private _ChargenTeiler As wb_Global.ModusChargenTeiler = wb_Global.ModusChargenTeiler.OptimalUndRest
    Private _AnzahlVorschlag As Integer
    Private _Bedarfmenge As Integer
    Private _BedarfFroster As Integer
    Private _FrosterEntnahme As Integer
    Private _FrosterEinlagerung As Integer
    Private _MengeFrosterEntnommen As Integer
    Private _MengeFrosterEingelagert As Integer
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

    ''' <summary>
    ''' Enthält die Soll-Produktionsmenge aus OrgaBack. Die MengeInProduktion ist schon verrechnet.
    ''' Wenn eine Ausback-Menge (Teiglinge) angegeben ist, wird dieser Wert verwendet. Die darunterliegende Rezeptur wird
    ''' in der Berechnung der Vorproduktion dann nicht berücksichtigt! (Flag Aufloesen = False)
    ''' </summary>
    ''' <returns></returns>
    Public Property Produktionsmenge As Double
        Get
            If Aufloesen Then
                Return _Produktionsmenge
            Else
                Return _AusbackMenge
            End If
        End Get
        Set(value As Double)
            _Produktionsmenge = value
        End Set
    End Property

    ''' <summary>
    ''' Flag Rezeptur auflösen.
    ''' Wenn aus der Produktions-Planung eine Anzahl an Teiglingen übergeben wird (Aufbackmenge) darf die darunterliegende Rezeptur nicht
    ''' aufgelöst werden. Die Teiglinge werden aus dem Froster entnommen und müssen nicht produziert werden
    ''' (Die Froster-Filiale bestellt bedarfsorientiert)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Aufloesen As Boolean
        Get
            Return (_AusbackMenge = 0)
        End Get
    End Property

    ''' <summary>
    ''' Enthält den Wert der schon in Produktion befindlicher Chargen.
    ''' Wird verwendet, wenn der Back/Teigzettel mehrmals am Tag ausgedruckt wird.
    ''' Mit jedem Ausdruck wird die MengeInProduktion an OrgaBack zurückgemeldet und dort mit
    ''' den Bestelldaten verrechnet.
    ''' </summary>
    ''' <returns></returns>
    Public Property MengeInProduktion As Double
        Get
            Return _MengeInProduktion
        End Get
        Set(value As Double)
            _MengeInProduktion = value
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
            Return _Bedarfmenge
        End Get
        Set(value As Double)
            _Bedarfmenge = value
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

    Public Property BedarfFroster As Integer
        Get
            Return _BedarfFroster
        End Get
        Set(value As Integer)
            _BedarfFroster = value
        End Set
    End Property

    Public Property FrosterEntnahme As Integer
        Get
            Return _FrosterEntnahme
        End Get
        Set(value As Integer)
            _FrosterEntnahme = value
        End Set
    End Property

    Public Property FrosterEinlagerung As Integer
        Get
            Return _FrosterEinlagerung
        End Get
        Set(value As Integer)
            _FrosterEinlagerung = value
        End Set
    End Property

    ''' <summary>
    ''' Aufteilen des SQL-Resultset nach Spalten-Namen auf die Objekt-Eigenschaften
    ''' Update pq_Prouktionsauftrag (30.08.2018/JE)
    ''' Update pq_Prouktionsauftrag (20.02.2020/JE) DB-Version 8.0050
    ''' Erweiterung pq_Produktionsauftrag (25.09.2020/JW) Aufbackmenge Teigling
    ''' 
    ''' [dbo].[pq_ProduktionsPlanung]
    '''   FilialNr                  / 1
    '''   LieferDatum               / 20180713
    '''   TourNr                    / 0
    '''   ArtikelNr                 / 267
    '''   Einheit                   / 0
    '''   Farbe                     / 0
    '''   Groesse                   / NULL
    '''   DispositionsArt           / 2..4                  (20.02.2020/JE)
    '''   BedarfMenge               / 4000,0000
    '''   AusbackMenge              /                       (30.09.2020/JE)
    '''   MengeInProduktion         / 1000,0000             (20.02.2020/JE)
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
    '''   BedarfFroster             / 0,0000                (20.02.2020/JE)
    '''   FrosterEntnahme           / 0,0000
    '''   FrosterEinlagerung        / 0,0000        
    '''   MengeFrosterEntnommen     / 0,0000                (20.02.2020/JE)
    '''   MengeFrosterEingelagert   / 0,0000                (20.02.2020/JE)
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
                'Einheit
                Case "Einheit"
                    _Einheit = wb_Functions.StrToInt(Value)
               'Zusatztexte(alpha getrennt durch CRLF)
                Case "Zusatztexte"
                    _SonderText = Value
                'Soll-Produktionsmenge in Stück
                Case "Produktionsmenge"
                    '_AusbackMenge = wb_Functions.StrToDouble(Value)
                    _Produktionsmenge = wb_Functions.StrToDouble(Value)
                'Soll-Aufbackmenge in Stück (2020-09-25 JE - Teigling aus Froster)
                Case "AusbackMenge"
                    '_Produktionsmenge = wb_Functions.StrToDouble(Value)
                    _AusbackMenge = wb_Functions.StrToDouble(Value)
                'Menge in Produktion in Stück
                Case "MengeInProduktion"
                    MengeInProduktion = wb_Functions.StrToDouble(Value)
                Case "BedarfMenge"
                    _Bedarfmenge = wb_Functions.StrToDouble(Value)
                Case "Einheit"
                    _Einheit = wb_Functions.StrToInt(Value)
                Case "DispositionsArt"
                    _DispositionsArt = wb_Functions.StrToInt(Value)

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

                'Bedarf Froster (in Bedarf Menge enthalten)
                Case "BedarfFroster"
                    BedarfFroster = wb_Functions.StrToInt(Value)
                'Anzahl Froster Entnahme
                Case "FrosterEntnahme"
                    _FrosterEntnahme = wb_Functions.StrToInt(Value)
                'Anzahl Froster Einlagerung
                Case "FrosterEinlagerung"
                    _FrosterEinlagerung = wb_Functions.StrToInt(Value)
                Case "MengeFrosterEntnommen"
                    _MengeFrosterEntnommen = wb_Functions.StrToInt(Value)
                'Anzahl Froster Einlagerung
                Case "MengeFrosterEingelagert"
                    _MengeFrosterEingelagert = wb_Functions.StrToInt(Value)

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
