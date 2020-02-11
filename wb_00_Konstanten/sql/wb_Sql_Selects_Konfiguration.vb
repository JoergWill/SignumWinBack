Partial Public Class wb_Sql_Selects
    'Sql-Statement Konfiguration
    Public Const sqlKonfiguration = "SELECT * FROM Konfiguration WHERE KF_Tag LIKE '[0]'"

    'Sql-Statement Liniengruppen
    Public Const sqlLinienGruppen = "SELECT * FROM LinienGruppen ORDER BY LG_Nr"
    Public Const sqlAufarbeitung = "SELECT * FROM LinienGruppen WHERE LG_Nr > [0] ORDER BY LG_Nr"
    Public Const sqlAddNewLinienGruppe = "INSERT INTO LinienGruppen([0]) VALUES ([1])"
    Public Const sqlRezeptLinienGruppe = "SELECT COUNT(*) AS Used FROM Rezepte WHERE RZ_Liniengruppe = [0]"
    Public Const sqlDeleteLinienGruppe = "DELETE FROM LinienGruppen WHERE LG_Nr = [0]"
    Public Const sqlChangeLinienGruppe = "UPDATE Rezepte SET RZ_Liniengruppe = [0] WHERE RZ_Liniengruppe = [1]"
    Public Const sqlChangeBackort = "UPDATE RohParams SET RP_Wert = [0] WHERE RP_Typ_Nr = 300 AND RP_ParamNr = [2] AND RP_Wert = [1]"

    'Sql-Statement Linien
    Public Const sqlLinien = "SELECT * FROM Linien ORDER BY L_Nr"
    'Sql-Statement Update(Replace)Linien
    Public Const sqlUpdteLinien = "REPLACE INTO LinienGruppen (LG_Nr, LG_Bezeichnung, LG_Linien) VALUES ([0],'[1]',[0])"

    'Sql-Statement Artikel- und Rohstoff-Gruppen
    Public Const sqlArtRohGruppen = "SELECT IP_Lfd_Nr, IP_Wert4Str, IP_Wert5Str, IP_Wert1int, IP_Wert2int, IP_Wert3int, " &
                                    "IP_ItemTyp, IP_ItemID, IP_ItemAttr FROM ItemParameter WHERE IP_ItemTyp=600 ORDER BY IP_Lfd_Nr"

    'Sql-Statement Artikel-/Rohstoffgruppe neu anlegen
    Public Const sqlAddNewArtRohGruppe = "INSERT INTO ItemParameter(IP_ItemTyp, IP_ItemID, IP_ItemAttr, IP_Lfd_Nr, " &
                                          "IP_Wert1int, IP_Wert2int, IP_Wert3int, IP_Wert4str, IP_Wert5str)" &
                                          "VALUES (600, 1, 601, [0], [0], 0, [1], '', '')"

    'Sql-Statement Artikel-/Rohstoffgruppe Vewendung prüfen
    Public Const sqlUsedRohArtGruppe = "SELECT COUNT(*) AS Used FROM Komponenten WHERE ((KA_Grp1 = [0]) OR (KA_Grp2 = [0]))"

    'Sql-Statement Artikel-/Rohstoffgruppe löschen
    Public Const sqlDeleteRohArtGruppe = "DELETE FROM ItemParameter WHERE IP_Lfd_Nr = [0] AND IP_ItemTyp=600"

    'Sql-Statement Einheiten
    Public Const sqlEinheiten = "Select * FROM Einheiten ORDER BY E_LfdNr"

    'Sql-Statement E-Nummern
    Public Const sqlENummern = "Select * FROM enummern"

    'Sql-Statement alle Texte aus winback.Texte
    Public Const sqlWinBackTxte = "SELECT T_TextIndex, T_Typ, T_Text FROM Texte WHERE T_Sprache = [0]"

    'Sql-Statement AktionsTimer
    Public Const sqlAktionsTimerAktiv = "Select * FROM AktionsTimer WHERE AT_Ziel_Aktion = 1 And (AT_Quelle_Typ = 'TW' OR AT_Quelle_Typ LIKE 'winback%' OR AT_Quelle_Typ LIKE 'office%')"
    Public Const sqlAktionsTimer = "Select * FROM AktionsTimer WHERE (AT_Quelle_Typ = 'TW' OR AT_Quelle_Typ LIKE 'winback%' OR AT_Quelle_Typ LIKE 'office%')"
    Public Const sqlUpdtAktionsTimer = "UPDATE AktionsTimer SET [0] WHERE AT_idx = '[1]'"

    'Sql-Statement Kocher
    Public Const sqlKocherBC9 = "SELECT * FROM BC9000Liste WHERE BC9_BC_Typ = 3 ORDER BY BC9_Nr"

End Class

'IP_ItemTyp
'IP_ItemID
'IP_ItemAttr
'IP_Lfd_Nr
'IP_Wert1int
'IP_Wert2int
'IP_Wert3int
'IP_Wert4str
'IP_Wert5str
'IP_Timestamp
