Imports System.Text
Imports System.Xml
Imports Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_nwtDatenLink

    <ClassInitialize()> Public Shared Sub Test_DatenLinkInit(ByVal context As TestContext)
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()> Public Sub Test_DatenLinkLookup()
        'Create new instance of nwtCloud
        Dim dl As New wb_nwtCl_DatenLink(wb_Credentials.Datenlink_PAT, wb_Credentials.Datenlink_CAT, wb_Credentials.Datenlink_Url)
        'Validate Company Token
        Assert.AreEqual(dl.validateCompanyToken(), 1)

        'Lookup Product Name (OK)
        Assert.IsTrue(dl.lookupProductName("Mehl") > 0)
        'Lookup Product Name (FAIL)
        Assert.IsTrue(dl.lookupProductName("ABCDEFGHXYZ") = 0)

        'Lookup Product (OK)
        Assert.AreEqual(dl.GetProductData("DL-CE3-C4D"), 1)
        Dim x As String = dl.GetXMLResult
        Debug.Print(x)

        'Lookup Product (FAIL)
        Assert.AreEqual(dl.GetProductData("XYZ"), -1)

        'Lookup Supplier (OK)
        Assert.IsTrue(dl.getDistributorData("613cd0c9-e47f-11e3-80e1-d43d7ed6cafe") > 0)

        'Lookup Supplier (FAIL)
        Assert.AreEqual(dl.getDistributorData("XYZ"), -1)

    End Sub

    <TestMethod()> Public Sub Test_DatenLinkUpdate()
        Dim nwtUpdate As New wb_nwtUpdate
        Dim nwtDaten As New wb_Komponente

        'Nährwert-Info aus der Cloud lesen (Datum der letzten Änderung)
        Dim LastChange As Date = nwtUpdate.GetNaehrwerte("DL-CE3-C4D", nwtDaten)

        'Ergebnis aus DatenLink XML auswerten
        Assert.AreEqual(nwtDaten.ktTyp301.TimeStamp, #03/09/2013 08:14:03#)
        Assert.IsTrue(nwtDaten.Lieferant.Contains("BÄKO"))
        'Die Komponenten-Bezeichnung wird nicht mehr überschrieben
        'Assert.IsTrue(nwtDaten.Bezeichnung.Contains("Weizenschrot"))

        'Nährwert-Info Kalorien(KJoule)
        Assert.AreEqual(nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_KiloJoule), 1293.0)

        'Allergene
        Assert.AreEqual(nwtDaten.ktTyp301.Allergen(wb_Global.T301_Gluten), wb_Global.AllergenInfo.C)
        Assert.AreEqual(nwtDaten.ktTyp301.Allergen(wb_Global.T301_Weizen), wb_Global.AllergenInfo.C)

    End Sub

    <TestMethod()> Public Sub Test_DatenLinkUpdate_JsonFormat()
        Dim a As ArrayList
        Dim dl As New wb_nwtCl_DatenLink(wb_GlobalSettings.Datenlink_PAT, wb_GlobalSettings.Datenlink_CAT, wb_GlobalSettings.Datenlink_Url)

        'Lookup Product Name (OK)
        Assert.IsTrue(dl.lookupProductName("DinkelMalz") > 0)
        a = dl.getProductList()
        Assert.IsTrue(a.Count > 0)

        'Nährwert-Info aus der Cloud lesen (Datum der letzten Änderung)
        'Dieser JSON-Datensatz hat ein spezielles Format (siehe Mail martin@niehaves.de vom 06.02.2024)
        'Rohstoff Structure-C DL-AA1-53D
        Assert.AreEqual(1, dl.lookupProductName("Structure C"))
        a = dl.getProductList()
        Assert.AreEqual(1, a.Count)

        'Der Fehler tritt offensichtlich auf, wenn Datenlink nur einen gefundenen Rohstoff zurückliefert.
        '(geändertes Datenformat)
        Assert.AreEqual(1, dl.lookupProductName("RoggenDinkelMalz"))
        a = dl.getProductList()
        Assert.AreEqual(1, a.Count)

    End Sub

End Class