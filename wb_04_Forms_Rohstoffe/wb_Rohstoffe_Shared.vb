Imports WinBack.wb_Language

Public Class wb_Rohstoffe_Shared
    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eEdit_Leave(sender As Object)
    Public Shared Event eParam_Changed(Sender As Object)

    Public Shared RohGruppe As New SortedList
    Public Shared RohAktiv As New Hashtable
    Public Shared RohStoff As New wb_Komponente 'Klasse wb_Rohstoffe wird durch wb_Komponente ersetzt

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
End Class
