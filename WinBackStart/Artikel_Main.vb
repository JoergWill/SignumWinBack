Imports Signum.OrgaSoft.AddIn
Imports WeifenLuo.WinFormsUI.Docking
Public Class Artikel_Main
    Public ArtikelListe As New wb_Artikel_Liste
    Public ArtikelDetails As New wb_Artikel_Details

    Private Sub SaveDockBarConfig()
        DockPanel.SaveAsXml(wb_Konfig.DockPanelPath & "wbArtikel.xml")
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml(wb_Konfig.DockPanelPath & "wbArtikel.xml", AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        ArtikelDetails.Show(DockPanel, DockState.DockTop)
        ArtikelDetails.CloseButtonVisible = False
        ArtikelListe.Show(DockPanel, DockState.DockLeft)
        ArtikelListe.CloseButtonVisible = False
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "ArtikelListe"
                Return ArtikelListe
            Case "ArtikelDetails"
                Return ArtikelDetails
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub User_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'alle erzeugten Fenster wieder schliessen
        ArtikelDetails.Close()
        ArtikelListe.Close()
    End Sub

    Private Sub User_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        'wb_User.LoadGrpTexte()
        'Fenster laden
        LoadDockBarConfig()
    End Sub

End Class