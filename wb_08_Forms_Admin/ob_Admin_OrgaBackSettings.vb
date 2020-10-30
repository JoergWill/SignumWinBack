Option Strict On
Option Explicit On

Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.Common
Imports System.Reflection

<Export(GetType(IExtension))>
<ExportMetadata("Description", "WinBack Festwerte")>
Public Class ob_Admin_OrgaBackSettings
    Implements IExtension
    Implements ISettingProvider

    Private oFactory As IFactoryService
    Private oSetting As ISettingService

#Region "IExtension"
    Private Property IExtension_ServiceProvider As IOrgasoftServiceProvider Implements IExtension.ServiceProvider
    Private Property IExtension_InfoContainer As IInfoContainer Implements IExtension.InfoContainer

    ''' <summary>
    '''Einstellungen für Admin-DB-Name, Main-DB-Name und Pfade werden beim Programm-Start in wb_Main_Menu
    '''eingelesen
    ''' </summary>
    Public Sub Initialize() Implements IExtension.Initialize
        'siehe Mail vom 13.Juli 2017 J.Erhardt - laden der dll schläg fehl 
        AddHandler System.AppDomain.CurrentDomain.AssemblyResolve, AddressOf MyAssemblyResolve
    End Sub

    Private Function MyAssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
        Return wb_Main_Shared.MyAssemblyResolve(sender, args)
    End Function
#End Region

#Region "ISettingProvider"
    ''' <summary>
    ''' Einstellungen für das WinBack-AddIn in OrgaBack Festwerten
    ''' </summary>
    ''' <returns></returns>
    Public Function Settings() As IList(Of ISetting) Implements ISettingProvider.Settings
        Dim oSettings As New List(Of ISetting)
        Dim oSetting As ISetting = Nothing
        ' Das AddIn muss nicht geladen/initialisiert worden sein, kann aber dennoch nach seinen Settings gefragt werden
        If (oFactory Is Nothing) AndAlso Not (IExtension_ServiceProvider Is Nothing) Then
            oFactory = TryCast(IExtension_ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)
        End If

        If Not (oFactory Is Nothing) Then

            'Setting WinBack.MySQLServerIP
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "WinBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "WinBack MySQL-Server IP-Adresse"
            oSetting.DisplayEntry = "MySQLServerIP"
            'oSetting.Entry = "127.0.0.1"
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "s"
            oSettings.Add(oSetting)

            'Setting WinBack.MySQL Database Name winback
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "WinBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "WinBack MySQL-Daten"
            oSetting.DisplayEntry = "MySQLDatabase"
            'oSetting.Entry = "winback"
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "s"
            oSettings.Add(oSetting)

            'Setting WinBack.MySQL Database Name wbdaten
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "WinBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "WinBack MySQL-Archiv"
            oSetting.DisplayEntry = "MySQLDatabaseDaten"
            'oSetting.Entry = "wbdaten"
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "s"
            oSettings.Add(oSetting)

            'Setting WinBack.GruppeBackwaren
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "OrgaBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "OrgaBack Artikelgruppe Backwaren"
            oSetting.DisplayEntry = "GruppeBackwaren"
            'oSetting.Entry = "10"
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "n"
            oSettings.Add(oSetting)

            'Setting WinBack.GruppeRohstoffe
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "OrgaBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "OrgaBack Artikelgruppe Rohstoffe"
            oSetting.DisplayEntry = "GruppeRohstoffe"
            'oSetting.Entry = "20"
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "n"
            oSettings.Add(oSetting)

            'Setting OrgaBack.MsSQL_UserID
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "OrgaBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "MsSQL USer ID"
            oSetting.DisplayEntry = "MSSQL_UserID"
            'oSetting.Entry = "sa"
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "s"
            oSettings.Add(oSetting)

            'Setting OrgaBack.MsSQL_UserPass
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "OrgaBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "MsSQL Passwort"
            oSetting.DisplayEntry = "MSSQL_Passwd"
            oSetting.Entry = ""
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "s"
            oSettings.Add(oSetting)

            'Setting WinBack.InterneDeklaration
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "WinBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "Die Zutatenliste wird aus der internen Deklaration der Rohstoffe erstellt." & vbCrLf & "Ist die interne Deklaration leer, wird das Feld externe Deklaration verwendet"
            oSetting.DisplayEntry = "InterneDeklaration"
            oSetting.Entry = "1"
            oSetting.RestartNeeded = 0
            oSetting.FormatString = "s"
            oSettings.Add(oSetting)

        End If

        Return oSettings
    End Function
#End Region

#Region "IOrgasoftService"
    Public ReadOnly Property ServiceName As String Implements IOrgasoftService.ServiceName
        Get
            Return "WinBack Festwerte"
        End Get
    End Property
#End Region
End Class
