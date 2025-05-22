Public Class wb_TraceListener
    Inherits TraceListener
    Private _EchoStackTrace As Boolean = True
    Public Event WriteText(trDate As String, trBenutzer As String, trTxt As String, trException As Exception, trZeile As String, trModul As String, trSubRoutine As String)

    ''' <summary>
    ''' Schaltet die Ausgabe des Stack-Trace ein/aus.
    ''' Default ist Ein
    ''' </summary>
    Public WriteOnly Property EchoStackTrace
        Set(value)
            _EchoStackTrace = value
        End Set
    End Property

    ''' <summary>
    ''' Aufruf für Unit-Test
    ''' </summary>
    ''' <param name="Stack"></param>
    ''' <returns></returns>
    Public ReadOnly Property TestLocalStackTrace(Stack As String) As String
        Get
            Return GetLocalStackTrace(Stack)
        End Get
    End Property

    ''' <summary>
    ''' Sub Write (overrides)
    ''' Verschickt den Debug/Trace-Text über eWriteText() and alle angemeldeten Listener
    ''' </summary>
    ''' <param name="Message"> String Debug/Trace-Text</param>
    Public Overrides Sub Write(Message As String)
        If _EchoStackTrace Then
            eWriteText(Message, Environment.StackTrace, Nothing)
        Else
            eWriteText(Message)
        End If
    End Sub

    ''' <summary>
    ''' Sub Write (overrides)
    ''' Verschickt den Debug/Trace-Text über eWriteText() an alle angemeldeten Listener
    ''' 
    ''' In StackTrace steht die aufrufende Routine im Format ZeilenNummer  WinBack.Name.Funktion(Argumente)
    ''' Diese Informationen werden aufgeschlüsselt in:
    '''     Zeilenummer im Source-Code
    '''     Programm-Name
    '''     Sub-Routine (mit Argumenten)
    ''' </summary>
    ''' <param name="Message"> String Debug/Trace-Text</param>
    Public Overrides Sub WriteLine(Message As String)
        If _EchoStackTrace Then
            eWriteText(Message, Environment.StackTrace, Nothing)
        Else
            eWriteText(Message)
        End If
    End Sub

    ''' <summary>
    ''' Sub WriteLine (overrides)
    ''' Verschickt das Debug/Trace-Objekt(Exception-Object!) über eWriteText() and alle angemeldeten Listener
    ''' 
    ''' Im StackTrace steht die Exception-Information
    ''' </summary>
    ''' <param name="o"> Exception Object</param>
    Public Overrides Sub WriteLine(o As Object)
        'Exception-Stack-Trace auswerten (Zeilen-Nummer, Modul und Sub-Routine)
        Dim e As Exception = TryCast(o, Exception)
        'Fehler-Typ Fatal-Error
        eWriteText("@F_" & e.Message, e.StackTrace, e)
    End Sub

    ''' <summary>
    ''' Verschickt Debug/Trace-Meldungen und optional die Exception-Information als Stack-Trace mit Datum/Uhrzeit und Zeilen-Vorschub als Event an alle angemeldeten Listener
    ''' </summary>
    ''' <param name="m"> Meldung(String)</param>
    ''' <param name="t"> Stack-Trace(String)</param>
    ''' <param name="e"> Exception-Object</param>
    Private Sub eWriteText(m As String, Optional t As String = "", Optional e As Exception = Nothing)
        'Ermitteln ob der Aufruf über Debug.Print() oder über Trace.Write() kommt
        If GetDebug(t) Then
            m = "@D_" & m
        End If

        'Programm-Aufruf aus dem Stack extrahieren
        Dim StackTrace() As String = GetLocalStackTrace(t).Split(vbTab)
        If StackTrace.Count >= 2 Then
            'Stack-Trace(1) - WinBack.Name.Funktion
            Dim SubRoutine() As String = StackTrace(1).Split(".")
            If SubRoutine.Count >= 3 Then
                RaiseEvent WriteText(String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now), wb_GlobalSettings.AktUserName, m, e, StackTrace(0), SubRoutine(1), SubRoutine(2))
            Else
                RaiseEvent WriteText(String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now), wb_GlobalSettings.AktUserName, m, e, StackTrace(0), StackTrace(1), "")
            End If
        Else
            RaiseEvent WriteText(String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now), wb_GlobalSettings.AktUserName, m, e, "", "", "")
        End If
    End Sub

    ''' <summary>
    ''' Extrahiert aus Environment.StackTrace die Programm-Zeile der aufrufenden Routine:
    ''' </summary>
    ''' <param name="Stack"></param>
    ''' <returns></returns>
    Private Function GetLocalStackTrace(Stack As String) As String
        Try
            If Stack IsNot Nothing Then
                'Aufruf-Baum aus Stack-Trace
                Dim x As ArrayList = wb_Functions.GetLocalStackTrace(Stack, True)
                'Das erste Element aus Array()
                If x.Count > 0 Then
                    Return x(0)
                End If
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Ermittelt aus dem Stack ob der Aufruf über Debug.Print() oder Trace.Write() erfolgt
    ''' </summary>
    ''' <returns></returns>
    Private Function GetDebug(Stack As String) As Boolean
        If Stack.Contains("Debug.Print") Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
