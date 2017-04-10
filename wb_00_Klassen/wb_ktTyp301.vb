Imports WinBack.wb_Functions
Imports WinBack.wb_Global

Public Class wb_ktTyp301
    Private Structure Typ301
        Public _Allergen As AllergenInfo
        Public _Naehrwert As Double
    End Structure
    Private NaehrwertInfo(maxTyp301) As Typ301
    Private _TimeStamp

    Public Property TimeStamp As DateTime
        Get
            Return _TimeStamp
        End Get
        Set(value As DateTime)
            _TimeStamp = value
        End Set
    End Property

    Public Function IsAllergen(index As Integer) As Boolean
        If index >= minTyp301Allergen And index <= maxTyp301Allergen Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Property Allergen(index As Integer) As AllergenInfo
        Get
            If IsAllergen(index) Then
                Return NaehrwertInfo(index)._Allergen
            Else
                Return AllergenInfo.ERR
            End If
        End Get
        Set(value As AllergenInfo)
            If IsAllergen(index) Then
                NaehrwertInfo(index)._Allergen = value
            End If
        End Set
    End Property

    Public Property Naehrwert(index As Integer) As Double
        Get
            If Not IsAllergen(index) Then
                Return NaehrwertInfo(index)._Naehrwert
            Else
                Return 0.0
            End If
        End Get
        Set(value As Double)
            If Not IsAllergen(index) Then
                NaehrwertInfo(index)._Naehrwert = value
            End If
        End Set
    End Property

    Public Property Wert(index As Integer) As VariantType
        Get
            If IsAllergen(index) Then
                Return NaehrwertInfo(index)._Allergen
            Else
                Return NaehrwertInfo(index)._Naehrwert
            End If
        End Get
        Set(value As VariantType)
            If IsAllergen(index) Then
                NaehrwertInfo(index)._Allergen = wb_Functions.StringtoAllergen(value)
            Else
                NaehrwertInfo(index)._Naehrwert = value
            End If
        End Set
    End Property

End Class
