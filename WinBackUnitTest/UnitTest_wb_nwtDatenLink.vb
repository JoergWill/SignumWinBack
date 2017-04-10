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
        Assert.AreEqual(dl.GetProductData("DL-CE3-C4D"), 1)
        Dim x As String = dl.GetXMLResult
        Debug.Print(x)

        'Dim doc As XElement = XElement.Parse(x)
        'x = doc.<Manufakturer>.<CompanyID>.Value

        'Auswertung XML-Info
        Dim objXML As New Xml.XmlDocument

        objXML.LoadXml(dl.GetXMLResult)
        Debug.Print(objXML.InnerXml)

        For Each oNode As Xml.XmlNode In objXML.ChildNodes(1).ChildNodes
            Select Case oNode.Name
                Case "Manufacturer"
                    For Each oItem As Xml.XmlNode In oNode.ChildNodes
                        Debug.Print(oItem.Name)
                        Debug.Print(oItem.InnerText)
                    Next
                Case "Product"
                    For Each oItem As Xml.XmlNode In oNode.ChildNodes
                        For Each oTag As XmlNode In oItem.ChildNodes
                            Debug.Print(oTag.Name)
                            Debug.Print(oTag.InnerText)
                        Next
                    Next
                Case "FoodFacts"
                    For Each oItem As Xml.XmlNode In oNode.ChildNodes
                        Select Case oItem.Name
                            Case "NutritionFacts"
                                For Each oTag As XmlNode In oItem.ChildNodes
                                    If oTag.Attributes.Count > 0 Then
                                        Debug.Print(oTag.Attributes(0).InnerText)
                                    Else
                                        Debug.Print(oTag.Name)
                                    End If
                                    Debug.Print(oTag.InnerText)
                                Next
                            Case "AllergenLabeling"
                                For Each oTag As XmlNode In oItem.ChildNodes
                                    Debug.Print(oTag.Name)
                                    Debug.Print(oTag.InnerText)
                                    Debug.Print(oTag.Attributes(1).Name)
                                    Debug.Print(oTag.Attributes(1).InnerText)
                                    For Each oDetail As XmlNode In oTag.ChildNodes
                                        Debug.Print(oDetail.Name)
                                        Debug.Print(oDetail.InnerText)
                                    Next
                                Next
                            Case "IngredientLists"
                        End Select
                    Next
            End Select
        Next


        'Lookup Product (FAIL)
        Assert.AreEqual(dl.GetProductData("XYZ"), -1)

        'Lookup Supplier (OK)
        Assert.IsTrue(dl.getDistributorData("613cd0c9-e47f-11e3-80e1-d43d7ed6cafe") > 0)





        'Lookup Supplier (FAIL)
        Assert.AreEqual(dl.getDistributorData("XYZ"), -1)

    End Sub

End Class