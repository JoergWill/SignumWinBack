Imports WinBack.wb_Functions
Public Class wb_Rohstoffe_Shared
    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eEdit_Leave(sender As Object)

    Public Shared RohGruppe As New SortedList
    Public Shared RohAktiv As New Hashtable
    Public Shared RohStoff As New wb_Rohstoff

    Enum AnzeigeFilter
        Alle        ' alle aktiven Rohstoffe Typ > 100
        Hand        ' alle aktiven Rohstoffe Typ 102
        Auto        ' alle aktiven Rohstoffe Typ 101,103,104
        Sauerteig   ' alle aktiven Rohstoffe Sauerteig
        Install     ' alle inaktiven Rohstoffe
        Sonstige    ' alle Rohstoffe Typ 105,106
    End Enum

    Public Shared Sub Load_RohstoffTables()
        'HashTable mit der Übersetzung der Rohstoffgruppen-Nummer in die Rohstoffgruppen-Bezeichnung laden
        'wenn die Rohstoffgruppen-Bezeichnung einen Verweis aus die Texte-Tabelle enthält wird die
        'entsprechende Übersetzung aus winback.Texte geladen
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)

        'SortedList Rohstoff-Gruppen
        winback.sqlSelect(wb_Sql_Selects.sqlRohstoffGrp)
        RohGruppe.Clear()
        While winback.Read
            RohGruppe.Add(winback.iField("IP_Wert1int"), TextFilter(winback.sField("IP_Wert4str")))
        End While
        winback.CloseRead()

        'HashTable aktive Rohstoffe (Silo-Umschaltung)
        winback.sqlSelect(wb_Sql_Selects.sqlRohstoffAct)
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
End Class
