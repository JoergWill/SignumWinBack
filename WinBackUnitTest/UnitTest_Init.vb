Imports WinBack

Public Class UnitTest_Init

    ''' <summary>
    ''' Setzt alle notwendigen Einstellungen in der WinBack.ini
    '''     - IP-Adresse WinBack-Datenbank      (127.0.0.1)
    '''     - Daten/Programmpfade aus dbo.Settings.Verzeichnisse
    '''     
    ''' </summary>
    Public Shared Sub Init_WinBackIni()

        'Einstellungen WinBack-Datenbank läuft auf Localhost (127.0.0.1)
        Dim Inifile As New wb_IniFile
        Inifile.WriteString("winback", "eMYSQLServerIP", "127.0.0.1")
        Inifile = Nothing

        'Einstellungen OrgaBack-DatenBank
        'wb_GlobalSettings.MsSQLAdmn = "UnitTest_vd3"
        'wb_GlobalSettings.MsSQLMain = "UnitTest300"
        'wb_GlobalSettings.MsSQLUserId = ""
        'wb_GlobalSettings.MySQLPass = ""

        'Programm-Einstellung OrgaBack
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack

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


    End Sub
End Class
