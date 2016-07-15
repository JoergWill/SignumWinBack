Imports WinBack.wb_User_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class User_Main
    Public UserListe As New wb_User_Liste
    Public UserDetails As New wb_User_Details
    Private UserRechte As New wb_User_Rechte

    Private Sub SaveDockBarConfig()
        DockPanel.SaveAsXml(wb_Konfig.DockPanelPath & "wbUser.xml")
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml(wb_Konfig.DockPanelPath & "wbUser.xml", AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        UserListe.Show(DockPanel, DockState.DockLeft)
        UserListe.CloseButtonVisible = False
        UserDetails.Show(DockPanel, DockState.DockTop)
        UserDetails.CloseButtonVisible = False
        UserRechte.Show(DockPanel, DockState.Document)
        UserRechte.CloseButtonVisible = False
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As DockContent
        Select Case persistString
            Case "UserListe"
                Return UserListe
            Case "UserDetails"
                Return UserDetails
            Case "UserRechte"
                Return UserRechte
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub User_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'alle erzeugten Fenster wieder schliessen
        UserDetails.Close()
        UserListe.Close()
        UserRechte.Close()
    End Sub

    Private Sub User_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Fenster laden
        LoadDockBarConfig()
    End Sub

    ''' <summary>
    ''' Button "Neuen Benutzer anlegen" aus WinBack(Main)
    ''' In der Datenbank wird ein neuer leerer Eintrag erzeugt. Danach wird die User-Liste aktualisiert.
    ''' </summary>
    Public Sub BtnUserNew()
        'neuen (Dummy)Datensatz anlegen
        User.AddNew()
        'Mitarbeiter-Liste aktualisieren
        UserListe.RefreshData()
        'auf den neuen Datensatz positionieren (lfd.Nummer = -1)
        UserListe.SelectData(1, wb_Global.NewUserPass)
    End Sub

    ''' <summary>
    ''' Button "Benutzer löschen" aus WinBack(Main)
    ''' Löscht den aktuellen Benutzer. Danach wird die User-Liste aktualisiert
    ''' </summary>
    Public Sub BtnUserDelete()
        User.Delete(User.Passwort)
        UserListe.RefreshData()
    End Sub

End Class