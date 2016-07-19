Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_Filiale

    <TestMethod()> Public Sub Test_sFiliale()
        'Klasse erzeugen
        Dim Filiale As New wb_Filiale

        'Test mit leerem Array
        Assert.IsFalse(Filiale.isProduction("9999"))
        Assert.IsFalse(Filiale.isProduction(""))

        'Testdaten in Array-List laden
        Filiale.AddFiliale = "9999"

        Assert.IsTrue(Filiale.isProduction("9999"))
        Assert.IsFalse(Filiale.isProduction("9"))
        Assert.IsFalse(Filiale.isProduction("1111"))
        Assert.IsFalse(Filiale.isProduction("-10"))
        Assert.IsFalse(Filiale.isProduction("abcd"))

        Filiale.AddFiliale = "9"
        Assert.IsTrue(Filiale.isProduction("9999"))
        Assert.IsTrue(Filiale.isProduction("9"))
        Assert.IsFalse(Filiale.isProduction("4"))

        Assert.IsTrue(Filiale.isProduction("4,9"))
        Assert.IsFalse(Filiale.isProduction("4,5"))
        Assert.IsTrue(Filiale.isProduction("9,9999"))
        Assert.IsTrue(Filiale.isProduction("9,4"))

        Filiale.AddFiliale = "4"
        Assert.IsTrue(Filiale.isProduction("4,9"))
        Assert.IsTrue(Filiale.isProduction("4,5"))
        Assert.IsTrue(Filiale.isProduction("5,9"))

        Assert.IsTrue(Filiale.isProduction(",9"))
        Assert.IsTrue(Filiale.isProduction("4,9,"))
        Assert.IsFalse(Filiale.isProduction(""))

    End Sub

End Class