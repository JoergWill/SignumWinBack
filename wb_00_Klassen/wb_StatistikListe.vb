<Serializable>
Public Class wb_StatistikListenElement

    Private _Nr As Integer
    Private _Nummer As String
    Private _Bezeichnung As String

    Public Property Nr As Integer
        Get
            Return _Nr
        End Get
        Set(value As Integer)
            _Nr = value
        End Set
    End Property

    Public Property Nummer As String
        Get
            Return _Nummer
        End Get
        Set(value As String)
            _Nummer = value
        End Set
    End Property

    Public Property Bezeichnung As String
        Get
            Return _Bezeichnung
        End Get
        Set(value As String)
            _Bezeichnung = value
        End Set
    End Property
End Class
