Imports WeifenLuo.WinFormsUI.Docking
Public Class Artikel_Main

    'Default-Fenster
    Public ArtikelListe As New wb_Artikel_Liste
    Public ArtikelDetails As New wb_Artikel_Details

    ''' <summary>
    ''' Eindeutiger Name für die Basis-Form. 
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormName As String
        Get
            Me.Tag = "Rohstoffe"
            Return "Rohstoffe"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        ArtikelListe.Show(DockState.DockTop)
        ArtikelDetails.Show(wbDockPanel, DockState.DockLeft)
    End Sub

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "WinBack.wb_ArtikelListe"
                Return ArtikelListe
            Case "WinBack.wb_ArtikelDetails"
                Return ArtikelDetails
            Case Else
                Return Nothing
        End Select
    End Function

End Class