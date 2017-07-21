Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_ZutatenListe

    <TestMethod()> Public Sub Test_DelDoubletten()
        Dim Zutaten As New wb_ZutatenListe
        Dim L1 As wb_Global.ZutatenListe

        Zutaten.Clear()

        L1.Zutaten = "{Weizenmehl}"
        L1.SollMenge = 100
        Zutaten.Liste.Add(L1)

        L1.Zutaten = "{Weizenmehl}"
        L1.SollMenge = 50
        Zutaten.Liste.Add(L1)

        Debug.Print("==========================")
        Debug.Print("2 Einträge erzeugt")
        Zutaten.DebugPrint()
        Assert.AreEqual(2, Zutaten.Liste.Count)

        Zutaten.Del_Doubletten()
        Debug.Print("==========================")
        Debug.Print("nach DelDoubletten")
        Zutaten.DebugPrint()
        Assert.AreEqual(1, Zutaten.Liste.Count)
        Assert.AreEqual(150.0, DirectCast(Zutaten.Liste(0), wb_Global.ZutatenListe).SollMenge)

    End Sub

End Class