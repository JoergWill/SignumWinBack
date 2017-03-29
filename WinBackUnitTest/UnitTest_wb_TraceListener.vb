Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_TraceListener
    Dim Logger As New wb_TraceListener

    <TestMethod()> Public Sub TestGetLocalStackTrace()
        Dim TestString As String

        TestString = "Zeile 1" + vbCrLf + "Test2" + vbCrLf + "WinBack Test"
        Assert.AreEqual("WinBack Test", Logger.TestLocalStackTrace(TestString))
    End Sub

End Class