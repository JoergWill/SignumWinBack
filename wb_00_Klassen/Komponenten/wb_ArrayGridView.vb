Imports System.Drawing
Imports System.Windows.Forms

Public MustInherit Class wb_ArrayGridView
    Inherits System.Windows.Forms.DataGridView

    'Anzeige von Zell-Tooltips?
    Public _ShowTooltips As Boolean
    Public ColNames As New List(Of String)
    Private _Font As New Drawing.Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel)

    Public Property wbFont As Font
        Get
            Return _Font
        End Get
        Set(value As Font)
            _Font = value
        End Set
    End Property

    Public Sub New()
        'Grid Grundeinstellungen
        InitGrid()
    End Sub

    Public Sub InitData(Optional ReadOnlyGrid As Boolean = True)
        'Start Initialisierung - Update Grid abschalten
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' Daten ins Grid eintragen
        FillGrid()
        ' Spaltenansicht einrichten
        InitColumns(ReadOnlyGrid)
        'Ende Initialisierung - Update Grid wieder einschalten
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(True)
    End Sub

    MustOverride Sub FillGrid()

    Public Sub FillColumns()
        'alle Spalten löschen
        ColumnCount = 0

        'Spalten-Überschriften eintragen
        For i = 0 To ColNames.Count - 1
            If Microsoft.VisualBasic.Left(ColNames(i), 1) = "&" Then
                'Spalten-Namen, die mit & beginnen werden als Auto-Size Spalten behandelt
                Columns.Add("C" & i, ColNames(i).Remove(0, 1) & Chr(10))
                Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Columns(i).Visible = True
            ElseIf ColNames(i) = "" Then
                'Spalten ohne Bezeichnung werden ausgeblendet
                Columns.Add("C" & i, "")
                Columns(i).Visible = False
            ElseIf Microsoft.VisualBasic.Left(ColNames(i), 1) = "#" Then
                'Spalten-Namen, die mit # beginnen werden als DateTime-Spalten formatiert
                Dim c As New wb_GridCalendarColumn With {
                    .Name = "C" & i,
                    .ReadOnly = False,
                    .HeaderText = ColNames(i).Remove(0, 1) & Chr(10),
                    .DateFormat = "dd.MM.yy HH:mm"
                }
                Columns.Add(c)
                Columns(i).Visible = True
            ElseIf Microsoft.VisualBasic.Left(ColNames(i), 1) = "?" Then
                'Spalten-Namen, die mit ? beginnen werden als CheckBox-Spalten formatiert
                Dim c As New DataGridViewCheckBoxColumn With {
                    .Name = "B" & i,
                    .ReadOnly = False,
                    .HeaderText = ColNames(i).Remove(0, 1) & Chr(10)
                }
                Columns.Add(c)
                Columns(i).Visible = True
            Else
                'normale Spalten haben feste Breite
                Columns.Add("C" & i, ColNames(i) & Chr(10))
                Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                Columns(i).Visible = True
            End If
        Next
    End Sub

    Friend Sub InitColumns(ReadOnlyGrid As Boolean)
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
                If ReadOnlyGrid Then
                    .ReadOnly = True
                End If
            End With
        Next c
    End Sub

    ''' <summary>
    ''' Grundeinstellungen des DataGridView
    ''' vor der Zuweisung von Array-Daten
    ''' </summary>
    Public Sub InitGrid()
        ' AlternatingRow-Hintergrundfarbe
        Dim gray245 As System.Drawing.Color = Color.FromArgb(255, 245, 245, 245)
        ' Cursor-Hintergrundfarbe
        Dim gray230 As System.Drawing.Color = Color.FromArgb(255, 230, 230, 230)

        With Me

            ' Einfügen, Löschen, Umordnen verbieten
            .AllowDrop = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = True
            .AllowUserToResizeRows = False

            ' Maus-Click auf Column/Rowheader soll NICHT zur Markierung führen
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            ' Keine Auswahl erlauben, weil Daten nur angezeigt werden
            .MultiSelect = False
            .BorderStyle = System.Windows.Forms.BorderStyle.None

            ' Es werden keine Scrollbars angezeigt
            .ScrollBars = System.Windows.Forms.ScrollBars.None

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
            .GridColor = Color.LightGray

        End With
    End Sub

    ''' <summary>
    ''' Hier darf nur der Event CellCONTENTClick verarbeitet werden. Sonst werden die Ergebnisse verfälscht(Niehaves)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S1066:Mergeable ""if"" statements should be combined", Justification:="<Ausstehend>")>
    Private Sub GridCellClick(sender As Object, e As DataGridViewCellEventArgs) Handles MyBase.CellContentClick
        Dim Col As Integer = e.ColumnIndex
        Dim Row As Integer = e.RowIndex

        'Wenn die Zeile gültig ist, wird der entsprechende Wert im Ergebnis-Array getoggelt
        Try
            If Col >= 0 AndAlso Col < Me.ColumnCount AndAlso Row >= 0 AndAlso Row < Me.RowCount Then
                If Me.Columns(Col).Name.First = "B" Then
                    'Toggle
                    Me.Rows(Row).Cells(Col).Value = Not Me.Rows(Row).Cells(Col).FormattedValue
                End If
            End If
        Catch ex As Exception
            Debug.Print("Fehler bei GridCellClick")
        End Try

    End Sub


    Public Sub GridLocation(ByVal Parent As System.Windows.Forms.TabPage)
        MyBase.Parent = Parent
        MyBase.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Location = New System.Drawing.Point(Parent.Top, Parent.Left)
        Size = New System.Drawing.Size(Parent.Width, Parent.Height)
        Dock = DockStyle.Fill
    End Sub

    Public Sub GridLocation(ByVal Parent As System.Windows.Forms.Panel)
        MyBase.Parent = Parent
        MyBase.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        MyBase.Location = New System.Drawing.Point(Parent.Top, Parent.Left)
        MyBase.Size = New System.Drawing.Size(Parent.Width, Parent.Height)
        MyBase.Dock = System.Windows.Forms.DockStyle.Fill
    End Sub

    ''' <summary>
    ''' Doppelclick auf den Header einer Check-Box-Spalte setzt alle Einträge in dieser Spalte auf False.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ColumnHeaderDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles MyBase.ColumnHeaderMouseDoubleClick
        'es darf keine Zeile mehr selektiert sein
        Me.ClearSelection()
        'Aktuelle Spalte
        Dim Col As Integer = e.ColumnIndex
        'Prüfen ob die Spalte eine Check-Box-Spalte ist
        If Me.Columns(Col).Name.First = "B" Then
            'alle Einträge in dieser Spalte auf false setzen
            For Each r As DataGridViewRow In Rows
                r.Cells(Col).Value = False
            Next
        End If
    End Sub


End Class
