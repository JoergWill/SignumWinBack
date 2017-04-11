Imports System.Text
Imports System.Xml
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_nwtDatenLink

    <TestMethod()> Public Sub Test_DatenLinkLookup()
        'Create new instance of nwtCloud
        Dim dl As New wb_nwtDatenLink(wb_Credentials.Datenlink_PAT, wb_Credentials.Datenlink_CAT, wb_Credentials.Datenlink_Url)
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

        'Nährwert-Info aus der Cloud lesen (Datum der letzten Änderung)
        Dim LastChange As Date = nwtUpdate.GetNaehrwerte("DL-CE3-C4D")

        'Ergebnis aus DatenLink XML auswerten
        Assert.AreEqual(nwtUpdate.nwtDaten.ktTyp301.TimeStamp, #03/09/2013 08:14:03#)
        Assert.AreEqual(nwtUpdate.nwtDaten.Lieferant, "BÄKO-Zentrale Süddeutschland eG")
        Assert.AreEqual(nwtUpdate.nwtDaten.Bezeichung, "BÄKO BiO Weizenschrot mittel")

        'Nährwert-Info Kalorien(KJoule)
        Assert.AreEqual(nwtUpdate.nwtDaten.ktTyp301.Naehrwert(2), 1293.0)

        'Allergene
        Assert.AreEqual(nwtUpdate.nwtDaten.ktTyp301.Allergen(141), wb_Global.AllergenInfo.C)
        Assert.AreEqual(nwtUpdate.nwtDaten.ktTyp301.Allergen(170), wb_Global.AllergenInfo.C)

    End Sub

End Class