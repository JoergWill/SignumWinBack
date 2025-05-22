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
        'in wb_Main registrieren
        wb_Main_Shared.RegisterAddIn("ob_Admin_OrgaBackSettings")
        'AssemblyResolve wird definiert in WinBackAddIn.Erweiterte Kompilierungsoptionen
        If wb_Global.AssemblyResolve Then
            'Die eigenen dll-Files in sep. Verzeichnis verlagern
            AddHandler System.AppDomain.CurrentDomain.AssemblyResolve, AddressOf MyAssemblyResolve
        End If
    End Sub

    Private Function MyAssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
        Return wb_Main_Shared.MyAssemblyResolve(sender, args, GetType(ob_Admin_OrgaBackSettings).Assembly)
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

            'Setting WinBack.InterneDeklaration
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "WinBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = " 0 - Die Zutatenliste wird aus der externen Deklaration der Rohstoffe erstellt." & vbCrLf & "1 - Die Zutatenliste wird aus der internen Deklaration der Rohstoffe erstellt. Ist die interne Deklaration leer, wird das Feld externe Deklaration verwendet"
            oSetting.DisplayEntry = "InterneDeklaration"
            'oSetting.Entry = "1"
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "n"
            oSettings.Add(oSetting)

            'Setting WinBack.ENummer
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "WinBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "0 - Die Zutatenliste enthält keine E-Nummern. Anstelle der E-Nummern wird der entsprechende Text verwendet" & vbCrLf & "1 - Die Zutatenliste enthält E-Nummern anstatt der Texte (wo möglich)"
            oSetting.DisplayEntry = "ENummernInZutatenListe"
            'oSetting.Entry = "0"
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "n"
            oSettings.Add(oSetting)

            'Setting WinBack.ZutatenlisteOptimieren
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "WinBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "0 - Die Zutatenliste wird nicht optimiert" & vbCrLf & "1 - Zutatenliste so weit wie möglich optimieren"
            oSetting.DisplayEntry = "OptimierenZutatenListe"
            'oSetting.Entry = "1"
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "n"
            oSettings.Add(oSetting)

            'Setting WinBack.ZutatenlisteOptimieren
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "WinBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "0 - 'Keine Angabe' bei Allergenen wird als fehlerhaft ausgewertet" & vbCrLf & "1 - 'Keine Angabe' bei Allergenen wird ignoriert"
            oSetting.DisplayEntry = "AllergenKeineAngabeError"
            'oSetting.Entry = "1"
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "n"
            oSettings.Add(oSetting)

            'Setting WinBack.Cloud-URL
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "WinBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "Internet-Adresse IP-Adresse der WinBack-Cloud "
            oSetting.DisplayEntry = "WinBackCloudURL"
            'oSetting.Entry = ""
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "s"
            oSettings.Add(oSetting)

            'Setting WinBack.Cloud-Pass
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "WinBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "Passwort WinBack-Cloud "
            oSetting.DisplayEntry = "WinBackCloudPass"
            'oSetting.Entry = ""
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "s"
            oSettings.Add(oSetting)

            'Setting WinBack.Datenlink-CAT
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "WinBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "datenlink Company-Token (CT)"
            oSetting.DisplayEntry = "datenlink_CT"
            'oSetting.Entry = ""
            oSetting.RestartNeeded = 0
            oSetting.FormatString = "s"
            oSettings.Add(oSetting)

            'Setting WinBack.Datenlink-PAT
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "WinBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "datenlink Application-Token (AT)"
            oSetting.DisplayEntry = "datenlink_AT"
            'oSetting.Entry = ""
            oSetting.RestartNeeded = 0
            oSetting.FormatString = "s"
            oSettings.Add(oSetting)

            'Setting WinBack.GruppeBackwaren
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "OrgaBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "OrgaBack Artikelgruppe Backwaren" & vbCrLf &
                                   "Ganzahliger Wert oder RegEx-Ausdruck für einen Bereich von Artikelgruppen" & vbCrLf & vbCrLf &
                                   "Bespielsweise     ^1[0-9]       für den Bereich von 10-19"
            oSetting.DisplayEntry = "GruppeBackwaren"
            'oSetting.Entry = "10"
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "s"
            oSettings.Add(oSetting)

            'Setting WinBack.GruppeRohstoffe
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "OrgaBack"
            oSetting.SubCategory = "WinBack"
            oSetting.Description = "OrgaBack Artikelgruppe Rohstoffe" & vbCrLf &
                                   "Ganzahliger Wert oder RegEx-Ausdruck für einen Bereich von Artikelgruppen" & vbCrLf & vbCrLf &
                                   "Bespielsweise     ^2[0-9]|      für den Bereich von 20-29"
            oSetting.DisplayEntry = "GruppeRohstoffe"
            'oSetting.Entry = "20"
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "s"
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
            'oSetting.Entry = ""
            oSetting.RestartNeeded = 1
            oSetting.FormatString = "s"
            oSettings.Add(oSetting)

            'Setting OrgaBack.Sonderartikel 0R9999
            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "OrgaBack"
            oSetting.SubCategory = "WinBack"
            oSetting.DisplayEntry = "Artikelnummer_SonderArtikel "
            oSetting.Description = "Im Sonderartikel (0R9999) werden alle Rückmeldungen der Produktion gesammelt," & vbCrLf &
                                    "die keinem Produktionsauftrag zugeordnet werden können."
            'oSetting.Entry = "sa"
            oSetting.RestartNeeded = 1
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
