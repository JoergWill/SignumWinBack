Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Lieferung
    Inherits DockContent

    Const COLMenge = 4
    Const COLVerbr = 5
    Const COLStats = 7

    Private Sub wb_Rohstoffe_Lieferung_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"Nr", "Datum", "Lieferant", "&Bemerkung", "Menge", "Verbraucht", "Charge", "Status"}
        For Each sName In sColNames
            LagerDataGridView.ColNames.Add(sName)
        Next


        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo


        'Beim ersten Aufruf wird der aktuelle Rohstoff angezeigt. Sonst wird beim Öffnen des Detail-Info-Fensters
        'der Inhalt der Textfelder gelöscht !!
        If RohStoff IsNot Nothing Then
            DetailInfo(sender)
        End If

    End Sub

    Private Sub wb_Rohstoffe_Lieferung_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler wb_Rohstoffe_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    ''' <summary>
    ''' Anzeige der Rohstoff-Details.
    ''' Wird aufgerufen durch Event eListe_Click(). Aktualisiert die Liste der Lieferungen
    ''' und die Textfelder (Bilanzmenge, Gebindegröße...)
    ''' </summary>
    Private Sub DetailInfo(sender)
        'Gebindegröße
        tbGebindeGroesse.Text = RohStoff.GebindeGroesse

        'Lagerbestand und Mindestmenge
        tbBilanzmenge.Text = RohStoff.Bilanzmenge
        tbMindestMenge.Text = RohStoff.MindestMenge
        'wenn die Mindestmenge unterschritten ist, rot markieren
        If RohStoff.MindestmengeUnterschritten Then
            tbBilanzmenge.BackColor = Drawing.Color.Red
        Else
            tbBilanzmenge.BackColor = Drawing.Color.LightGray
        End If

        'DataGrid füllen
        LagerDataGridView.LoadData(wb_Functions.SetParams(wb_Sql_Selects.sqlRohstoffLager, RohStoff.Lagerort), "RohstoffLieferungen")
        'Spalten formatieren
        LagerDataGridView.Columns(COLMenge).DefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleRight
        LagerDataGridView.Columns(COLVerbr).DefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleRight
        LagerDataGridView.Columns(COLStats).DefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter



    End Sub

    Private Sub LagerDataGridView_CellFormatting(sender As Object, e As Windows.Forms.DataGridViewCellFormattingEventArgs) Handles LagerDataGridView.CellFormatting
        'TODO DBNull abfrangen (Menge Null setzen ...)
        Try
            If e.Value IsNot Nothing Then
                Select Case e.ColumnIndex

                    Case COLMenge, COLVerbr
                        e.Value = wb_Functions.FormatStr(e.Value, 3)

                    Case COLStats
                        Select Case e.Value
                            Case "0"
                                e.Value = "-"
                            Case "1"
                                e.Value = "Gebucht"
                            Case "2"
                                e.Value = "Aktiv"
                            Case "3"
                                e.Value = "Verbraucht"
                        End Select

                End Select
            End If
        Catch
        End Try
    End Sub

    Private Sub LagerDataGridView_SizeChanged(sender As Object, e As EventArgs) Handles LagerDataGridView.SizeChanged
        If LagerDataGridView IsNot Nothing Then
            If LagerDataGridView.ColumnCount > 5 Then

                Dim x As Integer = LagerDataGridView.Left
                For i = 0 To 4
                    x += LagerDataGridView.Columns(i).Width
                Next

                tbBilanzmenge.Left = x
            End If
        End If

    End Sub
End Class