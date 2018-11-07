Public Class wb_Kocher_RezeptSchritt

    Private _Index As Integer = wb_Global.UNDEFINED
    Private _SchrittNr As Integer = wb_Global.UNDEFINED
    Private _ParamNr As Integer = wb_Global.UNDEFINED

    Private _RzptBezeichnung As String  'Parameter 1 - Schritt(0) Rezeptname 
    Private _RzptGesamtMenge As String  'Parameter 2 - Schritt(0) Rezept-Gesamtmenge

    Private _Laufzeit As Integer        'Parameter 1 - Laufzeit des Rezeptschrittes [Minuten]
    Private _TempOel As Double          'Parameter 2 - Solltemperatur Thermoöl [°C]
    Private _Heizen As Boolean          'Parameter 3 - Heizen/Kühlen aktiv [J/N]
    Private _RW_tEin As Integer         'Parameter 4 - Intervall Rührwerk ein [Minuten]
    Private _RW_tAus As Integer         'Parameter 5 - Intervall Rührwerk aus [Minuten]
    Private _TempKern As Double         'Parameter 6 - Soll Kerntemperatur [°C]
    Private _WasserMenge As Double      'Parameter 7 - Sollmenge Wasser [Liter]
    Private _RW_Geschw As Integer       'Parameter 8 - Rührwerk Geschwindigkeit [Hz]
    Private _TA As Integer              'Parameter 9 - Verhältnis Mehl/Wasser [TA]


    ''' <summary>
    ''' Berechnet (Set) Schritt-Nummer und Parameter aus dem Index (Zeilen-Nummer im Kocher-Rezept)
    ''' </summary>
    ''' <returns></returns>
    Public Property Index As Integer
        Get
            'Berechnet Index aus Schritt und Parameter-Nummer
            _Index = wb_Kocher_Global.ParamToIdx(_ParamNr, _SchrittNr)
            Return _Index
        End Get
        Set(value As Integer)
            _Index = value
            'Berechnet Schritt-Nummer
            _SchrittNr = wb_Kocher_Global.IdxToSchritt(value)
            'Berechnet Parameter-Nummer
            _ParamNr = wb_Kocher_Global.IdxToParam(value, _SchrittNr)
        End Set
    End Property

    Public Property SchrittNr As Integer
        Get
            Return _SchrittNr
        End Get
        Set(value As Integer)
            _SchrittNr = value
        End Set
    End Property

    Public Property ParamNr As Integer
        Get
            Return _ParamNr
        End Get
        Set(value As Integer)
            _ParamNr = value
        End Set
    End Property

    Public Property Parameter As String
        Get
            Select Case ParamNr

                'Parameter 1 - Laufzeit des Rezeptschrittes [Minuten]/RezeptBezeichnung
                Case 1
                    If SchrittNr = 0 Then
                        Return _RzptBezeichnung
                    Else
                        Return wb_Functions.FormatStr(_Laufzeit.ToString, 4, 7)
                    End If

                'Parameter 2 - Solltemperatur Thermoöl [°C]/Rezept-Gesamtmenge
                Case 2
                    If SchrittNr = 0 Then
                        Return wb_Functions.FormatStr(_RzptGesamtMenge.ToString, 4, 7)
                    Else
                        Return wb_Functions.FormatStr(_TempOel.ToString, 4, 7)
                    End If

                'Parameter 3 - Heizen/Kühlen aktiv [J/N]
                Case 3
                    If _Heizen Then
                        Return "0000001.0000"
                    Else
                        Return "0000000.0000"
                    End If

                'Parameter 4 - Intervall Rührwerk ein [Minuten]
                Case 4
                    Return wb_Functions.FormatStr(_RW_tEin.ToString, 4, 7)

                'Parameter 5 - Intervall Rührwerk aus [Minuten]
                Case 5
                    Return wb_Functions.FormatStr(_RW_tAus.ToString, 4, 7)

                'Parameter 6 - Soll Kerntemperatur [°C]
                Case 6
                    Return wb_Functions.FormatStr(_TempKern.ToString, 4, 7)

                'Parameter 7 - Sollmenge Wasser [Liter]
                Case 7
                    Return wb_Functions.FormatStr(_WasserMenge.ToString, 4, 7)

               'Parameter 8 - Rührwerk Geschwindigkeit [Hz]
                Case 8
                    Return wb_Functions.FormatStr(_RW_Geschw.ToString, 4, 7)

                'Parameter 9 - Verhältnis Mehl/Wasser [TA]
                Case 9
                    Return wb_Functions.FormatStr(_TA.ToString, 4, 7)

                Case Else
                    Return "0000000.0000"
            End Select
        End Get

        Set(value As String)
            Select Case ParamNr

                'Parameter 1 - Laufzeit des Rezeptschrittes [Minuten]/Rezeptname
                Case 1
                    If SchrittNr = 0 Then
                        _RzptBezeichnung = value
                    Else
                        _Laufzeit = wb_Functions.StrToInt(value)
                    End If

                'Parameter 2 - Solltemperatur Thermoöl [°C]/GesamtMenge
                Case 2
                    If SchrittNr = 0 Then
                        _RzptGesamtMenge = wb_Functions.StrToDouble(value)
                    Else
                        _TempOel = wb_Functions.StrToDouble(value)
                    End If

                'Parameter 3 - Heizen/Kühlen aktiv [J/N]
                Case 3
                    If value = "1" Then
                        _Heizen = True
                    Else
                        _Heizen = False
                    End If

                'Parameter 4 - Intervall Rührwerk ein [Minuten]
                Case 4
                    _RW_tEin = wb_Functions.StrToInt(value)

                'Parameter 5 - Intervall Rührwerk aus [Minuten]
                Case 5
                    _RW_tAus = wb_Functions.StrToInt(value)

                'Parameter 6 - Soll Kerntemperatur [°C]
                Case 6
                    _TempKern = wb_Functions.StrToDouble(value)

                'Parameter 7 - Sollmenge Wasser [Liter]
                Case 7
                    _WasserMenge = wb_Functions.StrToDouble(value)

               'Parameter 8 - Rührwerk Geschwindigkeit [Hz]
                Case 8
                    _RW_Geschw = wb_Functions.StrToInt(value)

                'Parameter 9 - Verhältnis Mehl/Wasser [TA]
                Case 9
                    _TA = wb_Functions.StrToInt(value)

            End Select
        End Set
    End Property

    Public Property Laufzeit As Integer
        Get
            Return _Laufzeit
        End Get
        Set(value As Integer)
            _Laufzeit = value
        End Set
    End Property

    Public Property TempOel As Double
        Get
            Return _TempOel
        End Get
        Set(value As Double)
            _TempOel = value
        End Set
    End Property

    Public Property Heizen As Boolean
        Get
            Return _Heizen
        End Get
        Set(value As Boolean)
            _Heizen = value
        End Set
    End Property

    Public Property RW_tEin As Integer
        Get
            Return _RW_tEin
        End Get
        Set(value As Integer)
            _RW_tEin = value
        End Set
    End Property

    Public Property RW_tAus As Integer
        Get
            Return _RW_tAus
        End Get
        Set(value As Integer)
            _RW_tAus = value
        End Set
    End Property

    Public Property TempKern As Double
        Get
            Return _TempKern
        End Get
        Set(value As Double)
            _TempKern = value
        End Set
    End Property

    Public Property WasserMenge As Double
        Get
            Return _WasserMenge
        End Get
        Set(value As Double)
            _WasserMenge = value
        End Set
    End Property

    Public Property RW_Geschw As Integer
        Get
            Return _RW_Geschw
        End Get
        Set(value As Integer)
            _RW_Geschw = value
        End Set
    End Property

    Public Property TA As Integer
        Get
            Return _TA
        End Get
        Set(value As Integer)
            _TA = value
        End Set
    End Property
End Class
