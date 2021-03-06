﻿Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack
Imports WinBack.wb_Sql_Selects


<TestClass()> Public Class UnitTest_wb_Komponenten
    <TestInitialize>
    Sub TestInitialize()
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then
            'Datenbank Verbindung Einstellungen setzen
            '(Muss in wb_Konfig gesetzt werden, weil My.Setting hier nicht funktioniert)
            wb_GlobalSettings.WinBackDBType = wb_Sql.dbType.mySql
            'Initialisierung Texte-Tabelle
            wb_Language.LoadTexteTabelle(wb_Language.GetLanguageNr())
        End If
    End Sub

    <TestMethod()> Public Sub Test_LesenRohstoff_MitNummer()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponente

            'ersten Datensatz aus Tabelle Komponenten lesen
            Assert.IsTrue(nwtDaten.MySQLdbRead(211))
            Assert.AreEqual(211, nwtDaten.Nr)
            Assert.AreEqual(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, nwtDaten.Type)
            Assert.AreEqual("Schüttwasser", nwtDaten.Bezeichnung)
        End If
    End Sub

    <TestMethod()> Public Sub Test_SchreibenRohstoff_MitNummer()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponente
            'ersten Datensatz aus Tabelle Komponenten lesen
            Assert.IsTrue(nwtDaten.MySQLdbRead(211))
            'Rohstoff-Bezeichnung ändern
            nwtDaten.Bezeichnung = "TEST"
            'Datensatz in WinBack-DB schreiben
            Assert.IsTrue(nwtDaten.MySQLdbUpdate())
            'ersten Datensatz aus Tabelle Komponenten lesen
            Assert.IsTrue(nwtDaten.MySQLdbRead(211))
            Assert.AreEqual("TEST", nwtDaten.Bezeichnung)
            'Daten wieder richtigstellen
            nwtDaten.Bezeichnung = "Schüttwasser"
            'Datensatz in WinBack-DB schreiben
            Assert.IsTrue(nwtDaten.MySQLdbUpdate())

        End If
    End Sub

    <TestMethod()> Public Sub Test_LesenRohstoff_Stammdaten()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponente

            Dim sql As String = setParams(sqlTestktTypX, "211")
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'ersten Datensatz aus Tabelle Komponenten lesen
            If winback.sqlSelect(sql) Then
                If winback.Read Then
                    Assert.IsTrue(nwtDaten.MySQLdbRead(winback.MySqlRead))
                    Assert.AreEqual(211, nwtDaten.Nr)
                    Assert.AreEqual(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, nwtDaten.Type)
                    Assert.AreEqual("Schüttwasser", nwtDaten.Bezeichnung)
                End If
            End If
        End If
    End Sub


    <TestMethod()> Public Sub Test_LesenRohstoff_Parameter()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponente

            Dim sql As String = setParams(sqlTestktTyp3, "211")
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

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

    <TestMethod()> Public Sub Test_SchreibenRohstoff_Parameter()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponente
            nwtDaten.MySQLdbRead(211)

            Dim sql As String = setParams(sqlTestktTyp3, "211")
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'ersten Datensatz aus Tabelle RohParams lesen
            If winback.sqlSelect(sql) Then
                If winback.Read Then
                    'den ersten und alle weiteren Daensätze aus der sql-Abfrage lesen
                    Assert.IsTrue(nwtDaten.MySQLdbRead(winback.MySqlRead))
                    'Nährwert-Info - Wasser-Anteil
                    Assert.IsTrue(nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_Wasser) > 90.0)
                    'Nährwert-Info - keine Allergene
                    Assert.AreEqual(wb_Global.AllergenInfo.K, nwtDaten.ktTyp301.Allergen(wb_Global.T301_Gluten))

                    'RohstoffParameter ändern
                    nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_Wasser) = 10
                    'Nährwert-Info - Wasser-Anteil
                    Assert.AreEqual(10.0, nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_Wasser))
                    'RohstoffParameter schreiben
                    nwtDaten.UpdateDB()
                End If
            End If

            'Datensatz schliessen
            winback.CloseRead()
            'ersten (geänderten) Datensatz aus Tabelle RohParams lesen
            If winback.sqlSelect(sql) Then
                If winback.Read Then
                    'den ersten und alle weiteren Daensätze aus der sql-Abfrage lesen
                    Assert.IsTrue(nwtDaten.MySQLdbRead(winback.MySqlRead))
                    'Nährwert-Info - Wasser-Anteil
                    Assert.AreEqual(10.0, nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_Wasser))

                    'RohstoffParameter ändern - wieder auf den Ursprungswert
                    nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_Wasser) = 99.0
                    'RohstoffParameter schreiben
                    nwtDaten.UpdateDB()
                End If
            End If

        End If
    End Sub

    <TestMethod()> Public Sub Test_SchreibenRohstoff_Parameter_Einzeln()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponente
            nwtDaten.MySQLdbRead(211)

            Dim sql As String = setParams(sqlTestktTyp3, "211")
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'ersten Datensatz aus Tabelle RohParams lesen
            If winback.sqlSelect(sql) Then
                If winback.Read Then
                    'den ersten und alle weiteren Daensätze aus der sql-Abfrage lesen
                    Assert.IsTrue(nwtDaten.MySQLdbRead(winback.MySqlRead))
                    'Nährwert-Info - Wasser-Anteil
                    'Assert.IsTrue(nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_Wasser) > 90.0)
                    'Nährwert-Info - keine Allergene
                    Assert.AreEqual(wb_Global.AllergenInfo.K, nwtDaten.ktTyp301.Allergen(wb_Global.T301_Gluten))

                    'RohstoffParameter ändern
                    nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_Wasser) = 10
                    'Nährwert-Info - Wasser-Anteil
                    Assert.AreEqual(10.0, nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_Wasser))

                    'Parameter-Array aufbauen
                    For Each c1 As wb_KomponParam In nwtDaten.RootParameter.ChildSteps
                        For Each c2 As wb_KomponParam In c1.ChildSteps
                            Select Case c2.TypNr
                                Case wb_Global.ktParam.kt301
                                    Select Case c2.ParamNr
                                        Case wb_Global.T301_Wasser
                                            c2.Wert = 20.0
                                            c2.Changed = True
                                    End Select
                            End Select
                        Next
                    Next

                    'geänderten Datensatz schreiben
                    nwtDaten.SaveParameterArray()

                End If
            End If

            'Datensatz schliessen
            winback.CloseRead()
            'ersten (geänderten) Datensatz aus Tabelle RohParams lesen
            If winback.sqlSelect(sql) Then
                If winback.Read Then
                    'den ersten und alle weiteren Daensätze aus der sql-Abfrage lesen
                    Assert.IsTrue(nwtDaten.MySQLdbRead(winback.MySqlRead))
                    'Nährwert-Info - Wasser-Anteil
                    Assert.AreEqual(20.0, nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_Wasser))

                    'RohstoffParameter ändern - wieder auf den Ursprungswert
                    nwtDaten.ktTyp301.Naehrwert(wb_Global.T301_Wasser) = 99.0
                    'RohstoffParameter schreiben
                    nwtDaten.UpdateDB()
                End If
            End If

        End If
    End Sub

    <TestMethod()> Public Sub Test_OrgaBackNaehrwerte()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponente
            nwtDaten.MySQLdbRead(3305)

            Dim sql As String = setParams(sqlTestktTyp3, "3305")
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'ersten Datensatz aus Tabelle RohParams lesen
            If winback.sqlSelect(sql) Then
                If winback.Read Then
                    'den ersten und alle weiteren Daensätze aus der sql-Abfrage lesen
                    Assert.IsTrue(nwtDaten.MySQLdbRead(winback.MySqlRead))

                    'Update der Parameter in OrgaBack
                    'Gibt true zurück, wenn der Artikel in OrgaBack existiert
                    nwtDaten.MsSQLdbUpdate_Parameter(wb_Global.ktParam.kt301)
                    'Update der Zutatenliste in OrgaBack
                    nwtDaten.MsSqldbUpdate_Zutatenliste()

                End If
            End If
        End If
    End Sub

    <TestMethod()> Public Sub Test_Update_Artikel_RezeptschritteMitKomponente()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponente

            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            'alle Artikel mit Rezeptschritt der Komponente 600 (Weizenmehl) enthält auf Update setzen
            Dim sql As String = setParams(sqlKompSetMarker, 600, wb_Global.ArtikelMarker.nwtUpdate)
            'Update Komponente in winback.Komponenten
            winback.sqlCommand(sql)

            'Datensatz lesen (3518 - Mehrkornbrötchen)
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlTestktTypX, 3518)

            'ersten Datensatz aus Tabelle Komponenten(3518) lesen
            If winback.sqlSelect(sql) Then
                If winback.Read Then
                    Assert.AreEqual(2, winback.iField("KA_PreisEinheit"))
                End If
            End If
            'Verbindung Lesen wieder schliessen
            winback.CloseRead()

            'alle Artikel mit Rezeptschritt der Komponente 600 (Weizenmehl) wieder auf OK setzen
            sql = setParams(sqlKompSetMarker, 600, wb_Global.ArtikelMarker.nwtUpdate)
            'Update Komponente in winback.Komponenten
            winback.sqlCommand(sql)

        End If

    End Sub

    ''' <summary>
    ''' Der Test Update_Artikel_RezeptschritteMitKomponente funktioniert nur wenn das Programm
    ''' NICHT im Debug-Modus läuft. Ansonsten wird ein Syntax-Error SQL angezeigt !!!
    ''' Dieser Test läuft auf einer MySQL 3.2 Datenbank (172.16.17.4) und muss angepasst werden 
    ''' </summary>
    <TestMethod()> Public Sub Test_Update_Artikel_RezeptschritteMitKomponente_172_16_17_4()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL And False Then
            'TEST MIT V3.0
            wb_GlobalSettings.MySQLServerIP = "172.16.17.4"

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponente

            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            'alle Artikel mit Rezeptschritt der Komponente 600 (Weizenmehl) enthält auf Update setzen
            'Komponenten-Nummer muss an Datenbank angepasst werden !!
            'TODO eigene Test-DB Version 3.2 aufsetzen (VM)
            Dim sql As String = setParams(sqlKompSetMarker, 3510, wb_Global.ArtikelMarker.nwtUpdate)
            'Update Komponente in winback.Komponenten
            winback.sqlCommand(sql)

            'Datensatz lesen (Artikel-KO-Nummer muss angepasst werden !!)
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlTestktTypX, 4312)

            'ersten Datensatz aus Tabelle Komponenten(3518) lesen
            If winback.sqlSelect(sql) Then
                If winback.Read Then
                    Assert.AreEqual(2, winback.iField("KA_PreisEinheit"))
                End If
            End If
            'Verbindung Lesen wieder schliessen
            winback.CloseRead()

            'alle Artikel mit Rezeptschritt der Komponente 600 (Weizenmehl) wieder auf OK setzen
            sql = setParams(sqlKompSetMarker, 3510, wb_Global.ArtikelMarker.nwtUpdate)
            'Update Komponente in winback.Komponenten
            winback.sqlCommand(sql)

        End If

    End Sub



    <TestMethod()> Public Sub Test_Schreiben_ArtikelLinienGruppe()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponente

            Dim sql As String = setParams(sqlTestktTyp3, "211")
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'ersten Datensatz aus Tabelle RohParams lesen
            If winback.sqlSelect(sql) Then
                If winback.Read Then
                    'den ersten und alle weiteren Daensätze aus der sql-Abfrage lesen
                    Assert.IsTrue(nwtDaten.MySQLdbRead(winback.MySqlRead))

                    'Artikel-Parameter (300,5) ändern
                    nwtDaten.iArtikelLinienGruppe = 211
                    'RohstoffParameter schreiben
                    nwtDaten.UpdateDB()
                End If
            End If

            'ersten Datensatz aus Tabelle RohParams lesen
            If winback.sqlSelect(sql) Then
                If winback.Read Then
                    'den ersten und alle weiteren Daensätze aus der sql-Abfrage lesen
                    Assert.IsTrue(nwtDaten.MySQLdbRead(winback.MySqlRead))

                    'Artikel-Parameter(300,5) prüfen (muss 211 sein)
                    Assert.AreEqual(211, nwtDaten.iArtikelLinienGruppe)
                    'Artikel-Parameter (300,5) ändern
                    nwtDaten.iArtikelLinienGruppe = 0
                    'RohstoffParameter schreiben
                    nwtDaten.UpdateDB()
                End If
            End If

            'ersten Datensatz aus Tabelle RohParams lesen
            If winback.sqlSelect(sql) Then
                If winback.Read Then
                    'den ersten und alle weiteren Daensätze aus der sql-Abfrage lesen
                    Assert.IsTrue(nwtDaten.MySQLdbRead(winback.MySqlRead))

                    'Artikel-Parameter(300,5) prüfen (muss wieder 0 sein)
                    Assert.AreEqual(0, nwtDaten.iArtikelLinienGruppe)
                End If
            End If

        End If
    End Sub

    <TestMethod()> Public Sub Test_ChangeLog()
        'Test kann nur ausgeführt werden, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponente

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

    <TestMethod()> Public Sub Test_NewKomp()
        'Test kann nur ausgeführt werden, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten
            Dim nwtDaten As New wb_Komponente
            nwtDaten.Nummer = "1111TEST"
            'Datenatz neu anlegen
            Dim NewKoNr As Integer = nwtDaten.MySQLdbNew(wb_Global.KomponTypen.KO_TYPE_ARTIKEL)

            'ersten Datensatz aus Tabelle Komponenten lesen
            Assert.IsTrue(nwtDaten.MySQLdbRead(NewKoNr))
            Assert.AreEqual(NewKoNr, nwtDaten.Nr)
            Assert.AreEqual(wb_Global.KomponTypen.KO_TYPE_ARTIKEL, nwtDaten.Type)
            Assert.AreEqual("Neu angelegt", Left(nwtDaten.Bezeichnung, 12))

            nwtDaten.Nummer = "1112TEST"
            'Datenatz neu anlegen
            NewKoNr = nwtDaten.MySQLdbNew(wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE)

            'zweiten Datensatz aus Tabelle Komponenten lesen
            Assert.IsTrue(nwtDaten.MySQLdbRead(NewKoNr))
            Assert.AreEqual(NewKoNr, nwtDaten.Nr)
            Assert.AreEqual(wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE, nwtDaten.Type)
            Assert.AreEqual("Neu angelegt", Left(nwtDaten.Bezeichnung, 12))

            'wieder aufräumen
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            winback.sqlCommand("DELETE FROM Komponenten WHERE KO_Nr_AlNum = '1111TEST'")
            winback.sqlCommand("DELETE FROM Komponenten WHERE KO_Nr_AlNum = '1112TEST'")
            winback.Close()
        End If
    End Sub


    <TestMethod()> Public Sub Test_DelKomp()
        'Test kann nur ausgeführt werden, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Rohstoff-Daten (Test Wasser)
            Dim KompDaten As New wb_Komponente

            'Verwendung im Rezept
            Assert.IsFalse(KompDaten.MySQLdbCanBeDeleted(211))

            'neuen Artikel anlegen
            Dim NewKoNr As Integer = KompDaten.MySQLdbNew(wb_Global.KomponTypen.KO_TYPE_ARTIKEL)
            'Verwendung im Rezept
            Assert.IsTrue(KompDaten.MySQLdbCanBeDeleted(NewKoNr))

            'neuen Rohstoff anlegen
            NewKoNr = KompDaten.MySQLdbNew(wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE)
            'Verwendung im Rezept
            Assert.IsTrue(KompDaten.MySQLdbCanBeDeleted(NewKoNr))

        End If
    End Sub


    <TestMethod()> Public Sub Test_ArtikelLinienGruppe()
        Dim Komponente As New wb_Komponente

        'normale Liniengruppe aus winback (Integer)
        Komponente.iArtikelLinienGruppe = 1
        Assert.AreEqual(1, Komponente.iArtikelLinienGruppe)

        'Artikel-Liniengruppe aus winback (Integer)
        Komponente.iArtikelLinienGruppe = 101
        'Artikel-Liniengruppe OrgaBack (String)
        Assert.AreEqual("0001", Komponente.sArtikeLinienGruppe)

        'Artikel-Liniengruppe aus OrgaBack (MFF-Feld107)
        Komponente.sArtikeLinienGruppe = "0002"
        Assert.AreEqual(102, Komponente.iArtikelLinienGruppe)
        Assert.AreEqual("0002", Komponente.sArtikeLinienGruppe)
    End Sub



End Class