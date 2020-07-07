Partial Public Class wb_sql_Selects

    'Sql_statement Select Lagerorte nach LG_Ort
    Public Const sqlLagerOrte = "SELECT * FROM Lagerorte WHERE LG_Ort = '[0]'"
    'Löschen Rohstoff/Artikel in Lagerorte
    Public Const sqlDelLagerOrte = "DELETE FROM Lagerorte WHERE LG_Ort = '[0]'"
    'Sql-Statement Update LagerOrte nach LG_Ort
    Public Const sqlUpdateLagerOrte = "UPDATE Lagerorte SET [1] WHERE LG_Ort = '[0]'"
    'Sql-Statement Insert LagerOrte nach LG_Ort
    Public Const sqlInsertLagerOrte = "INSERT IGNORE INTO Lagerorte(LG_Ort, LG_Weg_Nr, LG_TempFuehler_Nr, LG_Bilanzmenge, LG_Aktiv, LG_LF_Nr) VALUES('[0]', 0, 0, 0.0, 'A', -1)"

    'Sql-Statement LagerOrte Silo-Gruppen
    Public Const sqlSiloGrpLager = "SELECT * FROM Lagerorte WHERE LG_Ort LIKE '[0]'"

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
