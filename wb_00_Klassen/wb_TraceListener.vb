Imports System.Runtime.CompilerServices

Public Class wb_TraceListener
    Inherits TraceListener
    Dim bEchoStackTrace As Boolean = True

    ''' <param name="Txt"> String Debug/Trace-Text</param>
    Public Event WriteText(ByVal Txt As String)

    Public WriteOnly Property EchoStackTrace
        Set(value)
            bEchoStackTrace = value
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
        RaiseEvent WriteText(message)
    End Sub

    ''' <summary>
    ''' Sub Write (overrides)
    ''' Verschickt den Debug/Trace-Text mit Datum/Uhrzeit und Zeilen-Vorschub als Event an alle angemeldeten Listener
    ''' </summary>
    ''' <param name="message"> String Debug/Trace-Text</param>
    Public Overrides Sub WriteLine(message As String)
        If bEchoStackTrace Then
            RaiseEvent WriteText(String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now) & vbTab & wb_GlobalSettings.AktUserName & vbTab & message & vbTab & GetLocalStackTrace(Environment.StackTrace) & vbCrLf)
        Else
            RaiseEvent WriteText(String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now) & vbTab & wb_GlobalSettings.AktUserName & vbTab & message & vbCrLf)
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
End Class
