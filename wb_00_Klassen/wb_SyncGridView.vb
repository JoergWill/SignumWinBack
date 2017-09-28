Imports System.Windows.Forms

Public Class wb_SyncGridView
    Inherits wb_ArrayGridView
    Public GridArray As Array

    Public Sub New(ByVal xArray As ArrayList, ByVal sColNames As List(Of String), Optional ShowTooltips As Boolean = True)
        'Spalten-Überschriften
        ColNames = sColNames
        'Daten in das lokale Array übertragen
        GridArray = xArray.ToArray
        'Grid Grundeistellungen
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

        ' Daten Löschen
        MyBase.Rows.Clear()
        MyBase.RowCount = 0

        ' Die erforderliche Anzahl Zeilen in einem Rutsch erstellen:
        MyBase.Rows.Add(MaxRowCount)

        ' Daten ins DatagridView eintragen
        For r = 0 To MaxRowCount - 1
            With rows(r)
                ' Zeileneigenschaften festlegen: Keine 'verschwindende' Zeile zulassen
                .MinimumHeight = 20
                ' Strich zwischen den Zeilen  
                .DividerHeight = 0

                .Cells(0).Value = GridArray(r).Sort
                .Cells(1).Value = GridArray(r).wb_Nummer
                .Cells(2).Value = GridArray(r).wb_Bezeichnung
                .Cells(3).Value = GridArray(r).os_Nummer
                .Cells(4).Value = GridArray(r).os_Bezeichnung
                .Cells(5).Value = GridArray(r).SyncOK
            End With
        Next
    End Sub

End Class
