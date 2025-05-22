Imports System.IO

Public Class wb_Admin_Shared
    'Puffer Log-Einträge
    Shared _LogEvents As New List(Of String)
    Shared _Logger4net As log4net.ILog
    Shared _LoggerKonfigOK As Boolean = False
    Shared _LoggerAktiv As Boolean = False
    Private Shared _Log4NetKonfigTyp As wb_Global.Log4NetType

    'wird vor dem Ende der Applikation aufgerufen
    <CodeAnalysis.SuppressMessage("Style", "IDE0044:Modifizierer ""readonly"" hinzufügen", Justification:="<Ausstehend>")>
    Private Shared Finalizer As New wb_Finalizer_Shared()
    Public Shared Event NewLogText(txt As String)

    ''' <summary>
    ''' Initialisierung Apache-log4net. Die Konfiguration steht in winback.log4net und wird dynamisch gelesen.
    ''' Änderungen im Konfigurations-File sind während der Laufzeit möglich!
    ''' 
    ''' Bei Programmvariante OrgaBack liegt die Konfiguration im AddIn-Verzeichnis, bei Variante WinBack
    ''' im Programm-Verzeichnis.
    ''' </summary>
    Shared Sub New()
        'Setze Flag LoggerAktiv - bei Programmstart
        _LoggerAktiv = wb_GlobalSettings.Log4netAutoStart
        'Logger-Konfiguration aus Datei winback.log4net lesen
        If _LoggerAktiv Then
            LoadLoggerKonfigFile()
        End If
    End Sub

    Public Shared ReadOnly Property UpdateDatabaseFile As String
        Get
            Return "2.30_Log4Net.sql"
        End Get
    End Property

    Public Shared Sub LoadLoggerKonfigFile()
        'Pfad zum log4net-Konfigurations-File
        Dim Log4NetKonfigFileInfo As FileInfo
        'Verzeichnis abhängig von der Programm-Variante - Lade die Datei WinBack.log4net
        Select Case wb_GlobalSettings.pVariante
            Case wb_Global.ProgVariante.WinBack
                Log4NetKonfigFileInfo = New FileInfo(wb_GlobalSettings.pProgrammPath & wb_Global.Log4NetConfigFile)
            Case wb_Global.ProgVariante.Setup
                CreateSimpleLog4NetConfigFile(wb_GlobalSettings.pProgrammPath & wb_Global.Log4NetConfigFile)
                Log4NetKonfigFileInfo = New FileInfo(wb_GlobalSettings.pProgrammPath & wb_Global.Log4NetConfigFile)
            Case Else
                Log4NetKonfigFileInfo = New FileInfo(wb_GlobalSettings.pAddInPath & wb_Global.Log4NetConfigFile)
        End Select

        'Prüfen ob der Pfad zum log4net-Konfigurations-File existiert
        If Log4NetKonfigFileInfo.Exists Then
            'Auslesen, welcher Logger-Typ geladen wird
            _Log4NetKonfigTyp = ReadLog4NetKonfigTyp(Log4NetKonfigFileInfo)


            'Apache log4net initialisieren
            Try
                log4net.Config.XmlConfigurator.ConfigureAndWatch(Log4NetKonfigFileInfo)
                _Logger4net = log4net.LogManager.GetLogger("Log4NetWinBackAddIn")
                'Prüfen ob die Logger-Konfiguration richtig geladen wurde
                If Not _Logger4net.Logger.Repository.Configured Then
                    MsgBox("Log4net - Die Konfiguration konnte nicht geladen werden !", MsgBoxStyle.Exclamation, "Log4Net-Konfiguration")
                Else
                    _LoggerKonfigOK = True
                End If
                'Activate internal Debugging-Messages
#If DebugLog4Net Then
                'Aktiviert die internen Messages des log4net-Moduls beim Laden der Konfiguration
               log4net.Util.LogLog.InternalDebugging = True
#End If
            Catch
                MsgBox("Log4net - Fehler in der Konfiguration !", MsgBoxStyle.Exclamation, "Log4Net-Konfiguration")
            End Try
        Else
            'Log-Meldung ausgeben
            If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.Setup Then
                MsgBox("Log4net - Konfigurations-Datei wurde nicht gefunden !! " & vbCrLf & Log4NetKonfigFileInfo.FullName, MsgBoxStyle.Exclamation, "Log4Net-Konfiguration")
            Else
                AddLogToList(Date.Now.ToString("yyyy-MM-dd hh:mm:ss"), "", "", "Log4net - Konfigurations-Datei wurde nicht gefunden !!")
            End If
        End If
    End Sub

    Public Shared Function CheckLog4NetEnvironment() As Boolean

        'abhängig vom Logger-Typ prüfen ob die Voraussetzungen stimmen
        Select Case wb_Admin_Shared.Log4NetKonfigTyp

            Case wb_Global.Log4NetType.File
                'TODO Prüfen ob das Verzeichnis existiert
                Return False

            Case wb_Global.Log4NetType.Udp
                'TODO Prüfen ob Netzwerk-Verbindung aktiv
                Return False

            Case wb_Global.Log4NetType.MySQL
                'Prüfen ob die Datenbank-Tabelle winback.log4Net exisitiert
                If Not wb_sql_Functions.MySQLTableExist("log4Net") Then
                    Return False
                End If

            Case wb_Global.Log4NetType.Undef
                Return True
        End Select
        Return True
    End Function

    ''' <summary>
    ''' Ausgeben alle (noch nicht gespeicherten Log-Events)
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property LogEvents As List(Of String)
        Get
            Return _LogEvents
        End Get
    End Property

    Public Shared ReadOnly Property LoggerKonfigOK As Boolean
        Get
            Return _LoggerKonfigOK
        End Get
    End Property

    Public Shared Property LoggerAktiv As Boolean
        Get
            Return _LoggerAktiv
        End Get
        Set(value As Boolean)
            If value AndAlso LoggerKonfigOK Then
                _LoggerAktiv = True
            Else
                _LoggerAktiv = False
            End If
        End Set
    End Property

    Public Shared ReadOnly Property Log4NetKonfigTyp As wb_Global.Log4NetType
        Get
            Return _Log4NetKonfigTyp
        End Get
    End Property

    ''' <summary>
    ''' Hier landen alle Trace/Debug-Events
    ''' Aufruf bzw. Umleitung in ob_Main_Menu: AddHandler xLogger.WriteText, AddressOf wb_Admin_Shared.GetTraceListenerText
    ''' 
    '''     Debug.Print/Trace.Write/Trace.Writeln - Ausgaben werden über Trace.AddListener() auf wb_TraceListener umgeleitet.
    '''     
    '''     wb_TraceListener.Write(String)                  \
    '''     wb_TraceListener.Writeln(String)                 +-- leiten die Message über den Event WriteText an wb_AdminShared.GetTraceListenerText() weiter
    '''     wb_TraceListener.Writeln(Exception-Object)      /
    '''     
    ''' 
    ''' Die Log-Level sind hierarchisch aufgebaut
    ''' 
    '''     Level.Verbose        10 000         Trace.Writeln("@V_xxxxxx")
    '''     Level.Trace          20 000         Trace.Writeln("@T_xxxxxx") oder Trace.Writeln
    '''     Level.Debug          30 000         Trace.Writeln("@D_xxxxxx") oder Debug.Print
    '''     Level.Info           40 000         Trace.Writeln("@I_xxxxxx")
    '''     Level.Notice         50 000         Trace.Writeln("@N_xxxxxx")
    '''
    '''     Level.Warn           60 000         Trace.Writeln("@W_xxxxxx")
    '''     Level.Error          70 000         Trace.Writeln("@E_xxxxxx")
    '''     Level.Severe         80 000         Trace.Writeln("@S_xxxxxx")
    '''     Level.Critical       90 000         Trace.Writeln("@C_xxxxxx")
    '''     Level.Alert         100 000         Trace.Writeln("@A_xxxxxx")
    '''     Level.Fatal         110 000         Trace.Writeln("@F_xxxxxx")
    '''     Level.Emergency     120 000         Trace.Writeln("@X_xxxxxx")
    '''     
    ''' </summary>
    ''' <param name="trTxt"></param>
    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
    Public Shared Sub GetTraceListenerText(trDate As String, trBenutzer As String, trTxt As String, Optional trException As Exception = Nothing, Optional trZeile As String = "", Optional trModul As String = "", Optional trSubRoutine As String = "")
        'Prüfen ob ein gültiger Fehler-Text übergeben wurde
        If Len(trTxt) > 3 Then
            'Message dekodieren
            Dim LogLevel As String = Left(trTxt, 3)
            If LogLevel(0) = "@" AndAlso LogLevel(2) = "_" Then
                trTxt = Mid(trTxt, 4)
            End If

            Select Case LogLevel
                Case "@V_", "@T_", "@D_"
                    If wb_GlobalSettings.Log4net_DebugLevel Then
                        AddLogToList(trDate, trZeile, trModul, trTxt)
                    End If
                Case "@I_", "@N_"
                    If wb_GlobalSettings.Log4net_InfoLevel Then
                        AddLogToList(trDate, trZeile, trModul, trTxt)
                    End If
                Case "@W_"
                    If wb_GlobalSettings.Log4net_WarnLevel Then
                        AddLogToList(trDate, trZeile, trModul, trTxt)
                    End If
                Case "@E_", "@C_"
                    If wb_GlobalSettings.Log4net_LL_ErrorLevel Then
                        AddLogToList(trDate, trZeile, trModul, trTxt)
                    End If
                Case "@A_", "@F_"
                    'Log-Events an Liste anhängen
                    AddLogToList(trDate, trZeile, trModul, trTxt)
                Case Else
                    If wb_GlobalSettings.Log4net_DebugLevel Then
                        AddLogToList(trDate, trZeile, trModul, trTxt)
                    End If
            End Select

            'Apache log4net
            If _LoggerKonfigOK AndAlso LoggerAktiv Then
                Using log4net.ThreadContext.Stacks("Benutzer").Push(trBenutzer)
                    Using log4net.ThreadContext.Stacks("Zeile").Push(trZeile)
                        Using log4net.ThreadContext.Stacks("Modul").Push(trModul)
                            Using log4net.ThreadContext.Stacks("Funktion").Push(trSubRoutine)
                                Select Case LogLevel
                                    Case "@V_", "@T_", "@D_"
                                        If wb_GlobalSettings.Log4net_DebugLevel Then
                                            _Logger4net.Debug(trTxt, trException)
                                        End If
                                    Case "@I_", "@N_"
                                        If wb_GlobalSettings.Log4net_InfoLevel Then
                                            _Logger4net.Info(trTxt, trException)
                                        End If
                                    Case "@W_"
                                        If wb_GlobalSettings.Log4net_WarnLevel Then
                                            _Logger4net.Warn(trTxt, trException)
                                        End If
                                    Case "@E_", "@C_"
                                        If wb_GlobalSettings.Log4net_LL_ErrorLevel Then
                                            _Logger4net.Error(trTxt, trException)
                                        End If
                                    Case "@A_", "@F_"
                                        _Logger4net.Fatal(trTxt, trException)
                                    Case Else
                                        If wb_GlobalSettings.Log4net_DebugLevel Then
                                            _Logger4net.Debug(trTxt, trException)
                                        End If
                                End Select
                            End Using
                        End Using
                    End Using
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' Log-Meldungen in Liste(wb_Global.LogFileEntries) anhängen. Wenn die Liste zu lang wird,
    ''' werden die obersten Einträge gelöscht.
    ''' </summary>
    Private Shared Sub AddLogToList(trDate As String, trZeile As String, trModul As String, trTxt As String)
        Dim Txt As String = trDate & vbTab
        If trZeile <> "" Then
            Txt += trZeile & " " & trModul & vbTab & trTxt & vbCrLf
        Else
            Txt += trTxt & vbCrLf
        End If

        'Log-Events an Liste anhängen
        _LogEvents.Add(Txt & vbCr)

        'Log-Event Ergeignis auslösen (AdminLog ist geöffnet - Live-Anzeige)
        RaiseEvent NewLogText(Txt)

        'Log-Events max Anzeige
        If _LogEvents.Count > wb_Global.LogFileEntries Then
            _LogEvents.RemoveAt(0)
        End If
    End Sub

    ''' <summary>
    ''' List aus dem Log4Net-Konfiguration-File(XML) die Type aus und gibt diese als String zurück
    ''' Diese Typen sind momentan definiert:
    ''' 
    '''     log4net.Appender.RollingFileAppender
    '''     log4net.Appender.UdpAppender
    '''     log4net.Appender.AdoNetAppender
    '''     
    ''' Die Infoormation über den Logger-Typ steht im Xml-File unter
    '''     log4net.appender.type
    '''     
    ''' </summary>
    ''' <param name="Log4NetKonfigFileInfo"></param>
    ''' <returns></returns>
    Private Shared Function ReadLog4NetKonfigTyp(Log4NetKonfigFileInfo As FileInfo) As wb_Global.Log4NetType

        ' get the response stream so we can read it
        Dim Log4NetFile As FileStream = Log4NetKonfigFileInfo.OpenRead
        ' create a stream reader to read the response
        Dim Log4NetFileReader As StreamReader = New IO.StreamReader(Log4NetFile)
        ' read the response text (this should be javascript)
        Dim Log4NetKonfig = Log4NetFileReader.ReadToEnd()

        ' load the response into an XDocument
        Dim xmlDocument = XDocument.Parse(Log4NetKonfig)

        For Each x In xmlDocument...<appender>
            For Each a In x.Attributes
                Debug.Print(a.Name.ToString & " " & a.Value)
                If a.Name = "type" Then
                    Select Case a.Value
                        Case "log4net.Appender.RollingFileAppender"
                            Return wb_Global.Log4NetType.File
                        Case "log4net.Appender.UdpAppender"
                            Return wb_Global.Log4NetType.Udp
                        Case "log4net.Appender.AdoNetAppender"
                            Return wb_Global.Log4NetType.MySQL
                        Case Else
                            Return wb_Global.Log4NetType.Undef
                    End Select
                End If
            Next
        Next
        Return wb_Global.Log4NetType.Undef
    End Function
    Private Shared Sub CreateSimpleLog4NetConfigFile(FName As String)
        'Pass the file path and the file name to the StreamWriter constructor.
        Dim objStreamWriter As New StreamWriter(FName)

        'Write a line(s) of text.
        objStreamWriter.WriteLine("<?xml version=" & quot("1.0") & " encoding=" & quot("utf-8") & "?>")
        objStreamWriter.WriteLine("<configuration>")
        objStreamWriter.WriteLine("<log4net>")
        objStreamWriter.WriteLine("<appender name = " & quot("RollingLogFileAppender") & " type=" & quot("log4net.Appender.RollingFileAppender") & ">")
        objStreamWriter.WriteLine("<file value = " & quot("${TMP}\WinBack.log") & " />")
        objStreamWriter.WriteLine("<appendToFile value=" & quot("False") & " />")
        objStreamWriter.WriteLine("<rollingStyle value = " & quot("Size") & " />")
        objStreamWriter.WriteLine("<maxSizeRollBackups value=" & quot("3") & " />")
        objStreamWriter.WriteLine("<maximumFileSize value =" & quot("100KB") & " />")
        objStreamWriter.WriteLine("<staticLogFileName value=" & quot("True") & " />")
        objStreamWriter.WriteLine("<layout type=" & quot("log4net.Layout.PatternLayout") & " >")
        objStreamWriter.WriteLine("<conversionPattern value=" & quot("%Date %level %Property{Benutzer} [%Property{Zeile} %Property{Modul}.%Property{Funktion}] %message%newline") & " />")
        objStreamWriter.WriteLine("</layout>")
        objStreamWriter.WriteLine("</appender>")
        objStreamWriter.WriteLine("<logger name = " & quot("Log4NetWinBackAddIn") & " >")
        objStreamWriter.WriteLine("<level value=" & quot("ALL") & "/>")
        objStreamWriter.WriteLine("<appender-ref ref= " & quot("RollingLogFileAppender") & " />")
        objStreamWriter.WriteLine("</logger>")
        objStreamWriter.WriteLine("</log4net>")
        objStreamWriter.WriteLine("</configuration>")

        'Close the file
        objStreamWriter.Close()
    End Sub

    Private Shared Function quot(s As String) As String
        Return Chr(34) & s & Chr(34)
    End Function
End Class
