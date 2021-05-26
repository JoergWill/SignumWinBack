Partial Public Class wb_sql_Selects

    'Sql_statement Select Lagerorte nach LG_Ort
    Public Const sqlLagerOrte = "SELECT * FROM Lagerorte WHERE LG_Ort = '[0]'"
    'Sql_statement UPDATE Lagerorte.lfd_Nr
    Public Const sqlUpdateLagerort = "UPDATE Lagerorte SET LG_LF_Nr = [0], LG_Bilanzmenge = '[1]' WHERE LG_Ort = '[2]'"
    'Sql_statement UPDATE Bilanzmenge
    'die Bilanzmenge wird innerhalb des sql-Statements neu berechnet. Die Verbrauchsmenge muss 
    'als Dezimalwert mit Dezimalpunkt(!) übergeben werden
    Public Const sqlUpateBilanzmenge = "UPDATE Lagerorte SET LG_Bilanzmenge = LG_Bilanzmenge + [0] WHERE LG_ORT = '[1]';"

    'Löschen Rohstoff/Artikel in Lagerorte
    Public Const sqlDelLagerOrte = "DELETE FROM Lagerorte WHERE LG_Ort = '[0]'"
    'Sql-Statement Update LagerOrte nach LG_Ort
    Public Const sqlUpdateLagerOrte = "UPDATE Lagerorte SET [1] WHERE LG_Ort = '[0]'"
    'Sql-Statement Insert LagerOrte nach LG_Ort
    Public Const sqlInsertLagerOrte = "INSERT IGNORE INTO Lagerorte(LG_Ort, LG_Weg_Nr, LG_TempFuehler_Nr, LG_Bilanzmenge, LG_Aktiv, LG_LF_Nr) VALUES('[0]', 0, 0, 0.0, 'A', -1)"

    'Sql-Statement LagerOrte Silo-Gruppen
    Public Const sqlSiloGrpLager = "SELECT * FROM Lagerorte WHERE LG_Ort LIKE '[0]'"
    'Sql-Statement LagerOrte Silo-Gruppen nach Komponenten-Nummer
    Public Const sqlSiloGrpNummer = "SELECT * FROM Komponenten INNER JOIN Lagerorte ON Komponenten.KA_Lagerort = Lagerorte.LG_Ort " &
                                    "WHERE ((KA_aktiv=1) AND (KO_Type=101) AND (KO_Nr_AlNum = '[0]')) ORDER BY Lagerorte.LG_Ort "

    'Sql_statement SELECT nächster Artikel und LG_Ort (LIMIT 1, da mySQL kein SELECT FIRST kennt) LG_Timestamp ist als Dummy-Feld notwendig
    Public Const sqlRohstoffLagerort = "SELECT Komponenten.KO_Nr, Komponenten.KO_Nr_AlNum, Komponenten.KO_Bezeichnung, Komponenten.KO_Type, " &
                                       "Lagerorte.LG_Silo_Nr, Lagerorte.LG_LF_Nr, Lagerorte.LG_Ort, Lagerorte.LG_Bilanzmenge, Lagerorte.LG_Timestamp " &
                                       "FROM (Komponenten INNER JOIN Lagerorte ON Komponenten.KA_Lagerort = Lagerorte.LG_Ort) " &
                                       "WHERE KO_Type <> 0 AND [0] AND KA_aktiv = 1 ORDER BY KO_Type, KO_Nr LIMIT 1"

    'Prüfen ob Datenbankfeld KA_zaehlt_zu_NWT_Gesamtmenge vorhanden ist
    Public Const sqlCheck_LG_LF_Nr = "DESCRIBE Lagerorte LG_LF_Nr"



    'LG_Ort
    'LG_Bezeichnung
    'LG_Weg_Nr
    'LG_TempFuehler_Nr
    'LG_Bilanzmenge
    'LG_Silo_Nr
    'LG_aktiv
    'LG_Status
    'LG_Kommentar
    'LG_Befuell_varianten
    'LG_Mindestmenge
    'LG_max_Dosierfehler
    'LG_LF_Nr
    'LG_Timestamp

End Class
