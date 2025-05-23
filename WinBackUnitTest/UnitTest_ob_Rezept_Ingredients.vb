﻿Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_ob_Rezept_Ingredients

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
    ''' Artikel 109004 -   Quarkstuten Variante 1 (Branch 0) Losgröße 1 Stück
    '''                    Stückgewicht     1,000 kg
    '''                    Rezeptgewicht   22,710 kg
    '''                 
    '''     109004     Quarkstuten                 1,000 Stk   (Unit 0)
    '''     200001     Weissmehl       10,000 kg   0,44033 kg  (Unit 11)
    '''     201002     Zucker           1,500 kg   0,06605 kg  (Unit 11)
    '''     212011     Backmagarine     1,000 kg   0,04403 kg  (Unit 11)
    '''     ...
    '''     200000     Schüttwasser     2,000 L    0,08806 L   (Unit 16)
    ''' </summary>
    <TestMethod()> Public Sub TestRezeptschritteFlachesRezept()
        Dim Ri As New ob_RecipeProvider
        Dim Rc As ob_RecipeInfo

        'erste Abfrage Artikelnummer=500, Unit=0(Stk), Color=0, Size="2", Version=0, Branch=0
        Rc = Ri.GetRecipe("109004", 0, 0, "2", 0, 0)

        'Test Artikel-Nummer
        Assert.AreEqual("109004", Rc.ArticleNo)
        'Test Einheit (St)
        Assert.AreEqual(wb_Global.obEinheitStk, Rc.Unit)
        'Test Size muss die übergebenen Parameter zurückgeben
        Assert.AreEqual("2", Rc.Size)

        'Anzahl der Rezeptbestandteile (=10)
        Assert.AreEqual(10, Rc.Ingredients.Count)
        'Artikel ist Produktions-Artikel
        Assert.AreEqual(True, Rc.ProductionArticle)
        'Rezept-Type ist Produktions-Rezept variabel
        Assert.AreEqual(wb_Global.RecipeTypeProdVariabel, Rc.RecipeType)


        'Rezept-Zeile 1 - Rohstoffnummer
        Assert.AreEqual("200001", DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).ArticleNo)
        'Rezept-Zeile 1 - Sollwert berechnet auf 1 Stk (1kg)
        Assert.AreEqual(0.352D, Math.Round(DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).Amount, 3))
        'Rezept-Zeile 1 - Einheit (kg)
        Assert.AreEqual(wb_Global.obEinheitKilogramm, DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).Unit)
        'Rezept-Zeile 1 - Size (bei Unterartikeln immer NULL)
        Assert.AreEqual("NULL", DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).Size)

        'Rezept-Zeile 1 hat keine weiteren Rezept-im-Rezept-Verknüpfungen
        Assert.AreEqual(Nothing, DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).Ingredients)
        'Rezept-Zeile 1 hat keine weiteren Rezept-im-Rezept-Verknüpfungen - ProductionArticle=False
        Assert.AreEqual(False, DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).ProductionArticle)
        'Rezept-Zeile 1 hat keine weiteren Rezept-im-Rezept-Verknüpfungen - RecipeType=NoRercipe(0)
        Assert.AreEqual(wb_Global.RecipeTypeNoRecipe, DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).RecipeType)

        'Rezept-Zeile 2 - Rohstoffnummer
        Assert.AreEqual("201002", DirectCast(Rc.Ingredients(1), ob_RecipeIngredient).ArticleNo)
        'Rezept-Zeile 2 - Sollwert berechnet auf 1 Stk (1kg)
        Assert.AreEqual(0.053D, Math.Round(DirectCast(Rc.Ingredients(1), ob_RecipeIngredient).Amount, 3))
        'Rezept-Zeile 2 - Einheit (kg)
        Assert.AreEqual(wb_Global.obEinheitKilogramm, DirectCast(Rc.Ingredients(1), ob_RecipeIngredient).Unit)
        'Rezept-Zeile 2 - Size (bei Unterartikeln immer NULL)
        Assert.AreEqual("NULL", DirectCast(Rc.Ingredients(1), ob_RecipeIngredient).Size)

        'Rezept-Zeile 8 - Rohstoffnummer
        Assert.AreEqual("200000", DirectCast(Rc.Ingredients(7), ob_RecipeIngredient).ArticleNo)
        'Rezept-Zeile 8 - Sollwert berechnet auf 1 Stk (1kg)
        Assert.AreEqual(0.07D, Math.Round(DirectCast(Rc.Ingredients(7), ob_RecipeIngredient).Amount, 3))
        'Rezept-Zeile 8 - Einheit (L)
        Assert.AreEqual(wb_Global.obEinheitLiter, DirectCast(Rc.Ingredients(7), ob_RecipeIngredient).Unit)
        'Rezept-Zeile 8 - Size (bei Unterartikeln immer NULL)
        Assert.AreEqual("NULL", DirectCast(Rc.Ingredients(7), ob_RecipeIngredient).Size)


        'zweite Abfrage Artikelnummer=500, Unit=0(Stk), Color=0, Size="1", Version=0, Branch=0
        Rc = Ri.GetRecipe("109004", 0, 0, "1", 0, 0)

        'Test Artikel-Nummer
        Assert.AreEqual("109004", Rc.ArticleNo)
        'Test Einheit (St)
        Assert.AreEqual(wb_Global.obEinheitStk, Rc.Unit)
        'Test Size
        Assert.AreEqual("1", Rc.Size)

        'Anzahl der Rezeptbestandteile (=10)
        Assert.AreEqual(10, Rc.Ingredients.Count)

        'Rezept-Zeile 1 - Rohstoffnummer
        Assert.AreEqual("200001", DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).ArticleNo)
        'Rezept-Zeile 1 - Sollwert berechnet auf 1 Stk (1kg)
        Assert.AreEqual(0.352D, Math.Round(DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).Amount, 3))
        'Rezept-Zeile 1 - Einheit (kg)
        Assert.AreEqual(wb_Global.obEinheitKilogramm, DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).Unit)
        'Rezept-Zeile 1 - Size (bei Unterartikeln immer NULL)
        Assert.AreEqual("NULL", DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).Size)

        'Rezept-Zeile 2 - Rohstoffnummer
        Assert.AreEqual("201002", DirectCast(Rc.Ingredients(1), ob_RecipeIngredient).ArticleNo)
        'Rezept-Zeile 2 - Sollwert berechnet auf 1 Stk (1kg)
        Assert.AreEqual(0.053D, Math.Round(DirectCast(Rc.Ingredients(1), ob_RecipeIngredient).Amount, 3))
        'Rezept-Zeile 2 - Einheit (kg)
        Assert.AreEqual(wb_Global.obEinheitKilogramm, DirectCast(Rc.Ingredients(1), ob_RecipeIngredient).Unit)
        'Rezept-Zeile 2 - Size (bei Unterartikeln immer NULL)
        Assert.AreEqual("NULL", DirectCast(Rc.Ingredients(1), ob_RecipeIngredient).Size)


        'Abfrage Artikelnummer=9906 (Zucker), Unit=0(Stk), Color=0, Size="1", Version=0, Branch=0
        'Rohstoff hat KEINE Zutatenliste!
        Rc = Ri.GetRecipe("201002", 0, 0, "1", 0, 0)

        'Test Artikel-Nummer
        Assert.AreEqual("201002", Rc.ArticleNo)
        'Test Einheit (St)
        Assert.AreEqual(wb_Global.obEinheitStk, Rc.Unit)
        'Test Size
        Assert.AreEqual("1", Rc.Size)
        'Test Zutatenliste
        Assert.AreEqual(Nothing, Rc.Ingredients)

    End Sub

    ''' <summary>
    ''' Test RezeptInfo - Rezept-im-Rezept
    ''' Artikel 107002 -   Schwarz-Weiß-Gebäck Variante 1 (Branch 0) Losgröße 1 Stück
    '''                    Stückgewicht     3,265 kg
    '''                    Rezeptgewicht    3,265 kg
    '''                 
    '''     107002   Schwarz-Weiß-Gebäck   1,000 Stk   (Unit 0)
    '''     220009   Mürbeteig weiss  1,590 kg   1,590   kg  (Unit 11)
    '''     220010   Mürbeteig dunkel 1,590 kg   1,675   kg  (Unit 11)
    '''     
    ''' </summary>
    <TestMethod()> Public Sub TestRezeptschritteRezeptImRezept()
        Dim Ri As New ob_RecipeProvider
        Dim Rc As ob_RecipeInfo

        'erste Abfrage Artikelnummer=107002, Unit=0(Stk), Color=0, Size="1", Version=0, Branch=0
        Rc = Ri.GetRecipe("107002", 0, 0, "1", 0, 0)

        'Testausgabe
        DebugPrintIngedients(Rc.Ingredients)

        'Test Artikel-Nummer
        Assert.AreEqual("107002", Rc.ArticleNo)
        'Test Einheit (St)
        Assert.AreEqual(wb_Global.obEinheitStk, Rc.Unit)
        'Test Size muss die übergebenen Parameter zurückgeben
        Assert.AreEqual("1", Rc.Size)

        'Rezept-Zeile 1 - Rohstoffnummer
        Assert.AreEqual("220009", DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).ArticleNo)
        'Rezept-Zeile 1 - Sollwert berechnet auf 1 Stk (3,265kg)
        Assert.AreEqual(1.59D, Math.Round(DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).Amount, 5))
        'Rezept-Zeile 1 - Einheit (kg)
        Assert.AreEqual(wb_Global.obEinheitKilogramm, DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).Unit)
        'Rezept-Zeile 1 - Size (bei Unterartikeln immer NULL)
        Assert.AreEqual("NULL", DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).Size)
        'Rezept-Zeile 1 ist Produktions-Artikel
        Assert.AreEqual(True, DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).ProductionArticle)
        'Rezept-Zeile 1 Type ist Produktions-Rezept variabel
        Assert.AreEqual(wb_Global.RecipeTypeProdVariabel, DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).RecipeType)

        'Rezept-Zeile 2 - Rohstoffnummer
        Assert.AreEqual("220010", DirectCast(Rc.Ingredients(1), ob_RecipeIngredient).ArticleNo)
        'Rezept-Zeile 1 - Sollwert berechnet auf 1 Stk (3,265kg)
        Assert.AreEqual(1.675D, Math.Round(DirectCast(Rc.Ingredients(1), ob_RecipeIngredient).Amount, 5))
        'Rezept-Zeile 1 - Einheit (kg)
        Assert.AreEqual(wb_Global.obEinheitKilogramm, DirectCast(Rc.Ingredients(1), ob_RecipeIngredient).Unit)
        'Rezept-Zeile 1 - Size (bei Unterartikeln immer NULL)
        Assert.AreEqual("NULL", DirectCast(Rc.Ingredients(1), ob_RecipeIngredient).Size)


        'Rezept-im-Rezept Zeile 1.1
        Dim Rr As ob_RecipeIngredient = DirectCast(Rc.Ingredients(0), ob_RecipeIngredient).Ingredients(0)
        Assert.AreEqual("201003", Rr.ArticleNo)
        'Rezept-im-Rezept Zeile 1.1 - Sollwert berechnet auf 1,590kg)
        Assert.AreEqual(0.25D, Math.Round(Rr.Amount, 5))
        'Rezept-im-Rezept Zeile 1.1 - Einheit (St)
        Assert.AreEqual(wb_Global.obEinheitKilogramm, Rr.Unit)
        'Rezept-im-Rezept Zeile 1.1 - Size
        Assert.AreEqual("NULL", Rr.Size)
        'Rezept-Zeile 1.1 hat keine weiteren Rezept-im-Rezept-Verknüpfungen
        Assert.AreEqual(Nothing, Rr.Ingredients)
        'Rezept-Zeile 1.1 hat keine weiteren Rezept-im-Rezept-Verknüpfungen - ProductionArticle=False
        Assert.AreEqual(False, Rr.ProductionArticle)
        'Rezept-Zeile 1.1 hat keine weiteren Rezept-im-Rezept-Verknüpfungen - RecipeType=NoRercipe(0)
        Assert.AreEqual(wb_Global.RecipeTypeNoRecipe, Rr.RecipeType)


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

    <TestMethod()> Public Sub TestArticleUsage()
        Dim Ri As New ob_RecipeProvider
        Dim ra() As Signum.OrgaSoft.Services.IArticle

        'Liste aller Artikel die den Rohstoff mit der Nummer "201010" (Hefe trocken) enthalten
        ra = Ri.GetArticleUsage("201010", wb_Global.obEinheitStk, 0, "NULL")
        'Result - 2 Artikel (4 Treffer, wenn die Dubletten nicht gefiltert werden)
        Assert.AreEqual(2, ra.Length)

        'Liste aller Artikel die den Rohstoff mit der Nummer "200000" (Schüttwasser) enthalten
        ra = Ri.GetArticleUsage("200000", wb_Global.obEinheitStk, 0, "NULL")
        'Result - 2 Artikel (4 Treffer, wenn die Dubletten nicht gefiltert werden)
        Assert.AreEqual(23, ra.Length)
    End Sub
End Class