Imports Signum.OrgaSoft.AddIn.wb_Functions
Public Class wb_DataGridView
    Inherits Windows.Forms.DataGridView

    Dim MySqlCon As MySqlConnection
    Dim MySqlCmd As MySqlCommand
    Dim MySqlDta As MySqlDataAdapter
    Dim DtaTable As DataTable
    Private DtaView As DataView = Nothing

    Public ColNames As New List(Of String)
    Dim sFilter As String = ""
    Dim iSort As Integer = -1

    Dim mContextMenu As New System.Windows.Forms.ContextMenuStrip
    Dim mMenuItem As System.Windows.Forms.ToolStripMenuItem

    Sub LoadData()
        mContextMenu.SuspendLayout()

        MySqlCon = New MySqlConnection(My.Settings.MySQLConWinBack)
        MySqlCmd = New MySqlCommand("SELECT KO_Nr, KO_Type, KO_Bezeichnung, KO_Kommentar FROM Komponenten", MySqlCon)
        MySqlDta = New MySqlDataAdapter(MySqlCmd)
        DtaTable = New DataTable()

        MySqlDta.Fill(DtaTable)
        DtaView = DtaTable.DefaultView
        Me.DataSource = DtaView

        'Spalten-Überschriften eintragen
        For i = 0 To ColNames.Count - 1
            'Spalten-Namen, die mit & beginnen werden als Auto-Size Spalten behandelt
            If Microsoft.VisualBasic.Left(ColNames(i), 1) = "&" Then
                Me.Columns(i).HeaderText = ColNames(i).Remove(0, 1) + Chr(10)
                Me.Columns(i).AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Else
                Me.Columns(i).HeaderText = ColNames(i) + Chr(10)
            End If
        Next

        'Popup-Menu - Spalten ein-/ausblenden
        For i = 0 To ColNames.Count - 1
            mMenuItem = New Windows.Forms.ToolStripMenuItem
            mMenuItem.Text = Me.Columns(i).HeaderText.Remove(Me.Columns(i).HeaderText.Length - 1, 1)
            mMenuItem.Checked = True
            mMenuItem.CheckState = Windows.Forms.CheckState.Checked
            mContextMenu.Items.Add(TryCast(mMenuItem, System.Windows.Forms.ToolStripMenuItem))
        Next

        'Design
        Dim DataGridViewCellStyle As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        DataGridViewCellStyle.BackColor = My.Settings.DataGridAlternateRowColor
        Me.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle
        Me.RowHeadersVisible = False
        Me.ReadOnly = True
        Me.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ContextMenuStrip = mContextMenu
        mContextMenu.ResumeLayout(False)

    End Sub

    Sub SaveToDisk(sGridName As String)
        Dim IniFile As New Signum.OrgaSoft.AddIn.OrgasoftMain.wb_Konfig
        Dim sColumn As String

        For i = 0 To Me.ColumnCount - 1
            sColumn = "Column" & i.ToString & "-Width"
            If Me.Columns(i).Visible Then
                IniFile.WriteInt(sGridName, sColumn, Me.Columns(i).Width)
            Else
                IniFile.WriteInt(sGridName, sColumn, -1)
            End If
        Next
    End Sub

    Sub LoadFromDisk(sGridName As String)
        Dim IniFile As New Signum.OrgaSoft.AddIn.OrgasoftMain.wb_Konfig
        Dim w, i As Integer

        For i = 0 To Me.ColumnCount - 1
            w = IniFile.ReadInt(sGridName, "Column" & i.ToString & "-Width", 0)
            Try
                If w > 0 Then
                    Me.Columns(i).Width = w
                ElseIf w = -1 Then
                    Me.Columns(i).Visible = False
                End If
            Catch
            End Try
        Next
    End Sub

    Public Overloads Sub DataGridView_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim sRowFilter As String

        'Wenn das eingegebene Zeichen einem Suchstring zugeordnet werden kann, wird der Tastendruck nicht mehr weitergegeben
        e.Handled = KeyToString(e.KeyChar, sFilter)

        If iSort > 0 And (e.Handled Or sFilter <> "") Then
            Dim sHeaderName As String
            Try : sHeaderName = ColNames(iSort) : Catch : sHeaderName = "" : End Try
            If Len(sFilter) > 1 Then
                sRowFilter = Me.Columns(iSort).Name & " LIKE '%" & sFilter & "%'"
                Me.Columns(iSort).HeaderText = sHeaderName & Chr(10) & "~ " & sFilter.ToUpper
            Else
                sRowFilter = Me.Columns(iSort).Name & " LIKE '" & sFilter & "%'"
                Me.Columns(iSort).HeaderText = sHeaderName & Chr(10) & sFilter.ToUpper & "*"
            End If
            DtaView.RowFilter = sRowFilter
        Else
            DtaView.RowFilter = ""
        End If
    End Sub

    Private Overloads Sub DataGridView_ColumnHeaderMouseClick(sender As Object, e As Windows.Forms.DataGridViewCellMouseEventArgs) Handles MyBase.ColumnHeaderMouseClick

        If e.ColumnIndex <> iSort Then
            'alten Header wieder restaurieren
            If iSort > 0 Then
                Dim sHeaderName As String
                Try : sHeaderName = ColNames(iSort) : Catch : sHeaderName = "" : End Try
                Me.Columns(iSort).HeaderText = sHeaderName + Chr(10)
            End If

            If Me.Columns(e.ColumnIndex).ValueType = GetType(String) Then
                sFilter = ""
                iSort = e.ColumnIndex
            Else
                iSort = -1
            End If
            DtaView.RowFilter = ""
        End If
    End Sub

End Class
