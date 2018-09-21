Imports WinBack.wb_Global

Public Class wb_KomponParam

    Private _parentStep As wb_KomponParam
    Private _childSteps As New ArrayList()

    Private _TypNr As Integer
    Private _ParamNr As Integer
    Private _Bezeichnung As String
    Private _Wert As String
    Private _Einheit As String
    Private _Used As Boolean

    '' <summary>
    '' Create a new step with the given parent
    '' </summary>
    '' <param name="parent">The parent step</param>
    '' <param name="name">The name of this step</param>
    Public Sub New(parent As wb_KomponParam, TypNr As Integer, ParamNr As Integer, Bezeichnung As String)
        _parentStep = parent
        'Parameter-Type (200,210...401)
        _TypNr = TypNr
        'Parameter-Nummer
        _ParamNr = ParamNr
        'Parameter-Bezeichnung
        _Bezeichnung = Bezeichnung
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

    Public Property Einheit As String
        Get
            Return _Einheit
        End Get
        Set(value As String)
            _Einheit = value
        End Set
    End Property

    Public Property Used As Boolean
        Get
            Return _Used
        End Get
        Set(value As Boolean)
            _Used = value
        End Set
    End Property
End Class
