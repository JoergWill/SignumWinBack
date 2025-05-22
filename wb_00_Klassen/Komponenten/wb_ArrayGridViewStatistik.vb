Imports System.Windows.Forms
Imports WinBack

Public Class wb_ArrayGridViewStatistik
    Inherits wb_ArrayGridView
    Public GridArray As Array

    Public Const COLSORT = 0
    Public Const COLNUMR = 1
    Public Const COLNAME = 2

    Public Sub New(ByVal xArray As ArrayList, ByVal sColNames As List(Of String), Optional ShowTooltips As Boolean = True)
        'Spalten-Überschriften
        ColNames = sColNames
        'Daten in das lokale Array übertragen
        GridArray = xArray.ToArray
        'Grid Grundeinstellungen
        _ShowTooltips = ShowTooltips
        'Grid initialisieren
        InitGrid()
        'Daten anzeigen und Editieren (Readonly = False)
        InitData(False)
    End Sub

    Public Overloads Sub FillGrid(ByVal xArray As ArrayList)
        'Daten in das lokale Array übertragen
        GridArray = xArray.ToArray
        'Grid anzeigen
        FillGrid()
    End Sub


    Public Overrides Sub FillGrid()
        'Spalten erstellen
        MyBase.FillColumns()

        ' Die Arraydaten werden in das GridView eingetragen
        Dim rows As DataGridViewRowCollection = MyBase.Rows
        Dim MaxRowCount As Integer = UBound(GridArray)

        ' Daten Löschen
        MyBase.Rows.Clear()
        MyBase.RowCount = 0

        ' Daten ins DatagridView eintragen
        If MaxRowCount >= 0 Then
            ' Die erforderliche Anzahl Zeilen in einem Rutsch erstellen:
            MyBase.Rows.Add(MaxRowCount + 1)

            For r = 0 To MaxRowCount
                With rows(r)
                    ' Zeileneigenschaften festlegen: Keine 'verschwindende' Zeile zulassen
                    .MinimumHeight = 20
                    ' Strich zwischen den Zeilen  
                    .DividerHeight = 0

                    .Cells(COLSORT).Value = GridArray(r).Nr
                    .Cells(COLNUMR).Value = GridArray(r).Nummer
                    .Cells(COLNAME).Value = GridArray(r).Bezeichnung
                End With
            Next
        End If
    End Sub
End Class
