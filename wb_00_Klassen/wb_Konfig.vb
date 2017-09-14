Imports WinBack.wb_Global

Public Class wb_Konfig

    Shared Language As String
    Public Shared TexteTabelle As New Hashtable

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

        My.Settings.MySQLServerIP = IniFile.ReadString("winback", "eMySQLServerIP", My.Settings.MySQLServerIP)
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
                Return wb_GlobalSettings.OrgaBackMainConString
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

    ''' <summary>
    ''' Zuordnung ISO-Sprache zu WinBack-Sprache-Nr
    ''' </summary>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Liest die zuletzt gespeicherte Fenster-Position aus der winback.ini und setzt die entsprechenden 
    ''' Parameter im übergebenen Fenster.
    ''' Der File-Name der letzten aktuellen Dock-Bar-Konfiguration wird im Tag-Objekt gespeichert !!
    ''' </summary>
    ''' <param name="xForm"></param>
    ''' <param name="IniSektion"></param>
    Public Shared Sub SetFormBoundaries(xForm As Windows.Forms.Form, IniSektion As String)
        Dim IniFile As New WinBack.wb_IniFile

        'Fensterposition aus winback.ini
        xForm.Top = IniFile.ReadInt(IniSektion, "Top")
        xForm.Left = IniFile.ReadInt(IniSektion, "Left")
        xForm.Width = IniFile.ReadInt(IniSektion, "Width")
        xForm.Height = IniFile.ReadInt(IniSektion, "Height")

        'Dispose
        IniFile = Nothing
    End Sub

    Public Shared Sub SaveFormBoundaries(Top As Integer, Left As Integer, Width As Integer, Height As Integer, LayoutFile As String, IniSektion As String)
        Dim IniFile As New WinBack.wb_IniFile

        'Fensterposition in winback.ini sichern
        IniFile.WriteInt(IniSektion, "Top", Top)
        IniFile.WriteInt(IniSektion, "Left", Left)
        IniFile.WriteInt(IniSektion, "Width", Width)
        IniFile.WriteInt(IniSektion, "Height", Height)

        'Layout-File
        IniFile.WriteString(IniSektion, "LayoutFileName", LayoutFile)

        'Dispose
        IniFile = Nothing
    End Sub
End Class
