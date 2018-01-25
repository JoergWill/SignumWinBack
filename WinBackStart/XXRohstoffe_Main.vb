Imports WeifenLuo.WinFormsUI.Docking
Public Class XXRohstoffe_Main
    Dim DkPnlPath As String = wb_GlobalSettings.DockPanelPath & "wbRohstoff.xml"

    Public RohstoffListe As New wb_Rohstoffe_Liste
    Public RohstoffDetails As New wb_Rohstoffe_Details
    Public RohstoffVerwendung As New wb_Rohstoffe_Verwendung
    Public RohstoffParameter As New wb_Rohstoffe_Parameter

    Private Sub SaveDockBarConfig()
        DockPanel.SaveAsXml(DkPnlPath)
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml(DkPnlPath, AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        RohstoffDetails.Show(DockPanel, DockState.DockTop)
        RohstoffDetails.CloseButtonVisible = False
        RohstoffVerwendung.Show(DockPanel, DockState.Document)
        RohstoffVerwendung.CloseButtonVisible = False
        RohstoffParameter.Show(DockPanel, DockState.Document)
        RohstoffParameter.CloseButtonVisible = False
        RohstoffListe.Show(DockPanel, DockState.DockLeft)
        RohstoffListe.CloseButtonVisible = False
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "RohstoffListe"
                Return RohstoffListe
            Case "RohstoffDetails"
                Return RohstoffDetails
            Case "RohstoffVerwendung"
                Return RohstoffVerwendung
            Case "RohstoffParameter"
                Return RohstoffParameter
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub Rohstoffe_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Anzeige sichern
        SaveDockBarConfig()
        'alle erzeugten Fenster wieder schliessen
        RohstoffDetails.Close()
        RohstoffVerwendung.Close()
        RohstoffListe.Close()
        RohstoffParameter.Close()
    End Sub

    Private Sub Rohstoffe_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        wb_Rohstoffe_Shared.Load_RohstoffTables()
        'Fenster laden
        LoadDockBarConfig()
    End Sub

End Class