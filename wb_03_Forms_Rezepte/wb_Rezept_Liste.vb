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

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlRezeptListe, "RezeptListe")

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

    Private Sub DataGridView_HasChanged(sender As Object, e As EventArgs) Handles DataGridView.HasChanged
        wb_Rezept_Shared.aktRzNr = DataGridView.iField("RZ_Nr")
        wb_Rezept_Shared.aktRzNummer = DataGridView.Field("RZ_Nr_AlNum")
        wb_Rezept_Shared.aktRzName = DataGridView.Field("RZ_Bezeichnung")
        wb_Rezept_Shared.aktRzKommentar = DataGridView.Field("RZ_Kommentar")
        wb_Rezept_Shared.aktRzGewicht = DataGridView.Field("RZ_Gewicht")
        wb_Rezept_Shared.aktRzVariante = DataGridView.iField("RZ_Variante_Nr")
        wb_Rezept_Shared.aktRzLinienGrp = DataGridView.iField("RZ_Liniengruppe")

        wb_Rezept_Shared.aktChangeNr = DataGridView.iField("RZ_Aenderung_Nr")
        wb_Rezept_Shared.aktChangeDatum = DataGridView.Field("RZ_Aenderung_Datum")
        wb_Rezept_Shared.aktChangeName = DataGridView.Field("RZ_Aenderung_Name")

        wb_Rezept_Shared.Liste_Click(Nothing)
    End Sub

    Private Sub DataGridView_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView.DoubleClick
        Me.Cursor = Cursors.WaitCursor
        'Beim Erzeugen des Fensters werden die Daten aus der Datenbank gelesen
        Dim Rezeptur As New wb_Rezept_Rezeptur(wb_Rezept_Shared.aktRzNr, wb_Rezept_Shared.aktRzVariante)
        Rezeptur.Show()
        Me.Cursor = Cursors.Default
    End Sub
End Class