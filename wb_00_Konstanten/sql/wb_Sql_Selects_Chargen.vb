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
                                    "ORDER BY BAK_ArbRezepte.B_ARZ_TW_Nr, BAK_ArbRezepte.B_ARZ_TW_Idx, BAK_ArbRezepte.B_ARZ_Timestamp, " &
                                    "BAK_ArbRZSchritte.B_ARS_Art_Index, BAK_ArbRZSchritte.B_ARS_Schritt_Nr, " &
                                    "BAK_ArbRZSchritte.B_ARS_ParamNr LIMIT [1]"

    Public Const sqlMarkChargen = "Update BAK_ArbRezepte SET B_ARZ_Status = 'Exp' WHERE " &
                                  "B_ARZ_TW_Nr = [0] and B_ARZ_Charge_Nr = [1]"

    'Sql-Statement Liste Tageswechsel
    Public Const sqlListeTW = "SELECT * from Tageswechsel ORDER BY TW_Nr"

    'Public Const sqlListeTWFilter = "SELECT * from Tageswechsel WHERE (FORMATDATETIME('yyyymmddhhnnss', TRUNC([0]) " &
    '                               "<= TW_Beginn) AND (TW_Beginn <=  (FORMATDATETIME('yyyymmddhhnnss', TRUNC([1])"

    Public Const sqlChargenDetails = "Select BAK_ArbRezepte.*, BAK_ArbRZSchritte.*, (B_ARZ_LiBeh_Nr - 100) as Linie " &
                                     "FROM BAK_ArbRezepte INNER JOIN BAK_ArbRZSchritte ON " &
                                     "(BAK_ArbRezepte.B_ARZ_TW_Nr = BAK_ArbRZSchritte.B_ARS_TW_Nr) AND " &
                                     "(BAK_ArbRezepte.B_ARZ_LiBeh_Nr = BAK_ArbRZSchritte.B_ARS_Beh_Nr) AND " &
                                     "(BAK_ArbRezepte.B_ARZ_Art_Index = BAK_ArbRZSchritte.B_ARS_Art_Index) AND " &
                                     "(BAK_ArbRezepte.B_ARZ_Charge_Nr = BAK_ArbRZSchritte.B_ARS_Charge_Nr) " &
                                     "WHERE (B_ARZ_LiBeh_Nr > 100) AND B_ARZ_TW_Nr = " & Chr(34) & "[0]" & Chr(34) & " " &
                                     "ORDER BY [1], B_ARZ_TW_Idx, BAK_ArbRZSchritte.B_ARS_Index"

    Public Const sqlChargenTTS = "Select BAK_ArbRezepte.*, BAK_ArbRZSchritte.*, (B_ARZ_LiBeh_Nr - 100) as Linie " &
                                 "FROM BAK_ArbRezepte INNER JOIN BAK_ArbRZSchritte ON " &
                                 "(BAK_ArbRezepte.B_ARZ_TW_Nr = BAK_ArbRZSchritte.B_ARS_TW_Nr) AND " &
                                 "(BAK_ArbRezepte.B_ARZ_LiBeh_Nr = BAK_ArbRZSchritte.B_ARS_Beh_Nr) AND " &
                                 "(BAK_ArbRezepte.B_ARZ_Art_Index = BAK_ArbRZSchritte.B_ARS_Art_Index) AND " &
                                 "(BAK_ArbRezepte.B_ARZ_Charge_Nr = BAK_ArbRZSchritte.B_ARS_Charge_Nr) " &
                                 "WHERE (B_ARZ_LiBeh_Nr > 100) AND [0] AND B_ARZ_Nr = [1] AND B_ARZ_RZ_Variante_NR = [2] " &
                                 "ORDER BY B_ARZ_TW_Idx, BAK_ArbRZSchritte.B_ARS_Index"

    'Public Const sqlStatRezepte = "SELECT BAK_ArbRZSchritte.B_ARS_Ko_Nr, BAK_ArbRZSchritte.B_KO_Nr_AlNum, " &
    '                              "BAK_ArbRZSchritte.B_E_Einheit, BAK_ArbRZSchritte.B_KO_Bezeichnung, " &
    '                              "BAK_ArbRezepte.B_RZ_Nr_AlNum, BAK_ArbRezepte.B_RZ_Bezeichnung, (B_ARZ_LiBeh_Nr - 100) AS Linie, " &
    '                              "BAK_ArbRezepte.B_ARZ_Charge_Nr, BAK_ArbRezepte.B_ARZ_Typ, BAK_ArbRezepte.B_ARZ_Sollmenge_kg, " &
    '                              "BAK_ArbRezepte.B_ARZ_Sollmenge_stueck, BAK_ArbRZSchritte.B_ARS_Wert, BAK_ArbRZSchritte.B_ARS_Istwert, " &
    '                              "BAK_ArbRZSchritte.B_ARS_Gestartet, BAK_ArbRZSchritte.B_ARS_User_Nr, BAK_ArbRZSchritte.B_ARS_Status, " &
    '                              "BAK_ArbRezepte.B_ARZ_Zp_Gestartet, Tageswechsel.TW_Beginn,B_ARS_User_Name " &
    '                              "FROM (Tageswechsel INNER JOIN BAK_ArbRezepte ON Tageswechsel.TW_Nr = BAK_ArbRezepte.B_ARZ_TW_Nr) " &
    '                              "INNER JOIN BAK_ArbRZSchritte ON (BAK_ArbRezepte.B_ARZ_TW_Nr = BAK_ArbRZSchritte.B_ARS_TW_Nr) " &
    '                              "AND (BAK_ArbRezepte.B_ARZ_Charge_Nr = BAK_ArbRZSchritte.B_ARS_Charge_Nr)"

    Public Const sqlStatRezepte = "SELECT BAK_ArbRezepte.*, BAK_ArbRZSchritte.*, (B_ARZ_LiBeh_Nr - 100) as Linie " &
                                  "FROM (Tageswechsel INNER JOIN BAK_ArbRezepte ON Tageswechsel.TW_Nr = BAK_ArbRezepte.B_ARZ_TW_Nr) " &
                                  "INNER JOIN BAK_ArbRZSchritte ON (BAK_ArbRezepte.B_ARZ_TW_Nr = BAK_ArbRZSchritte.B_ARS_TW_Nr) " &
                                  "AND (BAK_ArbRezepte.B_ARZ_Charge_Nr = BAK_ArbRZSchritte.B_ARS_Charge_Nr)"

    Public Const sqlStatRohVerbrauch = "SELECT * FROM BAK_ArbRZSchritte"

    Public Const sqlStatRohDetails = "Select BAK_ArbRezepte.*, BAK_ArbRZSchritte.*, (B_ARZ_LiBeh_Nr - 100) as Linie " &
                                     "FROM BAK_ArbRezepte INNER JOIN BAK_ArbRZSchritte ON " &
                                     "(BAK_ArbRezepte.B_ARZ_TW_Nr = BAK_ArbRZSchritte.B_ARS_TW_Nr) AND " &
                                     "(BAK_ArbRezepte.B_ARZ_LiBeh_Nr = BAK_ArbRZSchritte.B_ARS_Beh_Nr) AND " &
                                     "(BAK_ArbRezepte.B_ARZ_Art_Index = BAK_ArbRZSchritte.B_ARS_Art_Index) AND " &
                                     "(BAK_ArbRezepte.B_ARZ_Charge_Nr = BAK_ArbRZSchritte.B_ARS_Charge_Nr) "

    Public Const sqlTWNrStrt = "SELECT TW_Nr from Tageswechsel WHERE '[0]' <= TW_Beginn " &
                               "ORDER BY TW_Nr ASC LIMIT 1"
    Public Const sqlTWNrEnde = "SELECT TW_Nr from Tageswechsel WHERE TW_Beginn <= '[0]' " &
                                "ORDER BY TW_Nr DESC LIMIT 1"

    Public Const sqlMaxTWNummer = "SELECT MAX(TW_Nr) from Tageswechsel"
    Public Const sqlInsertTWNummer = "INSERT INTO Tageswechsel SET TW_NR = [0], TW_Seg_Nr = [1], TW_Beginn = '[2]', TW_Ende = '[3]'"

    'Löschen Arbeits-Rezepte mit TWNr=0
    Public Const DelArbRezepte = "DELETE FROM ArbRezepte WHERE ARZ_TW_Nr = 0 AND ARZ_LiBeh_Nr = [0]"
    'Löschen ArbRZSchritte und AbrRZParams  mit TWNr=0
    'TODO was ist mit ArpRZPArams (welcher Join?)
    Public Const DelArbRZSchritte = "DELETE FROM ArbRZSchritte WHERE ARS_TW_Nr = 0 AND ARS_Beh_Nr = [0]"

    Public Const InsertBAKArbRZSchritte = "INSERT INTO  BAK_ArbRZSchritte SET [0]"
    Public Const InsertBAKArbRezepte = "INSERT INTO  BAK_ArbRezepte SET [0]"


    '     'Select BAK_ArbRezepte.*, BAK_ArbRZSchritte.*, (B_ARZ_LiBeh_Nr - 100) as Linie ' +
    '     'FROM BAK_ArbRezepte  ' +
    '     'INNER JOIN BAK_ArbRZSchritte ON ' +
    '     '(BAK_ArbRezepte.B_ARZ_TW_Nr = BAK_ArbRZSchritte.B_ARS_TW_Nr) AND ' +
    '     '(BAK_ArbRezepte.B_ARZ_LiBeh_Nr = BAK_ArbRZSchritte.B_ARS_Beh_Nr) AND ' + { 21-Nov-2003 }
    '     '(BAK_ArbRezepte.B_ARZ_Art_Index = BAK_ArbRZSchritte.B_ARS_Art_Index) AND ' + { 21-Nov-2003 }
    '     '(BAK_ArbRezepte.B_ARZ_Charge_Nr = BAK_ArbRZSchritte.B_ARS_Charge_Nr) ';

End Class
