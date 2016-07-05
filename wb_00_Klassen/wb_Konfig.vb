Imports System.Drawing
Imports System.Windows.Forms

Public Class wb_Konfig

    Enum ProgVariante
        OrgaBack
        WinBack
    End Enum

    Shared Language As String
    Public Shared DockPanelPath As String
    Public Shared TexteTabelle As New Hashtable

    Public Shared Sub MySqlSetting()
        Dim IniFile As New Signum.OrgaSoft.AddIn.OrgasoftMain.wb_IniFile

        My.Settings.MySQLServerIP = IniFile.ReadString("winback", "eMySQLServerIP", "172.16.17.5")
        My.Settings.MySQLWinBack = IniFile.ReadString("winback", "eMySQLDatabase", "winback")
        My.Settings.MySQLWbDaten = IniFile.ReadString("winback", "eMySQLDatabaseDaten", "wbdaten")
        My.Settings.MySQLUser = IniFile.ReadString("winback", "eMySQLUser", "herbst")
        My.Settings.MySQLPass = IniFile.ReadString("winback", "eMySQLPasswordDatabase", "herbst")

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
    Public Shared Function SqlConWinBack() As String
        Return My.Settings.MySQLConWinBack
    End Function
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
    Public Shared Sub SetPath(pVariante As ProgVariante)
        'Default
        DockPanelPath = ""
        Select Case pVariante
            Case ProgVariante.OrgaBack
                DockPanelPath = My.Settings.OrgaSoftDockPanelPath
            Case ProgVariante.WinBack
                DockPanelPath = My.Settings.WinBackDockPanelPath
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub
    Public Shared Sub SetLanguage(Lang As String)
        Dim IniFile As New Signum.OrgaSoft.AddIn.OrgasoftMain.wb_IniFile
        If Lang = "" Then
            Language = IniFile.ReadString("winback", "Language", "de-DE")
        Else
            Language = Lang
            IniFile.WriteString("winback", "Language", Language)
        End If
        My.Settings.AktLanguage = Language
    End Sub
    Public Shared Function GetLanguage() As String
        Try
            Language = My.Settings.AktLanguage
        Catch
            Language = "de-DE"
        End Try
        Return Language
    End Function

    Public Shared Function GetLanguageNr() As Integer
        Select Case Left(Language, 2)
            Case "de"
                Return 0
            Case "hu"
                Return 1
            Case "nl"
                Return 2
            Case "en"
                Return 3
            Case "pt"
                Return 4
            Case "sl"
                Return 5
            Case "ru"
                Return 6
            Case "fr"
                Return 7
            Case "es"
                Return 8
            Case Else
                Return 0
        End Select

    End Function

    Public Shared Sub LoadTexteTabelle(Sprache As Integer)
        Dim winback As New wb_Sql(My.Settings.MySQLConWinBack, wb_Sql.dbType.mySql)
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlTexte, Sprache.ToString))
        TexteTabelle.Clear()
        While winback.Read
            TexteTabelle.Add("@[" & winback.sField("T_Typ") & "," & winback.sField("T_TextIndex") & "]", winback.sField("T_Text"))
        End While
        winback.Close()

    End Sub
End Class
