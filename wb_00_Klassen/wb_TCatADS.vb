Imports System.Text
Imports TwinCAT.Ads

Public Class wb_TCatADS

    Private TCclient As TcAdsClient
    Private TCstream As AdsStream
    Public BinReader As IO.BinaryReader

    ''' <summary>
    ''' Verbinden mit der TwinCat-SPS-Steuerung.
    '''     Für BC9120 - IP-Adresse/Port 800
    '''     Für CX10xx - TwinCat Net-ID/Port 801
    ''' </summary>
    ''' <param name="IpAdresse"></param>
    ''' <param name="Port"></param>
    ''' <returns></returns>
    Public Function Connect(IpAdresse As String, Optional Port As Integer = 800) As Boolean
        Try
            'Instanz der Klasse TcAdsClient erzeugen
            TCclient = New TcAdsClient
            'Verbindung mit Port 800 auf dem BC 9000
            TCclient.Connect(IpAdresse & ".1.1", Port)
        Catch ex As Exception
            Trace.WriteLine("E@_Fehler beim Verbinden auf BC9120 mit Adresse " & IpAdresse)
            Return False
        End Try

        'Try erkennt ob TwinCat läuft 
        Dim state As Object
        Try
            state = TCclient.ReadState.DeviceState()
            state = TCclient.ReadState.AdsState()
            state = TCclient.ClientCycle
        Catch ex As Exception
            Trace.WriteLine("E@_Fehler bei Abfrage Status BC9120 mit Adresse " & IpAdresse)
            Return False
        End Try
        Return True
    End Function

    Public Function Disconnect()
        'Return TCclient.Disconnect()
        TCclient.Dispose()
        Return True
    End Function

    Public Function IsConnected() As Boolean
        If Not IsNothing(TCclient) Then
            Return TCclient.IsConnected
        Else
            Return False
        End If
    End Function

    Public Function ReadMem(Offset As Integer, AnzZeichen As Integer) As Boolean
        'Lesen aus dem Merker-Bereich ab Adresse
        Return ReadAds(&H4020, Offset, AnzZeichen)
    End Function

    Private Function ReadAds(Index As Integer, Offset As Integer, AnzZeichen As Integer) As Boolean
        TCstream = New AdsStream(AnzZeichen)
        BinReader = New IO.BinaryReader(TCstream)
        'Daten von ADS-Stream lesen
        Dim Result As Integer = TCclient.Read(Index, Offset, TCstream)
        TCstream.Position = 0
        'Anzahl der Zeichen (gelesen)
        Return (Result > 0)
    End Function

    Public Sub WriteString(Offset As Integer, Value As String)
        'Der String wird in einen Stream geschrieben
        TCstream = New AdsStream(Value.Length + 10)
        Dim ADSwriter As New AdsBinaryWriter(TCstream)
        ADSwriter.Write(Value)
        'dieser Stream wird dann per ADS an die Steuerung übertragen
        TCclient.Write(&H4020, Offset, TCstream)
    End Sub
End Class
