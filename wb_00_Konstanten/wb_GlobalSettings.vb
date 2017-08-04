Imports WinBack
''' <summary>
''' Shared Objekt. Hält alle globalen Programm-Einstellungen zentral in einem Objekt.
''' Ersetzt die projektabhängigen Settings.
''' 
''' Wenn eine Einstellung angefordert wird (Get) kann diese aus 
'''     - winback.ini zentral bzw. lokal gelesen werden
'''     - der WinBack-Datenbank direkt aus winback.Konfiguration
'''     - der OrgaBack-Datenbank aus ADMIN.dbo.Settings (über wb_GlobalOrgaBack)
'''     
''' Sind die Einstellungswerte schon gelesen worden, wird der Wert direkt zurückgegeben, ansonsten aus der Quelle gelesen (1x)
''' </summary>
Public Class wb_GlobalSettings
    Private Shared _pVariante As wb_Global.ProgVariante = wb_Global.ProgVariante.Undef

    Private Shared _MsSQLMainDB As String = Nothing
    Private Shared _MsSQLAdmnDB As String = Nothing
    Private Shared _MsSQLServer As String = Nothing

    Public Shared Property pVariante As wb_Global.ProgVariante
        Get
            Return _pVariante
        End Get
        Set(value As wb_Global.ProgVariante)
            _pVariante = value
        End Set
    End Property

    Private Shared Property MsSQLMain As String
        Get
            If _MsSQLMainDB Is Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MsSQLMainDB
        End Get
        Set(value As String)
            _MsSQLMainDB = value
        End Set
    End Property

    Private Shared Property MsSQLAdmn As String
        Get
            If _MsSQLAdmnDB Is Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MsSQLAdmnDB
        End Get
        Set(value As String)
            _MsSQLAdmnDB = value
        End Set
    End Property

    Private Shared Property MsSQLServer As String
        Get
            If _MsSQLServer Is Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MsSQLServer
        End Get
        Set(value As String)
            _MsSQLServer = value
        End Set
    End Property

    Public Shared ReadOnly Property OrgaBackMainConString As String
        Get
            Return "Data Source=" & MsSQLServer & "; Database=" & MsSQLMain & "; Integrated Security=True"
        End Get
    End Property

    Public Shared ReadOnly Property OrgaBackAdminConString As String
        Get
            Return "Data Source=" & MsSQLServer & "; Database=" & MsSQLAdmn & "; Integrated Security=True"
        End Get
    End Property

    Public Shared ReadOnly Property DockPanelPath
        Get
            'TODO Prüfen, ob der Pfad existiert - sonst anlegen !!!
            Select Case _pVariante
                Case wb_Global.ProgVariante.OrgaBack
                    Return wb_GlobalOrgaBack.OrgaBackDockPanelPath & wb_GlobalOrgaBack.OrgaBackWorkStationNumber & "\"
                Case wb_Global.ProgVariante.WinBack
                    'TODO Windows-Default Temp-Pfad ermitteln
                    Return My.Settings.WinBackDockPanelPath
                Case Else
                    Throw New NotImplementedException
            End Select
        End Get
    End Property

    Private Shared Sub getWinBackIni(Key As String)
        Dim IniFile As New wb_IniFile

        Select Case Key
            Case "SQL"
                _MsSQLMainDB = IniFile.ReadString("winback", "MsSQLServer_MainDB", "DemoOrgaBack_Main3")
                _MsSQLAdmnDB = IniFile.ReadString("winback", "MsSQLServer_AdmnDB", "DemoOrgaBack_Admin3")
                _MsSQLServer = IniFile.ReadString("winback", "MsSQLServer_Source", "WILL-WIN10\SIGNUM")
            Case Else

        End Select
    End Sub
End Class
