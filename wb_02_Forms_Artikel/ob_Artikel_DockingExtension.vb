﻿Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.GUI

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Erweiterung des Artikel-Dockingfensters um ein Unterfenster WinBack")>
Public Class ob_Artikel_DockingExtension
    Implements IDockingExtension

    Private _MenuService As Common.IMenuService
    Private _ViewProvider As IViewProvider
    Private _ContextTabs As List(Of GUI.ITab)
    Private Komponente As New wb_Komponenten

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
            Return ObjectEnum.Articles
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
        Debug.Print("Article_DockingExtension Valid")
    End Sub

    ''' <summary>
    ''' Das Objekt hat derzeit keinen gültigen Status.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_Invalid(sender As Object, e As EventArgs)
        Debug.Print("Article_DockingExtension Invalid")
    End Sub

    ''' <summary>
    ''' Es wird ein neues, leeres Objekt erzeugt und angezeigt. (Noch nicht gespeichert)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_AddNew(sender As Object, e As EventArgs)
        Debug.Print("Article_DockingExtension AddNew")
    End Sub

    ''' <summary>
    ''' Es wurde ein Obejkt gefunden (F") und wird nun angezeigt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_Found(sender As Object, e As EventArgs)
        Debug.Print("Article_DockingExtension Found")
    End Sub

    ''' <summary>
    ''' Das Objekt wurde geändert und soll gespeichert werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_BeforeUpdate(sender As Object, e As EventArgs)
        Debug.Print("Article_DockingExtension BeforeUpdate")
    End Sub

    ''' <summary>
    ''' Das Objekt wird gespeichert
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_Updated(sender As Object, e As EventArgs)
        Debug.Print("Article_DockingExtension Updated")
    End Sub

    ''' <summary>
    ''' Das Objekt ist gespeichert worden. Transaktion abgeschlossen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_Committed(sender As Object, e As EventArgs)
        Debug.Print("Article_DockingExtension Committed")

        If GetKomponentenDaten() Then
            Debug.Print("WinBack Artikel")
            'geänderten Datensatz in WinBack-DB schreiben
            Komponente.MySQLdbWrite(Komponente.Nr)
        End If
    End Sub

    ''' <summary>
    ''' Das Objekt soll gelöscht werden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_BeforeDelete(sender As Object, e As EventArgs)
        Debug.Print("Article_DockingExtension BeforeDelete")
    End Sub

    ''' <summary>
    ''' Das Objekt ist gelöscht worden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_Deleted(sender As Object, e As EventArgs)
        Debug.Print("Article_DockingExtension Deleted")
    End Sub

    ''' <summary>
    ''' Das Objekt soll kopiert werden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_BeforeCopy(sender As Object, e As EventArgs)
        Debug.Print("Article_DockingExtension BeforeCopy")
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
        _SubForms.Add("@wb_Article_DockingControlObjectInfo", Nothing)
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
        End If
    End Sub

    ''' <summary>
    ''' Liefert zu einem FormKey eine Instanz des UserControls zurück
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

    ''' <summary>
    ''' Ermittelt alle im _Extendee enthaltenen Daten, die für die Kopplung der Komponentendaten zu WinBack
    ''' notwendig sind.
    ''' Ob die Daten für WinBack relevant sind, wird durch die Angabe des Sortiments bestimmt:
    '''     Ist der Artikel einem Sortiment zugeordnet, das eine Filiale vom Typ Produktion besitzt, werden die Daten
    '''     ausgewertet und es wird True zurückgegeben.
    ''' 
    '''     WinBack         Bezeichung                  OrgaBack
    '''     =======         ==========                  ========
    '''     KO_Nr           Index Rohstoff/Artikel      MFF280
    '''     KO_Bezeichnung  Artikel-Bezeichnung         MFF224
    '''     KO_Kommentar    Artikel-Kommentar           MFF225
    '''     KO_Nr_AlNum     Artikel-Nummer              .ArtikelNr
    '''     KA_Matchcode    ID für Nährwerte            MFF281(?)
    '''     KA_Grp1         Artikelgruppe 1
    '''     KA_Grp2         Artikelgruppe 2
    ''' 
    '''     200.7           Haltbarkeit                 MFF102
    '''     200.8           Lagerung                    MFF103
    '''     200.9           Verkaufstage                MFF104
    '''     200.17          Warengruppe
    '''     200.20          Stk/Karton                  VPE
    '''     
    '''     Hinweise2(3/0)  Hinweise Artikel            MFF208
    '''     
    ''' </summary>
    Private Function GetKomponentenDaten() As Boolean

        'Sortiment-Kürzel aus Artikel.Sortiment
        Dim sSortiment As String = _Extendee.GetPropertyValue("Sortiment").ToString
        If wb_Filiale.SortimentIstProduktion(sSortiment) Then

            'Filiale mit Index(0) ist die Hauptfiliale aus Artikel.FilialFeld()
            Dim oFil = DirectCast(_Extendee.GetPropertyValue("FilialFelder"), ICollectionClass).InnerList.Cast(Of ICollectionSubClass).ElementAt(0)
            'Komponenten-Nummer aus OrgaBack ermitteln
            Komponente.Nr = CInt(GetMMFValue(oFil, wb_Global.MFF_KO_Nr))            'MFF280 - Index auf interne Komponenten-Nummer
            Komponente.Nummer = _Extendee.GetPropertyValue("ArtikelNr").ToString    'Artikel/Komponenten-Nummer alphanumerisch
            'Artikel/Komponente aus WinBack-Db einlesen
            Komponente.MySQLdbRead(Komponente.Nr, Komponente.Nummer)

            'Update aller in OrgaBack geänderten Daten
            Komponente.Bezeichung = _Extendee.GetPropertyValue("KurzText").ToString 'Artikel/Komponenten-Bezeichnung
            Komponente.MatchCode = GetMMFValue(oFil, wb_Global.MFF_MatchCode)       'MFF281 - MatchCode
            Komponente.ZutatenListe = GetMMFValue(oFil, wb_Global.MFF_Zutatenliste) 'MFF209 - Zutatenliste 


            Debug.Print("Artikelnummer(alpha)   " & Komponente.Nummer)
            Debug.Print("Artikel-Bezeichnung    " & Komponente.Bezeichung)
            Debug.Print("ZutatenListe           " & Komponente.ZutatenListe)
            Debug.Print("Index                  " & Komponente.Nr)
            Debug.Print("MatchCode              " & Komponente.MatchCode)

            Return True
        Else
            'Artikel/Rohstoff ist keiner Produktions-Filiale zugeordnet
            Return False

        End If
    End Function

    ''' <summary>
    ''' Gibt den Wert des Multifunktions-Feldes mit der übergebenen Nummer zurück. Der Wert steht als Property-Array an der dritten Stellen
    '''     PropertyValue(0) - ArtikelNummer
    '''     PropertyValue(1) - FelddNr
    '''     PropertyValue(2) - FilialNr
    '''     PropertyValue(3) - Inhalt
    '''     PropertyValue(4) - Bezeichnung
    '''     PropertyValue(5) - Filialspezifisch (True/False)
    '''
    ''' ''' Die Multifunktions-Felder, die WinBack betreffend sind per Definition nicht filialspezisch    
    ''' </summary>
    ''' <param name="ofil">ICollectionSubClass - Daten aus der Filiale 0 (Hauptfiliale)</param>
    ''' <param name="MFF">Short - Indes auf MFF-Feld</param>
    ''' <returns></returns>
    Private Function GetMMFValue(ofil As ICollectionSubClass, MFF As Integer) As String
        Dim iMFFIdx As Short = Short.MinValue         ' hier soll der Index eines Multifunktionsfelds hinein
        Dim oMFF As ICollectionSubClass = Nothing     ' hier wird das eigentliche MFF-Objekt gehalten

        iMFFIdx = DirectCast(ofil.GetPropertyValue("MultiFunktionsFeld"), ICollectionClass).FindInInnerList("MFF=" & CStr(MFF))
        If iMFFIdx >= 0 Then
            ' sollte ein MFF mit FeldNr=X gefunden worden sein, so wurde dessen Index innerhalb der Collection zurückgeliefert
            ' mit diesem Index greift man auf das Element zu, die Elemente innerhalb einer ICollectionClass sind vom Typ ICollectionSubClass
            oMFF = DirectCast(ofil.GetPropertyValue("MultiFunktionsFeld"), ICollectionClass).InnerList.Cast(Of ICollectionSubClass).ElementAt(iMFFIdx)
            If oMFF IsNot Nothing Then
                ' sofern oMFF nicht Nothing ist, hat hat man jetzt direkten Zugriff auf das MFF mit FeldNr x
                Return oMFF.PropertyValueCollection(3).Value
            End If
        Else
            Return ""
        End If
        Return ""
    End Function

End Class