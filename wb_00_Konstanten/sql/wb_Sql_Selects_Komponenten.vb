﻿Partial Public Class wb_sql_Selects

    'Sql-Statement Rohstoffliste aus winback.Komponenten (KO_Nr als Platzhalter für LG_aktiv)
    Public Const sqlRohstoffSimpleLst = "SELECT KO_Type, KO_Nr_AlNum, KO_Bezeichnung, KO_Nr, KO_Kommentar, KA_RZ_Nr, KA_aktiv, " &
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

    'Sql-Statement Artikelliste aus winback.Komponenten
    Public Const sqlArtikelLst = "Select KO_Nr, KO_Nr_AlNum, KO_Bezeichnung, KA_RZ_Nr, KO_Kommentar, KO_Type, " &
                                  "KA_Kurzname, KA_Matchcode, KA_Grp1, KA_Grp2 FROM Komponenten WHERE KO_Type = 0"

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

    'Sql-Statement Test wb_ktTypX (Select KO_Nr=x)
    Public Const sqlTestktTypX = "SELECT * FROM Komponenten WHERE KO_Nr = [0] "
    Public Const sqlTestktTyp3 = "SELECT * FROM RohParams INNER JOIN KomponTypen ON (RohParams.RP_ParamNr = KomponTypen.KT_ParamNr) AND " &
                                 "(RohParams.RP_Typ_Nr = KomponTypen.KT_Typ_Nr) WHERE ((RohParams.RP_Ko_Nr)= [0])"



End Class