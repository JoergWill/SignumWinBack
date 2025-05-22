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
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
        'Initialisierung Texte-Tabelle
        wb_Language.LoadTexteTabelle(wb_Language.GetLanguageNr())
    End Sub

    <TestMethod()> Public Sub Test_AddItem()
        Dim s As String = ""
        'alle Einträge löschen
        ChangeLogClear()
        s = ""
        'einen Eintrag anfügen (String)
        s += vbCrLf & "Bezeichnung [alt] " & "Test"
        s += vbCrLf & "Bezeichnung [neu] " & "TestNeu" & vbCrLf

        ChangeLogAdd(Prm, Tx_Bezeichnung, "Test", "TestNeu")
        Assert.AreEqual(s, ChangeLogReport(True))

        ''alle Einträge löschen
        ChangeLogClear()
        s = ""
        'einen Eintrag anfügen (Float)
        s += vbCrLf & "Nährwerte [alt/neu]" & vbCrLf
        s += "   0,000 kJ/  10,000 kJ Kilojoule" & vbCrLf

        ChangeLogAdd(Nrw, T301_KiloJoule, 0.0F, 10.0F)
        Assert.AreEqual(s, ChangeLogReport(True))

        'alle Einträge löschen
        ChangeLogClear()
        s = ""
        'einen Eintrag anfügen (Allergen)
        s += vbCrLf & "Allergene [alt/neu]" & vbCrLf
        s += "C/T Eier" + vbCrLf

        ChangeLogAdd(Alg, T301_Eier, wb_Global.AllergenInfo.C, wb_Global.AllergenInfo.T)
        Assert.AreEqual(s, ChangeLogReport(True))

        'alle Einträge löschen
        ChangeLogClear()
        s = ""
        'identische Einträge hinzufügen
        ChangeLogAdd(Prm, Tx_Bezeichnung, "Test", "Test")
        ChangeLogAdd(Alg, T301_Eier, wb_Global.AllergenInfo.C, wb_Global.AllergenInfo.C)
        Assert.AreEqual("", ChangeLogReport(True))

    End Sub
End Class