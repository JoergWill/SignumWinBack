Imports System.Windows.Forms

Public Class wb_Admin_UpdateDatabase
    Private DBUpdateFiles As New List(Of String)

    Private Sub wb_Admin_UpdateDatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Version WinBack
        tbWinBackOffice.Text = wb_GlobalSettings.WinBackVersion
        'Version WinBack-Datenbank
        tbWinBackDatabase.Text = wb_GlobalSettings.WinBackDBVersion
        'Kunde-Name WinBack
        tbWinbackKundeName.Text = wb_GlobalSettings.MandantName
        'winback-Datenbank
        tbWinBackDB.Text = wb_GlobalSettings.MySQLWinBack
        'wbdaten-Datenbank
        tbWBDatenDB.Text = wb_GlobalSettings.MySQLWbDaten
        'IP-Adresse WinBack-Server
        tbWinBackIP.Text = wb_GlobalSettings.MySQLServerIP
        'Mandant nummer
        tbMandant.Text = wb_GlobalSettings.MandantNr & "/" & wb_GlobalSettings.MandantName

        'Programmversion WinBack-Office hat keine OrgaBack-Anbindung
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            'Version OrgaBack
            tbOrgaBack.Text = wb_GlobalSettings.OrgaBackVersion
            'Version OrgaBack-Datenbank
            tbOrgaBackDataBase.Text = wb_GlobalSettings.OrgaBackDBVersion
            'OrgaBack-Main-DB
            tbMsSQLMain.Text = wb_GlobalSettings.MsSQLMain

            'OrgaBack Datenbank-Server
            tbOrgaBackDBServer.Text = wb_GlobalSettings.MsSQLServer
            'OrgaBack Admin-Datenbank
            tbOrgaBackAdminDB.Text = wb_GlobalSettings.MsSQLAdmn
            'OrgaBack Main-Datenbank
            tbOrgaBackMainDB.Text = wb_GlobalSettings.MsSQLMain
        Else
            PnlOrgaBack.Visible = False
        End If

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
        Dim Result As MsgBoxResult = MsgBoxResult.No
        Dim UpdProcErr As Boolean = False
        Dim UpdFileErr As Boolean = False
        'Updates für WinBack Datenbank
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        'Für OrgaBack/UnitTests werden evtl. auch OrgaBack-Updates bereitgestellt
        Dim orgaback As wb_Sql = Nothing

        'Anzahl der UpdateFiles in Progress-Bar
        pbFiles.Maximum = DBUpdateFiles.Count
        pbFiles.Value = 0
        pbFiles.Step = 1

        Me.Cursor = Cursors.WaitCursor
        For Each Update As String In DBUpdateFiles
            pbFiles.PerformStep()

            'UpdateFiles für OrgaBack beginnen mit O.30
            If (wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Or wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest) And Update.Contains("O.30") Then
                'Update durchführen (Alle Zeilen des sql-Files)
                UpdFileErr = UpdateMsSqlFile(orgaback, Update)
            Else
                'Update durchführen (Alle Zeilen des sql-Files)
                UpdFileErr = UpdateSqlFile(winback, Update)
            End If

            'Fehlermeldung ausgeben(Je Update-File)
            If UpdFileErr Then
                Me.Cursor = Cursors.Default
                MsgBox("Datenbank Update fehlgeschlagen ! " & vbCrLf & "Update : " & Update, MsgBoxStyle.Critical, "Update WinBack-Datenbank")
                UpdProcErr = True
                Me.Cursor = Cursors.WaitCursor
            End If

            'Update durchgeführt- Datei wird umbenannt
            Try
                If My.Computer.FileSystem.FileExists(Update & ".bak") Then
                    FileSystem.Kill(Update & ".bak")
                End If
                FileSystem.Rename(Update, Update & ".bak")
            Catch ex As Exception
            End Try
        Next

        'Aktion beendet
        If Not UpdProcErr Then
            Result = MsgBox("Datenbank Update erfolgreich beendet" & vbCrLf & "OrgaBack neu starten ?", MsgBoxStyle.YesNo, "Update WinBack-Datenbank")
        Else
            MsgBox("Datenbank Update mit Fehlern beendet", MsgBoxStyle.Exclamation, "Update WinBack-Datenbank")
        End If

        'Button Update-Start ausblenden
        ShowHideUpdate(0)
        winback.Close()
        Me.Cursor = Cursors.Default
        'Fenster(Dialog) schliessen
        Me.Close()
        'Neustart wenn erforderlich
        If Result = MsgBoxResult.Yes Then
            wb_Functions.Restart()
        End If
    End Sub

    Public Function UpdateSqlFile(ByRef winback As wb_Sql, Update As String) As Boolean
        Dim UpdateSql() As String = System.IO.File.ReadAllLines(Update)

        'Anzahl der UpdateDatensätze in ProgressBar
        pbData.Maximum = UpdateSql.Length
        pbData.Value = 0
        pbData.Step = 1

        'Fehler beim Update
        Dim UpdFileErr As Boolean = False
        'alle Update-Zeilen nacheinander ausführen
        For Each sql As String In UpdateSql
            pbData.PerformStep()
            If winback.sqlCommand(UpdateSqlMandant(sql)) < 0 Then
                UpdFileErr = True
            End If
        Next
        Return UpdFileErr
    End Function

    ''' <summary>
    ''' Tausch im sql-Kommando die Datenbank-Bezeichnungen
    '''     "use winback" 
    '''     "use wbdaten"
    ''' gegen die entsprechende Datenbank aus
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    <CodeAnalysis.SuppressMessage("Performance", "CA1862:""StringComparison""-Methodenüberladungen verwenden, um Zeichenfolgenvergleiche ohne Beachtung der Groß-/Kleinschreibung durchzuführen", Justification:="<Ausstehend>")>
    Private Function UpdateSqlMandant(sql As String) As String
        If sql.Length > 3 Then
            If sql.Substring(0, 3).ToUpper = "USE" Then
                sql = sql.Replace("winback", wb_GlobalSettings.MySQLWinBack)
                sql = sql.Replace("wbdaten", wb_GlobalSettings.MySQLWbDaten)
            End If
        End If
        Return sql
    End Function

    Public Function UpdateMsSqlFile(ByRef orgaback As wb_Sql, Update As String) As Boolean
        If orgaback Is Nothing Then
            orgaback = New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        End If
        'alle Datensätze aus dem Update-File
        Dim UpdateSql() As String = System.IO.File.ReadAllLines(Update)

        'Anzahl der UpdateDatensätze in ProgressBar
        pbData.Maximum = UpdateSql.Length
        pbData.Value = 0
        pbData.Step = 1

        'Fehler beim Update
        Dim UpdFileErr As Boolean = False
        'alle Update-Zeilen nacheinander ausführen
        For Each sql As String In UpdateSql
            pbData.PerformStep()
            If orgaback.sqlCommand(sql) < 0 Then
                UpdFileErr = True
            End If
        Next
        Return UpdFileErr
    End Function

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