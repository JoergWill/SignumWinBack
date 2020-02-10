Imports WinBack.wb_Language

Public Class wb_Rohstoffe_Shared
    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eEdit_Leave(sender As Object)
    Public Shared Event eParam_Changed(Sender As Object)

    Public Shared RohGruppe As New SortedList
    Public Shared RohAktiv As New Hashtable
    Public Shared RohStoff As New wb_Komponente

    Private Shared _ErrorText As String = ""

    Enum AnzeigeFilter
        Undefined   ' nicht definiert
        Alle        ' alle aktiven Rohstoffe Typ > 100
        Hand        ' alle aktiven Rohstoffe Typ 102
        Auto        ' alle aktiven Rohstoffe Typ 101,103,104
        Sauerteig   ' alle aktiven Rohstoffe Sauerteig
        Install     ' alle inaktiven Rohstoffe
        Sonstige    ' alle Rohstoffe Typ 105,106
        RezeptKomp  ' alle aktiven Komponenten für Rezeptverwaltung (101..104, 118,128)
        OhneKneter  ' alle aktiven Komponenten für die Rezeptverwaltung ohne 118/128
        NurKneter   ' alle aktiven Komponenten 118
    End Enum

    Shared Sub New()
        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        Load_RohstoffTables()
    End Sub

    Public Shared ReadOnly Property ErrorText As String
        Get
            Return _ErrorText
        End Get
    End Property

    Private Shared Sub Load_RohstoffTables()
        'HashTable mit der Übersetzung der Rohstoffgruppen-Nummer in die Rohstoffgruppen-Bezeichnung laden
        'wenn die Rohstoffgruppen-Bezeichnung einen Verweis aus die Texte-Tabelle enthält wird die
        'entsprechende Übersetzung aus winback.Texte geladen
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)

        'SortedList Rohstoff-Gruppen
        winback.sqlSelect(wb_Sql_Selects.sqlRohstoffGrp)
        RohGruppe.Clear()
        While winback.Read
            If Not RohGruppe.ContainsKey((winback.iField("IP_Wert1int"))) Then
                RohGruppe.Add(winback.iField("IP_Wert1int"), TextFilter(winback.sField("IP_Wert4str")))
            End If
        End While
        winback.CloseRead()

        'HashTable aktive Rohstoffe (Silo-Umschaltung)
        winback.sqlSelect(wb_Sql_Selects.sqlRohstoffAut)
        RohAktiv.Clear()
        While winback.Read
            RohAktiv.Add(winback.iField("KO_Nr"), TextFilter(winback.sField("LG_aktiv")))
        End While
        winback.Close()
    End Sub

    Public Shared Function Add_RohstoffGruppe() As Integer
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)

        'nächste freie Gruppen-Nummer ermitteln (Artikel UND Rohstoff-Gruppe)
        Dim RohGrpNr As Integer = 1
        Do While RohGruppe.ContainsKey(RohGrpNr) Or wb_Artikel_Shared.ArtGruppe.ContainsKey(RohGrpNr)
            RohGrpNr += 1
        Loop

        'Datensatz neu anlegen
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlAddNewArtRohGruppe, RohGrpNr, "0"))
        winback.Close()

        'Liste neu aufbauen
        RohGruppe.Add(RohGrpNr, "")

        'Neue Artikel-Gruppen-Nummer zurückgegeben
        Return RohGrpNr
    End Function

    Public Shared Function Delete_RohstoffGruppe(Nr As Integer) As Boolean
        'Default Fehlermeldung
        _ErrorText = "Fehler beim Löschen der Rohstoffgruppe"
        'Prüfen ob die Artikel-Gruppen-Nummer existiert
        If RohGruppe.ContainsKey(Nr) Then
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
                        _ErrorText = "Rohstoffgruppe " & Nr & " wird noch verwendet !"
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
        RohStoff.SaveParameterArray()
        'Parameter-Fenster neu aufbauen (Anzeige)
        RaiseEvent eParam_Changed(sender)
    End Sub

    Public Shared Sub RezChrg_Changed(Sender As Object)
        'Daten in der Komponenten-Klasse sichern
        RohStoff.UpdateDB()
    End Sub
End Class
