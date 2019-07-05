Imports Microsoft.Win32

Public Class CustomActions
    'Registry-Eintrag OrgaSoft
    Const ORGASOFTREGISTRY = "SOFTWARE\WOW6432Node\Signum GmbH Darmstadt\OrgaSoft.NET"
    Const ORGASOFTINI = "OrgaSoftINI"
    Const ORGASOFTNICHTINSTALLIERT = "NOINSTALL"

    Private Shared xLogger As New wb_TraceListener

    Private Shared Sub AddLogger()
        'Programm-Variante festlegen
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.Setup

        'alle Trace / Debug - Ausgaben werden auch in der Klasse wb_Admin_Shared in einer Text-Liste gespeichert.
        ' Nach x Zeilen werden die Einträge in ein Text-File gespeichert.
        ' Die Klasse xLogger (wb_Trace_Listener) leitet die Meldungen weiter.
        AddHandler xLogger.WriteText, AddressOf wb_Admin_Shared.GetTraceListenerText
        Trace.Listeners.Add(xLogger)
        'Meldung Programm-Start (initialisiert wb_Admin_Shared)
        Trace.WriteLine("Programmstart Setup")
    End Sub


    ''' <summary>
    ''' Ermittelt den Pfad und Datei-Namen zur OrgaSoft.ini
    ''' Die Installation des WinBackAddIn erfolgt im Verzeichnis AddIn
    ''' </summary>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <CustomAction()>
    Public Shared Function VBCustomAction_InstallPath(ByVal session As Session) As ActionResult
        AddLogger()
        'Installations-Pfad (OrgaSoft.INI)
        Try
            'Auslesen der Registry
            'Computer\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Signum GmbH Darmstadt\OrgaSoft.NET
            Dim OrgaSoftIni As String = GetOrgaBackIni(ORGASOFTNICHTINSTALLIERT)
            If OrgaSoftIni = ORGASOFTNICHTINSTALLIERT Then
                MsgBox("Keine Installation von OrgaSoft.NET gefunden")
                Return ActionResult.Failure
            Else
                'Die Installation erfolgt unterhalb des OrgaBack-Pfades im Verzeichnis AddIn
                Dim OrgaSoftPfad As String = IO.Path.GetDirectoryName(OrgaSoftIni)
                'ToDo REMOVE AFTER TEST
                OrgaSoftPfad = "C:\Temp\OrgaSoft"
                'Installationspfad ist unterhalb des OrgaBack-Verzeichnis
                If session IsNot Nothing Then
                    session.SetTargetPath("TARGETDIR", OrgaSoftPfad)
                End If
            End If
        Catch ex As Exception
            MsgBox("Error " & ex.Message)
            Return ActionResult.Failure
        End Try

        Return ActionResult.Success
    End Function

    <CustomAction()>
    Public Shared Function VBCustomAction_InstallMySQL(ByVal session As Session) As ActionResult
        MsgBox("Installation MySQL --- ")
        Return ActionResult.Success
    End Function

    <CustomAction()>
    Public Shared Function VBCustomAction_InstallLog4View(ByVal session As Session) As ActionResult
        MsgBox("Installation Log4View --- ")
        Return ActionResult.Success
    End Function


    ''' <summary>
    ''' Der Pfad zur Datei OrgaSoft.ini steht in der Registry unter
    '''     Computer\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Signum GmbH Darmstadt\OrgaSoft.NET
    '''     
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetOrgaBackIni(pDefault As String) As String
        Try
            Using Key As RegistryKey = Registry.LocalMachine.OpenSubKey(ORGASOFTREGISTRY, RegistryKeyPermissionCheck.ReadSubTree)
                GetOrgaBackIni = CStr(Key.GetValue(ORGASOFTINI, pDefault))
            End Using
        Catch ex As Exception
            Return pDefault
        End Try
    End Function

End Class


