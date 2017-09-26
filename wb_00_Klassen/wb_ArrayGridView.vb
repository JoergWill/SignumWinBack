Imports System.Drawing
Imports System.Windows.Forms

Public MustInherit Class wb_ArrayGridView
    Inherits Windows.Forms.DataGridView

    ' Zum Speichern der Werte der Optionalen Konstruktor-Parameter
    Friend _ShowTooltips As Boolean        ' Anzeige von Zell-Tooltips?
    Private _Font As Drawing.Font = New Drawing.Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel)

    Public Sub New()
        'Grid Grundeistellungen
        InitGrid()
    End Sub


    Public Sub InitData(ByVal arr() As Array)

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

    Friend Sub InitColumns()
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

    Sub FillGrid(arr As Object)

    End Sub

    ''' <summary>
    ''' Grundeinstellungen des DataGridView
    ''' vor der Zuweisung von Array-Daten
    ''' </summary>
    Friend Sub InitGrid()
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

    Public Shadows Sub GridLocation(ByVal Parent As Windows.Forms.TabPage)
        Dim c As Integer = 0

        MyBase.Parent = Parent
        MyBase.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Location = New System.Drawing.Point(Parent.Top, Parent.Left)
        Size = New System.Drawing.Size(Parent.Width, Parent.Height)
    End Sub

End Class
