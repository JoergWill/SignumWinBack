Imports System.Net.Sockets
Imports Microsoft.Win32

Public Class CustomActions
    'Registry-Eintrag OrgaSoft
    Const ORGASOFTREGISTRY = "SOFTWARE\WOW6432Node\Signum GmbH Darmstadt\OrgaSoft.NET"
    Const ORGASOFTINI = "OrgaSoftINI"
    Const ORGASOFTNICHTINSTALLIERT = "NOINSTALL"

    ''' <summary>
    ''' Ermittelt den Pfad und Datei-Namen zur OrgaSoft.ini
    ''' Die Installation des WinBackAddIn erfolgt im Verzeichnis AddIN
    ''' </summary>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <CustomAction()>
    Public Shared Function VBCustomAction(ByVal session As Session) As ActionResult

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
                session.SetTargetPath("TARGETDIR", OrgaSoftPfad)
            End If
        Catch ex As Exception
            MsgBox("Error " & ex.Message)
            Return ActionResult.Failure
        End Try

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


