Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_ktTyp301

    <TestMethod()> Public Sub TestAllergene()
        Dim ktTyp301 As New wb_ktTyp301

        Assert.IsTrue(ktTyp301.IsAllergen(141))
        Assert.IsTrue(ktTyp301.IsAllergen(189))

        Assert.IsFalse(ktTyp301.IsAllergen(1))
        Assert.IsFalse(ktTyp301.IsAllergen(140))
        Assert.IsFalse(ktTyp301.IsAllergen(200))
        Assert.IsFalse(ktTyp301.IsAllergen(-1))

        ktTyp301.Allergen(1) = wb_Global.AllergenInfo.K
        Assert.AreEqual(ktTyp301.Allergen(1), wb_Global.AllergenInfo.ERR)
        ktTyp301.Allergen(141) = wb_Global.AllergenInfo.K
        Assert.AreEqual(ktTyp301.Allergen(141), wb_Global.AllergenInfo.K)
        ktTyp301.Allergen(189) = wb_Global.AllergenInfo.K
        Assert.AreEqual(ktTyp301.Allergen(189), wb_Global.AllergenInfo.K)

        ktTyp301.Naehrwert(141) = 100
        Assert.AreEqual(ktTyp301.Naehrwert(141), 0.0)
        ktTyp301.Naehrwert(1) = 100
        Assert.AreEqual(ktTyp301.Naehrwert(1), 100.0)

        ktTyp301.Wert(140) = 100
        Assert.AreEqual(ktTyp301.Naehrwert(140), 100.0)

        ktTyp301.Wert(141) = wb_Global.AllergenInfo.C.ToString
        Assert.AreEqual(ktTyp301.Allergen(141), wb_Global.AllergenInfo.C)

    End Sub

End Class