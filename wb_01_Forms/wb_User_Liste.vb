Imports Signum.OrgaSoft.AddIn.wb_Functions

Public Class wb_User_Liste
    Dim MySqlCon As MySqlConnection
    Dim MySqlCommand As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Private dv As DataView = Nothing
    Dim filter As String = ""
    Dim sort As Integer = -1

    Private Sub wb_User_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Data_Load()
    End Sub

    Private Sub Data_Load()
        'MySqlCon = New MySqlConnection(My.Settings.MySQLConWinBack)
        ''MySqlCommand = New MySqlCommand("SELECT * FROM ItemParameter WHERE IP_ItemTyp = 500", MySqlCon)
        'MySqlCommand = New MySqlCommand("SELECT * FROM Komponenten", MySqlCon)
        'da = New MySqlDataAdapter(MySqlCommand)
        'dt = New DataTable()

        'da.Fill(dt)
        'dv = dt.DefaultView
        'DataGridView.DataSource = dv

        DataGridView.LoadData()
        DataGridView.Columns(0).Visible = False
        DataGridView.Columns(2).HeaderText = "Bezeichnung"
        DataGridView.Columns(2).Tag = DataGridView.Columns(1).HeaderText


    End Sub

    Private Sub DataGridView_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs)

    End Sub

    Private Sub DataGridView_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs) Handles DataGridView.KeyPress

    End Sub

    'Private Sub DataGridView_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles DataGridView.KeyDown
    '    'Wenn das eingegebene Zeichen einem Suchstring zugeordnet werden kann, wird der Tastendruck nicht mehr weitergegeben
    '    e.Handled = KeyToString(e.KeyCode.ToString, e.KeyValue, filter)

    '    Label1.Text = filter
    '    If sort > 0 And (e.Handled Or filter <> "") Then
    '        dv.RowFilter = DataGridView.Columns(sort).Name & " LIKE '%" & filter & "%'"
    '        DataGridView.Columns(sort).HeaderText = DataGridView.Columns(sort).Tag & " " & filter
    '    Else
    '        dv.RowFilter = ""
    '    End If

    '    Label3.Text = dv.RowFilter
    'End Sub

    'Private Sub DataGridView_ColumnHeaderMouseClick(sender As Object, e As Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView.ColumnHeaderMouseClick

    '    If DataGridView.Columns(e.ColumnIndex).ValueType = GetType(String) Then
    '        'alten Header wieder restaurieren
    '        If sort > 0 Then
    '            DataGridView.Columns(sort).HeaderText = DataGridView.Columns(sort).Tag
    '        End If

    '        filter = ""
    '        sort = e.ColumnIndex
    '    Else
    '        sort = -1
    '    End If
    'End Sub
End Class