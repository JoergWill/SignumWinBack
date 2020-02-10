Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_StammDaten_Allergene
    Inherits DockContent

    Const ColBez = 2        'Spalte Bezeichnung
    Const ColKurzBez = 3    'Spalte Kurz-Bezeichnung 
    Const ColGruppe = 4     'Spalte Gruppe
    Const ColVerw = 7       'Flag Auswerten J/N

    Private _RowIndex As Integer = wb_Global.UNDEFINED

    Private Sub wb_Allergen_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten, die mit + beginnen sind editierbar
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "", "&Bezeichnung", "Kurzname", "Gruppe", "", "", "J/N"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "KT_Bezeichnung"

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlKompTyp301, "Allergene")

        'Flag Verwendet zentriert darstellen
        DataGridView.Columns(ColVerw).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView.Columns(ColVerw).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        'Default Spaltenbreite Flag Verwendet
        DataGridView.Columns(ColVerw).Width = 50

        'Kein Multi-Select
        DataGridView.MultiSelect = False
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
        If (e.RowIndex = _RowIndex) And (e.ColumnIndex >= ColVerw) Then
            If DataGridView.CurrentCell.FormattedValue = "X" Then
                DataGridView.CurrentCell.Value = ""
            Else
                DataGridView.CurrentCell.Value = "X"
            End If
        End If
        _RowIndex = e.RowIndex
    End Sub

    Private Sub wb_StammDaten_Allergene_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Layout sichern
        DataGridView.SaveToDisk("Allergene")
    End Sub

    ''' <summary>
    ''' Übersetzung der Bezeichnung in die entsprechende Landessprache
    ''' Wenn keine Übersetzung in der Texte-Tabelle vorhanden ist, wird der Original-Text ohne Klammern ausgegeben
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView.CellFormatting
        If (e.ColumnIndex <= ColGruppe) Then
            Try
                e.Value = wb_Language.TextFilter(e.Value)
            Catch
            End Try
        End If
    End Sub
End Class


