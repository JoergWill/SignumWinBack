Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_ob_Rezept_Ingredients

    <TestMethod()> Public Sub TestServiceName()
        Dim Ri As New ob_RecipeProvider
        Assert.AreEqual("@WinBackRezepturSchnittstelle", Ri.ServiceName)
    End Sub

    <TestMethod()> Public Sub TestRezeptschritte()
        Dim Ri As New ob_RecipeProvider
        Dim Rc As ob_RecipeInfo

        Rc = Ri.GetRecipe("500", 0, 0, 0, 0, 0)
        DebugPrintIngedients(Rc.Ingredients)

        Rc = Ri.GetRecipe("500", 0, 0, 0, 0, 0)
        DebugPrintIngedients(Rc.Ingredients)

        Assert.AreEqual("@WinBackRezepturSchnittstelle", Ri.ServiceName)
    End Sub

    Private Sub DebugPrintIngedients(i As IList)
        'Prüfen ob ein Ergebnis zurückgemeldet wird    
        If i IsNot Nothing Then
            For Each x As ob_RecipeIngredient In i
                Debug.Print(x.ArticleNo & "/" & x.Amount)
                If x.Ingredients IsNot Nothing Then
                    DebugPrintIngedients(x.Ingredients)
                End If
            Next
        Else
            Debug.Print("Keine Daten ...")
        End If
    End Sub

End Class