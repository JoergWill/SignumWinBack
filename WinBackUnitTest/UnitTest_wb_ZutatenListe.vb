Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_ZutatenListe

    <TestInitialize>
    Sub TestInitialize()
        'Datenbank Verbindung Einstellungen setzen
        '(Muss in wb_Konfig gesetzt werden, weil My.Setting hier nicht funktioniert)
        wb_Konfig.SqlSetting("MySQL")
    End Sub

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

    <TestMethod()> Public Sub Test_Split()
        Dim Zutaten As New wb_ZutatenListe
        Dim L1 As wb_Global.ZutatenListe

        Zutaten.Clear()
        L1.Zutaten = "{Gerstenmalzextrakt getrocknet}, Zucker, Traubenzucker, {Gerstenmalzmehl}, " &
                     "{Roggenmehl}, Emulgator Mono-und Diacetylweinsäureester von Mono- und Diglyceriden von Speisefettsäuren E 472e, " &
                     "Stabilisator: Guarkernmehl(E 412), Säureregulator:(Natriumcarbonate), Calciumsulfat, pflanzliches Öl, Enzyme, " &
                     "Mehlbehandlungsmittel: Ascorbinsäure(E 300), {Weizen, Dinkel}, {Soja,Milch}"
        L1.SollMenge = 2
        Zutaten.Liste.Add(L1)
        Zutaten.Split_Ingredients()

        Assert.AreEqual("Gerstenmalzextrakt getrocknet", DirectCast(Zutaten.Liste(0), wb_Global.ZutatenListe).Zutaten)
        Assert.IsTrue(DirectCast(Zutaten.Liste(0), wb_Global.ZutatenListe).FettDruck)
        Assert.AreEqual("Zucker", DirectCast(Zutaten.Liste(1), wb_Global.ZutatenListe).Zutaten)
        Assert.IsFalse(DirectCast(Zutaten.Liste(1), wb_Global.ZutatenListe).FettDruck)
        Assert.AreEqual("Traubenzucker", DirectCast(Zutaten.Liste(2), wb_Global.ZutatenListe).Zutaten)
        Assert.IsFalse(DirectCast(Zutaten.Liste(2), wb_Global.ZutatenListe).FettDruck)
        Assert.AreEqual("Gerstenmalzmehl", DirectCast(Zutaten.Liste(3), wb_Global.ZutatenListe).Zutaten)
        Assert.IsTrue(DirectCast(Zutaten.Liste(3), wb_Global.ZutatenListe).FettDruck)
    End Sub

    <TestMethod()> Public Sub Test_DelSplitDel()
        Dim Zutaten As New wb_ZutatenListe
        Dim L1 As wb_Global.ZutatenListe

        Zutaten.Clear()
        L1.Zutaten = "{Weizen}"
        L1.SollMenge = 100
        Zutaten.Liste.Add(L1)

        L1.Zutaten = "{Weizen}"
        L1.SollMenge = 100
        Zutaten.Liste.Add(L1)

        L1.Zutaten = "{Gerstenmalzextrakt getrocknet}, Zucker, Traubenzucker, {Gerstenmalzmehl}, " &
                     "{Roggenmehl}, Emulgator Mono-und Diacetylweinsäureester von Mono- und Diglyceriden von Speisefettsäuren E 472e, " &
                     "Stabilisator: Guarkernmehl(E 412), Säureregulator:(Natriumcarbonate), Calciumsulfat, pflanzliches Öl, Enzyme, " &
                     "Mehlbehandlungsmittel: Ascorbinsäure(E 300), {Weizen, Dinkel}, {Soja,Milch}"
        L1.SollMenge = 2
        Zutaten.Liste.Add(L1)

        Zutaten.Del_Doubletten()
        Zutaten.Split_Ingredients()
        Zutaten.Del_Doubletten()
        Zutaten.DebugPrint()
        Assert.AreEqual(16, Zutaten.Liste.Count)

        Assert.AreEqual("Weizen", DirectCast(Zutaten.Liste(0), wb_Global.ZutatenListe).Zutaten)
        Assert.IsTrue(DirectCast(Zutaten.Liste(0), wb_Global.ZutatenListe).FettDruck)

    End Sub


    <TestMethod()> Public Sub Test_EListe()
        Dim x As wb_Global.ENummern

        x = wb_ZutatenListe_Global.Find_ENummer("E175")
        Assert.AreEqual("Gold", x.Bezeichnung)
        x = wb_ZutatenListe_Global.Find_EBezeichnung("Gold")
        Assert.AreEqual("Gold", x.Bezeichnung)
        Assert.AreEqual(175, x.Nr)
        x = wb_ZutatenListe_Global.Find_EBezeichnung("GOLD")
        Assert.AreEqual("Gold", x.Bezeichnung)
        Assert.AreEqual(175, x.Nr)

        x = wb_ZutatenListe_Global.Find_ENummer("1")
        Assert.AreEqual(-1, x.Nr)
        x = wb_ZutatenListe_Global.Find_ENummer("GGOLD")
        Assert.AreEqual(-1, x.Nr)
    End Sub

    <TestMethod()> Public Sub Test_Find_ENummer()
        Dim Zutaten As New wb_ZutatenListe
        Dim x As String

        x = Zutaten.Get_ENummer("E400")
        Assert.AreEqual("E400", x)
        x = Zutaten.Get_ENummer("E 400")
        Assert.AreEqual("E400", x)
        x = Zutaten.Get_ENummer("E 1001")
        Assert.AreEqual("E1001", x)
        x = Zutaten.Get_ENummer("E1001")
        Assert.AreEqual("E1001", x)

        x = Zutaten.Get_ENummer("E400a")
        Assert.AreEqual("E400a", x)
        x = Zutaten.Get_ENummer("E 400a")
        Assert.AreEqual("E400a", x)
        x = Zutaten.Get_ENummer("E1001a")
        Assert.AreEqual("E1001a", x)
        x = Zutaten.Get_ENummer("E 1001a")
        Assert.AreEqual("E1001a", x)

        x = Zutaten.Get_ENummer("Stabilisator: Guarkernmehl(E 412)")
        Assert.AreEqual("E412", x)
        x = Zutaten.Get_ENummer("Emulgator Mono-und Diacetylweinsäureester von Mono- und Diglyceriden von Speisefettsäuren E 472e")
        Assert.AreEqual("E472e", x)
        x = Zutaten.Get_ENummer("Mehlbehandlungsmittel: Ascorbinsäure(E 300)")
        Assert.AreEqual("E300", x)
    End Sub

    <TestMethod()> Public Sub Test_Convert_ENummer()
        Dim Zutaten As New wb_ZutatenListe
        Dim L1 As wb_Global.ZutatenListe



        Zutaten.Clear()
        L1.Zutaten = "Stabilisator: Guarkernmehl(E 412)"
        L1.SollMenge = 100
        Zutaten.Liste.Add(L1)

        L1.Zutaten = "Emulgator Mono-und Diacetylweinsäureester von Mono- und Diglyceriden von Speisefettsäuren E 472e"
        L1.SollMenge = 100
        Zutaten.Liste.Add(L1)

        L1.Zutaten = "Ascorbinsäure"
        L1.SollMenge = 100
        Zutaten.Liste.Add(L1)

        Zutaten.Convert_ToEnr()
        Zutaten.DebugPrint()
    End Sub

    <TestMethod()> Public Sub Test_Opt_ZutatenListe()
        Dim Zutaten As New wb_ZutatenListe
        Dim L1 As wb_Global.ZutatenListe

        Zutaten.Clear()
        L1.Zutaten = "{Weizen}"
        L1.SollMenge = 100
        Zutaten.Liste.Add(L1)

        L1.Zutaten = "{Weizen}"
        L1.SollMenge = 100
        Zutaten.Liste.Add(L1)

        L1.Zutaten = "{Gerstenmalzextrakt getrocknet}, Zucker, Traubenzucker, {Gerstenmalzmehl}, " &
                     "{Roggenmehl}, Emulgator Mono-und Diacetylweinsäureester von Mono- und Diglyceriden von Speisefettsäuren E 472e, " &
                     "Stabilisator: Guarkernmehl(E 412), Säureregulator:(Natriumcarbonate), Calciumsulfat, pflanzliches Öl, Enzyme, " &
                     "Mehlbehandlungsmittel: Ascorbinsäure(E 300), {Weizen, Dinkel}, {Soja,Milch}"
        L1.SollMenge = 2
        Zutaten.Liste.Add(L1)

        Zutaten.Opt()
        Zutaten.DebugPrint()
        Assert.AreEqual(16, Zutaten.Liste.Count)

        Assert.AreEqual("Weizen", DirectCast(Zutaten.Liste(0), wb_Global.ZutatenListe).Zutaten)
        Assert.IsTrue(DirectCast(Zutaten.Liste(0), wb_Global.ZutatenListe).FettDruck)
    End Sub
End Class