Imports System.Windows.Forms

Public Class wb_ArrayGridViewErrorList
    Inherits wb_ArrayGridView
    Public GridArray As Array
    Private ProdPlanErrZeile As wb_ProduktionPlanungError

    Const COLANR = 0
    Const COLBEZ = 1
    Const COLERR = 2

    Public Sub New(ByVal xArray As List(Of wb_ProduktionPlanungError), ByVal sColNames As List(Of String), Optional ShowTooltips As Boolean = True)
        'Spalten-Überschriften
        ColNames = sColNames
        'Daten sind in wb_Produktion_Shared abglegt
        GridArray = xArray.ToArray
        'Grid Grundeinstellungen
        _ShowTooltips = ShowTooltips
        'Font
        MyBase.wbFont = New Drawing.Font("Microsoft Sans Serif", 8)
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
        MyBase.Columns(COLANR).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        MyBase.Columns(COLBEZ).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        MyBase.Columns(COLERR).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        ' Daten Löschen
        MyBase.Rows.Clear()
        MyBase.RowCount = 0

        ' Die erforderliche Anzahl Zeilen in einem Rutsch erstellen:
        If MaxRowCount >= 0 Then
            MyBase.Rows.Add(MaxRowCount + 1)

            ' Daten ins DatagridView eintragen
            For r = 0 To MaxRowCount
                With rows(r)
                    ' Zeileneigenschaften festlegen: Keine 'verschwindende' Zeile zulassen
                    .MinimumHeight = 20
                    ' Strich zwischen den Zeilen  
                    .DividerHeight = 0
                    'WinBack-ProdPlanFehler aus dem Quell-Array
                    ProdPlanErrZeile = GridArray(r)
                    .Cells(COLANR).Value = ProdPlanErrZeile.ArtikelNummer
                    .Cells(COLBEZ).Value = ProdPlanErrZeile.Artikelbezeichnung
                    .Cells(COLERR).Value = ProdPlanErrZeile.ChrTeilerResultToString
                End With
            Next
        End If
    End Sub

End Class
