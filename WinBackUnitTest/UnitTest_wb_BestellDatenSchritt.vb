Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_BestellDatenSchritt

    ''' <summary>
    ''' Initialisiert die Datenbank-Einstellungen.
    ''' Falls notwendig werden die Datensicherungen aus ... in die DB eingespielt
    ''' </summary>
    ''' <param name="testContext"></param>
    <ClassInitialize()> Public Shared Sub InitDBTests(ByVal testContext As TestContext)
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()> Public Sub Test_SollwertTeilungText()
        'Bestelldaten aus OrgaBack (Stored Procedure)
        Dim BestellDatenSchritt As New wb_BestellDatenSchritt

        'Testdaten setzen - LosArt "Stück"
        BestellDatenSchritt.MsSQLdbRead_Fields(10, "0", 5)
        Assert.AreEqual("", BestellDatenSchritt.SollwertTeilungText)

        'Testdaten setzen - LosArt "Stück"
        BestellDatenSchritt.MsSQLdbRead_Fields(10, "Stück", 5)
        Assert.AreEqual("", BestellDatenSchritt.SollwertTeilungText)

        'Testdaten setzen - LosArt "Blech"
        BestellDatenSchritt.MsSQLdbRead_Fields(10, "Blech", 5)
        Assert.AreEqual("10 Blech à 5 Stk", BestellDatenSchritt.SollwertTeilungText)

    End Sub

End Class