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
                .DividerWidth = 2
                ' Sortieren der Spalte abschalten
                .SortMode =
                DataGridViewColumnSortMode.NotSortable

                If _AllowResizing Then
                    ' User kann die Spaltenbreite mit der Maus ändern
                    .AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.None
                    .Resizable = DataGridViewTriState.True

                    ' Einstellung der Spaltenbreite initialisieren
                    ' (!! Diese Methode schluckt ggf. Rechenzeit !!) 
                    .Width = .GetPreferredWidth(
                      DataGridViewAutoSizeColumnMode.
                      AllCellsExceptHeader, True)

                    ' keine 'verschwindende' Spalte zulassen
                    ' zu kleines Width wird ggf. automatisch angepasst
                    .MinimumWidth = 60
                Else
                    ' Spaltenbreite wird anhand der Daten festgelegt 
                    ' und ist fixiert 
                    .AutoSizeMode =
                     DataGridViewAutoSizeColumnMode.
                     AllCellsExceptHeader
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
        Dim wert As Double      ' Inhalt Array-Element
        Dim tt As String        ' Tooltip

        Dim rows As DataGridViewRowCollection = MyBase.Rows

        ' Daten Löschen
        MyBase.Columns.Clear()
        MyBase.Rows.Clear()

        ' Spalten erstellen
        For c = 0 To 3
            ' ArrayIndex in Spaltenkopf eintragen
            MyBase.Columns.Add(CStr(c), "Col_" + CStr(c))
        Next c

        ' Die erforderliche Anzahl Zeilen in einem
        ' Rutsch erstellen:
        MyBase.Rows.Add(arr.GetLength(0))

        ' Daten ins DatagridView eintragen
        For r = 0 To UBound(arr)
            With rows(r)
                ' Zeileneigenschaften festlegen:
                ' Keine 'verschwindende' Zeile zulassen
                .MinimumHeight = 20
                ' Fetter Strich zwischen den Zeilen  
                .DividerHeight = 2

                ' Zeile r mit Werten füllen
                For c = 0 To 3
                    wert = arr(r).Wert
                    tt = ""          ' Tooltip Initialisieren

                    With .Cells(c)
                        ' Array-Element: 
                        ' Inhalt der Zelle aufbereiten

                        .Tag = wert ' Original-Wert aufbewahren

                        If _HeadersVisible = False Then
                            If tt <> "" Then tt += vbCrLf
                            ' Array-Indizierung in Tooltip eintragen
                            tt = "Arr(" + CStr(r) + "," + CStr(c) + ")"
                        End If
                        If _ShowTooltips Then
                            ' Zell-Tooltip erstellen
                            .ToolTipText = tt
                        End If
                    End With
                Next c
                ' Array-Index in den Zeilenkopf eintragen
                .HeaderCell.Value = "Row_" + CStr(r)
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
            ' Grid-Einstellungen
            ' ==================
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

            .BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            ' Farbe des nicht mit Zellen belegten GridView-Hintergrundes
            .BackgroundColor = Color.DarkGray

            ' Nur bei 'Bedarf' werden die Scrollbars angezeigt (durch eine andere Einstellung dieser Eigenschaft kann man sie aber am Erscheinen hindern)
            .ScrollBars = Windows.Forms.ScrollBars.Both

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
            .RowHeadersVisible = _HeadersVisible
            .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            .RowHeadersBorderStyle = .ColumnHeadersBorderStyle

            ' von oben übernehmen
            .RowHeadersDefaultCellStyle = .ColumnHeadersDefaultCellStyle

            ' Row-Einstellungen
            With .RowsDefaultCellStyle
                ' Zahlen werden nach rechts ausgerichtet
                .Alignment = DataGridViewContentAlignment.MiddleRight
                .BackColor = Color.White
                .ForeColor = Color.Black
                .Format = "0.00000000"
                .NullValue = "???"  'NaN
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
            .TopLeftHeaderCell.Value = "--> Clipboard"
            .TopLeftHeaderCell.ToolTipText = "Doppel-Click kopiert die Daten"
            .ShowCellToolTips = _ShowTooltips
            .Cursor = Cursors.Arrow

            .DoubleBuffered = True
            .ResizeRedraw = True
            .GridColor = Color.DarkBlue

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
