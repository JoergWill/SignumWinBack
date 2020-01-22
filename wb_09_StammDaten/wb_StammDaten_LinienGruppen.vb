Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_StammDaten_LinienGruppen
    Inherits DockContent

    Const ColLGNr = 0  'Spalte Linien-Gruppennummer
    Const ColDruckBZ = 6  'Flag Backzettel für diese Linie drucken
    Const ColDruckTZ = 7  'Flag Teigzettel für diese Linie drucken
    Const ColDruckTR = 8  'Flag Backzettel für diese Linie drucken
    Const ColSendeBZ = 9  'Flag Backzettel für diese Linie an WinBack übertragen
    Const ColSendeTZ = 10 'Flag Teigzettel für diese Linie an WinBack übertragen

    Private _RowIndex As Integer = wb_Global.UNDEFINED

    Private Sub wb_Linien_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten, die mit + beginnen sind editierbar
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"Nr", "+&Bezeichnung", "", "+Linien", "", "", "BZ Drucken", "TZ Drucken", "TR Drucken", "BZ Senden", "TZ Senden"}
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

    End Sub

    Private Sub DataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellEndEdit
        Debug.Print("End Edit " & e.ColumnIndex & "/" & e.RowIndex)
    End Sub

    Private Sub DataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellContentClick
        Debug.Print("CellContent Click " & e.ColumnIndex & "/" & e.RowIndex)
    End Sub

    Private Sub DataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellClick
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
        wb_Linien_Global.AddLinienGruppe(True)
        'neuen Datensatz anzeigen
        DataGridView.RefreshData()
    End Sub
End Class


