Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_sql_Functions
    <TestInitialize>
    Sub TestInitialize()
        'Unittest
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest

        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then
            'Datenbank Verbindung Einstellungen setzen
            '(Muss in wb_Konfig gesetzt werden, weil My.Setting hier nicht funktioniert)
            wb_GlobalSettings.WinBackDBType = wb_Sql.dbType.mySql
        End If
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
        Assert.AreEqual("10,000", s)
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
        Assert.AreEqual(wb_sql_Functions.EinheitenUmrechnung("150013", 26, 10), 25)
    End Sub



End Class