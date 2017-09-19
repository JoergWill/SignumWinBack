Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.Common
Imports System.Windows.Forms

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Neu eingeloggten Mitarbeiter begrüßen")>
Public Class WelcomeAddIn
    Implements IExtension

    Public Property InfoContainer As Common.IInfoContainer Implements Extensibility.IExtension.InfoContainer
    Public Property ServiceProvider As Common.IOrgasoftServiceProvider Implements Extensibility.IExtension.ServiceProvider

    Private oFactory As IFactoryService
    Private oSetting As ISettingService

    Public Sub Initialize() Implements IExtension.Initialize

        oFactory = TryCast(ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)
        oSetting = TryCast(ServiceProvider.GetService(GetType(ISettingService)), ISettingService)
        Dim IEB As IEventBroker = TryCast(ServiceProvider.GetService(GetType(IEventBroker)), IEventBroker)
        If IEB IsNot Nothing Then
            AddHandler IEB.LoginChanged, AddressOf EmployeeLoggedIn
        End If

        ' Beim Laden des AddIns ist schon ein Mitarbeiter angemeldet, der sollte auch begrüßt werden
        EmployeeLoggedIn(Me, EventArgs.Empty)

    End Sub

    Private Sub EmployeeLoggedIn(sender As Object, e As EventArgs)
        Dim sEmployee As String = TryCast(oSetting.GetSetting("Anmeldung.Mitarbeiter"), String)
        If Not String.IsNullOrEmpty(sEmployee) Then

            Dim oData As IData = oFactory.GetData
            Dim sName As String
            Using oTable = oData.OpenDataTable(Database.Main, "SELECT Vorname, Nachname FROM Mitarbeiter WHERE MitarbeiterKürzel=@M", LockType.ReadOnly, sEmployee)
                sName = CType(oTable.Rows(0)(0), String) & " " & CType(oTable.Rows(0)(1), String)
            End Using

            MessageBox.Show("Herzlich willkommen " & sName & "!", "Welcome-AddIn", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

End Class
