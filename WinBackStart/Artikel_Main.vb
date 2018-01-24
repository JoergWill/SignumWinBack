Imports WeifenLuo.WinFormsUI.Docking

Public Class Artikel_Main

    Public ArtikelListe As New wb_Artikel_Liste
    Public ArtikelDetails As wb_Artikel_Details

    Public Overrides Function ExtendedCmd(Cmd As String, Prm As String) As Boolean
        Select Case Cmd
            Case "OPENDETAILS"
                ArtikelDetails = New wb_Artikel_Details
                ArtikelDetails.Show(DockPanel, DockState.DockLeft)
                Return True
            Case Else
                Return False
        End Select
    End Function

    Public Overrides Sub setDefaultLayout()
        ArtikelListe.Show(DockPanel, DockState.DockLeft)
        ArtikelListe.CloseButtonVisible = False
        WinBack.LayoutFilename = "Default"
    End Sub

    Public Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "WinBack.wb_Artikel_Liste"
                ArtikelListe.CloseButtonVisible = False
                _DockPanelList.Add(ArtikelListe)
                Return ArtikelListe

            Case "WinBack.wb_Artikel_Details"
                ArtikelDetails = New wb_Artikel_Details
                _DockPanelList.Add(ArtikelDetails)
                Return ArtikelDetails

            Case Else
                Return Nothing
        End Select
    End Function

    Public Overrides Sub FormOpen(Sender As Object, e As EventArgs)

    End Sub

    Public Overrides Sub FormClose(Sender As Object, e As FormClosedEventArgs)
        'alle erzeugten Fenster wieder schliessen
        ArtikelDetails.Close()
        ArtikelListe.Close()
    End Sub

End Class