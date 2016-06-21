Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Liste
    Inherits DockContent

    Private Sub wb_Rohstoffe_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "Nummer", "&Name", "Aktiv", "Kommentar", "", "", "", "", ""}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid füllen
        Dim sql As String = "SELECT KO_Nr, KO_Nr_AlNum, KO_Bezeichnung, KA_aktiv, KO_Kommentar, KO_Type, " &
                            "KA_Kurzname, KA_Matchcode, KA_Grp1, KA_Grp2 FROM Komponenten WHERE KO_Type <> 0"
        DataGridView.LoadData(sql, "RezeptListe", wb_Sql.dbType.mySql)
        'DataGrid Initialisierung Anzeige ohne Sauerteig
        DataGridView.Filter = "KO_Type > 100"

    End Sub
End Class

'CREATE TABLE winback.Komponenten (
'    KO_Nr int(10) UNSIGNED Not NULL Default 0,
'    KO_Type tinyint(3) UNSIGNED Not NULL Default 0,
'    KO_Bezeichnung varchar(60) Default NULL,
'    KO_Kommentar varchar(50) Default NULL,
'    KO_Nr_AlNum varchar(16) Default NULL,
'    KO_Temp_Korr smallint(5) Default 0,
'    KA_Nr int(11) Default 0,
'    KA_Kurzname varchar(16) Default NULL,
'    KA_Matchcode varchar(10) Default NULL,
'    KA_Art tinyint(3) UNSIGNED Default 0,
'    KA_Artikel_Typ smallint(6) Default 0,
'    KA_RZ_Nr int(10) UNSIGNED Default 0,
'    KA_Lagerort varchar(16) Default NULL,
'    KA_Prod_Linie tinyint(3) UNSIGNED Default 0,
'    KA_Stueckgewicht varchar(20) Default NULL,
'    KA_Charge_Opt varchar(30) Default NULL,
'    KA_Charge_Min varchar(30) Default NULL,
'    KA_Charge_Max varchar(30) Default NULL,
'    KA_Charge_Opt_kg varchar(30) Default NULL,
'    KA_Charge_Min_kg varchar(30) Default NULL,
'    KA_Charge_Max_kg varchar(30) Default NULL,
'    KA_RS_veraenderbar Char(1) Default NULL,
'    KA_RS_abh_von_RZ_Menge Char(1) Default NULL,
'    KA_RS_aendert_WasMenge Char(1) Default NULL,
'    KA_zaehlt_zu_RZ_Gesamtmenge Char(1) Default NULL,
'    KA_spez_WKap varchar(30) Default NULL,
'    KA_alternativ_RS varchar(16) Default NULL,
'    KA_Verarbeitungshinweise varchar(100) Default NULL,
'    KA_aktiv smallint(5) Default 1,
'    KA_Preis varchar(20) Default NULL,
'    KA_PreisEinheit smallint(5) Default 0,
'    KA_Grp1 int(10) Default 0,
'    KA_Grp2 int(10) Default 0,
'    KA_Timestammp timestamp(14),
'    PRIMARY KEY (KO_Nr)
')
'TYPE = MYISAM
'AVG_ROW_LENGTH = 96;