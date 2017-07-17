Imports System.Drawing
Imports System.Windows.Forms

Public Class wb_KomponParam301_GridView
    Inherits Windows.Forms.DataGridView

    ' Zum Speichern der Werte der Optionalen Konstruktor-Parameter
    Private _ShowTooltips As Boolean        ' Anzeige von Zell-Tooltips?
    Private _Font As Drawing.Font = New Drawing.Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel)

    Public Sub New()
        'Grid Grundeistellungen
        InitGrid()
    End Sub

    Public Sub New(ByVal arr() As wb_Global.Nwt, Optional ShowTooltips As Boolean = True)
        'Initialisierung nur mit gültigem Daten-Array
        If IsNothing(arr) Then Exit Sub
        'Grid Grundeistellungen
        InitGrid()
        'Daten anzeigen 
        InitData(arr, ShowTooltips)
    End Sub

    Public Sub InitData(ByVal arr() As wb_Global.Nwt, Optional ShowTooltips As Boolean = True)
        _ShowTooltips = ShowTooltips

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

    Private Sub InitColumns()
        ' die meisten allgemeinen Einstellungen der Spalten
        ' müssen in jeder einzelnen Spalte vorgenommen werden

        Dim c As Integer
        Dim col As DataGridViewColumn

        For c = 0 To MyBase.Columns.Count - 1
            col = MyBase.Columns(c)
            With col
                ' Spalten-Trennstrich
                .DividerWidth = 0
                ' Sortieren der Spalte abschalten
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .MinimumWidth = 20
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
                        .Cells(c * 3 - 2).ToolTipText = "Keine vollständige Berechnung möglich"
                    End If
                Next c
            End With
        Next r
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

            ' Einfügen, Löschen, Umordnen verbieten
            .AllowDrop = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = False
            .AllowUserToResizeRows = False

            ' Maus-Click auf Column/Rowheader soll NICHT zur Markierung führen
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            ' Keine Auswahl erlauben, weil Daten nur angezeigt werden
            .MultiSelect = False
            .BorderStyle = Windows.Forms.BorderStyle.None

            ' Es werden keine Scrollbars angezeigt
            .ScrollBars = Windows.Forms.ScrollBars.None

            ' ColumnHeader-Einstellungen
            .ColumnHeadersVisible = True
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised

            With .ColumnHeadersDefaultCellStyle
                .Font = _Font
                .ForeColor = Color.Black
                .BackColor = gray230
                .Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            ' RowHeader-Einstellungen
            .RowHeadersVisible = False

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
                .BackColor = Color.LightBlue
            End With

            ' Weitere allgemeine Eigenschaften
            .TopLeftHeaderCell.Value = ""
            .TopLeftHeaderCell.ToolTipText = ""
            .ShowCellToolTips = _ShowTooltips
            .Cursor = Cursors.Arrow

            .DoubleBuffered = True
            .ResizeRedraw = True
            .GridColor = Color.LightGray

        End With
    End Sub

    Public Shadows Sub MyLocation(ByVal Parent As Windows.Forms.TabPage, ByVal Top As Integer, ByVal Left As Integer, ByVal Width As Integer, ByVal Height As Integer)
        Dim c As Integer = 0

        MyBase.Parent = Parent
        MyBase.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Location = New System.Drawing.Point(Top, Left)
        Size = New System.Drawing.Size(Width, Height)
    End Sub
End Class
