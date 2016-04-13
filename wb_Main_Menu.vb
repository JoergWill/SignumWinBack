Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.Common
Imports System.Windows.Forms
Imports System.IO

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Erweitert das Ribbon um ein neues Tab und ruft ein eigenes Fenster auf")>
Public Class wb_Main_Menu
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
        Dim oNewTab = oMenuService.AddTab("CustomForm", "WinBack", "WinBack in Orgasoft.NET integriert")
        ' Das neue RibbonTab erhält eine Gruppe
        Dim oGrpChargen = oNewTab.AddGroup("WinBack", "Chargen")
        Dim oGrpArtikel = oNewTab.AddGroup("WinBack", "Artikel")
        Dim oGrpRezepte = oNewTab.AddGroup("WinBack", "Rezepte")
        ' ... und dieser Gruppe wird ein Button hinzugefügt
        oGrpChargen.AddButton("CustomFormButton1", "Chargen", "WinBack Auswertung Produktions-Chargen", My.Resources.Icon_Chargen, My.Resources.Icon_Chargen, AddressOf ShowChargenForm)
        oGrpArtikel.AddButton("CustomFormButton2", "Artikel", "WinBack Artikelverwaltung", My.Resources.Icon_Artikel, My.Resources.Icon_Artikel, AddressOf ShowArtikelForm)
        oGrpRezepte.AddButton("CustomFormButton3", "Rezepte", "WinBack Rezeptverwaltung", My.Resources.Icon_Material, My.Resources.Icon_Material, AddressOf ShowChargenForm)

    End Sub

    Private Sub ShowChargenForm(sender As Object, e As EventArgs)
        oViewProvider.OpenForm(New wb_MainChargen(ServiceProvider), My.Resources.EditTask_16)
    End Sub
    Private Sub ShowArtikelForm(sender As Object, e As EventArgs)
        oViewProvider.OpenForm(New wb_MainUser(ServiceProvider), My.Resources.EditTask_16)
    End Sub

End Class
