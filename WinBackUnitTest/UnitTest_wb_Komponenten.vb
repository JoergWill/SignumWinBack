﻿Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack
Imports WinBack.wb_Sql_Selects


<TestClass()> Public Class UnitTest_wb_Komponenten
    <TestInitialize>
    Sub TestInitialize()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then
            'Datenbank Verbindung Einstellungen setzen
            '(Muss in wb_Konfig gesetzt werden, weil My.Setting hier nicht funktioniert)
            wb_Konfig.SqlSetting("MySQL")
            'Initialisierung Texte-Tabelle
            wb_Konfig.LoadTexteTabelle(wb_Konfig.GetLanguageNr())
        End If
    End Sub

    <TestMethod()> Public Sub Test_LesenRohstoff_MitNummer()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponenten

            'ersten Datensatz aus Tabelle Komponenten lesen
            Assert.IsTrue(nwtDaten.MySQLdbRead(211))
            Assert.AreEqual(211, nwtDaten.Nr)
            Assert.AreEqual(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, nwtDaten.Type)
            Assert.AreEqual("Schüttwasser", nwtDaten.Bezeichung)
        End If
    End Sub

    <TestMethod()> Public Sub Test_SchreibenRohstoff_MitNummer()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponenten
            'ersten Datensatz aus Tabelle Komponenten lesen
            Assert.IsTrue(nwtDaten.MySQLdbRead(211))
            'Rohstoff-Bezeichnung ändern
            nwtDaten.Bezeichung = "TEST"
            'Datensatz in WinBack-DB schreiben
            Assert.IsTrue(nwtDaten.MySQLdbWrite(211))
            'ersten Datensatz aus Tabelle Komponenten lesen
            Assert.IsTrue(nwtDaten.MySQLdbRead(211))
            Assert.AreEqual("TEST", nwtDaten.Bezeichung)
            'Daten wieder richtigstellen
            nwtDaten.Bezeichung = "Schüttwasser"
            'Datensatz in WinBack-DB schreiben
            Assert.IsTrue(nwtDaten.MySQLdbWrite(211))

        End If
    End Sub

    <TestMethod()> Public Sub Test_LesenRohstoff_Stammdaten()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponenten

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

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponenten

            Dim sql As String = setParams(sqlTestktTyp3, "211")
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_Konfig.SqlConWinBack, wb_Sql.dbType.mySql)

            'ersten Datensatz aus Tabelle RohParams lesen
            If winback.sqlSelect(sql) Then
                If winback.Read Then
                    'den ersten und alle weiteren Daensätze aus der sql-Abfrage lesen
                    Assert.IsTrue(nwtDaten.MySQLdbRead(winback.MySqlRead))
                    'Nährwert-Info - Wasser-Anteil
                    Assert.IsTrue(nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_Wasser) > 90.0)
                    'Nährwert-Info - keine Allergene
                    Assert.AreEqual(wb_Global.AllergenInfo.N, nwtDaten.ktTyp301.Allergen(wb_Global.T301_Gluten))
                End If
            End If
        End If
    End Sub

    <TestMethod()> Public Sub Test_ChangeLog()
        'Test kann nur ausgeführt werden, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponenten

            'Eintrag Lieferant ändern
            nwtDaten.ClearReport()
            nwtDaten.Lieferant = "Neuer Lieferant"
            Debug.Print(nwtDaten.GetReport)
            Assert.AreEqual("/Neuer Lieferant" + vbNewLine, nwtDaten.GetReport(True))

            'Eintrag Nährwert-Info
            nwtDaten.ClearReport()
            nwtDaten.ktTyp301.Wert(wb_Global.T301_Fette) = 24.5F
            Debug.Print(nwtDaten.GetReport)
            Assert.AreEqual("   0,000 g/  24,500 g Fette" + vbNewLine, nwtDaten.GetReport(True))

            'Eintrag Nährwert-Info + Lieferanten-Bezeichnung
            nwtDaten.ClearReport()
            nwtDaten.ktTyp301.Wert(wb_Global.T301_Fette) = 24.6F
            nwtDaten.Lieferant = "Anderer Lieferant"
            Debug.Print(nwtDaten.GetReport)
            Assert.AreEqual("Neuer Lieferant/Anderer Lieferant" + vbNewLine + "  24,500 g/  24,600 g Fette" + vbNewLine, nwtDaten.GetReport(True))
        End If
    End Sub

End Class