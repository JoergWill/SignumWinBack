Imports WeifenLuo.WinFormsUI.Docking

''' <summary>
''' MDI-Main-Fenster Linien(VNC)
''' Abgeleitet von DockPanel_Main
'''     In DockPanelMain werden alle Funktionen für die Verwaltung der Layouts abgewickelt
'''     Default-Layout, Laden, Sichern, Löschen der Layouts
'''     Die Layouts werden, im Unterschied zu OrgaBack nicht vom jeweiligen Fenster sondern
'''     von der WinBack-Main-Form verwaltet.
''' </summary>
Public Class Linien_Main

    Public LinienListe As New wb_Linien_Liste
    Private LinienDetails As wb_Linien_Details
    Private LinienKneter As wb_Linien_Kneter

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
                LinienListe.Show(DockPanel, DockState.DockLeft)
                Return True
            Case "OPENDETAILS"
                If IsNothingOrDisposed(LinienDetails) Then
                    LinienDetails = New wb_Linien_Details
                End If
                LinienDetails.Show(DockPanel, DockState.DockLeft)
                Return True

            Case "LINIE_NEU"
                Return LinieNeu()
            Case "LINIE_DEL"
                Return LinieDel()
            Case "LINIE_AUTOINSTALL"
                Return LinienAutoInstall()
            Case "LISTE_DRUCKEN"
                Return LinienListeDrucken()

                'Case "KNETER_MAIN"
                '    If IsNothingOrDisposed(LinienKneter) Then
                '        LinienKneter = New wb_Linien_Kneter
                '    End If
                '    LinienKneter.Show(DockPanel, DockState.DockLeft)
                '    Return True

            Case Else
                Return False
        End Select
    End Function

    Private Function LinieNeu() As Boolean
        LinienListe.AddItems("", "Neuer Eintrag")
        LinienListe.SelectLastItem()
        LinienDetails.DetailInfo(Nothing)
        LinienDetails.DetailEdit()
        Return True
    End Function

    Private Function LinieDel() As Boolean
        LinienListe.RemoveItem()
        Return True
    End Function

    Private Function LinienAutoInstall() As Boolean
        LinienListe.AddFromDataBase()
        Return True
    End Function

    Private Function LinienListeDrucken() As Boolean
        Return True
    End Function

    ''' <summary>
    ''' Default-Layout anzeigen.
    ''' Falls keine Layout-Definitionen verhanden sind, wird das Haupt-Fenster (Liste) angezeigt.
    ''' </summary>
    Public Overrides Sub setDefaultLayout()
        DockPanel.Theme = wb_GlobalSettings.Theme
        LinienListe.Show(DockPanel, DockState.DockLeft)
        LinienListe.CloseButtonVisible = False
        OrgaBackOffice.LayoutFilename = "Default"
    End Sub

    ''' <summary>
    ''' Gibt für den jeweiligen Form-Namen die entsprechenden Klasse zurück, die dann im Dock dargestellt wird.
    ''' Füllt das Array DockPanelList in der Basis-Klasse
    ''' </summary>
    ''' <param name="persistString"></param>
    ''' <returns></returns>
    Public Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "WinBack.wb_Linien_Liste"
                LinienListe.CloseButtonVisible = False
                _DockPanelList.Add(LinienListe)
                Return LinienListe

            Case "WinBack.wb_Linien_Details"
                LinienDetails = New wb_Linien_Details
                _DockPanelList.Add(LinienDetails)
                Return LinienDetails

            Case "WinBack.wb_Linien_Kneter"
                LinienKneter = New wb_Linien_Kneter
                _DockPanelList.Add(LinienKneter)
                Return LinienKneter

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

    End Sub

    ''' <summary>
    ''' MDI-Form wird geschlossen. Vorher wurde schon in der Basis-Klasse die DockBar-Konfiguration gesichert.
    ''' Schliesst alle erzeugten Fenster.
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Public Overrides Sub FormClose(Sender As Object, e As FormClosedEventArgs)
        ''alle erzeugten Fenster wieder schliessen
        wb_Functions.CloseAndDisposeSubForm(LinienListe)
        wb_Functions.CloseAndDisposeSubForm(LinienDetails)
        wb_Functions.CloseAndDisposeSubForm(LinienKneter)
    End Sub
End Class