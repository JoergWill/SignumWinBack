Public Class wb_User
    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eEdit_Leave(sender As Object)

    Public Shared GrpTexte As New Hashtable

    Public Shared aktUserName As String
    Public Shared aktUserPass As String
    Public Shared aktUserGroup As Integer

    Public Shared Sub LoadGrpTexte()
        'HashTable mit der Übersetzung der Gruppen-Nummer in die Gruppen-Bezeichnung laden
        'wenn die Gruppen-Bezeichnung einen Verweis aus die Texte-Tabelle enthält wird die
        'entsprechende Übersetzung aus winback.Texte geladen
        Dim winback As New wb_Sql(My.Settings.MySQLConWinBack, wb_Sql.dbType.mySql)
        winback.sqlSelect("SELECT * FROM ItemIDs WHERE II_ItemTyp = 500 ORDER BY II_ItemID")
        While winback.Read
            GrpTexte.Add(winback.iField("II_ItemId"), winback.sField("II_Kommentar"))
        End While
        winback.Close()
    End Sub
    Public Shared Function GrpTexte_GetKeyFromText(sText As String) As Integer
        Dim i As Integer = 0
        For Each item As DictionaryEntry In wb_User.GrpTexte
            If item.Value = sText Then
                i = item.Key
                Exit For
            End If
        Next
        Return i
    End Function
    Public Shared Sub GrpTexte_SelectComboBox(ByRef cb As Windows.Forms.ComboBox, sText As String)
        Dim i As Integer
        For i = 0 To cb.Items.Count - 1
            If sText = cb.Items(i).ToString Then
                cb.SelectedIndex = i
            End If
        Next
    End Sub

    Public Shared Sub Liste_Click(sender As Object)
        RaiseEvent eListe_Click(sender)
    End Sub

    Public Shared Sub Edit_Leave(sender As Object)
        RaiseEvent eEdit_Leave(sender)
    End Sub
End Class
