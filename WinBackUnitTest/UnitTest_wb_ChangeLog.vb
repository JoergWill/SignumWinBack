Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack
Imports WinBack.wb_Global
Imports WinBack.wb_Global.Parameter
Imports WinBack.wb_Global.LogType

<TestClass()> Public Class UnitTest_wb_ChangeLog
    Inherits wb_ChangeLog

    <TestMethod()> Public Sub Test_AddItem()

        ''alle Einträge löschen
        'ChangeLogClear()
        ''einen Eintrag anfügen (String)
        'ChangeLog.ChangeLogAdd(Prm, Tx_Bezeichnung, "Test", "TestNeu")
        'Assert.AreEqual("Test/TestNeu" + vbNewLine, ChangeLog.ChangeLogReport)

        ''alle Einträge löschen
        'ChangeLog.ChangeLogClear()
        ''einen Eintrag anfügen (Float)
        'ChangeLog.ChangeLogAdd(Nrw, T301_KiloJoule, 0.0F, 10.0F)
        'Assert.AreEqual("   0,000/  10,000" + vbNewLine, ChangeLog.ChangeLogReport)

        ''alle Einträge löschen
        'ChangeLog.ChangeLogClear()
        ''einen Eintrag anfügen (Allergen)
        'ChangeLog.ChangeLogAdd(Alg, T301_Eier, wb_Global.AllergenInfo.C, wb_Global.AllergenInfo.T)
        'Assert.AreEqual("C/T" + vbNewLine, ChangeLog.ChangeLogReport)

        ''alle Einträge löschen
        'ChangeLog.ChangeLogClear()
        ''identische Einträge hinzufügen
        'ChangeLog.ChangeLogAdd(Prm, Tx_Bezeichnung, "Test", "Test")
        'ChangeLog.ChangeLogAdd(Alg, T301_Eier, wb_Global.AllergenInfo.C, wb_Global.AllergenInfo.C)
        'Assert.AreEqual("", ChangeLog.ChangeLogReport)

    End Sub

End Class