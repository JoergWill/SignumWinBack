Imports System.Windows.Forms

Public Class wb_ArrayGridViewBestand
    Inherits wb_ArrayGridView
    Public GridArray As Array
    Dim Bestand As wb_Global.WinBackBestand

    Const COLSORTNR = 0
    Const COLSORTNAM = 1
    Const COLSORTLFD = 2
    Const COLSORTDAT = 3
    Const COLSORTBST = 4
    Const COLSORTCRG = 5
    Const COLSORTVRF = 6

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
        MyBase.Columns(COLSORTLFD).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        MyBase.Columns(COLSORTDAT).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        MyBase.Columns(COLSORTBST).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        MyBase.Columns(COLSORTCRG).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        MyBase.Columns(COLSORTVRF).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

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
                'WinBack-Bestand aus dem Quell-Array
                Bestand = GridArray(r)
                .Cells(COLSORTNR).Value = Bestand.ArtikelNr
                .Cells(COLSORTNAM).Value = Bestand.Bezeichnung
                .Cells(COLSORTLFD).Value = Bestand.Lfd
                .Cells(COLSORTDAT).Value = Bestand.Datum
                .Cells(COLSORTBST).Value = Bestand.Bestand
                .Cells(COLSORTCRG).Value = Bestand.ChargenNr
                .Cells(COLSORTVRF).Value = Bestand.Vorfall
            End With
        Next
    End Sub

End Class
