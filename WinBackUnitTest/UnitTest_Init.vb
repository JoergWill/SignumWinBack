Imports WinBack

Public Class UnitTest_Init

    '''<summary>
    ''' Wird einmalig beim Start der Unit-Tests aufgerufen
    '''     Läd die WinBack-Test-Datenbank auf die lokale MySQL-Installation
    '''     Erzeugt eine UnitTest-Datenbank auf dem lokalen SQL-Server
    '''     Die MSSql-Datenbank-Tabellen liegen als bz2-File im Verzeichnis \dbu
    '''         C:\Dokumente\Projekte\Signum_WinBack\WinBackUnitTest\dbu\O.30_UnitTest_DbMain.bz2
    '''         C:\Dokumente\Projekte\Signum_WinBack\WinBackUnitTest\dbu\O.30_UnitTest_DbAdmin.bz2
    '''     
    ''' Die Test-Datenbank kann (nach Änderungen) über mysqldump wieder gesichert werden:
    '''     cd C:\Dokumente\Projekte\Signum_WinBack\WinBackUnitTest\dbu
    '''     mysqldump -uherbst -pherbst winback > WinBack-UnitTest-Daten.sql
    ''' 
    ''' Einstellungen WinBack-Datenbank läuft auf Localhost (127.0.0.1)
    '''     MySQLServerIP = "127.0.0.1"
    '''     MSQLUser = "herbst"
    '''     MySQLPass = "herbst"
    '''     WinBackDBType = wb_Sql.dbType.mySql
    '''     MySQLWinBack = "winback"
    '''     MySQLWbDaten = "wbdaten"
    '''
    ''' Einstellungen OrgaBack-Datenbank:
    '''     läuft auf WILL-PC (127.0.0.1)
    '''     MsSQLServer = Environment.MachineName
    '''     MsSQLUserId = "sa"
    '''     MsSQLPasswd = "OrgaBack.NET"
    '''     MsSQLMain = "UnitTest_Main"
    '''     MsSQLAdmn = "UniTest_Admin"
    '''
    ''' 
    ''' </summary>
    Shared Sub New()
        'Test-Meldung
        Debug.Print("Start Unit-Test im Debug-Modus")
        'Initialisierung der Einstellungen der WinBack-Ini (hier Mandant 5 - Niehaves ! zum Prüfen der Unit-Test-Datenbank)
        Init_WinBackIni_Settings(5)

        'Prüfen ob eine SQL-Datenbank UnitTest_* existiert.
        'Wenn nicht wird die Datenbank erzeugt
        Dim OrgaSoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)

        If OrgaSoftMain.sqlSelect("SELECT * FROM sys.databases WHERE [name] LIKE 'UnitTest_Main'") AndAlso OrgaSoftMain.Read Then
            Dim Id As Integer = OrgaSoftMain.iField("database_id").ToString
            Debug.Print("Datenbank UnitTest_Main ist vorhanden")
            'Settings auf Mandant 3 - UnitTest
            Init_WinBackIni_Settings()
        Else
            'Datenbank UnitTest_Main ist nicht vorhanden
            Debug.Print("Datenbank UnitTest_Main ist nicht vorhanden - Erzeugen")
            OrgaSoftMain.CloseRead()

            'Backup/Restore
            Dim mssql As New wb_sql_BackupRestore
            'SQL-Datenbank UnitTest_Main aus Backup laden (dauert länger)
            mssql.MSSQL_DatenRuecksicherung(wb_GlobalSettings.pDBUpdatePath & "O.30_UnitTest_DbMain.bz2", "UnitTest_Main")
            'SQL-Datenbank UnitTest_Admin aus Backup laden (dauert länger)
            mssql.MSSQL_DatenRuecksicherung(wb_GlobalSettings.pDBUpdatePath & "O.30_UnitTest_DbAdmin.bz2", "UnitTest_Admin")

            'Datenbank-Verbindung wieder schliessen
            OrgaSoftMain.Close()

            'Settings auf Mandant 3 - UnitTest
            Init_WinBackIni_Settings()

            'Backup der Test-Datenbank einspielen
            Restore_WinBackDaten()
        End If
    End Sub

    ''' <summary>
    ''' Läd das Backup aus 2.30_UnitTest_WinBackDaten.sql in die lokale WinBack-Test-DB
    ''' Damit sind die Testdaten für die folgenden Tests immer identisch und reproduzierbar
    ''' </summary>
    Public Shared Sub Restore_WinBackDaten()
        Dim mysql As New Global.WinBack.wb_sql_BackupRestore
        Dim pgBar As New System.Windows.Forms.ProgressBar

        If mysql.DatenRuecksicherung(wb_GlobalSettings.pDBUpdatePath & "2.30_UnitTest_WinBackDaten.sql", False, pgBar, False) Then
            Trace.WriteLine("Datenbank aus WinBack-UnitTest-Daten gelesen")
            Assert.IsTrue(True)
        Else
            Trace.WriteLine("Fehler bei Restore Datenbank aus WinBack-UnitTest-Daten")
            Assert.IsTrue(False)
        End If
    End Sub

    ''' <summary>
    ''' Setzt alle notwendigen Einstellungen in der WinBack.ini
    '''     - IP-Adresse WinBack-Datenbank      (127.0.0.1)
    '''     
    ''' </summary>
    Public Shared Sub Init_WinBackIni_Settings(Optional Mandant As Integer = 3)

        'Prog-Variante Unit-Test
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest

        'Einstellungen WinBack-Datenbank läuft auf Localhost (127.0.0.1)
        wb_GlobalSettings.MySQLServerIP = "127.0.0.1"
        wb_GlobalSettings.MySQLUser = "herbst"
        wb_GlobalSettings.MySQLPass = "herbst"
        wb_GlobalSettings.WinBackDBType = wb_Sql.dbType.mySql
        wb_GlobalSettings.MySQLWinBack = "winback"
        wb_GlobalSettings.MySQLWbDaten = "wbdaten"

        'Einstellungen OrgaBack-Datenbank läuft auf WILL-PC (127.0.0.1)
        wb_GlobalSettings.MsSQLServer = Environment.MachineName
        wb_GlobalSettings.MsSQLUserId = "sa"
        wb_GlobalSettings.MsSQLPasswd = "OrgaBack.NET"

        'Datenbank abhängig vom Mandanten (aus C:\OrgaSoft\OrgaSoft.ini)
        Select Case Mandant
            Case 1
                wb_GlobalSettings.MsSQLMain = "Baeckerei_Main"
                wb_GlobalSettings.MsSQLAdmn = "Baeckerei_Admin"
            Case 2
                wb_GlobalSettings.MsSQLMain = "OrgabackDemo_Main"
                wb_GlobalSettings.MsSQLAdmn = "OrgabackDemo_Admin"
            Case 3
                wb_GlobalSettings.MsSQLMain = "UnitTest_Main"
                wb_GlobalSettings.MsSQLAdmn = "UnitTest_Admin"
            Case 4
                wb_GlobalSettings.MsSQLMain = "Märkisches_Main"
                wb_GlobalSettings.MsSQLAdmn = "Märkisches_Admin"
            Case 5
                wb_GlobalSettings.MsSQLMain = "Niehaves_Main"
                wb_GlobalSettings.MsSQLAdmn = "Niehaves_Admin"
            Case 6
                wb_GlobalSettings.MsSQLMain = "Schaufler_Main"
                wb_GlobalSettings.MsSQLAdmn = "Schaufler_Admin"
            Case 7
                wb_GlobalSettings.MsSQLMain = "Goeken_Main"
                wb_GlobalSettings.MsSQLAdmn = "Goeken_Admin"
            Case 8
                wb_GlobalSettings.MsSQLMain = "Fonk_Main"
                wb_GlobalSettings.MsSQLAdmn = "Fonk_vd3"
            Case Else
                wb_GlobalSettings.MsSQLMain = "UnitTest_Main"
                wb_GlobalSettings.MsSQLAdmn = "UnitTest_Admin"
        End Select


        'LogFile-AutoStart Einschalten
        wb_GlobalSettings.Log4netAutoStart = True

    End Sub

    ''' <summary>
    ''' Setzt alle notwendigen Einstellungen in der WinBack.ini
    '''     - Daten/Programmpfade aus dbo.Settings.Verzeichnisse
    '''     
    ''' </summary>
    Public Shared Sub Init_WinBackIni(Optional Mandant As Integer = 3)

        'DB-Verbindung für diesen Test nicht notwendig
        If Mandant > 0 Then
            'Mandant setzen
            wb_GlobalSettings.MandantNr = Mandant

            'alle weiteren Einstellungen werden aus dbo.Settings gelesen
            Dim OrgasoftAdmin As New wb_Sql(wb_GlobalSettings.OrgaBackAdminConString, wb_Sql.dbType.msSql)

            'Daten aus Tabelle Settings lesen
            If OrgasoftAdmin.sqlSelect("SELECT Content,Entry FROM dbo.settings WHERE Category='Verzeichnisse'") Then
                While OrgasoftAdmin.Read
                    Select Case OrgasoftAdmin.sField("Entry")
                        Case "AddInPfad"
                            wb_GlobalSettings.pAddInPath = OrgasoftAdmin.sField("Content") & "\"
                        Case "ListenPfad"
                            wb_GlobalSettings.pListenPath = OrgasoftAdmin.sField("Content") & "\"
                        Case "ProgrammPfad"
                            wb_GlobalSettings.pProgrammPath = OrgasoftAdmin.sField("Content") & "\"
                        Case "DatenPfad"
                            wb_GlobalSettings.pDatenPath = OrgasoftAdmin.sField("Content") & "\"

                    End Select
                End While
            End If
            OrgasoftAdmin.Close()
        End If

    End Sub
End Class
