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
    ''' (Beispiel)
    ''' 
    '''     bei System.Environment.GetStackTrace(Exception e, Boolean needFileInfo)
    '''     bei System.Environment.get_StackTrace()
    '''     bei WinBack.wb_TraceListener.WriteLine(String message) In C:\Users\will.WINBACK\Source\Repos\Signum_WinBack\wb_00_Klassen\wb_TraceListener.vb:Zeile 36.
    '''     bei System.Diagnostics.TraceInternal.WriteLine(String message)
    '''     bei System.Diagnostics.Trace.WriteLine(String message)
    '''     bei WinBackUnitTest.UnitTest_wb_TraceLogger.TestTraceWriteLn() In C:\Users\will.WINBACK\Source\Repos\Signum_WinBack\WinBackUnitTest\UnitTest_wb_TraceLogger.vb:Zeile 39.
    '''         =======
    '''     bei System.RuntimeMethodHandle.InvokeMethod(Object target, Object[] arguments, Signature sig, Boolean constructor)
    '''     bei System.Reflection.RuntimeMethodInfo.UnsafeInvokeInternal(Object obj, Object[] parameters, Object[] arguments)
    '''     bei System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
    '''     bei Microsoft.VisualStudio.TestPlatform.MSTestFramework.TestMethodRunner.DefaultTestMethodInvoke(Object[] args)
    '''     bei Microsoft.VisualStudio.TestPlatform.MSTestFramework.TestMethodRunner.RunTestMethod()
    '''     bei Microsoft.VisualStudio.TestPlatform.MSTestFramework.TestMethodRunner.ExecuteTest()
    '''     bei Microsoft.VisualStudio.TestPlatform.MSTestFramework.TestMethodRunner.ExecuteInternal()
    '''     bei Microsoft.VisualStudio.TestPlatform.MSTestFramework.TestMethodRunner.Execute()
    '''     bei Microsoft.VisualStudio.TestPlatform.MSTestFramework.UnitTestRunner.RunInternal(TestMethod testMethod, Boolean isDataDriven, Dictionary`2 runParameters)
    '''     bei Microsoft.VisualStudio.TestPlatform.MSTestFramework.UnitTestRunner.RunSingleTest(String name, String fullClassName, Boolean isAsync, Dictionary`2 runParameters)
    ''' </summary>
    ''' <param name="Stack"></param>
    ''' <returns></returns>
    Private Function GetLocalStackTrace(Stack As String) As String
        Dim localStack As String = ""
        Dim subStack As String = ""
        Dim i As Integer = 0
        Dim j As Integer = 0

        Do
            i = Stack.IndexOf(vbCrLf, j)
            If (i < 0) Then
                i = Len(Stack)
            End If
            subStack = Stack.Substring(j, i - j)
            If subStack.Contains("WinBack") And Not subStack.Contains("TraceListener") Then

                'beim ersten Auftreten des passenden Musters wird der String zerlegt und die Schleife verlassen
                'TODO was passiert bei einem englischen VisualStudio?
                Dim x1 As Integer = subStack.IndexOf(" bei ")
                Dim x2 As Integer = subStack.IndexOf(" in ")
                Dim x3 As Integer = subStack.IndexOf(":Zeile")

                If (x1 > 0) And (x2 > 0) And (x3 > 0) Then

                    Dim s1 As String = subStack.Substring(x1 + 5, x2 - x1 - 5)
                    Dim s2 As String = subStack.Substring(x2 + 4, x3 - x2 - 4)
                    Dim s3 As Integer = Val(subStack.Substring(x3 + 7))

                    localStack = s1 & vbTab & s3.ToString
                    Return localStack
                    Exit Do
                Else
                    If localStack <> "" Then
                        localStack &= vbCrLf
                    End If
                    localStack = localStack + subStack
                End If
            End If
            j = i + 2
        Loop Until (i < 0) Or (i = Len(Stack))

        Return localStack
    End Function
End Class
