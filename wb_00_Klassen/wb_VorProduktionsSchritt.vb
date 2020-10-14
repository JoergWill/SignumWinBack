Public Class wb_VorProduktionsSchritt
    Implements IComparable

    Private _ArtikelNr As Integer
    Private _RezeptNr As Integer
    Private _RezeptVar As Integer
    Private _StkGewOK As Boolean
    Private _Sollwert_kg As Double
    Private _RezeptGroesse As Double
    Private _TeigChargen As wb_Global.ChargenMengen
    Private _LinienGruppe As Integer
    Private _Aufloesen As Boolean

    Public Property ArtikelNr As Integer
        Get
            Return _ArtikelNr
        End Get
        Set(value As Integer)
            _ArtikelNr = value
        End Set
    End Property

    Public Property RezeptNr As Integer
        Get
            Return _RezeptNr
        End Get
        Set(value As Integer)
            _RezeptNr = value
        End Set
    End Property

    Public Property RezeptVar As Integer
        Get
            Return _RezeptVar
        End Get
        Set(value As Integer)
            _RezeptVar = value
        End Set
    End Property

    Public Property StkGewOK As Boolean
        Get
            Return _StkGewOK
        End Get
        Set(value As Boolean)
            _StkGewOK = value
        End Set
    End Property

    Public Property Sollwert_kg As Double
        Get
            Return _Sollwert_kg
        End Get
        Set(value As Double)
            _Sollwert_kg = value
        End Set
    End Property

    Public Property RezeptGroesse As Double
        Get
            Return _RezeptGroesse
        End Get
        Set(value As Double)
            _RezeptGroesse = value
        End Set
    End Property

    Public Property TeigChargen As wb_Global.ChargenMengen
        Get
            Return _TeigChargen
        End Get
        Set(value As wb_Global.ChargenMengen)
            _TeigChargen = value
        End Set
    End Property

    Public Property LinienGruppe As Integer
        Get
            Return _LinienGruppe
        End Get
        Set(value As Integer)
            _LinienGruppe = value
        End Set
    End Property

    Public Property Aufloesen As Boolean
        Get
            Return _Aufloesen
        End Get
        Set(value As Boolean)
            _Aufloesen = value
        End Set
    End Property

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        'TODO Nach Produktionsdatum sortieren !!
        Return String.Compare(RezeptNr, DirectCast(obj, wb_VorProduktionsSchritt).RezeptNr)
    End Function

    '    ArtikelNr: String;                 // (@3.0.5) Rezept im Rezept
    '    KoNr: Integer;                     // (@3.0.5) interne Komponenten-Nummer (Rückmeldung Produktions-Menge)
    '    StkGewOK: Boolean;                 // (@3.0.5) Stückgewicht für Rohstoff ist angegeben
    '    RezeptNr: Integer;
    '    Sollmenge: Double;
    '    B_KA_Charge_Opt_kg: Double;        // (@2.7.5) für Produktionsplanung
    '    B_KA_Charge_Min_kg: Double;        // (@2.7.5) für Produktionsplanung
    '    B_KA_Charge_Max_kg: Double;        // (@2.7.5) für Produktionsplanung
    '    B_RS_RezMenge: Double;             // (@2.7.5) Rezeptgröße
    '    Liniengruppe: String;
    '    Variante: String;
    '    ProdStart: TDateTime;              // (@3.4.5) Startzeit
End Class
