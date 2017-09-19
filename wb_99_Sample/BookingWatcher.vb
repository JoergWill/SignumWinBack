Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.Common
Imports System.Windows.Forms
Imports System.IO

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Überwacht die Tabelle Verbuchung auf unverbuchbare Sätze")>
Public Class BookingWatcher
    Implements IExtension

    Public Property InfoContainer As Common.IInfoContainer Implements Extensibility.IExtension.InfoContainer
    Public Property ServiceProvider As Common.IOrgasoftServiceProvider Implements Extensibility.IExtension.ServiceProvider

    Private oFactory As IFactoryService
    Private oSetting As ISettingService
    Private oViewProvider As IViewProvider
    Private iMax As Integer = 999
    Private oTimer As New Timer
    Private iLastErrorCounter As Integer

    Public Sub Initialize() Implements IExtension.Initialize

        oFactory = TryCast(ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)
        oSetting = TryCast(ServiceProvider.GetService(GetType(ISettingService)), ISettingService)
        oViewProvider = TryCast(ServiceProvider.GetService(GetType(IViewProvider)), IViewProvider)

        iMax = CType(oSetting.GetSetting("BookingService.Retries"), Integer)

        oTimer.Interval = 60000
        AddHandler oTimer.Tick, AddressOf CheckBookingTable
        oTimer.Start()

    End Sub

    Private Sub CheckBookingTable(sender As Object, e As EventArgs)

        Dim iErrorCounter As Integer
        Dim oData As IData = oFactory.GetData
        Using oTable = oData.OpenDataTable(Database.Main, "SELECT Count(*) FROM Verbuchung WHERE VerbuchungsStatus>=@M", LockType.ReadOnly, iMax)
            iErrorCounter = CType(oTable.Rows(0)(0), Integer)
        End Using

        If iErrorCounter > iLastErrorCounter Then
            oTimer.Enabled = False
            If MessageBox.Show("Es sind Sätze in der Verbuchungstabelle, die nicht verbucht werden können!" & vbCrLf & "Sollen diese angezeigt werden?", "BookingWatcher-AddIn",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                oViewProvider.ShowSqlView(Database.Main, "SELECT * FROM Verbuchung WHERE VerbuchungsStatus>=" & iMax.ToString)
            End If
        End If
        iLastErrorCounter = iErrorCounter
        oTimer.Enabled = True
    End Sub

End Class
