Public Class wb_MehlGruppe
    Implements IComparable

    Private _MengeGesKg As Double = 0.0

    Public Property GruppeNr
    Public Property Bezeichnung

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        Return TryCast(obj, wb_MehlGruppe).MengeGesKg.CompareTo(MengeGesKg)
    End Function

    Public ReadOnly Property MengeGesKg As Double
        Get
            Return _MengeGesKg
        End Get
    End Property

    Public ReadOnly Property AnteilProzent(GesSumme As Double, RundungsFaktor As Integer) As Integer
        Get
            If GesSumme > 0 Then
                Return Convert.ToInt16(_MengeGesKg * 100 / (GesSumme * RundungsFaktor)) * RundungsFaktor
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property AnteilMehl(GesSumme As Double, RundungsFaktor As Integer) As String
        Get
            If _MengeGesKg > 0 Then
                Return AnteilProzent(GesSumme, RundungsFaktor).ToString("##") & "% " & Bezeichnung
            Else
                Return ""
            End If
        End Get
    End Property

    Public Function GetResultString(Result As String, Trennzeichen As String, GesSumme As Double, Optional RundungsFaktor As Integer = 5) As String
        If _MengeGesKg > 0 Then
            If Result <> "" Then
                Return Result & Trennzeichen & AnteilProzent(GesSumme, RundungsFaktor) & "% " & Bezeichnung
            Else
                Return AnteilProzent(GesSumme, RundungsFaktor) & "% " & Bezeichnung
            End If
        Else
            Return Result
        End If
    End Function

    Friend Sub ClearMenge()
        _MengeGesKg = 0
    End Sub

    Public Sub Add(Menge As Double)
        _MengeGesKg += Menge
    End Sub

End Class
