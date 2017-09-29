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

    Private Sub wb_Admin_Sync_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        HideResult()
    End Sub

    Private Sub btnSyncUserGruppen_Click(sender As Object, e As EventArgs) Handles btnSyncUserGruppen.Click
        'Liste der Tabellen-Überschriften
        Dim sColNames As New List(Of String) From {"", "Nr", "&WinBack-Benutzer-Gruppe", "", "Nr", "&OrgaBack-Benutzer-Gruppe", "", "Status"}
        Text = "Synchronisation Datenbank - Benutzergruppen"
        SyncType = wb_Global.SyncType.BenutzerGruppen
        HideResult()

        'Cursor umschalten
        Me.Cursor = Cursors.WaitCursor
        'Benutzer aus WinBack in Array einlesen
        wbUserGrp.DBRead()
        'Benutzer aus OrgaSoft in Array einlesen
        osUserGrp.DBRead()

        'Daten/Synchronisation prüfen und Ergebnis berechnen
        wbUserGrp.Case_01 = wb_Global.SyncState.WinBackMiss     'falls ein Eintrag in WinBack fehlt, wird der Konflikt nicht repariert
        wbUserGrp.Case_11 = wb_Global.SyncState.OrgaBackUpdate  'falls ein Eintrag in beiden Datenbanken vorhanden ist, wird in OrgaBack geschrieben
        wbUserGrp.CheckSync(osUserGrp.Data)

        'Ergebnis im Grid anzeigen
        DisplayResultGrid(wbUserGrp.Data, sColNames)
        DisplayResultText(wbUserGrp, osUserGrp)
        ShowResult()
        'Cursor wieder umschalten
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnSyncUser_Click(sender As Object, e As EventArgs) Handles btnSyncUser.Click
        'Liste der Tabellen-Überschriften
        Dim sColNames As New List(Of String) From {"", "Login", "&WinBack-Benutzer", "Grp", "Kassierer-Nr", "&OrgaBack-Benutzer", "Grp", "Status"}
        Text = "Synchronisation Datenbank - Mitarbeiter"
        SyncType = wb_Global.SyncType.Benutzer
        HideResult()

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
        DisplayResultText(wbUser, osUser)
        ShowResult()
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

    Private Sub ExcecuteSync(ByRef wb As wb_Sync, ByRef os As wb_Sync)
        'Synchronisation in WinBack-DB
        For Each x As wb_SyncItem In wb.Data
            Select Case x.SyncOK
                Case wb_Global.SyncState.WinBackWrite
                    wb.DBInsert(x.os_Nummer, x.os_Bezeichnung, x.os_Gruppe)
                Case wb_Global.SyncState.WinBackUpdate
                    wb.DBUpdate(x.os_Nummer, x.os_Bezeichnung, x.os_Gruppe)
            End Select
        Next
        'Synchronisation in OrgaBack-DB
        For Each x As wb_SyncItem In os.Data
            Select Case x.SyncOK
                Case wb_Global.SyncState.OrgaBackWrite
                    os.DBInsert(x.wb_Nummer, x.wb_Bezeichnung, x.wb_Gruppe)
                Case wb_Global.SyncState.OrgaBackUpdate
                    os.DBUpdate(x.wb_Nummer, x.wb_Bezeichnung, x.wb_Gruppe)
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

    Private Sub DisplayResultText(ByRef wb As wb_Sync, ByRef os As wb_Sync)
        'Zusammenfassung (Summe der Datensätze) anzeigen
        lvSyncResult.Items.Clear()
        lvSyncResult.Items.Add("OrgaBack")
        lvSyncResult.Items.Add("WinBack")

        lvSyncResult.Items(0).SubItems.Add(os.CntSyncAll)
        lvSyncResult.Items(0).SubItems.Add(wb.CntSync_osErr + os.CntSync_osErr)
        lvSyncResult.Items(0).SubItems.Add(wb.CntSync_osWrite + os.CntSync_osWrite)
        lvSyncResult.Items(0).SubItems.Add(wb.CntSync_osUpdate + os.CntSync_osUpdate)
        lvSyncResult.Items(0).SubItems.Add(wb.CntSync_osMiss + os.CntSync_osMiss)
        lvSyncResult.Items(1).SubItems.Add(wb.CntSyncAll)
        lvSyncResult.Items(1).SubItems.Add(wb.CntSync_wbErr + os.CntSync_wbErr)
        lvSyncResult.Items(1).SubItems.Add(wb.CntSync_wbWrite + os.CntSync_wbWrite)
        lvSyncResult.Items(1).SubItems.Add(wb.CntSync_wbUpdate + os.CntSync_wbUpdate)
        lvSyncResult.Items(1).SubItems.Add(wb.CntSync_wbMiss + os.CntSync_wbMiss)
    End Sub

    Private Sub HideResult()
        btnExportExcel.Enabled = False
        btnSyncStart.Enabled = False
        lvSyncResult.Items.Clear()
        lvSyncResult.Enabled = False
    End Sub

    Private Sub ShowResult()
        btnExportExcel.Enabled = True
        btnSyncStart.Enabled = True
        lvSyncResult.Enabled = True
    End Sub

End Class