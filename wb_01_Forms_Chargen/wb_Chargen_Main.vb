﻿Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Chargen_Main
    Implements IExternalFormUserControl

    'Default-Fenster
    Public ChargenDetails As New wb_Chargen_Details
    Public StatistikChargen As wb_Statistik_Chargen
    Public StatistikRohVerbrauch As wb_Statistik_RohVerbrauch
    Public StatistikRohDetails As wb_Statistik_RohDetails
    Public StatistikRezepte As wb_Statistik_Rezepte

    Public ChargenWasserTemp As wb_ChargenWasserTemp
    Public ChargenChartTemp As wb_Chargen_ChartTTS
    Public ChargenChartSauerteig As wb_Chargen_ChartVTS

    'alle anderen Fenster werden zur Laufzeit erzeugt
    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        'Initialisierung
        MyBase.New(ServiceProvider)
        'Default-Layout wenn keine Fenster angezeigt werden (neuer Benutzer...)
        If _DockPanelList.Count = 0 Then
            SetDefaultLayout()
        End If
        AddHandler wb_Chargen_Shared.eDetail_DblClick, AddressOf Details_DblClick
    End Sub

    ''' <summary>
    ''' Fenster-Name (Caption). Wird von Init() aufgerufen
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormText As String
        Get
            Return "WinBack Statistik-Produktion"
        End Get
    End Property

    ''' <summary>
    ''' Eindeutiger Name für die Basis-Form. 
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormName As String
        Get
            Me.Tag = "Statistik"
            Return "Statistik"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        DockPanel.Theme = wb_GlobalSettings.Theme
        ChargenDetails.Show(DockPanel, DockState.DockRight)
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
        'Fenster (ordentlich) schliessen - Speichert die Grid-Einstellungen
        wb_Functions.CloseAndDisposeSubForm(ChargenDetails)
        wb_Functions.CloseAndDisposeSubForm(StatistikChargen)
        wb_Functions.CloseAndDisposeSubForm(StatistikRohVerbrauch)
        wb_Functions.CloseAndDisposeSubForm(StatistikRohDetails)
        wb_Functions.CloseAndDisposeSubForm(StatistikRezepte)

        'Fenster darf geschlossen werden
        Return False
    End Function

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("Statistik-Auswertung", "WinBack-Statistik", "Auswertung der Produktionsdaten in WinBack")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpChargen", "WinBack Statistik")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("btnStatistikChargen", "Statistik Chargen", "WinBack Auswertung Produktions-Chargen", My.Resources.MainStatistikChargen_16x16, My.Resources.MainStatistikChargen_32x32, AddressOf BtnStatistikChargenForm)
                oGrp.AddButton("btnStatistikRohstoffe", "Verbrauch Rohstoffe", "WinBack Auswertung Rohstoff Verbrauch", My.Resources.MainStatistikRohstoffe_16x16, My.Resources.MainStatistikRohstoffe_32x32, AddressOf BtnStatistikRohVerbrauchForm)
                oGrp.AddButton("btnStatistikRezepte", "Statistik Rezepte", "WinBack Statistik - Auswertung der produzierten Teige", My.Resources.MainStatistikRezepte_16x16, My.Resources.MainStatistikRezepte_32x32, AddressOf BtnStatistikRezeptForm)
                oGrp.AddButton("BtnChargenDetails", "Details", "Detail-Ansicht aller produzierten Chargen", My.Resources.ChargenDetails_32x32, My.Resources.ChargenDetails_32x32, AddressOf BtnChargenDetails)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Private Sub BtnStatistikChargenForm()
        If IsNothingOrDisposed(StatistikChargen) Then
            StatistikChargen = New wb_Statistik_Chargen
            StatistikChargen.Show(DockPanel, DockState.DockLeft)
        Else
            StatistikChargen.Show(DockPanel)
        End If
    End Sub

    Private Sub BtnStatistikRohVerbrauchForm()
        If IsNothingOrDisposed(StatistikRohVerbrauch) Then
            StatistikRohVerbrauch = New wb_Statistik_RohVerbrauch
            StatistikRohVerbrauch.Show(DockPanel, DockState.DockLeft)
        Else
            StatistikRohVerbrauch.Show(DockPanel)
        End If
    End Sub

    Private Sub BtnStatistikRohDetailsForm()
        If IsNothingOrDisposed(StatistikRohDetails) Then
            StatistikRohDetails = New wb_Statistik_RohDetails
            StatistikRohDetails.Show(DockPanel, DockState.DockLeft)
        Else
            StatistikRohDetails.Show(DockPanel)
        End If
    End Sub

    Private Sub BtnStatistikRezeptForm()
        If IsNothingOrDisposed(StatistikRezepte) Then
            StatistikRezepte = New wb_Statistik_Rezepte
            StatistikRezepte.Show(DockPanel, DockState.DockLeft)
        Else
            StatistikRezepte.Show(DockPanel)
        End If
    End Sub

    Private Sub BtnChargenDetails()
        If IsNothingOrDisposed(ChargenDetails) Then
            ChargenDetails = New wb_Chargen_Details
            ChargenDetails.Show(DockPanel, DockState.DockRight)
        Else
            ChargenDetails.Show(DockPanel)
        End If
    End Sub

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString

            Case "WinBack.wb_Chargen_Details"
                ChargenDetails.CloseButtonVisible = False
                _DockPanelList.Add(ChargenDetails)
                Return ChargenDetails

            Case "WinBack.wb_Statistik_Chargen"
                StatistikChargen = New wb_Statistik_Chargen
                _DockPanelList.Add(StatistikChargen)
                Return StatistikChargen

            Case "WinBack.wb_Statistik_RohVerbrauch"
                StatistikRohVerbrauch = New wb_Statistik_RohVerbrauch
                _DockPanelList.Add(StatistikRohVerbrauch)
                Return StatistikRohVerbrauch

            Case "WinBack.wb_Statistik_RohDetails"
                StatistikRohDetails = New wb_Statistik_RohDetails
                _DockPanelList.Add(StatistikRohDetails)
                Return StatistikRohDetails

            Case "WinBack.wb_Statistik_Rezepte"
                StatistikRezepte = New wb_Statistik_Rezepte
                _DockPanelList.Add(StatistikRezepte)
                Return StatistikRezepte

            Case Else
                Return Nothing
        End Select
    End Function

    ''' <summary>
    ''' Doppel-Klick auf eine Chargen-Zeile in der Detail-Ansicht
    '''     
    '''     Temperatur-Erfassung    öffnet ein Fenster mit der Temperatur-Statistik über alle Teige mit dieser Rezeptnummer
    '''     Wasser-Zeile            öffnet ein Fenster mit der Berechnung der Wasser-Soll-Temperatur
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="ChargenZeile"></param>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S1172:Unused procedure parameters should be removed", Justification:="<Ausstehend>")>
    Private Sub Details_DblClick(Sender As Object, ChargenZeile As wb_ChargenSchritt)
        'Abhängig von der Komponente wird das entsprechende Fenster geöffnet
        Select Case ChargenZeile.KomponentenType

            Case wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE
                'Grafische Übersicht der Temperatur-Berechnung
                ShowChargenTempBerechnung(ChargenZeile)

            Case wb_Global.KomponTypen.KO_TYPE_TEMPERATURERFASSUNG
                'Grafische Übersicht der Temperatur-Erfassung - zeitlich gestaffelt
                ShowChargenTempErfassung(ChargenZeile)

            Case wb_Global.KomponTypen.KO_TYPE_KNETER
                'Kneter-Komponente ist Teigtemperatur-Erfassung
                If wb_Rohstoffe_Shared.TeigTempRohstoffe.ContainsKey(ChargenZeile.KomponentenNr) Then
                    'Grafische Übersicht der Temperatur-Erfassung - zeitlich gestaffelt
                    ShowChargenTempErfassung(ChargenZeile)
                End If

            Case Else
                'Linien-Nummer ist kleiner Null (ARZ_LiBehNr-100) - Anzeige Temperaturverlauf Sauerteig-Rezept
                If ChargenZeile.Linie < 0 Then
                    'Grafische Übersicht der Sauerteig-Chargen
                    ShowSauerteigChargen(ChargenZeile)
                End If

        End Select
    End Sub

    Private Sub ShowChargenTempBerechnung(ChargenZeile As wb_ChargenSchritt)
        If IsNothingOrDisposed(ChargenWasserTemp) Then
            ChargenWasserTemp = New wb_ChargenWasserTemp
        End If
        'ausgewählte Zeile aus Chargen-Details
        ChargenWasserTemp.ChargenZeile = ChargenZeile
        'Wenn eine gültige Zeile im log-File (0s1-s.dbg) gefunden wurde
        If ChargenWasserTemp.Result Then
            'Fenster Chargen-Auswertung neben dem Fenster Chargen-Details anzeigen
            ChargenWasserTemp.Show(DockPanel, ChargenDetails.DockState)
        Else
            If ChargenWasserTemp.ChargenNummer = "ERR" Then
                MsgBox("Fehler bei ssh-Verbindung zum WinBack-Server " & vbCrLf & "Es sind keine Daten im Log-File verfügbar", MsgBoxStyle.Critical, "Keine Daten vom WinBack-Server")
            Else
                MsgBox("Es sind keine Informationen (mehr) zu dieser Charge im Log-File vorhanden", MsgBoxStyle.Information, "Auswertung TTS")
            End If
        End If
    End Sub

    Private Sub ShowChargenTempErfassung(ChargenZeile As wb_ChargenSchritt)
        'Anzeige Chargenfolge graphisch
        If IsNothingOrDisposed(ChargenChartTemp) Then
            ChargenChartTemp = New wb_Chargen_ChartTTS
        End If
        'ausgewählte Zeile aus Chargen-Details
        ChargenChartTemp.ChargenZeile = ChargenZeile
        'Übersicht anzeigen
        ChargenChartTemp.Show(DockPanel, ChargenDetails.DockState)
    End Sub

    Private Sub ShowSauerteigChargen(ChargenZeile As wb_ChargenSchritt)
        'Anzeige Temperaturverlauf graphisch
        If IsNothingOrDisposed(ChargenChartSauerteig) Then
            ChargenChartSauerteig = New wb_Chargen_ChartVTS
        End If
        'ausgewählte Zeile aus Chargen-Details
        ChargenChartSauerteig.ChargenZeile = ChargenZeile
        'Übersicht anzeigen
        ChargenChartSauerteig.Show(DockPanel, ChargenDetails.DockState)
    End Sub

End Class
