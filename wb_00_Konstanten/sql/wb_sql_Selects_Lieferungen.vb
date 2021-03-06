﻿Partial Public Class wb_sql_Selects

    'Sql_statement SELECT Lagerorte nach LG_Ort
    Public Const sqlLieferungen = "SELECT * FROM Lieferungen WHERE LF_LG_Ort = '[0]' ORDER BY Lieferungen.LF_Nr"
    'Sql_statement SELECT Lagerorte nach LG_Ort und Status
    Public Const sqlLieferungenGebucht = "SELECT * FROM Lieferungen WHERE LF_LG_Ort = '[0]' AND (LF_MENGE + 1) < (LF_Verbrauch + 1) AND LF_gebucht= '2' ORDER BY Lieferungen.LF_Nr"
    'Sql_statement SELECT Lagerorte nach LG_Ort und Status
    Public Const sqlLieferungenAktiv = "SELECT * FROM Lieferungen WHERE LF_LG_Ort = '[0]' AND LF_gebucht = '[1]' AND ORDER BY Lieferungen.LF_Nr [2] LIMIT 1"
    'Sql_statement SELECT Lagerorte nach LG_Ort letzter Eintrag
    Public Const sqlLieferLfd = "SELECT * FROM Lieferungen WHERE LF_LG_Ort = '[0]' ORDER BY Lieferungen.LF_Nr DESC LIMIT 1"

    'Sql_statement SELECT Lagerorte nach LG_Ort und Chargen-Nummer
    Public Const sqlLieferCharge = "SELECT * FROM Lieferungen WHERE LF_LG_Ort = '[0]' AND LF_BF_Charge = '[1]' AND LF_gebucht <> '3'" &
                                  "ORDER BY Lieferungen.LF_Nr DESC"

    'Sql_statement UPDATE Lieferungen nach LF_lfd
    Public Const sqlUpdateLieferung = "UPDATE Lieferungen SET LF_Menge = '[2]', LF_Bemerkung = '[3]' WHERE LF_LG_Ort = '[0]' AND LF_Nr = [1]"

    'Sql_statement UPDATE Lieferungen nach LF_lfd
    Public Const sqlUpdateVerbrauch = "UPDATE Lieferungen SET LF_Verbrauch = '[2]', LF_gebucht = '[3]' WHERE LF_LG_Ort = '[0]' AND LF_Nr = [1]"

    'Sql_statement UPDATE Lieferungen nach LG_Ort
    Public Const sqlSetStatusLieferung = "UPDATE Lieferungen SET LF_Gebucht = '3' WHERE LF_LG_Ort = '[0]'"
    Public Const sqlUpdateStatusLieferung = "UPDATE Lieferungen SET LF_Gebucht = '3' WHERE LF_LG_Ort = '[0]' AND LF_Gebucht = '2'"

    'Sql_statement INSERT Lieferungen (Wareneingang)
    Public Const sqlInsertWE = "INSERT INTO Lieferungen(LF_LG_Ort, LF_Nr, LF_Datum, LF_Menge, LF_Lieferant, LF_gebucht, LF_Bemerkung, LF_Lager, " &
                               "LF_BF_Charge, LF_Liniengruppe, LF_BF_Frist, LF_Verbrauch, LF_User_Nr, LF_Preis, LF_PreisEinheit) VALUES([0])"

    'Löschen Rohstoff/Artikel in Lieferungen
    Public Const sqlDelLieferungen = "DELETE FROM Lieferungen WHERE LF_LG_Ort = '[0]'"

End Class
