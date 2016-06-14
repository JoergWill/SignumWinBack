Imports Signum.OrgaSoft.AddIn
Imports WeifenLuo.WinFormsUI.Docking
Public Class Rezepte_Main
    Public RezeptListe As New wb_Rezept_Liste
    Public RezeptDetails As New wb_Rezept_Details

    Private Sub SaveDockBarConfig()
        DockPanel.SaveAsXml("wbRezepte.xml")
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml("wbRezepte.xml", AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        RezeptDetails.Show(DockPanel, DockState.DockTop)
        RezeptDetails.CloseButtonVisible = False
        RezeptListe.Show(DockPanel, DockState.DockLeft)
        RezeptListe.CloseButtonVisible = False
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "RezeptListe"
                Return RezeptListe
            Case "RezeptDetails"
                Return RezeptDetails
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub User_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'alle erzeugten Fenster wieder schliessen
        RezeptDetails.Close()
        RezeptListe.Close()
    End Sub

    Private Sub User_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        wb_User.LoadGrpTexte()
        'Fenster laden
        LoadDockBarConfig()
    End Sub

End Class