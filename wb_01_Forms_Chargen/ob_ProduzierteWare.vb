Public Class ob_ProduzierteWare
    Private _FilialNummer As Integer = wb_Global.UNDEFINED
    Private _ProduktionsDatum As DateTime = Now
    Private _SatzTyp As Char = "V"
    Private _ArtikelNr As String = ""
    Private _Unit As Integer = wb_Global.EinheitKilogramm
    Private _Color As Integer = 0
    Private _Size As Integer = vbNull
    Private _Menge As Double = 0
    Private _ChargenNummer As String = ""
    Private _Haltbarkeit As DateTime = Now

    Public Property FilialNummer As Integer
        Get
            Return _FilialNummer
        End Get
        Set(value As Integer)
            _FilialNummer = value
        End Set
    End Property

    Public Property ProduktionsDatum As Date
        Get
            Return _ProduktionsDatum
        End Get
        Set(value As Date)
            _ProduktionsDatum = value
        End Set
    End Property

    Public Property SatzTyp As Char
        Get
            Return _SatzTyp
        End Get
        Set(value As Char)
            _SatzTyp = value
        End Set
    End Property

    Public Property ArtikelNr As String
        Get
            Return _ArtikelNr
        End Get
        Set(value As String)
            _ArtikelNr = value
        End Set
    End Property

    Public Property Unit As Integer
        Get
            Return _Unit
        End Get
        Set(value As Integer)
            _Unit = value
        End Set
    End Property

    Public Property Color As Integer
        Get
            Return _Color
        End Get
        Set(value As Integer)
            _Color = value
        End Set
    End Property

    Public Property Size As Integer
        Get
            Return _Size
        End Get
        Set(value As Integer)
            _Size = value
        End Set
    End Property

    Public Property Menge As Double
        Get
            Return _Menge
        End Get
        Set(value As Double)
            _Menge = value
        End Set
    End Property

    Public Property ChargenNummer As String
        Get
            Return _ChargenNummer
        End Get
        Set(value As String)
            _ChargenNummer = value
        End Set
    End Property

    Public Property Haltbarkeit As Date
        Get
            Return _Haltbarkeit
        End Get
        Set(value As Date)
            _Haltbarkeit = value
        End Set
    End Property
End Class
