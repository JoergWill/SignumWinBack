Imports WinBack.wb_Functions
Imports WinBack.wb_Global

Public Class wb_KomponParam301
    Inherits wb_ChangeLog

    Private Structure Typ301
        Public _Allergen As AllergenInfo
        Public _Naehrwert As Double
    End Structure

    'Array aller Nährwerte/Allergene
    Private NaehrwertInfo(maxTyp301) As Typ301
    Private _TimeStamp
    Private _IsEmpty

    ''' <summary>
    ''' Nach der Initialisierung ist das Array leer
    ''' </summary>
    Public Sub New()
        IsEmpty = True
    End Sub

    ''' <summary>
    ''' Gibt True zurück, wenn das Array leer ist (Lesen aus DB erforderlich)
    ''' False, wenn Daten vorhanden sind.
    ''' </summary>
    ''' <returns>Boolean - True wenn Allergeninfo fehlt</returns>
    Public Property IsEmpty As Object
        Get
            Return _IsEmpty
        End Get
        Set(value As Object)
            _IsEmpty = value
        End Set
    End Property

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
                IsEmpty = False
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
                IsEmpty = False
            End If
        End Set
    End Property

    Public Property Wert(index As Integer) As String
        Get
            If IsAllergen(index) Then
                Return NaehrwertInfo(index)._Allergen
            Else
                Return NaehrwertInfo(index)._Naehrwert
            End If
        End Get
        Set(value As String)
            If IsAllergen(index) Then
                'Änderungen loggen
                NaehrwertInfo(index)._Allergen = ChangeLogAdd(LogType.Alg, index, NaehrwertInfo(index)._Allergen, StringtoAllergen(value))
            Else
                'Änderungen loggen
                NaehrwertInfo(index)._Naehrwert = ChangeLogAdd(LogType.Nrw, index, NaehrwertInfo(index)._Naehrwert, StrToDouble(value))
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
                        Allergen(idx) = AllergenInfo.C
                    Case "MAY_CONTAINED"
                        Allergen(idx) = AllergenInfo.T
                    Case Else
                        Allergen(idx) = AllergenInfo.ERR
                End Select
            Else
                Trace.WriteLine("Fehler bei DatenLink - Index " & index & " nicht definiert")
            End If
        End Set
    End Property

    Public Sub ClearReport()
        ChangeLogClear()
    End Sub

    Public Function GetReport(Optional ReportAll As Boolean = vbFalse) As String
        Return ChangeLogReport(ReportAll)
    End Function

    ''' <summary>
    ''' Addiert alle Nährwerte und Allergene zum übergebenen KomponentenParameter-Array
    ''' </summary>
    ''' <param name="_ktTyp301"></param>
    Public Sub AddNwt(ByRef _ktTyp301 As wb_KomponParam301)
        For i = 0 To maxTyp301
            If IsAllergen(i) Then
                _ktTyp301.Allergen(i) = AddNwtAllergen(_ktTyp301.Allergen(i), Allergen(i))
            Else
                _ktTyp301.Naehrwert(i) += Naehrwert(i)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Addiert die Allergen-Info. Bei der Addition wird der jeweils größere Wert zurückgegeben.
    ''' Die Reihenfolge der Kontanten in wb_global entspricht der Reihenfolge der Wertigkeit.
    ''' </summary>
    ''' <param name="Allergen1"></param>
    ''' <param name="Allergen2"></param>
    Private Function AddNwtAllergen(Allergen1 As AllergenInfo, Allergen2 As AllergenInfo) As AllergenInfo

        'die Aufzählung der Konstanten entspricht der Reihenfolge der Wertigkeit.
        If Allergen1 > Allergen2 Then
            Return Allergen2
        Else
            Return Allergen1
        End If
    End Function
End Class
