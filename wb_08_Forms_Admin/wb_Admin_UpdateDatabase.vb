Imports System.Windows.Forms

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
        DBUpdateFiles.Clear()
        If IO.Directory.Exists(wb_GlobalSettings.pDBUpdatePath) Then
            For Each F As String In IO.Directory.GetFiles(wb_GlobalSettings.pDBUpdatePath, "*.sql")
                DBUpdateFiles.Add(F)
            Next
        End If

        'Update-Button, Anzeige und Progress-Bar anzeigen
        ShowHideUpdate(DBUpdateFiles.Count)
    End Sub

    Private Sub BtnUpdateWinBackDataBase_Click(sender As Object, e As EventArgs) Handles BtnUpdateWinBackDataBase.Click
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)

        'Anzahl der UpdateFiles in Progress-Bar
        pbFiles.Maximum = DBUpdateFiles.Count
        pbFiles.Value = 0
        pbFiles.Step = 1

        Me.Cursor = Cursors.WaitCursor
        For Each Update As String In DBUpdateFiles
            pbFiles.PerformStep()
            Dim UpdateSql() As String = System.IO.File.ReadAllLines(Update)

            'Anzahl der UpdateDatensätze in ProgressBar
            pbData.Maximum = UpdateSql.Count
            pbData.Value = 0
            pbData.Step = 1

            For Each sql As String In UpdateSql
                pbData.PerformStep()
                If winback.sqlCommand(sql) < 0 Then
                    Me.Cursor = Cursors.Default
                    MsgBox("Datenbank Update fehlgeschlagen !", MsgBoxStyle.Critical, "Update WinBack-Datenbank")
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
            Next

            'Update durchgeführt- Datei wird umbenannt
            Try
                FileSystem.Rename(Update, Update & ".bak")
            Catch ex As Exception
            End Try
        Next

        'Aktion beendet
        MsgBox("Datenbank Update erfolgreich beendet", MsgBoxStyle.Information, "Update WinBack-Datenbank")
        ShowHideUpdate(0)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ShowHideUpdate(c As Integer)
        If c > 0 Then
            'Button und Anzeige einblenden
            BtnUpdateWinBackDataBase.Enabled = True
            lblUpdateFilesCount.Text = DBUpdateFiles.Count & " Update(s) gefunden"
            lblUpdateFilesCount.Visible = True
            pbData.Visible = True
            pbFiles.Visible = True
        Else
            'Button und Anzeige ausblenden
            BtnUpdateWinBackDataBase.Enabled = False
            lblUpdateFilesCount.Visible = False
            pbData.Visible = False
            pbFiles.Visible = False
        End If
    End Sub
End Class