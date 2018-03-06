Partial Public Class wb_Sql_Selects
    'Sql-Statement Konfiguration
    Public Const sqlKonfiguration = "SELECT * FROM Konfiguration WHERE KF_Tag LIKE '[0]'"

    'Sql-Statement Liniengruppen
    Public Const sqlLinienGruppen = "SELECT * FROM LinienGruppen"

    'Sql-Statement E-Nummern
    Public Const sqlENummern = "Select * FROM enummern"

    'Sql-Statement alle Texte aus winback.Texte
    Public Const sqlWinBackTxte = "SELECT T_TextIndex, T_Typ, T_Text FROM Texte WHERE T_Sprache = [0]"

End Class
