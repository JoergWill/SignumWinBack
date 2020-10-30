Namespace My
    ' Für MyApplication sind folgende Ereignisse verfügbar:
    ' Startup: Wird beim Starten der Anwendung noch vor dem Erstellen des Startformulars ausgelöst.
    ' Shutdown: Wird nach dem Schließen aller Anwendungsformulare ausgelöst. Dieses Ereignis wird nicht ausgelöst, wenn die Anwendung mit einem Fehler beendet wird.
    ' UnhandledException: Wird bei einem Ausnahmefehler ausgelöst.
    ' StartupNextInstance: Wird beim Starten einer Einzelinstanzanwendung ausgelöst, wenn die Anwendung bereits aktiv ist. 
    ' NetworkAvailabilityChanged: Wird beim Herstellen oder Trennen der Netzwerkverbindung ausgelöst.
    Partial Friend Class MyApplication
        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            MsgBox(e.Exception.ToString)
            e.ExitApplication = False
        End Sub

        ''' <summary>
        ''' Lade die .ddl-Files aus einem definierten Unterorder /dll
        '''     ACHTUNG
        '''     Die dll-Files müssen dann auch in diesem Ordner vorhanden sein!
        ''' </summary>
        Private WithEvents MyDomain As AppDomain = AppDomain.CurrentDomain
        Private Function MyDomain_AssemblyResolve(ByVal sender As Object, ByVal args As System.ResolveEventArgs) As System.Reflection.Assembly Handles MyDomain.AssemblyResolve
            Dim dll_file As String = System.AppDomain.CurrentDomain.BaseDirectory + "dll\" + args.Name.Split(CChar(","))(0) + ".dll"
            If System.IO.File.Exists(dll_file) Then
                Return System.Reflection.Assembly.LoadFile(dll_file)
            Else
                Return Nothing
            End If
        End Function
    End Class
End Namespace
