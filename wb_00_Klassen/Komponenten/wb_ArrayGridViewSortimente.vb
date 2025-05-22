Imports System.Windows.Forms

Public Class wb_ArrayGridViewSortimente
    Inherits wb_ArrayGridView
    Public GridArray As Array
    Dim Sortiment As wb_Global.OrgaBackSortiment

    Const COLSORTNR = 0
    Const COLSORTNAM = 1
    Const COLSORTFIL = 2

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
        MyBase.Columns(COLSORTNR).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        MyBase.Columns(COLSORTNAM).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        MyBase.Columns(COLSORTFIL).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

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
                'OrgaBack-Sortimente aus dem Quell-Array
                Sortiment = GridArray(r)
                .Cells(COLSORTNR).Value = Sortiment.Srt
                .Cells(COLSORTNAM).Value = Sortiment.SName
                .Cells(COLSORTFIL).Value = Sortiment.FName
            End With
        Next
    End Sub
End Class
