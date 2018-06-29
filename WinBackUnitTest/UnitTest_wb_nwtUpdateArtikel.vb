Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_nwtUpdateArtikel

    <TestMethod()> Public Sub Test_VerwendungImRezept()
        'Klasse unter Test
        Dim nwt As New wb_nwtUpdateAtikel()
        'Anzahl der Rezeptschritte, die Komponente 3305 (Weizenmehl 550 Hand) enthalten.
        'In der Test-DB sind 4 Rezeptschritte mit 3305 verknüpft
        Dim wz550 As Integer = nwt.getRezepteZumRohstoff(3305)
        Assert.AreEqual(3, wz550)

        '3 Einträge sortiert nach interner Rezeptnummer
        Assert.AreEqual(15, nwt.ListeRezeptNr(0))
        Assert.AreEqual(43, nwt.ListeRezeptNr(1))
        Assert.AreEqual(44, nwt.ListeRezeptNr(2))

        'Rezept(15) 1200-Gewürzkuchen
        nwt.reCalcRezept(15)
        'TODO Berechnung der Nährwerte über Produktions-Stufen funktioniert nicht !!!
        'Assert.AreEqual(227.17, nwt.kt301.Naehrwert(wb_Global.T301_Kilokalorien))
        'Rezept(43) 600-Mürbeteig weiß
        nwt.reCalcRezept(43)
        'Assert.AreEqual(290.09, nwt.kt301.Naehrwert(wb_Global.T301_Kilokalorien))
        'Rezept(44) 601-Mürbeteig dunkel
        nwt.reCalcRezept(44)
        'Assert.AreEqual(280.0, nwt.kt301.Naehrwert(wb_Global.T301_Kilokalorien))


        'alle 3 Rezepte neu berechnen
        nwt.reCalcRezeptListe()

    End Sub

End Class