Public Class wb_Sync
    Shared WinBack_UserGruppe As New ArrayList
    Shared OrgaBack_UserGruppe As New ArrayList

    Shared Function Sync_UserGruppen(SyncErgebnis As Windows.Forms.ListBox, CheckOnly As Boolean) As Boolean

        If CheckOnly Then
            'Einesen der User-Gruppen aus WinBack.ItemIDs
            Dim Gruppe As wb_Global.wb_Gruppe
            WinBack_UserGruppe.Clear()
            OrgaBack_UserGruppe.Clear()

            Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
            If winback.sqlSelect(wb_Sql_Selects.sqlUserGrpTxt) Then
                While winback.Read
                    Gruppe.Nummer = winback.iField("II_ItemID")
                    Gruppe.Bezeichnung = wb_Functions.TextFilter(winback.sField("II_Kommentar"))
                    Gruppe.SyncOK = wb_Global.SyncState.NOK
                    Trace.WriteLine("Read WinBack User-Gruppe Nummer         " & Gruppe.Nummer.ToString + " User-Gruppe Bezeichnung        " & Gruppe.Bezeichnung)
                    WinBack_UserGruppe.Add(Gruppe)
                End While
            End If
            winback.Close()

            'Einlesen der WinBack-Gruppen aus OrgaBackMain.dbo.MitarbeiterMultiFunktionsFeld
            Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
            If orgaback.sqlSelect(wb_Sql_Selects.mssqlMitarbeiterGruppen) Then
                While orgaback.Read
                    Gruppe.Nummer = orgaback.iField("Hierarchie")
                    Gruppe.Bezeichnung = orgaback.sField("Bezeichnung")
                    Gruppe.SyncOK = wb_Global.SyncState.NOK
                    Trace.WriteLine("Read OrgaBack Mitarbeiter-Gruppe Nummer " & Gruppe.Nummer.ToString + " Mitarbeiter-Gruppe Bezeichnung " & Gruppe.Bezeichnung)
                    OrgaBack_UserGruppe.Add(Gruppe)
                End While
            End If
            orgaback.Close()

            'Daten vergleichen
            Dim o, w As Integer
            Dim OrgaBackGruppe, WinBackGruppe As wb_Global.wb_Gruppe

            'suche alle Datensätze bei denen die Gruppen-Nummer identisch ist
            For w = 0 To WinBack_UserGruppe.Count - 1
                WinBackGruppe = WinBack_UserGruppe.Item(w)

                For o = 0 To OrgaBack_UserGruppe.Count - 1
                    OrgaBackGruppe = OrgaBack_UserGruppe.Item(o)

                    ' Gruppen-Nummer gefunden
                    If (WinBackGruppe.Nummer = OrgaBackGruppe.Nummer) Then
                        'Datensatz in WinBack ist OK
                        WinBackGruppe.SyncOK = wb_Global.SyncState.OK
                        'Bezeichnung identisch
                        If WinBackGruppe.Bezeichnung = OrgaBackGruppe.Bezeichnung Then
                            'Datensatz in OrgaBack ist OK
                            OrgaBackGruppe.SyncOK = wb_Global.SyncState.OK
                        Else
                            'Bezeichnung in OrgaBack updaten (WinBack ist das führende System)
                            WinBackGruppe.SyncOK = wb_Global.SyncState.OrgaBackUpdate
                            OrgaBackGruppe.SyncOK = wb_Global.SyncState.OK
                        End If

                        ' Gruppen-Nummer gefunden - Exit innere Schleife
                        OrgaBack_UserGruppe(o) = OrgaBackGruppe
                        WinBack_UserGruppe.Item(w) = WinBackGruppe
                        Exit For
                    End If
                Next
            Next

            'alle Datensätze in OrgaBack, die nicht syncronisiert sind, müssen in WinBack eingefügt werden.
            For o = 0 To OrgaBack_UserGruppe.Count - 1
                OrgaBackGruppe = OrgaBack_UserGruppe.Item(o)
                If OrgaBackGruppe.SyncOK = wb_Global.SyncState.NOK Then
                    OrgaBackGruppe.SyncOK = wb_Global.SyncState.WinBackWrite
                    OrgaBack_UserGruppe(o) = OrgaBackGruppe
                End If
            Next

            'alle Datensätze in WinBack, die nicht syncronisiert sind, müssen in OrgaBack eingefügt werden.
            For o = 0 To WinBack_UserGruppe.Count - 1
                WinBackGruppe = WinBack_UserGruppe.Item(o)
                If WinBackGruppe.SyncOK = wb_Global.SyncState.NOK Then
                    WinBackGruppe.SyncOK = wb_Global.SyncState.OrgaBackWrite
                    WinBack_UserGruppe(o) = WinBackGruppe
                End If
            Next


            SyncErgebnis.Items.Add("Synchronisation Mitarbeiter-Gruppen")
            SyncErgebnis.Items.Add("===================================")

            For Each Gruppe In WinBack_UserGruppe
                If Gruppe.SyncOK <> wb_Global.SyncState.OK Then
                    SyncErgebnis.Items.Add("WinBack User-Gruppe " & Chr(9) & Gruppe.Nummer.ToString & " '" & Gruppe.Bezeichnung & "' " & ShowResult(Gruppe.SyncOK))
                End If
            Next
            For Each Gruppe In OrgaBack_UserGruppe
                If Gruppe.SyncOK <> wb_Global.SyncState.OK Then
                    SyncErgebnis.Items.Add("OrgaBack Mitarbeiter-Gruppe " & Chr(9) & Gruppe.Nummer.ToString & " '" & Gruppe.Bezeichnung & "' " & ShowResult(Gruppe.SyncOK))
                End If
            Next

        Else

            'Sync ausführen
            For Each Gruppe In WinBack_UserGruppe
                If Gruppe.SyncOK = wb_Global.SyncState.OrgaBackUpdate Then
                    OrgaBackUpdate(Gruppe.Nummer, Gruppe.Bezeichnung)
                End If
                If Gruppe.SyncOK = wb_Global.SyncState.OrgaBackWrite Then
                    OrgaBackInsert(Gruppe.Nummer, Gruppe.Bezeichnung)
                End If
            Next

            For Each Gruppe In OrgaBack_UserGruppe
                If Gruppe.SyncOK = wb_Global.SyncState.OrgaBackUpdate Then
                    OrgaBackUpdate(Gruppe.Nummer, Gruppe.Bezeichnung)
                End If
                If Gruppe.SyncOK = wb_Global.SyncState.OrgaBackWrite Then
                    OrgaBackInsert(Gruppe.Nummer, Gruppe.Bezeichnung)
                End If
            Next
        End If

        Return True
    End Function

    Shared Function OrgaBackUpdate(GrpNr As Integer, GrpBezeichnung As String) As Boolean
        'Gruppen-Nummer formatieren
        Dim sGrpNr As String
        sGrpNr = Strings.Right("0000" + GrpNr.ToString, 4)

        'Verbindung zu MsSQL-Datenbank
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Update OrgaBackMain.dbo.MitarbeiterMultiFunktionsFeld
        OrgaBackUpdate = orgaback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlUpdateMitarbeiterGruppen, sGrpNr, GrpBezeichnung))
        'Datenbank-Verbindung wieder schliessen
        orgaback.Close()
    End Function

    Shared Function OrgaBackInsert(GrpNr As Integer, GrpBezeichnung As String) As Boolean
        'Gruppen-Nummer formatieren
        Dim sGrpNr As String
        sGrpNr = Strings.Right("0000" + GrpNr.ToString, 4)

        'Verbindung zu MsSQL-Datenbank
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Update OrgaBackMain.dbo.MitarbeiterMultiFunktionsFeld
        OrgaBackInsert = orgaback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlInsertMitarbeiterGruppen, sGrpNr, GrpBezeichnung, 1))
        'Datenbank-Verbindung wieder schliessen
        orgaback.Close()
    End Function

    Shared Function ShowResult(SyncState As wb_Global.SyncState) As String
        Select Case SyncState
            Case wb_Global.SyncState.NOK
                Return "nicht sychronisiert"
            Case wb_Global.SyncState.OrgaBackUpdate
                Return "wird in OrgaBack geändert"
            Case wb_Global.SyncState.OrgaBackWrite
                Return "wird in OrgaBack eingefügt"
            Case Else
                Return "keine Änderung"
        End Select
    End Function


End Class

