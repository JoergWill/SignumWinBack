Public Class wb_Charge
    Private _TeigGewicht As Double = 0
    Private _StkGewicht As Double = 1000
    Private _MengeInkg As Double = 0
    Private _MengeInProzent As Double = 0
    Private _MengeInStk As Integer = 0

    Public Event OnChange(ByVal sender As Object)

    ''' <summary>
    ''' Teiggewicht (Rezept-Gesamtgewicht) in kg
    ''' </summary>
    ''' <returns></returns>
    Public Property TeigGewicht As String
        Get
            Return _TeigGewicht.ToString("F3")
        End Get
        Set(value As String)
            _TeigGewicht = wb_Functions.StrToDouble(value)
            'Chargengröße in [%] neu berechnen
            CalcMengeInProzent()
            'Event - Werte haben sich geändert
            ValuesChanged()
        End Set
    End Property

    ''' <summary>
    ''' Stückgewicht (nass) in Gramm.
    ''' Wenn sich das Sückgewicht ändert, wird die Chargen-Größe in kg angepasst, so dass die Stückzahl konstant bleibt.
    ''' (In WinBack-Office kann umgeschaltet werden)
    ''' </summary>
    ''' <returns></returns>
    Public Property StkGewicht As String
        Get
            Return _StkGewicht.ToString("F0")
        End Get
        Set(value As String)
            _StkGewicht = wb_Functions.StrToDouble(value)
            If _StkGewicht <= 0 Then
                _StkGewicht = 1000
            End If
            'Chargengröße in [kg] neu berechen
            _MengeInkg = CalcMengeInkg()
            'Chargengröße in [%] neu berechnen
            _MengeInProzent = CalcMengeInProzent()
            'Event - Werte haben sich geändert
            ValuesChanged()
        End Set
    End Property

    ''' <summary>
    ''' Chargengröße in kg als formatierter String. 
    ''' Bei Änderung der Chargengröße(kg) werden die Werte für Stk und Prozent neu berechnet wenn Teiggröße und Stückgewicht bekannt sind.
    ''' </summary>
    ''' <returns></returns>
    Public Property MengeInkg As String
        Get
            Return _MengeInkg.ToString("F3")
        End Get
        Set(value As String)
            _MengeInkg = wb_Functions.StrToDouble(value)
            'Chargengröße in [Stk] neu berechnen
            _MengeInStk = CalcMengeInStk()
            'Chargengröße in [%] neu berechnen
            _MengeInProzent = CalcMengeInProzent()
            'Event - Werte haben sich geändert
            ValuesChanged()
        End Set
    End Property

    ''' <summary>
    ''' Chargengröße in kg als Double.
    ''' Wird für Vergleich der Min/Max/Opt-Charge und zur Berechnung der Chargengrößen verwendet
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property fMengeInkg As Double
        Get
            Return _MengeInkg
        End Get
    End Property

    ''' <summary>
    ''' Chargenröße in Prozent der Teigmenge als formatierter String. 
    ''' Bei Änderung der Chargengröße(%) werden die Werte für Stk und kg neu berechnet wenn Teiggröße und Stückgewicht bekannt sind.
    ''' </summary>
    ''' <returns></returns>
    Public Property MengeInProzent As String
        Get
            Return _MengeInProzent.ToString("F0")
        End Get
        Set(value As String)
            _MengeInProzent = wb_Functions.StrToDouble(value)
            'Chargengröße in [kg] neu berechen
            _MengeInkg = _MengeInProzent * _TeigGewicht / 100
            'Chargengröße in [Stk] neu berechnen
            _MengeInStk = CalcMengeInStk()
            'Event - Werte haben sich geändert
            ValuesChanged()
        End Set
    End Property

    ''' <summary>
    ''' Chargengröße in Prozent als Double.
    ''' Wird zur Berechnung der Chargengrößen verwendet
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property fMengeInProzent As Double
        Get
            Return _MengeInkg
        End Get
    End Property

    ''' <summary>
    ''' Chargengröße in Stk als formatierter String. 
    ''' Bei Änderung der Chargengröße(Stk) werden die Werte für kg und % neu berechnet wenn Teiggröße und Stückgewicht bekannt sind.
    ''' </summary>
    ''' <returns></returns>
    Public Property MengeInStk As String
        Get
            Return _MengeInStk.ToString("F0")
        End Get
        Set(value As String)
            _MengeInStk = wb_Functions.StrToDouble(value)
            'Chargengröße in [kg] neu berechen
            _MengeInkg = CalcMengeInkg()
            'Chargengröße in [%] neu berechen
            _MengeInProzent = CalcMengeInProzent()
            'Event - Werte haben sich geändert
            ValuesChanged()
        End Set
    End Property

    ''' <summary>
    ''' Chargengröße in Prozent als Double.
    ''' Wird zur Berechnung der Chargengrößen verwendet
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property fMengeInStk As Double
        Get
            Return _MengeInkg
        End Get
    End Property

    ''' <summary>
    ''' Chargengröße in [kg] aus Menge in Stk und Stückgewicht[gr] neu berechnen
    ''' </summary>
    ''' <returns></returns>
    Private Function CalcMengeInkg() As Double
        Return (MengeInStk * _StkGewicht) / 1000
    End Function

    ''' <summary>
    ''' Chargengröße in [%] aus Teig-Gewicht (Rezeptgröße) und Chargengröße in [kg] neu berechnen
    ''' </summary>
    ''' <returns></returns>
    Private Function CalcMengeInProzent() As Double
        If _MengeInkg > 0 Then
            Return (_MengeInkg * 100) / _TeigGewicht
        Else
            Return 0.0
        End If
    End Function

    ''' <summary>
    ''' Chargengröße in [Stk] aus Chargengröße in [kg] und Stückgewicht [gr] neu berechnen
    ''' </summary>
    ''' <returns></returns>
    Private Function CalcMengeInStk() As Integer
        If _StkGewicht > 0 Then
            Return (_MengeInkg * 1000) / _StkGewicht
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Event - die Werte der Chargengrößen haben sich geändert.
    ''' </summary>
    Private Sub ValuesChanged()
        RaiseEvent OnChange(Me)
    End Sub
End Class
