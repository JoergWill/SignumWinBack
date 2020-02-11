Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_StammDaten_LinienGruppen
    Inherits DockContent

    Const ColLGNr = 0  'Spalte Linien-Gruppennummer
    Const ColxLinien = 3  'Spalte Linien
    Const ColStartZt = 4  'Spalte Startzeit Aufarbeitungs-Platz
    Const ColDruckBZ = 7  'Flag Backzettel für diese Linie drucken
    Const ColDruckTZ = 8  'Flag Teigzettel für diese Linie drucken
    Const ColDruckTR = 9  'Flag Backzettel für diese Linie drucken
    Const ColSendeBZ = 10 'Flag Backzettel für diese Linie an WinBack übertragen
    Const ColSendeTZ = 11 'Flag Teigzettel für diese Linie an WinBack übertragen

    Private _RowIndex As Integer = wb_Global.UNDEFINED
    Private _OrgWert As String = ""
    Private _NewWert As String = ""

    Private Sub wb_Linien_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten, die mit + beginnen sind editierbar
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"+Nr", "+&Bezeichnung", "+Kurzname", "+Linien", "+Startzeit", "", "", "BZ Drucken", "TZ Drucken", "TR Drucken", "BZ Senden", "TZ Senden"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "LG_Bezeichnung"

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlLinienGruppen, "Liniengruppen")
        'DataGrid Initialisierung Anzeige ohne Sauerteig, nur aktive Rohstoffe
        '        Me.Anzeige = AnzeigeFilter.Alle

        'Flags Drucken/Senden zentriert darstellen
        DataGridView.Columns(ColLGNr).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView.Columns(ColLGNr).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        If DataGridView.ColumnCount >= ColSendeTZ Then
            DataGridView.Columns(ColDruckBZ).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView.Columns(ColDruckTZ).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView.Columns(ColDruckTR).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView.Columns(ColSendeBZ).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView.Columns(ColSendeTZ).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Header Drucken/Senden zentriert darstellen
            DataGridView.Columns(ColDruckBZ).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView.Columns(ColDruckTZ).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView.Columns(ColDruckTR).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView.Columns(ColSendeBZ).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView.Columns(ColSendeTZ).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Default Spaltenbreite für Drucken/Senden (Überschrift in zwei Zeilen)
            DataGridView.Columns(ColDruckBZ).Width = 50
            DataGridView.Columns(ColDruckTZ).Width = 50
            DataGridView.Columns(ColDruckTR).Width = 50
            DataGridView.Columns(ColSendeBZ).Width = 50
            DataGridView.Columns(ColSendeTZ).Width = 50
        End If

        'Multi-Select zulassen (Löschen)
        DataGridView.MultiSelect = True

    End Sub

    Private Sub DataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellEndEdit
        Debug.Print("End Edit " & e.ColumnIndex & "/" & e.RowIndex)

        'neuer Wert der Zelle
        _NewWert = DataGridView.CurrentCell.Value.ToString
        'Linien-Gruppen-Nummer wurde geändert
        If (e.ColumnIndex = ColLGNr) And (_OrgWert <> _NewWert) Then

            'prüfen ob der neue Eintrag gültig ist (darf nicht schon belegt sein)
            Dim LinienGruppe As Integer = wb_Functions.StrToInt(_NewWert)
            If (LinienGruppe > 0) And (LinienGruppe < 255) Then

                If wb_Linien_Global.ExistLinienGruppe(LinienGruppe) Then
                    MsgBox("Diese Liniengruppe existiert schon !", MsgBoxStyle.Exclamation, "Liniengruppe ändern")
                    DataGridView.CurrentCell.Value = _OrgWert
                Else
                    'Liniengruppe oder Backort/Aufarbeitungs-Platz
                    If LinienGruppe >= wb_Global.OffsetBackorte Then

                        'bei Aufarbeitungsplätzen ist die Linien-Gruppen-Nummer gleich der Linie
                        DataGridView.CurrentRow.Cells(ColxLinien).Value = LinienGruppe.ToString
                        'Aufarbeitungsplätze anpassen - winback.RohParams
                        If Not wb_Linien_Global.ChangeBackort(_OrgWert, _NewWert) Then
                            MsgBox(wb_Linien_Global.ErrorText, MsgBoxStyle.Exclamation, "Liniengruppe ändern")
                        End If
                    Else
                        'Rezepturen anpassen (Liniengruppe ändern) winback.Rezepte
                        If Not wb_Linien_Global.ChangeLinienGruppe(_OrgWert, _NewWert) Then
                            MsgBox(wb_Linien_Global.ErrorText, MsgBoxStyle.Exclamation, "Liniengruppe ändern")
                        End If
                    End If
                End If

                'Liste neu aufbauen

            Else
                MsgBox("Liniengruppe ungültig !", MsgBoxStyle.Exclamation, "Liniengruppe ändern")
                DataGridView.CurrentCell.Value = _OrgWert
            End If
        End If
    End Sub

    Private Sub DataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        Debug.Print("CellContent Click " & e.ColumnIndex & "/" & e.RowIndex)
        'Originalwert der Zelle merken (Edit-Modus)
        _OrgWert = DataGridView.CurrentCell.Value.ToString
    End Sub

    ''' <summary>
    ''' Flag setzen/rücksetzen.
    ''' Wenn die Zeile schon markiert war, wird das Flag getoggelt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellClick, DataGridView.CellContentClick
        Debug.Print("Cell Click " & e.ColumnIndex & "/" & e.RowIndex)
        'wenn die Zeile schon markiert war
        If (e.RowIndex = _RowIndex) And (e.ColumnIndex >= ColDruckBZ) Then
            If DataGridView.CurrentCell.FormattedValue = "X" Then
                DataGridView.CurrentCell.Value = ""
            Else
                DataGridView.CurrentCell.Value = "X"
            End If
        End If
        _RowIndex = e.RowIndex
    End Sub

    Private Sub wb_StammDaten_LinienGruppen_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Layout sichern
        DataGridView.SaveToDisk("Liniengruppen")
    End Sub

    Private Sub BtnLinienGruppeNeu_Click(sender As Object, e As EventArgs) Handles BtnLinienGruppeNeu.Click
        'Neuen Datensatz anfügen
        wb_Linien_Global.AddLinienGruppe()
        'neuen Datensatz anzeigen
        DataGridView.RefreshData()
        'Spalte 1 darf/muss editiert werden
        DataGridView.Columns(ColLGNr).ReadOnly = False
        'Fokus auf Linengruppen-Nummer/Backort-Nummer
        'DataGridView.CurrentCell = DataGridView.Rows(DataGridView.Rows.Count).Cells(ColLGNr)
        'DataGridView.EditMode = True
    End Sub

    Private Sub BtnNeueAufarbeitung_Click(sender As Object, e As EventArgs) Handles BtnNeueAufarbeitung.Click
        'Neuen Datensatz anfügen
        If wb_Linien_Global.AddLinienGruppe(True) Then
            'neuen Datensatz anzeigen
            DataGridView.RefreshData()
        Else
            'Fehlermeldung ausgeben
            MsgBox(wb_Linien_Global.ErrorText, MsgBoxStyle.Exclamation, "Liniengruppe neu anlegen")
        End If
    End Sub

    Private Sub BtnLoeschen_Click(sender As Object, e As EventArgs) Handles BtnLoeschen.Click
        'wenn mehr als ein Datensatz markiert ist
        If DataGridView.SelectedCells.Count > 1 Then
            'Zähler OK/Err
            Dim DelOK As Integer = 0
            Dim DelErr As Integer = 0

            'Multiselect
            For Each DataGridRow As DataGridViewRow In DataGridView.SelectedRows
                If wb_Linien_Global.DeleteLinienGruppe(DataGridRow.Cells(ColLGNr).Value.ToString) Then
                    DelOK += 1
                Else
                    DelErr += 1
                End If
            Next
            'Anzeige aktualisieren
            DataGridView.RefreshData()

            'Meldung ausgeben
            MsgBox("Insgesamt " & (DelOK + DelErr).ToString & " Zeilen markiert" & vbCrLf & "davon " & DelOK & " gelöscht" & vbCrLf & "davon " & DelErr & " fehlerhaft", MsgBoxStyle.Exclamation, "Liniengruppe löschen")
        Else
            'Aktuellen Datensatz löschen
            Dim LinienGruppenNummer As Integer = DataGridView.iField("LG_Nr")
            If wb_Linien_Global.DeleteLinienGruppe(LinienGruppenNummer) Then
                'neuen Datensatz anzeigen
                DataGridView.RefreshData()
            Else
                'Fehlermeldung ausgeben
                MsgBox(wb_Linien_Global.ErrorText, MsgBoxStyle.Exclamation, "Liniengruppe löschen")
            End If
        End If

    End Sub

    Private Sub DataGridView_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView.CellFormatting
        If e.ColumnIndex = ColStartZt Then
            If e.Value IsNot Nothing And e.Value.ToString <> "" Then
                e.Value = wb_Functions.FormatTimeStr(e.Value)
            End If
        End If

    End Sub

    ''' <summary>
    ''' Syncronisiert die Einträge aus OrgaBack(ArtikelZusatzgruppen für MFF200-Aufarbeitungsplatz) mit den Einträgen in WinBack(Liniengruppen)
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnSync_Click(sender As Object, e As EventArgs) Handles BtnSync.Click
        Dim obAufarbeitung As New wb_SyncAufarbeitung_OrgaBack
        Dim wbAufarbeitung As New wb_SyncAufarbeitung_WinBack

        'Daten OrgaBack(Artikelzusatzfelder) einlesen
        obAufarbeitung.DBRead()
        'Daten WinBack(Liniengruppe) einlesen
        wbAufarbeitung.DBRead()

        'Daten/Synchronisation prüfen und Ergebnis berechnen
        wbAufarbeitung.Case_01 = wb_Global.SyncState.OrgaBackErr
        wbAufarbeitung.Case_10 = wb_Global.SyncState.OrgaBackWrite   'falls ein Eintrag in OrgaBack fehlt, wird der Eintrag eingefügt
        wbAufarbeitung.Case_11 = wb_Global.SyncState.OrgaBackUpdate  'WinBack ist das führende System - Bezeichnung wird in OrgaBack übernommen
        wbAufarbeitung.CheckSync(obAufarbeitung.Data)

        'Synchronisation in OrgaBack-DB
        For Each x As wb_SyncItem In wbAufarbeitung.Data
            Select Case x.SyncOK
                Case wb_Global.SyncState.OrgaBackWrite
                    obAufarbeitung.DBInsert(x.Wb_Nummer, x.Wb_Bezeichnung, x.Wb_Gruppe)
                Case wb_Global.SyncState.OrgaBackUpdate
                    obAufarbeitung.DBUpdate(x.Wb_Nummer, x.Wb_Bezeichnung, x.Os_Gruppe)
            End Select
        Next

        Debug.Print("Sync Aufarbeitungsplätze beendet ")

    End Sub
End Class


