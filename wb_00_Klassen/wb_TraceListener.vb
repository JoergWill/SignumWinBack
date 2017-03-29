Public Class wb_TraceListener
    Inherits TraceListener
    Dim bEchoStackTrace As Boolean = False

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
            RaiseEvent WriteText(String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now) + " " + GetLocalStackTrace(Environment.StackTrace) + " " + message + vbCrLf)
        Else
            RaiseEvent WriteText(String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now) + " " + message + vbCrLf)
        End If
    End Sub

    Private Function GetLocalStackTrace(Stack As String) As String
        Dim localStack As String = ""
        Dim i As Integer = 0
        Dim j As Integer = 0

        Do
            i = Stack.IndexOf(vbCrLf, j)
            If (i < 0) Then
                i = Len(Stack)
            End If
            If Stack.Substring(j, i - j).Contains("WinBack") And Not Stack.Substring(j, i - j).Contains("TraceListener") Then
                localStack = localStack + Stack.Substring(j, i - j) + vbCrLf
            End If
            j = i + 1
        Loop Until (i < 0) Or (i = Len(Stack))

        Return localStack
    End Function
End Class
