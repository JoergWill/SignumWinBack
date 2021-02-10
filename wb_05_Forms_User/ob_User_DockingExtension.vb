Imports System.Reflection
Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.GUI

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Erweiterung des Mitarbeiter-Dockingfensters um ein Unterfenster WinBack")>
Public Class ob_User_DockingExtension
    Implements IDockingExtension

    Private _MenuService As Common.IMenuService
    Private _ViewProvider As IViewProvider
    Private _ContextTabs As List(Of GUI.ITab)
    Private xForm As Windows.Forms.Form

    Private OrgaSoftEditState As wb_Global.EditState
    Private OldPersonalNr As String

    ''' <summary>
    ''' Falls die Extension ein eigenes Context-Ribbon zum bestehenden Ribbon hinzufügen möchte, kann sie dieses hier zurückliefern
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ContextTabs As ITab() Implements IDockingExtension.ContextTabs
        Get
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Klasse, deren Docking-Layout erweitert werden soll (Mitarbeiter)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ExtendedType As ObjectEnum Implements IDockingExtension.ExtendedType
        Get
            Return ObjectEnum.Employees
        End Get
    End Property

    Private _Extendee As IFrameWorkClass

    ''' <summary>
    ''' Referenz auf die Framework-Klasse, die im Docking-Fenster derzeit angezeigt wird
    ''' </summary>
    Public Property Extendee As IFrameWorkClass Implements IDockingExtension.Extendee
        Get
            Return _Extendee
        End Get
        Set(value As IFrameWorkClass)
            _Extendee = value
            ' Alle Objekte, die Suchen/Speichern/Löschen/Blättern unterstützen, implementieren das Interface INavigationClass
            ' Über dieses Interface werden Events bereitgestellt, die eine enge Interaktion mit dem Objekt zulassen.
            ' Die wichtigsten Events sind:

            ' Invalidated:  Das Objekt hat derzeit keinen gültigen Status (Eingabemaske befindet sich im Suchmodus)
            ' Found:        Es wurde ein Objekt gefunden und wird nun angezeigt
            ' AddingNew:    Es wird ein neues, leeres Objekt für die Neuanlage erzeugt und angezeigt (aber noch nicht gespeichert)
            ' BeforeUpdate:
            ' BeforeDelete:
            ' BeforeCopy:
            ' Updated:
            ' Deleted:
            ' Committed:

            If _Extendee IsNot Nothing AndAlso TypeOf _Extendee Is INavigationClass Then
                With DirectCast(_Extendee, INavigationClass)
                    AddHandler .Invalidated, AddressOf Extendee_Invalid
                    AddHandler .AddingNew, AddressOf Extendee_AddNew
                    AddHandler .Found, AddressOf Extendee_Found
                    AddHandler .BeforeUpdate, AddressOf Extendee_BeforeUpdate
                    AddHandler .Updated, AddressOf Extendee_Updated
                    AddHandler .BeforeDelete, AddressOf Extendee_BeforeDelete
                    AddHandler .Deleted, AddressOf Extendee_Deleted
                    AddHandler .BeforeCopy, AddressOf Extendee_BeforeCopy
                    AddHandler .Committed, AddressOf Extendee_Committed
                End With
            End If
        End Set
    End Property

    Private Sub Extendee_Valid(sender As Object, e As EventArgs)
        'For Each oForm In _SubForms.Values
        '    If oForm IsNot Nothing AndAlso Not DirectCast(oForm, UserControl).IsDisposed Then
        '        oForm.ExecuteCommand("VALID", Nothing)
        '    End If
        'Next
    End Sub

    Private Sub Extendee_Invalid(sender As Object, e As EventArgs)
        OrgaSoftEditState = wb_Global.EditState.Invalid
        OldPersonalNr = ""
        Debug.Print("User_DockingExtension Invalid")
    End Sub

    Private Sub Extendee_AddNew(sender As Object, e As EventArgs)
        OrgaSoftEditState = wb_Global.EditState.AddNew
        OldPersonalNr = ""
        Debug.Print("User_DockingExtension AddNew")
    End Sub

    Private Sub Extendee_Found(sender As Object, e As EventArgs)
        OrgaSoftEditState = wb_Global.EditState.Edit
        OldPersonalNr = _Extendee.GetPropertyValue("PersonalNr").ToString
        Debug.Print("User_DockingExtension Found")
    End Sub

    Private Sub Extendee_BeforeUpdate(sender As Object, e As EventArgs)
        Debug.Print("User_DockingExtension BeforeUpdate")
    End Sub

    Private Sub Extendee_Updated(sender As Object, e As EventArgs)
        Debug.Print("User_DockingExtension Updated")
    End Sub

    Private Sub Extendee_BeforeDelete(sender As Object, e As EventArgs)
        Debug.Print("User_DockingExtension BeforeDelete")
    End Sub

    Private Sub Extendee_Deleted(sender As Object, e As EventArgs)
        Debug.Print("User_DockingExtension Deleted")
        Dim PersonalNr As String = _Extendee.GetPropertyValue("PersonalNr").ToString
        wb_User_Shared.User.Delete(wb_Global.UNDEFINED, PersonalNr)
    End Sub

    Private Sub Extendee_BeforeCopy(sender As Object, e As EventArgs)
        Debug.Print("User_DockingExtension BeforeCopy")
    End Sub

    Private Sub Extendee_Committed(sender As Object, e As EventArgs)
        Debug.Print("User_DockingExtension Committed")

        Dim Vorname As String = _Extendee.GetPropertyValue("Vorname").ToString
        Dim Nachname As String = _Extendee.GetPropertyValue("Nachname").ToString
        Dim PersonalNr As String = _Extendee.GetPropertyValue("PersonalNr").ToString
        Dim FilialZuordnung As String = _Extendee.GetPropertyValue("FilialZuordnung").ToString
        Dim Name As String = Vorname & " " & Nachname
        Dim GruppeNr As String = "4"

        Dim iMFFIdx As Short = Short.MinValue         ' hier soll der Index eines Multifunktionsfelds hinein
        Dim oMFF As ICollectionSubClass = Nothing     ' hier wird das eigentliche MFF-Objekt gehalten

        ' die Multifunktionsfelder sind über das Property "MultiFunktionsFeld" zugänglich, welches eine ICollectionClass ist
        ' via FindInInnerlist kann man mit Kriterien suchen, die ein Element in der Collection erfüllen muss
        ' FeldNr ist ein Property eines MFF, natürlich ist es auch möglich, erstmal durch die Collection zu iterieren um zu schauen, was an MFF überhaupt enthalten ist
        iMFFIdx = DirectCast(_Extendee.GetPropertyValue("MultiFunktionsFeld"), ICollectionClass).FindInInnerList("FeldNr=" & CStr(wb_Global.MFF_USerGruppe))

        ' sollte ein MFF mit FeldNr=1 gefunden worden sein, so wurde dessen Index innerhalb der Collection zurückgeliefert
        ' mit diesem Index greift man auf das Element zu, die Elemente innerhalb einer ICollectionClass sind vom Typ ICollectionSubClass
        If iMFFIdx >= 0 Then
            oMFF = DirectCast(_Extendee.GetPropertyValue("MultiFunktionsFeld"), ICollectionClass).InnerList.Cast(Of ICollectionSubClass).ElementAt(iMFFIdx)
            If oMFF IsNot Nothing Then
                GruppeNr = oMFF.PropertyValueCollection(2).Value
            End If
        End If

        'Pürfen ob der User einer Produktionsfiliale zugeordnet ist
        If wb_Filiale.FilialeIstProduktion(FilialZuordnung) Then
            'Prüfen ob eine Personal-Nummer angeben ist. Ohne Personal-Nummer kann keine Synchronisation mit WinBack erfolgen
            If PersonalNr = "" Then
                MsgBox("Es ist keine Personal-Nummer angegeben !" & vbCrLf & "Damit ist keine Synchronisation mit WinBack möglich", MsgBoxStyle.Critical, "WinBack-Benutzer")
            Else
                Select Case OrgaSoftEditState
                    Case wb_Global.EditState.Edit
                        If Not wb_User_Shared.User.Update(OldPersonalNr, Name, PersonalNr, GruppeNr) Then
                            'Beim Neuanlegen ist das Passwort mit der Personal-Nummer identisch
                            wb_User_Shared.User.AddNew(Name, PersonalNr, PersonalNr, GruppeNr)
                        End If
                        'Anzeige im WinBack-Fenster "live" aktualisieren
                        wb_User_Shared.Reload(sender)

                    Case wb_Global.EditState.AddNew
                        'Beim Neuanlegen ist das Passwort mit der Personal-Nummer identisch
                        wb_User_Shared.User.AddNew(Name, PersonalNr, PersonalNr, GruppeNr)
                        'Anzeige im WinBack-Fenster "live" aktualisieren
                        wb_User_Shared.Reload(sender)

                End Select
            End If
        End If
    End Sub

    Public Property InfoContainer As IInfoContainer Implements IExtension.InfoContainer
    Public Property ServiceProvider As IOrgasoftServiceProvider Implements IExtension.ServiceProvider

    ''' <summary>
    ''' Initialisierung des AddIns beim Starten von Orgasoft.
    ''' </summary>
    ''' <remarks>
    ''' Achtung: Der FormController ist zu diesem Zeitpunkt noch nicht verfügbar!
    ''' Dieser wird erst erzeugt und gesetzt, wenn das Fenster auch angezeigt werden soll.
    ''' </remarks>
    Public Sub Initialize() Implements IExtension.Initialize
        'siehe Mail vom 13.Juli 2017 J.Erhardt - laden der dll schläg fehl 
        'AssemblyResolve wird definiert in WinBackAddIn.Erweiterte Kompilierungsoptionen
#If AssemblyResolve Then
        AddHandler System.AppDomain.CurrentDomain.AssemblyResolve, AddressOf MyAssemblyResolve
#End If
        _MenuService = TryCast(ServiceProvider.GetService(GetType(IMenuService)), IMenuService)
        _ViewProvider = TryCast(ServiceProvider.GetService(GetType(IViewProvider)), IViewProvider)
        _SubForms.Add("@wb_User_DockingControlObjectInfo", Nothing)
    End Sub

    Private Function MyAssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
        Return wb_Main_Shared.MyAssemblyResolve(sender, args, GetType(ob_User_DockingExtension).Assembly)
    End Function

    Private bContextTabInitialized As Boolean

    ''' <summary>
    ''' Diese Routine wird immer aufgerufen, wenn ein DockingController vom passenden Typ erzeugt wird. 
    ''' Hier können Einträge in die bestehenden Context-Tabs hinzugefügt werden. 
    ''' Achtung: Das Hinzufügen darf nur beim ersten Mal passieren, die Context-Tabs werden gecached!
    ''' </summary>
    Public Sub InitializeContextTabs() Implements IDockingExtension.InitializeContextTabs
        If Not bContextTabInitialized Then                 ' einmalige Ausführung sicherstellen
            bContextTabInitialized = True

            For Each oTab In Me.FormController.ContextualTabs
                Debug.Print("Tabs " & oTab.Name.ToString)
            Next

            Dim oSystemTab = From oTab In Me.FormController.ContextualTabs Where oTab.Name = "rtabMitarbeiter" Select oTab
            If oSystemTab IsNot Nothing AndAlso oSystemTab.Count > 0 Then
                oSystemTab(0).AddGroup("WinBack", "Produktion")
                oSystemTab(0).GetGroups(2).AddButton("btnBenutzer", "WinBack-Mitarbeiter", "Mtarbeiter-Verwaltung in WinBack", My.Resources.MainUser_16x16, My.Resources.MainUser_32x32, AddressOf ShowUserForm)
                oSystemTab(0).GetGroups(2).AddButton("BtnUserGroup", "Mitarbeiter-Gruppen", "Gruppen und Gruppen-Rechte verwalten", My.Resources.UserGruppen_32x32, My.Resources.UserGruppen_32x32, AddressOf ShowUserGroup)
            End If
        End If
    End Sub

    ''' <summary>
    ''' WinBack-Mitarbeiter-Hauptmenu in separatem Fenster anzeigen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowUserForm(sender As Object, e As EventArgs)
        xForm = _ViewProvider.OpenForm(New wb_User_Main(ServiceProvider), My.Resources.MainUser_16x16)
        'Fensterposition aus winback.ini
        wb_DockBarPanelShared.SetFormBoundaries(xForm, "User")

    End Sub

    ''' <summary>
    ''' WinBack-Fenster Gruppen-Rechte in separatem Fenster anzeigen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowUserGroup(sender As Object, e As EventArgs)
        Dim UserGruppenRechte = New wb_User_GruppenRechte
        UserGruppenRechte.ShowDialog()
    End Sub

    ''' <summary>
    ''' Liefert zu einem FormKey eine Instanz des UserControls (Mitarbeiter) zurück
    ''' </summary>
    ''' <param name="FormKey"></param>
    ''' <returns></returns>
    Public Function ProvideInstance(FormKey As String) As IBasicFormUserControl Implements IDockingExtension.ProvideInstance
        'If _SubForms.ContainsKey(FormKey) Then
        '    Dim oForm = _SubForms(FormKey)
        '    If oForm Is Nothing OrElse DirectCast(oForm, UserControl).IsDisposed Then
        '        ' Adresse der Klasse, die die Arbeit macht !!
        '        oForm = New wb_User_DockingControl(Me)
        '        _SubForms(FormKey) = oForm
        '    End If
        '    Return oForm
        'End If
        Return Nothing
    End Function

    Private _SubForms As New Dictionary(Of String, IBasicFormUserControl)

    ''' <summary>
    ''' Liste aller FormKeys, für die das AddIn Unterfenster definiert
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SubFormKeys As String() Implements IDockingExtension.SubFormKeys
        Get
            Return _SubForms.Keys.ToArray
        End Get
    End Property

    ''' <summary>
    ''' Liefert eine Instanz des FormControllers, über den das AddIn eigene Fenster öffnen und mit anderen Fenstern kommunizieren kann
    ''' </summary>
    ''' <returns></returns>
    Public Property FormController As IFormControllerBasic Implements IDockingExtension.FormController

End Class
