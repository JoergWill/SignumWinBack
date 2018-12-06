Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Artikel_Main
    Implements IExternalFormUserControl

    'Default-Fenster
    Public ArtikelListe As New wb_Artikel_Liste
    Public ArtikelDetails As New wb_Artikel_Details

    'alle anderen Fenster werden zur Laufzeit erzeugt
    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        MyBase.New(ServiceProvider)
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
        ArtikelDetails.Show(DockPanel, DockState.DockTop)
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
        If ArtikelListe IsNot Nothing Then
            ArtikelListe.Close()
        End If
        'Fenster darf geschlossen werden
        Return False
    End Function

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("ArtikeLVerwaltung", "WinBack-Artikel", "Verwaltung der WinBack-Artikel")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpArtikel", "WinBack Artikel")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnArtikelDetails", "Details", "weitere Artikel-Daten", My.Resources.MainArtikel_16x16, My.Resources.MainArtikel_32x32, AddressOf BtnArtikelDetails)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Private Sub BtnArtikelDetails()
        ArtikelDetails = New wb_Artikel_Details
        ArtikelDetails.Show(DockPanel, DockState.DockTop)
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

            Case Else
                Return Nothing
        End Select
    End Function
End Class
