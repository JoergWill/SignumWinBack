Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_AdminDatenbaseInfo


    ''' <summary>
    ''' Initialisiert die Datenbank-Einstellungen.
    ''' Falls notwendig werden die Datensicherungen aus ... in die DB eingespielt
    ''' </summary>
    ''' <param name="testContext"></param>
    <ClassInitialize()> Public Shared Sub InitDBTests(ByVal testContext As TestContext)
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni()

    End Sub



    <TestMethod()> Public Sub TestDatebaseInfo()

        'WinBack-Datenbank muss Test-Datenbank sein
        Assert.AreEqual("UnitTest300", wb_GlobalSettings.MandantName)

        'OrgaBack-Datenbank muss Test-Datenbank sein
        Assert.AreEqual("UnitTest", wb_GlobalSettings.OrgaBackMandantName)
    End Sub

End Class