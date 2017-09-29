Imports System.Drawing
Imports System.Windows.Forms

Public Class wb_SyncGridView
    Inherits wb_ArrayGridView
    Public GridArray As Array

    Const COLSORT = 0
    Const COLWBNR = 1
    Const COLWBBZ = 2
    Const COLWBGP = 3
    Const COLOSNR = 4
    Const COLOSBZ = 5
    Const COLOSGP = 6
    Const COLSTAT = 7

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

        'Spalten formatieren
        MyBase.Columns(COLWBNR).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        MyBase.Columns(COLWBBZ).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        MyBase.Columns(COLWBGP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        MyBase.Columns(COLOSNR).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        MyBase.Columns(COLOSBZ).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        MyBase.Columns(COLOSGP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        MyBase.Columns(COLSTAT).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

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

                .Cells(COLSORT).Value = GridArray(r).Sort
                .Cells(COLWBNR).Value = GridArray(r).wb_Nummer
                .Cells(COLWBBZ).Value = GridArray(r).wb_Bezeichnung
                .Cells(COLWBGP).Value = GridArray(r).wb_Gruppe
                .Cells(COLOSNR).Value = GridArray(r).os_Nummer
                .Cells(COLOSBZ).Value = GridArray(r).os_Bezeichnung
                .Cells(COLOSGP).Value = GridArray(r).os_Gruppe
                .Cells(COLSTAT).Value = GridArray(r).SyncOK
                .Cells(COLSTAT).ToolTipText = GridArray(r).ToolTipText
            End With
        Next
    End Sub

    Private Sub SyncCellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        If e.ColumnIndex = COLSTAT Then
            Select Case e.Value
                Case wb_Global.SyncState.OrgaBackErr, wb_Global.SyncState.WinBackErr
                    e.CellStyle.ForeColor = Color.Red
                Case wb_Global.SyncState.OK
                    e.CellStyle.ForeColor = Color.Green
            End Select
        End If
    End Sub

End Class
