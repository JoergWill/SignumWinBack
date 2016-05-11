Imports Signum.OrgaSoft.AddIn.wb_Functions
Public Class wb_DataGridView
    Inherits Windows.Forms.DataGridView

    Dim MySqlCon As MySqlConnection
    Dim MySqlCmd As MySqlCommand
    Dim MySqlDta As MySqlDataAdapter
    Dim DtaTable As DataTable
    Private DtaView As DataView = Nothing

    Dim sFilter As String = ""
    Dim iSort As Integer = -1

    Sub LoadData()
        MySqlCon = New MySqlConnection(My.Settings.MySQLConWinBack)
        MySqlCmd = New MySqlCommand("SELECT * FROM Komponenten", MySqlCon)
        MySqlDta = New MySqlDataAdapter(MySqlCmd)
        DtaTable = New DataTable()

        MySqlDta.Fill(DtaTable)
        DtaView = DtaTable.DefaultView
        Me.DataSource = DtaView

    End Sub

    Public Overloads Sub DataGridView_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        'Wenn das eingegebene Zeichen einem Suchstring zugeordnet werden kann, wird der Tastendruck nicht mehr weitergegeben
        e.Handled = KeyToString(e.KeyChar, sFilter)

        If iSort > 0 And (e.Handled Or sFilter <> "") Then
            DtaView.RowFilter = Me.Columns(iSort).Name & " LIKE '%" & sFilter & "%'"
            Me.Columns(iSort).HeaderText = Me.Columns(iSort).Tag & " " & sFilter
        Else
            DtaView.RowFilter = ""
        End If

    End Sub
    Private Overloads Sub DataGridView_ColumnHeaderMouseClick(sender As Object, e As Windows.Forms.DataGridViewCellMouseEventArgs) Handles MyBase.ColumnHeaderMouseClick
        If e.ColumnIndex <> iSort Then
            If Me.Columns(e.ColumnIndex).ValueType = GetType(String) Then
                'alten Header wieder restaurieren
                If iSort > 0 Then
                    Me.Columns(iSort).HeaderText = Me.Columns(iSort).Tag
                End If

                sFilter = ""
                iSort = e.ColumnIndex
            Else
                iSort = -1
            End If
            DtaView.RowFilter = ""
        End If
    End Sub

End Class
