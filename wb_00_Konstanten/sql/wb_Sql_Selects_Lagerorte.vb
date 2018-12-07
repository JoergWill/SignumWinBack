Partial Public Class wb_sql_Selects

    'Sql_statement Select Lagerorte nach LG_Ort
    Public Const sqlLagerOrte = "SELECT * FROM Lagerorte WHERE LG_Ort = '[0]'"
    'Löschen Rohstoff/Artikel in Lagerorte
    Public Const sqlDelLagerOrte = "DELETE FROM Lagerorte WHERE LG_Ort = '[0]'"
    'Sql-Statement Update LagerOrte nach LG_Ort
    Public Const sqlUpdateLagerOrte = "UPDATE Lagerorte SET [1] WHERE LG_Ort = '[0]'"

End Class
