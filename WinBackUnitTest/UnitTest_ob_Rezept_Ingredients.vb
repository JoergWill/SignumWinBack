Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_ob_Rezept_Ingredients

    Const Unit_Stk As Short = 0
    Const Unit_kg As Short = 11
    Const Unit_L As Short = 16

    ''' <summary>
    ''' Initialisiert die Datenbank-Einstellungen.
    ''' Falls notwendig werden die Datensicherungen aus ... in die DB eingespielt
    ''' </summary>
    ''' <param name="testContext"></param>
    <ClassInitialize()> Public Shared Sub InitDBTests(ByVal testContext As TestContext)
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni()
    End Sub

    <TestMethod()> Public Sub TestServiceName()
        Dim Ri As New ob_RecipeProvider
        Assert.AreEqual("@WinBackRezepturSchnittstelle", Ri.ServiceName)
    End Sub

    ''' <summary>
    ''' Test RezeptInfo - lineares Rezept
    ''' Artikel 500 -   Quarkstuten Variante 1 (Branch 0) Losgröße 1 Stück
    '''                 Stückgewicht     1,000 kg
    '''                 Rezeptgewicht   22,710 kg
    '''                 
    '''     500     Quarkstuten                 1,000 Stk   (Unit 0)
    '''     S01     Weissmehl       10,000 kg   0,44033 kg  (Unit 11)
    '''     9906    Zucker           1,500 kg   0,06605 kg  (Unit 11)
    '''     9907    Backmagarine     1,000 kg   0,04403 kg  (Unit 11)
    ''' </summary>
    <TestMethod()> Public Sub TestRezeptschritte()
        Dim Ri As New ob_RecipeProvider
        Dim Rc As ob_RecipeInfo

        'erste Abfrage Artikelnummer=500, Unit=0(Stk), Color=0, Size="2", Version=0, Branch=0
        Rc = Ri.GetRecipe("500", 0, 0, "2", 0, 0)

        'Test Artikel-Nummer
        Assert.AreEqual("500", Rc.ArticleNo)
        'Test Einheit (St)
        Assert.AreEqual(Unit_Stk, Rc.Unit)
        'Test Size
        Assert.AreEqual("2", Rc.Size)

        'Anzahl der Rezeptbestandteile (=10)
        Assert.AreEqual(10, Rc.Ingredients.Count)

        'Rezept-Zeile 1 - Rohstoffnummer
        Assert.AreEqual("S01", DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).ArticleNo)
        'Rezept-Zeile 1 - Sollwert berechnet auf 1 Stk (1kg)
        Dim Amount As Decimal = Math.Round(DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).Amount, 5)
        Assert.AreEqual(0.44033D, Amount)
        'Rezept-Zeile 1 - Einheit (kg)
        Assert.AreEqual(Unit_kg, DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).Unit)




        DebugPrintIngedients(Rc.Ingredients)

        'zweite Abfrage Artikelnummer=500, Unit=0(Stk), Color=0, Size="1", Version=0, Branch=0
        Rc = Ri.GetRecipe("500", 0, 0, "1", 0, 0)

        'Test Artikel-Nummer
        Assert.AreEqual("500", Rc.ArticleNo)
        'Test Einheit (St)
        Assert.AreEqual(Unit_Stk, Rc.Unit)
        'Test Size
        Assert.AreEqual("1", Rc.Size)
        DebugPrintIngedients(Rc.Ingredients)

        'Abfrage Artikelnummer=9906 (Zucker), Unit=0(Stk), Color=0, Size="1", Version=0, Branch=0
        'Rohstoff hat KEINE Zutatenliste!
        Rc = Ri.GetRecipe("9906", 0, 0, "1", 0, 0)

        'Test Artikel-Nummer
        Assert.AreEqual("9906", Rc.ArticleNo)
        'Test Einheit (St)
        Assert.AreEqual(Unit_Stk, Rc.Unit)
        'Test Size
        Assert.AreEqual("1", Rc.Size)
        'Test Zutatenliste
        Assert.AreEqual(Nothing, Rc.Ingredients)





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