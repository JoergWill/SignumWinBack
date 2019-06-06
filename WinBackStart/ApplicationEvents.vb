Namespace My
    ' Für MyApplication sind folgende Ereignisse verfügbar:
    ' Startup: Wird beim Starten der Anwendung noch vor dem Erstellen des Startformulars ausgelöst.
    ' Shutdown: Wird nach dem Schließen aller Anwendungsformulare ausgelöst. Dieses Ereignis wird nicht ausgelöst, wenn die Anwendung mit einem Fehler beendet wird.
    ' UnhandledException: Wird bei einem Ausnahmefehler ausgelöst.
    ' StartupNextInstance: Wird beim Starten einer Einzelinstanzanwendung ausgelöst, wenn die Anwendung bereits aktiv ist. 
    ' NetworkAvailabilityChanged: Wird beim Herstellen oder Trennen der Netzwerkverbindung ausgelöst.
    Partial Friend Class MyApplication

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            'Stacktrace und Fehlermeldung ermitteln
            'TODO funktioniert nicht bei Einzelanwedung Winback
            Dim StackTrace As String = DirectCast(e.Exception, System.Exception).StackTrace
            Dim Message As String = DirectCast(e.Exception, System.Exception).Message
            'Dialog-Fenster mit Fehlermeldung anzeigen
            ExceptionHandler(StackTrace, Message, True)
        End Sub

        ''' <summary>
        ''' Zeigt ein modales Dialog-Fenster mit der Fehlermeldung an.
        ''' Auswahl Abbrechen, neu starten, fortsetzen.
        ''' Die Fehlermeldung, Stacktrace und Log-File können per Mail an software@winback.de gesendet werden.
        ''' </summary>
        ''' <param name="StackTrace"></param>
        ''' <param name="Message"></param>
        ''' <param name="UnhadledException"></param>
        Private Sub ExceptionHandler(StackTrace As String, Message As String, UnhadledException As Boolean)
            'Dialog-Fenster mit Fehlermeldung anzeigen 
            Dim MainException As New wb_Main_Exception(StackTrace, Message)

            'abhängig vom Dialog-Result 
            Select Case MainException.ShowDialog()
                Case DialogResult.Abort
                    'WinBack-AddIn beenden
                    Trace.WriteLine("&I_DialogResult.Abort - WinBack/OrgaBack beenden")
                    'Exit()
                Case DialogResult.Retry
                    'WinBack-AddIn restart
                    Trace.WriteLine("&I_DialogResult.Retry - WinBack/OrgaBack neu starten")
                    'Restart()
                Case DialogResult.Ignore
                    'WinBack-AddIn fortsetzen
                    Trace.WriteLine("&I_DialogResult.Ignore - WinBack-AddIn fortsetzen")
                Case Else
                    'WinBack-AddIn fortsetzen
                    Trace.WriteLine("&I_DialogResult.xxx - WinBack-AddIn fortsetzen")

            End Select
        End Sub

    End Class
End Namespace
