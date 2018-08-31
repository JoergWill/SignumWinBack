Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking
Imports System.Windows.Forms

Public Class wb_Rohstoffe_Cloud
    Inherits DockContent

    Dim sColNames As New List(Of String)
    Dim nwt As New wb_nwtCloud(wb_Credentials.WinBackCloud_Pass, wb_Credentials.WinBackCloud_Url)
    Dim WithEvents CloudResultGrid As wb_ArrayGridViewNwt

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Wb_TabControl.HideTabs = True

    End Sub

    Private Sub btnCloud_Click(sender As Object, e As EventArgs) Handles btnCloud.Click
        Dim cnt As Integer

        'Suche nach Rohstoff oder Rohstoff/Lieferant
        If tRohstoffLieferant.Text = "" Then
            cnt = nwt.lookupProductName(tRohstoffName.Text)
        Else
            cnt = nwt.lookupProduct(tRohstoffName.Text, tRohstoffLieferant.Text)
        End If

        'Ausgabe Ergebnis
        If cnt = 1 Then
            MsgBox("Genau einen Rohstoff in der Cloud gefunden !", MsgBoxStyle.Information, "Verknüpfung Cloud")
        ElseIf cnt = 0 Then
            MsgBox("Keinen Rohstoff in der Cloud gefunden !", MsgBoxStyle.Critical, "Verknüpfung Cloud")
        Else
            MsgBox(cnt & " Rohstoffe in der Cloud gefunden !", MsgBoxStyle.Information, "Verknüpfung Cloud")
        End If

        'Tabelle-Überschriften
        sColNames.Clear()
        sColNames.AddRange({"", "Bezeichnung", "Lieferant", "&Deklarationsbezeichnung"})
        'Daten im Grid anzeigen
        CloudResultGrid = New wb_ArrayGridViewNwt(nwt.getProducList, sColNames)
        CloudResultGrid.ScrollBars = ScrollBars.Vertical
        CloudResultGrid.BackgroundColor = Me.BackColor
        CloudResultGrid.GridLocation(tpCloudGefunden)
        CloudResultGrid.PerformLayout()
        CloudResultGrid.Refresh()

        'weiter zur Auswertung
        Wb_TabControl.SelectedTab = tpCloudGefunden
    End Sub

    Private Sub CloudResultGrid_DoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles CloudResultGrid.CellMouseDoubleClick
        Debug.Print(e.ColumnIndex)

        Dim idx As Integer = e.RowIndex
        Dim rid As String = DirectCast(sender, wb_ArrayGridViewNwt).Rows(idx).Cells(0).Value

        'Nährwertdaten in Objekt schreiben
        nwt.GetProductData(rid, wb_Rohstoffe_Shared.RohStoff)

    End Sub
End Class