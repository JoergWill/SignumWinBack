Imports System.Drawing
Imports System.Windows.Forms

Public Class wb_ArrayGridViewSchnittstelleVorschau
    Inherits wb_ArrayGridView
    Private GridArray As Array
    Private cbHeader As ComboBox
    Private cbHeaders As New List(Of ComboBox)
    Private _Width As Integer
    Private _Height As Integer

    Public Event cbIndexChanged(Spalte As Integer, Index As Integer)

    Public ReadOnly Property AutoScrollSize As Size
        Get
            Dim s As New Size(_Width, Me.Height)
            Return s
        End Get
    End Property

    Public Sub New(ByRef xArray As Array, ByVal sColNames As List(Of String), Optional ShowTooltips As Boolean = True)
        'Spalten-Überschriften
        ColNames = sColNames
        For i = 0 To ColNames.Count - 1
            'neue Combo-Box (Spaltenüberschrift - Auswahl des Tabellenfeldes)
            cbHeader = GetDefaultComboBox(i)
            'Liste aller Combo-Boxen
            cbHeaders.Add(cbHeader)
            'an das GridView binden
            Me.Controls.Add(cbHeader)
        Next
        'Daten in das lokale Array übertragen
        GridArray = xArray
        'Grid Grundeinstellungen
        _ShowTooltips = ShowTooltips
        'Grid initialisieren
        InitGrid()
        'Daten anzeigen - Editieren erlaubt. Das Readonly-Flag wird in FillGrid für die einzelnen Spalten gesetzt.
        InitData(False)
    End Sub

    Private Function GetDefaultComboBox(Index As Integer) As ComboBox
        cbHeader = New ComboBox
        cbHeader.DropDownStyle = ComboBoxStyle.DropDownList
        Dim i As Integer = 0
        For Each s In ColNames
            cbHeader.Items.Add(s)
        Next
        'der Index wird im Tag zwischengespeichert
        cbHeader.Tag = Index
        'Default-Überschrift aus Fxxxx
        cbHeader.SelectedIndex = Index
        'TabellenFeld wird geändert
        AddHandler cbHeader.SelectedIndexChanged, AddressOf MySelectedIndexChanged
        Return cbHeader
    End Function

    Public Overrides Sub FillGrid()

        'Spalten erstellen
        MyBase.FillColumns()

        ' Die Arraydaten werden in das GridView eingetragen
        Dim rows As DataGridViewRowCollection = MyBase.Rows
        Dim MaxRowCount As Integer = UBound(GridArray)

        ' Daten Löschen
        MyBase.Rows.Clear()
        MyBase.RowCount = 0

        If MaxRowCount > 0 Then
            ' Die erforderliche Anzahl Zeilen in einem Rutsch erstellen
            MyBase.Rows.Add(MaxRowCount + 1)

            ' Daten ins DatagridView eintragen
            For r = 0 To MaxRowCount
                With rows(r)
                    ' Zeileneigenschaften festlegen: Keine 'verschwindende' Zeile zulassen
                    .MinimumHeight = 20
                    ' Strich zwischen den Zeilen  
                    .DividerHeight = 0
                    'wenn Daten vorhanden sind (Einlesen der Vorschau-Daten)
                    If GridArray(r) IsNot Nothing Then
                        'Der Index beim Import startet bei Eins
                        For i = 1 To GridArray(r).length - 1
                            If GridArray(r)(i) IsNot Nothing Then
                                .Cells(i - 1).Value = GridArray(r)(i).ToString
                            End If
                        Next
                    End If
                End With
            Next
        End If

    End Sub

    Public Sub ColWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Me.ColumnWidthChanged
        Dim x As Integer = 0
        Dim idx As Integer

        'Schleife über alle Spalten(Überschriften)
        For Each c In cbHeaders
            idx = Int(c.Tag)
            c.Location = New Point(x, 0)
            c.Width = Me.Columns(idx).Width + 1
            x = x + Me.Columns(idx).Width
        Next

        'Gesamtbreite speichern
        _Width = x
    End Sub

    ''' <summary>
    ''' Ein Tabellenfeld (Combo-Box im Header des GridArray) wurde geändert.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Sub MySelectedIndexChanged(sender As Object, e As EventArgs)
        'Debug.Print(CType(sender, ComboBox).Tag & " " & CType(sender, ComboBox).SelectedIndex)
        'Combo-Box in Spalte x (Grid) - Neuer Index Idx (TabelleFeld in Fxxxx)
        RaiseEvent cbIndexChanged(CType(sender, ComboBox).Tag + 1, CType(sender, ComboBox).SelectedIndex + 1)
    End Sub

    Public Shadows Sub Dispose() Handles Me.Disposed
        For Each c In cbHeaders
            RemoveHandler c.SelectedIndexChanged, AddressOf MySelectedIndexChanged
        Next
        MyBase.Dispose()
    End Sub
End Class
