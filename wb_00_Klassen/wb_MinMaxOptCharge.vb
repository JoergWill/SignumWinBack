Imports WinBack

Public Class wb_MinMaxOptCharge
    Public WithEvents MinCharge As New wb_Charge
    Public WithEvents MaxCharge As New wb_Charge
    Public WithEvents OptCharge As New wb_Charge
    Public Event OnError(ByVal sender As Object)

    Private _StkGewicht As Double
    Private _TeigGewicht As Double
    Private _ErrorCheck As Boolean = False
    Private _NoErrorCheck As Boolean = True
    Private _HasChanged As Boolean = False
    Private _ErrorCode As wb_Global.MinMaxOptChargenError
    Private _Toleranz As Double = 0.0

    ''' <summary>
    ''' Teigmenge in kg
    ''' </summary>
    ''' <returns></returns>
    Public Property TeigGewicht As String
        Get
            Return _TeigGewicht
        End Get
        Set(value As String)
            _TeigGewicht = wb_Functions.StrToDouble(value)
            MinCharge.TeigGewicht = _TeigGewicht
            MaxCharge.TeigGewicht = _TeigGewicht
            OptCharge.TeigGewicht = _TeigGewicht

            'Werte aktualisieren
            If _NoErrorCheck Then
                RaiseError(wb_Global.MinMaxOptChargenError.NoError)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Stückgewicht in Gramm
    ''' </summary>
    ''' <returns></returns>
    Public Property StkGewicht As String
        Get
            Return _StkGewicht
        End Get
        Set(value As String)
            _StkGewicht = wb_Functions.StrToDouble(value)
            If _StkGewicht <= 0 Then
                _StkGewicht = 1000
            End If

            MinCharge.StkGewicht = _StkGewicht
            MaxCharge.StkGewicht = _StkGewicht
            OptCharge.StkGewicht = _StkGewicht

            'Werte aktualisieren
            If _NoErrorCheck Then
                RaiseError(wb_Global.MinMaxOptChargenError.NoError)
            End If
        End Set
    End Property

    Public ReadOnly Property ErrorCode As wb_Global.MinMaxOptChargenError
        Get
            Return _ErrorCode
        End Get
    End Property

    Public Property HasChanged As Boolean
        Get
            Return _HasChanged
        End Get
        Set(value As Boolean)
            _HasChanged = value
        End Set
    End Property

    Public Property ErrorCheck As Boolean
        Get
            Return _ErrorCheck
        End Get
        Set(value As Boolean)
            _ErrorCheck = value
        End Set
    End Property

    ''' <summary>
    ''' Alle Werte mit Null initialisieren
    ''' </summary>
    Public Sub Invalidate()
        'Event(Fehler Validierung Daten) sperren
        _ErrorCheck = False
        'Event(Aktualisierung) sperren
        _NoErrorCheck = False
        MinCharge.MengeInkg = 0
        OptCharge.MengeInkg = 0
        MaxCharge.MengeInkg = 0
        TeigGewicht = 0
        StkGewicht = 0
        'Event(Aktualisierung) wieder freigeben
        _NoErrorCheck = True
    End Sub

    ''' <summary>
    ''' Kopiert alle Porperties der übergebenen Klasse OHNE(!) Events auszulösen 
    ''' </summary>
    ''' <param name="Chrg"></param>
    Public Sub CopyFrom(Chrg As wb_MinMaxOptCharge)
        MinCharge.CopyFrom(Chrg.MinCharge)
        OptCharge.CopyFrom(Chrg.OptCharge)
        MaxCharge.CopyFrom(Chrg.MaxCharge)
        _TeigGewicht = Chrg._TeigGewicht
        _StkGewicht = Chrg._StkGewicht
        _HasChanged = Chrg._HasChanged
    End Sub

    ''' <summary>
    ''' Der Wert für die Minimal-Charge hat sich geändert: 
    '''     - Prüfen ob die Minimal-Charge kleiner als Optimal und/oder Maximal-Charge ist
    ''' </summary>
    Private Sub CheckMinCharge() Handles MinCharge.OnChange
        'Prüfen ob Min-Charge größer als Opt-Charge
        If MinCharge.cMengeInkg > (OptCharge.cMengeInkg + _Toleranz) And OptCharge.cMengeInkg > 0 And _ErrorCheck Then
            MinCharge.MengeInkg = OptCharge.MengeInkg
            RaiseError(wb_Global.MinMaxOptChargenError.MinGrOpt)
            Exit Sub
        End If
        'Prüfen ob Min-Charge größer als Max-Charge (Opt-Charge ist gleich der Max-Charge)
        If MinCharge.cMengeInkg > (MaxCharge.cMengeInkg + _Toleranz) And MaxCharge.cMengeInkg > 0 And _ErrorCheck Then
            MinCharge.MengeInkg = MaxCharge.MengeInkg
            RaiseError(wb_Global.MinMaxOptChargenError.MinGrMax)
            Exit Sub
        End If
        'Min-Charge wurde geändert - kein Fehler
        If _NoErrorCheck Then
            RaiseError(wb_Global.MinMaxOptChargenError.NoError)
        End If
    End Sub

    ''' <summary>
    ''' Der Wert für die Optimal-Charge hat sich geändert: 
    '''     - Prüfen ob die Optimal-Charge kleiner als Minimal-Charge oder größer als Maximal-Charge ist
    ''' </summary>
    Private Sub CheckOptCharge() Handles OptCharge.OnChange
        'Prüfen ob Opt-Charge kleiner als Min-Charge
        If (OptCharge.cMengeInkg + _Toleranz) < MinCharge.cMengeInkg And MinCharge.cMengeInkg > 0 And _ErrorCheck Then
            OptCharge.MengeInkg = MinCharge.MengeInkg
            RaiseError(wb_Global.MinMaxOptChargenError.OptKlMin)
            Exit Sub
        End If
        'Prüfen ob Opt-Charge größer als Max-Charge
        If OptCharge.cMengeInkg > (MaxCharge.cMengeInkg + _Toleranz) And MaxCharge.cMengeInkg > 0 And _ErrorCheck Then
            OptCharge.MengeInkg = MaxCharge.MengeInkg
            RaiseError(wb_Global.MinMaxOptChargenError.OptGrMax)
            Exit Sub
        End If
        'Opt-Charge wurde geändert - kein Fehler
        If _NoErrorCheck Then
            RaiseError(wb_Global.MinMaxOptChargenError.NoError)
        End If
    End Sub

    ''' <summary>
    ''' Der Wert für die Maximal-Charge hat sich geändert: 
    '''     - Prüfen ob die Maximal-Charge kleiner als Minimal-Charge oder kleiner als Optimal-Charge ist
    ''' </summary>
    Private Sub CheckMaxCharge() Handles MaxCharge.OnChange
        'Prüfen ob Max-Charge kleiner als Opt-Charge
        If (MaxCharge.cMengeInkg + _Toleranz) < OptCharge.cMengeInkg And OptCharge.cMengeInkg > 0 And _ErrorCheck Then
            MaxCharge.MengeInkg = OptCharge.MengeInkg
            RaiseError(wb_Global.MinMaxOptChargenError.MaxKlOpt)
            Exit Sub
        End If
        'Prüfen ob Max-Charge kleine als Min-Charge
        If (MaxCharge.cMengeInkg + _Toleranz) < MinCharge.cMengeInkg And MinCharge.cMengeInkg > 0 And _ErrorCheck Then
            MaxCharge.MengeInkg = MinCharge.MengeInkg
            RaiseError(wb_Global.MinMaxOptChargenError.MaxKlMin)
            Exit Sub
        End If
        'Max-Charge wurde geändert - kein Fehler
        If _NoErrorCheck Then
            RaiseError(wb_Global.MinMaxOptChargenError.NoError)
        End If
    End Sub

    Private Sub RaiseError(ErrCode As wb_Global.MinMaxOptChargenError)
        _ErrorCode = ErrCode
        RaiseEvent OnError(Me)
    End Sub

End Class
