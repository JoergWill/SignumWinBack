Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_Produktion

    'Aufteilung in gleiche Chargen
    <TestMethod()> Public Sub Test_wb_Produktion_ChargenBerechnung_M01()
        Dim Produktion As New wb_Produktion
        Dim TestResult As wb_Global.ChargenMengen

        'Soll 100kg aufgeteilt in 1 Optimal-Charge zu 100kg
        TestResult = Produktion.CalcChargenMenge(100, 10, 100, 100, wb_Global.ModusChargenTeiler.XGleiche, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(100.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.OK, TestResult.Result)

        'Soll 110kg aufgeteilt in 2 Chargen zu 55kg
        TestResult = Produktion.CalcChargenMenge(110, 10, 100, 100, wb_Global.ModusChargenTeiler.XGleiche, False)
        Assert.AreEqual(2, TestResult.AnzahlOpt)
        Assert.AreEqual(55.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.OK, TestResult.Result)

        'Soll 10kg aufgeteilt in 1 Chargen zu 10kg
        TestResult = Produktion.CalcChargenMenge(10, 10, 100, 100, wb_Global.ModusChargenTeiler.XGleiche, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(10.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.OK, TestResult.Result)

        'Soll 14kg aufgeteilt in 1 Chargen zu 10kg - Fehler EP1
        TestResult = Produktion.CalcChargenMenge(14, 10, 10, 10, wb_Global.ModusChargenTeiler.XGleiche, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(10.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.EP1, TestResult.Result)

        'Soll 9kg aufgeteilt in 1 Chargen zu 10kg - Fehler EP2
        TestResult = Produktion.CalcChargenMenge(9, 10, 10, 10, wb_Global.ModusChargenTeiler.XGleiche, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(10.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.EP2, TestResult.Result)
    End Sub

    'Aufteilung nur in Optimal-Chargen
    <TestMethod()> Public Sub Test_wb_Produktion_ChargenBerechnung_00()
        Dim Produktion As New wb_Produktion
        Dim TestResult As wb_Global.ChargenMengen

        'Soll 100kg aufgeteilt in 1 Optimal-Charge zu 100kg
        TestResult = Produktion.CalcChargenMenge(100, 10, 100, 100, wb_Global.ModusChargenTeiler.NurOptimal, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(100.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.OK, TestResult.Result)

        'Soll 112kg aufgeteilt in 1 Optimal-Charge zu 100kg - Fehler EM1
        TestResult = Produktion.CalcChargenMenge(112, 10, 100, 100, wb_Global.ModusChargenTeiler.NurOptimal, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(100.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.EM1, TestResult.Result)

        'Soll 180kg aufgeteilt in 2 Optimal-Charge zu 100kg - Fehler EM2
        TestResult = Produktion.CalcChargenMenge(180, 10, 100, 100, wb_Global.ModusChargenTeiler.NurOptimal, False)
        Assert.AreEqual(2, TestResult.AnzahlOpt)
        Assert.AreEqual(100.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.EM2, TestResult.Result)

        'Soll 150.1kg aufgeteilt in 2 Optimal-Charge zu 100kg - Fehler EM2
        TestResult = Produktion.CalcChargenMenge(150.1, 10, 100, 100, wb_Global.ModusChargenTeiler.NurOptimal, False)
        Assert.AreEqual(2, TestResult.AnzahlOpt)
        Assert.AreEqual(100.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.EM2, TestResult.Result)

        'Soll 150.0kg aufgeteilt in 1 Optimal-Charge zu 100kg - Fehler EM1
        TestResult = Produktion.CalcChargenMenge(150.0, 10, 100, 100, wb_Global.ModusChargenTeiler.NurOptimal, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(100.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.EM1, TestResult.Result)
    End Sub

    <TestMethod()> Public Sub Test_wb_Produktion_ChargenBerechnung_01()
        Dim Produktion As New wb_Produktion
        Dim TestResult As wb_Global.ChargenMengen

        'Soll 100kg aufgeteilt in 1 Optimal-Charge zu 100kg
        TestResult = Produktion.CalcChargenMenge(100, 10, 100, 100, wb_Global.ModusChargenTeiler.OptimalUndRest, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(100.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.OK, TestResult.Result)

        'Soll 110kg aufgeteilt in 1 Optimal-Charge zu 100kg und 1 Rest-Charge zu 10kg
        TestResult = Produktion.CalcChargenMenge(110, 10, 100, 100, wb_Global.ModusChargenTeiler.OptimalUndRest, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(100.0, TestResult.MengeOpt)
        Assert.AreEqual(1, TestResult.AnzahlRest)
        Assert.AreEqual(10.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.OK, TestResult.Result)

        'Soll 109kg aufgeteilt in 1 Optimal-Charge zu 100kg
        TestResult = Produktion.CalcChargenMenge(109, 10, 100, 100, wb_Global.ModusChargenTeiler.OptimalUndRest, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(100.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.EM1, TestResult.Result)

        'Soll 200kg aufgeteilt in 2 Optimal-Chargen zu 100kg
        TestResult = Produktion.CalcChargenMenge(200, 10, 100, 100, wb_Global.ModusChargenTeiler.OptimalUndRest, False)
        Assert.AreEqual(2, TestResult.AnzahlOpt)
        Assert.AreEqual(100.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.OK, TestResult.Result)

        'Soll 220kg aufgeteilt in 2 Optimal-Chargen zu 100kg und 1 Rest-Charge zu 10kg
        TestResult = Produktion.CalcChargenMenge(210, 10, 100, 100, wb_Global.ModusChargenTeiler.OptimalUndRest, False)
        Assert.AreEqual(2, TestResult.AnzahlOpt)
        Assert.AreEqual(100.0, TestResult.MengeOpt)
        Assert.AreEqual(1, TestResult.AnzahlRest)
        Assert.AreEqual(10.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.OK, TestResult.Result)
    End Sub

    <TestMethod()> Public Sub Test_wb_Produktion_ChargenBerechnung_02()
        Dim Produktion As New wb_Produktion
        Dim TestResult As wb_Global.ChargenMengen

        'Soll 100kg aufgeteilt in 1 Optimal-Charge zu 100kg
        TestResult = Produktion.CalcChargenMenge(100, 10, 100, 100, wb_Global.ModusChargenTeiler.MaximalUndRest, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(100.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.OK, TestResult.Result)

        'Soll 100kg aufgeteilt in 1 Rest-Charge zu 100kg
        TestResult = Produktion.CalcChargenMenge(100, 10, 200, 100, wb_Global.ModusChargenTeiler.MaximalUndRest, False)
        Assert.AreEqual(0, TestResult.AnzahlOpt)
        Assert.AreEqual(1, TestResult.AnzahlRest)
        Assert.AreEqual(100.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.OK, TestResult.Result)

        'Soll 200kg aufgeteilt in 1 Optimal(Maximal)-Charge zu 200kg
        TestResult = Produktion.CalcChargenMenge(200, 10, 200, 100, wb_Global.ModusChargenTeiler.MaximalUndRest, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(200.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.OK, TestResult.Result)

        'Soll 209kg aufgeteilt in 1 Maximal-Charge zu 200kg
        TestResult = Produktion.CalcChargenMenge(209, 50, 200, 100, wb_Global.ModusChargenTeiler.MaximalUndRest, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(159.0, TestResult.MengeOpt)
        Assert.AreEqual(1, TestResult.AnzahlRest)
        Assert.AreEqual(50.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.OK, TestResult.Result)

        'Soll 210kg aufgeteilt in 1 Maximal-Charge zu 200kg und eine Rest-Charge zu 10kg
        TestResult = Produktion.CalcChargenMenge(210, 10, 200, 100, wb_Global.ModusChargenTeiler.MaximalUndRest, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(200.0, TestResult.MengeOpt)
        Assert.AreEqual(1, TestResult.AnzahlRest)
        Assert.AreEqual(10.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.OK, TestResult.Result)

        'Soll 21kg aufgeteilt in 2 Maximal-Charge zu 10kg - Fehler EM1
        TestResult = Produktion.CalcChargenMenge(21, 10, 10, 10, wb_Global.ModusChargenTeiler.MaximalUndRest, False)
        Assert.AreEqual(2, TestResult.AnzahlOpt)
        Assert.AreEqual(10.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.EM1, TestResult.Result)

    End Sub

    <TestMethod()> Public Sub Test_wb_Produktion_ChargenBerechnung_09()
        Dim Produktion As New wb_Produktion
        Dim TestResult As wb_Global.ChargenMengen

        'Soll 100kg aufgeteilt in 1 Maximal-Charge zu 100kg
        TestResult = Produktion.CalcChargenMenge(100, 0, 100, 0, wb_Global.ModusChargenTeiler.RezeptGroesse, False)
        Assert.AreEqual(1, TestResult.AnzahlOpt)
        Assert.AreEqual(100.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.EP9, TestResult.Result)

        'Soll 110kg aufgeteilt in 2 Maximal-Charge zu 100kg
        TestResult = Produktion.CalcChargenMenge(200, 0, 100, 0, wb_Global.ModusChargenTeiler.RezeptGroesse, False)
        Assert.AreEqual(2, TestResult.AnzahlOpt)
        Assert.AreEqual(100.0, TestResult.MengeOpt)
        Assert.AreEqual(0, TestResult.AnzahlRest)
        Assert.AreEqual(0.0, TestResult.MengeRest)
        Assert.AreEqual(wb_Global.ChargenTeilerResult.EP9, TestResult.Result)

    End Sub
End Class