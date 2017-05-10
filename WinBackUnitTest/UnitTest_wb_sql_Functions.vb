Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_sql_Functions
    <TestInitialize>
    Sub TestInitialize()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then
            'Datenbank Verbindung Einstellungen setzen
            '(Muss in wb_Konfig gesetzt werden, weil My.Setting hier nicht funktioniert)
            wb_Konfig.SqlSetting("MySQL")
        End If
    End Sub

    <TestMethod()>
    Public Sub TestMySQLDateTime()
        Dim x As String
        x = wb_sql_Functions.MySQLdatetime(#1964/11/22 12:10:20#)
        Assert.AreEqual("1964-11-22 12:10:20", x)
    End Sub
End Class