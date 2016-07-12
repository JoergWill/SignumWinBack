Imports System.Windows.Forms
Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.GUI

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Erweiterung des Adress-Dockingfensters um ein Unterfenster mit Objekt-Informationen")>
Public Class AddressDockingExtension
    Implements IDockingExtension

    Private _MenuService As Common.IMenuService
    Private _ViewProvider As IViewProvider
    Private _ContextTabs As List(Of GUI.ITab)

    ''' <summary>
    ''' Falls die Extension ein eigenes Context-Ribbon zum bestehenden Ribbon hinzufügen möchte, kann sie dieses hier zurückliefern
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ContextTabs As ITab() Implements IDockingExtension.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("AddressCustomContextTab", "My Address Tab", "Eigenes ContextTab für das Adressen-Docking-Fenster")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("AddressContextGroup", "ContextFormTest")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("AddressContextButton1", "WhatEver", "Do Something", My.Resources.MainChargen_16x16, My.Resources.MainChargen_32x32, AddressOf Search)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Private Sub Search(sender As Object, e As EventArgs)
        MessageBox.Show("Search!", "AddIn", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub

    ''' <summary>
    ''' Klasse, deren Docking-Layout erweitert werden soll
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ExtendedType As ObjectEnum Implements IDockingExtension.ExtendedType
        Get
            Return ObjectEnum.Employees

            '            Return ObjectEnum.Articles
            '            Return ObjectEnum.Addresses
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
            ' BeforeUpdate, BeforeDelete, BeforeCopy, Updated, Deleted, Committed
            If _Extendee IsNot Nothing AndAlso TypeOf _Extendee Is INavigationClass Then
                With DirectCast(_Extendee, INavigationClass)
                    AddHandler .Invalidated, AddressOf Extendee_Invalid
                    AddHandler .AddingNew, AddressOf Extendee_Valid
                    AddHandler .Found, AddressOf Extendee_Valid
                End With
            End If
        End Set
    End Property

    Private Sub Extendee_Valid(sender As Object, e As EventArgs)
        For Each oForm In _SubForms.Values
            If oForm IsNot Nothing AndAlso Not DirectCast(oForm, UserControl).IsDisposed Then
                oForm.ExecuteCommand("VALID", Nothing)
            End If
        Next
    End Sub

    Private Sub Extendee_Invalid(sender As Object, e As EventArgs)
        For Each oForm In _SubForms.Values
            If oForm IsNot Nothing AndAlso Not DirectCast(oForm, UserControl).IsDisposed Then
                oForm.ExecuteCommand("INVALID", Nothing)
            End If
        Next
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
        _MenuService = TryCast(ServiceProvider.GetService(GetType(IMenuService)), IMenuService)
        _ViewProvider = TryCast(ServiceProvider.GetService(GetType(IViewProvider)), IViewProvider)
        _SubForms.Add("@AddressDockingUserControlObjectInfo", Nothing)
    End Sub

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

            '            Dim oSystemTab = From oTab In Me.FormController.ContextualTabs Where oTab.Name = "rtabAdressen" Select oTab
            '            Dim oSystemTab = From oTab In Me.FormController.ContextualTabs Where oTab.Name = "rtabArtikel" Select oTab
            Dim oSystemTab = From oTab In Me.FormController.ContextualTabs Where oTab.Name = "rtabMitarbeiter" Select oTab
            If oSystemTab IsNot Nothing AndAlso oSystemTab.Count > 0 Then
                oSystemTab(0).GetGroups(1).AddButton("AddressDockingExtensionDeveloperButton", "Developer-Info", "Per AddIn erweitertes Docking-Fenster zur Anzeige von Entwickler-Informationen zum angezeigten Objekt", My.Resources.MainChargen_16x16, My.Resources.MainChargen_32x32, AddressOf LoadDockingSubForm)
            End If
        End If
    End Sub

    Private Sub LoadDockingSubForm(sender As Object, e As EventArgs)
        If Me.FormController IsNot Nothing Then
            Me.FormController.ExecuteCommand("AddressDockingUserControlObjectInfo", Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Liefert zu einem FormKey eine Instanz des UserControls zurück
    ''' </summary>
    ''' <param name="FormKey"></param>
    ''' <returns></returns>
    Public Function ProvideInstance(FormKey As String) As IBasicFormUserControl Implements IDockingExtension.ProvideInstance
        If _SubForms.ContainsKey(FormKey) Then
            Dim oForm = _SubForms(FormKey)
            If oForm Is Nothing OrElse DirectCast(oForm, UserControl).IsDisposed Then
                oForm = New AddressDockingUserControl(Me)
                _SubForms(FormKey) = oForm
            End If
            Return oForm
        End If
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
