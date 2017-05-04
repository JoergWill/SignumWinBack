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


    <TestMethod()> Public Sub Test_WriteHinweise()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then
            Dim s As String

            'erstmaliges Schreiben in Hinweise2.H2_Memo
            wb_sql_Functions.WriteHinweise(wb_Global.Hinweise.ArtikelHinweise, 9999, "TEST")
            s = wb_sql_Functions.ReadHinweise(wb_Global.Hinweise.ArtikelHinweise, 9999)
            Assert.AreEqual("TEST", s)
            'Zweites Schreiben in Hinweise2.H2_Memo
            wb_sql_Functions.WriteHinweise(wb_Global.Hinweise.ArtikelHinweise, 9999, "TESTNEU")
            s = wb_sql_Functions.ReadHinweise(wb_Global.Hinweise.ArtikelHinweise, 9999)
            Assert.AreEqual("TESTNEU", s)

            'wieder aufräumen
            wb_sql_Functions.DeleteHinweise(wb_Global.Hinweise.ArtikelHinweise, 9999)
            s = wb_sql_Functions.ReadHinweise(wb_Global.Hinweise.ArtikelHinweise, 9999)
            Assert.AreEqual("", s)

        End If
    End Sub

End Class