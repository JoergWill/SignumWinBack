Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_StammDaten_Main
    Implements IExternalFormUserControl

    'Fenster
    Dim WinBackKonfig As wb_StammDaten_Konfiguration
    Dim LinienGruppen As wb_StammDaten_LinienGruppen
    Dim ArtRohGruppen As wb_StammDaten_ArtRohGruppen
    Dim RezeptVarianten As wb_StammDaten_RezeptVarianten
    Dim RezeptGruppen As wb_StammDaten_Rezeptgruppen
    Dim Allergene As wb_StammDaten_Allergene

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
        'LinienGruppen-Liste (ordentlich) schliessen - Speichert die Grid-Einstellungen
        If LinienGruppen IsNot Nothing Then
            LinienGruppen.Close()
        End If

        'Allergen-Liste (ordentlich) schliessen - Speichert die Grid-Einstellungen
        If Allergene IsNot Nothing Then
            Allergene.Close()
        End If

        'Rohstoff- und Artikelgruppen (ordentlich) schliessen - Speichert die Grid-Einstellungen
        If ArtRohGruppen IsNot Nothing Then
            ArtRohGruppen.Close()
        End If

        'Fenster darf geschlossen werden
        Return False
    End Function

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("StammDaten", "Stammdaten-Verwaltung", "WinBack Stamm-und Schlüsseldaten")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpStammDaten", "Stammdaten")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnWinBackKonfig", "WinBack Konfiguration", "Konfigurations-Tabelle WinBack-Produktion", My.Resources.EditKonfig_16x16, My.Resources.EditKonfig_32x32, AddressOf BtnWinBackKonfig)
                oGrp.AddButton("BtnLinienGruppen", "Linien Gruppen", "Liniengruppen und Aufarbeitungsplätze einrichten", My.Resources.MainLinien_32x32, My.Resources.MainLinien_32x32, AddressOf BtnLinienGruppen)
                oGrp.AddButton("BtnAllergene", "Allergene und Inhaltsstoffe", "Allergene und Inhalts-Stoffe verwalten", My.Resources.RohstoffeNwt_32x32, My.Resources.RohstoffeNwt_32x32, AddressOf BtnAllergene)
                oGrp.AddButton("BtnArtRohGruppen", "Rohstoff- und Artikelgruppen", "Rohstoff- und Artikelgruppen verwalten", My.Resources.ArtikelParameter_32x32, My.Resources.ArtikelParameter_32x32, AddressOf BtnArtRohGruppen)
                oGrp.AddButton("BtnRzptVarianten", "Rezept Varianten", "Rezeptvarianten verwalten", My.Resources.RezeptDetails_32x32, My.Resources.RezeptDetails_32x32, AddressOf BtnRzptVarianten)
                oGrp.AddButton("BtnRzptGruppen", "Rezept Gruppen", "Rezeptgruppen verwalten", My.Resources.RezeptGruppen_32x32, My.Resources.RezeptGruppen_32x32, AddressOf BtnRzptGruppen)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString

            Case "WinBack.wb_StammDaten_Konfiguration"
                WinBackKonfig = New wb_StammDaten_Konfiguration
                _DockPanelList.Add(WinBackKonfig)
                Return WinBackKonfig

            Case "WinBack.wb_StammDaten_LinienGruppen"
                LinienGruppen = New wb_StammDaten_LinienGruppen
                _DockPanelList.Add(LinienGruppen)
                Return LinienGruppen

            Case "WinBack.wb_StammDaten_Allergene"
                Allergene = New wb_StammDaten_Allergene
                _DockPanelList.Add(Allergene)
                Return Allergene

            Case "WinBack.wb_StammDaten_ArtRohGruppen"
                ArtRohGruppen = New wb_StammDaten_ArtRohGruppen
                _DockPanelList.Add(ArtRohGruppen)
                Return ArtRohGruppen

            Case "WinBack.wb_StammDaten_RezeptVarianten"
                RezeptVarianten = New wb_StammDaten_RezeptVarianten
                _DockPanelList.Add(RezeptVarianten)
                Return RezeptVarianten

            Case "WinBack.wb_StammDaten_Rezeptgruppen"
                RezeptGruppen = New wb_StammDaten_Rezeptgruppen
                _DockPanelList.Add(RezeptGruppen)
                Return RezeptGruppen

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
    ''' Edit Tabelle winback.Konfiguration
    ''' </summary>
    Private Sub BtnWinBackKonfig()
        WinBackKonfig = New wb_StammDaten_Konfiguration
        WinBackKonfig.Show(DockPanel, DockState.DockTop)
    End Sub

    ''' <summary>
    ''' Liniengruppen und Aufarbeitungsplätze anlegen und verwalten.
    ''' Aufarbeitungsplätze werden in der Tabelle Liniengruppen mit Linien-Nummer größer als 100 eingetragen.
    ''' </summary>
    Private Sub BtnLinienGruppen()
        LinienGruppen = New wb_StammDaten_LinienGruppen
        LinienGruppen.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub BtnAllergene()
        Allergene = New wb_StammDaten_Allergene
        Allergene.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub BtnArtRohGruppen()
        ArtRohGruppen = New wb_StammDaten_ArtRohGruppen
        ArtRohGruppen.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub BtnRzptVarianten()
        'TODO Rezeptvarianten
        RezeptVarianten = New wb_StammDaten_RezeptVarianten
        RezeptVarianten.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub BtnRzptGruppen()
        RezeptGruppen = New wb_StammDaten_Rezeptgruppen
        RezeptGruppen.Show(DockPanel, DockState.DockTop)
    End Sub
End Class
