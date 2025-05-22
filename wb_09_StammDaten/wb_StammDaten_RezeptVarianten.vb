Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_StammDaten_RezeptVarianten
    Inherits DockContent

    Const ColNr = 0    'Spalte Nummer
    Const ColBez = 1   'Spalte Bezeichnung

    Private Sub wb_Gruppen_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten, die mit + beginnen sind editierbar
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"Nummer", "+&Bezeichnung"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "RV_Bezeichnung"

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlRezVarianten, "RezeptVarianten")

        'Spalte Nummer zentriert darstellen
        DataGridView.Columns(ColNr).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        'Default Spaltenbreite für Bezeichnung und Wert
        DataGridView.Columns(ColBez).Width = 350
        DataGridView.Columns(ColNr).Width = 150

        'Multi-Select nicht zulassen
        DataGridView.MultiSelect = False

    End Sub

    Private Sub wb_StammDaten_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Layout sichern
        DataGridView.SaveToDisk("RezeptVarianten")
        'Daten aktualisieren
        wb_Rezept_Shared.Reload()
    End Sub

    Private Sub BtnRezVarianteNeu_Click(sender As Object, e As EventArgs) Handles BtnRezVarianteNeu.Click
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'neuen Datensatz anlegen
        Dim RezVarNr As Integer = wb_Rezept_Shared.Add_RezeptVariante()
        'neuen Datensatz anzeigen
        DataGridView.RefreshData()

        'auf den neuen Datensatz positionieren
        DataGridView.ClearSelection()
        For i = 0 To DataGridView.RowCount - 1
            If DataGridView.Rows(i).Cells(ColNr).Value = RezVarNr Then
                DataGridView.Rows(i).Selected = True
            End If
        Next
    End Sub

    Private Sub BtnLoeschen_Click(sender As Object, e As EventArgs) Handles BtnLoeschen.Click
        'Aktuellen Datensatz löschen
        If DataGridView.SelectedRows.Count > 0 Then
            'aktuelle Rezeptvariante Nummer
            Dim Nr As String = DataGridView.SelectedRows(0).Cells(ColNr).Value.ToString
            'Daten in Datenbank sichern
            DataGridView.UpdateDataBase()
            'Rezeptvariante löschen
            If wb_Rezept_Shared.Delete_RezeptVariante(Nr) Then
                'neuen Datensatz anzeigen
                DataGridView.RefreshData()
            Else
                'Fehlermeldung ausgeben
                MsgBox(wb_Rezept_Shared.ErrorText, MsgBoxStyle.Exclamation, "Rezept-Variante löschen")
            End If
        End If
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Close()
    End Sub
End Class


