Imports System.Drawing
Imports System.Windows.Forms

Public Class wb_Konfig
    Shared Language As String

    Public Shared Sub MySqlSetting()
        Dim IniFile As New Signum.OrgaSoft.AddIn.OrgasoftMain.wb_IniFile

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
    Public Shared Sub SetLanguage(Lang As String)
        Language = Lang
        My.Settings.AktLanguage = Lang
    End Sub
    Public Shared Function GetLanguage() As String
        Try
            Language = My.Settings.AktLanguage
        Catch
            Language = "de"
        End Try
        Return Language
    End Function
End Class
