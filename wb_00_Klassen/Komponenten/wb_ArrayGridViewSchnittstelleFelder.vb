Imports System.Windows.Forms

Public Class wb_ArrayGridViewSchnittstelleFelder
    Inherits wb_ArrayGridView
    Private _xArray As List(Of wb_SchnittstelleFeld)

    Public Sub New(ByRef xArray As List(Of wb_SchnittstelleFeld), ByVal sColNames As List(Of String), Optional ShowTooltips As Boolean = True)
        'Grid kopieren
        _xArray = xArray
        'Spalten-Überschriften
        ColNames = sColNames
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
        Dim MaxRowCount As Integer = _xArray.Count - 1

        'Spalten formatieren
        ' Daten Löschen
        MyBase.Rows.Clear()
        MyBase.RowCount = 0

        ' Die erforderliche Anzahl Zeilen in einem Rutsch erstellen:
        MyBase.Rows.Add(MaxRowCount + 1)

        ' Daten ins DatagridView eintragen
        For r = 0 To MaxRowCount
            With rows(r)
                ' Zeileneigenschaften festlegen: Keine 'verschwindende' Zeile zulassen
                .MinimumHeight = 20
                ' Strich zwischen den Zeilen  
                .DividerHeight = 0

                'alle Spalten
                .Cells(0).Value = _xArray(r).Name
            End With
        Next
    End Sub

End Class
