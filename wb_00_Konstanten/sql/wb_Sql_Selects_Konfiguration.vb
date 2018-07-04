Partial Public Class wb_Sql_Selects
    'Sql-Statement Konfiguration
    Public Const sqlKonfiguration = "SELECT * FROM Konfiguration WHERE KF_Tag LIKE '[0]'"

    'Sql-Statement Liniengruppen
    Public Const sqlLinienGruppen = "SELECT * FROM LinienGruppen ORDER BY LG_Nr"
    'Sql-Statement Linien
    Public Const sqlLinien = "SELECT * FROM Linien ORDER BY L_Nr"
    'Sql-Statement Update(Replace)Linien
    Public Const sqlUpdteLinien = "REPLACE INTO LinienGruppen (LG_Nr, LG_Bezeichnung, LG_Linien) VALUES ([0],'[1]',[0])"

    'Sql-Statement Einheiten
    Public Const sqlEinheiten = "Select * FROM Einheiten ORDER BY E_LfdNr"

    'Sql-Statement E-Nummern
    Public Const sqlENummern = "Select * FROM enummern"

    'Sql-Statement alle Texte aus winback.Texte
    Public Const sqlWinBackTxte = "SELECT T_TextIndex, T_Typ, T_Text FROM Texte WHERE T_Sprache = [0]"

    'Sql-Statement AktionsTimer
    Public Const sqlAktionsTimer = "Select * FROM AktionsTimer WHERE AT_Ziel_Aktion = 1 AND (AT_Quelle_Typ = 'TW' OR AT_Quelle_Typ LIKE 'winback%' OR AT_Quelle_Typ LIKE 'office%')"
    Public Const sqlUpdtAktionsTimer = "UPDATE AktionsTimer SET [0] WHERE AT_idx = '[1]'"

End Class
