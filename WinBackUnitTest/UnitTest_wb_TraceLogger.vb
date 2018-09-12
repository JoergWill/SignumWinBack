Imports WinBack

''' <summary>
''' Testet den TraceListener für WinBack Die Test-Routine darf nicht den Text TraceListener enthalten, dieser wird in der 
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
        Dim ResultString As String

        TestString = "Zeile 1" + vbCrLf + "Test2" + vbCrLf + "WinBack Test bei in :Zeile 10"
        Assert.AreEqual("---", Logger.TestLocalStackTrace(TestString))
        TestString = "Zeile 1" + vbCrLf + "Test2" + vbCrLf + "XXX bei WinBack Test in UnitTest :Zeile 10" & vbCrLf
        Assert.AreEqual("Z00010" & vbTab & "WinBack Test", Logger.TestLocalStackTrace(TestString))
        TestString = vbCrLf & "Zeile 1" + vbCrLf + "Test2" + vbCrLf + "XXX bei WinBack Test in :Zeile 10" & vbCrLf
        Assert.AreEqual("---", Logger.TestLocalStackTrace(TestString))
        TestString = vbCrLf & "Zeile 1" + vbCrLf + "Test2" + vbCrLf + "XXX bei WinBack Test lin :Zeile 10"
        Assert.AreEqual("---", Logger.TestLocalStackTrace(TestString))

        TestString = vbCrLf & "Zeile 1" + vbCrLf + "Test2" + vbCrLf + "XXX bei WinBack TraceListener Test in :Zeile 10"
        Assert.AreEqual("---", Logger.TestLocalStackTrace(TestString))


        TestString = "   bei WinBack.wb_TraceListener.WriteLine(String message) In C:\Users\will.WINBACK\Source\Repos\Signum_WinBack\wb_00_Klassen\wb_TraceListener.vb:Zeile 36."
        ResultString = vbTab & "Z00026 WinBack.wb_TraceListener.WriteLine(String message)"
        Assert.AreEqual("---", Logger.TestLocalStackTrace(TestString))

    End Sub

    <TestMethod()> Public Sub TestTraceWrite()
        Assert.AreEqual("Empty", LogResult)
        Trace.Write("Test")
        Trace.Flush()
        Assert.AreEqual("Test", LogResult)
    End Sub

    <TestMethod()> Public Sub TestTraceWriteLn()
        Dim OK_Datum As String = String.Format("{0:dd/MM/yy H:mm:ss}", Date.Now)
        Dim OK_Message As String = "Test"
        Dim OK_Class As String = "WinBackUnitTest.UnitTest_wb_TraceLogger.TestTraceWriteLn()"
        Dim OK_Zeile As String = "71"
        Dim OK_Result As String

        'Aktueller User
        wb_GlobalSettings.AktUserLogin("TestUser")

        'Test ohne Stack-Trace
        Logger.EchoStackTrace = vbFalse
        LogResult = ""
        OK_Result = OK_Datum & vbTab & wb_GlobalSettings.AktUserName & vbTab & OK_Message & vbCrLf
        Trace.WriteLine(OK_Message)
        Trace.Flush()
        Assert.AreEqual(OK_Result, LogResult)

        'Test mit Stack-Trace
        Logger.EchoStackTrace = vbTrue
        LogResult = ""
        OK_Result = OK_Datum & vbTab & wb_GlobalSettings.AktUserName & vbTab & OK_Message & vbTab & OK_Class & vbTab & OK_Zeile & vbCrLf
        Trace.WriteLine(OK_Message)
        Trace.Flush()
        Assert.AreEqual(OK_Result, LogResult)
    End Sub

    Public Sub GetTrace(Txt As String)
        LogResult = Txt
    End Sub
End Class