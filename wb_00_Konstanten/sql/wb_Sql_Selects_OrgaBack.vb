Partial Public Class wb_Sql_Selects

    'Sql-Statement Abfrage Vorname, Nachname und Nummer aus [OrgaBackMain].[dbo].[Mitarbeiter]"
    Public Const mssqlMitarbeiterMFF500 = "SELECT * FROM [dbo].[Mitarbeiter] LEFT JOIN MitarbeiterHatMultiFeld " &
                                    "ON Mitarbeiter.MitarbeiterKürzel = MitarbeiterHatMultiFeld.MitarbeiterKürzel " &
                                    "WHERE MitarbeiterHatMultiFeld.FeldNummer = 500 ORDER BY KassiererNummer"
    Public Const mssqlMitarbeiter = "SELECT * FROM [dbo].[Mitarbeiter] LEFT JOIN MitarbeiterHatMultiFeld " &
                                    "ON Mitarbeiter.MitarbeiterKürzel = MitarbeiterHatMultiFeld.MitarbeiterKürzel ORDER BY KassiererNummer"


    'Sql-Statement Abfrage Gruppen-Nr(Hierarchie) und Gruppen-Bezeichnung aus [OrgaBackMain].[dbo].[MitarbeiterMultiFunktionsFeld]"
    Public Const mssqlMitarbeiterGruppen = "SELECT * FROM [dbo].[MitarbeiterMultiFunktionsFeld] WHERE GruppenNr=1 ORDER BY Hierarchie"
    'Sql-Statement Update Gruppen-Bezeichnung aus [OrgaBackMain].[dbo].[MitarbeiterMultiFunktionsFeld]"
    Public Const mssqlUpdateMitarbeiterGruppen = "UPDATE [dbo].[MitarbeiterMultiFunktionsFeld] SET Bezeichnung = '[1]' WHERE GruppenNr=1 AND Hierarchie='[0]'"
    'Sql-Statement Insert Mitarbeiter-Gruppe [OrgaBackMain].[dbo].[MitarbeiterMultiFunktionsFeld]"
    Public Const mssqlInsertMitarbeiterGruppen = "INSERT INTO [dbo].[MitarbeiterMultiFunktionsFeld] (GruppenNr, Hierarchie, Bezeichnung) VALUES ('[2]','[0]','[1]')"

    'Sql-Statement Artikel
    Public Const mssqlArtikel = "SELECT ArtikelNr,Kurztext,Sortiment FROM [dbo].[Artikel] WHERE Artikelgruppe = [0] ORDER BY ArtikelNr"

    'Sql-Statement Filiale zugeordnet zu Produktion (Typ=4)
    Public Const mssqlFiliale = "Select Filialnummer, Name1 FROM Filialen WHERE [Typ] = [0]"
    'Sql-Statament Sortiment zugeordnet zu Filiale mit Typ Produktion
    Public Const mssqlSortiment = "Select * FROM FilialeHatSortiment INNER JOIN Filialen On FilialeHatSortiment.Filialnr =  Filialen.Filialnummer " &
                                  "WHERE [Typ] = [0] ORDER BY SortimentsKürzel"

    'Sql-Statement Abfrage dbo.Settings.Category
    Public Const mssqlSettings = "Select * FROM Settings WHERE [Category] = '[0]'"
    'Sql-Statement Abfrage dbo.WorkStations.Computername
    Public Const mssqlWrkStations = "SELECT * FROM WorkStations WHERE [ComputerName] = '[0]'"
    'Sql-Statement Abfrage dbo.SystemInfo
    Public Const mssqlSystemInfo = "SELECT TOP(1) * FROM SystemInfo ORDER BY lfd DESC"

    'Sql-Statement Insert Produktions/Verbrauchsdaten in [OrgaBackMain].[dbo].[ProduzierteWare]"
    Public Const mssqlInsertProduktionsDaten = "INSERT INTO [dbo].[ProduzierteWare] (FilialNr, ProduktionsDatum, SatzTyp, ArtikelNr, " &
                                               "Einheit, Farbe, Groesse, Menge, ChargenNr, HaltbarkeitsDatum) VALUES ([0])"

End Class
