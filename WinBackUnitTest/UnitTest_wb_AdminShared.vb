Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_AdminShared

    <TestMethod()> Public Sub TestCreateEventLog()
        'Beim ersten Aufruf irgendeiner Routine wird Shared New() aufgerufen
        Assert.AreEqual(True, wb_Admin_Shared.WriteToEventLog("TEST"))
    End Sub

End Class