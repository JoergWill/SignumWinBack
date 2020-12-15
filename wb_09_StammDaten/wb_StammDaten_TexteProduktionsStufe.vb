Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_StammDaten_TexteProduktionsStufe
    Inherits DockContent

    Const ColNr = 0    'Spalte Nummer
    Const ColBez = 1   'Spalte Bezeichnung

    Private Sub wb_Gruppen_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten, die mit + beginnen sind editierbar
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "+&Text"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "IP_Wert4str"

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlTexteProdStufe, "TexteProdStufe")

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
        DataGridView.SaveToDisk("TexteProdStufe")
        'Daten aktualisieren
        wb_Rezept_Shared.Reload()
    End Sub
End Class


