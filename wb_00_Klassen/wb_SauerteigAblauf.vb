Public Class wb_SauerteigAblauf

    Private _BehNr As Integer
    Private _KomponType As wb_Global.KomponTypen
    Private _StartTime As Date
    Private _EndTime As Date

    Public Property BehNr As Integer
        Get
            Return _BehNr
        End Get
        Set(value As Integer)
            _BehNr = value
        End Set
    End Property

    Public Property KomponType As wb_Global.KomponTypen
        Get
            Return _KomponType
        End Get
        Set(value As wb_Global.KomponTypen)
            _KomponType = value
        End Set
    End Property

    Public Property StartTime As Date
        Get
            Return _StartTime
        End Get
        Set(value As Date)
            _StartTime = value
        End Set
    End Property

    Public ReadOnly Property StartTimeUnix As String
        Get
            Return wb_Functions.TimeToUnix(_StartTime)
        End Get
    End Property

    Public Property EndTime As Date
        Get
            Return _EndTime
        End Get
        Set(value As Date)
            _EndTime = value
        End Set
    End Property

    Public ReadOnly Property EndTimeUnix As String
        Get
            Return wb_Functions.TimeToUnix(_EndTime)
        End Get
    End Property

End Class
