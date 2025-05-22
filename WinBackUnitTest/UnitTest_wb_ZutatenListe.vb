Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_ZutatenListe

    ''' <summary>
    ''' Initialisiert die globalen Einstellungen.
    ''' </summary>
    <TestInitialize>
    Sub TestInitialize()
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()> Public Sub Test_DelDoubletten()
        Dim Zutaten As New wb_ZutatenListe
        Zutaten.Clear()

        Dim L1 As New wb_ZutatenElement
        Dim L2 As New wb_ZutatenElement
        Dim L3 As New wb_ZutatenElement

        L1.Zutaten = "{Weizenmehl}"
        L1.SollMenge = 100
        Zutaten.Liste.Add(L1)

        L2.Zutaten = "{Weizenmehl}"
        L2.SollMenge = 50
        Zutaten.Liste.Add(L2)

        L3.Zutaten = "{Weizen}"
        L3.SollMenge = 50
        Zutaten.Liste.Add(L3)

        Debug.Print("==========================")
        Debug.Print("3 Einträge erzeugt")
        Zutaten.DebugPrint()
        Assert.AreEqual(3, Zutaten.Liste.Count)

        Zutaten.Del_Doubletten(True)
        Debug.Print("==========================")
        Debug.Print("nach DelDoubletten")
        Zutaten.DebugPrint()
        Assert.AreEqual(2, Zutaten.Liste.Count)
        Assert.AreEqual(150.0, Zutaten.Liste(0).SollMenge)


    End Sub

    <TestMethod()> Public Sub Test_DelDoublettenMulti()
        Dim Zutaten As New wb_ZutatenListe
        Zutaten.Clear()

        Dim L1 As New wb_ZutatenElement
        Dim L2 As New wb_ZutatenElement
        Dim L3 As New wb_ZutatenElement

        L1.Zutaten = "{Weizenmehl}"
        L1.SollMenge = 100
        Zutaten.Liste.Add(L1)

        L2.Zutaten = "{Weizenmehl}"
        L2.SollMenge = 50
        Zutaten.Liste.Add(L2)

        L3.Zutaten = "{Weizenmehl}"
        L3.SollMenge = 100
        Zutaten.Liste.Add(L3)

        Debug.Print("==========================")
        Debug.Print("3 Einträge erzeugt")
        Zutaten.DebugPrint()
        Assert.AreEqual(3, Zutaten.Liste.Count)

        Zutaten.Del_Doubletten(True)
        Debug.Print("==========================")
        Debug.Print("nach DelDoubletten")
        Zutaten.DebugPrint()
        Assert.AreEqual(1, Zutaten.Liste.Count)
        Assert.AreEqual(250.0, Zutaten.Liste(0).SollMenge)
    End Sub

    <TestMethod()> Public Sub Test_Split_A()
        Dim Zutaten As New wb_ZutatenListe
        Zutaten.Clear()

        Dim L1 As New wb_ZutatenElement
        L1.Zutaten = "{Gerstenmalzextrakt getrocknet}, Zucker, Traubenzucker, {Gerstenmalzmehl}, " &
                     "{Roggenmehl}, Emulgator Mono-und Diacetylweinsäureester von Mono- und Diglyceriden von Speisefettsäuren E 472e, " &
                     "Stabilisator: Guarkernmehl(E 412), Säureregulator:(Natriumcarbonate), Calciumsulfat, pflanzliches Öl, Enzyme, " &
                     "Mehlbehandlungsmittel: Ascorbinsäure(E 300), {Weizen, Dinkel}, {Soja,Milch}"
        L1.SollMenge = 2
        Zutaten.Liste.Add(L1)
        Zutaten.Split_Ingredients()

        Assert.AreEqual("Gerstenmalzextrakt getrocknet", Zutaten.Liste(0).Zutaten)
        Assert.IsTrue(Zutaten.Liste(0).FettDruck)
        Assert.AreEqual("Zucker", Zutaten.Liste(1).Zutaten)
        Assert.IsFalse(Zutaten.Liste(1).FettDruck)
        Assert.AreEqual("Traubenzucker", Zutaten.Liste(2).Zutaten)
        Assert.IsFalse(Zutaten.Liste(2).FettDruck)
        Assert.AreEqual("Gerstenmalzmehl", Zutaten.Liste(3).Zutaten)
        Assert.IsTrue(Zutaten.Liste(3).FettDruck)
    End Sub

    ''' <summary>
    ''' Test auf zusammengesetzte Backmittel mit einer Klammerung. Diese Zutaten sollen NICHT in einzelne Elemente aufgesplittet werden,
    ''' sondern als EIN zusammenhängenden Element in der Liste bleiben.
    ''' Fettdruck der einzelnen Elemente wird direkt durch Großschreibung realisiert.
    ''' 
    ''' Mail von Goeken, Hr.Mandris vom 11.08.2022
    ''' </summary>
    <TestMethod()> Public Sub Test_Split_B()
        Dim Zutaten As New wb_ZutatenListe
        Zutaten.Clear()

        Dim L1 As New wb_ZutatenElement
        L1.Zutaten = "Backmittel: ({Weizenkleber},Zucker,{Weizensauerteig getrocknet},Malzmehl,({Gerste,Weizen}), " &
                     "{Sojalecithine},Verdickungsmittel Guarkernmehl,{Sojamehl},pflanzliches Öl:Raps)"
        L1.SollMenge = 2
        Zutaten.Liste.Add(L1)
        Zutaten.Split_Ingredients()

        Assert.AreEqual("Backmittel:(WEIZENKLEBER\Zucker\WEIZENSAUERTEIG GETROCKNET\Malzmehl\(GERSTE\WEIZEN)\ SOJALECITHINE\Verdickungsmittel Guarkernmehl\SOJAMEHL\pflanzliches Öl:Raps)", Zutaten.Liste(0).Zutaten)
        Assert.IsFalse(Zutaten.Liste(0).FettDruck)
    End Sub

    <TestMethod()> Public Sub Test_Split_ListeInListe()
        Dim Zutaten As New wb_ZutatenListe
        Zutaten.Clear()

        Dim L1 As New wb_ZutatenElement
        L1.Zutaten = "    Pflanzliches Fett :  Palm; Wasser; Emulgator: (Lecithine (E 322), Mono- und Diglyceride von Speisefetts?uren (E 471));" &
                     "Speisesalz; S?uerungsmittel: Citronens?ure (E 330); Aroma; Farbstoff: Carotin (E 160a)"
        L1.SollMenge = 2
        Zutaten.Liste.Add(L1)
        Zutaten.DebugPrint()
        Zutaten.Split_Ingredients()
        Zutaten.DebugPrint()

        Assert.AreEqual("Pflanzliches Fett:  Palm", Zutaten.Liste(0).Zutaten)
        Assert.IsFalse(Zutaten.Liste(0).FettDruck)
        Assert.AreEqual("Wasser", Zutaten.Liste(1).Zutaten)
        Assert.IsFalse(Zutaten.Liste(1).FettDruck)
        Assert.AreEqual("Speisesalz", Zutaten.Liste(3).Zutaten)
        Assert.IsFalse(Zutaten.Liste(2).FettDruck)
        Assert.AreEqual("Aroma", Zutaten.Liste(5).Zutaten)
        Assert.IsFalse(Zutaten.Liste(3).FettDruck)
    End Sub


    <TestMethod()> Public Sub Test_Split_Numbers()
        Dim Zutaten As New wb_ZutatenListe
        Dim L1 As New wb_ZutatenElement

        Zutaten.Clear()
        L1.Zutaten = "{Rohmilch} 3,5%"
        L1.SollMenge = 2
        Zutaten.Liste.Add(L1)
        Zutaten.Split_Ingredients()
        Assert.AreEqual("Rohmilch 3.5%", Zutaten.Liste(0).Zutaten)

        Zutaten.Clear()
        L1.Zutaten = "{Rohmilch} 3,5 %, {Reis}, Zucker, Zucker, Zimt, Zimt, Zimt"
        L1.SollMenge = 2
        Zutaten.Liste.Add(L1)
        Zutaten.Split_Ingredients()
        Assert.AreEqual("Rohmilch 3.5%", Zutaten.Liste(0).Zutaten)
        Assert.AreEqual("Reis", Zutaten.Liste(1).Zutaten)
        Assert.AreEqual(7, Zutaten.Liste.Count)

        Zutaten.Del_Doubletten(True)
        Assert.AreEqual(4, Zutaten.Liste.Count)
        Assert.AreEqual("Rohmilch 3.5%", Zutaten.Liste(0).Zutaten)
        Assert.AreEqual("Reis", Zutaten.Liste(1).Zutaten)
        Assert.AreEqual("Zucker", Zutaten.Liste(2).Zutaten)
        Assert.AreEqual("Zimt", Zutaten.Liste(3).Zutaten)

        Zutaten.Sort()
        Zutaten.DebugPrint()

        Assert.AreEqual(4, Zutaten.Liste.Count)
        Assert.AreEqual("Zimt", Zutaten.Liste(0).Zutaten)
        Assert.AreEqual("Zucker", Zutaten.Liste(1).Zutaten)
        Assert.AreEqual("Rohmilch 3.5%", Zutaten.Liste(2).Zutaten)
        Assert.AreEqual("Reis", Zutaten.Liste(3).Zutaten)

    End Sub


    <TestMethod()> Public Sub Test_DelSplitDel()
        Dim Zutaten As New wb_ZutatenListe
        Zutaten.Clear()

        Dim L1 As New wb_ZutatenElement
        Dim L2 As New wb_ZutatenElement
        Dim L3 As New wb_ZutatenElement

        L1.Zutaten = "{Weizen}"
        L1.SollMenge = 100
        Zutaten.Liste.Add(L1)

        L2.Zutaten = "{Weizen}"
        L2.SollMenge = 100
        Zutaten.Liste.Add(L2)

        L3.Zutaten = "{Gerstenmalzextrakt getrocknet}, Zucker, Traubenzucker, {Gerstenmalzmehl}, " &
                     "{Roggenmehl}, Emulgator Mono-und Diacetylweinsäureester von Mono- und Diglyceriden von Speisefettsäuren E 472e, " &
                     "Stabilisator: Guarkernmehl(E 412), Säureregulator:(Natriumcarbonate), Calciumsulfat, pflanzliches Öl, Enzyme, " &
                     "Mehlbehandlungsmittel: Ascorbinsäure(E 300), {Weizen, Dinkel}, {Soja,Milch}"
        L3.SollMenge = 2
        Zutaten.Liste.Add(L3)

        Zutaten.Del_Doubletten(True)
        Zutaten.Split_Ingredients()
        Zutaten.Del_Doubletten(True)
        Zutaten.DebugPrint()
        Assert.AreEqual(16, Zutaten.Liste.Count)

        Assert.AreEqual("Weizen", Zutaten.Liste(0).Zutaten)
        Assert.IsTrue(Zutaten.Liste(0).FettDruck)

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
        Zutaten.Clear()

        Dim L1 As New wb_ZutatenElement
        Dim L2 As New wb_ZutatenElement
        Dim L3 As New wb_ZutatenElement

        Zutaten.Clear()
        L1.Zutaten = "Stabilisator: Guarkernmehl(E 412)"
        L1.SollMenge = 100
        Zutaten.Liste.Add(L1)

        L2.Zutaten = "Emulgator Mono-und Diacetylweinsäureester von Mono- und Diglyceriden von Speisefettsäuren E 472e"
        L2.SollMenge = 100
        Zutaten.Liste.Add(L2)

        L3.Zutaten = "Ascorbinsäure"
        L3.SollMenge = 100
        Zutaten.Liste.Add(L3)

        Zutaten.Convert_ToEnr()
        Zutaten.DebugPrint()
    End Sub

    <TestMethod()> Public Sub Test_Opt_ZutatenListe()
        Dim Zutaten As New wb_ZutatenListe
        Zutaten.Clear()

        Dim L1 As New wb_ZutatenElement
        Dim L2 As New wb_ZutatenElement
        Dim L3 As New wb_ZutatenElement

        L1.Zutaten = "{Weizen}"
        L1.SollMenge = 100
        Zutaten.Liste.Add(L1)

        L2.Zutaten = "{Weizen}"
        L2.SollMenge = 100
        Zutaten.Liste.Add(L2)

        L3.Zutaten = "{Gerstenmalzextrakt getrocknet}, Zucker, Traubenzucker, {Gerstenmalzmehl}, " &
                     "{Roggenmehl}, Emulgator Mono-und Diacetylweinsäureester von Mono- und Diglyceriden von Speisefettsäuren E 472e, " &
                     "Stabilisator: Guarkernmehl(E 412), Säureregulator:(Natriumcarbonate), Calciumsulfat, pflanzliches Öl, Enzyme, " &
                     "Mehlbehandlungsmittel: Ascorbinsäure(E 300), {Weizen, Dinkel}, {Soja,Milch}"
        L3.SollMenge = 2
        Zutaten.Liste.Add(L3)

        Zutaten.Opt(True)
        Zutaten.DebugPrint()
        Assert.AreEqual(16, Zutaten.Liste.Count)

        Assert.AreEqual("Weizen", Zutaten.Liste(0).Zutaten)
        Assert.IsTrue(Zutaten.Liste(0).FettDruck)
    End Sub




End Class