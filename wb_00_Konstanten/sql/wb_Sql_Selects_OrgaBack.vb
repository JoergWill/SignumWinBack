﻿Partial Public Class wb_Sql_Selects

    'Sql-Statement Abfrage Vorname, Nachname und Nummer aus [OrgaBackMain].[dbo].[Mitarbeiter]"
    Public Const mssqlMitarbeiterMFF500 = "SELECT * FROM [dbo].[Mitarbeiter] LEFT JOIN MitarbeiterHatMultiFeld " &
                                    "ON Mitarbeiter.MitarbeiterKürzel = MitarbeiterHatMultiFeld.MitarbeiterKürzel ORDER BY PersonalNr"
    '"WHERE MitarbeiterHatMultiFeld.FeldNummer = 500 ORDER BY KassiererNummer"
    Public Const mssqlMitarbeiterMFF = "SELECT * FROM [dbo].[Mitarbeiter] LEFT JOIN MitarbeiterHatMultiFeld " &
                                    "ON Mitarbeiter.MitarbeiterKürzel = MitarbeiterHatMultiFeld.MitarbeiterKürzel ORDER BY KassiererNummer"
    'Sql-Statement Mitarbeiterkürzel aus Personal-Nummer
    Public Const mssqlMitarbeiter = "SELECT MitarbeiterKürzel FROM [dbo].[Mitarbeiter] WHERE PersonalNr = '[0]'"
    'Sql-Statement Update Gruppen-Bezeichnung aus [OrgaBackMain].[dbo].[MitarbeiterMultiFunktionsFeld]"
    Public Const mssqlUpdateMitarbeiterMFF = "UPDATE [dbo].[MitarbeiterHatMultiFeld] SET Inhalt = '[1]' WHERE MitarbeiterKürzel = '[0]' AND FeldNummer = 500"

    'Sql-Statement Abfrage Gruppen-Nr(Hierarchie) und Gruppen-Bezeichnung aus [OrgaBackMain].[dbo].[MitarbeiterMultiFunktionsFeld]"
    Public Const mssqlMitarbeiterGruppen = "SELECT * FROM [dbo].[MitarbeiterMultiFunktionsFeld] WHERE GruppenNr=1 ORDER BY Hierarchie"
    'Sql-Statement Update Gruppen-Bezeichnung aus [OrgaBackMain].[dbo].[MitarbeiterMultiFunktionsFeld]"
    Public Const mssqlUpdateMitarbeiterGruppen = "UPDATE [dbo].[MitarbeiterMultiFunktionsFeld] SET Bezeichnung = '[1]' WHERE GruppenNr=1 AND Hierarchie='[0]'"
    'Sql-Statement Insert Mitarbeiter-Gruppe [OrgaBackMain].[dbo].[MitarbeiterMultiFunktionsFeld]"
    Public Const mssqlInsertMitarbeiterGruppen = "INSERT INTO [dbo].[MitarbeiterMultiFunktionsFeld] (GruppenNr, Hierarchie, Bezeichnung) VALUES ('[2]','[0]','[1]')"

    'Sql-Statement Abfrage Gruppen-Nr der Artikel-Zusatzgruppe aus MFF200 (Aufarbeitungsplatz)
    Public Const mssqlArtikelZusGruppenNr = "SELECT GruppenNr FROM [dbo].[Multifunktionsfeld] WHERE Feldnummer = [0]"
    'Sql-Statement Abfrage Aufarbeitungsplätze in OrgaBack (Einträge im Auswahlfeld MFF200(Gruppe))
    Public Const mssqlAufarbeitungsPl = "SELECT * FROM [dbo].[ArtikelMultifunktionsfeld] WHERE GruppenNr = [0]"
    'Sql-Statement Update Artikel-Zusatzgruppe in [OrgaBackMain].[dbo].[ArtikelMultifunktionsfeld]"
    Public Const mssqlUpdateAufarbeitungsPl = "UPDATE [dbo].[ArtikelMultifunktionsfeld] SET Bezeichnung = '[2]' WHERE Hierarchie = '[1]' AND GruppenNr = '[0]'"
    'Sql-Statement Update Artikel-Zusatzgruppe in [OrgaBackMain].[dbo].[ArtikelMultifunktionsfeld]"
    Public Const mssqlInsertAufarbeitungsPl = "INSERT INTO [dbo].[ArtikelMultifunktionsfeld] (GruppenNr, Hierarchie, Bezeichnung) VALUES ('[0]', '[1]', '[2]')"

    'Sql-Statement Artikel mit Sortiment und Einheit - 2022-05-20/JW Regular-Expression für Artikelgruppe
    Public Const mssqlArtikel = "Select ArtikelNr,Kurztext,Sortiment, StdEinheit FROM [dbo].[Artikel] WHERE [0] ORDER BY ArtikelNr"

    'Sql-Statement Abfrage dbo.WorkStations.Computername
    Public Const mssqlSelArtikel = "Select * FROM [dbo].[Artikel] WHERE ArtikelNr = '[0]'"
    'Sql-Statement Delete ArtikelNaehrwerte für ALLE Stücklistenvarianten
    Public Const mssqlDeleteNwt = "DELETE FROM [dbo].[ArtikelNaehrwerte] WHERE [ArtikelNr] = '[0]' AND [Einheit] = [1]"
    'Sql-Statement Delete ArtikelAllergene für ALLE Stücklistenvarianten
    Public Const mssqlDeleteAlg = "DELETE FROM [dbo].[ArtikelAllergene] WHERE [ArtikelNr] = '[0]'"
    'Sql-Statement Delete ArtikelNaehrwerte
    Public Const mssqlDeleteParamNwt = "DELETE FROM [dbo].[ArtikelNaehrwerte] WHERE [ArtikelNr] = '[0]' AND [NaehrwertNr] = [1] AND [Einheit] = [2] AND [StuecklistenVariantenNr] = [3]"
    'Sql-Statement Delete ArtikelAllergene
    Public Const mssqlDeleteParamAlg = "DELETE FROM [dbo].[ArtikelAllergene] WHERE [ArtikelNr] = '[0]' AND [AllergenNr] = [1] AND [StuecklistenVariantenNr] = [2]"
    ''Sql-Statement Update ArtikelNaehrwerte
    'Public Const mssqUpdateNwt = "UPDATE [dbo].[ArtikelNaehrwerte] SET ArtikelNr = '[0]', Einheit = [3], Farbe = 0, Groesse = 'NULL', " &
    '                             "StuecklistenVariantenNr = [4] , NaehrwertNr = [1], Menge = [2]"
    ''Sql-Statement Update ArtikelAllergene
    'Public Const mssqUpdateAlg = "UPDATE [dbo].[ArtikelAllergene] SET ArtikelNr = '[0]', StuecklistenVariantenNr = [3], " &
    '                             "AllergenNr = [1], Kennzeichnung = '[2]' "
    'Sql-Statement Insert ArtikelNaehrwerte
    Public Const mssqlInsertNwt = "INSERT INTO [dbo].[ArtikelNaehrwerte] (ArtikelNr, Einheit, Farbe, Groesse, StuecklistenVariantenNr, NaehrwertNr, Menge) " &
                                  "VALUES ('[0]', [3], 0, 'NULL', [4], [1], [2])"
    'Sql-Statement Insert ArtikelAllergene
    Public Const mssqlInsertAlg = "INSERT INTO [dbo].[ArtikelAllergene] (ArtikelNr, StuecklistenVariantenNr, AllergenNr, Kennzeichnung) " &
                                  "VALUES ('[0]', [3], [1], '[2]')"
    'Sql-Statement Insert ArtikelErnährung
    'TOD Version 3
    Public Const mssqlInsertErng = "INSERT INTO [dbo].[ArtikelAllergene] (ArtikelNr, StuecklistenVariantenNr, AllergenNr, Kennzeichnung) " &
                                   "VALUES ('[0]', [3], [1], '[2]')"

    'Sql-Statement Update ArtikelHatMultifeld
    Public Const mssqlUpdateArtikelMFF = "UPDATE [dbo].[ArtikelHatMultiFeld] SET Inhalt = '[3]' WHERE ArtikelNr ='[0]' AND Feldnummer = [1] AND FilialNr = [2]"
    Public Const mssqlInsertArtikelMFF = "INSERT INTO [dbo].[ArtikelHatMultiFeld] (ArtikelNr, Feldnummer, FilialNr, Inhalt) VALUES ('[0]', [1], [2], '[3]')"
    Public Const mssqlDeleteArtikelMFF = "DELETE FROM [dbo].[ArtikelHatMultiFeld] WHERE ArtikelNr = '[0]' AND Feldnummer = [1]"

    'Sql-Statement Abfrage dbo.[Naehrwerte]
    Public Const mssqlSelNaehrwerte = "SELECT * FROM [dbo].[Naehrwerte]"
    'Sql-Statement Abfrage dbo.[Allergene]
    Public Const mssqlSelAllergen = "SELECT * FROM [dbo].[Allergene]"
    'Sql-Statement Abfrage dbo.[Umrechnung]
    Public Const mssqlSelUmrechnung = "SELECT * FROM [dbo].[Umrechnung] WHERE ArtikelNr = '[0]' AND VonEinheit = [1]"
    'Sql-Statement Abfrage dbo.[[StuecklistenVariante] (nur die ersten Werte)
    Public Const mssqlSelStkListeVariante = "SELECT StuecklistenVariantenNr FROM [dbo].[StuecklistenVariante] WHERE ArtikelNr = '[0]' GROUP BY StuecklistenVariantenNr"
    'Gewichtswert aus Handelsartikel
    Public Const mssqlHandelsArtikelGewicht = "SELECT NettoInhalt FROM [dbo].[Handelsartikel] WHERE ArtikelNr = '[0]' and Einheit = [1]"
    'Gewichtswert aus Stücklisten-Variante
    Public Const mssqlStkListeGewicht = "SELECT Gewicht FROM [dbo].[StuecklistenVariante] WHERE ArtikelNr = '[0]' and Einheit = [1]"

    'Sql-Statement Abfrage dbo.[Artikelgruppe]
    Public Const mssqlSelArtikelGruppe = "SELECT * FROM [dbo].[Artikelgruppe]"


    'Sql-Statement Zutatenliste und Deklarationstexte
    Public Const sqlReadDeklaration = "SELECT * FROM [dbo].[ArtikelDeklarationsTexte] WHERE ArtikelNr = '[0]' AND StuecklistenVariantenNr = [1] " &
                                      "AND [LaenderCode] = '[2]' AND [SprachenCode] = '[3]'"
    'Sql-Statement Update Zutatenliste und Deklarationstexte
    Public Const sqlUpdateDeklaration = "UPDATE [dbo].[ArtikelDeklarationsTexte] SET [0] WHERE ArtikelNr = '[1]' AND StuecklistenVariantenNr = [2] " &
                                      "AND [LaenderCode] = '[3]' AND [SprachenCode] = '[4]'"
    'Sql-Statement Insert Zutatenliste und Deklarationstexte
    Public Const sqlInsertDeklaration = "INSERT INTO [dbo].[ArtikelDeklarationsTexte] (ArtikelNr, StuecklistenVariantenNr, LaenderCode, SprachenCode, " &
                                        "Zutaten, AllergenDeklarationEnthalten, AllergenDeklarationSpuren, AllergenKurzDeklarationEnthalten, " &
                                        "AllergenKurzDeklarationSpuren) VALUES ('[1]', [2], '[3]', '[4]', [0])"

    'Sql-Statement Filiale zugeordnet zu Produktion (Typ=4)
    Public Const mssqlFiliale = "Select Filialnummer, Name1 FROM Filialen WHERE [Typ] = [0]"
    'Sql-Statament Sortiment zugeordnet zu Filiale mit Typ Produktion
    Public Const mssqlSortiment = "Select * FROM FilialeHatSortiment INNER JOIN Filialen On FilialeHatSortiment.Filialnr =  Filialen.Filialnummer " &
                                  "INNER JOIN Sortiment ON FilialeHatSortiment.SortimentsKürzel = Sortiment.SortimentsKürzel WHERE [Typ] = [0] " &
                                  "ORDER BY FilialeHatSortiment.SortimentsKürzel"
    'Sql-Statement Abfrage dbo.ArtikelMultifunktionsfeld.Bezeichnung (Backorte)
    Public Const mssqlBackorte = "Select * FROM ArtikelMultifunktionsfeld WHERE [GruppenNr] = '[0]'"

    'Sql-Statement Abfrage dbo.Settings.Category
    Public Const mssqlSettings = "Select * FROM Settings WHERE [Category] = '[0]'"
    'Sql-Statement Abfrage dbo.WorkStations.Computername
    Public Const mssqlWrkStations = "SELECT * FROM WorkStations WHERE [ComputerName] = '[0]'"
    'Sql-Statement Abfrage dbo.SystemInfo
    Public Const mssqlSystemInfo = "SELECT TOP(1) * FROM SystemInfo ORDER BY lfd DESC"

    'Sql-Statement Insert Produktions/Verbrauchsdaten in [OrgaBackMain].[dbo].[ProduzierteWare]"
    '(ab Version 1.10.0.0 wird auch die Uhrzeit mit übergeben)
    Public Const mssqlInsertProduktionsDaten = "INSERT INTO [dbo].[ProduzierteWare] (FilialNr, ProduktionsDatum, ProduktionsUhrzeit, SatzTyp, ArtikelNr, " &
                                               "Einheit, Farbe, Groesse, Menge, ChargenNr, HaltbarkeitsDatum) VALUES ([0])"

    'Sql-Statement Abfrage Datensatz vorhanden in dbo.ProduktionAktuell
    Public Const mssqlProduktionAktuell = "SELECT * FROM [dbo].[ProduktionAktuell] WHERE [FilialNr] = [0] AND [LieferDatum] = '[1]' AND [TourNr] = [2] AND [ArtikelNr] = '[3]'"
    'Sql-Statement Insert Datensatz in dbo.ProduktionAktuell
    Public Const mssqlInsertProduktionAktuell = "INSERT INTO [dbo].[ProduktionAktuell] (FilialNr, LieferDatum, TourNr, ArtikelNr, Einheit, " &
                                                "Farbe, Groesse ,MengeInProduktion) VALUES ([0])"
    'Sql-Statement Update Datensatz dbo.ProduktionAktuell
    Public Const mssqlUpdateProduktionAktuell = "UPDATE [dbo].[ProduktionAktuell] SET MengeInProduktion = [4] WHERE " &
                                                "[FilialNr] = [0] And [LieferDatum] = '[1]' AND [TourNr] = [2] AND [ArtikelNr] = '[3]'"

    'Sql-Statement Abfrage dbo.LosArt
    Public Const mssqlLosArt = "Select * FROM [dbo].[LosArten]"

    'Sql-Statement Abfrage dbo.ArtikelLagerkarte
    Public Const mssqlArtikelLagerKarte = "SELECT * FROM [dbo].[ArtikelLagerkarte] WHERE [Lfd] > [0] AND ArtikelNr = '[1]' ORDER BY [Lfd]"
    'Sql-Statement Abfrage dbo.ArtikelLagerkarte letzter Datensatz 
    Public Const mssqlArtikelLagerInit = "SELECT * FROM [dbo].[ArtikelLagerkarte] WHERE ArtikelNr = '[0]' ORDER BY [Lfd] DESC"
    'Sql-Statement Abfrage dbo.ArtikelLagerkarte letzter Datensatz 
    Public Const mssqlArtikelLagerLast = "SELECT TOP(1) * FROM [dbo].[ArtikelLagerkarte] WHERE ArtikelNr = '[0]' ORDER BY [Lfd] DESC"
    'Sql-Statement Abfrage dbo.ChargenBestand sortiert nach AnlageDatum 
    Public Const mssqlArtikelLagerChargen = "SELECT * FROM [dbo].[ChargenBestand] WHERE ArtikelNr ='[0]' AND FilialNr = 0 AND Einheit =[1] AND Bestand > 0 Order by AnlageDatum DESC"

    'Sql-Statement Abfrage Geschäftsvorfall-Positionen nach Vorfall-Kürzel und Vorfall-Nummer
    Public Const mssqlVorfallPositionen = "SELECT * FROM [dbo].[GeschäftsvorfallPosition] WHERE VorfallKürzel = '[0]' AND VorfallNr = '[1]'"

End Class
