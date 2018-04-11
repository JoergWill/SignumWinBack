Imports WinBack

Public Class UnitTest_Init

    ''' <summary>
    ''' Setzt alle notwendigen Einstellungen in der WinBack.ini
    '''     - IP-Adresse WinBack-Datenbank      (127.0.0.1)
    ''' </summary>
    Public Shared Sub Init_WinBackIni()

        'Einstellungen WinBack-Datenbank
        Dim Inifile As New wb_IniFile
        Inifile.WriteString("winback", "eMYSQLServerIP", "127.0.0.1")
        Inifile = Nothing

        'Einstellungen OrgaBack-DatenBank
        wb_GlobalSettings.MsSQLAdmn = "UnitTest_vd3"
        wb_GlobalSettings.MsSQLMain = "UnitTest300"
        wb_GlobalSettings.MsSQLUserId = ""
        wb_GlobalSettings.MySQLPass = ""

    End Sub
End Class
