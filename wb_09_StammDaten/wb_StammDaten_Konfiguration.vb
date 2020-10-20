Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_StammDaten_Konfiguration
    Inherits DockContent

    Const ColBez = 0    'Spalte Bezeichnung
    Const ColWrt = 2    'Spalte Wert
    Const ColKmt = 3    'Spalte Kommentar

    Private Sub wb_Gruppen_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten, die mit + beginnen sind editierbar
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"Bezeichnung", "", "+Wert", "&Kommentar"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "KF_Kommentar"

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKonfiguration, "%"), "WinBackKonfig")

        'Spalte Wert zentriert darstellen
        DataGridView.Columns(ColBez).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridView.Columns(ColBez).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

        'Default Spaltenbreite für Bezeichnung und Wert
        DataGridView.Columns(ColBez).Width = 250
        DataGridView.Columns(ColWrt).Width = 200

        'Multi-Select nicht zulassen
        DataGridView.MultiSelect = False

    End Sub

    Private Sub wb_StammDaten_ArtRohGruppen_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Layout sichern
        DataGridView.SaveToDisk("WinBackKonfig")
    End Sub

    Private Sub DataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub DataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub DataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub DataGridView_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)

    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Me.Close()
    End Sub
End Class


