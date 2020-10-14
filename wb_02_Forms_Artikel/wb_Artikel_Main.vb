Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Artikel_Main
    Implements IExternalFormUserControl

    'Default-Fenster
    Public ArtikelListe As New wb_Artikel_Liste
    'alle anderen Fenster werden zur Laufzeit erzeugt
    Public ArtikelDetails As wb_Artikel_Details
    Public ArtikelParameter As wb_Artikel_Parameter
    Public ArtikelHinweise As wb_Artikel_Hinweise

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
            Return "WinBack Artikel-Verwaltung"
        End Get
    End Property

    ''' <summary>
    ''' Eindeutiger Name für die Basis-Form. 
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormName As String
        Get
            Me.Tag = "Artikel"
            Return "Artikel"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        ArtikelListe.Show(DockPanel, DockState.DockLeft)
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
        'Artikel-Liste (ordentlich) schliessen - Speichert die Grid-Einstellungen
        wb_Functions.CloseAndDisposeSubForm(ArtikelListe)
        wb_Functions.CloseAndDisposeSubForm(ArtikelParameter)
        wb_Functions.CloseAndDisposeSubForm(ArtikelHinweise)
        wb_Functions.CloseAndDisposeSubForm(ArtikelDetails)

        'alle Spuren in Artikel_Shared löschen
        wb_Artikel_Shared.Invalid()

        'Fenster darf geschlossen werden
        Return False
    End Function

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("ArtikelVerwaltung", "WinBack-Artikel", "Verwaltung der WinBack-Artikel")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrpStammdaten = oNewTab.AddGroup("GrpWinBack", "WinBack")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrpStammdaten.AddButton("btnRohstoffStamm", "Rohstoffe", "WinBack Rohstoff-Stammdaten", My.Resources.MainRohstoffe_16x16, My.Resources.MainRohstoffe_32x32, AddressOf ShowRohstoffForm)
                oGrpStammdaten.AddButton("btnRezeptStamm", "Rezepte", "WinBack Rezeptverwaltung", My.Resources.MainRezept_16x16, My.Resources.MainRezept_32x32, AddressOf ShowRezeptForm)

                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpArtikel", "WinBack Artikel")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("btnArtikelListe", "Artikel", "WinBack Artikel-Liste", My.Resources.MainArtikel_16x16, My.Resources.MainArtikel_32x32, AddressOf BtnArtikelListe)
                oGrp.AddButton("BtnArtikelDetails", "Details", "weitere Artikel-Daten", My.Resources.ArtikelDetails_32x32, My.Resources.ArtikelDetails_32x32, AddressOf BtnArtikelDetails)
                oGrp.AddButton("BtnArtikelParameter", "Parameter", "Artikel Parameter Produktion und Nährwerte", My.Resources.ArtikelParameter_32x32, My.Resources.ArtikelParameter_32x32, AddressOf BtnArtikelParameter)
                oGrp.AddButton("BtnArtikelHinweise", "Hinweise", "Artikel Verarbeitungshinweise", My.Resources.ArtikelHinweise_32x32, My.Resources.ArtikelHinweise_32x32, AddressOf BtnArtikelHinweise)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Private Sub ShowRohstoffForm()
        wb_Main_Shared.OpenForm(Me, "Rohstoffe")
    End Sub

    Private Sub ShowRezeptForm()
        wb_Main_Shared.OpenForm(Me, "Rezepte")
    End Sub

    Private Sub BtnArtikelListe()
        ArtikelListe.Show(DockPanel, DockState.DockLeft)
    End Sub

    Private Sub BtnArtikelDetails()
        If IsNothingOrDisposed(ArtikelDetails) Then
            ArtikelDetails = New wb_Artikel_Details
        End If
        ArtikelDetails.Show(DockPanel, DockState.Document)
    End Sub

    Private Sub BtnArtikelParameter()
        If IsNothingOrDisposed(ArtikelParameter) Then
            ArtikelParameter = New wb_Artikel_Parameter
        End If
        ArtikelParameter.Show(DockPanel, DockState.Document)
    End Sub

    Private Sub BtnArtikelHinweise()
        If IsNothingOrDisposed(ArtikelHinweise) Then
            ArtikelHinweise = New wb_Artikel_Hinweise
        End If
        ArtikelHinweise.Show(DockPanel, DockState.Document)
    End Sub

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString

            Case "WinBack.wb_Artikel_Liste"
                ArtikelListe.CloseButtonVisible = False
                _DockPanelList.Add(ArtikelListe)
                Return ArtikelListe

            Case "WinBack.wb_Artikel_Details"
                ArtikelDetails = New wb_Artikel_Details
                _DockPanelList.Add(ArtikelDetails)
                Return ArtikelDetails

            Case "WinBack.wb_Artikel_Parameter"
                ArtikelParameter = New wb_Artikel_Parameter
                _DockPanelList.Add(ArtikelParameter)
                Return ArtikelParameter

            Case "WinBack.wb_Artikel_Hinweise"
                ArtikelHinweise = New wb_Artikel_Hinweise
                _DockPanelList.Add(ArtikelHinweise)
                Return ArtikelHinweise

            Case Else
                Return Nothing
        End Select
    End Function
End Class
