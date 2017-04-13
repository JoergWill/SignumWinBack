Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack
Imports WinBack.wb_Sql_Selects


<TestClass()> Public Class UnitTest_wb_ktTypX

    <TestMethod()> Public Sub Test_LesenRohstoff_Stammdaten()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then
            'Datenbank Verbindung Einstellungen setzen
            '(Muss in wb_Konfig gesetzt werden, weil My.Setting hier nicht funktioniert)
            wb_Konfig.SqlSetting("MySQL")

            'Rohstoff-Daten
            Dim nwtDaten As New wb_ktTypX
            Dim sql As String = setParams(sqlTestktTypX, "211")
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_Konfig.SqlConWinBack, wb_Sql.dbType.mySql)

            'ersten Datensatz aus Tabelle Komponenten lesen
            If winback.sqlSelect(sql) Then
                If winback.Read Then
                    Assert.IsTrue(nwtDaten.MySQLdbRead(winback.MySqlRead))
                    Assert.AreEqual(211, nwtDaten.Nr)
                    Assert.AreEqual(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, nwtDaten.Type)
                    Assert.AreEqual("Schüttwasser", nwtDaten.Bezeichung)
                End If
            End If
        End If
    End Sub

    <TestMethod()> Public Sub Test_LesenRohstoff_Parameter()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then
            'Datenbank Verbindung Einstellungen setzen
            '(Muss in wb_Konfig gesetzt werden, weil My.Setting hier nicht funktioniert)
            wb_Konfig.SqlSetting("MySQL")

            'Rohstoff-Daten
            Dim nwtDaten As New wb_ktTypX
            Dim sql As String = setParams(sqlTestktTyp3, "211")
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_Konfig.SqlConWinBack, wb_Sql.dbType.mySql)

            'ersten Datensatz aus Tabelle RohParams lesen
            If winback.sqlSelect(sql) Then
                If winback.Read Then
                    'den ersten und alle weiteren Daensätze aus der sql-Abfrage lesen
                    Assert.IsTrue(nwtDaten.MySQLdbRead(winback.MySqlRead))
                    Assert.IsTrue(nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_Wasser) > 90.0)
                    Assert.AreEqual(wb_Global.AllergenInfo.K, nwtDaten.ktTyp301.Allergen(wb_Global.T301_Gluten))
                End If
            End If
        End If
    End Sub

End Class