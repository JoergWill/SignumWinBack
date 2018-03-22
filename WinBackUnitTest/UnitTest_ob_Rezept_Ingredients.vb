Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_ob_Rezept_Ingredients

    <TestMethod()> Public Sub TestServiceName()
        Dim Ri As New ob_Rezept_Ingredients
        Assert.AreEqual("WinBackRezepturSchnittstelle", Ri.ServiceName)
    End Sub

End Class