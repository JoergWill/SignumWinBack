Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_AdminShared

    <TestMethod()> Public Sub TestCreateEventLog()
        'Beim ersten Aufruf irgendeiner Routine wird Shared New() aufgerufen
        Assert.AreEqual(False, wb_Admin_Shared.LoggerAktiv)
    End Sub

End Class