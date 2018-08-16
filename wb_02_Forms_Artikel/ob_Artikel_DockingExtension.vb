Imports System.ComponentModel
Imports System.Windows.Forms
Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.GUI

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Erweiterung des Artikel-Dockingfensters um ein Unterfenster WinBack. Synchronisation Artikeldaten bei Änderung in OrgaBack")>
Public Class ob_Artikel_DockingExtension
    Implements IDockingExtension

    Private _MenuService As IMenuService
    Private _ViewProvider As IViewProvider
    Private _ContextTabs As List(Of GUI.ITab)
    Private _SubForms As New Dictionary(Of String, IBasicFormUserControl)
    Private _Extendee As IFrameWorkClass

    Private bContextTabInitialized As Boolean = False
    Private bSortimentIstProduktion As Boolean = False
    Private bAddNew As Boolean = False

    Private Komponente As wb_Komponente
    Public Property InfoContainer As IInfoContainer Implements IExtension.InfoContainer
    Public Property ServiceProvider As IOrgasoftServiceProvider Implements IExtension.ServiceProvider

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
    ''' Klasse, deren Docking-Layout erweitert werden soll (Artikel)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ExtendedType As ObjectEnum Implements IDockingExtension.ExtendedType
        Get
            Return ObjectEnum.Articles
        End Get
    End Property

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

            'TEST BROTMACHER-FEHLER
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

    Private Sub Extendee_ExecuteCommand(Command As String, Parameter As Object)
        For Each oForm In _SubForms.Values
            If oForm IsNot Nothing AndAlso Not DirectCast(oForm, UserControl).IsDisposed Then
                Debug.Print("Extendee_ExecuteCommand " & Command)
                oForm.ExecuteCommand(Command, Parameter)
            End If
        Next
    End Sub

    Private Sub Extendee_Valid(sender As Object, e As EventArgs)
        Extendee_ExecuteCommand("VALID", Nothing)
        Debug.Print("Article_DockingExtension Valid")
    End Sub

    ''' <summary>
    ''' Das Objekt hat derzeit keinen gültigen Status.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_Invalid(sender As Object, e As EventArgs)
        bAddNew = False
        Extendee_ExecuteCommand("INVALID", Nothing)
        Debug.Print("Article_DockingExtension Invalid")
        Komponente = New wb_Komponente
    End Sub

    ''' <summary>
    ''' Es wird ein neues, leeres Objekt erzeugt und angezeigt. (Noch nicht gespeichert)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_AddNew(sender As Object, e As EventArgs)
        Komponente = New wb_Komponente
        bAddNew = True
        Extendee_ExecuteCommand("INVALID", Nothing)
        Debug.Print("Article_DockingExtension AddNew")
    End Sub

    ''' <summary>
    ''' Es wurde ein Objekt gefunden (F") und wird nun angezeigt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_Found(sender As Object, e As EventArgs)
        Debug.Print("Article_DockingExtension Found")
        'Artikel-Informationen in Klasse Komponenten einlesen
        bSortimentIstProduktion = GetKomponentenDaten()
        'Sub-Fenster WinBack aktualisieren
        Debug.Print("Article_DockingExtension Found")
        If bSortimentIstProduktion Then
            Debug.Print("Article_DockingExtension Found - Sortiment ist Produktion")
            Extendee_ExecuteCommand("wbFOUND", Komponente)
        End If

    End Sub

    ''' <summary>
    ''' Das Objekt wurde geändert und soll gespeichert werden. 
    ''' Vor dem Speichern wird gepürft, ob die Einheit in OrgaBack richtig ausgewählt worden ist:
    '''     
    '''     - Artikel in Stück
    '''     - Rohstoffe in kg
    '''     - Rohstoffe(Erweiterung) in Meter(Folie) oder Stk (Verpackung) 
    '''
    '''     .Cancel = True    Speichern nicht erlaubt
    '''     .Cancel = False   Speichern erlaubt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_BeforeUpdate(sender As Object, e As EventArgs)
        Debug.Print("Article_DockingExtension BeforeUpdate")
        'Artikel-Informationen (erneut)in Klasse Komponenten einlesen. Wenn der Artikel/Rohstoff neu angelegt wurde müssen die Daten neu eingelesen werden
        If bAddNew Then
            bSortimentIstProduktion = GetKomponentenDaten()
        End If
        If bSortimentIstProduktion Then
            'Prüfen ob die Einheit aus OrgaBack gültig ist
            Dim obKType As String = _Extendee.GetPropertyValue("ArtikelGruppe").ToString
            Dim obEinheit As String = _Extendee.GetPropertyValue("BestellEinheit").ToString
            If CheckEinheit(obKType, obEinheit) Then
                'Daten aus Unterfenster sichern
                Extendee_ExecuteCommand("wbSAVE", Komponente)
                'Komponentendaten nach OrgaBack schreiben (MFF..)
                SetKomponentenDaten()
                'Speichern erlaubt
                DirectCast(e, CancelEventArgs).Cancel = False
            Else
                'Speichern nicht erlaubt
                DirectCast(e, CancelEventArgs).Cancel = True
            End If
        End If
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

        If bSortimentIstProduktion Then
            Debug.Print("WinBack Artikel")
            UpdateKomponentenDaten()
            'Komponentendaten in Datenbank sichern
            Komponente.UpdateDB()
        End If
    End Sub

    ''' <summary>
    ''' Das Objekt soll gelöscht werden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_BeforeDelete(sender As Object, e As EventArgs)
        Dim CanBeDeleted As Boolean = True

        'Hier kann das Löschen von Artikel/Komponenten-Daten verhindert werden  
        Debug.Print("Article_DockingExtension BeforeDelete")

        'Sortiment-Kürzel aus Artikel.Sortiment
        If bSortimentIstProduktion Then

            'Filiale mit Index(0) ist die Hauptfiliale aus Artikel.FilialFeld()
            Dim oFil = DirectCast(_Extendee.GetPropertyValue("FilialFelder"), ICollectionClass).InnerList.Cast(Of ICollectionSubClass).ElementAt(0)
            'Komponenten-Nummer aus OrgaBack ermitteln
            Komponente.Nr = wb_Functions.StrToInt(MFFValue(oFil, wb_Global.MFF_KO_Nr))  'MFF280 - Index auf interne Komponenten-Nummer
            Komponente.Nummer = _Extendee.GetPropertyValue("ArtikelNr").ToString        'Artikel/Komponenten-Nummer alphanumerisch

            'Prüfen ob der Artikel/Rohstoff noch verwendet wird
            CanBeDeleted = Komponente.MySQLdbCanBeDeleted(Komponente.Nr, Komponente.Nummer)
            'Falls notwendig wird ein Fehlertext ausgegeben
            If Not CanBeDeleted Then
                MsgBox(Komponente.LastErrorText)
                Debug.Print("Article_DockingExtension " & Komponente.Nummer & " Can Not be deleted " & Komponente.LastErrorText)
            End If
        End If
        '     .Cancel = True    Löschen nicht erlaubt
        '     .Cancel = False   Löschen erlaubt
        DirectCast(e, CancelEventArgs).Cancel = Not CanBeDeleted
    End Sub

    ''' <summary>
    ''' Das Objekt ist gelöscht worden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_Deleted(sender As Object, e As EventArgs)
        Debug.Print("Article_DockingExtension Deleted")

        'Sortiment-Kürzel aus Artikel.Sortiment
        If bSortimentIstProduktion Then
            'Komponente endgültig löschen - Die Komponenten-Nummer ist vorab schon ermittelt worden (BeforeDelete)
            Komponente.MySQLdbDelete()
        End If
    End Sub

    ''' <summary>
    ''' Das Objekt soll kopiert werden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Extendee_BeforeCopy(sender As Object, e As EventArgs)
        Debug.Print("Article_DockingExtension BeforeCopy")
    End Sub

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
        _SubForms.Add("@ob_ArtikelDocking_ZuordnungRezept", Nothing)
        _SubForms.Add("@ob_ArtikelDocking_VerwendungRezept", Nothing)

        'Neuinitialisierung der AddIn-Buttons beim Benutzerwechsel
        Dim IEB As IEventBroker = TryCast(ServiceProvider.GetService(GetType(IEventBroker)), IEventBroker)
        If IEB IsNot Nothing Then
            AddHandler IEB.LoginChanged, Sub() bContextTabInitialized = False
        End If
    End Sub

    ''' <summary>
    ''' Diese Routine wird immer aufgerufen, wenn ein DockingController vom passenden Typ erzeugt wird. 
    ''' Hier können Einträge in die bestehenden Context-Tabs hinzugefügt werden. 
    ''' Achtung: Das Hinzufügen darf nur beim ersten Mal passieren, die Context-Tabs werden gecached!
    ''' </summary>
    Public Sub InitializeContextTabs() Implements IDockingExtension.InitializeContextTabs
        If Not bContextTabInitialized Then
            'einmalige Ausführung sicherstellen
            bContextTabInitialized = True
            'fügt einen Tab im Artkel-Ribbon(rtabArtikel) hinzu
            Dim oSystemTab = From oTab In Me.FormController.ContextualTabs Where oTab.Name = "rtabArtikel" Select oTab
            If oSystemTab IsNot Nothing AndAlso oSystemTab.Count > 0 Then
                oSystemTab(0).GetGroups(0).AddButton("WinBackProduktion", "WinBack-Produktion", "Artikel-Stammdaten relevant für die Produktion in WinBack", My.Resources.MainArtikel_16x16, My.Resources.MainArtikel_32x32, AddressOf LoadDockingSubFormRezept)
                oSystemTab(0).GetGroups(0).AddButton("WinBackVerwendung", "Artikel Verwendung", "Verwendung des Artikels in Rezepturen", My.Resources.RohstoffeVerwendung_32x32, My.Resources.RohstoffeVerwendung_32x32, AddressOf LoadDockingSubFormVerwendung)
            End If
        End If
    End Sub

    Private Sub LoadDockingSubFormRezept(sender As Object, e As EventArgs)
        Trace.WriteLine("LoadDockingSubFormRezept")
        If Me.FormController IsNot Nothing Then
            Me.FormController.ExecuteCommand("ob_ArtikelDocking_ZuordnungRezept", Nothing)
            Trace.WriteLine("Me.FormController.ExecuteCommand")

            'Sub-Fenster WinBack aktualisieren, falls das Fenster geöffnet wird, nachdem der Artikel ausgewählt wurde
            If bSortimentIstProduktion And Komponente IsNot Nothing Then
                Extendee_ExecuteCommand("wbFOUND", Komponente)
            End If
        End If
    End Sub

    Private Sub LoadDockingSubFormVerwendung(sender As Object, e As EventArgs)
        Trace.WriteLine("LoadDockingSubFormVerwendung")
        If Me.FormController IsNot Nothing Then
            Me.FormController.ExecuteCommand("ob_ArtikelDocking_VerwendungRezept", Nothing)
            Trace.WriteLine("Me.FormController.ExecuteCommand")

            'Sub-Fenster WinBack aktualisieren, falls das Fenster geöffnet wird, nachdem der Rohstoff ausgewählt wurde
            If bSortimentIstProduktion And Komponente IsNot Nothing Then
                Extendee_ExecuteCommand("wbFOUND", Komponente)
            End If
        End If
    End Sub

    Private Sub SaveAtClose()
        Debug.Print("SaveAtClose")
        _Extendee.Changed = True
    End Sub

    ''' <summary>
    ''' Liefert zu einem FormKey eine Instanz des UserControls zurück
    ''' </summary>
    ''' <param name="FormKey"></param>
    ''' <returns></returns>
    Public Function ProvideInstance(FormKey As String) As IBasicFormUserControl Implements IDockingExtension.ProvideInstance
        Trace.WriteLine("ProvideInstance FormKey= " & FormKey)
        If _SubForms.ContainsKey(FormKey) Then
            Trace.WriteLine("_SubForms.ContainsFormKey ")
            Select Case FormKey
                Case "@ob_ArtikelDocking_ZuordnungRezept"
                    Dim oForm = _SubForms(FormKey)
                    If oForm Is Nothing OrElse DirectCast(oForm, UserControl).IsDisposed Then
                        ' Adresse der Klasse, die die Arbeit macht !!
                        oForm = New ob_Artikel_ZuordnungRezept(Me)
                        AddHandler DirectCast(oForm, ob_Artikel_ZuordnungRezept).DataInvalidated, AddressOf SaveAtClose
                        _SubForms(FormKey) = oForm
                    End If
                    Return oForm

                Case "@ob_ArtikelDocking_VerwendungRezept"
                    Dim oForm = _SubForms(FormKey)
                    If oForm Is Nothing OrElse DirectCast(oForm, UserControl).IsDisposed Then
                        ' Adresse der Klasse, die die Arbeit macht !!
                        oForm = New ob_Artikel_VerwendungRezept(Me)
                        _SubForms(FormKey) = oForm
                    End If
                    Return oForm

            End Select
        End If
        Return Nothing
    End Function

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
    '''     WinBack         Bezeichung                      OrgaBack
    '''     =======         ==========                      ========
    '''     KO_Nr_AlNum     Artikel-Nummer                  dbo.Artikel.ArtikelNr
    '''     KO_Bezeichnung  Artikel-Bezeichnung             dbo.Artikel.KurzText
    '''     KO_Type           0 - Artikel                   .Artikelgruppe =  0 (Gruppe Backwaren aus winback.ini)
    '''                     102 - Rohstoff                  .Artikelgruppe = 40 (Gruppe Rohstoffe aus winback.ini)
    '''     KO_Kommentar    Artikel-Kommentar               MFF156
    '''     
    '''     KA_Matchcode    ID für Nährwerte            
    '''     Aufarbeitung(Backort)                           MFF200 (OrgaBack Readonly)
    '''     KO_Nr           Index Rohstoff/Artikel          MFF201
    '''     KA_RZ_Nr        Rezeptnummer zum Artikel        MFF202 (WinBack schreibt)
    '''     RZ_Bezeichnung  Rezeptbezeichnung zum Artikel   MFF203 (WinBack schreibt)
    ''' 
    '''     Hinweise2(03/0) Hinweise Artikel                nur WinBack
    '''     Hinweise2(09/1) Zutatenliste Artikel            dbo.ArtikelDeklarationsTexte.Zutaten (WinBack schreibt)
    '''     Hinweise2(09/2) Mehlzusammensetzung             MFF210 (WinBack schreibt)
    ''' 
    '''     In WinBack nicht verwendet
    '''     ==========================
    '''     200.2           Dateiname Bild                  .ArtikelBildDateiname
    '''     200.3           Kurztext                        .Kurztext
    '''     200.7           Haltbarkeit                 
    '''     200.8           Lagerung                        MFF151 nur OrgaBack - keine Synchr.
    '''     200.9           Verkaufstage                    MFF150 nur OrgaBack - keine Synchr.
    '''     200.17          Warengruppe
    '''     200.20          Stk/Karton                  
    '''     
    '''     Hinweise2(10/1) Gebäck-Charakteristik           MFF152 nur OrgaBack - keine Synchr.
    '''     Hinweise2(10/2) Verzehr-Tipps                   MFF153 nur OrgaBack - keine Synchr.
    '''     Hinweise2(10/3) Wissenswertes                   MFF154 nur OrgaBack - keine Synchr.
    '''     
    ''' </summary>
    Private Function GetKomponentenDaten() As Boolean

        'Sortiment-Kürzel aus Artikel.Sortiment
        Dim sSortiment As String = _Extendee.GetPropertyValue("Sortiment").ToString
        Debug.Print("DockingExtension-GetKomponentenDaten sSortiment " & sSortiment)
        If wb_Filiale.SortimentIstProduktion(sSortiment) Then
            Debug.Print("DockingExtension-GetKomponentenDaten sSortiment - Sortiment " & sSortiment & " ist Produktion")

            'Filiale mit Index(0) ist die Hauptfiliale aus Artikel.FilialFeld()
            Dim oFil = DirectCast(_Extendee.GetPropertyValue("FilialFelder"), ICollectionClass).InnerList.Cast(Of ICollectionSubClass).ElementAt(0)
            'Komponenten-Nummer aus OrgaBack ermitteln
            If Komponente Is Nothing Then Komponente = New wb_Komponente
            Komponente.Nr = wb_Functions.StrToInt(MFFValue(oFil, wb_Global.MFF_KO_Nr))   'MFF201 - Index auf interne Komponenten-Nummer
            Debug.Print("DockingExtension-GetKomponentenDaten KomponenteNr " & Komponente.Nr.ToString)
            Komponente.Nummer = _Extendee.GetPropertyValue("ArtikelNr").ToString         'Artikel/Komponenten-Nummer alphanumerisch
            Debug.Print("DockingExtension-GetKomponentenDaten KomponenteNummer " & Komponente.Nummer.ToString)
            Komponente.sArtikeLinienGruppe = MFFValue(oFil, wb_Global.MFF_ProduktionsLinie)
            'Artikel/Komponente aus WinBack-Db einlesen
            Dim obKType As String = _Extendee.GetPropertyValue("ArtikelGruppe").ToString

            If Not Komponente.MySQLdbRead(Komponente.Nr, Komponente.Nummer) Then
                Debug.Print("DockingExtension-GetKomponentenDaten Komponente in WinBack nicht vorhanden " & Komponente.Bezeichnung)
                'Datensatz ist in Winback nicht vorhanden - Komponententype (Artikel/Handkomponente) ermitteln
                Dim KType As wb_Global.KomponTypen = wb_Functions.obKtypeToKType(obKType)
                'Datensatz in WinBack neu anlegen
                Komponente.MySQLdbNew(KType)
            End If
            Debug.Print("DockingExtension-GetKomponentenDaten Komponente in WinBack (jetzt) vorhanden " & Komponente.Bezeichnung)
            Return True
        Else
            'Artikel/Rohstoff ist keiner Produktions-Filiale zugeordnet
            Debug.Print("DockingExtension-GetKomponentenDaten Komponente keiner ProduktionsFiliale zugeordnet" & Komponente.Bezeichnung)
            Return False
        End If
    End Function

    ''' <summary>
    ''' Setzt oder gibt den Wert des Multifunktions-Feldes mit der übergebenen Nummer zurück. Der Wert steht als Property-Array an der dritten Stellen
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
    Private Property MFFValue(ofil As ICollectionSubClass, MFF As Integer) As String
        Get
            Dim iMFFIdx As Short = Short.MinValue         ' hier soll der Index eines Multifunktionsfelds hinein
            Dim oMFF As ICollectionSubClass = Nothing     ' hier wird das eigentliche MFF-Objekt gehalten

            iMFFIdx = DirectCast(ofil.GetPropertyValue("MultiFunktionsFeld"), ICollectionClass).FindInInnerList("FeldNr=" & CStr(MFF))
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
        End Get

        Set(Value As String)
            Dim iMFFIdx As Short = Short.MinValue         ' hier soll der Index eines Multifunktionsfelds hinein
            Dim oMFF As ICollectionSubClass = Nothing     ' hier wird das eigentliche MFF-Objekt gehalten
            iMFFIdx = DirectCast(ofil.GetPropertyValue("MultiFunktionsFeld"), ICollectionClass).FindInInnerList("FeldNr=" & CStr(MFF))

            If iMFFIdx >= 0 Then
                ' sollte ein MFF mit FeldNr=X gefunden worden sein, so wurde dessen Index innerhalb der Collection zurückgeliefert
                ' mit diesem Index greift man auf das Element zu, die Elemente innerhalb einer ICollectionClass sind vom Typ ICollectionSubClass
                oMFF = DirectCast(ofil.GetPropertyValue("MultiFunktionsFeld"), ICollectionClass).InnerList.Cast(Of ICollectionSubClass).ElementAt(iMFFIdx)
                If oMFF IsNot Nothing Then
                    ' sofern oMFF nicht Nothing ist, hat hat man jetzt direkten Zugriff auf das MFF mit FeldNr x
                    oMFF.SetPropertyValue("Inhalt", Value)
                End If
            End If
        End Set
    End Property

    Private Sub UpdateKomponentenDaten()
        'Filiale mit Index(0) ist die Hauptfiliale aus Artikel.FilialFeld()
        Dim oFil = DirectCast(_Extendee.GetPropertyValue("FilialFelder"), ICollectionClass).InnerList.Cast(Of ICollectionSubClass).ElementAt(0)

        'Update aller in OrgaBack geänderten Daten
        Komponente.Nummer = _Extendee.GetPropertyValue("ArtikelNr").ToString     'Artikel-Nummer (Ändern über Shift-F9)
        Komponente.Bezeichnung = _Extendee.GetPropertyValue("KurzText").ToString 'Artikel/Komponenten-Bezeichnung
        Komponente.Kommentar = MFFValue(oFil, wb_Global.MFF_Kommentar)           'Artikel/Komponenten-Kommentar

        'Testausgabe
        Debug.Print("Artikelnummer(alpha)   " & Komponente.Nummer)
        Debug.Print("Artikel-Bezeichnung    " & Komponente.Bezeichnung)
        Debug.Print("Artikel-Kurzname       " & Komponente.Kurzname)
        Debug.Print("Index                  " & Komponente.Nr)
        Debug.Print("Komponenten-Type       " & wb_Functions.KomponTypeToInt(Komponente.Type).ToString)
        Debug.Print("ZutatenListe           " & Komponente.ZutatenListe)
        Debug.Print("MatchCode              " & Komponente.MatchCode)
    End Sub

    Private Sub SetKomponentenDaten()
        'Filiale mit Index(0) ist die Hauptfiliale aus Artikel.FilialFeld()
        Dim oFil = DirectCast(_Extendee.GetPropertyValue("FilialFelder"), ICollectionClass).InnerList.Cast(Of ICollectionSubClass).ElementAt(0)

        'Update aller in WinBack geänderten Daten
        MFFValue(oFil, wb_Global.MFF_RezeptNummer) = Komponente.RezeptNummer
        MFFValue(oFil, wb_Global.MFF_MehlZusammensetzung) = Komponente.Mehlzusammensetzung
        MFFValue(oFil, wb_Global.MFF_ProduktionsLinie) = Komponente.sArtikeLinienGruppe
    End Sub

    ''' <summary>
    ''' Prüft ob in OrgaBack die richtige Einheit zur Type verwendet wird
    ''' Der FehlerText wird als Komponente.LastError zurückgegeben.
    '''     
    ''' </summary>
    ''' <param name="obKType"></param>
    ''' <param name="obEinheit"></param>
    ''' <returns></returns>
    Private Function CheckEinheit(obKType As String, obEinheit As String) As Boolean
        'prüft die Einheit abhängig von der Komponenten-Type
        Select Case obKType
            'Artikel
            Case wb_GlobalSettings.OsGrpBackwaren
                If wb_Functions.StrToInt(obEinheit) = wb_Einheiten_Global.GetobEinheitNr(wb_Global.wbEinheitStk) Then
                    Return True
                Else
                    MsgBox("Speichern nicht möglich." & vbCrLf & "Ein WinBack-Artikel kann nur in der Einheit 'Stk' angelegt werden !")
                    Return False
                End If
            Case wb_GlobalSettings.OsGrpRohstoffe
                If wb_Functions.StrToInt(obEinheit) = wb_Einheiten_Global.GetobEinheitNr(wb_Global.wbEinheitKilogramm) Then
                    Return True
                Else
                    MsgBox("Speichern nicht möglich." & vbCrLf & "Ein WinBack-Rohstoff kann nur in der Einheit 'kg' angelegt werden !")
                    Return False
                End If
            Case Else
                Return True
        End Select
    End Function
End Class
