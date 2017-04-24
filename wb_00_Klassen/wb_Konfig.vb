Imports WinBack.wb_Global

Public Class wb_Konfig

    Shared Language As String
    Public Shared DockPanelPath As String
    Public Shared TexteTabelle As New Hashtable
    Public Shared ktTyp301Params As New Hashtable

    Public Shared Filiale As New wb_Filiale

    Public Shared Sub SqlSetting(Optional ByVal DBType As String = "")
        Dim IniFile As New wb_IniFile
        'für UnitTest kann die Einstellung in winback.ini überschrieben werden
        If DBType Is "" Then
            DBType = IniFile.ReadString("winback", "DBType", "MySQL")
        End If

        Select Case DBType.ToLower
            'winback läuft unter MySQL 
            Case "mysql"
                My.Settings.WinBackDBType = wb_Sql.dbType.mySql
                MySqlSetting()
                My.Settings.WinBackConString = My.Settings.MySQLConWinBack
                My.Settings.WbDatenConString = My.Settings.MySQLConWbDaten

            'winback läuft unter MicroSoft SQL
            Case "mssql"
                My.Settings.WinBackDBType = wb_Sql.dbType.msSql
                MsSqlSetting()
                My.Settings.WinBackConString = My.Settings.MsSQLConWinBack
                My.Settings.WbDatenConString = My.Settings.MsSQLConWbDaten

                'Datenbank nicht definiert
            Case Else
                My.Settings.WinBackDBType = wb_Sql.dbType.undef
                Throw New NotImplementedException
        End Select

    End Sub
    Private Shared Sub MySqlSetting()
        Dim IniFile As New wb_IniFile

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

    Private Shared Sub MsSqlSetting()
        Dim IniFile As New wb_IniFile

        'Default-Wert für die IP-Adresse is der Rechner-Name !! 
        'sonst funktioniert der Zugriff auf die MS-SQL2014-Datenbank nicht
        My.Settings.MsSQLServerIP = IniFile.ReadString("winback", "eMsSQLServerIP", Environment.MachineName)
        My.Settings.MySQLWinBack = IniFile.ReadString("winback", "eMySQLDatabase", "winback")
        My.Settings.MySQLWbDaten = IniFile.ReadString("winback", "eMySQLDatabaseDaten", "wbdaten")

        ' Connection-String für WinBack-MsSQL-Datenbank
        My.Settings.MsSQLConWinBack = "Data Source=" & My.Settings.MsSQLServerIP & "\SIGNUM; " _
                                    & "Database=" & My.Settings.MySQLWinBack & "; " _
                                    & "Integrated Security=True"
        ' Connection-String für WbDaten-MsSQL-Datenbank
        My.Settings.MsSQLConWbDaten = "Data Source=" & My.Settings.MsSQLServerIP & "\SIGNUM; " _
                                    & "Database=" & My.Settings.MySQLWbDaten & "; " _
                                    & "Integrated Security=True"

        ' Connection-String für OrgaBackMain-MsSQL-Datenbank
        My.Settings.OrgaBackMainConString = "Data Source=" & My.Settings.MsSQLServerIP & "\SIGNUM; " _
                                    & "Database=" & My.Settings.MsSQLMain & "; " _
                                    & "Integrated Security=True"
    End Sub

    Public Shared Function SqlConWinBack() As String
        Select Case My.Settings.WinBackDBType
            Case wb_Sql.dbType.mySql
                Return My.Settings.MySQLConWinBack
            Case wb_Sql.dbType.msSql
                Return My.Settings.MsSQLConWinBack
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Shared Function SqlConWbDaten() As String
        Select Case My.Settings.WinBackDBType
            Case wb_Sql.dbType.mySql
                Return My.Settings.MySQLConWbDaten
            Case wb_Sql.dbType.msSql
                Return My.Settings.MsSQLConWbDaten
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Shared Function SqlConOrgaBack() As String
        Select Case My.Settings.WinBackDBType
            Case wb_Sql.dbType.mySql
                Throw New NotImplementedException
            Case wb_Sql.dbType.msSql
                Return My.Settings.OrgaBackMainConString
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Shared Function SqlIP() As String
        Select Case My.Settings.WinBackDBType
            Case wb_Sql.dbType.mySql
                Return My.Settings.MySQLServerIP
            Case wb_Sql.dbType.msSql
                Return My.Settings.MsSQLServerIP
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Shared Function DbType() As String
        Select Case My.Settings.WinBackDBType
            Case wb_Sql.dbType.mySql
                Return "MySQL"
            Case wb_Sql.dbType.msSql
                Return "MSsql"
            Case Else
                Throw New NotImplementedException
        End Select
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
        Dim IniFile As New wb_IniFile
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

    'Zuordnung ISO-Sprache zu WinBack-Sprache-Nr
    Public Shared Function GetLanguageNr() As String
        Select Case Left(Language, 2)
            Case "de"       'Deutsch
                Return "0"
            Case "hu"       'Ungarisch
                Return "1"
            Case "nl"       'Niederländisch
                Return "2"
            Case "en"       'Englisch(US)
                Return "3"
            Case "pt"       'Portugisisch
                Return "4"
            Case "sl"       'Slovenisch
                Return "5"
            Case "ru"       'Russisch
                Return "6"
            Case "fr"       'Französisch
                Return "7"
            Case "es"       'Spanisch
                Return "8"
            Case "sk"       'Slovakisch
                Return "9"
            Case "ro"       'Rumänisch
                Return "10"
            Case Else
                Return 0
        End Select
    End Function

    Public Shared Sub LoadTexteTabelle(Sprache As String)
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlWinBackTxte, Sprache))
        TexteTabelle.Clear()
        While winback.Read
            TexteTabelle.Add("@[" & winback.sField("T_Typ") & "," & winback.sField("T_TextIndex") & "]", winback.sField("T_Text"))
        End While
        winback.Close()
    End Sub

    Public Shared Sub LoadKompon301Tabelle()
        Dim k As ktTyp301Param
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.sqlKompTyp301)
        ktTyp301Params.Clear()
        While winback.Read

            k.ParamNr = winback.iField("KT_ParamNr")
            k.Bezeichnung = winback.sField("KT_Bezeichnung")
            k.KurzBezeichnung = winback.sField("KT_KurzBez")
            k.Gruppe = wb_Functions.kt301GruppeToString(winback.sField("KT_Wert"))
            k.Einheit = winback.sField("E_Einheit")
            k.Feld = winback.sField("KT_Kommentar")
            k.Used = (winback.sField("KT_Rezept") = "X")

            ktTyp301Params.Add(k.ParamNr, k)
        End While
        winback.Close()

    End Sub
End Class
