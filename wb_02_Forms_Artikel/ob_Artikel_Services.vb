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
        oArticle = TryCast(ServiceProvider.GetService(GetType(IArticleService)), IArticleService)
    End Sub

    Public Shared Function GetArtikelPreis(Nummer As String, KomponType As wb_Global.KomponTypen) As Double
        Const _Color As Short = 0        'Farbe ist immer 0
        Const _Size As String = "NULL"   'Größe ist immer Null
        Dim Preis As Decimal

        If oArticle IsNot Nothing Then
            Select Case KomponType
                'Artikel-Preis aus OrgaBack
                Case wb_Global.KomponTypen.KO_TYPE_ARTIKEL
                    Preis = oArticle.GetArticleCostPrice(Nummer, wb_Global.obEinheitStk, _Color, _Size)
                'Rohstoff-Preis Wasser aus OrgaBack
                Case wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE
                    Preis = oArticle.GetArticleCostPrice(Nummer, wb_Global.obEinheitLiter, _Color, _Size)
                    'Rohstoff-Preis aus OrgaBack
                Case Else
                    Preis = oArticle.GetArticleCostPrice(Nummer, wb_Global.obEinheitKilogramm, _Color, _Size)
            End Select

            Debug.Print("GetArtikelPreis (Nummer/Preis) " & Nummer & " " & Preis)
            Return Preis
        Else
            Return 0.0
        End If
    End Function

End Class
