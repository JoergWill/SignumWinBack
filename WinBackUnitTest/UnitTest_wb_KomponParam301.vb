Imports WinBack

<TestClass()> Public Class UnitTest_wb_KomponParam301
    <TestInitialize>
    Sub TestInitialize()
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()> Public Sub TestAllergene()
        Dim ktTyp301 As New wb_KomponParam301

        'TODO in anderen Test umlagern 
        'Assert.IsTrue(ktTyp301.IsAllergen(141))
        'Assert.IsTrue(ktTyp301.IsAllergen(189))

        'Assert.IsFalse(ktTyp301.IsAllergen(1))
        'Assert.IsFalse(ktTyp301.IsAllergen(140))
        'Assert.IsFalse(ktTyp301.IsAllergen(200))
        'Assert.IsFalse(ktTyp301.IsAllergen(-1))

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

    <TestMethod()>
    Public Sub Test_LoadKompon301Tabelle()
        Assert.AreEqual("Kilojoule", wb_KomponParam301_Global.kt301Param(wb_Global.T301_KiloJoule).Bezeichnung)
        Assert.AreEqual("kJ", wb_KomponParam301_Global.kt301Param(wb_Global.T301_KiloJoule).Einheit)
    End Sub

    <TestMethod()>
    Public Sub Test_CheckCalcNaehrwerteKonsistent()
        Dim ktTyp301 As New wb_KomponParam301

        'Test FEHLERHAFTE Angaben Kalorien/KJ
        ktTyp301.Naehrwert(wb_Global.T301_KiloJoule) = 100
        ktTyp301.Naehrwert(wb_Global.T301_Kilokalorien) = 2000

        'Fehler bei der Konsistenzprüfung kJ-kcal
        Assert.IsTrue(ktTyp301.NwtTabelle(wb_Global.T301_KiloJoule).ErrIntern)
        'Fehler bei der Konsistenzprüfung kcal-kJ
        Assert.IsTrue(ktTyp301.NwtTabelle(wb_Global.T301_Kilokalorien).ErrIntern)

        'Test RICHTIGE Angaben Kalorien/KJ (Umrechnung 1 kcal = 4,18684 KJ)
        ktTyp301.Naehrwert(wb_Global.T301_Kilokalorien) = 23.88
        'Kein Fehler bei der Konsistenzprüfung kJ-kcal
        Assert.IsFalse(ktTyp301.NwtTabelle(wb_Global.T301_KiloJoule).ErrIntern)
        'Kein Fehler bei der Konsistenzprüfung kcal-kJ
        Assert.IsFalse(ktTyp301.NwtTabelle(wb_Global.T301_Kilokalorien).ErrIntern)


        'Test FEHLERHAFTE Angaben Natrium/Kochsalz
        ktTyp301.Naehrwert(wb_Global.T301_Natrium) = 1000
        ktTyp301.Naehrwert(wb_Global.T301_GesamtKochsalz) = 2000

        'Fehler bei der Konsistenzprüfung Natrium-Kochsalz
        Assert.IsTrue(ktTyp301.NwtTabelle(wb_Global.T301_Natrium).ErrIntern)
        'Fehler bei der Konsistenzprüfung Kochsalz-Natrium
        Assert.IsTrue(ktTyp301.NwtTabelle(wb_Global.T301_GesamtKochsalz).ErrIntern)

        'Test RICHTIGE Angaben Natrium/Kochsalz
        ktTyp301.Naehrwert(wb_Global.T301_GesamtKochsalz) = 2540

        'Kein Fehler bei der Konsistenzprüfung Natrium-Kochsalz
        Assert.IsFalse(ktTyp301.NwtTabelle(wb_Global.T301_Natrium).ErrIntern)
        'Kein Fehler bei der Konsistenzprüfung Kochsalz-Natrium
        Assert.IsFalse(ktTyp301.NwtTabelle(wb_Global.T301_GesamtKochsalz).ErrIntern)
    End Sub

End Class