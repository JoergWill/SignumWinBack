Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rezept_Liste
    Inherits DockContent

    'Event Form wird geladen
    Private Sub wb_Rezept_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "Nummer", "&Name", "Variante"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "RZ_Bezeichnung"
        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlRezeptListe, "RezeptListe")

        AddHandler wb_Rezept_Shared.eEdit_Leave, AddressOf SaveData
    End Sub

    Public Sub RefreshData()
        'Daten neu einlesen
        DataGridView.RefreshData()
    End Sub

    'Event Form wird geschlossen
    Private Sub wb_Rezept_Liste_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Layout sichern
        DataGridView.SaveToDisk("RezeptListe")
    End Sub

    'Datensatz in Datenbank sichern
    Private Sub SaveData()
        'Daten in Datenbank sichern
        If wb_Rezept_Shared.Rezept.SaveData(DataGridView) Then
            DataGridView.UpdateDataBase()
        End If
    End Sub

    Private Sub DataGridView_HasChanged(sender As Object, e As EventArgs) Handles DataGridView.HasChanged
        wb_Rezept_Shared.Rezept.LoadData(DataGridView)
        wb_Rezept_Shared.Liste_Click(Nothing)
    End Sub

    Private Sub DataGridView_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView.DoubleClick
        Me.Cursor = Cursors.WaitCursor
        'Beim Erzeugen des Fensters werden die Daten aus der Datenbank gelesen
        Dim Rezeptur As New wb_Rezept_Rezeptur(wb_Rezept_Shared.Rezept.RezeptNr, wb_Rezept_Shared.Rezept.Variante)
        Rezeptur.Show()
        Me.Cursor = Cursors.Default
    End Sub
End Class