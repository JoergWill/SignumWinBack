Imports Signum.OrgaSoft.AddIn.wb_Functions

Public Class wb_User_Liste

    Private Sub wb_User_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Data_Load()
    End Sub

    Private Sub Data_Load()
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "Type", "Nummer", "Bezeichnung", "&Kommentar"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        DataGridView.LoadData("SELECT KO_Nr, KO_Type, KO_Nr_AlNum, KO_Bezeichnung, KO_Kommentar FROM Komponenten;", "UserListe", wb_Sql.dbType.mySql)
        'DataGridView.LoadData("SELECT * FROM ItemParameter WHERE IP_ItemTyp = 500;", "UserListe", wb_Sql.dbType.mySql)
    End Sub

    Private Sub wb_User_Liste_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DataGridView.SaveToDisk("UserListe")
    End Sub

    Private Sub DataGridView_HasChanged() Handles DataGridView.HasChanged
        TextBox1.Text = DataGridView.Field("KO_Bezeichnung")
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Debug.Print("CheckChanged RB1")
        If RadioButton1.Checked Then
            DataGridView.Filter = ""
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Debug.Print("CheckChanged RB2")
        If RadioButton2.Checked Then
            DataGridView.Filter = "KO_Type = 101"
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        Debug.Print("CheckChanged RB3")
        If RadioButton3.Checked Then
            DataGridView.Filter = "KO_Type = 102"
        End If
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        DataGridView.Field("KO_Bezeichnung") = TextBox1.Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DataGridView.updateDataBase(wb_Sql.dbType.mySql)
    End Sub

End Class