Partial Public Class wb_Sql_Selects
    'Sql-Statement Userliste aus winback.ItemParameter
    Public Const sqlUsersListe = "Select IP_ItemTyp, IP_Lfd_Nr, IP_Wert4str, IP_ItemID, IP_Wert1int FROM ItemParameter " &
                                  "WHERE IP_ItemTyp = 500 And IP_ItemAttr = 501 And IP_Wert1int <> " & wb_Credentials.WinBackMasterUser
    'Sql-Statement User Datensatz neu anlegen   [0]-Name [1]-Password [2]-Gruppe
    Public Const sqlUserInsert = "INSERT INTO ItemParameter (IP_ItemTyp, IP_ItemID, IP_ItemAttr, IP_Lfd_Nr, IP_Wert1int, IP_Wert4str) " &
                                 "VALUES (500, [2], 501, [1], [1], '[0]')"
    'Sql-Statement User Datensatz ändern        [0]-Name [1]-NewPassword [2]-Gruppe [3]-NewPassword
    Public Const sqlUserUpdate = "UPDATE ItemParameter SET IP_ItemID = [2], IP_Lfd_Nr = [1], IP_Wert1int = [1], IP_Wert4str = '[0]' " &
                                 "WHERE IP_Wert1Int = [3] AND IP_ItemTyp = 500 AND IP_ItemAttr = 501"
    'Sql-Statement User Datensatz ändern        [0]-Password [1]-Sprache
    Public Const sqlUserUpdateLang = "UPDATE ItemParameter SET IP_Wert5str = '[1]' WHERE IP_Wert1Int = [0] AND IP_ItemTyp = 500 AND IP_ItemAttr = 501"
    'Sql-Statement User Datensatz löschen
    Public Const sqlUserDelete = "DELETE FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert1int = [0]"
    'Sql-Statement User mit diesem Passwort existiert
    Public Const sqlUserExists = "SELECT COUNT(*) as IP_Cnt FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert1int = [0]"

    'Sql-Statement Gruppen-Nummer und Gruppen-Bezeichnung aus winback.II_ItemID
    Public Const sqlUserGrpTxt = "SELECT * FROM ItemIDs WHERE II_ItemTyp = 500 ORDER BY II_ItemID"
    'Sql-Statement UserRechte aus winback.ItemParameter
    Public Const sqlUserRechte = "Select ItemTypen.IT_Bezeichnung, ItemIDs.II_Kommentar, IP_Wert2int, AT_Wert2int, AT_Attr_Nr, Texte.T_Text, IP_ItemTyp, IP_ItemID FROM ItemIDs " &
                                 "INNER JOIN ItemTypen On ItemIDs.II_ItemTyp = ItemTypen.IT_ItemTyp " &
                                 "INNER JOIN ItemParameter On ItemIDs.II_ItemID = ItemParameter.IP_ItemID AND ItemIDs.II_ItemTyp = ItemParameter.IP_ItemTyp " &
                                 "INNER JOIN IAttrParams On ItemParameter.IP_Wert2int = IAttrParams.AT_Wert3str AND ItemParameter.IP_ItemAttr = IAttrParams.AT_Attr_Nr " &
                                 "INNER JOIN Texte On IAttrParams.AT_Wert2int = Texte.T_TextIndex AND IAttrParams.AT_Attr_Nr = Texte.T_Typ " &
                                 "WHERE ItemIDs.II_ItemTyp <= 230 AND ItemParameter.IP_Wert1int = [0] AND  Texte.T_Sprache = [1] " &
                                 "ORDER BY ItemIDs.II_ItemTyp, ItemIDs.II_ItemID;"
    'Sql-Statement UserGruppenRechte aus winback.ItemParameter (IP_ItemTyp=2 NUR PROGRAMM-RECHTE)
    Public Const sqlUserGrpRechte = "Select * FROM ItemParameter WHERE IP_ItemTyp = 2 AND IP_Wert1int = [0] ORDER BY IP_ItemID;"
    'Sql-Statement UserGruppenRechte löschen
    Public Const sqlUserGrpRemoveAll = "DELETE FROM ItemParameter WHERE IP_ItemTyp >= 1 AND IP_ItemTyp <= 200"
    'Sql-Statement UserGruppenRechte einfügen
    Public Const sqlUserGrpInsert = "INSERT INTO ItemParameter (IP_ItemTyp, IP_ItemID, IP_ItemAttr, IP_Lfd_Nr, IP_Wert1int, IP_Wert2int) VALUES ([0])"

    'Sql-Statement User Datensatz lesen
    Public Const sqlUserLogin = "SELECT * FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert1int = [0]"
    'Sql-Statement User Datensatz lesen
    Public Const sqlUserName = "SELECT * FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert4str = '[0]'"

    'Sql-Statement Liste aller Usergruppen
    Public Const sqlUserGrpListe = "SELECT * FROM IAListe INNER JOIN Texte ON IAL_ItemID = T_TextIndex AND IAL_ItemTyp = T_Typ " &
                                   "WHERE IAL_ItemTyp = 500 AND T_SPRACHE = [0] ORDER BY IAL_ItemID"
    'Sql-Statement Liste Rechte-Parameter mit Texten
    Public Const sqlUserRechteParam = "SELECT * FROM IAttrParams INNER JOIN Texte ON AT_Wert1int = T_Typ AND AT_Wert2int = T_TextIndex " &
                                      "WHERE T_SPRACHE = [0] ORDER BY AT_Wert1int, AT_Wert2int"


End Class
