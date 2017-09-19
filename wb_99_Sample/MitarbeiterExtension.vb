Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork

<Export(GetType(IClassExtension))>
<ExportMetadata("Description", "Maximalgehalt festlegen")>
Public Class MitarbeiterGehaltEinschraenkung
    Implements IExtension, IClassExtension

    Private Property InfoContainer As Common.IInfoContainer Implements Extensibility.IExtension.InfoContainer
    Private Property ServiceProvider As Common.IOrgasoftServiceProvider Implements Extensibility.IExtension.ServiceProvider
    Private Property Extendee As FrameWork.IFrameWorkClass Implements Extensibility.IClassExtension.Extendee

    Public Sub DefineMembersEx() Implements IClassExtension.DefineMembersEx

        With Extendee.GetPropertyInfo("Gehalt")
            .MaxValue = 10000
            .RangeErrorText = "So viel darf man nicht verdienen!"
        End With

    End Sub

    Public ReadOnly Property ExtendedType As String Implements IClassExtension.ExtendedType
        Get
            Return "Mitarbeiter"
        End Get
    End Property

    Public Sub Initialize() Implements IExtension.Initialize
    End Sub

End Class

