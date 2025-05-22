Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack


<TestClass()> Public Class UnitTest_wb_Rezeptschritt

    <TestMethod()> Public Sub Test_Sollwert()
        Dim rs As New wb_Rezeptschritt(Nothing, "Test")

        'Sollwert mit Dezimalkomma
        rs.Sollwert = "2,567"
        Assert.AreEqual(rs.fSollwert, 2.567)

        'Sollwert mit Dezimalkomma
        rs.Sollwert = "2.5"
        Assert.AreEqual(rs.fSollwert, 2.5)

        'Sollwert als String
        rs.Sollwert = "ProduktionsStufe"
        Assert.AreEqual(rs.fSollwert, 0.0)

        'Test_Sollwert float schreiben
        rs.fSollwert = 12.3
        Assert.AreEqual(rs.Sollwert, "12,300")


    End Sub

End Class