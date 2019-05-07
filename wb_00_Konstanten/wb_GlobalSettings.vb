Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports WinBack
Imports WinBack.wb_Global
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
''' 
''' Die Datei winback.ini liegt im Programm-Wurzel-Verzeichnis von OrgaSoft(C:\Program Files (x86)\Signum\OrgaSoft)
''' nicht im AddIn-Verzeichnis. Der Pfad des AddIn-Verzeichisses kann erst nach Initialisierung der Datenbank ausgelesen werden.
''' 
''' </summary>
Public Class wb_GlobalSettings
    Private Shared _pVariante As wb_Global.ProgVariante = wb_Global.ProgVariante.Undef
    Private Shared _AktLanguage As String = "de-DE"
    Private Shared _WinBackLanguage1 As Integer = UNDEFINED
    Private Shared _WinBackLanguage2 As Integer = UNDEFINED
    Private Shared _WinBackLanguageVariante As Integer = UNDEFINED
    Private Shared _MandantName As String = Nothing
    Private Shared _MandantNr As Integer = UNDEFINED
    Private Shared _OrgaBackMandantName As String = Nothing
    Private Shared _OrgaBackMandantNr As Integer = UNDEFINED

    Private Shared _pAddInPath As String = Nothing
    Private Shared _pListenPath As String
    Private Shared _pWinBackIniPath As String = Nothing
    Private Shared _pProgrammPath As String = ""
    Private Shared _pDatenPath As String = ""
    Private Shared _pExportPath As String = Nothing

    Private Shared _OrgaBackDBVersion As String = Nothing
    Private Shared _WinBackDBVersion As String = Nothing

    Private Shared _MsSQLMainDB As String = Nothing
    Private Shared _MsSQLAdmnDB As String = Nothing
    Private Shared _MsSQLServer As String = Nothing
    Private Shared _MsSQLUserId As String = Nothing
    Private Shared _MsSQLPasswd As String = Nothing

    Private Shared _osGrpRohstoffe As String = Nothing                        'Warengruppe Rohstoffe in OrgaBack
    Private Shared _osGrpBackwaren As String = Nothing                        'Warengruppe Verkaufsartikel(Backwaren) in OrgaBack
    Private Shared _osLaendercode As String = Nothing                         'Ländercode in OrgaBack (Update der Artikel/Komponenten-Nährwerte, Allergene und Deklaration
    Private Shared _osSprachCode As String = Nothing                          'Sprachencode in OrgaBack (Update der Artikel/Komponenten-Nährwerte, Allergene und Deklaration

    Private Shared _WinBackDBType As wb_Sql.dbType = wb_Sql.dbType.undef
    Private Shared _MySQLServerIP As String = Nothing
    Private Shared _MySQLWinBack As String = Nothing
    Private Shared _MySQLWbDaten As String = Nothing
    Private Shared _MySQLUser As String = Nothing
    Private Shared _MySQLPass As String = Nothing
    Private Shared _MySQLPath As String = Nothing

    Private Shared _MsSQLServerIP As String = Nothing
    Private Shared _MsSQLWinBack As String = Nothing
    Private Shared _MsSQLWbDaten As String = Nothing

    Private Shared _LogToTextFile As Integer = wb_Global.UNDEFINED
    Private Shared _LogToDataBase As Integer = wb_Global.UNDEFINED

    Private Shared _AktUserName As String = ""
    Private Shared _AktUserNr As Integer = wb_Global.UNDEFINED
    Private Shared _AktUserGruppe As Integer = -1

    Private Shared _SauerteigAnlage As Boolean = Nothing
    Private Shared _SauerteigAnzBeh As Integer = wb_Global.UNDEFINED
    Private Shared _IPBasisAdresse As String = Nothing

    Private Shared _ChargenTeiler As wb_Global.ModusChargenTeiler
    Private Shared _TeigOptimierung As wb_Global.ModusTeigOptimierung
    Private Shared _ProdPlanDatum As Date = Nothing
    Private Shared _ProdPlanfiliale As String = Nothing
    Private Shared _ProdPlanReadOnOpen As Boolean = False

    Private Shared _mHost = Nothing
    Private Shared _mSenderAddr = Nothing
    Private Shared _mSenderPass = Nothing

    Private Shared _ImportPathPistor = Nothing

    Public Shared Property MsSQLMain As String
        Get
            If _MsSQLMainDB Is Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MsSQLMainDB
        End Get
        Set(value As String)
            _MsSQLMainDB = value
            setWinBackIni("winback", "MsSQLServer_MainDB", value)
        End Set
    End Property

    Public Shared Property MsSQLAdmn As String
        Get
            If _MsSQLAdmnDB Is Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MsSQLAdmnDB
        End Get
        Set(value As String)
            _MsSQLAdmnDB = value
            setWinBackIni("winback", "MsSQLServer_AdmnDB", value)
        End Set
    End Property

    Public Shared Property MsSQLServer As String
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

    Public Shared Property MsSQLUserId As String
        Get
            If _MsSQLUserId Is Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MsSQLUserId
        End Get
        Set(value As String)
            _MsSQLUserId = value
        End Set
    End Property

    Public Shared Property MsSQLPasswd As String
        Get
            If _MsSQLPasswd Is Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MsSQLPasswd
        End Get
        Set(value As String)
            _MsSQLPasswd = value
        End Set
    End Property

    Public Shared ReadOnly Property OrgaBackMainConString As String
        Get
            If MsSQLUserId <> "xx" Then
                Return "Data Source=" & MsSQLServer & "; Database=" & MsSQLMain & "; user id=" & MsSQLUserId & "; password=" & MsSQLPasswd
            Else
                Return "Data Source=" & MsSQLServer & "; Database=" & MsSQLMain & "; Integrated Security=True"
            End If
        End Get
    End Property

    Public Shared ReadOnly Property OrgaBackAdminConString As String
        Get
            If MsSQLUserId <> "xx" Then
                Return "Data Source=" & MsSQLServer & "; Database=" & MsSQLAdmn & "; user id=" & MsSQLUserId & "; password=" & MsSQLPasswd
            Else
                Return "Data Source=" & MsSQLServer & "; Database=" & MsSQLAdmn & "; Integrated Security=True"
            End If
        End Get
    End Property

    Public Shared ReadOnly Property DockPanelPath(Optional DefaultPath As wb_Global.OrgaBackDockPanelLayoutPath = wb_Global.OrgaBackDockPanelLayoutPath.UserLokal)
        Get
            Dim WindowsTempPfad As String = System.IO.Path.GetTempPath
            Select Case _pVariante
                Case wb_Global.ProgVariante.OrgaBack

                    Dim OrgaBackTempPfad As String = wb_GlobalOrgaBack.OrgaBackDockPanelPath
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

    Public Shared Function AktUserLogin(Nummer As Integer)
        If wb_AktUser.Login(Nummer) Then
            wb_Language.SetLanguage(wb_AktUser.UserLanguage)
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function AktUserLogin(Name As String)
        If wb_AktUser.Login(Name) Then
            wb_Language.SetLanguage(wb_AktUser.UserLanguage)
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared ReadOnly Property AktUserName As String
        Get
            Return wb_AktUser.UserName
        End Get
    End Property

    Public Shared ReadOnly Property AktUserNr As Integer
        Get
            Return wb_AktUser.UserNr
        End Get
    End Property

    Public Shared ReadOnly Property AktUserGruppe As Integer
        Get
            Return wb_AktUser.UserGruppe
        End Get
    End Property

    Public Shared ReadOnly Property DataGridAlternateRowColor As Color
        Get
            Return System.Drawing.Color.LightGray
        End Get
    End Property

    Public Shared Property LogToTextFile As Boolean
        Get
            If _LogToTextFile = wb_Global.UNDEFINED Then
                getWinBackIni("Logger")
            End If
            Return (_LogToTextFile = wbTRUE)
        End Get
        Set(value As Boolean)
            If Convert.ToInt16(value) <> _LogToTextFile Then
                _LogToTextFile = Convert.ToInt16(value)

                Dim IniFile As New wb_IniFile
                IniFile.WriteInt("winback", "LogToTextFile", _LogToTextFile)
            End If
        End Set
    End Property

    Public Shared Property logToDataBase As Boolean
        Get
            If _LogToDataBase = wb_Global.UNDEFINED Then
                getWinBackIni("Logger")
            End If
            Return (_LogToDataBase = wbTRUE)
        End Get
        Set(value As Boolean)
            If Convert.ToInt16(value) <> _LogToDataBase Then
                _LogToDataBase = Convert.ToInt16(value)

                Dim IniFile As New wb_IniFile
                IniFile.WriteInt("winback", "LogToDataBase", _LogToDataBase)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Liefert den Connection-String für die WinBack-Datenbank zurück.
    '''     Für mysql wird mit "ConvertZeroDateTime=True" der Konvertierungs-Fehler bei der Umwandlung der TimeStamp-Daten unterdrückt
    '''     (siehe https://www.connectionstrings.com/mysql/)
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property SqlConWinBack As String
        Get
            Select Case wb_GlobalSettings.WinBackDBType
                Case wb_Sql.dbType.mySql
                    Return "server=" & MySQLServerIP & ";" & "user id=" & MySQLUser & ";" & "password=" & MySQLPass & ";" & "database=" & MySQLWinBack & ";" & "ConvertZeroDateTime=True,"
                Case wb_Sql.dbType.msSql
                    If MsSQLUserId <> "xx" Then
                        Return "Data Source=" & MsSQLServer & "; Database=" & MySQLWinBack & "; user id=" & MsSQLUserId & "; password=" & MsSQLPasswd
                    Else
                        Return "Data Source=" & MsSQLServer & "; Database=" & MySQLWinBack & "; Integrated Security=True"
                    End If
                Case Else
                    Throw New NotImplementedException
            End Select
        End Get
    End Property

    Public Shared ReadOnly Property SqlConWbDaten As String
        Get
            Select Case wb_GlobalSettings.WinBackDBType
                Case wb_Sql.dbType.mySql
                    Return "server=" & MySQLServerIP & ";" & "user id=" & MySQLUser & ";" & "password=" & MySQLPass & ";" & "database=" & MySQLWbDaten & ";" & "ConvertZeroDateTime=True;"
                Case wb_Sql.dbType.msSql
                    If MsSQLUserId <> "xx" Then
                        Return "Data Source=" & MsSQLServer & "; Database=" & MySQLWbDaten & "; user id=" & MsSQLUserId & "; password=" & MsSQLPasswd
                    Else
                        Return "Data Source=" & MsSQLServer & "; Database=" & MySQLWbDaten & "; Integrated Security=True"
                    End If
                Case Else
                    Throw New NotImplementedException
            End Select
        End Get
    End Property

    Public Shared Property MySQLPath As String
        Get
            If _MySQLPath = Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MySQLPath
        End Get
        Set(value As String)
            _MySQLPath = value
        End Set
    End Property

    Public Shared Property WinBackDBType As wb_Sql.dbType
        Get
            If _WinBackDBType = wb_Sql.dbType.undef Then
                getWinBackIni("SQL")
            End If
            Return _WinBackDBType
        End Get
        Set(value As wb_Sql.dbType)
            _WinBackDBType = value
        End Set
    End Property

    Public Shared Property MySQLServerIP As String
        Get
            If _MySQLServerIP = Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MySQLServerIP
        End Get
        Set(value As String)
            _MySQLServerIP = value
        End Set
    End Property

    Public Shared Property MySQLWinBack As String
        Get
            If _MySQLWinBack = Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MySQLWinBack
        End Get
        Set(value As String)
            _MySQLWinBack = value
        End Set
    End Property

    Public Shared Property MySQLWbDaten As String
        Get
            If _MySQLWbDaten = Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MySQLWbDaten
        End Get
        Set(value As String)
            _MySQLWbDaten = value
        End Set
    End Property

    Public Shared Property MySQLUser As String
        Get
            If _MySQLUser = Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MySQLUser
        End Get
        Set(value As String)
            _MySQLUser = value
        End Set
    End Property

    Public Shared Property MySQLPass As String
        Get
            If _MySQLPass = Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MySQLPass
        End Get
        Set(value As String)
            _MySQLPass = value
        End Set
    End Property

    Public Shared Property MsSQLServerIP As String
        Get
            If _MsSQLServerIP = Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MsSQLServerIP
        End Get
        Set(value As String)
            _MsSQLServerIP = value
        End Set
    End Property

    Public Shared Property MsSQLWinBack As String
        Get
            If _MsSQLWinBack = Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MsSQLWinBack
        End Get
        Set(value As String)
            _MsSQLWinBack = value
        End Set
    End Property

    Public Shared Property MsSQLWbDaten As String
        Get
            If _MsSQLWbDaten = Nothing Then
                getWinBackIni("SQL")
            End If
            Return _MsSQLWbDaten
        End Get
        Set(value As String)
            _MsSQLWbDaten = value
        End Set
    End Property

    Public Shared ReadOnly Property OrgaBackVersion As String
        Get
            If _pVariante = wb_Global.ProgVariante.OrgaBack Then
                Return Assembly.GetEntryAssembly().GetName().Version.ToString
            Else
                Return ""
            End If
        End Get
    End Property

    Public Shared ReadOnly Property OrgaBackDBVersion As String
        Get
            If _OrgaBackDBVersion Is Nothing Then
                _OrgaBackDBVersion = ""
                Dim Orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
                If Orgaback.sqlSelect(wb_Sql_Selects.mssqlSystemInfo) Then
                    If Orgaback.Read Then
                        _OrgaBackDBVersion = Orgaback.sField("DBVersion")
                    End If
                    Orgaback.Close()
                End If
            End If
            Return _OrgaBackDBVersion
        End Get
    End Property

    Public Shared ReadOnly Property SignumContractsVersion As String
        Get
            If _pVariante = ProgVariante.OrgaBack Then
                Return FileVersionInfo.GetVersionInfo(wb_GlobalSettings.pProgrammPath & "Signum.OrgaSoft.Contracts.dll").FileVersion
            Else
                Return "V0.0.0"
            End If
        End Get
    End Property

    Public Shared ReadOnly Property WinBackVersion As String
        Get
            If _pVariante = wb_Global.ProgVariante.OrgaBack Then
                Return Assembly.GetExecutingAssembly().GetName().Version.ToString
            Else
                Return My.Application.Info.Version.ToString
            End If
        End Get
    End Property

    Public Shared Property WinBackDBVersion As String
        Get
            If _WinBackDBVersion Is Nothing Then
                _WinBackDBVersion = ""
                getWinBackKonfiguration()
            End If
            Return _WinBackDBVersion
        End Get
        Set(value As String)
            _WinBackDBVersion = value
        End Set
    End Property

    Public Shared Property pAddInPath As String
        Get
            Return _pAddInPath
        End Get
        'wird in wb_Main_Menu gesetzt
        Set(value As String)
            _pAddInPath = value
        End Set
    End Property

    Public Shared ReadOnly Property pDBUpdatePath As String
        Get
            Return _pAddInPath & "DBUpdate\"
        End Get
    End Property

    Public Shared ReadOnly Property pWinBackUpdatePath As String
        Get
            Return _pAddInPath & "Update\"
        End Get
    End Property

    Public Shared ReadOnly Property pKocherPath As String
        Get
            Return _pAddInPath & "Temp\"
        End Get
    End Property

    ''' <summary>
    ''' Pfad für die ListUndLabel-Listen-Files
    ''' Im Debug-Modus wird als Verzeichnis direkt das Repository zurückgegeben, damit werden alle Änderungen automatisch synchronisiert.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property pListenPath As String
        Get
            If _pListenPath = Nothing Then
                If pVariante = wb_Global.ProgVariante.WinBack Then
#If DEBUG Then
                    _pListenPath = pProgrammPath
                    _pListenPath = _pListenPath.Replace("WinBackStart\bin\Messe", "ListLabel")
                    _pListenPath = _pListenPath.Replace("WinBackStart\bin\Debug-M5", "ListLabel")
                    _pListenPath = _pListenPath.Replace("WinBackStart\bin\Debug-M4", "ListLabel")
                    _pListenPath = _pListenPath.Replace("WinBackStart\bin\Debug-M3", "ListLabel")
                    _pListenPath = _pListenPath.Replace("WinBackStart\bin\Debug-M2", "ListLabel")
                    _pListenPath = _pListenPath.Replace("WinBackStart\bin\Debug-M1", "ListLabel")
                    _pListenPath = _pListenPath.Replace("WinBackStart\bin\Debug", "ListLabel")
#Else
                    _pListenPath = PProgrammPath & "Listen\"
#End If
                End If
            End If
            Return _pListenPath
        End Get
        Set(value As String)
            _pListenPath = value
        End Set
    End Property

    Public Shared Property OsGrpRohstoffe As String
        Get
            If _osGrpRohstoffe Is Nothing Then
                getWinBackIni("OrgaBack")
            End If
            Return _osGrpRohstoffe
        End Get
        Set(value As String)
            _osGrpRohstoffe = value
        End Set
    End Property

    Public Shared Property OsGrpBackwaren As String
        Get
            If _osGrpBackwaren Is Nothing Then
                getWinBackIni("OrgaBack")
            End If
            Return _osGrpBackwaren
        End Get
        Set(value As String)
            _osGrpBackwaren = value
        End Set
    End Property

    Public Shared Property osLaendercode As String
        Get
            If _osLaendercode Is Nothing Then
                getWinBackIni("OrgaBack")
            End If
            Return _osLaendercode
        End Get
        Set(value As String)
            _osLaendercode = value
            setWinBackIni("OrgaBack", "LaenderCode", value)
        End Set
    End Property

    Public Shared Property osSprachcode As String
        Get
            If _osSprachCode Is Nothing Then
                getWinBackIni("OrgaBack")
            End If
            Return _osSprachCode
        End Get
        Set(value As String)
            _osSprachCode = value
            setWinBackIni("OrgaBack", "SprachCode", value)
        End Set
    End Property

    ''' <summary>
    ''' Setzt/gibt den Pfad zur winback.ini zurück.
    ''' 
    ''' Läuft das Programm als OrgaBack-AddIn ist der Pfad für die winback.ini im Verzeichnis OrgaSoft
    ''' Das Addin läuft in OrgaSoft/Addin
    ''' 
    ''' Das Hintergrund-Programm WinBack-Server-Task startet im Verzeichnis OrgaSoft/AddIn, der Verweis
    ''' auf die winback.ini liegt damit eine Ebene tiefer.
    ''' Für den Debugger wird der Pfad zur winback.ini als Parameter übergeben, da sonst der Programm-Pfad
    ''' verwendet werden muss.
    '''
    ''' als winback.exe im Standalone-Betrieb liegt die winback.ini im Programm-Verzeichnis
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property pWinBackIniPath As String
        Get
            If _pWinBackIniPath = Nothing Then
                Select Case _pVariante
                    Case wb_Global.ProgVariante.OBServerTask
                        'die Server-Task startet im AddIn-Verzeichnis, der Pfad zur winback.ini liegt eine Ebene davor
                        _pWinBackIniPath = Path.GetDirectoryName(pProgrammPath)
#If DEBUG Then
                        _pWinBackIniPath = _pWinBackIniPath.Substring(0, _pWinBackIniPath.LastIndexOf("\"))
                        _pWinBackIniPath = _pWinBackIniPath.Substring(0, _pWinBackIniPath.LastIndexOf("\"))
                        _pWinBackIniPath = _pWinBackIniPath.Substring(0, _pWinBackIniPath.LastIndexOf("\")) & "\WinBack.ini"
#Else
                        _pWinBackIniPath = _pWinBackIniPath.Substring(0, _pWinBackIniPath.LastIndexOf("\")) & "\WinBack.ini"
#End If
                    Case wb_Global.ProgVariante.WBServerTask
                        _pWinBackIniPath = _pWinBackIniPath & "\WinBack.ini"

                    Case wb_Global.ProgVariante.OrgaBack
                        'die winback.ini liegt direkt über dem AddIn-Pfad '..\OrgaBack\AddIn
                        Dim directoryInfo As System.IO.DirectoryInfo = Directory.GetParent(pAddInPath)
                        _pWinBackIniPath = directoryInfo.Parent.FullName & "\WinBack.ini"
                    Case wb_Global.ProgVariante.WinBack
                        'die winback.ini liegt im Programm-Verzeichnis
                        'TODO User-Verzeichnis
                        _pWinBackIniPath = pProgrammPath & "\WinBack.ini"
                    Case Else
                        'die winback.ini liegt direkt über dem AddIn-Pfad '..\OrgaBack\AddIn
                        Dim directoryInfo As System.IO.DirectoryInfo = Directory.GetParent(pAddInPath)
                        _pWinBackIniPath = directoryInfo.Parent.FullName & "\WinBack.ini"
                End Select
            End If

            'Fehlermeldung ausgeben, wenn winback.ini nicht vorhanden ist
            If Not System.IO.File.Exists(_pWinBackIniPath) Then
                MsgBox("Die Konfiguration 'winback.ini' kann nicht gefunden werden " & vbCrLf & "Such-Pfad :" & _pWinBackIniPath, MsgBoxStyle.Critical)
            End If

            Return _pWinBackIniPath
        End Get
        Set(value As String)
            _pWinBackIniPath = value
        End Set
    End Property

    Public Shared ReadOnly Property pVersionTxtPath As String
        Get
            Return pProgrammPath & "Version.txt"
        End Get
    End Property

    Public Shared ReadOnly Property pColorThemePath As String
        Get
            Return pProgrammPath & "WinBackTheme.ini"
        End Get
    End Property

    Public Shared Property pProgrammPath As String
        Get
            If _pProgrammPath = Nothing Then
                _pProgrammPath = My.Application.Info.DirectoryPath & "\"
                Debug.Print("Programm-Pfad " & _pProgrammPath)
            End If
                Return _pProgrammPath
        End Get
        Set(value As String)
            _pProgrammPath = value
        End Set
    End Property

    Public Shared Property pDatenPath As String
        Get
            Return _pDatenPath
        End Get
        Set(value As String)
            _pDatenPath = value
        End Set
    End Property

    Public Shared ReadOnly Property pTempPath As String
        Get
            Try
                Return IO.Path.GetTempPath()
            Catch ex As Exception
                Return "C:\"
            End Try
        End Get
    End Property

    Public Shared ReadOnly Property SauerteigAnlage As Boolean
        Get
            If _SauerteigAnlage = Nothing Then
                _SauerteigAnlage = (wb_sql_Functions.Lookup("BC9000Liste", "BC9_aktiv", "BC9_IpAdresse='11'") = "1")
            End If
            Return _SauerteigAnlage
        End Get
    End Property

    Public Shared ReadOnly Property SauerteigAnzBeh As Integer
        Get
            If _SauerteigAnzBeh = wb_Global.UNDEFINED Then
                getWinBackKonfiguration()
            End If
            Return _SauerteigAnzBeh
        End Get
    End Property

    Public Shared Property IPBasisAdresse As String
        Get
            If _IPBasisAdresse = Nothing Then
                getWinBackKonfiguration()
            End If
            Return _IPBasisAdresse
        End Get
        Set(value As String)
            _IPBasisAdresse = value
        End Set
    End Property

    Public Shared Property pExportPath As String
        Get
            If _pExportPath = Nothing Then
                _pExportPath = System.IO.Path.GetTempPath
            End If
            Return _pExportPath
        End Get
        Set(value As String)
            _pExportPath = value
        End Set
    End Property

    Public Shared Property ChargenTeiler As ModusChargenTeiler
        Get
            If _ChargenTeiler = Nothing Then
                getWinBackIni("Produktion")
            End If
            Return _ChargenTeiler
        End Get
        Set(value As ModusChargenTeiler)
            _ChargenTeiler = value
            setWinBackIni("Produktion", "ChargenTeiler", value)
        End Set
    End Property

    Public Shared Property TeigOptimierung As ModusTeigOptimierung
        Get
            If _TeigOptimierung = Nothing Then
                getWinBackIni("Produktion")
            End If
            Return _TeigOptimierung
        End Get
        Set(value As ModusTeigOptimierung)
            _TeigOptimierung = value
            setWinBackIni("Produktion", "TeigOptimierung", value)
        End Set
    End Property

    Public Shared Property ProdPlanDatum As String
        Get
            If _ProdPlanDatum = Nothing Then
                _ProdPlanDatum = DateTime.Today.AddDays(1)
            End If
            Return _ProdPlanDatum
        End Get
        Set(value As String)
            _ProdPlanDatum = wb_Functions.ConvertUSDateStringToDate(value)
        End Set
    End Property

    Public Shared Property ProdPlanfiliale As String
        Get
            Return _ProdPlanfiliale
        End Get
        Set(value As String)
            _ProdPlanfiliale = value
        End Set
    End Property

    Public Shared Property ProdPlanReadOnOpen As Boolean
        Get
            Return _ProdPlanReadOnOpen
        End Get
        Set(value As Boolean)
            _ProdPlanReadOnOpen = value
        End Set
    End Property

    Public Shared Property AktLanguage As String
        Get
            Return _AktLanguage
        End Get
        Set(value As String)
            _AktLanguage = value
        End Set
    End Property

    Public Shared ReadOnly Property ConvertMySQL_CodePage As MySqlCodepage
        Get
            If _WinBackLanguage1 = UNDEFINED Then
                getWinBackKonfiguration()
            End If

            Return wb_Language.GetLanguageSQLCodePage(_WinBackLanguage1)
        End Get
    End Property

    ''' <summary>
    ''' Anzeige des Kommentarfeldes anstelle der Bezeichnung für Installationen im Ausland.
    ''' Wenn die WinBack.Konfiguration.SprachenVariante eingeschaltet ist (1) wird anstelle der Komponenten/Artikel/Rezeptbezeichnung das
    ''' entsprechende Kommentarfeld eingeblendet. Dies passiert nur, wenn die eingestellte Sprache in WinBack-Office gleiche der Sprache2
    ''' in der WinBack Konfiguration ist.
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property KommentarStattBezeichnung As Boolean
        Get
            If _WinBackLanguage2 = UNDEFINED Then
                getWinBackKonfiguration()
            End If
            If _WinBackLanguageVariante = 1 And (_WinBackLanguage2 = wb_Language.GetLanguageNr) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Shared Property MandantName As String
        Get
            If _MandantName Is Nothing Then
                getWinBackKonfiguration()
            End If
            Return _MandantName
        End Get
        Set(value As String)
            _MandantName = value
            'TODO Name setzen - DB und Konfig neu lesen
        End Set
    End Property

    Public Shared Property MandantNr As Integer
        Get
            Return _MandantNr
        End Get
        Set(value As Integer)
            _MandantNr = value
        End Set
    End Property

    Public Shared ReadOnly Property OrgaBackMandantName As String
        Get
            If _OrgaBackMandantName = Nothing Then
                GetOrgaBackMandant()
            End If
            Return _OrgaBackMandantName
        End Get
    End Property

    Public Shared ReadOnly Property OrgaBackMandantNr As Integer
        Get
            If _OrgaBackMandantNr = UNDEFINED Then
                GetOrgaBackMandant()
            End If
            Return _OrgaBackMandantNr
        End Get
    End Property

    Public Shared Property mHost As String
        Get
            If _mHost = Nothing Then
                getWinBackIni("Mail")
            End If
            Return _mHost
        End Get
        Set(value As String)
            _mHost = value
            setWinBackIni("smtp", "smtpHost", value)
        End Set
    End Property

    Public Shared Property mSenderAddr As String
        Get
            If _mSenderAddr = Nothing Then
                getWinBackIni("Mail")
            End If
            Return _mSenderAddr
        End Get
        Set(value As String)
            _mSenderAddr = value
            setWinBackIni("smtp", "smtpUser", value)
        End Set
    End Property

    Public Shared Property mSenderPass As String
        Get
            If _mSenderPass = Nothing Then
                getWinBackIni("Mail")
            End If
            Return _mSenderPass
        End Get
        Set(value As String)
            _mSenderPass = value
            setWinBackIni("smtp", "smtpPass", value)
        End Set
    End Property

    Public Shared Property ImportPathPistor As Object
        Get
            If _ImportPathPistor = Nothing Then
                getWinBackIni("Path")
            End If
            Return _ImportPathPistor
        End Get
        Set(value As Object)
            _ImportPathPistor = value
            setWinBackIni("Path", "Pistor", value)
        End Set
    End Property

    Private Shared Sub GetOrgaBackMandant()
        'xml-File OrgaBack.ini aus DatenPfad einlesen
        Dim XMLReader As Xml.XmlReader = New Xml.XmlTextReader(pDatenPath & "OrgaSoft.ini")
        'Anzahl der Mandanten
        Dim MandantNr As Integer = 0
        Dim MandantName As String = ""
        Dim AdminDBName As String = ""

        'alle Mandanten durchlaufen bis Admin-DB gefunden
        With XMLReader
            Do While .Read
                Select Case .NodeType

                    'Ein Element 
                    Case Xml.XmlNodeType.Element
                        'Name
                        Select Case .Name
                            'Mandant
                            Case "Mandant"
                                MandantNr += 1
                                'mehrere Attribute
                                If .AttributeCount > 0 Then
                                    While .MoveToNextAttribute
                                        'Attribut-Name
                                        Select Case .Name
                                            'Mandant-Name
                                            Case "Mandantname"
                                                MandantName = .Value
                                            'Admin-Datenbank
                                            Case "AdminDatabaseName"
                                                AdminDBName = .Value
                                        End Select
                                    End While
                                End If
                        End Select

                        'Wenn die Admin-Datenbank mit der aktuelle Admin-DB übereinstimmt, ist der Mandant gefunden
                        If AdminDBName = _MsSQLAdmnDB Then
                            _OrgaBackMandantName = MandantName
                            _OrgaBackMandantNr = MandantNr
                        End If
                End Select
            Loop
            .Close()
        End With
    End Sub

    Public Shared Function GetFileName(Tabelle As String) As String
        Return pExportPath & Tabelle & "_" & DateTime.Now.ToString("yyMMdd") & "_" & DateTime.Now.ToString("hhmmss") & ".csv"
    End Function

    Private Shared Sub setWinBackIni(Section As String, Key As String, value As String)
        Dim Inifile As New wb_IniFile
        Inifile.WriteString(Section, Key, value)
        Inifile = Nothing
    End Sub

    ''' <summary>
    ''' [smtp]
    ''' smtpUser=server@winback.de
    ''' smtpPass=de2la6de2
    ''' smtpHost=smtp.1und1.de
    ''' smtpPort=25
    ''' smtpAuth=atLOGIN
    ''' smtpFrom=server@winback.de
    ''' smtpMailAdr=service@winback.de
    ''' smtpSubject=Chargenauswertung WinBack
    ''' smtpPrio=mpNormal
    ''' </summary>
    ''' <param name="Key"></param>
    Private Shared Sub getWinBackIni(Key As String)
        Dim IniFile As New wb_IniFile

        'Keys aufgeteilt nach Mandant-Nummer
        Dim IniWinBack_Mandant As String = "winback-m" & MandantNr.ToString
        Dim IniOrgaBack_Mandant As String = "orgaback-m" & MandantNr.ToString

        Select Case Key
            Case "SQL"
                _WinBackDBType = wb_Functions.StringToDBType(IniFile.ReadString("winback", "DBType", "MySQL"))
                _MySQLServerIP = IniFile.ReadString("winback", "eMySQLServerIP", Environment.MachineName)
                _MySQLWinBack = IniFile.ReadString("winback", "eMySQLDatabase", "winback")
                _MySQLWbDaten = IniFile.ReadString("winback", "eMySQLDatabaseDaten", "wbdaten")
                _MySQLUser = IniFile.ReadString("winback", "eMySQLUser", "herbst")
                _MySQLPass = IniFile.ReadString("winback", "eMySQLPasswordDatabase", "herbst")

                'Default-Wert für die IP-Adresse is der Rechner-Name sonst funktioniert der Zugriff auf die MS-SQL2014-Datenbank nicht
                _MsSQLServerIP = IniFile.ReadString("winback", "eMsSQLServerIP", Environment.MachineName)
                _MsSQLWinBack = IniFile.ReadString("winback", "eMsSQLDatabase", "winback")
                _MsSQLWbDaten = IniFile.ReadString("winback", "eMsSQLDatabaseDaten", "wbdaten")

                _MsSQLMainDB = IniFile.ReadString("winback", "MsSQLServer_MainDB", "DemoOrgaBack_Main3")
                _MsSQLAdmnDB = IniFile.ReadString("winback", "MsSQLServer_AdmnDB", "DemoOrgaBack_Admin3")
                _MsSQLServer = IniFile.ReadString("winback", "MsSQLServer_Source", "WILL-WIN10\ORGA")
                _MsSQLUserId = IniFile.ReadString("winback", "MsSQLServer_UserId", "sa")
                _MsSQLPasswd = IniFile.ReadEncryptedString("winback", "MsSQLServer_Passwd", "OrgaBack.NET")

                _MySQLPath = IniFile.ReadString("winback", "MySQLServer_Path", "C:\Program Files\MySQL\MySQL Server 5.0")

                'wenn eine Mandanten-Nummer angegeben ist, wird versucht entsprechend der Mandanten-Nummer die Daten aus der Ini-Datei zu lesen
                If MandantNr <> UNDEFINED Then
                    _MySQLServerIP = IniFile.ReadString(IniWinBack_Mandant, "eMySQLServerIP", _MySQLServerIP)
                    _MySQLWinBack = IniFile.ReadString(IniWinBack_Mandant, "eMySQLDatabase", _MySQLWinBack)
                    _MySQLWbDaten = IniFile.ReadString(IniWinBack_Mandant, "eMySQLDatabaseDaten", _MySQLWbDaten)

                    _MsSQLMainDB = IniFile.ReadString(IniWinBack_Mandant, "MsSQLServer_MainDB", _MsSQLMainDB)
                    _MsSQLAdmnDB = IniFile.ReadString(IniWinBack_Mandant, "MsSQLServer_AdmnDB", _MsSQLAdmnDB)
                    _MsSQLServer = IniFile.ReadString(IniWinBack_Mandant, "MsSQLServer_Source", _MsSQLServer)
                    _MsSQLUserId = IniFile.ReadString(IniWinBack_Mandant, "MsSQLServer_UserId", _MsSQLUserId)
                    _MsSQLPasswd = IniFile.ReadEncryptedString(IniWinBack_Mandant, "MsSQLServer_Passwd", _MsSQLPasswd)
                End If

                Debug.Print("_MsSQLMainDB " & _MsSQLMainDB)
                Debug.Print("_MsSQLAdmnDB " & _MsSQLAdmnDB)
                Debug.Print("_MsSQLServer " & _MsSQLServer)
                Debug.Print("_MsSQLUserId " & _MsSQLUserId)
                Debug.Print("_MsSQLPasswd " & _MsSQLPasswd)

            Case "Logger"
                _LogToTextFile = IniFile.ReadInt("winback", "LogToTextFile", wbFALSE)
                _LogToDataBase = IniFile.ReadInt("winback", "LogToDataBase", wbFALSE)

            Case "OrgaBack"
                _osGrpBackwaren = IniFile.ReadString("orgaback", "GruppeBackwaren", "0")
                _osGrpRohstoffe = IniFile.ReadString("orgaback", "GruppeRohstoffe", "40")
                _osLaendercode = IniFile.ReadString("orgaback", "LaenderCode", "DE")
                _osSprachCode = IniFile.ReadString("orgaback", "SprachCode", "D")

                'wenn eine Mandanten-Nummer angegeben ist, wird versucht entsprechend der Mandanten-Nummer die Daten aus der Ini-Datei zu lesen
                If MandantNr <> UNDEFINED Then
                    _osGrpBackwaren = IniFile.ReadString(IniOrgaBack_Mandant, "GruppeBackwaren", _osGrpBackwaren)
                    _osGrpRohstoffe = IniFile.ReadString(IniOrgaBack_Mandant, "GruppeRohstoffe", _osGrpRohstoffe)
                End If

            Case "Produktion"
                _ChargenTeiler = IniFile.ReadString("Produktion", "ChargenTeiler", wb_Global.ModusChargenTeiler.OptimalUndRest)
                _TeigOptimierung = IniFile.ReadString("Produktion", "TeigOptimierung", wb_Global.ModusTeigOptimierung.AlleTeige)

            Case "Mail"
                mHost = IniFile.ReadString("smpt", "smtpHost")
                mHost = IniFile.ReadString("smpt", "smtpUser")
                mHost = IniFile.ReadEncryptedString("smpt", "smtpPass")

            Case "Path"
                _ImportPathPistor = IniFile.ReadString("Path", "Pistor", "")

        End Select
    End Sub

    Private Shared Sub getWinBackKonfiguration(Optional Key As String = "%")
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Daten aus Tabelle Konfiguration lesen
        If winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKonfiguration, Key)) Then
            While winback.Read
                Select Case winback.sField("KF_Tag")
                    Case "KundenName"
                        _MandantName = winback.sField("KF_Wert")
                    Case "DatenbankVersion"
                        _WinBackDBVersion = winback.sField("KF_Wert")
                    Case "vts__anzahl_behaelter"
                        _SauerteigAnzBeh = winback.sField("KF_Wert")
                    Case "Sprache1"
                        _WinBackLanguage1 = wb_Functions.StrToInt(winback.sField("KF_Wert"))
                    Case "Sprache2"
                        _WinBackLanguage2 = wb_Functions.StrToInt(winback.sField("KF_Wert"))
                    Case "SprachenVariante"
                        _WinBackLanguageVariante = wb_Functions.StrToInt(winback.sField("KF_Wert"))
                    Case "IpBasisAdresse"
                        _IPBasisAdresse = winback.sField("KF_Wert")
                End Select
            End While
        End If
    End Sub

    ''' <summary>
    ''' Gibt das aktuelle Betriebssystem zurück.
    ''' </summary>
    ''' <returns>Das aktuelle Betriebssystem</returns>
    ''' <remarks></remarks>
    Public Shared Function GetOSVersion() As String
        Select Case Environment.OSVersion.Platform
            Case PlatformID.Win32S
                Return "Windows 3.1"
            Case PlatformID.Win32Windows
                Select Case Environment.OSVersion.Version.Minor
                    Case 0
                        Return "Windows 95" 'Windows 95 unterstützt .Net nicht
                    Case 10
                        If Environment.OSVersion.Version.Revision.ToString() = "2222A" Then
                            Return "Windows 98 - Second Edition"
                        Else
                            Return "Windows 98"
                        End If
                    Case 90
                        Return "Windows ME"
                    Case Else
                        Return "Unbekannt"
                End Select
            Case PlatformID.Win32NT
                Select Case Environment.OSVersion.Version.Major
                    Case 3
                        Select Case Environment.OSVersion.Version.Minor
                            Case 0
                                Return "Windows NT 3" 'Windows NT 3 unterstützt .Net nicht
                            Case 1
                                Return "Windows NT 3.1" 'Windows NT 3.1 unterstützt .Net nicht
                            Case 5
                                Return "Windows NT 3.5" 'Windows NT 3.5 unterstützt .Net nicht
                            Case 51
                                Return "Windows NT 3.51" 'Windows NT 3.51 unterstützt .Net nicht
                            Case Else
                                Return "Unbekannt"
                        End Select
                    Case 4
                        Return "Windows NT 4.0"
                    Case 5
                        Select Case Environment.OSVersion.Version.Minor
                            Case 0
                                Return "Windows 2000"
                            Case 1
                                Return "Windows XP"
                            Case 2
                                Return "Windows 2003"
                            Case Else
                                Return "Unbekannt"
                        End Select
                    Case 6
                        Select Case Environment.OSVersion.Version.Minor
                            Case 0
                                Return "Windows Vista/Windows 2008 Server"
                            Case 1
                                Return "Windows 7"
                            Case Else
                                Return "Win8/Win10"
                        End Select
                    Case Else
                        Return "Unbekannt"
                End Select
            Case PlatformID.WinCE
                Return "Windows CE"
            Case PlatformID.Xbox
                Return "XBox"
            Case PlatformID.MacOSX
                Return "Mac OS X"
            Case PlatformID.Unix
                Return "Unix"
            Case Else
                Return "Unbekannt"
        End Select
    End Function

    Public Shared ReadOnly Property WinBackUpdateSetupExe
        Get
            If Environment.Is64BitProcess Then
                Return wb_Global.WinBackUpdateSetupExe_64Bit
            Else
                Return wb_Global.WinBackUpdateSetupExe_32Bit
            End If
        End Get
    End Property

    Public Shared Property pVariante As wb_Global.ProgVariante
        Get
            Return _pVariante
        End Get
        Set(value As wb_Global.ProgVariante)
            _pVariante = value
        End Set
    End Property

End Class
