Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.Services

''' <summary>
''' Klasse zum Auflösen von Rezepturen bzw. Stücklisten
''' </summary>
#Region "ob_RecipeProvider"
<Export(GetType(IExtension))>
<ExportMetadata("Description", "Service Rezeptauflösung und Stückliste WinBack")>
Public Class ob_RecipeProvider
    Implements IExtension
    Implements IRecipeProvider

    Dim RecipeInfo As ob_RecipeInfo

    Public Property InfoContainer As IInfoContainer Implements IExtension.InfoContainer
    Public Property ServiceProvider As IOrgasoftServiceProvider Implements IExtension.ServiceProvider

    Public ReadOnly Property ServiceName As String Implements IOrgasoftService.ServiceName
        Get
            Return "@WinBackRezepturSchnittstelle"
        End Get
    End Property

    Public Sub Initialize() Implements IExtension.Initialize

        ' Diese Klasse als (benannten) Service registrieren
        ServiceProvider.AddService(GetType(IRecipeProvider), Me.ServiceName, Me)
        ' Den ServiceProvider so konfigurieren, dass er für diesen Service auf diese Implementierung zurückgreifen soll
        ServiceProvider.ConfigureService(GetType(IRecipeProvider), Me.ServiceName)
    End Sub

    ''' <summary>
    ''' Ermittelt die Rezeptur und liefert die Bestandteile zurück. Die Rezeptgröße wird dabei immer auf
    ''' ein Stück (Artikel-Nassgewicht) berechnet.
    ''' </summary>
    ''' <param name="ArticleNo">ArtikelNummer der Rezeptur, die aufgelöst werden soll</param>
    ''' <param name="Unit">Einheit der Rezeptur, die aufgelöst werden soll. Ist immer in kg(11)</param>
    ''' <param name="Color">Farbe, für WinBack nicht relevant. Ist immer 0</param>
    ''' <param name="Size">Grösse, für WinBack nicht relevant. Ist immer Null</param>
    ''' <param name="Version">Entspricht der Variante der Rezeptur, die gelesen werden soll. 0: Filial- bzw. Haupt-Variante</param>
    ''' <param name="Branch">Filiale, für die die Rezeptur aufgelöst werden soll. Bei Werten > 0 werden evtl. vorhandene filialspezifische Varianten berücksichtigt</param>
    ''' <returns></returns>
    Public Function GetRecipe(ArticleNo As String, Unit As Short, Color As Short, Size As String, Version As Short, Branch As Short) As IRecipeInfo Implements IRecipeProvider.GetRecipe
        RecipeInfo = New ob_RecipeInfo(ArticleNo, Unit, Size, Version, Branch)
        Return RecipeInfo
    End Function
End Class
#End Region
#Region "ob_RecipeInfo"
''' <summary>
''' Klasse, die eine Rezeptur mit ihren Bestandteilen beschreibt
''' </summary>
Public Class ob_RecipeInfo
    Implements IRecipeInfo

    Private _ArticleNo As String
    Private _Version As Short
    Private _Branch As Short
    Private _Ingredients As IList

    Private _Color As Short = 0                                         'Farbe ist immer 0
    Private _Size As String = "NULL"                                    'Größe ist immer Null
    Private _Unit As Short = wb_Global.obEinheitKilogramm               'Einheit ist immer kg(11)
    Private _RecipeType As Short = wb_Global.RecipeTypeProdVariabel     'Rezept-Type ist immer variable Produktionsrezeptur(5)
    Private _ProductionArticle As Boolean = True                        'Rezept-Type Produktion
    Private _Variable As Boolean = True                                 'Rezept-Type variabel

    ''' <summary>
    ''' Konstruktor. Liest die Artikel-Stammdaten aus der WinBack-Datenbank. Wenn eine Rezept mit dem Artikel
    ''' verknüpft ist, wird diese aus der WinBack-Datenbank gelesen.
    ''' </summary>
    ''' <param name="ArticleNo">ArtikelNummer der Rezeptur, die aufgelöst werden soll</param>
    ''' <param name="Version">Versions-Nummer entspricht der Variante in WinBack - 1</param>
    ''' <param name="Branch">Filiale, für die die Rezeptur aufgelöst werden soll. Bei Werten > 0 werden evtl. vorhandene filialspezifische Varianten berücksichtigt</param>
    Public Sub New(ArticleNo As String, Unit As Short, Size As String, Version As Short, Branch As Short)
        'Parameter
        Me.ArticleNo = ArticleNo
        Me.Version = Version
        Me.Branch = Branch
        Me.Unit = Unit
        Me.Size = Size

        'Komponenten-Stammdaten    
        Dim Komponente As New wb_Komponente
        'Komponenten-Stammdaten (Alphanumerische Komponenten-Nummer) lesen
        If Not Komponente.MySQLdbRead(0, ArticleNo) Then
            Debug.Print("Komponente in WinBack nicht vorhanden " & ArticleNo)
            'Liste aller Rezeptbestandteile ist leer
            _Ingredients = Nothing
            _ProductionArticle = False
            _RecipeType = wb_Global.RecipeTypeNoRecipe
        Else
            'Rezeptnummer aus Komponenten-Stammdaten (Alphanumerische Komponenten-Nummer)
            Dim RzNr = Komponente.RzNr
            'Stückgewicht(nass) aus Artikelstamm (Umrechnung in kg)
            Dim StkGewicht As Double = wb_Functions.StrToDouble(Komponente.ArtikelChargen.StkGewicht) / 1000

            If RzNr <= 0 Then
                Debug.Print("Keine Rezeptur mit Komponente verknüpft " & ArticleNo)
                'Liste aller Rezeptbestandteile ist leer
                _Ingredients = Nothing
                _ProductionArticle = False
                _RecipeType = wb_Global.RecipeTypeNoRecipe
            Else
                'Rezeptur einlesen
                'TODO Sauerteig-Rezepte (Variante 0) und kein Rezept abfangen !!
                Dim Rz As New wb_Rezept(RzNr, Nothing, Variante)

                'Liste aller Child-Rezeptschritte aus dem Root-Rezeptschritt berechnet auf das Stückgewicht(Nass)
                _Ingredients = Rz.RootRezeptSchritt.CalcIngredients(StkGewicht, Variante)
                _ProductionArticle = True
                _RecipeType = wb_Global.RecipeTypeProdVariabel
            End If
        End If
    End Sub

    ''' <summary>
    ''' ArtikelNummer der Rezeptur, die aufgelöst werden soll
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
    ''' Filiale, für die die Rezeptur aufgelöst werden soll. Bei Werten > 0 werden evtl. vorhandene filialspezifische Varianten berücksichtigt
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
    ''' Farbe. Wird in WinBack nicht verwendet. Ist immer = 0
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
    ''' Size. Wird in WinBack nicht verwendet. Ist immer Null
    ''' In der Kopfzeile wird der Wert aus dem Aufruf zurücgegeben
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
    ''' Version der Rezeptur, die gelesen wurde. Standard bei OrgaBack ist Variante 0
    ''' </summary>
    Public Property Version As Short Implements IRecipeInfo.Version
        Get
            Return _Version
        End Get
        Set(value As Short)
            _Version = value
        End Set
    End Property

    ''' <summary>
    ''' Variante WinBack. Entspricht der Version + 1.
    ''' Variante 0 wird zu Version 0 !! (Sauerteig)
    ''' </summary>
    ''' <returns></returns>
    Public Property Variante As Short
        Get
            Return _Version + 1
        End Get
        Set(value As Short)
            _Version = value - 1
            If _Version < 0 Then
                _Version = 0
            End If
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
    ''' Art der Rezeptur. In WinBack immer (5)variable Produktionsrezeptur
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

    ''' <summary>
    ''' Rezept-Type Produktion
    ''' </summary>
    ''' <returns></returns>
    Public Property ProductionArticle As Boolean Implements IRecipeInfo.ProductionArticle
        Get
            Return _ProductionArticle
        End Get
        Set(value As Boolean)
            _ProductionArticle = value
        End Set
    End Property

    ''' <summary>
    ''' Rezept-Type variabel
    ''' </summary>
    ''' <returns></returns>
    Public Property Variable As Boolean Implements IRecipeInfo.Variable
        Get
            Return _Variable
        End Get
        Set(value As Boolean)
            _Variable = value
        End Set
    End Property
End Class
#End Region
#Region "IRecipeIngredient"
''' <summary>
''' Klasse, die einen Rezeptur-Bestandteil beschreibt
''' </summary>
Public Class ob_RecipeIngredient
    Implements IRecipeIngredient

    Private _ArticleNo As String
    Private _Version As Short = 0
    Private _Amount As Decimal
    Private _Unit As Short = wb_Global.obEinheitKilogramm
    Private _LossPercentage As Decimal
    Private _Ingredients As IList

    Private _Branch As Short = 0
    Private _Color As Short = 0
    Private _Size As String = "NULL"
    Private _RecipeType As Short = wb_Global.RecipeTypeProdVariabel
    Private _ProductionArticle As Boolean = True
    Private _Variable As Boolean = True

    ''' <summary>
    ''' Rohstoff-Nummer der Rezept-Zeile
    ''' </summary>
    ''' <returns></returns>
    Public Property ArticleNo As String Implements IRecipeIngredient.ArticleNo
        Get
            Return _ArticleNo
        End Get
        Set(value As String)
            _ArticleNo = value
        End Set
    End Property

    ''' <summary>
    ''' Version der Rezeptur, die gelesen wurde. Standard bei OrgaBack ist Variante 0
    ''' </summary>
    ''' <returns></returns>
    Public Property Version As Short Implements IRecipeIngredient.Version
        Get
            Return _Version
        End Get
        Set(value As Short)
            _Version = value
        End Set
    End Property

    ''' <summary>
    ''' Rezept-Type variabel
    ''' </summary>
    ''' <returns></returns>
    Public Property Variante As Short
        Get
            Return _Version + 1
        End Get
        Set(value As Short)
            _Version = value - 1
            If _Version < 0 Then
                _Version = 0
            End If
        End Set
    End Property

    ''' <summary>
    ''' Amount. Sollwert der Rezept-Zeile in kg (Unit 11)
    ''' </summary>
    ''' <returns></returns>
    Public Property Amount As Decimal Implements IRecipeIngredient.Amount
        Get
            Return _Amount
        End Get
        Set(value As Decimal)
            _Amount = value
        End Set
    End Property

    ''' <summary>
    ''' Einheit der Rezept-Zeile. Wird immer in kg ausgegeben
    ''' </summary>
    ''' <returns></returns>
    Public Property Unit As Short Implements IRecipeIngredient.Unit
        Get
            Return _Unit
        End Get
        Set(value As Short)
            _Unit = value
        End Set
    End Property

    ''' <summary>
    ''' Back-/Zuschnittverlust der Rezeptzeile
    ''' </summary>
    ''' <returns></returns>
    Public Property LossPercentage As Decimal Implements IRecipeIngredient.LossPercentage
        Get
            'TODO Backverlust ermitteln !!
            Return _LossPercentage
        End Get
        Set(value As Decimal)
            _LossPercentage = value
        End Set
    End Property

    ''' <summary>
    ''' Liste aller Rezeptbestandteile eines verknüpften Rezeptes. (Rezept-im-Rezept)
    ''' </summary>
    ''' <returns></returns>
    Public Property Ingredients As IList Implements IRecipeIngredient.Ingredients
        Get
            Return _Ingredients
        End Get
        Set(value As IList)
            _Ingredients = value
        End Set
    End Property

    ''' <summary>
    ''' Farbe. Wird in WinBack nicht verwendet. Ist immer = 0
    ''' </summary>
    ''' <returns></returns>
    Public Property Color As Short Implements IRecipeIngredient.Color
        Get
            Return _Color
        End Get
        Set(value As Short)
            _Color = value
        End Set
    End Property

    ''' <summary>
    ''' Größe. Wird in WinBack nicht verwendet. Ist immer Null
    ''' </summary>
    ''' <returns></returns>
    Public Property Size As String Implements IRecipeIngredient.Size
        Get
            Return _Size
        End Get
        Set(value As String)
            _Size = value
        End Set
    End Property

    ''' <summary>
    ''' Art der Rezeptur. In WinBack immer (5)variable Produktionsrezeptur
    '''     0=keine Rezeptur !!
    '''     1=normale Rezeptur
    '''     3=Produktionsrezeptur
    '''     4=variable Rezeptur
    '''     5=variable Produktionsrezeptur
    '''     6=Pauschale
    ''' </summary>
    Public Property RecipeType As Short Implements IRecipeIngredient.RecipeType
        Get
            Return _RecipeType
        End Get
        Set(value As Short)
            _RecipeType = value
        End Set
    End Property

    ''' <summary>
    ''' Rezept-Type Produktion
    ''' </summary>
    ''' <returns></returns>
    Public Property ProductionArticle As Boolean Implements IRecipeIngredient.ProductionArticle
        Get
            Return _ProductionArticle
        End Get
        Set(value As Boolean)
            _ProductionArticle = value
        End Set
    End Property

    ''' <summary>
    ''' Rezept-Type variabel
    ''' </summary>
    ''' <returns></returns>
    Public Property Variable As Boolean Implements IRecipeIngredient.Variable
        Get
            Return _Variable
        End Get
        Set(value As Boolean)
            _Variable = value
        End Set
    End Property
End Class
#End Region