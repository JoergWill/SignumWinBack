Imports System.Reflection
Imports System.Net.Sockets
Imports System.IO

Public Class wb_Main_ServerTaskConnect
    Private _TcpClient As New TcpClient
    Private _ip As String
    Private _port As String

    Public Delegate Sub messageDelegate(ByVal message As String)
    Public Event eMessage(ByVal Msg As String)

    ''' <summary>
    ''' IP-Connect zum WinBack-Server-Task-Prozess.
    ''' Versucht eine Verbindung zum WinBack-Server-Task über Port 22046 herzustellen. Die IP-Adresse entspricht der IP-Adresse des msSQL-Servers.
    ''' </summary>
    ''' <param name="ip"></param>
    ''' <param name="port"></param>
    Public Function Connect(ByVal ip As String, ByVal port As Integer, ReConnect As Boolean)
        'IP-Adresse und Port merken
        _ip = ip
        _port = port
        'Connect
        Try
            _TcpClient.Connect(ip, port) 'tries to connect
            If _TcpClient.Connected Then 'if connected, start the reading procedure
                _TcpClient.GetStream.BeginRead(New Byte() {0}, 0, 0, AddressOf DoRead, Nothing)
                Login() 'send our details to the server
                'connect OK
                Return True
            End If
        Catch ex As Exception
            If ReConnect Then
                System.Threading.Thread.Sleep(10000) 'if an error occurs sleep for 10 seconds
                Connect(ip, port, ReConnect) 'try to reconnect
            End If
        End Try
        'no Connect
        Return False
    End Function

    Private Sub Login()
        SendData("LOGIN|") 'log in to the chatserver
    End Sub

    Public Sub SendData(ByVal message As String)
        Dim sw As New StreamWriter(_TcpClient.GetStream) 'declare a new streamwriter
        sw.WriteLine(message) 'write the message
        sw.Flush()
    End Sub

    Private Sub DoRead(ByVal ar As IAsyncResult)
        Try
            Dim sr As New StreamReader(_TcpClient.GetStream) 'declare a new streamreader to read from the network stream
            Dim msg As String = sr.ReadLine() 'the msg is what is being read
            'MessageRecievedBox.Invoke(New messageDelegate(AddressOf MessageRecieved), New Object() {msg})
            'MessageRecieved(msg) 'start processing the message
            'Dim msd As MySubDelegate = AddressOf c1.sub1
            Dim msd As messageDelegate = AddressOf MessageRecieved
            msd.Invoke(msg)
            _TcpClient.GetStream.BeginRead(New Byte() {0}, 0, 0, AddressOf DoRead, Nothing) 'continue to read

        Catch ex As Exception
            System.Threading.Thread.Sleep(10000) 'if an error occurs, wait for 10 seconds
            Connect(_ip, _port, False) 'try to reconnect
        End Try
    End Sub

    ''' <summary>
    ''' Wertet die empfangenen Messages vom WinBack-Server-Task aus.
    ''' 
    '''     CHAT    - Chat-Funktion Client/Server
    '''     TIMR    - Änderungen in der Aktions-Timer-Tabelle
    '''     
    ''' </summary>
    ''' <param name="Message"></param>
    Private Sub MessageRecieved(ByVal Message As String)
        Dim msg() As String = Message.Split("|") ' if a message is recieved, split it to process it

        'mögliche Übertragungsfehler abfangen
        If msg.Length >= 2 Then
            Try
                'Message-Type
                Select Case msg(0) 'process it by the first element in the split array

                    Case "CHAT"
                        'Server/Client-Chat
                        'MessageRecievedBox.Text &= "Server: " & " " & msg(1) & vbNewLine

                    Case "TIMR"
                        'WinBack-Aktions-Timer-Tabelle. Änderungen/Events
                        RaiseEvent eMessage(msg(1))
                        'wb_Main_Shared.RefreshTimer(Nothing, msg(1))
                End Select
            Catch
            End Try
        End If
    End Sub

End Class
