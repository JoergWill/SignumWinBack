Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_StammDaten_Rezeptgruppen
    Inherits DockContent

    Const ColNr = 0    'Spalte Nummer
    Const ColBez = 1   'Spalte Bezeichnung

    Private Sub wb_Gruppen_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten, die mit + beginnen sind editierbar
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "Nummer", "", "+&Bezeichnung"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "II_Kommentar"

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlRezeptgruppen, "Rezeptgruppen")

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
        DataGridView.SaveToDisk("Rezeptgruppen")
        'Daten aktualisieren
        wb_Rezept_Shared.Reload()
    End Sub

    Private Sub BtnRezeptGruppeNeu_Click(sender As Object, e As EventArgs) Handles BtnRezeptGruppeNeu.Click
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Neuen Datensatz anfügen
        If wb_Rezept_Shared.AddRezeptGruppe() Then
            'neuen Datensatz anzeigen
            DataGridView.RefreshData()
        Else
            'Fehlermeldung ausgeben
            MsgBox(wb_Rezept_Shared.ErrorText, MsgBoxStyle.Exclamation, "Rezeptgruppe anlegen")
        End If
    End Sub

    Private Sub BtnRezeptGruppeLoeschen_Click(sender As Object, e As EventArgs) Handles BtnRezeptGruppeLoeschen.Click
        'Aktuellen Datensatz löschen
        Dim RezeptGruppenNummer As Integer = DataGridView.iField("II_ItemID")
        ' vorher Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        If wb_Rezept_Shared.DeleteRezeptGruppe(RezeptGruppenNummer) Then
            'Grid aktualisieren
            DataGridView.RefreshData()
        Else
            'Fehlermeldung ausgeben
            MsgBox(wb_Rezept_Shared.ErrorText, MsgBoxStyle.Exclamation, "Rezeptgruppe löschen")
        End If
    End Sub
End Class


