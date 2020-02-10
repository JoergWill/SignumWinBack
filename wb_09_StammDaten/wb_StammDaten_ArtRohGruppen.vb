Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_StammDaten_ArtRohGruppen
    Inherits DockContent

    Const ColNmr = 0    'Spalte Artikel/Rohstoff Gruppennummer
    Const ColBez = 1    'Spalte Bezeichnung
    Const ColDek = 4    'Flag Deklarieren
    Const ColArt = 5    'Flag Artikelgruppe

    Private _RowIndex As Integer = wb_Global.UNDEFINED
    Private _OrgWert As String = ""
    Private _NewWert As String = ""

    Private Sub wb_Gruppen_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten, die mit + beginnen sind editierbar
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "+&Bezeichnung", "", "", "Dekl.", "Artikel"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "IP_Wert4str"

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlArtRohGruppen, "ArtRohgruppen")
        'DataGrid Initialisierung Anzeige ohne Sauerteig, nur aktive Rohstoffe
        '        Me.Anzeige = AnzeigeFilter.Alle

        'Flags Deklarieren/Artikel zentriert darstellen
        DataGridView.Columns(ColDek).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView.Columns(ColDek).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DataGridView.Columns(ColArt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView.Columns(ColArt).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        'Default Spaltenbreite für Deklaration und Artikel
        DataGridView.Columns(ColDek).Width = 50
        DataGridView.Columns(ColArt).Width = 50

        'Multi-Select zulassen (Löschen)
        DataGridView.MultiSelect = True

    End Sub

    Private Sub DataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellEndEdit
        Debug.Print("End Edit " & e.ColumnIndex & "/" & e.RowIndex)

    End Sub

    Private Sub DataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellContentClick
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
    Private Sub DataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellClick
        Debug.Print("Cell Click " & e.ColumnIndex & "/" & e.RowIndex)
        'wenn die Zeile schon markiert war
        If (e.RowIndex = _RowIndex) And (e.ColumnIndex >= ColDek) Then
            If DataGridView.CurrentCell.FormattedValue = "X" Then
                DataGridView.CurrentCell.Value = "0"
            Else
                DataGridView.CurrentCell.Value = "1"
            End If
        End If
        _RowIndex = e.RowIndex
    End Sub

    Private Sub wb_StammDaten_ArtRohGruppen_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Layout sichern
        DataGridView.SaveToDisk("ArtRohgruppen")
    End Sub

    Private Sub BtnRohstoffGruppeNeu_Click(sender As Object, e As EventArgs) Handles BtnRohstoffGruppeNeu.Click
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'neuen Datensatz anlegen
        Dim RohGrpNr As Integer = wb_Rohstoffe_Shared.Add_RohstoffGruppe()
        'neuen Datensatz anzeigen
        DataGridView.RefreshData()

        'auf den neuen Datensatz positionieren
        DataGridView.ClearSelection()
        For i = 0 To DataGridView.RowCount - 1
            If DataGridView.Rows(i).Cells(ColNmr).Value = RohGrpNr Then
                DataGridView.Rows(i).Selected = True
            End If
        Next
    End Sub

    Private Sub BtnArtikelGruppeNeu_Click(sender As Object, e As EventArgs) Handles BtnArtikelGruppeNeu.Click
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'neuen Datensatz anlegen
        Dim ArtGrpNr As Integer = wb_Artikel_Shared.Add_ArtikelGruppe()
        'neuen Datensatz anzeigen
        DataGridView.RefreshData()

        'auf den neuen Datensatz positionieren
        DataGridView.ClearSelection()
        For i = 0 To DataGridView.RowCount - 1
            If DataGridView.Rows(i).Cells(ColNmr).Value = ArtGrpNr Then
                DataGridView.Rows(i).Selected = True
            End If
        Next
    End Sub

    Private Sub BtnLoeschen_Click(sender As Object, e As EventArgs) Handles BtnLoeschen.Click
        'wenn mehr als ein Datensatz markiert ist
        If DataGridView.SelectedRows.Count > 1 Then
            'Zähler OK/Err
            Dim DelOK As Integer = 0
            Dim DelErr As Integer = 0

            'Multiselect
            For Each DataGridRow As DataGridViewRow In DataGridView.SelectedRows
                'Artikel- oder Rohstoffgruppe löschen
                If DeleteArtRohGruppe(DataGridRow.Cells) Then
                    DelOK += 1
                Else
                    DelErr += 1
                End If
            Next
            'Anzeige aktualisieren
            DataGridView.RefreshData()

            'Meldung ausgeben
            MsgBox("Insgesamt " & (DelOK + DelErr).ToString & " Zeilen markiert" & vbCrLf & "davon " & DelOK & " gelöscht" & vbCrLf & "davon " & DelErr & " fehlerhaft", MsgBoxStyle.Exclamation, "Artikel-/Rohstoffgruppe löschen")
        Else
            'Aktuellen Datensatz löschen
            If DeleteArtRohGruppe(DataGridView.SelectedRows(0).Cells) Then
                'neuen Datensatz anzeigen
                DataGridView.RefreshData()
            Else
                'Fehlermeldung ausgeben
                MsgBox(wb_Linien_Global.ErrorText, MsgBoxStyle.Exclamation, "Artikel-/Rohstoffgruppe löschen")
            End If
        End If

    End Sub

    Private Function DeleteArtRohGruppe(Cells As DataGridViewCellCollection) As Boolean
        Dim Nr As String = Cells(ColNmr).Value.ToString
        Dim Artikel As Boolean = (Cells(ColArt).Value.ToString = "1")

        'Artikel- oder Rohstoffgruppe
        If Artikel Then
            'Artikelgruppe löschen
            Return wb_Artikel_Shared.Delete_ArtikelGruppe(Nr)
        Else
            'Rohstoffgruppe löschen
            Return wb_Rohstoffe_Shared.Delete_RohstoffGruppe(Nr)
        End If

    End Function

    Private Sub DataGridView_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView.CellFormatting
        If (e.ColumnIndex = ColDek) Or (e.ColumnIndex = ColArt) Then
            Try
                Select Case e.Value
                    Case "0"
                        e.Value = ""
                    Case "1"
                        e.Value = "X"
                    Case Else
                        e.Value = "-"
                End Select
            Catch
            End Try
        End If
    End Sub
End Class


'IP_ItemTyp
'IP_ItemID
'IP_ItemAttr
'IP_Lfd_Nr
'IP_Wert1int
'IP_Wert2int
'IP_Wert3int
'IP_Wert4str
'IP_Wert5str
'IP_Timestamp
