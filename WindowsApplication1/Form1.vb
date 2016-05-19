Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.Common
Imports System.Windows.Forms
Imports Signum.OrgaSoft.AddIn

Public Class Form1
    Implements IExtension

    Private oViewProvider As IViewProvider
    Private oMenuService As IMenuService
    Private oSetting As ISettingService
    Private oFactory As IFactoryService

    Public Property ServiceProvider As Signum.OrgaSoft.Common.IOrgasoftServiceProvider Implements Signum.OrgaSoft.Extensibility.IExtension.ServiceProvider
    Public Property InfoContainer As Signum.OrgaSoft.Common.IInfoContainer Implements Signum.OrgaSoft.Extensibility.IExtension.InfoContainer

    Private xForm As Form

    Public Sub Initialize() Implements IExtension.Initialize

        oViewProvider = TryCast(ServiceProvider.GetService(GetType(IViewProvider)), IViewProvider)
        oMenuService = TryCast(ServiceProvider.GetService(GetType(IMenuService)), IMenuService)
        oSetting = TryCast(ServiceProvider.GetService(GetType(ISettingService)), ISettingService)
        oFactory = TryCast(ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'CloseAllForms()
        Dim am As New wb_Artikel_Main()
        am
    End Sub
End Class
