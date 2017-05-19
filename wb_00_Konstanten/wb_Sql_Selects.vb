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
    Public Const sqlUserUpdate = "UPDATE ItemParameter SET IP_ItemID = [2], IP_Lfd_Nr = [1], IP_Wert1int = [1], IP_Wert4str = '[0]' " &
                                 "WHERE IP_Wert1Int = [3] AND IP_ItemTyp = 500 AND IP_ItemAttr = 501"
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

    'Sql-Statement Lesen Komponenten nach KO_Nr (Select KO_Nr=x)
    Public Const sqlSelectKomp_KO_Nr = "SELECT * FROM Komponenten WHERE KO_Nr = [0] "
    Public Const sqlSelectKomp_AlNum = "SELECT * FROM Komponenten WHERE KO_Nr_AlNum = '[0]' "
    'Sql-Statement Update Komponenten nach KO_Nr (Select KO_Nr=x)
    Public Const sqlUpdateKomp_KO_Nr = "UPDATE Komponenten SET [1] WHERE KO_Nr = [0] "


    'Sql-Statement Test wb_ktTypX (Select KO_Nr=x)
    Public Const sqlTestktTypX = "SELECT * FROM Komponenten WHERE KO_Nr = [0] "
    Public Const sqlTestktTyp3 = "SELECT * FROM RohParams INNER JOIN KomponTypen ON (RohParams.RP_ParamNr = KomponTypen.KT_ParamNr) AND " &
                                 "(RohParams.RP_Typ_Nr = KomponTypen.KT_Typ_Nr) WHERE ((RohParams.RP_Ko_Nr)= [0])"

    'Sql-Statement H2_Memo(Memo) aus Hinweise2
    Public Const sqlSelectH2 = "SELECT * FROM Hinweise2 WHERE H2_Typ = [0] AND H2_Typ2 = [1] AND H2_Id1= [2]"
    Public Const sqlUpdateH2 = "UPDATE Hinweise2 SET [3] WHERE H2_Typ = [0] AND H2_Typ2 = [1] AND H2_Id1= [2]"
    Public Const sqlInsertH2 = "INSERT INTO Hinweise2 (H2_Typ, H2_Typ2, H2_Id1, [3]) VALUES ([0],[1],[2],[4])"
    Public Const sqlDeleteH2 = "DELETE FROM Hinweise2 WHERE H2_Typ = [0] AND H2_Typ2 = [1] AND H2_Id1= [2]"


    'Sql-Statement Liste aller Komponenten-Parameter Typ301
    Public Const sqlKompTyp301 = "SELECT KomponTypen.*, Einheiten.E_Einheit " &
                                 "FROM KomponTypen INNER JOIN Einheiten ON KomponTypen.KT_EinheitIndex = (Einheiten.E_LfdNr) " &
                                 "WHERE (((KomponTypen.KT_Typ_Nr)=301)) ORDER BY KomponTypen.KT_ParamNr"
    'Sql-Statement Suche nächsten Rohstoffdatensatz nach KO_Nr (Update Nährwerte)
    Public Const sqlUpdateNWT = "SELECT * FROM Komponenten WHERE KA_Matchcode <> '' AND KO_Nr > [0] " &
                                 "ORDER BY Komponenten.KO_Nr LIMIT 1"
    'Sql-Statement kompletten Rohstoffdatensatz nach KO_Nr (Update Nährwerte)
    Public Const sqlgetNWT = "SELECT * FROM RohParams INNER JOIN KomponTypen ON (RohParams.RP_ParamNr = KomponTypen.KT_ParamNr) AND " &
                                 "(RohParams.RP_Typ_Nr = KomponTypen.KT_Typ_Nr) WHERE ((RohParams.RP_Ko_Nr)= [0])"


    'Sql-Statement Abfrage Gruppen-Nr(Hierarchie) und Gruppen-Bezeichnung aus [OrgaBackMain].[dbo].[MitarbeiterMultiFunktionsFeld]"
    Public Const mssqlMitarbeiterGruppen = "SELECT * FROM [dbo].[MitarbeiterMultiFunktionsFeld] WHERE GruppenNr=1 ORDER BY Hierarchie"
    'Sql-Statement Update Gruppen-Bezeichnung aus [OrgaBackMain].[dbo].[MitarbeiterMultiFunktionsFeld]"
    Public Const mssqlUpdateMitarbeiterGruppen = "UPDATE [dbo].[MitarbeiterMultiFunktionsFeld] SET Bezeichnung = '[1]' WHERE GruppenNr=1 AND Hierarchie='[0]'"
    'Sql-Statement Insert Mitarbeiter-Gruppe [OrgaBackMain].[dbo].[MitarbeiterMultiFunktionsFeld]"
    Public Const mssqlInsertMitarbeiterGruppen = "INSERT INTO [dbo].[MitarbeiterMultiFunktionsFeld] (GruppenNr, Hierarchie, Bezeichnung) VALUES ('[2]','[0]','[1]')"

    'Sql-Statement Filiale zugeordnet zu Produktion (Typ=4)
    Public Const mssqlFiliale = "SELECT Filialnummer, Name1 FROM Filialen WHERE [Typ] = [0]"
    'Sql-Statament Sortiment zugeordnet zu Filiale mit Typ Produktion
    Public Const mssqlSortiment = "SELECT * FROM FilialeHatSortiment INNER JOIN Filialen ON FilialeHatSortiment.Filialnr =  Filialen.Filialnummer " &
                                  "WHERE [Typ] = [0] ORDER BY SortimentsKürzel"


    Public Shared Function setParams(sql As String, Param0 As String, Optional Param1 As String = "-",
                                     Optional Param2 As String = "-", Optional Param3 As String = "-",
                                     Optional Param4 As String = "-") As String
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
        If Param4 <> "-" Then
            sql = Replace(sql, "[4]", Param4)
        End If
        Return sql
    End Function

End Class
