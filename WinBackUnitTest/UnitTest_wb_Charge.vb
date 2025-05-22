Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_Charge

    <TestMethod()> Public Sub TestChargeStkGewicht()
        'neue Klasse Chargen-Größen
        Dim c As New wb_Charge
        'Stk-Gewicht (default 1000gr)
        Assert.AreEqual("1000", c.StkGewicht)
        'Stk-Gewicht ändern
        c.StkGewicht = "300"
        Assert.AreEqual("300", c.StkGewicht)
        'Stk-Gewicht ändern
        c.StkGewicht = "300.2"
        Assert.AreEqual("300", c.StkGewicht)
        'Stk-Gewicht ändern
        c.StkGewicht = "300,2"
        Assert.AreEqual("300", c.StkGewicht)

        'Stk-Gewicht ändern
        c.StkGewicht = "0"
        Assert.AreEqual("1000", c.StkGewicht)
        'Stk-Gewicht ändern
        c.StkGewicht = ""
        Assert.AreEqual("1000", c.StkGewicht)
        'Stk-Gewicht ändern
        c.StkGewicht = "x"
        Assert.AreEqual("1000", c.StkGewicht)
    End Sub

    <TestMethod()> Public Sub TestChargeRezGewicht()
        'neue Klasse Chargen-Größen
        Dim c As New wb_Charge
        'Stk-Gewicht (default 1000gr)
        Assert.AreEqual("1000", c.StkGewicht)
        Assert.AreEqual("0,000", c.TeigGewicht)

        'Rezept-Gewicht ändern
        c.TeigGewicht = "10"
        Assert.AreEqual("10,000", c.TeigGewicht)

        'Rezept-Gewicht ändern
        c.TeigGewicht = "0"
        Assert.AreEqual("0,000", c.TeigGewicht)

        'Rezept-Gewicht ändern
        c.TeigGewicht = ""
        Assert.AreEqual("0,000", c.TeigGewicht)

        'Rezept-Gewicht ändern
        c.TeigGewicht = "x"
        Assert.AreEqual("0,000", c.TeigGewicht)
    End Sub

    <TestMethod()> Public Sub TestChargenGroessen()
        'neue Klasse Chargen-Größen
        Dim c As New wb_Charge
        'Stk-Gewicht (default 1000gr)
        Assert.AreEqual("1000", c.StkGewicht)
        Assert.AreEqual("0,000", c.TeigGewicht)

        'Teig-Gewicht
        c.TeigGewicht = "100"

        'Chargen-Größe in kg
        c.MengeInkg = "10KG"
        Assert.AreEqual("10,000", c.MengeInkg)
        c.MengeInkg = "10kg"
        Assert.AreEqual("10,000", c.MengeInkg)
        c.MengeInkg = "10,0KG"
        Assert.AreEqual("10,000", c.MengeInkg)

        'Chargen-Größe in kg
        c.MengeInkg = "10"
        Assert.AreEqual("10", c.MengeInProzent)
        c.MengeInkg = "100"
        Assert.AreEqual("100", c.MengeInProzent)
        c.MengeInkg = "200"
        Assert.AreEqual("200", c.MengeInProzent)
        c.MengeInkg = "0"
        Assert.AreEqual("0", c.MengeInProzent)
        c.MengeInkg = ""
        Assert.AreEqual("0", c.MengeInProzent)
        c.MengeInkg = "x"
        Assert.AreEqual("0", c.MengeInProzent)

        'Chargen-Größe in Stk
        c.MengeInStk = "10"
        Assert.AreEqual("10,000", c.MengeInkg)
        Assert.AreEqual("10", c.MengeInProzent)
        c.MengeInStk = "100"
        Assert.AreEqual("100,000", c.MengeInkg)
        Assert.AreEqual("100", c.MengeInProzent)
        c.MengeInStk = "1000"
        Assert.AreEqual("1000,000", c.MengeInkg)
        c.MengeInStk = "0"
        Assert.AreEqual("0,000", c.MengeInkg)
        c.MengeInStk = ""
        Assert.AreEqual("0,000", c.MengeInkg)
        c.MengeInStk = "x"
        Assert.AreEqual("0,000", c.MengeInkg)

        'Chargen-Größe in Prozent
        c.MengeInProzent = "10"
        Assert.AreEqual("10,000", c.MengeInkg)
        c.MengeInProzent = "100"
        Assert.AreEqual("100,000", c.MengeInkg)
        c.MengeInProzent = "1"
        Assert.AreEqual("1,000", c.MengeInkg)
        c.MengeInProzent = "0"
        Assert.AreEqual("0,000", c.MengeInkg)
        c.MengeInProzent = ""
        Assert.AreEqual("0,000", c.MengeInkg)
        c.MengeInProzent = "x"
        Assert.AreEqual("0,000", c.MengeInkg)

    End Sub
End Class