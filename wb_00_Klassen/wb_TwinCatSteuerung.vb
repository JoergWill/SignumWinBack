Public Class wb_TwinCatSteuerung
    Public TwinCatADS As New wb_TCatADS
    Friend _Ip As String
    Private _Port As Integer
    Friend _Connected As Boolean = False

    Public Property Ip As String
        Get
            Return _Ip
        End Get
        Set(value As String)
            _Ip = value
        End Set
    End Property
    Friend Property Port As Integer
        Get
            Return _Port
        End Get
        Set(value As Integer)
            _Port = value
        End Set
    End Property

    Sub New(IpAdresse As String, Optional ADSPort As Integer = 800)
        Ip = IpAdresse
        Port = ADSPort
    End Sub

    Public Property Connected As Boolean
        Get
            Return _Connected
        End Get
        Set(value As Boolean)
            _Connected = value
        End Set
    End Property


    Public Function TryConnect() As Boolean
        'Keine Verbindung zur SPS-Steuerung
        If Not TwinCatADS.IsConnected Then
            'Verbinden (Versuch)
            Connected = TwinCatADS.Connect(Ip, Port)
        End If

        Return Connected
    End Function

    Public Function TryDisconnect() As Boolean
        If TwinCatADS.IsConnected Then
            Return TwinCatADS.Disconnect
        End If
        Return False
    End Function

    Public Function ReadMem(Offset As Integer, AnzahlBytes As Integer) As Boolean
        Return TwinCatADS.ReadMem(Offset, AnzahlBytes)
    End Function

    Public Sub WriteMem(Offset As Integer, s As String)
        TwinCatADS.WriteString(Offset, s)
    End Sub

End Class
