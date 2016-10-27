Public Class wb_Sql_Selects

    'Sql-Statement Rezeptliste aus winback.Rezepte
    Public Const sqlRezeptListe = "SELECT RZ_Nr, RZ_Nr_AlNum, RZ_Bezeichnung, RZ_Variante_Nr, RZ_Kommentar, RZ_Gewicht, " &
                                  "RZ_Aenderung_Nr, RZ_Aenderung_Datum, RZ_Aenderung_Name, RZ_Liniengruppe, " &
                                  "RZ_Charge_Min, RZ_Charge_Max, RZ_Charge_Opt FROM Rezepte"
    'Sql-Statement Rezept-Historie aus winback.HisRezepte
    Public Const sqlRezeptHistr = "SELECT H_RZ_Aenderung_Nr, H_RZ_Aenderung_Datum, H_RZ_Aenderung_Name FROM His_Rezepte " &
                                  "WHERE H_RZ_Nr=[0] AND H_RZ_Variante_Nr=[1]"

    'Sql-Statement Rohstoffliste aus winback.Komponenten (KO_Nr als Platzhalter für LG_aktiv)
    Public Const sqlRohstoffLst = "SELECT KO_Nr_AlNum, KO_Bezeichnung, KO_Nr, KO_Kommentar, KO_Type, KA_aktiv, " &
                                  "KA_Kurzname, KA_Matchcode, KA_Preis, KA_Grp1, KA_Grp2 FROM Komponenten WHERE KO_Type <> 0"
    'Sql-Statement RohstoffGruppen aus winback.ItemParameter
    Public Const sqlRohstoffGrp = "SELECT IP_Wert1int, IP_Wert4str FROM ItemParameter WHERE " &
                                  "IP_ItemTyp = 600 AND IP_Wert3int = 0 ORDER BY IP_Lfd_Nr DESC"
    'Sql-Statement Rohstoff-Verwendung aus winback.Rezeptschritte
    Public Const sqlRohstoffUse = "SELECT Rezepte.RZ_Nr_AlNum, Rezepte.RZ_Bezeichnung FROM RezeptSchritte INNER JOIN " &
                                  "Rezepte ON (RezeptSchritte.RS_RZ_Variante_Nr = Rezepte.RZ_Variante_Nr) And (RezeptSchritte.RS_RZ_Nr = Rezepte.RZ_Nr) " &
                                  "WHERE RezeptSchritte.RS_Ko_Nr= [0] And RezeptSchritte.RS_ParamNr = 1"
    'Sql-Statement Automatik-Rohstoffe aus winback.Lagerorte
    Public Const sqlRohstoffAct = "SELECT Komponenten.KO_Nr, Lagerorte.LG_aktiv FROM Komponenten " &
                                  "INNER JOIN Lagerorte ON Komponenten.KA_Lagerort = Lagerorte.LG_Ort " &
                                  "WHERE KO_TYPE = 101 OR KO_TYPE = 103 OR KO_TYPE = 104"

    'Sql-Statement Artikelliste aus winback.Komponenten
    Public Const sqlArtikelLste = "SELECT KO_Nr, KO_Nr_AlNum, KO_Bezeichnung, KA_RZ_Nr, KO_Kommentar, KO_Type, " &
                                  "KA_Kurzname, KA_Matchcode, KA_Grp1, KA_Grp2 FROM Komponenten WHERE KO_Type = 0"

    'Sql-Statement Userliste aus winback.ItemParameter
    Public Const sqlUsersListe = "SELECT IP_ItemTyp, IP_Lfd_Nr, IP_Wert4str, IP_ItemID, IP_Wert1int FROM ItemParameter " &
                                  "WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert1int <> 709760"
    'Sql-Statement User Datensatz neu anlegen   [0]-Name [1]-Password [2]-Gruppe
    Public Const sqlUserInsert = "INSERT INTO ItemParameter (IP_ItemTyp, IP_ItemID, IP_ItemAttr, IP_Lfd_Nr, IP_Wert1int, IP_Wert4str) " &
                                 "VALUES (500, [2], 501, [1], [1], '[0]')"
    'Sql-Statement User Datensatz ändern        [0]-Name [1]-NewPassword [2]-Gruppe [3]-NewPassword
    Public Const sqlUserUpdate = "REPLACE INTO ItemParameter (IP_ItemID, IP_Lfd_Nr, IP_Wert1int, IP_Wert4str) VALUES([2],[1],[1],'[0]') WHERE IP_Wert1Int = [3]"
    'Sql-Statemant User Datensatz löschen
    Public Const sqlUserDelete = "DELETE FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert1int = [0]"
    'Sql-Statemant User mit diesem Passwort existiert
    Public Const sqlUserExists = "SELECT COUNT(*) as IP_Cnt FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert1int = [0]"

    'Sql-Statement Gruppen-Nummer und Gruppen-Bezeichnung aus winback.II_ItemID
    Public Const sqlUserGrpTxt = "SELECT * FROM ItemIDs WHERE II_ItemTyp = 500 ORDER BY II_ItemID"
    'Sql-Statement UserRechte aus winback.ItemParameter
    Public Const sqlUserRechte = "Select ItemTypen.IT_Bezeichnung, ItemIDs.II_Kommentar, AT_Wert2int, Texte.T_Text FROM ItemIDs " &
                                 "INNER JOIN ItemTypen On ItemIDs.II_ItemTyp = ItemTypen.IT_ItemTyp " &
                                 "INNER JOIN ItemParameter On ItemIDs.II_ItemID = ItemParameter.IP_ItemID AND ItemIDs.II_ItemTyp = ItemParameter.IP_ItemTyp " &
                                 "INNER JOIN IAttrParams On ItemParameter.IP_Wert2int = IAttrParams.AT_Wert3str AND ItemParameter.IP_ItemAttr = IAttrParams.AT_Attr_Nr " &
                                 "INNER JOIN Texte On IAttrParams.AT_Wert2int = Texte.T_TextIndex AND IAttrParams.AT_Attr_Nr = Texte.T_Typ " &
                                 "WHERE ItemIDs.II_ItemTyp <= 230 AND ItemParameter.IP_Wert1int = [0] AND  Texte.T_Sprache = [1] " &
                                 "ORDER BY ItemIDs.II_ItemTyp, ItemIDs.II_ItemID;"

    'Sql-Statement alle Texte aus winback.Texte
    Public Const sqlWinBackTxte = "SELECT T_TextIndex, T_Typ, T_Text FROM Texte WHERE T_Sprache = [0]"

    Friend Shared Function setParams(sql As String, Param0 As String, Optional Param1 As String = "-", Optional Param2 As String = "-", Optional Param3 As String = "-") As String
        sql = Replace(sql, "[0]", Param0)
        If Param1 <> "-" Then
            sql = Replace(sql, "[1]", Param1)
        End If
        If Param2 <> "-" Then
            sql = Replace(sql, "[2]", Param2)
        End If
        If Param3 <> "-" Then
            sql = Replace(sql, "[3]", Param3)
        End If
        Return sql
    End Function

End Class
