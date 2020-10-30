Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_LieferungenDB
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

    <TestMethod()> Public Sub TestAbbuchen_WennLieferungVorhanden()

        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'Tabelle Lieferungen Einträge (vorsichtshalber) löschen
            winback.sqlCommand("DELETE FROM Lieferungen WHERE LF_LG_Ort = 'KT102_TEST'")

            'Tabelle Lieferungen neue Einträge erzeugen 
            'LF_LG_Ort, LF_Nr, LF_Datum, LF_Menge, LF_Lieferant, LF_Gebucht, LF_Bemerkung
            'LF_Lager, LF_BF_Charge, LF_Liniengruppe, LF_BF_Frist, LF_Verbrauch, LF_User_Nr, LF_Preis, LF_PreisEinheit 
            winback.sqlCommand("INSERT INTO Lieferungen VALUES ('KT102_TEST', 0, '2020-10-16 11:17:10', '240', 'WinBack_GmbH', '1', 'UnitTest-WinBack', 0, 'CHRG-0', 0, NULL, '0', 709760, ' ', 0, '2020-10-16 11:17:10')")
            winback.sqlCommand("INSERT INTO Lieferungen VALUES ('KT102_TEST', 1, '2020-10-16 11:17:10', '240', 'WinBack_GmbH', '1', 'UnitTest-WinBack', 0, 'CHRG-1', 0, NULL, '0', 709760, ' ', 0, '2020-10-16 11:17:10')")

            'UnitTest Lieferungen.ProdukionVerbuchen 
            Dim Lieferungen As New wb_Lieferungen
            Dim ChrgNr As String
            'erste verbrauchte Charge 100kg von 240kg - Rest 140kg
            ChrgNr = Lieferungen.ProduktionVerbuchen("KT102_TEST", "100")
            Assert.AreEqual("CHRG-0", ChrgNr)

            'zweite verbrauchte Charge 100kg von 240kg - Rest 40kg
            ChrgNr = Lieferungen.ProduktionVerbuchen("KT102_TEST", "100")
            Assert.AreEqual("CHRG-0", ChrgNr)

            'dritte verbrauchte Charge 100kg von 240kg - Muss zwei Chargen-Nummern zurückgeben
            ChrgNr = Lieferungen.ProduktionVerbuchen("KT102_TEST", "100")
            Assert.AreEqual("CHRG-0, CHRG-1", ChrgNr)

            'vierte verbrauchte Charge 100kg von 240kg - Rest 80kg 
            ChrgNr = Lieferungen.ProduktionVerbuchen("KT102_TEST", "100")
            Assert.AreEqual("CHRG-1", ChrgNr)

            'fünfte verbrauchte Charge 100kg von 240kg - Bestand wird negativ 
            ChrgNr = Lieferungen.ProduktionVerbuchen("KT102_TEST", "100")
            Assert.AreEqual("CHRG-1", ChrgNr)

            'sechste verbrauchte Charge 100kg von 240kg - Bestand wird negativ 
            ChrgNr = Lieferungen.ProduktionVerbuchen("KT102_TEST", "100")
            Assert.AreEqual("CHRG-1", ChrgNr)

            'Neue Lieferung
            winback.sqlCommand("INSERT INTO Lieferungen VALUES ('KT102_TEST', 2, '2020-10-16 11:17:10', '240', 'WinBack_GmbH', '1', 'UnitTest-WinBack', 0, 'CHRG-2', 0, NULL, '0', 709760, ' ', 0, '2020-10-16 11:17:10')")
            'verbrauchte Charge 100kg von 240kg - Rest 140
            ChrgNr = Lieferungen.ProduktionVerbuchen("KT102_TEST", "100")
            Assert.AreEqual("CHRG-2", ChrgNr)

            'verbrauchte Charge 140kg von 240kg - Rest 0
            ChrgNr = Lieferungen.ProduktionVerbuchen("KT102_TEST", "140")
            Assert.AreEqual("CHRG-2", ChrgNr)

            'Neue Lieferung
            winback.sqlCommand("INSERT INTO Lieferungen VALUES ('KT102_TEST', 3, '2020-10-16 11:17:10', '240', 'WinBack_GmbH', '1', 'UnitTest-WinBack', 0, 'CHRG-3', 0, NULL, '0', 709760, ' ', 0, '2020-10-16 11:17:10')")
            'verbrauchte Charge 100kg von 240kg - Rest 140
            ChrgNr = Lieferungen.ProduktionVerbuchen("KT102_TEST", "10,67888")
            Assert.AreEqual("CHRG-3", ChrgNr)

            'Verbindung wieder schliessen
            winback.Close()

        End If
    End Sub

    <TestMethod()> Public Sub TestAbbuchen_KeineLieferungVorhanden()

        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'UnitTest Lieferungen.ProdukionVerbuchen 
            Dim Lieferungen As New wb_Lieferungen
            Dim ChrgNr As String

            'Tabelle Lieferungen Einträge (vorsichtshalber) löschen
            winback.sqlCommand("DELETE FROM Lieferungen WHERE LF_LG_Ort = 'KT102_TEST'")

            'verbrauchte Charge 100kg von 0kg
            ChrgNr = Lieferungen.ProduktionVerbuchen("KT102_TEST", "10,4")
            Assert.AreEqual("", ChrgNr)

            'Verbindung wieder schliessen
            winback.Close()

        End If
    End Sub
End Class