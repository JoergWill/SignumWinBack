Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_Filiale

    <TestInitialize>
    Sub TestInitialize()
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()> Public Sub Test_FilialeIstProduktion()

        'Test mit leerem Array
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("9999"))
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion(""))

        'Test wenn Ms-Datenbank aktiv
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("1"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("2"))

        'Testdaten in Array-List laden
        wb_Filiale.AddFiliale("9999")

        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("9999"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("9"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("1111"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("-10"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("abcd"))

        wb_Filiale.AddFiliale("9")
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("9999"))
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("9"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("4"))

        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("4,9"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("4,5"))
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("9,9999"))
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("9,4"))

        wb_Filiale.AddFiliale("4")
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("4,9"))
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("4,5"))
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("5,9"))

        Assert.IsTrue(wb_Filiale.FilialeIstProduktion(",9"))
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("4,9,"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion(""))

    End Sub

    <TestMethod()> Public Sub Test_SortimentIstProduktion()

        'Test mit leerem Array
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest
        Assert.IsTrue(wb_Filiale.SortimentIstProduktion("9"))
        Assert.IsTrue(wb_Filiale.SortimentIstProduktion(""))

        'Test wenn Ms-Datenbank aktiv
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack
        Assert.IsTrue(wb_Filiale.SortimentIstProduktion("01"))
        Assert.IsTrue(wb_Filiale.SortimentIstProduktion("02"))
    End Sub

End Class