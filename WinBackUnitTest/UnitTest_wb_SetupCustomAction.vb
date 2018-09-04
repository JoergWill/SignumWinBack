Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Microsoft.Win32
Imports WinBack

<TestClass()> Public Class UnitTest_wb_SetupCustomAction

    ''' <summary>
    ''' Test Zugriff auf die Registry - Ermittlung Pfad un Dateiname OrgaBack.ini
    ''' Setup-Helper-Routine
    ''' </summary>
    <TestMethod()> Public Sub ca_GetOrgaBackIniPfad()

        Const ORGASOFTREGISTRY = "SOFTWARE\WOW6432Node\Signum GmbH Darmstadt\OrgaSoft.NET"
        Const ORGASOFTINI = "OrgaSoftINI"
        'Test Zugriff auf Registry Key
        Using Key As RegistryKey = Registry.LocalMachine.OpenSubKey(ORGASOFTREGISTRY, RegistryKeyPermissionCheck.ReadSubTree)
            Dim s As String = CStr(Key.GetValue(ORGASOFTINI, "TEST"))
            Assert.AreEqual("C:\ProgramData\OrgaSoft\OrgaSoft.INI", s)
        End Using
        'Test Routine in CustomActions
        'TODO Hier stimmt was nicht
        'Dim obIniPath = CustomAction.GetOrgaBackIni("Test")
        'Assert.AreEqual("C:\ProgramData\OrgaSoft\OrgaSoft.INI", obIniPath)
    End Sub

    ''' <summary>
    ''' Ermittelt die IP-Adressen aller LAN-Teilnehmer im Netzwerk
    ''' Versucht die WinBack-IP-Adresse zu ermitteln
    ''' </summary>
    <TestMethod()> Public Sub ca_GetWinBackIP()

        'Test Routine in CustomActions
        'TODO Hier stimmt was nicht
        'Dim WinBackIP = winbackSetup_CA.CustomAction.getWinBackIP()
        'Assert.AreEqual("172.16.17.3", WinBackIP)
    End Sub



End Class