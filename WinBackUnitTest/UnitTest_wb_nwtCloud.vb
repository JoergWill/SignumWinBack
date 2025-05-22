Imports WinBack

<TestClass()> Public Class UnitTest_wb_nwtCloud

    ''' <summary>
    ''' Initialisiert die globalen Einstellungen.
    ''' </summary>
    <TestInitialize>
    Sub TestInitialize()
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
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
        'Die Komponenten-Bezeichnung wird nicht überschrieben !!
        'Assert.AreEqual(nwtDaten.Bezeichnung, "Westfalia Kornkruste Kornfit")
        Debug.Print(nwtDaten.GetReport)

        'Komponenten-Nummer (intern) setzen
        nwtDaten.Nr = 1
        'Report schreiben (in Tabelle Hinweise2)
        nwtDaten.SaveReport()

        'Nährwert-Info Kalorien(KJoule)
        Assert.AreEqual(nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_KiloJoule), 1998.0)

        'Allergene
        Assert.AreEqual(nwtDaten.ktTyp301.Allergen(wb_Global.T301_Gluten), wb_Global.AllergenInfo.C)
        Assert.AreEqual(nwtDaten.ktTyp301.Allergen(wb_Global.T301_Sojaerzeugnisse), wb_Global.AllergenInfo.C)
        Assert.AreEqual(nwtDaten.ktTyp301.Allergen(wb_Global.T301_Eier), wb_Global.AllergenInfo.T)

    End Sub

    <TestMethod()> Public Sub Test_GetSheetList()
        'Create new instance of nwtCloud
        Dim nwt As New wb_nwtCl_WinBack(wb_Credentials.WinBackCloud_Pass, wb_Credentials.WinBackCloud_Url)

        'Lookup Product Sheet-Liste
        Assert.IsTrue(nwt.GetProductSheetCount("33") > 0)
        nwt.DebugResultSet(0)

        'Lookup Product Sheet
        Assert.IsTrue(nwt.GetProductSheet("33", "MM Dinkelmehl Type 1050.pdf") > 0)
        nwt.DebugResultSet(0)
    End Sub



End Class