Imports WeifenLuo.WinFormsUI.Docking

''' <summary>
''' MDI-Main-Fenster Benutzerverwaltung
''' Abgeleitet von DockPanel_Main
'''     In DockPanelMain werden alle Funktionen für die Verwaltung der Layouts abgewickelt
'''     Default-Layout, Laden, Sichern, Löschen der Layouts
'''     Die Layouts werden, im Unetrschied zu OrgaBack nicht vom jeweiligen Fenster sondern
'''     von der WinBack-Main-Form verwaltet.
''' </summary>
Public Class User_Main

    Public UserListe As New wb_User_Liste
    Public UserDetails As wb_User_Details
    Private UserRechte As wb_User_Rechte
    Private UserGruppenRechte As wb_User_GruppenRechte

    ''' <summary>
    ''' Execute-Command von Winback-Main-Form.
    ''' Routine wird von Winback-Main aufgerufen um verschiedene Funktionen in der MDI-Form auszuführen.
    ''' 
    '''     OPENDETAILS         -   Detail-Fenster wird geöffnet und angezeigt.
    '''     
    ''' </summary>
    ''' <param name="Cmd"></param>
    ''' <param name="Prm"></param>
    ''' <returns></returns>
    Public Overrides Function ExtendedCmd(Cmd As String, Prm As String) As Boolean
        Select Case Cmd
            Case "OPENLISTE"
                UserListe.Show(DockPanel, DockState.DockLeft)
                Return True
            Case "OPENDETAILS"
                UserDetails = New wb_User_Details
                UserDetails.Show(DockPanel, DockState.DockLeft)
                Return True
            Case "OPENPARAMETER"
                UserRechte = New wb_User_Rechte
                UserRechte.Show(DockPanel, DockState.DockLeft)
                Return True
            Case "OPENGRPLIST"
                UserGruppenRechte = New wb_User_GruppenRechte
                UserGruppenRechte.Show(DockPanel, DockState.DockLeft)
                Return True

            Case "NEW"
                Return UserNeu()
            Case "DELETE"
                Return UserDel()
            Case "USER_PASSWD"
                Return UserPassWd()
            Case "USER_DRUCKEN"
                Return UserDrucken()

            Case Else
                Return False
        End Select
    End Function

    Private Function UserNeu() As Boolean
        'neuen (Dummy)Datensatz anlegen
        wb_User_Shared.User.AddNew()
        'Mitarbeiter-Liste aktualisieren
        UserListe.RefreshData()
        'auf den neuen Datensatz positionieren (lfd.Nummer = -1)
        UserListe.SelectData(1, wb_Global.NewUserPass)
        Return True
    End Function

    Private Function UserDel() As Boolean
        wb_User_Shared.User.Delete(wb_User_Shared.User.Passwort)
        UserListe.RefreshData()
        Return True
    End Function

    Private Function UserPassWd() As Boolean
        Return True
    End Function

    Private Function UserDrucken() As Boolean
        Return True
    End Function

    ''' <summary>
    ''' Default-Layout anzeigen.
    ''' Falls keine Layout-Definitionen verhanden sind, wird das Haupt-Fenster (Liste) angezeigt.
    ''' </summary>
    Public Overrides Sub setDefaultLayout()
        UserListe.Show(DockPanel, DockState.DockLeft)
        UserListe.CloseButtonVisible = False
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
            Case "WinBack.wb_User_Liste"
                UserListe.CloseButtonVisible = False
                _DockPanelList.Add(UserListe)
                Return UserListe
            Case "WinBack.wb_User_Details"
                _DockPanelList.Add(UserDetails)
                Return UserDetails
            Case "WinBack.wb_User_Rechte"
                _DockPanelList.Add(UserRechte)
                Return UserRechte
            Case "WinBack.wb_User_GruppenRechte"
                _DockPanelList.Add(UserGruppenRechte)
                Return UserGruppenRechte
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
        'alle erzeugten Fenster wieder schliessen
        wb_Functions.CloseAndDisposeSubForm(UserListe)
        wb_Functions.CloseAndDisposeSubForm(UserDetails)
        wb_Functions.CloseAndDisposeSubForm(UserRechte)
        wb_Functions.CloseAndDisposeSubForm(UserGruppenRechte)

        'alle "alten" Daten löschen
        wb_User_Shared.Invalid()
    End Sub
End Class