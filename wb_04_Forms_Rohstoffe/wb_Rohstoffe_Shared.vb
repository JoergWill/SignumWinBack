Public Class wb_Rohstoffe_Shared
    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eEdit_Leave(sender As Object)

    Public Shared RohGruppe As New SortedList
    Public Shared RohStoff As New wb_Rohstoff

    Public Shared Sub Load_RohGruppenTexte()
        'HashTable mit der Übersetzung der Rohstoffgruppen-Nummer in die Rohstoffgruppen-Bezeichnung laden
        'wenn die Rohstoffgruppen-Bezeichnung einen Verweis aus die Texte-Tabelle enthält wird die
        'entsprechende Übersetzung aus winback.Texte geladen
        Dim winback As New wb_Sql(My.Settings.MySQLConWinBack, wb_Sql.dbType.mySql)
        winback.sqlSelect(wb_Sql_Selects.sqlRohstoffGruppen)
        RohGruppe.Clear()
        While winback.Read
            RohGruppe.Add(winback.iField("IP_Wert1int"), winback.sField("IP_Wert4str"))
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
