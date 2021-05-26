Imports System.Windows.Forms

Public Class wb_LagerSilo

    Private _KompNr As Integer
    Private _KompNummer As String
    Private _KompBezeichnung As String

    Private _SiloNr As Integer
    Private _VorfallNr As String
    Private _LagerOrt As String
    Private _Preis As Double

    Private _TaraWert As Integer
    Private _BefMenge As Integer
    Private _VerbrauchtMenge As Double
    Private _ChargenNummer As String

    Friend Sub CopyFrom(control As Control)
        Select Case control.Name
            Case "wb_Silo"
                CopyFrom(DirectCast(control, wb_Silo))
            Case "wb_SiloSackWare"
                CopyFrom(DirectCast(control, wb_SiloSackware))
        End Select
    End Sub

    Private Sub CopyFrom(s As wb_Silo)
        _KompNr = s.KompNr
        _KompNummer = s.KompNummer
        _KompBezeichnung = s.KompBezeichnung
        _LagerOrt = s.LagerOrt
        _SiloNr = s.SiloNr
        _TaraWert = s.TaraWert
        _BefMenge = s.BefMenge
        _VerbrauchtMenge = s.VerbrauchtMenge
        _ChargenNummer = s.ChargenNummer
        _Preis = s.Preis
    End Sub

    Private Sub CopyFrom(s As wb_SiloSackware)
        _KompNr = s.KompNr
        _KompNummer = s.KompNummer
        _KompBezeichnung = s.KompBezeichnung
        _LagerOrt = s.LagerOrt
        _SiloNr = s.SiloNr
        _TaraWert = s.TaraWert
        _BefMenge = s.BefMenge
        _VerbrauchtMenge = s.VerbrauchtMenge
        _ChargenNummer = s.ChargenNummer
        _Preis = s.Preis
    End Sub

    Public Sub CopyFrom(p As wb_OrgaBackProcessPosition)
        _KompNr = wb_Global.UNDEFINED
        _KompNummer = p.ArtikelNummer
        _VorfallNr = p.VorfallNr
        _BefMenge = p.GelieferteMengeInkg
        _ChargenNummer = p.SerienNummer
        _Preis = p.Preis
    End Sub

    Public Property KompNr As Integer
        Get
            Return _KompNr
        End Get
        Set(value As Integer)
            _KompNr = value
        End Set
    End Property

    Public Property KompNummer As String
        Get
            Return _KompNummer
        End Get
        Set(value As String)
            _KompNummer = value
        End Set
    End Property

    Public Property KompBezeichnung As String
        Get
            Return _KompBezeichnung
        End Get
        Set(value As String)
            _KompBezeichnung = value
        End Set
    End Property

    Public Property SiloNr As Integer
        Get
            Return _SiloNr
        End Get
        Set(value As Integer)
            _SiloNr = value
        End Set
    End Property

    Public Property LagerOrt As String
        Get
            Return _LagerOrt
        End Get
        Set(value As String)
            _LagerOrt = value
        End Set
    End Property

    Public Property TaraWert As Integer
        Get
            Return _TaraWert
        End Get
        Set(value As Integer)
            _TaraWert = value
        End Set
    End Property

    Public Property BefMenge As Integer
        Get
            Return _BefMenge
        End Get
        Set(value As Integer)
            _BefMenge = value
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

    Public Property VerbrauchtMenge As Double
        Get
            Return _VerbrauchtMenge
        End Get
        Set(value As Double)
            _VerbrauchtMenge = value
        End Set
    End Property

End Class
