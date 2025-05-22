Public Class wb_LinienVirtuell
    Dim Produktion As New wb_Produktion_virtuell

    Public Sub New(Linie As Integer, Variante As Integer, Tageswechsel As Boolean)
        'ProduktionsDaten für diese Linie einlesen
        If ReadProduktionWinBack(Linie, Variante) Then
            'Produktion starten 
            StartProduktionWinBack(Linie, Variante, Tageswechsel)
        End If
    End Sub

    Private Function ReadProduktionWinBack(ProdLinie As Integer, ProdVariante As Integer) As Boolean
        'alle alten Einträge löschen
        Produktion.RootProduktionsSchritt.ChildSteps.Clear()
        'Daten aus WinBack ArbRezepte und ArbRZSchritte einlesen
        Return Produktion.MySQLdbSelect_ArbRzSchritte(ProdLinie, ProdVariante)
    End Function

    Private Sub StartProduktionWinBack(ProdLinie As Integer, ProdVariante As Integer, Tageswechsel As Boolean)
        'Produktion durchlaufen (virtuelle Linie)
        Produktion.VirtProduktion(ProdLinie, ProdVariante, Tageswechsel)
    End Sub

End Class
