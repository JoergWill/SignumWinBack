Imports Signum.OrgaSoft.AddIn
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

End Class