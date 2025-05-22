Public Class wb_KomponProduktInfo

    Private _Artikel As wb_Komponente
    Private _ArtikelNr As Integer

    Public Sub New(Artikel As wb_Komponente)
        Me._Artikel = Artikel
    End Sub

    Public Sub New(Artikelnr As Integer)
        _ArtikelNr = Artikelnr
    End Sub

    Private ReadOnly Property ArtikelNr As Integer
        Get
            If _Artikel IsNot Nothing Then
                Return _Artikel.Nr
            Else
                Return _ArtikelNr
            End If
        End Get
    End Property

    Public ReadOnly Property Bild As String
        Get
            Return _Artikel.Bild
        End Get
    End Property

    Public ReadOnly Property VerkaufsBezeichnung As String
        Get
            Return _Artikel.Nummer & " " & _Artikel.Bezeichnung
        End Get
    End Property

    Public ReadOnly Property WarenGruppe As Integer
        Get
            Return _Artikel.ktTyp200.Warengruppe
        End Get
    End Property

    Public ReadOnly Property Haltbarkeit As Integer
        Get
            Return _Artikel.ktTyp200.Haltbarkeit
        End Get
    End Property

    Public ReadOnly Property Verkaufstage As Integer
        Get
            Return _Artikel.ktTyp200.VerkaufsTage
        End Get
    End Property

    Public ReadOnly Property Lagerung As String
        Get
            Return _Artikel.ktTyp200.Lagerung
        End Get
    End Property

    Public ReadOnly Property Gebäckcharakteristik As String
        Get
            Dim Value As New wb_Hinweise(wb_Global.Hinweise.GebCharakteristik, ArtikelNr)
            If Value.Memo Is Nothing OrElse Value.Memo = "" Then
                Return "."
            Else
                Return Value.Memo
            End If
        End Get
    End Property

    Public ReadOnly Property Getreidemischung As String
        Get
            Dim Value As New wb_Hinweise(wb_Global.Hinweise.MehlZusammensetzung, ArtikelNr)
            If Value.Memo Is Nothing OrElse Value.Memo = "" Then
                Return "."
            Else
                Return Value.Memo
            End If
        End Get
    End Property

    Public ReadOnly Property Zutatenliste As String
        Get
            Dim Value As New wb_Hinweise(wb_Global.Hinweise.ZutatenListe, ArtikelNr)
            If Value.Memo Is Nothing OrElse Value.Memo = "" Then
                Return "."
            Else
                Return wb_Functions.FormatZutatenListe(Value.Memo)
            End If
        End Get
    End Property

    Public ReadOnly Property DeklarationsBezeichung As String
        Get
            Dim Value As New wb_Hinweise(wb_Global.Hinweise.DeklBezRohstoff, ArtikelNr)
            If Value.Memo Is Nothing OrElse Value.Memo = "" Then
                Return "."
            Else
                Return wb_Functions.FormatZutatenListe(Value.Memo)
            End If
        End Get
    End Property

    Public ReadOnly Property DeklarationsBezeichungIntern As String
        Get
            Dim Value As New wb_Hinweise(wb_Global.Hinweise.DeklBezRohstoffIntern, ArtikelNr)
            If Value.Memo Is Nothing OrElse Value.Memo = "" Then
                Return "."
            Else
                Return wb_Functions.FormatZutatenListe(Value.Memo)
            End If
        End Get
    End Property

    Public ReadOnly Property Verzehrtipps As String
        Get
            Dim Value As New wb_Hinweise(wb_Global.Hinweise.Verzehrtipps, ArtikelNr)
            If Value.Memo Is Nothing OrElse Value.Memo = "" Then
                Return "."
            Else
                Return Value.Memo
            End If
        End Get
    End Property

    Public ReadOnly Property Wissenswertes As String
        Get
            Dim Value As New wb_Hinweise(wb_Global.Hinweise.Wissenswertes, ArtikelNr)
            If Value.Memo Is Nothing OrElse Value.Memo = "" Then
                Return "."
            Else
                Return Value.Memo
            End If
        End Get
    End Property

    Public ReadOnly Property Allergene As String
        Get
            Return _Artikel.ktTyp301.AllergenListe_C & vbCrLf & _Artikel.ktTyp301.AllergenListe_T
        End Get
    End Property

    Public ReadOnly Property Nwt_Energiekcal As String
        Get
            Return wb_Functions.FormatStr(_Artikel.ktTyp301.Naehrwert(wb_Global.T301_Kilokalorien).ToString, 0)
        End Get
    End Property

    Public ReadOnly Property Nwt_EnergiekJ As String
        Get
            Return wb_Functions.FormatStr(_Artikel.ktTyp301.Naehrwert(wb_Global.T301_KiloJoule).ToString, 0)
        End Get
    End Property

    Public ReadOnly Property Nwt_Fett As String
        Get
            Return wb_Functions.FormatStr(_Artikel.ktTyp301.Naehrwert(wb_Global.T301_Fette).ToString, 1)
        End Get
    End Property

    Public ReadOnly Property Nwt_FettGes As String
        Get
            Return wb_Functions.FormatStr(_Artikel.ktTyp301.Naehrwert(wb_Global.T301_gesFettsaeuren).ToString, 1)
        End Get
    End Property

    Public ReadOnly Property Nwt_Kohlehydrate As String
        Get
            Return wb_Functions.FormatStr(_Artikel.ktTyp301.Naehrwert(wb_Global.T301_Kohlenhydrate).ToString, 1)
        End Get
    End Property

    Public ReadOnly Property Nwt_Zucker As String
        Get
            Return wb_Functions.FormatStr(_Artikel.ktTyp301.Naehrwert(wb_Global.T301_Zucker).ToString, 1)
        End Get
    End Property

    Public ReadOnly Property Nwt_Ballaststoffe As String
        Get
            Return wb_Functions.FormatStr(_Artikel.ktTyp301.Naehrwert(wb_Global.T301_Ballaststoffe).ToString, 1)
        End Get
    End Property

    Public ReadOnly Property Nwt_Proteine As String
        Get
            Return wb_Functions.FormatStr(_Artikel.ktTyp301.Naehrwert(wb_Global.T301_Proteine).ToString, 1)
        End Get
    End Property

    Public ReadOnly Property Nwt_Salz As String
        Get
            Return wb_Functions.FormatStr(_Artikel.ktTyp301.Naehrwert(wb_Global.T301_GesamtKochsalz).ToString, 2)
        End Get
    End Property

End Class
