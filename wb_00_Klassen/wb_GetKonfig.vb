Public Class wb_GetKonfig
    Public Shared Sub MySqlSetting()
        Dim IniFile As New Signum.OrgaSoft.AddIn.OrgasoftMain.wb_Konfig

        My.Settings.MySQLServerIP = IniFile.ReadString("winback", "eMySQLServerIP", "172.16.17.5")
        My.Settings.MySQLWinBack = IniFile.ReadString("winback", "eMySQLDatabase", "winback")
        My.Settings.MySQLWbDaten = IniFile.ReadString("winback", "eMySQLDatabaseDaten", "wbdaten")
        My.Settings.MySQLUser = IniFile.ReadString("winback", "eMySQLUser", "wbdaten")
        My.Settings.MySQLPass = IniFile.ReadString("winback", "eMySQLPasswordDatabase", "wbdaten")

        ' Connection-String für WinBack-MySQL-Datenbank
        My.Settings.MySQLConWinBack = "server=" & My.Settings.MySQLServerIP & ";" _
                                    & "user id=" & My.Settings.MySQLUser & ";" _
                                    & "password=" & My.Settings.MySQLPass & ";" _
                                    & "database=" & My.Settings.MySQLWinBack & ";"
        ' Connection-String für WbDaten-MySQL-Datenbank
        My.Settings.MySQLConWbDaten = "server=" & My.Settings.MySQLServerIP & ";" _
                                    & "user id=" & My.Settings.MySQLUser & ";" _
                                    & "password=" & My.Settings.MySQLPass & ";" _
                                    & "database=" & My.Settings.MySQLWbDaten & ";"
    End Sub
    Public Shared Function SqlIP() As String
        Return My.Settings.MySQLServerIP
    End Function
    Public Shared Function DbType() As String
        Return "MySQL"
    End Function
    Public Shared Sub SetColors()
        'Hintergrund-Farbe jeder zweiten Zeile des DataViewGrid (alternative RowSetting)
        My.Settings.DataGridAlternateRowColor = System.Drawing.Color.LightGray
    End Sub
End Class
