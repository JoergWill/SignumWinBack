Imports WeifenLuo.WinFormsUI.Docking
Public Class Rezepte_Main
    Dim DkPnlPath As String = wb_GlobalSettings.DockPanelPath & "wbRezepte.xml"

    Public RezeptListe As New wb_Rezept_Liste
    Public RezeptDetails As New wb_Rezept_Details
    Public RezeptHinweise As New wb_Rezept_Hinweise
    Public RezeptHistorie As New wb_Rezept_Historie
    Public Rezept_Shared As New wb_Rezept_Shared


    Private Sub SaveDockBarConfig()
        Try
            DockPanel.SaveAsXml(DkPnlPath)
        Catch ex As Exception
            MsgBox("Fehler beim Sichern der Konfiguration: " & ex.Message.ToString)
            'TODO diese Konstruktion bei allen DockPanel.SaveAsXml einbauen
            'Fehler bei Win64 - Ohne TryCatch wird das Programm nicht geschlossen !!
        End Try
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml(DkPnlPath, AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        RezeptHinweise.Show(DockPanel, DockState.Document)
        RezeptHinweise.CloseButtonVisible = False
        RezeptHistorie.Show(DockPanel, DockState.Document)
        RezeptHistorie.CloseButtonVisible = False
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
            Case "RezeptHinweise"
                Return RezeptHinweise
            Case "RezeptHistorie"
                Return RezeptHistorie
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub Rezepte_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'HashTable mit der Übersetzung der Varianten-Nummer zu Rezept-Varianten-Bezeichnung
        'HashTable mit der Übersetzung der Liniengruppen-Nummer zu Liniengruppen-Bezeichnung
        'wb_Rezept_Shared.LoadLinienGruppenTexte()

        'Fenster laden
        LoadDockBarConfig()
    End Sub

    Private Sub Rezepte_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'alle erzeugten Fenster wieder schliessen
        RezeptDetails.Close()
        RezeptListe.Close()
        RezeptHinweise.Close()
        RezeptHistorie.Close()
    End Sub
End Class