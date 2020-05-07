Public Class wb_User_Shared
    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eEdit_Leave(sender As Object)
    Public Shared Event eData_Reload(sender As Object)

    Public Shared GrpTexte As New SortedList
    Public Shared User As New wb_User
    Public Shared Gruppe As New wb_User_Gruppe

    Shared Sub New()
        LoadGrpTexte()
    End Sub

    Public Shared Sub LoadGrpTexte()
        'HashTable mit der Übersetzung der Gruppen-Nummer in die Gruppen-Bezeichnung laden
        'wenn die Gruppen-Bezeichnung einen Verweis aus die Texte-Tabelle enthält wird die
        'entsprechende Übersetzung aus winback.Texte geladen
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserGrpTxt, wb_Language.GetLanguageNr()))
        GrpTexte.Clear()
        While winback.Read
            GrpTexte.Add(winback.iField("II_ItemId"), wb_Language.TextFilter(winback.sField("T_Text")))
        End While
        winback.Close()
    End Sub

    Public Shared Sub Liste_Click(sender As Object)
        RaiseEvent eListe_Click(sender)
    End Sub

    Public Shared Sub Edit_Leave(sender As Object)
        RaiseEvent eEdit_Leave(sender)
    End Sub

    Public Shared Sub Reload(Sender As Object)
        RaiseEvent eData_Reload(Sender)
    End Sub
End Class
