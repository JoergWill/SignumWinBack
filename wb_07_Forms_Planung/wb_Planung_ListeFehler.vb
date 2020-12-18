Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Planung_ListeFehler
    Inherits DockContent
    Dim WithEvents ProdPlanErrorGrid As wb_ArrayGridViewErrorList
    Dim sColNames As New List(Of String)

    Private Sub wb_Planung_ListeFehler_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Fehler anzeigen
        'RefreshData(Nothing)
    End Sub

    Public Sub RefreshData(sender As Object)
        'Cursor umschalten
        Me.Cursor = Cursors.WaitCursor

        'Spaltenüberschriften
        sColNames.Clear()
        sColNames.AddRange({"ArtikelNr", "Bezeichnung", "&Fehler"})

        'falls schon Daten vorhanden sind, löschen
        If ProdPlanErrorGrid IsNot Nothing Then
            ProdPlanErrorGrid.Dispose()
            ProdPlanErrorGrid = Nothing
        End If

        'Daten im Grid anzeigen
        ProdPlanErrorGrid = New wb_ArrayGridViewErrorList(wb_Planung_Shared.ErrorList, sColNames)
        ProdPlanErrorGrid.ScrollBars = ScrollBars.Vertical
        ProdPlanErrorGrid.BackgroundColor = Me.BackColor
        ProdPlanErrorGrid.GridLocation(pnlErrorList)
        ProdPlanErrorGrid.PerformLayout()
        ProdPlanErrorGrid.Refresh()

        'Cursor wieder umschalten
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Doppelklick auf die Fehlerzeile öffnet den entsprechenden Artikel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ProdPlanErrorGrid_DoubleClick(sender As Object, e As EventArgs) Handles ProdPlanErrorGrid.DoubleClick
        'Die Artikelnummer steht in Spalte 0
        Dim ArtikelNr As String = ProdPlanErrorGrid.CurrentRow.Cells.Item(0).Value

        'Artikel-Details in Dialog-Fenster anzeigen
        Dim FehlerArtikelDetails As New wb_Planung_ListeFehlerArtikel(ArtikelNr, ProdPlanErrorGrid.CurrentRow.Index)
        'Lesen der Artikeldaten war erfolgreich
        If FehlerArtikelDetails.ReadOK Then
            'Detail-Fenster anzeigen
            FehlerArtikelDetails.ShowDialog()
        Else
            MsgBox("Artikel in WinBack nicht gefunden !", MsgBoxStyle.Critical, "Fehler beim Verarbeiten der Bestellungen")
        End If
    End Sub

End Class