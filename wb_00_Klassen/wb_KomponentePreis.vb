Public Class wb_KomponentePreis

    ''' <summary>
    ''' Sonder-Konstruktion um den Preis für die Komponente zu ermitteln. Die Funktion
    ''' wird nur aufgerufen, wenn die Variante OrgaBack ist. In diesem Fall wird
    ''' der Preis über den Webservice ermittelt.
    ''' Diese Konstruktion ist notwendig, damit in OrgaBack-Office die Signum-dll nicht
    ''' geladen werden muss.
    ''' <param name="Nummer"></param>
    ''' <param name="KomponType"></param>
    ''' <returns></returns>
    ''' </summary>
    Public Shared Function GetArtikelPreis(Nummer As String, KomponType As wb_Global.KomponTypen) As Double
        Return ob_Artikel_Services.GetArtikelPreis(Nummer, KomponType)
    End Function

End Class
