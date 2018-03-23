Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Services

#Region "ob_RecipeProvider"
Public Class ob_RecipeProvider
    Implements IRecipeProvider
    Dim RecipeInfo As ob_RecipeInfo

    Public ReadOnly Property ServiceName As String Implements IOrgasoftService.ServiceName
        Get
            Return "@WinBackRezepturSchnittstelle"
        End Get
    End Property

    Public Function GetRecipe(ArticleNo As String, Unit As Short, Color As Short, Size As String, Version As Short, Branch As Short) As IRecipeInfo Implements IRecipeProvider.GetRecipe

        RecipeInfo = New ob_RecipeInfo(ArticleNo, Version, Size, Branch)
        Return RecipeInfo
    End Function
End Class
#End Region
#Region "ob_RecipeInfo"
Public Class ob_RecipeInfo
    Implements IRecipeInfo
    Private _ArticleNo As String
    Private _Branch As Short
    Private _Color As Short
    Private _Ingredients As IList
    Private _ProductionArticle As Boolean
    Private _RecipeType As Short
    Private _Size As String
    Private _Unit As Short
    Private _Variable As Boolean
    Private _Version As Short

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ArticleNo">ArtikelNr der Rezeptur, die aufgelöst werden soll</param>
    ''' <param name="Version"></param>
    ''' <param name="Size"></param>
    ''' <param name="Branch"></param>
    Public Sub New(ArticleNo As String, Version As Short, Size As String, Branch As Short)
        'Parameter
        Me.ArticleNo = ArticleNo
        Me.Version = Version
        Me.Size = Size
        Me.Branch = Branch

        'Komponenten-Stammdaten    
        Dim Komponente As New wb_Komponente
        'Komponenten-Stammdaten (Alphanumerische Komponenten-Nummer) lesen
        If Not Komponente.MySQLdbRead(0, ArticleNo) Then
            Debug.Print("Komponente in WinBack nicht vorhanden " & ArticleNo)
            'Liste aller Rezeptbestandteile ist leer
            _Ingredients = Nothing
        Else
            'Rezeptnummer aus Komponenten-Stammdaten (Alphanumerische Komponenten-Nummer)
            Dim RzNr = Komponente.RzNr
            If RzNr <= 0 Then
                Debug.Print("Keine Rezeptur mit Komponente verknüpft " & ArticleNo)
                'Liste aller Rezeptbestandteile ist leer
                _Ingredients = Nothing
            Else
                'Rezeptur einlesen
                Dim Rz As New wb_Rezept(RzNr, Nothing, Variante)
                'alle Child-Rezeptschritte aus dem Root-Rezeptschritt
                _Ingredients = Rz.RootRezeptSchritt.Ingredients
            End If
        End If
    End Sub

    ''' <summary>
    ''' ArtikelNr der Rezeptur, die aufgelöst werden soll
    ''' </summary>
    ''' <returns></returns>
    Public Property ArticleNo As String Implements IRecipeInfo.ArticleNo
        Get
            Return _ArticleNo
        End Get
        Set(value As String)
            _ArticleNo = value
        End Set
    End Property

    ''' <summary>
    ''' Filiale, für die die Rezeptur aufgelöst werden soll.
    ''' Wird in WinBack nicht unterstützt.
    ''' </summary>
    ''' <returns></returns>
    Public Property Branch As Short Implements IRecipeInfo.Branch
        Get
            Return _Branch
        End Get
        Set(value As Short)
            _Branch = value
        End Set
    End Property

    ''' <summary>
    ''' Farbe der Rezeptur, die aufgelöst werden soll
    ''' Wird in WinBack nicht unterstützt.
    ''' </summary>
    ''' <returns></returns>
    Public Property Color As Short Implements IRecipeInfo.Color
        Get
            Return _Color
        End Get
        Set(value As Short)
            _Color = value
        End Set
    End Property

    ''' <summary>
    ''' Liste der Bestandteile dieser Rezeptur (als IList(of IRecipeIngredient))
    ''' </summary>
    Public Property Ingredients As IList Implements IRecipeInfo.Ingredients
        Get
            Return _Ingredients
        End Get
        Set(value As IList)
            _Ingredients = value
        End Set
    End Property

    ''' <summary>
    ''' Art der Rezeptur
    '''     1=normale Rezeptur
    '''     3=Produktionsrezeptur
    '''     4=variable Rezeptur
    '''     5=variable Produktionsrezeptur
    '''     6=Pauschale
    ''' </summary>
    Public Property RecipeType As Short Implements IRecipeInfo.RecipeType
        Get
            Return _RecipeType
        End Get
        Set(value As Short)
            _RecipeType = value
        End Set
    End Property

    Public Property ProductionArticle As Boolean Implements IRecipeInfo.ProductionArticle
        Get
            Return (RecipeType = 3) Or (RecipeType = 5)
        End Get
        Set(value As Boolean)
            Select Case value
                Case False
                    Select Case RecipeType
                        Case 4
                            RecipeType = 1
                        Case 5
                            RecipeType = 3
                    End Select

                Case True
                    Select Case RecipeType
                        Case 1
                            RecipeType = 4
                        Case 3
                            RecipeType = 5
                    End Select
            End Select
        End Set
    End Property

    Public Property Variable As Boolean Implements IRecipeInfo.Variable
        Get
            Return (RecipeType = 4) Or (RecipeType = 5)
        End Get
        Set(value As Boolean)
            Select Case value
                Case False
                    Select Case RecipeType
                        Case 3
                            RecipeType = 1
                        Case 5
                            RecipeType = 4
                    End Select

                Case True
                    Select Case RecipeType
                        Case 1
                            RecipeType = 3
                        Case 4
                            RecipeType = 5
                    End Select
            End Select
        End Set
    End Property

    ''' <summary>
    ''' Grösse der Rezeptur, die aufgelöst werden soll
    ''' </summary>
    ''' <returns></returns>
    Public Property Size As String Implements IRecipeInfo.Size
        Get
            Return _Size
        End Get
        Set(value As String)
            _Size = value
        End Set
    End Property

    ''' <summary>
    ''' Einheit der Rezeptur, die aufgelöst werden soll
    ''' </summary>
    ''' <returns></returns>
    Public Property Unit As Short Implements IRecipeInfo.Unit
        Get
            Return _Unit
        End Get
        Set(value As Short)
            _Unit = value
        End Set
    End Property

    ''' <summary>
    ''' Variante der Rezeptur, die gelesen wurde
    ''' </summary>
    Public Property Version As Short Implements IRecipeInfo.Version
        Get
            Return _Version
        End Get
        Set(value As Short)
            _Version = value
        End Set
    End Property

    Public Property Variante As Short
        Get
            Return _Version + 1
        End Get
        Set(value As Short)
            _Version = value - 1
        End Set
    End Property
End Class
#End Region
#Region "IRecipeIngredient"
Public Class ob_RecipeIngredient
    Implements IRecipeIngredient

    Private _ArticleNo As String
    Private _Branch As Short
    Private _Color As Short
    Private _ProductionArticle As Boolean
    Private _RecipeType As Short
    Private _Size As String
    Private _Unit As Short
    Private _Variable As Boolean
    Private _Version As Short
    Private _Amount As Decimal
    Private _LossPercentage As Decimal
    Private _Ingredients As IList

    Public Property ArticleNo As String Implements IRecipeIngredient.ArticleNo
        Get
            Return _ArticleNo
        End Get
        Set(value As String)
            _ArticleNo = value
        End Set
    End Property

    Public Property Unit As Short Implements IRecipeIngredient.Unit
        Get
            'Rezeptschritte in Winback immer in Gramm
            Return wb_Global.EinheitGramm
        End Get
        Set(value As Short)
            _Unit = value
        End Set
    End Property

    Public Property Color As Short Implements IRecipeIngredient.Color
        Get
            'Keine Farbe
            Return 0
        End Get
        Set(value As Short)
            _Color = value
        End Set
    End Property

    Public Property Size As String Implements IRecipeIngredient.Size
        Get
            'Rezeptschritte haben immer Faktor Eins
            Return 1
        End Get
        Set(value As String)
            _Size = value
        End Set
    End Property

    Public Property Version As Short Implements IRecipeIngredient.Version
        Get
            Return _Version
        End Get
        Set(value As Short)
            _Version = value
        End Set
    End Property

    Public Property RecipeType As Short Implements IRecipeIngredient.RecipeType
        Get
            Return 1
        End Get
        Set(value As Short)
            _RecipeType = value
        End Set
    End Property

    Public Property ProductionArticle As Boolean Implements IRecipeIngredient.ProductionArticle
        Get
            'TODO JErhardt nach der Bedeutung fragen
            Return False
        End Get
        Set(value As Boolean)
            _ProductionArticle = value
        End Set
    End Property

    Public Property Variable As Boolean Implements IRecipeIngredient.Variable
        Get
            'TODO JErhardt nach der Bedeutung fragen
            Return False
        End Get
        Set(value As Boolean)
            _Variable = value
        End Set
    End Property

    Public Property Amount As Decimal Implements IRecipeIngredient.Amount
        Get
            Return _Amount
        End Get
        Set(value As Decimal)
            _Amount = value
        End Set
    End Property

    Public Property LossPercentage As Decimal Implements IRecipeIngredient.LossPercentage
        Get
            'TODO Backverlust ermitteln !!
            Return _LossPercentage
        End Get
        Set(value As Decimal)
            _LossPercentage = value
        End Set
    End Property

    Public Property Ingredients As IList Implements IRecipeIngredient.Ingredients
        Get
            Return _Ingredients
        End Get
        Set(value As IList)
            _Ingredients = value
        End Set
    End Property
End Class
#End Region