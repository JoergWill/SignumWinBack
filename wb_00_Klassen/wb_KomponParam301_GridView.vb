Imports System.Drawing
Imports System.Windows.Forms
Imports WinBack

Public Class wb_KomponParam301_GridView
    Inherits wb_ArrayGridView


    Public Sub New(ByVal arr() As wb_Global.Nwt, Optional ShowTooltips As Boolean = True)
        'Initialisierung nur mit gültigem Daten-Array
        If IsNothing(arr) Then Exit Sub
        'Grid Grundeistellungen
        MyBase._ShowTooltips = ShowTooltips
        InitGrid()
        'Daten anzeigen 
        InitData(arr)
    End Sub
    'TODO das geht noch schöner (ohne wb_Global.nwt)
    Public Overloads Sub InitData(ByVal arr() As wb_Global.Nwt)

        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        'Initialisierung nur mit gültigem Daten-Array
        If Not IsNothing(arr) Then
            ' Daten ins Grid eintragen
            FillGrid(arr)
            ' Spaltenansicht einrichten
            InitColumns()
        End If

        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(True)

    End Sub

    'Public Overrides Sub FillGrid(arr As Object)
    '    FillGrid(DirectCast(arr, wb_Global.Nwt))
    'End Sub

    Private Overloads Sub FillGrid(ByVal arr() As wb_Global.Nwt)
        ' Die Arraydaten werden in das GridView eingetragen

        Dim r, c As Integer     ' Loops
        Dim i As Integer

        Dim rows As DataGridViewRowCollection = MyBase.Rows

        ' Daten Löschen
        MyBase.Columns.Clear()
        MyBase.Rows.Clear()

        ' Spalten erstellen
        Dim Header As String = ""
        Dim ColCount As Integer = 0
        Dim RowCount As Integer = 0
        Dim MaxRowCount As Integer = 0
        Dim Grid(UBound(arr), UBound(arr)) As Integer

        For i = 0 To UBound(arr)
            If arr(i).Visible Then

                If Header <> arr(i).Header Then
                    ColCount += 1
                    MaxRowCount = Math.Max(MaxRowCount, RowCount)
                    RowCount = 0

                    'Gruppen-Bezeichnung (Big4/Big8/Allergene...)
                    Header = arr(i).Header
                    MyBase.Columns.Add(CStr(ColCount) & "_Text", Header)
                    MyBase.Columns(MyBase.ColumnCount - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader

                    'Nährwerte/Allergene
                    If wb_KomponParam301_Global.IsAllergen(i) Then
                        'Allergene ohne Überschrift
                        MyBase.Columns.Add(CStr(ColCount) & "_Wert", "")
                        MyBase.Columns(MyBase.ColumnCount - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                    Else
                        'Überschrift pro 100g
                        MyBase.Columns.Add(CStr(ColCount) & "_Wert", "pro 100g")
                        MyBase.Columns(MyBase.ColumnCount - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                    End If

                    'Spalte Einheiten
                    If wb_KomponParam301_Global.IsAllergen(i) Then
                        'Bei Allergenen wird keine Einheit eingetragen - variable Spaltenbreite
                        MyBase.Columns.Add(CStr(ColCount) & "_X", "")
                        MyBase.Columns(MyBase.ColumnCount - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    Else
                        'bei allen anderen Werten wird der Einheiten-Text einggetragen
                        MyBase.Columns.Add(CStr(ColCount) & "_Einh", "")
                        MyBase.Columns(MyBase.ColumnCount - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                    End If
                End If

                'Index-Array für Daten erstellen
                Grid(ColCount, RowCount) = i
                RowCount += 1
            End If
        Next

        ' Die erforderliche Anzahl Zeilen in einem Rutsch erstellen:
        MyBase.Rows.Add(MaxRowCount)

        ' Daten ins DatagridView eintragen
        For r = 0 To MaxRowCount - 1
            With rows(r)
                ' Zeileneigenschaften festlegen: Keine 'verschwindende' Zeile zulassen
                .MinimumHeight = 20
                ' Strich zwischen den Zeilen  
                .DividerHeight = 0

                ' Zeile r mit Werten füllen
                For c = 1 To ColCount

                    i = Grid(c, r)
                    If i > 0 Then

                        'Bezeichnung
                        .Cells(c * 3 - 3).Value = arr(i).Text
                        .Cells(c * 3 - 3).Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                        'Allergene/Nährwerte
                        If wb_KomponParam301_Global.IsAllergen(i) Then
                            'Allergen-Kennzeichnung ohne Einheit
                            .Cells(c * 3 - 2).Value = wb_Functions.AllergenToString(arr(i).Wert)
                            .Cells(c * 3 - 1).Value = ""
                        Else
                            'Nährwert und Einheit
                            If wb_KomponParam301_Global.kt301Param(i).Gruppe = wb_Global.ktTyp301Gruppen.Gesamtkennzahlen Then
                                'Gesamtkennzahlen werden mit nur einer Kommastelle ausgegeben
                                .Cells(c * 3 - 2).Value = wb_Functions.FormatStr(arr(i).Wert, 1)
                            Else
                                'alle anderen Werte auf 3 Nachkommastellen formatieren
                                .Cells(c * 3 - 2).Value = wb_Functions.FormatStr(arr(i).Wert, 3)
                            End If
                            .Cells(c * 3 - 1).Value = arr(i).Einheit
                        End If

                        'Tooltip
                        If arr(i).FehlerText <> "" Then
                            .Cells(c * 3 - 1).Style.BackColor = Color.Red
                            .Cells(c * 3 - 2).Style.BackColor = Color.Red
                            .Cells(c * 3 - 3).Style.BackColor = Color.Red
                            .Cells(c * 3 - 3).ToolTipText = "Angaben fehlen für: " & arr(i).FehlerText

                        End If
                    End If
                Next c
            End With
        Next r
    End Sub

End Class
