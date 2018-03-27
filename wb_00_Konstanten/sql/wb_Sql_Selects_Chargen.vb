Partial Public Class wb_sql_Selects
    'Sql-Statement Chargen-Kopf und Detail-Daten
    Public Const sqlExportChargen = "SELECT BAK_ArbRezepte.*, BAK_ArbRZSchritte.*, (B_ARZ_LiBeh_Nr - 100) as Linie " &
                                    "FROM BAK_ArbRezepte INNER JOIN BAK_ArbRZSchritte ON " &
                                    "(BAK_ArbRezepte.B_ARZ_TW_Nr = BAK_ArbRZSchritte.B_ARS_TW_Nr) AND " &
                                    "(BAK_ArbRezepte.B_ARZ_LiBeh_Nr = BAK_ArbRZSchritte.B_ARS_Beh_Nr) AND " &
                                    "(BAK_ArbRezepte.B_ARZ_Art_Index = BAK_ArbRZSchritte.B_ARS_Art_Index) AND " &
                                    "(BAK_ArbRezepte.B_ARZ_Charge_Nr = BAK_ArbRZSchritte.B_ARS_Charge_Nr) " &
                                    "WHERE (B_ARZ_LiBeh_Nr>100) AND (B_ARS_Schritt_SubNr >= 0) AND " &
                                    "B_ARS_TW_Nr >= [0] AND BAK_ArbRezepte.B_ARZ_Status <> 'Exp' " &
                                    "ORDER BY BAK_ArbRezepte.B_ARZ_TW_Idx, BAK_ArbRezepte.B_ARZ_Timestamp, " &
                                    "BAK_ArbRZSchritte.B_ARS_Art_Index, BAK_ArbRZSchritte.B_ARS_Schritt_Nr, " &
                                    "BAK_ArbRZSchritte.B_ARS_ParamNr"

End Class
