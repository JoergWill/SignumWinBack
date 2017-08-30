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

    Private Shared _AktUser As String = ""
    Private Shared _AktUserNr As String = ""

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

    Public Shared ReadOnly Property DockPanelPath(Optional DefaultPath As wb_Global.OrgaBackDockPanelLayoutPath = wb_Global.OrgaBackDockPanelLayoutPath.UserLokal)
        Get
            Dim WindowsTempPfad As String = System.IO.Path.GetTempPath
            Dim OrgaBackTempPfad As String = wb_GlobalOrgaBack.OrgaBackDockPanelPath
            Select Case _pVariante
                Case wb_Global.ProgVariante.OrgaBack

                    Select Case DefaultPath
                        Case wb_Global.OrgaBackDockPanelLayoutPath.UserLokal
                            OrgaBackTempPfad &= wb_GlobalOrgaBack.OrgaBackWorkStationNumber & "\"
                        Case wb_Global.OrgaBackDockPanelLayoutPath.ProgrammGlobal
                            OrgaBackTempPfad &= "00\"
                    End Select

                    If System.IO.Directory.Exists(OrgaBackTempPfad) Then
                        Return OrgaBackTempPfad
                    Else
                        Return WindowsTempPfad & "\"
                    End If

                Case wb_Global.ProgVariante.WinBack
                    Return WindowsTempPfad & "\"
                Case Else
                    Throw New NotImplementedException
            End Select
        End Get
    End Property

    Public Shared Property AktUser As String
        Get
            Return _AktUser
        End Get
        Set(value As String)
            _AktUser = value
        End Set
    End Property

    Public Shared Property AktUserNr As String
        Get
            Return _AktUserNr
        End Get
        Set(value As String)
            _AktUserNr = value
        End Set
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
