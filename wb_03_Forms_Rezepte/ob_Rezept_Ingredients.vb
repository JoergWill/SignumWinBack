Imports System.Reflection
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.Services
Imports WinBack

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
        'in wb_Main registrieren
        wb_Main_Shared.RegisterAddIn("ob_RecipeProvider")
        'siehe Mail vom 13.Juli 2017 J.Erhardt - laden der dll schläg fehl 
        'AssemblyResolve wird definiert in WinBackAddIn.Erweiterte Kompilierungsoptionen
        If wb_Global.AssemblyResolve Then
            'Die eigenen dll-Files in sep. Verzeichnis verlagern
            AddHandler System.AppDomain.CurrentDomain.AssemblyResolve, AddressOf MyAssemblyResolve
        End If

        ' Diese Klasse als (benannten) Service registrieren
        ServiceProvider.AddService(GetType(IRecipeProvider), Me.ServiceName, Me)
        ' Den ServiceProvider so konfigurieren, dass er für diesen Service auf diese Implementierung zurückgreifen soll
        ServiceProvider.ConfigureService(GetType(IRecipeProvider), Me.ServiceName)
    End Sub

    Private Function MyAssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
        Return wb_Main_Shared.MyAssemblyResolve(sender, args, GetType(ob_RecipeProvider).Assembly)
    End Function

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
        Debug.Print("GetRecipe " & ArticleNo)
        RecipeInfo = New ob_RecipeInfo(ArticleNo, Unit, Size, Version, Branch)
        Return RecipeInfo
    End Function

    '''' <summary>
    '''' Liefert alle Artikel(mit Verweisen auf Rezepturen) zurück, in denen der übergebene Artikel enthalten ist
    '''' </summary>
    '''' <param name="ArticleNo">Nummer des Artikels, der Bestandteil zurückgegebener Rezepturen sein muss</param>
    '''' <param name="Unit">Einheit des Artikels, der Bestandteil zurückgegebener Rezepturen sein muss</param>
    '''' <param name="Color">Farbe des Artikels, der Bestandteil zurückgegebener Rezepturen sein muss</param>
    '''' <param name="Size">Grösse des Artikels, der Bestandteil zurückgegebener Rezepturen sein muss</param>
    '''' <returns></returns>
    Public Function GetArticleUsage(ArticleNo As String, Unit As Short, Color As Short, Size As String) As IArticle() Implements IRecipeProvider.GetArticleUsage
        Debug.Print("GetArticleUsage " & ArticleNo)
        'ArrayList initialisieren
        Dim ArticleUsage As New List(Of ob_ArticleUsage)

        'Interne Komponenten-Nummer ermitteln (nur Rohstoff)
        Dim KO_Numbers As List(Of Integer) = wb_sql_Functions.getKONrFromAlNum(ArticleNo)

        'Prüfen ob ein Artikel zu dieser Nummer existiert - Sonst wird ein leeres Array zurückgeben
        For Each KO_Nr In KO_Numbers
            'Ermittelt alle Artikel/Rohstoff-Nummern aus Rezepturen zum angegeben Rohstoff
            ReadArticleUsage(KO_Nr, ArticleUsage)
        Next

        'Liste nach Artikelnummer sortieren
        ArticleUsage.Sort()
        'Doppelte Einträge entfernen
        Dim i As Integer = ArticleUsage.Count - 1
        While i > 0
            If ArticleUsage(i).ArticleNo = ArticleUsage(i - 1).ArticleNo Then
                ArticleUsage.RemoveAt(i)
            End If
            i -= 1
        End While
        'Als array zurückgeben
        Return ArticleUsage.ToArray()
    End Function

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S3385:""Exit"" statements should not be used", Justification:="<Ausstehend>")>
    Private Sub ReadArticleUsage(KO_Nr As Integer, ByRef ArticleUsage As List(Of ob_ArticleUsage))
        'bei rekursiven Aufrufen wird geprüft, ob der Rohstoff schon in der Liste steht
        For Each x As ob_ArticleUsage In ArticleUsage
            ' Prüfe alle Einträge in der Liste mit Ausnahme des letzten Eintrags (hier steht schon der gesuchte Rohstoff drin)
            If x.KO_Nr = KO_Nr AndAlso x IsNot ArticleUsage.Last() Then
                Exit Sub
            End If
        Next

        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Abfrage aller Artikel/Rohstoffe mit Rezeptur, die diese Komponente enthalten
        Dim Sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlArtikelUse, KO_Nr.ToString)
        If winback.sqlSelect(Sql) Then
            'Alle Datensätze aus dem Result-Set durchlaufen
            While winback.Read
                'Dieser Artikel/Rohstoff(Rezept-im-Rezept) enthält den Rohstoff in seiner Rezeptur
                Dim Article As New ob_ArticleUsage
                Article.ArticleNo = winback.sField("KO_Nr_AlNum")
                Article.KO_Nr = winback.iField("KO_Nr")
                Article.KO_Type = wb_Functions.IntToKomponType(winback.iField("KO_Type"))

                'zum Array hinzufügen
                ArticleUsage.Add(Article)
                'Ist der resultierende Artikel wieder ein Rohstoff (Rezept-Im-Rezept) wird die Unter-Rezeptur aufgelöst
                If Article.KO_Type <> wb_Global.KomponTypen.KO_TYPE_ARTIKEL Then
                    'rekursiver Aufruf
                    ReadArticleUsage(Article.KO_Nr, ArticleUsage)
                End If
            End While
        End If
        'Datenbank-Verbindung wieder freigeben
        winback.Close()
    End Sub

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

    Private _Color As Short = wb_Global.obDEFAULTCOLOR                  'Farbe ist immer 0
    Private _Size As String = wb_Global.obDEFAULTSIZE                   'Größe ist immer Null
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
        If Not Komponente.xMySQLdbRead(0, ArticleNo) Then
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
                'Rezeptur einlesen (Der Backverlust ist hier eigentlich nicht relevant)
                'TODO Sauerteig-Rezepte (Variante 0) und kein Rezept abfangen !!
                Dim Rz As New wb_Rezept(RzNr, Nothing, Komponente.Backverlust, Variante, "", "", True, False)
                Debug.Print("Rezeptur " & Rz.RezeptNummer & "/" & Rz.RezeptBezeichnung & " gefunden für Artikel/Komponente " & ArticleNo)

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
            'If Not IsNothing(_Ingredients) Then
            '    Debug.Print("Ingredients")
            '    For Each x As ob_RecipeIngredient In _Ingredients
            '        Debug.Print(" Ingredient/Amount " & x.ArticleNo & "/" & x.Amount)
            '    Next
            'Else
            '    Debug.Print("Ingredients is Nothing")
            'End If
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
    Private _Color As Short = wb_Global.obDEFAULTCOLOR
    Private _Size As String = wb_Global.obDEFAULTSIZE
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
#Region "ob_ArticleUsage"
''' <summary>
''' Klasse, die eine Rezeptur mit ihren Bestandteilen beschreibt
''' </summary>
Public Class ob_ArticleUsage
    Implements IArticle
    Implements IComparable

    Private _ArticleNo As String
    Private _Unit As Short = wb_Global.obEinheitStk
    Private _Color As Short = wb_Global.obDEFAULTCOLOR
    Private _Size As String = wb_Global.obDEFAULTSIZE
    Private _KO_Nr As Integer
    Private _KO_Type As wb_Global.KomponTypen

    Public Sub New()
        Me.ArticleNo = wb_Global.UNDEFINED
    End Sub

    Public Sub New(ArticleNo As String, Unit As Short, Color As Short, Size As String)
        Me.ArticleNo = ArticleNo
        Me.Unit = Unit
        Me.Size = Size
    End Sub

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        Return String.Compare(ArticleNo, DirectCast(obj, ob_ArticleUsage).ArticleNo)
    End Function

    Public Property ArticleNo As String Implements IArticle.ArticleNo
        Get
            Return _ArticleNo
        End Get
        Set(value As String)
            _ArticleNo = value
        End Set
    End Property

    Public Property Unit As Short Implements IArticle.Unit
        Get
            Return _Unit
        End Get
        Set(value As Short)
            _Unit = value
        End Set
    End Property
    Public Property Color As Short Implements IArticle.Color
        Get
            Return _Color
        End Get
        Set(value As Short)
            value = _Color
        End Set
    End Property

    Public Property Size As String Implements IArticle.Size
        Get
            Return _Size
        End Get
        Set(value As String)
            _Size = value
        End Set
    End Property

    Public Property KO_Nr As Integer
        Get
            Return _KO_Nr
        End Get
        Set(value As Integer)
            _KO_Nr = value
        End Set
    End Property

    Public Property KO_Type As wb_Global.KomponTypen
        Get
            Return _KO_Type
        End Get
        Set(value As wb_Global.KomponTypen)
            _KO_Type = value
        End Set
    End Property
End Class
#End Region
