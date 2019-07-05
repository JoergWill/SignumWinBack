Imports WinBack

<TestClass()> Public Class UnitTest_wb_nwtCloud

    ''' <summary>
    ''' Initialisiert die globalen Einstellungen.
    ''' </summary>
    ''' <param name="testContext"></param>
    <ClassInitialize()> Public Shared Sub Init_CloudTests(ByVal testContext As TestContext)
        'Programm-Variante Unit-Test
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest
    End Sub

    <TestMethod()> Public Sub Test_CloudLookup()

        'Create new instance of nwtCloud
        Dim nwt As New wb_nwtCl_WinBack(wb_Credentials.WinBackCloud_Pass, wb_Credentials.WinBackCloud_Url)

        'Lookup Product Name
        Assert.AreEqual(nwt.lookupProductName("XYZABCDEFG"), 0)
        Assert.IsTrue(nwt.lookupProductName("Mehl") > 0)

        'Lookup Product Name + Supplier
        Assert.AreEqual(nwt.lookupProduct("XYZABCDEFG", "XYZ"), 0)
        Assert.IsTrue(nwt.lookupProduct("Mehl", "Bäko") > 0)

        'Lookup ID
        Assert.AreEqual(nwt.GetProductData("1"), 1)
        nwt.DebugResultSet(0)

        'Change Url
        nwt.Url = "DieseUrlGibtEsNicht.de"
        Assert.AreEqual(nwt.lookupProductName("XYZABCDEFG"), -9)
        Assert.AreNotEqual(nwt.ErrorCode, "OK")
    End Sub

    <TestMethod()> Public Sub Test_CloudUpdate()
        Dim nwtUpdate As New wb_nwtUpdate
        Dim nwtDaten As New wb_Komponente

        'Nährwert-Info aus der Cloud lesen (Datum der letzten Änderung)
        nwtDaten.ClearReport()
        Dim LastChange As Date = nwtUpdate.GetNaehrwerte("1", nwtDaten)

        'Ergebnis aus Cloud(JSON) auswerten
        Assert.AreEqual(nwtDaten.ktTyp301.TimeStamp, #06/28/2018 09:27:45#)
        Assert.AreEqual(nwtDaten.Lieferant, "LINDEMANN")
        Assert.AreEqual(nwtDaten.Bezeichnung, "Westfalia Kornkruste Kornfit")
        Debug.Print(nwtDaten.GetReport)

        'Nährwert-Info Kalorien(KJoule)
        Assert.AreEqual(nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_KiloJoule), 1998.0)

        'Allergene
        Assert.AreEqual(nwtDaten.ktTyp301.Allergen(wb_Global.T301_Gluten), wb_Global.AllergenInfo.C)
        Assert.AreEqual(nwtDaten.ktTyp301.Allergen(wb_Global.T301_Sojaerzeugnisse), wb_Global.AllergenInfo.C)
        Assert.AreEqual(nwtDaten.ktTyp301.Allergen(wb_Global.T301_Eier), wb_Global.AllergenInfo.T)

    End Sub
End Class