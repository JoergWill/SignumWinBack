Imports System.Drawing
Imports System.Windows.Forms
Imports WinBack

Public Class wb_ArrayGridViewKomponParam301
    Inherits wb_ArrayGridView
    Public arr As wb_Global.Nwt()

    Public Sub New(ByVal x() As wb_Global.Nwt, Optional ShowTooltips As Boolean = True)
        'Grid Grundeinstellungen
        arr = x
        MyBase._ShowTooltips = ShowTooltips
        InitGrid()
        'Daten anzeigen - Editieren erlaubt. Das Readonly-Flag wird in FillGrid für die einzelnen Spalten gesetzt.
        InitData(False)
    End Sub

    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
    Public Overrides Sub FillGrid()
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
                    If wb_KomponParam301_Global.IsErnaehrung(i) Then
                        MyBase.Columns(MyBase.ColumnCount - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    Else
                        MyBase.Columns(MyBase.ColumnCount - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                    End If
                    'Editieren in dieser Spalte nicht möglich
                    MyBase.Columns(MyBase.ColumnCount - 1).ReadOnly = True

                    'Nährwerte/Allergene
                    If wb_KomponParam301_Global.IsAllergen(i) OrElse wb_KomponParam301_Global.IsErnaehrung(i) Then
                        'Allergene ohne Überschrift
                        MyBase.Columns.Add(CStr(ColCount) & "_Wert", "")
                        MyBase.Columns(MyBase.ColumnCount - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                    Else
                        'Überschrift pro 100g
                        MyBase.Columns.Add(CStr(ColCount) & "_Wert", "pro 100g")
                        MyBase.Columns(MyBase.ColumnCount - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                        'Mindest-Breite der Spalten wird durch Formatieren der Werte erreicht (PadLeft)
                    End If
                    'Editieren in dieser Spalte NICHT zulassen
                    MyBase.Columns(MyBase.ColumnCount - 1).ReadOnly = True

                    'Spalte Einheiten
                    If wb_KomponParam301_Global.IsAllergen(i) OrElse wb_KomponParam301_Global.IsErnaehrung(i) Then
                        'Bei Allergenen wird keine Einheit eingetragen - variable Spaltenbreite
                        MyBase.Columns.Add(CStr(ColCount) & "_X", "")
                        MyBase.Columns(MyBase.ColumnCount - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    Else
                        'bei allen anderen Werten wird der Einheiten-Text einggetragen
                        MyBase.Columns.Add(CStr(ColCount) & "_Einh", "")
                        MyBase.Columns(MyBase.ColumnCount - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                    End If
                    'Editieren in dieser Spalte nicht möglich
                    MyBase.Columns(MyBase.ColumnCount - 1).ReadOnly = True
                End If

                'Index-Array für Daten erstellen
                Grid(ColCount, RowCount) = i
                RowCount += 1
            End If
        Next

        ' Die erforderliche Anzahl Zeilen in einem Rutsch erstellen:
        If MaxRowCount > 0 Then
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
                            ElseIf wb_KomponParam301_Global.IsErnaehrung(i) Then
                                'Kennzeichnung Ernährungsform ohne Einheit
                                .Cells(c * 3 - 2).Value = wb_Functions.ErnaehrungToString(arr(i).Wert)
                                .Cells(c * 3 - 1).Value = ""
                            Else
                                'Nährwert und Einheit
                                If wb_KomponParam301_Global.kt301Param(i).Gruppe = wb_Global.ktTyp301Gruppen.Gesamtkennzahlen Then
                                    'Gesamtkennzahlen werden mit nur einer Kommastelle ausgegeben - Leerzeichen links auffüllen(Column-Width)
                                    .Cells(c * 3 - 2).Value = wb_Functions.FormatStr(arr(i).Wert, 1).PadLeft(8)
                                    'Rechtsbündig formatieren
                                    .Cells(c * 3 - 2).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                                Else
                                    'alle anderen Werte auf 3 Nachkommastellen formatieren
                                    .Cells(c * 3 - 2).Value = wb_Functions.FormatStr(arr(i).Wert, 3)
                                    'Rechtsbündig formatieren
                                    .Cells(c * 3 - 2).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                                End If
                                'Einheit 
                                .Cells(c * 3 - 1).Value = arr(i).Einheit

                            End If

                            'Fehler interne Abhängigkeit (z.B. Umrechnung kcal-kJ)
                            If arr(i).ErrIntern Then
                                .Cells(c * 3 - 2).Style.ForeColor = Color.Red
                                .Cells(c * 3 - 2).ToolTipText = "Angaben sind wiedersprüchlich "
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
        End If
    End Sub

End Class
