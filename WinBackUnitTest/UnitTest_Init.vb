Imports WinBack

Public Class UnitTest_Init

    ''' <summary>
    ''' Setzt alle notwendigen Einstellungen in der WinBack.ini
    '''     - IP-Adresse WinBack-Datenbank      (127.0.0.1)
    '''     - Daten/Programmpfade aus dbo.Settings.Verzeichnisse
    '''     
    ''' </summary>
    Public Shared Sub Init_WinBackIni(Optional Mandant As Integer = 3)

        'Einstellungen WinBack-Datenbank läuft auf Localhost (127.0.0.1)
        Dim Inifile As New wb_IniFile
        Inifile.WriteString("winback", "eMYSQLServerIP", "127.0.0.1")
        Inifile = Nothing

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


    End Sub
End Class
