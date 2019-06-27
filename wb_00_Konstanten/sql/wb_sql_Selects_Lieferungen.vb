Partial Public Class wb_sql_Selects

    'Sql_statement SELECT nächster Artikel und LG_Ort (LIMIT 1, da mySQL kein SELECT FIRST kennt)
    Public Const sqlRohstoffLagerort = "SELECT Komponenten.KO_Nr, Komponenten.KO_Nr_AlNum, Komponenten.KO_Bezeichnung, Komponenten.KO_Type, " &
                                       "Lagerorte.LG_Silo_Nr, Lagerorte.LG_LF_Nr, Lagerorte.LG_Ort, Lagerorte.LG_Bilanzmenge " &
                                       "FROM (Komponenten INNER JOIN Lagerorte ON Komponenten.KA_Lagerort = Lagerorte.LG_Ort) " &
                                       "WHERE KO_Type <> 0 AND KO_Nr > '[0]' AND KA_aktiv = 1 ORDER BY KO_Nr LIMIT 1"

    'Sql_statement UPDATE Lagerorte.lfd_Nr
    Public Const sqlUpdateLagerort = "UPDATE Lagerorte SET LG_LF_Nr = [0], LG_Bilanzmenge = '[1]' WHERE LG_Ort = '[2]'"

    'Sql_statement SELECT Lagerorte nach LG_Ort
    Public Const sqlLieferungen = "SELECT * FROM Lieferungen WHERE LF_LG_Ort = '[0]' ORDER BY Lieferungen.LF_Nr"

    'Sql_statement SELECT Lagerorte nach LG_Ort und Chargen-Nummer
    Public Const sqlLieferCharge = "SELECT * FROM Lieferungen WHERE LF_LG_Ort = '[0]' AND LF_BF_Charge = '[1]' AND LF_gebucht <> '3'" &
                                  "ORDER BY Lieferungen.LF_Nr DESC"

    'Sql_statement UPDATE Lieferungen nach LF_lfd
    Public Const sqlUpdateLieferung = "UPDATE Lieferungen SET LF_Menge = '[2]' WHERE LF_LG_Ort = '[0]' AND LF_Nr = [1]"

    'Sql_statement INSERT Lieferungen (Wareneingang)
    Public Const sqlInsertWE = "INSERT INTO Lieferungen(LF_LG_Ort, LF_Nr, LF_Datum, LF_Menge, LF_Lieferant, LF_gebucht, LF_Bemerkung, LF_Lager, " &
                               "LF_BF_Charge, LF_Liniengruppe, LF_BF_Frist, LF_Verbrauch, LF_User_Nr, LF_Preis, LF_PreisEinheit) VALUES([0])"

    'Löschen Rohstoff/Artikel in Lieferungen
    Public Const sqlDelLieferungen = "DELETE FROM Lieferungen WHERE LF_LG_Ort = '[0]'"

End Class
