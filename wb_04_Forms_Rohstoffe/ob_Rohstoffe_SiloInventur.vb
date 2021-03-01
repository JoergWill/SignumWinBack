'Imports System.Reflection
'Imports Signum.OrgaSoft.Common
'Imports Signum.OrgaSoft.Extensibility

'Public Class ob_Rohstoffe_SiloInventur
'    Implements IExtension

'    Private oFactory As IFactoryService
'    Private oObject As Signum.OrgaSoft.FrameWork.IFrameWorkClass

'    Public Property InfoContainer As IInfoContainer Implements IExtension.InfoContainer
'    Public Property ServiceProvider As IOrgasoftServiceProvider Implements IExtension.ServiceProvider

'    Public Sub Initialize() Implements IExtension.Initialize
'        'Versuche die eigenen dll-Files in sep. Verzeichnis zu verlagern
'        AddHandler System.AppDomain.CurrentDomain.AssemblyResolve, AddressOf MyAssemblyResolve

'        'Factory-Service
'        oFactory = TryCast(ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)
'        oObject = oFactory.GetOrgasoftObject(ObjectEnum.InventoryManagement)
'        Debug.Print(oObject.ToString)

'    End Sub

'    Private Function MyAssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
'        Return wb_Main_Shared.MyAssemblyResolve(sender, args, GetType(ob_Main_Menu).Assembly)
'    End Function

'    Public Shared Function SetInventoryAmount(ArtikelNr As String, Menge As Double) As Boolean


'        Return True

'    End Function

'End Class
