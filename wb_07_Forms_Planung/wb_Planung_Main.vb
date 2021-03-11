Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Planung_Main
    Implements IExternalFormUserControl

    'Default-Fenster
    Public PlanungListe As New wb_Planung_Liste
    'Fenster werden bei Bedarf erzeugt
    Public PlanungTeiler As wb_Planung_Teiler
    Public PlanungListeFehler As wb_Planung_ListeFehler
    Public PlanungDrucken As wb_Planung_Drucken

#Region "Signum"
    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        'Initialisierung
        MyBase.New(ServiceProvider)
        'Event-Handler (Fehlerliste aktualisieren/anzeigen)
        AddHandler wb_Planung_Shared.eListe_Refresh, AddressOf Refresh_Data
        'Default-Layout wenn keine Fenster angezeigt werden (neuer Benutzer...)
        If _DockPanelList.Count = 0 Then
            SetDefaultLayout()
        End If
    End Sub

    ''' <summary>
    ''' Fenster-Name (Caption). Wird von Init() aufgerufen
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormText As String
        Get
            Return "WinBack Produktions-Planung"
        End Get
    End Property

    ''' <summary>
    ''' Eindeutiger Name für die Basis-Form. 
    ''' Unter diesem Namen werden die Einstellungen in der winback.ini gespeichert.
    ''' 
    ''' Die DockPanel-Konfiguration wird gespeichert unter wbXXXXYYYY.xml, dabei ist XXXX der FormName und YYYY der Layout-Name.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormName As String
        Get
            Return "Produktion"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        PlanungListe.Show(DockPanel, DockState.DockTop)
    End Sub

    ''' <summary>
    ''' Diese Function wird aufgerufen, wenn das Fenster geschlossen werden soll.
    ''' </summary>
    ''' <param name="Reason"></param>
    ''' <returns>
    ''' False, wenn das Fenster geschlossen werden darf
    ''' True, wenn das Fenster geöffnet bleiben muss
    ''' </returns>
    ''' <remarks></remarks>
    Public Overrides Function FormClosing(Reason As Short) As Boolean Implements IBasicFormUserControl.FormClosing
        'Event-Handler wieder freigeben !!
        RemoveHandler wb_Planung_Shared.eListe_Refresh, AddressOf Refresh_Data

        'Planung-Liste (ordentlich) schliessen - Speichert die Grid-Einstellungen
        If PlanungListe IsNot Nothing Then
            'Verbuchte Datensätze in OrgaBack-DB registrieren
            If PlanungListe.FormClosingFromMain() Then
                Return True
            End If
            PlanungListe.Close()
        End If

        'Planung-Teiler schliessen
        wb_Functions.CloseAndDisposeSubForm(PlanungTeiler)

        'Fehlerliste schliessen
        wb_Functions.CloseAndDisposeSubForm(PlanungListeFehler)

        'Drucken schliessen
        wb_Functions.CloseAndDisposeSubForm(PlanungDrucken)

        'Fenster darf geschlossen werden
        Return False
    End Function

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("Teig-Herstellung", "Teig-Herstellung", "WinBack Teig-Herstellung")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpPlanung", "Teigliste/Backzettel")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnFehlerListe", "Fehlerliste", "Fehlerliste nach Import der Produktionsdaten anzeigen", My.Resources.ProdFehlerListe_32x32, My.Resources.ProdFehlerListe_32x32, AddressOf BtnProdPlanungError)
                oGrp.AddButton("BtnExportProdListen", "Export/Drucken", "Backzettel und Teigliste drucken. Produktionsplan an WinBack senden", My.Resources.ProdExport_32x32, My.Resources.ProdExport_32x32, AddressOf BtnProdListeExport)
                oGrp.AddButton("BtnProdTeiler", "Einstellungen Optimierung", "Einstellungen zur Optimierung der Teigliste", My.Resources.ProdTeiler_32x32, My.Resources.ProdTeiler_32x32, AddressOf BtnPlanungTeiler)
                'Gruppe Linien
                Dim lGrp = oNewTab.AddGroup("GrpLinien", "WinBack Linien")
                lGrp.AddButton("btnLinien", "Produktions-Linien", "WinBack Produktion Linie 1...", My.Resources.MainLinien_32x32, My.Resources.MainLinien_32x32, AddressOf ShowLinienForm)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString

            Case "WinBack.wb_Planung_Liste"
                PlanungListe.CloseButtonVisible = False
                _DockPanelList.Add(PlanungListe)
                Return PlanungListe

            Case "WinBack.wb_Planung_ListeFehler"
                PlanungListeFehler = New wb_Planung_ListeFehler
                _DockPanelList.Add(PlanungListeFehler)
                Return PlanungListeFehler

            Case "WinBack.wb_Planung_Teiler"
                PlanungTeiler = New wb_Planung_Teiler
                _DockPanelList.Add(PlanungTeiler)
                Return PlanungTeiler

            Case "WinBack.wb_Planung_Drucken"
                PlanungDrucken = New wb_Planung_Drucken
                _DockPanelList.Add(PlanungDrucken)
                Return PlanungDrucken
            Case Else
                Return Nothing
        End Select
    End Function
#End Region

    Public Overrides Function Init() As Boolean Implements IBasicFormUserControl.Init
        'Init aus der Basis-Klasse aufrufen (zuerst)
        Init = MyBase.Init()
    End Function

    Private Sub Refresh_Data(Sender As Object)
        If IsNothingOrDisposed(PlanungListeFehler) Then
            PlanungListeFehler = New wb_Planung_ListeFehler
            '            PlanungListeFehler.Show(DockPanel, DockState.DockLeft)
            PlanungListeFehler.RefreshData(Nothing)
            PlanungListeFehler.ShowDialog()
        Else
            PlanungListeFehler.RefreshData(Nothing)
        End If
    End Sub

    Private Sub BtnProdPlanungError()
        If IsNothingOrDisposed(PlanungListeFehler) Then
            PlanungListeFehler = New wb_Planung_ListeFehler
        End If
        PlanungListeFehler.Show(DockPanel)
    End Sub

    Private Sub BtnProdListeExport()
        If IsNothingOrDisposed(PlanungDrucken) Then
            PlanungDrucken = New wb_Planung_Drucken
        End If
        PlanungDrucken.Show(DockPanel)
    End Sub

    Private Sub BtnPlanungTeiler()
        If IsNothingOrDisposed(PlanungTeiler) Then
            PlanungTeiler = New wb_Planung_Teiler
        End If
        PlanungTeiler.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub ShowLinienForm()
        wb_Main_Shared.OpenForm(Me, "Linien")
    End Sub

End Class
