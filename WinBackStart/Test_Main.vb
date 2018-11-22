Imports Infralution.Controls.VirtualTree

Public Class Test_Main
    Implements IMainMenu
    Dim Rezept As wb_Rezept

    Private _RezeptSchritt As wb_Rezeptschritt = Nothing    'aktuelle ausgewählter Rezeptschritt (Popup)

#Region "MainForm"
    Public Property DkPnlConfigFileName As String Implements IMainMenu.DkPnlConfigFileName
        Get
            Return ""
        End Get
        Set(value As String)
        End Set
    End Property

    Public Function ExecuteCmd(Cmd As String, Prm As String) As Boolean Implements IMainMenu.ExecuteCmd
        Debug.Print("ExecuteCmd " & Cmd & " / " & Prm)
        Return True
    End Function
#End Region

    Private Sub Test_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Rezeptkopf und Rezeptschritte aktuell (winback) laden
        Rezept = New wb_Rezept(2691, Nothing, 1)

        'Virtual Tree anzeigen
        VirtualTree.DataSource = Rezept.RootRezeptSchritt
        'alle Zeilen aufklappen
        VirtualTree.RootRow.ExpandChildren(True)

    End Sub


    Private Sub VirtualTree_GetCellData(sender As Object, e As GetCellDataEventArgs) Handles VirtualTree.GetCellData
        'get the default binding for the given row And use it to populate the cell data
        Dim Binding As RowBinding = _VirtualTree.GetRowBinding(e.Row)
        Binding.GetCellData(e.Row, e.Column, e.CellData)

        'aktuell ausgewählten Rezeptschritt merken (Popup)
        _RezeptSchritt = DirectCast(e.Row.Item, wb_Rezeptschritt)

        'Edit Bezeichnungs-Text
        If e.Column.Name = "ColBezeichnung" And wb_Functions.TypeIstText(_RezeptSchritt.Type) Then
            Exit Sub
        End If

        'Edit Sollwert
        If e.Column.Name = "ColSollwert" And (wb_Functions.TypeIstSollMenge(_RezeptSchritt.Type, 1) Or wb_Functions.TypeIstSollWert(_RezeptSchritt.Type, 3)) Then
            Exit Sub
        End If

        'Edit nicht erlaubt
        e.CellData.Editor = Nothing
    End Sub

End Class