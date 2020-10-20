Imports System.Windows.Forms
Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Main
    Implements IExternalFormUserControl

    'Default-Fenster
    Public RohstoffListe As New wb_Rohstoffe_Liste
    Public RohstoffDetails As New wb_Rohstoffe_Details

    'alle anderen Fenster werden zur Laufzeit erzeugt
    Public RohstoffVerwendung As wb_Rohstoffe_Verwendung
    Public RohstoffParameter As wb_Rohstoffe_Parameter
    Public RohstoffSilo As wb_Rohstoffe_Silo
    Public RohstoffLieferung As wb_Rohstoffe_Lieferung
    Public RohstoffNwt As wb_Rohstoffe_Nwt
    Public RohstoffCloud As wb_Rohstoffe_Cloud

    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        'Initialisierung
        MyBase.New(ServiceProvider)
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
            Return "WinBack Rohstoff-Verwaltung"
        End Get
    End Property

    ''' <summary>
    ''' Eindeutiger Name für die Basis-Form. 
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormName As String
        Get
            Me.Tag = "Rohstoffe"
            Return "Rohstoffe"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        RohstoffListe.Show(DockPanel, DockState.DockLeft)
        RohstoffDetails.Show(DockPanel, DockState.DockTop)
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
        'Rohstoff-Liste (ordentlich) schliessen - Speichert die Grid-Einstellungen
        wb_Functions.CloseAndDisposeSubForm(RohstoffListe)
        wb_Functions.CloseAndDisposeSubForm(RohstoffParameter)
        wb_Functions.CloseAndDisposeSubForm(RohstoffLieferung)
        wb_Functions.CloseAndDisposeSubForm(RohstoffNwt)
        wb_Functions.CloseAndDisposeSubForm(RohstoffCloud)
        wb_Functions.CloseAndDisposeSubForm(RohstoffVerwendung)
        wb_Functions.CloseAndDisposeSubForm(RohstoffSilo)

        'alle Spuren in Rohstoffe_Shared löschen
        wb_Rohstoffe_Shared.Invalid()

        'Fenster darf geschlossen werden
        Return False
    End Function

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("RohstoffVerwaltung", "WinBack-Rohstoffe", "Verwaltung der WinBack-Rohstoffe")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrpStammdaten = oNewTab.AddGroup("GrpWinBack", "WinBack")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrpStammdaten.AddButton("btnArtikelStamm", "Artikel", "WinBack Artikelstammdaten", My.Resources.MainArtikel_16x16, My.Resources.MainArtikel_32x32, AddressOf ShowArtikelForm)
                oGrpStammdaten.AddButton("btnRezeptStamm", "Rezepte", "WinBack Rezeptverwaltung", My.Resources.MainRezept_16x16, My.Resources.MainRezept_32x32, AddressOf ShowRezeptForm)
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpRohstoffe", "WinBack Rohstoffe")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("btnRohstoffListe", "Rohstoffe", "WinBack Rohstoff-Liste", My.Resources.MainRohstoffe_16x16, My.Resources.MainRohstoffe_32x32, AddressOf ShowRohstoffListe)
                oGrp.AddButton("BtnRohstoffDetails", "Details", "weitere Rohstoff-Daten", My.Resources.RohstoffeDetails_32x32, My.Resources.RohstoffeDetails_32x32, AddressOf BtnRohstoffDetails)
                oGrp.AddButton("BtnRohstoffParameter", "Parameter", "Rohstoffparameter", My.Resources.RohstoffeParameter_32x32, My.Resources.RohstoffeParameter_32x32, AddressOf BtnRohstoffParameter)
                oGrp.AddButton("BtnRohstoffSilo", "Silo Füllstand", "Rohstoffe Silo-Füllstände", My.Resources.RohstoffSilo_32x32, My.Resources.RohstoffSilo_32x32, AddressOf BtnRohstoffSilo)
                oGrp.AddButton("BtnRohstoffLieferung", "Lager Produktion", "Rohstoffe Produktionslager", My.Resources.RohstoffeLieferung_32x32, My.Resources.RohstoffeLieferung_32x32, AddressOf BtnRohstoffLieferung)
                oGrp.AddButton("BtnRohstoffNwt", "Nährwerte", "Rohstoffe Allergene und Nährwert-Angaben", My.Resources.RohstoffeNwt_32x32, My.Resources.RohstoffeNwt_32x32, AddressOf BtnRohstoffNwt)
                oGrp.AddButton("BtnRohstoffCloud", "Cloud/Rezept", "Rohstoffe Verknüpfung zur Cloud/Rezeptur", My.Resources.RohstoffeLink_32x32, My.Resources.RohstoffeLink_32x32, AddressOf BtnRohstoffCloud)
                oGrp.AddButton("BtnRohstoffVerwendung", "Verwendung", "Verwendung des Rohstoffes in Rezepturen", My.Resources.RohstoffeVerwendung_32x32, My.Resources.RohstoffeVerwendung_32x32, AddressOf BtnRohstoffVerwendung)
                Dim oGrpPrnt = oNewTab.AddGroup("Printer", "Drucken")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrpPrnt.AddButton("BtnRohstoffListeDrucken", "Drucke Rohstoffliste", "Liste aller Rohstoffe drucken", My.Resources.RohstoffeDrucken_32x32, My.Resources.RohstoffeDrucken_32x32, AddressOf BtnRohstoffListeDrucken)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Private Sub ShowArtikelForm()
        wb_Main_Shared.OpenForm(Me, "Artikel")
    End Sub

    Private Sub ShowRezeptForm()
        wb_Main_Shared.OpenForm(Me, "Rezepte")
    End Sub

    Private Sub ShowRohstoffListe()
        RohstoffListe.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub BtnRohstoffDetails()
        If IsNothingOrDisposed(RohstoffDetails) Then
            RohstoffDetails = New wb_Rohstoffe_Details
        End If
        RohstoffDetails.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub BtnRohstoffParameter()
        If IsNothingOrDisposed(RohstoffParameter) Then
            RohstoffParameter = New wb_Rohstoffe_Parameter
        End If
        RohstoffParameter.Show(DockPanel, DockState.Document)
    End Sub

    Private Sub BtnRohstoffNwt()
        If IsNothingOrDisposed(RohstoffNwt) Then
            RohstoffNwt = New wb_Rohstoffe_Nwt
        End If
        RohstoffNwt.Show(DockPanel, DockState.Document)
    End Sub

    Private Sub BtnRohstoffSilo()
        If IsNothingOrDisposed(RohstoffSilo) Then
            RohstoffSilo = New wb_Rohstoffe_Silo
        End If
        RohstoffSilo.Show(DockPanel, DockState.Document)
    End Sub

    Private Sub BtnRohstoffLieferung()
        If IsNothingOrDisposed(RohstoffLieferung) Then
            RohstoffLieferung = New wb_Rohstoffe_Lieferung
        End If
        RohstoffLieferung.Show(DockPanel, DockState.Document)
    End Sub

    Private Sub BtnRohstoffCloud()
        If IsNothingOrDisposed(RohstoffCloud) Then
            RohstoffCloud = New wb_Rohstoffe_Cloud
        End If
        RohstoffCloud.Show(DockPanel, DockState.Document)
    End Sub

    Private Sub BtnRohstoffVerwendung()
        If IsNothingOrDisposed(RohstoffVerwendung) Then
            RohstoffVerwendung = New wb_Rohstoffe_Verwendung
        End If
        RohstoffVerwendung.Show(DockPanel, DockState.Document)
    End Sub

    Private Sub BtnRohstoffListeDrucken()
        'sicherheitshalber abfragen
        If Not IsNothing(RohstoffListe) Then

            'Druck-Daten
            Dim pDialog As New wb_PrinterDialog(False) 'Drucker-Dialog
            pDialog.LL_KopfZeile_1 = RohstoffListe.FilterText

            'Liste aller Rohstoffe aus den DataGridView
            pDialog.LL.DataSource = New combit.ListLabel22.DataProviders.AdoDataProvider(RohstoffListe.DataGridView.LLData)

            'List und Label-Verzeichnis für die Listen
            pDialog.ListSubDirectory = "Rohstoffe"
            pDialog.ListFileName = "RohstoffListe.lst"
            pDialog.ShowDialog()
            pDialog = Nothing
        End If
    End Sub

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString

            Case "WinBack.wb_Rohstoffe_Liste"
                RohstoffListe.CloseButtonVisible = False
                _DockPanelList.Add(RohstoffListe)
                Return RohstoffListe

            Case "WinBack.wb_Rohstoffe_Details"
                RohstoffDetails = New wb_Rohstoffe_Details
                _DockPanelList.Add(RohstoffDetails)
                Return RohstoffDetails

            Case "WinBack.wb_Rohstoffe_Verwendung"
                RohstoffVerwendung = New wb_Rohstoffe_Verwendung
                _DockPanelList.Add(RohstoffVerwendung)
                Return RohstoffVerwendung

            Case "WinBack.wb_Rohstoffe_Parameter"
                RohstoffParameter = New wb_Rohstoffe_Parameter
                _DockPanelList.Add(RohstoffParameter)
                Return RohstoffParameter

            Case "WinBack.wb_Rohstoffe_Silo"
                RohstoffSilo = New wb_Rohstoffe_Silo
                _DockPanelList.Add(RohstoffSilo)
                Return RohstoffSilo

            Case "WinBack.wb_Rohstoffe_Nwt"
                RohstoffNwt = New wb_Rohstoffe_Nwt
                _DockPanelList.Add(RohstoffNwt)
                Return RohstoffNwt

            Case "WinBack.wb_Rohstoffe_Cloud"
                RohstoffCloud = New wb_Rohstoffe_Cloud
                _DockPanelList.Add(RohstoffCloud)
                Return RohstoffCloud
            Case Else
                Return Nothing
        End Select
    End Function
End Class
