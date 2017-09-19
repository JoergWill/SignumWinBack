Imports System.IO

Public Class wb_Admin_Shared
    'Puffer Log-Einträge
    Shared _LogEvents As New List(Of String)
    'wird vor dem Ende der Applikation aufgerufen
    Private Shared Finalizer As New wb_Finalizer_Shared()
    Public Shared Event NewLogText(txt As String)

    ''' <summary>
    ''' Beim Starten der Applikation wird das alte Log-File gelöscht.
    ''' </summary>
    Shared Sub New()
        ' Set a variable to the My Documents path.
        Dim mydocpath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'alte Log-Files löschen
        File.Delete(mydocpath & Convert.ToString(wb_Global.LogFileName))
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

    ''' <summary>
    ''' Hier landen alle Trace/Debug-Events
    ''' </summary>
    ''' <param name="Txt"></param>
    Public Shared Sub GetTraceListenerText(Txt As String)
        'Log-Events an Liste anhängen
        _LogEvents.Add(Txt & vbCr)

        'Log-Event Ergeignis auslösen (AdminLog ist geöffnet - Live-Anzeige)
        RaiseEvent NewLogText(Txt)

        'Log-Events in Text-Datei schreiben
        If _LogEvents.Count > wb_Global.LogFileEntries Then
            If wb_GlobalSettings.LogToTextFile Then
                WriteToEventFile()
            Else
                _LogEvents.RemoveAt(0)
            End If
        End If

        'Log-Event in Windows-Ereignisprotkollschreiben
        'TODO das Schreiben in das Windows-Ereignisprotokoll funktioniert nicht !! (siehe auch Unit-Test)
        'WriteToEventLog(Txt)
    End Sub

    ''' <summary>
    ''' Write Entry to Event Log using VB.NET
    ''' NOTE: EventSources are tightly tied to their log. So don't use the same source name for different logs, and vice versa.
    ''' </summary>
    ''' <param name="Entry">Value to Write</param>
    ''' <param name="EventType">Entry Type, from EventLogEntryType Structure e.g., EventLogEntryType.Warning, EventLogEntryType.Error</param>
    ''' <param name="LogName">Name of Log (System, Application; Security is read-only) If you specify a non-existent log, the log will be created</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function WriteToEventLog(ByVal Entry As String, Optional ByVal EventType As EventLogEntryType = EventLogEntryType.Information, Optional ByVal LogName As String = "Application") As Boolean
        Dim appname As String = My.Application.Info.Title
        Dim objEventLog As New EventLog()

        Try
            'Register the App as an Event Source
            If Not EventLog.SourceExists(appname) Then
                EventLog.CreateEventSource(appname, LogName)
            End If

            objEventLog.Source = appname

            'WriteEntry is overloaded; this is one
            'of 10 ways to call it
            objEventLog.WriteEntry(Entry, EventType)
            Return True
        Catch Ex As Exception
            Return False
        End Try
    End Function

    Public Shared Sub WriteToEventFile()
        ' Set a variable to the My Documents path.
        Dim mydocpath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        ' Write the text to a existing file
        File.AppendAllLines(mydocpath & Convert.ToString(wb_Global.LogFileName), _LogEvents)
        _LogEvents.Clear()
    End Sub

End Class
