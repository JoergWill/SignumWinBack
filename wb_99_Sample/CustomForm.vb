Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.Common
Imports System.Windows.Forms
Imports System.IO

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Erweitert das Ribbon um ein neues Tab und ruft ein eigenes Fenster auf")>
Public Class CustomForm
    Implements IExtension

    Public Property InfoContainer As Common.IInfoContainer Implements Extensibility.IExtension.InfoContainer
    Public Property ServiceProvider As Common.IOrgasoftServiceProvider Implements Extensibility.IExtension.ServiceProvider

    Private oViewProvider As IViewProvider
    Private oMenuService As IMenuService
    Private oSetting As ISettingService

    Public Sub Initialize() Implements IExtension.Initialize

        oViewProvider = TryCast(ServiceProvider.GetService(GetType(IViewProvider)), IViewProvider)
        oMenuService = TryCast(ServiceProvider.GetService(GetType(IMenuService)), IMenuService)
        oSetting = TryCast(ServiceProvider.GetService(GetType(ISettingService)), ISettingService)

        ' Fügt dem Ribbon ein neues RibbonTab hinzu
        Dim oNewTab = oMenuService.AddTab("CustomForm", "Eigene Programme", "Eigene Programme in Orgasoft.NET integriert")
        ' Das neue RibbonTab erhält eine Gruppe
        Dim oGrp = oNewTab.AddGroup("CustomFormTest", "Test")
        ' ... und dieser Gruppe wird ein Button hinzugefügt
        oGrp.AddButton("CustomFormButton1", "Test", "Zeige ein Testfenster", My.Resources.EditTask_16, My.Resources.EditTask_32, AddressOf ShowTestForm)

    End Sub

    Private Sub ShowTestForm(sender As Object, e As EventArgs)
        oViewProvider.OpenForm(New TestUserControl(ServiceProvider), My.Resources.EditTask_16)
    End Sub

End Class
