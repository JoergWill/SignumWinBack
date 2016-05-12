Imports Signum.OrgaSoft.AddIn.wb_Functions

Public Class wb_User_Liste

    Private Sub wb_User_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Data_Load()
    End Sub

    Private Sub Data_Load()
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        Dim sColNames As New List(Of String) From {"Nummer", "Type", "Bezeichnung", "&Kommentar"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        DataGridView.LoadData()
        DataGridView.loadFromDisk("UserListe")
        DataGridView.Columns(0).Visible = False

    End Sub

    Private Sub wb_User_Liste_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DataGridView.SaveToDisk("UserListe")
    End Sub

    Private Sub DataGridView_SizeChanged(sender As Object, e As EventArgs) Handles DataGridView.SizeChanged
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As Windows.Forms.ToolStripItemClickedEventArgs) 

    End Sub
End Class