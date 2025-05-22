Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports WeifenLuo.WinFormsUI.Docking
Imports Microsoft.Win32
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
    Private Shared _osDefaultWaehrung As String = Nothing

    Private Shared _MandantName As String = Nothing
    Private Shared _MandantNr As Integer = UNDEFINED
    Private Shared _OrgaBackMandantName As String = Nothing
    Private Shared _OrgaBackMandantNr As Integer = UNDEFINED
    Private Shared _ProduktionDummyArtikel As String = "0R9999"               'OrgaBack Dummy-Artikel für Rezepte ohne Zuordnung (ProduzierteWare)
    Private Shared _TeigruheAnzeige As Boolean = False                        'WinBack-AnyWhere Teigruhe und Kneter-Anzeige
    Private Shared _TeigruheVNC As Integer = UNDEFINED                        'WinBack-AnyWhere VNC-Teigruhe

    Private Shared _pAddInPath As String = Nothing
    Private Shared _pListenPath As String
    Private Shared _pTempPath As String
    Private Shared _pXConfigPath As String = Nothing
    Private Shared _pWinBackIniPath As String = Nothing
    Private Shared _pProgrammPath As String = ""
    Private Shared _pAppDataPath As String = Nothing
    Private Shared _Log4ViewExe As String = ""
    Private Shared _NotePadPlusExe As String = ""
    Private Shared _pDatenPath As String = ""
    Private Shared _pExportPath As String = Nothing
    Private Shared _pPicturePath As String = Nothing
    Private Shared _OrgaBackTheme As Integer = -1
    Private Shared _OrgaBackEmployee As String

    Private Shared _OrgaBackDBVersion As String = Nothing
    Private Shared _WinBackDBVersion As String = Nothing
    Private Shared _ShowDashboard As Integer = UNDEFINED

    Private Shared _MsSQLMainDB As String = Nothing
    Private Shared _MsSQLAdmnDB As String = Nothing
    Private Shared _MsSQLServer As String = Nothing
    Private Shared _MsSQLUserId As String = Nothing
    Private Shared _MsSQLPasswd As String = Nothing

    Private Shared _osGrpRohstoffe As String = Nothing                        'Warengruppe Rohstoffe in OrgaBack                        - Default 20
    Private Shared _osGrpBackwaren As String = Nothing                        'Warengruppe Verkaufsartikel(Backwaren) in OrgaBack       - Default 10
    Private Shared _osLaendercode As String = Nothing                         'Ländercode in OrgaBack (Update der Artikel/Komponenten-Nährwerte, Allergene und Deklaration
    Private Shared _osSprachCode As String = Nothing                          'Sprachencode in OrgaBack (Update der Artikel/Komponenten-Nährwerte, Allergene und Deklaration
    Private Shared _osProdTageVoraus As Integer = UNDEFINED                   'Produktions-Plan x Tage vor Bestell-Datum

    Private Shared _WinBackDBType As wb_Sql.dbType = wb_Sql.dbType.undef
    Private Shared _MySQLServerIP As String = Nothing
    Private Shared _MySQLWinBack As String = Nothing
    Private Shared _MySQLWbDaten As String = Nothing
    Private Shared _MySQLUser As String = Nothing
    Private Shared _MySQLPass As String = Nothing
    Private Shared _MySQLPath As String = Nothing
    Private Shared _TempMySQLServerIP As String = Nothing

    Private Shared _MsSQLServerIP As String = Nothing
    Private Shared _MsSQLWinBack As String = Nothing
    Private Shared _MsSQLWbDaten As String = Nothing

    Private Shared _Log4netKonfigFile As String = ""
    Private Shared _Log4netAutoStart As Integer = UNDEFINED

    Private Shared _Log4net_DebugLevel As Boolean
    Private Shared _Log4net_InfoLevel As Boolean
    Private Shared _Log4net_WarnLevel As Boolean
    Private Shared _Log4net_ErrorLevel As Boolean

    Private Shared _Log4net_LL_EnablePrinterInformation As Boolean
    Private Shared _Log4net_LL_EnableDataProvider As Boolean
    Private Shared _Log4net_LL_EnableLicensing As Boolean
    Private Shared _Log4net_LL_EnableDotNetComponent As Boolean
    Private Shared _Log4net_LL_EnableApiCalls As Boolean
    Private Shared _Log4net_LL_EnableOther As Boolean
    Private Shared _Log4net_LL_DebugLevel As Boolean
    Private Shared _Log4net_LL_InfoLevel As Boolean
    Private Shared _Log4net_LL_WarnLevel As Boolean
    Private Shared _Log4net_LL_ErrorLevel As Boolean

    'Private Shared _AktUserName As String = ""
    'Private Shared _AktUserNr As Integer = UNDEFINED
    'Private Shared _AktUserGruppe As Integer = -1

    Private Shared _ShowMsg_UpdateOrgaBackNWT As Boolean = True
    Private Shared _ShowMsg_RohstoffeSTK As Boolean = True

    Private Shared _SauerteigAnlage As Boolean = Nothing
    Private Shared _SauerteigAnzBeh As Integer = UNDEFINED
    Private Shared _IPBasisAdresse As String = Nothing

    Private Shared _ChargenTeiler As ModusChargenTeiler
    Private Shared _TeigOptimierung As ModusTeigOptimierung
    Private Shared _ProdPlanDatum As Date = Nothing
    Private Shared _ProdPlanfiliale As String = Nothing
    Private Shared _ProdPlanReadOnOpen As Boolean = True
    Private Shared _ProdPlanClearOnRead As Boolean = False

    Private Shared _RohChargen_ErfassungAktiv As String = Nothing
    Private Shared _RohChargen_GebindeGrsTol As String = Nothing
    Private Shared _RohChargen_ErfassungVariante As String = Nothing

    Private Shared _NwtInterneDeklaration As Integer = UNDEFINED
    Private Shared _NwtAllergeneNoTrace As Integer = UNDEFINED
    Private Shared _NwtAllergeneTxtTrace As Integer = UNDEFINED
    Private Shared _NwtAllergeneNoText As Integer = UNDEFINED
    Private Shared _NwtAllergeneGluten As Integer = UNDEFINED
    Private Shared _NwtAllergeneSchalen As Integer = UNDEFINED
    Private Shared _NwtAllergeneNoDefinitionErr As Integer = UNDEFINED
    Private Shared _NwtShowENummer As Integer = UNDEFINED

    Private Shared _NwtShowOptimized As Integer = UNDEFINED
    Private Shared _NwtENummerZutatenListe As Integer = UNDEFINED
    Private Shared _NwtOptimizeZutatenListe As Integer = UNDEFINED
    Private Shared _NwtCalcQuid As Integer = UNDEFINED
    Private Shared _NwtMehlZusammensetzung As Boolean

    Private Shared _Datenlink_Url As String = Nothing
    Private Shared _Datenlink_CAT As String = Nothing
    Private Shared _Datenlink_PAT As String = Nothing
    Private Shared _WinBackCloud_Url As String = Nothing
    Private Shared _WinBackCloud_Pass As String = Nothing

    Private Shared _mHost As String = Nothing
    Private Shared _mSenderAddr As String = Nothing
    Private Shared _mSenderPass As String = Nothing

    Private Shared _DefaultSchnittstelle As String = Nothing
    Private Shared _ImportPath As String = Nothing
    Private Shared _ExportPath As String = Nothing
    Private Shared _ImportArtikel As Boolean = Nothing
    Private Shared _ImportRezeptKopf As Boolean = Nothing
    Private Shared _ImportRezept As Boolean = Nothing
    Private Shared _ImportLieferungen As Boolean = Nothing
    Private Shared _ImportBackzettel As Boolean = Nothing
    Private Shared _ImportRzpVerarbeitungsHinweise As Boolean = Nothing
    Private Shared _ImportRzptUpdate As Boolean = Nothing
    Private Shared _ImportRzptWasserSpeichern As Boolean = Nothing
    Private Shared _ImportProdArtikelRezept As Boolean = Nothing
    Private Shared _ImportProdFTP As Boolean = Nothing

    Private Shared _ExportArtikel As Boolean = Nothing
    Private Shared _ExportRohstoffe As Boolean = Nothing
    Private Shared _ExportRezepte As Boolean = Nothing
    Private Shared _ExportChargen As Boolean = Nothing
    Private Shared _ExportChargenTWNr As Integer = wb_Global.UNDEFINED

    Private Shared _ExportArtikel_Nwt As Boolean = Nothing
    Private Shared _ExportRohstoffe_Nwt As Boolean = Nothing
    Private Shared _ExportRezepte_Aendrgn As Boolean = Nothing
    Private Shared _ExportRezepte_Sauertg As Boolean = Nothing

    Private Shared _ImportPathPistor As String = Nothing
    Private Shared _ArtikelVerarbeitungsHinweisPath As String = Nothing
    Private Shared _ExcelInstalled As Integer = wb_Global.UNDEFINED
    Private Shared _WinBackBackgroudTaskConnected As Boolean = False

    Private Shared _TwinCatADS As Boolean = Nothing
    Private Shared _TwinCatADS_IP As String = Nothing
    Private Shared _TwinCatADS_Port As Integer = 800

    Private Shared _Mandaten As New List(Of obMandant)

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
            'wenn der Winback-Mandant noch nicht definiert wurde - jetzt setzen (OrgaBack-Mandant-Nummer)
            If _MandantNr = wb_Global.UNDEFINED Then
                _MandantNr = OrgaBackMandantNr
            End If
            setWinBackIni("winback", "MsSQLServer_AdmnDB", value)
        End Set
    End Property

    Public Shared Property ProduktionDummyArtikel As String
        Get
            Return _ProduktionDummyArtikel
        End Get
        Set(value As String)
            _ProduktionDummyArtikel = value
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
            If value <> "" Then
                _MsSQLUserId = value
                setWinBackIni("winback", "MsSQLServer_UserId", value)
            End If
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
            If value <> "" Then
                _MsSQLPasswd = value
                setWinBackIni("winback", "MsSQLServer_Passwd", value)
            End If
        End Set
    End Property

    Public Shared ReadOnly Property OrgaBackMainConString As String
        Get
            If MsSQLUserId <> "xx" Then
                Return "Data Source=" & MsSQLServer & "; Database=" & MsSQLMain & "; user id=" & MsSQLUserId & "; password=" & MsSQLPasswd & ";TrustServerCertificate=True"
            Else
                Return "Data Source=" & MsSQLServer & "; Database=" & MsSQLMain & "; Integrated Security=True"
            End If
        End Get
    End Property

    Public Shared ReadOnly Property OrgaBackAdminConString As String
        Get
            If MsSQLUserId <> "xx" Then
                Return "Data Source=" & MsSQLServer & "; Database=" & MsSQLAdmn & "; user id=" & MsSQLUserId & "; password=" & MsSQLPasswd & ";TrustServerCertificate=True"
            Else
                Return "Data Source=" & MsSQLServer & "; Database=" & MsSQLAdmn & "; Integrated Security=True"
            End If
        End Get
    End Property

    Public Shared ReadOnly Property DockPanelPath(Optional DefaultPath As OrgaBackDockPanelLayoutPath = OrgaBackDockPanelLayoutPath.UserLokal)
        Get
            Dim WindowsTempPfad As String = pWindowsTempPath
            Select Case pVariante
                Case ProgVariante.OrgaBack
                    Dim OrgaBackTempPfad As String = ""
                    If DefaultPath = OrgaBackDockPanelLayoutPath.ProgrammGlobal Then
                        Dim sArr() As String = pTempPath.Split("\")
                        'Temp-Pfad global
                        For i = 0 To sArr.Length - 2
                            OrgaBackTempPfad &= sArr(i) & "\"
                        Next
                    Else
                        'Temp-Pfad User-lokal
                        OrgaBackTempPfad = pTempPath
                    End If

                    If System.IO.Directory.Exists(OrgaBackTempPfad) Then
                        Return OrgaBackTempPfad
                    Else
                        Return WindowsTempPfad & "\"
                    End If

                Case ProgVariante.WinBack
                    Return WindowsTempPfad & "\"
                Case Else
                    Throw New NotImplementedException
            End Select
        End Get
    End Property

    ''' <summary>
    ''' Farbschema für Fenster-Docking. In der Programm-Variante OrgaBack wird immer VS2015BlueTheme zurückgegeben
    ''' </summary>
    ''' <returns>Theme - ThemeBase</returns>
    Public Shared ReadOnly Property Theme As ThemeBase
        Get
            'Einstellung aus Desktop.DockingTheme auslesen
            If _OrgaBackTheme < 0 AndAlso wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
                'DbReadSetting("Desktop")
            Else
                _OrgaBackTheme = wb_Global.OrgaBackThemes.Blau
            End If

            'Rückgabe-Wert abhängig von der Einstellung in OrgaBack
            Select Case _OrgaBackTheme
                Case wb_Global.OrgaBackThemes.Standard
                    Return New VS2015BlueTheme
                Case wb_Global.OrgaBackThemes.Blau
                    Return New VS2015BlueTheme
                Case wb_Global.OrgaBackThemes.Grau
                    Return New VS2015LightTheme
                Case wb_Global.OrgaBackThemes.Anthrazit
                    Return New VS2015DarkTheme
                Case Else
                    Return New VS2015BlueTheme
            End Select
        End Get
    End Property

    Public Shared WriteOnly Property OrgaBackTheme As Integer
        Set(value As Integer)
            _OrgaBackTheme = value
        End Set
    End Property

    Public Shared ReadOnly Property MyOwnExeFileName As String
        Get
            Return MyExeDirectory & MyExeFileName
        End Get
    End Property
    Public Shared ReadOnly Property MyExeDirectory As String
        Get
            'Programm-Verzeichnis Signum OrgaSoft
            Return System.AppDomain.CurrentDomain.BaseDirectory
        End Get
    End Property
    Public Shared ReadOnly Property MyExeFileName As String
        Get
            'Dateiname Signum OrgaSoft
            Return System.AppDomain.CurrentDomain.FriendlyName
        End Get
    End Property

    Public Shared Property ShowDashboard As Boolean
        Get
            If pVariante = ProgVariante.AnyWhere Then
                _ShowDashboard = True
            Else
                If _ShowDashboard = UNDEFINED Then
                    getWinBackIni("DashBoard")
                End If
            End If
            Return (_ShowDashboard = 1)

        End Get
        Set(value As Boolean)
            If value Then
                _ShowDashboard = 1
            Else
                _ShowDashboard = 0
            End If

            'wird in der Sektion winback gespeichert (abhängig vom Mandanten)
            setWinBackIni("winback", "ShowDashboard", value)
        End Set
    End Property


    ''' <summary>
    ''' OrgaBack Mitarbeiter-Kürzel des aktuell angemeldeten Mitarbeiters
    ''' Wird geschrieben von OB_Main_Menu.UserLogin
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property OrgaBackEmployee As String
        Get
            Return _OrgaBackEmployee
        End Get
        Set(value As String)
            _OrgaBackEmployee = value
        End Set
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

    ''' <summary>
    ''' Die Filialzuordnung Gesamtunternehmen beeinhaltet auch die Filiale Produktion
    ''' 
    ''' Wenn dieser Switch gesetzt ist, wird jeder Mitarbeiter mit der Filialzuordnung 'Gesamtunternehmen#
    ''' auch der Filiale Produktion zugeordnet !
    ''' Achtung: Dann sind alle Mitarbeiter auch in WinBack vorhanden. (Default=False)
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property GesamtUnterNehmenHatAuchProduktion As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Das WinBack-AddIn kann auf einer lokalen(Windows)-MySql-Datenbank laufen oder auf
    ''' einer "echten" Linux-Datenbank mit Produktion.
    ''' Wenn die MySql-Version mit 3.xxx beginnt kann davon ausgegangen werden, dass es sich
    ''' um eine Linux-Installation mit "echter" Produktion handelt
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property LocalMySql As Boolean
        Get
            If Not wb_sql_Functions.GetMySqlVersion().StartsWith("3.") Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Shared ReadOnly Property DataGridAlternateRowColor As Color
        Get
            Return System.Drawing.Color.LightGray
        End Get
    End Property

    Public Shared Property Log4netKonfigFile As String
        Get
            If _Log4netKonfigFile = "" Then
                getWinBackIni("Logger")
            End If
            Return _Log4netKonfigFile
        End Get
        Set(value As String)
            _Log4netKonfigFile = value
            setWinBackIni("winback", "Log4netKonfigFile", _Log4netKonfigFile)
        End Set
    End Property

    Public Shared Property Log4netAutoStart As Boolean
        Get
            If _Log4netAutoStart = UNDEFINED Then
                If pVariante = ProgVariante.Setup Then
                    _Log4netAutoStart = 1
                Else
                    getWinBackIni("Logger")
                End If
            End If
            Return (_Log4netAutoStart = 1)
        End Get
        Set(value As Boolean)
            If value Then
                _Log4netAutoStart = 1
            Else
                _Log4netAutoStart = 0
            End If
            setWinBackIni("winback", "Log4netAutoStart", _Log4netAutoStart.ToString)
        End Set
    End Property

    Public Shared Property Log4net_DebugLevel As Boolean
        Get
            Return _Log4net_DebugLevel
        End Get
        Set(value As Boolean)
            _Log4net_DebugLevel = value
            setWinBackIni("log4net", "DebugLevel", _Log4net_DebugLevel)
        End Set
    End Property

    Public Shared Property Log4net_InfoLevel As Boolean
        Get
            Return _Log4net_InfoLevel
        End Get
        Set(value As Boolean)
            _Log4net_InfoLevel = value
            setWinBackIni("log4net", "InfoLevel", _Log4net_InfoLevel)
        End Set
    End Property

    Public Shared Property Log4net_WarnLevel As Boolean
        Get
            Return _Log4net_WarnLevel
        End Get
        Set(value As Boolean)
            _Log4net_WarnLevel = value
            setWinBackIni("log4net", "WarnLevel", _Log4net_WarnLevel)
        End Set
    End Property

    Public Shared Property Log4net_ErrorLevel As Boolean
        Get
            Return _Log4net_ErrorLevel
        End Get
        Set(value As Boolean)
            _Log4net_ErrorLevel = value
            setWinBackIni("log4net", "ErrorLevel", _Log4net_ErrorLevel)
        End Set
    End Property

    Public Shared Property Log4net_LL_EnablePrinterInformation As Boolean
        Get
            Return _Log4net_LL_EnablePrinterInformation
        End Get
        Set(value As Boolean)
            _Log4net_LL_EnablePrinterInformation = value
            setWinBackIni("log4net", "LL_PrinterInformation", _Log4net_LL_EnablePrinterInformation)
        End Set
    End Property

    Public Shared Property Log4net_LL_EnableDataProvider As Boolean
        Get
            Return _Log4net_LL_EnableDataProvider
        End Get
        Set(value As Boolean)
            _Log4net_LL_EnableDataProvider = value
            setWinBackIni("log4net", "LL_DataProvider", _Log4net_LL_EnableDataProvider)
        End Set
    End Property

    Public Shared Property Log4net_LL_EnableLicensing As Boolean
        Get
            Return _Log4net_LL_EnableLicensing
        End Get
        Set(value As Boolean)
            _Log4net_LL_EnableLicensing = value
            setWinBackIni("log4net", "LL_Licensing", _Log4net_LL_EnableLicensing)
        End Set
    End Property

    Public Shared Property Log4net_LL_EnableDotNetComponent As Boolean
        Get
            Return _Log4net_LL_EnableDotNetComponent
        End Get
        Set(value As Boolean)
            _Log4net_LL_EnableDotNetComponent = value
            setWinBackIni("log4net", "LL_DotNetComponent", _Log4net_LL_EnableDotNetComponent)
        End Set
    End Property

    Public Shared Property Log4net_LL_EnableApiCalls As Boolean
        Get
            Return _Log4net_LL_EnableApiCalls
        End Get
        Set(value As Boolean)
            _Log4net_LL_EnableApiCalls = value
            setWinBackIni("log4net", "LL_ApiCalls", _Log4net_LL_EnableApiCalls)
        End Set
    End Property

    Public Shared Property Log4net_LL_EnableOther As Boolean
        Get
            Return _Log4net_LL_EnableOther
        End Get
        Set(value As Boolean)
            _Log4net_LL_EnableOther = value
            setWinBackIni("log4net", "LL_Other", _Log4net_LL_EnableOther)
        End Set
    End Property

    Public Shared Property Log4net_LL_DebugLevel As Boolean
        Get
            Return _Log4net_LL_DebugLevel
        End Get
        Set(value As Boolean)
            _Log4net_LL_DebugLevel = value
            setWinBackIni("log4net", "LL_DebugLevel", _Log4net_LL_DebugLevel)
        End Set
    End Property

    Public Shared Property Log4net_LL_InfoLevel As Boolean
        Get
            Return _Log4net_LL_InfoLevel
        End Get
        Set(value As Boolean)
            _Log4net_LL_InfoLevel = value
            setWinBackIni("log4net", "LL_InfoLevel", _Log4net_LL_InfoLevel)
        End Set
    End Property

    Public Shared Property Log4net_LL_WarnLevel As Boolean
        Get
            Return _Log4net_LL_WarnLevel
        End Get
        Set(value As Boolean)
            _Log4net_LL_WarnLevel = value
            setWinBackIni("log4net", "LL_WarnLevel", _Log4net_LL_WarnLevel)
        End Set
    End Property

    Public Shared Property Log4net_LL_ErrorLevel As Boolean
        Get
            Return _Log4net_LL_ErrorLevel
        End Get
        Set(value As Boolean)
            _Log4net_LL_ErrorLevel = value
            setWinBackIni("log4net", "LL_ErrorLevel", _Log4net_LL_ErrorLevel)
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
                        Return "Data Source=" & MsSQLServer & "; Database=" & MySQLWinBack & "; user id=" & MsSQLUserId & "; password=" & MsSQLPasswd & ";TrustServerCertificate=True"
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

    Public Shared WriteOnly Property TempMySQLServerIP As String
        Set(value As String)
            _TempMySQLServerIP = value
        End Set
    End Property

    Public Shared Property MySQLServerIP As String
        Get
            If _MySQLServerIP = Nothing OrElse _MySQLServerIP = "" Then
                getWinBackIni("SQL")
            End If
            'MySql-Server-IP-Adresse wurde über Aufruf-Parameter temporär geändert
            If _TempMySQLServerIP = Nothing OrElse _TempMySQLServerIP = "" Then
                Return _MySQLServerIP
            Else
                Return _TempMySQLServerIP
            End If
        End Get
        Set(value As String)
            If value <> "" Then
                _MySQLServerIP = value
                setWinBackIni("winback", "eMySQLServerIP", _MySQLServerIP)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Schreibt die IP-Adresse in die winback.ini im Programm-Verzeichnis.
    ''' Notwendig nach Setup/Änderung der IP-Adresse
    ''' </summary>
    <CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnötige Zuweisung eines Werts.", Justification:="<Ausstehend>")>
    Public Shared WriteOnly Property MySQLMasterServerIP As String
        Set(value As String)
            If value <> "" Then
                'IP-Adresse in die winback.ini im Programm-Verzeichnis schreiben
                Dim Inifile As New wb_IniFile(wb_GlobalSettings.pWinBackMainIniPath)
                Inifile.WriteString("winback", "eMySQLServerIP", value)
                Inifile = Nothing
                'IP-Adresse wird zusätzlich in die locale winback.in im User-Verzeichnis geschrieben
                MySQLServerIP = value
            End If
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
            If value <> "" Then
                _MySQLWinBack = value
                setWinBackIni("winback", "eMySQLDatabase", _MySQLWinBack)
            End If
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
            If value <> "" Then
                _MySQLWbDaten = value
                setWinBackIni("winback", "eMySQLDatabaseDaten", _MySQLWbDaten)
            End If
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
            setWinBackIni("winback", "eMySQLUser", _MySQLUser)
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
            setWinBackIni("winback", "eMySQLPasswordDatabase", _MySQLPass)
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
            If pVariante = wb_Global.ProgVariante.OrgaBack Then
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
                If Orgaback.sqlSelect(wb_sql_Selects.mssqlSystemInfo) Then
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
            If pVariante = ProgVariante.OrgaBack Then
                Return FileVersionInfo.GetVersionInfo(wb_GlobalSettings.MyExeDirectory & "OrgaSoft.Contracts.dll").FileVersion
            Else
                Return "V0.0.0"
            End If
        End Get
    End Property

    Public Shared ReadOnly Property WinBackVersion As String
        Get
            If pVariante = wb_Global.ProgVariante.OrgaBack Then
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

    Public Shared ReadOnly Property WinBackMySqlServerVersion As String
        Get
            'Version vorbelegen
            Dim version As String = ""
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            'DB-Version abfragen
            If winback.sqlSelect("SELECT version()") AndAlso winback.Read Then
                version = winback.sField("version()")
            End If
            'Datenbank-Verbindung wieder schliessen
            winback.Close()
            Return version
        End Get
    End Property

    Public Shared Property pAddInPath As String
        Get
            If pVariante = ProgVariante.WinBack Then
                Return pProgrammPath
            Else
                Return _pAddInPath
            End If
        End Get
        'wird in wb_Main_Menu gesetzt
        Set(value As String)
            _pAddInPath = value
        End Set
    End Property

    Public Shared ReadOnly Property pLog4netPath As String
        Get
            Return pAddInPath & wb_Global.SubDir_log
        End Get
    End Property

    Public Shared ReadOnly Property pDBUpdatePath As String
        Get
            If pVariante = ProgVariante.UnitTest Then
                Return pProgrammPath & "..\..\..\" & wb_Global.SubDir_dbu
            Else
                Return pAddInPath & wb_Global.SubDir_dbu
            End If
        End Get
    End Property

    Public Shared ReadOnly Property pWinBackUpdatePath As String
        Get
            Return pAddInPath & wb_Global.SubDir_upd
        End Get
    End Property

    Public Shared ReadOnly Property pKocherPath As String
        Get
            Return pAddInPath & wb_Global.SubDir_tmp
        End Get
    End Property

    Public Shared ReadOnly Property pRohstoffDatenPath As String
        Get
            Dim _pRohstoffDatenPath As String

            If pVariante = wb_Global.ProgVariante.UnitTest Then
                _pRohstoffDatenPath = pProgrammPath & "..\..\..\" & wb_Global.SubDir_tmp
            Else
                _pRohstoffDatenPath = pAddInPath & wb_Global.SubDir_tmp
            End If

            'wenn der tmp-Pfad nicht existiert, erzeugen...
            If System.IO.Directory.Exists(_pRohstoffDatenPath) = False Then
                System.IO.Directory.CreateDirectory(_pRohstoffDatenPath)
            End If

            Return _pRohstoffDatenPath
        End Get
    End Property

    Public Shared ReadOnly Property pDruckHistoriePath As String
        Get
            Return pAddInPath & wb_Global.SubDir_pdf
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
                'If Debugger.IsAttached Then
                'aus dem Programm-Pfad wird /bin/... durch /lst ersetzt
                Select Case pVariante
                        Case ProgVariante.AnyWhere
                            _pListenPath = Left(pProgrammPath, InStr(pProgrammPath, "\WinBackAnyWhere\")) & "ListLabel30" & wb_Global.SubDir_lst
                        Case ProgVariante.WinBack
                            _pListenPath = Left(pProgrammPath, InStr(pProgrammPath, "\WinBackStart\")) & "ListLabel30" & wb_Global.SubDir_lst
                        Case ProgVariante.OrgaBack
                            _pListenPath = Left(pProgrammPath, InStr(pProgrammPath, "\bin\")) & "ListLabel30" & wb_Global.SubDir_lst
                        Case Else
                            _pListenPath = Left(pProgrammPath, InStr(pProgrammPath, "\bin\")) & "ListLabel30" & wb_Global.SubDir_lst
                    End Select
                'Else
                _pListenPath = pProgrammPath & wb_Global.SubDir_lst
                'End If
            End If
                Return _pListenPath
        End Get
        Set(value As String)
            _pListenPath = value
        End Set
    End Property

    Public Shared ReadOnly Property pRedistPath As String
        Get
            Return pProgrammPath & wb_Global.SubDir_ll3
        End Get
    End Property

    Public Shared Property pTempPath As String
        Get
            Return _pTempPath
        End Get
        Set(value As String)
            _pTempPath = value
        End Set
    End Property

    Public Shared Property pXConfigPath As String
        Get
            If Debugger.IsAttached Then
                'aus dem Programm-Pfad wird /bin/... durch /lst ersetzt
                Select Case pVariante
                    Case ProgVariante.AnyWhere
                        _pXConfigPath = Left(pProgrammPath, InStr(pProgrammPath, "\WinBackAnyWhere\")) & wb_Global.SubDir_xfd
                    Case ProgVariante.WinBack
                        _pXConfigPath = Left(pProgrammPath, InStr(pProgrammPath, "\WinBackStart\")) & wb_Global.SubDir_xfd
                    Case ProgVariante.OrgaBack
                        _pXConfigPath = Left(pProgrammPath, InStr(pProgrammPath, "\bin\")) & wb_Global.SubDir_xfd
                    Case Else
                        _pXConfigPath = Left(pProgrammPath, InStr(pProgrammPath, "\bin\")) & wb_Global.SubDir_xfd
                End Select
            Else
                If _pXConfigPath = Nothing Then
                    _pXConfigPath = _pProgrammPath & wb_Global.SubDir_xfc
                End If
            End If
            Return _pXConfigPath
        End Get
        Set(value As String)
            _pXConfigPath = value
        End Set
    End Property

    ''' <summary>
    ''' Windows-Umgebungsvariable TMP. System-Temp-Path (C:\Temp).
    ''' Sollte diese Umgebungsvariable nicht existieren, erfolgt ein Eintrag im System-Log und es wird stattdessen der OrgaBack-tmp-Pfad
    ''' zurückgegeben
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property pTmpPath As String
        Get
            Try
                Return Environment.GetEnvironmentVariable("TMP")
            Catch
                Trace.Write("&E_Envrionment-Variable TMP existiert nicht")
                Return pTempPath
            End Try
        End Get
    End Property

    ''' <summary>
    ''' Pfad zu den Artikel-Bildern
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property pPicturePath As String
        Get
            Return pAddInPath & wb_Global.SubDir_pic
        End Get
        Set(value As String)
            'TODO Pfad variabel mit Edit
        End Set
    End Property

    <CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnötige Zuweisung eines Werts.", Justification:="<Ausstehend>")>
    Public Shared Property OsGrpRohstoffe As String
        Get
            If _osGrpRohstoffe Is Nothing Then
                getWinBackIni("OrgaBack")
            End If
            Return _osGrpRohstoffe
        End Get
        Set(value As String)
            If value <> "" Then
                _osGrpRohstoffe = value
                Dim IniFile As New wb_IniFile
                IniFile.WriteString("orgaback", "GruppeRohstoffe", _osGrpRohstoffe)
                IniFile = Nothing
            End If
        End Set
    End Property

    Public Shared ReadOnly Property OsGrpRohstoffe_MsSql As String
        Get
            Dim Grp As String = OsGrpRohstoffe
            'Sonderzeichen aus dem SQL-String ausfiltern (Für MsSQL)
            Grp = Grp.Replace("^", "")
            Grp = Grp.Replace("$", "")
            Return Grp
        End Get
    End Property

    <CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnötige Zuweisung eines Werts.", Justification:="<Ausstehend>")>
    Public Shared Property OsGrpBackwaren As String
        Get
            If _osGrpBackwaren Is Nothing Then
                getWinBackIni("OrgaBack")
            End If
            Return _osGrpBackwaren
        End Get
        Set(value As String)
            If value <> "" Then
                _osGrpBackwaren = value
                Dim IniFile As New wb_IniFile
                IniFile.WriteString("orgaback", "GruppeBackwaren", _osGrpBackwaren)
                IniFile = Nothing
            End If
        End Set
    End Property

    Public Shared ReadOnly Property OsGrpBackwaren_MsSql As String
        Get
            Dim Grp As String = OsGrpBackwaren
            'Sonderzeichen aus dem SQL-String ausfiltern (Für MsSQL)
            Grp = Grp.Replace("^", "")
            Grp = Grp.Replace("$", "")
            Return Grp
        End Get
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

    ''' <summary>
    ''' Default-Währung.
    ''' Wird aus OrgaBack-DB-Festwerten gelesen. Ist die Default-Währung nicht definiert, kommt der Wert aus der winback.ini
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property osDefaultWaehrung As String
        Get
            If _osDefaultWaehrung Is Nothing Then
                getWinBackIni("OrgaBack")
            End If
            Return _osDefaultWaehrung
        End Get
        Set(value As String)
            Select Case value.ToUpper
                Case "EUR"
                    _osDefaultWaehrung = "€"
                Case "DOLLAR"
                    _osDefaultWaehrung = "$"
                Case Else
                    _osDefaultWaehrung = value
            End Select
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

    Public Shared Property osProdTageVoraus As Integer
        Get
            If _osProdTageVoraus = UNDEFINED Then
                getWinBackIni("OrgaBack")
            End If
            Return _osProdTageVoraus
        End Get
        Set(value As Integer)
            _osProdTageVoraus = value
            setWinBackIni("OrgaBack", "ProdPlanTage", value.ToString)
        End Set
    End Property

    ''' <summary>
    ''' Setzt/gibt den Pfad zur winback.ini zurück.
    ''' 
    ''' Läuft das Programm als OrgaBack-AddIn ist der Pfad für die winback.ini im Verzeichnis OrgaSoft
    ''' Das Addin läuft in OrgaSoft/Addin
    ''' Im Debug-Mode wird die WinBack.ini nach dem Übersetzen automatisch von 
    '''     ..repos\Signum_WinBack in das \bin\debug\-Verzeichnis kopiert (Build-Ereignisse)
    ''' 
    ''' Das Hintergrund-Programm WinBack-Server-Task startet im Verzeichnis OrgaSoft/AddIn, der Verweis
    ''' auf die winback.ini liegt damit eine Ebene tiefer.
    ''' Für den Debugger wird der Pfad zur winback.ini als Parameter übergeben, da sonst der Programm-Pfad
    ''' verwendet werden muss.
    '''
    ''' als winback.exe im Standalone-Betrieb liegt die winback.ini im User-Verzeichnis (AppData/Roaming/OrgaBack)
    ''' und im Programm-Verzeichnis. Wenn die Datei im Programm-Verzeichnis neuer ist, wird diese in das User-Verzeichnis kopiert.
    ''' </summary>
    ''' <returns></returns>
    <CodeAnalysis.SuppressMessage("Performance", "CA1845:""string.Concat"" auf span-Basis verwenden", Justification:="<Ausstehend>")>
    <CodeAnalysis.SuppressMessage("Performance", "CA1866:Char-Überladung verwenden", Justification:="<Ausstehend>")>
    Public Shared Property pWinBackIniPath As String
        Get
            If _pWinBackIniPath = Nothing Then
                Select Case pVariante

                    Case ProgVariante.OBServerTask
                        If Debugger.IsAttached Then
                            _pWinBackIniPath = Path.GetDirectoryName(pProgrammPath)
                            _pWinBackIniPath = _pWinBackIniPath.Substring(0, _pWinBackIniPath.LastIndexOf("\"))
                            _pWinBackIniPath = _pWinBackIniPath.Substring(0, _pWinBackIniPath.LastIndexOf("\")) & "\WinBack.ini"
                        Else
                            'die Server-Task startet im AddIn-Verzeichnis, der Pfad zur winback.ini liegt eine Ebene davor
                            _pWinBackIniPath = Path.GetDirectoryName(pProgrammPath)
                            _pWinBackIniPath = _pWinBackIniPath.Substring(0, _pWinBackIniPath.LastIndexOf("\")) & "\WinBack.ini"
                        End If

                    Case ProgVariante.WBServerTask
                        'die Server-Task startet im Programm-Verzeichnis, der Pfad zur winback.ini ist identisch
                        _pWinBackIniPath = Path.GetDirectoryName(pProgrammPath) & "\WinBack.ini"

                    Case ProgVariante.OrgaBack
                        'die winback.ini liegt direkt über dem AddIn-Pfad '..\OrgaBack\AddIn
                        Dim directoryInfo As System.IO.DirectoryInfo = Directory.GetParent(pAddInPath)
                        _pWinBackIniPath = directoryInfo.Parent.FullName & "\WinBack.ini"

                    Case ProgVariante.WinBack, ProgVariante.AnyWhere
                        'die winback.ini liegt im User-Verzeichnis
                        _pWinBackIniPath = pAppDataPath & "\WinBack.ini"
                        'prüfen ob die winback.ini im User-Verzeichnis vorhanden und aktueller ist
                        If Not CheckWinBackIni(pProgrammPath & "\WinBack.ini", _pWinBackIniPath) Then
                            'aus irgendeinem Grund kann auf den User-Path nicht zugegriffen werden...
                            _pWinBackIniPath = pProgrammPath & "\WinBack.ini"
                        End If

                    Case ProgVariante.UnitTest
                        'die winback.ini liegt im Programm-Verzeichnis
                        _pWinBackIniPath = pProgrammPath & "\WinBack.ini"

                    Case Else
                        Return ""
                        Exit Property

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

    ''' <summary>
    ''' Gibt den Pfad zur MASTER winback.ini zurück.
    ''' Einen Master-Pfad gibt nur in der Variante WinBack/AnyWhere (Programm-Verzeichnis)
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property pWinBackMainIniPath As String
        Get
            Select Case pVariante
                Case ProgVariante.WinBack, ProgVariante.AnyWhere
                    'der MASTER-Path liegt im Programm-Verzeichnis
                    _pWinBackIniPath = pProgrammPath & "\WinBack.ini"
                Case Else
                    Return pWinBackIniPath
            End Select
            Return pWinBackIniPath
        End Get
    End Property

    ''' <summary>
    ''' Prüft ob die winback.ini im User-Verzeichnis vorhanden und aktueller ist als die winback.ini im Programm-Verzeichnis
    ''' Falls nicht werden die notwendigen Zeilen bzw. die gesamte Datei ins User-Verzeichnis kopiert
    ''' </summary>
    ''' <param name="ProgPath"></param>
    ''' <param name="UserPath"></param>
    Private Shared Function CheckWinBackIni(ProgPath As String, UserPath As String) As Boolean
        Try

            'Prüfen ob die winback.ini im User-Pfad vorhanden ist.
            If Not File.Exists(UserPath) Then
                'winback.ini ins User-Verzeichnis kopieren
                File.Copy(ProgPath, UserPath)
                Trace.WriteLine("@I_WinBack.ini im User-Verzeichnis ist nicht vorhanden - wird aus Programm-Verzeichnis kopiert")
                Return True
            End If

            'prüfen ob die winback.ini im Programm-Pfad aktueller als die winback.ini im User-Pfad ist
            If IO.File.GetLastWriteTime(ProgPath) > IO.File.GetLastWriteTime(UserPath) Then
                'alle Einträge aus der winback.ini im Programm-Verzeichnis in die winback.ini im User-Verzeichnis mergen
                Dim UserIniFile As New wb_IniFile(UserPath)
                UserIniFile.MergeFile(ProgPath)
                Trace.WriteLine("@I_WinBack.ini im User-Verzeichnis ist nicht aktuell - wird aus Programm-Verzeichnis aktualisiert")
                Return True
            End If

        Catch ex As Exception
            Debug.Print("@E_Fehler beim Prüfen WinBack.ini")
            Return False
        End Try
        Return True
    End Function

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
            End If
            Return _pProgrammPath
        End Get
        Set(value As String)
            If pVariante <> ProgVariante.UnitTest Then
                _pProgrammPath = value
            End If
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

    Public Shared ReadOnly Property pWindowsTempPath As String
        Get
            Try
                Return IO.Path.GetTempPath()
            Catch ex As Exception
                Return "C:\"
            End Try
        End Get
    End Property

    Public Shared ReadOnly Property pAppDataPath As String
        Get
            If _pAppDataPath = Nothing Then
                _pAppDataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\WinBack"
            End If
            Return _pAppDataPath
        End Get
    End Property

    <CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnötige Zuweisung eines Werts.", Justification:="<Ausstehend>")>
    Public Shared ReadOnly Property ExcelInstalled As Boolean
        Get
            If _ExcelInstalled = wb_Global.UNDEFINED Then
                Dim rkVersionKey As RegistryKey = Nothing
                Try
                    Const stXL_SUBKEY As String = "\Excel.Application\CurVer"
                    'Prüfen ob ein Entrag in der Registry vorhanden ist
                    rkVersionKey = Registry.ClassesRoot.OpenSubKey(stXL_SUBKEY, False)
                    'Wenn der Key vorhanden ist, ist eine beliebige Excel-Version installiert
                    If rkVersionKey IsNot Nothing Then
                        'Excel ist installiert
                        _ExcelInstalled = 1
                    Else
                        'Excel ist nicht installiert
                        _ExcelInstalled = 0
                    End If
                Catch
                    _ExcelInstalled = 0
                End Try
            End If
            Return (_ExcelInstalled = 1)
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
            setWinBackIni("Produktion", "ChargenTeiler", Int(value).ToString)
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
            setWinBackIni("Produktion", "TeigOptimierung", Int(value).ToString)
        End Set
    End Property



    ''' <summary>
    ''' Global verfügbarer Wert Datum Produktionsplan (Produktion für...)
    ''' </summary>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Eventuell bestehenden Produktionsplan vor Einlesen der Produktionsdaten aus OrgaBack ohne Nachfrage löschen.
    ''' Normalerweise wird ein schon bestehender Produktionplan in WinBack beim Einlesen der Daten von OrgaBack nur
    ''' auf Nachfrage gelöscht. 
    ''' 
    ''' Da bei Niehaves immer wieder Probleme mit doppelten Einträgen in der Aufarbeitungsliste
    ''' auftreten, kann die bestehende Liste ohne Nachfragen vor Lesen der Daten aus OrgaBack gelöscht werden.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property ProdPlanClearOnRead As Boolean
        Get
            getWinBackIni("Produktion")
            Return _ProdPlanClearOnRead
        End Get
        Set(value As Boolean)
            _ProdPlanClearOnRead = value
            setWinBackIni("Produktion", "ProdPlanClear", value.ToString)
        End Set
    End Property

    Friend Shared Function AufarbeitungZielDrucken() As Boolean
        'TODO - evtl als globale Einstellung in OrgaBack realisieren
        Return True
    End Function

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
            If _WinBackLanguageVariante = 1 AndAlso (_WinBackLanguage2 = wb_Language.GetLanguageNr) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Shared ReadOnly Property Mandanten As IList
        Get
            If _OrgaBackMandantName = Nothing Then
                GetOrgaBackMandant()
            End If
            Return _Mandaten
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

    Public Shared ReadOnly Property WinBackDemoMandant As Boolean
        Get
            If MandantName.Contains("Demo") Then
                Return True
            Else
                Return False
            End If
        End Get
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

    Public Shared Property ImportPathPistor As String
        Get
            If _ImportPathPistor = Nothing Then
                getWinBackIni("Path")
            End If
            Return _ImportPathPistor
        End Get
        Set(value As String)
            _ImportPathPistor = value
            setWinBackIni("Path", "Pistor", value)
        End Set
    End Property

    Public Shared Property ArtikelVerarbeitungsHinweisPath As String
        Get
            If _ArtikelVerarbeitungsHinweisPath = Nothing Then
                getWinBackIni("Path")
            End If
            Return _ArtikelVerarbeitungsHinweisPath
        End Get
        Set(value As String)
            _ArtikelVerarbeitungsHinweisPath = value
            setWinBackIni("Artikel", "VerzeichnisArtikelHinweise", value)
        End Set
    End Property

    Private Shared Sub GetOrgaBackMandant()
        'xml-File OrgaBack.ini aus DatenPfad einlesen
        Dim XMLReader As Xml.XmlReader = New Xml.XmlTextReader(pDatenPath & "OrgaSoft.ini")
        'Anzahl der Mandanten
        Dim MandantNr As Integer = 0
        Dim MandantName As String = ""
        Dim AdminDBName As String = ""

        'Fehler in xml-File abfangen (Falscher OrgaBack-DB-Eintrag in pDatenPath)
        Try

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

                                    'Mandant und Admin-DB in Liste eintragen
                                    Dim m As New wb_Global.obMandant With {.MandantNr = MandantNr, .MandantName = MandantName, .AdminDBName = AdminDBName}
                                    _Mandaten.Add(m)

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
        Catch
            'Fehler beim Lesen der Mandanten-Information aus OrgaBack.ini
            _Mandaten.Clear()
            'Mandant und Admin-DB in Liste eintragen
            Dim m As New wb_Global.obMandant With {.MandantNr = 1, .MandantName = "FEHLER BEI ORGABACK.INI", .AdminDBName = Nothing}
            _Mandaten.Add(m)
        End Try
    End Sub

    Public Shared Function GetFileName(Tabelle As String) As String
        Return pExportPath & Tabelle & "_" & DateTime.Now.ToString("yyMMdd") & "_" & DateTime.Now.ToString("hhmmss") & ".csv"
    End Function

    <CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnötige Zuweisung eines Werts.", Justification:="<Ausstehend>")>
    Private Shared Sub setWinBackIni(Section As String, Key As String, value As String)
        'Abhängig vom Mandanten
        If Section = "winback" Then
            Section = IniMandantSection("winback")
        End If
        'Abhängig vom Mandanten
        If Section = "orgaback" Then
            Section = IniMandantSection("orgaback")
        End If

        Dim Inifile As New wb_IniFile
        Inifile.WriteString(Section, Key, value)
        Inifile = Nothing
    End Sub

    <CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnötige Zuweisung eines Werts.", Justification:="<Ausstehend>")>
    Private Shared Sub setWinBackIni(Section As String, Key As String, value As Boolean)
        'Abhängig vom Mandanten
        If Section = "winback" Then
            Section = IniMandantSection("winback")
        End If
        'Abhängig vom Mandanten
        If Section = "orgaback" Then
            Section = IniMandantSection("orgaback")
        End If

        Dim Inifile As New wb_IniFile
        Inifile.WriteBool(Section, Key, value)
        Inifile = Nothing
    End Sub

    ''' <summary>
    ''' </summary>
    ''' <param name="Key"></param>
    Private Shared Sub getWinBackIni(Key As String)
        Dim IniFile As New wb_IniFile

        'Keys aufgeteilt nach Mandant-Nummer
        Dim IniWinBack_Mandant As String = IniMandantSection("winback")
        Dim IniOrgaBack_Mandant As String = IniMandantSection("orgaback")

        Select Case Key
            Case "SQL"
                _WinBackDBType = wb_Functions.StringToDBType(IniFile.ReadString("winback", "DBType", "MySQL"))
                _MySQLServerIP = IniFile.ReadString(IniWinBack_Mandant, "eMySQLServerIP", IniFile.ReadString("winback", "eMySQLServerIP", _MySQLServerIP))
                _MySQLWinBack = IniFile.ReadString(IniWinBack_Mandant, "eMySQLDatabase", IniFile.ReadString("winback", "eMySQLDatabase", "winback"))
                _MySQLWbDaten = IniFile.ReadString(IniWinBack_Mandant, "eMySQLDatabaseDaten", IniFile.ReadString("winback", "eMySQLDatabaseDaten", "wbdaten"))

                _MySQLUser = IniFile.ReadString("winback", "eMySQLUser", "herbst")
                _MySQLPass = IniFile.ReadString("winback", "eMySQLPasswordDatabase", "herbst")

                'Default-Wert für die IP-Adresse is der Rechner-Name sonst funktioniert der Zugriff auf die MS-SQL2014-Datenbank nicht
                _MsSQLServerIP = IniFile.ReadString("winback", "eMsSQLServerIP", Environment.MachineName)
                _MsSQLWinBack = IniFile.ReadString("winback", "eMsSQLDatabase", "winback")
                _MsSQLWbDaten = IniFile.ReadString("winback", "eMsSQLDatabaseDaten", "wbdaten")

                _MsSQLMainDB = IniFile.ReadString(IniWinBack_Mandant, "MsSQLServer_MainDB", IniFile.ReadString("winback", "MsSQLServer_MainDB", _MsSQLMainDB))
                _MsSQLAdmnDB = IniFile.ReadString(IniWinBack_Mandant, "MsSQLServer_AdmnDB", IniFile.ReadString("winback", "MsSQLServer_AdmnDB", _MsSQLAdmnDB))
                _MsSQLServer = IniFile.ReadString(IniWinBack_Mandant, "MsSQLServer_Source", IniFile.ReadString("winback", "MsSQLServer_Source", _MsSQLServer))
                _MsSQLUserId = IniFile.ReadString(IniWinBack_Mandant, "MsSQLServer_UserId", IniFile.ReadString("winback", "MsSQLServer_UserId", ""))
                _MsSQLPasswd = IniFile.ReadEncryptedString(IniWinBack_Mandant, "MsSQLServer_Passwd", _MsSQLPasswd)

                _MySQLPath = IniFile.ReadString("winback", "MySQLServer_Path", "C:\Program Files\MySQL\MySQL Server 5.0")

                Trace.WriteLine("@I_MsSQLMainDB " & _MsSQLMainDB)
                Trace.WriteLine("@I_MsSQLAdmnDB " & _MsSQLAdmnDB)
                Trace.WriteLine("@I_MsSQLServer " & _MsSQLServer)
                Trace.WriteLine("@I_MsSQLUserId " & _MsSQLUserId)
                Trace.WriteLine("@I_MsSQLPasswd " & _MsSQLPasswd)

            Case "DashBoard"
                _ShowDashboard = IniFile.ReadInt(IniWinBack_Mandant, "ShowDashboard", 0)

            Case "Logger"
                _Log4netKonfigFile = IniFile.ReadString(IniWinBack_Mandant, "Log4netKonfigFile", "")
                _Log4netAutoStart = IniFile.ReadInt(IniWinBack_Mandant, "Log4netAutoStart", UNDEFINED)
                _Log4ViewExe = IniFile.ReadString("winback", "Log4View_Path", "C:\Program Files (x86)\Log4View V2\Log4View.exe")
                _NotePadPlusExe = IniFile.ReadString("winback", "NotePadPlusPath", "C:\Program Files (x86)\Notepad++\Notepad++.exe")

                _Log4net_DebugLevel = IniFile.ReadBool("log4net", "DebugLevel")
                _Log4net_InfoLevel = IniFile.ReadBool("log4net", "InfoLevel")
                _Log4net_WarnLevel = IniFile.ReadBool("log4net", "WarnLevel")
                _Log4net_ErrorLevel = IniFile.ReadBool("log4net", "ErrorLevel", True)

                _Log4net_LL_EnablePrinterInformation = IniFile.ReadBool("log4net", "LL_PrinterInformation")
                _Log4net_LL_EnableDataProvider = IniFile.ReadBool("log4net", "LL_DataProvider")
                _Log4net_LL_EnableLicensing = IniFile.ReadBool("log4net", "LL_Licensing")
                _Log4net_LL_EnableDotNetComponent = IniFile.ReadBool("log4net", "LL_DotNetComponent")
                _Log4net_LL_EnableApiCalls = IniFile.ReadBool("log4net", "LL_ApiCalls")
                _Log4net_LL_EnableOther = IniFile.ReadBool("log4net", "LL_Other")
                _Log4net_LL_DebugLevel = IniFile.ReadBool("log4net", "LL_DebugLevel")
                _Log4net_LL_InfoLevel = IniFile.ReadBool("log4net", "LL_InfoLevel")
                _Log4net_LL_WarnLevel = IniFile.ReadBool("log4net", "LL_WarnLevel")
                _Log4net_LL_ErrorLevel = IniFile.ReadBool("log4net", "LL_ErrorLevel")

            Case "OrgaBack"
                _osGrpBackwaren = IniFile.ReadString(IniOrgaBack_Mandant, "GruppeBackwaren", IniFile.ReadString("orgaback", "GruppeBackwaren", _osGrpBackwaren))
                _osGrpRohstoffe = IniFile.ReadString(IniOrgaBack_Mandant, "GruppeRohstoffe", IniFile.ReadString("orgaback", "GruppeRohstoffe", _osGrpRohstoffe))
                _osLaendercode = IniFile.ReadString("orgaback", "LaenderCode", "DE")
                _osSprachCode = IniFile.ReadString("orgaback", "SprachCode", "D")
                _osDefaultWaehrung = IniFile.ReadString("orgaback", "DefaultWaehrung", "€")
                _osProdTageVoraus = IniFile.ReadString("orgaback", "ProdPlanTage", "1")

            Case "Produktion"
                _ChargenTeiler = IniFile.ReadInt("Produktion", "ChargenTeiler", wb_Global.ModusChargenTeiler.OptimalUndRest)
                _TeigOptimierung = IniFile.ReadInt("Produktion", "TeigOptimierung", wb_Global.ModusTeigOptimierung.AlleTeige)
                _ProdPlanClearOnRead = IniFile.ReadString("Produktion", "ProdPlanClear", True)

            Case "Nährwerte"
                _NwtInterneDeklaration = IniFile.ReadInt("Artikel", "InterneDeklaration", 1)
                _NwtAllergeneNoTrace = IniFile.ReadInt("Artikel", "KonfigAllergeneNoTrace", 0)
                _NwtAllergeneTxtTrace = IniFile.ReadInt("Artikel", "KonfigAllergeneTxtTrace", 0)
                _NwtAllergeneNoText = IniFile.ReadInt("Artikel", "KeineAllergeneImText", 0)
                _NwtAllergeneGluten = IniFile.ReadInt("Artikel", "KeineAusgabeGluten", 0)
                _NwtAllergeneSchalen = IniFile.ReadInt("Artikel", "KeineAusgabeSchalefruechte", 0)
                _NwtAllergeneNoDefinitionErr = IniFile.ReadInt("Artikel", "AllergenKeineAngabeError", 0)
                _NwtShowENummer = IniFile.ReadInt("Artikel", "ENummernAnzeigen", 0)
                _NwtShowOptimized = IniFile.ReadInt("Artikel", "ZutatenOptimiertAnzeigen", 1)
                _NwtENummerZutatenListe = IniFile.ReadInt("Artikel", "ENummernZutatenliste", 0)
                _NwtOptimizeZutatenListe = IniFile.ReadInt("Artikel", "ZutatenListeOptimieren", 1)

                _Datenlink_Url = IniFile.ReadString("Datenlink", "URL", wb_Credentials.Datenlink_Url)
                _Datenlink_CAT = IniFile.ReadString("Datenlink", "CAT", wb_Credentials.Datenlink_CAT)
                _Datenlink_PAT = IniFile.ReadString("Datenlink", "PAT", wb_Credentials.Datenlink_PAT)
                _WinBackCloud_Url = IniFile.ReadString("Cloud", "URL", wb_Credentials.WinBackCloud_Url)
                _WinBackCloud_Pass = IniFile.ReadString("Cloud", "Pass", wb_Credentials.WinBackCloud_Pass)

            Case "Mail"
                mHost = IniFile.ReadString("smpt", "smtpHost")
                mHost = IniFile.ReadString("smpt", "smtpUser")
                mHost = IniFile.ReadEncryptedString("smpt", "smtpPass")

            Case "Path"
                _ImportPathPistor = IniFile.ReadString("Path", "Pistor", "")
                _ArtikelVerarbeitungsHinweisPath = IniFile.ReadString("Artikel", "VerzeichnisArtikelHinweise", "")

            Case "Bakelink"
                _DefaultSchnittstelle = IniFile.ReadString("Bakelink", "ConnectType", "")
                _ImportPath = IniFile.ReadString("Bakelink", "ImportPfad", "")
                _ExportPath = IniFile.ReadString("Bakelink", "ExportPfad", "")

                _ImportArtikel = IniFile.ReadBool("Bakelink", "ImportArtikel", False)
                _ImportRezeptKopf = IniFile.ReadBool("Bakelink", "ImportRezeptKopf", False)
                _ImportRezept = IniFile.ReadBool("Bakelink", "ImportRezept", False)
                _ImportLieferungen = IniFile.ReadBool("Bakelink", "ImportLieferungen", False)
                _ImportBackzettel = IniFile.ReadBool("Bakelink", "ImportBackzettel", False)

                _ImportRzpVerarbeitungsHinweise = IniFile.ReadBool("Bakelink", "Rezeptverarbeitungshinweise", False)
                _ImportRzptUpdate = IniFile.ReadBool("Bakelink", "RezepturUpdate", False)
                _ImportRzptWasserSpeichern = IniFile.ReadBool("Bakelink", "WasserTempSpeichern", False)
                _ImportProdArtikelRezept = IniFile.ReadBool("Bakelink", "ListeArtikelRezept", False)
                _ImportProdFTP = IniFile.ReadBool("Bakelink", "ProdListeAutoCreate", False)

                _ExportArtikel = IniFile.ReadBool("Bakelink", "ExportArtikel", False)
                _ExportRohstoffe = IniFile.ReadBool("Bakelink", "ExportRohstoffe", False)
                _ExportRezepte = IniFile.ReadBool("Bakelink", "ExportRezepte", False)
                _ExportChargen = IniFile.ReadBool("Bakelink", "ExportChargen", False)
                ExportChargenTWNr = IniFile.ReadInt("Bakelink", "TWNr", wb_Global.UNDEFINED)

                _ExportArtikel_Nwt = IniFile.ReadBool("Bakelink", "Tabelle1002", False)
                _ExportRohstoffe_Nwt = IniFile.ReadBool("Bakelink", "Tabelle1002Rohstoffe", False)
                _ExportRezepte_Aendrgn = IniFile.ReadBool("Bakelink", "RezepturUpdate", False)
                _ExportRezepte_Sauertg = IniFile.ReadBool("Bakelink", "SauerteigExport", False)

            Case "TwinCat"
                _TwinCatADS = IniFile.ReadBool("TwinCat", "TwinCatADS", False)
                _TwinCatADS_IP = IniFile.ReadString("TwinCat", "TwinCatIP", "127.0.0.1")
                _TwinCatADS_Port = IniFile.ReadInt("TwinCat", "TwinCatPort", 800)

        End Select
    End Sub

    ''' <summary>
    ''' Ändert die Sektions-Bezeichnung winback oder orgaback entsprechend der Mandanten-Nummer ab:
    ''' Aus winback wird winback-m1..winback-m6. 
    ''' Die gleiche Logik gilt auch für den Text orgaback.
    ''' 
    ''' Ist der Mandant nicht definiert oder gleich 0 wird die Original-Sektion zurückgegeben.
    ''' </summary>
    ''' <param name="Section"></param>
    ''' <returns></returns>
    Private Shared Function IniMandantSection(Section As String) As String
        If (MandantNr <> UNDEFINED) AndAlso (MandantNr <> 0) Then
            Return Section & "-m" & MandantNr.ToString
        Else
            Return Section
        End If
    End Function

    Public Shared KonfigurationTag As String
    Public Shared KonfigurationWert As String
    Public Shared Function getWinBackKonfiguration(Optional Key As String = "%") As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim Result As Boolean = False

        'Daten aus Tabelle Konfiguration lesen
        If winback.sqlSelect(wb_sql_Selects.setParams(wb_sql_Selects.sqlKonfiguration, Key)) Then

            While winback.Read
                'Daten gelesen
                Result = True

                'Tabelle winback.Konfiguration Tag
                KonfigurationTag = winback.sField("KF_Tag")
                'Tabelle winback.Konfiguration Wert
                KonfigurationWert = winback.sField("KF_Wert")

                'Globale Konfigurations-Einstellungen 
                Select Case KonfigurationTag
                    Case "KundenName"
                        _MandantName = KonfigurationWert
                    Case "DatenbankVersion"
                        _WinBackDBVersion = KonfigurationWert
                    Case "vts__anzahl_behaelter"
                        _SauerteigAnzBeh = KonfigurationWert
                    Case "Sprache1"
                        _WinBackLanguage1 = wb_Functions.StrToInt(KonfigurationWert)
                    Case "Sprache2"
                        _WinBackLanguage2 = wb_Functions.StrToInt(KonfigurationWert)
                    Case "SprachenVariante"
                        _WinBackLanguageVariante = wb_Functions.StrToInt(KonfigurationWert)
                    Case "IpBasisAdresse"
                        _IPBasisAdresse = KonfigurationWert

                    Case "KonfigChargenErfEin"
                        _RohChargen_ErfassungAktiv = KonfigurationWert
                    Case "KonfigGebindeGroessenTol"
                        _RohChargen_GebindeGrsTol = KonfigurationWert
                    Case "KonfigChargenErfVariante"
                        _RohChargen_ErfassungVariante = KonfigurationWert
                End Select
            End While
        End If

        winback.Close()
        Return Result
    End Function

    Public Shared Sub setWinBackKonfiguration(Key As String, value As String)
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'Daten in Tabelle Konfiguration schreiben
        winback.sqlCommand("UPDATE Konfiguration SET KF_WERT = " & value & " WHERE KF_Tag = " & Chr(34) & Key & Chr(34))
        winback.Close()
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
            Return wb_Credentials.WinBackUpdateSetupExe_64Bit
        End Get
    End Property
    Public Shared ReadOnly Property WinBackUpgradeExe
        Get
            Return wb_Credentials.WinBackUpgradeSetupExe_64Bit
        End Get
    End Property
    Public Shared ReadOnly Property WinBackUpgradeZip
        Get
            Return wb_Credentials.WinBackUpgradeZip_64Bit
        End Get
    End Property

    ''' <summary>
    ''' Programm-Variante setzen/abfragen.
    ''' Wenn die Programm-Variante nicht definiert ist, wird versucht anhand des Product-Namens
    ''' die Programm-Variante zu ermitteln. (Aufruf Stacktrace bevor die Prog-Variante gesetzt ist)
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property pVariante As wb_Global.ProgVariante
        Get
            If _pVariante = wb_Global.ProgVariante.Undef Then
                'Programm-Name
                Dim ProductName As String = (System.Windows.Forms.Application.ProductName).ToLower

                'Versuche die Programm-Variante anhand des Programm-Namens zu ermitteln              
                Select Case ProductName
                    Case "orgasoft.net"
                        _pVariante = ProgVariante.OrgaBack
                    Case "winBack background task"
                        _pVariante = ProgVariante.OBServerTask
                    Case "winback"
                        _pVariante = ProgVariante.WinBack
                    Case Else
                        If ProductName.StartsWith("orgasoft") Then
                            _pVariante = ProgVariante.OrgaBack
                        ElseIf ProductName.StartsWith("microsoft") Then
                            _pVariante = ProgVariante.UnitTest
                        Else
                            _pVariante = ProgVariante.Undef
                        End If
                End Select
            End If

            Return _pVariante
        End Get
        Set(value As wb_Global.ProgVariante)
            _pVariante = value
        End Set
    End Property

    ''' <summary>
    ''' Nur WinBack-AnyWhere
    ''' Anzeige Teigruhe und Kneter abwechselnd als FullHD 1920x1080
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property TeigruheAnzeige As Boolean
        Get
            Return _TeigruheAnzeige
        End Get
        Set(value As Boolean)
            _TeigruheAnzeige = value
        End Set
    End Property

    ''' <summary>
    ''' WinBack-AnyWhere
    ''' VNC-Linie für die Teigruhe-Anzeige (FullHD)
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property TeigruheVNC As Integer
        Get
            Return _TeigruheVNC
        End Get
        Set(value As Integer)
            _TeigruheVNC = value
        End Set
    End Property

    Public Shared Property Log4ViewExe As String
        Get
            If _Log4ViewExe = "" Then
                getWinBackIni("Logger")
            End If
            Return _Log4ViewExe
        End Get
        Set(value As String)
            _Log4ViewExe = value
            setWinBackIni("Logger", "Log4ViewExe", value)
        End Set
    End Property

    Public Shared Property NotePadPlusExe As String
        Get
            If _NotePadPlusExe = "" Then
                getWinBackIni("Logger")
            End If
            Return _NotePadPlusExe
        End Get
        Set(value As String)
            _NotePadPlusExe = value
            setWinBackIni("Logger", "NotePadPlusExe", value)
        End Set
    End Property

    ''' <summary>
    ''' Liest die Einstellung aus winback.Konfiguration
    ''' KonfigChargenErfEin       - Chargenerfassung aktiv
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property RohChargen_ErfassungAktiv As String
        Get
            If _RohChargen_ErfassungAktiv Is Nothing Then
                _RohChargen_ErfassungAktiv = "0"
                getWinBackKonfiguration()
            End If
            Return _RohChargen_ErfassungAktiv
        End Get
        Set(value As String)
            _RohChargen_ErfassungAktiv = value
        End Set
    End Property

    ''' <summary>
    ''' Liest die Einstellung aus winback.Konfiguration
    ''' KonfigGebindeGroessenTol  - Toleranzwert bis zur Erkennung
    '''                             Gebinde leer.
    '''                             (Bis unter diese Grenze darf ausdosiert werden)
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property RohChargen_GebindeGrsTol As String
        Get
            If _RohChargen_GebindeGrsTol Is Nothing Then
                _RohChargen_GebindeGrsTol = "0"
                getWinBackKonfiguration()
            End If
            Return _RohChargen_GebindeGrsTol
        End Get
        Set(value As String)
            _RohChargen_GebindeGrsTol = value
        End Set
    End Property

    ''' <summary>
    ''' Liest die Einstellung aus winback.Konfiguration
    ''' KonfigChargenErfVariante  - Verfahren nach Ablauf Gebinde
    '''
    '''     1 - darf weiter verwiegen, auch wenn Gebinde theoretisch
    '''         leer.
    '''     2 - darf nicht weiter verwiegen, wenn Verbrauch größer als
    '''         Gebindegröße +  KonfigGebindeGroessenTol.
    '''         
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property RohChargen_ErfassungVariante As String
        Get
            If _RohChargen_ErfassungVariante Is Nothing Then
                _RohChargen_ErfassungVariante = "0"
                getWinBackKonfiguration()
            End If
            Return _RohChargen_ErfassungVariante
        End Get
        Set(value As String)
            _RohChargen_ErfassungVariante = value
        End Set
    End Property

    Public Shared Property Datenlink_Url As String
        Get
            If _Datenlink_Url = "" Then
                getWinBackIni("Nährwerte")
            End If
            Return _Datenlink_Url
        End Get
        Set(value As String)
            _Datenlink_Url = value
        End Set
    End Property

    Public Shared Property Datenlink_CAT As String
        Get
            If _Datenlink_CAT = "" Then
                getWinBackIni("Nährwerte")
            End If
            Return _Datenlink_CAT
        End Get
        Set(value As String)
            _Datenlink_CAT = value
        End Set
    End Property

    Public Shared Property Datenlink_PAT As String
        Get
            If _Datenlink_PAT = "" Then
                getWinBackIni("Nährwerte")
            End If
            Return _Datenlink_PAT
        End Get
        Set(value As String)
            _Datenlink_PAT = value
        End Set
    End Property

    Public Shared Property WinBackCloud_Url As String
        Get
            If _WinBackCloud_Url = "" Then
                getWinBackIni("Nährwerte")
            End If
            Return _WinBackCloud_Url
        End Get
        Set(value As String)
            _WinBackCloud_Url = value
            If _WinBackCloud_Url <> "" Then
                setWinBackIni("Cloud", "URL", _WinBackCloud_Url)
            End If
        End Set
    End Property

    Public Shared Property WinBackCloud_Pass As String
        Get
            If _WinBackCloud_Pass = "" Then
                getWinBackIni("Nährwerte")
            End If
            Return _WinBackCloud_Pass
        End Get
        Set(value As String)
            _WinBackCloud_Pass = value
            If _WinBackCloud_Pass <> "" Then
                setWinBackIni("Cloud", "Pass", _WinBackCloud_Pass)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Verwendung der internen Deklaration anstelle der externen Deklaration
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property NwtInterneDeklaration As Boolean
        Get
            If _NwtInterneDeklaration = UNDEFINED Then
                getWinBackIni("Nährwerte")
            End If
            Return (_NwtInterneDeklaration = 1)
        End Get
        Set(value As Boolean)
            If value Then
                _NwtInterneDeklaration = 1
            Else
                _NwtInterneDeklaration = 0
            End If
            setWinBackIni("Artikel", "InterneDeklaration", _NwtInterneDeklaration.ToString)
        End Set
    End Property

    ''' <summary>
    ''' Ausgabe der Allergen ohne "Spuren von"
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property NwtAllergeneNoTrace As Boolean
        Get
            If _NwtAllergeneNoTrace = UNDEFINED Then
                getWinBackIni("Nährwerte")
            End If
            Return (_NwtAllergeneNoTrace = 1)
        End Get
        Set(value As Boolean)
            If value Then
                _NwtAllergeneNoTrace = 1
            Else
                _NwtAllergeneNoTrace = 0
            End If
            setWinBackIni("Artikel", "KonfigAllergeneNoTrace", _NwtAllergeneNoTrace.ToString)
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property NwtAllergeneTxtTrace As Boolean
        Get
            If _NwtAllergeneTxtTrace = UNDEFINED Then
                getWinBackIni("Nährwerte")
            End If
            Return (_NwtAllergeneTxtTrace = 1)
        End Get
        Set(value As Boolean)
            If value Then
                _NwtAllergeneTxtTrace = 1
            Else
                _NwtAllergeneTxtTrace = 0
            End If
            setWinBackIni("Artikel", "KonfigAllergeneTxtTrace", _NwtAllergeneTxtTrace.ToString)
        End Set
    End Property

    Public Shared Property NwtAllergeneNoText As Boolean
        Get
            If _NwtAllergeneNoText = UNDEFINED Then
                getWinBackIni("Nährwerte")
            End If
            Return (_NwtAllergeneTxtTrace = 1)
        End Get
        Set(value As Boolean)
            If value Then
                _NwtAllergeneTxtTrace = 1
            Else
                _NwtAllergeneTxtTrace = 0
            End If
            setWinBackIni("Artikel", "KeineAllergeneImText", _NwtAllergeneTxtTrace.ToString)
        End Set
    End Property

    ''' <summary>
    ''' Unterdrücken Ausgabe "Gluten", wenn die einzelnen Arten(Weizen, Roggen, Gerste ...) angegeben sind
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property NwtAllergeneGluten As Boolean
        Get
            If _NwtAllergeneGluten = UNDEFINED Then
                getWinBackIni("Nährwerte")
            End If
            Return (_NwtAllergeneTxtTrace = 1)
        End Get
        Set(value As Boolean)
            If value Then
                _NwtAllergeneTxtTrace = 1
            Else
                _NwtAllergeneTxtTrace = 0
            End If
            setWinBackIni("Artikel", "KeineAusgabeGluten", _NwtAllergeneTxtTrace.ToString)
        End Set
    End Property

    ''' <summary>
    ''' Unterdrücken Ausgabe "Schalenfrüchte", wenn die einzelnen Arten(Mandeln, Haselnüsse ...) angegeben sind
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property NwtAllergeneSchalen As Boolean
        Get
            If _NwtAllergeneTxtTrace = UNDEFINED Then
                getWinBackIni("Nährwerte")
            End If
            Return (_NwtAllergeneSchalen = 1)
        End Get
        Set(value As Boolean)
            If value Then
                _NwtAllergeneTxtTrace = 1
            Else
                _NwtAllergeneTxtTrace = 0
            End If
            setWinBackIni("Artikel", "KeineAusgabeSchalefruechte", _NwtAllergeneTxtTrace.ToString)
        End Set
    End Property

    ''' <summary>
    ''' Eintrag "keine Angabe" bei Allergene führt zu Fehlerausgabe in Rezeptur
    ''' (Rote Markierung)
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property NwtAllergeneNoDefinitionErr As Boolean
        Get
            If _NwtAllergeneNoDefinitionErr = UNDEFINED Then
                getWinBackIni("Nährwerte")
            End If
            Return (_NwtAllergeneNoDefinitionErr = 1)
        End Get
        Set(value As Boolean)
            If value Then
                _NwtAllergeneNoDefinitionErr = 1
            Else
                _NwtAllergeneNoDefinitionErr = 0
            End If
            setWinBackIni("Artikel", "AllergenKeineAngabeError", _NwtAllergeneNoDefinitionErr.ToString)
        End Set
    End Property

    ''' <summary>
    ''' E-Nummern in Zutatenliste anzeigen/verbergen
    ''' Interne Einstellung für die Anzeige der Zutatenliste im der Rezeptur-Fenster
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property NwtShowENummer As Boolean
        Get
            If _NwtShowENummer = UNDEFINED Then
                getWinBackIni("Nährwerte")
            End If
            Return (_NwtShowENummer = 1)
        End Get
        Set(value As Boolean)
            If value Then
                _NwtShowENummer = 1
            Else
                _NwtShowENummer = 0
            End If
            setWinBackIni("Artikel", "ENummernAnzeigen", _NwtShowENummer.ToString)
        End Set
    End Property

    ''' <summary>
    ''' E-Nummern in der Zutatenliste anzeigen/verbergen
    ''' Einstellung für den Export nach OrgaBack und den Ausdruck des Artikelinformations-Blattes
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property NwtENummerZutatenListe As Boolean
        Get
            If _NwtENummerZutatenListe = UNDEFINED Then
                getWinBackIni("Nährwerte")
            End If
            Return (_NwtENummerZutatenListe = 1)
        End Get
        Set(value As Boolean)
            If value Then
                _NwtENummerZutatenListe = 1
            Else
                _NwtENummerZutatenListe = 0
            End If
            setWinBackIni("Artikel", "ENummernZutatenliste", _NwtENummerZutatenListe.ToString)
        End Set
    End Property

    ''' <summary>
    ''' Zutatenliste optimieren (doppelte Einträge entfernen)
    ''' Interne Einstellung für die Anzeige der Zutatenliste im der Rezeptur-Fenster
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property NwtShowOptimized As Integer
        Get
            If _NwtShowOptimized = UNDEFINED Then
                getWinBackIni("Nährwerte")
            End If
            Return (_NwtShowOptimized = 1)
        End Get
        Set(value As Integer)
            If value Then
                _NwtShowOptimized = 1
            Else
                _NwtShowOptimized = 0
            End If
            setWinBackIni("Artikel", "ZutatenOptimiertAnzeigen", _NwtShowOptimized.ToString)
        End Set
    End Property

    ''' <summary>
    ''' Zutatenliste Quid-Berechnung durchführen und in der Zutatenliste aus Prozentangabe einfügen
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property NwtCalcQuid As Integer
        Get
            'TODO in Winback.ini und OrgaBack  Konfig eintragen
            Return True
            'Return _NwtCalcQuid
        End Get
        Set(value As Integer)
            _NwtCalcQuid = value
        End Set
    End Property

    ''' <summary>
    ''' Zutatenliste optimieren (doppelte Einträge entfernen)
    ''' Einstellung für den Export nach OrgaBack und den Ausdruck des Artikelinformations-Blattes
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property NwtOptimizeZutatenListe As Boolean
        Get
            If _NwtOptimizeZutatenListe = UNDEFINED Then
                getWinBackIni("Nährwerte")
            End If
            Return (_NwtOptimizeZutatenListe = 1)
        End Get
        Set(value As Boolean)
            If value Then
                _NwtOptimizeZutatenListe = 1
            Else
                _NwtOptimizeZutatenListe = 0
            End If
            setWinBackIni("Artikel", "ZutatenListeOptimieren", _NwtOptimizeZutatenListe.ToString)
            _NwtOptimizeZutatenListe = value
        End Set
    End Property

    ''' <summary>
    ''' Globales Flag - Mehlzusammensetzung berechnen.
    ''' Falls erforderlich kann die Berechnung der Mehlanteile global abgeschaltet werden.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property NwtCalcMehlZusammensetzung As Boolean
        Get
            'vorläufig immer True
            Return True
        End Get
        Set(value As Boolean)
            _NwtMehlZusammensetzung = value
        End Set
    End Property

    Public Shared Property WinBackBackgroudTaskConnected As Boolean
        Get
            Return _WinBackBackgroudTaskConnected
        End Get
        Set(value As Boolean)
            _WinBackBackgroudTaskConnected = value
        End Set
    End Property

    Public Shared Property DefaultSchnittstelle As String
        Get
            If _DefaultSchnittstelle Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _DefaultSchnittstelle
        End Get
        Set(value As String)
            If value <> "" Then
                _DefaultSchnittstelle = value
                setWinBackIni("Bakelink", "ConnectType", value)
            End If
        End Set
    End Property

    Public Shared Property ImportPath As String
        Get
            If _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ImportPath
        End Get
        Set(value As String)
            If value <> "" Then
                _ImportPath = value
                setWinBackIni("Bakelink", "ImportPfad", value)
            End If
        End Set
    End Property

    Public Shared Property ExportPath As String
        Get
            If _ExportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ExportPath
        End Get
        Set(value As String)
            If value <> "" Then
                _ExportPath = value
                setWinBackIni("Bakelink", "ExportPfad", value)
            End If
        End Set
    End Property

    Public Shared Property ImportArtikel As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ImportArtikel
        End Get
        Set(value As Boolean)
            _ImportArtikel = value
            setWinBackIni("Bakelink", "ImportArtikel", value)
        End Set
    End Property

    Public Shared Property ImportRezeptKopf As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ImportRezeptKopf
        End Get
        Set(value As Boolean)
            _ImportRezeptKopf = value
            setWinBackIni("Bakelink", "ImportRezeptKopf", value)
        End Set
    End Property

    Public Shared Property ImportRezept As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ImportRezept
        End Get
        Set(value As Boolean)
            _ImportRezept = value
            setWinBackIni("Bakelink", "ImportRezept", value)
        End Set
    End Property

    Public Shared Property ImportLieferungen As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ImportLieferungen
        End Get
        Set(value As Boolean)
            _ImportLieferungen = value
            setWinBackIni("Bakelink", "ImportLieferungen", value)
        End Set
    End Property

    Public Shared Property ImportBackzettel As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ImportBackzettel
        End Get
        Set(value As Boolean)
            _ImportBackzettel = value
            setWinBackIni("Bakelink", "ImportBackzettel", value)
        End Set
    End Property

    Public Shared Property ImportRzpVerarbeitungsHinweise As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ImportRzpVerarbeitungsHinweise
        End Get
        Set(value As Boolean)
            _ImportRzpVerarbeitungsHinweise = value
            setWinBackIni("Bakelink", "Rezeptverarbeitungshinweise", value)
        End Set
    End Property

    Public Shared Property ImportRzptUpdate As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ImportRzptUpdate
        End Get
        Set(value As Boolean)
            _ImportRzptUpdate = value
            setWinBackIni("Bakelink", "RezepturUpdate", value)
        End Set
    End Property

    Public Shared Property ImportRzptWasserSpeichern As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ImportRzptWasserSpeichern
        End Get
        Set(value As Boolean)
            _ImportRzptWasserSpeichern = value
            setWinBackIni("Bakelink", "WasserTempSpeichern", value)
        End Set
    End Property

    Public Shared Property ImportProdArtikelRezept As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ImportProdArtikelRezept
        End Get
        Set(value As Boolean)
            _ImportProdArtikelRezept = value
            setWinBackIni("Bakelink", "ListeArtikelRezept", value)
        End Set
    End Property

    Public Shared Property ImportProdFTP As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ImportProdFTP
        End Get
        Set(value As Boolean)
            _ImportProdFTP = value
            setWinBackIni("Bakelink", "ProdListeAutoCreate", value)
        End Set
    End Property

    Public Shared Property ExportArtikel As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ExportArtikel
        End Get
        Set(value As Boolean)
            _ExportArtikel = value
            setWinBackIni("Bakelink", "ExportArtikel", value)
        End Set
    End Property

    Public Shared Property ExportRohstoffe As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ExportRohstoffe
        End Get
        Set(value As Boolean)
            _ExportRohstoffe = value
            setWinBackIni("Bakelink", "ExportRohstoffe", value)
        End Set
    End Property

    Public Shared Property ExportRezepte As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ExportRezepte
        End Get
        Set(value As Boolean)
            _ExportRezepte = value
            setWinBackIni("Bakelink", "ExportRezepte", value)
        End Set
    End Property

    Public Shared Property ExportChargen As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ExportChargen
        End Get
        Set(value As Boolean)
            _ExportChargen = value
            setWinBackIni("Bakelink", "ExportChargen", value)
        End Set
    End Property

    Public Shared Property ExportChargenTWNr As Integer
        Get
            Return _ExportChargenTWNr
        End Get
        Set(value As Integer)
            _ExportChargenTWNr = value
            setWinBackIni("Bakelink", "ExportChargenTWNr", value.ToString)
        End Set
    End Property

    Public Shared Property ExportArtikel_Nwt As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ExportArtikel_Nwt
        End Get
        Set(value As Boolean)
            _ExportArtikel_Nwt = value
            setWinBackIni("Bakelink", "Tabelle1002", value)
        End Set
    End Property

    Public Shared Property ExportRohstoffe_Nwt As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ExportRohstoffe_Nwt
        End Get
        Set(value As Boolean)
            _ExportRohstoffe_Nwt = value
            setWinBackIni("Bakelink", "Tabelle1002Rohstoffe", value)
        End Set
    End Property

    Public Shared Property ExportRezepte_Aendrgn As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ExportRezepte_Aendrgn
        End Get
        Set(value As Boolean)
            _ExportRezepte_Aendrgn = value
            setWinBackIni("Bakelink", "RezepturUpdate", value)
        End Set
    End Property

    Public Shared Property ExportRezepte_Sauertg As Boolean
        Get
            If _ExportPath Is Nothing Or _ImportPath Is Nothing Then
                getWinBackIni("Bakelink")
            End If
            Return _ExportRezepte_Sauertg
        End Get
        Set(value As Boolean)
            _ExportRezepte_Sauertg = value
            setWinBackIni("Bakelink", "SauerteigExport", value)
        End Set
    End Property

    Public Shared Property TwinCatADS As Boolean
        Get
            If _TwinCatADS = Nothing Then
                getWinBackIni("TwinCat")
            End If
            Return _TwinCatADS
        End Get
        Set(value As Boolean)
            _TwinCatADS = value
        End Set
    End Property

    Public Shared Property TwinCatADS_IP As String
        Get
            If _TwinCatADS_IP = Nothing Then
                getWinBackIni("TwinCat")
            End If
            Return _TwinCatADS_IP
        End Get
        Set(value As String)
            _TwinCatADS_IP = value
        End Set
    End Property

    Public Shared Property TwinCatADS_Port As Integer
        Get
            Return _TwinCatADS_Port
        End Get
        Set(value As Integer)
            _TwinCatADS_Port = value
        End Set
    End Property

    Public Shared ReadOnly Property WinBackLanguage1 As Integer
        Get
            If _WinBackLanguage1 = wb_Global.UNDEFINED Then
                getWinBackKonfiguration()
            End If
            Return _WinBackLanguage1
        End Get
    End Property

    Public Shared ReadOnly Property WinBackLanguage2 As Integer
        Get
            If _WinBackLanguage2 = wb_Global.UNDEFINED Then
                getWinBackKonfiguration()
            End If
            Return _WinBackLanguage2
        End Get
    End Property

    ''' <summary>
    ''' Unterdrückt die Meldung "Die Nährwerte sind erst nach erneutem Aufrud des Artikels in OrgaBack sichtbar"
    ''' Wird bei Programm-Start wieder auf True gesetzt
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property ShowMsg_UpdateOrgaBackNWT As Boolean
        Get
            Return _ShowMsg_UpdateOrgaBackNWT
        End Get
        Set(value As Boolean)
            _ShowMsg_UpdateOrgaBackNWT = value
        End Set
    End Property

    Public Shared Property ShowMsg_RohstoffeSTK As Boolean
        Get
            Return _ShowMsg_RohstoffeSTK
        End Get
        Set(value As Boolean)
            _ShowMsg_RohstoffeSTK = value
        End Set
    End Property

End Class
