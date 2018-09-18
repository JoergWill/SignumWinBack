Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_BestellDatenSchritt

    <TestMethod()> Public Sub Test_SollwertTeilungText()
        'Programm-Variante einstellen
        UnitTest_Init.Init_WinBackIni()
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