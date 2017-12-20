Imports System.Drawing
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
    Private Shared _ConvertMySQL_CodePage As MySqlCodepage = MySqlCodepage.iso8859_15
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

    Private Shared _osGrpRohstoffe As String = Nothing  'Warengruppe Rohstoffe in OrgaBack
    Private Shared _osGrpBackwaren As String = Nothing  'Warengruppe Verkaufsartikel(Backwaren) in OrgaBack

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

    Private Shared _AktUser As String = ""
    Private Shared _AktUserNr As Integer = wb_Global.UNDEFINED

    Private Shared _SauerteigAnlage As Boolean = Nothing
    Private Shared _SauerteigAnzBeh As Integer = wb_Global.UNDEFINED

    Private Shared _ChargenTeiler As wb_Global.ModusChargenTeiler
    Private Shared _TeigOptimierung As wb_Global.ModusTeigOptimierung
    Private Shared _ProdPlanDatum As Date = Nothing
    Private Shared _ProdPlanfiliale As String = Nothing
    Private Shared _ProdPlanReadOnOpen As Boolean = False


    Public Shared Property pVariante As wb_Global.ProgVariante
        Get
            Return _pVariante
        End Get
        Set(value As wb_Global.ProgVariante)
            _pVariante = value
        End Set
    End Property

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

    Public Shared Property AktUser As String
        Get
            Return _AktUser
        End Get
        Set(value As String)
            _AktUser = value
        End Set
    End Property

    Public Shared Property AktUserNr As Integer
        Get
            Return _AktUserNr
        End Get
        Set(value As Integer)
            _AktUserNr = value
        End Set
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

    Public Shared ReadOnly Property pDBUpdatePath
        Get
            Return _pAddInPath & "DBUpdate\"
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

    Public Shared Property pWinBackIniPath As String
        Get
            If _pWinBackIniPath = Nothing Then
                _pWinBackIniPath = pProgrammPath & "WinBack.ini"
            End If
            Return _pWinBackIniPath
        End Get
        Set(value As String)
            _pWinBackIniPath = value
        End Set
    End Property

    Public Shared Property pProgrammPath As String
        Get
            If _pProgrammPath = Nothing Then
                _pProgrammPath = My.Application.Info.DirectoryPath & "\"
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

    Public Shared Property ConvertMySQL_CodePage As MySqlCodepage
        Get
            'TODO Test
            Return MySqlCodepage.iso8859_5
            Return _ConvertMySQL_CodePage
        End Get
        Set(value As MySqlCodepage)
            _ConvertMySQL_CodePage = value
        End Set
    End Property

    Public Shared Function GetFileName(Tabelle As String) As String
        Return pExportPath & Tabelle & "_" & DateTime.Now.ToString("yyMMdd") & "_" & DateTime.Now.ToString("hhmmss") & ".csv"
    End Function

    Private Shared Sub setWinBackIni(Section As String, Key As String, value As String)
        Dim Inifile As New wb_IniFile
        Inifile.WriteString(Section, Key, value)
        Inifile = Nothing
    End Sub

    Private Shared Sub getWinBackIni(Key As String)
        Dim IniFile As New wb_IniFile

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
                _MsSQLPasswd = IniFile.ReadString("winback", "MsSQLServer_Passwd", "OrgaBack.NET")

                _MySQLPath = IniFile.ReadString("winback", "MySQLServer_Path", "C:\Program Files\MySQL\MySQL Server 5.0")

            Case "Logger"
                _LogToTextFile = IniFile.ReadInt("winback", "LogToTextFile", wbFALSE)
                _LogToDataBase = IniFile.ReadInt("winback", "LogToDataBase", wbFALSE)

            Case "OrgaBack"
                _osGrpBackwaren = IniFile.ReadString("orgaback", "GruppeBackwaren", "0")
                _osGrpRohstoffe = IniFile.ReadString("orgaback", "GruppeRohstoffe", "40")

            Case "Produktion"
                _ChargenTeiler = IniFile.ReadString("Produktion", "ChargenTeiler", wb_Global.ModusChargenTeiler.OptimalUndRest)
                _TeigOptimierung = IniFile.ReadString("Produktion", "TeigOptimierung", wb_Global.ModusTeigOptimierung.AlleTeige)

        End Select
    End Sub

    Private Shared Sub getWinBackKonfiguration(Optional Key As String = "%")
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Daten aus Tabelle Konfiguration lesen
        If winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKonfiguration, Key)) Then
            While winback.Read
                Select Case winback.sField("KF_Tag")
                    Case "DatenbankVersion"
                        _WinBackDBVersion = winback.sField("KF_Wert")
                    Case "vts__anzahl_behaelter"
                        _SauerteigAnzBeh = winback.sField("KF_Wert")
                End Select
            End While
        End If
    End Sub
End Class
