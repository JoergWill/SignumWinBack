Imports WeifenLuo.WinFormsUI.Docking

''' <summary>
''' MDI-Main-Fenster Statistische Auswertung
''' Abgeleitet von DockPanel_Main
'''     In DockPanelMain werden alle Funktionen für die Verwaltung der Layouts abgewickelt
'''     Default-Layout, Laden, Sichern, Löschen der Layouts
'''     Die Layouts werden, im Unterschied zu OrgaBack nicht vom jeweiligen Fenster sondern
'''     von der WinBack-Main-Form verwaltet.
''' </summary>
Public Class Chargen_Main

    Public ChargenListe As New wb_Chargen_Liste         'Default-Fenster    (wird beim Öffnen immer angezeigt)
    Public ChargenDetails As wb_Chargen_Details         'Detail-Fenster
    Public ChargenWasserTemp As wb_ChargenWasserTemp
    Public ChargenChartTemp As wb_Chargen_ChartTTS

    Public StatistikRohVerbrauch As wb_Statistik_RohVerbrauch
    Public StatistikRohDetails As wb_Statistik_RohDetails
    Public StatistikRezepte As wb_Statistik_Rezepte

    ''' <summary>
    ''' Execute-Command von Winback-Main-Form.
    ''' Routine wird von Winback-Main aufgerufen um verschiedene Funktionen in der MDI-Form auszuführen.
    ''' 
    '''     OPENDLISTE          -   Listen-Fenster wird geöffnet und angezeigt.
    '''     OPENDETAILS         -   Detail-Fenster wird geöffnet und angezeigt.
    '''     
    ''' </summary>
    ''' <param name="Cmd"></param>
    ''' <param name="Prm"></param>
    ''' <returns></returns>
    Public Overrides Function ExtendedCmd(Cmd As String, Prm As String) As Boolean
        Select Case Cmd
            Case "OPENLISTE"
                ChargenListe.Show(DockPanel, DockState.DockLeft)
                Return True
            Case "OPENDETAILS"
                If Not DockIsVisible("wb_Chargen_Details") Then
                    ChargenDetails = New wb_Chargen_Details
                    ChargenDetails.Show(DockPanel)
                End If
                Return True
            Case "STATROHVERBR"
                If Not DockIsVisible("wb_Statistik_RohVerbrauch") Then
                    StatistikRohVerbrauch = New wb_Statistik_RohVerbrauch
                    StatistikRohVerbrauch.Show(DockPanel)
                End If
                Return True
            Case "STATROHDETAIL"
                If Not DockIsVisible("wb_Statistik_RohDetails") Then
                    StatistikRohDetails = New wb_Statistik_RohDetails
                    StatistikRohDetails.Show(DockPanel)
                End If
                Return True
            Case "STATREZEPT"
                If Not DockIsVisible("wb_Statistik_Rezepte") Then
                    StatistikRezepte = New wb_Statistik_Rezepte
                    StatistikRezepte.Show(DockPanel)
                End If
                Return True
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>
    ''' Default-Layout anzeigen.
    ''' Falls keine Layout-Definitionen verhanden sind, wird das Haupt-Fenster (Liste) angezeigt.
    ''' </summary>
    Public Overrides Sub setDefaultLayout()
        ChargenListe.Show(DockPanel, DockState.DockLeft)
        ChargenListe.CloseButtonVisible = False
        WinBack.LayoutFilename = "Default"
    End Sub

    ''' <summary>
    ''' Gibt für den jeweiligen Form-Namen die entsprechenden Klasse zurück, die dann im Dock dargestellt wird.
    ''' Füllt das Array DockPanelList in der Basis-Klasse
    ''' </summary>
    ''' <param name="persistString"></param>
    ''' <returns></returns>
    Public Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "WinBack.wb_Chargen_Liste"
                ChargenListe.CloseButtonVisible = False
                _DockPanelList.Add(ChargenListe)
                Return ChargenListe
            Case "WinBack.wb_Chargen_Details"
                ChargenDetails = New wb_Chargen_Details
                _DockPanelList.Add(ChargenDetails)
                Return ChargenDetails
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
    ''' MID-Form wird geöffnet. Vorher wurde schon in der Basis-Klasse die DockBar-Konfiguration geladen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Public Overrides Sub FormOpen(Sender As Object, e As EventArgs)
        AddHandler wb_Chargen_Shared.eDetail_DblClick, AddressOf Details_DblClick
    End Sub

    ''' <summary>
    ''' MDI-Form wird geschlossen. Vorher wurde schon in der Basis-Klasse die DockBar-Konfiguration gesichert.
    ''' Schliesst alle erzeugten Fenster.
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Public Overrides Sub FormClose(Sender As Object, e As FormClosedEventArgs)
        'Event Details_DblClick wieder löschen
        RemoveHandler wb_Chargen_Shared.eDetail_DblClick, AddressOf Details_DblClick

        'alle erzeugten Fenster wieder schliessen
        wb_Functions.CloseAndDisposeSubForm(ChargenDetails)
        wb_Functions.CloseAndDisposeSubForm(ChargenListe)
    End Sub


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

                'Anzeige Chargenfolge graphisch
                If IsNothingOrDisposed(ChargenChartTemp) Then
                    ChargenChartTemp = New wb_Chargen_ChartTTS
                End If
                'ausgewählte Zeile aus Chargen-Details
                ChargenChartTemp.ChargenZeile = ChargenZeile
                'Übersicht anzeigen
                ChargenChartTemp.Show(DockPanel, ChargenDetails.DockState)


            Case wb_Global.KomponTypen.KO_TYPE_KNETER
        End Select
    End Sub

End Class