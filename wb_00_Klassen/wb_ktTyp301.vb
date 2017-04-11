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

    Public WriteOnly Property dlNaehrWert(index As String) As String
        Set(value As String)
            Dim idx As Integer = DatenLinkToIndex(index)
            If idx > 0 Then
                Try
                    Naehrwert(idx) = CDbl(value)
                Catch ex As Exception
                    Trace.WriteLine("Fehler bei DatenLink - Index = " & index & " Wert = " & value)
                End Try
            Else
                Trace.WriteLine("Fehler bei DatenLink - Index " & index & " nicht definiert")
            End If
        End Set
    End Property

    Public WriteOnly Property dlAllergen(index As String) As String
        Set(value As String)
            Dim idx As Integer = DatenLinkToIndex(index)
            If idx > 0 Then
                Select Case value
                    Case "CONTAINED"
                        Allergen(idx) = wb_Global.AllergenInfo.C
                    Case "MAY_CONTAINED"
                        Allergen(idx) = wb_Global.AllergenInfo.T
                    Case Else
                        Allergen(idx) = wb_Global.AllergenInfo.ERR
                End Select
            Else
                Trace.WriteLine("Fehler bei DatenLink - Index " & index & " nicht definiert")
            End If
        End Set
    End Property
End Class
