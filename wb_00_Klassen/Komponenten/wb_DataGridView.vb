Imports WinBack.wb_Functions
Imports System.Windows.Forms
Imports WinBack.wb_Sql
Imports MySql.Data.MySqlClient
Imports System.Data
Imports Microsoft.Data.SqlClient

''' <summary>
'''Ableitung der Klasse DataGridView.
'''Enthält die Verbindung von DataGridView zu
'''wahlweise MySQl(winback) oder MSSQL(OrgasoftMain)
'''
'''LoadData(sql) lädt die entsprechenden Daten ins Grid
'''Über Filter kann eine zusätzliche Filter-Eigenschaft
'''angeben werden.
'''Nach tDataChangedTime wird der Event HasChanged ausgelöst,
'''damit kann das aufrufende Programm die entsprechenden
'''Felder abrufen und anzeigen. (Funktion Field)
'''
'''Änderungen über Field werden nach Aufruf der Update-Funtkion
'''in die Datenbank geschrieben.
'''Dazu muss der MySQL-Data-Client der MySQL-Version angepasst
'''sein, sonst funktioniert die Update-Anweisung (automatisch
'''generiert) nicht. 
''' </summary>
Public Class wb_DataGridView
    Inherits System.Windows.Forms.DataGridView

    Dim MySqlCon As MySqlConnection
    Dim MySqlCmd As MySqlCommand
    Dim MySqlDta As MySqlDataAdapter
    Dim MySqlCbd As MySqlCommandBuilder

    Dim msCon As SqlConnection
    Dim msCmd As SqlCommand
    Dim msDta As SqlDataAdapter
    Dim msCbd As SqlCommandBuilder

    Private DtaView As DataView = Nothing
    Private DtaTable As New DataTable

    Private _SuppressChangeEvent As Boolean = False
    Private _tDataChangedTime = 500
    Private _Filter As String = ""
    Private _8859_5_FieldName As String = ""
    Private _HoverRow As Integer

    Public ColNames As New List(Of String)
    Dim sFilter As String = ""
    Dim iSort As Integer = -1

    Dim mContextMenu As New ContextMenuStrip
    Dim mMenuItem As ToolStripMenuItem
    Private bContextMenuInitialized As Boolean = False
    Dim WithEvents tDataHasChanged As New Timer

    'Minimale Spaltenbreite (Doppelclick optimiert die Spaltenbreite wieder)
    Const ColMinWidth = 20

    ''' <summary>
    ''' GridView-Daten als DataTable publizieren. Für ListUndLabel Druck der Artikel/Rohstoff/Rezept/User-Liste
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property LLData As DataTable
        Get
            Return DtaTable
        End Get
    End Property

    ''' <summary>
    ''' Filterbedingung im Klartext.
    ''' (Überschrift für Druck-Report...)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FilterText As String
        Get
            FilterText = ""

            'Sortier-Folge
            If iSort > wb_Global.UNDEFINED AndAlso iSort < ColNames.Count Then
                FilterText += "Sortiert nach " & ColNames(iSort).ToString.Replace("&", "")
            End If
            'Filter
            If sFilter <> "" Then
                FilterText += " - Filter (*" & sFilter & "*)"
            End If

        End Get
    End Property

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2376:Write-only properties should not be used", Justification:="<Ausstehend>")>
    Public WriteOnly Property SetMultiSelect As Boolean
        Set(value As Boolean)
            MultiSelect = value
        End Set
    End Property

    ''' <summary>
    '''Läd die Daten aus der Datenbank in das DataGridView.
    '''Die Spaltenüberschriften werden aus ColNames (Public)
    '''in das DataView und in das Pop-Up-Menu eingetragen
    ''' </summary>
    ''' <param name="sSql">String SQL-Abfrage der Listen-Elemente</param>
    ''' <param name="sGridName">String DataGrid-Name läd die Spalten-Einstellungen aus winback.ini</param>
    ''' <param name="table">dbTable Datenbank Tabelle winback/wbdaten</param>
    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:SQL-Abfragen auf Sicherheitsrisiken überprüfen")>
    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
    Public Sub LoadData(sSql As String, sGridName As String, Optional table As dbTable = dbTable.winback)
        mContextMenu.SuspendLayout()
        'x mSek nachdem sich der Datensatz geändert hat, wird der aktuelle Datensatz im 
        'Detail-Fenster angezeigt
        tDataHasChanged.Interval = _tDataChangedTime
        tDataHasChanged.Enabled = False
        DtaTable.Clear()
        'DtaTable.DataSet.EnforceConstraints = False

        Select Case wb_GlobalSettings.WinBackDBType

            'Verbindung über mySql
            Case wb_Sql.dbType.mySql
                'Verbindung zur MySQL-Datenbank
                If table = wb_Sql.dbTable.winback Then
                    MySqlCon = New MySqlConnection(wb_GlobalSettings.SqlConWinBack)
                Else
                    MySqlCon = New MySqlConnection(wb_GlobalSettings.SqlConWbDaten)
                End If
                MySqlCmd = New MySqlCommand(sSql, MySqlCon)
                MySqlDta = New MySqlDataAdapter(MySqlCmd)
                MySqlDta.MissingSchemaAction = MissingSchemaAction.AddWithKey
                MySqlCbd = New MySqlCommandBuilder(MySqlDta)
                'Einstellungen Fehler
                MySqlDta.ContinueUpdateOnError = True
                MySqlDta.AcceptChangesDuringUpdate = True

                Try
                    MySqlDta.Fill(DtaTable)
                Catch e As Exception
                    'Fehlende Spalten in Datenbank abfangen und Meldung ausgeben
                    If e.ToString.Contains("Unknown column") Then
                        MsgBox("Fehler in Datenbank. Update erforderlich!", MsgBoxStyle.Critical, "Fehler Datenbank")
                    End If
                    Debug.Print("ssql=" & sSql & vbCrLf & e.ToString)
                End Try

            ' Verbindung über msSQL
            Case wb_Sql.dbType.msSql
                'Verbindung zur SQL-Datenbank
                If table = wb_Sql.dbTable.winback Then
                    msCon = New SqlConnection(wb_GlobalSettings.SqlConWinBack)
                Else
                    msCon = New SqlConnection(wb_GlobalSettings.SqlConWbDaten)
                End If
                msCmd = New SqlCommand(sSql, msCon)
                msDta = New SqlDataAdapter(msCmd)
                msCbd = New SqlCommandBuilder(msDta)
                Try
                    msDta.Fill(DtaTable)
                Catch
                    Debug.Print("@E_Fehler beim Speichern DataGridView")
                End Try
        End Select

        DtaView = DtaTable.DefaultView
        DataSource = DtaView
        'Readonly wird pro Spalte festgelegt
        [ReadOnly] = False

        'Spalten-Überschriften eintragen
        For i = 0 To ColumnCount - 1
            If i < ColNames.Count Then
                'Spalten-Namen´, die mit + beginnen sind editierbar
                If Microsoft.VisualBasic.Left(ColNames(i), 1) = "+" Then
                    Columns(i).ReadOnly = False
                    ColNames(i) = ColNames(i).Remove(0, 1)
                Else
                    Columns(i).ReadOnly = True
                End If

                'Spalten-Namen, die mit & beginnen werden als Auto-Size Spalten behandelt
                If (Microsoft.VisualBasic.Left(ColNames(i), 1) = "&") Then
                    Columns(i).HeaderText = ColNames(i).Remove(0, 1) + Chr(10)
                    Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    'Spalten ohne Bezeichnung werden ausgeblendet
                ElseIf Microsoft.VisualBasic.Left(ColNames(i), 1) = "" Then
                    Columns(i).HeaderText = ""
                    Columns(i).Visible = False
                    Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                Else
                    Columns(i).HeaderText = ColNames(i) + Chr(10)
                    Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                End If
            Else
                Columns(i).HeaderText = ""
                Columns(i).Visible = False
            End If

            'Mindenstbreite
            Columns(i).MinimumWidth = ColMinWidth
        Next

        If Not bContextMenuInitialized Then
            'Spaltenbreiten aus ini-Datei laden
            LoadFromDisk(sGridName)
            'Flag initialisiert
            bContextMenuInitialized = True

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
        End If

        'Design
        Dim DataGridViewCellStyle As DataGridViewCellStyle = New DataGridViewCellStyle()
        DataGridViewCellStyle.BackColor = wb_GlobalSettings.DataGridAlternateRowColor
        AlternatingRowsDefaultCellStyle = DataGridViewCellStyle
        RowHeadersVisible = False
        AllowUserToAddRows = False
        AllowUserToDeleteRows = False
        AllowUserToResizeRows = False
        MultiSelect = False
        SelectionMode = DataGridViewSelectionMode.FullRowSelect
        ContextMenuStrip = mContextMenu
        mContextMenu.ResumeLayout(False)
    End Sub

    Friend Sub ClearData()
        DtaTable.Clear()
    End Sub

    ''' <summary>
    ''' ChangeEvent unterdrücken. (z.B. bei Doppelclick Artikel-Liste)
    ''' </summary>
    Friend WriteOnly Property SuppressChangeEvent As Boolean
        Set(value As Boolean)
            _SuppressChangeEvent = value
            'Timer starten (hat keine Auswirkungen). Reset SuppressChangeEvent
            tDataHasChanged.Enabled = True
        End Set
    End Property

    ''' <summary>
    ''' Setzt die Sortierspalte (ohne dass ein Click auf die Titelzeile notwendig ist)
    ''' </summary>
    ''' <param name="ColNr"></param>
    Public Sub SetSortColumn(Optional ColNr As Integer = 1)
        'Sicherheitshalber prüfen ob die Spalten-Nummer gültig ist
        If (ColNr >= 0) AndAlso (ColNr < Columns.Count) Then
            'Sortieren nach Spalte x
            SortColumn(ColNr)
            MyBase.Sort(Columns(ColNr), System.ComponentModel.ListSortDirection.Ascending)
            'Focus auf das DataGridView setzen (KeyPress-Ereignis)
            MyBase.Select()
        End If
    End Sub

    ''' <summary>
    ''' Daten im Grid neu laden
    ''' </summary>
    Sub RefreshData()
        Dim SaveRow As Integer = wb_Global.UNDEFINED
        Dim AktRow As Integer = wb_Global.UNDEFINED
        'ermittelt die erste sichtbare Spalte im Grid. Ist notwendig,
        'weil Me.CurrentCell keine unsichtbaren Spalten selektieren kann
        Dim xcol As Integer = wb_Global.UNDEFINED
        If Me.FirstDisplayedCell IsNot Nothing Then
            xcol = Me.FirstDisplayedCell.ColumnIndex
        End If

        'aktuellen Datensatz merken
        If (Me.Rows.Count > 0) AndAlso (Me.SelectedRows.Count > 0) Then
            AktRow = Me.SelectedRows(0).Index
            SaveRow = Me.FirstDisplayedCell.RowIndex
        End If

        Select Case wb_GlobalSettings.WinBackDBType
            ' Verbindung über mySql
            Case wb_Sql.dbType.mySql
                Try
                    DtaTable.Clear()
                    MySqlDta.Fill(DtaTable)
                Catch
                    Debug.Print("@E_Fehler beim Speichern des DataGridView")
                End Try
            ' Verbindung über msSQL
            Case wb_Sql.dbType.msSql
                Throw New NotImplementedException
        End Select

        'zurück zur aktuellen Zeile
        If AktRow >= Me.RowCount Then
            AktRow = Me.RowCount - 1
        End If
        If SaveRow <> wb_Global.UNDEFINED AndAlso AktRow <> wb_Global.UNDEFINED AndAlso xcol <> wb_Global.UNDEFINED AndAlso SaveRow < Me.RowCount Then
            Me.FirstDisplayedScrollingRowIndex = SaveRow
            Me.Rows(AktRow).Selected = True
            Me.CurrentCell = Me.Item(xcol, AktRow)
            RaiseEvent HasChanged(Me, EventArgs.Empty)
        End If
    End Sub

    ''' <summary>
    ''' x Sekunden nach Änderung des Datensatz-Zeigers wird der
    ''' Event HasChanged() ausgelöst
    ''' </summary>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2376:Write-only properties should not be used", Justification:="<Ausstehend>")>
    WriteOnly Property tDataChangedTime As Integer
        Set(value As Integer)
            _tDataChangedTime = value
        End Set
    End Property

    ''' <summary>
    ''' zusätzliche Filter-Bedingung (SQL)
    ''' </summary>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2376:Write-only properties should not be used", Justification:="<Ausstehend>")>
    WriteOnly Property Filter As String
        Set(value As String)
            _Filter = value
            sFilter = ""
            If DtaView IsNot Nothing Then
                Try
                    DtaView.RowFilter = _Filter
                Catch ex As Exception
                    _Filter = ""
                End Try
            End If
        End Set
    End Property

    ''' <summary>
    ''' Spalte in der gesucht werden soll.(Filter)
    ''' </summary>
    ''' <returns></returns>
    Property SortCol As Integer
        Get
            Return iSort
        End Get
        Set(value As Integer)
            iSort = value
        End Set
    End Property

    ''' <summary>
    ''' Zeilen-Nummer unter der Maus (Hover). Zeilen-Index bei Maus-Click rechts
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HoverRow As Integer
        Get
            Return _HoverRow
        End Get
    End Property

    ''' <summary>
    ''' Popup-Menu - Item anfügen. Der Eventhandler wird in der aufrufenden Routine festgelegt.
    ''' </summary>
    ''' <param name="Text"></param>
    ''' <param name="Tag"></param>
    ''' <param name="Image"></param>
    ''' <param name="OnClick"></param>
    ''' <param name="Separator"></param>
    Public Sub PopupItemAdd(Text As String, Tag As String, Image As Drawing.Image, OnClick As EventHandler, Optional Separator As Boolean = False, Optional Checked As Boolean = False)
        'Menu-Item anfügen
        Dim mMenuItem As New System.Windows.Forms.ToolStripMenuItem(Text, Image, OnClick)
        mMenuItem.Tag = Tag
        mMenuItem.Checked = Checked
        mMenuItem.CheckState = CheckState.Unchecked
        mMenuItem.CheckOnClick = Checked

        mContextMenu.Items.Add(TryCast(mMenuItem, ToolStripMenuItem))

        'Wenn notwendig, Separator anfügen
        If Separator Then
            mContextMenu.Items.Add(New ToolStripSeparator)
        End If
    End Sub

    Public Sub PopupItemAdd(Text As String, Tag As String, Image As Drawing.Image, Separator As Boolean, ParamArray DropDownItems() As ToolStripItem)
        'Menu-Item anfügen
        Try
            Dim mMenuItem As New System.Windows.Forms.ToolStripMenuItem(Text, Image, DropDownItems)
            mMenuItem.Tag = Tag
            'mMenuItem.Checked = False
            'mMenuItem.CheckState = CheckState.Unchecked
            'mMenuItem.CheckOnClick = False

            mContextMenu.Items.Add(TryCast(mMenuItem, ToolStripMenuItem))

            'Wenn notwendig, Separator anfügen
            If Separator Then
                mContextMenu.Items.Add(New ToolStripSeparator)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub PopupItemsUncheck(Items As List(Of String))
        'alle Popup-Menu-Einträge
        For Each m In mContextMenu.Items
            If Items.Contains(m.Tag) Then
                TryCast(m, ToolStripMenuItem).CheckState = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' Bestimmt die aktuelle Zeile unter dem Maus-Zeiger. Die Zelle muss dazu nicht selektiert sein.
    ''' Dient zur Bestimmung der Zeile bei Maus-Click rechts.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView_CellMouseenter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MyBase.CellMouseEnter
        _HoverRow = e.RowIndex
    End Sub

    ''' <summary>
    ''' Update Datenbank nach Änderung eines Datenfeldes.
    ''' Der DataHasChanged-Event muss unterdrückt werden, sonst treten beim Schliessen der Forms Fehler auf, da dann
    ''' mit x Sekunden Zeitverzögerung der Change-Event auftritt und ins Leere läuft.
    ''' </summary>
    Public Sub UpdateDataBase()
        'DataHasChanged-Event unterdrücken
        _SuppressChangeEvent = True

        ''DEBUG - CurrentCell
        'Dim cc As Integer = Me.CurrentCell.RowIndex
        ''DEBUG - Alle Elemente an dieser Stelle im Array
        'Debug.Print("Vor CurrentCell = Nothing")
        'For i = 0 To DtaTable.Columns.Count - 1
        '    Debug.Print(DtaTable.Rows(cc).ItemArray(i).ToString)
        'Next

        'damit die Update-Routine richtig funktioniert 
        'muss vorher die Zeile im DataGrid gewechselt worden sein !!
        Try
            Me.CurrentCell = Nothing
        Catch
            Debug.Print("@E_Fehler beim Speichern Des DatagridView")
        End Try

        ''DEBUG - Alle Elemente an dieser Stelle im Array
        'Debug.Print("Nach CurrentCell = Nothing")
        'For i = 0 To DtaTable.Columns.Count - 1
        '    Debug.Print(DtaTable.Rows(cc).ItemArray(i).ToString)
        'Next

        Select Case wb_GlobalSettings.WinBackDBType
            ' Verbindung über mySql
            Case wb_Sql.dbType.mySql
                If MySqlDta IsNot Nothing Then
                    MySqlDta.Update(DtaTable)
                End If
                ' Verbindung über msSQL
            Case wb_Sql.dbType.msSql
                If msDta IsNot Nothing Then
                    msDta.Update(DtaTable)
                End If
        End Select
        'DataHasChanged-Event wieder freigeben
        _SuppressChangeEvent = False
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
                    If FieldName = _8859_5_FieldName Then
                        CurrentRow.Cells(FieldName).Value = wb_Functions.UTF8toMySql(value)
                    Else
                        CurrentRow.Cells(FieldName).Value = value
                    End If
                End If
            Catch
                Debug.Print("Exception in DataGridView.Field: FieldName = " & FieldName)
            End Try
        End Set
        Get
            'Prüfen ob überhaupt Datensätze vorhanden sind (Filter-Result)
            If DtaView.Count > 0 AndAlso CurrentRow IsNot Nothing Then
                Try
                    If FieldName = _8859_5_FieldName Then
                        Return wb_Functions.MySqlToUtf8(CurrentRow.Cells(FieldName).Value.ToString())
                    Else
                        Return CurrentRow.Cells(FieldName).Value.ToString
                    End If
                Catch
                    Return Nothing
                End Try
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Datenbankfeld aus einer definierten Zeile (nicht selektiert) lesen
    ''' </summary>
    ''' <param name="FieldName"></param>
    ''' <param name="iRow"></param>
    ''' <returns></returns>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2352:Indexed properties with more than one parameter should not be used", Justification:="<Ausstehend>")>
    ReadOnly Property Field(FieldName As String, iRow As Integer) As String
        Get
            Try
                If FieldName = _8859_5_FieldName Then
                    Return wb_Functions.MySqlToUtf8(Rows(iRow).Cells(FieldName).Value.ToString)
                Else
                    Return Rows(iRow).Cells(FieldName).Value.ToString
                End If
            Catch
                Debug.Print("Exception in DataGridView.Field: FieldName = " & FieldName)
                Return Nothing
            End Try
        End Get
    End Property

    ''' <summary>
    ''' Datenbank-Feld lesen/ändern
    ''' </summary>
    ''' <param name="FieldName">String Feldname in Datenbank</param>
    ''' <returns>Integer</returns>
    Property iField(FieldName As String) As Integer
        Set(value As Integer)
            Try
                CurrentRow.Cells(FieldName).Value = CStr(value)
            Catch
                Debug.Print("Fehler beim Ändern DataGridView.iField Name/Wert " & FieldName & "/" & value.ToString)
            End Try
        End Set
        Get
            Try
                If CurrentRow IsNot Nothing Then
                    Return wb_Functions.StrToInt(CurrentRow.Cells(FieldName).Value.ToString)
                Else
                    Return 0
                End If
            Catch
                Return 0
            End Try
        End Get
    End Property

    ''' <summary>
    ''' Feld-Name des Datenbank-Feldes, das bei Fremdsprachen konvertiert werden muss (ISO8859-5 nach UTF-8)
    ''' </summary>
    ''' <returns></returns>
    Public Property x8859_5_FieldName As String
        Get
            Return _8859_5_FieldName
        End Get
        Set(value As String)
            _8859_5_FieldName = value
        End Set
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
        If iColumn >= 0 AndAlso iColumn <= ColumnCount Then
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
                If w > 0 AndAlso Columns(i).Name IsNot "" AndAlso Columns(i).Visible AndAlso Columns(i) IsNot Nothing Then
                    If Columns(i).AutoSizeMode <> DataGridViewAutoSizeColumnMode.Fill Then
                        Columns(i).Width = w
                    End If
                    Columns(i).Visible = True
                ElseIf w = -1 Then
                        Columns(i).Visible = False
                End If
            Catch
            End Try
        Next
    End Sub

    ''' <summary>
    ''' Einen Datensatz im Grid suchen. Wenn der Datensatz gefunden wurde, wird die entsprechende Zeile markiert und
    ''' nach der Zeit tDataChangedTime der Event HasChanged ausgelöst.
    ''' </summary>
    ''' <param name="col"> (Integer) Spalte in der gesucht werden soll</param>
    ''' <param name="s">   (String)  Suchbegriff</param>
    ''' <returns>
    ''' True - Wert gefunden
    ''' False - Wert nicht gefunden</returns>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S3385:""Exit"" statements should not be used", Justification:="<Ausstehend>")>
    Public Function SelectData(col As Integer, s As String) As Boolean
        'ermittelt die erste sichtbare Spalte im Grid. Ist notwendig,
        'weil Me.CurrentCell keine unsichtbaren Spalten selektieren kann
        Dim xcol As Integer = Me.FirstDisplayedCell.ColumnIndex

        'Keine Zeile selektiert
        Me.Rows(0).Selected = False
        'alle Zeilen durchsuchen
        For i As Integer = 0 To Me.RowCount - 1
            If Me.Rows(i).Cells(col).Value.ToString = s Then
                'Zeile markieren
                Me.Rows(i).Selected = True
                Me.CurrentCell = Me.Item(xcol, i)
                'Scollt zur markierten Zeile
                Me.FirstDisplayedScrollingRowIndex = i
                'Zeile gefunden
                Return True
                Exit For
            End If
        Next
        'keine passende Zeile gefunden
        Return False
    End Function

    ''' <summary>
    ''' Datensatz-Zeiger wurde geändert. Verbundene Text-Felder auslesen und anzeigen
    ''' </summary>
    Public Event HasChanged As EventHandler
    Private Sub DataHasChanged(sender As Object, e As EventArgs) Handles tDataHasChanged.Tick
        tDataHasChanged.Enabled = False
        If Not _SuppressChangeEvent Then
            RaiseEvent HasChanged(Me, EventArgs.Empty)
        End If
        _SuppressChangeEvent = False
    End Sub

    ''' <summary>
    '''Start Timer nach Änderung Datensatz-Zeiger
    ''' x Sekunden nach Änderung des Datensatz-Zeigers wird der
    ''' Event HasChanged() ausgelöst.
    ''' 
    ''' Der Timer wird nicht gestartet, wenn der Event unterdrückt werden soll (_SuppressChangeEvent), z.B. beim
    ''' Speichern des Datensatzes im Grid (UpdateDataBase)
    ''' </summary>
    Private Overloads Sub DataGridView_CurrentCellChanged(sender As Object, e As EventArgs) Handles MyBase.CurrentCellChanged
        'Reset Timer
        tDataHasChanged.Enabled = False
        'Start Timer wenn die Freigabe des Change-Events da ist
        If Not _SuppressChangeEvent Then
            tDataHasChanged.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Return-Taste abfangen. Auswahl übernehmen (entspricht Doppelclick)
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="keyData"></param>
    ''' <returns></returns>
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        'Debug.Print("ProcessCmdKey " & keyData.ToString)
        'Return-Taste abfangen
        If keyData = Keys.Return Then
            'Doppelclick simulieren
            OnDoubleClick(Nothing)
            Return True
        End If
        Return False
    End Function

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

        If iSort >= 0 AndAlso (e.Handled OrElse sFilter <> "") Then
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
    ''' Doppel-Click auf die Header-Zelle setzt die Breite auf Optimalwert.
    ''' Die Spalten sind mindestens 20px breit.
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Overloads Sub DataGrdiView_ColumnHeadermouseDblClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles MyBase.ColumnHeaderMouseDoubleClick
        Me.AutoResizeColumn(e.ColumnIndex)
    End Sub

    ''' <summary>
    ''' Maus-Klick auf Header-Zeile 
    ''' Sortierkriterium umschalten
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Overloads Sub DataGridView_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles MyBase.ColumnHeaderMouseClick
        'alten Filter löschen (entspricht dem Verhalten des alten WinBack-Office)
        ResetFilter()
        'Sortieren nach Spalte x
        SortColumn(e.ColumnIndex)
    End Sub

    Private Sub SortColumn(e As Integer)
        If e <> iSort Then
            'alten Header wieder restaurieren
            If iSort > 0 Then
                Dim sHeaderName As String
                Try : sHeaderName = ColNames(iSort) : Catch : sHeaderName = "" : End Try
                If Microsoft.VisualBasic.Left(sHeaderName, 1) = "&" Then
                    sHeaderName = sHeaderName.Remove(0, 1)
                End If
                Columns(iSort).HeaderText = sHeaderName + Chr(10)
            End If

            If Columns(e).ValueType = GetType(String) Then
                sFilter = ""
                iSort = e
            Else
                iSort = -1
            End If
            DtaView.RowFilter = _Filter
        End If
    End Sub

    ''' <summary>
    ''' Sortier-Kriterium zurücksetzen.
    ''' </summary>
    Public Sub ResetFilter()
        'alten Header wieder restaurieren
        If iSort > 0 Then
            Dim sHeaderName As String
            Try : sHeaderName = ColNames(iSort) : Catch : sHeaderName = "" : End Try
            If Microsoft.VisualBasic.Left(sHeaderName, 1) = "&" Then
                sHeaderName = sHeaderName.Remove(0, 1)
            End If
            Columns(iSort).HeaderText = sHeaderName + Chr(10)
        End If
        'Reset Filter
        sFilter = ""
        iSort = -1
        DtaView.RowFilter = _Filter
    End Sub

    ''' <summary>
    ''' Ausgabe des Datenbank-Feldes.
    ''' Es wird anhand des Feldnamens geprüft, ob das Datenbank-Feld aus der MySQL-Datenbank von iso-8859-5 nach utf-8 konvertiert werden muss.
    ''' 
    ''' Die Datenbankfelder KA_Grp1 und KA_Grp2 werden anhand der Tabelle ItemParameter aufgelöst
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView_CellFormating(ByVal Sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        'abhängig von Datenbank-Feld-Namen
        Select Case MyBase.Columns(e.ColumnIndex).Name
            Case x8859_5_FieldName
                If Not IsDBNull(e.Value) Then
                    e.Value = MySqlToUtf8(e.Value)
                End If
            Case "KA_Grp1", "KA_Grp2"
                'Bezeichnung aus Rohstoff-Gruppe
                e.Value = wb_Rohstoffe_Shared.GetRohstoffGruppeFromNr(wb_Functions.StrToInt(e.Value))
        End Select
    End Sub

    ''' <summary>
    ''' Abfangen den Data-Error-Meldungen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Overloads Sub DataGridView_DataError(sender As Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles MyBase.DataError
        'Exception-Text ausgeben
        Debug.Print(e.Exception.ToString)
    End Sub

    ''' <summary>
    ''' Beim Doppelclick auf die Datenzeile wird vorher der Event HasChanged ausgelöst. Damit werden die Daten sicher geladen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Overloads Sub DataGridView_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        If tDataHasChanged.Enabled Then
            RaiseEvent HasChanged(Me, EventArgs.Empty)
        End If
    End Sub

End Class
