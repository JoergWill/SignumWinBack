Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Nwt
    Inherits DockContent
    Dim nwtGrid As wb_ArrayGridViewKomponParam301

    Private Sub wb_Rohstoffe_Nwt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf NaehrwertInfo
        'Daten vom aktuellen Rohstoff anzeigen
        If RohStoff.Nr > 0 Then
            NaehrwertInfo()
        End If
    End Sub

    Public Sub NaehrwertInfo()
        'Deklarationsfelder
        tbDeklarationExtern.Text = RohStoff.DeklBezeichungExtern
        tbDeklarationIntern.Text = RohStoff.DeklBezeichungIntern

        'Daten im Grid anzeigen
        If nwtGrid IsNot Nothing Then
            nwtGrid.Dispose()
        End If
        nwtGrid = New wb_ArrayGridViewKomponParam301(RohStoff.ktTyp301.NwtTabelle)
        nwtGrid.BackgroundColor = Me.BackColor
        nwtGrid.GridLocation(pnl_Nwt)
        nwtGrid.PerformLayout()
    End Sub
End Class