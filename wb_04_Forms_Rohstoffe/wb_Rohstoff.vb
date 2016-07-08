Imports WinBack.wb_Functions
Imports WinBack.wb_Global

Public Class wb_Rohstoff
    Private KO_Nr As Integer
    Private KO_Type As KomponTypen
    Private KO_Nr_AlNum As String
    Private KO_Bezeichnung As String
    Private KO_Kommentar As String
    Private KA_Preis As String
    Private KA_Grp1 As Integer
    Private KA_Grp2 As Integer

    Public ReadOnly Property Nr As Integer
        Get
            Return KO_Nr
        End Get
    End Property

    Public ReadOnly Property Type As KomponTypen
        Get
            Return KO_Type
        End Get
    End Property

    Public Property Nummer As String
        Set(value As String)
            KO_Nr_AlNum = value
        End Set
        Get
            Return KO_Nr_AlNum
        End Get
    End Property
    Public Property Bezeichung As String
        Set(value As String)
            KO_Bezeichnung = value
        End Set
        Get
            Return KO_Bezeichnung
        End Get
    End Property
    Public Property Kommentar As String
        Set(value As String)
            KO_Kommentar = value
        End Set
        Get
            Return KO_Kommentar
        End Get
    End Property
    Public Property Preis As String
        Set(value As String)
            KA_Preis = value
        End Set
        Get
            'Zahlenwerte aus der Datenbank immer inm Format de-DE
            Return FormatStr(KA_Preis, 4, 3, "de-DE")
        End Get
    End Property

    Public ReadOnly Property Gruppe1 As Integer
        Get
            Return KA_Grp1
        End Get
    End Property

    Public ReadOnly Property Gruppe2 As Integer
        Get
            Return KA_Grp2
        End Get
    End Property

    Friend Sub LoadData(dataGridView As wb_DataGridView)
        KO_Nr = CInt(dataGridView.Field("KO_Nr"))
        KO_Type = wb_Functions.IntToKomponType(dataGridView.Field("KO_Type"))
        KO_Nr_AlNum = dataGridView.Field("KO_Nr_AlNum")
        KO_Bezeichnung = dataGridView.Field("KO_Bezeichnung")
        KO_Kommentar = dataGridView.Field("KO_Kommentar")
        KA_Preis = dataGridView.Field("KA_Preis")
        KA_Grp1 = CInt(dataGridView.Field("KA_Grp1"))
        KA_Grp2 = CInt(dataGridView.Field("KA_Grp2"))
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

