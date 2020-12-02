Imports System.IO
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking


Public Class wb_Admin_Timer
    Inherits DockContent

    Private WithEvents AktionsTimerGrid As wb_TimerGridView
    Private WithEvents EditTimer As wb_Admin_TimerEdit
    Private tArray As New ArrayList

    Private Sub wb_Admin_Timer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        '   Spalten mit & dienen als Ausgleich der Breite
        '   Spalten mit # enthalten Datum/Uhrzeit-Angaben
        Dim sColNames As New List(Of String)
        sColNames.AddRange({"", "&Task", "Startzeit", "Periode", "Status"})

        LoadAktionsTimer()
        AktionsTimerGrid = New wb_TimerGridView(tArray, sColNames)
        'Tabelle darf editiert werden
        AktionsTimerGrid.ReadOnly = True

        'Tabelle Timer anzeigen
        ShowAktionsTimer()
        RefreshAktionsTimer()

        'Event-Handler (Aktualisierung der Aktions-Timer im WinBack-Server-Task)
        AddHandler wb_Main_Shared.eTimer, AddressOf AktTimerFromServerTask
    End Sub

    ''' <summary>
    ''' Der WinBack-Server-Task meldet eine Änderung der Aktions-Timer
    ''' Aktionen abhänging von EventArgs(e) ausführen:
    ''' 
    '''     Reload     - Daten neu einlesen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Sub AktTimerFromServerTask(sender As Object, e As String)
        'Falls Parameter vorhanden sind
        Try
            'Meldungen und Parameter trennen
            Dim m As String = e.Trim("|")
            Dim Message() As String = m.Split("|")

            'Aktion abhängig vom EventArgs
            Select Case Message(0)
                Case "REFRESH"
                    'Timer-Einstellungen haben sich geändert - Daten neu laden und anzeigen
                    LoadAktionsTimer()
                    'Array neu initialisieren und anzeigen
                    AktionsTimerGrid.RefreshGrid(tArray)
                Case "RUN"
                    'Timer-Task wird als Argument mitgeschickt
                    LoadAktionsTimer()
                    SetStatusAktionsTimer(Message(1))
                    'Array neu initialisieren und anzeigen
                    AktionsTimerGrid.RefreshGrid(tArray)
            End Select
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Setzt das (interne)Run-Flag für den angegebenen Task auf True. Alle anderen Tasks werden auf False gesetzt
    ''' </summary>
    ''' <param name="Task"></param>
    Private Sub SetStatusAktionsTimer(Task As String)
        'Schleife über alle Timer-Einträge
        For Each TimerEvent As wb_TimerEvent In tArray
            'Task prüfen
            If TimerEvent.Task = Task Then
                TimerEvent.SetRunFlag = True
            Else
                TimerEvent.SetRunFlag = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' Läd die Daten aus der Tabelle winback.AktionsTimer in tArray
    ''' </summary>
    Private Sub LoadAktionsTimer()
        'Array leeren
        tArray.Clear()
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        If winback.sqlSelect(wb_Sql_Selects.sqlAktionsTimer) Then
            While winback.Read
                Dim _Item As New wb_TimerEvent
                _Item.MySQLdbRead(winback.MySqlRead)
                tArray.Add(_Item)
            End While
        End If
        'Datenbank-Verbindung wieder schliessen
        winback.Close()
        winback = Nothing
    End Sub

    ''' <summary>
    ''' Zeigt das aktualisierte Array der Timer-Einstellungen aus der Tabelle winback.AktionsTimer
    ''' </summary>
    Private Sub ShowAktionsTimer()
        'Daten im Grid anzeigen
        AktionsTimerGrid.ScrollBars = Windows.Forms.ScrollBars.Vertical
        AktionsTimerGrid.BackgroundColor = Me.BackColor
        AktionsTimerGrid.GridLocation(tbAktionsTimer)
        AktionsTimerGrid.PerformLayout()
    End Sub

    Private Sub RefreshAktionsTimer()
        'Refresh Tabelle nur wenn die entsprechende Seite sichtbar ist
        AktionsTimerGrid.FillGrid()
        'Zeit zum Zeichnen
        System.Threading.Thread.Sleep(100)
    End Sub

    Private Sub AktionsTimerDrawCell(ByVal sender As Object, ByVal e As DataGridViewCellPaintingEventArgs) Handles AktionsTimerGrid.CellPainting
        Dim Grid = DirectCast(sender, DataGridView)
        If e.ColumnIndex = wb_TimerGridView.COLSTAT And (e.RowIndex >= 0) Then
            Dim _Brush As New Drawing.SolidBrush(Drawing.Color.Yellow)
            Select Case e.Value
                Case wb_Global.wbAktionsTimerStatus.Disabled
                    _Brush.Color = Drawing.Color.Red
                Case wb_Global.wbAktionsTimerStatus.Enabled
                    _Brush.Color = Drawing.Color.Yellow
                Case wb_Global.wbAktionsTimerStatus.Running
                    _Brush.Color = Drawing.Color.Green
                Case Else
                    Return
            End Select
            e.Graphics.FillRectangle(_Brush, e.CellBounds)
            e.Handled = True
        Else
            Return
        End If
    End Sub

    ''' <summary>
    ''' Doppelclick auf Aktions-Timer-Tabelle.
    ''' Edit-Fenster Start-Zeit, Zyklus und Sonderfunktionen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AktionsTimerDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles AktionsTimerGrid.CellMouseDoubleClick
        Dim Grid = DirectCast(sender, DataGridView)

        If (e.RowIndex >= 0) Then
            'wenn das Fenster noch nicht vorhanden ist - erzeugen
            If EditTimer Is Nothing Then
                EditTimer = New wb_Admin_TimerEdit
            End If

            'Index (Zeiger auf Datensatz im Grid/Array)
            EditTimer.Index = e.RowIndex
            'Task-Name
            EditTimer.TimerName = DirectCast(tArray(e.RowIndex), wb_TimerEvent).Task
            'Timer Bezeichnung
            EditTimer.TimerBezeichnung = DirectCast(tArray(e.RowIndex), wb_TimerEvent).Task
            'Task-Start Datum/Uhrzeit
            EditTimer.TimerStart = DirectCast(tArray(e.RowIndex), wb_TimerEvent).Startzeit
            'Task-Zyklus
            EditTimer.TimerZyklus = DirectCast(tArray(e.RowIndex), wb_TimerEvent).Periode
            'aktueller Index
            EditTimer.TimerAktIndex = DirectCast(tArray(e.RowIndex), wb_TimerEvent).Str2
            'Task aktiv
            If DirectCast(tArray(e.RowIndex), wb_TimerEvent).Status = wb_Global.wbAktionsTimerStatus.Disabled Then
                EditTimer.TimerAktiv = False
            Else
                EditTimer.TimerAktiv = True
            End If

            'Eingabe-Form anzeigen
            EditTimer.ShowDialog()
        End If
    End Sub

    ''' <summary>
    ''' Fenster Edit Timer-Events wird geschlossen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub EditTimerEventClosing(Sender As Object, e As EventArgs) Handles EditTimer.Closing
        'Index (Zeiger auf Datensatz im Grid/Array)
        Dim i As Integer = EditTimer.Index

        'Daten im Array aktualisieren - Startzeit
        DirectCast(tArray(i), wb_TimerEvent).Startzeit = EditTimer.TimerStart
        'Daten im Array aktualisieren - Zyklus
        DirectCast(tArray(i), wb_TimerEvent).Periode = EditTimer.TimerZyklus
        'Daten im Array aktualisieren - aktueller Index
        DirectCast(tArray(i), wb_TimerEvent).Str2 = EditTimer.TimerAktIndex
        'Timer aktiv (Wenn der Timer aktuell läuft, wird er erst nach dem Ende des Task auf inaktiv gesetzt
        DirectCast(tArray(i), wb_TimerEvent).Aktiv = EditTimer.TimerAktiv

        'Anzeige der Tabelle aktualisieren
        RefreshAktionsTimer()
        'Daten in MySQl sichern
        DirectCast(tArray(i), wb_TimerEvent).MySQLdbUpdate_Fields()
        'Eingabe-Fenster wieder freigeben
        EditTimer = Nothing
        'Anzeige der Tabelle in allen verbundenen Clients (Message)
        SendRefreshAktionsTimer()
    End Sub

    Private Sub SendRefreshAktionsTimer()
        'Message an alle verbundenen Clients
        wb_Main_Shared.SendMessage(Nothing, "TIMR|REFRESH")
    End Sub

    Private Sub wb_Admin_Timer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'alle Events wieder abmelden
        RemoveHandler wb_Main_Shared.eTimer, AddressOf AktTimerFromServerTask
    End Sub
End Class