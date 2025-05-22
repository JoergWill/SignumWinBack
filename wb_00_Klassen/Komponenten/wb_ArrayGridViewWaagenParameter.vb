Imports System.Drawing
Imports System.Windows.Forms
Imports WinBack

Public Class wb_ArrayGridViewWaagenParameter
    Inherits wb_ArrayGridView
    Public arr() As wb_Global.WaagenParameter

    Public Sub New(ByVal x() As wb_Global.WaagenParameter, Optional ShowTooltips As Boolean = True)
        'Grid Grundeinstellungen
        arr = x
        MyBase._ShowTooltips = ShowTooltips
        InitGrid()
        'Daten anzeigen - Editieren erlaubt. Das Readonly-Flag wird in FillGrid für die einzelnen Spalten gesetzt.
        InitData(False)
    End Sub

    Public Sub RefreshGrid(ByVal x() As wb_Global.WaagenParameter)
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
        Dim MaxRowCount As Integer = 17
        'Überschriften in unterschiedlichen Styles
        MyBase.EnableHeadersVisualStyles = False
        MyBase.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray

        'Spalte 0..1 enthalter Beschreibung und Parameter-Nummer
        MyBase.Columns.Add("0_Text", "Waage Nummer")
        MyBase.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
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

        'Pro Waage eine Spalte
        For i = 0 To UBound(arr)
            'Spalte erzeugen
            ColCount += 1

            'nur gültige Waagen/Linien anzeigen
            If arr(i).WaageNr > wb_Global.UNDEFINED Then
                MyBase.Columns.Add(CStr(ColCount) & "_Waage", arr(i).WaageNr.ToString)
                'Spaltenbreite und Eigenschaften festlegen
                MyBase.Columns(MyBase.ColumnCount - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                MyBase.Columns(MyBase.ColumnCount - 1).Width = 50
                MyBase.Columns(MyBase.ColumnCount - 1).Visible = True

                'wenn die Waage/Linie nicht verwendet wird - grau ausblenden
                If Not arr(i).LinieAktiv Or (arr(i).LinieNr < 0) Then
                    MyBase.Columns(MyBase.ColumnCount - 1).HeaderCell.Style.ForeColor = Color.LightGray
                End If
            Else
                MyBase.Columns.Add(CStr(ColCount) & "_Waage", "")
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
                With Rows(r)
                    ' Zeileneigenschaften festlegen: Keine 'verschwindende' Zeile zulassen
                    .MinimumHeight = 20
                    ' Strich zwischen den Zeilen  
                    .DividerHeight = 0

                    'Bezeichnung in Spalte 1
                    Select Case r
                        Case 0
                            .Cells(0).Value = "Linie"
                            .Cells(0).Tag = ""
                        Case 1
                            .Cells(0).Value = "Waagengröße(max) [kg]"
                            .Cells(0).Tag = "WA_Maximal_Dosierung"
                        Case 2
                            .Cells(0).Value = "Förderstromüberwachung [kg/s]"
                            .Cells(0).Tag = "WA_Foerderstrom_Ueberwachung"
                        Case 3
                            .Cells(0).Value = "Austragefunktion"
                            .Cells(0).Tag = "WA_Austragefunktion"
                        Case 4
                            .Cells(0).Value = "Anlauf Transportgebläse [s]"
                            .Cells(0).Tag = "WA_Anlaufzeit_Geblaese"
                        Case 5
                            .Cells(0).Value = "Nachlauf Transport [s]"
                            .Cells(0).Tag = "WA_Nachlaufzeit_Geblaese"
                        Case 6
                            .Cells(0).Value = "Rüttler tEin [s]"
                            .Cells(0).Tag = "WA_Zeit_Ruettler_Ein"
                        Case 7
                            .Cells(0).Value = "Rüttler tAus [s]"
                            .Cells(0).Tag = "WA_Zeit_Ruettler_Aus"
                        Case 8
                            .Cells(0).Value = "Düsen tEin [s]"
                            .Cells(0).Tag = "WA_Zeit_Austrageduesen_Ein"
                        Case 9
                            .Cells(0).Value = "Düsen tAus [s]"
                            .Cells(0).Tag = "WA_Pause_Austrageduesen_Aus"
                        Case 10
                            .Cells(0).Value = "FilterReinigung tEin [s]"
                            .Cells(0).Tag = "WA_Zeit_Filterreinigung_Ein"
                        Case 11
                            .Cells(0).Value = "FilterReinigung tAus [s]"
                            .Cells(0).Tag = "WA_Pause_Filterreinigung_Aus"
                        Case 12
                            .Cells(0).Value = "Impuls Klappe [s]"
                            .Cells(0).Tag = "WA_Impuls_Klappe_Auf_Zu"
                        Case 13
                            .Cells(0).Value = "Nachkommastellen"
                            .Cells(0).Tag = "WA_Anz_NK_Stellen"
                        Case 14
                            .Cells(0).Value = "BC-AnalogKanal"
                            .Cells(0).Tag = ""
                        Case 15
                            .Cells(0).Value = "BC-IP"
                            .Cells(0).Tag = ""
                        Case 16
                            .Cells(0).Value = "BC-KanalNr"
                            .Cells(0).Tag = ""
                    End Select

                    ' Zeile r mit Werten füllen
                    For c = 2 To ColCount - 1
                        'Linien die nicht definiert sind werden nicht angezeigt
                        If arr(c - 2).WaageNr > wb_Global.UNDEFINED Then

                            'Default Ausrichtung zentriert
                            .Cells(c).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                            'wenn die Linie nicht verwendet wird - grau ausblenden
                            If Not arr(c - 2).LinieAktiv Or (arr(c - 2).LinieNr < 0) Then
                                .Cells(c).Style.ForeColor = Color.LightGray
                            End If

                            Select Case r
                                Case 0
                                    If arr(c - 2).LinieNr > 0 Then
                                        .Cells(c).Value = arr(c - 2).LinieNr
                                    Else
                                        .Cells(c).Value = ""
                                    End If
                                Case 1
                                    .Cells(c).Value = wb_Functions.FormatStr(arr(c - 2).WaagenGroesse, 1)
                                    .Cells(c).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                                Case 2
                                    .Cells(c).Value = arr(c - 2).FoerderStrom
                                Case 3
                                    .Cells(c).Value = arr(c - 2).AustrageFkt
                                Case 4
                                    .Cells(c).Value = arr(c - 2).Anlauf_Transport
                                Case 5
                                    .Cells(c).Value = arr(c - 2).Nachlauf_Transport
                                Case 6
                                    .Cells(c).Value = arr(c - 2).Ruettler_Ein
                                Case 7
                                    .Cells(c).Value = arr(c - 2).Ruettler_Aus
                                Case 8
                                    .Cells(c).Value = arr(c - 2).Duesen_Ein
                                Case 9
                                    .Cells(c).Value = arr(c - 2).Duesen_Aus
                                Case 10
                                    .Cells(c).Value = arr(c - 2).Filter_Ein
                                Case 11
                                    .Cells(c).Value = arr(c - 2).Filter_Aus
                                Case 12
                                    .Cells(c).Value = arr(c - 2).Impuls_Klappe
                                Case 13
                                    .Cells(c).Value = arr(c - 2).Nachkomma
                                Case 14
                                    .Cells(c).Value = arr(c - 2).Analog_Kanal
                                Case 15
                                    .Cells(c).Value = arr(c - 2).BC9000
                                Case 16
                                    .Cells(c).Value = arr(c - 2).ParameterNr
                            End Select
                        End If
                    Next c
                End With
            Next r
        End If
    End Sub

End Class
