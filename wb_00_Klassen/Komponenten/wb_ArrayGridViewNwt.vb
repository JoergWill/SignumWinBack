Imports System.Drawing
Imports System.Windows.Forms

Public Class wb_ArrayGridViewNwt
    Inherits wb_ArrayGridView
    Public GridArray As Array

    Const COLSORT = 0   'sortieren nach dieser Spalte 
    Const COLBZNG = 1   'Rohstoff-Bezeichnung aus der Cloud
    Const COLLIEF = 2   'Rohstoff-Lieferant aus der Cloud
    Const COLDKLR = 3   'Rohstoff-Deklarationsbezeichnung aus der Cloud
    Const COLEANC = 4   'Rohstoff-EAN-Nummer (OpenFoodFacts)

    Public Sub New(ByVal xArray As ArrayList, ByVal sColNames As List(Of String), Optional ShowTooltips As Boolean = True)
        'Spalten-Überschriften
        ColNames = sColNames
        'Daten in das lokale Array übertragen
        GridArray = xArray.ToArray
        'Grid Grundeinstellungen
        _ShowTooltips = ShowTooltips
        'Grid initialisieren
        InitGrid()
        'Daten anzeigen 
        InitData()
    End Sub

    Public Overrides Sub FillGrid()
        'Spalten erstellen
        MyBase.FillColumns()

        ' Die Arraydaten werden in das GridView eingetragen
        Dim rows As DataGridViewRowCollection = MyBase.Rows
        Dim MaxRowCount As Integer = UBound(GridArray)

        'Spalten formatieren
        MyBase.Columns(COLBZNG).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        MyBase.Columns(COLLIEF).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        MyBase.Columns(COLDKLR).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        MyBase.Columns(COLEANC).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        ' Daten Löschen
        MyBase.Rows.Clear()
        MyBase.RowCount = 0

        If MaxRowCount >= 0 Then
            ' Die erforderliche Anzahl Zeilen in einem Rutsch erstellen:
            MyBase.Rows.Add(MaxRowCount + 1)

            ' Daten ins DatagridView eintragen
            For r = 0 To MaxRowCount
                With rows(r)
                    ' Zeileneigenschaften festlegen: Keine 'verschwindende' Zeile zulassen
                    .MinimumHeight = 20
                    ' Strich zwischen den Zeilen  
                    .DividerHeight = 0

                    .Cells(COLSORT).Value = DirectCast(GridArray(r), wb_Global.NwtCloud).id
                    .Cells(COLBZNG).Value = DirectCast(GridArray(r), wb_Global.NwtCloud).name
                    .Cells(COLLIEF).Value = DirectCast(GridArray(r), wb_Global.NwtCloud).lieferant
                    .Cells(COLDKLR).Value = DirectCast(GridArray(r), wb_Global.NwtCloud).deklarationsname
                    .Cells(COLEANC).Value = DirectCast(GridArray(r), wb_Global.NwtCloud).ean
                End With
            Next
        End If

    End Sub
End Class
