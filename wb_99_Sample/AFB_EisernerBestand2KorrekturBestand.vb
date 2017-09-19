Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports System.ComponentModel

<Export(GetType(IClassExtension))>
<ExportMetadata("Description", "AddIn erlaubt beim Eisernen Bestand auch negative Zahlen und ergänzt ein weiteres Property für den korrigierten Bestand")>
Public Class AFB_EisernerBestand2KorrekturBestand_Alt

    Implements IExtension, IClassExtension

    Public Property InfoContainer As Common.IInfoContainer Implements IExtension.InfoContainer
    Public Property ServiceProvider As Common.IOrgasoftServiceProvider Implements IExtension.ServiceProvider
    Public Property Extendee As FrameWork.IFrameWorkClass Implements IClassExtension.Extendee

    Dim oFactory As IFactoryService

    Public Sub DefineMembersEx() Implements IClassExtension.DefineMembersEx

        With Extendee.GetPropertyInfo("EisernerBestand")
            .InputType = Common.InputType.ValueNegative
            .CustomValidator = Nothing
        End With

        With Extendee.GetPropertyInfo("EisernerBestandKumuliert")
            .InputType = Common.InputType.ValueNegative
            .CustomValidator = Nothing
        End With

        ' Factory-Objekt holen, damit neu Instanzen der notwendigen Objekte erzeugt werden können
        oFactory = ServiceProvider.GetService(GetType(IFactoryService))

        ' Neues Property definieren
        Dim oMP = oFactory.GetManagedProperty
        With oMP
            .AccessPermission = .AccessPermission And Common.PropertyPermission.Read
            .PropertyName = "KorrigierterBestand"
            .FormattingInfo = Common.FormattingInfo.Quantity
            .DataType = DbDataType.Currency
            .DatabaseField = String.Empty
            .DefaultValue = 0D
            .Internal = True
        End With
        Extendee.AddCustomProperty(oMP)
        AddHandler CType(Extendee, INotifyPropertyChanged).PropertyChanged, AddressOf PropertyChangedHandler
        AddHandler CType(Extendee, ICollectionSubClass).InitializationComplete, AddressOf FoundHandler

    End Sub

    Private Sub PropertyChangedHandler(sender As Object, e As PropertyChangedEventArgs)
        Select Case e.PropertyName
            Case "BestandKumuliert", "EisernerBestandKumuliert"
                SetKorrigierterBestand()
        End Select
    End Sub

    Private Sub FoundHandler(sender As Object, e As EventArgs)
        SetKorrigierterBestand()
    End Sub

    Private Sub SetKorrigierterBestand()
        Extendee.SetPropertyValue("KorrigierterBestand", CDec(Extendee.GetPropertyValue("BestandKumuliert")) - CDec(Extendee.GetPropertyValue("EisernerBestandKumuliert")))
    End Sub

    ''' <summary>
    ''' Angabe, welche Klasse durch diese Extension erweitert wird
    ''' </summary>
    Public ReadOnly Property ExtendedType As String Implements IClassExtension.ExtendedType
        Get
            Return "Artikel.HandelsArtikel.ArtikelFilialDaten"
        End Get
    End Property

    ''' <summary>
    ''' Initialize wird nach DefineMembersEx aufgerufen, hier keine Aktion notwendig
    ''' </summary>
    Public Sub Initialize() Implements IExtension.Initialize
    End Sub


End Class
