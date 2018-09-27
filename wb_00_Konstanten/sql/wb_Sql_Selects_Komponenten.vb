Partial Public Class wb_sql_Selects

    'Sql-Statement Rohstoffliste aus winback.Komponenten (KO_Nr als Platzhalter für LG_aktiv)
    Public Const sqlRohstoffSimpleLst = "SELECT KO_Type, KO_Nr_AlNum, KO_Bezeichnung, KO_Nr, KO_Kommentar, KA_RZ_Nr, KA_aktiv, " &
                                  "KA_Kurzname, KA_Matchcode, KA_Preis, KA_Grp1, KA_Grp2, KA_Matchcode FROM Komponenten"
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
    Public Const sqlRohstoffGrp = "SELECT IP_Wert1int, IP_Wert4str FROM ItemParameter WHERE " &
                                  "IP_ItemTyp = 600 And IP_Wert3int = 0 ORDER BY IP_Lfd_Nr DESC"
    'Sql-Statement Rohstoff-Verwendung aus winback.Rezeptschritte
    Public Const sqlRohstoffUse = "SELECT Rezepte.RZ_Nr, Rezepte.RZ_Nr_AlNum, Rezepte.RZ_Bezeichnung FROM RezeptSchritte INNER JOIN " &
                                  "Rezepte On (RezeptSchritte.RS_RZ_Variante_Nr = Rezepte.RZ_Variante_Nr) And (RezeptSchritte.RS_RZ_Nr = Rezepte.RZ_Nr) " &
                                  "WHERE RezeptSchritte.RS_Ko_Nr= [0] And RezeptSchritte.RS_ParamNr = 1"
    'Sql-Statement Automatik-Rohstoffe aus winback.Lagerorte
    Public Const sqlRohstoffAut = "SELECT Komponenten.KO_Nr, Lagerorte.LG_aktiv FROM Komponenten " &
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

    'Sql-Statement Liste aller Komponenten-Parameter Typ(=200,=201..=301,<200)
    Public Const sqlKompTypXXX = "SELECT KomponTypen.*, Einheiten.E_Einheit " &
                                 "FROM KomponTypen INNER JOIN Einheiten ON KomponTypen.KT_EinheitIndex = (Einheiten.E_LfdNr) " &
                                 "WHERE (((KomponTypen.KT_Typ_Nr)[0])) ORDER BY KomponTypen.KT_Typ_Nr, KomponTypen.KT_ParamNr"
    'Sql-Statmente Liste aller Komponenten-Parameter zur Komponenten-Type(Produktion)
    Public Const sqlKomponParamsXXX = "SELECT * FROM KomponParams WHERE KP_Ko_Nr = [0] ORDER BY KP_ParamNr"
    'Sql-Statmente Liste aller Komponenten-Parameter (erweiterte Parameter/Nährwerte)
    Public Const sqlRohParamsXXX = "SELECT * FROM RohParams INNER JOIN KomponTypen ON (RohParams.RP_ParamNr = KomponTypen.KT_ParamNr) AND " &
                                 "(RohParams.RP_Typ_Nr = KomponTypen.KT_Typ_Nr) WHERE ((RohParams.RP_Ko_Nr)= [0]) ORDER BY RP_Typ_Nr, RP_ParamNr"

    'Sql-Statement Suche nächsten Rohstoffdatensatz nach KO_Nr (Update Nährwerte)
    Public Const sqlUpdateNWT = "SELECT * FROM Komponenten WHERE KA_Matchcode <> '' AND KO_Nr > [0] AND KA_aktiv = 1 " &
                                 "ORDER BY Komponenten.KO_Nr LIMIT 1"
    'Sql-Statement Suche nächsten Artikel/Komponente nach KO_Nr (Update Nährwerte-Artikel)
    Public Const sqlUpdateArtikelNWT = "SELECT * FROM Komponenten WHERE KA_RZ_Nr > 0 AND KO_Nr > [0] AND KA_aktiv = 1 " &
                                       "AND KA_Artikel_Typ >= [1] ORDER BY Komponenten.KO_Nr LIMIT 1"
    'Sql-Statement kompletten Rohstoffdatensatz nach KO_Nr (Update Nährwerte)
    Public Const sqlgetNWT = "SELECT * FROM RohParams INNER JOIN KomponTypen ON (RohParams.RP_ParamNr = KomponTypen.KT_ParamNr) AND " &
                                 "(RohParams.RP_Typ_Nr = KomponTypen.KT_Typ_Nr) WHERE ((RohParams.RP_Ko_Nr)= [0])"
    'Sql-Statement Update(Replace) RohParams nach RP_Ko_Nr und RP_Typ_Nr und RP_ParamNr
    Public Const sqlUpdateRohParams = "REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES ([0])"
    'TODO Update KomponParams

    'Sql-Statement Komponenten-Parameter zum Komponenten-Nummer und Parameter-Nummer 
    Public Const sqlKompParams = "Select KP_Wert FROM KomponParams WHERE KP_KO_Nr = [0] And KP_ParamNr = [1]"

    'Sql-Statement Artikelliste aus winback.Komponenten
    Public Const sqlArtikelLst = "Select KO_Nr, KO_Nr_AlNum, KO_Bezeichnung, KA_RZ_Nr, KO_Kommentar, KO_Type, " &
                                  "KA_Kurzname, KA_Matchcode, KA_Grp1, KA_Grp2 FROM Komponenten WHERE KO_Type = 0"

    'Sql-Statement Lesen Komponenten nach KO_Nr (Select KO_Nr=x)
    Public Const sqlSelectKomp_KO_Nr = "Select * FROM Komponenten WHERE KO_Nr = [0] "
    'Sql-Statement Lesen Komponenten nach KO_Nr_AlNum (Select KO_Nr=x) aufsteigend nach Komponenten-Type (Artikel, Automatik, Hand-Komponente)
    Public Const sqlSelectKomp_AlNum = "Select * FROM Komponenten WHERE KO_Nr_AlNum = '[0]' ORDER BY KO_Type"
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

    'Sql-Statement Update aktuelle Komponente (Marker setzen - Update Nährwert-Info notwendig 
    Public Const sqlKompSetMarker = "UPDATE Komponenten SET Komponenten.KA_Artikel_Typ = [1] WHERE KO_Nr = [0]"
    ''Sql-Statement Liste aler Rezeptnummern mit Rezeptschritte enthalten Komponente mit Nummer 
    Public Const sqlKompSetMarkerRzListe = "SELECT RS_RZ_Nr FROM RezeptSchritte WHERE RS_Ko_Nr = [0]"
    'Sql-Statement Update alle Komponenten mit Rezept-Nummer (aus Liste sqlKompSetMarkerRzListe) 
    Public Const sqlKompSetMarkerRzNr = "UPDATE Komponenten Set Komponenten.KA_Artikel_Typ = [1] WHERE KA_RZ_Nr = [0]"


    'Sql-Statement Test wb_ktTypX (Select KO_Nr=x)
    Public Const sqlTestktTypX = "SELECT * FROM Komponenten WHERE KO_Nr = [0] "
    Public Const sqlTestktTyp3 = "SELECT * FROM RohParams INNER JOIN KomponTypen ON (RohParams.RP_ParamNr = KomponTypen.KT_ParamNr) AND " &
                                 "(RohParams.RP_Typ_Nr = KomponTypen.KT_Typ_Nr) WHERE ((RohParams.RP_Ko_Nr)= [0])"

    'Sql-Statement Update Bezeichnung Artikel/Rohstoff (Sync OrgaBack-WinBack)
    Public Const sqlUpdateSyncKomp = "UPDATE Komponenten SET KO_Bezeichnung = '[1]' WHERE KO_Nr_AlNum = '[0]'"
    'Sql-Statement Update AlNummer Artikel/Rohstoff (Sync OrgaBack-WinBack)
    Public Const sqlUpdateSyncKompAlNr = "UPDATE Komponenten SET KO_Nr_AlNum = '[1]' WHERE KO_Nr_AlNum = '[0]'"
    'Sql-Statement Update AlNummer Artikel/Rohstoff (Sync OrgaBack-WinBack)
    Public Const sqlDelSyncKoNr = "DELETE FROM Komponenten WHERE KO_Nr = [0]"
    'Sql-Statement Alle Rohstoffe (KomponType = 102) die nicht in Rezepturen verwendet werden
    Public Const sqlKompNotUsed = "SELECT Komponenten.KO_Nr, Komponenten.KO_Type, Komponenten.KO_Nr_AlNum, " &
                                  "Komponenten.KA_aktiv, RezeptSchritte.RS_Ko_Nr " &
                                  "FROM Komponenten LEFT JOIN RezeptSchritte ON Komponenten.KO_Nr = RezeptSchritte.RS_Ko_Nr " &
                                  "WHERE (((Komponenten.KO_Type) = 102) AND ((Komponenten.KA_aktiv) = 1) " &
                                  "AND ((RezeptSchritte.RS_Ko_Nr) Is Null)) ORDER BY Komponenten.KO_Nr"

End Class

'Tabelle winback.Komponenten
'
'KO_Nr
'KO_Type
'KO_Bezeichnung
'KO_Kommentar
'KO_Nr_AlNum
'KO_Temp_Korr
'KA_Nr
'KA_Kurzname
'KA_Matchcode
'KA_Art
'KA_Artikel_Typ
'KA_RZ_Nr
'KA_Lagerort
'KA_Prod_Linie
'KA_Stueckgewicht
'KA_Charge_Opt
'KA_Charge_Min
'KA_Charge_Max
'KA_Charge_Opt_kg
'KA_Charge_Min_kg
'KA_Charge_Max_kg
'KA_RS_veraenderbar
'KA_RS_abh_von_RZ_Menge
'KA_RS_aendert_WasMenge
'KA_zaehlt_zu_RZ_Gesamtmenge
'KA_spez_WKap
'KA_alternativ_RS
'KA_Verarbeitungshinweise
'KA_aktiv
'KA_Preis
'KA_PreisEinheit
'KA_Grp1
'KA_Grp2
'KA_Timestammp

