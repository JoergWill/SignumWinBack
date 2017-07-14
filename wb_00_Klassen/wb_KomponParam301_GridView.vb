Imports System.Drawing
Imports System.Windows.Forms

Public Class wb_KomponParam301_GridView
    Inherits Windows.Forms.DataGridView

    ' Zum Speichern der Werte der Optionalen Konstruktor-Parameter
    Private _HeadersVisible As Boolean      ' Zeilen-/Spaltenköpfe sichtbar?
    Private _AllowResizing As Boolean       ' Zeilen-/Spaltenbreite variabel?
    Private _ShowTooltips As Boolean        ' Anzeige von Zell-Tooltips?
    Private _Font As Drawing.Font = New Drawing.Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel)

    Public Sub New(ByVal arr() As wb_Global.Nwt, Optional HeadersVisible As Boolean = True, Optional AllowResizing As Boolean = False, Optional ShowTooltips As Boolean = True)

        If IsNothing(arr) Then Exit Sub

        _HeadersVisible = HeadersVisible
        _AllowResizing = AllowResizing
        _ShowTooltips = ShowTooltips

        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        InitGrid()
        If Not IsNothing(arr) Then
            ' Daten ins Grid eintragen
            FillGrid(arr)
            ' Spaltenansicht einrichten
            InitColumns()
        End If

        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(True)

    End Sub

    Private Sub InitColumns()
        ' die meisten allgemeinen Einstellungen der Spalten
        ' müssen in jeder einzelnen Spalte vorgenommen werden

        Dim c As Integer
        Dim col As DataGridViewColumn

        ' Dim cw(Me.ColumnCount - 1) As Integer

        For c = 0 To MyBase.Columns.Count - 1
            col = MyBase.Columns(c)
            With col
                ' Fetter Spalten-Trennstrich
                .DividerWidth = 1
                ' Sortieren der Spalte abschalten
                .SortMode = DataGridViewColumnSortMode.NotSortable

                If _AllowResizing Then
                    ' User kann die Spaltenbreite mit der Maus ändern
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Resizable = DataGridViewTriState.True

                    ' Einstellung der Spaltenbreite initialisieren
                    ' (!! Diese Methode schluckt ggf. Rechenzeit !!) 
                    .Width = .GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCellsExceptHeader, True)

                    ' keine 'verschwindende' Spalte zulassen
                    ' zu kleines Width wird ggf. automatisch angepasst
                    .MinimumWidth = 60
                Else
                    ' Spaltenbreite wird anhand der Daten festgelegt 
                    ' und ist fixiert 
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                End If

                .ValueType = GetType(String)

                ' nur Anzeige der Daten, kein Editieren zulassen
                .ReadOnly = True
                .Visible = True
            End With
        Next c
    End Sub

    Private Sub FillGrid(ByVal arr() As wb_Global.Nwt)
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
                    Header = arr(i).Header
                    'Gruppen-Bezeichnung (Big4/Big8/Allergene...)
                    MyBase.Columns.Add(CStr(ColCount) & "_Text", Header)
                    'Überschrift pro 100g nicht bei Allergenen
                    If wb_KomponParam301_Global.IsAllergen(i) Then
                        MyBase.Columns.Add(CStr(ColCount) & "_Wert", "")
                    Else
                        MyBase.Columns.Add(CStr(ColCount) & "_Wert", "pro 100g")
                    End If
                    'Spalte Einheiten
                    MyBase.Columns.Add(CStr(ColCount) & "_Einh", "")
                End If
                    RowCount += 1

                'Index-Array für Daten erstellen
                Grid(ColCount, RowCount) = i
            End If
        Next

        ' Die erforderliche Anzahl Zeilen in einem Rutsch erstellen:
        MyBase.Rows.Add(MaxRowCount + 1)

        ' Daten ins DatagridView eintragen
        For r = 0 To MaxRowCount
            With rows(r)
                ' Zeileneigenschaften festlegen: Keine 'verschwindende' Zeile zulassen
                .MinimumHeight = 20
                ' Strich zwischen den Zeilen  
                .DividerHeight = 1

                ' Zeile r mit Werten füllen
                For c = 1 To ColCount

                    i = Grid(c, r)
                    If i > 0 Then
                        'Bezeichnung
                        .Cells(c * 3 - 3).Value = arr(i).Text
                        'Allergene/Nährwerte
                        If wb_KomponParam301_Global.IsAllergen(i) Then
                            'Allergen-Kennzeichnung ohne Einheit
                            .Cells(c * 3 - 2).Value = wb_Functions.AllergenToString(arr(i).Wert)
                            .Cells(c * 3 - 1).Value = ""
                        Else
                            'Nährwert und Einheit
                            .Cells(c * 3 - 2).Value = wb_Functions.FormatStr(arr(i).Wert, 3)
                            .Cells(c * 3 - 1).Value = arr(i).Einheit
                        End If
                        'Tooltip
                        .Cells(c * 3 - 2).ToolTipText = "Keine vollständige Berechnung möglich"
                    End If
                Next c
            End With
        Next r


        'wert = arr(r).Wert
        '            tt = ""          ' Tooltip Initialisieren

        '            With .Cells(c)
        '                ' Array-Element: 
        '                ' Inhalt der Zelle aufbereiten
        '                .Value = wert

        '                If _HeadersVisible = False Then
        '                    If tt <> "" Then
        '                        tt += vbCrLf
        '                    End If
        '                    ' Array-Indizierung in Tooltip eintragen
        '                    tt = "Arr(" + CStr(r) + "," + CStr(c) + ")"
        '                End If
        '                If _ShowTooltips Then
        '                    ' Zell-Tooltip erstellen
        '                    .ToolTipText = tt
        '                End If
        '            End With
        '        Next c
        '        ' Array-Index in den Zeilenkopf eintragen
        '        .HeaderCell.Value = "Row_" + CStr(r)
        '    End With
        'Next r
    End Sub

    ''' <summary>
    ''' Grundeinstellungen des DataGridView
    ''' vor der Zuweisung von Array-Daten
    ''' </summary>
    Private Sub InitGrid()
        ' AlternatingRow-Hintergrundfarbe
        Dim gray245 As System.Drawing.Color = Color.FromArgb(255, 245, 245, 245)
        ' Cursor-Hintergrundfarbe
        Dim gray230 As System.Drawing.Color = Color.FromArgb(255, 230, 230, 230)

        With Me
            ' Grid-Einstellungen
            If _HeadersVisible Then
                .ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
            Else
                .ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText
            End If

            ' Einfügen, Löschen, Umordnen verbieten
            .AllowDrop = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = False
            .AllowUserToResizeRows = _AllowResizing

            ' Maus-Click auf Column/Rowheader soll NICHT zur Markierung führen
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            ' Keine Auswahl erlauben, weil Daten nur angezeigt werden
            .MultiSelect = False

            .BorderStyle = Windows.Forms.BorderStyle.FixedSingle

            ' Farbe des nicht mit Zellen belegten GridView-Hintergrundes
            .BackgroundColor = Color.DarkGray

            ' Es werden keine Scrollbars angezeigt
            .ScrollBars = Windows.Forms.ScrollBars.None

            ' ColumnHeader-Einstellungen
            .ColumnHeadersVisible = _HeadersVisible
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised

            With .ColumnHeadersDefaultCellStyle
                .Font = _Font
                .ForeColor = Color.Black
                .BackColor = gray230
                .Alignment = DataGridViewContentAlignment.TopLeft
            End With

            ' RowHeader-Einstellungen
            .RowHeadersVisible = False
            '.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            '.RowHeadersBorderStyle = .ColumnHeadersBorderStyle

            ' von oben übernehmen
            '.RowHeadersDefaultCellStyle = .ColumnHeadersDefaultCellStyle

            ' Row-Einstellungen
            With .RowsDefaultCellStyle
                ' Zahlen werden nach rechts ausgerichtet
                .Alignment = DataGridViewContentAlignment.MiddleRight
                .BackColor = Color.White
                .ForeColor = Color.Black
                ' 'Cursor'-Farbe
                .SelectionBackColor = gray230
                .SelectionForeColor = Color.Black
                .Font = _Font
            End With

            With .AlternatingRowsDefaultCellStyle
                ' für bessere Lesbarkeit der DatenZeilen
                .BackColor = gray245
            End With

            ' Weitere allgemeine Eigenschaften
            .TopLeftHeaderCell.Value = ""
            .TopLeftHeaderCell.ToolTipText = ""
            .ShowCellToolTips = _ShowTooltips
            .Cursor = Cursors.Arrow

            .DoubleBuffered = True
            .ResizeRedraw = True
            .GridColor = Color.Black

        End With
    End Sub

    Public Shadows Sub Location(ByVal Parent As Windows.Forms.TabPage, ByVal Top As Integer, ByVal Left As Integer, ByVal Width As Integer, ByVal Height As Integer)

        MyBase.Parent = Parent
        MyBase.Width = Width
        MyBase.Height = Height
        MyBase.Top = Top
        MyBase.Left = Left
    End Sub
End Class
