Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.Common
Imports System.Windows.Forms
Imports System.IO

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Neue Kassendaten automatisch zum Verbuchen anbieten")>
Public Class FileWatcher
    Implements IExtension

    Public Property InfoContainer As Common.IInfoContainer Implements Extensibility.IExtension.InfoContainer
    Public Property ServiceProvider As Common.IOrgasoftServiceProvider Implements Extensibility.IExtension.ServiceProvider

    Private oFactory As IFactoryService
    Private oSetting As ISettingService
    Private oViewProvider As IViewProvider
    Private oFSW As FileSystemWatcher

    Public Sub Initialize() Implements IExtension.Initialize

        oFactory = TryCast(ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)
        oSetting = TryCast(ServiceProvider.GetService(GetType(ISettingService)), ISettingService)
        oViewProvider = TryCast(ServiceProvider.GetService(GetType(IViewProvider)), IViewProvider)
        Try
            oFSW = New FileSystemWatcher("C:\Signum\Orgasoft\Kasse", "*.txt")
            oFSW.EnableRaisingEvents = True
            AddHandler oFSW.Changed, AddressOf FileChanged
        Catch
        End Try

        ' hMainWindow = oSetting.GetSetting()   ' steht in General.MaianformHandle

    End Sub

    Private Sub FileChanged(sender As Object, e As FileSystemEventArgs)
        If e.ChangeType = WatcherChangeTypes.Created Or e.ChangeType = WatcherChangeTypes.Changed Then
            If MessageBox.Show("Es sind neue Kassendaten vorhanden!" & vbCrLf & e.FullPath & "\" & e.Name & vbCrLf & "Sollen die Dateien gleich verarbeitet werden?", "FileWatcher-AddIn",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                oViewProvider.OpenForm(ObjectEnum.ReadPosData)
            End If
        End If
    End Sub

End Class
