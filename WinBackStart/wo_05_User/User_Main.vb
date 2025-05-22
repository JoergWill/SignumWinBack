Imports WeifenLuo.WinFormsUI.Docking
Imports combit.Reporting.DataProviders

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
    Private UserPasswort As wb_User_Passwort

    ''' <summary>
    ''' Execute-Command von Winback-Main-Form.
    ''' Routine wird von Winback-Main aufgerufen um verschiedene Funktionen in der MDI-Form auszuführen.
    ''' 
    '''     OPENDETAILS         -   Detail-Fenster wird erzeugt, geöffnet und angezeigt.
    '''     OPENPARAMETER       -   Rechte-Fenster wird erzeugt, geöffnet und angezeigt
    '''     OPENGRPLIST         -   Gruppen-Rechte-Fenster wird erzeugt, geöffnet und angezeigt
    '''     NEW                 -   Neuen Benutzer anlegen (User_Shared)
    '''     DELETE              -   Benutzer löschen (User_Shared)
    '''     USER_PASSWD         -   Passwort ändern TODO
    '''     USER_DRUCKEN        -   Benutzer-Liste drucken TODO
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
                If IsNothingOrDisposed(UserDetails) Then
                    UserDetails = New wb_User_Details
                End If
                UserDetails.Show(DockPanel)
                Return True
            Case "OPENPARAMETER"
                If IsNothingOrDisposed(UserRechte) Then
                    UserRechte = New wb_User_Rechte
                End If
                UserRechte.Show(DockPanel)
                Return True
            Case "OPENGRPLIST"
                If IsNothingOrDisposed(UserGruppenRechte) Then
                    UserGruppenRechte = New wb_User_GruppenRechte
                End If
                UserGruppenRechte.Show(DockPanel)
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
        UserListe.RefreshData(Nothing)
        'auf den neuen Datensatz positionieren (lfd.Nummer = -1)
        UserListe.SelectData(4, wb_Global.NewUserPass)
        Return True
    End Function

    Private Function UserDel() As Boolean
        wb_User_Shared.User.Delete(wb_User_Shared.User.Passwort)
        UserListe.RefreshData(Nothing)
        Return True
    End Function

    Private Function UserPassWd() As Boolean
        UserPasswort = New wb_User_Passwort
        UserPasswort.ShowDialog(DockPanel)
        UserPasswort = Nothing
        UserListe.RefreshData(Nothing)
        Return True
    End Function

    Private Function UserDrucken() As Boolean
        'sicherheitshalber abfragen
        If Not IsNothing(UserListe) Then

            'Druck-Daten
            Dim pDialog As New wb_PrinterDialog(False) 'Drucker-Dialog

            'Liste aller Rohstoffe aus den DataGridView
            pDialog.LL.DataSource = New AdoDataProvider(UserListe.DataGridView.LLData)

            'List und Label-Verzeichnis für die Listen
            pDialog.ListSubDirectory = "StammDaten"
            pDialog.ListFileName = "UserListe.lst"
            pDialog.ShowDialog()
            pDialog = Nothing
        End If
        Return True
    End Function

    ''' <summary>
    ''' Default-Layout anzeigen.
    ''' Falls keine Layout-Definitionen verhanden sind, wird das Haupt-Fenster (Liste) angezeigt.
    ''' </summary>
    Public Overrides Sub setDefaultLayout()
        DockPanel.Theme = wb_GlobalSettings.Theme
        UserListe.Show(DockPanel, DockState.DockLeft)
        UserListe.CloseButtonVisible = False
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
            Case "WinBack.wb_User_Liste"
                UserListe.CloseButtonVisible = False
                _DockPanelList.Add(UserListe)
                Return UserListe

            Case "WinBack.wb_User_Details"
                UserDetails = New wb_User_Details
                _DockPanelList.Add(UserDetails)
                Return UserDetails

            Case "WinBack.wb_User_Rechte"
                UserRechte = New wb_User_Rechte
                _DockPanelList.Add(UserRechte)
                Return UserRechte

            Case "WinBack.wb_User_GruppenRechte"
                UserGruppenRechte = New wb_User_GruppenRechte
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