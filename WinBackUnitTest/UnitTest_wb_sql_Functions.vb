Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_sql_Functions

    ''' <summary>
    ''' Initialisiert die globalen Einstellungen.
    ''' </summary>
    <TestInitialize>
    Sub TestInitialize()
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()>
    Public Sub Test_MySQLDateTime()
        Dim x As String
        x = wb_sql_Functions.MySQLdatetime(#1964/11/22 12:10:20#)
        Assert.AreEqual("1964-11-22 12:10:20", x)
    End Sub

    <TestMethod()>
    Public Sub Test_GetKomponParams()
        Dim s As String
        Dim d As Double
        Dim i As Integer

        s = wb_sql_Functions.getKomponParam(211, 1, "0")
        Assert.AreEqual("10", s)
        d = wb_sql_Functions.getKomponParam(211, 1, 20.0)
        Assert.AreEqual(10.0, d)
        i = wb_sql_Functions.getKomponParam(211, 1, 20)
        Assert.AreEqual(10, i)
        i = wb_sql_Functions.getKomponParam(600, 7, wb_Global.TA_Null)
        Assert.AreEqual(100, i)

    End Sub

    <TestMethod()>
    Public Sub Test_WinBackTableNames()
        Assert.IsTrue(wb_sql_Functions.MySQLTableExist("Rezepte"))
        Assert.IsFalse(wb_sql_Functions.MySQLTableExist("XXX"))
    End Sub

    <TestMethod()>
    Public Sub Test_EinheitenUmrechnung()
        'Umrechnung Artikel-Nr.201002 Zucker Sack in kg
        Assert.AreEqual(wb_sql_Functions.EinheitenUmrechnung("201002", 26, 10), 25.0)
        'Umrechnung Artikel-Nr.000000 (Artikel nicht vorhanden)
        Assert.AreEqual(wb_sql_Functions.EinheitenUmrechnung("000000", 26, 10), 0.0)
    End Sub

    <TestMethod()>
    Public Sub Test_Version()
        Assert.IsTrue(wb_sql_Functions.GetMySqlVersion.Contains("5.") Or wb_sql_Functions.GetMySqlVersion.Contains("3."))
    End Sub


End Class