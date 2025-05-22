Imports System.Windows.Forms

Public Class wb_ArrayGridViewSchnittstelle
    Inherits wb_ArrayGridView
    Public GridArray As Array

    Const COLNAME = 0
    Const COLINDX = 1
    Const COLDFLT = 2
    Const COLXPOS = 3
    Const COLXLEN = 4
    Const COLCALC = 5

    Public Sub New(ByVal xArray As List(Of wb_SchnittstelleFeld), ByVal sColNames As List(Of String), Optional ShowTooltips As Boolean = True)
        'Spalten-Überschriften
        ColNames = sColNames
        'Daten in das lokale Array übertragen
        GridArray = xArray.ToArray
        'Grid Grundeinstellungen
        _ShowTooltips = ShowTooltips
        'Grid initialisieren
        InitGrid()
        'Daten anzeigen - Editieren erlaubt. Das Readonly-Flag wird in FillGrid für die einzelnen Spalten gesetzt.
        InitData(False)
    End Sub

    Public Overrides Sub FillGrid()

        'Spalten erstellen
        MyBase.FillColumns()

        ' Die Arraydaten werden in das GridView eingetragen
        Dim rows As DataGridViewRowCollection = MyBase.Rows
        Dim MaxRowCount As Integer = UBound(GridArray)

        'Spalten formatieren
        MyBase.Columns(COLNAME).ReadOnly = True

        MyBase.Columns(COLINDX).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        MyBase.Columns(COLXPOS).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        MyBase.Columns(COLXLEN).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' Daten Löschen
        MyBase.Rows.Clear()
        MyBase.RowCount = 0

        If MaxRowCount > 0 Then
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
                    .Cells(COLNAME).Value = CType(GridArray(r), wb_SchnittstelleFeld).Name
                    .Cells(COLINDX).Value = CType(GridArray(r), wb_SchnittstelleFeld).Idx
                    .Cells(COLDFLT).Value = CType(GridArray(r), wb_SchnittstelleFeld).DefaultValue
                    .Cells(COLXPOS).Value = CType(GridArray(r), wb_SchnittstelleFeld).Pos
                    .Cells(COLXLEN).Value = CType(GridArray(r), wb_SchnittstelleFeld).Len
                    .Cells(COLCALC).Value = CType(GridArray(r), wb_SchnittstelleFeld).Calc
                End With
            Next
        End If
    End Sub

End Class
