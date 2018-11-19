Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking
Imports Infralution.Controls.VirtualTree

Public Class wb_Rohstoffe_Parameter
    Inherits DockContent

    Private _ParamHeadingDeltaStyle As New Infralution.Controls.StyleDelta
    Private _ParamAllergenDeltaStyle As New Infralution.Controls.StyleDelta
    Private _ParamChangedStyle As Infralution.Controls.Style
    Private _KomponParam As wb_KomponParam
    Private _ParamChanged

    ''' <summary>
    ''' Initialisierung - Laden der Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Rohstoffe_Parameter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo

        'Virtual-Tree Styles initialisieren - Fettdruck Überschrift Parameter-Sektionen
        _ParamHeadingDeltaStyle.Font = New Drawing.Font(VirtualTree.Columns(0).CellEvenStyle.Font, Drawing.FontStyle.Bold)
        _ParamHeadingDeltaStyle.HorzAlignment = Drawing.StringAlignment.Near

        'Virtual-Tree Styles initialisieren - Fettdruck Überschrift Parameter-Sektionen
        _ParamAllergenDeltaStyle.Font = New Drawing.Font(VirtualTree.Columns(0).CellEvenStyle.Font, Drawing.FontStyle.Regular)
        _ParamAllergenDeltaStyle.HorzAlignment = Drawing.StringAlignment.Center

        'Daten vom aktuellen Rohstoff anzeigen
        If RohStoff.Nr > 0 Then
            DetailInfo()
        End If
    End Sub

    Private Sub DetailInfo()
        'Virtual Tree anzeigen
        VirtualTree.DataSource = RohStoff.RootParameter
        'alle Zeilen aufklappen
        VirtualTree.RootRow.ExpandChildren(True)
    End Sub

    ''' <summary>
    ''' Eingabe-Feld wurde selektiert. (Mausklick oder Tastatur-Navigation)
    ''' Setzt die Parameter für den Editor: Format, Eingabe: Ober- und Untergrenze
    ''' 
    ''' Verhindert, dass einzelne Zellen markiert werden 
    ''' (Infralution Support): handle the SelectionChanging event and set Cancel to true. This prevents any selection occurring
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_SelectionChanging(sender As Object, e As SelectionChangingEventArgs) Handles VirtualTree.SelectionChanging
        'Daten aus dem aktuell ausgewählten Rezeptschritt
        _KomponParam = DirectCast(e.StartRow.Item, wb_KomponParam)

        'Einstellungen Editor
        Debug.Print("VirtualTree_SelectionChanging " & _KomponParam.Bezeichnung & " UG/OG/Format " & _KomponParam.eUG & "/" & _KomponParam.eOG & "/" & _KomponParam.eFormat)
        DirectCast(EnhEdit.Control, EnhEdit.EnhEdit).eFormat = _KomponParam.eFormat
        DirectCast(EnhEdit.Control, EnhEdit.EnhEdit).eOG = _KomponParam.eOG
        DirectCast(EnhEdit.Control, EnhEdit.EnhEdit).eUG = _KomponParam.eUG

        'Verhindert dass einzelne Zellen markiert werden
        e.Cancel = True
    End Sub

    ''' <summary>
    ''' Ausgabe VirtualTree formatieren. 
    ''' Überschriften (Parameter-Nummer gleich Null) werden fett formatiert
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_GetCellData(sender As Object, e As Infralution.Controls.VirtualTree.GetCellDataEventArgs) Handles VirtualTree.GetCellData
        'get the default binding for the given row And use it to populate the cell data
        Dim Binding As RowBinding = _VirtualTree.GetRowBinding(e.Row)
        Binding.GetCellData(e.Row, e.Column, e.CellData)

        'Datensatz aus der Zeile
        _KomponParam = DirectCast(e.Row.Item, wb_KomponParam)

        'Font einstellen - Überschrift
        If _KomponParam.ParamNr = 0 Then
            VirtualTree_SetFontStyle(e.CellData.EvenStyle)
            VirtualTree_SetFontStyle(e.CellData.OddStyle)
        End If

        'Soll/Istwert 
        If e.Column.Name = "ColWert" Then
            If _KomponParam.TypNr = wb_Global.ktParam.kt301 And wb_KomponParam301_Global.IsAllergen(_KomponParam.ParamNr) Then
                'Allergen-Informationen zentriert
                VirtualTree_SetFontAlignment(e.CellData.EvenStyle)
                VirtualTree_SetFontAlignment(e.CellData.OddStyle)
            End If

            'Sollwert - Editieren erlaubt
            Exit Sub
        End If

        'Alle anderen Spalten - Edit nicht erlaubt
        e.CellData.Editor = Nothing
    End Sub

    ''' <summary>
    ''' Wird aufgerufen, wenn der Wert geändert wurde (Edit)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_SetCellValue(sender As Object, e As SetCellValueEventArgs) Handles VirtualTree.SetCellValue
        Dim Binding As RowBinding = _VirtualTree.GetRowBinding(e.Row)

        'aktuell ausgewählten Rezeptschritt
        _KomponParam = DirectCast(e.Row.Item, wb_KomponParam)
        'geänderten Wert eintragen (nach Edit)
        Binding.SetCellValue(e.Row, e.Column, e.OldValue, e.NewValue)

        'Parameter wurde geändert
        _ParamChanged = True
    End Sub

    ''' <summary>
    ''' Setzt den Font.Style für die angegebene Zelle auf Fettdruck
    ''' Anzeige der Überschriften für die einzelen Parameter-Sektionen
    ''' </summary>
    ''' <param name="ColumnStyle"></param>
    Private Sub VirtualTree_SetFontStyle(ByRef ColumnStyle As Infralution.Controls.Style)
        'Überschriften der einzelnen Sektionen - Fettdruck ein
        _ParamChangedStyle = New Infralution.Controls.Style(ColumnStyle, _ParamHeadingDeltaStyle)
        ColumnStyle = _ParamChangedStyle
    End Sub

    Private Sub VirtualTree_SetFontAlignment(ByRef ColumnStyle As Infralution.Controls.Style)
        _ParamChangedStyle = New Infralution.Controls.Style(ColumnStyle, _ParamAllergenDeltaStyle)
        ColumnStyle = _ParamChangedStyle
    End Sub

End Class