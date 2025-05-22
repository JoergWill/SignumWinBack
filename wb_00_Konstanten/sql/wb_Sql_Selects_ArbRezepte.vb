Partial Public Class wb_Sql_Selects
    'Sql-Statement alle Kneter-Rezept-Schritte zur Chargen-Nummer
    Public Const sqlKneterArbRezepte = "SELECT * FROM ArbRZSchritte INNER JOIN KomponParams ON ARS_KO_Nr = KP_Ko_Nr " &
                                       "INNER JOIN ArbRezepte ON (ARS_Art_Index = ARZ_Art_Index AND ARS_Charge_Nr = ARZ_Charge_Nr) " &
                                       "WHERE ARS_KT_Typ_Nr = 118 AND KP_ParamNr = 2 AND ARS_Charge_Nr = '[0]' ORDER BY ARS_TW_Nr, ARS_RunIdx"
End Class
