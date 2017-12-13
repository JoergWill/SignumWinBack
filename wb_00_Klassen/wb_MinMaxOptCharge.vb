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
            _TeigGewicht = value
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
            _StkGewicht = value
            MinCharge.StkGewicht = _StkGewicht
            MaxCharge.StkGewicht = _StkGewicht
            OptCharge.StkGewicht = _StkGewicht
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
        'Error-Code zurücksetzen
        _ErrorCode = wb_Global.MinMaxOptChargenError.NoError

        'Prüfen ob Min-Charge größer als Opt-Charge
        If MinCharge.fMengeInkg > OptCharge.fMengeInkg And OptCharge.fMengeInkg > 0 Then
            RaiseError(wb_Global.MinMaxOptChargenError.MinGrOpt)
            MinCharge.MengeInkg = OptCharge.MengeInkg
            Exit Sub
        End If
        'Prüfen ob Min-Charge größer als Max-Charge (Opt-Charge ist gleich der Max-Charge)
        If MinCharge.fMengeInkg > MaxCharge.fMengeInkg And MaxCharge.fMengeInkg > 0 Then
            RaiseError(wb_Global.MinMaxOptChargenError.MinGrMax)
            MinCharge.MengeInkg = MaxCharge.MengeInkg
        End If
    End Sub

    Private Sub RaiseError(ErrCode As wb_Global.MinMaxOptChargenError)
        _ErrorCode = ErrCode
        RaiseEvent OnError(Me)
    End Sub

End Class
