Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork

<Export(GetType(IClassExtension))>
<ExportMetadata("Description", "Property Bonus beim Mitarbeiter ergänzen")>
Public Class MitarbeiterBonusErweiterung
    Implements IExtension, IClassExtension

    Public Property InfoContainer As Common.IInfoContainer Implements IExtension.InfoContainer
    Public Property ServiceProvider As Common.IOrgasoftServiceProvider Implements IExtension.ServiceProvider
    Public Property Extendee As FrameWork.IFrameWorkClass Implements IClassExtension.Extendee

    Private oFactory As IFactoryService

    Public Sub DefineMembersEx() Implements IClassExtension.DefineMembersEx

        ' Factory-Objekt holen, damit neu Instanzen der notwendigen Objekte erzeugt werden können
        oFactory = ServiceProvider.GetService(GetType(IFactoryService))

        ' Zusätzliche UserTable definieren, damit das Framework weiß, wo wir das neue Property speichern wollen
        Dim oATI = oFactory.GetAdditionalTableInfo
        With oATI
            .TableName = "UserMitarbeiterExtension"
            .PrimaryKeyDbType = DbDataType.String
            .PrimaryKey = "MitarbeiterKürzel"
            .DeleteIfEmpty = False
        End With
        CType(Extendee, INavigationClass).RegisterAdditionalTable(oATI)

        ' Neues Property definieren
        Dim oMP = oFactory.GetManagedProperty
        With oMP
            .PropertyName = "Bonus"
            .FormattingInfo = Common.FormattingInfo.GeneralAmount
            .DataType = DbDataType.Currency
            .DatabaseField = "Bonus"
            .DefaultValue = 0D
            .CustomValidatorEx = AddressOf BonusValidating
            .TableName = "UserMitarbeiterExtension"
        End With
        Extendee.AddCustomProperty(oMP)

    End Sub

    ''' <summary>
    ''' Validation-Handler zum Prüfen der Gültigkeit des Properties
    ''' </summary>
    Private Function BonusValidating(PropertyName As String, NewValue As Object, ByRef ErrorText As String) As Boolean
        Dim dGehalt As Decimal = CType(Extendee.GetPropertyInfo("Gehalt").Value, Decimal)
        Dim dBonus As Decimal = Convert.ToDecimal(NewValue)
        Dim bResult As Boolean = True
        If dBonus < 0 Then
            ErrorText = "Der Bonus kann nicht kleiner als 0 sein!"
            bResult = False
        ElseIf dBonus > dGehalt * 3 Then
            ErrorText = "Der Bonus darf maximal 3 Monatsgehälter betragen!"
            bResult = False
        End If
        Return bResult
    End Function

    ''' <summary>
    ''' Angabe, welche Klasse durch diese Extension erweitert wird
    ''' </summary>
    Public ReadOnly Property ExtendedType As String Implements IClassExtension.ExtendedType
        Get
            Return "Mitarbeiter"
        End Get
    End Property

    ''' <summary>
    ''' Initialize wird nach DefineMembersEx aufgerufen, hier keine Aktion notwendig
    ''' </summary>
    Public Sub Initialize() Implements IExtension.Initialize
    End Sub

End Class
