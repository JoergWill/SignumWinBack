Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack
Imports WinBack.wb_Global
Imports WinBack.wb_Global.Parameter
Imports WinBack.wb_Global.LogType

<TestClass()> Public Class UnitTest_wb_ChangeLog
    Inherits wb_ChangeLog

    <TestMethod()> Public Sub Test_AddItem()

        'alle Einträge löschen
        ChangeLogClear()
        'einen Eintrag anfügen (String)
        ChangeLogAdd(Prm, Tx_Bezeichnung, "Test", "TestNeu")
        Assert.AreEqual("Test/TestNeu" + vbNewLine, ChangeLogReport)

        ''alle Einträge löschen
        ChangeLogClear()
        'einen Eintrag anfügen (Float)
        ChangeLogAdd(Nrw, T301_KiloJoule, 0.0F, 10.0F)
        Assert.AreEqual("   0,000/  10,000" + vbNewLine, ChangeLogReport)

        'alle Einträge löschen
        ChangeLogClear()
        'einen Eintrag anfügen (Allergen)
        ChangeLogAdd(Alg, T301_Eier, wb_Global.AllergenInfo.C, wb_Global.AllergenInfo.T)
        Assert.AreEqual("C/T" + vbNewLine, ChangeLogReport)

        'alle Einträge löschen
        ChangeLogClear()
        'identische Einträge hinzufügen
        ChangeLogAdd(Prm, Tx_Bezeichnung, "Test", "Test")
        ChangeLogAdd(Alg, T301_Eier, wb_Global.AllergenInfo.C, wb_Global.AllergenInfo.C)
        Assert.AreEqual("", ChangeLogReport)

    End Sub

End Class