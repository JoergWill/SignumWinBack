Imports System.Runtime.CompilerServices

Public Class wb_TraceListener
    Inherits TraceListener
    Private _EchoStackTrace As Boolean = True
    Private _StackTrace As String = ""

    ''' <param name="Txt"> String Debug/Trace-Text</param>
    Public Event WriteText(ByVal trDate As String, ByVal trUser As String, ByVal trLine As String, ByVal trSubName As String, ByVal trSubRoutine As String, ByVal Txt As String)

    Public WriteOnly Property EchoStackTrace
        Set(value)
            _EchoStackTrace = value
        End Set
    End Property

    Public ReadOnly Property TestLocalStackTrace(Stack As String) As String
        Get
            Return GetLocalStackTrace(Stack)
        End Get
    End Property

    ''' <summary>
    ''' Sub Write (overrides)
    ''' Verschickt den Debug/Trace-Text an alle angemeldeten Listener
    ''' </summary>
    ''' <param name="message"> String Debug/Trace-Text</param>
    Public Overrides Sub Write(message As String)
        RaiseEvent WriteText(String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now), wb_GlobalSettings.AktUserName, "", "", "", message)
    End Sub

    ''' <summary>
    ''' Sub Write (overrides)
    ''' Verschickt den Debug/Trace-Text mit Datum/Uhrzeit und Zeilen-Vorschub als Event an alle angemeldeten Listener
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
            'Stack-Trace (Systemaufruf)
            _StackTrace = Environment.StackTrace

            'Ermitteln ob der Aufruf über Debug.Print() oder über Trace.Write() kommt
            If GetDebug(_StackTrace) Then
                Message &= "@D_" & Message
            End If

            'Programm-Aufruf aus dem Stack extrahieren
            Dim StackTrace() As String = GetLocalStackTrace(_StackTrace).Split(vbTab)
            If StackTrace.Count >= 2 Then
                'Stack-Trace(1) - WinBack.Name.Funktion
                Dim SubRoutine() As String = StackTrace(1).Split(".")
                If SubRoutine.Count >= 3 Then
                    RaiseEvent WriteText(String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now), wb_GlobalSettings.AktUserName, StackTrace(0), SubRoutine(1), SubRoutine(2), Message)
                Else
                    RaiseEvent WriteText(String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now), wb_GlobalSettings.AktUserName, StackTrace(0), StackTrace(1), "", Message)
                End If
            Else
                RaiseEvent WriteText(String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now), wb_GlobalSettings.AktUserName, "", "", "", Message)
            End If
        Else
            RaiseEvent WriteText(String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now), wb_GlobalSettings.AktUserName, "", "", "", Message)
        End If
    End Sub

    ''' <summary>
    ''' Extrahiert aus Environment.StackTrace die Programm-Zeile der aufrufenden Routine:
    ''' </summary>
    ''' <param name="Stack"></param>
    ''' <returns></returns>
    Private Function GetLocalStackTrace(Stack As String) As String
        'Aufruf-Baum aus Stack-Trace
        Dim x = wb_Functions.GetLocalStackTrace(Stack, True)
        'Das erste Element aus Array()
        Return x(0)
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
