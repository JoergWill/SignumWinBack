Imports System.Reflection
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Enthält dir Routinen für IArticleService")>
Public Class ob_Artikel_Services
    Implements IExtension
    Private Shared oArticle As IArticleService

    Public Property ServiceProvider As IOrgasoftServiceProvider Implements IExtension.ServiceProvider
    Public Property InfoContainer As IInfoContainer Implements IExtension.InfoContainer

    Public Sub Initialize() Implements IExtension.Initialize
        'siehe Mail vom 13.Juli 2017 J.Erhardt - laden der dll schläg fehl 
        AddHandler System.AppDomain.CurrentDomain.AssemblyResolve, AddressOf MyAssemblyResolve
        oArticle = TryCast(ServiceProvider.GetService(GetType(IArticleService)), IArticleService)
    End Sub

    Private Function MyAssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
        Return wb_Main_Shared.MyAssemblyResolve(sender, args, GetType(ob_Artikel_Services).Assembly)
    End Function

    Public Shared Function GetArtikelPreis(Nummer As String, KomponType As wb_Global.KomponTypen) As Double
        Dim Preis As Decimal
        If oArticle IsNot Nothing Then
            Select Case KomponType
                Case wb_Global.KomponTypen.KO_TYPE_ARTIKEL
                    'Artikel-Preis aus OrgaBack
                    Preis = oArticle.GetArticleCostPrice(Nummer, wb_Global.obEinheitStk, wb_Global.obDEFAULTCOLOR, wb_Global.obDEFAULTSIZE)
                Case wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE
                    'Rohstoff-Preis Wasser aus OrgaBack
                    Preis = oArticle.GetArticleCostPrice(Nummer, wb_Global.obEinheitLiter, wb_Global.obDEFAULTCOLOR, wb_Global.obDEFAULTSIZE)
                Case Else
                    'Rohstoff-Preis aus OrgaBack
                    Preis = oArticle.GetArticleCostPrice(Nummer, wb_Global.obEinheitKilogramm, wb_Global.obDEFAULTCOLOR, wb_Global.obDEFAULTSIZE)
            End Select
            Return Preis
        Else
            Return 0.0
        End If
    End Function

End Class
