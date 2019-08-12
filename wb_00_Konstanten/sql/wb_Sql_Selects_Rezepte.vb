Partial Public Class wb_Sql_Selects

    'Sql-Statement Rezeptname zu Rezeptnummer (wb_Artikel_Shared)
    Public Const sqlRezeptNrName = "SELECT * FROM Rezepte WHERE RZ_Variante_Nr = 1"

    'Sql-Statement Rezeptliste aus winback.Rezepte
    Public Const sqlRezeptListe = "SELECT RZ_Nr, RZ_Nr_AlNum, RZ_Bezeichnung, RZ_Variante_Nr, RZ_Kommentar, RZ_Gewicht, " &
                                  "RZ_Aenderung_Nr, RZ_Aenderung_Datum, RZ_Aenderung_Name, RZ_Liniengruppe, " &
                                  "RZ_Charge_Min, RZ_Charge_Max, RZ_Charge_Opt FROM Rezepte WHERE RZ_Variante_Nr = 1"
    'Sql-Statement Rezept-Historie aus winback.HisRezepte
    Public Const sqlRezeptHistr = "SELECT H_RZ_Aenderung_Nr, H_RZ_Aenderung_Datum, H_RZ_Aenderung_Name FROM His_Rezepte " &
                                  "WHERE H_RZ_Nr=[0] AND H_RZ_Variante_Nr=[1]"
    'Sql-Statement Rezeptkopf aus winback.Rezepte
    Public Const sqlRezeptKopf = "SELECT RZ_Nr, RZ_Nr_AlNum, RZ_Bezeichnung, RZ_Variante_Nr, RZ_Kommentar, RZ_Gewicht, " &
                                  "RZ_Aenderung_Nr, RZ_Aenderung_Datum, RZ_Aenderung_Name, RZ_Liniengruppe, RZ_Teigtemperatur, " &
                                  "RZ_Charge_Min, RZ_Charge_Max, RZ_Charge_Opt FROM Rezepte WHERE RZ_Nr=[0] " &
                                  "AND (RZ_Variante_Nr=[1] Or RZ_Variante_Nr=1 Or RZ_Variante_Nr=0) ORDER BY RZ_Variante_Nr DESC"
    'Sql-Statement Rezeptkopf aus winback.Rezepte
    Public Const sqlRezeptNummer = "SELECT RZ_Nr, RZ_Nr_AlNum, RZ_Bezeichnung, RZ_Variante_Nr, RZ_Kommentar, RZ_Gewicht, " &
                                   "RZ_Aenderung_Nr, RZ_Aenderung_Datum, RZ_Aenderung_Name, RZ_Liniengruppe, RZ_Teigtemperatur, " &
                                   "RZ_Charge_Min, RZ_Charge_Max, RZ_Charge_Opt FROM Rezepte WHERE RZ_Nr_AlNum = '[0]' " &
                                   "AND (RZ_Variante_Nr=[1] Or RZ_Variante_Nr=1 Or RZ_Variante_Nr=0) ORDER BY RZ_Variante_Nr DESC"
    'Sql-Statement Rezept-Verwendung aus winback.Komponenten
    Public Const sqlRezeptVerwendung = "SELECT KO_Nr_AlNum, KO_Bezeichnung, KO_Kommentar, KA_Art, KA_Matchcode, KO_Nr " &
                                  "FROM Komponenten WHERE KA_RZ_Nr = [0]"


    Public Const sqlAddNewRezept = "INSERT INTO Rezepte([0]) VALUES ([1])"
    Public Const sqlMaxRzNummer = "SELECT MAX(RZ_Nr) FROM Rezepte"

    'Sql-Statement Verwendung Rezept in Artikeln/Rohstoffen
    Public Const sqlRezeptInKomp = "SELECT COUNT(*) AS Used FROM Komponenten WHERE KA_RZ_Nr = [0]"
    'Sql-Statement Verwengung Komponente in Rezepschritten (momentan nur Variante 1 !!!)
    Public Const sqlKompInRZSchritte = "SELECT RS_RZ_Nr FROM RezeptSchritte WHERE RS_Ko_Nr = [0] AND RS_RZ_Variante_Nr = 1 ORDER BY RS_RZ_Nr"

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


    'sql-Statement komplette Rezeptur nach RzNr und Variante
    Public Const sqlRezeptur = "SELECT RezeptSchritte.RS_Schritt_Nr, RezeptSchritte.RS_ParamNr, " &
                               "Komponenten.KO_Nr, Komponenten.KO_Type, Komponenten.KO_Nr_AlNum, Komponenten.KO_Kommentar, Komponenten.KO_Bezeichnung, " &
                               "Komponenten.KA_Artikel_Typ, Komponenten.KO_Temp_Korr, KomponTypen.KT_KurzBez, KomponTypen.KT_Format, " &
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

    'Sql-Statement Rezeptkopf in wb_daten.His_Rezepte löschen
    Public Const sqlDelRezKopfInHisRezepte = "DELETE FROM wbdaten.His_Rezepte " &
                                              "WHERE (H_RZ_Nr = [0]) AND (H_RZ_Variante_Nr = [1]) AND (H_RZ_Aenderung_Nr = [2])"

    'Sql-Statement Rezeptkopf nach wb_daten.His_Rezepte kopieren von winback.Rezepte
    Public Const sqlCopyRezKopfInHisRezepte = "INSERT INTO wbdaten.His_Rezepte(H_RZ_Nr, H_RZ_Variante_Nr, H_RZ_Aenderung_Nr, " &
                                              "H_RZ_Aenderung_Datum, H_RZ_Aenderung_User, H_RZ_Aenderung_Name, H_RZ_Nr_AlNum, " &
                                              "H_RZ_Bezeichnung, H_RZ_Gewicht, H_RZ_Kommentar, H_RZ_Kurzname, H_RZ_Matchcode, " &
                                              "H_RZ_Type, H_RZ_Teigtemperatur, H_RZ_Kneterkennlinie, H_RZ_Verarbeitungshinweise, " &
                                              "H_RZ_Liniengruppe, H_RZ_Gruppe, H_KA_Gruppe, H_RZ_Timestamp) " &
                                              "SELECT RZ_Nr, RZ_Variante_Nr, RZ_Aenderung_Nr, " &
                                              "RZ_Aenderung_Datum, RZ_Aenderung_User, RZ_Aenderung_Name, RZ_Nr_AlNum, " &
                                              "RZ_Bezeichnung, RZ_Gewicht, RZ_Kommentar, RZ_Kurzname, RZ_Matchcode, " &
                                              "RZ_Type, RZ_Teigtemperatur, RZ_Kneterkennlinie, RZ_Verarbeitungshinweise, " &
                                              "RZ_Liniengruppe, RZ_Gruppe, KA_Gruppe, RZ_Timestamp FROM Rezepte " &
                                              "WHERE (Rezepte.RZ_Nr = [0]) And (Rezepte.RZ_Variante_Nr = [1])"

    'Sql-Statement Rezeptschritte in wb_daten.His_Rezeptschritte löschen
    Public Const sqlDelRezSchritteInHisRezepte = "DELETE FROM wbdaten.His_RezeptSchritte " &
                                                 "WHERE (H_RS_RZ_Nr = [0]) AND (H_RS_RZ_Variante_Nr = [1]) AND (H_RS_Aenderung_Nr = [2])"

    'Sql-Statement Rezeptkopf nach wb_daten.His_Rezepte kopieren von winback.Rezepte
    Public Const sqlCopyRezSchritteInHisRezepte = "INSERT INTO wbdaten.His_RezeptSchritte(H_RS_RZ_Nr, H_RS_RZ_Variante_Nr, " &
                                                  "H_RS_Index, H_RS_Aenderung_Nr, H_RS_Schritt_Nr, H_RS_Ko_Nr, H_RS_ParamNr, " &
                                                  "H_RS_Wert, H_RS_Wert_Prod, H_RS_Par1, H_RS_Par2, H_RS_Par3, H_RS_Preis, H_RS_PreisEinheit, " &
                                                  "H_KO_Nr, H_KO_Type, H_KO_Bezeichnung, H_KO_Kommentar, H_KO_Nr_AlNum, H_KO_Temp_Korr, " &
                                                  "H_KT_Bezeichnung, H_KT_KurzBez, H_KT_EinheitIndex, H_E_Einheit, H_E_Bezeichnung, " &
                                                  "H_KT_Kommentar, H_KT_Format, H_KT_Laenge, H_KA_Nr, H_KA_Kurzname, H_KA_Matchcode, H_KA_Art, " &
                                                  "H_KA_Artikel_Typ, H_KA_RZ_Nr, H_KA_Lagerort, H_KA_Prod_Linie, H_KA_Stueckgewicht, " &
                                                  "H_KA_Charge_Opt, H_KA_Charge_Min, H_KA_Charge_Max, H_KA_RS_veraenderbar, " &
                                                  "H_KA_RS_abh_von_RZ_Menge, H_KA_RS_aendert_WasMenge, H_KA_zaehlt_zu_RZ_Gesamtmenge, " &
                                                  "H_KA_spez_WKap, H_KA_alternativ_RS, H_KA_Verarbeitungshinweise, H_KA_aktiv, " &
                                                  "H_KA_Preis, H_KA_PreisEinheit, H_KA_Grp1, H_KA_Grp2, H_RS_Timestamp) " &
                                                  "SELECT RZ_Nr, RZ_Variante_Nr, " &
                                                  "RS_Schritt_Nr, [2], RS_Schritt_Nr, RS_Ko_Nr, RS_ParamNr, " &
                                                  "RS_Wert, RS_Wert_Prod, RS_Par1, RS_Par2, RS_Par3, KA_Preis, KA_PreisEinheit, " &
                                                  "KO_Nr, KO_Type, KO_Bezeichnung, KO_Kommentar, KO_Nr_AlNum, KO_Temp_Korr, " &
                                                  "KT_Bezeichnung, KT_KurzBez, KT_EinheitIndex, E_Einheit, E_Bezeichnung, " &
                                                  "KT_Kommentar, KT_Format, KT_Laenge, KA_Nr, KA_Kurzname, KA_Matchcode, KA_Art, " &
                                                  "KA_Artikel_Typ, KA_RZ_Nr, KA_Lagerort, KA_Prod_Linie, KA_Stueckgewicht, " &
                                                  "KA_Charge_Opt, KA_Charge_Min, KA_Charge_Max, KA_RS_veraenderbar, " &
                                                  "KA_RS_abh_von_RZ_Menge, KA_RS_aendert_WasMenge, KA_zaehlt_zu_RZ_Gesamtmenge, " &
                                                  "KA_spez_WKap, KA_alternativ_RS, KA_Verarbeitungshinweise, KA_aktiv, " &
                                                  "KA_Preis, KA_PreisEinheit, KA_Grp1, KA_Grp2, KA_Timestammp " &
                                                  "FROM (((Rezepte INNER JOIN RezeptSchritte On " &
                                                  "(Rezepte.RZ_Variante_Nr = RezeptSchritte.RS_RZ_Variante_Nr) And " &
                                                  "(Rezepte.RZ_Nr = RezeptSchritte.RS_RZ_Nr)) INNER JOIN Komponenten " &
                                                  "On RezeptSchritte.RS_Ko_Nr = Komponenten.KO_Nr) INNER JOIN " &
                                                  "KomponTypen On (RezeptSchritte.RS_ParamNr = KomponTypen.KT_ParamNr) " &
                                                  "And (Komponenten.KO_Type = KomponTypen.KT_Typ_Nr)) INNER JOIN Einheiten On (KomponTypen.KT_EinheitIndex = Einheiten.E_LfdNr) " &
                                                  "WHERE (Rezepte.RZ_Nr = [0]) And (Rezepte.RZ_Variante_Nr = [1]) " &
                                                  "And ((KomponTypen.KT_Rezept='R') OR (KomponTypen.KT_Rezept='X')) " &
                                                  "ORDER BY RezeptSchritte.RS_Schritt_Nr, RezeptSchritte.RS_ParamNr"

    'Sql-Statemente Lesen Rezeptkopf aus His_Rezepte
    Public Const sqlHisRezeptKopf = "SELECT * FROM His_Rezepte WHERE H_RZ_Nr=[0] " &
                                    "AND (H_RZ_Variante_Nr=[1] Or H_RZ_Variante_Nr=1 Or H_RZ_Variante_Nr=0) " &
                                    "AND H_RZ_Aenderung_Nr=[2] ORDER BY H_RZ_Variante_Nr DESC"

    'Sql-Statemente Lesen Rezeptur aus His_RezeptSchritte
    Public Const sqlHisRezeptur = "SELECT * FROM His_RezeptSchritte WHERE H_RS_RZ_Nr=[0] " &
                                  "AND H_RS_RZ_Variante_Nr=[1] AND H_RS_Aenderung_Nr=[2] ORDER BY H_RS_Schritt_Nr, H_RS_ParamNr"


    'Sql-Statement neuen Rezeptschritt schreiben
    Public Const sqlAddRZSchritt = "INSERT INTO RezeptSchritte([0]) VALUES ([1])"

    Public Const bakArbRezepte = "Select * FROM BAK_ArbRezepte WHERE (B_ARZ_LiBeh_Nr>100) And " &
                                 "B_ARZ_TW_Nr = " & Chr(34) & "[0]" & Chr(34) & " ORDER BY B_ARZ_TW_Idx, B_ARZ_Timestamp"

    'Sql-Statement Kneter-Rezept aus winback.RohParams
    Public Const sqlKneterRezept = "Select * FROM RohParams INNER JOIN Komponenten On RohParams.RP_Wert = Komponenten.KO_Nr " &
                                  "INNER JOIN KomponTypen On Komponenten.KO_Type = KomponTypen.KT_Typ_Nr " &
                                  "INNER JOIN Einheiten On KomponTypen.KT_EinheitIndex = Einheiten.E_LfdNr " &
                                   "WHERE RP_KO_Nr = [0] And KT_Rezept = 'R'"

    'Sql-Statement Kneter-Rezept löschen in RohParams
    Public Const sqlDelKneterRzt = "DELETE FROM RohParams WHERE RP_KO_Nr = [0]"
    'Sql-Statement Kneter-Rezept anlegen in RohParams
    Public Const sqlInsKneterRzt = "INSERT INTO RohParams(RP_KO_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES ([0],118,[1])"

    'Sql-Statement H2_Memo(Memo) aus Hinweise2
    Public Const sqlSelectH2 = "SELECT * FROM Hinweise2 WHERE H2_Typ = [0] AND H2_Typ2 = [1] AND H2_Id1= [2]"
    Public Const sqlUpdateH2 = "UPDATE Hinweise2 SET [3] WHERE H2_Typ = [0] AND H2_Typ2 = [1] AND H2_Id1= [2]"
    Public Const sqlInsertH2 = "INSERT INTO Hinweise2 (H2_Typ, H2_Typ2, H2_Id1, [3]) VALUES ([0],[1],[2],[4])"
    Public Const sqlDeleteH2 = "DELETE FROM Hinweise2 WHERE H2_Typ = [0] AND H2_Typ2 = [1] AND H2_Id1= [2]"


    'Sql-Statement Rezeptliste Kocher aus winback.Rezepte
    Public Const sqlKocherRzptListe = "SELECT RZ_Nr, RZ_Bezeichnung, RZ_Aenderung_Nr, RZ_Aenderung_Datum, " &
                                      "RZ_Aenderung_Name, RZ_Liniengruppe, FROM Rezepte WHERE RZ_Variante_Nr = 1 " &
                                      "RZ_Liniengruppe = [0] And RZ_Nr_AlNum = '[1]'"

    'Sql-Statement Rezeptliste Kocher aus winback.Rezepte
    Public Const sqlKocherDeleteRzpt = "DELETE FROM Rezepte WHERE RZ_Liniengruppe = [0] And RZ_Nr_AlNum = '[1]'"

End Class
