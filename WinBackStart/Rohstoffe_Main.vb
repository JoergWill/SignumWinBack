Imports Signum.OrgaSoft.AddIn
Imports WeifenLuo.WinFormsUI.Docking
Public Class Rohstoffe_Main
    Public RohstoffListe As New wb_Rohstoffe_Liste
    Public RohstoffDetails As New wb_Rohstoffe_Details

    Private Sub SaveDockBarConfig()
        DockPanel.SaveAsXml(wb_Konfig.DockPanelPath & "Rohstoff.xml")
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml(wb_Konfig.DockPanelPath & "Rohstoff.xml", AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        RohstoffDetails.Show(DockPanel, DockState.DockTop)
        RohstoffDetails.CloseButtonVisible = False
        RohstoffListe.Show(DockPanel, DockState.DockLeft)
        RohstoffListe.CloseButtonVisible = False
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "RohstoffListe"
                Return RohstoffListe
            Case "RohstoffDetails"
                Return RohstoffDetails
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub User_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'alle erzeugten Fenster wieder schliessen
        RohstoffDetails.Close()
        RohstoffListe.Close()
    End Sub

    Private Sub User_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        wb_User.LoadGrpTexte()
        'Fenster laden
        LoadDockBarConfig()
    End Sub

End Class