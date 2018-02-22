Imports WinBack

Public Class wb_MinMaxOptCharge
    Public WithEvents MinCharge As New wb_Charge
    Public WithEvents MaxCharge As New wb_Charge
    Public WithEvents OptCharge As New wb_Charge
    Public Event OnError(ByVal sender As Object)

    Private _StkGewicht As Double
    Private _TeigGewicht As Double
    Private _ErrorCode As wb_Global.MinMaxOptChargenError

    Public Property TeigGewicht As String
        Get
            Return _TeigGewicht
        End Get
        Set(value As String)
            _TeigGewicht = wb_Functions.StrToDouble(value)
            MinCharge.TeigGewicht = _TeigGewicht
            MaxCharge.TeigGewicht = _TeigGewicht
            OptCharge.TeigGewicht = _TeigGewicht
        End Set
    End Property

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
            RaiseError(wb_Global.MinMaxOptChargenError.NoError)
        End Set
    End Property

    Public ReadOnly Property ErrorCode As wb_Global.MinMaxOptChargenError
        Get
            Return _ErrorCode
        End Get
    End Property

    ''' <summary>
    ''' Der Wert für die Minimal-Charge hat sich geändert: 
    '''     - Prüfen ob die Minimal-Charge kleiner als Optimal und/oder Maximal-Charge ist
    ''' </summary>
    Private Sub CheckMinCharge() Handles MinCharge.OnChange
        'Prüfen ob Min-Charge größer als Opt-Charge
        If MinCharge.cMengeInkg > OptCharge.cMengeInkg And OptCharge.cMengeInkg > 0 Then
            MinCharge.MengeInkg = OptCharge.MengeInkg
            RaiseError(wb_Global.MinMaxOptChargenError.MinGrOpt)
            Exit Sub
        End If
        'Prüfen ob Min-Charge größer als Max-Charge (Opt-Charge ist gleich der Max-Charge)
        If MinCharge.cMengeInkg > MaxCharge.cMengeInkg And MaxCharge.cMengeInkg > 0 Then
            MinCharge.MengeInkg = MaxCharge.MengeInkg
            RaiseError(wb_Global.MinMaxOptChargenError.MinGrMax)
            Exit Sub
        End If
        'Min-Charge wurde geändert - kein Fehler
        RaiseError(wb_Global.MinMaxOptChargenError.NoError)
    End Sub

    ''' <summary>
    ''' Der Wert für die Optimal-Charge hat sich geändert: 
    '''     - Prüfen ob die Optimal-Charge kleiner als Minimal-Charge oder größer als Maximal-Charge ist
    ''' </summary>
    Private Sub CheckOptCharge() Handles OptCharge.OnChange
        'Prüfen ob Opt-Charge kleiner als Min-Charge
        If OptCharge.cMengeInkg < MinCharge.cMengeInkg And MinCharge.cMengeInkg > 0 Then
            OptCharge.MengeInkg = MinCharge.MengeInkg
            RaiseError(wb_Global.MinMaxOptChargenError.OptKlMin)
            Exit Sub
        End If
        'Prüfen ob Opt-Charge größer als Max-Charge
        If OptCharge.cMengeInkg > MaxCharge.cMengeInkg And MaxCharge.cMengeInkg > 0 Then
            OptCharge.MengeInkg = MaxCharge.MengeInkg
            RaiseError(wb_Global.MinMaxOptChargenError.OptGrMax)
            Exit Sub
        End If
        'Opt-Charge wurde geändert - kein Fehler
        RaiseError(wb_Global.MinMaxOptChargenError.NoError)
    End Sub

    ''' <summary>
    ''' Der Wert für die Maximal-Charge hat sich geändert: 
    '''     - Prüfen ob die Maximal-Charge kleiner als Minimal-Charge oder kleiner als Optimal-Charge ist
    ''' </summary>
    Private Sub CheckMaxCharge() Handles MaxCharge.OnChange
        'Prüfen ob Max-Charge kleiner als Opt-Charge
        If MaxCharge.cMengeInkg < OptCharge.cMengeInkg And OptCharge.cMengeInkg > 0 Then
            MaxCharge.MengeInkg = OptCharge.MengeInkg
            RaiseError(wb_Global.MinMaxOptChargenError.MaxKlOpt)
            Exit Sub
        End If
        'Prüfen ob Max-Charge kleine als Min-Charge
        If MaxCharge.cMengeInkg < MinCharge.cMengeInkg And MinCharge.cMengeInkg > 0 Then
            MaxCharge.MengeInkg = MinCharge.MengeInkg
            RaiseError(wb_Global.MinMaxOptChargenError.MaxKlMin)
            Exit Sub
        End If
        'Max-Charge wurde geändert - kein Fehler
        RaiseError(wb_Global.MinMaxOptChargenError.NoError)
    End Sub

    Private Sub RaiseError(ErrCode As wb_Global.MinMaxOptChargenError)
        _ErrorCode = ErrCode
        RaiseEvent OnError(Me)
    End Sub

End Class
