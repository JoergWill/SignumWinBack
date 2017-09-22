Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack
Imports WinBack.wb_Global
Imports WinBack.wb_Global.Parameter
Imports WinBack.wb_Global.LogType

<TestClass()> Public Class UnitTest_wb_ChangeLog
    Inherits wb_ChangeLog

    <TestInitialize>
    Sub TestInitialize()
        'Datenbank Verbindung Einstellungen setzen
        '(Muss in wb_Konfig gesetzt werden, weil My.Setting hier nicht funktioniert)
        wb_GlobalSettings.WinBackDBType = wb_Sql.dbType.mySql
        'Initialisierung Texte-Tabelle
        wb_Konfig.LoadTexteTabelle(wb_Konfig.GetLanguageNr())
    End Sub

    <TestMethod()> Public Sub Test_AddItem()
        'alle Einträge löschen
        ChangeLogClear()
        'einen Eintrag anfügen (String)
        ChangeLogAdd(Prm, Tx_Bezeichnung, "Test", "TestNeu")
        Assert.AreEqual("Test/TestNeu" + vbNewLine, ChangeLogReport(True))

        ''alle Einträge löschen
        ChangeLogClear()
        'einen Eintrag anfügen (Float)
        ChangeLogAdd(Nrw, T301_KiloJoule, 0.0F, 10.0F)
        Assert.AreEqual("   0,000 kJ/  10,000 kJ Kilojoule" + vbNewLine, ChangeLogReport(True))

        'alle Einträge löschen
        ChangeLogClear()
        'einen Eintrag anfügen (Allergen)
        ChangeLogAdd(Alg, T301_Eier, wb_Global.AllergenInfo.C, wb_Global.AllergenInfo.T)
        Assert.AreEqual("C/T Eier" + vbNewLine, ChangeLogReport(True))

        'alle Einträge löschen
        ChangeLogClear()
        'identische Einträge hinzufügen
        ChangeLogAdd(Prm, Tx_Bezeichnung, "Test", "Test")
        ChangeLogAdd(Alg, T301_Eier, wb_Global.AllergenInfo.C, wb_Global.AllergenInfo.C)
        Assert.AreEqual("", ChangeLogReport(True))

    End Sub
End Class