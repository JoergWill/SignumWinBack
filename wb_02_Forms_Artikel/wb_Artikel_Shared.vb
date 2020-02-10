Imports WinBack.wb_Language

Public Class wb_Artikel_Shared

    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eEdit_Leave(sender As Object)
    'TODO Prüfen ob eParamChanged notwendig ist (kopiert von wb_Rohstoffe_Shared aus Rohstoffe Parameter)
    'Public Shared Event eParam_Changed(Sender As Object)

    Public Shared Artikel As New wb_Komponente
    Public Shared Rzpt As New Hashtable
    Public Shared ArtGruppe As New SortedList

    Public Shared aktArtikelName As String
    Private Shared _ErrorText As String = ""

    Shared Sub New()
        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        LoadRzptNamen()
        'HashTable mit den Artikelgruppen laden
        Load_ArtikelTables()
    End Sub

    Public Shared ReadOnly Property ErrorText As String
        Get
            Return _ErrorText
        End Get
    End Property

    Public Shared Sub LoadRzptNamen()
        'HashTable mit der Übersetzung der Rezept-Nummer(Idx) in die Rezept-Bezeichnung laden
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.sqlRezeptNrName)
        Rzpt.Clear()
        While winback.Read
            Rzpt.Add(winback.iField("RZ_Nr"), winback.sField("RZ_Bezeichnung"))
            'Debug.Print("Artikel-Shared Rezeptnummer-Name Umsetzliste " & winback.iField("RZ_Nr") & "/" & winback.sField("RZ_Bezeichnung"))
        End While
        winback.Close()
    End Sub

    Public Shared Sub Load_ArtikelTables()
        'HashTable mit der Übersetzung der Artikelgruppen-Nummer in die Artikelgruppen-Bezeichnung laden
        'wenn die Artikelgruppen-Bezeichnung einen Verweis aus die Texte-Tabelle enthält wird die
        'entsprechende Übersetzung aus winback.Texte geladen
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)

        'SortedList Artikel-Gruppen
        winback.sqlSelect(wb_Sql_Selects.sqlartikelGrp)
        ArtGruppe.Clear()
        While winback.Read
            If Not ArtGruppe.ContainsKey((winback.iField("IP_Wert1int"))) Then
                ArtGruppe.Add(winback.iField("IP_Wert1int"), TextFilter(winback.sField("IP_Wert4str")))
            End If
        End While
        winback.Close()
    End Sub

    Public Shared Function Add_ArtikelGruppe() As Integer
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)

        'nächste freie Gruppen-Nummer ermitteln (Artikel UND Rohstoff-Gruppe)
        Dim ArtGrpNr As Integer = 1
        Do While ArtGruppe.ContainsKey(ArtGrpNr) Or wb_Rohstoffe_Shared.RohGruppe.ContainsKey(ArtGrpNr)
            ArtGrpNr += 1
        Loop

        'Datensatz neu anlegen
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlAddNewArtRohGruppe, ArtGrpNr, "1"))
        winback.Close()

        'Liste neu aufbauen
        ArtGruppe.Add(ArtGrpNr, "")

        'Neue Artikel-Gruppen-Nummer zurückgegeben
        Return ArtGrpNr
    End Function

    Public Shared Function Delete_ArtikelGruppe(Nr As Integer) As Boolean
        'Default Fehlermeldung
        _ErrorText = "Fehler beim Löschen der Artikelgruppe"
        'Prüfen ob die Artikel-Gruppen-Nummer existiert
        If ArtGruppe.ContainsKey(Nr) Then
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'Prüfen ob die Artikelgruppe noch verwendet wird
            If winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUsedRohArtGruppe, Nr.ToString)) Then
                If winback.Read Then
                    If winback.iField("Used") = 0 Then
                        'Artikelgruppe löschen
                        winback.CloseRead()
                        If winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDeleteRohArtGruppe, Nr.ToString)) Then
                            _ErrorText = ""
                            Return True
                        End If
                    Else
                        'Artikelgruppe wird noch verwendet
                        _ErrorText = "Artikelgruppe " & Nr & " wird noch verwendet !"
                        Return False
                    End If
                    Return False
                End If
            Else
                Return False
            End If
        Else
            'Artikelgruppe existiert nicht
            Return False
        End If
        Return False
    End Function

    Public Shared Sub Liste_Click(sender As Object)
        RaiseEvent eListe_Click(sender)
    End Sub

    Public Shared Sub Edit_Leave(sender As Object)
        RaiseEvent eEdit_Leave(sender)
    End Sub

    Public Shared Sub Param_Changed(sender As Object)
        'alle geänderten Rohstoff-Parameter in Datenbank schreiben (WinBack und OrgaBack)
        Artikel.SaveParameterArray()
        'Parameter-Fenster neu aufbauen (Anzeige)
        'TODO Prüfen ob dieser Event notwendig ist !! (Kopiert von Rohstoffe_Shared)
        'RaiseEvent eParam_Changed(sender)
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