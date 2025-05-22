Imports System.Drawing
Imports System.Windows.Forms
Imports WinBack

Public Class wb_ArrayGridViewSiloParameter
    Inherits wb_ArrayGridView
    Public arr() As wb_Global.SiloParameter

    Public Sub New(ByVal x() As wb_Global.SiloParameter, Optional ShowTooltips As Boolean = True)
        'Grid Grundeinstellungen
        arr = x
        MyBase._ShowTooltips = ShowTooltips
        InitGrid()
        'Daten anzeigen - Editieren erlaubt. Das Readonly-Flag wird in FillGrid für die einzelnen Spalten gesetzt.
        InitData(False)
    End Sub

    Public Sub RefreshGrid(ByVal x() As wb_Global.SiloParameter)
        'Daten in das lokale Array übertragen
        arr = x
        FillGrid()
    End Sub

    Public Overrides Sub FillGrid()
        'Die Arraydaten werden in das GridView eingetragen
        Dim i, r, c As Integer     ' Loops

        ' Daten Löschen
        MyBase.Columns.Clear()
        MyBase.Rows.Clear()

        'Spalten erstellen
        Dim Header As String = ""
        'Spalte 0..1 enthalter Beschreibung und Parameter-Nummer
        Dim ColCount As Integer = 2
        Dim MaxRowCount As Integer = 5
        'Überschriften in unterschiedlichen Styles
        MyBase.EnableHeadersVisualStyles = False
        MyBase.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray

        'Spalte 0..1 enthalter Beschreibung und Parameter-Nummer
        MyBase.Columns.Add("0_Text", "Linie")
        MyBase.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        MyBase.Columns(0).MinimumWidth = 250
        MyBase.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        MyBase.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        'Editieren in dieser Spalte nicht möglich
        MyBase.Columns(0).ReadOnly = True
        '
        MyBase.Columns.Add("1_ParamNr", "")
        MyBase.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        MyBase.Columns(1).Width = 50
        'Editieren in dieser Spalte nicht möglich
        MyBase.Columns(1).ReadOnly = True

        'Pro Linie eine Spalte
        For i = 0 To UBound(arr)
            'Spalte erzeugen
            ColCount += 1

            'nur gültige Linien anzeigen
            If arr(i).LinieNr > wb_Global.UNDEFINED Then
                MyBase.Columns.Add(CStr(ColCount) & "_Linie", arr(i).LinieNr.ToString)
                'Spaltenbreite und Eigenschaften festlegen
                MyBase.Columns(MyBase.ColumnCount - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                MyBase.Columns(MyBase.ColumnCount - 1).Width = 50
                MyBase.Columns(MyBase.ColumnCount - 1).Visible = True

                'wenn die Linie nicht verwendet wird - grau ausblenden
                If Not arr(i).LinieAktiv Then
                    MyBase.Columns(MyBase.ColumnCount - 1).HeaderCell.Style.ForeColor = Color.LightGray
                End If
            Else
                MyBase.Columns.Add(CStr(ColCount) & "_Linie", "")
                'Spalte unsichtbar und Eigenschaften festlegen
                MyBase.Columns(MyBase.ColumnCount - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                MyBase.Columns(MyBase.ColumnCount - 1).Visible = False
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

                    'Bezeichnung in Spalte 1/Parameter-Nummer im Tag-Attribut der Zelle
                    Select Case r
                        Case 0
                            .Cells(0).Value = "Nachlauf [kg]"
                            .Cells(0).Tag = "2"
                        Case 1
                            .Cells(0).Value = "Umschaltung Grob/Feinstrom [kg]"
                            .Cells(0).Tag = "3"
                        Case 2
                            .Cells(0).Value = "Schleuse Drehzahl Grobstrom [Hz]"
                            .Cells(0).Tag = "8"
                        Case 3
                            .Cells(0).Value = "Schleuse Drehzahl Feinstrom [Hz]"
                            .Cells(0).Tag = "9"
                        Case 4
                            .Cells(0).Value = "Faktor Menge/Zeit [kg/s]"
                            .Cells(0).Tag = "20"
                    End Select

                    ' Zeile r mit Werten füllen
                    For c = 2 To ColCount - 1
                        'Linien die nicht definiert sind werden nicht angezeigt
                        If arr(c - 2).LinieNr > wb_Global.UNDEFINED Then

                            'Default Ausrichtung zentriert
                            .Cells(c).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                            'wenn die Linie nicht verwendet wird - grau ausblenden
                            If Not arr(c - 2).LinieAktiv Then
                                .Cells(c).Style.ForeColor = Color.LightGray
                                .Cells(c).ReadOnly = True
                            Else
                                .Cells(c).Tag = arr(c - 2).ParamSatz.ToString
                            End If

                            Select Case r
                                Case 0
                                    .Cells(c).Value = wb_Functions.FormatStr(arr(c - 2).Nachlauf, 3)
                                    .Cells(c).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                                Case 1
                                    .Cells(c).Value = wb_Functions.FormatStr(arr(c - 2).GrobFein, 3)
                                    .Cells(c).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                                Case 2
                                    .Cells(c).Value = wb_Functions.FormatStr(arr(c - 2).Frequenz_Grob, 0)
                                Case 3
                                    .Cells(c).Value = wb_Functions.FormatStr(arr(c - 2).Frequenz_Fein, 0)
                                Case 4
                                    .Cells(c).Value = wb_Functions.FormatStr(arr(c - 2).Faktor_MengeZeit, 3)
                                    .Cells(c).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                            End Select
                        End If
                    Next c
                End With
            Next r
        End If
    End Sub

End Class
