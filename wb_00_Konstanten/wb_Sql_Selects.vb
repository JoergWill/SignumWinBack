﻿Public Class wb_Sql_Selects

    'Sql-Statement Rezeptliste aus winback.Rezepte
    Public Const sqlRezeptListe = "SELECT RZ_Nr, RZ_Nr_AlNum, RZ_Bezeichnung, RZ_Variante_Nr, RZ_Kommentar, RZ_Gewicht, " &
                                  "RZ_Aenderung_Nr, RZ_Aenderung_Datum, RZ_Aenderung_Name, RZ_Liniengruppe, " &
                                  "RZ_Charge_Min, RZ_Charge_Max, RZ_Charge_Opt FROM Rezepte"

    'Sql-Statement Rohstoffliste aus winback.Komponenten
    Public Const sqlRohstoffListe = "SELECT KO_Nr, KO_Nr_AlNum, KO_Bezeichnung, KA_aktiv, KO_Kommentar, KO_Type, " &
                                    "KA_Kurzname, KA_Matchcode, KA_Preis, KA_Grp1, KA_Grp2 FROM Komponenten WHERE KO_Type <> 0"
    'Sql-Statement RohstoffGruppen aus winback.
    Public Const sqlRohstoffGruppen = "SELECT IP_Wert1int, IP_Wert4str FROM ItemParameter WHERE " &
                                      "IP_ItemTyp = 600 AND IP_Wert3int = 0 ORDER BY IP_Lfd_Nr DESC"

    'Sql-Statement Artikelliste aus winback.Komponenten
    Public Const sqlArtikelListe = "SELECT KO_Nr, KO_Nr_AlNum, KO_Bezeichnung, KA_RZ_Nr, KO_Kommentar, KO_Type, " &
                                   "KA_Kurzname, KA_Matchcode, KA_Grp1, KA_Grp2 FROM Komponenten WHERE KO_Type = 0"

    'Sql-Statement Userliste aus winback.ItemParameter
    Public Const sqlUserListe = "SELECT IP_ItemTyp, IP_Lfd_Nr, IP_Wert4str, IP_ItemID, IP_Wert1int FROM ItemParameter " &
                                "WHERE IP_ItemTyp = 500 AND IP_ItemAttr = 501 AND IP_Wert1int <> 709760"

    'Sql-Statement alle Texte aus winback.Texte
    Public Const sqlTexte = "SELECT T_TextIndex, T_Typ, T_Text FROM Texte WHERE T_Sprache = [0]"

    Friend Shared Function setParams(sql As String, Param1 As String, Optional Param2 As String = "-", Optional Param3 As String = "-") As String
        sql = Replace(sql, "[0]", Param1)
        If Param2 <> "-" Then
            sql = Replace(sql, "[0]", Param2)
        End If
        If Param3 <> "-" Then
            sql = Replace(sql, "[0]", Param3)
        End If
        Return sql
    End Function
End Class