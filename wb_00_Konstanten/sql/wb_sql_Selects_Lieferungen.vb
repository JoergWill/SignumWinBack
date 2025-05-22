Partial Public Class wb_sql_Selects

    'Sql-Statement SELECT Lagerorte nach LG_Ort
    Public Const sqlLieferungen = "SELECT * FROM Lieferungen WHERE LF_LG_Ort = '[0]' ORDER BY Lieferungen.LF_Nr"
    'Sql-Statement Chargen-Nummern aller Lieferungen zur Rohstoff-Nummer sortiert nach Lieferdatum (Silo/Hand-Rohstoffe)
    Public Const sqlLieferungenDesc = "Select KO_Nr, KA_aktiv, KO_Type, KO_Bezeichnung, KO_Nr_AlNum, LF_LG_Ort, LF_Datum, LF_Nr, LF_Menge, LF_BF_Charge, LF_Timestamp " &
                                      "FROM Komponenten INNER JOIN Lieferungen On Komponenten.KA_Lagerort = Lieferungen.LF_LG_Ort " &
                                      "WHERE ((Komponenten.KA_aktiv = 1) AND (Komponenten.KO_Nr_AlNum = '[0]')) ORDER BY Lieferungen.LF_Datum DESC"

    'Sql-Statement SELECT Lagerorte nach LG_Ort und Status
    Public Const sqlLieferungenGebucht_ASC = "SELECT * FROM Lieferungen WHERE LF_LG_Ort = '[0]' AND (LF_MENGE + 1) >= (LF_Verbrauch + 1) AND LF_gebucht= '[1]' ORDER BY Lieferungen.LF_Nr ASC LIMIT 1"
    Public Const sqlLieferungenGebucht_DESC = "SELECT * FROM Lieferungen WHERE LF_LG_Ort = '[0]' AND LF_gebucht= '[1]' ORDER BY Lieferungen.LF_Nr DESC LIMIT 1"
    'Sql-Statement SELECT Lagerorte nach LG_Ort und Verbrauch > Liefermenge !!
    Public Const sqlLieferungenGebucht = "SELECT * FROM Lieferungen WHERE LF_LG_Ort = '[0]' AND (LF_MENGE + 1) < (LF_Verbrauch + 1) AND LF_gebucht= '2' ORDER BY Lieferungen.LF_Nr"

    'Sql-Statement SELECT Lagerorte nach LG_Ort und Status
    Public Const sqlLieferungenAktiv = "SELECT * FROM Lieferungen WHERE LF_LG_Ort = '[0]' AND LF_gebucht = '[1]' AND ORDER BY Lieferungen.LF_Nr [2] LIMIT 1"
    'Sql-Statement SELECT Lagerorte nach LG_Ort letzter Eintrag
    Public Const sqlLieferLfd = "SELECT * FROM Lieferungen WHERE LF_LG_Ort = '[0]' ORDER BY Lieferungen.LF_Nr DESC LIMIT 1"

    'Sql-Statement SELECT Lagerorte nach LG_Ort und Chargen-Nummer
    Public Const sqlLieferCharge = "SELECT * FROM Lieferungen WHERE LF_LG_Ort = '[0]' AND LF_BF_Charge = '[1]' ORDER BY Lieferungen.LF_Nr DESC"

    'Sql-Statement UPDATE Lieferungen nach LF_lfd
    Public Const sqlReadLieferMenge = "SELECT LF_Menge FROM Lieferungen WHERE LF_LG_Ort = '[0]' AND LF_Nr = [1]"
    Public Const sqlUpdateLieferung = "UPDATE Lieferungen SET LF_Verbrauch = [2], LF_Bemerkung = '[3]', LF_gebucht = '[4]' WHERE LF_LG_Ort = '[0]' AND LF_Nr = [1]"
    Public Const sqlUpdateLieferungVerbraucht = "UPDATE Lieferungen SET LF_Verbrauch = LF_Menge, LF_Bemerkung = '[2]', LF_gebucht = '3' WHERE LF_LG_Ort = '[0]' AND LF_Nr = [1]"

    'Sql-Statement UPDATE Lieferungen nach LF_lfd
    Public Const sqlUpdateVerbrauch = "UPDATE Lieferungen SET LF_Verbrauch = '[2]', LF_gebucht = '[3]' WHERE LF_LG_Ort = '[0]' AND LF_Nr = [1]"

    'Sql-Statement UPDATE Lieferungen nach LG_Ort
    Public Const sqlSetStatusLieferung = "UPDATE Lieferungen SET LF_Gebucht = '3', LF_Verbrauch = LF_Menge WHERE LF_LG_Ort = '[0]'"
    Public Const sqlUpdateStatusLieferung = "UPDATE Lieferungen SET LF_Gebucht = '3' WHERE LF_LG_Ort = '[0]' AND LF_Gebucht = '2'"

    'Sql-Statement INSERT Lieferungen (Wareneingang)
    Public Const sqlInsertWE = "INSERT INTO Lieferungen(LF_LG_Ort, LF_Nr, LF_Datum, LF_Menge, LF_Lieferant, LF_gebucht, LF_Bemerkung, LF_Lager, " &
                               "LF_BF_Charge, LF_Liniengruppe, LF_BF_Frist, LF_Verbrauch, LF_User_Nr, LF_Preis, LF_PreisEinheit) VALUES([0])"

    'Löschen Rohstoff/Artikel in Lieferungen
    Public Const sqlDelLieferungen = "DELETE FROM Lieferungen WHERE LF_LG_Ort = '[0]'"

End Class
