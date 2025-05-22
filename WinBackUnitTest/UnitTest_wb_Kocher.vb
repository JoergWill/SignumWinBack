Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_Kocher

    'Im "normalen" Testablauf werden die Kocher-Funktionen nicht getestet
    Public TestAktiv As Boolean = False


    <TestMethod()> Public Sub Test_CheckFTP()
        If TestAktiv Then
            Dim K As New wb_Kocher_Sync
            'Dim i As Integer = 1
            'i = K.CheckKocher(i)
            'Assert.AreEqual(2, i)
            'Assert.AreEqual(wb_Global.VerbindungsStatus.ERR, DirectCast(wb_Kocher_Global.KocherListe(1), wb_Kocher).VerbindungsStatus)

            Dim i As Integer = 2
            wb_GlobalSettings.IPBasisAdresse = "172.16.17"
            i = K.CheckKocher(i)
            Assert.AreEqual(0, i)

            i = K.CheckKocher(i)
            Assert.AreEqual(1, i)
            Assert.AreEqual(wb_Global.Kocher_VerbindungsStatus.OK, DirectCast(wb_Kocher_Global.KocherListe(2), wb_Kocher).VerbindungsStatus)

            i = K.CheckKocher(i)
            Assert.AreEqual(2, i)
            Assert.AreEqual(wb_Global.Kocher_VerbindungsStatus.OK, DirectCast(wb_Kocher_Global.KocherListe(1), wb_Kocher).VerbindungsStatus)

            i = K.CheckKocher(0)
            Assert.AreEqual(1, i)
        End If
    End Sub

    <TestMethod()> Public Sub Test_Rezept_WinBackRzNummer()
        If TestAktiv Then
            Dim r As New wb_Kocher_Rezept

            'Rezeptnummer setzen
            r.Nummer = 10
            Assert.AreEqual("KCHR10", r.WinBackRzNummer)
            r.Nummer = wb_Global.UNDEFINED
            r.WinBackRzNummer = "KCHR11"
            Assert.AreEqual(11, r.Nummer)
        End If
    End Sub

    <TestMethod()> Public Sub Test_RezeptSchrittIndex()
        If TestAktiv Then
            Dim rs As New wb_Kocher_RezeptSchritt

            'Index setzen - Parameter und Schritt-Nummer wird berechnet
            rs.Index = 1
            Assert.AreEqual(0, rs.SchrittNr)
            Assert.AreEqual(9, rs.ParamNr)
            Assert.AreEqual(1, rs.Index)

            'Index setzen - Parameter und Schritt-Nummer wird berechnet
            rs.Index = 2
            Assert.AreEqual(0, rs.SchrittNr)
            Assert.AreEqual(10, rs.ParamNr)
            Assert.AreEqual(2, rs.Index)

            'Index setzen - Parameter und Schritt-Nummer wird berechnet
            rs.Index = 3
            Assert.AreEqual(1, rs.SchrittNr)
            Assert.AreEqual(1, rs.ParamNr)
            Assert.AreEqual(3, rs.Index)

            'Index setzen - Parameter und Schritt-Nummer wird berechnet
            rs.Index = 13
            Assert.AreEqual(2, rs.SchrittNr)
            Assert.AreEqual(1, rs.ParamNr)
            Assert.AreEqual(13, rs.Index)

            'Index setzen - Parameter und Schritt-Nummer wird berechnet
            rs.Index = 23
            Assert.AreEqual(3, rs.SchrittNr)
            Assert.AreEqual(1, rs.ParamNr)
            Assert.AreEqual(23, rs.Index)

            'Index setzen - Parameter und Schritt-Nummer wird berechnet
            rs.Index = 11
            Assert.AreEqual(1, rs.SchrittNr)
            Assert.AreEqual(9, rs.ParamNr)

            'Index setzen - Parameter und Schritt-Nummer wird berechnet
            rs.Index = 12
            Assert.AreEqual(1, rs.SchrittNr)
            Assert.AreEqual(10, rs.ParamNr)

            'Index setzen - Parameter und Schritt-Nummer wird berechnet
            rs.Index = 39
            Assert.AreEqual(4, rs.SchrittNr)
            Assert.AreEqual(7, rs.ParamNr)
        End If
    End Sub

    <TestMethod()> Public Sub Test_TxtReadRezept()
        If TestAktiv Then
            Dim k As New wb_Kocher_Rezept

            k.Nummer = 2
            k.TxtReadRezept()
            k.Filename = "R20.0.rzp"
            k.TxtWriteRezept()
            k.TxtWriteRezept()
        End If
    End Sub

    <TestMethod()> Public Sub Test_DBUpdateRezept()
        If TestAktiv Then
            Dim k As New wb_Kocher_Rezept

            k.Nummer = 2
            k.TxtReadRezept()
            k.Filename = "R20.0.rzp"
            k.DBUpdateRezept(False)
        End If
    End Sub
End Class