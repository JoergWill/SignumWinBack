Imports System.Windows.Forms
Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rezept_Main
    Implements IExternalFormUserControl

    'Default Fenster
    Public RezeptListe As New wb_Rezept_Liste
    Public RezeptDetails As New wb_Rezept_Details

    'alle anderen Fenster werden zur Laufzeit erzeugt
    Public RezeptHinweise As wb_Rezept_Hinweise
    Public RezeptHistorie As wb_Rezept_Historie
    Public RezeptVerwendung As wb_Rezept_Verwendung

    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        'Initialisierung
        MyBase.New(ServiceProvider)
        'Default-Layout wenn keine Fenster angezeigt werden (neuer Benutzer...)
        If _DockPanelList.Count = 0 Then
            SetDefaultLayout()
        End If
        'Event-Handler (Klick auf Rezept-Kopieren -> Rezept-Neu)
        AddHandler wb_Rezept_Shared.eRezept_Copy, AddressOf RezeptCopy
    End Sub

    ''' <summary>
    ''' Fenster-Name (Caption). Wird von Init() aufgerufen
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormText As String
        Get
            Return "WinBack Rezeptverwaltung"
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
            Return "Rezepte"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        RezeptDetails.Show(DockPanel, DockState.DockTop)
        RezeptDetails.CloseButtonVisible = False
        RezeptListe.Show(DockPanel, DockState.DockLeft)
        RezeptListe.CloseButtonVisible = False
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
        'alle erzeugten Fenster wieder schliessen
        wb_Functions.CloseAndDisposeSubForm(RezeptListe)
        wb_Functions.CloseAndDisposeSubForm(RezeptDetails)
        wb_Functions.CloseAndDisposeSubForm(RezeptHinweise)
        wb_Functions.CloseAndDisposeSubForm(RezeptHistorie)
        wb_Functions.CloseAndDisposeSubForm(RezeptVerwendung)

        'alle Spuren in Rezepte_Shared löschen
        wb_Rezept_Shared.Invalid()

        'Fenster darf geschlossen werden
        Return False
    End Function

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("RezeptVerwaltung", "WinBack-Rezepte", "Verwaltung der WinBack-Rezepturen")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrpStammdaten = oNewTab.AddGroup("GrpWinBack", "WinBack")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrpStammdaten.AddButton("btnArtikelStamm", "Artikel", "WinBack Artikelstammdaten", My.Resources.MainArtikel_16x16, My.Resources.MainArtikel_32x32, AddressOf ShowArtikelForm)
                oGrpStammdaten.AddButton("btnRohstoffStamm", "Rohstoffe", "WinBack Rohstoff-Stammdaten", My.Resources.MainRohstoffe_16x16, My.Resources.MainRohstoffe_32x32, AddressOf ShowRohstoffForm)
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrpRzpt = oNewTab.AddGroup("Verwaltung", "Rezepte")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrpRzpt.AddButton("BtnRezeptNeu", "Neu anlegen", "Leeres Rezept neu anlegen", My.Resources.RezeptNeu_32x32, My.Resources.RezeptNeu_32x32, AddressOf BtnRezeptNew)
                oGrpRzpt.AddButton("BtnRezeptRemove", "Rezept(e) löschen", "Rezept/Rezepte löschen", My.Resources.RezeptLoeschen_32x32, My.Resources.RezeptLoeschen_32x32, AddressOf BtnRezeptRemove)
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpRezepte", "Ansicht")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnRezeptListe", "Rezepte Liste", "Liste aller Rezepte anzeigen", My.Resources.RezeptListe_32x32, My.Resources.RezeptListe_32x32, AddressOf BtnRezeptListe)
                oGrp.AddButton("BtnRezeptDetails", "Rezept Details", "Fenster Rezept-Details", My.Resources.RezeptDetails_32x32, My.Resources.RezeptDetails_32x32, AddressOf BtnRezeptDetails)
                oGrp.AddButton("BtnRezeptHinweise", "Rezept Hinweise", "Rezept Verarbeitungshinweise", My.Resources.RezeptHinweise_32x32, My.Resources.RezeptHinweise_32x32, AddressOf BtnRezeptHinweise)
                oGrp.AddButton("BtnRezeptHistorie", "Rezept Historie", "Anzeige der Änderungshistorie der Rezeptur", My.Resources.RezeptHistorie_32x32, My.Resources.RezeptHistorie_32x32, AddressOf BtnRezeptHistorie)
                oGrp.AddButton("BtnRezeptVerwendung", "Rezept Verwendung", "Liste aller Artikel mit Bezug zu diesem Rezept", My.Resources.RezeptVerwendung_32x32, My.Resources.RezeptVerwendung_32x32, AddressOf BtnRezeptVerwendung)
                Dim oGrpPrnt = oNewTab.AddGroup("Printer", "Drucken")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrpPrnt.AddButton("BtnRezeptListeDrucken", "Drucke Rezeptliste", "Liste aller Rezepte drucken", My.Resources.RezeptDruckenListe_32x32, My.Resources.RezeptDruckenListe_32x32, AddressOf BtnRezeptListeDrucken)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Private Sub ShowArtikelForm()
        wb_Main_Shared.OpenForm(Me, "Artikel")
    End Sub

    Private Sub ShowRohstoffForm()
        wb_Main_Shared.OpenForm(Me, "Rohstoffe")
    End Sub

    ''' <summary>
    ''' Das Kopieren einer Rezeptur erfolgt über die Funktion Rezept-Neu.
    ''' Die übergebene Rezept-Nummer wird kopiert.
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="RzNr"></param>
    Private Sub RezeptCopy(Sender As Object, RzNr As Integer, Variante As Integer)
        RezeptNew(RzNr, Variante)
    End Sub

    ''' <summary>
    ''' Neues Rezept anlegen. Wenn keine Rezeptnummer angegeben ist, wird nur eine leere Hülle erzeugt. Ist eine Rezeptnummer angegeben,
    ''' wird eine Kopie des aktuellen Rezeptes erzeugt und angezeigt.
    ''' </summary>
    ''' <param name="RzNr"></param>
    Private Sub RezeptNew(Optional RzNr As Integer = wb_Global.UNDEFINED, Optional RzVariante As Integer = 1)
        Dim Rezept As New wb_Rezept
        Dim RezeptNrNeu As Integer = Rezept.MySQLdbNew(wb_Global.LinienGruppeStandard)
        RezeptListe.RefreshData(RezeptNrNeu)

        'Rezeptur kopieren
        If RzNr > wb_Global.UNDEFINED Then
            'Rezeptschritte kopieren
            Rezept.Copy(RzNr, RzVariante)
        End If

        'Speicher wieder freigeben
        Rezept = Nothing

        'Das neu erzeugte Rezept gleich öffnen
        Me.Cursor = Cursors.WaitCursor
        'Beim Erzeugen des Fensters werden die Daten aus der Datenbank gelesen
        Dim Rezeptur As New wb_Rezept_Rezeptur(RezeptNrNeu, wb_Global.RezeptVarianteStandard)
        Rezeptur.Show()
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Neues (leeres) Rezept anlegen
    ''' </summary>
    Private Sub BtnRezeptNew()
        RezeptNew()
    End Sub

    Private Sub BtnRezeptRemove()
        Dim Rezeptur As New wb_Rezept_Rezeptur(wb_Rezept_Shared.Rezept.RezeptNr, 1)
        Rezeptur.RzptLoeschen()
    End Sub

    Private Sub BtnRezeptListe()
        RezeptListe.Show(DockPanel, DockState.DockLeft)
    End Sub

    Private Sub BtnRezeptDetails()
        RezeptDetails.Show(DockPanel, DockState.Document)
    End Sub

    Private Sub BtnRezeptHinweise()
        If IsNothingOrDisposed(RezeptHinweise) Then
            RezeptHinweise = New wb_Rezept_Hinweise
        End If
        RezeptHinweise.Show(DockPanel, DockState.Document)
    End Sub

    Private Sub BtnRezeptHistorie()
        If IsNothingOrDisposed(RezeptHistorie) Then
            RezeptHistorie = New wb_Rezept_Historie
        End If
        RezeptHistorie.Show(DockPanel, DockState.Document)
    End Sub

    Private Sub BtnRezeptVerwendung()
        If IsNothingOrDisposed(RezeptVerwendung) Then
            RezeptVerwendung = New wb_Rezept_Verwendung
        End If
        RezeptVerwendung.Show(DockPanel, DockState.Document)
    End Sub

    Private Sub BtnRezeptListeDrucken()
        'sicherheitshalber abfragen
        If Not IsNothing(RezeptListe) Then

            'Druck-Daten
            Dim pDialog As New wb_PrinterDialog(False) 'Drucker-Dialog
            pDialog.LL_KopfZeile_1 = RezeptListe.FilterText

            'Liste aller Rohstoffe aus den DataGridView
            pDialog.LL.DataSource = New combit.ListLabel22.DataProviders.AdoDataProvider(RezeptListe.DataGridView.LLData)

            'List und Label-Verzeichnis für die Listen
            pDialog.ListSubDirectory = "Rezepte"
            pDialog.ListFileName = "RezeptListe.lst"
            pDialog.ShowDialog()
            pDialog = Nothing
        End If
    End Sub

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "WinBack.wb_Rezept_Liste"
                RezeptListe.CloseButtonVisible = False
                _DockPanelList.Add(RezeptListe)
                Return RezeptListe
            Case "WinBack.wb_Rezept_Details"
                RezeptDetails.CloseButtonVisible = True
                _DockPanelList.Add(RezeptListe)
                Return RezeptDetails
            Case "WinBack.wb_Rezept_Hinweise"
                RezeptHinweise = New wb_Rezept_Hinweise
                _DockPanelList.Add(RezeptHinweise)
                Return RezeptHinweise
            Case "WinBack.wb_Rezept_Historie"
                RezeptHistorie = New wb_Rezept_Historie
                _DockPanelList.Add(RezeptHistorie)
                Return RezeptHistorie
            Case "WinBack.wb_Rezept_Verwendung"
                RezeptVerwendung = New wb_Rezept_Verwendung
                _DockPanelList.Add(RezeptVerwendung)
                Return RezeptVerwendung
            Case Else
                Return Nothing
        End Select
    End Function

End Class
