Imports WinBack

<TestClass()> Public Class UnitTest_wb_nwtCloud

    <TestMethod()> Public Sub Test_Create()

        'Create new instance of nwtCloud
        Dim nwt As New wb_nwtCloud(wb_Credentials.WinBackCloud_Pass, wb_Credentials.WinBackCloud_Url)

        'Lookup Product Name
        Assert.AreEqual(nwt.lookupProductName("XYZABCDEFG"), 0)
        Assert.IsTrue(nwt.lookupProductName("Mehl") > 0)

        'Lookup Product Name + Supplier
        Assert.AreEqual(nwt.lookupProduct("XYZABCDEFG", "XYZ"), 0)
        Assert.IsTrue(nwt.lookupProduct("Mehl", "Bäko") > 0)

        'Lookup ID
        Assert.AreEqual(nwt.GetProductData("1"), 1)
        nwt.DebugResultSet(0)

    End Sub

End Class