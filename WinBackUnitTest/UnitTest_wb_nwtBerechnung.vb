Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack
Imports WinBack.wb_Sql_Selects

<TestClass()> Public Class UnitTest_wb_nwtBerechnung

    <TestInitialize>
    Sub TestInitialize()
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
        'Backup der Test-Datenbank einspielen
        UnitTest_Init.Restore_WinBackDaten()
    End Sub

    <TestMethod()> Public Sub TestRezeptOhneProduktionsStufen()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'nächsten Datensatz aus Tabelle Komponenten lesen
            'Artikel 102 Brötchen (KO_Nr=3493)
            If winback.sqlSelect(setParams(sqlSelectKomp_KO_Nr, "3355")) Then
                'Lesen KO-Nr
                If winback.Read Then
                    'Komponenten-Objekt nimmt die aktuellen Daten auf
                    Dim nwtArtikelDaten As New wb_Komponente

                    'nächste KO_Nr aus DB in Object nwtDaten einlesen
                    nwtArtikelDaten.MySQLdbRead(winback.MySqlRead)
                    winback.CloseRead()
                    'aktuelle Komponenten-Nummer
                    Dim _AktKO_Nr As Integer = nwtArtikelDaten.Nr

                    'verknüpfte Rezeptnummer zum Artikel/Rohstoff
                    Dim _AktRZ_Nr As Integer = nwtArtikelDaten.RzNr
                    'Rezept mit allen Rezeptschritten lesen (NoMessage=True unterdrückt die Meldung "Rezept verweist auf sich selbst")
                    Dim Rzpt As New wb_Rezept(_AktRZ_Nr, Nothing, 0.0, 1, "", "", True)

                    'Änderungs-Log löschen
                    nwtArtikelDaten.ClearReport()

                    'Nährwert-Information berechnen
                    nwtArtikelDaten.ktTyp301 = Rzpt.RootRezeptSchritt.ktTyp301
                    Debug.Print("reCalcRezept (" & _AktRZ_Nr & ") " & Rzpt.RezeptNummer & " " & Rzpt.RezeptBezeichnung & " kt301(Kilokalorien) " & nwtArtikelDaten.ktTyp301.Naehrwert(wb_Global.T301_Kilokalorien))
                    'Kilokalorien
                    Dim kcal As Double = nwtArtikelDaten.ktTyp301.Naehrwert(wb_Global.T301_Kilokalorien)
                    Assert.AreEqual(177, Convert.ToInt32(kcal))

                End If
            End If
        End If
    End Sub

    <TestMethod()> Public Sub TestRezeptMitProduktionsStufen()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'nächsten Datensatz aus Tabelle Komponenten lesen
            'Artikel 1202 Honigkuchen (KO_Nr=3362)
            If winback.sqlSelect(setParams(sqlSelectKomp_KO_Nr, "3362")) Then
                'Lesen KO-Nr
                If winback.Read Then
                    'Komponenten-Objekt nimmt die aktuellen Daten auf
                    Dim nwtArtikelDaten As New wb_Komponente

                    'nächste KO_Nr aus DB in Object nwtDaten einlesen
                    nwtArtikelDaten.MySQLdbRead(winback.MySqlRead)
                    winback.CloseRead()
                    'aktuelle Komponenten-Nummer
                    Dim _AktKO_Nr As Integer = nwtArtikelDaten.Nr

                    'verknüpfte Rezeptnummer zum Artikel/Rohstoff
                    Dim _AktRZ_Nr As Integer = nwtArtikelDaten.RzNr
                    'Rezept mit allen Rezeptschritten lesen (NoMessage=True unterdrückt die Meldung "Rezept verweist auf sich selbst")
                    Dim Rzpt As New wb_Rezept(_AktRZ_Nr, Nothing, 0.0, 1, "", "", True)

                    'Änderungs-Log löschen
                    nwtArtikelDaten.ClearReport()

                    'Nährwert-Information berechnen
                    nwtArtikelDaten.ktTyp301 = Rzpt.RootRezeptSchritt.ktTyp301
                    Debug.Print("reCalcRezept (" & _AktRZ_Nr & ") " & Rzpt.RezeptNummer & " " & Rzpt.RezeptBezeichnung & " kt301(Kilokalorien) " & nwtArtikelDaten.ktTyp301.Naehrwert(wb_Global.T301_Kilokalorien))
                    'Kilokalorien
                    Dim kcal As Double = nwtArtikelDaten.ktTyp301.Naehrwert(wb_Global.T301_Kilokalorien)
                    Assert.AreEqual(179, Convert.ToInt32(kcal))

                End If
            End If
        End If
    End Sub
End Class