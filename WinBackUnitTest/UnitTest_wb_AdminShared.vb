Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_AdminShared


    ''' <summary>
    ''' Initialisiert die Datenbank-Einstellungen.
    ''' Falls notwendig werden die Datensicherungen aus ... in die DB eingespielt
    ''' </summary>
    ''' <param name="testContext"></param>
    <ClassInitialize()> Public Shared Sub InitDBTests(ByVal testContext As TestContext)
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni(wb_Global.UNDEFINED)
    End Sub

    <TestMethod()> Public Sub TestCreateEventLog()
        'Beim ersten Aufruf irgendeiner Routine wird Shared New() aufgerufen
        Assert.AreEqual(False, wb_Admin_Shared.LoggerAktiv)
    End Sub

    <TestMethod()> Public Sub TestTraceListenerText()
        'Normaler Aufruf 
        wb_Admin_Shared.GetTraceListenerText("01.01.2020", "TestUser", "FehlerText")
        'Aufruf mit leerem Fehlertext (Fehler bei Weber 15.07.2020)
        wb_Admin_Shared.GetTraceListenerText("01.01.2020", "TestUser", "")

        Assert.AreEqual(False, wb_Admin_Shared.LoggerAktiv)
    End Sub


End Class