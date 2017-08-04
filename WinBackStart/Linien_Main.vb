Imports WeifenLuo.WinFormsUI.Docking

Public Class Linien_Main
    Dim DkPnlPath As String = wb_GlobalSettings.DockPanelPath & "wbLinien.xml"

    Public LinienListe As New wb_Linien_Liste
    Private LinienDetails As New wb_Linien_Details

    Private Sub SaveDockBarConfig()
        DockPanel.SaveAsXml(DkPnlPath)
    End Sub

    Private Sub LoadDockBarConfig()
        Try
            DockPanel.LoadFromXml(DkPnlPath, AddressOf wbBuildDocContent)
        Catch ex As Exception
        End Try

        LinienDetails.Show(DockPanel, DockState.DockTop)
        LinienDetails.CloseButtonVisible = False
        LinienListe.Show(DockPanel, DockState.DockLeft)
        LinienListe.CloseButtonVisible = False
    End Sub

    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "LinienListe"
                Return LinienListe
            Case "LinenDetails"
                Return LinienDetails
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub Linien_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        SaveDockBarConfig()
        'Linien-Info in Datei sichern
        LinienListe.SaveItems()
        'alle erzeugten Fenster wieder schliessen
        LinienDetails.Close()
        LinienListe.Close()
    End Sub

    Private Sub Linien_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDockBarConfig()
    End Sub

    Public Sub BtnLinienNew()
        LinienListe.AddItems("", "Neuer Eintrag")
        LinienListe.SelectLastItem()
        LinienDetails.DetailInfo()
        LinienDetails.DetailEdit()
    End Sub

    Public Sub BtnLinienRemove()
        LinienListe.RemoveItem()
    End Sub

    Public Sub btnLinienAutoInstall()
        LinienListe.AddFromDataBase()
    End Sub

End Class