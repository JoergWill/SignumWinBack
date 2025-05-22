Partial Public Class wb_Sql_Selects

    'Sql-Statement - Artikel
    Public Const sqlT1001A = "SELECT Komponenten.*, Rezepte.RZ_Nr_AlNum FROM (Komponenten INNER JOIN Rezepte ON (Komponenten.KA_RZ_Nr = Rezepte.RZ_Nr)) " &
                             "WHERE ((Komponenten.KA_Art = 1) AND (Komponenten.KO_Type = 0) AND (Rezepte.RZ_Variante_Nr = 1)) ORDER BY Komponenten.KO_Nr_AlNum"
    'Sql-Statement - Rohstoffe (ohne Sauerteig)
    Public Const sqlT1001R = "SELECT Komponenten.*, Rezepte.RZ_Nr_AlNum FROM (Komponenten INNER JOIN Rezepte ON (Komponenten.KA_RZ_Nr = Rezepte.RZ_Nr)) " &
                             "WHERE (Komponenten.KA_aktiv = 1) AND (Komponenten.KO_Type <> 0) ORDER BY Komponenten.KO_Nr_AlNum"

    'Sql-Statement - Rohstoffe (mit Sauerteig)
    Public Const sqlT1001R_ST = "SELECT Komponenten.*, Rezepte.RZ_Nr_AlNum FROM (Komponenten INNER JOIN Rezepte ON (Komponenten.KA_RZ_Nr = Rezepte.RZ_Nr)) " &
                                "WHERE (Komponenten.KA_aktiv = 1) AND (Komponenten.KO_Type > 99) ORDER BY Komponenten.KO_Nr_AlNum"

    'Sql-Statement - Nährwerte Artikel
    Public Const sqlT1002A = "SELECT Komponenten.*, RohParams.*, KomponTypen.*, Einheiten.* FROM (Komponenten " &
                             "INNER JOIN RohParams ON Komponenten.KO_Nr = RohParams.RP_Ko_Nr) " &
                             "INNER JOIN KomponTypen ON ((RohParams.RP_ParamNr = KomponTypen.KT_ParamNr) " &
                             "AND (RohParams.RP_Typ_Nr = KomponTypen.KT_Typ_Nr)) INNER JOIN Einheiten ON KomponTypen.KT_EinheitIndex = Einheiten.E_LfdNr " &
                             "WHERE (Komponenten.KO_Type = 0 AND KT_Typ_Nr = 301) ORDER BY Komponenten.KO_Nr_AlNum, Komponenten.KO_Nr, RP_Typ_Nr, RP_ParamNr"
    'Sql-Statement - Nährwerte Rohstoffe
    Public Const sqlT1002R = "SELECT Komponenten.*, RohParams.*, KomponTypen.*, Einheiten.* FROM (Komponenten " &
                             "INNER JOIN RohParams ON Komponenten.KO_Nr = RohParams.RP_Ko_Nr) " &
                             "INNER JOIN KomponTypen ON ((RohParams.RP_ParamNr = KomponTypen.KT_ParamNr) " &
                             "AND (RohParams.RP_Typ_Nr = KomponTypen.KT_Typ_Nr)) INNER JOIN Einheiten ON KomponTypen.KT_EinheitIndex = Einheiten.E_LfdNr " &
                             "WHERE (Komponenten.KO_Type > 99 AND KT_Typ_Nr = 301) ORDER BY Komponenten.KO_Nr_AlNum, Komponenten.KO_Nr, RP_Typ_Nr, RP_ParamNr"

    'Sql-Statemente - Rezepturen
    Public Const sqlT1006 = "SELECT Rezepte.*,RezeptVarianten.RV_Bezeichnung FROM Rezepte INNER JOIN RezeptVarianten " &
                            "ON Rezepte.RZ_Variante_Nr = RezeptVarianten.RV_Nr WHERE RZ_Variante_Nr > 0 ORDER BY RZ_Nr_AlNum"
    Public Const sqlT1006_ST = "SELECT Rezepte.*,RezeptVarianten.RV_Bezeichnung FROM Rezepte INNER JOIN RezeptVarianten " &
                               "ON Rezepte.RZ_Variante_Nr = RezeptVarianten.RV_Nr ORDER BY RZ_Nr_AlNum"

    Public Const sqlT1007 = "SELECT Rezepte.*, RezeptSchritte.*, Komponenten.*, Einheiten.* FROM " &
                            "(((Rezepte INNER JOIN RezeptSchritte ON (Rezepte.RZ_Variante_Nr = RezeptSchritte.RS_RZ_Variante_Nr) " &
                            "AND (Rezepte.RZ_Nr = RezeptSchritte.RS_RZ_Nr)) INNER JOIN Komponenten ON RezeptSchritte.RS_Ko_Nr = Komponenten.KO_Nr) " &
                            "INNER JOIN KomponTypen ON (RezeptSchritte.RS_ParamNr = KomponTypen.KT_ParamNr) AND " &
                            "(Komponenten.KO_Type = KomponTypen.KT_Typ_Nr)) INNER JOIN Einheiten ON KomponTypen.KT_EinheitIndex = Einheiten.E_LfdNr " &
                            "WHERE (KomponTypen.KT_Rezept) = 'R' AND Rezepte.RZ_Variante_Nr > 0 " &
                            "ORDER BY Rezepte.RZ_Nr_AlNum, Rezepte.Rz_Nr, Rezepte.RZ_Variante_Nr,RezeptSchritte.RS_Schritt_Nr, RezeptSchritte.RS_ParamNr"

    Public Const sqlT1007_ST = "SELECT Rezepte.*, RezeptSchritte.*, Komponenten.*, Einheiten.* FROM " &
                               "(((Rezepte INNER JOIN RezeptSchritte ON (Rezepte.RZ_Variante_Nr = RezeptSchritte.RS_RZ_Variante_Nr) " &
                               "AND (Rezepte.RZ_Nr = RezeptSchritte.RS_RZ_Nr)) INNER JOIN Komponenten ON RezeptSchritte.RS_Ko_Nr = Komponenten.KO_Nr) " &
                               "INNER JOIN KomponTypen ON (RezeptSchritte.RS_ParamNr = KomponTypen.KT_ParamNr) AND " &
                               "(Komponenten.KO_Type = KomponTypen.KT_Typ_Nr)) INNER JOIN Einheiten ON KomponTypen.KT_EinheitIndex = Einheiten.E_LfdNr " &
                               "WHERE (KomponTypen.KT_Rezept) = 'R' " &
                               "ORDER BY Rezepte.RZ_Nr_AlNum, Rezepte.Rz_Nr, Rezepte.RZ_Variante_Nr,RezeptSchritte.RS_Schritt_Nr, RezeptSchritte.RS_ParamNr"

    Public Const sqlT4105 = "Select *, SUM(BAK_ArbRZSchritte.B_ARS_Istwert) As BGesIstwert FROM BAK_ArbRezepte INNER JOIN BAK_ArbRZSchritte " &
                            "ON (BAK_ArbRezepte.B_ARZ_TW_Nr = BAK_ArbRZSchritte.B_ARS_TW_Nr) AND " &
                            "(BAK_ArbRezepte.B_ARZ_LiBeh_Nr = BAK_ArbRZSchritte.B_ARS_Beh_Nr) AND " &
                            "(BAK_ArbRezepte.B_ARZ_Art_Index = BAK_ArbRZSchritte.B_ARS_Art_Index) AND " &
                            "(BAK_ArbRezepte.B_ARZ_Charge_Nr = BAK_ArbRZSchritte.B_ARS_Charge_Nr) " &
                            "WHERE B_ARZ_TW_Nr >= [0] AND (BAK_ArbRezepte.B_ARZ_Status <> 'Exp' OR BAK_ArbRezepte.B_ARZ_Status IS NULL) AND " &
                            "(((BAK_ArbRZSchritte.B_ARS_ParamNr)=1)) " &
                            "GROUP BY BAK_ArbRezepte.B_ARZ_Charge_Nr, BAK_ArbRezepte.B_ARZ_Best_Nr, BAK_ArbRezepte.B_ARZ_Bezeichnung " &
                            "ORDER BY B_ARZ_TW_Nr,B_ARZ_TW_Idx,B_ARZ_Charge_Nr"

    Public Const sqlT4106 = "SELECT BAK_ArbRezepte.*, BAK_ArbRZSchritte.*, (B_ARZ_LiBeh_Nr - 100) as Linie " &
                            "FROM BAK_ArbRezepte INNER JOIN BAK_ArbRZSchritte ON " &
                            "(BAK_ArbRezepte.B_ARZ_TW_Nr = BAK_ArbRZSchritte.B_ARS_TW_Nr) AND " &
                            "(BAK_ArbRezepte.B_ARZ_LiBeh_Nr = BAK_ArbRZSchritte.B_ARS_Beh_Nr) AND " &
                            "(BAK_ArbRezepte.B_ARZ_Art_Index = BAK_ArbRZSchritte.B_ARS_Art_Index) AND " &
                            "(BAK_ArbRezepte.B_ARZ_Charge_Nr = BAK_ArbRZSchritte.B_ARS_Charge_Nr) " &
                            "WHERE (B_ARZ_LiBeh_Nr>100) AND (B_ARS_Schritt_SubNr >= 0) AND B_ARS_TW_Nr >= [0] AND " &
                            "(BAK_ArbRezepte.B_ARZ_Status <> 'Exp' OR BAK_ArbRezepte.B_ARZ_Status IS Null) " &
                            "ORDER BY BAK_ArbRezepte.B_ARZ_TW_Idx, BAK_ArbRezepte.B_ARZ_Timestamp, " &
                            "BAK_ArbRZSchritte.B_ARS_Art_Index, BAK_ArbRZSchritte.B_ARS_Schritt_Nr, BAK_ArbRZSchritte.B_ARS_ParamNr"

    'Sql-Statement Anzahl der Artikel
    Public Const sqlT1001A_Count = "SELECT COUNT(*) AS Anzahl FROM Komponenten WHERE KO_Type = 0"
    Public Const sqlT1002A_Count = "SELECT COUNT(*) AS Anzahl FROM (Komponenten INNER JOIN RohParams ON Komponenten.KO_Nr = RohParams.RP_Ko_Nr) WHERE KO_Type = 0"

    'Sql-Statement Anzahl der Rohstoffe
    Public Const sqlT1001R_Count = "SELECT COUNT(*) AS Anzahl FROM Komponenten WHERE KO_Type > 99 AND Komponenten.KA_aktiv = 1"
    Public Const sqlT1002R_Count = "SELECT COUNT(*) AS Anzahl FROM (Komponenten INNER JOIN RohParams ON Komponenten.KO_Nr = RohParams.RP_Ko_Nr) WHERE KO_Type > 99"
    Public Const sqlT1001R_ST_Count = "SELECT COUNT(*) AS Anzahl FROM Komponenten WHERE KO_Type <> 0 AND Komponenten.KA_aktiv = 1"

    'Sql-Statement Anzahl der Rezepte/Rezeptschritte
    Public Const sqlT1006_Count = "SELECT COUNT(*) AS Anzahl FROM Rezepte WHERE RZ_Variante_Nr = 1"
    Public Const sqlT1007_Count = "SELECT COUNT(*) AS Anzahl FROM RezeptSchritte WHERE RS_RZ_Variante_Nr = 1"
    Public Const sqlT1006_ST_Count = "SELECT COUNT(*) AS Anzahl FROM Rezepte"
    Public Const sqlT1007_ST_Count = "SELECT COUNT(*) AS Anzahl FROM RezeptSchritte"

    'Sql-Statement Anzahl der Chargen-Datensätze
    Public Const sqlT4105_Count = "SELECT COUNT(*) AS Anzahl FROM BAK_ArbRezepte WHERE (B_ARZ_LiBeh_Nr>100) AND " &
                                  "(B_ARS_Schritt_SubNr >= 0) And B_ARS_TW_Nr >= [0] AND " &
                                  "(BAK_ArbRezepte.B_ARZ_Status <> 'Exp' OR BAK_ArbRezepte.B_ARZ_Status IS NULL)"
    Public Const sqlT4106_Count = "SELECT COUNT(*) AS Anzahl FROM BAK_ArbRZSchritte WHERE B_ARS_TW_Nr >= [0]"

    'Sql-Statemant Suchen Artikel/Rohstoffe (Nummer und Type)
    Public Const sqlT1001_ExistsType = "SELECT * FROM Komponenten WHERE KO_Nr_AlNum = '[0]' AND KO_Type = [1]"
    Public Const sqlT1001_ExistsNumr = "SELECT * FROM Komponenten WHERE KO_Nr_AlNum = '[0]'"
    Public Const sqlT1001_ExistsLger = "SELECT * FROM Komponenten WHERE KA_Lagerort = [0] AND KO_Type <> 0 AND KO_Type <> 102"

End Class
