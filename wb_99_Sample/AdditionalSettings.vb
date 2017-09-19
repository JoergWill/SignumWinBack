Option Strict On
Option Explicit On

Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.Common
Imports System.Windows.Forms
Imports System.IO

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Test Festwerte")>
Public Class AdditionalSettingsExtension
    Implements IExtension
    ' Dieses Interface muss nur implementiert werden, wenn das AddIn eigene Festwert-Einstellungen definieren möchte
    ' Andernfalls kann diese Zeile und die Region "ISettingProvider" ersatzlos gestrichen werden
    Implements ISettingProvider

    Private oFactory As IFactoryService
    Private oSetting As ISettingService

#Region "IExtension"
    Public Property InfoContainer As Common.IInfoContainer Implements Extensibility.IExtension.InfoContainer

    Public Property ServiceProvider As Common.IOrgasoftServiceProvider Implements Extensibility.IExtension.ServiceProvider

    Public Sub Initialize() Implements IExtension.Initialize
        oFactory = TryCast(ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)
        oSetting = TryCast(ServiceProvider.GetService(GetType(ISettingService)), ISettingService)

        Dim sZahl As String = TryCast(oSetting.ReadSetting("AddIn", "AddtionalSettings", "Zahl", String.Empty), String)
        Dim sPrimzahl As String = TryCast(oSetting.ReadSetting("AddIn", "AddtionalSettings", "Primzahl", String.Empty), String)

        Dim sMessage As String = String.Empty
        If Not (String.IsNullOrEmpty(sPrimzahl)) Then sMessage = "Der Festwert 'Primzahl' steht auf dem Wert '" & sPrimzahl & "'"
        If Not (String.IsNullOrEmpty(sZahl)) Then sMessage = "Der Festwert 'Zahl' steht auf dem Wert '" & sZahl & "'"
        If Not (String.IsNullOrEmpty(sMessage)) Then MessageBox.Show(sMessage, "AddtionalSettings-AddIn", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
#End Region

#Region "ISettingProvider"
    Public Function Settings() As IList(Of ISetting) Implements ISettingProvider.Settings

        Dim oSettings As New List(Of ISetting)

        ' Das AddIn muss nicht geladen/initialisiert worden sein, kann aber dennoch nach seinen Settings gefragt werden
        If (oFactory Is Nothing) AndAlso Not (ServiceProvider Is Nothing) Then
            oFactory = TryCast(ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)
        End If
        If Not (oFactory Is Nothing) Then
            Dim oSetting As ISetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "AddIn Festwert"
            oSetting.DisplayEntry = "Zahl"
            oSetting.Description = "Ein Beispiel für einen numerischen Festwert eines Addin"
            oSetting.SubCategory = "AddtionalSettings"   ' AddIn-Name
            oSetting.Entry = "Zahl"
            oSetting.RestartNeeded = 0
            oSetting.FormatString = "n"
            oSettings.Add(oSetting)

            oSetting = oFactory.GetSetting
            oSetting.DisplaySubCategory = "AddIn Festwert"
            oSetting.DisplayEntry = "Primzahl"
            oSetting.Description = "Ein Beispiel für einen Festwert eines Addin mit festen Einträgen"
            oSetting.SubCategory = "AddtionalSettings"
            oSetting.Entry = "Primzahl"
            oSetting.RestartNeeded = 0
            oSetting.SubItems = "1,3,5"
            oSettings.Add(oSetting)
        End If

        Return oSettings
    End Function
#End Region

#Region "IOrgasoftService"
    Public ReadOnly Property ServiceName As String Implements IOrgasoftService.ServiceName
        Get
            Return "Test Festwerte"
        End Get
    End Property
#End Region
End Class
