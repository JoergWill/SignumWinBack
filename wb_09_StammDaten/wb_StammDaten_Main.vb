Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_StammDaten_Main
    Implements IExternalFormUserControl

    'Fenster
    Dim LinienGruppen As wb_StammDaten_LinienGruppen

#Region "Signum"
    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        MyBase.New(ServiceProvider)
    End Sub

    ''' <summary>
    ''' Fenster-Name (Caption). Wird von Init() aufgerufen
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormText As String
        Get
            Return "WinBack Stammdaten"
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
            Return "StammDaten"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        'PlanungListe.Show(DockPanel, DockState.DockLeft)
    End Sub

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("StammDaten", "Stammdaten-Verwaltung", "WinBack Stamm-und Schlüsseldaten")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpStammDaten", "Stammdaten")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnLinienGruppen", "Linien Gruppen", "Liniengruppen und Aufarbeitungsplätze einrichten", My.Resources.MainLinien_32x32, My.Resources.MainLinien_32x32, AddressOf BtnLinienGruppen)
                oGrp.AddButton("BtnAllergene", "Allergene und Inhaltsstoffe", "Allergene und Inhalts-Stoffe verwalten", My.Resources.RohstoffeNwt_32x32, My.Resources.RohstoffeNwt_32x32, AddressOf BtnAllergene)
                oGrp.AddButton("BtnRzptArtGruppen", "Rezept und Artikelgruppen", "Rezeptgruppen und Artikelgruppen verwalten", My.Resources.ArtikelParameter_32x32, My.Resources.ArtikelParameter_32x32, AddressOf BtnRzptArtGruppen)
                oGrp.AddButton("BtnRzptVarianten", "Rezept Varianten", "Rezeptvarianten verwalten", My.Resources.RezeptDetails_32x32, My.Resources.RezeptDetails_32x32, AddressOf BtnRzptVarianten)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString

            Case "WinBack.wb_StammDaten_LinienGruppen"
                LinienGruppen = New wb_StammDaten_LinienGruppen
                _DockPanelList.Add(LinienGruppen)
                Return LinienGruppen

            Case Else
                Return Nothing
        End Select
    End Function
#End Region

    Public Overrides Function Init() As Boolean Implements IBasicFormUserControl.Init
        'Init aus der Basis-Klasse aufrufen (zuerst)
        Init = MyBase.Init()
    End Function

    ''' <summary>
    ''' Liniengruppen und Aufarbeitungsplätze anlegen und verwalten.
    ''' Aufarbeitungsplätze werden in der Tabelle Liniengruppen mit Linien-Nummer größer als 100 eingetragen.
    ''' </summary>
    Private Sub BtnLinienGruppen()
        LinienGruppen = New wb_StammDaten_LinienGruppen
        LinienGruppen.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub BtnAllergene()
        'TODO Allergene und Zusatzstoffe ein/aus
    End Sub

    Private Sub BtnRzptArtGruppen()
        'TODO Rezept/Artikel-Gruppen
    End Sub

    Private Sub BtnRzptVarianten()
        'TODO Rezeptvarianten
    End Sub
End Class
