Imports Signum.OrgaSoft.AddIn
Imports WeifenLuo.WinFormsUI.Docking
Public Class Produktion_Main
    Public ProduktionListe As New wb_Planung_Liste

    Private Sub SaveDockBarConfig()
        DockPanel.SaveAsXml("wbProduktion.xml")
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml("wbProduktion.xml", AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        ProduktionListe.Show(DockPanel, DockState.DockLeft)
        ProduktionListe.CloseButtonVisible = False
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "ProduktionListe"
                Return ProduktionListe
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub User_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'alle erzeugten Fenster wieder schliessen
        ProduktionListe.Close()
    End Sub

    Private Sub User_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        wb_User.LoadGrpTexte()
        'Fenster laden
        LoadDockBarConfig()
    End Sub

End Class