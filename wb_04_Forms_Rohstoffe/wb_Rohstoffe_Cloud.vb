Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking
Imports System.Windows.Forms

Public Class wb_Rohstoffe_Cloud
    Inherits DockContent

    Dim sColNames As New List(Of String)
    Dim nwt As New wb_nwtCl_WinBack(wb_Credentials.WinBackCloud_Pass, wb_Credentials.WinBackCloud_Url)
    Dim dl As New wb_nwtCl_DatenLink(wb_Credentials.Datenlink_PAT, wb_Credentials.Datenlink_CAT, wb_Credentials.Datenlink_Url)

    Dim WithEvents CloudResultGrid As wb_ArrayGridViewNwt
    Dim WithEvents nwtGrid As wb_ArrayGridViewKomponParam301

    Private Sub wb_Rohstoffe_Cloud_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo
    End Sub

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Wb_TabControl.HideTabs = True
        'Hilfetexte
        lblHilfeText.Text = "Suchen über WinBack oder Datenlink verknüpft den Rohstoff mit dem entsprechenden Eintrag in der Cloud." & vbCrLf & vbCrLf &
                            "Die Nährwertdaten und Allergen-Info werden automatisch importiert und anschliessend regelmäßig im Hintergrund aktualisiert." &
                            "Für beide Dienste sind Anmeldeinformationen erforderlich" & vbCrLf & vbCrLf &
                            "Die Suche erfolgt nach Bezeichnung und Hersteller/Lieferant. Das Feld Lieferant kann frei gelassen werden"
    End Sub

    ''' <summary>
    ''' Zeigt Rohstoffnummer und Rohstoffname an. 
    ''' Wenn schon eine Cloud-ID vorhanden ist, wird auf die Tab-Page Löschen/Aktualisieren umgeschaltet
    ''' Ist eine Rezeptur mit dem Rohstoff verknüpft, werden die Chargen-Größen und Rezeptnummer und Bezeichnung angezeigt.
    ''' </summary>
    Private Sub DetailInfo()
        'Rohstoff-Nummer
        tRohstoffNummer.Text = wb_Rohstoffe_Shared.RohStoff.Nummer
        'Rohstoff-Bezeichnung
        tRohstoffName.Text = wb_Rohstoffe_Shared.RohStoff.Bezeichnung
        'ID in der Cloud
        tCloudID.Text = wb_Rohstoffe_Shared.RohStoff.MatchCode

        'wenn schon eine Verknüpfung vorhanden ist wird Tab-Page Verknüpfung löschen angezeigt
        If tCloudID.Text <> "" Then
            'Verknüpfung zur Cloud löschen/aktualisieren
            ChangeTab(tpCloudResult)

        ElseIf wb_Rohstoffe_Shared.RohStoff.RzNr > 0 Then
            'Rohstoff ist mit einer Rezeptur verknüpft
            ChangeTab(tpRezept)

        Else
            'Suchen Rohstoff in der Cloud
            ChangeTab(tpCloudSuchen)
        End If
    End Sub

    ''' <summary>
    ''' Anzeige der Tab-Page wechseln. Vor dem Einblenden der Seite werden die Datenfelder aktualisiert.
    ''' Enthält die Ablauf-Logik (Regeln zum Wechsel der Pages)
    ''' </summary>
    ''' <param name="NewTab"></param>
    Private Sub ChangeTab(NewTab As TabPage, Optional Parameter As Object = Nothing)
        Select Case NewTab.Name

            Case tpCloudSuchen.Name
                'Suchtext
                tSuchtextBezeichnung.Text = tRohstoffName.Text
                'Suche Rohstoff-Bezeichnung in Cloud (WinBack/Datenlink)
                Wb_TabControl.SelectedTab = tpCloudSuchen

            Case tpCloudGefunden.Name
                'Ausgabe Ergebnis
                Dim cnt As Integer = DirectCast(Parameter, wb_nwtCL).cnt
                Select Case cnt
                    Case 0
                        lblErgebnisText.Text = "Keinen passenden Rohstoff in der Cloud gefunden !"
                    Case 1
                        lblErgebnisText.Text = "Genau einen Rohstoff in der Cloud gefunden !"
                    Case Else
                        lblErgebnisText.Text = cnt & " Rohstoffe in der Cloud gefunden !"
                End Select

                'Anzeige der Ergebnisse
                If cnt > 0 Then
                    'Tabelle-Überschriften
                    sColNames.Clear()
                    sColNames.AddRange({"", "Bezeichnung", "Lieferant", "&Deklarationsbezeichnung"})
                    'Daten im Grid anzeigen
                    CloudResultGrid = New wb_ArrayGridViewNwt(DirectCast(Parameter, wb_nwtCL).getProducList, sColNames)
                    CloudResultGrid.ScrollBars = ScrollBars.Vertical
                    CloudResultGrid.BackgroundColor = Me.BackColor
                    CloudResultGrid.GridLocation(pnlNwtGrid)
                    CloudResultGrid.PerformLayout()
                    CloudResultGrid.Refresh()
                    'Anzeige Liste aller gefundenen Rohstoffe
                    Wb_TabControl.SelectedTab = tpCloudGefunden
                End If

            Case tpCloudAnzeige.Name
                'Anzeige Nährwerte des ausgewählten Rohstoffes
                Wb_TabControl.SelectedTab = tpCloudAnzeige

            Case tpCloudResult.Name
                'Anzeige Zutatenliste und Bezeichnung 
                'Löschen und Aktualisieren aus der Cloud
                Wb_TabControl.SelectedTab = tpCloudResult

            Case tpRezept.Name
                'Daten aus der Komponenten-Klasse lesen
                KompRzChargen.GetDataFromKomp(RohStoff)
                'Anzeigen der Werte
                KompRzChargen.DataValid = True
                'Rohstoff ist mit Rezeptur verknüpft
                Wb_TabControl.SelectedTab = tpRezept

        End Select
    End Sub

    ''' <summary>
    ''' Suchen Rohstoff/Lieferant in der WinBack-Cloud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCloud_Click(sender As Object, e As EventArgs) Handles btnCloud.Click
        'Anzahl der gefundenen Datensätze
        Dim cnt As Integer
        'Suche nach Rohstoff oder Rohstoff/Lieferant
        If tSuchtextLieferant.Text = "" Then
            cnt = nwt.lookupProductName(tSuchtextBezeichnung.Text)
        Else
            cnt = nwt.lookupProduct(tSuchtextBezeichnung.Text, tSuchtextLieferant.Text)
        End If

        'Tabelle-Überschriften
        sColNames.Clear()
        sColNames.AddRange({"", "Bezeichnung", "Lieferant", "&Deklarationsbezeichnung"})
        'Daten im Grid anzeigen
        CloudResultGrid = New wb_ArrayGridViewNwt(nwt.getProducList, sColNames)
        CloudResultGrid.ScrollBars = ScrollBars.Vertical
        CloudResultGrid.BackgroundColor = Me.BackColor
        CloudResultGrid.GridLocation(pnlNwtGrid)
        CloudResultGrid.PerformLayout()
        CloudResultGrid.Refresh()

        'Ergebnis der Cloud-Suche anzeigen
        ChangeTab(tpCloudGefunden, nwt)
    End Sub

    ''' <summary>
    ''' Suchen Rohstoff/Lieferant bei Datenlink
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnDatenLink_Click(sender As Object, e As EventArgs) Handles btnDatenLink.Click
        'Anzahl der gefundenen Datensätze
        Dim cnt As Integer
        'Suche nach Rohstoff
        dl.lookupProductName(tSuchtextBezeichnung.Text)

        'Ergebnis der Cloud-Suche anzeigen
        ChangeTab(tpCloudGefunden, dl)
    End Sub

    Private Sub CloudResultGrid_DoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles CloudResultGrid.CellMouseDoubleClick
        Dim idx As Integer = e.RowIndex
        Dim rid As String = DirectCast(sender, wb_ArrayGridViewNwt).Rows(idx).Cells(0).Value

        'Nährwertdaten in Objekt schreiben
        If wb_Functions.IsDatenLinkID(rid) Then
            'Datenlink
            dl.GetProductData(rid, wb_Rohstoffe_Shared.RohStoff)
        Else
            'WinBack-Cloud
            nwt.GetProductData(rid, wb_Rohstoffe_Shared.RohStoff)
        End If

        'Daten im Grid anzeigen
        If nwtGrid IsNot Nothing Then
            nwtGrid.Dispose()
        End If
        nwtGrid = New wb_ArrayGridViewKomponParam301(RohStoff.ktTyp301.NwtTabelle)
        nwtGrid.BackgroundColor = Me.BackColor
        nwtGrid.GridLocation(pnlNwt)
        nwtGrid.PerformLayout()

        'weiter zur Anzeige
        Wb_TabControl.SelectedTab = tpCloudAnzeige
    End Sub

    Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click

    End Sub

End Class