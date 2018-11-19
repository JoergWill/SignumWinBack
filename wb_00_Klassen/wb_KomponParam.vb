Imports WinBack.wb_Global

Public Class wb_KomponParam

    Private _parentStep As wb_KomponParam
    Private _childSteps As New ArrayList()

    Private _TypNr As Integer
    Private _ParamNr As Integer
    Private _Bezeichnung As String
    Private _Wert As String
    Private _Used As Boolean

    '' <summary>
    '' Create a new step with the given parent
    '' </summary>
    '' <param parent="parent">The parent step</param>
    '' <param Bezeichnung="name">The name of this step</param>
    Public Sub New(parent As wb_KomponParam, TypNr As Integer, ParamNr As Integer, Bezeichnung As String)
        _parentStep = parent
        'Parameter-Type (200,210...401)
        _TypNr = TypNr
        'Parameter-Nummer
        _ParamNr = ParamNr
        'Parameter-Bezeichnung
        _Bezeichnung = Bezeichnung
        'Knoten in die Struktur einfügen
        If Not (_parentStep Is Nothing) Then
            parent._childSteps.Add(Me)
        End If
    End Sub 'New

    '' <summary>
    '' Parent dieses Komponenten-Parameters
    '' </summary>
    Public Property ParentStep() As wb_KomponParam
        Get
            Return _parentStep
        End Get
        Set(ByVal value As wb_KomponParam)
            _parentStep = value
        End Set
    End Property

    '' <summary>
    '' Liste aller Child-Parameter
    '' </summary>
    Public ReadOnly Property ChildSteps() As IList
        Get
            Return _childSteps
        End Get
    End Property

    Public ReadOnly Property TypNr As Integer
        Get
            Return _TypNr
        End Get
    End Property

    Public ReadOnly Property ParamNr As Integer
        Get
            Return _ParamNr
        End Get
    End Property

    Public Property Bezeichnung As String
        Get
            Return _Bezeichnung
        End Get
        Set(value As String)
            _Bezeichnung = value
        End Set
    End Property

    Public Property Wert As String
        Get
            Return _Wert
        End Get
        Set(value As String)
            _Wert = value
        End Set
    End Property
    ''' <summary>
    ''' Parameter-Wert abhängig von Komponenten-Type/Parameter-Type ausgeben
    ''' </summary>
    ''' <returns></returns>
    Public Property VirtualTree_Wert As String
        Get
            'Parameter-Nummer 0 ist die Überschrift
            If _ParamNr > 0 Then
                'abhängig vom Paramter/Komponenten-Typ
                Select Case _TypNr

                    'Komponenten-Type
                    Case < ktParam.kt200
                        Select Case eFormat
                            'String/Time/Boolean
                            Case 1, 4, 5
                                Return _Wert
                            'Integer/Real
                            Case 2, 3
                                Return wb_Functions.FormatStr(_Wert, 3)
                            Case Else
                                Return _Wert
                        End Select

                    'Parameter-Type
                    Case ktParam.kt301
                        If wb_KomponParam301_Global.IsAllergen(_ParamNr) Then
                            Return wb_Functions.AllergenToString(_Wert)
                        Else
                            Return wb_Functions.FormatStr(_Wert, 3)
                        End If

                        'alle anderen Parameter
                    Case Else
                        Return _Wert
                End Select
            Else
                Return ""
            End If
        End Get

        Set(value As String)
            'abhängig vom Paramter/Komponenten-Typ
            Select Case _TypNr
                Case ktParam.kt301
                    _Wert = wb_Functions.StringtoAllergen(value)
                Case Else
                    _Wert = value
            End Select
        End Set
    End Property

    Public ReadOnly Property VirtualTree_TypNr As String
        Get
            Return ""
        End Get
    End Property

    Public ReadOnly Property VirtualTree_ParamNr As String
        Get
            If ParamNr = 0 Then
                Return ""
            Else
                Return ParamNr.ToString
            End If
        End Get
    End Property

    Public ReadOnly Property Einheit As String
        Get
            Return " " & wb_KomponParam_Global.ktXXXParam(_TypNr, _ParamNr).Einheit
        End Get
    End Property

    Public ReadOnly Property eUG As String
        Get
            Return wb_KomponParam_Global.ktXXXParam(_TypNr, _ParamNr).eUG
        End Get
    End Property

    Public ReadOnly Property eOG As String
        Get
            Return wb_KomponParam_Global.ktXXXParam(_TypNr, _ParamNr).eOG
        End Get
    End Property

    Public ReadOnly Property eFormat As String
        Get
            Return wb_KomponParam_Global.ktXXXParam(_TypNr, _ParamNr).eFormat
        End Get
    End Property

    Public ReadOnly Property Used As Boolean
        Get
            Return wb_KomponParam_Global.ktXXXParam(_TypNr, _ParamNr).Used
        End Get
    End Property
End Class
