Partial Public Class wb_Sql_Selects
    'Sql-Statement Userliste aus winback.ItemParameter
    Public Const sqlUsersListe = "Select IP_ItemTyp, IP_Lfd_Nr, IP_Wert4str, IP_ItemID, IP_Wert1int, IP_Wert2int, IP_Wert5str FROM ItemParameter " &
                                  "WHERE IP_ItemTyp = 500 And IP_ItemAttr = 501 And IP_Wert1int <> " & wb_Credentials.WinBackMasterUser
    'Sql-Statement User Datensatz neu anlegen   [0]-Name [1]-Password [2]-Gruppe
    Public Const sqlUserInsert = "INSERT INTO ItemParameter (IP_ItemTyp, IP_ItemID, IP_ItemAttr, IP_Lfd_Nr, IP_Wert1int, IP_Wert2int, IP_Wert4str) " &
                                 "VALUES (500, '[2]', 501, [3], [1], [1], '[0]')"
    'Sql-Statement User Datensatz ändern        [0]-Name [1]-NewPersonalNr [2]-OldPersonalNr
    Public Const sqlUserPNrUpdate = "UPDATE ItemParameter SET IP_Wert2int = [1] WHERE ((IP_Wert2Int = [2] AND IP_Wert2Int <> 0) OR IP_Wert4Str = '[0]') AND IP_ItemTyp = 500 AND IP_ItemAttr = 501"
    'Sql-Statement User Datensatz ändern        [0]-Name [1]-NewPersonalNr [2]-Gruppe [3]-OldPersonalNr
    Public Const sqlUserUpdate = "UPDATE ItemParameter SET IP_Wert2int = [1], IP_Wert4str = '[0]' " &
                                 "WHERE IP_Wert2Int = [2] AND IP_ItemTyp = 500 AND IP_ItemAttr = 501"
    'Sql-Statement User Datensatz ändern        [0]-Password [1]-Sprache
    Public Const sqlUserUpdateLang = "UPDATE ItemParameter SET IP_Wert5str = '[1]' WHERE IP_Wert1Int = [0] AND IP_ItemTyp = 500 AND IP_ItemAttr = 501"
    'Sql-Statement User ohne Personalnummer(OrgaBack)
    Public Const sqlUserOhnePersonalNummer = "SELECT IP_Lfd_Nr FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert2int = 0"
    'Sql-Statement User Datensatz löschen
    Public Const sqlOrgaBackUserDelete = "DELETE FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert2int = [0]"
    'Sql-Statement User Datensatz löschen
    Public Const sqlWinBackUserDelete = "DELETE FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert1int = [0]"
    'Sql-Statement User mit diesem Passwort existiert
    Public Const sqlUserExists = "SELECT COUNT(*) as IP_Cnt FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND (IP_Wert1int = [0] OR IP_Lfd_Nr = [0])"
    'Sql-Statement User mit dieser ID existiert
    Public Const sqlUserExistsID = "SELECT COUNT(*) as IP_Cnt FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert5Str = '[0]'"
    'Sql-Statement User Anzahl
    Public Const sqlUserCount = "SELECT COUNT(*) as Anzahl FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501"
    'Sql-Statement Test Einträge RezeptgruppenRechte in ItemParameter
    Public Const sqlChkRzptGrpRechte = "SELECT COUNT(*) as IP_Cnt FROM ItemParameter WHERE IP_ItemTyp = 230 AND IP_Wert1int = 99"

    'Sql-Statement Test Einträge UserGruppenRechte in ItemParameter
    Public Const sqlChk99ItemParameter = "SELECT COUNT(*) as IP_Cnt FROM ItemParameter WHERE IP_ItemTyp < 230 AND IP_Wert1int = 99"
    'Sql-Statement Test Einträge UserGruppenRechte in IAListe
    Public Const sqlChk99IAListe = "SELECT COUNT(*) as IA_Cnt FROM IAListe WHERE IAL_ItemTyp < 230"
    'Sql-Statement Test Einträge UserGruppenRechte in ItemIDs
    Public Const sqlChk99ItemIDs = "SELECT COUNT(*) as ID_Cnt FROM ItemIDs WHERE II_ItemTyp < 230"

    'Sql-Statement Gruppen-Nummer und Gruppen-Bezeichnung aus winback.II_ItemID
    Public Const sqlUserGrpTxt = "SELECT * FROM ItemIDs INNER JOIN Texte ON ItemIDs.II_ItemID = Texte.T_TextIndex AND " &
                                 "ItemIDs.II_ItemTyp = Texte.T_Typ WHERE II_ItemTyp = 500 AND Texte.T_Sprache = [0] ORDER BY II_ItemID"
    'Sql-Statement UserRechte aus winback.ItemParameter
    Public Const sqlUserRechte = "Select ItemTypen.IT_Bezeichnung, ItemIDs.II_Kommentar, IP_Lfd_Nr, IP_Wert1int, IP_Wert2int, AT_Wert2int, AT_Attr_Nr, Texte.T_Text, IP_ItemTyp, IP_ItemID FROM ItemIDs " &
                                 "INNER JOIN ItemTypen On ItemIDs.II_ItemTyp = ItemTypen.IT_ItemTyp " &
                                 "INNER JOIN ItemParameter On ItemIDs.II_ItemID = ItemParameter.IP_ItemID And ItemIDs.II_ItemTyp = ItemParameter.IP_ItemTyp " &
                                 "INNER JOIN IAttrParams On ItemParameter.IP_Wert2int = IAttrParams.AT_Wert3str And ItemParameter.IP_ItemAttr = IAttrParams.AT_Attr_Nr " &
                                 "INNER JOIN Texte On IAttrParams.AT_Wert2int = Texte.T_TextIndex And IAttrParams.AT_Attr_Nr = Texte.T_Typ " &
                                 "WHERE ItemIDs.II_ItemTyp < 230 And ItemParameter.IP_Wert1int = [0] And  Texte.T_Sprache = [1] " &
                                 "ORDER BY ItemIDs.II_ItemTyp ASC, ItemIDs.II_ItemID ASC , IP_Lfd_Nr DESC;"

    'Sql-Statement RezeptgruppenRechte aus winback.ItemParameter
    Public Const sqlRezUserRechte = "Select ItemTypen.IT_Bezeichnung, ItemIDs.II_Kommentar, IP_Lfd_Nr, IP_Wert1int, IP_Wert2int, AT_Wert2int, AT_Attr_Nr, Texte.T_Text, IP_ItemTyp, IP_ItemID FROM ItemIDs " &
                                 "INNER JOIN ItemTypen On ItemIDs.II_ItemTyp = ItemTypen.IT_ItemTyp " &
                                 "INNER JOIN ItemParameter On ItemIDs.II_ItemID = ItemParameter.IP_ItemID And ItemIDs.II_ItemTyp = ItemParameter.IP_ItemTyp " &
                                 "INNER JOIN IAttrParams On ItemParameter.IP_Wert2int = IAttrParams.AT_Wert3str And ItemParameter.IP_ItemAttr = IAttrParams.AT_Attr_Nr " &
                                 "INNER JOIN Texte On IAttrParams.AT_Wert2int = Texte.T_TextIndex And IAttrParams.AT_Attr_Nr = Texte.T_Typ " &
                                 "WHERE ItemIDs.II_ItemTyp = 230 And ItemParameter.IP_Wert1int = [0] And  Texte.T_Sprache = [1] " &
                                 "ORDER BY ItemIDs.II_ItemTyp, ItemIDs.II_ItemID;"



    'Sql-Statement UserGruppenRechte aus winback.ItemParameter (IP_ItemTyp=2 NUR PROGRAMM-RECHTE)
    Public Const sqlUserGrpRechte = "Select * FROM ItemParameter WHERE IP_ItemTyp = 2 And IP_Wert1int = [0] ORDER BY IP_ItemID;"
    'Sql-Statement UserGruppenRechte löschen
    Public Const sqlUserGrpRemoveAll = "DELETE FROM ItemParameter WHERE IP_ItemTyp >= 1 And IP_ItemTyp <= 200 And IP_Wert1int > 0"
    'Sql-Statement UserRezeptGruppenRechte löschen
    Public Const sqlUserRezGrpRemoveAll = "DELETE FROM ItemParameter WHERE IP_ItemTyp = 230 And IP_Wert1int > 0"
    'Sql-Statement UserGruppenRechte einfügen
    Public Const sqlUserGrpInsert = "INSERT INTO ItemParameter (IP_ItemTyp, IP_ItemID, IP_ItemAttr, IP_Lfd_Nr, IP_Wert1int, IP_Wert2int) VALUES ([0])"

    'Sql-Statement Texte UserGruppen löschen
    Public Const sqlUserGrpTexteRemove = "DELETE FROM Texte WHERE T_Typ = 500 And T_Sprache = [0]"
    'Sql-Statement Texte UserGruppen einfügen
    Public Const sqlUserGrpTexteInsert = "INSERT INTO Texte (T_TextIndex, T_Typ, T_Sprache, T_Text, T_Flags) VALUES ([1], 500, [0], '[2]', 100)"


    'Sql-Statement User Datensatz lesen
    Public Const sqlUserLogin = "Select * FROM ItemParameter WHERE IP_ItemTyp = 500 And IP_ItemAttr = 501 And IP_Wert1int = [0]"
    'Sql-Statement User Datensatz lesen
    Public Const sqlUserName = "Select * FROM ItemParameter WHERE IP_ItemTyp = 500 And IP_ItemAttr = 501 And IP_Wert4str = '[0]'"

    'Sql-Statement Liste aller Usergruppen
    Public Const sqlUserGrpListeA = "SELECT * FROM IAListe INNER JOIN Texte ON IAL_ItemID = T_TextIndex AND IAL_ItemTyp = T_Typ " &
                                   "WHERE IAL_ItemTyp = 500 AND T_SPRACHE = [0] ORDER BY IAL_ItemID"
    Public Const sqlUserGrpListeB = "SELECT * FROM IAListe WHERE IAL_ItemTyp = 500 ORDER BY IAL_ItemID"

    'Sql-Statement Liste Rechte-Parameter mit Texten
    Public Const sqlUserRechteParam = "SELECT * FROM IAttrParams INNER JOIN Texte ON AT_Wert1int = T_Typ AND AT_Wert2int = T_TextIndex " &
                                      "WHERE T_SPRACHE = [0] ORDER BY AT_Wert1int, AT_Wert2int"

    'Sql-Statement User Datensatz Drucken Teigliste/Aufarbeitungsliste lesen
    Public Const sqlPlanungListe = "SELECT *  FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr > 501 AND IP_Lfd_Nr = [0] ORDER BY IP_ItemAttr"
    'Sql-Statement User Datensatz Drucken Teigliste/Aufarbeitungsliste
    Public Const sqlPlanungListeDelete = "DELETE FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr > 501 AND IP_Lfd_Nr = [0]"
    'Sql-Statement User Datensatz Drucken
    Public Const sqlPlanungListeInsert = "INSERT INTO ItemParameter (IP_ItemTyp, IP_ItemID, IP_ItemAttr, IP_Lfd_Nr, IP_Wert5Str) VALUES (500, 0, [0], [1], '[2]')"

End Class
