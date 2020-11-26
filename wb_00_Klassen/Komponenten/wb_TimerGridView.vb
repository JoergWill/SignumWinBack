Imports System.Windows.Forms
Imports WinBack

Public Class wb_TimerGridView
    Inherits wb_ArrayGridView
    Public GridArray As Array

    Public Const COLSORT = 0
    Public Const COLTASK = 1
    Public Const COLSTRT = 2
    Public Const COLPRDE = 3
    Public Const COLSTAT = 4

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

    Public Sub RefreshGrid(ByVal xArray As ArrayList)
        'Daten in das lokale Array übertragen
        GridArray = xArray.ToArray
        FillGrid()
    End Sub

    Public Overrides Sub FillGrid()
        Try
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

                ' Daten ins DatagridView eintragen
                For r = 0 To MaxRowCount
                    With rows(r)
                        ' Zeileneigenschaften festlegen: Keine 'verschwindende' Zeile zulassen
                        .MinimumHeight = 20
                        ' Strich zwischen den Zeilen  
                        .DividerHeight = 0

                        .Cells(COLSORT).Value = GridArray(r).Sort
                        .Cells(COLTASK).Value = GridArray(r).Bezeichnung
                        .Cells(COLSTRT).Value = GridArray(r).sStartzeit
                        .Cells(COLPRDE).Value = GridArray(r).Periode
                        .Cells(COLSTAT).Value = GridArray(r).Status

                    End With
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
