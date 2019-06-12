Imports System.IO

Public Class wb_Admin_Shared
    'Puffer Log-Einträge
    Shared _LogEvents As New List(Of String)
    Shared _Logger4net As log4net.ILog
    Shared _LoggerKonfigOK As Boolean = False
    Shared _LoggerAktiv As Boolean = False

    'wird vor dem Ende der Applikation aufgerufen
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

        'Pfad zum log4net-Konfigurations-File
        Dim Log4NetKonfigFileInfo As FileInfo
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack Then
            Log4NetKonfigFileInfo = New FileInfo(wb_GlobalSettings.pProgrammPath & wb_Global.Log4NetConfigFile)
        Else
            Log4NetKonfigFileInfo = New FileInfo(wb_GlobalSettings.pAddInPath & wb_Global.Log4NetConfigFile)
        End If

        'Prüfen ob der Pfad zum log4net-Konfigurations-File existiert
        If Log4NetKonfigFileInfo.Exists Then
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
            Catch
                MsgBox("Log4net - Fehler in der Konfiguration !", MsgBoxStyle.Exclamation, "Log4Net-Konfiguration")
            End Try
        Else
            'Log-Meldung ausgeben
            AddLogToList("Log4net - Konfigurations-Datei wurde nicht gefunden !!")
        End If
    End Sub

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
            If value And LoggerKonfigOK Then
                _LoggerAktiv = True
            Else
                _LoggerAktiv = False
            End If
        End Set
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
    Public Shared Sub GetTraceListenerText(trDate As String, trBenutzer As String, trTxt As String, Optional trException As Exception = Nothing, Optional trZeile As String = "", Optional trModul As String = "", Optional trSubRoutine As String = "")
        'Message dekodieren
        Dim LogLevel As String = Left(trTxt, 3)
        If LogLevel(0) = "@" And LogLevel(2) = "_" Then
            trTxt = Mid(trTxt, 4)
        End If

        'Log-Events an Liste anhängen
        If trZeile <> "" Then
            AddLogToList(trDate & vbTab & trZeile & " " & trModul & vbTab & trTxt & vbCrLf)
        Else
            AddLogToList(trDate & vbTab & trTxt & vbCrLf)
        End If

        'Apache log4net
        If _LoggerKonfigOK And LoggerAktiv Then
            Using log4net.ThreadContext.Stacks("Benutzer").Push(trBenutzer)
                Using log4net.ThreadContext.Stacks("Zeile").Push(trZeile)
                    Using log4net.ThreadContext.Stacks("Modul").Push(trModul)
                        Using log4net.ThreadContext.Stacks("Funktion").Push(trSubRoutine)
                            Select Case LogLevel
                                Case "@V_", "@T_", "@D_"
                                    _Logger4net.Debug(trTxt, trException)
                                Case "@I_", "@N_"
                                    _Logger4net.Info(trTxt, trException)
                                Case "@W_"
                                    _Logger4net.Warn(trTxt, trException)
                                Case "@E_", "@C_"
                                    _Logger4net.Error(trTxt, trException)
                                Case "@A_", "@F_"
                                    _Logger4net.Fatal(trTxt, trException)
                                Case Else
                                    _Logger4net.Debug(trTxt, trException)
                            End Select
                        End Using
                    End Using
                End Using
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Log-Meldungen in Liste(wb_Global.LogFileEntries) anhängen. Wenn die Liste zu lang wird,
    ''' werden die obersten Einträge gelöscht.
    ''' </summary>
    ''' <param name="Txt"></param>
    Private Shared Sub AddLogToList(Txt As String)
        'Log-Events an Liste anhängen
        _LogEvents.Add(Txt & vbCr)

        'Log-Event Ergeignis auslösen (AdminLog ist geöffnet - Live-Anzeige)
        RaiseEvent NewLogText(Txt)

        'Log-Events max Anzeige
        If _LogEvents.Count > wb_Global.LogFileEntries Then
            _LogEvents.RemoveAt(0)
        End If
    End Sub

End Class
