Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Enthält dir Routinen für IArticleAservices")>
Public Class ob_Artikel_Services
    Implements IExtension
    Private Shared oArticle As IArticleService

    Public Property ServiceProvider As IOrgasoftServiceProvider Implements IExtension.ServiceProvider
    Public Property InfoContainer As IInfoContainer Implements IExtension.InfoContainer

    Public Sub Initialize() Implements IExtension.Initialize
        oArticle = TryCast(ServiceProvider.GetService(GetType(IArticleService)), IArticleService)
    End Sub

    Public Shared Function GetArtikelPreis(Nummer As String, KomponType As wb_Global.KomponTypen) As Double
        Const _Color As Short = 0                                         'Farbe ist immer 0
        Const _Size As String = "NULL"                                    'Größe ist immer Null

        If oArticle IsNot Nothing Then
            Select Case KomponType
                Case wb_Global.KomponTypen.KO_TYPE_ARTIKEL
                    Return oArticle.GetArticleCostPrice(Nummer, wb_Global.obEinheitStk, _Color, _Size)

                Case wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE
                    Return oArticle.GetArticleCostPrice(Nummer, wb_Global.obEinheitLiter, _Color, _Size)

                Case Else
                    Return oArticle.GetArticleCostPrice(Nummer, wb_Global.obEinheitKilogramm, _Color, _Size)
            End Select
        Else
            Return 0.0
        End If
    End Function

End Class
