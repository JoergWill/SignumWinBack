Partial Public Class wb_Sql_Selects
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
    'Sql-Statement Rezept-Verwendung aus winback.Komponenten
    Public Const sqlRezeptVerwendung = "SELECT KO_Nr_AlNum, KO_Bezeichnung, KO_Kommentar, KA_Art, KA_Matchcode " &
                                  "FROM Komponenten WHERE KA_RZ_Nr = [0]"
    Public Const sqlAddNewRezept = "INSERT INTO Rezepte([0]) VALUES ([1])"
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

    'Sql-Statement Kneter-Rezept aus winback.RohParams
    Public Const sqlKneterRezept = "SELECT * FROM RohParams INNER JOIN Komponenten ON RohParams.RP_Wert = Komponenten.KO_Nr " &
                                  "INNER JOIN KomponTypen On Komponenten.KO_Type = KomponTypen.KT_Typ_Nr " &
                                  "INNER JOIN Einheiten On KomponTypen.KT_EinheitIndex = Einheiten.E_LfdNr " &
                                   "WHERE RP_KO_Nr = [0] AND KT_Rezept = 'R'"

    'Sql-Statement Kneter-Rezept löschen in RohParams
    Public Const sqlDelKneterRzt = "DELETE FROM RohParams WHERE RP_KO_Nr = [0]"
    'Sql-Statement Kneter-Rezept anlegen in RohParams
    Public Const sqlInsKneterRzt = "INSERT INTO RohParams(RP_KO_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES ([0],118,[1])"

    'Sql-Statement H2_Memo(Memo) aus Hinweise2
    Public Const sqlSelectH2 = "SELECT * FROM Hinweise2 WHERE H2_Typ = [0] AND H2_Typ2 = [1] AND H2_Id1= [2]"
    Public Const sqlUpdateH2 = "UPDATE Hinweise2 SET [3] WHERE H2_Typ = [0] AND H2_Typ2 = [1] AND H2_Id1= [2]"
    Public Const sqlInsertH2 = "INSERT INTO Hinweise2 (H2_Typ, H2_Typ2, H2_Id1, [3]) VALUES ([0],[1],[2],[4])"
    Public Const sqlDeleteH2 = "DELETE FROM Hinweise2 WHERE H2_Typ = [0] AND H2_Typ2 = [1] AND H2_Id1= [2]"

End Class
