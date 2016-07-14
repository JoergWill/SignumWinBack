Imports WinBack.wb_Functions
Imports System.Windows.Forms
Imports WinBack.wb_Sql
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

'---------------------------------------------------------
'19.05.2016/ V0.9/JW            :Neuanlage
'Bearbeitet von                 :Will
'
'Änderungen:
'---------------------------------------------------------
'Beschreibung:
'Ableitung der Klasse DataGridView.
'Enthält die Verbindung von DataGridView zu
'wahlweise MySQl(winback) oder MSSQL(OrgasoftMain)
'
'LoadData(sql) lädt die entsprechenden Daten ins Grid
'Über Filter kann eine zusätzliche Filter-Eigenschaft
'angeben werden.
'Nach tDataChangedTime wird der Event HasChanged ausgelöst,
'damit kann das aufrufende Programm die entsprechenden
'Felder abrufen und anzeigen. (Funktion Field)
'
'Änderungen über Field werden nach Aufruf der Update-Funtkion
'in die Datenbank geschrieben.
'Dazu muss der MySQL-Data-Client der MySQL-Version angepasst
'sein, sonst funktioniert die Update-Anweisung (automatisch
'generiert) nicht.
'---------------------------------------------------------

Public Class wb_DataGridView
    Inherits Windows.Forms.DataGridView

    Dim MySqlCon As MySqlConnection
    Dim MySqlCmd As MySqlCommand
    Dim MySqlDta As MySqlDataAdapter
    Dim MySqlCbd As MySqlCommandBuilder

    Dim msCon As SqlConnection
    Dim msCmd As SqlCommand
    Dim msDta As SqlDataAdapter
    Dim msCbd As SqlCommandBuilder

    Private DtaView As DataView = Nothing
    Dim DtaTable As New DataTable

    Private _tDataChangedTime = 500
    Private _Filter As String = ""

    Public ColNames As New List(Of String)
    Dim sFilter As String = ""
    Dim iSort As Integer = -1

    Dim mContextMenu As New ContextMenuStrip
    Dim mMenuItem As ToolStripMenuItem
    Dim WithEvents tDataHasChanged As New Timer

    ''' <summary>
    '''Läd die Daten aus der Datenbank in das DataGridView.
    '''Die Spaltenüberschriften werden aus ColNames (Public)
    '''in das DataView und in das Pop-Up-Menu eingetragen
    ''' </summary>
    ''' <param name="sSql">String SQL-Abfrage der Listen-Elemente</param>
    ''' <param name="sGridName">String DataGrid-Name läd die Spalten-Einstellungen aus winback.ini</param>
    ''' <param name="table">dbTable Datenbank Tabelle winback/wbdaten</param>
    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:SQL-Abfragen auf Sicherheitsrisiken überprüfen")>
    Sub LoadData(sSql As String, sGridName As String, Optional table As dbTable = dbTable.winback)
        mContextMenu.SuspendLayout()
        'x mSek nachdem sich der Datensatz geändert hat, wird der aktuelle Datensatz im 
        'Detail-Fenster angezeigt
        tDataHasChanged.Interval = _tDataChangedTime
        tDataHasChanged.Enabled = False
        DtaTable.Clear()

        Select Case My.Settings.WinBackDBType

            'Verbindung über mySql
            Case dbType.mySql
                'Verbindung zur MySQL-Datenbank
                If table = wb_Sql.dbTable.winback Then
                    MySqlCon = New MySqlConnection(My.Settings.MySQLConWinBack)
                Else
                    MySqlCon = New MySqlConnection(My.Settings.MySQLConWbDaten)
                End If
                MySqlCmd = New MySqlCommand(sSql, MySqlCon)
                MySqlDta = New MySqlDataAdapter(MySqlCmd)
                MySqlDta.MissingSchemaAction = MissingSchemaAction.AddWithKey
                MySqlCbd = New MySqlCommandBuilder(MySqlDta)
                Try
                    MySqlDta.Fill(DtaTable)
                Catch e As Exception
                    Debug.Print(e.ToString)
                End Try

            ' Verbindung über msSQL
            Case dbType.msSql
                'Verbindung zur SQL-Datenbank
                If table = wb_Sql.dbTable.winback Then
                    msCon = New SqlConnection(My.Settings.MsSQLConWinBack)
                Else
                    msCon = New SqlConnection(My.Settings.MsSQLConWbDaten)
                End If
                msCmd = New SqlCommand(sSql, msCon)
                msDta = New SqlDataAdapter(msCmd)
                msCbd = New SqlCommandBuilder(msDta)
                Try
                    msDta.Fill(DtaTable)
                Catch
                End Try
        End Select

        DtaView = DtaTable.DefaultView
        DataSource = DtaView

        'Spalten-Überschriften eintragen
        For i = 0 To ColumnCount - 1
            If i < ColNames.Count Then
                'Spalten-Namen, die mit & beginnen werden als Auto-Size Spalten behandelt
                If Microsoft.VisualBasic.Left(ColNames(i), 1) = "&" Then
                    Columns(i).HeaderText = ColNames(i).Remove(0, 1) + Chr(10)
                    Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    'Spalten ohne Bezeichnung werden ausgeblendet
                ElseIf Microsoft.VisualBasic.Left(ColNames(i), 1) = "" Then
                    Columns(i).HeaderText = ""
                    Columns(i).Visible = False
                Else
                    Columns(i).HeaderText = ColNames(i) + Chr(10)
                End If
            Else
                Columns(i).HeaderText = ""
                Columns(i).Visible = False
            End If
        Next

        'Spaltenbreiten aus ini-Datei laden
        LoadFromDisk(sGridName)

        'Popup-Menu - Spalten ein-/ausblenden
        Dim evH As New EventHandler(AddressOf mContextMenu_Click)
        mContextMenu.Items.Add("Spalten ein-/ausblenden")
        mContextMenu.Items.Add(New ToolStripSeparator)

        For i = 0 To ColNames.Count - 1
            If i < ColumnCount Then
                'Spalten ohne Bezeichnung werden ausgeblendet
                If Columns(i).HeaderText IsNot "" Then
                    mMenuItem = New ToolStripMenuItem("", Nothing, evH)
                    mMenuItem.Text = Columns(i).HeaderText.Remove(Columns(i).HeaderText.Length - 1, 1)
                    mMenuItem.Tag = i
                    'Menu-Item ist Check
                    mMenuItem.Checked = True
                    'sichtbare Spalten markieren
                    If Columns(i).Visible Then
                        mMenuItem.CheckState = CheckState.Checked
                    Else
                        mMenuItem.CheckState = CheckState.Unchecked
                    End If
                    mMenuItem.CheckOnClick = True
                    mContextMenu.Items.Add(TryCast(mMenuItem, ToolStripMenuItem))
                End If
            End If
        Next

        'Design
        Dim DataGridViewCellStyle As DataGridViewCellStyle = New DataGridViewCellStyle()
        DataGridViewCellStyle.BackColor = My.Settings.DataGridAlternateRowColor
        AlternatingRowsDefaultCellStyle = DataGridViewCellStyle
        RowHeadersVisible = False
        [ReadOnly] = True
        AllowUserToAddRows = False
        AllowUserToDeleteRows = False
        AllowUserToResizeRows = False
        MultiSelect = False
        SelectionMode = DataGridViewSelectionMode.FullRowSelect
        ContextMenuStrip = mContextMenu
        mContextMenu.ResumeLayout(False)
    End Sub

    ''' <summary>
    ''' Daten im Grid neu laden
    ''' </summary>
    Sub RefreshData()
        Select Case My.Settings.WinBackDBType
            ' Verbindung über mySql
            Case dbType.mySql
                Try
                    DtaTable.Clear()
                    MySqlDta.Fill(DtaTable)
                Catch
                End Try
            ' Verbindung über msSQL
            Case dbType.msSql
                Throw New NotImplementedException
        End Select
    End Sub

    ''' <summary>
    ''' x Sekunden nach Änderung des Datensatz-Zeigers wird der
    ''' Event HasChanged() ausgelöst
    ''' </summary>
    WriteOnly Property tDataChangedTime As Integer
        Set(value As Integer)
            _tDataChangedTime = value
        End Set
    End Property

    ''' <summary>
    ''' zusätzliche Filter-Bedingung (SQL)
    ''' </summary>
    WriteOnly Property Filter As String
        Set(value As String)
            _Filter = value
            sFilter = ""
            DtaView.RowFilter = _Filter
        End Set
    End Property

    ''' <summary>
    ''' Update Datenbank nach Änderung eines Datenfeldes
    ''' </summary>
    Public Sub UpdateDataBase()
        'damit die Update-Routine richtig funktioniert 
        'muss vorher die Zeile im DataGrid gewechselt worden sein !!
        Me.CurrentCell = Nothing
        Select Case My.Settings.WinBackDBType
            ' Verbindung über mySql
            Case dbType.mySql
                MySqlDta.Update(DtaTable)
            ' Verbindung über msSQL
            Case dbType.msSql
                msDta.Update(DtaTable)
        End Select
    End Sub

    ''' <summary>
    ''' Datenbank-Feld lesen/ändern
    ''' </summary>
    ''' <param name="FieldName">String Feldname in Datenbank</param>
    ''' <returns></returns>
    Property Field(FieldName As String) As String
        Set(value As String)
            Try
                If value IsNot Nothing Then
                    CurrentRow.Cells(FieldName).Value = value
                End If
            Catch
            End Try
        End Set
        Get
            Try
                Return CurrentRow.Cells(FieldName).Value.ToString
            Catch
                Return Nothing
            End Try
        End Get
    End Property

    ''' <summary>
    ''' Popup-Menu Spalten ein/ausblenden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mContextMenu_Click(ByVal sender As Object, ByVal e As EventArgs)
        'ausgewählte Spalte steht in MenuItem.Tag
        Dim iColumn As Integer
        iColumn = DirectCast(sender, ToolStripMenuItem).Tag
        'Spalte ein-/ausblenden
        If iColumn >= 0 And iColumn <= ColumnCount Then
            Columns(iColumn).Visible = Not Columns(iColumn).Visible
        End If
    End Sub

    ''' <summary>
    ''' Spaltenbreiten in winback.ini schreiben
    ''' </summary>
    ''' <param name="sGridName">String - Name des Grid. Speichert die Spaltenbreiten in winback.ini in der Sektion [GridName]</param>
    Public Sub SaveToDisk(sGridName As String)
        Dim IniFile As New wb_IniFile
        Dim sColumn As String

        For i = 0 To ColumnCount - 1
            sColumn = "Column" & i.ToString & "-Width"
            If Columns(i).Visible Then
                IniFile.WriteInt(sGridName, sColumn, Columns(i).Width)
            Else
                IniFile.WriteInt(sGridName, sColumn, -1)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Spaltenbreiten aus winback.ini lesen
    ''' </summary>
    ''' <param name="sGridName">String - Name des Grid. Läd die Spaltenbreiten aus winback.ini in der Sektion [GridName]</param>
    Private Sub LoadFromDisk(sGridName As String)
        Dim IniFile As New wb_IniFile
        Dim w, i As Integer

        For i = 0 To ColumnCount - 1
            w = IniFile.ReadInt(sGridName, "Column" & i.ToString & "-Width", 0)
            Try
                If w > 0 And Columns(i).Name IsNot "" And Columns(i).Visible Then
                    Columns(i).Width = w
                    Columns(i).Visible = True
                ElseIf w = -1 Then
                    Columns(i).Visible = False
                End If
            Catch
            End Try
        Next
    End Sub

    ''' <summary>
    ''' Datensatz-Zeiger wurde geändert. Verbundene Text-Felder auslesen und anzeigen
    ''' </summary>
    Public Event HasChanged As EventHandler
    Private Sub DataHasChanged(sender As Object, e As EventArgs) Handles tDataHasChanged.Tick
        tDataHasChanged.Enabled = False
        RaiseEvent HasChanged(Me, EventArgs.Empty)
    End Sub

    ''' <summary>
    '''Start Timer nach Änderung Datensatz-Zeiger
    ''' x Sekunden nach Änderung des Datensatz-Zeigers wird der
    ''' Event HasChanged() ausgelöst
    ''' </summary>
    Private Overloads Sub DataGridView_CurrentCellChanged(sender As Object, e As EventArgs) Handles MyBase.CurrentCellChanged
        'Reset Timer
        tDataHasChanged.Enabled = False
        'Start Timer
        tDataHasChanged.Enabled = True
    End Sub

    ''' <summary>
    ''' Key-Press im Grid - Filter-Kriterium in Header anzeigen
    ''' Filter-String bilden und anwenden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Overloads Sub DataGridView_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        Dim sRowFilter As String
        'Wenn das eingegebene Zeichen einem Suchstring zugeordnet werden kann, wird der Tastendruck nicht mehr weitergegeben
        e.Handled = KeyToString(e.KeyChar, sFilter)

        If iSort > 0 And (e.Handled Or sFilter <> "") Then
            Dim sHeaderName As String
            Try : sHeaderName = ColNames(iSort) : Catch : sHeaderName = "" : End Try
            If Microsoft.VisualBasic.Left(sHeaderName, 1) = "&" Then
                sHeaderName = sHeaderName.Remove(0, 1)
            End If
            If Len(sFilter) > 1 Then
                sRowFilter = Columns(iSort).Name & " LIKE '%" & sFilter & "%'"
                Columns(iSort).HeaderText = sHeaderName & Chr(10) & "~ " & sFilter.ToUpper
            Else
                sRowFilter = Columns(iSort).Name & " LIKE '" & sFilter & "%'"
                Columns(iSort).HeaderText = sHeaderName & Chr(10) & sFilter.ToUpper & "*"
            End If
            If _Filter IsNot "" Then
                sRowFilter = sRowFilter & " AND " & _Filter
            End If
            DtaView.RowFilter = sRowFilter
        Else
            DtaView.RowFilter = _Filter
        End If
    End Sub

    ''' <summary>
    ''' Maus-Klick auf Header-Zeile 
    ''' Sortierkriterium umschalten
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Overloads Sub DataGridView_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles MyBase.ColumnHeaderMouseClick
        If e.ColumnIndex <> iSort Then
            'alten Header wieder restaurieren
            If iSort > 0 Then
                Dim sHeaderName As String
                Try : sHeaderName = ColNames(iSort) : Catch : sHeaderName = "" : End Try
                Columns(iSort).HeaderText = sHeaderName + Chr(10)
            End If

            If Columns(e.ColumnIndex).ValueType = GetType(String) Then
                sFilter = ""
                iSort = e.ColumnIndex
            Else
                iSort = -1
            End If
            DtaView.RowFilter = _Filter
        End If
    End Sub

    ''' <summary>
    ''' Abfangen den Data-Error-Meldungen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Overloads Sub DataGridView_DataError(sender As Object, e As Windows.Forms.DataGridViewDataErrorEventArgs) Handles MyBase.DataError
        'Exception-Text ausgeben
        Debug.Print(e.Exception.ToString)
    End Sub
End Class
