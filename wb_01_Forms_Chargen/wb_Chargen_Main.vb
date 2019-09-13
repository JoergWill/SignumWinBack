Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Chargen_Main
    Implements IExternalFormUserControl

    'Default-Fenster
    Public ChargenListe As New wb_Chargen_Liste
    Public ChargenFunktionen As wb_Chargen_Funktionen
    Public ChargenDetails As wb_Chargen_Details
    Public ChargenWasserTemp As wb_ChargenWasserTemp

    'alle anderen Fenster werden zur Laufzeit erzeugt
    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        MyBase.New(ServiceProvider)
        AddHandler wb_Chargen_Shared.eDetail_DblClick, AddressOf Details_DblClick
    End Sub

    ''' <summary>
    ''' Fenster-Name (Caption). Wird von Init() aufgerufen
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormText As String
        Get
            Return "WinBack Chargen-Auswertung"
        End Get
    End Property

    ''' <summary>
    ''' Eindeutiger Name für die Basis-Form. 
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormName As String
        Get
            Me.Tag = "StatistikChargen"
            Return "StatistikChargen"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        ChargenListe.Show(DockPanel, DockState.DockLeft)
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
        'Chargen-Liste (ordentlich) schliessen - Speichert die Grid-Einstellungen
        If ChargenListe IsNot Nothing Then
            ChargenListe.Close()
        End If
        'Fenster darf geschlossen werden
        Return False
    End Function

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("Chargen-Auswertung", "WinBack-Chargen", "Auswertung der produzierten Chargen in WinBack")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpChargen", "WinBack Chargen")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnChargenInfo", "Funktionen", "Einstellungen Filter, Export-Optionen", My.Resources.ChargenInfo_32x32, My.Resources.ChargenInfo_32x32, AddressOf BtnChargenFunktionen)
                oGrp.AddButton("BtnChargenDetails", "Details", "Detail-Ansicht aller prodzierten Chargen", My.Resources.ChargenDetails_32x32, My.Resources.ChargenDetails_32x32, AddressOf BtnChargenDetails)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Private Sub BtnChargenFunktionen()
        ChargenFunktionen = New wb_Chargen_Funktionen
        ChargenFunktionen.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub BtnChargenDetails()
        ChargenDetails = New wb_Chargen_Details
        ChargenDetails.Show(DockPanel, DockState.DockTop)
    End Sub

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString

            Case "WinBack.wb_Chargen_Liste"
                ChargenListe.CloseButtonVisible = False
                _DockPanelList.Add(ChargenListe)
                Return ChargenListe

            Case "WinBack.wb_Chargen_Funktionen"
                ChargenFunktionen = New wb_Chargen_Funktionen
                _DockPanelList.Add(ChargenFunktionen)
                Return ChargenFunktionen

            Case "WinBack.wb_Chargen_Details"
                ChargenDetails = New wb_Chargen_Details
                _DockPanelList.Add(ChargenDetails)
                Return ChargenDetails

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
    Private Sub Details_DblClick(Sender As Object, ChargenZeile As wb_ChargenSchritt)
        'Abhängig von der Komponente wird das entsprechende Fenster geöffnet
        Select Case ChargenZeile.KomponentenType

            Case wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE
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

            Case wb_Global.KomponTypen.KO_TYPE_TEMPERATURERFASSUNG

            Case wb_Global.KomponTypen.KO_TYPE_KNETER
        End Select
    End Sub



End Class
