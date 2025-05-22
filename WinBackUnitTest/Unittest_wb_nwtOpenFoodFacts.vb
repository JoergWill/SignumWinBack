Imports WinBack

<TestClass()> Public Class Unittest_wb_nwtOpenFoodFacts
    ''' <summary>
    ''' Initialisiert die globalen Einstellungen.
    ''' </summary>
    ''' <param name="testContext"></param>
    <ClassInitialize()> Public Shared Sub Init_CloudTests(ByVal testContext As TestContext)
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()> Public Sub Test_CloudLookup()

        'Create new instance of nwtCloud
        Dim nwt As New wb_nwtCl_OpenFoodFacts(wb_Credentials.OpenFoodFacts_Url)

        'Lookup Product Name
        Assert.AreEqual(0, nwt.lookupProductName("XYZABCDEFG"))
        Assert.IsTrue(nwt.lookupProductName("Mehl") > 0)

        'Produktdaten lesen
        nwt.getProductList()

        ''Lookup Product Name + Supplier
        'Assert.AreEqual(nwt.lookupProduct("XYZABCDEFG", "XYZ"), 0)
        'Assert.IsTrue(nwt.lookupProduct("Mehl", "Bäko") > 0)

        ''Lookup ID
        'Assert.AreEqual(nwt.GetProductData("1"), 1)
        'nwt.DebugResultSet(0)

        'Change Url
        nwt.Url = "DieseUrlGibtEsNicht.de"
        Assert.AreEqual(nwt.lookupProductName("XYZABCDEFG"), -9)
        Assert.AreNotEqual(nwt.ErrorCode, "OK")
    End Sub

End Class
