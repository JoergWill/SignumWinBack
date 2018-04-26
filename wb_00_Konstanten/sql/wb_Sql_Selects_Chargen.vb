Partial Public Class wb_sql_Selects
    'Sql-Statement Chargen-Kopf und Detail-Daten
    Public Const sqlExportChargen = "SELECT B_ARZ_Charge_Nr, B_ARZ_Typ, B_ARS_BF_Charge, " &
                                    "B_ARZ_Art_Einheit, B_ARZ_Sollmenge_kg, B_ARZ_Sollmenge_stueck, " &
                                    "B_ARS_Istwert, B_ARZ_Erststart, B_ARS_Gestartet, B_ARZ_TW_Nr, " &
                                    "B_ARS_ParamNr, B_KT_Typ_Nr, B_KT_EinheitIndex, " &
                                    "B_ARZ_KA_NrAlNum, B_KO_Nr_AlNum, (B_ARZ_LiBeh_Nr - 100) as Linie " &
                                    "FROM BAK_ArbRezepte INNER JOIN BAK_ArbRZSchritte ON " &
                                    "(BAK_ArbRezepte.B_ARZ_TW_Nr = BAK_ArbRZSchritte.B_ARS_TW_Nr) AND " &
                                    "(BAK_ArbRezepte.B_ARZ_LiBeh_Nr = BAK_ArbRZSchritte.B_ARS_Beh_Nr) AND " &
                                    "(BAK_ArbRezepte.B_ARZ_Art_Index = BAK_ArbRZSchritte.B_ARS_Art_Index) AND " &
                                    "(BAK_ArbRezepte.B_ARZ_Charge_Nr = BAK_ArbRZSchritte.B_ARS_Charge_Nr) " &
                                    "WHERE (B_ARZ_LiBeh_Nr>100) AND (B_ARS_Schritt_SubNr >= 0) AND " &
                                    "B_ARS_TW_Nr >= [0] AND BAK_ArbRezepte.B_ARZ_Status <> 'Exp' " &
                                    "ORDER BY BAK_ArbRezepte.B_ARZ_TW_Idx, BAK_ArbRezepte.B_ARZ_Timestamp, " &
                                    "BAK_ArbRZSchritte.B_ARS_Art_Index, BAK_ArbRZSchritte.B_ARS_Schritt_Nr, " &
                                    "BAK_ArbRZSchritte.B_ARS_ParamNr LIMIT [1]"

    Public Const sqlMarkChargen = "Update BAK_ArbRezepte SET B_ARZ_Status = 'Exp' WHERE " &
                                  "B_ARZ_TW_Nr = [0] and B_ARZ_Charge_Nr = [1]"

End Class
