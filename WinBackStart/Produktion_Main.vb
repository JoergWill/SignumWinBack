Imports WeifenLuo.WinFormsUI.Docking
Public Class Produktion_Main
    Dim DkPnlPath As String = wb_GlobalSettings.DockPanelPath & "wbProduktion.xml"
    Public PlanungListe As New wb_Planung_Liste
    Public PlanungTeiler As New wb_Planung_Teiler

    Private Sub SaveDockBarConfig()
        DockPanel.SaveAsXml(DkPnlPath)
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml(DkPnlPath, AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        PlanungListe.Show(DockPanel, DockState.DockLeft)
        PlanungListe.CloseButtonVisible = False
        PlanungTeiler.Show(DockPanel, DockState.DockRight)
        PlanungTeiler.CloseButtonVisible = False
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "PlanungListe"
                Return PlanungListe
            Case "PlanungTeiler"
                Return PlanungTeiler
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub User_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'alle erzeugten Fenster wieder schliessen
        PlanungListe.Close()
        PlanungTeiler.Close()
    End Sub

    Private Sub User_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        wb_User_Shared.LoadGrpTexte()
        'Fenster laden
        LoadDockBarConfig()
    End Sub

End Class