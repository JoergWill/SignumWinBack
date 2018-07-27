Imports System.Net.Sockets
Imports Microsoft.Win32
Imports WinBack

Public Class CustomActions
    'Registry-Eintrag OrgaSoft
    Const ORGASOFTREGISTRY = "SOFTWARE\WOW6432Node\Signum GmbH Darmstadt\OrgaSoft.NET"
    Const ORGASOFTINI = "OrgaSoftINI"
    Const ORGASOFTNICHTINSTALLIERT = "NOINSTALL"

    'Durchsucht alle 255 Adressen im Netzwerk 
    Private Shared x(255) As PingWaiter
    Private Shared _ResultScans As Integer
    Private Shared _Servers As String

    ''' <summary>
    ''' Ermittelt den Pfad und Datei-Namen zur OrgaSoft.ini
    ''' Die Installation des WinBackAddIn erfolgt im Verzeichnis AddIN
    ''' </summary>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <CustomAction()>
    Public Shared Function VBCustomAction(ByVal session As Session) As ActionResult

        'IP-Adresse WinBack-Server
        session.Item("WINBACKIPADR") = GetWinBackIP()

        'Fehler - Kein WinBack-Server gefunden 
        If ResultScans = 0 Then
            MsgBox("Es konnte kein WinBack-Server im Netz gefunden werden ...")
        End If

        'Hinweis - Mehr als ein WinBack-Server gefunden 
        If ResultScans > 1 Then
            Try
                'Fehlermeldung zusammensetzen
                MsgBox("Es wurde mehr als ein WinBack-Server im Netzwerk gefunden ..." & vbCrLf & _Servers)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

        'Datenbank OrgaBack-Admin
        session.Item("WINBACKIPADR") = "172.16.17.5"
        'Datenbank OrgaBack-Main
        session.Item("WINBACKIPADR") = "172.16.17.5"
        session.Item("WINBACKIPADR") = "172.16.17.5"

        'Installations-Pfad (OrgaSoft.INI)
        Try
            'Auslesen der Registry
            'Computer\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Signum GmbH Darmstadt\OrgaSoft.NET
            Dim OrgaSoftIni As String = GetOrgaBackIni(ORGASOFTNICHTINSTALLIERT)
            If OrgaSoftIni = ORGASOFTNICHTINSTALLIERT Then
                MsgBox("Keine Installation von OrgaSoft.NET gefunden")
                Return ActionResult.Failure
            Else
                'Die Installation erfolgt unterhalb des OrgaBack-Pfades im Verzeichnis AddIn
                'ToDo aus OrgaSoft-DB auslesen - Prüfen ob Verzeichnis vorhanden ist
                Dim OrgaSoftPfad As String = IO.Path.GetDirectoryName(OrgaSoftIni)
                'ToDo REMOVE AFTER TEST
                OrgaSoftPfad = "C:\Temp\OrgaSoft"
                'Installationspfad ist unterhalb des OrgaBack-Verzeichnis
                session.SetTargetPath("TARGETDIR", OrgaSoftPfad)
            End If
        Catch ex As Exception
            MsgBox("Error " & ex.Message)
            Return ActionResult.Failure
        End Try

        Return ActionResult.Success
    End Function

    ''' <summary>
    ''' Der Pfad zur Datei OrgaSoft.ini steht in der Registry unter
    '''     Computer\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Signum GmbH Darmstadt\OrgaSoft.NET
    '''     
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetOrgaBackIni(pDefault As String) As String
        Try
            Using Key As RegistryKey = Registry.LocalMachine.OpenSubKey(ORGASOFTREGISTRY, RegistryKeyPermissionCheck.ReadSubTree)
                GetOrgaBackIni = CStr(Key.GetValue(ORGASOFTINI, pDefault))
            End Using
        Catch ex As Exception
            Return pDefault
        End Try
    End Function

    ''' <summary>
    ''' Scannt alle IP-Adressen im Netzwerk nach MySQL-Ports
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetWinBackIP() As String
        'Reset Counter(Shared)
        ResultScans = 0
        'Default-Rückgabewert
        GetWinBackIP = "127.0.0.1"

        'Lokale IP wird ermittelt
        Dim locip As String = ""
        For Each ip As System.Net.IPAddress In System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName)
            If ip.ToString.Contains(".") Then locip = ip.ToString
        Next

        'Die ersten drei Blöcke werden in 'ThreeBlocks' geschrieben
        Dim ThreeBlocks As String = ""
        Dim ind As Integer = 0
        For Each IPBlock As String In locip.Split(".")
            If ind = 3 Then Exit For
            ThreeBlocks &= IPBlock
            ThreeBlocks &= "."
            ind += 1
        Next

        'Start Scanning all IP-Addresses in Range 0..254 (Hintergrund-Task)
        For i = 0 To 254
            x(i) = New PingWaiter(ThreeBlocks & i)
        Next

        'Wait for all Tasks get ready
        For i = 0 To 254
            'Wait Task to be ready
            x(i).TaskWait()
            'Check Port 3305 and Port 5901
            If x(i).mip.Port5901 Then
                'IP-Adresse(n) auflisten 
                _Servers &= vbCrLf & "   " & x(i).mip.IPAddresse
                GetWinBackIP = x(i).mip.IPAddresse
            End If
        Next

        'No Match
    End Function

    Public Shared Property ResultScans As Integer
        Get
            Return _ResultScans
        End Get
        Set(value As Integer)
            _ResultScans = value
        End Set
    End Property

    Public Shared Sub AddResultScan()
        _ResultScans += 1
    End Sub

End Class


''' <summary>
''' Führt den eigentlichen Verbindungs-Versuch durch.
''' Erzeugt einen Thread, der in Hintergrund läuft und per Ping versucht, den Server
''' zu erreichen. Nach TimeOut oder Exception wird der Task beendet.
''' 
''' War der Verbindungs-Versuch erfolgreich wird versucht, die Ports 3305 und 5901 zu
''' erreichen. Sind beide Ports offen, handelt es sich um einen WinBack-Server.
''' </summary>
Public Class PingWaiter

    Private Task As New Threading.Thread(AddressOf PingTask)
    Public mip As IPInfo

    Public Structure IPInfo
        Public IPAddresse As String
        Public Connect As Boolean
        Public Port3306 As Boolean
        Public Port5901 As Boolean
    End Structure

    Public Sub New(ByVal Ip As String) 'Aufruf zum Starten des Ping
        mip.IPAddresse = Ip
        mip.Connect = False
        Task.Start()
    End Sub

    Public Sub TaskWait()
        'warte bis Thread beendet wurde
        Task.Join()
    End Sub

    Private Sub PingTask() 'führt den eigentlichen Ping mit 'ip' aus
        Dim x As New Net.NetworkInformation.Ping()
        Try
            'Verbindungs-Versuch auf die IP-Adresse
            If x.Send(mip.IPAddresse).Status = Net.NetworkInformation.IPStatus.Success Then
                mip.Connect = True
                'Port-Scan 3305
                If PortScan(3306) Then
                    mip.Port3306 = True
                    'Port-Scan 5901
                    If PortScan(5901) Then
                        mip.Port5901 = True
                        CustomActions.AddResultScan()
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function PortScan(Port As Integer) As Boolean
        Dim tcpClient As New TcpClient
        Try
            tcpClient.Connect(mip.IPAddresse, Port)
            tcpClient.Close()
            Return True
        Catch ex As Exception
        End Try
        Return False
    End Function

End Class