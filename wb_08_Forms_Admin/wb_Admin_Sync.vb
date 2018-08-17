Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Admin_Sync
    Inherits DockContent

    Dim wbUserGrp As New wb_SyncUserGruppen_WinBack
    Dim osUserGrp As New wb_SyncUserGruppen_OrgaBack
    Dim wbUser As New wb_SyncUser_WinBack
    Dim osUser As New wb_SyncUser_OrgaBack
    Dim wbArtikel As New wb_SyncArtikel_WinBack
    Dim osArtikel As New wb_SyncArtikel_OrgaBack
    Dim wbRohstoffe As New wb_SyncRohstoffe_WinBack
    Dim osRohstoffe As New wb_SyncRohstoffe_OrgaBack

    Dim SyncResultGrid As wb_SyncGridView
    Dim sColNames As New List(Of String)
    Dim SyncType As wb_Global.SyncType = wb_Global.SyncType.Undefined

    Dim cntSync_wbMissMatch As Integer
    Dim cntSync_wbWriteMatch As Integer
    Dim cntSync_osMissMatch As Integer
    Dim cntSync_osWriteMatch As Integer
    Dim cntSync_SumMatch As Integer

    Dim pDialog As New wb_PrinterDialog 'Drucker-Dialog

    Private Sub wb_Admin_Sync_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Ausgabe des Ergebnis-Blocks ausblenden
        HideResult()
        'List und Label-Verzeichnis für die Listen
        pDialog.ListSubDirectory = "AdminSync"
    End Sub

    Private Sub btnSyncUserGruppen_Click(sender As Object, e As EventArgs) Handles btnSyncUserGruppen.Click
        'Liste der Tabellen-Überschriften
        sColNames.Clear()
        sColNames.AddRange({"", "Nr", "&WinBack-Benutzer-Gruppe", "", "Nr", "&OrgaBack-Benutzer-Gruppe", "", "Status"})

        Text = "Synchronisation Datenbank - Benutzergruppen"
        SyncType = wb_Global.SyncType.BenutzerGruppen
        pDialog.ListFileName = "SyncReport_MitarbeiterGrupppen.lst"
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
        DisplayResultSummary(wbUserGrp, osUserGrp)
        ShowResult()
        'Cursor wieder umschalten
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnSyncUser_Click(sender As Object, e As EventArgs) Handles btnSyncUser.Click
        'Liste der Tabellen-Überschriften
        sColNames.Clear()
        sColNames.AddRange({"", "Login", "&WinBack-Benutzer", "Grp", "Kassierer-Nr", "&OrgaBack-Benutzer", "Grp", "Status"})

        Text = "Synchronisation Datenbank - Mitarbeiter"
        SyncType = wb_Global.SyncType.Benutzer
        pDialog.ListFileName = "SyncReport_Mitarbeiter.lst"
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
        DisplayResultSummary(wbUser, osUser)
        ShowResult()
        'Cursor wieder umschalten
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnSyncArtikel_Click(sender As Object, e As EventArgs) Handles btnSyncArtikel.Click
        'Liste der Tabellen-Überschriften
        sColNames.Clear()
        sColNames.AddRange({"", "Nr", "&WinBack-Artikel", "", "Nr", "&OrgaBack-Artikel", "", "Status"})
        Text = "Synchronisation Datenbank - Artikel"
        SyncType = wb_Global.SyncType.Artikel
        pDialog.ListFileName = "SyncReport_Artikel.lst"
        HideResult()

        'Cursor umschalten
        Me.Cursor = Cursors.WaitCursor
        'Artikel aus WinBack in Array einlesen
        wbArtikel.DBRead()
        'doppelte Einträge (Bezeichnung) in WinBack ermitteln markieren
        wbArtikel.CheckDbl()
        'Artikel aus OrgaSoft in Array einlesen
        osArtikel.DBRead()

        'Daten/Synchronisation prüfen und Ergebnis berechnen
        wbArtikel.Case_10 = wb_Global.SyncState.OrgaBackMiss   'falls ein Eintrag in OrgaBack fehlt, wird der Konflikt nicht repariert !
        wbArtikel.CheckSync(osArtikel.Data)

        'Ergebnis im Grid anzeigen
        DisplayResultGrid(wbArtikel.Data, sColNames)
        DisplayResultSummary(wbArtikel, osArtikel)
        ShowResult()
        'Cursor wieder umschalten
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnSyncRohstoffe_Click(sender As Object, e As EventArgs) Handles btnSyncRohstoffe.Click
        'Liste der Tabellen-Überschriften
        sColNames.Clear()
        sColNames.AddRange({"", "Nr", "&WinBack-Rohstoff", "", "Nr", "&OrgaBack-Rohstoff", "", "Status"})
        Text = "Synchronisation Datenbank - Rohstoffe"
        SyncType = wb_Global.SyncType.Rohstoffe
        pDialog.ListFileName = "SyncReport_Rohstoffe.lst"
        HideResult()

        'Cursor umschalten
        Me.Cursor = Cursors.WaitCursor
        'Rohstoffe aus WinBack in Array einlesen
        wbRohstoffe.DBRead()
        'Rohstoffe aus OrgaSoft in Array einlesen
        osRohstoffe.DBRead()

        'Daten/Synchronisation prüfen und Ergebnis berechnen
        wbRohstoffe.Case_10 = wb_Global.SyncState.OrgaBackMiss   'falls ein Eintrag in OrgaBack fehlt, wird der Konflikt nicht repariert !
        wbRohstoffe.CheckSync(osRohstoffe.Data)

        'Ergebnis im Grid anzeigen
        DisplayResultGrid(wbRohstoffe.Data, sColNames)
        DisplayResultSummary(wbRohstoffe, osRohstoffe)
        ShowResult()
        'Cursor wieder umschalten
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnSyncStart_Click(sender As Object, e As EventArgs) Handles btnSyncStart.Click
        'Cursor umschalten
        Me.Cursor = Cursors.WaitCursor

        Select Case SyncType
            Case wb_Global.SyncType.BenutzerGruppen
                ExcecuteSync(wbUserGrp, osUserGrp)
            Case wb_Global.SyncType.Benutzer
                ExcecuteSync(wbUser, osUser)
            Case wb_Global.SyncType.Artikel
                ExcecuteSync(wbArtikel, Nothing)
            Case wb_Global.SyncType.Rohstoffe
                ExcecuteSync(wbRohstoffe, Nothing)
        End Select

        'Cursor wieder umschalten
        Me.Cursor = Cursors.Default
        MsgBox("Synchronisation erfolgreich abgeschlossen", MsgBoxStyle.Information, "Synchroniation")
    End Sub

    Private Sub ExcecuteSync(ByRef wb As wb_Sync, ByRef os As wb_Sync)
        'Synchronisation in WinBack-DB
        For Each x As wb_SyncItem In wb.Data
            Select Case x.SyncOK
                Case wb_Global.SyncState.WinBackWrite
                    wb.DBInsert(x.Os_Nummer, x.Os_Bezeichnung, x.Os_Gruppe)
                Case wb_Global.SyncState.WinBackUpdate
                    wb.DBUpdate(x.Os_Nummer, x.Os_Bezeichnung, x.Os_Gruppe)
                Case wb_Global.SyncState.TryMatchWinBackUpdate
                    wb.DBNumber(x.Wb_Nummer, x.Os_Nummer, x.Os_Gruppe)
                Case wb_Global.SyncState.TryMatchDel
                    wb.DBDelete(x.Wb_Index)
            End Select
        Next

        If Not IsNothing(os) Then
            'Synchronisation in OrgaBack-DB
            For Each x As wb_SyncItem In os.Data
                Select Case x.SyncOK
                    Case wb_Global.SyncState.OrgaBackWrite
                        os.DBInsert(x.Wb_Nummer, x.Wb_Bezeichnung, x.Wb_Gruppe)
                    Case wb_Global.SyncState.OrgaBackUpdate
                        os.DBUpdate(x.Wb_Nummer, x.Wb_Bezeichnung, x.Wb_Gruppe)
                    Case wb_Global.SyncState.TryMatchWinBackUpdate
                        wb.DBNumber(x.Wb_Nummer, x.Os_Nummer, x.Os_Gruppe)
                End Select
            Next
        End If
    End Sub

    Private Sub btnRemoveDBL_Click(sender As Object, e As EventArgs) Handles btnRemoveDBL.Click
        'Cursor umschalten
        Me.Cursor = Cursors.WaitCursor

        Select Case SyncType
            Case wb_Global.SyncType.Artikel
                'Doppelte Einträge nur in WinBack möglich !
                ExecuteDelDBL(wbArtikel)
                'Ergebnis im Grid anzeigen
                DisplayResultGrid(wbArtikel.Data, sColNames)
                DisplayResultSummary(wbArtikel, osArtikel)
        End Select

        'Cursor wieder umschalten
        Me.Cursor = Cursors.Default
        MsgBox("Löschen der doppelten Einträge erfolgreich abgeschlossen", MsgBoxStyle.Information, "Löschen")
    End Sub

    Private Sub ExecuteDelDBL(ByRef wb As wb_Sync)
        Dim idx As Integer = 1
        For Each x As wb_SyncItem In wb.Data
            idx += 1
            If x.SyncOK = wb_Global.SyncState.DBL Then
                For i = idx To wb.Data.Count - 1
                    If wb.Data(i).Wb_Bezeichnung = x.Wb_Bezeichnung And wb.Data(i).SyncOK = wb_Global.SyncState.OK Then
                        'Datensatz löschen
                        x.SyncOK = wb_Global.SyncState.TryMatchDel
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub btnTryToMatch_Click(sender As Object, e As EventArgs) Handles btnTryToMatch.Click
        Select Case SyncType
            Case wb_Global.SyncType.BenutzerGruppen
                'Matching durchführen
                FindMatches(wbUserGrp)
                'Ergebnis im Grid anzeigen
                DisplayResultGrid(wbUserGrp.Data, sColNames)
                DisplayResultSummary(wbUserGrp, osUserGrp)
            Case wb_Global.SyncType.Benutzer
                'Matching durchführen
                FindMatches(wbUser)
                'Ergebnis im Grid anzeigen
                DisplayResultGrid(wbUser.Data, sColNames)
                DisplayResultSummary(wbUser, osUser)
            Case wb_Global.SyncType.Artikel
                'Matching durchführen
                FindMatches(wbArtikel)
                'Ergebnis im Grid anzeigen
                DisplayResultGrid(wbArtikel.Data, sColNames)
                DisplayResultSummary(wbArtikel, osArtikel)
            Case wb_Global.SyncType.Rohstoffe
                'Matching durchführen
                FindMatches(wbRohstoffe)
                'Ergebnis im Grid anzeigen
                DisplayResultGrid(wbRohstoffe.Data, sColNames)
                DisplayResultSummary(wbRohstoffe, osRohstoffe)
        End Select
    End Sub

    ''' <summary>
    ''' Prüft für alle Einträge im Grid, ob sich die Konflikte lösen lassen:
    '''     -   SyncState.WinbackWrite
    '''     -   SyncState.WinBackMiss
    '''     -   SyncState.OrgaBackWrite
    '''     -   SyncState.OrgaBackMiss
    '''     
    ''' Die Prüfung erfolgt über die Bezeichnungs-Spalte. Die Nummern sind schon über Excute Sync geprüft worden)
    ''' </summary>
    ''' <param name="wb"></param>
    Private Sub FindMatches(ByRef wb As wb_Sync)
        'Statistik Reset    
        CalculateResultSummary(wb_Global.SyncState.NOK)

        'Match durchführen
        For Each w As wb_SyncItem In wb.Data
            Select Case w.SyncOK
                Case wb_Global.SyncState.WinBackMiss, wb_Global.SyncState.WinBackWrite
                    For Each o As wb_SyncItem In wb.Data
                        If w.Os_Bezeichnung = o.Wb_Bezeichnung And o.SyncOK <> wb_Global.SyncState.OK Then
                            w.Merge(o)
                            CalculateResultSummary(w.SyncOK)
                            'Artikel/Rohstoff-Nummer nur in WinBack anpassen
                            w.SyncOK = wb_Global.SyncState.TryMatchWinBackUpdate
                            o.SyncOK = wb_Global.SyncState.TryMatchDel
                            Exit For
                        End If
                    Next
                Case wb_Global.SyncState.OrgaBackMiss, wb_Global.SyncState.OrgaBackWrite
                    For Each o As wb_SyncItem In wb.Data
                        If w.Wb_Bezeichnung = o.Os_Bezeichnung And o.SyncOK <> wb_Global.SyncState.OK Then
                            w.Merge(o)
                            CalculateResultSummary(w.SyncOK)
                            'Artikel/Rohstoff-Nummer nur in WinBack anpassen
                            w.SyncOK = wb_Global.SyncState.TryMatchWinBackUpdate
                            o.SyncOK = wb_Global.SyncState.TryMatchDel
                            Exit For
                        End If
                    Next
            End Select
        Next

        'Nach TryMatch - Meldung ausgeben
        cntSync_SumMatch = cntSync_wbMissMatch + cntSync_wbWriteMatch + cntSync_osMissMatch + cntSync_osWriteMatch
        If cntSync_SumMatch > 0 Then
            MsgBox("Matching erfolgreich abgeschlossen" & vbCr & cntSync_SumMatch & " Datensätze gefunden", MsgBoxStyle.Information, "Synchroniation Matching")
        Else
            MsgBox("Matching hat keine Datensätze gefunden", MsgBoxStyle.Information, "Synchroniation Matching")
        End If

        'gekennzeichnete Datensätze nach Matching löschen
        Dim iMax As Integer = wb.Data.Count
        Dim i As Integer = 0
        While i < iMax
            If wb.Data(i).SyncOK = wb_Global.SyncState.TryMatchDel Then
                wb.Data.RemoveAt(i)
                iMax -= 1
            Else
                i += 1
            End If
        End While
    End Sub

    Private Sub DisplayResultGrid(ByVal xArray As ArrayList, Optional sColNames As List(Of String) = Nothing)
        Dim ColNames As List(Of String) = sColNames

        'Wenn alte Daten vorhanden sind vorher löschen
        If SyncResultGrid IsNot Nothing Then
            If ColNames Is Nothing Then
                ColNames = SyncResultGrid.ColNames
            End If
            SyncResultGrid.Dispose()
            SyncResultGrid = Nothing
        End If

        'Daten im Grid anzeigen
        SyncResultGrid = New wb_SyncGridView(xArray, sColNames)
        SyncResultGrid.ScrollBars = ScrollBars.Vertical
        SyncResultGrid.BackgroundColor = Me.BackColor
        SyncResultGrid.GridLocation(tbSyncResult)
        SyncResultGrid.PerformLayout()
        SyncResultGrid.Refresh()
    End Sub

    Private Sub CalculateResultSummary(State As wb_Global.SyncState)
        Select Case State
            Case wb_Global.SyncState.WinBackMiss
                cntSync_wbMissMatch += 1
            Case wb_Global.SyncState.WinBackWrite
                cntSync_wbWriteMatch += 1
            Case wb_Global.SyncState.OrgaBackMiss
                cntSync_osMissMatch += 1
            Case wb_Global.SyncState.OrgaBackWrite
                cntSync_osWriteMatch += 1
            Case Else
                cntSync_wbMissMatch = 0
                cntSync_wbWriteMatch = 0
                cntSync_osMissMatch = 0
                cntSync_osWriteMatch = 0
        End Select
    End Sub

    Private Sub DisplayResultSummary(ByRef wb As wb_Sync, ByRef os As wb_Sync)
        'Zusammenfassung (Summe der Datensätze) anzeigen
        lvSyncResult.Items.Clear()
        'Zeilen anlegen
        lvSyncResult.Items.Add("OrgaBack")
        lvSyncResult.Items.Add("WinBack")
        'Spalten ausfüllen
        lvSyncResult.Items(0).SubItems.Add(os.CntSyncAll)
        lvSyncResult.Items(0).SubItems.Add(wb.CntSync_osErr + os.CntSync_osErr)
        lvSyncResult.Items(0).SubItems.Add(wb.CntSync_osWrite + os.CntSync_osWrite - cntSync_osWriteMatch)
        lvSyncResult.Items(0).SubItems.Add(wb.CntSync_osUpdate + os.CntSync_osUpdate)
        lvSyncResult.Items(0).SubItems.Add(wb.CntSync_osMiss + os.CntSync_osMiss - cntSync_osMissMatch)
        lvSyncResult.Items(1).SubItems.Add(wb.CntSyncAll)
        lvSyncResult.Items(1).SubItems.Add(wb.CntSync_wbErr + os.CntSync_wbErr)
        lvSyncResult.Items(1).SubItems.Add(wb.CntSync_wbWrite + os.CntSync_wbWrite - cntSync_wbWriteMatch)
        lvSyncResult.Items(1).SubItems.Add(wb.CntSync_wbUpdate + os.CntSync_wbUpdate)
        lvSyncResult.Items(1).SubItems.Add(wb.CntSync_wbMiss + os.CntSync_wbMiss - cntSync_wbMissMatch)
        lvSyncResult.Items(1).SubItems.Add(wb.CntSyncDBL + os.CntSyncDBL)
    End Sub

    Private Sub HideResult()
        btnExportPrint.Enabled = False
        btnSyncStart.Enabled = False
        btnTryToMatch.Enabled = False
        lvSyncResult.Items.Clear()
        lvSyncResult.Enabled = False
    End Sub

    Private Sub ShowResult()
        btnExportPrint.Enabled = True
        btnSyncStart.Enabled = True
        btnTryToMatch.Enabled = True
        lvSyncResult.Enabled = True
    End Sub

    Private Sub btnExportPrint_Click(sender As Object, e As EventArgs) Handles btnExportPrint.Click
        'Erstelle eine generische Liste aus dem Result-Grid
        Dim SyncList As New List(Of wb_SyncItem)
        For Each SyncResultItem As wb_SyncItem In SyncResultGrid.GridArray
            SyncList.Add(SyncResultItem)
        Next

        'An die generische Liste binden
        pDialog.LL.SetDataBinding(SyncList, String.Empty)
        pDialog.ShowDialog()
    End Sub

End Class