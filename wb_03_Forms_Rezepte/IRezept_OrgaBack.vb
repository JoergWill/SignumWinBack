Imports System.Runtime.InteropServices
Imports Signum.OrgaSoft.Common

Namespace Services

    ''' <summary>
    ''' Interface zum Auflösen von Rezepturen bzw. Stücklisten
    ''' </summary>
    <ComImport()>
    <Guid("66B0307D-698B-4A16-965F-61C4745B7861")>
    Public Interface IRecipeProvider
        Inherits IOrgasoftService

        ''' <summary>
        ''' Ermittelt die Rezeptur und liefert die Bestandteile zurück
        ''' </summary>
        ''' <param name="ArticleNo">ArtikelNr der Rezeptur, die aufgelöst werden soll</param>
        ''' <param name="Unit">Einheit der Rezeptur, die aufgelöst werden soll</param>
        ''' <param name="Color">Farbe der Rezeptur, die aufgelöst werden soll</param>
        ''' <param name="Size">Grösse der Rezeptur, die aufgelöst werden soll</param>
        ''' <param name="Version">Variante der Rezeptur, die gelesen werden soll. 0: Filial- bzw. Haupt-Variante</param>
        ''' <param name="Branch">Filiale, für die die Rezeptur aufgelöst werden soll.
        ''' Bei Werten > 0 werden evtl. vorhandene filialspezifische Varianten berücksichtigt</param>
        ''' <returns></returns>
        Function GetRecipe(ArticleNo As String, Unit As Short, Color As Short, Size As String, Version As Short, Branch As Short) As IRecipeInfo

        ''' <summary>
        ''' Liefert alle Rezepturen zurück, in denen der übergebene Artikel enthalten ist
        ''' </summary>
        ''' <param name="ArticleNo">Nummer des Artikels, der Bestandteil zurückgegebener Rezepturen sein muss</param>
        ''' <param name="Unit">Einheit des Artikels, der Bestandteil zurückgegebener Rezepturen sein muss</param>
        ''' <param name="Color">Farbe des Artikels, der Bestandteil zurückgegebener Rezepturen sein muss</param>
        ''' <param name="Size">Grösse des Artikels, der Bestandteil zurückgegebener Rezepturen sein muss</param>
        ''' <returns></returns>
        Function GetArticleUsage(ArticleNo As String, Unit As Short, Color As Short, Size As String) As IArticle()

    End Interface


    ''' <summary>
    ''' Interface, das eine Rezeptur bzw. Stückliste mit ihren Bestandteilen beschreibt
    ''' </summary>
    <ComImport()>
    <Guid("79B74E92-C71C-4905-941F-10144DBB39E1")>
    Public Interface IRecipeInfo
        Property ArticleNo As String
        Property Unit As Short
        Property Color As Short
        Property Size As String

        ''' <summary>
        ''' Variante der Rezeptur, die gelesen wurde
        ''' </summary>
        ''' <returns></returns>
        Property Version As Short

        ''' <summary>
        ''' Filiale, für die die Rezeptur ermittelt wurde
        ''' </summary>
        Property Branch As Short

        ''' <summary>
        ''' Art der Rezeptur
        ''' -1/1=normale Rezeptur, 3=Produktionsrezeptur, 4=variable Rezeptur, 5=variable Produktionsrezeptur, 6=Pauschale
        ''' </summary>
        Property RecipeType As Short

        ''' <summary>
        ''' Gibt an, ob dies eine Produktions-Rezeptur ist (RecipeType 3 oder 5)
        ''' </summary>
        Property ProductionArticle As Boolean

        ''' <summary>
        ''' Gibt an, ob dies eine variable Rezeptur ist (RecipeType 4 oder 5)
        ''' </summary>
        Property Variable As Boolean

        ''' <summary>
        ''' Liste der Bestandteile dieser Rezeptur (als IList(of IRecipeIngredient))
        ''' </summary>
        Property Ingredients As IList

    End Interface

    ''' <summary>
    ''' Interface, das einen Rezeptur-Bestandteil beschreibt
    ''' </summary>
    <ComImport()>
    <Guid("3B82FC25-C592-4E72-B05D-EA8506F097F6")>
    Public Interface IRecipeIngredient
        Property ArticleNo As String
        Property Unit As Short
        Property Color As Short
        Property Size As String
        Property Version As Short

        ''' <summary>
        ''' Art der Rezeptur
        ''' -1/1=normale Rezeptur, 3=Produktionsrezeptur, 4=variable Rezeptur, 5=variable Produktionsrezeptur, 6=Pauschale
        ''' </summary>
        Property RecipeType As Short

        ''' <summary>
        ''' Gibt an, ob dies eine Produktions-Rezeptur ist (RecipeType 3 oder 5)
        ''' </summary>
        Property ProductionArticle As Boolean

        ''' <summary>
        ''' Gibt an, ob dies eine variable Rezeptur ist (RecipeType 4 oder 5)
        ''' </summary>
        Property Variable As Boolean

        ''' <summary>
        ''' Menge, die von diesem Artikel im (direkt) übergeordneten Artikel enthalten ist
        ''' </summary>
        Property Amount As Decimal

        ''' <summary>
        ''' Prozentualer Schwund, der beim Verarbeiten dieses Unter-Artikels zum übergeordneten Produkt anfällt.
        ''' </summary>
        Property LossPercentage As Decimal

        ''' <summary>
        ''' Liste der Bestandteile dieses Artikels (als IList(of IRecipeIngredient)), 
        ''' sofern dieser Artikel wiederum eine Rezeptur ist, andernfalls Nothing oder eine leere IList
        ''' </summary>
        Property Ingredients As IList

    End Interface

    ''' <summary>
    ''' Interface, das einen Handelsartikel beschreibt
    ''' </summary>
    <ComImport()>
    <Guid("DDE6FAD9-E240-415B-A8A9-4189440C7120")>
    Public Interface IArticle
        Property ArticleNo As String
        Property Unit As Short
        Property Color As Short
        Property Size As String
    End Interface

End Namespace
