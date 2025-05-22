Public Class wb_OrgaBackProcessPosition
    Private _PositionNummer As Integer
    Private _ArtikelNummer As String
    Private _Mitarbeiter As String
    Private _VorfallNr As String
    Private _LieferDatum As String
    Private _GelieferteMenge As Double
    Private _Einheit As Integer
    Private _SerienNummern As String
    Private _Preis As Double

    Public Property PositionNummer As Integer
        Get
            Return _PositionNummer
        End Get
        Set(value As Integer)
            _PositionNummer = value
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

    Public Property Mitarbeiter As String
        Get
            Return _Mitarbeiter
        End Get
        Set(value As String)
            _Mitarbeiter = value
        End Set
    End Property

    Public Property LieferDatum As String
        Get
            Return _LieferDatum
        End Get
        Set(value As String)
            _LieferDatum = value
        End Set
    End Property

    Public Property GelieferteMenge As Double
        Get
            Return _GelieferteMenge
        End Get
        Set(value As Double)
            _GelieferteMenge = value
        End Set
    End Property

    Public Property SerienNummer As String
        Get
            Return _SerienNummern
        End Get
        Set(value As String)
            _SerienNummern = value
        End Set
    End Property

    Public Property Preis As Double
        Get
            Return _Preis
        End Get
        Set(value As Double)
            _Preis = value
        End Set
    End Property

    Public Property VorfallNr As String
        Get
            Return _VorfallNr
        End Get
        Set(value As String)
            _VorfallNr = value
        End Set
    End Property

    Public Property Einheit As Integer
        Get
            Return _Einheit
        End Get
        Set(value As Integer)
            _Einheit = value
        End Set
    End Property

    ''' <summary>
    ''' Die gelieferte Menge in kg.
    ''' Wenn in der Tabelle dbo.GeschäftsvorfallPosition die Einheit nicht kg ist, wird über die Funktion EinheitenUmrechnung 
    ''' der Wert in kg umgerechnet und zurückgegeben.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property GelieferteMengeInkg As Double
        Get
            If Einheit = wb_Global.obEinheitKilogramm Then
                Return GelieferteMenge
            Else
                Return wb_sql_Functions.EinheitenUmrechnung(ArtikelNummer, Einheit, wb_Global.obEinheitKilogramm)
            End If
        End Get
    End Property


    ''' <summary>
    ''' Liest die Datenfelder aus der Tabelle dbo.GeschäftsvorfallPosition
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    Public Function MsSQLdbRead_Fields(Name As String, Value As Object) As Boolean
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Feldname aus der Datenbank
        Try
            Debug.Print("OrgaBack Process-Position " & Name & " / " & Value)
        Catch ex As Exception
        End Try

        Try
            Select Case Name
                'PositionNummer
                Case "PositionNummer"
                    _PositionNummer = Value
                'ArtikelNummer
                Case "ArtikelNr"
                    _ArtikelNummer = Value
                'Mitarbeiter/User
                Case "MitarbeiterKürzel"
                    _Mitarbeiter = Value
                Case "VorfallNr"
                    _VorfallNr = Value
                'Lieferdatum
                Case "LieferDatum"
                    _LieferDatum = Value
                'Gelieferte Menge
                Case "Menge"
                    _GelieferteMenge = Value
                'Seriennummer
                Case "SerienNummer"
                    _SerienNummern = Value
                'Einheit (z.B. bei Lieferung)
                Case "Einheit"
                    _Einheit = Value
                'Einzelpreis
                Case "EinzelpreisNetto"
                    _Preis = Value
            End Select
        Catch ex As Exception
        End Try
        Return True
    End Function


End Class

'Private _ArbeitsPlatz As String
'Private _ArtikelNr As String
'Private _Einheit As Integer
'Private _Farbe As Integer
'Private _Groesse As String
'Private _SerienNummer As String
'Private _FilialNummer As Integer
'Private _KorrNr As String
'Private _MwStSatzNr As Integer
'Private _BestellDatenNr As Integer
'Private _GarantieMonate As Integer
'Private _VerkaufVermietung As String
'Private _FixKauf As String
'Private _Text As String
'Private _Menge As Double
'Private _EinzelpreisNetto As Double
'Private _RabattProzent As Double
'Private _RabattBetrag As Double
'Private _EinzelpreisBrutto As Double
'Private _MwStBetrag As Double
'Private _MwStProzent As Double
'Private _ZeilenSummeNetto As Double
'Private _ZeilenSummeBrutto As Double
'Private _RabattBrutto As Double
'Private _Status As String
'Private _Status1 As String
'Private _Status2 As String
'Private _RückstandJN As Integer
'Private _TeilLieferungJN As Integer
'Private _Gewicht As Double
'Private _BonusBetrag As Double
'Private _Abteilung As Integer
'Private _SammelText As Integer
'Private _PreisfindungsArt As String
'Private _NatRabattMenge As Double
'Private _Rohertrag As Double
'Private _Hierarchie As String
'Private _PositionsArt As String
'Private _Bemerkung As String
'Private _SatzTyp As String
'Private _VKPreis As Double
'Private _AnsprechpartnerNr As Integer
'Private _ReferenzMenge As Double
'Private _Paketnummer As String
'Private _FremdPositionsNr As Integer
'Private _Lagerort As String
'Private _IntraStatID As Integer
'Private _EAN As String
'Private _Konto As String
'Private _Kostenstelle As String
'Private _VaterPosition As Integer
'Private _KontraktNr As Integer

