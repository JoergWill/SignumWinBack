Imports System.Windows.Forms

Public Class wb_ArrayGridViewDruckAuftrag
    Inherits wb_ArrayGridView
    Public GridArray As Array

    Const COLIDX = 0
    Const COLDRK = 1
    Const COLBEZ = 2
    Const COLKOM = 3
    Const COLSDT = 4

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
        InitData(False)
    End Sub

    Public Overrides Sub FillGrid()

        'Spalten erstellen
        MyBase.FillColumns()

        'Spaltenbreiten festlegen
        Me.Columns(COLDRK).Width = 10
        Me.Columns(COLKOM).Width = 10
        Me.Columns(COLSDT).Width = 10
        Me.Columns(COLBEZ).ReadOnly = True

        'Die Arraydaten werden in das GridView eingetragen
        Dim rows As DataGridViewRowCollection = MyBase.Rows
        Dim MaxRowCount As Integer = UBound(GridArray)

        'Daten Löschen
        MyBase.Rows.Clear()
        MyBase.RowCount = 0

        'Die erforderliche Anzahl Zeilen in einem Rutsch erstellen:
        If MaxRowCount > 0 Then
            MyBase.Rows.Add(MaxRowCount + 1)
            Dim s As wb_Global.wb_LinienGruppe

            'Daten ins DatagridView eintragen
            For r = 0 To MaxRowCount
                With rows(r)
                    'Zeileneigenschaften festlegen: Keine 'verschwindende' Zeile zulassen
                    .MinimumHeight = 20
                    'Strich zwischen den Zeilen  
                    .DividerHeight = 0

                    'alle Spalten
                    s = GridArray(r)
                    .Cells(COLIDX).Value = s.LinienGruppe
                    .Cells(COLDRK).Value = s.bDrucken
                    .Cells(COLBEZ).Value = s.Bezeichnung
                    .Cells(COLKOM).Value = s.bKommentar
                    .Cells(COLSDT).Value = s.bSonderText
                End With
            Next
        End If
    End Sub

    ''' <summary>
    ''' Flag setzen/rücksetzen.
    ''' Wenn die Zeile schon markiert war, wird das Flag getoggelt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DruckAuftrag_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles MyBase.CellClick
        Dim Col As Integer = e.ColumnIndex
        Dim Row As Integer = e.RowIndex

        'Wenn die Zeile gültig ist, wird der entsprechende Wert im Ergebnis-Array abgelegt
        Try
            If Row >= 0 Then
                Dim s As wb_Global.wb_LinienGruppe = GridArray(Row)
                Select Case Col
                    Case COLDRK
                        s.bDrucken = CType(CurrentCell, DataGridViewCheckBoxCell).FormattedValue
                    Case COLKOM
                        s.bKommentar = CType(CurrentCell, DataGridViewCheckBoxCell).FormattedValue
                    Case COLSDT
                        s.bSonderText = CType(CurrentCell, DataGridViewCheckBoxCell).FormattedValue
                End Select
                GridArray(Row) = s
            End If
        Catch ex As Exception
        End Try

    End Sub

End Class
