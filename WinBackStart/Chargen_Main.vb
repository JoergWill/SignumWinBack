Imports Signum.OrgaSoft.AddIn
Imports WeifenLuo.WinFormsUI.Docking
Public Class Chargen_Main
    Public ChargenListe As New wb_Chargen_Liste

    Private Sub SaveDockBarConfig()
        DockPanel.SaveAsXml(wb_Konfig.DockPanelPath & "Chargen.xml")
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml(wb_Konfig.DockPanelPath & "Chargen.xml", AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        ChargenListe.Show(DockPanel, DockState.DockLeft)
        ChargenListe.CloseButtonVisible = False
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "ChargenListe"
                Return ChargenListe
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub User_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'alle erzeugten Fenster wieder schliessen
        ChargenListe.Close()
    End Sub

    Private Sub User_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        wb_User.LoadGrpTexte()
        'Fenster laden
        LoadDockBarConfig()
    End Sub

End Class