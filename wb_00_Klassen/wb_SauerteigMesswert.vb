Public Class wb_SauerteigMesswert

    Private _id As Integer
    Private _Wert As Double
    Private _UnixTimestamp As String

    Public Property Wert As Double
        Get
            Return _Wert
        End Get
        Set(value As Double)
            _Wert = value
        End Set
    End Property

    Public Property UnixTimestamp As String
        Get
            Return _UnixTimestamp
        End Get
        Set(value As String)
            _UnixTimestamp = value
        End Set
    End Property

    Public Property TimeStamp As Date
        Get
            Return wb_Functions.UnixToTime(_UnixTimestamp)
        End Get
        Set(value As Date)
            _UnixTimestamp = wb_Functions.TimeToUnix(value)
        End Set
    End Property

    Public Property Id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property
End Class
