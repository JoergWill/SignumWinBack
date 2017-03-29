Imports System.Text
Imports System.Xml
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_nwtDatenLink

    <TestMethod()> Public Sub Test_Create()
        'Create new instance of nwtCloud
        Dim dl As New wb_nwtDatenLink(wb_Credentials.Datenlink_PAT, wb_Credentials.Datenlink_CAT, wb_Credentials.Datenlink_Url)
        'Validate Company Token
        Assert.AreEqual(dl.validateCompanyToken(), 1)

        'Lookup Product Name (OK)
        Assert.IsTrue(dl.lookupProductName("Mehl") > 0)
        'Lookup Product Name (FAIL)
        Assert.IsTrue(dl.lookupProductName("ABCDEFGHXYZ") = 0)

        'Lookup Product (OK)
        Assert.AreEqual(dl.GetProductData("DL-AC1-D10"), 1)

        Dim doc As XElement = XElement.Parse(dl.GetXMLResult)
        Dim x As String = doc.<Manufakturer>.<CompanyID>.Value
        Debug.Print(x)

        'Auswertung XML-Info
        Dim objXML As New Xml.XmlDocument
        objXML.LoadXml(dl.GetXMLResult)
        'Dim nsmgr As New XmlNamespaceManager(objXML.NameTable)
        'nsmgr.AddNamespace("datenlink", "urn:datenlink")

        Debug.Print(objXML.InnerXml)
        Debug.Print(objXML.ChildNodes(1).Name)
        Debug.Print(objXML.ChildNodes(1).NamespaceURI)
        Debug.Print(objXML.ChildNodes(1).ChildNodes(1).Name)
        Debug.Print(objXML.ChildNodes(1).ChildNodes(1).ChildNodes(0).Name)
        Debug.Print(objXML.ChildNodes(1).ChildNodes(1).ChildNodes(0).Item("CompanyID").Value)
        Debug.Print(objXML.ChildNodes(1).ChildNodes(1).ChildNodes(0).InnerText)
        Dim xs As String
        xs = objXML.SelectSingleNode("Manufacturer:CompanyID").InnerText
        If xs IsNot vbNullString Then
            Debug.Print(xs)
        End If

        'Lookup Product (FAIL)
        Assert.AreEqual(dl.GetProductData("XYZ"), -1)

        'Lookup Supplier (OK)
        Assert.IsTrue(dl.getDistributorData("613cd0c9-e47f-11e3-80e1-d43d7ed6cafe") > 0)





        'Lookup Supplier (FAIL)
        Assert.AreEqual(dl.getDistributorData("XYZ"), -1)

    End Sub

End Class