﻿Imports WeifenLuo.WinFormsUI.Docking
Public Class XXService_Main
    Dim DkPnlPath As String = wb_GlobalSettings.DockPanelPath & "wbArtikel.xml"
    Public ServiceListe As New wb_Service_Liste

    Private Sub SaveDockBarConfig()
        DockPanel.SaveAsXml(DkPnlPath)
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml(DkPnlPath, AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        ServiceListe.Show(DockPanel, DockState.DockLeft)
        ServiceListe.CloseButtonVisible = False
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "ServiceListe"
                Return ServiceListe
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub User_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'alle erzeugten Fenster wieder schliessen
        ServiceListe.Close()
    End Sub

    Private Sub User_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        wb_User_Shared.LoadGrpTexte()
        'Fenster laden
        LoadDockBarConfig()
    End Sub

End Class