Public Class wb_Sql_Selects
    'Sql-Statement Konfiguration
    Public Const sqlKonfiguration = "SELECT * FROM Konfiguration WHERE KF_Tag LIKE '[0]'"

    'Sql-Statement Liniengruppen
    Public Const sqlLinienGruppen = "SELECT * FROM LinienGruppen"

    'Sql-Statement Rezeptliste aus winback.Rezepte
    Public Const sqlRezeptListe = "SELECT RZ_Nr, RZ_Nr_AlNum, RZ_Bezeichnung, RZ_Variante_Nr, RZ_Kommentar, RZ_Gewicht, " &
                                  "RZ_Aenderung_Nr, RZ_Aenderung_Datum, RZ_Aenderung_Name, RZ_Liniengruppe, " &
                                  "RZ_Charge_Min, RZ_Charge_Max, RZ_Charge_Opt FROM Rezepte"
    'Sql-Statement Rezept-Historie aus winback.HisRezepte
    Public Const sqlRezeptHistr = "SELECT H_RZ_Aenderung_Nr, H_RZ_Aenderung_Datum, H_RZ_Aenderung_Name FROM His_Rezepte " &
                                  "WHERE H_RZ_Nr=[0] AND H_RZ_Variante_Nr=[1]"
    'Sql-Statement Rezeptkopf aus winback.Rezepte
    Public Const sqlRezeptKopf = "SELECT RZ_Nr, RZ_Nr_AlNum, RZ_Bezeichnung, RZ_Variante_Nr, RZ_Kommentar, RZ_Gewicht, " &
                                  "RZ_Aenderung_Nr, RZ_Aenderung_Datum, RZ_Aenderung_Name, RZ_Liniengruppe, RZ_Teigtemperatur, " &
                                  "RZ_Charge_Min, RZ_Charge_Max, RZ_Charge_Opt FROM Rezepte WHERE RZ_Nr=[0] " &
                                  "AND (RZ_Variante_Nr=[1] Or RZ_Variante_Nr=1 Or RZ_Variante_Nr=0) ORDER BY RZ_Variante_Nr DESC"
    'Sql-Statement Rezept-Verwendung aus winback.Komponenten
    Public Const sqlRezeptVerwendung = "SELECT KO_Nr_AlNum, KO_Bezeichnung, KO_Kommentar, KA_Art, KA_Matchcode " &
                                  "FROM Komponenten WHERE KA_RZ_Nr = [0]"
    Public Const sqlAddNewRezept = "INSERT INTO Rezepte(RZ_Nr, RZ_Variante_Nr, RZ_Bezeichnung) VALUES ([0],[1],'[2]')"
    Public Const sqlMaxRzNummer = "SELECT MAX(RZ_Nr) FROM Rezepte"
    'Sql-Statement Verwendung Rezept in Artikeln/Rohstoffen
    Public Const sqlRezeptInKomp = "SELECT COUNT(*) AS Used FROM Komponenten WHERE KA_RZ_Nr = [0]"

    'Sql-Statement Rezeptschritte Datensatze löschen
    Public Const sqlDelRzptSchr = "DELETE FROM RezeptSchritte WHERE RS_RZ_Nr = [0] AND RS_RZ_Variante_Nr = [1]"
    'Sql-Statement Rezeptkopf Datensatze löschen
    Public Const sqlDelRzptKopf = "DELETE FROM Rezepte WHERE RZ_Nr = [0] AND RZ_Variante_Nr = [1]"
    'Sql-Statement HisRezeptschritte Datensatze löschen
    Public Const sqlDelHRzptSchr = "DELETE FROM His_RezeptSchritte WHERE H_RS_RZ_Nr = [0] AND H_RS_RZ_Variante_Nr = [1]"
    'Sql-Statement HisRezepte Datensatze löschen (Rezeptkopf)
    Public Const sqlDelHRzptKopf = "DELETE FROM His_Rezepte WHERE H_RZ_Nr = [0] AND H_RZ_Variante_Nr = [1]"

    'Sql-Statement Tabelle Rezepte Update
    Public Const sqlRezeptUpdate = "UPDATE Rezepte SET [2] WHERE RZ_Nr = [0] AND RZ_Variante_Nr = [1]"


    'Sql-Statement Rohstoffliste aus winback.Komponenten (KO_Nr als Platzhalter für LG_aktiv)
    Public Const sqlRohstoffSimpleLst = "SELECT KO_Nr_AlNum, KO_Bezeichnung, KO_Nr, KO_Kommentar, KA_RZ_Nr, KO_Type, KA_aktiv, " &
                                  "KA_Kurzname, KA_Matchcode, KA_Preis, KA_Grp1, KA_Grp2 FROM Komponenten"
    'Sql-Statement Rohstoffliste aus winback.Komponenten (KO_Nr als Platzhalter für LG_aktiv)
    Public Const sqlRohstoffLst = "SELECT KO_Nr_AlNum, KO_Bezeichnung, KO_Nr, KO_Kommentar, KA_RZ_Nr, KO_Type, KA_aktiv, " &
                                  "KA_Kurzname, KA_Matchcode, KA_Preis, KA_Grp1, KA_Grp2, E_Einheit FROM Komponenten " &
                                  "INNER JOIN KomponTypen On Komponenten.KO_Type = KomponTypen.KT_Typ_Nr " &
                                  "INNER JOIN Einheiten On KomponTypen.KT_EinheitIndex = Einheiten.E_LfdNr " &
                                  "WHERE KO_Type <> 0 AND KT_ParamNr = 1"

    'Sql-Statement Rohstoff aus winback.Komponenten (KO_Nr) alle Zeilen mit KomponTypen.KT_Rezept = "R"
    Public Const sqlRohstoffRez = "SELECT * FROM Komponenten INNER JOIN KomponTypen On Komponenten.KO_Type = KomponTypen.KT_Typ_Nr " &
                                  "INNER JOIN Einheiten On KomponTypen.KT_EinheitIndex = Einheiten.E_LfdNr " &
                                  "WHERE KO_Nr = [0] AND KT_Rezept = 'R' AND KT_ParamNr > 1"

    'Sql-Statement Kneter-Rezept aus winback.RohParams
    Public Const sqlKneterRezept = "SELECT * FROM RohParams INNER JOIN Komponenten ON RohParams.RP_Wert = Komponenten.KO_Nr " &
                                  "INNER JOIN KomponTypen On Komponenten.KO_Type = KomponTypen.KT_Typ_Nr " &
                                  "INNER JOIN Einheiten On KomponTypen.KT_EinheitIndex = Einheiten.E_LfdNr " &
                                   "WHERE RP_KO_Nr = [0] AND KT_Rezept = 'R'"

    'Sql-Statement Kneter-Rezept löschen in RohParams
    Public Const sqlDelKneterRzt = "DELETE FROM RohParams WHERE RP_KO_Nr = [0]"
    'Sql-Statement Kneter-Rezept anlegen in RohParams
    Public Const sqlInsKneterRzt = "INSERT INTO RohParams(RP_KO_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES ([0],118,[1])"


    'Sql-Statement RohstoffGruppen aus winback.ItemParameter
    Public Const sqlRohstoffGrp = "Select IP_Wert1int, IP_Wert4str FROM ItemParameter WHERE " &
                                  "IP_ItemTyp = 600 And IP_Wert3int = 0 ORDER BY IP_Lfd_Nr DESC"
    'Sql-Statement Rohstoff-Verwendung aus winback.Rezeptschritte
    Public Const sqlRohstoffUse = "Select Rezepte.RZ_Nr, Rezepte.RZ_Nr_AlNum, Rezepte.RZ_Bezeichnung FROM RezeptSchritte INNER JOIN " &
                                  "Rezepte On (RezeptSchritte.RS_RZ_Variante_Nr = Rezepte.RZ_Variante_Nr) And (RezeptSchritte.RS_RZ_Nr = Rezepte.RZ_Nr) " &
                                  "WHERE RezeptSchritte.RS_Ko_Nr= [0] And RezeptSchritte.RS_ParamNr = 1"
    'Sql-Statement Automatik-Rohstoffe aus winback.Lagerorte
    Public Const sqlRohstoffAut = "Select Komponenten.KO_Nr, Lagerorte.LG_aktiv FROM Komponenten " &
                                  "INNER JOIN Lagerorte On Komponenten.KA_Lagerort = Lagerorte.LG_Ort " &
                                  "WHERE KO_TYPE = 101 Or KO_TYPE = 103 Or KO_TYPE = 104"

    Public Const sqlENummern = "Select * FROM enummern"

    'Sql-Statement Artikelliste aus winback.Komponenten
    Public Const sqlArtikelLst = "Select KO_Nr, KO_Nr_AlNum, KO_Bezeichnung, KA_RZ_Nr, KO_Kommentar, KO_Type, " &
                                  "KA_Kurzname, KA_Matchcode, KA_Grp1, KA_Grp2 FROM Komponenten WHERE KO_Type = 0"

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
    Public Const sqlUserRechte = "Select ItemTypen.IT_Bezeichnung, ItemIDs.II_Kommentar, AT_Wert2int, Texte.T_Text FROM ItemIDs " &
                                 "INNER JOIN ItemTypen On ItemIDs.II_ItemTyp = ItemTypen.IT_ItemTyp " &
                                 "INNER JOIN ItemParameter On ItemIDs.II_ItemID = ItemParameter.IP_ItemID AND ItemIDs.II_ItemTyp = ItemParameter.IP_ItemTyp " &
                                 "INNER JOIN IAttrParams On ItemParameter.IP_Wert2int = IAttrParams.AT_Wert3str AND ItemParameter.IP_ItemAttr = IAttrParams.AT_Attr_Nr " &
                                 "INNER JOIN Texte On IAttrParams.AT_Wert2int = Texte.T_TextIndex AND IAttrParams.AT_Attr_Nr = Texte.T_Typ " &
                                 "WHERE ItemIDs.II_ItemTyp <= 230 AND ItemParameter.IP_Wert1int = [0] AND  Texte.T_Sprache = [1] " &
                                 "ORDER BY ItemIDs.II_ItemTyp, ItemIDs.II_ItemID;"
    'Sql-Statement UserGruppenRechte aus winback.ItemParameter
    'Public Const sqlUserGrpRechte = "Select * FROM ItemParameter WHERE IP_ItemTyp = 2 AND IP_Wert1int = [0] AND IP_Wert5Str <> 'X' ORDER BY IP_ItemID;"
    Public Const sqlUserGrpRechte = "Select * FROM ItemParameter WHERE IP_ItemTyp = 2 AND IP_Wert1int = [0] ORDER BY IP_ItemID;"
    'Sql-Statement User Datensatz lesen
    Public Const sqlUserLogin = "SELECT * FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert1int = [0]"
    'Sql-Statement User Datensatz lesen
    Public Const sqlUserName = "SELECT * FROM ItemParameter WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert4str = '[0]'"

    'Sql-Statement alle Texte aus winback.Texte
    Public Const sqlWinBackTxte = "SELECT T_TextIndex, T_Typ, T_Text FROM Texte WHERE T_Sprache = [0]"

    'Sql-Statement Lesen Komponenten nach KO_Nr (Select KO_Nr=x)
    Public Const sqlSelectKomp_KO_Nr = "SELECT * FROM Komponenten WHERE KO_Nr = [0] "
    Public Const sqlSelectKomp_AlNum = "SELECT * FROM Komponenten WHERE KO_Nr_AlNum = '[0]' "
    Public Const sqlSelectKomp_KO_Type = "SELECT * FROM Komponenten WHERE KO_Type = [0] "
    'Sql-Statement Update Komponenten nach KO_Nr (Select KO_Nr=x)
    Public Const sqlUpdateKomp_KO_Nr = "UPDATE Komponenten SET [1] WHERE KO_Nr = [0] "
    'Sql-Statement letzte interne Komponenten-Nummer (Max(KO-Nr))
    Public Const sqlMaxKompNummer = "SELECT MAX(KO_Nr) FROM Komponenten"
    'Sql-Statement neue Komponente/Artikel anlegen
    Public Const sqlAddNewKompon = "INSERT INTO Komponenten(KO_Nr, KO_Nr_AlNum, KO_Type, KO_Bezeichnung) VALUES ([0],'[1]',[2],'[3]')"
    'Sql-Statement Verwendung Komponente in Rezeptschritten
    Public Const sqlKompInRezept = "SELECT COUNT(*) AS Used FROM RezeptSchritte WHERE RS_Ko_nr = [0]"
    'Sql-Statement Verwendung Komponente in Arbeits-Rezeptschritten
    Public Const sqlKompInArbRzp = "SELECT COUNT(*) AS Used FROM BAK_ArbRZSchritte WHERE B_ARS_TW_Nr = 0 AND B_ARS_Ko_nr = [0]"

    'Löschen Rohstoff/Artikel in Komponenten
    Public Const sqlDelKomponenten = "DELETE FROM Komponenten WHERE KO_NR = [0]"
    'Löschen Rohstoff/Artikel in KomponParams
    Public Const sqlDelKomponParams = "DELETE FROM KomponParams WHERE KP_Ko_NR = [0]"
    'Löschen Rohstoff/Artikel in Hinweise2
    Public Const sqlDelKompHinweise = "DELETE FROM Hinweise2 WHERE H2_Id1= [0]"
    'Löschen Rohstoff/Artikel in RohParams
    Public Const sqlDelRohParams = "DELETE FROM RohParams WHERE RP_Ko_NR = [0]"
    'Löschen Rohstoff/Artikel in Lagerorte
    Public Const sqlDelLagerOrte = "DELETE FROM Lagerorte WHERE LG_Ort = '[0]'"
    'Löschen Rohstoff/Artikel in Lieferungen
    Public Const sqlDelLieferungen = "DELETE FROM Lieferungen WHERE LF_LG_Ort = '[0]'"


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
    'Sql-Statmente Liste aller Komponenten-Parameter
    Public Const sqlKompTypXXX = "SELECT * FROM RohParams INNER JOIN KomponTypen ON (RohParams.RP_ParamNr = KomponTypen.KT_ParamNr) AND " &
                                 "(RohParams.RP_Typ_Nr = KomponTypen.KT_Typ_Nr) WHERE ((RohParams.RP_Ko_Nr)= [0]) ORDER BY RP_Typ_Nr, RP_ParamNr"

    'Sql-Statement Suche nächsten Rohstoffdatensatz nach KO_Nr (Update Nährwerte)
    Public Const sqlUpdateNWT = "SELECT * FROM Komponenten WHERE KA_Matchcode <> '' AND KO_Nr > [0] " &
                                 "ORDER BY Komponenten.KO_Nr LIMIT 1"
    'Sql-Statement kompletten Rohstoffdatensatz nach KO_Nr (Update Nährwerte)
    Public Const sqlgetNWT = "SELECT * FROM RohParams INNER JOIN KomponTypen ON (RohParams.RP_ParamNr = KomponTypen.KT_ParamNr) AND " &
                                 "(RohParams.RP_Typ_Nr = KomponTypen.KT_Typ_Nr) WHERE ((RohParams.RP_Ko_Nr)= [0])"


    'Sql-Statement Komponenten-Parameter zum Komponenten-Nummer und Parameter-Nummer 
    Public Const sqlKompParams = "SELECT KP_Wert FROM KomponParams WHERE KP_KO_Nr = [0] AND KP_ParamNr = [1]"


    'sql-Statement komplette Rezeptur nach RzNr und Variante
    Public Const sqlRezeptur = "SELECT RezeptSchritte.RS_Schritt_Nr, RezeptSchritte.RS_ParamNr, " &
                               "Komponenten.KO_Nr, Komponenten.KO_Type, Komponenten.KO_Nr_AlNum, Komponenten.KO_Kommentar, " &
                               "Komponenten.KO_Bezeichnung, Komponenten.KO_Temp_Korr, KomponTypen.KT_KurzBez, KomponTypen.KT_Format, " &
                               "KomponTypen.KT_Laenge, RezeptSchritte.RS_Wert, Komponenten.KA_RS_veraenderbar, Einheiten.E_Einheit, " &
                               "RezeptSchritte.RS_Wert_Prod,RezeptSchritte.RS_Par1,RezeptSchritte.RS_Par2,RezeptSchritte.RS_Par3, " &
                               "KomponTypen.KT_Bezeichnung, KomponTypen.KT_OberGW, KomponTypen.KT_UnterGW, KA_Charge_Opt, " &
                               "Komponenten.KA_Preis, Komponenten.KA_zaehlt_zu_RZ_Gesamtmenge, Komponenten.KA_RZ_Nr, " &
                               "Komponenten.KA_Verarbeitungshinweise, KA_Grp1, KA_Grp2, Komponenten.KA_Matchcode " & "FROM (((Rezepte INNER JOIN RezeptSchritte ON " &
                               "(Rezepte.RZ_Variante_Nr = RezeptSchritte.RS_RZ_Variante_Nr) AND " &
                               "(Rezepte.RZ_Nr = RezeptSchritte.RS_RZ_Nr)) INNER JOIN Komponenten " &
                               "ON RezeptSchritte.RS_Ko_Nr = Komponenten.KO_Nr) INNER JOIN " &
                               "KomponTypen ON (RezeptSchritte.RS_ParamNr = KomponTypen.KT_ParamNr) " &
                               "AND (Komponenten.KO_Type = KomponTypen.KT_Typ_Nr)) INNER JOIN Einheiten ON (KomponTypen.KT_EinheitIndex = Einheiten.E_LfdNr) " &
                               "WHERE (Rezepte.RZ_Nr = [0]) AND (Rezepte.RZ_Variante_Nr = [1]) " &
                               "AND ((KomponTypen.KT_Rezept='R') OR (KomponTypen.KT_Rezept='X')) " &
                               "ORDER BY RezeptSchritte.RS_Schritt_Nr, RezeptSchritte.RS_ParamNr"

    'Sql-Statement neuen Rezeptschritt schreiben
    Public Const sqlAddRZSchritt = "INSERT INTO RezeptSchritte([0]) VALUES ([1])"


    Public Const bakArbRezepte = "SELECT * FROM BAK_ArbRezepte WHERE (B_ARZ_LiBeh_Nr>100) AND " &
                                 "B_ARZ_TW_Nr = " & Chr(34) & "[0]" & Chr(34) & " ORDER BY B_ARZ_TW_Idx, B_ARZ_Timestamp"

    'Sql-Statement Abfrage Vorname, Nachname und Nummer aus [OrgaBackMain].[dbo].[Mitarbeiter]"
    Public Const mssqlMitarbeiterMFF500 = "SELECT * FROM [dbo].[Mitarbeiter] LEFT JOIN MitarbeiterHatMultiFeld " &
                                    "ON Mitarbeiter.MitarbeiterKürzel = MitarbeiterHatMultiFeld.MitarbeiterKürzel " &
                                    "WHERE MitarbeiterHatMultiFeld.FeldNummer = 500 ORDER BY KassiererNummer"
    Public Const mssqlMitarbeiter = "SELECT * FROM [dbo].[Mitarbeiter] LEFT JOIN MitarbeiterHatMultiFeld " &
                                    "ON Mitarbeiter.MitarbeiterKürzel = MitarbeiterHatMultiFeld.MitarbeiterKürzel ORDER BY KassiererNummer"


    'Sql-Statement Abfrage Gruppen-Nr(Hierarchie) und Gruppen-Bezeichnung aus [OrgaBackMain].[dbo].[MitarbeiterMultiFunktionsFeld]"
    Public Const mssqlMitarbeiterGruppen = "SELECT * FROM [dbo].[MitarbeiterMultiFunktionsFeld] WHERE GruppenNr=1 ORDER BY Hierarchie"
    'Sql-Statement Update Gruppen-Bezeichnung aus [OrgaBackMain].[dbo].[MitarbeiterMultiFunktionsFeld]"
    Public Const mssqlUpdateMitarbeiterGruppen = "UPDATE [dbo].[MitarbeiterMultiFunktionsFeld] SET Bezeichnung = '[1]' WHERE GruppenNr=1 AND Hierarchie='[0]'"
    'Sql-Statement Insert Mitarbeiter-Gruppe [OrgaBackMain].[dbo].[MitarbeiterMultiFunktionsFeld]"
    Public Const mssqlInsertMitarbeiterGruppen = "INSERT INTO [dbo].[MitarbeiterMultiFunktionsFeld] (GruppenNr, Hierarchie, Bezeichnung) VALUES ('[2]','[0]','[1]')"

    'Sql-Statement Artikel
    Public Const mssqlArtikel = "SELECT ArtikelNr,Kurztext,Sortiment FROM [dbo].[Artikel] WHERE Artikelgruppe = [0] ORDER BY ArtikelNr"

    'Sql-Statement Filiale zugeordnet zu Produktion (Typ=4)
    Public Const mssqlFiliale = "Select Filialnummer, Name1 FROM Filialen WHERE [Typ] = [0]"
    'Sql-Statament Sortiment zugeordnet zu Filiale mit Typ Produktion
    Public Const mssqlSortiment = "Select * FROM FilialeHatSortiment INNER JOIN Filialen On FilialeHatSortiment.Filialnr =  Filialen.Filialnummer " &
                                  "WHERE [Typ] = [0] ORDER BY SortimentsKürzel"

    'Sql-Statement Abfrage dbo.Settings.Category
    Public Const mssqlSettings = "Select * FROM Settings WHERE [Category] = '[0]'"
    'Sql-Statement Abfrage dbo.WorkStations.Computername
    Public Const mssqlWrkStations = "SELECT * FROM WorkStations WHERE [ComputerName] = '[0]'"
    'Sql-Statement Abfrage dbo.SystemInfo
    Public Const mssqlSystemInfo = "SELECT TOP(1) * FROM SystemInfo ORDER BY lfd DESC"


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
