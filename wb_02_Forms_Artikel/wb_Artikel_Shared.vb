Public Class wb_Artikel_Shared
    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eEdit_Leave(sender As Object)

    Public Shared Rzpt As New Hashtable

    Public Shared aktArtikelName As String

    Public Shared Sub LoadRzptNamen()
        'HashTable mit der Übersetzung der Rezept-Nummer(Idx) in die Rezept-Bezeichnung laden
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
        winback.sqlSelect("SELECT * FROM Rezepte")
        Rzpt.Clear()
        While winback.Read
            Rzpt.Add(winback.iField("RZ_Nr"), winback.sField("RZ_Bezeichnung"))
        End While
        winback.Close()
    End Sub

    Public Shared Sub Liste_Click(sender As Object)
        RaiseEvent eListe_Click(sender)
    End Sub

    Public Shared Sub Edit_Leave(sender As Object)
        RaiseEvent eEdit_Leave(sender)
    End Sub
End Class

'CREATE TABLE winback.Rezepte (
'    RZ_Nr int(10) UNSIGNED Not NULL Default 0,
'    RZ_Variante_Nr smallint(3) Not NULL Default 0,
'    RZ_Nr_AlNum varchar(8) Default NULL,
'    RZ_Bezeichnung varchar(60) Default NULL,
'    RZ_Gewicht varchar(30) Default '0,0',
'    RZ_Kommentar varchar(30) Default NULL,
'    RZ_Kurzname varchar(16) Default NULL,
'    RZ_Matchcode varchar(10) Default NULL,
'    RZ_Type Char(1) Default NULL,
'    RZ_Charge_Opt varchar(30) Default NULL,
'    RZ_Charge_Min varchar(30) Default NULL,
'    RZ_Charge_Max varchar(30) Default NULL,
'    RZ_Aenderung_Datum datetime Default NULL,
'    RZ_Aenderung_User int(10) Default -1,
'    RZ_Aenderung_Name varchar(24) Default NULL,
'    RZ_Aenderung_Nr smallint(5) Default NULL,
'    RZ_Teigtemperatur varchar(10) Default NULL,
'    RZ_Kneterkennlinie smallint(5) Default NULL,
'    RZ_Verarbeitungshinweise varchar(100) Default NULL,
'    RZ_Liniengruppe tinyint(3) Default NULL,
'    RZ_Gruppe int(10) Default 0,
'    KA_Gruppe int(10) Default 0,
'    RZ_Timestamp timestamp(14),
'    PRIMARY KEY (RZ_Nr, RZ_Variante_Nr)
')
'TYPE = MYISAM
'AVG_ROW_LENGTH = 98;