Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

''' <summary>
''' Testet ob die Routine InitDBTests alle Einstellungen richtig setzt:
''' 
'''     Datenbank WinBack auf localhost
'''     Datenbank Konfiguration.Kunde = UnitTest300
'''     
'''     Datenbank OrgaBack auf localhost (WILL/WIN10)
'''     Datenbank OrgaBack Mandant UnitTest
'''     
''' </summary>
<TestClass()> Public Class UnitTest_InitOK

    ''' <summary>
    ''' Initialisiert die Datenbank-Einstellungen.
    ''' Falls notwendig werden die Datensicherungen aus ... in die DB eingespielt
    ''' </summary>
    ''' <param name="testContext"></param>
    <ClassInitialize()> Public Shared Sub InitDBTests(ByVal testContext As TestContext)
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest

        'Einstellungen aus dbo.Settings lesen
        UnitTest_Init.Init_WinBackIni()
    End Sub

    ''' <summary>
    ''' Prüfen ob eine UnitTest-Datenbank exisitiert und geladen ist.
    ''' Test, ob die Initialisierung und Datenrücksicherung funktionieren.
    ''' </summary>
    <TestMethod()> Public Sub TestDatebaseInfo()

        'WinBack-Datenbank muss Test-Datenbank sein
        Assert.AreEqual("UnitTest OrgaBack", wb_GlobalSettings.MandantName)

        'OrgaBack-Datenbank muss Test-Datenbank sein
        Assert.AreEqual("UnitTest", wb_GlobalSettings.OrgaBackMandantName)
    End Sub

End Class