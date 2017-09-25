Public Class wb_Admin_UpdateDatabase
    Private DBUpdateFiles As New List(Of String)

    Private Sub wb_Admin_UpdateDatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Version WinBack
        tbWinBackOffice.Text = wb_GlobalSettings.WinBackVersion
        'Version WinBack-Datenbank
        tbWinBackDatabase.Text = wb_GlobalSettings.WinBackDBVersion
        'Version OrgaBack
        tbOrgaBack.Text = wb_GlobalSettings.OrgaBackVersion
        'Version OrgaBack-Datenbank
        tbOrgaBackDataBase.Text = wb_GlobalSettings.OrgaBackDBVersion

        'Anzahl der verfügbaren WinBack-Datenbank-Updates
        For Each F As String In IO.Directory.GetFiles(wb_GlobalSettings.pAddInPath, "*.sql")
            DBUpdateFiles.Add(F)
        Next
        If DBUpdateFiles.Count > 0 Then
            BtnUpdateWinBackDataBase.Enabled = True
            lblUpdateFilesCount.Text = DBUpdateFiles.Count & " Update(s) gefunden"
            lblUpdateFilesCount.Visible = True
        Else
            BtnUpdateWinBackDataBase.Enabled = False
            lblUpdateFilesCount.Visible = False
        End If
    End Sub

    Private Sub BtnUpdateWinBackDataBase_Click(sender As Object, e As EventArgs) Handles BtnUpdateWinBackDataBase.Click
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)

        For Each Update As String In DBUpdateFiles
            Dim UpdateSql() As String = System.IO.File.ReadAllLines(Update)

            For Each sql As String In UpdateSql
                Debug.Print(sql)
                winback.sqlCommand(sql)
            Next
        Next
    End Sub
End Class