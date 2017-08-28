Imports WinBack

''' <summary>
''' Testet den TraceListener für WinBack. Die Test-Routine darf nicht den Text TraceListener enthalten, dieser wird in der 
''' Routine GetLocalStack-Trace ausgefiltert
''' </summary>
<TestClass()> Public Class UnitTest_wb_TraceLogger
    Dim Logger As New wb_TraceListener
    Dim LogResult As String = "Empty"

    <TestInitialize>
    Sub TestInitialize()
        AddHandler Logger.WriteText, AddressOf GetTrace
        Trace.Listeners.Add(Logger)
    End Sub

    <TestMethod()> Public Sub TestGetLocalStackTrace()
        Dim TestString As String

        TestString = "Zeile 1" + vbCrLf + "Test2" + vbCrLf + "WinBack Test"
        Assert.AreEqual("WinBack Test", Logger.TestLocalStackTrace(TestString))
        TestString = "Zeile 1" + vbCrLf + "Test2" + vbCrLf + "WinBack Test" & vbCrLf
        Assert.AreEqual("WinBack Test", Logger.TestLocalStackTrace(TestString))
        TestString = vbCrLf & "Zeile 1" + vbCrLf + "Test2" + vbCrLf + "WinBack Test" & vbCrLf
        Assert.AreEqual("WinBack Test", Logger.TestLocalStackTrace(TestString))
        TestString = vbCrLf & "Zeile 1" + vbCrLf + "Test2" + vbCrLf + "WinBack Test"
        Assert.AreEqual("WinBack Test", Logger.TestLocalStackTrace(TestString))

        TestString = vbCrLf & "Zeile 1" + vbCrLf + "Test2" + vbCrLf + "WinBack TraceListener"
        Assert.AreEqual("", Logger.TestLocalStackTrace(TestString))
    End Sub

    <TestMethod()> Public Sub TestTraceWrite()
        Assert.AreEqual("Empty", LogResult)
        Trace.Write("WinBack Test")
        Trace.Flush()
        Assert.AreEqual("WinBack Test", LogResult)
    End Sub

    <TestMethod()> Public Sub TestTraceWriteLn()
        Dim OK_Datum As String = String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now)
        Dim OK_Class As String = "WinBackUnitTest.UnitTest_wb_TraceLogger.TestTraceWriteLn()"
        Dim OK_Zeile As String = "45"

        Dim OK_Result As String = OK_Datum & vbTab & OK_Class & vbTab & OK_Zeile & vbTab & "WinBack Test"

        'Dim OK_Result As String = String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now) &
        '                           "    bei WinBackUnitTest.UnitTest_wb_TraceLogger.TestTraceWriteLn() in " &
        '                           "C:\Users\will.WINBACK\Source\Repos\Signum_WinBack\WinBackUnitTest\UnitTest_wb_TraceLogger.vb:Zeile 45." &
        '                           " WinBack Test" + vbCrLf
        Trace.WriteLine("WinBack Test")
        Trace.Flush()
        Assert.AreEqual(OK_Result, LogResult)
    End Sub

    Public Sub GetTrace(Txt As String)
        LogResult = Txt
    End Sub
End Class