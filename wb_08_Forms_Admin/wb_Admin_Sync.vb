Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_SyncUser_X


Public Class wb_Admin_Sync
    Inherits DockContent

    Dim wbUserGrp As New wb_SyncUserGruppen_WinBack
    Dim osUserGrp As New wb_SyncUserGruppen_OrgaBack
    Dim wbUser As New wb_SyncUser_WinBack
    Dim osUser As New wb_SyncUser_OrgaBack

    Dim SyncResultGrid As wb_SyncGridView
    Dim SyncType As wb_Global.SyncType = wb_Global.SyncType.Undefined

    Private Sub btnSyncUserGruppen_Click(sender As Object, e As EventArgs) Handles btnSyncUserGruppen.Click
        'Liste der Tabellen-Überschriften
        Dim sColNames As New List(Of String) From {"", "Nr", "&WinBack-Benutzer-Gruppe", "Nr", "&OrgaBack-Benutzer-Gruppe", "Status"}
        SyncType = wb_Global.SyncType.BenutzerGruppen

        'Cursor umschalten
        Me.Cursor = Cursors.WaitCursor
        'Benutzer aus WinBack in Array einlesen
        wbUserGrp.DBRead()
        'Benutzer aus OrgaSoft in Array einlesen
        osUserGrp.DBRead()
        'Daten/Synchronisation prüfen und Ergebnis berechnen
        wbUserGrp.CheckSync(osUserGrp.Data)
        'Ergebnis im Grid anzeigen
        DisplayResultGrid(wbUserGrp.Data, sColNames)
        'Cursor wieder umschalten
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnSyncUser_Click(sender As Object, e As EventArgs) Handles btnSyncUser.Click
        'Liste der Tabellen-Überschriften
        Dim sColNames As New List(Of String) From {"", "Login", "&WinBack-Benutzer", "Kassierer-Nr", "&OrgaBack-Benutzer", "Status"}
        SyncType = wb_Global.SyncType.Benutzer

        'Cursor umschalten
        Me.Cursor = Cursors.WaitCursor
        'Benutzer aus WinBack in Array einlesen
        wbUser.DBRead()
        'Benutzer aus OrgaSoft in Array einlesen
        osUser.DBRead()

        'Daten/Synchronisation prüfen und Ergebnis berechnen
        wbUser.Case_10 = wb_Global.SyncState.OrgaBackMiss   'falls ein Eintrag in OrgaBack fehlt, wird der Konflikt nicht repariert !
        wbUser.CheckSync(osUser.Data)

        'Ergebnis im Grid anzeigen
        DisplayResultGrid(wbUser.Data, sColNames)
        'Cursor wieder umschalten
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnSyncStart_Click(sender As Object, e As EventArgs) Handles btnSyncStart.Click
        Select Case SyncType
            Case wb_Global.SyncType.BenutzerGruppen
                ExcecuteSync(wbUserGrp, osUserGrp)
            Case wb_Global.SyncType.Benutzer
                ExcecuteSync(wbUser, osUser)
        End Select
    End Sub

    Private Sub ExcecuteSync(wb As wb_Sync, os As wb_Sync)
        'Synchronisation in WinBack-DB
        For Each x As wb_SyncItem In wb.Data
            Select Case x.SyncOK
                Case wb_Global.SyncState.WinBackWrite
                    wb.DBInsert(x.os_Nummer, x.os_Bezeichnung)
                Case wb_Global.SyncState.WinBackUpdate
                    wb.DBUpdate(x.os_Nummer, x.os_Bezeichnung)
            End Select
        Next
        'Synchronisation in OrgaBack-DB
        For Each x As wb_SyncItem In os.Data
            Select Case x.SyncOK
                Case wb_Global.SyncState.OrgaBackWrite
                    os.DBInsert(x.wb_Nummer, x.wb_Bezeichnung)
                Case wb_Global.SyncState.OrgaBackUpdate
                    os.DBUpdate(x.wb_Nummer, x.wb_Bezeichnung)
            End Select
        Next
    End Sub


    Private Sub DisplayResultGrid(ByVal xArray As ArrayList, ByVal sColNames As List(Of String))
        'Wenn alte Daten vorhanden sind vorher löschen
        If SyncResultGrid IsNot Nothing Then
            SyncResultGrid.Dispose()
            SyncResultGrid = Nothing
        End If
        'Daten im Grid anzeigen
        SyncResultGrid = New wb_SyncGridView(xArray, sColNames)
        SyncResultGrid.BackgroundColor = Me.BackColor
        SyncResultGrid.GridLocation(tbSyncResult)
        SyncResultGrid.PerformLayout()
        SyncResultGrid.Refresh()
    End Sub

End Class