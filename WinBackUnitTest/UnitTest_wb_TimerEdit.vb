Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBackServerTask

<TestClass()> Public Class UnitTest_wb_TimerEdit

    <TestMethod()> Public Sub Test_TimerEditSet()

        Dim t As New wb_TimerEdit
        t.TimerZyklus = 120
        Assert.AreEqual(120, t.TimerZyklus)
    End Sub

End Class